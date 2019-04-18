using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Input;
using System.Data;
using System.ComponentModel;
using System.Xml.Serialization;

using gloSurescriptSecureMessage;
using System.Diagnostics;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using gloAuditTrail;
using UnZipFileIonic;
using System.Collections.ObjectModel;

namespace gloSurescriptSecureMessage_InBox
{
    public interface IObserver
    {
        void Update();
    }

    public class InboxAttachmentConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string returned = string.Empty;

            if (value != null)
            {
                string codes = Convert.ToString(value);

                if (!string.IsNullOrWhiteSpace(codes))
                {
                    if (codes.Contains("R"))
                    {
                        returned = "graphics\\RestrictedAttachemnt.gif";
                    }
                    else
                    {
                        returned = "graphics\\Attachment.gif";
                    }
                }
            }
            return returned;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }

    public partial class Inbox : IObserver
    {

        #region "Private & Public Variables Variables"
        DataTable dtMailPageCount = null;
        public event CDAImportEventHandler tlbCDAImport;
        public delegate void CDAImportEventHandler(string _FilePath);

        public event DMSImportEventHandler tlbDMSImport;
        public delegate void DMSImportEventHandler(string _FilePath);


        private GridViewColumnHeader _lastHeaderClicked = null;
        private ListSortDirection _lastSortDirection = ListSortDirection.Ascending;

        private MouseButtonEventHandler mbNavigationEventHandler = null;
        private DependencyPropertyChangedEventHandler visibilityEventHandler = null;

        private GridLength gridLength;

        private int CurrentPageCount = 0;
        private int nStartIndex = 0;
        private int nEndIndex = 0;
        private const int nMsgPerPage = 40;

        private RequestFrom requestFrom;
        PagingViewModel _viewMode =new PagingViewModel();

        gloSurescriptSecureMessage_InBox.MyFoldersExpandersControl oMyFoldersExpandersControl = null;
        gloSurescriptSecureMessage_InBox.MyToolBarTrayControl oMyToolBarTrayControl = null;
        gloSurescriptSecureMessage_InBox.MyMailHeaderGridControl oMyMailHeaderGridControl = null;
        gloSurescriptSecureMessage_InBox.MyInboxExpanderControl oMyInboxExpanderControl = null;

        StackPanel ostackPannel = default(StackPanel);
        TextBlock oTextBlock = default(TextBlock);
        Image oAttachmentImage = default(Image);
        ContextMenu oContextmainMenu= default(ContextMenu);
        Image oDownloadIcon = default(Image);

        Image oRestrictedIcon = default(Image);

        MenuItem mnuItemDownload = default(MenuItem);
        MenuItem mnuItemMapToPat = default(MenuItem);
        MenuItem mnuItemMapToDMS = default(MenuItem);

        #region "Multiple Provider User association"
        private DirectUserProviderAssociation PreferredProvider = null;
        #endregion

        private DispatcherTimer SendReceiveTimer;// = new DispatcherTimer();

        InBox.NewMail newWindow;

        ViewMessage oViewMessage;

        public string sNodeSelected { get; set; }

        public string strInboxFilePath { get; set; }

        private string sSelectedNode = "";
        public bool PreventLoading { get; set; }
        string FileExtension = string.Empty;
        bool UnstructuredCDA = false;
        string actualFileName = string.Empty;
        #endregion

        #region "Check Instance"

        private static Inbox frm;

        public static gloSurescriptSecureMessage_InBox.Inbox GetInstance()
        {

            try
            {
                if (frm != null)
                {

                    frm.Show();

                }
                else
                {
                    frm = new Inbox();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return frm;
        }

        public static gloSurescriptSecureMessage_InBox.Inbox CheckFormOpen()
        {
            try
            {
                if (frm == null)
                {
                    return null;
                }
                else
                {
                    return frm;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public static void CloseForm()
        {
            Application.Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        #endregion

        #region "Constructor"

        public Inbox()
        {

            this.InitializeComponent();
            SendReceiveTimer = new DispatcherTimer();// Used for automatic Send/Receive functionality

            #region "MyFoldersExpandersControl"

            //Find the Tree View Element From the User Control and Invoke the Events .. To Avoid Repetition Of Code i.e Load Inbox
            oMyFoldersExpandersControl = ((gloSurescriptSecureMessage_InBox.Inbox)(this)).myFoldersExpandersControl;
            oMyFoldersExpandersControl.FavInbox.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.FavUnReadMail.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.FavSentItems.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);


            oMyFoldersExpandersControl.DeletedItems.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.Inbox.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.Outbox.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.SentItems.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);

            oMyFoldersExpandersControl.SavingsDeletedItems.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.SavingsInbox.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.SavingsOutbox.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);
            oMyFoldersExpandersControl.SavingsSentItems.Selected += new RoutedEventHandler(MyInboxTreeView_Selected);


            oMyFoldersExpandersControl.LostFocus += new RoutedEventHandler(oMyFoldersExpandersControl_LostFocus);
            #endregion

            #region "MyToolBarTrayControl"

            oMyToolBarTrayControl = ((gloSurescriptSecureMessage_InBox.Inbox)(this)).myToolBarTrayControl;
            oMyToolBarTrayControl.DeleteButton.Click += new RoutedEventHandler(mnuToolBarButton_Click);
            oMyToolBarTrayControl.PrintButton.Click += new RoutedEventHandler(mnuToolBarButton_Click);
            oMyToolBarTrayControl.SendReceive.Click += new RoutedEventHandler(mnuToolBarButton_Click);
            oMyToolBarTrayControl.NewMail.Click += new RoutedEventHandler(mnuToolBarButton_Click);
            oMyToolBarTrayControl.ProviderInboxChangedEvent += new ProviderInboxChangedEventHandler(oMyToolBarTrayControl_ProviderInboxChangedEvent);                        

            oMyMailHeaderGridControl = ((gloSurescriptSecureMessage_InBox.Inbox)(this)).myMailHeaderGridControl;
            oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Collapsed;
            oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Collapsed;


            oMyInboxExpanderControl = ((gloSurescriptSecureMessage_InBox.Inbox)(this)).myInboxExpanderControl;
            oMyInboxExpanderControl.SearchFired += new MyInboxExpanderControl.SearchFiredEventHander(oMyInboxExpanderControl_SearchFired);
            
            this.MessagesObservable = new ObservableCollection<gloSecureMail>();
            this.MessagesView = CollectionViewSource.GetDefaultView(this.MessagesObservable);
            this.MessagesView.Filter = MessageFilter;

            #endregion

            #region "Display Patient Savings"
            if (gloSurescriptSecureMessage.SecureMessageProperties.DisplayPatientSavingsInbox)
            { this.oMyFoldersExpandersControl.mySavingsMailExpander.Visibility = Visibility.Visible; }
            else
            {
                this.oMyFoldersExpandersControl.mySavingsMailExpander.Visibility = Visibility.Collapsed;
            }
            #endregion

            #region "Multiple Provider Combobox display"
            if (SecureMessageProperties.ListUserProviderAssociation != null)
            {
                oMyToolBarTrayControl.ListOfProviders = SecureMessageProperties.ListUserProviderAssociation;
                oMyToolBarTrayControl.stkProviderInbox.Visibility = Visibility.Visible;
            }
            else
            {
                oMyToolBarTrayControl.ListOfProviders = null;
                oMyToolBarTrayControl.stkProviderInbox.Visibility = Visibility.Collapsed;
            }

            #endregion

            #region "Page Navigation Control Event"

            pagingControl1.NextPage += new RoutedEventHandler(pagingControl1_NextPage);
            pagingControl1.FirstPage += new RoutedEventHandler(pagingControl1_FirstPage);
            pagingControl1.PreviousPage += new RoutedEventHandler(pagingControl1_PreviousPage);
            pagingControl1.LastPage += new RoutedEventHandler(pagingControl1_LastPage);
            #endregion

        }

       

       

        void oMyFoldersExpandersControl_LostFocus(object sender, RoutedEventArgs e)
        {
            TreeViewItem LostFocusObject = e.OriginalSource as TreeViewItem;

            try
            {
                if (LostFocusObject != null)
                { LostFocusObject.IsSelected = false; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally { LostFocusObject = null; }

        }

        #endregion

        #region "Form Control Events"

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                GrdViewStatusCode.Width = 0;
                GrdViewStatusDescription.Width = 0;
                GrdViewTo.Width = 0;
                GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
             
                if (!oMyToolBarTrayControl.cmbProviderInbox.IsVisible && this.PreventLoading == false)
                { LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox); }
                

                _viewMode = new PagingViewModel();
                //Used for automatic Send/Receive functionality
                SendReceiveTimer.Interval = new TimeSpan(0, 3, 0);
                SendReceiveTimer.Tick += new EventHandler(SendReceiveTimer_Tick);
                SendReceiveTimer.Start();
                myListView.Focus();
                oMyFoldersExpandersControl.Inbox.Selected -= MyInboxTreeView_Selected;
                oMyFoldersExpandersControl.Inbox.IsSelected = true;
                oMyFoldersExpandersControl.Inbox.Focus();
                oMyFoldersExpandersControl.Inbox.Selected += MyInboxTreeView_Selected;

                gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection = new System.Collections.Hashtable();

            }
            catch (Exception exLoading)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exLoading.ToString(), false);
            }
        }

        private void GetPageCount(string DirectAddress,RequestFrom oRequestFrom )
        {
            clsSecureMessageDB oSecureMsg = null;
            try
            {
                #region "Get Direct Message Count"
                oSecureMsg = new clsSecureMessageDB();
                dtMailPageCount = oSecureMsg.RetrieveMailPageCount(DirectAddress, oRequestFrom);
                if (dtMailPageCount != null)
                {
                    if (dtMailPageCount.Rows.Count >= 1)
                    {
                        if ((int)dtMailPageCount.Rows[0][0] > 0)
                        {
                            pagingControl1.lblNumberOfPages.Content = dtMailPageCount.Rows[0][0];
                            pagingControl1.lblPageIndex.Content = 1;
                            _viewMode.NumberOfPages = (int)dtMailPageCount.Rows[0][0];
                            _viewMode.PageIndex = 1;
                            CurrentPageCount = 1;
                            nStartIndex = 1;
                            nEndIndex = nMsgPerPage;
                        }
                        else
                        {
                            pagingControl1.lblNumberOfPages.Content = 0;
                            pagingControl1.lblPageIndex.Content = 0;
                            _viewMode.NumberOfPages = 0;
                            _viewMode.PageIndex = 0;
                            CurrentPageCount = 0;
                            nStartIndex = 0;
                            nEndIndex = 0;
                        }

                    }
                }
                #endregion
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                //if (oSecureMsg != null)
                //{
                //    oSecureMsg = null;
                //    oSecureMsg.Dispose();
                //}
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (frm != null)
            {
                frm = null;
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {          
            if (InBox.NewMail.checkMailInstance == true)
            {
                newWindow = InBox.NewMail.GetInstance();
                newWindow.EvntGenerateCDA -= new InBox.NewMail.GenerateCDAHandler(newWindow_EvntGenerateCDA);
                newWindow.EvntGenerateCDA += new InBox.NewMail.GenerateCDAHandler(newWindow_EvntGenerateCDA);

                if (newWindow != null)
                {
                    System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(newWindow);

                    // Turn the below two line on to
                    // debug NewMail Multiple Providers ComboBox from Inbox

                    //if (SecureMessageProperties.ListUserProviderAssociation != null)
                    //{ newWindow.ListOfProviders = SecureMessageProperties.ListUserProviderAssociation; }

                    newWindow.Show();
                    newWindow.Activate();
                    newWindow.SetObserver = this;
                    oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                    oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {

        }

        #region "Auto Send/Receive functionality"

        void SendReceiveTimer_Tick(object sender, EventArgs e)
        {
            LoadNewMailCount();

            if (sNodeSelected == "Inbox")
            {
                LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
            }
        }

        #endregion

        private void myMainGridToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (myMainGridFirstColumn.ActualWidth > myMainGridFirstColumn.MinWidth)
                    gridLength = myMainGridFirstColumn.Width;

                myMainGridFirstColumn.Width = new GridLength(myMainGridFirstColumn.MinWidth);
                myFoldersExpandersControl.Visibility = Visibility.Collapsed;
                mySidebarControl.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void myMainGridToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!myMainGridFirstColumnSplitter.IsDragging)
                    myMainGridFirstColumn.Width = gridLength;

                myFoldersExpandersControl.Visibility = Visibility.Visible;
                mySidebarControl.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void myMainGridFirstColumnSplitter_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            try
            {
                if (myMainGridFirstColumn.ActualWidth <= myMainGridFirstColumn.MinWidth)
                    myMainGridToggleButton.IsChecked = true;
                else
                    myMainGridToggleButton.IsChecked = false;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void myNavigationPaneButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (mbNavigationEventHandler == null)
                {
                    mbNavigationEventHandler = new MouseButtonEventHandler(LayoutRoot_PreviewMouseLeftButtonUp);
                }
                if (visibilityEventHandler == null)
                {
                    visibilityEventHandler = new DependencyPropertyChangedEventHandler(myNavigationPaneControl_IsVisibleChanged);
                }
                if (myNavigationPaneControl.Visibility == Visibility.Collapsed)
                {
                    myNavigationPaneButton.SetValue(Button.BackgroundProperty, (Brush)MyApp.Current.Resources["MyOrangeSolidBrush"]);
                    myNavigationPaneControl.Visibility = Visibility.Visible;
                    LayoutRoot.PreviewMouseLeftButtonUp += mbNavigationEventHandler;
                    myNavigationPaneControl.IsVisibleChanged += visibilityEventHandler;
                }
                else
                {
                    hideNavigationPane();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void myNavigationPaneControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            try
            {
                if (myNavigationPaneControl.Visibility == Visibility.Collapsed)
                {
                    hideNavigationPane();
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void LayoutRoot_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (e.OriginalSource != myNavigationPaneButton
                           && e.Source != myNavigationPaneControl)
                {
                    if (myNavigationPaneControl.Visibility == Visibility.Visible)
                    {
                        hideNavigationPane();
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void myGridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
                ListSortDirection direction;

                if (headerClicked != null)
                {
                    if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                    {
                        if (headerClicked != _lastHeaderClicked)
                        {
                            direction = ListSortDirection.Ascending;
                        }
                        else
                        {
                            if (_lastSortDirection == ListSortDirection.Ascending)
                            {
                                direction = ListSortDirection.Descending;
                            }
                            else
                            {
                                direction = ListSortDirection.Ascending;
                            }
                        }

                        if (headerClicked.Column.Header is string)
                        {
                            string header = headerClicked.Column.Header as string;
                            Sort(header, direction);
                        }
                        else if (headerClicked.Column.Header is Image)
                        {
                            Image header = headerClicked.Column.Header as Image;
                            string type = header.Source.ToString();
                            if (type.Contains("attachment"))
                            {
                                Sort("Attachment", direction);
                            }
                            else if (type.Contains("mailType"))
                            {
                                Sort("Read", direction);
                            }
                            else if (type.Contains("importance"))
                            {
                                Sort("Importance", direction);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            return;
                        }

                        // Add arrow to the column header if it is a string
                        if (headerClicked.Column.Header is string)
                        {
                            if (direction == ListSortDirection.Ascending)
                            {
                                headerClicked.Column.HeaderTemplate = ((ListView)sender).Resources["MyArrowUpColumnHeaderTemplate"] as DataTemplate;
                            }
                            else
                            {
                                headerClicked.Column.HeaderTemplate = ((ListView)sender).Resources["MyArrowDownColumnHeaderTemplate"] as DataTemplate;
                            }
                        }

                        // Remove arrow from previously sorted header
                        if (_lastHeaderClicked != null && _lastHeaderClicked != headerClicked)
                        {
                            _lastHeaderClicked.Column.HeaderTemplate = null;
                        }

                        // Update sorting information
                        _lastHeaderClicked = headerClicked;
                        _lastSortDirection = direction;
                    }
                }
            }
            catch (Exception exSorting)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exSorting.ToString(), false);
            }
        }

        

        

        private void myListView_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sNodeSelected == "DeletedItems")
            { mnuDelete.Visibility = Visibility.Collapsed; }
            else
            { mnuDelete.Visibility = Visibility.Visible;}
        }
        
        

        private void myListView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            try
            {                
                if (myListView.SelectedItem != null)
                {
                    if (myListView.SelectedItems.Count > 1)
                    {
                        mnuUnRead.Visibility = System.Windows.Visibility.Visible;
                        mnuReadUnRead.Header = "Mark As Read";
                    }
                    else
                    {
                        mnuUnRead.Visibility = System.Windows.Visibility.Collapsed;

                        if (myListView.SelectedItem is gloSecureMail)
                        {
                            gloSecureMail mail = (gloSecureMail)myListView.SelectedItem;

                            if (mail.bIsRead)
                            { mnuReadUnRead.Header = "Mark As Unread"; }
                            else
                            { mnuReadUnRead.Header = "Mark As Read"; }
                        }
                    }
                }
                else
                { e.Handled = true; }
            }
            catch (Exception exContextMenuOpening)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exContextMenuOpening.ToString(), false);
            }

        }     

        private void Attachment_MouseClick(System.Object sender, MouseButtonEventArgs e)
        {

            byte[] AttachmentData = null;

            string strFileName = "";
            Int64 nAttachmentID = 0;
            String strQuery = "";
            Attachment oAttachment = null;
            string sCDAConfidentiality = "";
            byte[] pdfBytes = null;
            UnstructuredCDA = false;
            actualFileName = string.Empty;
            try
            {
                if (e.ClickCount == 2)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                    if (lstAttachments.SelectedItem != null)
                    {
                        StackPanel stac = (StackPanel)lstAttachments.SelectedItem;
                        TextBlock TBlock = (TextBlock)stac.Children[1];

                        if (TBlock != null)
                        {
                            if (Convert.ToString(TBlock.Tag) != "")
                            {
                                nAttachmentID = Convert.ToInt64(TBlock.Tag);
                            }

                            if (nAttachmentID > 0)
                            {
                                gloDatabaseLayer.DBLayer gloDatabaseLayer = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                                //
                                strQuery = "SELECT isnull(sCDAConfidentiality,'') FROM dbo.SecureMessage_Attachment WHERE nAttachmentID=" + nAttachmentID + "";
                                gloDatabaseLayer.Connect(false);
                                sCDAConfidentiality = (string)gloDatabaseLayer.ExecuteScalar_Query(strQuery);
                                //
                                if (SecureMessage.bIsAccess(sCDAConfidentiality.Trim()))
                                {
                                    strQuery = "SELECT iContent FROM dbo.SecureMessage_Attachment WHERE nAttachmentID=" + nAttachmentID + "";
                                    gloDatabaseLayer.Connect(false);
                                    AttachmentData = (Byte[])gloDatabaseLayer.ExecuteScalar_Query(strQuery);
                                    gloDatabaseLayer.Disconnect();

                                    if (AttachmentData != null)
                                    {
                                        oAttachment = new Attachment();
                                        strFileName = oAttachment.GenerateFile(AttachmentData, TBlock.Text);
                                        if (System.IO.Path.GetExtension(strFileName) == ".xml")
                                        {
                                             pdfBytes = ReadNonXMLBodyAttchement(Convert.ToString(strFileName));
                                             if (UnstructuredCDA)
                                             {
                                                 string tempfilename = System.IO.Path.Combine(gloSettings.FolderSettings.AppFolderPath, actualFileName);
                                                 if (pdfBytes != null && UnstructuredCDA == true)
                                                 {
                                                     File.WriteAllBytes(tempfilename, pdfBytes);
                                                     strFileName = tempfilename;
                                                 }
                                             }
                                        }
                                        
                                        System.Diagnostics.ProcessStartInfo startInfo = null;
                                        startInfo = new System.Diagnostics.ProcessStartInfo(strFileName);
                                        startInfo.UseShellExecute = true;
                                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                                        startInfo.CreateNoWindow = false;
                                        Process.Start(startInfo);
                                        if (startInfo != null) { startInfo = null; }
                                    }
                                }
                                else
                                {
                                //    MessageBox.Show("Restricted");
                                    MessageBox.Show("Preview Restricted: You do not have sufficient privileges to view the selected CCDA document." + Environment.NewLine + "Please contact system administrator to grant the required access.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "Preview Restricted: User do not have sufficient privileges to import the selected CCDA document" + " for attachment ID.", 0, nAttachmentID, 0, gloAuditTrail.ActivityOutCome.Success);
                                }
                                if (gloDatabaseLayer != null) { gloDatabaseLayer.Dispose(); }

                                TBlock = null;
                                stac = null;
                                //
                                sCDAConfidentiality = "";
                                //
                            }
                        }

                    }
                }
            }
            catch (Exception exAtchMoseClick)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exAtchMoseClick.ToString(), false);
            }
            finally
            {
                strFileName = null;
                AttachmentData = null;
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                if (oAttachment != null) { oAttachment.Dispose(); }

            }

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            InBox.NewMail ofrmNew = null;
            try
            {

                ofrmNew = InBox.NewMail.CheckFormOpen();
                if (ofrmNew != null)
                {
                    // System.Windows.Forms.MessageBox.Show("New message form is open. Please close form and try again.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    // ofrmNew.Activate();
                    e.Cancel = true;
                    return;
                }
                #region "MyFoldersExpandersControl"

                ExecutePreferredProvider();

                oMyFoldersExpandersControl.FavInbox.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.FavUnReadMail.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.FavSentItems.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);


                oMyFoldersExpandersControl.DeletedItems.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.Inbox.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.Outbox.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.SentItems.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);

                oMyFoldersExpandersControl.SavingsDeletedItems.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.SavingsInbox.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.SavingsOutbox.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);
                oMyFoldersExpandersControl.SavingsInbox.Selected -= new RoutedEventHandler(MyInboxTreeView_Selected);



                #endregion

                #region "MyToolBarTrayControl"


                oMyToolBarTrayControl.DeleteButton.Click -= new RoutedEventHandler(mnuToolBarButton_Click);
                oMyToolBarTrayControl.PrintButton.Click -= new RoutedEventHandler(mnuToolBarButton_Click);
                oMyToolBarTrayControl.SendReceive.Click -= new RoutedEventHandler(mnuToolBarButton_Click);
                oMyToolBarTrayControl.NewMail.Click -= new RoutedEventHandler(mnuToolBarButton_Click);

                #endregion

                #region "Other Dispose Events"

                oMyFoldersExpandersControl = null;
                oMyToolBarTrayControl = null;
                oMyMailHeaderGridControl = null;
                oMyInboxExpanderControl = null;
                ostackPannel = null;
                oTextBlock = null;
                oAttachmentImage = null;
                oContextmainMenu = null;
                oDownloadIcon = null;
                //                
                oRestrictedIcon = null;
                //
                mnuItemDownload = null;
                mnuItemMapToPat = null;
                SendReceiveTimer.Stop();
                SendReceiveTimer = null;
                _lastHeaderClicked = null;
                mbNavigationEventHandler = null;
                visibilityEventHandler = null;

                if (newWindow != null)
                { newWindow.ResetObserver(); }

                if (this.PreferredProvider != null)
                { this.PreferredProvider = null; }

                if (this.MessagesView != null)
                {
                    this.MessagesView.Filter -= MessageFilter;
                    this.MessagesView = null;
                }

                if (this.MessagesObservable != null)
                {
                    this.MessagesObservable.Clear();
                    this.MessagesObservable = null;
                }

                #endregion

                if ((gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection != null))
                {
                    if (gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.Count > 0)
                    {
                        gloSurescriptSecureMessage.ViewMessage oViewMessage = default(gloSurescriptSecureMessage.ViewMessage);
                        Hashtable _OpenedFormscollections = null;
                        _OpenedFormscollections = (Hashtable)gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.Clone();
                        foreach (DictionaryEntry Item in _OpenedFormscollections)
                        {
                            oViewMessage = (ViewMessage)Item.Value;
                            if ((oViewMessage != null))
                            {
                                oViewMessage.Close();
                            }
                        }
                        _OpenedFormscollections.Clear();
                        _OpenedFormscollections = null;
                        oViewMessage = null;
                    }
                }

                //Clear the View Message Opened Forms Collections
                gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.Clear();
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                ClearFileCache();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string sCallFrom = RequestFrom.Inbox.ToString();
            MenuItem oMenuSender = null;
            try
            {
                oMenuSender = (MenuItem)sender;
                
                oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Collapsed;
                oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Collapsed;

                if (oMenuSender != null)
                {

                    sCallFrom = sNodeSelected;

                    if (sCallFrom == "SentItems")
                    {
                        GrdViewTo.Width = 150;
                        GrdViewFrom.Width = 0;
                        GrdViewStatusCode.Width = 80;
                        GrdViewStatusDescription.Width = 100;

                        GrdViewReceived.Header = "Sent";
                        oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                        oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                    }
                    else
                    {
                        GrdViewTo.Width = 0;
                        GrdViewFrom.Width = 150;
                        GrdViewStatusCode.Width = 0;
                        GrdViewStatusDescription.Width = 0;
                        GrdViewReceived.Header = "Received";
                    }
                    if (myListView.SelectedItem != null && myListView.SelectedItem is gloSecureMail)
                    {                        
                        if (oMenuSender.Name == "mnuReadUnRead" || oMenuSender.Name =="mnuUnRead")
                        {
                            foreach (gloSecureMail secureMail in myListView.SelectedItems)
                            {                                
                                this.MarkMessageAsReadUnread(secureMail, !secureMail.bIsRead);
                            }                                      
                        }
                        else if (oMenuSender.Name == "mnuDelete")
                        {
                            if (System.Windows.MessageBox.Show("Are you sure you want to delete this message ? ", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                            {
                                foreach (gloSecureMail secureMail in myListView.SelectedItems)
                                {
                                    this.MarkMessageAsDeleted(secureMail);
                                }
                            }                                                       
                        }
                        else if (oMenuSender.Name == "mnuPrint")
                        {
                            SecureMessage oSecureMessage = new SecureMessage();

                            foreach (gloSecureMail secureMail in myListView.SelectedItems)
                            {
                                if (myListView.SelectedItems.Count > 0)
                                { oSecureMessage.PrintReport(Convert.ToString(secureMail.nSecureMessageInboxID), false); }
                                else
                                { oSecureMessage.PrintReport(Convert.ToString(secureMail.nSecureMessageInboxID)); }
                            }       
                                                        
                            if (oSecureMessage != null) { oSecureMessage.Dispose(); }

                        }

                        if (oMenuSender.Name != "mnuPrint")
                        {
                            if (sCallFrom == null)
                            {
                                sCallFrom = "Inbox";
                            }
                            switch (sCallFrom)
                            {
                                case "PatientSavingsOutbox":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsOutbox);
                                    sNodeSelected = RequestFrom.PatientSavingsOutbox.ToString();
                                    requestFrom = RequestFrom.PatientSavingsOutbox;
                                    sSelectedNode = "Outbox";
                                    break;
                                case "PatientSavingsSentItems":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsSentItems);
                                    sNodeSelected = RequestFrom.PatientSavingsSentItems.ToString();
                                    requestFrom = RequestFrom.PatientSavingsSentItems;
                                    GrdViewStatusCode.Width = 80;
                                    GrdViewStatusDescription.Width = 100;
                                    GrdViewTo.Width = 150;
                                    GrdViewFrom.Width = 0;
                                    GrdViewReceived.Header = "Sent";
                                    oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                                    oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                                    sSelectedNode = "Sent Items";
                                    break;
                                case "PatientSavingsDeletedItems":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsDeletedItems);
                                    sNodeSelected = RequestFrom.PatientSavingsDeletedItems.ToString();
                                    requestFrom = RequestFrom.PatientSavingsDeletedItems;
                                    sSelectedNode = "Deleted Items";
                                    break;
                                case "PatientSavingsInbox":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsInbox);
                                    sNodeSelected = RequestFrom.PatientSavingsInbox.ToString();
                                    requestFrom = RequestFrom.PatientSavingsInbox;
                                    sSelectedNode = "Inbox";
                                    break;
                                //case "Inbox":
                                //    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                                //    break;
                                //case "UnReadMail":
                                //    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.UnReadMail);
                                //    break;
                                case "SentItems":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                                    GrdViewStatusCode.Width = 80;
                                    GrdViewStatusDescription.Width = 100;
                                    GrdViewTo.Width = 150;
                                    GrdViewFrom.Width = 0;
                                    GrdViewReceived.Header = "Sent";
                                    oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                                    oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                                    break;
                                case "DeletedItems":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.DeletedItems);
                                    break;
                                case "Outbox":
                                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.OutBox);
                                    break;


                            }
                        }
                    }

                }

            }
            catch (Exception exMnuClick)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exMnuClick.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }                
            }
        }

        

        private void MyInboxTreeView_Selected(object sender, RoutedEventArgs e)
        {
            try
            {
                oMyToolBarTrayControl.DeleteButton.Visibility = Visibility.Visible ;
                oMyToolBarTrayControl.pnlDelete.Visibility = Visibility.Visible;
                GrdViewStatusCode.Width = 0;
                GrdViewStatusDescription.Width = 0;
                GrdViewTo.Width = 0;
                GrdViewFrom.Width = 150;
                GrdViewReceived.Header = "Received";
                oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Collapsed;
                oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Collapsed;
                GrdDelegatedUser.Width = 0;

                switch ((e.OriginalSource as TreeViewItem).Name)
                {
                    case "SavingsOutbox":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsOutbox);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsOutbox);
                        sNodeSelected = RequestFrom.PatientSavingsOutbox.ToString();
                        requestFrom = RequestFrom.PatientSavingsOutbox;
                        sSelectedNode = "Outbox";
                        break;
                    case "SavingsSentItems":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsSentItems);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsSentItems);
                        sNodeSelected = RequestFrom.PatientSavingsSentItems.ToString();
                        requestFrom = RequestFrom.PatientSavingsSentItems;
                        GrdViewStatusCode.Width = 80;
                        GrdViewStatusDescription.Width = 100;
                        GrdViewTo.Width = 150;
                        GrdViewFrom.Width = 0;
                        GrdViewReceived.Header = "Sent";
                        oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                        oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                        sSelectedNode = "Sent Items";
                        break;
                    case "SavingsDeletedItems":
                        oMyToolBarTrayControl.DeleteButton.Visibility = Visibility.Collapsed;
                        oMyToolBarTrayControl.pnlDelete.Visibility = Visibility.Collapsed;
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsDeletedItems);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsDeletedItems);
                        sNodeSelected = RequestFrom.PatientSavingsDeletedItems.ToString();
                        requestFrom = RequestFrom.PatientSavingsDeletedItems;
                        sSelectedNode = "Deleted Items";
                        break;
                    case "SavingsInbox":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsInbox);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.PatientSavingsInbox);
                        sNodeSelected = RequestFrom.PatientSavingsInbox.ToString();
                        requestFrom = RequestFrom.PatientSavingsInbox;
                        sSelectedNode = "Inbox";
                        break;
                    case "FavInbox":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                        sNodeSelected = RequestFrom.Inbox.ToString();
                        requestFrom = RequestFrom.Inbox;
                        sSelectedNode = "Inbox";
                        break;
                    case "FavUnReadMail":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.UnReadMail);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.UnReadMail);
                        sNodeSelected = RequestFrom.UnReadMail.ToString();
                        requestFrom = RequestFrom.UnReadMail;
                        sSelectedNode = "Unread Mail";
                        break;
                    case "FavSentItems":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                        sNodeSelected = RequestFrom.SentItems.ToString();
                        requestFrom = RequestFrom.SentItems;
                        GrdViewStatusCode.Width = 80;
                        GrdViewStatusDescription.Width = 100;
                        GrdViewTo.Width = 150;
                        GrdViewFrom.Width = 0;
                        GrdViewReceived.Header = "Sent";
                        oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                        oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                        sSelectedNode = "Sent Items";
                        break;
                    case "DeletedItems":
                        oMyToolBarTrayControl.DeleteButton.Visibility = Visibility.Collapsed;
                        oMyToolBarTrayControl.pnlDelete.Visibility = Visibility.Collapsed;
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.DeletedItems);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.DeletedItems);
                        sNodeSelected = RequestFrom.DeletedItems.ToString();
                        requestFrom = RequestFrom.DeletedItems;
                        sSelectedNode = "Deleted Items";
                        break;
                    case "Inbox":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                        sNodeSelected = RequestFrom.Inbox.ToString();
                        requestFrom = RequestFrom.Inbox;
                        sSelectedNode = "Inbox";
                        break;
                    case "Outbox":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.OutBox);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.OutBox);
                        sNodeSelected = RequestFrom.OutBox.ToString();
                        requestFrom = RequestFrom.OutBox;
                        sSelectedNode = "Outbox";
                        break;
                    case "SentItems":
                        GetPageCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                        LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                        sNodeSelected = RequestFrom.SentItems.ToString();
                        requestFrom = RequestFrom.SentItems;
                        GrdViewStatusCode.Width = 80;
                        GrdViewStatusDescription.Width = 100;
                        GrdViewTo.Width = 150;
                        GrdViewFrom.Width = 0;
                        GrdViewReceived.Header = "Sent";
                        oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                        oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                        sSelectedNode = "Sent Items";
                        GrdDelegatedUser.Width = 100;
                        break;

                }
                oMyInboxExpanderControl.txtSelectedNode.Text = sSelectedNode;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void mnuToolBarButton_Click(object sender, RoutedEventArgs e)
        {
            Button oMenuSender = null;
            try
            {

                GrdViewReceived.Header = "Received";
                oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Collapsed;
                oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Collapsed;

                oMenuSender = (Button)sender;               
                switch (oMenuSender.Name.Trim())
                {
                    case "NewMail":
                        newWindow = InBox.NewMail.GetInstance();

                        newWindow.EvntGenerateCDA += new InBox.NewMail.GenerateCDAHandler(newWindow_EvntGenerateCDA);

                        if (newWindow != null)
                        {
                            System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(newWindow);

                            // Turn the below two line on to
                            // debug NewMail Multiple Providers ComboBox from Inbox

                            //if (SecureMessageProperties.ListUserProviderAssociation != null)
                            //{ newWindow.ListOfProviders = SecureMessageProperties.ListUserProviderAssociation; }
                            InBox.NewMail.checkMailInstance = true;
                            newWindow.Show();
                            newWindow.Activate();
                            newWindow.SetObserver = this;
                            oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                            oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;
                        }
                        break;
                    case "DeleteButton":
                        if ((System.Windows.MessageBox.Show("Are you sure you want to delete this message ? ", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes))
                        {
                            using (clsSecureMessageDB oclsSecureMessageDB = new clsSecureMessageDB())
                            {
                                foreach (gloSecureMail secureMail in myListView.SelectedItems)
                                {
                                    this.MarkMessageAsDeleted(secureMail);
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage , ActivityType.Delete , "Secure Message with subject : '"+ Convert.ToString(secureMail.Subject) + "' deleted"  , 0, 0, 0, ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloEMR,true );
                                }
                            }                            
                        }                        
                        break;
                    case "PrintButton":                        
                        using (SecureMessage secureMessage = new SecureMessage())
                        {
                            foreach (gloSecureMail Mail in myListView.SelectedItems)
                            {
                                if (myListView.SelectedItems.Count > 0)
                                {
                                    secureMessage.PrintReport(Convert.ToString(Mail.nSecureMessageInboxID), false);
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage, ActivityType.Delete, "Secure Message with subject : '" + Convert.ToString(Mail.Subject) + "' printed", 0, 0, 0, ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                                }
                                else
                                {
                                    secureMessage.PrintReport(Convert.ToString(Mail.nSecureMessageInboxID));
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage, ActivityType.Delete, "Secure Message with subject : '" + Convert.ToString(Mail.Subject) + "' printed", 0, 0, 0, ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                                }
                            }
                        }

                        break;
                    case "SendReceive":                        
                        SendReceive();
                        switch (sNodeSelected)
                        {
                            case "Inbox":
                                GrdViewTo.Width = 0;
                                LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                                break;
                            case "UnReadMail":
                                GrdViewTo.Width = 0;
                                LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.UnReadMail);
                                break;
                            case "SentItems":
                                LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                                //GrdViewStatusCode.Width = 80;
                                //GrdViewStatusDescription.Width = 100;
                                //GrdViewTo.Width = 150;
                                GrdViewFrom.Width = 0;
                                GrdViewReceived.Header = "Sent";
                                oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                                oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;

                                break;
                            case "DeletedItems":
                                GrdViewTo.Width = 0;
                                LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.DeletedItems);
                                break;
                            case "Outbox":
                                GrdViewTo.Width = 0;
                                LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.OutBox);
                                break;
                        }
                        break;
                }
                
                if (oMenuSender.Name.Trim() == "DeleteButton")
                {
                    switch (sNodeSelected)
                    {
                        case "Inbox":
                            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                            break;
                        case "UnReadMail":
                            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.UnReadMail);
                            break;
                        case "SentItems":
                            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);
                            GrdViewStatusCode.Width = 80;
                            GrdViewStatusDescription.Width = 100;
                            GrdViewTo.Width = 150;
                            GrdViewFrom.Width = 0;
                            GrdViewReceived.Header = "Sent";
                            oMyMailHeaderGridControl.StkPnlStatusCode.Visibility = System.Windows.Visibility.Visible;
                            oMyMailHeaderGridControl.StkPnlStatusDesc.Visibility = System.Windows.Visibility.Visible;

                            break;
                        case "DeletedItems":
                            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.DeletedItems);
                            break;
                        case "Outbox":
                            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.OutBox);
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                oMenuSender = null;
            }
        }
    
        #endregion

        #region "Private & Public Methods"

        #region "IObserver implementation. Used to update Sent Items from NewMail."

        public void Update()
        {

            switch (sNodeSelected)
            {
                case "Inbox":
                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox);
                    break;
                case "UnReadMail":
                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.UnReadMail);
                    break;
                case "SentItems":
                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.SentItems);

                    break;
                case "DeletedItems":
                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.DeletedItems);
                    break;
                case "Outbox":
                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.OutBox);
                    break;
            }
            //oMyInboxExpanderControl.txtSelectedNode.Text = sSelectedNode;
        }
        #endregion

        private void LoadNewMailCount()
        {
            clsSecureMessageDB oSecureMsg = null;            
            Int32[] nNewMailCount = null;
            try
            {
                oSecureMsg = new clsSecureMessageDB();
                nNewMailCount = oSecureMsg.GetUnReadMailCount(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress);


                oMyFoldersExpandersControl.txtFavInbox.Text = nNewMailCount[0].ToString();
                oMyFoldersExpandersControl.txtInboxCount.Text = nNewMailCount[0].ToString();
                oMyFoldersExpandersControl.txtSavingsInboxCount.Text = nNewMailCount[1].ToString();
                txtLeftNavInboxCount.Text = nNewMailCount[0].ToString();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            finally
            {
                if (oSecureMsg != null)
                {
                    oSecureMsg.Dispose();
                    oSecureMsg = null;
                }
            }
        }       

        private void LoadMails(string DirectAddress, RequestFrom oRequestFrom)
        {
           
            clsSecureMessageDB oSecureMsg = null;
            DataSet dsInbox = null;
            DataSet dsInboxAndMailCount = null;
            DataTable dtInbox = null;
            List<gloSecureMail> lstGloSecureMail = new List<gloSecureMail>();            
            Int64 nSelectedMessageID = 0;
            Int32[] nNewMailCount = null;
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                
                oSecureMsg = new clsSecureMessageDB();
                dsInboxAndMailCount = new DataSet();
                dsInboxAndMailCount = oSecureMsg.RetrieveMails(DirectAddress, oRequestFrom,nStartIndex,nEndIndex);

                if (myListView.SelectedItem != null && myListView.SelectedItem is gloSecureMail)
                { nSelectedMessageID = ((gloSecureMail)myListView.SelectedValue).nSecureMessageInboxID; }

                this.MessagesObservable.Clear();                                
               
                dtInbox = dsInboxAndMailCount.Tables[0];
                nNewMailCount = new int[2];
                if (dsInboxAndMailCount.Tables[1].Rows.Count > 0)
                { nNewMailCount[0] = Convert.ToInt32(dsInboxAndMailCount.Tables[1].Rows[0][0]); }
                else
                { nNewMailCount[0] = 0; }

                if (dsInboxAndMailCount.Tables[2].Rows.Count > 0)
                { nNewMailCount[1] = Convert.ToInt32(dsInboxAndMailCount.Tables[2].Rows[0][0]); }
                else
                { nNewMailCount[1] = 0; }

                

                lstGloSecureMail = dsInboxAndMailCount.Tables[0].AsEnumerable().Select(p => new gloSecureMail()
                {
                    nSecureMessageInboxID = Convert.ToInt64(p["nSecureMessageInboxID"]),
                    RowNo = Convert.ToString(p["RowNo"]),
                    bIsRead = Convert.ToBoolean(p["bIsRead"]),
                    nNoOfAttachments = Convert.ToString(p["nNoOfAttachments"]),
                    From = Convert.ToString(p["From"]),
                    sTo = Convert.ToString(p["sTo"]),
                    Subject = Convert.ToString(p["Subject"]),
                    Received = Convert.ToString(p["Received"]),
                    sMessageBody = Convert.ToString(p["sMessageBody"]),
                    StatusCode = Convert.ToString(p["StatusCode"]),
                    StatusDescription = Convert.ToString(p["StatusDescription"]),
                    DocumentName = Convert.ToString(p["sDocumentName"]),
                    AttachmentID = Convert.ToString(p["nAttachmentID"]),
                    nUseCase = Convert.ToString(p["nUseCase"]),
                    EMailName = Convert.ToString(p["EMailName"]),
                    DelegatedUser = Convert.ToString(p["sDelegatedUser"]),
                    dtSendReceiveDateTime = Convert.ToString(p["dtSendReceiveDateTime"]),
                    noOfRAttachements = Convert.ToInt32(p["NumberOfRAttachments"]),
                    sCDAConfidentiality = Convert.ToString(p["sCDAConfidentiality"])
                   
                }).ToList();

                foreach (gloSecureMail mail in lstGloSecureMail)
                {
                    this.MessagesObservable.Add(mail);
                }

                

                myListView.DataContext = this.MessagesView;
                myListView.ItemsSource = this.MessagesView;

                oMyFoldersExpandersControl.txtFavInbox.Text = nNewMailCount[0].ToString();
                oMyFoldersExpandersControl.txtInboxCount.Text = nNewMailCount[0].ToString();
                oMyFoldersExpandersControl.txtSavingsInboxCount.Text = nNewMailCount[1].ToString();
                txtLeftNavInboxCount.Text = nNewMailCount[0].ToString();

                oMyFoldersExpandersControl.txtMailFor.Text = "Messages - " + gloSurescriptSecureMessage.SecureMessageProperties.ProviderName + "";
                oMyFoldersExpandersControl.txtSavingsMailFor.Text = "Messages - " + gloSurescriptSecureMessage.SecureMessageProperties.ProviderName + "";

                if (myListView.Items != null)
                {
                    object selectedItem = myListView.Items.OfType<gloSecureMail>().Where(p => p.nSecureMessageInboxID == nSelectedMessageID).FirstOrDefault();
                    if (selectedItem != null) { myListView.SelectedItem = selectedItem; }
                    selectedItem = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
            finally
            {

                if (dtInbox != null) { dtInbox.Dispose(); }
                if (dsInbox != null) { dsInbox.Dispose(); }
                if (oSecureMsg != null) { oSecureMsg.Dispose(); }
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            try
            {
                //Based on the labels caption the sorting will be performed . To Match the column Name 
                if (sortBy == "Sent")
                {
                    sortBy = "Received";
                }
                else if (sortBy == "To")
                {
                    sortBy = "sTo";
                }
                else if (sortBy == "Status Code")
                {
                    sortBy = "StatusCode";
                }
                else if (sortBy == "Status Description")
                {
                    sortBy = "StatusDescription";
                }
                else if (sortBy == "Attachment")
                {
                    sortBy = "nNoOfAttachments";
                }
                else if (sortBy == "Read")
                {
                    sortBy = "bIsRead";
                }

                ICollectionView DataviewSort = CollectionViewSource.GetDefaultView(myListView.ItemsSource);
                DataviewSort.SortDescriptions.Clear();
                SortDescription sd = new SortDescription(sortBy, direction);
                DataviewSort.SortDescriptions.Add(sd);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        private void hideNavigationPane()
        {
            try
            {
                myNavigationPaneButton.SetValue(Button.BackgroundProperty, (Brush)MyApp.Current.Resources["MyBrightBlueSolidBrush2"]);
                myNavigationPaneControl.Visibility = Visibility.Collapsed;
                LayoutRoot.PreviewMouseLeftButtonUp -= mbNavigationEventHandler;
                myNavigationPaneControl.IsVisibleChanged -= visibilityEventHandler;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        private static FontFamily CallibiriFontFamily = new FontFamily("Callibri");

        private void DisplayAttachments(string DocumentName, string AttachmentID, string sCDAConfidentiality = "")
        {
            try
            {

                string[] _Extension = null;
                string _FileExt = "";

                if (DocumentName != "")
                {
                    _Extension = DocumentName.Split('.');
                    if (_Extension.Length > 0)
                    {
                        _FileExt = _Extension[_Extension.Length - 1];
                    }
                }
                _Extension = null;
                if (SecureMessage.bIsAccess(sCDAConfidentiality.Trim()))
                {

                    oContextmainMenu = new ContextMenu();

                    //System.Windows.Media.Imaging.BitmapImage imgDownload = new System.Windows.Media.Imaging.BitmapImage();
                    //imgDownload.BeginInit();
                    //Uri myUri = new Uri("graphics\\Download.ico", UriKind.RelativeOrAbsolute);
                    //imgDownload.UriSource = myUri;
                    //imgDownload.EndInit();


                    oDownloadIcon = new Image();

                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDownloadBitmapImage;

                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;

                    mnuItemDownload = new MenuItem();
                    mnuItemDownload.Header = "Download";
                    mnuItemDownload.Icon = oDownloadIcon;
                    mnuItemDownload.Uid = AttachmentID;
                    mnuItemDownload.Tag = _FileExt;
                    mnuItemDownload.ToolTip = DocumentName;
                    mnuItemDownload.Click += new RoutedEventHandler(MenuDownloadItem_Click);


                    #region "Map To Patient"

                    //System.Windows.Media.Imaging.BitmapImage imgMapToPatient = new System.Windows.Media.Imaging.BitmapImage();
                    //imgMapToPatient.BeginInit();
                    //Uri myUriMapToPatient = new Uri("graphics\\MapPatient.ico", UriKind.RelativeOrAbsolute);
                    //imgMapToPatient.UriSource = myUriMapToPatient;
                    //imgMapToPatient.EndInit();


                    oDownloadIcon = new Image();
                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsPatientBitmapImage;//imgMapToPatient;
                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;

                    mnuItemMapToPat = new MenuItem();
                    mnuItemMapToPat.Header = "Import CCD-CCR-CDA files to patient";
                    mnuItemMapToPat.Icon = oDownloadIcon;
                    mnuItemMapToPat.Uid = AttachmentID;
                    mnuItemMapToPat.Tag = _FileExt;
                    mnuItemMapToPat.ToolTip = DocumentName;
                    mnuItemMapToPat.Click += new RoutedEventHandler(MenuMapToPatItem_Click);

                    #endregion
                    #region "Map To DMS"

                    //System.Windows.Media.Imaging.BitmapImage imgMapToDMS = new System.Windows.Media.Imaging.BitmapImage();
                    //imgMapToDMS.BeginInit();
                    //Uri myUriMapToDMS = new Uri("graphics\\ImportDMSFiles.ico", UriKind.RelativeOrAbsolute);
                    //imgMapToDMS.UriSource = myUriMapToDMS;
                    //imgMapToDMS.EndInit();


                    oDownloadIcon = new Image();
                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDMSBitmapImage;//imgMapToDMS;
                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;

                    mnuItemMapToDMS = new MenuItem();
                    mnuItemMapToDMS.Header = "Import selected file to DMS";
                    mnuItemMapToDMS.Icon = oDownloadIcon;
                    mnuItemMapToDMS.Uid = AttachmentID;
                    mnuItemMapToDMS.Tag = _FileExt;
                    mnuItemMapToDMS.ToolTip = DocumentName;
                    mnuItemMapToDMS.Click += new RoutedEventHandler(MenuMapToDMSItem_Click);

                    #endregion

                    oContextmainMenu.Items.Add(mnuItemDownload);
                    oContextmainMenu.Items.Add(mnuItemMapToPat);
                    oContextmainMenu.Items.Add(mnuItemMapToDMS);
                }
                else // code commented not adding anything if restricted... only message icon change is sufficient // again added code on 23112017
                {
                    //MessageBox.Show("Restricted");     
                    oContextmainMenu = new ContextMenu();                                       
                    MenuItem mnuItemRtClick = new MenuItem();
                    mnuItemRtClick.Header = "Restricted Message";
                    oRestrictedIcon = new Image();
                    oRestrictedIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsRestrictedAttachment;//imgMapToDMS;
                    oRestrictedIcon.Height = 16;
                    oRestrictedIcon.Width = 16;
                    mnuItemRtClick.Icon = oRestrictedIcon;
                    
                    mnuItemRtClick.Tag = AttachmentID;
                    mnuItemRtClick.Click += new RoutedEventHandler(mnuItemRtClick_Click);
                    oContextmainMenu.Items.Add(mnuItemRtClick);
                }
                ostackPannel = new StackPanel();
                ostackPannel.Orientation = System.Windows.Controls.Orientation.Horizontal;

                oAttachmentImage = new Image();
                oAttachmentImage.Margin = new Thickness(5, 0, 5, 0);

                if (sCDAConfidentiality.Contains("R"))
                {
                    oAttachmentImage.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsRestrictedAttachment;
                }
                else
                { 
                    oAttachmentImage.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsUnReadBitmapImage;
                }
                                        
                //oAttachmentImage.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDeleteBitmapImage;//new System.Windows.Media.Imaging.BitmapImage(new Uri("/gloSurescriptSecureMessage;component/graphics/unread.gif", UriKind.Relative));
                oAttachmentImage.Height = 14;
                oAttachmentImage.Width = 14;
                oAttachmentImage.Stretch = Stretch.Fill;
                oAttachmentImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                oAttachmentImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                ostackPannel.Children.Add(oAttachmentImage);

                oTextBlock = new TextBlock();
                oTextBlock.Margin = new Thickness(2, 4, 0, 0);
                oTextBlock.TextWrapping = TextWrapping.Wrap;
                oTextBlock.Foreground = Brushes.Black;
                oTextBlock.FontFamily = CallibiriFontFamily; //new FontFamily("Calibri");
                oTextBlock.FontSize = 12;
                oTextBlock.Text = DocumentName;
                oTextBlock.Tag = AttachmentID;
                oTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                oTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                oTextBlock.MouseLeftButtonDown -= Attachment_MouseClick;

                ostackPannel.Children.Add(oTextBlock);
                //if (SecureMessage.bIsAccess(sCDAConfidentiality.Trim())) // commented code on 23112017
                //{
                ostackPannel.ContextMenu = oContextmainMenu;
                //}
                lstAttachments.ScrollIntoView(lstAttachments);
                lstAttachments.Items.Add(ostackPannel);
                lstAttachments.SelectedIndex = 0;


            }
            catch (Exception exListBox)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exListBox.ToString(), false);
            }
            finally
            {
                oTextBlock.MouseLeftButtonDown += Attachment_MouseClick;
            }
        }

        private void mnuItemRtClick_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Preview Restricted: You do not have sufficient privileges to view the selected CCDA document." + Environment.NewLine + "Please contact system administrator to grant the required access.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
           //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Extract, "Preview Restricted: User do not have sufficient privileges to import the selected CCDA document" + " for attachment ID.", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success);
        }
        private void MenuDownloadItem_Click(object sender, RoutedEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string sCallFrom = RequestFrom.Inbox.ToString();
            byte[] AttachmentData = null;
            MenuItem oMenuSender = null;
            string strQuery = "";
            UnstructuredCDA = false;
            FileExtension = string.Empty;
            actualFileName = string.Empty;
            Microsoft.Win32.SaveFileDialog oSaveDialog = null;
            try
            {
                oMenuSender = (MenuItem)sender;

                if (oMenuSender != null)
                {

                    sCallFrom = sNodeSelected;
                    Int64 nAttachmentID = Convert.ToInt64(oMenuSender.Uid);
                    String sFileExtension = Convert.ToString(oMenuSender.Tag);
                    DataTable dt = null;
                    String DocumentName = string.Empty;
                   
                    byte[] pdfBytes = null;
                    if (nAttachmentID > 0)
                    {
                        dt = new DataTable();
                        oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                        strQuery = "SELECT iContent  FROM dbo.SecureMessage_Attachment WHERE nAttachmentID=" + nAttachmentID + "";
                        oDB.Connect(false);
                        oDB.Retrive_Query(strQuery ,out dt);
                        AttachmentData = (Byte[])oDB.ExecuteScalar_Query(strQuery);
                        //if (dt!= null && dt.Rows.Count > 0)
                        //{
                        //        AttachmentData = (Byte[])dt.Rows[0]["iContent"];
                        //        DocumentName = Convert.ToString(dt.Rows[0]["sDocumentName"]);
                        //        UnstructuredCDA = Convert.ToBoolean(dt.Rows[0]["IsUnstructuredCDA"]);
                        //}
                        oDB.Disconnect();

                        if (AttachmentData != null)
                        {
                            //Check if fileextension is xml else it is normal document not unstructured DOC
                            if (sFileExtension == "xml")
                            {
                                string tempfilename = System.IO.Path.Combine(gloSettings.FolderSettings.AppFolderPath, Convert.ToString(oMenuSender.ToolTip));
                                File.WriteAllBytes(tempfilename, AttachmentData);
                                pdfBytes = ReadNonXMLBodyAttchement(Convert.ToString(tempfilename));
                            }
                          
                            if (UnstructuredCDA)
                            {
                               
                               
                                if (pdfBytes != null)
                                {

                                      oSaveDialog = new Microsoft.Win32.SaveFileDialog();
                                      oSaveDialog.FileName = actualFileName;
                                      oSaveDialog.Filter = "Word 97-2003 Document(*.doc) |*.doc |Word Document(*.docx) |*.docx |RichText Format (*.rtf) |*.rtf |Adobe Acrobat (*.pdf) |*.pdf | Web Page(*.htm;*.html) |*.htm;*.html |Plain Text(*.txt) |*.txt| PNG (*.png)|*.png|JPEG (*.jpeg) |*.jpeg|TIFF (*.tiff)|*.tiff|GIF (*.gif)|*.gif";
                                }
                                
                            switch (FileExtension)
                            {
                                case "application/msword":
                                    oSaveDialog.FilterIndex = 1;
                                    break;
                                //case "docx":
                                //    oSaveDialog.FilterIndex = 2;
                                //    break;
                                //case "dotx":
                                //    oSaveDialog.FilterIndex = 3;
                                //    break;
                                //case "dot":
                                //    oSaveDialog.FilterIndex = 4;
                                //    break;
                                case "text/rtf":
                                    oSaveDialog.FilterIndex = 3;
                                    break;
                                case "application/pdf":
                                    oSaveDialog.FilterIndex = 4;
                                    break;
                                //case "xps":
                                //    oSaveDialog.FilterIndex = 7;
                                //    break;
                                case "text/html":
                                    oSaveDialog.FilterIndex = 5;
                                    break;
                                case "text/plain":
                                    oSaveDialog.FilterIndex = 6;
                                    break;
                                case "image/png" :
                                    oSaveDialog.FilterIndex = 7;
                                    break;
                                case "image/jpeg":
                                    oSaveDialog.FilterIndex = 8;
                                    break;
                                case "image/tiff":
                                    oSaveDialog.FilterIndex = 9;
                                    break;
                                case "image/gif":
                                    oSaveDialog.FilterIndex = 10;
                                    break;
                            
                               
                            }
                                }
                            else
                            {
                                oSaveDialog = new Microsoft.Win32.SaveFileDialog();
                                oSaveDialog.FileName = Convert.ToString(oMenuSender.ToolTip);
                                oSaveDialog.Filter = "Word 97-2003 Document(*.doc) |*.doc |Word Document(*.docx) |*.docx |Word Template(*.dotx) |*.dotx |Word 97-2003 Template(*.dot) |*.dot |RichText Format (*.rtf) |*.rtf |Adobe Acrobat (*.pdf) |*.pdf |XPS Document(*.xps) |*.xps |Web Page(*.htm;*.html) |*.htm;*.html |Plain Text(*.txt) |*.txt |Word XML Document(*.xml) |*.xml|Zip (*.zip) |*.zip ";
                                switch (sFileExtension)
                                {
                                    case "doc":
                                        oSaveDialog.FilterIndex = 1;
                                        break;
                                    case "docx":
                                        oSaveDialog.FilterIndex = 2;
                                        break;
                                    case "dotx":
                                        oSaveDialog.FilterIndex = 3;
                                        break;
                                    case "dot":
                                        oSaveDialog.FilterIndex = 4;
                                        break;
                                    case "rtf":
                                        oSaveDialog.FilterIndex = 5;
                                        break;
                                    case "pdf":
                                        oSaveDialog.FilterIndex = 6;
                                        break;
                                    case "xps":
                                        oSaveDialog.FilterIndex = 7;
                                        break;
                                    case "html":
                                        oSaveDialog.FilterIndex = 8;
                                        break;
                                    case "txt":
                                        oSaveDialog.FilterIndex = 9;
                                        break;
                                    case "xml":
                                        oSaveDialog.FilterIndex = 10;
                                        break;
                                    case "zip":
                                        oSaveDialog.FilterIndex = 11;
                                        break;
                                }

                            
                            }


                            oSaveDialog.AddExtension = true;

                            // Show save file dialog box
                            Nullable<bool> result = oSaveDialog.ShowDialog(this);

                            // Process save file dialog box results
                            if (result == true)
                            {
                                // Save the Document in the Local Path 
                                string sSelectedFileName = oSaveDialog.FileName.Trim();
                                if (sSelectedFileName != "")
                                {
                                    if (pdfBytes!=null && UnstructuredCDA == true)
                                    {
                                        File.WriteAllBytes(sSelectedFileName, pdfBytes);
                                    }
                                    else
                                    {
                                        File.WriteAllBytes(sSelectedFileName, AttachmentData);
                                    }
                                 

                                    //if (sFileExtension == "xml")
                                    //{
                                    //    ReadNonXMLBodyAttchement(Convert.ToString(sSelectedFileName));
                                    //}
                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage, ActivityType.Download, "Secure Message attachment name : '" + Convert.ToString(oMenuSender.ToolTip) + "' downloaded", 0, 0, 0, ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                                }
                            }

                        }

                    }
                    

                }

            }
            catch (Exception exMnuClick)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exMnuClick.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                AttachmentData = null;
                oMenuSender = null;
                
                oSaveDialog = null;
            }

        }
        private byte[] ReadNonXMLBodyAttchement(String FileName)
        {
            try
            {
                //bool _IsunstructuredCDA = false;
                
                gloCCDLibrary.gloCDAReader CDAReader = new gloCCDLibrary.gloCDAReader();
                String base64string = CDAReader.getNONXMLBody(FileName, ref FileExtension, ref  UnstructuredCDA,ref actualFileName);
                byte[] pdfBytes = null;
                pdfBytes = Convert.FromBase64String(base64string);
                return pdfBytes;
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void MenuMapToPatItem_Click(object sender, RoutedEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string sCallFrom = RequestFrom.Inbox.ToString();
            byte[] AttachmentData = null;
            MenuItem oMenuSender = null;
            string strQuery = "";
            // Microsoft.Win32.SaveFileDialog oSaveDialog = null;
            try
            {
                oMenuSender = (MenuItem)sender;

                if (oMenuSender != null)
                {

                    sCallFrom = sNodeSelected;
                    Int64 nAttachmentID = Convert.ToInt64(oMenuSender.Uid);
                    String sFileExtension = Convert.ToString(oMenuSender.Tag);
                    String sFileName = Convert.ToString(oMenuSender.ToolTip);

                    if (nAttachmentID > 0)
                    {
                        oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                        strQuery = "SELECT iContent FROM dbo.SecureMessage_Attachment WHERE nAttachmentID=" + nAttachmentID + "";
                        oDB.Connect(false);
                        AttachmentData = (Byte[])oDB.ExecuteScalar_Query(strQuery);
                        oDB.Disconnect();

                        if (AttachmentData != null)
                        {
                            //oSaveDialog = new Microsoft.Win32.SaveFileDialog();
                            //oSaveDialog.FileName = Convert.ToString(oMenuSender.ToolTip);
                            //oSaveDialog.Filter = "Word 97-2003 Document(*.doc) |*.doc |Word Document(*.docx) |*.docx |Word Template(*.dotx) |*.dotx |Word 97-2003 Template(*.dot) |*.dot |RichText Format (*.rtf) |*.rtf |Adobe Acrobat (*.pdf) |*.pdf |XPS Document(*.xps) |*.xps |Web Page(*.htm;*.html) |*.htm;*.html |Plain Text(*.txt) |*.txt |Word XML Document(*.xml) |*.xml|Zip (*.zip) |*.zip ";


                            if (sFileExtension == "doc" || sFileExtension == "docx" || sFileExtension == "rtf" || sFileExtension == "doc" || sFileExtension == "pdf" || sFileExtension == "XPS" || sFileExtension == "htm" || sFileExtension == "html" || sFileExtension == "txt" || sFileExtension == "xml")
                            {
                                //string sSelectedFileName = GetFileName(sFileName);//oSaveDialog.FileName.Trim();
                                if (sFileExtension == "xml")
                                {
                                  
                                    string sSelectedFileName = gloSettings.FolderSettings.AppTempFolderPath + sFileName;
                                    if (sSelectedFileName != "")
                                    {
                                        // string strFileName = GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                                        File.WriteAllBytes(sSelectedFileName, AttachmentData);
                                        tlbCDAImport(sSelectedFileName);
                                       //gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage, ActivityType.Import, "Secure Message Attachment name : '" + Convert.ToString(oMenuSender.ToolTip) + "' imported to the patient", 0, 0, 0, ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                                    }
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select a Valid CCD-CCR-CDA Files.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                }
                            }
                            else if (sFileExtension == "zip")
                            {
                                string sSelectedFileName = gloSettings.FolderSettings.AppTempFolderPath + sFileName;
                                if (sSelectedFileName != "")
                                {
                                    File.WriteAllBytes(sSelectedFileName, AttachmentData);

                                    string FinalDirectory = clsExtractFile.ExtractZipFile(sSelectedFileName);
                                    string[] Directories = Directory.GetDirectories(FinalDirectory);
                                    int flagxml = 0;

                                    if (Directories.Length == 0)
                                    {
                                        string[] filePaths = Directory.GetFiles(FinalDirectory);

                                        foreach (string file in filePaths)
                                        {
                                            if (System.IO.Path.GetExtension(file) != ".xsl")
                                            {
                                                if (System.IO.Path.GetExtension(file) == ".xml")
                                                {
                                                    flagxml = 1;
                                                    tlbCDAImport(file);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                        foreach (string Dirfile in Directories)
                                        {
                                            string[] filePaths = Directory.GetFiles(Dirfile);

                                            foreach (string file in filePaths)
                                            {
                                                if (System.IO.Path.GetExtension(file) != ".xsl")
                                                {
                                                    if (System.IO.Path.GetExtension(file) == ".xml")
                                                    {
                                                        flagxml = 1;
                                                        tlbCDAImport(file);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    if (flagxml == 0)
                                    {
                                        System.Windows.Forms.MessageBox.Show("Attachment file does not contain valid CCD-CCR-CDA Files.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                    }

                                }


                            }
                            else
                            {
                                System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select a Valid CCD-CCR-CDA Files.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            }
                        }

                    }

                }

            }
            catch (Exception exMnuClick)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exMnuClick.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                AttachmentData = null;
                oMenuSender = null;
                //oSaveDialog = null;
            }


        }
        private void MenuMapToDMSItem_Click(object sender, RoutedEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string sCallFrom = RequestFrom.Inbox.ToString();
            byte[] AttachmentData = null;
            MenuItem oMenuSender = null;
            string strQuery = "";
            byte[] pdfBytes = null;
            UnstructuredCDA = false;
            FileExtension = string.Empty;
            actualFileName = string.Empty;
            // Microsoft.Win32.SaveFileDialog oSaveDialog = null;
            try
            {
                oMenuSender = (MenuItem)sender;

                if (oMenuSender != null)
                {

                    sCallFrom = sNodeSelected;
                    Int64 nAttachmentID = Convert.ToInt64(oMenuSender.Uid);
                    String sFileExtension = Convert.ToString(oMenuSender.Tag);
                    String sFileName = Convert.ToString(oMenuSender.ToolTip);

                    if (nAttachmentID > 0)
                    {
                        oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                        strQuery = "SELECT iContent FROM dbo.SecureMessage_Attachment WHERE nAttachmentID=" + nAttachmentID + "";
                        oDB.Connect(false);
                        AttachmentData = (Byte[])oDB.ExecuteScalar_Query(strQuery);
                        oDB.Disconnect();

                        if (AttachmentData != null)
                        {
                            //oSaveDialog = new Microsoft.Win32.SaveFileDialog();
                            //oSaveDialog.FileName = Convert.ToString(oMenuSender.ToolTip);
                            //oSaveDialog.Filter = "Word 97-2003 Document(*.doc) |*.doc |Word Document(*.docx) |*.docx |Word Template(*.dotx) |*.dotx |Word 97-2003 Template(*.dot) |*.dot |RichText Format (*.rtf) |*.rtf |Adobe Acrobat (*.pdf) |*.pdf |XPS Document(*.xps) |*.xps |Web Page(*.htm;*.html) |*.htm;*.html |Plain Text(*.txt) |*.txt |Word XML Document(*.xml) |*.xml|Zip (*.zip) |*.zip ";
                            if (sFileExtension  == "xml")
                            {
                                string tempfilename = System.IO.Path.Combine(gloSettings.FolderSettings.AppFolderPath, Convert.ToString(oMenuSender.ToolTip));
                                File.WriteAllBytes(tempfilename, AttachmentData);
                                pdfBytes = ReadNonXMLBodyAttchement(Convert.ToString(tempfilename));

                            }

                            if (UnstructuredCDA == true && pdfBytes != null)
                            {
                                if (FileExtension == "application/pdf" || FileExtension == "application/msword" || FileExtension == "text/rtf" || FileExtension == "text/html" || FileExtension == "text/plain")
                                {
                                    string selectedfilename = gloSettings.FolderSettings.AppTempFolderPath + actualFileName;
                                    if (selectedfilename != "")
                                    {
                                        File.WriteAllBytes(selectedfilename, pdfBytes);
                                        tlbDMSImport(selectedfilename);
                                    }
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select a Valid Files to import into DMS.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                }
                            }
                            else
                            {
                                if (sFileExtension == "doc" || sFileExtension == "docx" || sFileExtension == "rtf" || sFileExtension == "doc" || sFileExtension == "pdf" || sFileExtension == "XPS" || sFileExtension == "htm" || sFileExtension == "html" || sFileExtension == "txt")
                                {


                                    string sSelectedFileName = gloSettings.FolderSettings.AppTempFolderPath + sFileName;
                                    if (sSelectedFileName != "")
                                    {

                                        File.WriteAllBytes(sSelectedFileName, AttachmentData);

                                        tlbDMSImport(sSelectedFileName);


                                    }
                                    // gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage, ActivityType.Download, "Secure Message Attachment name : '" + Convert.ToString(oMenuSender.ToolTip) + "' imported into the DMS", 0, 0, 0, ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select a Valid Files to import into DMS.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                }
                            }
                            
                        }

                    }
                   
                }

            }
            catch (Exception exMnuClick)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exMnuClick.ToString(), false);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                AttachmentData = null;
                oMenuSender = null;
                //oSaveDialog = null;
            }


        }

        private void ClearFileCache()
        {
            try
            {
                //Delete the XML Files From Temp In case if the application was closed by exception
                if (File.Exists(strInboxFilePath) == true)
                {
                    File.SetAttributes(strInboxFilePath, FileAttributes.Normal);
                    File.Delete(strInboxFilePath);
                }
                if (File.Exists(strInboxFilePath) == true)
                {
                    File.SetAttributes(strInboxFilePath, FileAttributes.Normal);
                    File.Delete(strInboxFilePath);
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void SendReceive()
        {
            int i = 0;
            DataSet sureScriptTables = null;
            SecureMessage objSecureMessage = null;
            List<Attachment> lstAttachment = null;
            EnumerableRowCollection AllAttachments = null;
            byte[] byteArray = null;
            XmlSerializer xs = null;
            FileStream fs = null;
            N2NMessageType objN2N = null;
            clsSecureMessageDB oclsSecureMessageDB = null;
            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                oclsSecureMessageDB = new clsSecureMessageDB();
                sureScriptTables = oclsSecureMessageDB.RetrieveSecureMessageStagging();

                if ((sureScriptTables != null))
                {
                    if (sureScriptTables.Tables["MessagesDataTable"].Rows.Count > 0)
                    {
                        EnumerableRowCollection AttachmentListDataRowCollection = null;

                        if (sureScriptTables.Tables["AttachmentsDataTable"].Rows.Count > 0)
                        {
                            AttachmentListDataRowCollection = sureScriptTables.Tables["AttachmentsDataTable"].AsEnumerable();
                        }

                        int sureScriptCount = 0;

                        while ((sureScriptCount < sureScriptTables.Tables["MessagesDataTable"].Rows.Count))
                        {
                            objSecureMessage = PopulateSecureMessage(sureScriptTables.Tables["MessagesDataTable"].Rows[sureScriptCount]);

                            if (objSecureMessage.noofAttachements > 0)
                            {
                                AllAttachments = from DataRow inner in AttachmentListDataRowCollection
                                                 where (Convert.ToInt64(inner.Field<decimal>("nSecureMessageInboxID")) == objSecureMessage.secureMessageInboxID)
                                                 select inner;
                                lstAttachment = new List<Attachment>();
                                foreach (DataRow AttachmentElement in AllAttachments)
                                {
                                    lstAttachment.Add(PopulateAttachment(AttachmentElement));
                                }
                            }
                            else
                            {
                                lstAttachment = null;
                            }

                            byteArray = SecureMessage.GenerateXML(objSecureMessage, lstAttachment);
                            byte[] Response = null;
                            string key = string.Empty;

                            gloSurescriptSecureMessage.gloDirectservice.IgloDirectClient oDirect;
                            if (gloSurescriptSecureMessage.SecureMessageProperties.IsStagingServerEnable)
                                oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc(gloSurescriptSecureMessage.SecureMessageProperties.StagingServerUrl);
                            else
                                oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc(gloSurescriptSecureMessage.SecureMessageProperties.ProductionServerUrl);
                            key = oDirect.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik");
                            Response = oDirect.PostSecureMessage(objSecureMessage.messageID, objSecureMessage.From, objSecureMessage.To, SecureMessageProperties.SPID, SecureMessageProperties.ClinicName, SecureMessageProperties.AUSID, SecureMessageProperties.SiteID, SecureMessageProperties.Location, byteArray, (gloSurescriptSecureMessage.gloDirectservice.ClsglobalMessageType)gloSurescriptSecureMessage.MessageType.Send);

                            if (Response != null)
                            {
                                string strFileName = SecureMessage.GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                                SecureMessage.ConvertBinarytoFile(Response, strFileName);

                                if (strFileName.Trim() != "")
                                {
                                    xs = new XmlSerializer(typeof(N2NMessageType));
                                    fs = new FileStream(strFileName, FileMode.Open);
                                    try
                                    {
                                        objN2N = (N2NMessageType)xs.Deserialize(fs);
                                    }
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                                        System.Windows.Forms.MessageBox.Show("Sure Script Unable to process Message", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                    }

                                    fs.Close();
                                    if (objN2N != null)
                                    {
                                        objSecureMessage = SecureMessage.ExtractXML(objN2N, objSecureMessage);

                                        objSecureMessage.messageID = oclsSecureMessageDB.InsertSureScriptMessageInDB(objSecureMessage);

                                        if (objSecureMessage.deliveryStatusDescription != "")
                                        {
                                            System.Windows.Forms.MessageBox.Show(objSecureMessage.deliveryStatusDescription, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                        }

                                    }
                                    else
                                    {
                                        objSecureMessage.deliveryStatusCode = "999";
                                        objSecureMessage.deliveryStatusDescription = "Processing";
                                        objSecureMessage.messageID = oclsSecureMessageDB.InsertSureScriptMessageInDB(objSecureMessage);
                                    }
                                }
                            }
                            else
                            {
                                objSecureMessage.deliveryStatusCode = "999";
                                objSecureMessage.deliveryStatusDescription = "Processing";
                                objSecureMessage.messageID = oclsSecureMessageDB.InsertSureScriptMessageInDB(objSecureMessage);

                            }

                            oDirect.Close();
                            oDirect = null;
                            Response = null;
                            byteArray = null;
                            sureScriptCount++;
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.SecureMessage, ActivityType.Send, "Send Secure Message", objSecureMessage.patientID, 0, 0, ActivityOutCome.Success);
                        }


                    }

                }

                i = (i + 1);
                gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.NewMessage, ActivityType.Action , "Send/Receive messages list refreshed by clicking send/receive button", 0, 0, 0, ActivityOutCome.Success,gloAuditTrail.SoftwareComponent.gloEMR   ,true );

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                if (objSecureMessage.secureMessageInboxID > 0)
                {
                    oclsSecureMessageDB.RevertDownloadStatus(objSecureMessage.secureMessageInboxID);
                }
            }
            finally
            {
                if (sureScriptTables != null)
                {
                    sureScriptTables.Dispose();
                    sureScriptTables = null;
                }

                if (objSecureMessage != null)
                {
                    objSecureMessage.Dispose();
                    objSecureMessage = null;
                }

                if (xs != null)
                {
                    xs = null;
                }

                if (fs != null)
                {
                    fs.Dispose();
                    fs = null;
                }

                if (objN2N != null)
                {
                    objN2N = null;
                }

                if (oclsSecureMessageDB != null)
                {
                    oclsSecureMessageDB.Dispose();
                    oclsSecureMessageDB = null;
                }

                if ((lstAttachment != null))
                {
                    lstAttachment.Clear();
                    lstAttachment = null;
                }

                if ((AllAttachments != null))
                {
                    AllAttachments = null;
                }

                if ((byteArray != null))
                {
                    byteArray = null;
                }
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;

            }

        }

        private Attachment PopulateAttachment(DataRow Element)
        {
            Attachment oAttachment = null;
            try
            {
                oAttachment = new Attachment();

                var _with1 = oAttachment;
                _with1.nSecureMessageInboxID = Convert.ToInt64(Element["nSecureMessageInboxID"]);
                _with1.attachmentID = Convert.ToInt64(Element["nAttachmentID"]);
                _with1.moduleName = Convert.ToInt16(Element["nModuleName"]);
                _with1.fileExtension = Convert.ToInt16(Element["nFileExtension"]);
                _with1.documentName = Convert.ToString(Element["sDocumentName"]);
                _with1.iContent = (byte[])(Element["iContent"]);

                return oAttachment;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (oAttachment != null)
                {
                    oAttachment.Dispose();
                    oAttachment = null;
                }
            }

        }

        private SecureMessage PopulateSecureMessage(DataRow Element)
        {
            SecureMessage oSecureMessage = null;
            try
            {
                oSecureMessage = new SecureMessage();

                var _with1 = oSecureMessage;
                _with1.secureMessageInboxID = Convert.ToInt64(Element["nSecureMessageInboxID"]);
                _with1.messageID = Convert.ToString(Element["sMessageID"]);
                _with1.relateMessageID = Convert.ToString(Element["sRelatesToMessageID"]);
                _with1.version = Convert.ToString(Element["sMessageVersionNo"]);
                _with1.release = Convert.ToString(Element["sMessageReleaseNo"]);
                _with1.highVersion = Convert.ToString(Element["sMessageHighestVersion"]);

                _with1.senderID = Convert.ToInt64(Element["nSenderID"]);
                _with1.receiverID = Convert.ToInt64(Element["nReceiverID"]);
                _with1.From = Convert.ToString(Element["sFrom"]);
                _with1.FromQualifier = Convert.ToString(Element["sFromQualifier"]);

                _with1.To = Convert.ToString(Element["sTo"]);
                _with1.ToQualifier = Convert.ToString(Element["sToQualifier"]);
                _with1.subject = Convert.ToString(Element["sSubject"]);
                _with1.messageBody = Convert.ToString(Element["sMessageBody"]);

                _with1.dateTimeUTC = Convert.ToString(Element["dtSendReceiveDateTime_UTC"]);
                _with1.dateTimeNormal = Convert.ToDateTime(Element["dtSendReceiveDateTime"]);
                _with1.isRead = Convert.ToInt16(Element["bIsRead"]);
                _with1.patientID = Convert.ToInt64(Element["nPatientID"]);

                _with1.noofAttachements = Convert.ToInt16(Element["nNoOfAttachments"]);
                _with1.MessageStatus = Convert.ToInt16(Element["bMessageStatus"]);
                _with1.messageType = Convert.ToInt16(Element["bMessageType"]);
                _with1.associated = Convert.ToInt16(Element["bIsAssociated"]);

                _with1.deliveryStatusCode = Convert.ToString(Element["sDeliveryStatusCode"]);
                _with1.deliveryStatusDescription = Convert.ToString(Element["sDeliveryStatusDescription"]);
                _with1.softwareVersion = Convert.ToString(Element["sSoftwareVersion"]);
                _with1.softwareProduct = Convert.ToString(Element["sSoftwareProduct"]);

                _with1.companyName = Convert.ToString(Element["sCompanyName"]);
                _with1.userName = Convert.ToString(Element["sUserName"]);
                _with1.machineName = Convert.ToString(Element["sMachineName"]);
                _with1.deleted = Convert.ToInt16(Element["bIsDeleted"]);

                _with1.noOfRAttachements = Convert.ToInt16(Element["NumberOfRAttachments"]);

                return oSecureMessage;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;

            }
            finally
            {
                if (oSecureMessage != null)
                {
                    oSecureMessage.Dispose();
                    oSecureMessage = null;
                }
            }

        }

        public static String GetFileName(String strAppPath)
        {
            try
            {                
                return gloGlobal.clsFileExtensions.NewDocumentName(strAppPath, ".xml", "MMddyyyyHHmmssffff");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString());
                return "";
            }
            finally
            {

            }
        }

        #endregion

        #region "Multiple Providers"
        //Ashish Tamhane 19-Dec-2013

        public void NavigateToMessage(Int64 SecureMessageID, string DirectAddress)
        {
            try
            {
                if (!this.MessagesObservable.Any(p => p.nSecureMessageInboxID == SecureMessageID))
                {
                    if (oMyToolBarTrayControl.ListOfProviders != null && oMyToolBarTrayControl.ListOfProviders.Any(p => p.DirectAddress.ToLower() == DirectAddress.ToLower()))
                    {
                        DirectUserProviderAssociation directUser = oMyToolBarTrayControl.ListOfProviders.FirstOrDefault(p => p.DirectAddress.ToLower() == DirectAddress.ToLower());
                        ItemCollection objectCollection = oMyToolBarTrayControl.cmbProviderInbox.Items;

                        if (objectCollection.Contains(directUser))
                        {

                            oMyToolBarTrayControl.IsChangedEventEnabled = true;
                            oMyToolBarTrayControl.cmbProviderInbox.SelectedValue = objectCollection.GetItemAt(objectCollection.IndexOf(directUser));
                            oMyToolBarTrayControl.cmbProviderInbox.SelectedIndex = objectCollection.IndexOf(directUser);
                        }
                    }
                }

                if (!this.MessagesObservable.Any(p => p.nSecureMessageInboxID == SecureMessageID))
                {
                    nStartIndex = 0;
                    nEndIndex = 40;
                    LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, RequestFrom.Inbox); 
                }
                

                if (this.MessagesObservable.Any(p => p.nSecureMessageInboxID == SecureMessageID))
                {
                    gloSecureMail mail = this.MessagesObservable.FirstOrDefault(p => p.nSecureMessageInboxID == SecureMessageID);
                    ItemCollection objectCollection = myListView.Items;

                    if (objectCollection.OfType<gloSecureMail>().Contains(mail))
                    {
                        myListView.SelectedValue = mail;
                    }
                }                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        void oMyToolBarTrayControl_ProviderInboxChangedEvent(object sender, InboxProviderChangedEventArgs InboxProviderChangedArgs)
        {
            try
            {
                if (this.IsLoaded || oMyToolBarTrayControl.IsChangedEventEnabled)
                {
                    gloSurescriptSecureMessage.SecureMessageProperties.ProviderID = InboxProviderChangedArgs.ProviderID;
                    gloSurescriptSecureMessage.SecureMessageProperties.ProviderName = InboxProviderChangedArgs.Name;
                    gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress = InboxProviderChangedArgs.DirectAddress;
                    gloSurescriptSecureMessage.SecureMessageProperties.SPID = InboxProviderChangedArgs.SSPID;
                    SetPreferredProvider(InboxProviderChangedArgs.ProviderID);
                    GetPageCount(InboxProviderChangedArgs.DirectAddress, requestFrom);
                    LoadMails(InboxProviderChangedArgs.DirectAddress, requestFrom);                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }


        }
   
        void SetPreferredProvider(long _nProviderID)
        {

            try
            {
                if (
                    SecureMessageProperties.ListUserProviderAssociation != null
                    &&
                    SecureMessageProperties.ListUserProviderAssociation.Any()
                    )
                {
                    List<DirectUserProviderAssociation> localList = SecureMessageProperties.ListUserProviderAssociation;

                    System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _iEnumerable = null;

                        _iEnumerable = localList.Where(p => p.ProviderID == _nProviderID).Take(1);

                            if (!_iEnumerable.First().IsProviderInbox)
                            {
                                PreferredProvider = _iEnumerable.First();
                                SecureMessageProperties.DelegatedProvider = _iEnumerable.First();
                            }
                            else
                            {
                                PreferredProvider = null;
                                SecureMessageProperties.DelegatedProvider = null;
                            }

                        localList = null;
                        _iEnumerable = null;                    
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

        }

        void ExecutePreferredProvider()
        {
            try
            {
                if (this.PreferredProvider != null)
                {
                    clsSecureMessageDB dbLayer = new clsSecureMessageDB();
                    dbLayer.SetPreferredProvider(PreferredProvider.ProviderID, PreferredProvider.UserID);
                    dbLayer.Dispose();
                    dbLayer = null;

                    List<DirectUserProviderAssociation> localList = SecureMessageProperties.ListUserProviderAssociation;

                    System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _iEnumerable = null;

                    _iEnumerable = localList.Where(p => p.IsPreferred && !p.IsProviderInbox);

                    if (_iEnumerable.Count() == 1) { _iEnumerable.First().IsPreferred = false; }

                    _iEnumerable = null;

                    _iEnumerable = localList.Where(p => p.ProviderID == PreferredProvider.ProviderID && p.UserID == PreferredProvider.UserID);

                    if (_iEnumerable.Count() == 1) { _iEnumerable.First().IsPreferred = true; }

                    localList = null;
                    _iEnumerable = null;

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }
        #endregion

        #region "Delegates"

        protected virtual void RaiseEvntGenerateCDA(Int64 PatientID)
        { if (this.EvntGenerateCDAFromInbox != null) EvntGenerateCDAFromInbox(PatientID); }

        public delegate void GenerateCDAFromInbox(Int64 PatientID);
        public event GenerateCDAFromInbox EvntGenerateCDAFromInbox;

        void newWindow_EvntGenerateCDA(Int64 PatientID)
        {
            RaiseEvntGenerateCDA(PatientID);
        }

        #region Paging Control Events
        
        void pagingControl1_LastPage(object sender, RoutedEventArgs e)
        {
            if ((int)pagingControl1.lblNumberOfPages.Content == CurrentPageCount)
            {
                return;
            }

            CurrentPageCount = (int)pagingControl1.lblNumberOfPages.Content;
            nEndIndex = CurrentPageCount * nMsgPerPage;
            nStartIndex = nEndIndex - (nMsgPerPage-1);
            pagingControl1.lblPageIndex.Content = CurrentPageCount;
            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, requestFrom);
        }

        void pagingControl1_PreviousPage(object sender, RoutedEventArgs e)
        {
            if ((int)pagingControl1.lblPageIndex.Content == 1)
            {
                return;
            }

            CurrentPageCount = CurrentPageCount - 1;
           
            nEndIndex = nStartIndex - 1;
            nStartIndex = nStartIndex - nMsgPerPage;
            pagingControl1.lblPageIndex.Content = CurrentPageCount;
            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, requestFrom);
        }

        void pagingControl1_FirstPage(object sender, RoutedEventArgs e)
        {
            if ((int)pagingControl1.lblPageIndex.Content == 1)
            {
                return;
            }
            CurrentPageCount = 1;
            nStartIndex = 1;
            nEndIndex = nMsgPerPage;
            pagingControl1.lblPageIndex.Content = CurrentPageCount;
            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, requestFrom);

        }

        void pagingControl1_NextPage(object sender, RoutedEventArgs e)
        {
            if ((int)pagingControl1.lblNumberOfPages.Content == CurrentPageCount)
            {
                return;
            }
            CurrentPageCount = CurrentPageCount + 1;
            nStartIndex = nEndIndex + 1;
            nEndIndex = CurrentPageCount * nMsgPerPage;
            pagingControl1.lblPageIndex.Content = CurrentPageCount;
            LoadMails(gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress, requestFrom);
        }
        #endregion
        #endregion


        #region "Messages view Selection Change"

        private void myListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView omyList = null;

            try
            {
                if (sender != null && sender is ListView)
                { 
                    omyList = (ListView)sender;

                    if (omyList.SelectedItem is gloSecureMail)
                     {
                        gloSecureMail mail = (gloSecureMail)myListView.SelectedItem;

                        if (!mail.bIsRead) { this.MarkMessageAsReadUnread(mail, true); }

                        this.myMailHeaderGridControl.DataContext = mail;
                        this.sMessageBody.DataContext = mail;

                        lstAttachments.ContextMenu = null;
                        lstAttachments.Items.Clear();

                        if (!string.IsNullOrWhiteSpace(mail.DocumentName))
                        {
                            string[] Documents = mail.DocumentName.Split(',');
                            string[] AttachmentIDs = mail.AttachmentID.Split(',');
                            string[] sCDAConfidentiality = mail.sCDAConfidentiality.Split(',');

                            for (int i = 0; i < Documents.Length; i++)
                            {
                                if (Convert.ToString(AttachmentIDs[i]) != "" && Convert.ToString(Documents[i]) != "")
                                {
                                    DisplayAttachments(Documents[i].ToString(), AttachmentIDs[i].ToString(), sCDAConfidentiality[i].ToString());
                                }
                            }

                            AttachmentBorder.Visibility = System.Windows.Visibility.Visible;
                        }
                    }
                    else
                    {
                        lstAttachments.ContextMenu = null;
                        lstAttachments.Items.Clear();
                    }
                }
                
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion 
       
        #region Message double click
        private void myListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView omyList = null;

            try
            {
                if (sender != null && sender is ListView)
                {
                    omyList = (ListView)sender;

                    if (omyList.SelectedItem is gloSecureMail)
                    {
                        gloSecureMail mail = (gloSecureMail)myListView.SelectedItem;

                        if (gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.ContainsKey(mail.nSecureMessageInboxID))
                        {
                            oViewMessage = (ViewMessage)gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection[mail.nSecureMessageInboxID];
                        }
                        else
                        {
                            var sfromName = "objForm_" + mail.nSecureMessageInboxID;
                            oViewMessage = new ViewMessage();
                            oViewMessage.tlbCDAImport += new ViewMessage.CDAImportEventHandler(tlbCDAImport);
                            oViewMessage.tlbDMSImport += new ViewMessage.DMSImportEventHandler(tlbDMSImport);
                            oViewMessage.MailDeleted += new ViewMessage.MailDeletedEventHander(oViewMessage_MailDeleted);
                            oViewMessage.Name = sfromName;
                            gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.Add(mail.nSecureMessageInboxID, oViewMessage);
                            oViewMessage.nMessageID = mail.nSecureMessageInboxID;
                        }

                        if (oViewMessage != null)
                        {
                            oViewMessage.DataContext = mail;
                            oViewMessage.Show();
                            oViewMessage.Activate();
                            if (oViewMessage.WindowState == System.Windows.WindowState.Minimized)
                            {
                                oViewMessage.WindowState = System.Windows.WindowState.Normal;
                            }
                            //oViewMessage.SetObserver = this;

                            this.MarkMessageAsReadUnread(mail, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }           
        }

        void oViewMessage_MailDeleted(object sender, EventArgs e)
        {
            try
            {
                if (e is SecureMessageEventArgs)
                {
                    SecureMessageEventArgs eventArgs = (SecureMessageEventArgs)e;

                    this.MarkMessageAsDeleted(eventArgs.SecureMail);                    
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }  
        } 
        #endregion

        public ObservableCollection<gloSecureMail> MessagesObservable { get; set; }        
        public ICollectionView MessagesView { get; set; }

        #region Search Functionality

        void oMyInboxExpanderControl_SearchFired(object sender, EventArgs e)
        {
            try
            {
                if (e is SearchEventArgs)
                {
                    SearchEventArgs args = (SearchEventArgs)e;

                    this.SearchText = args.SearchText;
                    this.SearchType = args.SearchType;

                    if (this.MessagesView != null) { this.MessagesView.Refresh(); }                    
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
            }

        }

        private bool MessageFilter(object Message)
        {
            bool returned = true;

            if (SearchText != null && Message is gloSecureMail)
            {
                gloSecureMail mail = (gloSecureMail)Message;

                switch (this.SearchType)
                {
                    case InboxSearchType.General:

                        returned = mail.From.ToLower().Contains(this.SearchText.ToLower())
                            || mail.sTo.ToLower().Contains(this.SearchText.ToLower())
                            || mail.Subject.ToLower().Contains(this.SearchText.ToLower())
                            || mail.StatusCode.ToLower().Contains(this.SearchText.ToLower())
                            || mail.StatusDescription.ToLower().Contains(this.SearchText.ToLower())
                            || mail.Received.ToLower().Contains(this.SearchText.ToLower())
                            || mail.DelegatedUser.ToLower().Contains(this.SearchText.ToLower());                        
                        break;
                    case InboxSearchType.From:
                        returned = mail.From.ToLower().Contains(this.SearchText.ToLower());
                        break;
                    case InboxSearchType.Received:
                        returned = mail.Received.ToLower().Contains(this.SearchText.ToLower());
                        break;
                    case InboxSearchType.Subject:
                        returned = mail.Subject.ToLower().Contains(this.SearchText.ToLower());
                        break;
                }
            }

            return returned;
        }

        public InboxSearchType SearchType { get; set; }

        public string SearchText { get; set; } 

        #endregion

        #region Mark message as deleted
        private void MarkMessageAsDeleted(gloSecureMail mail)
        {
            try
            {
                using (clsSecureMessageDB dbLayer = new clsSecureMessageDB())
                {
                    dbLayer.DeleteMail(mail.nSecureMessageInboxID);

                    if (MessagesObservable.Contains(mail))
                    { MessagesObservable.Remove(mail); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        } 
        #endregion

        #region Mark message as read/unread
        private void MarkMessageAsReadUnread(gloSecureMail mail, bool IsRead)
        {
            gloDatabaseLayer.DBLayer oDB = null;

            try
            {
                mail.bIsRead = IsRead;
                string _strQuery = "UPDATE SecureMessage_Inbox SET bIsRead = '" + IsRead + "' WHERE nSecureMessageInboxID = " + Convert.ToString(mail.nSecureMessageInboxID);
                oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                oDB.Connect(false);
                oDB.Execute_Query(_strQuery);
                oDB.Disconnect();
                this.LoadNewMailCount();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        } 
        #endregion
    }

}
