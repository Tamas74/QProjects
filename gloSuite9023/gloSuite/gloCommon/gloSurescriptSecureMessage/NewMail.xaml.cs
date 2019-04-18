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
using System.Windows.Forms;
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
using System.ComponentModel;
using gloAuditTrail;
using gloGlobal;
using System.IO.Compression;



namespace InBox
{
    public partial class NewMail : Window
    {

        #region "Variable Declarations"
        // Ashish added on 31-Oct-2013
        // for preventing same provider from being added twice.

        private HashSet<long> hashProviders = null;
        double maxSize = 0;
        bool _isMouseDown = false;
        bool _isToMouseDown = false;
        bool _isSelected = false;
        bool _ShowAgeInDays = false;
        Int64 _AgeLimit;
        DateTime _DateOfBirth;
        TimeSpan _TimeSpan = TimeSpan.Zero;

        private bool _flagLoaded = false;
        private bool _IsPatientLoad = false;

        public delegate void GenerateCDAHandler(Int64 PatientID);
        public event GenerateCDAHandler EvntGenerateCDA; 
        

  
        private DirectUserProviderAssociation CurrentUserProvider = null;

        private long _patientID = 0;
        private bool _isMessageSend = false;
        private bool _IsUnstrucuredCDA = false;
        private string ActualFileName = string.Empty;

        //private string ReadMeFileName = string.Empty;
        //private string XSLFileName = string.Empty;
        //private string HtmlFileName = string.Empty;
        //private string XMLFileName = string.Empty;
        //private string XDMFolderName = string.Empty;
        //private const string ReadMeFileName = "README.TXT";
        //private const string  HtmlFileName = "INDEX.HTM";
        //private const string XSLFileName =  "TRIACDA.XSL";
        //private const string XMLFileName = "DOC0001.XML";
        //private const string XDMFolderName = "IHE_XDM";


        [DefaultValue(false)]
        public bool SendButtonClicked { get; set; }
        //double height= 0.0;        
        //double width= 0.0;

        public IObserver SetObserver
        {
            get;
            set;
        }

        public void ResetObserver()
        {
            this.SetObserver = null;
        }

        public long PatientID
        {
            get { return _patientID; }
            set { _patientID = value; }
        }

        private long _ContactID = 0;

        public long ContactID
        {
            get { return _ContactID; }
            set { _ContactID = value; }
        }

        public bool IsChangePatient { get; set; }

        public bool IsToListOrAttachmentsExists { get; set; }

        private static InBox.NewMail frmNew;

        public static bool _checkMailInstance = false;

        //public static bool checkMailInstance
        //{
        //    get
        //    {
        //        if (frmNew == null)
        //        {
        //            _checkMailInstance = false;

        //        }
        //        else
        //        {
        //            _checkMailInstance = true;
        //        }

        //        return _checkMailInstance;
        //    }
        //    set { _checkMailInstance = value; }
        //}

        public static bool checkMailInstance
        {
            get { return _checkMailInstance; }
            set { _checkMailInstance = value; }
        }

