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
using System.Windows.Shapes;
using gloUserControlLibrary;
using System.Windows.Forms.Integration;
using System.Xml;
using System.IO;
using System.Windows.Media.Animation;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using gloSettings;
using gloSurescriptSecureMessage;
using System.Data;
using System.Reflection;
using System.Xml.Serialization;
using System.Xml.Schema;
using gloSurescriptSecureMessage_InBox;
using UnZipFileIonic;


namespace gloSurescriptSecureMessage
{

    public class SecureMessageEventArgs : EventArgs
    {
        public gloSecureMail SecureMail { get; set; }

        public SecureMessageEventArgs(gloSecureMail Mail)
        {
            this.SecureMail = Mail;
        }
    }

    public partial class ViewMessage : Window
    {

        public delegate void MailDeletedEventHander(object sender, EventArgs e);
        public event MailDeletedEventHander MailDeleted;
        bool UnstructuredCDA = false;
        string actualFileName = string.Empty;
        string FileExtension = string.Empty;

        private void RaiseMailDeletedEvent(gloSecureMail Mail)
        {
            try
            {
                if (this.MailDeleted != null)
                {
                    this.MailDeleted(this, new SecureMessageEventArgs(Mail));
                }
            }
            catch (Exception exsearchMails)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exsearchMails.ToString(), false);
            }
        }

        #region "Variable Declarations"

        public event CDAImportEventHandler tlbCDAImport;
        public delegate void CDAImportEventHandler(string _FilePath);

        public event DMSImportEventHandler tlbDMSImport;
        public delegate void DMSImportEventHandler(string _FilePath);

        ContextMenu oContextmainMenu = default(ContextMenu);
        TextBlock oTextBlock = default(TextBlock);
        Image oAttachmentImage = default(Image);
        Image oDownloadIcon = default(Image);
        MenuItem mnuItemDownload = default(MenuItem);
        MenuItem mnuItemMapToPat = default(MenuItem);
        MenuItem mnuItemMapToDMS = default(MenuItem);
        StackPanel ostackPannel = default(StackPanel);


        #endregion

        #region "Properties"
        
        public Int64 nMessageID
        {
            get;
            set;
        }

        //public IObserver SetObserver
        //{
        //    get;
        //    set;
        //}

        //public void ResetObserver()
        //{
        //    this.SetObserver = null;
        //}

        #endregion

        #region "Constructor"
      
        public ViewMessage()
        {
            InitializeComponent();
        }

      //  private static ViewMessage frmNew;



        #endregion

        #region "Form Control Events"
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dckAttachments.Visibility = Visibility.Hidden;
            try
            {
                if (this.DataContext != null && this.DataContext is gloSecureMail)
                {
                    gloSecureMail mail = (gloSecureMail)this.DataContext;

                    if (!string.IsNullOrEmpty(mail.DocumentName))
                    {
                        string[] sDocumentName = mail.DocumentName.Split(',');
                        string[] sAttachmentID = mail.AttachmentID.Split(',');
                        string[] sCDAConfidentiality = mail.sCDAConfidentiality.Split(',');

                        if (sAttachmentID.Length > 0)
                        {
                            for (int i = 0; i < sDocumentName.Length; i++)
                            {
                                if (Convert.ToString(sAttachmentID[i]) != "" && Convert.ToString(sDocumentName[i]) != "")
                                {
                                    DisplayAttachments(sDocumentName[i].ToString(), sAttachmentID[i].ToString(), sCDAConfidentiality[i].ToString());
                                }
                            }
                        }
                        dckAttachments.Visibility = Visibility.Visible;
                    }
                    this.Title = Convert.ToString(lblSubject.Content);                    
                }                              
            }
            catch (Exception exLoaded)
            {
                
                gloAuditTrail.gloAuditTrail.ExceptionLog(exLoaded.ToString(), false);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {


        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                ostackPannel = null;
                oTextBlock = null;
                oAttachmentImage = null;
                oContextmainMenu = null;
                oDownloadIcon = null;
                mnuItemDownload = null;
                if (gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.Count > 0)
                {
                    gloSurescriptSecureMessage.SecureMessageProperties.OpenedFormsCollection.Remove(nMessageID);
                    nMessageID = 0;
                }
               //ResetObserver();
            }
            catch (Exception exClosing)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exClosing.ToString(), false);
            }
                
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


                    Int64 nAttachmentID = Convert.ToInt64(oMenuSender.Uid);
                    String sFileExtension = Convert.ToString(oMenuSender.Tag);
                    byte[] pdfBytes = null;
                    if (nAttachmentID > 0)
                    {
                        oDB = new gloDatabaseLayer.DBLayer(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
                        strQuery = "SELECT iContent FROM dbo.SecureMessage_Attachment WHERE nAttachmentID=" + nAttachmentID + "";
                        oDB.Connect(false);
                        AttachmentData = (Byte[])oDB.ExecuteScalar_Query(strQuery);
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
                                    case "image/png":
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
                                    if (pdfBytes != null && UnstructuredCDA == true)
                                    {
                                        File.WriteAllBytes(sSelectedFileName, pdfBytes);
                                    }
                                    else
                                    {
                                        File.WriteAllBytes(sSelectedFileName, AttachmentData);
                                    }
                                 
                                    
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

        private void MenuMapToPat_Click(object sender, RoutedEventArgs e)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            string sCallFrom = RequestFrom.Inbox.ToString();
            byte[] AttachmentData = null;
            MenuItem oMenuSender = null;
            string strQuery = "";
         
            try
            {
                oMenuSender = (MenuItem)sender;

                if (oMenuSender != null)
                {

                   
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
                           
                            if (sFileExtension == "doc" || sFileExtension == "docx" || sFileExtension == "rtf" || sFileExtension == "doc" || sFileExtension == "pdf" || sFileExtension == "XPS" || sFileExtension == "htm" || sFileExtension == "html" || sFileExtension == "txt" || sFileExtension == "xml")
                            {
                          
                                if (sFileExtension == "xml")
                                {
                                    string sSelectedFileName = gloSettings.FolderSettings.AppTempFolderPath + sFileName;
                                    if (sSelectedFileName != "")
                                    {
                                       
                                        File.WriteAllBytes(sSelectedFileName, AttachmentData);
                                        tlbCDAImport(sSelectedFileName);
                                    }
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select Valid CCD-CCR-CDA Files.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
                                System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select Valid CCD-CCR-CDA Files.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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
        private void MenuMapToDMS_Click(object sender, RoutedEventArgs e)
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
            try
            {
                oMenuSender = (MenuItem)sender;

                if (oMenuSender != null)
                {


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
                            if (sFileExtension == "xml")
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

                                }

                                else
                                {
                                    System.Windows.Forms.MessageBox.Show("Cannot Import " + sFileName + ". Please select Valid Files to import into DMS.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lblFrom.Tag.ToString() != "")
                {
                    if (this.DataContext is gloSecureMail)
                    {
                        if (System.Windows.MessageBox.Show("Are you sure you want to delete this message ? ", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            this.RaiseMailDeletedEvent((gloSecureMail)this.DataContext);
                            this.Close();
                        }
                    }                    
                }
            }
            catch (Exception exDelete_Click)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exDelete_Click.ToString(), false);
            }
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lblFrom.Tag.ToString() != "")
                {
                    SecureMessage oSecureMessage = new SecureMessage();
                    Int64 nSecureMessageInboxID = Convert.ToInt64(lblFrom.Tag);
                    oSecureMessage.PrintReport(nSecureMessageInboxID.ToString());
                    if (oSecureMessage != null) { oSecureMessage.Dispose(); }
                }
            }
            catch (Exception exPrint)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(exPrint.ToString(), false);
            }


        }
        private byte[] ReadNonXMLBodyAttchement(String FileName)
        {
            try
            {
                //bool _IsunstructuredCDA = false;

                gloCCDLibrary.gloCDAReader CDAReader = new gloCCDLibrary.gloCDAReader();
                String base64string = CDAReader.getNONXMLBody(FileName, ref FileExtension, ref UnstructuredCDA, ref actualFileName);
                byte[] pdfBytes = null;
                pdfBytes = Convert.FromBase64String(base64string);
                return pdfBytes;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region "Private and Public Methods"

        private void DisplayAttachments(string DocumentName, string AttachmentID, string sCDAConfidentiality = "")
        {
            try
            {
                BrushConverter bc = new BrushConverter();
                string[] _Extension = null;
                string _FileExt = "";

                if (DocumentName != "")
                {
                    _Extension = DocumentName.Split('.');
                    if (_Extension.Length > 0)
                    {
                        _FileExt = _Extension[_Extension.Length-1];
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

                    lstAttachments.Margin = new Thickness(-6, 2, 0, 4);

                    oDownloadIcon = new Image();
                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDownloadBitmapImage;//imgDownload;
                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;


                    mnuItemDownload = new MenuItem();
                    mnuItemDownload.Header = "Download";
                    mnuItemDownload.Icon = oDownloadIcon;
                    mnuItemDownload.Uid = AttachmentID;
                    mnuItemDownload.Tag = _FileExt;
                    mnuItemDownload.ToolTip = DocumentName;
                    mnuItemDownload.Click += new RoutedEventHandler(MenuDownloadItem_Click);
                    mnuItemDownload.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");

                    #region "Map To Patient"

                    //System.Windows.Media.Imaging.BitmapImage imgMaptoPat = new System.Windows.Media.Imaging.BitmapImage();
                    //imgMaptoPat.BeginInit();
                    //Uri UriMaptoPat = new Uri("graphics\\MapPatient.ico", UriKind.RelativeOrAbsolute);
                    //imgMaptoPat.UriSource = UriMaptoPat;
                    //imgMaptoPat.EndInit();

                    oDownloadIcon = new Image();
                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsPatientBitmapImage;//imgMaptoPat;
                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;

                    mnuItemMapToPat = new MenuItem();
                    mnuItemMapToPat.Header = "Import CCD-CCR-CDA files to patient";
                    mnuItemMapToPat.Icon = oDownloadIcon;
                    mnuItemMapToPat.Uid = AttachmentID;
                    mnuItemMapToPat.Tag = _FileExt;
                    mnuItemMapToPat.ToolTip = DocumentName;
                    mnuItemMapToPat.Click += new RoutedEventHandler(MenuMapToPat_Click);
                    mnuItemMapToPat.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");

                    #endregion
                    #region "Map To DMS"

                    //System.Windows.Media.Imaging.BitmapImage imgMaptoDMS = new System.Windows.Media.Imaging.BitmapImage();
                    //imgMaptoDMS.BeginInit();
                    //Uri UriMaptoDMS = new Uri("graphics\\ImportDMSFiles.ico", UriKind.RelativeOrAbsolute);
                    //imgMaptoDMS.UriSource = UriMaptoDMS;
                    //imgMaptoDMS.EndInit();

                    oDownloadIcon = new Image();
                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDMSBitmapImage;//imgMaptoDMS;
                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;

                    mnuItemMapToDMS = new MenuItem();
                    mnuItemMapToDMS.Header = "Import selected file to DMS";
                    mnuItemMapToDMS.Icon = oDownloadIcon;
                    mnuItemMapToDMS.Uid = AttachmentID;
                    mnuItemMapToDMS.Tag = _FileExt;
                    mnuItemMapToDMS.ToolTip = DocumentName;
                    mnuItemMapToDMS.Click += new RoutedEventHandler(MenuMapToDMS_Click);
                    mnuItemMapToDMS.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");

                    #endregion
                    oContextmainMenu.Items.Add(mnuItemDownload);
                    oContextmainMenu.Items.Add(mnuItemMapToPat);
                    oContextmainMenu.Items.Add(mnuItemMapToDMS);
                }
                ostackPannel = new StackPanel();
                ostackPannel.Margin = new Thickness(-2, 0, 0, 0);
                ostackPannel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                ostackPannel.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");

                oAttachmentImage = new Image();
                oAttachmentImage.Margin = new Thickness(6, 2, 0, 4);

                if (sCDAConfidentiality.Contains("R"))
                {
                    oAttachmentImage.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsRestrictedAttachment;
                }
                else
                {
                    oAttachmentImage.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsUnReadBitmapImage;
                }

                //oAttachmentImage.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsUnReadBitmapImage;//new System.Windows.Media.Imaging.BitmapImage(new Uri("/gloSurescriptSecureMessage;component/graphics/unread.gif", UriKind.Relative));
                oAttachmentImage.Height = 14;
                oAttachmentImage.Width = 14;
                oAttachmentImage.Stretch = Stretch.Fill;
                oAttachmentImage.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                oAttachmentImage.VerticalAlignment = System.Windows.VerticalAlignment.Center;

                ostackPannel.Children.Add(oAttachmentImage);



                oTextBlock = new TextBlock();
                oTextBlock.Margin = new Thickness(6, 2, 0, 4);
                oTextBlock.TextWrapping = TextWrapping.Wrap;
                oTextBlock.Foreground = (Brush)bc.ConvertFrom("#FF3D6394");
                oTextBlock.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");
                oTextBlock.FontFamily = new FontFamily("Calibri");
                oTextBlock.FontSize = 13;
                oTextBlock.Text = DocumentName;
                oTextBlock.Tag = AttachmentID;
                oTextBlock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                oTextBlock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                oTextBlock.MouseLeftButtonDown -= Attachment_MouseClick;


                ostackPannel.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");
                ostackPannel.Children.Add(oTextBlock);

                if (SecureMessage.bIsAccess(sCDAConfidentiality.Trim()))
                {

                    ostackPannel.ContextMenu = oContextmainMenu;
                }
                //lstAttachments.BorderThickness = new Thickness(2); 
                lstAttachments.Background = (Brush)bc.ConvertFrom("#FFBFDBFF");
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



        private void btnClose_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	 Window parent = Window.GetWindow(this);
            parent.Close();
        }

        #endregion
    }
}