        public static InBox.NewMail GetInstance()
        {

            try
            {
                if (frmNew != null)
                {

                    checkMailInstance = true;
                    frmNew.Show();
                    
                }
                else
                {
                    frmNew = new NewMail();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {

            }
            return frmNew;
        }

        public static InBox.NewMail CheckFormOpen()
        {
            try
            {
                if (frmNew == null)
                {
                    return null;
                }
                else
                {
                    return frmNew;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        private string sFromDirectAddress = string.Empty;

        [DefaultValue(0)]
        public Int64  DocumentReferenceID
        {
            get;
            set;
        }
      

        #endregion 

        #region "Constructor"

        public NewMail()
        {
            InitializeComponent();
            PatientStrip.Visibility = Visibility.Collapsed;
            _IsUnstrucuredCDA = gloSettings.gloEMRAdminSettings.XDmMessageEnabled;
        }

        public NewMail(long _PatientID, string fileName, long _ReferedbyID = 0 )
        {
            FileInfo fd = null;
            InitializeComponent();
            try
            {
                ContactID = _ReferedbyID;
                PatientID = _PatientID;
                PatList.Visibility = Visibility.Collapsed;

                if (PatientID == 0)
                {
                    PatientStrip.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PatientStrip.Visibility = Visibility.Visible;
                }

                fd = new FileInfo(fileName);
                if (fd.Exists)
                {
                    SendFileToAttachment(fd.Name, fd.FullName);
                    gloSurescriptSecureMessage.SecureMessageProperties.PatientID = _patientID;
                    AddPatientFromOrder(_patientID);
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                if (fd != null)
                {
                    fd = null;
                }
            }
        }

        public NewMail(long _PatientID, string fileName, long _ContactID, string Reffletter, string Subject)
        {
            //string Reffletter = string.Empty;
            FileInfo fd = null;
            InitializeComponent();
            try
            {
                PatientID = _PatientID;
                ContactID = _ContactID;
                PatList.Visibility = Visibility.Collapsed;

                if (PatientID == 0)
                {
                    PatientStrip.Visibility = Visibility.Collapsed;
                }
                else
                {
                    PatientStrip.Visibility = Visibility.Visible;
                }

                fd = new FileInfo(fileName);
                if (fd.Exists)
                {
                    SendFileToAttachment(fd.Name, fd.FullName);
                    AddReferalByIDFromRefferal(ContactID, PatientID);
                }
                fd = null;
                if (Reffletter != "")
                {
                    fd = new FileInfo(Reffletter);
                    if (fd.Exists)
                    {
                        SendtoBody(Reffletter);
                    }
                }

                if (Subject != "")
                {
                    txtSubject.Text = Subject;
                }

                fd = null;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                if (fd != null)
                {
                    fd = null;
                }
            }
        } 

        #endregion

        #region " Form Control Events"

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _flagLoaded = true;
          
            // GetProviderEmail(gloSurescriptSecureMessage.SecureMessageProperties.ProviderID);
            this.sFromDirectAddress = gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress;
            lblFrom.Content = gloSurescriptSecureMessage.SecureMessageProperties.ProviderName + " <" + gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress + ">";
           

        }

        private void btnPatList_Click(object sender, RoutedEventArgs e)
        {
           
            frmPatientList frm = null;
            try
            {
                
                frm = new frmPatientList();
                frm.DatabaseConnection = gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString;
                frm.UserName = gloSurescriptSecureMessage.SecureMessageProperties.UserName;
                frm.ShowDialog(frm.Parent);
                long _patID = frm.PatientID;
                if (_patID > 0)
                {
                    AddPatient(_patID);
                }

            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (frm != null)
                {
                    frm.Dispose();
                    frm = null;
                }

            }

        }

        private void btnAddbook_Click(object sender, RoutedEventArgs e)
        {
            //frmReferralsList frm = null;
            //DataTable dtRef = null;
            //try
            //{
            //    frm = new frmReferralsList();
            //    frm.DatabaseConnection = gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString;
            //    frm.ShowDialog();
            //    dtRef = frm.dtRefferal;

            //    if (dtRef != null)
            //    {
            //        if (dtRef.Rows.Count > 0)
            //        {
            //            AddRefferels(dtRef);
            //        }
            //    }

            double toWidth = lstTo.Width;

            AddressAndbtnToClick();

            //Window_SizeChanged(null, null);
            _IsPatientLoad = true;
            lstAttachment.Width = toWidth + 65;
            lstTo.Width = toWidth;     

            //}

            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            //}

            //finally
            //{
            //    if (frm != null)
            //    {
            //        frm.Dispose();
            //        frm = null;
            //    }

            //}

        }

        private void btnAttFile_Click(object sender, RoutedEventArgs e)
        {
            double toWidth = lstTo.Width;

            if (_IsUnstrucuredCDA == true)
            {
                AttachClick_UnstructuredCDA();
            }
            else
            {
                AttachClick();

            }
            //Window_SizeChanged(null, null);
            _IsPatientLoad = true;
            lstAttachment.Width = toWidth+65;
            lstTo.Width = toWidth;     
         
        }

        private void btnAddScanDoc_Click(object sender, RoutedEventArgs e)
        {
            //double windowWidth = this.Width;
            //this.Topmost = false;
            if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID == 0)
            {                
                this.btnPatList_Click(this, new RoutedEventArgs());
            }

            if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID != 0)
            {
                gloEDocumentV3.gloEDocV3Management oScanDocument = new gloEDocumentV3.gloEDocV3Management();
                gloCCDLibrary.gloCCDInterface ogloInterface = new gloCCDLibrary.gloCCDInterface();

                double toWidth = lstTo.Width;
                try
                {
                    ArrayList _FilePaths = null;
                    ArrayList _DocumentNames = null;

                    oScanDocument.oPatientExam = gloSurescriptSecureMessage.SecureMessageProperties.objPatientExam;
                    oScanDocument.oPatientMessages = gloSurescriptSecureMessage.SecureMessageProperties.objPatientMessages;
                    oScanDocument.oPatientLetters = gloSurescriptSecureMessage.SecureMessageProperties.objPatientLetters;
                    oScanDocument.oNurseNotes = gloSurescriptSecureMessage.SecureMessageProperties.objNurseNotes;
                    oScanDocument.oHistory = gloSurescriptSecureMessage.SecureMessageProperties.objHistory;
                    oScanDocument.oLabs = gloSurescriptSecureMessage.SecureMessageProperties.objLabs;
                    oScanDocument.oDMS = gloSurescriptSecureMessage.SecureMessageProperties.objDMS;
                    oScanDocument.oRxmed = gloSurescriptSecureMessage.SecureMessageProperties.objRxmed;
                    oScanDocument.oOrders = gloSurescriptSecureMessage.SecureMessageProperties.objOrders;
                    oScanDocument.oProblemList = gloSurescriptSecureMessage.SecureMessageProperties.objProblemList;
                    oScanDocument.oCriteria = gloSurescriptSecureMessage.SecureMessageProperties.objCriteria;
                    oScanDocument.oWord = gloSurescriptSecureMessage.SecureMessageProperties.objWord;

                    oScanDocument.ShowEDocument_IntuitMessage(gloSurescriptSecureMessage.SecureMessageProperties.PatientID, gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ScanDocument, null, gloEDocumentV3.Enumeration.enum_OpenExternalSource.SecureMessage, 0, out _FilePaths, out  _DocumentNames);

                    if ((_FilePaths != null))
                    {
                        int cntFlag = 0;
                        for (Int16 i = 0; i <= _FilePaths.Count - 1; i++)
                        {
                            if (File.Exists(_FilePaths[i].ToString()) == true)
                            {
                                //Addded Ext. to open the file in PDF format when double click on list item.


                                string validFilename = string.Empty;
                                string onlyFilename = string.Empty;

                                if (_DocumentNames[i].ToString().Contains(".pdf"))
                                {
                                    validFilename = _DocumentNames[i].ToString();
                                    onlyFilename = validFilename.Substring(0, validFilename.Length - 4);
                                    if (onlyFilename.Length > 251)
                                    {
                                        onlyFilename = onlyFilename.Substring(0, 251);
                                    }
                                    validFilename = onlyFilename + ".pdf";
                                    SendFileToAttachment(validFilename, _FilePaths[i].ToString());
                                    //  SendFileToAttachment(_DocumentNames[i].ToString(), _FilePaths[i].ToString());
                                    cntFlag = 1;

                                }
                                else
                                {
                                    validFilename = _DocumentNames[i].ToString() + ".pdf";
                                    onlyFilename = validFilename.Substring(0, validFilename.Length - 4);
                                    if (onlyFilename.Length > 251)
                                    {
                                        onlyFilename = onlyFilename.Substring(0, 251);
                                    }
                                    validFilename = onlyFilename + ".pdf";
                                    SendFileToAttachment(validFilename, _FilePaths[i].ToString());
                                    // SendFileToAttachment(_DocumentNames[i].ToString() + ".pdf", _FilePaths[i].ToString());
                                    cntFlag = 1;
                                }
                            }
                        }
                        if (cntFlag == 1)
                        {
                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SecureMessage , gloAuditTrail.ActivityCategory.NewMessage, gloAuditTrail.ActivityType.Export, "Scan document attached to DIRECT message.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR,true );
                        }

                    }

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
                }
                finally
                {
                    if ((ogloInterface != null))
                    {
                        ogloInterface.Dispose();
                        ogloInterface = null;
                    }
                    if ((oScanDocument != null))
                    {
                        oScanDocument.Dispose();
                        oScanDocument = null;
                    }
                    string sFilepath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SecureMsgDocs";
                    if (Directory.Exists(sFilepath) == true)
                    {
                        Directory.Delete(sFilepath, true);
                    }

                    //Window_SizeChanged(null, null);
                    _IsPatientLoad = true;
                    lstAttachment.Width = toWidth + 65;
                    lstTo.Width = toWidth;
                    // this.Topmost = true;
                }
            }

            

        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

            if (lstTo.Items.Count > 0)
            {
                if (txtSubject.Text != "")
                {
                    //Changes made by Ashish Tamhane for Bug #49486
                    if (txtMessage.Text.Length > 0)
                    {
                        this.SendButtonClicked = true;
                        InsertDataInToTable();
                        // After the message has been sent MyInbox.Update (via IObserver)
                        // is called which checks if the SelectedNode is 'SentItems'
                        // If it is then it is updated.
                        //
                        // Finally Observer has no job here so it is set to Null.
                        if (this.SetObserver != null)
                        {
                            this.SetObserver.Update();
                            this.SetObserver = null;
                        }
                    }
                    else
                    { System.Windows.MessageBox.Show("Please enter the Message content.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information); }
                }
                else
                {
                    System.Windows.MessageBox.Show("Please enter the Subject.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else
            {
                // Changes made by Ashish Tamhane for Bug #49428
                System.Windows.MessageBox.Show("There must be at least one name or Contact group in the To box.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnTo_Click(object sender, RoutedEventArgs e)
        {
            double toWidth = lstTo.Width;

            AddressAndbtnToClick();
            //this.Height = height;
            //this.Width = width;

            //Window_SizeChanged(null, null);
            _IsPatientLoad = true;
            lstAttachment.Width = toWidth + 65;
            lstTo.Width = toWidth; 
        }

        public void AddressAndbtnToClick()
        {
            frmToList frm = null;
            DataTable dtRef = null;

            try
            {             
                frm = new frmToList();
                frm.DatabaseConnection = gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString;
                frm.PatientID = PatientID; //gloSurescriptSecureMessage.SecureMessageProperties.PatientID;
                long _patID = PatientID;

                if (PatientStrip.Visibility == Visibility.Visible)
                {

                    if ((lstTo.Items.Count > 0) || (lstAttachment.Items.Count > 0))
                    {
                        IsToListOrAttachmentsExists = true;
                    }
                    else
                    {
                        IsToListOrAttachmentsExists = false;
                    }
                }
                else
                {
                    IsToListOrAttachmentsExists = false; 
                }
            

                frm.flagIsToListOrAttachmentExists = IsToListOrAttachmentsExists;
                if (PatList.Visibility == Visibility.Collapsed)
                {
                    frm.flagIsBtnPatientListVisible = false;
                }
                else
                {
                    frm.flagIsBtnPatientListVisible = true;
                }
                frm.ShowDialog(frm.Parent);
               
                dtRef = frm.dtTable;

                if (dtRef != null)
                {
                    if (dtRef.Rows.Count > 0)
                    {
                        PatientID = frm.PatientID;
                        IsChangePatient = frm.flagIsChangePatient;
                       _IsPatientLoad = true;


              // if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID > 0)
             //  {
                       if ((PatientID != _patID) && (IsChangePatient == true))
                       {

                       if (lstTo.Items.Count > 0)
                       {
                           lstTo.Items.Clear();
                       }

                       if (lstAttachment.Items.Count > 0)
                       {
                           lstAttachment.Items.Clear();
                       }

                        gloSurescriptSecureMessage.SecureMessageProperties.PatientID= PatientID;
                         GetPatientDetails(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                         PatientStrip.Visibility = Visibility.Visible;
                         _IsPatientLoad = true;
                       
                      }
             //  }


                
                long contactID = 0;
                string fullName = string.Empty;
                string nPI = string.Empty;
                string email = string.Empty;


               

                        for (int count = 0; count <= dtRef.Rows.Count - 1; count++)
                        {
                            if (dtRef.Rows[count]["Name"] != null)
                                fullName = Convert.ToString(dtRef.Rows[count]["Name"]);

                            if (dtRef.Rows[count]["ContactID"] != null)
                                contactID = Convert.ToInt64(dtRef.Rows[count]["ContactID"]);

                            if (dtRef.Rows[count]["NPI"] != null)
                                nPI = Convert.ToString(dtRef.Rows[count]["NPI"]);

                            if (dtRef.Rows[count]["Email"] != null)
                                email = Convert.ToString(dtRef.Rows[count]["Email"]);


                            AddToList(fullName, contactID, nPI, email);
                        }
                    }
                }


            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (frm != null)
                {
                    frm.Dispose();
                    frm = null;
                }
               // this.BringIntoView();
              //  this.Focus();

            }
        }

        private void btnTo_Copy_Click(object sender, RoutedEventArgs e)
        {
            frmDefaultPatient frm = null;
            string _defaultEmailID = string.Empty;
            bool _isExists = false;
            try
            {
                frm = new frmDefaultPatient();
                frm.ShowInTaskbar = false;
                frm.ShowDialog(frm.Parent);
                _defaultEmailID = frm.DefaultName;

                if (!string.IsNullOrEmpty(_defaultEmailID))
                {
                    string[] strArray = null;
                    strArray = _defaultEmailID.Trim().Split(';');
                    if (strArray.Length > 0)
                    {
                        _isExists = false;
                        for (int i = 0; i <= strArray.Length - 1; i++)
                        {
                            Int64 contactID = 0;
                            string sEmail = "";
                            for (int countTo = 0; countTo <= lstTo.Items.Count - 1; countTo++)
                            {
                                StackPanel stac = (StackPanel)lstTo.Items.GetItemAt(countTo);
                                TextBlock tBlock = (TextBlock)stac.Children[0];
                                TextBlock ttBlock = (TextBlock)stac.Children[1];
                                contactID = Convert.ToInt64(tBlock.Tag);
                                sEmail = Convert.ToString(ttBlock.Text);

                                if (sEmail.ToLower() == strArray[i].ToString().ToLower())
                                {
                                    _isExists = true;
                                    break;
                                }
                            }

                            if (_isExists == true)
                            {
                                System.Windows.MessageBox.Show("Contact : " + sEmail + " already exists.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                            }
                            else
                            {

                                AddDefaultEmailID(strArray[i].ToString());
                                _IsPatientLoad = true;
                            }
                        }
                    }
                }


            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (frm != null)
                {
                    frm.Dispose();
                    frm = null;
                }

            }
        }

        private void btnAddCCD_Click(object sender, RoutedEventArgs e)
        {
            double toWidth = lstTo.Width;
            if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID == 0)
            {                
                this.btnPatList_Click(this, new RoutedEventArgs());
            }

            if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID != 0)
            {
                frmCCD frm = null;
                string _strPath = string.Empty;
                FileInfo fd = null;
                try
                {
                    frm = new frmCCD(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                    frm.ShowDialog(frm.Parent);
                    _strPath = frm.Path;

                    if (_strPath != "")
                    {
                        fd = new FileInfo(_strPath);
                        if (fd.Exists)
                        {
                            SendFileToAttachment(fd.Name, fd.FullName);
                        }
                    }


                }

                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }

                finally
                {
                    if (frm != null)
                    {
                        frm.Dispose();
                        frm = null;
                    }                    
                    _IsPatientLoad = true;
                    lstAttachment.Width = toWidth + 65;
                    lstTo.Width = toWidth;
                }
            }            
        }

        private void txtSubject_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void lstAttachment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        void ImgToDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isToMouseDown = true;
        }

        void ImgToDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            double toWidth = lstTo.Width;
            if (_isToMouseDown == true)
            {
                _isToMouseDown = false;

                if (lstTo.SelectedIndex >= 0)
                {
                    if ((System.Windows.MessageBox.Show("Are you sure you want to remove '" + GetName(lstTo.SelectedIndex) + "' ?", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
                    {
                        lstTo.Items.Remove(lstTo.SelectedItem);

                        if (lstTo.Items.Count >= 0)
                        {
                            lstTo.SelectedIndex = 0;
                        }

                        //Window_SizeChanged(null, null);
                        _IsPatientLoad = true;
                        lstAttachment.Width = toWidth + 65;
                        lstTo.Width = toWidth;
                    }
                }
            }
        }

        void ImgDelete_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
        }

        void ImgDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {

            double toWidth = lstTo.Width;
            if (_isMouseDown == true)
            {
                _isMouseDown = false;

                if (lstAttachment.SelectedIndex >= 0)
                {
                    if ((System.Windows.MessageBox.Show("Are you sure you want to remove the '" + GetAttachmentFileName(lstAttachment.SelectedIndex) + "' file?", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
                    {
                        lstAttachment.Items.Remove(lstAttachment.SelectedItem);

                        if (lstAttachment.Items.Count >= 0)
                        {
                            lstAttachment.SelectedIndex = 0;
                        }
                        CalculateSize();

                        //Window_SizeChanged(null, null);
                        _IsPatientLoad = true;
                        lstAttachment.Width = toWidth + 65;
                        lstTo.Width = toWidth;
                       
                    }
                }

            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
           // this.Topmost = true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            //if (Owner == null || Owner.IsActive)
            //    return;
            //bool hasActiveWindow = false;
            //foreach (Window ownedWindow in Owner.OwnedWindows)
            //{
            //    if (ownedWindow.IsActive)
            //        hasActiveWindow = true;
            //}

            //if (!hasActiveWindow)
            //    this.Topmost = false;

        }
        private void Border_MouseClick(System.Object sender, MouseButtonEventArgs e)
        {
            //this.Topmost = false;
            if (e.ClickCount == 2)
            {
                byte[] AttachmentData = null;
                string strFileName = "";
                try
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

                    if (lstAttachment.SelectedItem != null)
                    {
                        StackPanel stac = (StackPanel)lstAttachment.SelectedItem;
                        TextBlock TBlock = (TextBlock)stac.Children[1];

                        string[] _Extension = null;
                        string _FileExt = "";

                        _Extension = TBlock.Text.Split('.');

                        if (_Extension.Length > 1)
                        {
                            _FileExt = _Extension[_Extension.Length - 1];
                        }

                        AttachmentData = (Byte[])TBlock.Tag;    

                        if (AttachmentData != null)
                        {
                            

                            
                            if (!File.Exists(gloSettings.FolderSettings.AppTempFolderPath + TBlock.Text))
                            { strFileName = GenerateFile(AttachmentData, TBlock.Text); }
                            else
                            { strFileName = gloSettings.FolderSettings.AppTempFolderPath + TBlock.Text; }
                            
                            System.Diagnostics.ProcessStartInfo startInfo = null;
                            System.Diagnostics.Process pStart = new System.Diagnostics.Process();
                            startInfo = new System.Diagnostics.ProcessStartInfo(strFileName);
                            startInfo.UseShellExecute = true;
                            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal ;
                           
                            startInfo.CreateNoWindow = false;
                            Process.Start(startInfo);

                            
                        }

                        _Extension = null;
                        _FileExt = null;
                        TBlock = null;
                        stac = null;


                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }
                finally
                {
                    
                    strFileName = null;
                    AttachmentData = null;
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                   // this.Topmost = true;
                }
            }
        }

        private void BorderRight_MouseClick(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem oMenuSender = null;

            try
            {
                oMenuSender = (System.Windows.Controls.MenuItem)sender;
                double toWidth = lstTo.Width;
                if (oMenuSender != null)
                {

                    if (lstAttachment.SelectedIndex >= 0)
                    {
                        if ((System.Windows.MessageBox.Show("Are you sure you want to remove the '" + GetAttachmentFileName(lstAttachment.SelectedIndex) + "' file?", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
                        {
                            lstAttachment.Items.Remove(lstAttachment.SelectedItem);

                            if (lstAttachment.Items.Count >= 0)
                            {
                                lstAttachment.SelectedIndex = 0;
                            }
                            CalculateSize();

                            Window_SizeChanged(null, null);
                            _IsPatientLoad = true;
                            lstAttachment.Width = toWidth + 65;
                            lstTo.Width = toWidth;
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

                oMenuSender = null;

            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            gloSurescriptSecureMessage.SecureMessageProperties.PatientID = 0;

            checkMailInstance = false;

            if (frmNew != null)
            {
                frmNew = null;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_flagLoaded == true && _IsPatientLoad == false)
            {
                if (txtMessage.Text == "")
                {
                    txtMessage.Height = Double.NaN;
                    txtMessage.Width = Double.NaN;
                }
                txtSubject.Height = Double.NaN;
                txtSubject.Width = Double.NaN;
                lstTo.Height = Double.NaN;
                lstTo.Width = Double.NaN;
                lstAttachment.Height = Double.NaN;
                lstAttachment.Width = Double.NaN;
            }

            _IsPatientLoad = false;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isMessageSend == false)
            {
                if (System.Windows.MessageBox.Show("Are you sure you want to close New message window?", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    checkMailInstance = false;
                    if (hashProviders != null)
                    {
                        hashProviders.Clear();
                        hashProviders = null;   
                    }
                    e.Cancel = false;
                }
                else
                {
                    this.Activate();
                    _isMessageSend = false;
                    e.Cancel = true;
                }
            }
            else
            {
                _isMessageSend = false;
            }
        }

        protected virtual void RaiseEvntGenerateCDA()
        { if (this.EvntGenerateCDA != null) EvntGenerateCDA(_patientID); }

        private void btnAddCDA_Click(object sender, RoutedEventArgs e)
        {
            double toWidth = lstTo.Width;
            ActualFileName = string.Empty;
            if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID == 0)
            {                
                this.btnPatList_Click(this, new RoutedEventArgs());
            }


            if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID != 0)
            {
                try
                {
                    string XMLFileName = String.Empty;
                    SecureMessageProperties.CCDAFilePath = string.Empty;
                    RaiseEvntGenerateCDA();
                    string safeFileName = "";
                    if (!string.IsNullOrWhiteSpace(SecureMessageProperties.CCDAFilePath))
                    {
                        //if (gloSettings.gloEMRAdminSettings.XDmMessageEnabled == false)
                        //{
                             safeFileName = System.IO.Path.GetFileName(SecureMessageProperties.CCDAFilePath);
                            SendFileToAttachment(safeFileName, SecureMessageProperties.CCDAFilePath);
                        //}
                        //else
                        //{
                        //    XMLFileName = CreateDirectoryStructure(SecureMessageProperties.CCDAFilePath);
                        //    safeFileName = System.IO.Path.GetFileName(XMLFileName);
                        //    SendFileToAttachment(safeFileName, XMLFileName);
                        //}
                        
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.NewMessage, ActivityType.Export, "CCDA file name : '" + Convert.ToString(safeFileName) + "' attached to direct message", 0, 0, 0, ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                    }

                    //Code added for Auto Deleting CCDA files
                    try
                    {
                        if (gloSurescriptSecureMessage.SecureMessageProperties.IsAutoDeleteCCDA == true)
                        {
                            File.Delete(SecureMessageProperties.CCDAFilePath);
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.CCD, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.General, ex, gloAuditTrail.ActivityOutCome.Failure);
                    }

                }

                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                }

                finally
                {
                    _IsPatientLoad = true;
                    lstAttachment.Width = toWidth + 65;
                    lstTo.Width = toWidth;
                }
            }            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parent = Window.GetWindow(this);
            parent.Close();
        }

        #endregion

        #region "Private and Public Methods"

        private void SendtoBody(string ReffFileName)
        {

            StreamReader oReder = new StreamReader(ReffFileName);
            try
            {
                txtMessage.Text = oReder.ReadToEnd();
            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                oReder.Dispose();
                oReder = null;

            }


        }

        private void AddReferalByID(long _ContactID, long _PatientID)
        {

            clsSecureMessageDB clsDb = null;
            DataTable dtReference = null;

            try
            {

                clsDb = new clsSecureMessageDB();
               // dtReference = clsDb.GetReferalListByContactID(_ContactID);

                string fName = string.Empty;
                string lName = string.Empty;
                long contactID = 0;
                string fullName = string.Empty;
                string nPI = string.Empty;
                string email = string.Empty;

                gloSurescriptSecureMessage.SecureMessageProperties.PatientID = _PatientID;
                GetPatientDetails(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                PatientStrip.Visibility = Visibility.Visible;


                if (dtReference != null)
                {
                    if (dtReference.Rows.Count > 0)
                    {

                        for (int count = 0; count <= dtReference.Rows.Count - 1; count++)
                        {
                            if (dtReference.Rows[count]["fName"] != null)
                                fName = Convert.ToString(dtReference.Rows[count]["fName"]);

                            if (dtReference.Rows[count]["lName"] != null)
                                lName = Convert.ToString(dtReference.Rows[count]["lName"]);

                            if (dtReference.Rows[count]["nContactID"] != null)
                                contactID = Convert.ToInt64(dtReference.Rows[count]["nContactID"]);

                            if (dtReference.Rows[count]["sNPI"] != null)
                                nPI = Convert.ToString(dtReference.Rows[count]["sNPI"]);

                            if (dtReference.Rows[count]["email"] != null)
                                email = Convert.ToString(dtReference.Rows[count]["email"]);

                            fullName = "";

                            if ((fName != "") && (lName != ""))
                            {
                                fullName = fName.Trim() + ' ' + lName.Trim();
                            }

                            if ((fName != "") && (lName == ""))
                            {
                                fullName = fName.Trim();
                            }
                            if ((fName == "") && (lName != ""))
                            {
                                fullName = lName.Trim();
                            }

                            AddToList(fullName, contactID, nPI, email);

                        }
                    }
                }


            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (dtReference != null)
                {
                    dtReference.Dispose();
                    dtReference = null;
                }

                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }

            }

        }

        private void AddReferalByIDFromRefferal(long _ContactID, long _PatientID)
        {

            clsSecureMessageDB clsDb = null;
            DataTable dtReference = null;

            try
            {

                clsDb = new clsSecureMessageDB();
                dtReference = clsDb.GetReferalListByContactID(_ContactID);

                string fName = string.Empty;
                string lName = string.Empty;
                long contactID = 0;
                string fullName = string.Empty;
                string nPI = string.Empty;
                string email = string.Empty;

                gloSurescriptSecureMessage.SecureMessageProperties.PatientID = _PatientID;
                GetPatientDetails(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                PatientStrip.Visibility = Visibility.Visible;


                if (dtReference != null)
                {
                    if (dtReference.Rows.Count > 0)
                    {

                        for (int count = 0; count <= dtReference.Rows.Count - 1; count++)
                        {
                            if (dtReference.Rows[count]["fName"] != null)
                                fName = Convert.ToString(dtReference.Rows[count]["fName"]);

                            if (dtReference.Rows[count]["lName"] != null)
                                lName = Convert.ToString(dtReference.Rows[count]["lName"]);

                            if (dtReference.Rows[count]["nContactID"] != null)
                                contactID = Convert.ToInt64(dtReference.Rows[count]["nContactID"]);

                            if (dtReference.Rows[count]["sNPI"] != null)
                                nPI = Convert.ToString(dtReference.Rows[count]["sNPI"]);

                            if (dtReference.Rows[count]["email"] != null)
                                email = Convert.ToString(dtReference.Rows[count]["email"]);

                            fullName = "";

                            if ((fName != "") && (lName != ""))
                            {
                                fullName = fName.Trim() + ' ' + lName.Trim();
                            }

                            if ((fName != "") && (lName == ""))
                            {
                                fullName = fName.Trim();
                            }
                            if ((fName == "") && (lName != ""))
                            {
                                fullName = lName.Trim();
                            }

                            AddToList(fullName, contactID, nPI, email);

                        }
                    }
                }


            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (dtReference != null)
                {
                    dtReference.Dispose();
                    dtReference = null;
                }

                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }

            }

        }

        private void GetProviderEmail(long providerID)
        {
            clsSecureMessageDB clsDb = null;

            try
            {
                clsDb = new clsSecureMessageDB();
                gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress = Convert.ToString(clsDb.GetProviderEmailByID(providerID));
                lblFrom.Content = Convert.ToString(clsDb.GetProviderEmailByID(providerID));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }
            }
        }

        private void GetPatientDetails(long PatientID)
        {
            clsSecureMessageDB clsDb = null;
            DataSet dsPatient = null;
            DataTable oDataTable = null;
            AgeDetail _Age = null;
            string _Code = string.Empty;
            string _Name = string.Empty;
            string _strAge = string.Empty;
            string _Gender = string.Empty;
            string _BirthTime = string.Empty;
            bool _IsPediatric = false;


            try
            {
                clsDb = new clsSecureMessageDB();
                //dsPatient = new DataSet();
                dsPatient = clsDb.GetPatientDetails(PatientID);
                

                if (dsPatient != null)
                {
                    _IsPediatric = GetPediatricSetting(dsPatient);

                  //  oDataTable = new DataTable();
                    oDataTable = dsPatient.Tables["AgeSettings"];

                    if (dsPatient.Tables["AgeSettings"].Rows.Count > 0)
                    {


                        Int16 nShowAgeInDays = 0;
                        if ((Convert.ToString(oDataTable.Rows[0][0]).ToUpper() == "SHOW AGE IN DAYS") && (!string.IsNullOrEmpty(Convert.ToString(oDataTable.Rows[0][0]).ToUpper())))
                        {
                            nShowAgeInDays = Convert.ToInt16(oDataTable.Rows[0][1]);
                            _ShowAgeInDays = Convert.ToBoolean(nShowAgeInDays);
                        }
                        else
                        {
                            nShowAgeInDays = Convert.ToInt16(oDataTable.Rows[1][1]);
                            _ShowAgeInDays = Convert.ToBoolean(nShowAgeInDays);
                        }

                        //' Age Limit in Years, Convert it to Days
                        if ((Convert.ToString(oDataTable.Rows[1][0]).ToUpper() == "AGE LIMIT") && (!string.IsNullOrEmpty(Convert.ToString(oDataTable.Rows[1][0]).ToUpper())))
                        {
                            _AgeLimit = Convert.ToInt64((oDataTable.Rows[1][1])) * 365;
                        }
                        else
                        {
                            _AgeLimit = Convert.ToInt64((oDataTable.Rows[0][1])) * 365;
                        }

                    }

           //         oDataTable = new DataTable();
                    oDataTable = dsPatient.Tables["PatientInfo"];

                    if (dsPatient.Tables["PatientInfo"].Rows.Count > 0)
                    {
             //           oDataTable = new DataTable();
                        oDataTable = dsPatient.Tables["PatientInfo"];
                        _Code = Convert.ToString(oDataTable.Rows[0]["sPatientCode"]);
                        _Name = Convert.ToString(oDataTable.Rows[0]["PatientName"]);
                        _Gender = Convert.ToString(oDataTable.Rows[0]["sGender"]);


                        _DateOfBirth = Convert.ToDateTime(oDataTable.Rows[0]["dtDOB"]);
                        _BirthTime = Convert.ToString(oDataTable.Rows[0]["sBirthTime"]);
                        _strAge = Convert.ToString(oDataTable.Rows[0]["Age"]);


                        if (_IsPediatric == false)
                        {
                            _Age = FormatAge(_DateOfBirth);
                        }
                        else
                        {
                            
                            _TimeSpan = GetAgeInHrs(_DateOfBirth, _BirthTime);

                            if (_TimeSpan != TimeSpan.Zero)
                            {

                                if (Convert.ToInt32(_TimeSpan.Days) < 4)
                                {
                                    _Age = new AgeDetail();
                                    _Age.Hours = Convert.ToInt64(_TimeSpan.TotalHours);
                                    _Age.Age = _TimeSpan.TotalHours.ToString("0") + " Hours";
                                }

                                else if ((_TimeSpan.TotalDays > 4) && ((_TimeSpan.TotalDays <= 28) || (_TimeSpan.Hours == 0)))
                                {
                                    _ShowAgeInDays = true;
                                    _Age = FormatAge(_DateOfBirth);
                                }
                                else if (_TimeSpan.TotalDays > 28 && _TimeSpan.TotalDays <= 90)
                                {
                                    _Age = new AgeDetail();
                                    _Age.Age = (_TimeSpan.Days / 7).ToString("0") + " Weeks";
                                }
                                else
                                {
                                    _Age = FormatAge(_DateOfBirth);
                                    if (_Age.Years < 2 && _Age.Months >= 0)
                                    {
                                        _Age.Age = (_Age.Years * 12) + _Age.Months + " Months";
                                    }
                                }

                            }
                        }
                    }
                    if (_Age == null)
                    {
                        _Age = new AgeDetail();
                    }
                    tbPatientName.Text = _Name;
                    tbPatientBorn.Text = _DateOfBirth.ToShortDateString() + " (" + _Age.Age + ")";
                    tbPatientGender.Text = _Gender;
                    tbPatientCode.Text = _Code;


                }

            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (_Age != null)
                {
                    // _Age.Dispose();
                    _Age = null;
                }

                if (oDataTable != null)
                {
                    oDataTable.Dispose();
                    oDataTable = null;
                }

                if (dsPatient != null)
                {
                    dsPatient.Dispose();
                    dsPatient = null;
                }


                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }



            }
        }

        public TimeSpan GetAgeInHrs(System.DateTime _DateOfBirth, string _BirthTime)
        {
           
            string sDateTime = _DateOfBirth.Date.ToShortDateString() + " " + _BirthTime;
            TimeSpan AgeDiff =  DateTime.Now.Subtract(Convert.ToDateTime(sDateTime));
            return AgeDiff;
        }

        public AgeDetail FormatAge(DateTime BirthDate)
        {
            // Compute the difference between BirthDate 'CODE FROM gloPM
            //year and end year. 
            bool IsBirthDateLeap = false;
            int years = DateTime.Now.Year - BirthDate.Year;
            int months = 0;

            int days = 0;
            //Test if BirthDay for LeapYear.
            if (BirthDate.Day == 29 && BirthDate.Month == 2)
            {
                IsBirthDateLeap = true;
            }
            // Check if the last year was a full year. 
            if (DateTime.Now < BirthDate.AddYears(years) && years != 0)
            {
                years -= 1;
            }
            BirthDate = BirthDate.AddYears(years);
            // Now we know BirthDate <= end and the diff between them 
            // is < 1 year. 
            if (BirthDate.Year == DateTime.Now.Year)
            {
                months = DateTime.Now.Month - BirthDate.Month;
            }
            else
            {
                months = (12 - BirthDate.Month) + DateTime.Now.Month;
            }
            // Check if the last month was a full month. 
            if (DateTime.Now < BirthDate.AddMonths(months) && months != 0)
            {
                months -= 1;
            }
            BirthDate = BirthDate.AddMonths(months);
            // Now we know that BirthDate < end and is within 1 month 
            // of each other. 
            days = (DateTime.Now - BirthDate).Days;

            //To Adjust Age if BirthDate is 29th Feb in leap year
            //'Sequence of following IF code is too important.. DON'T MODIFY
            if (IsBirthDateLeap == true)
            {
                days -= 1;
                if (DateTime.Now.Day == 29 && DateTime.Now.Month == 2)
                {
                    days += 1;
                }
                else if (DateTime.Now.Year % 4 == 0)
                {
                    days += 1;
                }
                if (days < 0 && DateTime.Now.Year % 4 != 0)
                {
                    days = 30;
                    months = months - 1;
                    if (months < 0)
                    {
                        months = 11;
                        years = years - 1;
                    }
                }
                if (months == 12)
                {
                    days = 30;
                    months = 11;
                }
            }

            //Return years & " years " & months & " months " & days & " days"
            //Following code to display age in Numeric and Text
            AgeDetail age = new AgeDetail();
            //age.Age = years & " Years " & months & " Months " & days & " Days"
            //' Cases

            //'20081119   ''Following Code to Store ExactAge in String
            string _AgeStr = "";
            TimeSpan t = (System.DateTime.Now.Date - Convert.ToDateTime(_DateOfBirth));
            double tDays = Convert.ToInt64(t.Days);

            if (_ShowAgeInDays == true && _AgeLimit >= tDays)
            {
                if (years == 0)
                {
                    if (months == 0)
                    {
                        if (days <= 1)
                        {
                            _AgeStr = days + " Day";
                        }
                        else
                        {
                            _AgeStr = days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = months + " Month";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = months + " Months";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = months + " Months " + days + " Days";
                        }
                    }
                }
                else if (years == 1)
                {
                    if (months == 0)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year " + months + " Month ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Year " + months + " Months ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Year " + months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Year " + months + " Months " + days + " Days";
                        }
                    }
                }
                else if (years > 1)
                {
                    if (months == 0)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years ";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + days + " Days";
                        }
                    }
                    else if (months == 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years " + months + " Month";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + months + " Month " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + months + " Month " + days + " Days";
                        }
                    }
                    else if (months > 1)
                    {
                        if (days == 0)
                        {
                            _AgeStr = years + " Years " + months + " Months";
                        }
                        else if (days == 1)
                        {
                            _AgeStr = years + " Years " + months + " Months " + days + " Day";
                        }
                        else
                        {
                            _AgeStr = years + " Years " + months + " Months " + days + " Days";
                        }
                    }
                }
                //ShowAgeInDay is False OR AgeLimit less than Settings.
            }
            else
            {
                if (years == 0)
                {
                    //Added by pravin on 11/25/2008
                    //                If months = 0 And months = 1 Then
                    if (months == 1)
                    {
                        _AgeStr = months + " Month";
                    }
                    else if (months > 1)
                    {
                        _AgeStr = months + " Months";
                    }
                }
                else if (years == 1)
                {
                    if (months == 0)
                    {
                        _AgeStr = years + " Year ";
                    }
                    else if (months == 1)
                    {
                        _AgeStr = years + " Year " + months + " Month ";
                    }
                    else if (months > 1)
                    {
                        _AgeStr = years + " Year " + months + " Months ";
                    }
                }
                else if (years > 1)
                {
                    if (months == 0)
                    {
                        _AgeStr = years + " Years ";
                    }
                    else if (months == 1)
                    {
                        _AgeStr = years + " Years " + months + " Month ";
                    }
                    else if (months > 1)
                    {
                        _AgeStr = years + " Years " + months + " Months ";
                    }
                }
                //Added by pravin if age in days  11/25/2008
                if (years == 0 && months == 0)
                {
                    if (days <= 1)
                    {
                        _AgeStr = days + " Day";
                    }
                    else
                    {
                        _AgeStr = days + " Days";


                    }
                }
            }
            age.Age = _AgeStr;
            age.Years = Convert.ToInt16(years);
            age.Months = Convert.ToInt16(months);
            age.Days = Convert.ToInt16(days);
            return age;
        }

        private void ControlSizeChanged()
        {
            try
            {
                //(Wfh.Child as gloUC_PatientStrip).Height = (Wfh.Child as gloUC_PatientStrip).Height;
                //PatientStrip.Height = (Wfh.Child as gloUC_PatientStrip).Height;

            }
            catch //(Exception ex)
            {
            }
        }

        private void AddPatient(long patientID)
        {

          //  clsSecureMessageDB clsDb = null;
          //  DataTable dtReference = null;

            try
            {
                if (PatientStrip.Visibility == Visibility.Visible)
                {
                    if (patientID != gloSurescriptSecureMessage.SecureMessageProperties.PatientID)
                    {
                        if ((lstTo.Items.Count > 0) || (lstAttachment.Items.Count > 0))
                        {
                             //The To-list and Attachments will be deleted

                            if ((System.Windows.MessageBox.Show("Message patient has been changed."+System.Environment.NewLine+"Removing any addresses in the To List and any message Attachments.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OKCancel, MessageBoxImage.Information)) == MessageBoxResult.OK)
                            {
                                // clsDb = new clsSecureMessageDB();
                                // dtReference = clsDb.GetReferalList(patientID);
                                //if (_isSelected == true)
                                // {
                                if (lstTo.Items.Count > 0)
                                {
                                    lstTo.Items.Clear();
                                }

                                if (lstAttachment.Items.Count > 0)
                                {
                                    lstAttachment.Items.Clear();
                                }

                                //}

                            }

                        }
                    }
                }
               
                    PatientID = patientID;
                    gloSurescriptSecureMessage.SecureMessageProperties.PatientID = patientID;
                    GetPatientDetails(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                    PatientStrip.Visibility = Visibility.Visible;
                    _IsPatientLoad = true;
              


                //string fName = string.Empty;
                //string lName = string.Empty;
                //long contactID = 0;
                //string fullName = string.Empty;
                //string nPI = string.Empty;
                //string email = string.Empty;where cm.sDescription='Consent form'

                //if (dtReference != null)
                //{
                //    if (dtReference.Rows.Count > 0)
                //    {
                //        if (hashProviders == null)
                //        { hashProviders = new HashSet<long>(); }

                //        for (int count = 0; count <= dtReference.Rows.Count - 1; count++)
                //        {
                //            if (dtReference.Rows[count]["fName"] != null)
                //                fName = Convert.ToString(dtReference.Rows[count]["fName"]);

                //            if (dtReference.Rows[count]["lName"] != null)
                //                lName = Convert.ToString(dtReference.Rows[count]["lName"]);

                //            if (dtReference.Rows[count]["nContactID"] != null)
                //                contactID = Convert.ToInt64(dtReference.Rows[count]["nContactID"]);

                //            if (!hashProviders.Contains(contactID))
                //            { hashProviders.Add(contactID); }

                //            if (dtReference.Rows[count]["sNPI"] != null)
                //                nPI = Convert.ToString(dtReference.Rows[count]["sNPI"]);

                //            if (dtReference.Rows[count]["email"] != null)
                //                email = Convert.ToString(dtReference.Rows[count]["email"]);

                //            fullName = "";

                //            if ((fName != "") && (lName != ""))
                //            {
                //                fullName = lName.Trim() + ' ' + fName.Trim();
                //            }

                //            if ((fName != "") && (lName == ""))
                //            {
                //                fullName = fName.Trim();
                //            }
                //            if ((fName == "") && (lName != ""))
                //            {
                //                fullName = lName.Trim();
                //            }                          
                //                AddToList(fullName, contactID, nPI, email);
                //                _isSelected = true;                                                       
                //        }
                       
                //    }

                //    if (ContactID != 0)
                //    {
                //        if (hashProviders == null)
                //        { hashProviders = new HashSet<long>(); }

                //        if (!hashProviders.Contains(ContactID))
                //        {
                //            clsDb = new clsSecureMessageDB();
                //            DataTable dtReferrals = clsDb.GetReferalListByContactID(ContactID);
                //            if (dtReferrals != null)
                //            {
                //                long nToAddContactID = 0;

                //                if (dtReferrals.Rows.Count > 0)
                //                {
                //                    if (dtReferrals.Rows[0]["fName"] != null)
                //                        fName = Convert.ToString(dtReferrals.Rows[0]["fName"]);

                //                    if (dtReferrals.Rows[0]["lName"] != null)
                //                        lName = Convert.ToString(dtReferrals.Rows[0]["lName"]);

                //                    if (dtReferrals.Rows[0]["nContactID"] != null)
                //                        nToAddContactID = Convert.ToInt64(dtReferrals.Rows[0]["nContactID"]);

                //                    hashProviders.Add(nToAddContactID);

                //                    if (dtReferrals.Rows[0]["sNPI"] != null)
                //                        nPI = Convert.ToString(dtReferrals.Rows[0]["sNPI"]);

                //                    if (dtReferrals.Rows[0]["email"] != null)
                //                        email = Convert.ToString(dtReferrals.Rows[0]["email"]);

                //                    fullName = "";

                //                    if ((fName != "") && (lName != ""))
                //                    {
                //                        fullName = lName.Trim() + ' ' + fName.Trim();
                //                    }

                //                    if ((fName != "") && (lName == ""))
                //                    {
                //                        fullName = fName.Trim();
                //                    }
                //                    if ((fName == "") && (lName != ""))
                //                    {
                //                        fullName = lName.Trim();
                //                    }
                //                    AddToList(fullName, nToAddContactID, nPI, email);
                //                }
                //            }
                //        }                                               
                //    }
                //}


            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                //if (dtReference != null)
                //{
                //    dtReference.Dispose();
                //    dtReference = null;
                //}

                //if (clsDb != null)
                //{
                //    clsDb.Dispose();
                //    clsDb = null;
                //}

            }

        }


        private void AddPatientFromOrder(long patientID)
        {

              clsSecureMessageDB clsDb = null;
              DataTable dtReferrals = null;
            
            try
            {
                clsDb = new clsSecureMessageDB();
               // dtReference = clsDb.GetReferalList(patientID);

                if (PatientStrip.Visibility == Visibility.Visible)
                {
                    if (patientID != gloSurescriptSecureMessage.SecureMessageProperties.PatientID)
                    {
                        if ((lstTo.Items.Count > 0) || (lstAttachment.Items.Count > 0))
                        {
                            //The To-list and Attachments will be deleted

                            if ((System.Windows.MessageBox.Show("Message patient has been changed." + System.Environment.NewLine + "Removing any addresses in the To List and any message Attachments.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OKCancel, MessageBoxImage.Information)) == MessageBoxResult.OK)
                            {
                               
                                 if (_isSelected == true)
                                 {
                                
                                    if (lstTo.Items.Count > 0)
                                   {
                                    lstTo.Items.Clear();
                                   }

                                    if (lstAttachment.Items.Count > 0)
                                   {
                                    lstAttachment.Items.Clear();
                                    }

                                }

                            }

                        }
                    }
                }

                PatientID = patientID;
                gloSurescriptSecureMessage.SecureMessageProperties.PatientID = patientID;
                GetPatientDetails(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                PatientStrip.Visibility = Visibility.Visible;
                _IsPatientLoad = true;



                string fName = string.Empty;
                string lName = string.Empty;
                //long contactID = 0;
                string fullName = string.Empty;
                string nPI = string.Empty;
                string email = string.Empty;

                //if (dtReference != null)
                //{
                //    if (dtReference.Rows.Count > 0)
                //    {
                //        if (hashProviders == null)
                //        { hashProviders = new HashSet<long>(); }

                //        for (int count = 0; count <= dtReference.Rows.Count - 1; count++)
                //        {
                //            if (dtReference.Rows[count]["fName"] != null)
                //                fName = Convert.ToString(dtReference.Rows[count]["fName"]);

                //            if (dtReference.Rows[count]["lName"] != null)
                //                lName = Convert.ToString(dtReference.Rows[count]["lName"]);

                //            if (dtReference.Rows[count]["nContactID"] != null)
                //                contactID = Convert.ToInt64(dtReference.Rows[count]["nContactID"]);

                //            if (!hashProviders.Contains(contactID))
                //            { hashProviders.Add(contactID); }

                //            if (dtReference.Rows[count]["sNPI"] != null)
                //                nPI = Convert.ToString(dtReference.Rows[count]["sNPI"]);

                //            if (dtReference.Rows[count]["email"] != null)
                //                email = Convert.ToString(dtReference.Rows[count]["email"]);

                //            fullName = "";

                //            if ((fName != "") && (lName != ""))
                //            {
                //                fullName = lName.Trim() + ' ' + fName.Trim();
                //            }

                //            if ((fName != "") && (lName == ""))
                //            {
                //                fullName = fName.Trim();
                //            }
                //            if ((fName == "") && (lName != ""))
                //            {
                //                fullName = lName.Trim();
                //            }                          
                //                AddToList(fullName, contactID, nPI, email);
                //                _isSelected = true;                                                       
                //        }

                //    }

                    if (ContactID != 0)
                    {
                        if (hashProviders == null)
                        { hashProviders = new HashSet<long>(); }

                        if (!hashProviders.Contains(ContactID))
                        {
                            clsDb = new clsSecureMessageDB();
                            dtReferrals = clsDb.GetReferalListByContactID(ContactID);
                            if (dtReferrals != null)
                            {
                                long nToAddContactID = 0;

                                if (dtReferrals.Rows.Count > 0)
                                {
                                    if (dtReferrals.Rows[0]["fName"] != null)
                                        fName = Convert.ToString(dtReferrals.Rows[0]["fName"]);

                                    if (dtReferrals.Rows[0]["lName"] != null)
                                        lName = Convert.ToString(dtReferrals.Rows[0]["lName"]);

                                    if (dtReferrals.Rows[0]["nContactID"] != null)
                                        nToAddContactID = Convert.ToInt64(dtReferrals.Rows[0]["nContactID"]);

                                    hashProviders.Add(nToAddContactID);

                                    if (dtReferrals.Rows[0]["sNPI"] != null)
                                        nPI = Convert.ToString(dtReferrals.Rows[0]["sNPI"]);

                                    if (dtReferrals.Rows[0]["email"] != null)
                                        email = Convert.ToString(dtReferrals.Rows[0]["email"]);

                                    fullName = "";

                                    if ((fName != "") && (lName != ""))
                                    {
                                        fullName = lName.Trim() + ' ' + fName.Trim();
                                    }

                                    if ((fName != "") && (lName == ""))
                                    {
                                        fullName = fName.Trim();
                                    }
                                    if ((fName == "") && (lName != ""))
                                    {
                                        fullName = lName.Trim();
                                    }
                                    AddToList(fullName, nToAddContactID, nPI, email);
                                }
                            }
                        }                                               
                    }
                }


            //}

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (dtReferrals != null)
                {
                    dtReferrals.Dispose();
                    dtReferrals = null;
                }

                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }

            }

        }
        private static FontFamily CallibiriFontFamily = new FontFamily("Callibri");
        private void AddToList(string fName, long patientID, string nPI, string eMail)
        {
            StackPanel ostackInner = null;
            TextBlock olbl = null;
            TextBlock olblEmail = null;
            TextBlock olblock = null;
            Image oimgDelete = null;

            System.Windows.Controls.ContextMenu oContextmainMenu = null;
            Image oDownloadIcon = null;
            System.Windows.Controls.MenuItem mnuItemDownload = null;


            bool _isExists = false;
            long contactID = 0;
            string sEmail = string.Empty;

            try
            {


                for (int countTo = 0; countTo <= lstTo.Items.Count - 1; countTo++)
                {
                    StackPanel stac = (StackPanel)lstTo.Items.GetItemAt(countTo);
                    TextBlock tBlock = (TextBlock)stac.Children[0];
                    TextBlock ttBlock = (TextBlock)stac.Children[1];
                    contactID = Convert.ToInt64(tBlock.Tag);
                    sEmail = Convert.ToString(ttBlock.Text);

                    if (ContactID > 0)
                    {
                        if (contactID == patientID)
                        {
                            _isExists = true;
                            break;
                        }
                    }

                    if (sEmail == eMail)
                    {
                        _isExists = true;
                        break;
                    }
                }

                if (_isExists == false)
                {
                    
                    ostackInner = new StackPanel();
                    olbl = new TextBlock();
                    olblEmail = new TextBlock();
                    oimgDelete = new Image();
                    ostackInner = new StackPanel();
                    ostackInner.Orientation = System.Windows.Controls.Orientation.Horizontal;
                 //   ostackInner.Orientation = System.Windows.Controls.Orientation.Vertical;
                    olbl = new TextBlock();
                    olbl.Margin = new Thickness(2, 4, 0, 0);
                    olbl.TextWrapping = TextWrapping.Wrap;
                    olbl.Foreground = Brushes.Black;
                    olbl.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                    olbl.FontSize = 12;
                    olbl.Text = fName+"<"+eMail+">";
                    olbl.ToolTip = fName + "<" + eMail + ">";
                    olbl.Tag = patientID;
                    olbl.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    olbl.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                    olblEmail = new TextBlock();
                    olblEmail.Margin = new Thickness(2, 4, 0, 0);
                    olblEmail.TextWrapping = TextWrapping.Wrap;
                    olblEmail.Foreground = Brushes.Black;
                    olblEmail.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                    olblEmail.FontSize = 12;
                    olblEmail.Text = eMail;
                    olblEmail.Visibility = Visibility.Collapsed;
                    //olblEmail.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    //olblEmail.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                    olblock = new TextBlock();
                    olblock.Margin = new Thickness(2, 4, 0, 0);
                    olblock.TextWrapping = TextWrapping.Wrap;
                    olblock.Foreground = Brushes.Black;
                    olblock.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                    olblock.FontSize = 12;
                    olblock.Text = nPI;
                    olblock.Tag = fName;
                    olblock.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    olblock.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    olblock.Visibility = Visibility.Collapsed;

                    oimgDelete = new Image();
                    oimgDelete.Height = 20;
                    oimgDelete.Width = 20;
                    oimgDelete.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsCloseBlackBitmapImage;//new System.Windows.Media.Imaging.BitmapImage(new Uri("/gloSurescriptSecureMessage;component/Image/close-black.ico", UriKind.Relative));

                    System.Windows.Controls.ToolTip tt = new System.Windows.Controls.ToolTip();
                    tt.Content = "Remove";
                    oimgDelete.ToolTip = tt;
                    oimgDelete.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    oimgDelete.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                    oimgDelete.MouseDown += new MouseButtonEventHandler(ImgToDelete_MouseDown);
                    oimgDelete.MouseLeftButtonUp += new MouseButtonEventHandler(ImgToDelete_MouseUp);

                    oContextmainMenu = new System.Windows.Controls.ContextMenu();

                    //System.Windows.Media.Imaging.BitmapImage imgDownload = new System.Windows.Media.Imaging.BitmapImage();
                    //imgDownload.BeginInit();
                    //Uri myUri = new Uri("graphics\\delete.gif", UriKind.RelativeOrAbsolute);
                    //imgDownload.UriSource = myUri;
                    //imgDownload.EndInit();


                    oDownloadIcon = new Image();
                    oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDeleteBitmapImage;//imgDownload;
                    oDownloadIcon.Height = 16;
                    oDownloadIcon.Width = 16;

                    mnuItemDownload = new System.Windows.Controls.MenuItem();
                    mnuItemDownload.Header = "Delete";
                    mnuItemDownload.Icon = oDownloadIcon;
                    //mnuItemDownload.Uid = AttachmentID;
                    //mnuItemDownload.Tag = _FileExt;
                    mnuItemDownload.Click += new RoutedEventHandler(DeleteFromTo);
                    oContextmainMenu.Items.Add(mnuItemDownload);

                    ostackInner.Children.Add(olbl);
                    ostackInner.Children.Add(olblEmail);
                    ostackInner.Children.Add(olblock);
                    ostackInner.Children.Add(oimgDelete);

                    ostackInner.ContextMenu = oContextmainMenu;

                    lstTo.ScrollIntoView(lstAttachment);
                    lstTo.Items.Add(ostackInner);
                    lstTo.SelectedIndex = 0;
                }
                else
                {
                    System.Windows.MessageBox.Show("Contact : " + sEmail + " already exists.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                }
            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {

                if (mnuItemDownload != null)
                {
                    mnuItemDownload = null;
                }

                if (oDownloadIcon != null)
                {
                    oDownloadIcon = null;
                }

                if (oContextmainMenu != null)
                {
                    oContextmainMenu = null;
                }


                if (oimgDelete != null)
                {
                    oimgDelete = null;
                }

                if (olblock != null)
                {
                    olblock = null;
                }

                if (olbl != null)
                {
                    olbl = null;
                }

                if (ostackInner != null)
                {
                    ostackInner = null;
                }

            }

        }

        private void DeleteFromTo(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem oMenuSender = null;
            double toWidth = lstTo.Width;
            try
            {
                oMenuSender = (System.Windows.Controls.MenuItem)sender;

                if (oMenuSender != null)
                {

                    if (lstTo.SelectedIndex >= 0)
                    {
                        if ((System.Windows.MessageBox.Show("Are you sure you want to remove '" + GetName(lstTo.SelectedIndex) + "' ?", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
                        {
                            lstTo.Items.Remove(lstTo.SelectedItem);

                            if (lstTo.Items.Count >= 0)
                            {
                                lstTo.SelectedIndex = 0;
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

                oMenuSender = null;

                //Window_SizeChanged(null, null);
                _IsPatientLoad = true;
                lstAttachment.Width = toWidth + 65;
                lstTo.Width = toWidth;     

            }

        }
        
        private void CalculateSize()
        {
            double size = 0.0;

            for (int countAttach = 0; countAttach < lstAttachment.Items.Count; countAttach++)
            {
                StackPanel stacPanel = (StackPanel)lstAttachment.Items.GetItemAt(countAttach);
                TextBlock txtLength = (TextBlock)stacPanel.Children[4];
                size = size + Convert.ToDouble(txtLength.Tag);

            }

            maxSize = size;
        }

        private void AddRefferels(DataTable dtRef)
        {

            clsSecureMessageDB clsDb = null;
            DataTable dtReference = null;

            try
            {


                clsDb = new clsSecureMessageDB();
                dtReference = clsDb.GetReferalListByDataTable(dtRef);
                //if (lstTo.Items.Count > 0)
                //{
                //    int countTo = lstTo.Items.Count;
                //    for (int count = 0; count < countTo; count++)
                //    {
                //        lstTo.Items.RemoveAt(count);
                //    }
                //}
                // gloSurescriptSecureMessage.SecureMessageProperties.PatientID = patientID;
                // GetPatientDetails(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                //PatientStrip.Visibility = Visibility.Collapsed;

                string fName = string.Empty;
                string lName = string.Empty;
                long contactID = 0;
                string fullName = string.Empty;
                string nPI = string.Empty;
                string email = string.Empty;

                if (dtReference != null)
                {
                    if (dtReference.Rows.Count > 0)
                    {

                        for (int count = 0; count <= dtReference.Rows.Count - 1; count++)
                        {
                            if (dtReference.Rows[count]["fName"] != null)
                                fName = Convert.ToString(dtReference.Rows[count]["fName"]);

                            if (dtReference.Rows[count]["lName"] != null)
                                lName = Convert.ToString(dtReference.Rows[count]["lName"]);

                            if (dtReference.Rows[count]["nContactID"] != null)
                                contactID = Convert.ToInt64(dtReference.Rows[count]["nContactID"]);

                            if (dtReference.Rows[count]["sNPI"] != null)
                                nPI = Convert.ToString(dtReference.Rows[count]["sNPI"]);

                            if (dtReference.Rows[count]["email"] != null)
                                email = Convert.ToString(dtReference.Rows[count]["email"]);

                            fullName = "";

                            if ((fName != "") && (lName != ""))
                            {
                                fullName = fName.Trim() + ' ' + lName.Trim();
                            }

                            if ((fName != "") && (lName == ""))
                            {
                                fullName = fName.Trim();
                            }
                            if ((fName == "") && (lName != ""))
                            {
                                fullName = lName.Trim();
                            }

                            AddToList(fullName, contactID, nPI, email);
                            _IsPatientLoad = true;

                        }
                    }
                }


            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (dtReference != null)
                {
                    dtReference.Dispose();
                    dtReference = null;
                }

                if (clsDb != null)
                {
                    clsDb.Dispose();
                    clsDb = null;
                }

            }





        }

        private void AttachClick()
        {
            OpenFileDialog fd = null;
            OpenFileDialog fd1 = null;
            gloCCDLibrary.gloCCDInterface ogloInterface = null;
            string[] supportfiletypes = null;
            double toWidth = lstTo.Width;
            ActualFileName = string.Empty;
            try
            {
                
                fd = new OpenFileDialog();
                ogloInterface = new gloCCDLibrary.gloCCDInterface();
                fd.Title = "Open File Dialog";
                //fd.InitialDirectory = "C:\\";
                fd.Filter = "All files|*.docx;*.doc;*.rtf;*.xml;*.pdf;*.html;*.htm;*.txt|XML files|*.xml|PDF files|*.pdf|Word files|*.docx;*.doc;*.rtf|HTML files|*.html;*.htm|Text files|*.txt|Zip files|*.zip";
               
                //fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                fd.FilterIndex = 1;
                fd.RestoreDirectory = true;
                fd.Multiselect = true;
                fd.ShowDialog(System.Windows.Forms.Form.ActiveForm);
                string SupportfileType = ".docx;.doc;.rtf;.xml;.pdf;.html;.htm;.txt;.xml;.pdf;.zip";
                 
                supportfiletypes = SupportfileType.Split(';');
                
                int cntFlag = 0;
                foreach (string file in fd.FileNames)
                {
                    fd1 = new OpenFileDialog();
                    fd1.FileName = file;
                
                    if (gloSettings.FolderSettings.AppTempFolderPath.Length + fd1.SafeFileName.Length <= 251)
                    {
                        if (supportfiletypes.Contains<string>(System.IO.Path.GetExtension(fd1.SafeFileName)) == true)
                        {
                            SendFileToAttachment(fd1.SafeFileName, fd1.FileName);
                            cntFlag = 1;
                        }
                        else
                        {
                            //Window_SizeChanged(null, null);
                            _IsPatientLoad = true;
                            lstAttachment.Width = toWidth + 65;
                            lstTo.Width = toWidth; 
                            System.Windows.MessageBox.Show(fd1.SafeFileName + " file not supported for attachment.\n\n We are supporting only .pdf,.docx,.doc,.rtf,.xml,.html,.htm,.txt files for attachment.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        }


                        //if (SupportfileType.Contains(System.IO.Path.GetExtension(fd1.SafeFileName)) == true)
                        //{
                        //    SendFileToAttachment(fd1.SafeFileName, fd1.FileName);
                        //}
                        //else
                        //{
                        //    System.Windows.MessageBox.Show("Length of file Name should be less than 35 characters", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        //}

                    }

                    else
                    {
                        //Window_SizeChanged(null, null);
                        _IsPatientLoad = true;
                        lstAttachment.Width = toWidth + 65;
                        lstTo.Width = toWidth;   
                        System.Windows.MessageBox.Show("File name is too large. Please reduce the file name.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                        
                    }
                    if (cntFlag == 1)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.NewMessage, gloAuditTrail.ActivityType.Export, "External file name :'" + Convert.ToString(fd1.SafeFileName) + "' attached to DIRECT message.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                    }
                    if (fd1 != null)
                    {
                        fd1.Dispose();
                        fd1 = null;
                    }

                  
                   
                }

            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (ogloInterface != null)
                {
                    ogloInterface.Dispose();
                    ogloInterface = null;
                }

                if (fd != null)
                {
                    fd.Dispose();
                    fd = null;
                }
                supportfiletypes = null;

            }
        }
        private void AttachClick_UnstructuredCDA()
        {
            OpenFileDialog fd = null;
            OpenFileDialog fd1 = null;
            gloCCDLibrary.gloCCDInterface ogloInterface = null;
            string[] supportfiletypes_unstructured = null;
            double toWidth = lstTo.Width;
            String UnstructuredCDAFilename = "";
            //String ZipFilename = "";
            string MediaType = "";
            ActualFileName = string.Empty;

            try
            {

                fd = new OpenFileDialog();
                ogloInterface = new gloCCDLibrary.gloCCDInterface();
                fd.Title = "Open File Dialog";
                fd.Filter = "All files|*.docx;*.doc;*.rtf;*.pdf;*.html;*.htm;*.txt,*.png,*.jpeg,*.tiff,*.jpg,*.gif| PDF files|*.pdf|Word files|*.docx;*.doc;*.rtf|HTML files|*.html;*.htm|TEXT files|*.txt|PNG|*.png|JPEG |*.jpeg|TIFF|*.tiff|GIF|*.gif";
                fd.FilterIndex = 1;
                fd.RestoreDirectory = true;
                fd.Multiselect = true;
                fd.ShowDialog(System.Windows.Forms.Form.ActiveForm);
                string SupportfileType_Unstructured = ".docx;.doc;.rtf;.pdf;.html;.htm;.txt;.gif;.jpeg;.png;.tiff;";
                supportfiletypes_unstructured = SupportfileType_Unstructured.Split(';');
                int cntFlag = 0;
                foreach (string file in fd.FileNames)
                {
                    fd1 = new OpenFileDialog();
                    fd1.FileName = file;

                    if (gloSettings.FolderSettings.AppTempFolderPath.Length + fd1.SafeFileName.Length <= 251)
                    {
                        if (supportfiletypes_unstructured.Contains<string>(System.IO.Path.GetExtension(fd1.SafeFileName)) == true)
                        {
                            ActualFileName = fd1.SafeFileName;
                                string extension = "";
                                extension = System.IO.Path.GetExtension(fd1.SafeFileName);
                                switch (extension.ToUpper())
                                {
                                    case ".PDF":
                                        MediaType = "application/pdf";
                                        break;
                                    case ".DOCX":
                                    case ".DOC":
                                        MediaType = "application/msword";
                                        break;
                                    case ".HTML":
                                    case ".HTM":
                                        MediaType = "text/html";
                                        break;
                                    case ".RTF":
                                        MediaType = "text/rtf";
                                        break;
                                    case ".GIF":
                                        MediaType = "image/gif";
                                        break;
                                    case ".TIFF":
                                        MediaType = "image/tiff";
                                        break;
                                    case ".JPEG":
                                        MediaType = "image/jpeg";
                                        break;
                                    case ".PNG":
                                        MediaType = "image/png";
                                        break;
                                    case ".TXT":
                                        MediaType = "text/plain";
                                        break;
                                }
                                byte[] pdfBytes = null;
                                String pdfBase64 = "";
                                pdfBytes = File.ReadAllBytes(fd1.FileName);
                                pdfBase64 = System.Convert.ToBase64String(pdfBytes);
                                gloCCDLibrary.gloCDADataExtraction Data = new gloCCDLibrary.gloCDADataExtraction();
                                if (PatientID == 0)
                                {
                                    System.Windows.MessageBox.Show("Please select patient from patient list to send secure message.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
                                    return;
                                }
                                else
                                {
                                    UnstructuredCDAFilename = Data.GenerateClinicalInformationForNonXml(PatientID, 1, pdfBase64, MediaType,fd1.SafeFileName);
                              
                                }
                                   //Dont  create directory structure
                                //if (XDMFilename != "")
                                //{
                                //    ZipFilename = CreateDirectoryStructure(XDMFilename);
                                //}
                                SendFileToAttachment(System.IO.Path.GetFileName(UnstructuredCDAFilename), UnstructuredCDAFilename);
                            cntFlag = 1;
                        }
                        else
                        {
                            //Window_SizeChanged(null, null);
                            _IsPatientLoad = true;
                            lstAttachment.Width = toWidth + 65;
                            lstTo.Width = toWidth;
                            System.Windows.MessageBox.Show(fd1.SafeFileName + " file not supported for attachment.\n\n We are supporting only .pdf,.docx,.doc,.rtf,.html,.htm,.txt files for attachment.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);


                        }

                    }

                    else
                    {
                        _IsPatientLoad = true;
                        lstAttachment.Width = toWidth + 65;
                        lstTo.Width = toWidth;
                        System.Windows.MessageBox.Show("File name is too large. Please reduce the file name.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);

                    }
                    if (cntFlag == 1)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.NewMessage, gloAuditTrail.ActivityType.Export, "External file name :'" + Convert.ToString(fd1.SafeFileName) + "' attached to DIRECT message.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR, true);
                    }
                    if (fd1 != null)
                    {
                        fd1.Dispose();
                        fd1 = null;
                    }



                }

            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {
                if (ogloInterface != null)
                {
                    ogloInterface.Dispose();
                    ogloInterface = null;
                }

                if (fd != null)
                {
                    fd.Dispose();
                    fd = null;
                }
                supportfiletypes_unstructured= null;

            }
        }

        //private string CreateDirectoryStructure(string XDMFileName)
        //{
        //    try
        //    {
                
        //        //string tempFolderPath = System.IO.Path.GetTempPath();
        //        string tempFolderPath = gloSettings.FolderSettings.AppTempFolderPath;
        //        string path = System.Windows.Forms.Application.StartupPath;
        //        string GUID = Convert.ToString(System.Guid.NewGuid());
        //        //String firstFolder = @"C:\" + GUID+ "-xdm";
        //        String firstFolder = tempFolderPath + "\\XDM\\" + GUID + "-xdm";
        //        string Destinationfolder = System.IO.Path.Combine(tempFolderPath, "XdmZip");
        //        if (!Directory.Exists(firstFolder))
        //        {
        //            Directory.CreateDirectory(firstFolder);
        //            String ReadMeFile = "";
        //            ReadMeFile = System.IO.Path.Combine(firstFolder,ReadMeFileName);
        //            System.IO.File.Copy(path + "\\ReadMe.txt", ReadMeFile);

        //            String HtmFile = "";
        //            HtmFile = System.IO.Path.Combine(firstFolder, HtmlFileName);
        //            System.IO.File.Copy(path + "\\Index.htm", HtmFile);
        //        }
        //        string subFolder = System.IO.Path.Combine(firstFolder,XDMFolderName);

        //        if (!Directory.Exists(subFolder))
        //        {
        //            Directory.CreateDirectory(subFolder);
        //            String XSLPath = "";
        //            XSLPath = System.IO.Path.Combine(subFolder,XSLFileName);
        //            System.IO.File.Copy(path + "\\gloCCDAcss_MU2.xsl", XSLPath);

        //            String CDAFile = "";
        //            CDAFile = System.IO.Path.Combine(subFolder, XMLFileName);
        //            System.IO.File.Copy(XDMFileName, CDAFile);
        //        }
        //        if (!Directory.Exists(Destinationfolder))
        //        {
        //            Directory.CreateDirectory(Destinationfolder);
        //        }
        //        String ZipFilePath = CreateZipFile(firstFolder, Destinationfolder);
        //        return ZipFilePath;
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //        throw;
        //    }
           
          
        //}
        

        //public static string CreateZipFile(string folderToZip, string targetFolder)
        //{
        //    try
        //    {
        //        string targetFile = string.Empty;
        //        if (!string.IsNullOrWhiteSpace(folderToZip) && Directory.Exists(folderToZip))
        //        {
        //            targetFile = System.IO.Path.Combine(targetFolder, System.IO.Path.GetFileName(folderToZip) + ".zip");
        //            ZipPath(targetFile, folderToZip, null, true, null);
                   
        //        }
        //        return targetFile;
        //    }
        //    catch (Exception)
        //    {
        //        return "";
        //        throw;
        //    }
         
               
            
        
        //}
        //public static void ZipPath(string zipFilePath, string sourceDir, string pattern, bool withSubdirs, string password)
        //{
        //    try
        //    {
        //        FastZip fz = new FastZip();
        //        if (password != null)
        //            fz.Password = password;

        //        fz.CreateZip(zipFilePath, sourceDir, withSubdirs, pattern);
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
      
        //}
      
        private void SendFileToAttachment(string safeFileName, string FileName)
        {
            gloCCDLibrary.gloCCDInterface ogloInterface = null;
            ogloInterface = new gloCCDLibrary.gloCCDInterface();
            string fileName = string.Empty;
            string Base64String = string.Empty;
            double fileLength = 0.0;
            //FileInfo fInfo;
            //string fName = string.Empty;
            //string fExt = string.Empty;
            //string fNewName = string.Empty;
            //int fExtLength = 0;

            if (!string.IsNullOrEmpty(FileName))
            {
                //fInfo=new FileInfo(FileName);
                //fName = fInfo.Name;
                //if (fName.Length > 35)
                //{
                //    fExt = fInfo.Extension;
                //    fExtLength = fExt.Length;

                //    fNewName = fName.Replace(fExt, "");

                //    if (fNewName.Length + (35 - fExtLength) > 35)
                //    {
                //        fNewName = fNewName.Substring(0, 35 - fExtLength);
                //    }
                //    else
                //    {

                //    }

                //    string DirectoryPath = fInfo.DirectoryName;
                //    string DestinationFile = DirectoryPath + "\\" + fNewName + fExt;
                //    File.Copy(FileName, DestinationFile);
                //}              



                fileName = FileName;
                Base64String = Convert.ToBase64String(File.ReadAllBytes(fileName), Base64FormattingOptions.InsertLineBreaks);
                fileLength = Base64String.Length;

                if (CalculateFileSize(fileLength))
                {
                    Byte[] arrByte = ogloInterface.ConvertFiletoBinary(FileName);
                    AddListAttachments(safeFileName, FileName, arrByte, Base64String);
                    arrByte = null;                    
                }
                else
                {
                    CalculateSize();
                    //System.Windows.MessageBox.Show("File size of '" & strFileName & "' is " & SetBytes(Bytes) & ". File size should not exceed 2MB.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information)  "File size Exceed";
                    System.Windows.MessageBox.Show("File '" + safeFileName + "'  size exceeded", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool CalculateFileSize(double Length)
        {
            if (Length > 1024)
            {
                double fileLength = Length / 1024;
                maxSize = maxSize + fileLength;
            }
            else
            {
                double fileLength = (Length / 1024f) / 1024f;
                fileLength = Math.Round(fileLength, 4);
                maxSize = maxSize + fileLength;
            }

            if (maxSize >= 19968) // 19.5 mb
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        
        private bool CalFileSize(double Length)
        {
            double fileLength = 0.0;

            if (Length > 1024)
            {
                fileLength = Length / 1024;
            }
            else
            {
                fileLength = (Length / 1024f) / 1024f;
                fileLength = Math.Round(fileLength, 4);
            }

            if (fileLength >= 20480) // 20.0 mb
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void InsertDataInToTable()
        {
            clsSecureMessageDB objclsSecureDB = null;
            List<Attachment> oLsAttach = null;
            Attachment objAttach = null;
            SecureMessage objSecureMessage = null;

            try
            {
                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                objclsSecureDB = new clsSecureMessageDB();
                oLsAttach = new List<Attachment>();
                Int16 countAttachment = Convert.ToInt16(lstAttachment.Items.Count);
                if (countAttachment > 0)
                {

                    for (int countAttach = 0; countAttach < lstAttachment.Items.Count; countAttach++)
                    {
                        objAttach = new Attachment();
                        StackPanel stacPanel = (StackPanel)lstAttachment.Items.GetItemAt(countAttach);
                        TextBlock textBlock = (TextBlock)stacPanel.Children[1];
                        TextBlock txtBlock = (TextBlock)stacPanel.Children[2];
                        TextBlock txt64Block = (TextBlock)stacPanel.Children[3];

                        string extension = txtBlock.Text;
                        objAttach.mimeType = Convert.ToString(txtBlock.Tag);
                        Int16 fileExtension = 0;
                        extension = extension.Remove(0, 1);
                        objAttach.base64 = Convert.ToString(txt64Block.Tag);

                        foreach (FileExtension fx in Enum.GetValues(typeof(FileExtension)))
                        {
                            if (extension.ToLower() == Convert.ToString(fx))
                            {
                                fileExtension = Convert.ToInt16(fx.GetHashCode());
                                break;
                            }

                        }


                        objAttach.moduleName = Convert.ToInt16(ModuleName.None.GetHashCode());
                        objAttach.fileExtension = fileExtension;
                        objAttach.documentName = textBlock.Text;
                        objAttach.iContent = (byte[])(textBlock.Tag);
                        oLsAttach.Add(objAttach);
                        textBlock = null;
                        stacPanel = null;
                    }
                }

                

                objSecureMessage = new SecureMessage();
                if (lstTo.Items.Count > 0)
                {
                    DateTime dtdate;
                    string Messagedescription = "";
                    for (int countTo = 0; countTo <= lstTo.Items.Count - 1; countTo++)
                    {
                        StackPanel stac = (StackPanel)lstTo.Items.GetItemAt(countTo);
                        TextBlock tBlock = (TextBlock)stac.Children[0];
                        TextBlock ttBlock = (TextBlock)stac.Children[1];


                        objSecureMessage = new SecureMessage();
                        objSecureMessage.relateMessageID = "";
                        objSecureMessage.version = "010";
                        objSecureMessage.release = "006";
                        objSecureMessage.highVersion = "010";
                        objSecureMessage.senderID = gloSurescriptSecureMessage.SecureMessageProperties.ProviderID;
                        objSecureMessage.receiverID = Convert.ToInt64(tBlock.Tag);
                        //objSecureMessage.From = gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress;
                        objSecureMessage.From = sFromDirectAddress;
                        objSecureMessage.FromQualifier = "DIRECT";
                        objSecureMessage.To = Convert.ToString(ttBlock.Text);
                        objSecureMessage.ToQualifier = "DIRECT";
                        objSecureMessage.subject = txtSubject.Text.ToString().Replace("–", "-");
                        objSecureMessage.messageBody = Convert.ToString(txtMessage.Text);

                        dtdate = System.DateTime.UtcNow;
                        string strdate = dtdate.ToString("yyyy-MM-dd");
                        string strtime = dtdate.ToString("hh:mm:ss");
                        string strUTCFormat = strdate + "T" + strtime + ".0Z";

                        objSecureMessage.dateTimeUTC = strUTCFormat;
                        objSecureMessage.dateTimeNormal = System.DateTime.Now;
                        objSecureMessage.isRead = 0;
                        objSecureMessage.patientID = gloSurescriptSecureMessage.SecureMessageProperties.PatientID;

                        if (gloSurescriptSecureMessage.SecureMessageProperties.PatientID > 0)
                        {
                            clsSecureMessageDB clsDb = new clsSecureMessageDB();
                            DataTable dtPatient = new DataTable();
                            dtPatient = clsDb.GetPatientDetailsforSecureMessage(gloSurescriptSecureMessage.SecureMessageProperties.PatientID);
                            if (dtPatient != null)
                            {
                                if (dtPatient.Rows.Count > 0)
                                {
                                    objSecureMessage.firstName = Convert.ToString(dtPatient.Rows[0]["sfirstname"]);
                                    objSecureMessage.lastName = Convert.ToString(dtPatient.Rows[0]["sLastName"]);
                                    objSecureMessage.Dob = Convert.ToString(dtPatient.Rows[0]["dob"]).Trim();
                                    objSecureMessage.gender = Convert.ToString(dtPatient.Rows[0]["Gender"]);
                                    objSecureMessage.zip = Convert.ToString(dtPatient.Rows[0]["sZIP"]);
                                    objSecureMessage.clinicCode = Convert.ToString(dtPatient.Rows[0]["sExternalCode"]);
                                    objSecureMessage.patientCode = Convert.ToString(dtPatient.Rows[0]["sPatientCode"]);
                                }
                            }
                            if (clsDb != null)
                            {
                                clsDb.Dispose();
                                clsDb = null;
                            }

                            if (dtPatient != null)
                            {
                                dtPatient.Dispose();
                                dtPatient = null;
                            }
                        }
                                              
                        objSecureMessage.noofAttachements = countAttachment;
                        objSecureMessage.MessageStatus = Convert.ToInt16(MessageStatus.Send.GetHashCode());
                        objSecureMessage.messageType = Convert.ToInt16(gloSurescriptSecureMessage.MessageType.Send.GetHashCode());
                        objSecureMessage.associated = 0;
                        objSecureMessage.deliveryStatusCode = "";
                        objSecureMessage.deliveryStatusDescription = "";
                        objSecureMessage.softwareVersion = System.Windows.Forms.Application.ProductVersion;
                        objSecureMessage.softwareProduct = System.Windows.Forms.Application.ProductName;
                        objSecureMessage.companyName = System.Windows.Forms.Application.CompanyName;
                        objSecureMessage.userName = gloSurescriptSecureMessage.SecureMessageProperties.UserName;
                        objSecureMessage.machineName = System.Environment.MachineName;
                        objSecureMessage.deleted = 0;
                        objSecureMessage.DocumentReferenceID = DocumentReferenceID;
                        objSecureMessage.UseCase = 0;

                        if (SecureMessageProperties.DelegatedProvider != null)
                        {
                            objSecureMessage.DelegatedUser = SecureMessageProperties.DelegatedProvider.UserLoginName;
                        }
                        else
                        {
                            objSecureMessage.DelegatedUser = gloSurescriptSecureMessage.SecureMessageProperties.UserName;
                        }

                        ttBlock = null;
                        tBlock = null;
                        stac = null;

                        objSecureMessage.messageID = "aaaaabbbbbcccccdddddeeeeefffffggggghhhhh";

                        if (CheckFinalFileLength(objSecureMessage, oLsAttach))
                        {
                            objSecureMessage.messageID = null;


                            if (objSecureMessage.noofAttachements > 0)
                            {
                                objSecureMessage.messageID = objclsSecureDB.InsertSureScriptMessageInDB(objSecureMessage, oLsAttach);
                            }
                            else
                            {
                                objSecureMessage.messageID = objclsSecureDB.InsertSureScriptMessageInDB(objSecureMessage);
                            }


                            if (!string.IsNullOrEmpty(objSecureMessage.messageID))
                            {
                                XmlSerializer xs = null;
                                FileStream fs = null;
                                N2NMessageType objN2N = null;

                                try
                                {
                                    byte[] byteArray = SecureMessage.GenerateXML(objSecureMessage, oLsAttach);
                                    byte[] Response = null;
                                    string key = string.Empty;

                                    gloSurescriptSecureMessage.gloDirectservice.IgloDirectClient oDirect;
                                    //oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc("http://localhost:1454/gloDirect.svc/Secure");
                                    if(gloSurescriptSecureMessage.SecureMessageProperties.IsStagingServerEnable )
                                        oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc(gloSurescriptSecureMessage.SecureMessageProperties.StagingServerUrl);
                                    else
                                        oDirect = gloSurescriptSecureMessage.SecureMessage.GetSecureMsgFSvc(gloSurescriptSecureMessage.SecureMessageProperties.ProductionServerUrl); 
                                    //oDirect.
                                    key = oDirect.Login("gloSecureMsg@ophit.net", "spX12ss@!!21nasik");
                                    //Response = oDirect.PostSecureMessage(byteArray);

                                    Response = oDirect.PostSecureMessage(objSecureMessage.messageID, objSecureMessage.From, objSecureMessage.To, SecureMessageProperties.SPID, SecureMessageProperties.ClinicName, SecureMessageProperties.AUSID, SecureMessageProperties.SiteID, SecureMessageProperties.Location, byteArray, (gloSurescriptSecureMessage.gloDirectservice.ClsglobalMessageType)gloSurescriptSecureMessage.MessageType.Send);

                                    if (Response != null)
                                    {
                                        string strFileName = GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                                        SecureMessage.ConvertBinarytoFile(Response, strFileName);

                                        if (strFileName.Trim() != "")
                                        {
                                            xs = new XmlSerializer(typeof(N2NMessageType));
                                            fs = new FileStream(strFileName, FileMode.Open);
                                            try
                                            {
                                                objN2N = (N2NMessageType)xs.Deserialize(fs);
                                            }
                                            catch //(Exception ex)
                                            {

                                                System.Windows.Forms.MessageBox.Show("Sure Script Unable to process Message", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                            }


                                            fs.Close();
                                            if (objN2N != null)
                                            {
                                                objSecureMessage = SecureMessage.ExtractXML(objN2N, objSecureMessage);

                                                if (objSecureMessage.deliveryStatusDescription == "" && objSecureMessage.deliveryStatusCode == "010")
                                                {
                                                    objSecureMessage.deliveryStatusDescription ="Clinical message delivered";
                                                }
                                                else if (objSecureMessage.deliveryStatusDescription == "" && objSecureMessage.deliveryStatusCode == "000")
                                                {
                                                   objSecureMessage.deliveryStatusDescription="Clinical message sent to partner network";
                                                }

                                                objSecureMessage.messageID = objclsSecureDB.InsertSureScriptMessageInDB(objSecureMessage);

                                                if (objSecureMessage.deliveryStatusDescription != "" )
                                                {
                                                    //System.Windows.Forms.MessageBox.Show(objSecureMessage.deliveryStatusDescription, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                                                    if (Messagedescription != "")
                                                    {
                                                        if (objSecureMessage.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") || objSecureMessage.deliveryStatusDescription.Contains("@"))
                                                        {
                                                            Messagedescription = Messagedescription + objSecureMessage.deliveryStatusDescription + "\n\n";
                                                        }
                                                        else
                                                        {
                                                            //Messagedescription = Messagedescription + objSecureMessage.deliveryStatusDescription + " for " + objSecureMessage.To + "\n";
                                                            Messagedescription = Messagedescription + objSecureMessage.From + "\n     " + objSecureMessage.deliveryStatusDescription + "\n\n";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //Messagedescription = objSecureMessage.deliveryStatusDescription + " for " + objSecureMessage.To + "\n";
                                                        if (objSecureMessage.deliveryStatusDescription.Contains("NetworkAddress has an invalid format") || objSecureMessage.deliveryStatusDescription.Contains("@"))
                                                        {
                                                            Messagedescription = objSecureMessage.deliveryStatusDescription + "\n\n";
                                                        }
                                                        else
                                                        {
                                                            Messagedescription = objSecureMessage.From + "\n    " + objSecureMessage.deliveryStatusDescription + "\n\n";
                                                        }
                                                    }

                                                }

                                            }
                                            else
                                            {
                                                objSecureMessage.deliveryStatusCode = "999";
                                                objSecureMessage.deliveryStatusDescription = "Processing";
                                                if (Messagedescription != "")
                                                {
                                                    Messagedescription = Messagedescription + objSecureMessage.To + "\n     " + "Message sending is in queue" + "\n\n";
                                                }
                                                else
                                                {
                                                    Messagedescription = objSecureMessage.To + "\n    " + "Message sending is in queue" + "\n\n";
                                                }
                                                objSecureMessage.messageID = objclsSecureDB.InsertSureScriptMessageInDB(objSecureMessage);
                                            }
                                            _isMessageSend = true;
                                            checkMailInstance = false;
                                            this.Close();

                                        }
                                    }
                                    else
                                    {
                                        objSecureMessage.deliveryStatusCode = "999";
                                        objSecureMessage.deliveryStatusDescription = "Processing";
                                        if (Messagedescription != "")
                                        {
                                            Messagedescription = Messagedescription + objSecureMessage.To + "\n     " + "Message sending is in queue" + "\n\n";
                                        }
                                        else
                                        {
                                            Messagedescription = objSecureMessage.To + "\n    " + "Message sending is in queue" + "\n\n";
                                        }
                                        objSecureMessage.messageID = objclsSecureDB.InsertSureScriptMessageInDB(objSecureMessage);

                                    }

                                    oDirect.Close();
                                    oDirect = null;

                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.SecureMessage, ActivityCategory.NewMessage, ActivityType.Send, "Secure Message Sent.", objSecureMessage.patientID, 0, Convert.ToInt64(objSecureMessage.senderID)  , ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR,true );
                                }
                                catch //(Exception ex)
                                {
                                    objSecureMessage.deliveryStatusCode = "999";
                                    objSecureMessage.deliveryStatusDescription = "Processing";
                                    if (Messagedescription != "")
                                    {
                                        Messagedescription = Messagedescription + objSecureMessage.To + "\n     " + "Message sending is in queue" + "\n\n";
                                    }
                                    else
                                    {
                                        Messagedescription = objSecureMessage.To + "\n    " + "Message sending is in queue" + "\n\n";
                                    }
                                    objSecureMessage.messageID = objclsSecureDB.InsertSureScriptMessageInDB(objSecureMessage);
                                    _isMessageSend = true;
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                                }
                                finally
                                {
                                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
                                    if (objN2N != null)
                                    {
                                        objN2N = null;
                                    }
                                    if (fs != null)
                                    {
                                        fs = null;
                                    }
                                    if (xs != null)
                                    {
                                        xs = null;
                                    }
                                }

                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("Message size exceeded more than 20MB. Please remove some content and try again ", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                            return;
                        }


                    }
                    if (Messagedescription != "")
                    {
                        System.Windows.Forms.MessageBox.Show(Messagedescription, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

                    }
                }

                objSecureMessage = null;
                oLsAttach = null;
                checkMailInstance = false;
                this.Close();
            }


            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                _isMessageSend = false;
            }

            finally
            {
                if (objSecureMessage != null)
                {
                    objSecureMessage.Dispose();
                    objSecureMessage = null;
                }

                if (objAttach != null)
                {
                    objAttach.Dispose();
                    objAttach = null;
                }
                if (oLsAttach != null)
                {
                    oLsAttach = null;
                }
                if (objclsSecureDB != null)
                {
                    objclsSecureDB.Dispose();
                    objclsSecureDB = null;
                }

            }



        }

        private bool CheckFinalFileLength(SecureMessage objSecureMessage, List<Attachment> oLsAttach)
        {
            N2NMessageType NewN2NMessage = null;
            bool isValid = false;
            try
            {
                NewN2NMessage = SecureMessage.CreateN2NMessage(objSecureMessage, oLsAttach);
                isValid = CheckAndGenerateXML(NewN2NMessage);
                return isValid;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return isValid;
            }
            finally
            {
                if (NewN2NMessage != null)
                {
                    NewN2NMessage = null;
                }
            }

        }

        private bool CheckAndGenerateXML(N2NMessageType message)
        {

            XmlSerializer xs = null;
            FileStream fs = null;
            double fileLength = 0.0;
            bool isVaild = false;
            try
            {
                string strFileName = GetFileName(gloSettings.FolderSettings.AppTempFolderPath);
                xs = new XmlSerializer(typeof(N2NMessageType));
                fs = new FileStream(strFileName, FileMode.Create);
                xs.Serialize(fs, message);
                fs.Close();

                FileInfo f = new FileInfo(strFileName);
                fileLength = Convert.ToDouble(f.Length);

                isVaild = CalFileSize(fileLength);
                return isVaild;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return isVaild;
            }
            finally
            {
                if (fs != null)
                {
                    fs = null;
                }
                if (xs != null)
                {
                    xs = null;
                }
            }





        }

        public static String GetFileName(String strAppPath)
        {
            try
            {

                ////clsException.UpdateLog("start GetFileName", LogFilePath, EnableLog);
                //string _NewDocumentName = "";

                //string _Extension = ".xml";
                //DateTime _dtCurrentDateTime = System.DateTime.Now;

                //int i = 0;
                //_NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + _Extension;
                //while (File.Exists(Convert.ToString(strAppPath) + "\\" + _NewDocumentName) == true)
                //{
                //    i = i + 1;
                //    _NewDocumentName = _dtCurrentDateTime.ToString("MMddyyyyhhmmssffftt") + "-" + i + _Extension;

                //}
                ////clsException.UpdateLog("End GetFileName", LogFilePath, EnableLog);
                //return Convert.ToString(strAppPath) + _NewDocumentName;
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

        private string GetMimeType(string extension)
        {
            string documentType = string.Empty;
            switch (extension.ToUpper())
            {
                case ".XML":
                    {
                        documentType = "application/xml";
                        break;
                    }
                case ".PDF":
                    {
                        documentType = "application/pdf";
                        break;
                    }
                case ".DOC":
                    {
                        documentType = "application/msword";
                        break;
                    }
                case ".DOCX":
                    {
                        documentType = "application/msword";
                        break;
                    }
                case ".ZIP":
                    {
                        documentType = "application/zip";
                        break;
                    }
                case ".HTML":
                    {
                        documentType = "text/html";
                        break;
                    }
                case ".HTM":
                    {
                        documentType = "text/html";
                        break;
                    }
                case ".TXT":
                    {
                        documentType = "text/plain";
                        break;
                    }
                case ".RTF":
                    {
                        documentType = "text/RTF";
                        break;
                    }

                default:
                    break;

            }

            return documentType;
        }

        private void AddListAttachments(string FileName, string FilePath, Byte[] arrByte, string strBase64)
        {

            StackPanel ostackInner = null;
            TextBlock olbl = null;
            TextBlock olbFile = null;
            TextBlock olb64 = null;
            TextBlock olblength = null;
            Image oimgAckw = null;
            Image oimgDelete = null;

            System.Windows.Controls.ContextMenu oContextmainMenu = null;
            Image oDownloadIcon = null;
            System.Windows.Controls.MenuItem mnuItemDownload = null;


            double fileLength = 0.0;
            double Length = 0.0;
            try
            {

                Length = strBase64.Length;

                if (Length > 1024)
                {
                    fileLength = Length / 1024;

                }
                else
                {
                    fileLength = (Length / 1024f) / 1024f;
                    fileLength = Math.Round(fileLength, 4);
                }

                ostackInner = new StackPanel();
                ostackInner.Orientation = System.Windows.Controls.Orientation.Horizontal;

                oimgAckw = new Image();
                oimgAckw.Margin = new Thickness(5, 0, 5, 0);
                oimgAckw.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsForwardBitmapImage;//new System.Windows.Media.Imaging.BitmapImage(new Uri("/gloSurescriptSecureMessage;component/Image/Forward Email.png", UriKind.Relative));
                oimgAckw.Height = 20;
                oimgAckw.Width = 20;
                oimgAckw.Stretch = Stretch.Fill;
                oimgAckw.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                oimgAckw.VerticalAlignment = System.Windows.VerticalAlignment.Top;

                olbl = new TextBlock();
                olbl.Margin = new Thickness(2, 4, 0, 0);
                olbl.TextWrapping = TextWrapping.Wrap;
                //olbl.Width = 130;
                olbl.Foreground = Brushes.Black;
                olbl.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                olbl.FontSize = 12;
                olbl.Text = FileName;
                olbl.MouseLeftButtonDown -= Border_MouseClick;
                olbl.MouseLeftButtonDown += Border_MouseClick;



                oContextmainMenu = new System.Windows.Controls.ContextMenu();

                //System.Windows.Media.Imaging.BitmapImage imgDownload = new System.Windows.Media.Imaging.BitmapImage();
                //imgDownload.BeginInit();
                //Uri myUri = new Uri("graphics\\delete.gif", UriKind.RelativeOrAbsolute);
                //imgDownload.UriSource = myUri;
                //imgDownload.EndInit();


                oDownloadIcon = new Image();
                oDownloadIcon.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsDeleteBitmapImage;//imgDownload;
                oDownloadIcon.Height = 16;
                oDownloadIcon.Width = 16;

                mnuItemDownload = new System.Windows.Controls.MenuItem();
                mnuItemDownload.Header = "Delete";
                mnuItemDownload.Icon = oDownloadIcon;
                //mnuItemDownload.Uid = AttachmentID;
                //mnuItemDownload.Tag = _FileExt;
                mnuItemDownload.Click += new RoutedEventHandler(BorderRight_MouseClick);
                oContextmainMenu.Items.Add(mnuItemDownload);



                olbl.Tag = arrByte;
                olbl.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                olbl.VerticalAlignment = System.Windows.VerticalAlignment.Top;


                olbFile = new TextBlock();
                olbFile.Margin = new Thickness(2, 4, 0, 0);
                olbFile.TextWrapping = TextWrapping.Wrap;
                //olbl.Width = 130;
                olbFile.Foreground = Brushes.Black;
                olbFile.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                olbFile.FontSize = 12;
                olbFile.Text = System.IO.Path.GetExtension(FilePath);
                olbFile.Visibility = Visibility.Collapsed;
                olbFile.Tag = GetMimeType(olbFile.Text);
                olbFile.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                olbFile.VerticalAlignment = System.Windows.VerticalAlignment.Top;


                olb64 = new TextBlock();
                olb64.Margin = new Thickness(2, 4, 0, 0);
                olb64.TextWrapping = TextWrapping.Wrap;
                olb64.Foreground = Brushes.Black;
                olb64.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                olb64.FontSize = 12;
                olb64.Tag = strBase64;
                olb64.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                olb64.VerticalAlignment = System.Windows.VerticalAlignment.Top;


                olblength = new TextBlock();
                olblength.Margin = new Thickness(2, 4, 0, 0);
                olblength.TextWrapping = TextWrapping.Wrap;
                olblength.Foreground = Brushes.Black;
                olblength.FontFamily = CallibiriFontFamily;//new FontFamily("Callibri");
                olblength.FontSize = 12;
                olblength.Tag = fileLength;
                olblength.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                olblength.VerticalAlignment = System.Windows.VerticalAlignment.Top;


                oimgDelete = new Image();
                oimgDelete.Height = 20;
                oimgDelete.Width = 20;
                oimgDelete.Source = gloSurescriptSecureMessage.gloBitmapResources.SurescriptsCloseBlackBitmapImage;//new System.Windows.Media.Imaging.BitmapImage(new Uri("/gloSurescriptSecureMessage;component/Image/close-black.ico", UriKind.Relative));
                //   oimgDelete.Margin = New Thickness(0, 0, -20, -20)
                System.Windows.Controls.ToolTip tt = new System.Windows.Controls.ToolTip();
                tt.Content = "Remove Attachment";
                oimgDelete.ToolTip = tt;
                oimgDelete.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                oimgDelete.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;

                oimgDelete.MouseDown += new MouseButtonEventHandler(ImgDelete_MouseDown);
                oimgDelete.MouseLeftButtonUp += new MouseButtonEventHandler(ImgDelete_MouseUp);

                ostackInner.Children.Add(oimgAckw);
                ostackInner.Children.Add(olbl);
                ostackInner.Children.Add(olbFile);
                ostackInner.Children.Add(olb64);
                ostackInner.Children.Add(olblength);
                ostackInner.Children.Add(oimgDelete);

                ostackInner.ContextMenu = oContextmainMenu;

                lstAttachment.ScrollIntoView(lstAttachment);
                lstAttachment.Items.Add(ostackInner);
                lstAttachment.SelectedIndex = 0;

            }

            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }

            finally
            {

                if (mnuItemDownload != null)
                {
                    mnuItemDownload = null;
                }

                if (oDownloadIcon != null)
                {
                    oDownloadIcon = null;
                }

                if (oContextmainMenu != null)
                {
                    oContextmainMenu = null;
                }


                if (oimgDelete != null)
                {
                    oimgDelete = null;
                }

                if (oimgAckw != null)
                {
                    oimgAckw = null;
                }

                if (olb64 != null)
                {
                    olb64 = null;
                }


                if (olbFile != null)
                {
                    olbFile = null;
                }


                if (olbl != null)
                {
                    olbl = null;
                }


                if (ostackInner != null)
                {
                    ostackInner = null;
                }

            }


        }

        private string GenerateFile(byte[] oResult, string strFileName)
        {
         //   MemoryStream stream = null;
            System.IO.FileStream oFile = null;

            try
            {
                if (oResult != null)
                {
                    byte[] content = (byte[])(oResult);
           //         stream = new MemoryStream(content);
                    strFileName = gloSettings.FolderSettings.AppTempFolderPath + strFileName;
                    oFile = new System.IO.FileStream(strFileName, System.IO.FileMode.Create);
                    oFile.Write(content, 0, content.Length);

             //       stream.WriteTo(oFile);
                    oFile.Close();
                    content = null;
                }
                else
                {
                    return null;
                }
            }            
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
            }
            finally
            {
                if (oFile != null)
                {
                    oFile.Dispose();
                    oFile = null;
                }

                //if (stream != null)
                //{
                //    stream.Dispose();
                //    stream = null;
                //}
            }
            return strFileName;
        }

        private string GetAttachmentFileName(int i)
        {
            string strFileName = null;
            try
            {
                StackPanel stac = (StackPanel)lstAttachment.Items.GetItemAt(i);
                TextBlock TBlock = (TextBlock)stac.Children[1];
                strFileName = TBlock.Text;

                TBlock = null;
                stac = null;

                return strFileName;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
                return null;
            }
            finally
            {
                strFileName = null;
            }
        }

        private byte[] GetAttachmentFile(int i)
        {
            byte[] arrByte = null;

            try
            {
                StackPanel stac = (StackPanel)lstAttachment.Items.GetItemAt(i);
                TextBlock TBlock = (TextBlock)stac.Children[1];
                arrByte = (Byte[])(TBlock.Tag);

                TBlock = null;
                stac = null;

                return arrByte;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
                return null;
            }
            finally
            {
                arrByte = null;
            }

        }

        private string GetName(int i)
        {
            string strName = null;
            try
            {
                StackPanel stac = (StackPanel)lstTo.Items.GetItemAt(i);
                TextBlock TBlock = (TextBlock)stac.Children[0];
                strName = TBlock.Text;

                TBlock = null;
                stac = null;

                return strName;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
                return null;
            }
            finally
            {
                strName = null;
            }
        }

        public class AgeDetail
        {
            public string Age;
            public Int16 Years;
            public Int16 Months;
            public Int16 Days;
            public Int64 Hours;

            public AgeDetail()
                : base()
            {
            }

            ~AgeDetail()
            {

            }

        }

        public bool GetPediatricSetting(DataSet dsPatient)
        {
            DataTable dt = null;
            bool _IsPediatric = false;
            try
            {

                dt = new DataTable();
                if (dsPatient != null)
                {
                    if (dsPatient.Tables["PediatricSetting"].Rows.Count > 0)
                    {
                        dt = dsPatient.Tables["PediatricSetting"];
                    }
                }
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _IsPediatric = Convert.ToBoolean(dt.Rows[0][1]);
                    }
                }

                return _IsPediatric;

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                return _IsPediatric;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }

        private void AddDefaultEmailID(string _defaultEmailID)
        {
            string fName = string.Empty;
            string lName = string.Empty;
            long contactID = 0;
            string fullName = string.Empty;
            string nPI = string.Empty;
            string email = _defaultEmailID;

            AddToList(fullName, contactID, nPI, email);
        }

        #endregion
        
        #region "Multiple Providers"
        private List<DirectUserProviderAssociation> _listOfProviders;
        public List<gloSurescriptSecureMessage.DirectUserProviderAssociation> ListOfProviders
        {
            get
            {
                return this._listOfProviders;
            }
            set
            {
                if (value != null)
                {
                    this._listOfProviders = value;
                    this.lblFrom.Visibility = Visibility.Collapsed;
                    this.cmbProviders.Visibility = Visibility.Visible;
                }
                else
                {
                    this._listOfProviders = null;
                    this.cmbProviders.Items.Clear();
                    this.cmbProviders.Visibility = Visibility.Collapsed;
                    this.lblFrom.Visibility = Visibility.Visible;
                }

                if (value != null)
                { MultipleProvidersAdded(_listOfProviders); }
            }
        }

        private Dictionary<long, DirectUserProviderAssociation> dictionaryProviders;

        private void SetPreferredProvider()
        {
            try
            {
                System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _EnumPreferredProvider;
                _EnumPreferredProvider = _listOfProviders.Where(p => p.IsPreferred);

                 if (_EnumPreferredProvider.Count() == 1) // If the Provider does not have 
                    {                                        // his own Direct Address check if he has a Preferred Provider
                        DirectUserProviderAssociation providerElement = _EnumPreferredProvider.ElementAt(0);
                        ItemCollection objectCollection = cmbProviders.Items;

                        if (objectCollection.Contains(providerElement))
                        { this.SetComboBoxSelectedItem(providerElement); }

                        objectCollection = null;
                        providerElement = null;
                        _EnumPreferredProvider = null;
                    }
               
                else
                {
                    System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _EnumDefaultProvider;
                    _EnumDefaultProvider = this._listOfProviders.Where(p => p.IsProviderInbox);

                    if (_EnumDefaultProvider.Count() == 1) //If the Provider has his own Direct Address
                    {
                        DirectUserProviderAssociation providerElement = _EnumDefaultProvider.ElementAt(0);
                        ItemCollection objectCollection = cmbProviders.Items;

                        if (objectCollection.Contains(providerElement))
                        { this.SetComboBoxSelectedItem(providerElement); }

                        objectCollection = null;
                        providerElement = null;

                    }
                    else
                    // If Provider does NOT have his own Direct Address
                    // and as well as no Associations then load the first option
                    // in the Combo list
                    { cmbProviders.SelectedIndex = 0; }
                    _EnumDefaultProvider = null;
                }
                

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

           
        }

        private void MultipleProvidersAdded(List<DirectUserProviderAssociation> UserList)
        {
            try 
            {
                if (this._listOfProviders != null && this._listOfProviders.Any())
                {
                    //if (this._listOfProviders.Count > 0)
                    //{

                        // DirectUserProviderAssociation PreferredProvider = null;

                        if (this.dictionaryProviders == null)
                        { this.dictionaryProviders = new Dictionary<Int64, DirectUserProviderAssociation>(); }

                        foreach (DirectUserProviderAssociation Element in this._listOfProviders)
                        {
                            if (!dictionaryProviders.ContainsKey(Element.AssociationID))
                            {
                                dictionaryProviders.Add(Element.AssociationID, Element);
                            }
                        }
                        this.cmbProviders.ItemsSource = ListOfProviders;

                        this.cmbProviders.DisplayMemberPath = "ProviderNameAndAddress";
                        this.cmbProviders.SelectedValuePath = "AssociationID";

                        this.SetPreferredProvider();

                   // }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }

            
        }

        private void SetComboBoxSelectedItem(DirectUserProviderAssociation Provider)
        {
            try
            {
                ItemCollection objectCollection = this.cmbProviders.Items;

                if (Provider != null && objectCollection.Contains(Provider))
                {
                    cmbProviders.SelectedIndex = objectCollection.IndexOf(Provider);

                    gloSurescriptSecureMessage.SecureMessageProperties.ProviderID = Provider.ProviderID;
                    gloSurescriptSecureMessage.SecureMessageProperties.ProviderName = Provider.FirstAndLastName;
                    gloSurescriptSecureMessage.SecureMessageProperties.ProviderDirectAddress = Provider.DirectAddress;
                    gloSurescriptSecureMessage.SecureMessageProperties.SPID = Provider.SSPID;
                 
                    objectCollection = null;
                }
                Provider = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        private void cmbProviders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Int64 nAssociationID = Convert.ToInt64(cmbProviders.SelectedValue);

                if (this.dictionaryProviders != null && this.dictionaryProviders.ContainsKey(nAssociationID))
                {
                    //if (this.dictionaryProviders.ContainsKey(nAssociationID))
                    //{
                        this.CurrentUserProvider = dictionaryProviders[nAssociationID];
                        this.sFromDirectAddress = CurrentUserProvider.DirectAddress;

                        SetDelegatedProvider(CurrentUserProvider.ProviderID);
                   // }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }  
        }

        private void SetDelegatedProvider(long _nProviderID)
        {
            
            try
            {
                if (SecureMessageProperties.ListUserProviderAssociation != null && SecureMessageProperties.ListUserProviderAssociation.Count > 0)
                {
                    //if (SecureMessageProperties.ListUserProviderAssociation.Count > 0)
                    //{
                       // List<DirectUserProviderAssociation> localList = SecureMessageProperties.ListUserProviderAssociation;

                        System.Collections.Generic.IEnumerable<DirectUserProviderAssociation> _iEnumerable = null;

                        _iEnumerable = from elementObject in SecureMessageProperties.ListUserProviderAssociation //localList
                                       where elementObject.ProviderID == _nProviderID
                                       && elementObject.IsProviderInbox
                                       select elementObject;            

                        if (_iEnumerable.Count() == 1)
                        { 
                            //SecureMessageProperties.IsProviderDelegated = false; 
                            SecureMessageProperties.DelegatedProvider = null;
                        }
                        else 
                        { 
                          //  SecureMessageProperties.IsProviderDelegated = true; 

                            _iEnumerable = null;

                            _iEnumerable = from elementObject in SecureMessageProperties.ListUserProviderAssociation //localList
                                           where elementObject.ProviderID == _nProviderID
                                           && !elementObject.IsProviderInbox
                                           select elementObject;

                            if (_iEnumerable.Count() == 1)
                            {
                                SecureMessageProperties.DelegatedProvider = _iEnumerable.ElementAt(0);
                            }
                        }

                       // localList = null;
                        _iEnumerable = null;
                    //}
                    //else 
                    //{ 
                    //  //  SecureMessageProperties.IsProviderDelegated = false; 
                    //    SecureMessageProperties.DelegatedProvider = null;
                    //}
                }
                else 
                { 
                    //SecureMessageProperties.IsProviderDelegated = false;
                    SecureMessageProperties.DelegatedProvider = null;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
        }

        #endregion

        private Boolean bDisplayCDAButton = true;
        public Boolean DisplayCDAButton 
        { 
            get {return this.bDisplayCDAButton;}
            set 
            {
                this.bDisplayCDAButton = value;

                if (value)
                {stkAddCDAGroup.Visibility = Visibility.Visible;}
                else
                {
                    stkAddCDAGroup.Visibility = Visibility.Collapsed;                                        
                }
            } 
        }

        private void btnAttachment_Click(object sender, RoutedEventArgs e)
        {
            double toWidth = lstTo.Width;
            if (_IsUnstrucuredCDA == true)
            {
                AttachClick_UnstructuredCDA();
            }
            else
            {
                AttachClick();
            
            }
            //Window_SizeChanged(null, null);
            _IsPatientLoad = true;
            lstAttachment.Width = toWidth + 65;
            lstTo.Width = toWidth;     
        }

       

       
    }    
}
