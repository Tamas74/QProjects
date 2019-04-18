using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEDocumentV3.Enumeration;
using System.IO;
using pdftron.PDF;
using pdftron.Common;
using pdftron.Filters;
using pdftron.SDF;
using System.Data.SqlClient;
using gloSettings;
using Microsoft.Win32;
using System.Collections;
using System.Xml;
using gloGlobal;
namespace gloEDocumentV3.Forms
{
    public partial class frmEDocumentViewer : Form, IHotKey
    {
        #region "Constructor"
        public delegate void RefreshDocuments();
        public event RefreshDocuments EvnRefreshDocuments;
        public delegate void AcknowledgeTask();
        public event AcknowledgeTask EvnAcknowledgeTask; //new event added to refresh task screen depend on setting if open from task screen 
        //Variabble _DMSPagePriview uded to get Vetting value retirn from gloSettings.GetSetting()
     //   private bool blntaskack = false;
        private String _DMSPagePreview = "";
        bool _isDemoVersion = gloEDocV3Admin.ISDMSDEMO;
        bool _IsPagesClick = false;
        private MyPDFView oPDFView = null;
        public bool flgShowAckDocs = false;
        public string slblDocumentHeader = "";
        //private static frmEDocumentViewer  frm;
        //private static Boolean IsOpen = false;

        public object objPatientExam { get; set; }
        public object objPatientLetters { get; set; }
        public object objPatientMessages { get; set; }
        public object objNurseNotes { get; set; }
        public object objHistory { get; set; }
        public object objLabs { get; set; }
        public object objDMS { get; set; }
        public object objRxmed { get; set; }
        public object objOrders { get; set; }
        public object objProblemList { get; set; }
        public object objCriteria { get; set; }
        public object objWord { get; set; }
        public dynamic dMdi { get; set; }

        gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString);
        public frmEDocumentViewer()
        {
            InitializeComponent();
            // added by rahul patel on 02/11/2010   
            //For resetting the connection string for tool strip .
            tls_MaintainDoc.ConnectionString = gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString;
            //End of code by rahul patel
            //***
            //Code added on 20090102 By - Sagar Ghodke
            numYear.Maximum = DateTime.Now.Year + 2;
            //End - 20090102

            oPDFDoc = null;
            oPDFView = null;
            oProcessLabel = null;
            _DocumentLoadAsFile = true;

            _PatientID = 0;
            _OpenEDocumentAs = enum_OpenEDocumentAs.None;
            _CurrentYear = DateTime.Now.Year.ToString();
            _SelectedYear = DateTime.Now.Year.ToString();
            _oMdiParent = null;
            _OpenExternalSource = enum_OpenExternalSource.None;
            _ExternalDocumentID = 0;
            _ExternalContainerID = 0;
            _IsDocumentsLoading = false;
            // oPDFView = new PDFViewCtrl();
            oPDFView = new MyPDFView(this, 0);
            //Integrated by Mayuri:20101022-provider sign change
            AddChildMenu();
            AddChildMenuAnnot();
            //End 20101022
        }

        public frmEDocumentViewer(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int64 ExternalDocumentID)
        {
            InitializeComponent();

            if (OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                this.Text = "RCM Documents";
                this.Icon = global:: gloEDocumentV3.Properties.Resources.RCMDocs;
            }
            else
            {
                this.Text = "Scan Documents";
                this.Icon = global:: gloEDocumentV3.Properties.Resources.Scan;
            }

            //***
            //Code added on 20090102 By - Sagar Ghodke
            numYear.Maximum = DateTime.Now.Year + 2;
            //End - 20090102

            oPDFDoc = null;
            // oPDFView = null;
            oProcessLabel = null;
            _DocumentLoadAsFile = true;

            _PatientID = PatientID;
            _OpenEDocumentAs = OpenEDocumentAs;
            _CurrentYear = DateTime.Now.Year.ToString();
            _SelectedYear = DateTime.Now.Year.ToString();
            _oMdiParent = oMdiParent;
            _OpenExternalSource = OpenExternalSource;
            _ExternalDocumentID = ExternalDocumentID;
            //Resolved Bug # : 38886
            _DocumentIDForImmnunization = _ExternalDocumentID;

            //Get Container ID From Document ID
            gloEDocumentV3.eDocManager.eDocGetList oContainer = new gloEDocumentV3.eDocManager.eDocGetList();
            _ExternalContainerID = oContainer.GetContainerID(ExternalDocumentID, _OpenExternalSource);
            //Resolved Bug # : 38886
            _ContainerIDForImmnunization = _ExternalContainerID;
            if (oContainer != null) { oContainer.Dispose(); oContainer = null; }
            //-------x-----------
            _IsDocumentsLoading = false;
            // oPDFView = new PDFViewCtrl();          
            oPDFView = new MyPDFView(this, 0);
            AddChildMenu();
            AddChildMenuAnnot();
        }

        # region "Check Instance of the form"
        public static frmEDocumentViewer GetInstance(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int64 ExternalDocumentID)
        {
            try
            {
                if (eDocInstance == null)
                {
                    eDocInstance = new frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);
                }
                else
                {
                    eDocInstance._PatientID = PatientID;
                    eDocInstance._OpenEDocumentAs = OpenEDocumentAs;
                    eDocInstance._CurrentYear = DateTime.Now.Year.ToString();
                    eDocInstance._SelectedYear = DateTime.Now.Year.ToString();
                    eDocInstance._oMdiParent = oMdiParent;
                    eDocInstance._OpenExternalSource = OpenExternalSource;
                    eDocInstance._ExternalDocumentID = ExternalDocumentID;

                    //Problem #00000154 : View Documents not working as expcted.
                    //Get Container ID From Document ID
                    gloEDocumentV3.eDocManager.eDocGetList oContainer = new gloEDocumentV3.eDocManager.eDocGetList();
                    eDocInstance._ExternalContainerID = oContainer.GetContainerID(ExternalDocumentID, OpenExternalSource);
                    if (oContainer != null) { oContainer.Dispose(); oContainer = null; }

                    eDocInstance.FormLoadProcedure();
                }

            }
            catch (Exception ex)
            {
                #region " Make Log Entry "

                string _ErrorMessage = ex.ToString();
                //Code added on 7rd October 2008 By - Sagar Ghodke
                //Make Log entry in DMSExceptionLog file for any exceptions found
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }

                //End Code add
                #endregion " Make Log Entry "

            }
            return eDocInstance;
        }

        public static frmEDocumentViewer GetAnyInstance()
        {
            try
            {

                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "frmEDocumentViewer")
                    {
                        return (frmEDocumentViewer)f;
                    }
                }
                return null;
            }
            catch (Exception exc)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(exc.ToString(), true);
                return null;
            }
        }



        #endregion

        #endregion

        #region "Private Variables Declaration"
        pdftron.PDF.PDFDoc oPDFDoc = null;
        //pdftron.PDF.PDFViewCtrl oPDFView = null;
        Label oProcessLabel = null;
        bool _DocumentLoadAsFile = false;
        private static frmEDocumentViewer eDocInstance;

        Int64 _PatientID = 0;
        enum_OpenEDocumentAs _OpenEDocumentAs = enum_OpenEDocumentAs.None;
        string _CurrentYear = "";
        string _SelectedYear = "";
        System.Windows.Forms.Form _oMdiParent = null;
        enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;
        public Boolean IsShowCopyExisting = false;
        public Boolean IsDeleteEnabledFromImmunization = true;
        Int64 _ExternalDocumentID = 0;
        Int64 _ExternalContainerID = 0;
        bool _IsDocumentsLoading = false;

        bool _IsFormLoading = false;
        //string _MessageBoxCaption = "Document management system";
        string _ErrorMessage = "";

        ToolTip oNoteToolTip = null;
        ToolTip oButtonToolTip = null;
        bool _IsMouserRightClick = false;

        //Sudhir 20090106   -- for PDF Page Cursor
        MemoryStream MsHandFree = new MemoryStream(Properties.Resources.HandFree);
        MemoryStream MsHandHold = new MemoryStream(Properties.Resources.HandHold);
        Cursor oPDFHandFreeCursor;
        Cursor oPDFHandHoldCursor;
        pdftron.PDF.Annots.Widget temp;

        //dhruv
        //  string _tempFilePath = "";
        //  bool _isCloseSave = false;
        //Added on 20100527
        bool _IsgridCancel = false;
        bool _Isgrid = false;
        bool _isAnnotationPrvClick = false;
        bool _isAnnotationChildPrvClick = false;
        //Sanjog
        Int64 _ProviderID;
        bool blnSignClick = false;
        //Sanjog
        // public Boolean IsSaved = false;
        enum ShowHideFlag
        {
            Opearations = 1, Document = 2, Legend = 3, Patient = 4, Pages = 5, Preview = 6, Notes = 7, Tags = 8, Search = 9
        }
        //Added By mitesh 20122801
        bool _SaveResult = true;



        #endregion


        public enum_OpenEDocumentAs OpenEDocumentAs;
        public Int64 _DocumentIDForImmnunization = 0;
        public Int64 _ContainerIDForImmnunization = 0;

        public Int64 _selectDocumentIDForImmunization = 0;

        public ArrayList sIntuitDocumentPath = null;
        public ArrayList sIntuitDocumentName = null;
        public ArrayList sSecureMsgDocumentPath = null;
        public ArrayList sSecureMsgDocumentName = null;
        public Int16 iAttachmentCnt = 0;
        public Boolean _IsAcknowledge = true;

        public ArrayList iLabDMSIDs = null;

        public DataTable dtSearchYear = null;

        //public Janus.Windows.UI.Dock.UIPanelManager UiPanelManager1=new Janus.Windows.UI.Dock.UIPanelManager ();
        public Janus.Windows.UI.Dock.UIPanelGroup uiPanSplitScreen = null;//=new Janus.Windows.UI.Dock.UIPanelGroup ();
        public gloEMRGeneralLibrary.clsSplitScreen clsSplit = null; //new gloEMRGeneralLibrary.clsSplitScreen();
        public bool IsSplitScreenShown = true;
        long VisitID = 0;
        private bool IsDocLoaded = true; //added for bugid 76048 giving exception if open from task and closing before loading
        public Boolean  IsAcknowledge
        {
            get { return _IsAcknowledge ; }
            set { _IsAcknowledge = value; }
        }


        public Tuple<Int64, string, string> SyncPatientId
        {
            get { return new Tuple<Int64, string, string>(_PatientID, oPatientStrip.PatientCode, oPatientStrip.PatientName); }
        }



        #region "Form Load and Unload"
        private void frmEDocumentViewer_Load(object sender, EventArgs e)
        {
            InitializeToolStrip();
            

            #region " Page Cursor "
            try
            {
                oPDFHandFreeCursor = new Cursor(MsHandFree);
                oPDFHandHoldCursor = new Cursor(MsHandHold);
            }
            catch
            {
                oPDFHandFreeCursor = Cursors.Default;
                oPDFHandHoldCursor = Cursors.Default;
            }

            #endregion

            //08-Mar-2017 Aniket: Shifting Split Screen code to reduce screen flickering
            #region " Split Screen "
            if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization || _OpenExternalSource == enum_OpenExternalSource.RCM) && (_PatientID == -1))
            {
            }
            else
            {
                if (!IsSplitScreenShown == false)
                    //return;
                {
                    if (clsSplit != null)
                    {
                        clsSplit.Dispose();
                        clsSplit = null;
                    }
                    clsSplit = new gloEMRGeneralLibrary.clsSplitScreen();
                    clsSplit.clsUCLabControl = new gloUserControlLibrary.gloUC_TransactionHistory();
                    clsSplit.clsPatientExams = objPatientExam;
                    clsSplit.clsPatientLetters = objPatientLetters;
                    clsSplit.clsPatientMessages = objPatientMessages;
                    clsSplit.clsNurseNotes = objNurseNotes;
                    clsSplit.clsHistory = objHistory;
                    clsSplit.clsLabs = objLabs;
                    clsSplit.clsDMS = new gloEDocumentV3.eDocManager.eDocGetList();
                    clsSplit.clsRxmed = objRxmed;
                    clsSplit.clsOrders = objOrders;
                    clsSplit.clsProblemList = objProblemList;

                    VisitID = GetVisitID(System.DateTime.Now, _PatientID);
                    uiPanSplitScreen = clsSplit.LoadSplitControl(this, _PatientID, VisitID, "PatientExam", objCriteria, objWord, gloEDocV3Admin.gClinicID, gloEDocV3Admin.gUserID);
                    uiPanSplitScreen.BringToFront();
                }
            }
            #endregion

            FormLoadProcedure();

            slblDocumentHeader = lblDocumentsHeader.Text;
           
            lvwPages.Cursor = Cursors.Hand;

            #region " Tool Tip for Buttons "

            oButtonToolTip = new ToolTip();
            oButtonToolTip.SetToolTip(btnNextPage, "Next Page");
            oButtonToolTip.SetToolTip(btnLastPage, "Last Page");
            oButtonToolTip.SetToolTip(btnPrevPage, "Previous Page");
            oButtonToolTip.SetToolTip(btnFirstPage, "First Page");
            oButtonToolTip.Active = true;
            #endregion

            //Added by Mayuri:20100417-To fix case No:#0004968
            if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization && _OpenExternalSource != enum_OpenExternalSource.RCM)
            {
                btnPat_Up_Click(null, null);
            }


            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ViewDocument, gloAuditTrail.ActivityType.Load, "RCM Documents Loaded.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            else
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ViewDocument, gloAuditTrail.ActivityType.Load, "Scan Documents Loaded.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }

        }

        //Developer: Mitesh Patel
        //Date:27-Dec-2011'
        //Case No: GLO2011-0015589 
        private void SetLastDMSSettings()
        {
            try
            {

                //  bool IsFirstNodeExpanded = false;
                for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.Rows[i].Node.Level == 0)
                    {
                        c1Documents.Rows[i].Node.Expanded = true;
                    }
                }
                //string xmlFilePath = Application.StartupPath + "\\DMSCategorySetting.xml";
                string xmlFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DMSCategorySetting.xml"; ;
                if (File.Exists(xmlFilePath))
                {
                    XmlNodeList m_nodelist;
                    XmlDocument DMSdoc = new XmlDocument();
                    XmlTextReader reader = new XmlTextReader(xmlFilePath);
                    reader.Read();
                    // load reader
                    DMSdoc.Load(reader);
                    m_nodelist = DMSdoc.SelectNodes("DMSCategory/Category");

                    foreach (XmlNode m_node in m_nodelist)
                    {
                        for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
                        {
                            if (c1Documents.GetData(i, COL_CATEGORYID).ToString() == m_node.Attributes.GetNamedItem("CategoryID").Value)
                            {

                                if (c1Documents.Rows[i].Node.Level == 0)
                                {
                                    
                                    c1Documents.Rows[i].Node.Collapsed = true;
                                    
                                    break;
                                }

                            }

                        }
                    }

                    reader.Close();
                    if (reader != null) { reader = null; }
                    if (DMSdoc != null) { DMSdoc = null; }
                }

                for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
                {
                    //if (IsFirstNodeExpanded == false)
                    //{
                    if (c1Documents.Rows[i].Node.Collapsed == false && c1Documents.Rows[i].Node.Children > 0)
                    {
                        // IsFirstNodeExpanded = true;
                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                        {
                            Int64 _retDocumentID = 0;
                            Int64 _retContainerID = 0;
                            Int32 iDocCnt = c1Documents.Rows[i + 1].Node.Children;

                            if (iDocCnt > 0)
                            {
                                for (Int32 iDoc = 1; iDoc <= iDocCnt; iDoc++)
                                {
                                    if (c1Documents.Rows[i + 1 + iDoc].Visible)
                                    {
                                        _retDocumentID = System.Convert.ToInt64(c1Documents.GetData((i + 1 + iDoc), COL_DOCUMENTID));
                                        _retContainerID = ((Document.eBaseContainers)c1Documents.GetData((i + 1 + iDoc), COL_CONATINERS))[0].EContainerID;
                                        SelectDocumentInGrid(_retDocumentID, _retContainerID);
                                        break;
                                    }
                                }

                                if (_retDocumentID > 0 && _retContainerID > 0)
                                {
                                    break;
                                }
                            }
                        }
                        else
                        {
                            Int64 _retDocumentID = System.Convert.ToInt64(c1Documents.GetData((i + 1), COL_DOCUMENTID));
                            Int64 _retContainerID = ((Document.eBaseContainers)c1Documents.GetData((i + 1), COL_CONATINERS))[0].EContainerID;
                            SelectDocumentInGrid(_retDocumentID, _retContainerID);
                            break;
                        }
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }
        private DataSet Xml_To_Dataset(string _xmlFilePath)
        {

            DataSet ds = new DataSet();
            try
            {
                ds.ReadXml(_xmlFilePath);


            }
            catch (Exception ex)
            {
                MessageBox.Show("XML data reading problem: " + ex.ToString());
                return null;
            }
            finally
            {
                //if (ds != null)
                //{
                //    ds.Dispose(); 
                //}
            }
            return ds;
        }


        private bool GetCheckedIntuitDocs()
        {
            //SG: Memory Leaks, did not found error handling mechanism here so firt adding try catch and refactoring the code
            //    all local varibles are moved to top (including variables declared in for loop) and disposed in finally block


            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            eDocManager.eDocGetList oList = null;
            bool _result = false;
            Int16 _count = 0;
            string _DocumentIDs = "";
            Int64 DocumentID = 0;
            Int64 ContainerID = 0;

            string _FilePath = "";
            string _FolderPath = "";
            object ContainerStream = null;
            Byte[] byteRead = null;

            try
            {

                oSelectedDocuments = GetIntuitSelectedDocuments(out _DocumentIDs);
                oList = new gloEDocumentV3.eDocManager.eDocGetList();

                sIntuitDocumentPath = new ArrayList();
                if (sIntuitDocumentPath != null) { sIntuitDocumentPath.Clear(); }

                sIntuitDocumentName = new ArrayList();
                if (sIntuitDocumentName != null) { sIntuitDocumentName.Clear(); }

                _count = (Int16)(_count + iAttachmentCnt);
                _count = (Int16)(_count + oSelectedDocuments.Count);

                if (_count > 3)
                {
                    MessageBox.Show("There cannot be more than three attachments in a single message. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                {
                    DocumentID = oSelectedDocuments[i].DocumentID;
                    ContainerID = oSelectedDocuments[i].Containers[0].ContainerID;

                    _FilePath = "";
                    _FolderPath = "";
                    ContainerStream = null;
                    byteRead = null;

                    #region "Decide Whether Load as file or stream"

                    if (_DocumentLoadAsFile == true)
                    {

                        _FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\IntuitDocs";

                        if (System.IO.Directory.Exists(_FolderPath) == false)
                        {
                            System.IO.Directory.CreateDirectory(_FolderPath);
                        }

                        _FilePath = _FolderPath + "\\" + DocumentID.ToString() + "~" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";

                        try
                        {
                            if (System.IO.File.Exists(_FilePath) == true)
                            {
                                System.IO.File.Delete(_FilePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                        }

                        oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, ref _FilePath, _OpenExternalSource);

                        if (System.IO.File.Exists(_FilePath) == true)
                        {

                            FileInfo _file = new FileInfo(_FilePath);
                            double filesize = (_file.Length / 1024f) / 1024f;
                            if (filesize > 2)
                            {
                                MessageBox.Show("The following document cannot be added as an attachment " + Environment.NewLine + "because it exceeds the attachment size limit (2 MB). " + Environment.NewLine + Environment.NewLine + "'" + oSelectedDocuments[i].DocumentName.ToString() + "'  " + Math.Round(filesize, 2) + " MB", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return true;
                            }
                            _file = null;
                            sIntuitDocumentPath.Add(_FilePath);
                            sIntuitDocumentName.Add(oSelectedDocuments[i].DocumentName.ToString());
                        }
                        else
                        {
                            MessageBox.Show("The file was not generated. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }

                    }
                    else
                    {
                        ContainerStream = oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                        if (ContainerStream != null) { byteRead = (byte[])ContainerStream; }

                    }

                    #endregion


                } //End For loop

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {

                if (oList != null) { oList.Dispose(); oList = null; }
                if (oSelectedDocuments != null) { oSelectedDocuments.Clear(); oSelectedDocuments.Dispose(); oSelectedDocuments = null; }
                if (byteRead != null) { byteRead = null; }
                if (ContainerStream != null) { ContainerStream = null; }

            }

            return _result;
        }

        private bool GetCheckedSecureMsgDocs()
        {
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            eDocManager.eDocGetList oList = null;
            bool _result = false;
            string _DocumentIDs = "";
            Int64 DocumentID = 0;
            Int64 ContainerID = 0;

            string _FilePath = "";
            string _FolderPath = "";
            object ContainerStream = null;
            Byte[] byteRead = null;


            try
            {
                oSelectedDocuments = GetIntuitSelectedDocuments(out _DocumentIDs);
                oList = new gloEDocumentV3.eDocManager.eDocGetList();

                sSecureMsgDocumentPath = new ArrayList();
                if (sSecureMsgDocumentPath != null) { sSecureMsgDocumentPath.Clear(); }

                sSecureMsgDocumentName = new ArrayList();
                if (sSecureMsgDocumentName != null) { sSecureMsgDocumentName.Clear(); }


                for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                {
                    DocumentID = oSelectedDocuments[i].DocumentID;
                    ContainerID = oSelectedDocuments[i].Containers[0].ContainerID;

                    _FilePath = "";
                    _FolderPath = "";
                    ContainerStream = null;
                    byteRead = null;

                    #region "Decide Whether Load as file or stream"

                    if (_DocumentLoadAsFile == true)
                    {

                        // _FolderPath = gloEDocV3Admin.gDocumentOpenTemporaryProcessPath;
                        _FolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SecureMsgDocs";

                        if (System.IO.Directory.Exists(_FolderPath) == false)
                        { System.IO.Directory.CreateDirectory(_FolderPath); }

                        _FilePath = _FolderPath + "\\" + DocumentID.ToString() + "~" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";

                        try
                        {
                            if (System.IO.File.Exists(_FilePath) == true)
                            { System.IO.File.Delete(_FilePath); }
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            AuditLogErrorMessage(_ErrorMessage);
                        }

                        oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, ref _FilePath, _OpenExternalSource);

                        if (System.IO.File.Exists(_FilePath) == true)
                        {
                            sSecureMsgDocumentPath.Add(_FilePath);
                            sSecureMsgDocumentName.Add(oSelectedDocuments[i].DocumentName.ToString());
                        }
                        else
                        {
                            MessageBox.Show("The file was not generated. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }

                    }
                    else
                    {
                        ContainerStream = oList.GetContainerStream(DocumentID, ContainerID, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                        if (ContainerStream != null) { byteRead = (byte[])ContainerStream; }
                    }

                    #endregion

                } //End For loop

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oSelectedDocuments != null) { oSelectedDocuments.Clear(); oSelectedDocuments.Dispose(); oSelectedDocuments = null; }
                if (oList != null) { oList.Dispose(); oList = null; }
                ContainerStream = null;
                byteRead = null;
            }

            return _result;
        }


        private bool GetCheckedLabOrderDocs()
        {


            //DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            //eDocManager.eDocGetList oList = null;

            bool _result = false;
            try
            {
                System.Collections.ArrayList _selecteddocumentindexs = GetlaborderCheckedDocumentsIndex();

                iLabDMSIDs = new ArrayList();
                if (iLabDMSIDs != null) { iLabDMSIDs.Clear(); }

                if (_selecteddocumentindexs != null)
                {
                    for (int i = 0; i <= _selecteddocumentindexs.Count - 1; i++)
                    {
                        int _rowindex = System.Convert.ToInt32(_selecteddocumentindexs[i].ToString());

                        iLabDMSIDs.Add(System.Convert.ToInt64(c1Documents.GetData(_rowindex, COL_DOCUMENTID)));
                        _result = true;
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {


            }

            return _result;
        }

        private void frmEDocumentViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(c1Documents !=null )
            //{
            //    if (c1Documents .Rows .Count >1)
            //    {
            //        Int16 i;
            //        for (i = 0; i <= c1Documents.Rows.Count - 1; i++)
            //        {
            //            if (c1Documents 
            //        }
            //    }
            //}
            if (IsDocLoaded == false)
            {

                e.Cancel = true;
                return;
            }
            if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
            {
                if (GetCheckedCountImmnunization() == true)
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (c1Documents.Rows.Count > 1)
            {
                if (_DocumentIDForImmnunization == 0)
                {
                    _DocumentIDForImmnunization = _ExternalDocumentID;
                }
            }
            else
            {
            }
            #region "Intuit message"
            if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.IntuitMessage)
            {
                if (GetCheckedIntuitDocs() == true)
                {
                    e.Cancel = true;
                    return;
                }
            }

            #endregion "End Intuit message"
            #region "Secure message"
            if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.SecureMessage)
            {
                if (GetCheckedSecureMsgDocs() == true)
                {
                    e.Cancel = true;
                    return;
                }
            }

            #endregion "End Intuit message"

            #region "Laborder message"
            if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.LabOrder && _OpenEDocumentAs != gloEDocumentV3.Enumeration.enum_OpenEDocumentAs.ViewDocumentForExternalModule)
            {
                //if (GetCheckedLabOrderDocs() == false)
                //{
                //    e.Cancel = true;
                //    return;
                //}
                GetCheckedLabOrderDocs();
            }
            #endregion "End Laborder message"
            //Boolean IsChangedState = false; 
            //for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
            //{

            //   // if (c1Documents.Rows[i].Node.Collapsed == true && c1Documents.Rows[i].Node.Children > 0)
            //    if (c1Documents.Rows[i].Node.Children > 0)
            //    {
            //        if (c1Documents.Rows[i].Node.Collapsed == true)
            //        {
            //            IsChangedState = true;
            //            break;
            //        }
            //    }

            //}

            //Developer: Mitesh Patel
            //Date:27-Dec-2011'
            //Case No: GLO2011-0015589 

            //SG: Memory Leaks, need not to have new here 
            //DataSet dsCategory = new DataSet();
            DataSet dsCategory = null;
            //30-Apr-13 Aniket: The above dataset needs to be new as it throws error if the DMScategorySetting.xml file is not present. Resolving bug 49707.


            String xmlFilePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DMSCategorySetting.xml";//System.IO.Path.GetTempPath() + "DMSCategorySetting.xml"; 
            if (File.Exists(xmlFilePath))
            {
                dsCategory = Xml_To_Dataset(xmlFilePath);
                if (dsCategory == null)
                {
                    dsCategory = new DataSet();
                }
                if (dsCategory.Tables.Count == 0)
                {
                    DataTable table = new DataTable();
                    DataColumn colCategory = new DataColumn();
                    colCategory.DataType = System.Type.GetType("System.String");
                    colCategory.Caption = "CategoryID";
                    colCategory.ColumnName = "CategoryID";
                    table.Columns.Add(colCategory);
                    dsCategory.Tables.Add(table);

                    ////SG: Memory Leaks, disposing temp table and datacolumn
                    //if (colCategory != null)
                    //{colCategory.Dispose(); colCategory = null; }

                    //if (table != null)
                    //{ table.Dispose(); table = null; }
                }

                for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.Rows[i].Node.Children > 0)
                    {
                        if (c1Documents.Rows[i].Node.Collapsed == true)
                        {
                            DataView dv = new DataView(dsCategory.Tables[0]);
                            dv.RowFilter = "CategoryID = '" + c1Documents.GetData(i, COL_CATEGORYID).ToString() + "'";
                            if (dv.Count == 0)
                            {
                                DataRow dr = dsCategory.Tables[0].NewRow();
                                dr[0] = c1Documents.GetData(i, COL_CATEGORYID).ToString();
                                dsCategory.Tables[0].Rows.Add(dr);
                            }
                            if (dv != null) { dv.Dispose(); }

                            //IsChangedState = true;
                            //break;
                        }
                        else
                        {
                            for (int j = 0; j < dsCategory.Tables[0].Rows.Count; j++)
                            {
                                if (c1Documents.GetData(i, COL_CATEGORYID).ToString() == dsCategory.Tables[0].Rows[j][0].ToString())
                                {
                                    dsCategory.Tables[0].Rows[j].Delete();
                                    break;
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                dsCategory = new DataSet();
                if (dsCategory.Tables.Count == 0)
                {
                    DataTable table = new DataTable();
                    DataColumn colCategory = new DataColumn();
                    colCategory.DataType = System.Type.GetType("System.String");
                    colCategory.Caption = "CategoryID";
                    colCategory.ColumnName = "CategoryID";
                    table.Columns.Add(colCategory);
                    dsCategory.Tables.Add(table);

                    ////SG: Memory Leaks, disposing temp table and datacolumn
                    //if (colCategory != null)
                    //{ colCategory.Dispose(); colCategory = null; }

                    //if (table != null)
                    //{ table.Dispose(); table = null; }
                }
                for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
                {

                    if (c1Documents.Rows[i].Node.Children > 0)
                    {
                        if (c1Documents.Rows[i].Node.Collapsed == true)
                        {
                            DataRow dr = dsCategory.Tables[0].NewRow();
                            dr[0] = c1Documents.GetData(i, COL_CATEGORYID).ToString();
                            dsCategory.Tables[0].Rows.Add(dr);

                        }

                    }
                }
            }


            try
            {
                //if (IsChangedState == true)
                //{

                #region "Save xml document for category"
                if (pnlDocumentsLegends.Visible == true) // Check Category panel visible 
                {

                    if (File.Exists(xmlFilePath))
                    {
                        try
                        {
                            File.Delete(xmlFilePath);
                        }
                        catch //(Exception ex)
                        {
                        }
                    }
                    if (dsCategory != null)
                    {
                        XmlTextWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
                        xmlWriter.WriteStartDocument(true);
                        xmlWriter.WriteStartElement("DMSCategory"); //Root Element


                        // for (int i = 0; i < c1Documents.Rows.Count - 1; i++)
                        for (int j = 0; j < dsCategory.Tables[0].Rows.Count; j++)
                        {

                            //if (c1Documents.Rows[i].Node.Children > 0 && c1Documents.Rows[i].Node.Level == 0 && c1Documents.Rows[i].Node.Collapsed == true)
                            //{

                            xmlWriter.WriteStartElement("Category");
                            xmlWriter.WriteStartAttribute("CategoryID");
                            // xmlWriter.WriteString(c1Documents.GetData(j, COL_CATEGORYID).ToString());
                            xmlWriter.WriteString(dsCategory.Tables[0].Rows[j][0].ToString());
                            xmlWriter.WriteEndAttribute();


                            xmlWriter.WriteEndElement();

                            // }


                        }

                        xmlWriter.WriteEndElement(); //End of Root Element
                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();

                        if (xmlWriter != null) { xmlWriter = null; }

                    }
                }

                if (dsCategory != null) { dsCategory.Dispose(); dsCategory = null; }

                #endregion""

                // }

                //code added by dipak 20091009 for add user level setting for DMSPagePreview
                #region "Save ZoomPercentage Setting"
                ogloSettings.AddSetting("DMSPagePreview", cmbZoomPercentage.SelectedIndex.ToString(), gloEDocumentV3.gloEDocV3Admin.gClinicID, gloEDocumentV3.gloEDocV3Admin.gUserID, gloSettings.SettingFlag.User);
                #endregion
                //end code added by dipak 20091009

                #region " Close the Note View Window "
                if (pnlNotes.Height == 120) { btnNote_Down_Click(null, null); }
                #endregion

                //------------------------------------------------------------
                //UnloadDocView();
                UnloadDocuments();
                OnFormsClosing();

                //SG: Memory Leaks, moving the remove control to Unload Documents
                //if (pnlPreview.Controls.Contains(oPDFView) == true) { pnlPreview.Controls.Remove(oPDFView); }
                //

                lvwPages.Items.Clear();
                UnloadDocuments();
                if (gloEDocV3Admin.gTemporaryProcessPath != null && gloEDocV3Admin.gTemporaryProcessPath != "")
                {
                    if (Directory.Exists(gloEDocV3Admin.gTemporaryProcessPath))
                    {

                        try
                        {
                            Directory.Delete(gloEDocV3Admin.gTemporaryProcessPath, true);
                        }
                        catch
                        {

                        }
                        //Directory.Delete(gloEDocV3Admin.gTemporaryProcessPath, true);
                    }
                }

            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                //if (File.Exists(xmlFilePath))
                //{
                //    File.Delete(xmlFilePath);
                //}
            }

            finally
            {
                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                {
                }
                else
                {
                    if (e.Cancel == false)
                    {
                        if (clsSplit != null)
                        {
                            clsSplit.SaveControlDisplaySettings();
                        }
                        if (uiPanSplitScreen != null)
                        {
                            uiPanSplitScreen.Dispose();
                            uiPanSplitScreen = null;
                        }
                        if (clsSplit != null)
                        {
                            clsSplit.Dispose();
                            clsSplit = null;
                        }
                    }
                }
               // if (blntaskack == true)
               // {
                    if (EvnAcknowledgeTask != null)
                    {
                        EvnAcknowledgeTask();
                    }
               // }
                if ((OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocument) || (OpenEDocumentAs == enum_OpenEDocumentAs.ViewAllDocuments) || (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule))
                {

                    if (eDocInstance != null)
                    {
                        if ((OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule) || (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocument))
                        {
                            eDocInstance.Dispose();
                            eDocInstance = null;
                        }

                    }
                }

                if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule)
                {
                   if(EvnRefreshDocuments!=null)
                    EvnRefreshDocuments();

                }

               
            }

            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ViewDocument, gloAuditTrail.ActivityType.Close, "RCM Documents closed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }
            else
            {
                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ViewDocument, gloAuditTrail.ActivityType.Close, "Scan Documents closed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
            }

        }


        #region " Form Activate DeActivate "
        // SUDHIR 20100206 // ISSUE : FORM HIDE AFTER CONTEXT MENU (FIXED)//
        private void frmEDocumentViewer_Activated(object sender, EventArgs e)
        {

            foreach (Form myForm in Application.OpenForms)
            {

                if (myForm.TopMost)
                {
                    myForm.TopMost = false;
                }

            }


            this.TopMost = true;  
            //Added by  Mayuri:20121207-Split Screen
            if (uiPanSplitScreen != null)
            {
                if ((uiPanSplitScreen.Parent) != null)
                {
                    if (uiPanSplitScreen.Parent.Text == "Split Screen")
                    {
                        uiPanSplitScreen.Parent.Visible = true;

                    }
                    if (uiPanSplitScreen.Text == "Split Screen")
                    {
                        uiPanSplitScreen.Visible = true;

                    }
                }
            }
        }

        private void frmEDocumentViewer_Deactivate(object sender, EventArgs e)
        {
            this.TopMost = false;
            //Added by  Mayuri:20121207-Split Screen
            //if (uiPanSplitScreen != null)
            //{
            //    if ((uiPanSplitScreen.Parent) != null)
            //    {
            //        if (uiPanSplitScreen.Parent.Text == "Split Screen")
            //        {
            //            // uiPanSplitScreen.Parent.Visible = false;
            //        }
            //    }
            //}

            if (this.Parent != null)
            {
                if ((uiPanSplitScreen) != null)
                {
                    if ((uiPanSplitScreen.Parent) != null)
                    {
                        if ((uiPanSplitScreen.Parent) != this)
                        {
                            uiPanSplitScreen.Parent.Visible = false;
                            uiPanSplitScreen.Parent.Hide();
                            uiPanSplitScreen.Parent.Update();
                        }
                    }
                }
            }
        }
        #endregion

        private DataTable GetYearsForSearch()
        {
            gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocumentV3.gloEDocV3Admin.gDMSDatabaseConnectionString);
            DataTable oDataTable = null;
            gloEDocumentV3.Database.DBParameters oDBParams = new gloEDocumentV3.Database.DBParameters();
            string DocuSearchSource = "";
            try
            {
                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    DocuSearchSource = "RCM";
                else
                    DocuSearchSource = "DMS";

                if (oDB != null)
                {
                    if (oDB.Connect(false))
                    {
                        if (oDBParams != null)
                        {
                            oDBParams.Clear();
                            oDBParams.Add("@DocuSearchSource", DocuSearchSource, ParameterDirection.Input, SqlDbType.VarChar);
                            oDBParams.Add("@PatientID", _PatientID, ParameterDirection.Input, SqlDbType.VarChar);

                            oDB.Retrive("gsp_GetYearForSearch", oDBParams, out oDataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (oDBParams != null) { oDBParams.Dispose(); oDBParams = null; }
            }

            return oDataTable;
        }

        private void FormLoadProcedure()
        {
            _IsFormLoading = true;
            _IsDocumentsLoading = true;
            lblAlertMessage.Text = string.Empty;
            IsDocLoaded = false;

            
            tsb_ShowHideAck.Image = global::gloEDocumentV3.Properties.Resources.ShowAcknowledged;
            tsb_ShowHideAck.Visible = false;
            tls_MaintainDoc.ButtonsToHide.Add(tsb_ShowHideAck.Name.ToString());

         

            if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization || _OpenExternalSource == enum_OpenExternalSource.RCM) && (_PatientID == -1))
            {
            }
            else
            {

                if (clsSplit != null)
                {
                    if (uiPanSplitScreen != null)
                    {
                        if (uiPanSplitScreen.SelectedPanel != null)
                        {
                            clsSplit.loadSplitControlData(_PatientID, VisitID, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, gloEDocV3Admin.gClinicID);
                        }
                    }
                }
            }
            if (lvwPages != null && lvwPages.Items.Count > 0) { lvwPages.Items.Clear(); }

            #region " Button Images "
            btnDoc_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btnDoc_Left.BackgroundImageLayout = ImageLayout.Center;


            btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btnPat_Down.BackgroundImageLayout = ImageLayout.Center;

            btnLastPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Last11;
            btnLastPage.BackgroundImageLayout = ImageLayout.Center;

            btnNextPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btnNextPage.BackgroundImageLayout = ImageLayout.Center;

            btnPrevPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btnPrevPage.BackgroundImageLayout = ImageLayout.Center;

            btnFirstPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.First1;
            btnFirstPage.BackgroundImageLayout = ImageLayout.Center;

            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Pluse;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;

            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Minus1;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;

            btnNote_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
            btnNote_Up.BackgroundImageLayout = ImageLayout.Center;
            #endregion

            if (_OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {
                #region OpeneDocumentAs ScanDocument

                #region "Show/Hide Controls"
                ShowHideControl(ShowHideFlag.Opearations, false);
                ShowHideControl(ShowHideFlag.Tags, false);

                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization && _OpenExternalSource != enum_OpenExternalSource.RCM)
                {
                    ShowHideControl(ShowHideFlag.Patient, true);
                }
                else
                {
                    if (_OpenExternalSource != enum_OpenExternalSource.RCM)
                    {
                        pnlNotes.Visible = false;
                        panel19.Visible = false;
                    }
                    ShowHideControl(ShowHideFlag.Patient, false);
                }

                ShowHideControl(ShowHideFlag.Legend, false);
                ShowHideControl(ShowHideFlag.Search, false);
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    tsb_Acknowledge.Enabled = true;
                    btnPat_Up_Click(null, null);
                    btnNote_Down_Click(null, null);
                    btn_Right_Click(null, null);
                }
                else
                {
                    tsb_Acknowledge.Enabled = false;
                }

                //Code Added by dipak 20091009 to SetDMSPagePriviewSetting in order to fix bug 
                //No:4445 ->Make default document view while in Scan Docs/View Docs customizable by user.
                SetDMSPagePreviewSetting();



                #endregion

                #region "Year Settings"

                #region " Read Min Year Value from Registry "


                string _minYearValue = "2006";
                bool _isValuePresent = false;
                try
                {
                    gloRegistrySetting.OpenRemoteBaseKey();
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                    {
                        // Read Setting From Registry
                        if (System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) != ""
                            && System.Convert.ToInt64(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) > 0)
                        {
                            _minYearValue = System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue"));
                            _isValuePresent = true;
                        }
                    }

                    if (_isValuePresent == false)
                    {
                        //If no field for the Value is present in the Registry 
                        //Make entry with the default value
                        gloRegistrySetting.OpenRemoteBaseKey();
                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                        {
                            gloRegistrySetting.SetRegistryValue("DMSMinYearValue", _minYearValue);
                            //gloRegistrySetting.CloseRegistryKey(); //SG: Memory Leaks, closing the key in finally to apply for all opened keys
                            _isValuePresent = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
                finally
                {
                    gloRegistrySetting.CloseRegistryKey();
                }
                #endregion " Read Min Year Value from Registry "


                int _MinYear = DateTime.Now.Year;
                int _MaxYear = DateTime.Now.Year;
                Int32 _PatientMinYear = 0;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

                //Changes made on 20090225 By - Sagar Ghodke 
                //Changes made to implement the registry Minimum Year value
                //Below commented code is previous code
                //_MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true);

                if (_isValuePresent)
                {
                    //Even if the value is present in the Registry check if the value is not
                    //greater than the Last Document Year
                    //IF YES - then set minimun year to Last Document Year
                    //IF NO - then set minimum year to Registry Value
                    _PatientMinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false, _OpenExternalSource);
                    if (System.Convert.ToInt32(_minYearValue) > _PatientMinYear)
                    { _MinYear = _PatientMinYear; }//oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false); }
                    else
                    { _MinYear = System.Convert.ToInt32(_minYearValue); }
                }
                else
                { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource); }


                //End Changes 20090225 - Sagar Ghodke

                _MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false,_OpenExternalSource);

                #region "Show attention Message"
                //if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                //{
                string _PrevYear = string.Empty;
                if (_PatientMinYear != _MaxYear)
                {
                    _PrevYear = oList.GetLastPreviousYearDocuments(_PatientID, gloEDocV3Admin.gClinicID, DateTime.Now.Year, _OpenExternalSource);  //_PatientMinYear.ToString() + " - " + _MaxYear.ToString();
                }
                else if (_PatientMinYear == DateTime.Now.Year)
                {
                    _PrevYear = string.Empty;
                }
                else
                {
                    _PrevYear = _PatientMinYear.ToString();
                }

                if (_PrevYear != string.Empty)
                {
                    lblAlertMessage.Text = "Attention: Records can be viewed in years: " + _PrevYear;
                }
                else
                {
                    lblAlertMessage.Text = string.Empty;
                }
                //}
                //else
                //{
                //    lblAlertMessage.Text = string.Empty;
                //}
                #endregion " Show attention Message "

                
                numYear.Minimum = _MinYear - 1;
                if (_MaxYear > DateTime.Now.Year)
                {
                    numYear.Maximum = _MaxYear + 2;
                    numYear.Value = DateTime.Now.Year;
                    _SelectedYear = DateTime.Now.Year.ToString();
                }
                else
                {
                    if (_MaxYear < DateTime.Now.Year)
                    { numYear.Maximum = DateTime.Now.Year + 2; }
                    numYear.Value = _MaxYear;
                    _SelectedYear = _MaxYear.ToString();
                }
                tsb_ChangeYear.Text = numYear.Value.ToString();

                if ((numYear.Value - 1) == numYear.Minimum)
                {
                    tsb_ChangeYearPrevious.Enabled = false;
                }
                else
                {
                    tsb_ChangeYearPrevious.Enabled = true;
                }
                //End Changes - 20080820
                if ((numYear.Value + 1) == numYear.Maximum)
                {
                    //tsb_ChangeYearNext.Visible = false;
                    tsb_ChangeYearNext.Enabled = false;
                }
                else
                {
                    //tsb_ChangeYearNext.Visible = true;
                    tsb_ChangeYearNext.Enabled = true;
                }
                //// Check Patient has Archive Docuemnt 
                if (oList.GetArchiveInformation(_PatientID, _OpenExternalSource) == true)
                {
                    tsb_Archive.Visible = true;
                    tls_MaintainDoc.ButtonsToHide.Remove(tsb_Archive.Name.ToString());
                }
                else
                {
                    tsb_Archive.Visible = false;
                    if (tls_MaintainDoc.ButtonsToHide.Contains(tsb_Archive.Name.ToString()) == false)
                        tls_MaintainDoc.ButtonsToHide.Add(tsb_Archive.Name.ToString());
                }
                oList.Dispose();
                oList = null;

                #endregion
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization && _OpenExternalSource != enum_OpenExternalSource.RCM)
                {
                    LoadPatientStrip();
                }

                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                #region "Show/Hide ToolBar\Menus"

                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    tsb_ChangeYearPrevious.Enabled = true;
                    tsb_ChangeYearNext.Enabled = true;
                    tsb_Acknowledge.Enabled = true;
                    tsb_ViewAcknowledge.Enabled = true;
                    tsb_Import.Enabled = true;
                    tsb_Scan.Enabled = true;
                    tsb_Delete.Enabled = true;
                    tsb_DeletePage.Enabled = true;
                }
                else
                {
                    tsb_CopyDocument.Enabled = true;
                    if (IsShowCopyExisting == false)
                    {
                        tsb_CopyDocument.Enabled = false;
                    }
                    if (c1Documents.Rows.Count > 1)
                    {

                        tsb_Scan.Enabled = false;
                        tsb_Import.Enabled = false;
                    }
                    else
                    {

                        tsb_Scan.Enabled = true;
                        tsb_Import.Enabled = true;
                    }
                    tsb_Delete.Enabled = true;
                    tsb_DeletePage.Enabled = true;
                    tsb_ChangeYearPrevious.Enabled = false;
                    tsb_ChangeYearNext.Enabled = false;
                    tsb_Acknowledge.Enabled = false;
                    tsb_ViewAcknowledge.Enabled = false;
                }


                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    tsb_InsertSign1.Enabled = true;
                    tsb_Print.Enabled = true;
                    tsb_PrintAll.Enabled = true;
                    tsb_Fax.Enabled = true;
                    tsb_FaxAll.Enabled = true;
                    tsb_AddNote.Enabled = true;
                    tsb_AddTags.Enabled = true;
                    tsb_RotateBack.Enabled = true;
                    tsb_RotateForward.Enabled = true;
                    tsb_Refresh.Enabled = true;
                    tsb_History.Enabled = true;
                    tsb_Search.Enabled = true;

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        tsb_ShowHideAck.Visible = true;
                        tsb_ProviderSign.Enabled= false;
                        tsb_InsertSign.Enabled = false;
                        tsb_Task.Enabled = false;

                        stsannot_Signature.Enabled = false;
                        stsannot_Signature.Visible = false;
                        stsannot_ProviderSign.Enabled = false;
                        stsannot_ProviderSign.Visible = false;

                        tsb_ProviderSign.Visible = false;
                        tsb_InsertSign.Visible = false;
                        //tsb_Annotate.Visible = false;
                        tsb_Task.Visible = false;
                        //tsb_Save.Visible = false;
                        tsb_CopyDocument.Visible = false;

                        tsb_Fax.Enabled = false;
                        tsb_Fax.Visible = false;

                        tls_MaintainDoc.ButtonsToHide.Add(tsb_Fax.Name.ToString());

                        tls_MaintainDoc.ButtonsToHide.Add(tsb_ProviderSign.Name.ToString());
                        tls_MaintainDoc.ButtonsToHide.Add(tsb_InsertSign.Name.ToString());
                        //tls_MaintainDoc.ButtonsToHide.Add(tsb_Annotate.Name.ToString());
                        tls_MaintainDoc.ButtonsToHide.Add(tsb_Task.Name.ToString());
                        //tls_MaintainDoc.ButtonsToHide.Add(tsb_Save.Name.ToString());
                        tls_MaintainDoc.ButtonsToHide.Add(tsb_CopyDocument.Name.ToString());

                        if (tls_MaintainDoc.ButtonsToHide.Contains(tsb_ShowHideAck.Name.ToString()))
                        { tls_MaintainDoc.ButtonsToHide.Remove(tsb_ShowHideAck.Name.ToString()); }
                        
                    }
                }
                else
                {
                    tsb_InsertSign1.Enabled = false;
                    tsb_Print.Enabled = true;
                    tsb_PrintAll.Enabled = false;
                    tsb_Fax.Enabled = true;
                    tsb_FaxAll.Enabled = false;
                    tsb_AddNote.Enabled = false;
                    tsb_AddTags.Enabled = false;
                    tsb_RotateBack.Enabled = false;
                    tsb_RotateForward.Enabled = false;
                    tsb_Refresh.Enabled = false;
                    tsb_History.Enabled = false;
                    tsb_Search.Enabled = false;
                    tsb_ProviderSign.Enabled = false;
                    tsb_Annotate.Enabled = false;
                    tsb_InsertSign.Enabled = false;
                    tsb_ProviderSign.Enabled = false;

                    tsb_InsertSign1.Visible = false;
                    tsb_Print.Visible = true;
                    tsb_PrintAll.Visible = false;
                    tsb_Fax.Visible = true;
                    tsb_FaxAll.Visible = false;
                    tsb_AddNote.Visible = false;
                    tsb_AddTags.Visible = false;
                    tsb_RotateBack.Visible = false;
                    tsb_RotateForward.Visible = false;
                    tsb_Refresh.Visible = false;
                    tsb_History.Visible = false;
                    tsb_Search.Visible = false;
                    tsb_ProviderSign.Visible = false;
                    tsb_Annotate.Visible = false;
                    tsb_InsertSign.Visible = false;
                    tsb_ProviderSign.Visible = false;
                    tsb_ChangeYearPrevious.Visible = false;
                    tsb_ChangeYearNext.Visible = false;
                    tsb_ChangeYear.Visible = false;
                    if (_PatientID == -1)
                    {
                        tsb_Fax.Enabled = false;
                        tsb_Fax.Visible = false;
                    }

                }
                #endregion
                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);


                #region "Set Default category"
                SetLastDMSSettings();
                #endregion""



                #region "Load First Document"
                //Now Load Document is depend on last selected Document.



                #region "Acknowledge\Review"
                int _rowIndex = c1Documents.RowSel;
                if (_rowIndex >= 0)
                {
                    if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                    }
                    else
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                    }
                }
                #endregion

                #endregion

                if (_OpenExternalSource != enum_OpenExternalSource.RCM)
                {
                    this.Text = "Scan Documents";
                    this.Icon = global:: gloEDocumentV3.Properties.Resources.Scan;
                }

                #endregion

            }
            else if (_OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocument)
            {
                #region OpeneDocumentAs ViewDocument

                #region "Show/Hide Controls"
                ShowHideControl(ShowHideFlag.Opearations, false);
                ShowHideControl(ShowHideFlag.Tags, false);
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    ShowHideControl(ShowHideFlag.Patient, true);
                }
                else
                {
                    pnlNotes.Visible = false;
                    panel19.Visible = false;
                    ShowHideControl(ShowHideFlag.Patient, false);
                }

                ShowHideControl(ShowHideFlag.Legend, false);
                ShowHideControl(ShowHideFlag.Search, false);
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    btnPat_Up_Click(null, null);
                    btnNote_Down_Click(null, null);
                    btn_Right_Click(null, null);
                }
                //Code Added by dipak 20091009 to SetDMSPagePriviewSetting in order to fix bug 
                //No:4445 ->Make default document view while in Scan Docs/View Docs customizable by user.
                SetDMSPagePreviewSetting();

                tsb_Acknowledge.Enabled = true;

                #endregion

                #region "Year Settings"

                #region " Read Min Year Value from Registry "

                string _minYearValue = "2006";
                bool _isValuePresent = false;
                try
                {

                    gloRegistrySetting.OpenRemoteBaseKey();
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                    {
                        // Read Setting From Registry
                        if (System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) != ""
                            && System.Convert.ToInt64(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) > 0)
                        {
                            _minYearValue = System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue"));
                            _isValuePresent = true;
                        }
                    }

                    if (_isValuePresent == false)
                    {
                        //If no field for the Value is present in the Registry 
                        //Make entry with the default value
                        gloRegistrySetting.OpenRemoteBaseKey();

                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                        {
                            gloRegistrySetting.SetRegistryValue("DMSMinYearValue", _minYearValue);
                            //gloRegistrySetting.CloseRegistryKey(); //SG: Memory Leaks, closing the key in finally to apply for all opened keys
                            _isValuePresent = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
                finally
                {
                    gloRegistrySetting.CloseRegistryKey();
                }

                #endregion " Read Min Year Value from Registry "

                int _MinYear = DateTime.Now.Year;
                int _MaxYear = DateTime.Now.Year;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

                if (_isValuePresent)
                {
                    //Even if the value is present in the Registry check if the value is not
                    //greater than the Last Document Year
                    //IF YES - then set minimun year to Last Document Year
                    //IF NO - then set minimum year to Registry Value
                    if (System.Convert.ToInt32(_minYearValue) > oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false,_OpenExternalSource))
                    { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false,_OpenExternalSource); }
                    else
                    { _MinYear = System.Convert.ToInt32(_minYearValue); }
                }
                else
                { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource); }


                //End Changes 20090225 - Sagar Ghodke

                _MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false,_OpenExternalSource);
               
                numYear.Minimum = _MinYear - 1;
                if (_MaxYear > DateTime.Now.Year)
                {
                    numYear.Maximum = _MaxYear + 2;
                    numYear.Value = DateTime.Now.Year;
                    _SelectedYear = DateTime.Now.Year.ToString();
                }
                else
                {
                    if (_MaxYear < DateTime.Now.Year)
                    { numYear.Maximum = DateTime.Now.Year + 2; }
                    numYear.Value = _MaxYear;
                    _SelectedYear = _MaxYear.ToString();
                }
                tsb_ChangeYear.Text = numYear.Value.ToString();

                if ((numYear.Value - 1) == numYear.Minimum)
                {
                    tsb_ChangeYearPrevious.Enabled = false;
                }
                else
                {
                    tsb_ChangeYearPrevious.Enabled = true;
                }
                //End Changes - 20080820
                if ((numYear.Value + 1) == numYear.Maximum)
                {
                    //tsb_ChangeYearNext.Visible = false;
                    tsb_ChangeYearNext.Enabled = false;
                }
                else
                {
                    //tsb_ChangeYearNext.Visible = true;
                    tsb_ChangeYearNext.Enabled = true;
                }
                oList.Dispose();
                oList = null;
                #endregion
                if (_OpenExternalSource != enum_OpenExternalSource.Immunization)
                {
                    LoadPatientStrip();
                }
                #region "Show/Hide ToolBar\Menus"


                eDocInstance.tsb_ChangeYearPrevious.Enabled = true;
                eDocInstance.tsb_ChangeYearNext.Enabled = true;
                eDocInstance.tsb_Acknowledge.Enabled = true;
                eDocInstance.tsb_ViewAcknowledge.Enabled = true;

                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

              

                if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {
                    eDocInstance.tsb_Delete.Enabled = true;
                    eDocInstance.tsb_DeletePage.Enabled = true;
                    if (IsDeleteEnabledFromImmunization == false)
                    {
                        eDocInstance.tsb_Delete.Enabled = false;
                        eDocInstance.tsb_DeletePage.Enabled = false;
                    }
                    tsb_CopyDocument.Enabled = true;
                    if (IsShowCopyExisting == false)
                    {
                        tsb_CopyDocument.Enabled = false;
                    }
                    if (c1Documents.Rows.Count > 1)
                    {

                        eDocInstance.tsb_Scan.Enabled = false;
                        eDocInstance.tsb_Import.Enabled = false;
                    }
                    else
                    {

                        eDocInstance.tsb_Scan.Enabled = true;
                        eDocInstance.tsb_Import.Enabled = true;
                    }
                    eDocInstance.tsb_InsertSign1.Enabled = false;
                    eDocInstance.tsb_Print.Enabled = true;
                    eDocInstance.tsb_PrintAll.Enabled = false;
                    eDocInstance.tsb_Fax.Enabled = true;
                    eDocInstance.tsb_FaxAll.Enabled = false;
                    eDocInstance.tsb_AddNote.Enabled = false;
                    eDocInstance.tsb_AddTags.Enabled = false;
                    eDocInstance.tsb_RotateBack.Enabled = false;
                    eDocInstance.tsb_RotateForward.Enabled = false;
                    eDocInstance.tsb_Refresh.Enabled = false;
                    eDocInstance.tsb_History.Enabled = false;
                    eDocInstance.tsb_Search.Enabled = false;
                    eDocInstance.tsb_Annotate.Enabled = false;
                    eDocInstance.tsb_ChangeYearPrevious.Enabled = false;
                    eDocInstance.tsb_ChangeYearNext.Enabled = false;
                    eDocInstance.tsb_InsertSign.Enabled = false;
                    eDocInstance.tsb_ProviderSign.Enabled = false;

                    tsb_Acknowledge.Enabled = false;

                    eDocInstance.tsb_InsertSign1.Visible = false;
                    eDocInstance.tsb_Print.Visible = true;
                    eDocInstance.tsb_PrintAll.Visible = false;
                    eDocInstance.tsb_Fax.Visible = true;
                    eDocInstance.tsb_FaxAll.Visible = false;
                    eDocInstance.tsb_AddNote.Visible = false;
                    eDocInstance.tsb_AddTags.Visible = false;
                    eDocInstance.tsb_RotateBack.Visible = false;
                    eDocInstance.tsb_RotateForward.Visible = false;
                    eDocInstance.tsb_Refresh.Visible = false;
                    eDocInstance.tsb_History.Visible = false;
                    eDocInstance.tsb_Search.Visible = false;
                    eDocInstance.tsb_Annotate.Visible = false;
                    eDocInstance.tsb_ChangeYearPrevious.Visible = false;
                    eDocInstance.tsb_ChangeYearNext.Visible = false;
                    eDocInstance.tsb_ChangeYear.Visible = false;
                    eDocInstance.tsb_InsertSign.Visible = false;
                    eDocInstance.tsb_ProviderSign.Visible = false;

                    tsb_Acknowledge.Visible = false;
                    if (_PatientID == -1)
                    {
                        tsb_Fax.Enabled = false;
                        tsb_Fax.Visible = false;
                    }
                }
                else
                {
                    eDocInstance.tsb_Import.Enabled = false;
                    eDocInstance.tsb_Scan.Enabled = false;
                    eDocInstance.tsb_Delete.Enabled = false;
                    eDocInstance.tsb_DeletePage.Enabled = false;
                    eDocInstance.tsb_InsertSign1.Enabled = true;
                    eDocInstance.tsb_Print.Enabled = true;
                    eDocInstance.tsb_PrintAll.Enabled = true;
                    eDocInstance.tsb_Fax.Enabled = true;
                    eDocInstance.tsb_FaxAll.Enabled = true;
                    eDocInstance.tsb_AddNote.Enabled = true;
                    eDocInstance.tsb_AddTags.Enabled = true;
                    eDocInstance.tsb_RotateBack.Enabled = true;
                    eDocInstance.tsb_RotateForward.Enabled = true;
                    eDocInstance.tsb_Refresh.Enabled = true;
                    eDocInstance.tsb_History.Enabled = true;
                    eDocInstance.tsb_Search.Enabled = true;
                }
                //eDocInstance.tsb_InsertSign.Enabled = false; // COMMENT BY SUDHIR 20090626 ''



                #endregion
                //FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                //FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

             

                #region "Load First Document"

                //Int64 _docid = 0;
                //Int64 _contid = 0;

                //code added to open document from RxMeds for case GLO2011-0013188
                if (_OpenExternalSource == enum_OpenExternalSource.RxMeds || _OpenExternalSource == enum_OpenExternalSource.Immunization)
                {
                    SelectDocumentInGrid(_ExternalDocumentID, _ExternalContainerID);

                    //08-Mar-2017 Aniket: Commented the following line to stop duplicate database calls
                    //LoadDocument(_ExternalDocumentID, _ExternalContainerID, _OpenExternalSource);
                }
                else
                {
                    //GetFirstDocumentInGrid(out _docid, out _contid);
                    //LoadDocument(_docid, _contid);

                    #region "Set Default category"

                    SetLastDMSSettings();

                    #endregion""
                }


                #region "Acknowledge\Review"
                int _rowIndex = c1Documents.RowSel;
                if (_rowIndex >= 0)
                {
                    if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                    }
                    else
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                    }
                }
                #endregion
                #endregion

                this.Text = "View Documents";

                #endregion


            }
            else if (_OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule)
            {
                #region OpeneDocumentAs ViewDocumentForExternalModule

                #region "Show/Hide Controls"
                if (_OpenExternalSource == enum_OpenExternalSource.AdvanceDirective || _OpenExternalSource == enum_OpenExternalSource.ViewTask || _OpenExternalSource == enum_OpenExternalSource.RxMeds || _OpenExternalSource == enum_OpenExternalSource.Immunization)
                {
                    ShowHideControl(ShowHideFlag.Document, true);
                }
                else
                {
                    ShowHideControl(ShowHideFlag.Document, false);
                }
                ShowHideControl(ShowHideFlag.Opearations, false);
                ShowHideControl(ShowHideFlag.Tags, false);

                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                {
                    ShowHideControl(ShowHideFlag.Patient, false);
                }
                else
                {
                    ShowHideControl(ShowHideFlag.Patient, true);
                }
                
                ShowHideControl(ShowHideFlag.Legend, false);
                ShowHideControl(ShowHideFlag.Search, false);

                SetDMSPagePreviewSetting();

                tsb_Acknowledge.Enabled = true;

                #endregion

                #region "Year Settings"


                #region " Read Min Year Value from Registry "

                string _minYearValue = "2006";
                bool _isValuePresent = false;
                try
                {

                    gloRegistrySetting.OpenRemoteBaseKey();
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                    {
                        // Read Setting From Registry
                        if (System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) != ""
                            && System.Convert.ToInt64(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) > 0)
                        {
                            _minYearValue = System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue"));
                            _isValuePresent = true;
                        }
                    }

                    if (_isValuePresent == false)
                    {
                        //If no field for the Value is present in the Registry 
                        //Make entry with the default value
                        gloRegistrySetting.OpenRemoteBaseKey();
                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                        {
                            gloRegistrySetting.SetRegistryValue("DMSMinYearValue", _minYearValue);
                            //gloRegistrySetting.CloseRegistryKey(); //SG: Memory Leaks, closing the key in finally to apply for all opened keys
                            _isValuePresent = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
                finally
                { gloRegistrySetting.CloseRegistryKey(); }

                #endregion " Read Min Year Value from Registry "

                int _MinYear = DateTime.Now.Year;
                int _MaxYear = DateTime.Now.Year;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

                if (_isValuePresent)
                {

                    if (System.Convert.ToInt32(_minYearValue) > oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false,_OpenExternalSource))
                    { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false,_OpenExternalSource); }
                    else
                    { _MinYear = System.Convert.ToInt32(_minYearValue); }
                }
                else
                { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource); }


                //End Changes 20090225 - Sagar Ghodke

                //code Commented by Dipak 20090819
                //_MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false);
                //end comment

                //code added by dipak 20090819 to fix th bug 1715:Incorrect label in View Document
                _MaxYear = oList.GetYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, _ExternalDocumentID, _OpenExternalSource);
                oList.Dispose();
                oList = null;
                numYear.Minimum = _MinYear - 1;
                if (_MaxYear > DateTime.Now.Year)
                {
                    numYear.Maximum = _MaxYear + 2;
                    numYear.Value = DateTime.Now.Year;
                    _SelectedYear = DateTime.Now.Year.ToString();
                }
                else
                {
                    if (_MaxYear < DateTime.Now.Year)
                    { numYear.Maximum = DateTime.Now.Year + 2; }
                    numYear.Value = _MaxYear;
                    _SelectedYear = _MaxYear.ToString();
                }

                //line commented & aded by dipak 20091224 to fix bug no :2700 Show Wrong year while view document of different year
                //tsb_ChangeYear.Text = numYear.Value.ToString();
                tsb_ChangeYear.Text = _MaxYear.ToString(); //year of doc
                //end added by dipak


                if ((numYear.Value - 1) == numYear.Minimum)
                {
                    tsb_ChangeYearPrevious.Enabled = false;
                }
                else
                {
                    tsb_ChangeYearPrevious.Enabled = true;
                }
                //End Changes - 20080820
                if ((numYear.Value + 1) == numYear.Maximum)
                {
                    //tsb_ChangeYearNext.Visible = false;
                    tsb_ChangeYearNext.Enabled = false;
                }
                else
                {
                    //tsb_ChangeYearNext.Visible = true;
                    tsb_ChangeYearNext.Enabled = true;
                }
                #endregion

                if (_OpenExternalSource != enum_OpenExternalSource.RCM)
                {
                    LoadPatientStrip();
                }

                btnPat_Up_Click(null, null);
                btnNote_Down_Click(null, null);

                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

                #region "Load First Document"
                Int64 _docid = 0;
                Int64 _contid = 0;

                if (_OpenExternalSource == enum_OpenExternalSource.RxMeds || _OpenExternalSource == enum_OpenExternalSource.Immunization)
                {
                    GetSelectedDocumentInGrid(out _ExternalDocumentID, out _ExternalContainerID);
                    //08-Mar-2017 Aniket: Commented the following line to stop duplicate database calls
                    //LoadDocument(_ExternalDocumentID, _ExternalContainerID, _OpenExternalSource);
                }
                else if (_OpenExternalSource == enum_OpenExternalSource.DashBoard || _OpenExternalSource == enum_OpenExternalSource.LabOrder || _OpenExternalSource == enum_OpenExternalSource.ViewPatientSummary || _OpenExternalSource == enum_OpenExternalSource.ViewTask || _OpenExternalSource == enum_OpenExternalSource.RCM)//condition added to fix bug 12709
                {
                    SelectDocumentInGrid(_ExternalDocumentID, _ExternalContainerID);
                    //08-Mar-2017 Aniket: Commented the following line to stop duplicate database calls
                    //LoadDocument(_ExternalDocumentID, _ExternalContainerID, _OpenExternalSource);
                }
                else
                {
                    GetFirstDocumentInGrid(out _docid, out _contid);
                    LoadDocument(_docid, _contid, _OpenExternalSource);
                }

                #region "Acknowledge\Review"
                int _rowIndex = c1Documents.RowSel;
                if (_rowIndex >= 0)
                {
                    if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                    }
                    else
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                    }
                }
                #endregion
                #endregion

                #region "Show/Hide ToolBar\Menus"
                if (eDocInstance != null)
                {

                    eDocInstance.tsb_Import.Enabled = false;
                    eDocInstance.tsb_ChangeYearPrevious.Enabled = false;
                    eDocInstance.tsb_ChangeYearNext.Enabled = false;
                    eDocInstance.tsb_Acknowledge.Enabled = true;
                    eDocInstance.tsb_ViewAcknowledge.Enabled = true;
                    eDocInstance.tsb_Scan.Enabled = false;
                    eDocInstance.tsb_Delete.Enabled = false;
                    eDocInstance.tsb_DeletePage.Enabled = false;
                    //eDocInstance.tsb_InsertSign.Enabled = false; // COMMENT BY SUDHIR 20090626 ''
                    eDocInstance.tsb_InsertSign1.Enabled = true;
                    eDocInstance.tsb_Print.Enabled = true;
                    eDocInstance.tsb_PrintAll.Enabled = true;
                    eDocInstance.tsb_Fax.Enabled = true;
                    eDocInstance.tsb_FaxAll.Enabled = true;
                    eDocInstance.tsb_AddNote.Enabled = true;
                    eDocInstance.tsb_AddTags.Enabled = true;
                    eDocInstance.tsb_RotateBack.Enabled = true;
                    eDocInstance.tsb_RotateForward.Enabled = true;
                    //end comment and add by dipak 20091003
                    eDocInstance.tsb_Refresh.Enabled = true;
                    eDocInstance.tsb_History.Enabled = true;
                    eDocInstance.tsb_Search.Enabled = false;

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        eDocInstance.tsb_ShowHideAck.Visible = false;
                        eDocInstance.tsb_ProviderSign.Enabled = false;
                        eDocInstance.tsb_InsertSign.Enabled = false;
                        eDocInstance.tsb_Task.Enabled = false;

                        eDocInstance.stsannot_Signature.Enabled = false;
                        eDocInstance.stsannot_Signature.Visible = false;
                        eDocInstance.stsannot_ProviderSign.Enabled = false;
                        eDocInstance.stsannot_ProviderSign.Visible = false;

                        eDocInstance.tsb_ProviderSign.Visible = false;
                        eDocInstance.tsb_InsertSign.Visible = false;
                        eDocInstance.tsb_Task.Visible = false;
                        eDocInstance.tsb_CopyDocument.Visible = false;

                        eDocInstance.tsb_Fax.Enabled = false;
                        eDocInstance.tsb_Fax.Visible = false;

                        eDocInstance.tls_MaintainDoc.ButtonsToHide.Add(tsb_Fax.Name.ToString());

                        eDocInstance.tls_MaintainDoc.ButtonsToHide.Add(tsb_ProviderSign.Name.ToString());
                        eDocInstance.tls_MaintainDoc.ButtonsToHide.Add(tsb_InsertSign.Name.ToString());
                        eDocInstance.tls_MaintainDoc.ButtonsToHide.Add(tsb_Task.Name.ToString());
                        eDocInstance.tls_MaintainDoc.ButtonsToHide.Add(tsb_CopyDocument.Name.ToString());

                        eDocInstance.tls_MaintainDoc.ButtonsToHide.Add(tsb_ShowHideAck.Name.ToString());

                    }
                }
                #endregion

                if (_OpenExternalSource != enum_OpenExternalSource.RCM)
                {
                    this.Text = "View Documents";
                }
                #endregion
            }
            else if (_OpenEDocumentAs == enum_OpenEDocumentAs.ViewAllDocuments)
            {
                //Bugzilla ID-4056, RC3 List No -36
                //Enable all buttons excpet Scan, Import  & Delete 
                #region OpeneDocumentAs ViewAllDocuments

                #region "Show/Hide Controls"
                ShowHideControl(ShowHideFlag.Opearations, false);
                ShowHideControl(ShowHideFlag.Tags, false);
                ShowHideControl(ShowHideFlag.Patient, true);
                ShowHideControl(ShowHideFlag.Legend, false);
                ShowHideControl(ShowHideFlag.Search, false);

                btnPat_Up_Click(null, null);
                btnNote_Down_Click(null, null);
                btn_Right_Click(null, null);

                SetDMSPagePreviewSetting();

                tsb_Acknowledge.Enabled = true;

                #endregion

                #region "Year Settings"

                #region " Read Min Year Value from Registry "

                string _minYearValue = "2006";
                bool _isValuePresent = false;
                try
                {

                    gloRegistrySetting.OpenRemoteBaseKey();
                    if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                    {
                        // Read Setting From Registry
                        if (System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) != ""
                            && System.Convert.ToInt64(gloRegistrySetting.GetRegistryValue("DMSMinYearValue")) > 0)
                        {
                            _minYearValue = System.Convert.ToString(gloRegistrySetting.GetRegistryValue("DMSMinYearValue"));
                            _isValuePresent = true;
                        }
                    }

                    if (_isValuePresent == false)
                    {
                        //If no field for the Value is present in the Registry 
                        //Make entry with the default value
                        gloRegistrySetting.OpenRemoteBaseKey();
                        if (gloRegistrySetting.OpenSubKey(gloRegistrySetting.gstrSoftEMR, true, "") == true)
                        {
                            gloRegistrySetting.SetRegistryValue("DMSMinYearValue", _minYearValue);
                            //gloRegistrySetting.CloseRegistryKey(); //SG: Memory Leaks, closing the key in finally to apply for all opened keys
                            _isValuePresent = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                }
                finally
                { gloRegistrySetting.CloseRegistryKey(); }

                #endregion " Read Min Year Value from Registry "


                int _MinYear = DateTime.Now.Year;
                int _MaxYear = DateTime.Now.Year;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();

                if (_isValuePresent)
                {
                    if (System.Convert.ToInt32(_minYearValue) > oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false,_OpenExternalSource))
                    { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, false,_OpenExternalSource); }
                    else
                    { _MinYear = System.Convert.ToInt32(_minYearValue); }
                }
                else
                { _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource); }


                //End Changes 20090225 - Sagar Ghodke

                _MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false,_OpenExternalSource);
                
                numYear.Minimum = _MinYear - 1;
                if (_MaxYear > DateTime.Now.Year)
                {
                    numYear.Maximum = _MaxYear + 2;
                    numYear.Value = DateTime.Now.Year;
                    _SelectedYear = DateTime.Now.Year.ToString();
                }
                else
                {
                    if (_MaxYear < DateTime.Now.Year)
                    { numYear.Maximum = DateTime.Now.Year + 2; }
                    numYear.Value = _MaxYear;
                    _SelectedYear = _MaxYear.ToString();
                }
                tsb_ChangeYear.Text = numYear.Value.ToString();

                if ((numYear.Value - 1) == numYear.Minimum)
                {
                    tsb_ChangeYearPrevious.Enabled = false;
                }
                else
                {
                    tsb_ChangeYearPrevious.Enabled = true;
                }
                //End Changes - 20080820
                if ((numYear.Value + 1) == numYear.Maximum)
                {
                    //tsb_ChangeYearNext.Visible = false;
                    tsb_ChangeYearNext.Enabled = false;
                }
                else
                {
                    //tsb_ChangeYearNext.Visible = true;
                    tsb_ChangeYearNext.Enabled = true;
                }
                //// Check Patient has Archive Docuemnt 
                if (oList.GetArchiveInformation(_PatientID, _OpenExternalSource) == true)
                {
                    tsb_Archive.Visible = true;
                    tls_MaintainDoc.ButtonsToHide.Remove(tsb_Archive.Name.ToString());
                }
                else
                {
                    tsb_Archive.Visible = false;
                    if (tls_MaintainDoc.ButtonsToHide.Contains(tsb_Archive.Name.ToString()) == false)
                        tls_MaintainDoc.ButtonsToHide.Add(tsb_Archive.Name.ToString());
                }
                oList.Dispose();
                oList = null;
                #endregion

                LoadPatientStrip();
                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

                #region "Load First Document"

                SetLastDMSSettings();


                #region "Acknowledge\Review"
                int _rowIndex = c1Documents.RowSel;
                if (_rowIndex >= 0)
                {
                    if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                    }
                    else
                    {
                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                    }
                }
                #endregion
                #endregion

                #region "Show/Hide ToolBar\Menus"
                tsb_Import.Enabled = false;
                tsb_ChangeYearPrevious.Enabled = true;
                tsb_ChangeYearNext.Enabled = true;
                tsb_Acknowledge.Enabled = true;
                tsb_ViewAcknowledge.Enabled = true;
                tsb_Scan.Enabled = false;
                tsb_Delete.Enabled = false;
                tsb_DeletePage.Enabled = true;
                tsb_InsertSign1.Enabled = true;
                tsb_Print.Enabled = true;
                tsb_PrintAll.Enabled = true;
                tsb_Fax.Enabled = true;
                tsb_FaxAll.Enabled = true;
                tsb_AddNote.Enabled = true;
                tsb_AddTags.Enabled = true;
                tsb_RotateBack.Enabled = true;
                tsb_RotateForward.Enabled = true;
                tsb_Refresh.Enabled = true;
                tsb_History.Enabled = true;
                tsb_Search.Enabled = true;
                #endregion

                this.Text = "View Documents";
                #endregion
            }

            //if (uiPanSplitScreen != null)
            //{
            //    if ((uiPanSplitScreen.Parent) != null)
            //    {
            //        if (uiPanSplitScreen.Parent.Text == "Split Screen")
            //        {
            //            uiPanSplitScreen.Parent.Visible = true;
            //            uiPanSplitScreen.Parent.Dispose();
            //        }
            //        if (uiPanSplitScreen.Text == "Split Screen")
            //        {
            //            uiPanSplitScreen.Dispose();
            //            uiPanSplitScreen.Visible = false;

            //        }
            //        if (clsSplit != null)
            //        {
            //            clsSplit.Dispose();
            //            clsSplit = null;
            //        }
            //    }
            //}

            _IsDocumentsLoading = false;
            _IsFormLoading = false;

            //if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            //{
            //    ShowHideAckDocuments(flgShowAckDocs); 
            //}
            
            try
            {
                gloPatient.gloPatient.GetWindowTitle(this, _PatientID, gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString, gloGlobal.gloPMGlobal.MessageBoxCaption);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.Load, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                IsDocLoaded = true;
            }
        }
        #endregion

        #region "Toolstrip Button event"

        private void tsb_Import_Click(object sender, EventArgs e)
        {
            gloEDocV3Management oValidateYear = new gloEDocumentV3.gloEDocV3Management();
            if (oValidateYear.ValidateScanInCurrentYear(tsb_ChangeYear.Text, "Import") == false)
            {
                oValidateYear.Dispose();
                oValidateYear = null;
                return;
            }
            if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
            {
            }
            else
            {
                if (uiPanSplitScreen != null)
                {
                    uiPanSplitScreen.Enabled = false;
                }
            }
            try
            {

                int _CurRow = c1Documents.RowSel;
                int _ImportCategoryID = 0;
                string _ImportCategory = "";
                string _ImportSubCategory = "";
                string _ImportYear = "";
                string _ImportMonth = "";
                Int64 _ReturnDocumentID = 0;
                Int64 _ReturnContainerID = 0;
                bool _Result = false;

                #region"Dhruv -> If the category Present or Not"
                bool _value = false;
                _value = gloEDocumentV3.eDocManager.eDocValidator.IsCategoryPresent(gloEDocV3Admin.gClinicID, _OpenExternalSource);
                if (_value == false)
                {
                    AddCategory();
                    _CurRow = c1Documents.RowSel;
                }
                else if (_CurRow < 0)
                {
                    MessageBox.Show("Select the category.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion


                if (_IsDocumentsLoading == false)
                {
                    if (_CurRow >= 0)
                    {
                        if (c1Documents.GetData(_CurRow, COL_COLTYPE) != null)
                        {
                            if ((enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.Document)
                            {
                                //_ImportCategory = c1Documents.Rows[_CurRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString();
                                //_ImportCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, COL_CATEGORY));
                                //_ImportSubCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, COL_SUBCATEGORY));
                                //_ImportCategoryID = System.Convert.ToInt32(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, COL_CATEGORYID));

                                _ImportCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_CATEGORY));
                                _ImportSubCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_SUBCATEGORY));
                                _ImportCategoryID = System.Convert.ToInt32(c1Documents.GetData(c1Documents.RowSel, COL_CATEGORYID));
                                _ReturnDocumentID = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                _ReturnContainerID = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                            }
                            else if ((enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.Category || (enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.SubCategory)
                            {
                                //_ImportCategory = c1Documents.Rows[_CurRow].Node.Data.ToString();
                                _ImportCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.Row.Index, COL_CATEGORY));
                                _ImportSubCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.Row.Index, COL_SUBCATEGORY));
                                _ImportCategoryID = System.Convert.ToInt32(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.Row.Index, COL_CATEGORYID));
                                _ReturnDocumentID = 0;
                                _ReturnContainerID = 0;
                            }

                            _ImportMonth = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                            _ImportYear = _SelectedYear;
                            //Line Commented by dipak 20091002 to fix bug no:Bug 4445 :Make default document view while in Scan Docs/View Docs customizable by user.(as comment no5 in bugzilla)
                            // UnloadDocuments();

                            #region "Load Import Form"
                            frmEDocEvent_Import oDocEvents = new frmEDocEvent_Import();

                            oDocEvents.oPatientID = _PatientID;
                            oDocEvents.oImportInCategoryID = _ImportCategoryID;
                            oDocEvents.oImportInCategory = _ImportCategory;
                            oDocEvents.oImportInSubCategory = _ImportSubCategory;
                            
                            oDocEvents.oImportInYear = _ImportYear;
                            oDocEvents.oImportInMonth = _ImportMonth;
                            oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                            oDocEvents._OpenExternalSource = _OpenExternalSource;
                            oDocEvents.ShowDialog(this);

                            

                            _Result = oDocEvents.oDialogResultIsOK;
                            if (oDocEvents.oDialogContainerID > 0 && oDocEvents.oDialogDocumentID > 0)
                            {
                                _ReturnContainerID = oDocEvents.oDialogContainerID;
                                _ReturnDocumentID = oDocEvents.oDialogDocumentID;
                                _DocumentIDForImmnunization = _ReturnDocumentID;
                                // _IsImportorScanClick = true;

                                //gloEDocV3Management frm = new gloEDocV3Management();
                                //  frm.DocumentImmunization =_DocumentIDForImmnunization ;
                                _ContainerIDForImmnunization = _ReturnContainerID;
                                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                                {
                                }
                                else
                                {
                                    if (clsSplit != null)
                                    {
                                        clsSplit.loadSplitControlData(_PatientID, VisitID, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, gloEDocV3Admin.gClinicID);
                                    }
                                }
                            }

                            oDocEvents.Dispose();
                            oDocEvents = null;
                            #endregion

                            #region "Refresh Events"
                            if (_Result == true)
                            {
                                if (_ReturnContainerID > 0 && _ReturnDocumentID > 0)
                                {
                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                    {
                                        tsb_Import.Enabled = false;
                                        tsb_Scan.Enabled = false;
                                        if (IsShowCopyExisting == true)
                                        {
                                            tsb_CopyDocument.Enabled = true;
                                        }
                                    }
                                    _IsDocumentsLoading = true;

                                    Application.DoEvents();

                                    #region "Wait Process"
                                    if (oProcessLabel != null)
                                    {
                                        if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                                        oProcessLabel.Dispose(); oProcessLabel = null;
                                    }
                                    oProcessLabel = new Label();
                                    pnlPreview.Controls.Add(oProcessLabel);
                                    oProcessLabel.Dock = DockStyle.Fill;
                                    oProcessLabel.Location = new System.Drawing.Point(0, 0);
                                    oProcessLabel.ForeColor = Color.Blue;
                                    oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                                    oProcessLabel.Text = "Please wait !!!";
                                    oProcessLabel.Name = "lblProcess";
                                    oProcessLabel.Visible = true;
                                    oProcessLabel.BringToFront();
                                    #endregion

                                    Application.DoEvents();

                                    #region "Fill Documents"
                                    lvwPages.BeginUpdate();
                                    lvwPages.Items.Clear();
                                    lvwPages.EndUpdate();
                                    UnloadDocuments();//Dhruv 20100622
                                    //UnloadDocView();
                                    //Added by Mayuri:20120123-To show documents agianst documentid and patinetid for VIS-Immunization
                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                    {
                                        FillCategories(c1Documents, false, _DocumentIDForImmnunization, _OpenExternalSource);
                                        FillDocuments(c1Documents, _SelectedYear, _PatientID, _DocumentIDForImmnunization, _OpenExternalSource);

                                    }
                                    else
                                    {
                                        FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                                        FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

                                    }
                                    #endregion

                                    LoadDocument(_ReturnDocumentID, _ReturnContainerID, _OpenExternalSource);
                                    //LoadPages(_ReturnContainerID, _ReturnDocumentID, gloEDocumentAdmin.gClinicID);
                                    this.c1Documents.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);
                                    SelectDocumentInGrid(_ReturnDocumentID, _ReturnContainerID);
                                    this.c1Documents.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);
                                    lvwPages_Click(null, null);
                                    _IsDocumentsLoading = false;

                                    #region "Year Settings"
                                    int _MinYear = DateTime.Now.Year;
                                    int _MaxYear = DateTime.Now.Year;
                                    eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                                    _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource);
                                    _MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false,_OpenExternalSource);
                                    oList.Dispose();
                                    oList = null;
                                    numYear.Minimum = _MinYear - 1;
                                    if (_MaxYear > DateTime.Now.Year)
                                    { numYear.Maximum = _MaxYear + 2; }
                                    else
                                    {
                                        if (_MaxYear < DateTime.Now.Year)
                                        { numYear.Maximum = DateTime.Now.Year + 2; }
                                    }
                                    if ((numYear.Value - 1) == numYear.Minimum)
                                    { tsb_ChangeYearPrevious.Enabled = false; }
                                    else
                                    { tsb_ChangeYearPrevious.Enabled = true; }

                                    if ((numYear.Value + 1) >= numYear.Maximum)
                                    { tsb_ChangeYearNext.Enabled = false; }
                                    else
                                    { tsb_ChangeYearNext.Enabled = true; }

                                    #endregion
                                    //Sanjog - Added on 2011 June 6 to show the Acknowledgement button when new image import
                                    if (System.Convert.ToBoolean(c1Documents.GetData(c1Documents.RowSel, COL_ISACKNOWLEDGE)) == true)
                                    {
                                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                    }
                                    else
                                    {
                                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                    }
                                    //Sanjog - Added on 2011 June 6 to show the Acknowledgement button when new image import

                                    #region "Enabled Buttons"
                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                    {
                                        tsb_ChangeYearNext.Enabled = false;
                                        tsb_ChangeYearPrevious.Enabled = false;
                                        tsb_Delete.Enabled = true;
                                    }
                                    else
                                    {
                                        if (tsb_Acknowledge.Enabled == false) { tsb_Acknowledge.Enabled = true; }
                                        if (tsb_Delete.Enabled == false) { tsb_Delete.Enabled = true; }
                                        if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                                        if (tsb_Print.Enabled == false) { tsb_Print.Enabled = true; }
                                        if (tsb_Fax.Enabled == false) { tsb_Fax.Enabled = true; }
                                        if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                                        if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                                        if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                                        if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                                        if (tsb_Refresh.Enabled == false) { tsb_Refresh.Enabled = true; }
                                        if (tsb_Search.Enabled == false) { tsb_Search.Enabled = true; }
                                        if (tsb_History.Enabled == false) { tsb_History.Enabled = true; }
                                    }
                                    #endregion

                                }
                            }
                            else
                            {
                                SelectDocumentInGrid(_ReturnDocumentID, _ReturnContainerID);
                                LoadDocument(_ReturnDocumentID, _ReturnContainerID, _OpenExternalSource);
                            }
                            #endregion


                        }
                    }
                }
                //MessageBox.Show("Documents Imported Successfully.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                {
                }
                else
                {
                    if (uiPanSplitScreen != null)
                    {
                        uiPanSplitScreen.Enabled = true;
                    }
                }
                if ((oValidateYear != null))
                {
                    oValidateYear.Dispose();
                    oValidateYear = null;
                }
            }
        }

        private void tsb_ChangeYearPrevious_Click(object sender, EventArgs e)
        {

            string _CurYr = tsb_ChangeYear.Text;

            bool _NextYear = true;
            bool _PreviousYear = true;
            tsb_ChangeYearNext.Enabled = false;
            tsb_ChangeYearPrevious.Enabled = false;

            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if ((numYear.Value - 1) >= numYear.Minimum)
                    {
                        numYear.Value = numYear.Value - 1;
                    }
                    if (System.Convert.ToInt32(_CurYr) != numYear.Value)
                    {
                        tsb_ChangeYear.Text = numYear.Value.ToString();

                        if ((numYear.Value - 1) == numYear.Minimum)
                        {
                            _PreviousYear = false;
                        }
                        else
                        {
                            _PreviousYear = true;
                        }
                        if ((numYear.Value + 1) == numYear.Maximum)
                        {
                            _NextYear = false;
                        }
                        else
                        {
                            _NextYear = true;
                        }
                        Cursor.Current = Cursors.WaitCursor;
                        ChangeYear_Click(numYear.Value.ToString());
                        if (OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
                        {
                            if (tsb_Scan.Enabled == false) { tsb_Scan.Enabled = true; }
                            if (tsb_Import.Enabled == false) { tsb_Import.Enabled = true; }
                            if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                            if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                            if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                            if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                            if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
                if (lvwPages.Items.Count > 0) { lvwPages.Cursor = Cursors.Hand; }
                else { lvwPages.Cursor = Cursors.Default; }

                //05-Apr-17 Aniket: Resolving Bug #104938: gloEMR: Scan Docs: After change year close button loosing focus
                this.Focus();
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
                tsb_ChangeYearNext.Enabled = _NextYear;
                tsb_ChangeYearPrevious.Enabled = _PreviousYear;
            }
        }

        private void tsb_ChangeYearNext_Click(object sender, EventArgs e)
        {
            string _CurYr = tsb_ChangeYear.Text;

            bool _NextYear = true;
            bool _PreviousYear = true;
            tsb_ChangeYearNext.Enabled = false;
            tsb_ChangeYearPrevious.Enabled = false;

            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if ((numYear.Value + 1) <= numYear.Maximum)
                    {
                        numYear.Value = numYear.Value + 1;
                    }
                    if (System.Convert.ToInt32(_CurYr) != numYear.Value)
                    {
                        tsb_ChangeYear.Text = numYear.Value.ToString();

                        if ((numYear.Value - 1) == numYear.Minimum)
                        {
                            _PreviousYear = false;
                        }
                        else
                        {
                            _PreviousYear = true;
                        }
                        if ((numYear.Value + 1) == numYear.Maximum)
                        {
                            _NextYear = false;
                        }
                        else
                        {
                            _NextYear = true;
                        }
                        Cursor.Current = Cursors.WaitCursor;
                        ChangeYear_Click(numYear.Value.ToString());
                        if (OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
                        {
                            if (tsb_Scan.Enabled == false) { tsb_Scan.Enabled = true; }
                            if (tsb_Import.Enabled == false) { tsb_Import.Enabled = true; }
                            if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                            if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                            if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                            if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                            if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                        }
                        Cursor.Current = Cursors.Default;
                    }
                }
                if (lvwPages.Items.Count > 0) { lvwPages.Cursor = Cursors.Hand; }
                else { lvwPages.Cursor = Cursors.Default; }

                //05-Apr-17 Aniket: Resolving Bug #104938: gloEMR: Scan Docs: After change year close button loosing focus
                this.Focus();
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
            finally
            {
                tsb_ChangeYearPrevious.Enabled = _PreviousYear;
                tsb_ChangeYearNext.Enabled = _NextYear;
            }
        }


        #region "Dhruv 20100623 -> Acknowledgement Click"

        private void DMS_UpdateDMSTask(string DocumentIDs)
        {
            SqlConnection con = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                con = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
                cmd = new SqlCommand("DMS_UpdateDMSTask", con);
                SqlParameter objParam = default(SqlParameter);

                objParam = cmd.Parameters.Add("@DocumentIDs", SqlDbType.Text);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = DocumentIDs;

                objParam = cmd.Parameters.Add("@nserID", SqlDbType.Decimal);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = gloEDocV3Admin.gUserID;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                object id = cmd.ExecuteNonQuery();
                con.Close();
                objParam = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SG: Memory Leaks, disposing command variable
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }

                if ((con != null))
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Dispose(); con = null;
                }

            }
        }

        private void RCM_UpdateRCMTask(string DocumentIDs)
        {
            SqlConnection con = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                con = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
                cmd = new SqlCommand("RCM_UpdateRCMTask", con);
                SqlParameter objParam = default(SqlParameter);

                objParam = cmd.Parameters.Add("@DocumentIDs", SqlDbType.Text);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = DocumentIDs;

                objParam = cmd.Parameters.Add("@nserID", SqlDbType.Decimal);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = gloEDocV3Admin.gUserID;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                object id = cmd.ExecuteNonQuery();
                con.Close();
                objParam = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SG: Memory Leaks, disposing command variable
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }

                if ((con != null))
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Dispose(); con = null;
                }

            }
        }

        private void tsb_Acknowledge_Click(object sender, EventArgs e)
        {
           // blntaskack = true;
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {

                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    if (_IsDocumentsLoading == false)
                    {
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0)
                            {
                                bool _Result = false;
                                using (frmEDocEvent_Acknowledge oDocEvents = new frmEDocEvent_Acknowledge())
                                {

                                    oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                    if (System.Convert.ToBoolean(c1Documents.GetData(c1Documents.RowSel , COL_ISACKNOWLEDGE)) == true)
                                    {
                                        oDocEvents._IsAcknowledge = true;
                                    }
                                    else
                                    {
                                        oDocEvents._IsAcknowledge = false;
                                    }
                                    oDocEvents._OpenExternalSource = _OpenExternalSource;
                                    oDocEvents.ShowDialog(this);
                                    _Result = oDocEvents.oDialogResultIsOK;

                                    if (_Result)
                                    {
                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            RCM_UpdateRCMTask(_DocumentIDs);
                                        }
                                        else
                                        {
                                            DMS_UpdateDMSTask(_DocumentIDs);
                                        }
                                            
                                            //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                                            if (dMdi != null)
                                            {
                                                dMdi.ShowTasks();
                                                //Bug #82464: 00000909: Task screen not opening respective screen
                                                dMdi.ShowPatientDetails();
                                            }
                                    }

                                }
                                //oDocEvents.Dispose();

                                if (_Result == true)
                                {
                                    if (c1Documents != null && c1Documents.Rows.Count > 0)
                                    {
                                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                                        {
                                            int _fillrowindex = c1Documents.FindRow(oSelectedDocuments[i].DocumentID.ToString(), 0, COL_DOCUMENTID, false, true, false);
                                            c1Documents.SetData(_fillrowindex, COL_ISACKNOWLEDGE, true);
                                            c1Documents.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagAcknowledge);
                                            c1Documents.Rows[_fillrowindex].Style = c1Documents.Styles["style_Document_Acknowledge"];
                                        }

                                        #region "Acknowledge\Review"
                                        if (oSelectedDocuments.Count > 1)
                                        {
                                            tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_AcknowledgeReview;
                                            tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_AcknowledgeReview;
                                        }
                                        else if (oSelectedDocuments.Count == 1)
                                        {
                                            int _rowIndex = c1Documents.RowSel;
                                            if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                                            {
                                                tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                                tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                            }
                                            else
                                            {
                                                tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                                tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                            }
                                        }
                                        #endregion
                                        
                                    }
                                    lvwPages_Click(null, null);

                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Add, "RCM document(s) acknowledged.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Acknowledgement, gloAuditTrail.ActivityType.Add, "Document(s) acknowledged.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    }

                                }

                            }
                        }

                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }
                }
            }
        }

        private void DMS_UpdateUnAcknowledgedDocAndTask(string DocumentIDs)
        {
            SqlConnection con = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                con = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
                cmd = new SqlCommand("DMS_UpdateUnAcknowledgedDocAndTask", con);
                SqlParameter objParam = default(SqlParameter);

                objParam = cmd.Parameters.Add("@DocumentIDs", SqlDbType.Text);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = DocumentIDs;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                object id = cmd.ExecuteNonQuery();
                con.Close();
                objParam = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SG: Memory Leaks, disposing command variable
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }

                if ((con != null))
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Dispose(); con = null;
                }

            }
        }


        private void tsb_UnAcknowledge_Click(object sender, EventArgs e)
        {
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {

                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    if (_IsDocumentsLoading == false)
                    {
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0)
                            {

                                _DocumentIDs = _DocumentIDs.Replace("(", "").Replace(")", "");

                                SqlConnection con = default(SqlConnection);
                                SqlCommand cmd = default(SqlCommand);

                                string strQuery = null;

                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    strQuery = "UPDATE eDocument_Details_V3_RCM WITH(ROWLOCK) SET IsAcknowledge = '" + false + "' " +
                                                             " WHERE eDocumentID=" + _DocumentIDs; 
                                }
                                else
                                {
                                    strQuery = "UPDATE eDocument_Details_V3 WITH(ROWLOCK) SET IsAcknowledge = '" + false + "' " +
                                                             " WHERE eDocumentID=" + _DocumentIDs; 
                                }
                                
                
                                con = new SqlConnection(gloEDocV3Admin.gDMSDatabaseConnectionString);
                                cmd = new SqlCommand(strQuery, con);
                                cmd.CommandType = CommandType.Text;

                                con.Open();
                                object objResult = cmd.ExecuteScalar();
                                con.Close();
                                  
                                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }
                                if (con != null) { con.Dispose(); con = null; }


                                DMS_UpdateUnAcknowledgedDocAndTask(_DocumentIDs);
                                if (dMdi != null )
                                {
                                    dMdi.ShowTasks();
                                    //Bug #82464: 00000909: Task screen not opening respective screen
                                    dMdi.ShowPatientDetails();
                                }

                                if (c1Documents != null && c1Documents.Rows.Count > 0)
                                {
                                    for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                                    {
                                        int _fillrowindex = c1Documents.FindRow(oSelectedDocuments[i].DocumentID.ToString(), 0, COL_DOCUMENTID, false, true, false);
                                        c1Documents.SetData(_fillrowindex, COL_ISACKNOWLEDGE, false);
                                        c1Documents.SetCellImage(_fillrowindex, COL_FLAG_ACKNOWLEDGE, gloEDocumentV3.Properties.Resources.FlagNone );
                                    }

                                    #region "Acknowledge\Review"
                                    if (oSelectedDocuments.Count > 1)
                                    {
                                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_AcknowledgeReview;
                                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_AcknowledgeReview;
                                    }
                                    else if (oSelectedDocuments.Count == 1)
                                    {
                                        int _rowIndex = c1Documents.RowSel;
                                        if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                                        {
                                            tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                            tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                        }
                                        else
                                        {
                                            tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                            tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                        }
                                    }
                                    #endregion
                                }

                                lvwPages_Click(null, null);

                            }
                        }

                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }
                }
            }
        }


        #endregion "Dhruv 20100623 -> Acknowledgement Click"

        private void tsb_ViewAcknowledge_Click(object sender, EventArgs e)
        {

        }

        private void tsb_GenerateTask_Click(object sender, EventArgs e)
        {
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
            string _DocumentIDs = null;
            try
            {
                if (c1Documents != null)
                {
                    //if (c1Documents.RowSel > 0)
                    //{
                        oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                        if (_IsDocumentsLoading == false)
                        {
                            if (oSelectedDocuments != null)
                            {
                                if (oSelectedDocuments.Count <= 0)
                                {
                                    MessageBox.Show("Please select a document to create the task.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else if (oSelectedDocuments.Count == 1)
                                {
                                    //_SelectedDocuments.Add(oSelectedDocuments);
                                    //using (frmEDocEvent_SendRecFax ofrmSendRexFax = new frmEDocEvent_SendRecFax(oSelectedDocuments))
                                    //{
                                    //    ofrmSendRexFax.PatientID = this._PatientID;
                                    //    //ofrmSendRexFax.oSelectedDocuments = _SelectedDocuments;
                                    //    ofrmSendRexFax.ShowDialog(this);
                                    //}

                                    _DocumentIDs = _DocumentIDs.Replace("(", "").Replace(")", "");

                                    Int64 nProviderID = 0;
                                    nProviderID = Int64.Parse(gloEDocV3Admin.GetProviderDetails());

                                    Int64 DMSUser = 0;
                                    if (nProviderID > 0)
                                    {
                                        DMSUser = GetLoginProvider_DMSUser(nProviderID);
                                    }
                                    String DMSDescription = "Task for the DMS Document: " + oSelectedDocuments[0].Category.ToString () + " - " + oSelectedDocuments[0].DocumentName.ToString(); 
                                    using (gloTaskMail.frmTask ofrmTask = new gloTaskMail.frmTask(gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString, 0, 0, System.Convert.ToInt64(_DocumentIDs), gloTaskMail.TaskType.DMS))
                                    {
                                        ofrmTask.IsEMREnable = true;
                                        ofrmTask.PatientID = this._PatientID;
                                        ofrmTask.ProviderID = nProviderID;
                                        ofrmTask.DMSUser = DMSUser;
                                        ofrmTask.DMSDescription = DMSDescription;

                                        ofrmTask.ShowDialog(ofrmTask.Parent == null ? this : ofrmTask.Parent);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("DMS Task can be sent for only one document at a time. Please select a single document", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                    //}
                }
            }
            catch (Exception ex)
            {
                AuditLogErrorMessage(ex.Message.ToString());
            }
            finally
            {
                if (oSelectedDocuments != null)
                {
                    oSelectedDocuments.Dispose();
                    oSelectedDocuments = null;
                }
            }
        } //tsb_GenerateTask_Click


        private Int64 GetLoginProvider_DMSUser(Int64 nProviderID)
        {
            SqlConnection con = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                string strQuery = strQuery = "SELECT ISNULL(nOthersID,0) as  nOthersID FROM ProviderSettings Where UPPER(sSettingsType)='DMSUSER' AND nProviderID=" + nProviderID;
                
                con = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
                cmd = new SqlCommand(strQuery, con);
                cmd.CommandType = CommandType.Text ;

                con.Open();
                object objResult = cmd.ExecuteScalar();
                con.Close();
                return System.Convert.ToInt64(objResult);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                //SG: Memory Leaks, disposing command variable
                if (cmd != null) { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }

                if ((con != null))
                {
                    if (con.State == ConnectionState.Open) { con.Close(); }
                    con.Dispose(); con = null;
                }

            }
        }


        private void tsb_Scan_Click(object sender, EventArgs e)
        {
            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewer.cs at tsb_Scan_Click Start");
            gloEDocV3Management oValidateYear = new gloEDocumentV3.gloEDocV3Management();
            if (oValidateYear.ValidateScanInCurrentYear(tsb_ChangeYear.Text, "Scan") == false)
            {
                oValidateYear.Dispose();
                oValidateYear = null;
                return;
            }

            if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization || _OpenExternalSource == enum_OpenExternalSource.RCM) && (_PatientID == -1))
            {
            }
            else
            {
                if (uiPanSplitScreen != null)
                {
                    uiPanSplitScreen.Enabled = false;
                }
            }
            int _CurRow = c1Documents.RowSel;
            int _ScanCategoryID = 0;
            string _ScanCategory = "";
            string _ScanSubCategory = "";
            string _ScanYear = "";
            string _ScanMonth = "";
            Int64 _ReturnDocumentID = 0;
            Int64 _ReturnContainerID = 0;
            bool _Result = false;
            try
            {

                #region"Dhruv -> If the category Present or Not"
                bool _value = false;
                _value = gloEDocumentV3.eDocManager.eDocValidator.IsCategoryPresent(gloEDocV3Admin.gClinicID, _OpenExternalSource);
                if (_value == false)
                {
                    AddCategory();
                    _CurRow = c1Documents.RowSel;
                }
                else if (_CurRow < 0)
                {
                    MessageBox.Show("Select the category.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                #endregion

                if (_IsDocumentsLoading == false)
                {
                    if (_CurRow >= 0)
                    {
                        if (c1Documents.GetData(_CurRow, COL_COLTYPE) != null)
                        {
                            if ((enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.Document)
                            {
                                //_ScanCategory = c1Documents.Rows[_CurRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString();
                                //_ScanCategoryID = System.Convert.ToInt32(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Row.Index, COL_CATEGORYID));

                                _ScanCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_CATEGORY));
                                _ScanCategoryID = System.Convert.ToInt32(c1Documents.GetData(c1Documents.RowSel, COL_CATEGORYID));
                                _ScanSubCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.RowSel, COL_SUBCATEGORY));
                                _ReturnDocumentID = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                _ReturnContainerID = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                            }
                            else if ((enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.Category || (enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.SubCategory)
                            {
                                //_ScanCategory = c1Documents.Rows[_CurRow].Node.Data.ToString();
                                _ScanCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.Row.Index, COL_CATEGORY));
                                _ScanCategoryID = System.Convert.ToInt32(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.Row.Index, COL_CATEGORYID));
                                _ScanSubCategory = System.Convert.ToString(c1Documents.GetData(c1Documents.Rows[_CurRow].Node.Row.Index, COL_SUBCATEGORY));
                                _ReturnDocumentID = 0;
                                _ReturnContainerID = 0;
                            }

                            _ScanMonth = eDocManager.eDocValidator.GetMonthName(DateTime.Now.Month);
                            _ScanYear = _SelectedYear;
                            //Line Commented by dipak 20091002 to fix bug no:Bug 4445 :Make default document view while in Scan Docs/View Docs customizable by user.(as comment no5 in bugzilla)
                            //UnloadDocuments();

                            #region "Load Import Form"
                            frmEDocEvent_ScanNSend_PS oDocEvents = new frmEDocEvent_ScanNSend_PS();

                            oDocEvents.oPatientID = _PatientID;
                            oDocEvents.oScanInCategoryID = _ScanCategoryID;
                            oDocEvents.oScanInCategory = _ScanCategory;
                            oDocEvents.oScanInSubCategory = _ScanSubCategory;
                            oDocEvents.oScanInYear = _ScanYear;
                            oDocEvents.oScanInMonth = _ScanMonth;
                            oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                            oDocEvents._OpenExternalSource = _OpenExternalSource;

                            oDocEvents.ShowDialog(this);
                            //Reason : this new parameter added to resolve the scan focus issue
                            //Problem Bug #51056: 00000444 : Scanning
                            _Result = oDocEvents.oDialogResultIsOK;
                            if (oDocEvents.oDialogContainerID > 0 && oDocEvents.oDialogDocumentID > 0)
                            {
                                _ReturnContainerID = oDocEvents.oDialogContainerID;
                                _ReturnDocumentID = oDocEvents.oDialogDocumentID;
                                _DocumentIDForImmnunization = _ReturnDocumentID;
                                _ContainerIDForImmnunization = _ReturnContainerID;
                                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization || _OpenExternalSource == enum_OpenExternalSource.RCM) && (_PatientID == -1))
                                {
                                }
                                else
                                {
                                    if (clsSplit != null)
                                    {
                                        clsSplit.loadSplitControlData(_PatientID, VisitID, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, gloEDocV3Admin.gClinicID);
                                    }
                                }

                            }

                            oDocEvents.Dispose();
                            #endregion

                            #region "Year Settings"
                            int _MinYear = DateTime.Now.Year;
                            int _MaxYear = DateTime.Now.Year;
                            eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                            _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource);
                            _MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false,_OpenExternalSource);
                            oList.Dispose();
                            oList = null;
                            numYear.Minimum = _MinYear - 1;
                            if (_MaxYear > DateTime.Now.Year)
                            { numYear.Maximum = _MaxYear + 2; }
                            else
                            {
                                if (_MaxYear < DateTime.Now.Year)
                                { numYear.Maximum = DateTime.Now.Year + 2; }
                            }
                            if ((numYear.Value - 1) == numYear.Minimum)
                            { tsb_ChangeYearPrevious.Enabled = false; }
                            else
                            { tsb_ChangeYearPrevious.Enabled = true; }

                            if ((numYear.Value + 1) >= numYear.Maximum)
                            { tsb_ChangeYearNext.Enabled = false; }
                            else
                            { tsb_ChangeYearNext.Enabled = true; }

                            #endregion
                            if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                            {
                                tsb_ChangeYearPrevious.Enabled = false;
                                tsb_ChangeYearNext.Enabled = false;

                            }
                            #region "Refresh Events"
                            if (_Result == true)
                            {
                                if (_ReturnContainerID > 0 && _ReturnDocumentID > 0)
                                {
                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                    {
                                        tsb_Import.Enabled = false;
                                        tsb_Scan.Enabled = false;
                                        tsb_CopyDocument.Enabled = true;
                                    }
                                    _IsDocumentsLoading = true;

                                    Application.DoEvents();

                                    #region "Wait Process"
                                    if (oProcessLabel != null)
                                    {
                                        if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                                        oProcessLabel.Dispose(); oProcessLabel = null;
                                    }
                                    oProcessLabel = new Label();
                                    pnlPreview.Controls.Add(oProcessLabel);
                                    oProcessLabel.Dock = DockStyle.Fill;
                                    //oProcessLabel.Image = Properties.Resources.Wait;
                                    //oProcessLabel.ImageAlign = ContentAlignment.MiddleCenter;
                                    oProcessLabel.Location = new System.Drawing.Point(0, 0);
                                    oProcessLabel.ForeColor = Color.Blue;
                                    //oProcessLabel.ForeColor = System.Drawing.Color.FromArgb(75, 175, 253);
                                    oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                    oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                                    //oProcessLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Please wait !!!";
                                    oProcessLabel.Text = "Please wait !!!";
                                    oProcessLabel.Name = "lblProcess";
                                    oProcessLabel.Visible = true;
                                    oProcessLabel.BringToFront();
                                    #endregion

                                    Application.DoEvents();

                                    #region "Fill Documents"
                                    lvwPages.BeginUpdate();
                                    lvwPages.Items.Clear();
                                    lvwPages.EndUpdate();
                                    UnloadDocuments();//Dhruv 20100622
                                    //UnloadDocView();
                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                    {
                                        FillCategories(c1Documents, false, _ReturnDocumentID, _OpenExternalSource);
                                        tsb_ChangeYearNext.Enabled = false;
                                        tsb_ChangeYearPrevious.Enabled = false;

                                        FillDocuments(c1Documents, _SelectedYear, _PatientID, _ReturnDocumentID, _OpenExternalSource);

                                    }
                                    else
                                    {

                                        FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                                        FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);
                                    }
                                    #endregion
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewer.cs at tsb_Scan_Click at Line 3555 :Before Load Document");
                                    LoadDocument(_ReturnDocumentID, _ReturnContainerID, _OpenExternalSource);
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewer.cs at tsb_Scan_Click at Line 3557 :After Load Document");
                                    //LoadPages(_ReturnContainerID, _ReturnDocumentID, gloEDocumentAdmin.gClinicID);
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewer.cs at tsb_Scan_Click at Line 3559 :Before SelectDocumentingrid");
                                    SelectDocumentInGrid(_ReturnDocumentID, _ReturnContainerID);
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Date Time :" + DateTime.Now.ToString() + "frmEDocumentViewer.cs at tsb_Scan_Click at Line 3561 :After SelectDocumentingrid");
                                    _IsDocumentsLoading = false;

                                    #region "Enabled Buttons"
                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                    {
                                        tsb_Acknowledge.Enabled = false;

                                        tsb_InsertSign1.Enabled = false;
                                        tsb_Print.Enabled = false;
                                        tsb_Fax.Enabled = false;
                                        tsb_AddNote.Enabled = false;
                                        tsb_RotateBack.Enabled = false;
                                        tsb_RotateForward.Enabled = false;
                                        tsb_Refresh.Enabled = false;
                                        tsb_Search.Enabled = false;
                                        tsb_History.Enabled = false;

                                        tsb_Delete.Enabled = true;
                                    }
                                    else
                                    {
                                        if (tsb_Acknowledge.Enabled == false) { tsb_Acknowledge.Enabled = true; }
                                        if (tsb_Delete.Enabled == false) { tsb_Delete.Enabled = true; }
                                        if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                                        if (tsb_Print.Enabled == false) { tsb_Print.Enabled = true; }
                                        if (tsb_Fax.Enabled == false) { tsb_Fax.Enabled = true; }
                                        if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                                        if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                                        if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                                        if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                                        if (tsb_Refresh.Enabled == false) { tsb_Refresh.Enabled = true; }
                                        if (tsb_Search.Enabled == false) { tsb_Search.Enabled = true; }
                                        if (tsb_History.Enabled == false) { tsb_History.Enabled = true; }
                                    }
                                    #endregion

                                }
                            }
                            else
                            {
                                //Line Commented by dipak 20091002 to fix bug no:Bug 4445 :Make default document view while in Scan Docs/View Docs customizable by user.(as comment no5 in bugzilla)
                                // tsb_Refresh_Click(null, null);
                                SelectDocumentInGrid(_ReturnDocumentID, _ReturnContainerID);
                                LoadDocument(_ReturnDocumentID, _ReturnContainerID, _OpenExternalSource);
                            }
                            #endregion

                        }
                    }
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                {
                }
                else
                {
                    if (uiPanSplitScreen != null)
                    {
                        uiPanSplitScreen.Enabled = true;
                    }
                }

                if ((oValidateYear != null))
                {
                    oValidateYear.Dispose();
                    oValidateYear = null;
                }
            }
        }

        #region "Dhruv 20100623 -> AddCatagory"

        private void AddCategory()
        {
            if (MessageBox.Show("Category of document is not present. Do you want to add category?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (Forms.frmEDocEvent_AddCategory ofrmCategory = new Forms.frmEDocEvent_AddCategory())
                {
                    ofrmCategory.isShowAddCategory = true;
                    ofrmCategory._OpenExternalSource = _OpenExternalSource;
                    ofrmCategory.ShowDialog(this);
                    //flag isShowAddCategory is set true for to know form is open for add new category
                    //at frmEDocEvent_AddCategory_Load()-i.e. load event of form.
                    ofrmCategory.isShowAddCategory = true;
                }
                tsb_Refresh_Click(null, null);
            }
        }

        #endregion "Dhruv 20100623 -> AddCatagory"

        #region "Dhruv 20100623 -> Delete Category"

        private void tsb_Delete_Click(object sender, EventArgs e)
        {
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;

            try
            {
                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                { }
                else
                {
                    if (uiPanSplitScreen != null)
                    {
                        uiPanSplitScreen.Enabled = false;
                    }
                }

                if (c1Documents != null)
                {
                    if (c1Documents.RowSel >= 0)
                    {
                        //SG: Memory Leaks, moving declaration out of try to handle dispose in finally block
                        //DocumentContextMenu.eContextDocuments oSelectedDocuments = null;

                        string _DocumentIDs = "";
                        oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);
                        if (oSelectedDocuments != null)
                        {
                            int _CurRow = c1Documents.RowSel;
                            Int64 _ReturnDocumentID = 0;
                            Int64 _ReturnContainerID = 0;
                            Cursor.Current = Cursors.WaitCursor;
                            DialogResult oDlgResult = DialogResult.None;

                            if (_IsDocumentsLoading == false)
                            {
                                if (_CurRow >= 0)
                                {
                                    if (c1Documents.GetData(_CurRow, COL_COLTYPE) != null)
                                    {
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Delete Pages - Start" + " " + DateTime.Now.TimeOfDay);
                                        if (GetCheckedCount() >= 1 || (enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.Document)
                                        {
                                            #region " Check if operation is on Page or Document "

                                            bool _IsPageSource = true;
                                            for (int d = 0; d <= c1Documents.Rows.Count - 1; d++)
                                            {
                                                if (d >= 0)
                                                {
                                                    if (c1Documents.GetData(d, COL_COLTYPE) != null)
                                                    {
                                                        if ((Enumeration.enum_DocumentColumnType)c1Documents.GetData(d, COL_COLTYPE) == gloEDocumentV3.Enumeration.enum_DocumentColumnType.Document)
                                                        {
                                                            if (c1Documents.GetCellCheck(d, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                                                            {
                                                                _IsPageSource = false;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            #endregion " Check if operation is on Page or Document "

                                            string _MessageString = "";
                                            if (_IsPageSource == true)
                                            { _MessageString = "Are you sure you want to delete selected Page(s)?"; }
                                            else
                                            { _MessageString = "Are you sure you want to delete selected document(s)?"; }

                                            oDlgResult = MessageBox.Show(_MessageString, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

                                            if (oDlgResult == DialogResult.Yes)
                                            {

                                                _ReturnDocumentID = oSelectedDocuments[0].DocumentID;
                                                //code added by dipak 20090820 for solv problem of index outof range after right clicking on large document 
                                                //problem occure when solve Bug #2680
                                                if (oSelectedDocuments[0].Containers.Count > 0)
                                                {
                                                    _ReturnContainerID = oSelectedDocuments[0].Containers[0].ContainerID;
                                                }
                                                else
                                                {
                                                    _ReturnContainerID = 0;
                                                }



                                                #region "Enabled Buttons"
                                                //tsb_Acknowledge.Enabled = false;
                                                //tsb_Delete.Enabled = false;
                                                //tsb_InsertSign.Enabled = false;
                                                //tsb_Print.Enabled = false;
                                                //tsb_Fax.Enabled = false;
                                                //tsb_AddNote.Enabled = false;
                                                //tsb_AddTags.Enabled = false;
                                                //tsb_RotateBack.Enabled = false;
                                                //tsb_RotateForward.Enabled = false;
                                                //tsb_Refresh.Enabled = false;
                                                //tsb_Search.Enabled = false;
                                                //tsb_History.Enabled = false;
                                                #endregion

                                                UnloadDocuments();//Dhruv 20100622
                                                //UnloadDocView();

                                                _IsDocumentsLoading = true;
                                                Application.DoEvents();

                                                #region "Wait Process"
                                                if (oProcessLabel != null)
                                                {
                                                    if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                                                    oProcessLabel.Dispose(); oProcessLabel = null;
                                                }
                                                oProcessLabel = new Label();
                                                pnlPreview.Controls.Add(oProcessLabel);
                                                oProcessLabel.Dock = DockStyle.Fill;
                                                //oProcessLabel.Image = Properties.Resources.Wait;
                                                //oProcessLabel.ImageAlign = ContentAlignment.MiddleCenter;
                                                oProcessLabel.Location = new System.Drawing.Point(0, 0);
                                                oProcessLabel.ForeColor = Color.Blue;
                                                //oProcessLabel.ForeColor = System.Drawing.Color.FromArgb(75, 175, 253);
                                                oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                                oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                                                //oProcessLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Please wait !!!";
                                                oProcessLabel.Text = "Please wait !!!";
                                                oProcessLabel.Name = "lblProcess";
                                                oProcessLabel.Visible = true;
                                                oProcessLabel.BringToFront();
                                                #endregion

                                                Application.DoEvents();

                                                //eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                                                //oList.Delete(oSelectedDocuments);
                                                //oList.Dispose();
                                                eDocManager.eDocManager oManager = new gloEDocumentV3.eDocManager.eDocManager();
                                                oManager.Delete(oSelectedDocuments, _OpenExternalSource);
                                                if (oManager != null) { oManager.Dispose(); }

                                                #region "Year Settings"
                                                int _MinYear = DateTime.Now.Year;
                                                int _MaxYear = DateTime.Now.Year;
                                                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                                                _MinYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, true, true,_OpenExternalSource);
                                                _MaxYear = oList.GetLastYearOfDocuments(_PatientID, gloEDocV3Admin.gClinicID, false, false,_OpenExternalSource);
                                                oList.Dispose();
                                                oList = null; 
                                                numYear.Minimum = _MinYear - 1;
                                                numYear.Maximum = _MaxYear + 2;
                                                if (_MaxYear > DateTime.Now.Year)
                                                { numYear.Maximum = _MaxYear + 2; }
                                                else
                                                {
                                                    if (_MaxYear < DateTime.Now.Year)
                                                    { numYear.Maximum = DateTime.Now.Year + 2; }
                                                }
                                                if ((numYear.Value - 1) == numYear.Minimum)
                                                { tsb_ChangeYearPrevious.Enabled = false; }
                                                else
                                                { tsb_ChangeYearPrevious.Enabled = true; }

                                                if ((numYear.Value + 1) >= numYear.Maximum)
                                                { tsb_ChangeYearNext.Enabled = false; }
                                                else
                                                { tsb_ChangeYearNext.Enabled = true; }

                                                #endregion

                                                #region "Refresh Events"
                                                if (_ReturnContainerID > 0 && _ReturnDocumentID > 0)
                                                {
                                                    #region "Fill Documents"
                                                    lvwPages.BeginUpdate();
                                                    lvwPages.Items.Clear();
                                                    lvwPages.EndUpdate();
                                                    #endregion
                                                    #region "Enabled Buttons"
                                                    if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                                    {

                                                        tsb_ChangeYearNext.Enabled = false;
                                                        tsb_ChangeYearPrevious.Enabled = false;
                                                        tsb_Scan.Enabled = true;
                                                        tsb_Import.Enabled = true;


                                                    }
                                                    else
                                                    {
                                                        if (tsb_Acknowledge.Enabled == false) { tsb_Acknowledge.Enabled = true; }
                                                        if (tsb_Delete.Enabled == false) { tsb_Delete.Enabled = true; }
                                                        if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                                                        if (tsb_Print.Enabled == false) { tsb_Print.Enabled = true; }
                                                        if (tsb_Fax.Enabled == false) { tsb_Fax.Enabled = true; }
                                                        if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                                                        if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                                                        if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                                                        if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                                                        if (tsb_Refresh.Enabled == false) { tsb_Refresh.Enabled = true; }
                                                        if (tsb_Search.Enabled == false) { tsb_Search.Enabled = true; }
                                                        if (tsb_History.Enabled == false) { tsb_History.Enabled = true; }
                                                    }
                                                    #endregion
                                                    if (chkInSearchMode.Checked == true)
                                                    {
                                                     //   tsb_Refresh_Click(null, null);
                                                        int SelectedIndex;
                                                       // ShowHideControl(ShowHideFlag.Search, false);
                                                       // chkInSearchMode.Checked = false;
                                                        SelectedIndex = cmbZoomPercentage.SelectedIndex;

                                                        string _WhereUserTagIs = "";
                                                        string _WhereNotesIs = "";
                                                        string _WhereAcknowledgeIs = "";
                                                        string _WhereDocumentNameIs = "";
                                                        string _WhichYearIs = "";

                                                        if (chkSearch_UserTag.Checked == true) { _WhereUserTagIs = txtSearch_UserTag.Text.Trim(); }
                                                        if (chkSearch_Notes.Checked == true) { _WhereNotesIs = txtSearch_Notes.Text.Trim(); }
                                                        if (chkSearch_Acknowledge.Checked == true) { _WhereAcknowledgeIs = txtSearch_Acknowledge.Text.Trim(); }
                                                        if (chkSearch_DocumentName.Checked == true) { _WhereDocumentNameIs = txtSearch_DocumentName.Text.Trim(); }

                                                        if (cmbSearchYear.Items.Count > 0)
                                                        {
                                                            if (cmbSearchYear.SelectedValue.ToString() != "All")
                                                            { _WhichYearIs = cmbSearchYear.SelectedValue.ToString(); }
                                                        }
                                                        FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                                                        //FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);
                                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                        {
                                                            FillFilteredDocuments_RCM(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                                                        }
                                                        else
                                                        {
                                                            FillFilteredDocuments(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                                                        }

                                                        Int64 _docid = 0;
                                                        Int64 _contid = 0;

                                                        GetFirstDocumentInGrid(out _docid, out _contid);
                                                        //##########

                                                        if (oProcessLabel != null)
                                                        {
                                                            if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                                                            oProcessLabel.Dispose(); oProcessLabel = null;
                                                        }

                                                        Cursor.Current = Cursors.Default;

                                                        _IsDocumentsLoading = false;
                                                        c1Documents.Enabled = true;

                                                        //##########

                                                        cmbZoomPercentage.SelectedIndex = SelectedIndex;
                                                        lblDocumentsHeader.Text = slblDocumentHeader;
                                                    }
                                                    else
                                                    {
                                                        if (oSelectedDocuments.Count >= 1)
                                                        {
                                                            //Resolved Bug # : 38886
                                                            if (_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                                                            {
                                                                FillCategories(c1Documents, false, _DocumentIDForImmnunization, _OpenExternalSource);
                                                                FillDocuments(c1Documents, _SelectedYear, _PatientID, _DocumentIDForImmnunization, _OpenExternalSource);

                                                            }
                                                            else
                                                            {
                                                                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                                                                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);
                                                            }
                                                            Int64 _docid = 0;
                                                            Int64 _contid = 0;
                                                            if (oSelectedDocuments.Count == 1)
                                                            {
                                                                if (eDocManager.eDocValidator.IsDocumentExist(oSelectedDocuments[0].DocumentID, _PatientID, gloEDocV3Admin.gClinicID, _OpenExternalSource))
                                                                {
                                                                    _docid = oSelectedDocuments[0].DocumentID;
                                                                    _contid = oSelectedDocuments[0].Containers[0].ContainerID;
                                                                }
                                                                else
                                                                {
                                                                    GetFirstDocumentInGrid(out _docid, out _contid);
                                                                }
                                                                LoadDocument(_docid, _contid, _OpenExternalSource);
                                                            }
                                                            else
                                                            {
                                                                _docid = 0;
                                                                _contid = 0;
                                                                GetFirstDocumentInGrid(out _docid, out _contid);
                                                                LoadDocument(_docid, _contid, _OpenExternalSource);
                                                            }

                                                        }
                                                    }
                                         
                                                    SelectDocumentInGrid(_ReturnDocumentID, _ReturnContainerID);
                                                    _IsDocumentsLoading = false;



                                                }
                                                #endregion
                                                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                                                {
                                                }
                                                else
                                                {
                                                    if (clsSplit != null)
                                                    {
                                                        clsSplit.loadSplitControlData(_PatientID, VisitID, uiPanSplitScreen.SelectedPanel.Name, objCriteria, objWord, gloEDocV3Admin.gClinicID);
                                                    }
                                                }

                                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                {
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.Delete, "RCM document(s) deleted.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                                }
                                                else
                                                {
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.ModifyDocument, gloAuditTrail.ActivityType.Delete, "Document(s) deleted", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                                }
                                            }

                                        }
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Delete Pages - END" + " " + DateTime.Now.TimeOfDay);
                                    }
                                }
                            }
                            Cursor.Current = Cursors.Default;
                        }//oSelectedDocuments Checking is null
                    }
                }//c1Document is null
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SG: Momory Leaks, disposing temporary varible
                if (oSelectedDocuments != null) { oSelectedDocuments.Clear(); oSelectedDocuments.Dispose(); oSelectedDocuments = null; }

                if ((_OpenExternalSource == gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization) && (_PatientID == -1))
                { }
                else
                {
                    if (uiPanSplitScreen != null) { uiPanSplitScreen.Enabled = true; }
                }
            }
        }


        #endregion "Dhruv 20100623 -> Delete Category"

        public Int64 GetVisitID(System.DateTime VisitDate, long PatientID = 0)
        {
            SqlConnection con = default(SqlConnection);
            SqlCommand cmd = default(SqlCommand);
            try
            {
                //Call InitialzeCon()
                con = new SqlConnection(gloEDocV3Admin.gDatabaseConnectionString);
                cmd = new SqlCommand("gsp_GetVisitID", con);
                SqlParameter objParam = default(SqlParameter);
                objParam = cmd.Parameters.Add("@VisitDate", SqlDbType.DateTime);
                objParam.Direction = ParameterDirection.Input;
                objParam.Value = VisitDate;

                objParam = cmd.Parameters.Add("@PatientID", SqlDbType.BigInt, 18);
                objParam.Direction = ParameterDirection.Input;
                //Lines commented by dipak 20100007 as we not using gnPatientID in local scope.
                //If PatientID = 0 Then
                //    PatientID = gnPatientID
                //End If
                objParam.Value = PatientID;

                cmd.CommandType = CommandType.StoredProcedure;

                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                Int64 VisitID = 0;
                object id;
                id = cmd.ExecuteScalar();
                if (id != null)
                {
                    VisitID = System.Convert.ToInt64(id);
                }
                if (objParam != null)
                {
                    objParam = null;
                }
                con.Close();

                return VisitID;

            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                //SG: Memory Leaks, disposing command variable
                if (cmd != null)
                { cmd.Parameters.Clear(); cmd.Dispose(); cmd = null; }

                if ((con != null))
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                    con.Dispose();
                    con = null;
                }

            }
        }

        private void tsb_DeletePage_Click(object sender, EventArgs e)
        {

        }


        #region "Dhruv 20100623 -> Print Click"

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            foreach (Form ofrm in Application.OpenForms)
            {
                if (ofrm.Name == "gloPrintProgressController")
                {
                    DialogResult dg = MessageBox.Show("Printing is in progress. Do you want to cancel the printing?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dg == DialogResult.Yes)
                    {
                        ofrm.Close();


                        break;
                    }
                    else
                    {
                       
                        return;
                      
                    }
                }
            }
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {
                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    if (_IsDocumentsLoading == false)
                    {
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0)
                            {
                                bool _Result = false;
                                using (frmEDocEvent_Print oDocEvents = new frmEDocEvent_Print())
                                {
                                    //Developer: Mitesh Patel
                                    //Date:28-Jan-2012'
                                    //Bug ID: 15961
                                    _SaveResult = true;
                                    lvwPages_Click(null, null);

                                    if (_SaveResult != false)
                                    {
                                        oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        //oDocEvents.PrintDocuments();
                                        oDocEvents._OpenExternalSource = _OpenExternalSource;
                                        oDocEvents.ShowDialog(this);
                                        _Result = oDocEvents.oDialogResultIsOK;
                                        if (_Result)
                                        {
                                            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.PrintDocument, gloAuditTrail.ActivityType.Print, "RCM document(s) printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                            }
                                            else
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.PrintDocument, gloAuditTrail.ActivityType.Print, "Document(s) printed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                            }
                                        }
                                    }

                                }
                                ////oDocEvents.Dispose();

                                //if (_Result == true)
                                //{
                                //    lvwPages_Click(null, null);
                                //}
                            }
                        }

                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }
                }
            }
        }

        #endregion "Dhruv 20100623 -> Print Click"

        private void tsb_PrintAll_Click(object sender, EventArgs e)
        {

        }

        #region "Dhruv 20100623 -> FaxClick"

        private void tsb_Fax_Click(object sender, EventArgs e)
        {
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {
                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    if (_IsDocumentsLoading == false)
                    {
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0)
                            {
                                if (oSelectedDocuments != null)
                                {
                                    if (oSelectedDocuments.Count > 0)
                                    {
                                        bool _Result = false;
                                        using (frmEDocEvent_Fax oDocEvents = new frmEDocEvent_Fax())
                                        {
                                         //Added code for Bug #76177: 00000809: Fax does not contain anotation while faxing
                                           _SaveResult = true;
                                           lvwPages_Click(null, null);
                                           if (_SaveResult != false)
                                           {
                                                oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                                                oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                                oDocEvents.PatientID = _PatientID;
                                                oDocEvents._OpenExternalSource = _OpenExternalSource;
                                                oDocEvents.ShowDialog(this);
                                                _Result = oDocEvents.oDialogResultIsOK;
                                                if (_Result)
                                                {
                                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                                    {
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, "RCM document(s) faxed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                                    }
                                                    else
                                                    {
                                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Fax, gloAuditTrail.ActivityType.Fax, "Document(s) faxed.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                                    }
                                                }
                                           }
                                        }
                                        //oDocEvents.Dispose();

                                        //if (_Result == true)
                                        //{
                                        //    lvwPages_Click(null, null);
                                        //}

                                    }
                                }

                            }//end - if (oSelectedDocuments.Count > 0) 

                        }//end - if (oSelectedDocuments != null)
                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }
                    Cursor.Current = Cursors.Default;
                    //Resolved issue #88002-gloEMR: Scan Docs- As user click on fax and close button, application take focus to exam window
                    this.Focus();
                }
            }
        }
        #endregion "Dhruv 20100623 -> FaxClick"


        private void tsb_FaxAll_Click(object sender, EventArgs e)
        {

        }

        #region "Dhruv 20100623 -> AddnotesClick"

        private void tsb_AddNote_Click(object sender, EventArgs e)
        {
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {
                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    if (_IsDocumentsLoading == false)
                    {
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0
                                && oSelectedDocuments[0].Containers != null && oSelectedDocuments[0].Containers.Count > 0
                                && oSelectedDocuments[0].Containers[0].Pages != null && oSelectedDocuments[0].Containers[0].Pages.Count > 0)
                            {
                                bool _Result = false;
                                using (frmEDocEvent_AddNote oDocEvents = new frmEDocEvent_AddNote())
                                {

                                    oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                                    oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                    oDocEvents._OpenExternalSource = _OpenExternalSource;
                                    oDocEvents.ShowDialog(this);
                                    _Result = oDocEvents.oDialogResultIsOK;
                                }
                                //oDocEvents.Dispose();

                                if (_Result == true)
                                {
                                    if (c1Documents != null && c1Documents.Rows.Count > 0)
                                    {
                                        for (int i = 0; i <= oSelectedDocuments.Count - 1; i++)
                                        {
                                            int _fillrowindex = c1Documents.FindRow(oSelectedDocuments[i].DocumentID.ToString(), 0, COL_DOCUMENTID, false, true, false);
                                            c1Documents.SetData(_fillrowindex, COL_HASNOTES, true);
                                            c1Documents.SetCellImage(_fillrowindex, COL_FLAG_NOTES, gloEDocumentV3.Properties.Resources.FlagNote);
                                        }
                                    }
                                    if (oSelectedDocuments != null && oSelectedDocuments.Count > 0)
                                    {
                                        LoadPages(oSelectedDocuments[0].DocumentID, gloEDocV3Admin.gClinicID);
                                    }

                                    lvwPages_Click(null, null);

                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Note added for RCM document.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Note added for Scan document.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    }

                                }

                            }
                            else
                            { MessageBox.Show("Select Page(s) to add notes.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        }

                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }
                }
            }
        }
        #endregion "Dhruv 20100623 -> AddnotesClick"


        #region "Dhruv 20100623 -> AddTags"

        private void tsb_AddTags_Click(object sender, EventArgs e)
        {
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {
                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    if (_IsDocumentsLoading == false)
                    {
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0
                                && oSelectedDocuments[0].Containers != null && oSelectedDocuments[0].Containers.Count > 0
                                && oSelectedDocuments[0].Containers[0].Pages != null && oSelectedDocuments[0].Containers[0].Pages.Count > 0)
                            {
                                bool _Result = false;
                                using (frmEDocEvent_AddUserTag oDocEvents = new frmEDocEvent_AddUserTag())
                                {

                                    oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                                    oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                    oDocEvents._OpenExternalSource = _OpenExternalSource;
                                    oDocEvents.ShowDialog(this);
                                    _Result = oDocEvents.oDialogResultIsOK;
                                }
                                //oDocEvents.Dispose();

                                if (_Result == true)
                                {
                                    if (oSelectedDocuments != null && oSelectedDocuments.Count > 0)
                                    {
                                        LoadPages(oSelectedDocuments[0].DocumentID, gloEDocV3Admin.gClinicID);
                                    }
                                    lvwPages_Click(null, null);

                                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Tag added for RCM document.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    }
                                    else
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.Add, "Tag added for Scan document.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                    }

                                }

                            }
                            else
                            { MessageBox.Show("Select Page(s) to add user tag.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                        }

                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }
                }
            }
        }

        #endregion "Dhruv 20100623 -> AddTags"

        #region "Dhruv 20100623 -> RotateBackClick"

        private void tsb_RotateBack_Click(object sender, EventArgs e)
        {
            string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            int _ConPageNumber = 0;
            tsb_RotateBack.Enabled = false;
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            #region "Container and Document Information"
                            if (c1Documents != null)
                            {
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            }
                            #endregion

                            #region "Rotation on Doc Object"
                            pdftron.PDF.Page.Rotate oCurRotate = oPDFView.GetDoc().GetPage(_ConPageNumber).GetRotation();
                            // if (oCurRotate != null)
                            {
                                if (oPDFView != null)
                                {
                                    if (oCurRotate == Page.Rotate.e_0)
                                    {
                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_270);
                                    }
                                    else if (oCurRotate == Page.Rotate.e_90)
                                    {

                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_0);
                                    }
                                    else if (oCurRotate == Page.Rotate.e_180)
                                    {
                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_90);
                                    }
                                    else if (oCurRotate == Page.Rotate.e_270)
                                    {
                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_180);
                                    }
                                }
                            }
                            #endregion


                            #region "Generate File to store into Database"
                            //code added by dipak 20091007 as temp folder not find while rotating page from view document
                            string _FolderPath;
                            _FolderPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString();
                            try
                            {
                                if (_FolderPath != "")
                                {
                                    if (System.IO.Directory.Exists(_FolderPath) == false)
                                    {
                                        System.IO.Directory.CreateDirectory(_FolderPath);
                                        if (System.IO.Directory.Exists(_FolderPath) == false)
                                        {
                                            _ErrorMessage = "Directory does not exists. " + _FolderPath;
                                            AuditLogErrorMessage(_ErrorMessage);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                            }
                            //End dipak 20091007

                            _FileFullPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString() + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";
                            if (_FileFullPath != "")
                            {
                                if (oPDFDoc != null)
                                {
                                    oPDFDoc.Save(_FileFullPath, 0);
                                }
                            }
                            if (oPDFView != null)
                            {
                                if (oPDFView.GetPageCount() > 1)
                                {
                                    if (oPDFView.GetCurrentPage() == oPDFView.GetDoc().GetPageCount())
                                    {
                                        oPDFView.GotoFirstPage();
                                    }
                                    else if (oPDFView.GetCurrentPage() == 1)
                                    {
                                        oPDFView.GotoLastPage();
                                    }
                                    else
                                    {
                                        oPDFView.GotoFirstPage();
                                    }
                                }


                                oPDFView.SetCurrentPage(_ConPageNumber);
                                oPDFView.Update();

                                oPDFView.Refresh();
                            }
                            #endregion


                            //Aniket: Added following condition to resolve error when no page is selected
                            //Version: 6052, BugID: 15612

                            if (_DocumentId != 0 && _ContainerId != 0)
                            {
                                using (gloEDocumentV3.eDocManager.eDocManager DocManager = new gloEDocumentV3.eDocManager.eDocManager())
                                {
                                    bool _retVal = DocManager.UpdateContainer(_DocumentId, _ContainerId, _FileFullPath, true, _OpenExternalSource);
                                    //DocManager.Dispose();
                                }
                            }

                            //lines added by dipak 20091007 to solve problem of rotate page of single page document.
                            if (oPDFView != null)
                            {
                                if (oPDFView.GetPageCount() == 1)
                                {
                                    LoadDocument(_DocumentId, _ContainerId, _OpenExternalSource);
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                tsb_RotateBack.Enabled = true;
            }
        }

        #endregion "Dhruv 20100623 -> RotateBackClick"

        #region "Dhruv 20100623 -> RotateForwardClick "

        private void tsb_RotateForward_Click(object sender, EventArgs e)
        {
            string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            tsb_RotateForward.Enabled = false;
            int _ConPageNumber = 0;
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Rotate  - Start" + " " + DateTime.Now.TimeOfDay);
                            #region "Container and Document Information"
                            if (c1Documents != null)
                            {
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            }

                            #endregion

                            #region "Rotation on Doc Object"
                            pdftron.PDF.Page.Rotate oCurRotate = oPDFView.GetDoc().GetPage(_ConPageNumber).GetRotation();
                            //   if (oCurRotate != null)
                            {
                                if (oPDFView != null)
                                {
                                    if (oCurRotate == Page.Rotate.e_0)
                                    {
                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_90);
                                    }
                                    else if (oCurRotate == Page.Rotate.e_90)
                                    {

                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_180);
                                    }
                                    else if (oCurRotate == Page.Rotate.e_180)
                                    {
                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_270);
                                    }
                                    else if (oCurRotate == Page.Rotate.e_270)
                                    {
                                        oPDFView.GetDoc().GetPage(_ConPageNumber).SetRotation(Page.Rotate.e_0);
                                    }
                                }
                            }
                            #endregion

                            #region "Generate File to store into Database"
                            //code added by dipak 20091007 as temp folder not find while rotate from view document
                            string _FolderPath;
                            _FolderPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString();
                            try
                            {
                                if (_FolderPath != null)
                                {
                                    if (System.IO.Directory.Exists(_FolderPath) == false)
                                    {
                                        System.IO.Directory.CreateDirectory(_FolderPath);
                                        if (System.IO.Directory.Exists(_FolderPath) == false)
                                        {
                                            _ErrorMessage = "Directory does not exists. " + _FolderPath;
                                            AuditLogErrorMessage(_ErrorMessage);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                            }
                            //End dipak 20091007
                            _FileFullPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString() + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";
                            if (_FileFullPath != "")
                            {
                                if (oPDFDoc != null)
                                {
                                    oPDFDoc.Save(_FileFullPath, 0);
                                }
                            }
                            if (oPDFView != null)
                            {
                                if (oPDFView.GetPageCount() > 1)
                                {
                                    if (oPDFView.GetCurrentPage() == oPDFView.GetDoc().GetPageCount())
                                    {
                                        oPDFView.GotoFirstPage();
                                    }
                                    else if (oPDFView.GetCurrentPage() == 1)
                                    {
                                        oPDFView.GotoLastPage();
                                    }
                                    else
                                    {
                                        oPDFView.GotoFirstPage();
                                    }
                                    oPDFView.GotoFirstPage();
                                    oPDFView.SetCurrentPage(_ConPageNumber);
                                    oPDFView.Update();
                                }
                                else
                                    oPDFView.Refresh();
                            }
                            #endregion

                            //Aniket: Added following condition to resolve error when no page is selected
                            //Version: 6052, BugID: 15612

                            if (_DocumentId != 0 && _ContainerId != 0)
                            {

                                using (gloEDocumentV3.eDocManager.eDocManager DocManager = new gloEDocumentV3.eDocManager.eDocManager())
                                {
                                    bool _retVal = DocManager.UpdateContainer(_DocumentId, _ContainerId, _FileFullPath, true, _OpenExternalSource);
                                }
                            }
                            //DocManager.Dispose();
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Rotate - End" + " " + DateTime.Now.TimeOfDay);

                            //lines added by dipak 20091007 to solve problem of rotating page of single page document.
                            if (oPDFView != null)
                            {
                                if (oPDFView.GetPageCount() == 1)
                                {
                                    LoadDocument(_DocumentId, _ContainerId, _OpenExternalSource);
                                }
                            }
                            //end added by dipak 20091007
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            finally
            {
                tsb_RotateForward.Enabled = true;
            }
        }
        #endregion "Dhruv 20100623 -> RotateForwardClick "

        private void tsb_Refresh_Click(object sender, EventArgs e)
        {
            //code added by dipak 20090821 
            //variables added for store documentid and container id return by function GetSelectedDocumentInGrid ()
            Int64 _docid = 0;
            Int64 _contid = 0;
            GetSelectedDocumentInGrid(out _docid, out _contid);
            _IsDocumentsLoading = true;
            Application.DoEvents();
            #region "Show/Hide Controls"
            ShowHideControl(ShowHideFlag.Opearations, false);
            ShowHideControl(ShowHideFlag.Tags, false);

            if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            { ShowHideControl(ShowHideFlag.Patient, false); }
            else
            { ShowHideControl(ShowHideFlag.Patient, true); }

            ShowHideControl(ShowHideFlag.Legend, false);
            ShowHideControl(ShowHideFlag.Search, false);
            tsb_Close.Enabled = false;
            btnPat_Up_Click(null, null);
            btnNote_Down_Click(null, null);
            if (_OpenEDocumentAs != enum_OpenEDocumentAs.ViewDocumentForExternalModule)
            {
                btn_Right_Click(null, null);
            }
            //Code Added by dipak 20091009 to SetDMSPagePriviewSetting in order to fix bug 
            //No:4445 ->Make default document view while in Scan Docs/View Docs customizable by user.
            SetDMSPagePreviewSetting();

            tsb_Acknowledge.Enabled = true;

            #endregion

            //numYear.Value = DateTime.Now.Year;
            tsb_ChangeYear.Text = numYear.Value.ToString();

            if (chkInSearchMode.Checked == false)
            {
                ShowHideControl(ShowHideFlag.Search, false);
                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);
            }
            else
            {
                ShowHideControl(ShowHideFlag.Search, true);
                //Searching Text
                string _WhereUserTagIs = "";
                string _WhereNotesIs = "";
                string _WhereAcknowledgeIs = "";
                string _WhereDocumentNameIs = "";
                string _WhichYearIs = "";

                if (chkSearch_UserTag.Checked == true) { _WhereUserTagIs = txtSearch_UserTag.Text.Trim(); }
                if (chkSearch_Notes.Checked == true) { _WhereNotesIs = txtSearch_Notes.Text.Trim(); }
                if (chkSearch_Acknowledge.Checked == true) { _WhereAcknowledgeIs = txtSearch_Acknowledge.Text.Trim(); }
                if (chkSearch_DocumentName.Checked == true) { _WhereDocumentNameIs = txtSearch_DocumentName.Text.Trim(); }

                if (cmbSearchYear.Items.Count > 0)
                {
                    if (cmbSearchYear.SelectedValue.ToString() != "All")
                    {
                        _WhichYearIs = cmbSearchYear.SelectedValue.ToString();
                    }
                }

                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);

                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                {
                    FillFilteredDocuments_RCM(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                }
                else
                {
                    FillFilteredDocuments(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                }
            }

            //GetSelectedDocumentInGrid(out _docid, out _contid);

            //if (_OpenExternalSource == enum_OpenExternalSource.RCM)
            //{
            //    ShowHideAckDocuments(flgShowAckDocs);
            //}
            //code added by dipak 20090821 to select the document which was selected before refresh
            SelectDocumentInGrid(_docid, _contid);
            //end add
            if (lvwPages.Items.Count > 0)
            { lvwPages.Cursor = Cursors.Hand; }
            else
            { lvwPages.Cursor = Cursors.Default; }
            #region "Show/Hide Menus"
            if (_OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {

                tsb_Import.Enabled = true;
                //tsb_ChangeYearPrevious.Enabled = true;
                //tsb_ChangeYearNext.Enabled = true;
                tsb_Acknowledge.Enabled = true;
                tsb_ViewAcknowledge.Enabled = true;
                tsb_Scan.Enabled = true;
                tsb_Delete.Enabled = true;
                tsb_DeletePage.Enabled = true;
                tsb_InsertSign1.Enabled = true;
                tsb_Print.Enabled = true;
                tsb_PrintAll.Enabled = true;
                tsb_Fax.Enabled = true;
                tsb_FaxAll.Enabled = true;
                tsb_AddNote.Enabled = true;
                tsb_AddTags.Enabled = true;
                tsb_RotateBack.Enabled = true;
                tsb_RotateForward.Enabled = true;
                tsb_Refresh.Enabled = true;
                tsb_History.Enabled = true;
                tsb_Search.Enabled = true;
            }
            else if (_OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocument)
            {
                eDocInstance.tsb_Import.Enabled = false;
                //eDocInstance.tsb_ChangeYearPrevious.Enabled = true;
                //eDocInstance.tsb_ChangeYearNext.Enabled = true;
                eDocInstance.tsb_Acknowledge.Enabled = true;
                eDocInstance.tsb_ViewAcknowledge.Enabled = true;
                eDocInstance.tsb_Scan.Enabled = false;
                if (_OpenExternalSource == enum_OpenExternalSource.Immunization)
                {
                    eDocInstance.tsb_Delete.Enabled = true;
                    eDocInstance.tsb_DeletePage.Enabled = true;
                }
                else
                {
                    eDocInstance.tsb_Delete.Enabled = false;
                    eDocInstance.tsb_DeletePage.Enabled = false;
                }
                eDocInstance.tsb_InsertSign1.Enabled = false;
                eDocInstance.tsb_Print.Enabled = true;
                eDocInstance.tsb_PrintAll.Enabled = true;
                eDocInstance.tsb_Fax.Enabled = true;
                eDocInstance.tsb_FaxAll.Enabled = true;
                eDocInstance.tsb_AddNote.Enabled = true;
                eDocInstance.tsb_AddTags.Enabled = true;
                eDocInstance.tsb_RotateBack.Enabled = true;
                eDocInstance.tsb_RotateForward.Enabled = true;
                eDocInstance.tsb_Refresh.Enabled = true;
                eDocInstance.tsb_History.Enabled = true;
                eDocInstance.tsb_Search.Enabled = true;
            }
            else if (_OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule)
            {
                eDocInstance.tsb_Import.Enabled = false;
                //eDocInstance.tsb_ChangeYearPrevious.Enabled = true;
                //eDocInstance.tsb_ChangeYearNext.Enabled = true;
                eDocInstance.tsb_Acknowledge.Enabled = true;
                eDocInstance.tsb_ViewAcknowledge.Enabled = true;
                eDocInstance.tsb_Scan.Enabled = false;
                eDocInstance.tsb_Delete.Enabled = false;
                eDocInstance.tsb_DeletePage.Enabled = false;
                eDocInstance.tsb_InsertSign1.Enabled = false;
                eDocInstance.tsb_Print.Enabled = true;
                eDocInstance.tsb_PrintAll.Enabled = true;
                eDocInstance.tsb_Fax.Enabled = true;
                eDocInstance.tsb_FaxAll.Enabled = true;
                eDocInstance.tsb_AddNote.Enabled = true;
                eDocInstance.tsb_AddTags.Enabled = true;
                //eDocInstance.tsb_RotateBack.Enabled = false;
                //eDocInstance.tsb_RotateForward.Enabled = false;
                eDocInstance.tsb_RotateBack.Enabled = true;
                eDocInstance.tsb_RotateForward.Enabled = true;
                eDocInstance.tsb_Refresh.Enabled = true;
                eDocInstance.tsb_History.Enabled = true;
                eDocInstance.tsb_Search.Enabled = false;
            }
            #endregion

            _IsDocumentsLoading = false;
            tsb_Close.Enabled = true;

            //// Check Patient has Archive Docuemnt 
            eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
            if (oList.GetArchiveInformation(_PatientID,_OpenExternalSource) == true)
            {
                tsb_Archive.Visible = true;
                tls_MaintainDoc.ButtonsToHide.Remove(tsb_Archive.Name.ToString());
            }
            else
            {
                tsb_Archive.Visible = false;
                if (tls_MaintainDoc.ButtonsToHide.Contains(tsb_Archive.Name.ToString()) == false)
                    tls_MaintainDoc.ButtonsToHide.Add(tsb_Archive.Name.ToString());
            }

            //SG: Memory Leaks, disposing oList object
            if (oList != null) { oList.Dispose(); oList = null; }

            ///////////////
        }

        #region "Edit Pdf doc"

        private void tsb_EditDoc_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region "Search"
        private void tsb_Search_Click(object sender, EventArgs e)
        {
            ShowHideControl(ShowHideFlag.Search, true);
            chkSearch_Acknowledge.Checked = false;
            chkSearch_Notes.Checked = false;
            chkSearch_UserTag.Checked = true;
            chkSearch_PageName.Checked = false;
            chkSearch_DocumentName.Checked = false;
            txtSearch_Acknowledge.Text = "";
            txtSearch_Notes.Text = "";
            txtSearch_UserTag.Text = "";
            txtSearch_PageName.Text = "";
            txtSearch_DocumentName.Text = "";

            if (pnlDocumentsLegends.Visible == false)
            {
                pnlDocumentsLegends.Visible = true;
                pnlSmallStrip.Visible = false;
            }

            #region " Fill Search Year DropDown "
            try
            {
                dtSearchYear = GetYearsForSearch();

                if (dtSearchYear != null && dtSearchYear.Rows.Count > 0)
                {
                    DataRow dr = dtSearchYear.NewRow();
                    dr["sYear"] = "All";
                    dtSearchYear.Rows.InsertAt(dr, 0);

                    cmbSearchYear.BeginUpdate();
                    cmbSearchYear.DataSource = dtSearchYear.Copy();
                    cmbSearchYear.ValueMember = dtSearchYear.Columns["sYear"].ColumnName;
                    cmbSearchYear.DisplayMember = dtSearchYear.Columns["sYear"].ColumnName;
                    cmbSearchYear.EndUpdate();
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }


            #endregion
        }

        private void tsb_Search_Search_Click(object sender, EventArgs e)
        {
            chkInSearchMode.Checked = true;
            _IsDocumentsLoading = true;

            //Searching Text
            string _WhereUserTagIs = "";
            string _WhereNotesIs = "";
            string _WhereAcknowledgeIs = "";
            string _WhereDocumentNameIs = "";
            string _WhichYearIs = "";
          
            try
            {
                if (chkSearch_UserTag.Checked == true) { _WhereUserTagIs = txtSearch_UserTag.Text.Trim(); }
                if (chkSearch_Notes.Checked == true) { _WhereNotesIs = txtSearch_Notes.Text.Trim(); }
                if (chkSearch_Acknowledge.Checked == true) { _WhereAcknowledgeIs = txtSearch_Acknowledge.Text.Trim(); }
                if (chkSearch_DocumentName.Checked == true) { _WhereDocumentNameIs = txtSearch_DocumentName.Text.Trim(); }

                // DropDown Value
                if (cmbSearchYear.Items.Count > 0)
                {
                    if (cmbSearchYear.SelectedValue.ToString() != "All")
                    {
                        _WhichYearIs = cmbSearchYear.SelectedValue.ToString();
                    }
                }

                // slblDocumentHeader = lblDocumentsHeader.Text;

                lblDocumentsHeader.Text = cmbSearchYear.SelectedValue.ToString() + " Documents";

                if (c1Documents != null)
                {

                    FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        FillFilteredDocuments_RCM(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                    }
                    else
                    {
                        FillFilteredDocuments(c1Documents, _SelectedYear, _PatientID, _WhereUserTagIs, _WhereNotesIs, _WhereAcknowledgeIs, _WhereDocumentNameIs, _WhichYearIs);
                    }
                }
                #region "Load First Document"
                if (lvwPages != null)
                {
                    lvwPages.Items.Clear();
                    txtNotes.Text = "";
                    Int64 _docid = 0;
                    Int64 _contid = 0;
                    GetFirstDocumentInGrid(out _docid, out _contid);
    

                    if (oProcessLabel != null)
                    {
                        if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                        oProcessLabel.Dispose(); oProcessLabel = null;
                    }

                    Cursor.Current = Cursors.Default;

                    _IsDocumentsLoading = false;
                    c1Documents.Enabled = true;

                    
                    if (lvwPages.Items.Count > 0)
                    { lvwPages.Cursor = Cursors.Hand; }
                    else
                    { lvwPages.Cursor = Cursors.Default; }
                }
                #endregion
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }

        private void tsb_Search_Close_Click(object sender, EventArgs e)
        {
            int SelectedIndex;
            ShowHideControl(ShowHideFlag.Search, false);
            chkInSearchMode.Checked = false;
            SelectedIndex = cmbZoomPercentage.SelectedIndex;
            //tsb_Refresh_Click(null, null);

            try
            {
                FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
                FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

                Int64 _docid = 0;
                Int64 _contid = 0;
                //GetSelectedDocumentInGrid(out _docid, out _contid);
                //SelectDocumentInGrid(_docid, _contid);

                GetFirstDocumentInGrid(out _docid, out _contid);

                cmbZoomPercentage.SelectedIndex = SelectedIndex;
                lblDocumentsHeader.Text = slblDocumentHeader;
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }

        #endregion

        private void tsb_Migration_Click(object sender, EventArgs e)
        {

        }



        #region "Dhruv 20100623 -> HistroyClick"

        #endregion "Dhruv 20100623 -> HistroyClick"

        private void tsb_History_Click(object sender, EventArgs e)
        {
            if (c1Documents != null)
            {
                if (c1Documents.RowSel >= 0)
                {
                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);

                    try
                    {
                        if (_IsDocumentsLoading == false)
                        {
                            if (oSelectedDocuments != null)
                            {
                                if (oSelectedDocuments.Count > 0)
                                {
                                    if (oNoteToolTip != null)
                                    {
                                        oNoteToolTip.RemoveAll();
                                    }
                                    bool _Result = false;
                                    using (frmEDocEvent_History oDocEvents = new frmEDocEvent_History())
                                    {

                                        oDocEvents.oClinicID = gloEDocV3Admin.gClinicID;
                                        oDocEvents.oSelectedDocuments = oSelectedDocuments;
                                        oDocEvents.StartPosition = FormStartPosition.CenterScreen;
                                        oDocEvents._OpenExternalSource = _OpenExternalSource;
                                        oDocEvents.ShowDialog(this);
                                        _Result = oDocEvents.oDialogResultIsOK;
                                    }
                                    //                                    oDocEvents.Dispose();

                                    if (_Result == true)
                                    {
                                        lvwPages_Click(null, null);

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Amendments, gloAuditTrail.ActivityType.View, "Amendments viewed for RCM document.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }
                                        else
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Amendments, gloAuditTrail.ActivityType.View, "Amendments viewed for Scan document.", _PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                        }

                                    }

                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                    }
                    finally
                    {
                        if (oSelectedDocuments != null)
                        {
                            oSelectedDocuments.Dispose();
                            oSelectedDocuments = null;
                        }

                    }
                }
            }
        }

        private void tsb_Save_Click(object sender, EventArgs e)
        {
            AnnotationSaving1();
        }


        private void tsb_Close_Click(object sender, EventArgs e)
        {
            //_isCloseSave = true;

            int result = 0;

            bool goAhead = (oProcessLabel == null);
            if (!goAhead)
            {
                goAhead = (pnlPreview.Controls.Contains(oProcessLabel) == false);
            }

            if (goAhead)
            {

                result = AnnotationSaving();
                if (result == 0)
                {
                    UnloadDocuments();
                    //UnloadDocView();
                    this.Close();
                }
                else if (result == 1)
                {
                    // UnloadDocView();
                    UnloadDocuments();
                    this.Close();
                }

            }

        }

        private void InitializeToolStrip()
        {
            tls_MaintainDoc.ConnectionString = gloEDocV3Admin.gDatabaseConnectionString;
            tls_MaintainDoc.ModuleName = this.Name.ToString();
            tls_MaintainDoc.UserID = gloEDocV3Admin.gUserID;
            tls_MaintainDoc.ButtonsToHide.Add(tsb_DeletePage.Name.ToString());
            tls_MaintainDoc.ButtonsToHide.Add(tsb_PrintAll.Name.ToString());
            tls_MaintainDoc.ButtonsToHide.Add(tsb_FaxAll.Name.ToString());
            //tls_MaintainDoc.ButtonsToHide.Add(tsb_Category.Name.ToString());
            tls_MaintainDoc.ButtonsToHide.Add(tsb_Archive.Name.ToString());
            tls_MaintainDoc.ButtonsToHide.Add(tsb_ViewAcknowledge.Name.ToString());
            tls_MaintainDoc.InitializeContextMenu();
        }

        #endregion

        #region "C1 Control Events"

        #region "Dhruv 20100623 -> C1DocumentAfterRowColumnChanged"

        private void c1Documents_AfterRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            _IsDocumentsLoading = true;
            int _rowIndex = c1Documents.RowSel;
            Int64 _selectDocumentID = 0;
            Int64 _selectcontainerID = 0;

            try
            {
                this.c1Documents.AfterRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);

                if (e.OldRange.r1 != e.NewRange.r1)
                {
                    UnloadDocuments();

                    if (_rowIndex >= 0)
                    {
                        if (c1Documents != null)
                        {
                            if (c1Documents.GetData(_rowIndex, COL_COLTYPE) != null)
                            {
                                if ((enum_DocumentColumnType)c1Documents.GetData(_rowIndex, COL_COLTYPE) == enum_DocumentColumnType.Document)
                                {
                                    _selectDocumentID = System.Convert.ToInt64(c1Documents.GetData(_rowIndex, COL_DOCUMENTID));// GetNode(C1.Win.C1FlexGrid.NodeTypeEnum.Parent).Data.ToString();
                                    //  _selectDocumentIDForImmunization = _selectDocumentID;
                                    _selectcontainerID = ((Document.eBaseContainers)c1Documents.GetData(_rowIndex, COL_CONATINERS))[0].EContainerID;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Load Document - START from c1Documents_AfterRowColChange Event in frmEDocumentViewer.cs" + " " + DateTime.Now.TimeOfDay);
                                    LoadDocument(_selectDocumentID, _selectcontainerID, _OpenExternalSource);
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - End from c1Documents_AfterRowColChange Event in frmEDocumentViewer.cs" + " " + DateTime.Now.TimeOfDay);                                    
                                }
                            }
                        }
                    }
                }

                #region "Acknowledge\Review"
                if (_rowIndex >= 0)
                {
                    if (c1Documents != null)
                    {
                        if (c1Documents.GetData(_rowIndex, COL_COLTYPE) != null)
                        {
                            if ((enum_DocumentColumnType)c1Documents.GetData(_rowIndex, COL_COLTYPE) == enum_DocumentColumnType.Document)
                            {
                                if (GetCheckedCount() > 1)
                                {
                                    tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_AcknowledgeReview;
                                    tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_AcknowledgeReview;
                                }
                                else
                                {
                                    //int _rowIndex = c1Documents.RowSel;
                                    if (System.Convert.ToBoolean(c1Documents.GetData(_rowIndex, COL_ISACKNOWLEDGE)) == true)
                                    {
                                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Reviwed;
                                    }
                                    else
                                    {
                                        tsb_Acknowledge.Text = "&" + DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                        tsb_Acknowledge.ToolTipText = DocumentContextMenu.ContextMenuNames.gDocMenu_Acknowledge;
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                _IsDocumentsLoading = false;
                if (lvwPages != null)
                {
                    if (lvwPages.Items.Count > 0) { lvwPages.Cursor = Cursors.Hand; }
                    else { lvwPages.Cursor = Cursors.Default; }
                }
                _IsgridCancel = false;                
            }
            catch (Exception ex)
            {

                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                //UnloadDocuments();
                lvwPages.Cursor = Cursors.Default;
            }
            finally
            {
                this.c1Documents.AfterRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_AfterRowColChange);
            }
        }
        #endregion "Dhruv 20100623 -> C1DocumentAfterRowColumnChanged"

        private void c1Documents_Click(object sender, EventArgs e)
        {

        }

        #region "Dhruv 20100623 -> c1documentMouseDown"

        private void c1Documents_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (_IsgridCancel == true)
                {
                    return;
                }
                else
                {
                    if (c1Documents != null)
                    {
                        // Resolved Bug #38948   'To avoid saving message box call 2 times on 'No' click.
                        this.c1Documents.BeforeRowColChange -= new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_BeforeRowColChange);

                        //06-Mar-15 Aniket: Resolving Bug #79978 ( Modified): gloEMR: Scan Docs - selected document should not loose focus as user click on empty space
                        //c1Documents.Select(c1Documents.HitTest(e.X, e.Y).Row, COL_DOCUMENTNAME);
                        this.c1Documents.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_BeforeRowColChange);
                        //----
                        
                        if (e.Button == MouseButtons.Right && _IsDocumentsLoading == false)
                        {
                            #region " Mouse Right Click functionality "

                            int _rowIndex = c1Documents.RowSel;//c1Documents.HitTest(e.X, e.Y).Row;

                            if (_rowIndex >= 0)
                            {
                                c1Documents.Select(_rowIndex, 1);

                                if (c1Documents.GetData(_rowIndex, COL_COLTYPE) != null)
                                {
                                    if ((enum_DocumentColumnType)c1Documents.GetData(_rowIndex, COL_COLTYPE) == enum_DocumentColumnType.Document)
                                    {
                                        if (lvwPages.Items.Count > 0 && lvwPages.SelectedItems.Count == 0)
                                        {
                                            lvwPages.Items[0].Selected = true;
                                        }

                                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                        { FillContextMenu_RCM(_PatientID, gloEDocV3Admin.gClinicID, _SelectedYear, false); }
                                        else
                                        { FillContextMenu(_PatientID, gloEDocV3Admin.gClinicID, _SelectedYear, false); }
                                        
                                    }
                                    else
                                    {
                                        c1Documents.ContextMenuStrip = null;
                                    }
                                }
                            }
                            else
                            {
                                c1Documents.ContextMenuStrip = null;
                            }

                            #endregion
                        }
                        c1Documents.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                // UnloadDocuments();
                this.c1Documents.BeforeRowColChange += new C1.Win.C1FlexGrid.RangeEventHandler(this.c1Documents_BeforeRowColChange);
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
            }
        }
        #endregion "Dhruv 20100623 -> c1documentMouseDown"


        private void c1Documents_AfterEdit(object sender, C1.Win.C1FlexGrid.RowColEventArgs e)
        {
            if (_IsDocumentsLoading == false && _OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {
                if (_OpenExternalSource != gloEDocumentV3.Enumeration.enum_OpenExternalSource.Immunization)
                {

                    if (GetCheckedCount() >= 1)
                    {
                        //tsb_Scan.Enabled = false;
                        //tsb_Import.Enabled = false;
                        if (GetCheckedCount() != 1)
                        {
                            tsb_InsertSign1.Enabled = false;
                            tsb_AddNote.Enabled = false;
                            tsb_AddTags.Enabled = false;
                        }
                        else
                        {

                            tsb_InsertSign1.Enabled = true;
                            tsb_AddNote.Enabled = true;
                            tsb_AddTags.Enabled = true;
                        }
                        tsb_RotateBack.Enabled = false;
                        tsb_RotateForward.Enabled = false;
                    }
                    else
                    {
                        if (tsb_Scan.Enabled == false) { tsb_Scan.Enabled = true; }
                        if (tsb_Import.Enabled == false) { tsb_Import.Enabled = true; }
                        if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                        if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                        if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                        if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                        if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                    }
                }
            }
        }


        #region "Dhruv 20100623 -> GetCheckedCount"

        private int GetCheckedCount()
        {
            int _result = 0;
            if (c1Documents != null)
            {
                for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.GetData(i, COL_COLTYPE) != null && ((enum_DocumentColumnType)c1Documents.GetData(i, COL_COLTYPE)) == enum_DocumentColumnType.Document)
                    {
                        if (c1Documents.GetCellCheck(i, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            _result = _result + 1;
                        }
                    }
                }
            }
            return _result;
        }
        #endregion "Dhruv 20100623 -> GetCheckedCount"

        #endregion
        //Added by Mayuri:20120125-For Immnunization VIS Category:
        private bool GetCheckedCountImmnunization()
        {
            bool _result = false;
            int _count = 0;
            if (c1Documents != null)
            {
                for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
                {
                    if (c1Documents.GetData(i, COL_COLTYPE) != null && ((enum_DocumentColumnType)c1Documents.GetData(i, COL_COLTYPE)) == enum_DocumentColumnType.Document)
                    {
                        if (c1Documents.GetCellCheck(i, COL_NODENAME) == C1.Win.C1FlexGrid.CheckEnum.Checked)
                        {
                            _count = _count + 1;
                            _DocumentIDForImmnunization = System.Convert.ToInt64(c1Documents.GetData(i, COL_DOCUMENTID));
                            if (_count > 1)
                            {

                                MessageBox.Show("One Vaccine record can have only one VIS document. ", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _result = true;
                                return _result;
                            }
                        }
                    }
                }
            }
            return _result;
        }
        //
        //Added on 20100527-Annot saving for Cancel-Click

        #region "Dhruv 20100623 -> AnnotationSaving1"

        private int AnnotationSaving1()
        {
            string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            int result = 1;
            //    DialogResult save;
            int _ConPageNumber = 0;
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFDoc != null)
                            {
                                if (oPDFDoc.IsModified())
                                {
                                    #region "Container and Document Information"
                                    if (c1Documents != null)
                                    {
                                        if (c1Documents.RowSel > 0)
                                        {
                                            _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                            _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));

                                        }
                                        _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                                    }
                                    #endregion



                                    Cursor.Current = Cursors.WaitCursor;
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);


                                    #region "Generate File to store into Database"
                                    //code added by dipak 20091007 as temp folder not find while rotate from view document
                                    string _FolderPath;
                                    _FolderPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString();
                                    try
                                    {
                                        if (_FolderPath != "")
                                        {
                                            if (System.IO.Directory.Exists(_FolderPath) == false)
                                            {
                                                System.IO.Directory.CreateDirectory(_FolderPath);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        _ErrorMessage = ex.ToString();
                                        AuditLogErrorMessage(_ErrorMessage);
                                    }
                                    //End dipak 20091007
                                    _FileFullPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString() + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";
                                    if (_FileFullPath != null)
                                    {
                                        if (oPDFDoc != null)
                                        {
                                            //  oPDFDoc.Lock();  //Lock Doc
                                            oPDFDoc.Save(_FileFullPath, 0);
                                        }
                                    }
                                    if (oPDFView != null)
                                    {
                                        if (oPDFView.GetPageCount() > 1)
                                        {
                                            if (oPDFView.GetCurrentPage() == oPDFView.GetDoc().GetPageCount())
                                            {
                                                oPDFView.GotoFirstPage();
                                            }
                                            else if (oPDFView.GetCurrentPage() == 1)
                                            {
                                                oPDFView.GotoLastPage();
                                            }
                                            else
                                            {
                                                oPDFView.GotoFirstPage();
                                            }
                                            oPDFView.GotoFirstPage();
                                            oPDFView.SetCurrentPage(_ConPageNumber);
                                            oPDFView.Update();
                                        }

                                        oPDFView.Refresh();
                                    }
                                    //if (oPDFDoc != null)//unlock Doc
                                    //{
                                    //    oPDFDoc.Unlock();
                                    //}
                                    #endregion

                                    using (gloEDocumentV3.eDocManager.eDocManager DocManager = new gloEDocumentV3.eDocManager.eDocManager())
                                    {
                                        bool _retVal = DocManager.UpdateContainer(_DocumentId, _ContainerId, _FileFullPath, true, _OpenExternalSource);
                                    }
                                    //DocManager.Dispose();
                                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation" + " " + DateTime.Now.TimeOfDay);

                                    Cursor.Current = Cursors.Default;
                                    result = 0;



                                }
                            }
                        }
                    }

                }

            }


            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            return result;
        }
        #endregion "Dhruv 20100623 -> AnnotationSaving1"

        //

        #region "Page Events"

        #region""
        Int32 PrevSelect = 0;
        Int32 Swap = 0;
        int _ConPageNumber = 0;
        int iNo = 0;
        bool _isAnnotNo = false;

        private void lvwPages_Click(object sender, EventArgs e)
        {
            #region"Dhruv"
            if (oPDFDoc != null)
            {
                //
                if (_IsFormLoading == false)
                {
                    if (oPDFDoc.IsModified() == true)
                    {
                        //if (IsSaved != true)
                        //{

                        _IsPagesClick = true;
                        PrevSelect = Swap;
                        DialogResult save = MessageBox.Show("Would you like to save the changes to the document?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (save == DialogResult.Yes)
                        {
                            _IsPagesClick = false;
                            AnnotationSaving1();
                        }
                        else if (save == DialogResult.No)
                        {
                            _IsPagesClick = false;

                            iNo = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            tsb_Refresh_Click(null, null);
                            _isAnnotNo = true;

                        }
                        else if (save == DialogResult.Cancel)
                        {
                            _IsPagesClick = true;
                            int selectedindex = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            if (lvwPages != null)
                            {
                                lvwPages.Items[selectedindex - 1].Selected = false;
                                lvwPages.Items[_ConPageNumber - 1].Selected = true;
                            }

                            if (oPDFView != null)
                            {
                                if (oPDFView.GetDoc() != null)
                                {
                                    oPDFView.SetVScrollPos(0);
                                    oPDFView.SetCurrentPage(PrevSelect);
                                    //oPDFView.SetPageViewMode(PDFView.PageViewMode.e_fit_width);
                                    try { oPDFView.Cursor = oPDFHandFreeCursor; }
                                    catch { oPDFView.Cursor = Cursors.Default; }

                                    cmbZoomPercentage_SelectedIndexChanged(null, null);
                                    //oPDFView.Select();Due to new dll added 5.6.0.0 it does not support Select()

                                    _SaveResult = false;
                                    return;

                                }
                            }
                        }
                        //}
                        //else IsSaved = false;
                    }//modify

                }
                //else
                //{
                //    _IsPagesClick = false;
                //    AnnotationSaving1();
                //}
            }

            #endregion


            if (lvwPages != null)
            {
                if (lvwPages.SelectedItems.Count <= 0)
                {
                    if (lvwPages.Items.Count > 0)
                    {
                        lvwPages.Items[0].Selected = true;
                    }
                }


                if (lvwPages.SelectedItems.Count > 0)
                {
                    int _DocPageNumber = 0;

                    Int64 _ContainerNumber = 0;
                    Int64 _DocumentNumber = 0;
                    // string _NotesText = "";

                    gloEDocumentV3.eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                    gloEDocumentV3.Common.NTAOs oNotes = null;
                    txtNotes.Text = "";

                    System.Drawing.Font _fntArial9F = null;
                    System.Drawing.Font _fntArial8F = null;

                    try
                    {
                        #region "Set Page into Viewer"


                        _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                        _DocPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[3].Text);


                        #region "For the Cancel click and No Click"
                        Swap = _ConPageNumber;

                        if (_isAnnotNo == true)
                        {
                            if (lvwPages.Items.Count > 0)
                            {

                                lvwPages.Items[_ConPageNumber - 1].Selected = false;
                                if (PrevSelect > _ConPageNumber)
                                    lvwPages.Items[iNo - 1].Selected = true;
                                _ConPageNumber = iNo;
                                _isAnnotNo = false;
                            }
                        }

                        #endregion
                        if (oPDFView != null)
                        {
                            if (oPDFView.GetDoc() != null)
                            {
                                oPDFView.SetVScrollPos(0);
                                oPDFView.SetCurrentPage(_ConPageNumber);
                                //oPDFView.SetPageViewMode(PDFView.PageViewMode.e_fit_width);
                                try { oPDFView.Cursor = oPDFHandFreeCursor; }
                                catch { oPDFView.Cursor = Cursors.Default; }

                                cmbZoomPercentage_SelectedIndexChanged(null, null);
                                //oPDFView.Select();Due to new dll added 5.6.0.0 it does not support Select()

                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerNumber = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentNumber = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));


                                }
                            }
                        }
                        #endregion

                        #region "Show Notes"
                        if (oList != null)
                        {
                            oNotes = oList.GetNotes(_DocumentNumber, _DocPageNumber, _ConPageNumber, gloEDocV3Admin.gClinicID, _OpenExternalSource);
                        }

                        if (oNotes != null)
                        {
                            string _Date = "";
                            string _Description = "";
                            int _SelStart = 0;
                            int _SelEnd = 0;

                            _fntArial9F = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            _fntArial8F = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                            for (int i = 0; i < oNotes.Count; i++)
                            {
                                if (i == 0)
                                {
                                    _Date = oNotes[i].NTAODateTime.ToString();
                                    _Description = oNotes[i].NTAODescription;

                                    _SelStart = txtNotes.Text.Length;
                                    _SelEnd = _Date.Length;
                                    txtNotes.AppendText(_Date);
                                    txtNotes.Select(_SelStart, _SelEnd);
                                    txtNotes.SelectionFont = _fntArial9F;
                                    txtNotes.SelectionColor = Color.FromArgb(194, 72, 0);

                                    txtNotes.AppendText(Environment.NewLine);

                                    _SelStart = txtNotes.Text.Length;
                                    _SelEnd = _Description.Length;
                                    txtNotes.AppendText(_Description);
                                    txtNotes.Select(_SelStart, _SelEnd);
                                    txtNotes.SelectionFont = _fntArial8F;
                                    txtNotes.SelectionColor = Color.FromArgb(54, 95, 145);
                                    txtNotes.Select(0, 0);
                                }
                                else
                                {
                                    _Date = oNotes[i].NTAODateTime.ToString();
                                    _Description = oNotes[i].NTAODescription;

                                    txtNotes.AppendText(Environment.NewLine);

                                    txtNotes.Select(0, 0);
                                    _SelStart = txtNotes.Text.Length;
                                    _SelEnd = _Date.Length;
                                    txtNotes.AppendText(_Date);
                                    txtNotes.Select(_SelStart, _SelEnd);
                                    txtNotes.SelectionFont = _fntArial9F;
                                    txtNotes.SelectionColor = Color.FromArgb(194, 72, 0);

                                    txtNotes.AppendText(Environment.NewLine);

                                    txtNotes.Select(0, 0);
                                    _SelStart = txtNotes.Text.Length;
                                    _SelEnd = _Description.Length;
                                    txtNotes.AppendText(_Description);
                                    txtNotes.Select(_SelStart, _SelEnd);
                                    txtNotes.SelectionFont = _fntArial8F;
                                    txtNotes.SelectionColor = Color.FromArgb(54, 95, 145);
                                    txtNotes.Select(0, 0);
                                }
                            }
                        }
                        #endregion

                        #region "Right Click Checking"
                        if (_IsMouserRightClick == true)
                        {

                        }
                        #endregion

                        #region "Assign ToolTip"
                        //sudhir 20090105
                        if (pnlNotes.Height == 28)
                        {
                            if (oNoteToolTip != null)
                            {
                                oNoteToolTip.RemoveAll();
                                oNoteToolTip.Dispose();
                            }
                            oNoteToolTip = new ToolTip();
                            oNoteToolTip.SetToolTip(lblNotes, txtNotes.Text);
                            oNoteToolTip.Active = true;
                        }
                        //
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);

                    }
                    finally
                    {
                        _IsMouserRightClick = false;

                        //SG: Memory Leaks, disposing font object used above
                        if (_fntArial8F != null) { _fntArial8F.Dispose(); _fntArial8F = null; }
                        if (_fntArial9F != null) { _fntArial9F.Dispose(); _fntArial9F = null; }

                        if (oList != null) { oList.Dispose(); oList = null; }
                        if (oNotes != null) { oNotes.Dispose(); oNotes = null; }
                    }


                }


            }


        }

        #endregion


        private void lvwPages_MouseDown(object sender, MouseEventArgs e)
        {

        }


        #region "Dhruv 20100623 -> lvwPagesMouseUp"

        private void lvwPages_MouseUp(object sender, MouseEventArgs e)
        {
            if (lvwPages != null)
            {
                if (lvwPages.HitTest(e.X, e.Y).Item == null)
                {
                    if (lvwPages.SelectedItems.Count <= 0)
                    {
                        if (lvwPages.Items.Count > 0)
                        {
                            lvwPages.Items[0].Selected = true;
                            lvwPages_Click(null, null);
                        }
                        else
                        {
                            //Developer: Mitesh Patel
                            //Date:20-Dec-2011'
                            //Bug ID: 15959
                            if (lvwPages.ContextMenuStrip != null) { gloGlobal.cEventHelper.RemoveAllEventHandlers(lvwPages.ContextMenuStrip); lvwPages.ContextMenuStrip.Dispose(); }
                        }
                    }
                }
            }
        }
        #endregion "Dhruv 20100623 -> lvwPagesMouseUp"

        #region "Dhruv 20100623 -> ButtonNextPageClick"

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            if (lvwPages != null)
            {
                if (lvwPages.SelectedItems.Count > 0)
                {
                    if (lvwPages.SelectedItems[0].Index != lvwPages.Items.Count - 1)
                    {
                        Int32 _selItemIndex = lvwPages.SelectedItems[0].Index;
                        //lvwPages.SelectedItems[0].SubItems[2].i
                        lvwPages.SelectedItems.Clear();
                        lvwPages.Items[_selItemIndex + 1].Selected = true;
                        lvwPages.SelectedItems[0].EnsureVisible();
                        lvwPages_Click(null, null);
                        _selItemIndex = 0;
                    }
                }
            }

        }
        #endregion "Dhruv 20100623 -> ButtonNextPageClick"


        #region "Dhruv 20100623 -> buttonLastPage"

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (lvwPages != null)
            {
                if (lvwPages.SelectedItems.Count > 0)
                {
                    if (lvwPages.SelectedItems[0].Index != lvwPages.Items.Count - 1)
                    {
                        lvwPages.SelectedItems.Clear();
                        lvwPages.Items[lvwPages.Items.Count - 1].Selected = true;
                        lvwPages.SelectedItems[0].EnsureVisible();
                        lvwPages_Click(null, null);
                    }
                }
            }

        }
        #endregion "Dhruv 20100623 -> buttonLastPage"

        #region "Dhruv 20100623 -> ButtonPreviousPageClick "

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            if (lvwPages != null)
            {
                if (lvwPages.SelectedItems.Count > 0)
                {
                    if (lvwPages.SelectedItems[0].Index != 0)
                    {
                        Int32 _selItemIndex = lvwPages.SelectedItems[0].Index;
                        //lvwPages.SelectedItems[0].SubItems[2].i
                        lvwPages.SelectedItems.Clear();
                        lvwPages.Items[_selItemIndex - 1].Selected = true;
                        lvwPages.SelectedItems[0].EnsureVisible();
                        lvwPages_Click(null, null);
                        _selItemIndex = 0;
                    }
                }
            }
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            if (lvwPages != null)
            {
                if (lvwPages.SelectedItems.Count > 0)
                {
                    if (lvwPages.SelectedItems[0].Index != 0)
                    {
                        lvwPages.SelectedItems.Clear();
                        lvwPages.Items[0].Selected = true;
                        lvwPages.SelectedItems[0].EnsureVisible();
                        lvwPages_Click(null, null);
                    }
                }
            }
        }
        #endregion "Dhruv 20100623 -> ButtonPreviousPageClick "

        #endregion

        #region "Show/Hide Commands"
        private void btnPat_Down_Click(object sender, EventArgs e)
        {  //if condiotion added by dipak 20090930 to avoid null accessing  of oPatientStrip
            if (oPatientStrip != null)
            //Added by Mayuri on 20090919
            {
                pnlPatients.Height = oPatientStrip.Height + 28;
            }
            //pnlPatients.Height = 91;
            // pnlPatients.Height = oPatientStrip.Height;
            btnPat_Up.Visible = true;
            btnPat_Down.Visible = false;
        }

        private void btnPat_Up_Click(object sender, EventArgs e)
        {
            //if else condiotion added by dipak 20090930 to avoid null accessing  of oPatientStrip
            if (oPatientStrip == null)
            { pnlPatients.Height = 24; }
            else
            { pnlPatients.Height = oPatientStrip.Height; }

            btnPat_Up.Visible = false;
            btnPat_Down.Visible = true;
        }

        private void btnNote_Down_Click(object sender, EventArgs e)
        {
            pnlNotes.Height = 26;
            btnNote_Up.Visible = true;
            btnNote_Down.Visible = false;
            lblSplNoteTop.Visible = false;
            lblSplNoteBottom.Visible = false;
            if (oNoteToolTip != null)
            {
                oNoteToolTip.RemoveAll();
                oNoteToolTip.Dispose();
            }
            oNoteToolTip = new ToolTip();
            oNoteToolTip.SetToolTip(lblNotes, txtNotes.Text);
            oNoteToolTip.Active = true;

        }

        private void btnNote_Up_Click(object sender, EventArgs e)
        {
            pnlNotes.Height = 120;
            btnNote_Up.Visible = false;
            btnNote_Down.Visible = true;
            btnNote_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btnNote_Down.BackgroundImageLayout = ImageLayout.Center;
            lblSplNoteTop.Visible = true;
            lblSplNoteBottom.Visible = true;
            if (oNoteToolTip != null) { oNoteToolTip.RemoveAll(); oNoteToolTip.Dispose(); oNoteToolTip = null; }
        }

        private void btnDoc_Left_Click(object sender, EventArgs e)
        {

            btnDoc_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btnDoc_Left.BackgroundImageLayout = ImageLayout.Center;
            pnlSmallStrip.Visible = true;
            pnlDocumentsLegends.Visible = false;
            splitter6.Visible = true;

        }

        private void btn_Right_Click(object sender, EventArgs e)
        {

            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
            splitter6.Visible = true;
            pnlSmallStrip.Visible = false;
            pnlDocumentsLegends.Visible = true;


        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (cmbZoomPercentage.SelectedIndex < (cmbZoomPercentage.Items.Count - 1))
            {
                cmbZoomPercentage.SelectedIndex = cmbZoomPercentage.SelectedIndex + 1;
                ZoomDocument(cmbZoomPercentage.SelectedItem.ToString());
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if ((cmbZoomPercentage.SelectedIndex - 1) >= 0)
            {
                cmbZoomPercentage.SelectedIndex = cmbZoomPercentage.SelectedIndex - 1;
                ZoomDocument(cmbZoomPercentage.SelectedItem.ToString());
            }
        }

        private void cmbZoomPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GLO2011-0012077	DMS Zoom Issue
            // To set the selected Zoom (index) value to globle variable
            if (cmbZoomPercentage.SelectedIndex >= 0)
            {
                gloEDocumentV3.gloEDocV3Admin.gStrZoomIndexValue = cmbZoomPercentage.SelectedIndex;
                ZoomDocument(cmbZoomPercentage.SelectedItem.ToString());
            }
        }

        private void ZoomDocument(string Percentage)
        {
            if (Percentage.Substring(Percentage.Length - 1, 1) == "%")
            {
                if (oPDFView != null)
                {
                    if (oPDFView.GetDoc() != null)
                    {
                        double _curZoom = oPDFView.GetZoom();
                        oPDFView.SetZoom(System.Convert.ToDouble(Percentage.Substring(0, Percentage.Length - 1).ToString()) / 100);
                    }
                }
            }
            else if (Percentage == "Actual Size")
            {
                if (oPDFView != null)
                {
                    if (oPDFView.GetDoc() != null)
                    {
                        // oPDFView.SetPageViewMode(PDFView.PageViewMode.e_zoom);
                    }
                }
            }
            else if (Percentage == "Fit To Width")
            {
                if (oPDFView != null)
                {
                    if (oPDFView.GetDoc() != null)
                    {
                        //oPDFView.SetPageViewMode(PDFView.PageViewMode.e_fit_width);
                        oPDFView.SetPageViewMode(PDFViewCtrl.PageViewMode.e_fit_width);
                    }
                }
            }
            else if (Percentage == "Fit To Height")
            {
                if (oPDFView != null)
                {
                    if (oPDFView.GetDoc() != null)
                    {
                        // oPDFView.SetPageViewMode(PDFView.PageViewMode.e_zoom);
                        oPDFView.SetPageViewMode(PDFViewCtrl.PageViewMode.e_zoom);
                    }
                }
            }
            else if (Percentage == "Full Page")
            {
                if (oPDFView != null)
                {
                    if (oPDFView.GetDoc() != null)
                    {
                        //oPDFView.SetPageViewMode(PDFView.PageViewMode.e_fit_page);
                        oPDFView.SetPageViewMode(PDFViewCtrl.PageViewMode.e_fit_page);
                    }
                }
            }

        }

        void oPDFView_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                oPDFView.OnScroll(0, -(e.Delta));
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
        }

        private void ts_SmallStrip_btn_Document_Click(object sender, EventArgs e)
        {
            pnlDocumentsLegends.Visible = true;
            pnlSmallStrip.Visible = false;
        }

        private void btnPageView_Large_Click(object sender, EventArgs e)
        {
            lvwPages.View = View.LargeIcon;
        }

        private void btnPageView_Tile_Click(object sender, EventArgs e)
        {
            lvwPages.View = View.Tile;
        }

        private void btnPageView_Details_Click(object sender, EventArgs e)
        {
            lvwPages.View = View.Details;
        }

        private void btnPageView_Small_Click(object sender, EventArgs e)
        {
            lvwPages.View = View.SmallIcon;
        }

        private void btnPageView_List_Click(object sender, EventArgs e)
        {
            lvwPages.View = View.List;
        }

        #endregion

        #region " Designer Events "

        private void btn_Right_MouseHover(object sender, EventArgs e)
        {

        }

        private void btn_Right_MouseLeave(object sender, EventArgs e)
        {

        }

        #endregion

        //**************Ojeswini********************

        private void btn_Right_MouseHover_1(object sender, EventArgs e)
        {
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.ForwardHover;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btn_Right_MouseLeave_1(object sender, EventArgs e)
        {
            btn_Right.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btn_Right.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDoc_Left_MouseHover(object sender, EventArgs e)
        {
            btnDoc_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.RewindHover;
            btnDoc_Left.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnDoc_Left_MouseLeave(object sender, EventArgs e)
        {
            btnDoc_Left.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btnDoc_Left.BackgroundImageLayout = ImageLayout.Center;
        }


        private void btnPat_Up_MouseHover(object sender, EventArgs e)
        {
            btnPat_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UPHover;
            btnPat_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Up_MouseLeave(object sender, EventArgs e)
        {
            btnPat_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
            btnPat_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Down_MouseLeave(object sender, EventArgs e)
        {
            btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btnPat_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPat_Down_MouseHover(object sender, EventArgs e)
        {
            btnPat_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.DownHover;
            btnPat_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnLastPage_MouseHover(object sender, EventArgs e)
        {
            btnLastPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.LastHover;
            btnLastPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnLastPage_MouseLeave(object sender, EventArgs e)
        {
            btnLastPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Last11;
            btnLastPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnNextPage_MouseHover(object sender, EventArgs e)
        {
            btnNextPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.ForwardHover;
            btnNextPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnNextPage_MouseLeave(object sender, EventArgs e)
        {
            btnNextPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Forward;
            btnNextPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPrevPage_MouseHover(object sender, EventArgs e)
        {
            btnPrevPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.RewindHover;
            btnPrevPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnPrevPage_MouseLeave(object sender, EventArgs e)
        {
            btnPrevPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Rewind;
            btnPrevPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnFirstPage_MouseHover(object sender, EventArgs e)
        {
            btnFirstPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.FirstHover;
            btnFirstPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnFirstPage_MouseLeave(object sender, EventArgs e)
        {
            btnFirstPage.BackgroundImage = global::gloEDocumentV3.Properties.Resources.First1;
            btnFirstPage.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomIn_MouseHover(object sender, EventArgs e)
        {
            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.pluseHover;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomIn_MouseLeave(object sender, EventArgs e)
        {
            btnZoomIn.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Pluse;
            btnZoomIn.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomOut_MouseHover(object sender, EventArgs e)
        {
            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.MinusHover;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnZoomOut_MouseLeave(object sender, EventArgs e)
        {
            btnZoomOut.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Minus1;
            btnZoomOut.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnNote_Up_MouseHover(object sender, EventArgs e)
        {
            btnNote_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UPHover;
            btnNote_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnNote_Up_MouseLeave(object sender, EventArgs e)
        {
            btnNote_Up.BackgroundImage = global::gloEDocumentV3.Properties.Resources.UP;
            btnNote_Up.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnNote_Down_MouseHover(object sender, EventArgs e)
        {
            btnNote_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.DownHover;
            btnNote_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void btnNote_Down_MouseLeave(object sender, EventArgs e)
        {
            btnNote_Down.BackgroundImage = global::gloEDocumentV3.Properties.Resources.Down;
            btnNote_Down.BackgroundImageLayout = ImageLayout.Center;
        }

        private void tsb_Archive_Click(object sender, EventArgs e)
        {
            try
            {
                frmEDocEvent__ArchiveDocuments ofrm = new frmEDocEvent__ArchiveDocuments(_PatientID, _OpenExternalSource);
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.ShowInTaskbar = false;
                ofrm.ShowDialog(this);
                tsb_Refresh_Click(null, null);
                if (ofrm != null)  //Disposed by Mitesh
                {
                    ofrm.Dispose();
                    ofrm = null;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
        }

        private void chkSearch_UserTag_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch_UserTag.Checked == true)
            {
                txtSearch_UserTag.ReadOnly = false;
                txtSearch_UserTag.Focus();
            }
            else
            {
                txtSearch_UserTag.ReadOnly = true;
                txtSearch_UserTag.Text = "";
            }
        }

        private void chkSearch_Notes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch_Notes.Checked == true)
            {
                txtSearch_Notes.ReadOnly = false;
                chkSearch_Notes.Focus();
            }
            else
            {
                txtSearch_Notes.ReadOnly = true;
                txtSearch_Notes.Text = "";
            }
        }

        private void chkSearch_Acknowledge_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch_Acknowledge.Checked == true)
            {
                txtSearch_Acknowledge.ReadOnly = false;
                txtSearch_Acknowledge.Focus();
            }
            else
            {
                txtSearch_Acknowledge.ReadOnly = true;
                txtSearch_Acknowledge.Text = "";
            }
        }

        /// <summary>
        /// Function added for Set DMSPagePriview
        /// </summary>
        /// <returns>Added by dipak 20091009</returns>
        private void SetDMSPagePreviewSetting()
        {
            try
            {

                object dt;
                //get setting form Setting table and return to out variable dt
                ogloSettings.GetSetting("DMSPagePreview", gloEDocV3Admin.gUserID, gloEDocV3Admin.gClinicID, out dt);
                if (dt == null)
                {
                    _DMSPagePreview = "";
                }
                else
                {
                    _DMSPagePreview = System.Convert.ToString(dt);
                }

                // GLO2011-0012077	DMS Zoom Issue
                // Set global variable from database for 1st time
                if (System.Convert.ToInt32(gloEDocumentV3.gloEDocV3Admin.gStrZoomIndexValue) == 0)
                { gloEDocumentV3.gloEDocV3Admin.gStrZoomIndexValue = System.Convert.ToInt32(_DMSPagePreview); }


                //if setting not found in database then 
                if (_DMSPagePreview != "")
                {
                    // GLO2011-0012077	DMS Zoom Issue
                    // Set the combobox selection from global variable rather than from DB.

                    cmbZoomPercentage.SelectedIndex = System.Convert.ToInt32(gloEDocumentV3.gloEDocV3Admin.gStrZoomIndexValue);
                    //cmbZoomPercentage.SelectedIndex = System.Convert.ToInt16(_DMSPagePreview);  
                }
                else if (cmbZoomPercentage.Items.Count > 10)
                {
                    cmbZoomPercentage.SelectedIndex = 9;
                }
                else if (cmbZoomPercentage.Items.Count > 0)
                {
                    cmbZoomPercentage.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

        }
        /// <summary>
        /// Region od the annotate 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        #region "Annotate"


        #region "Sub annots Line,Rectangle,Ellipse,Arrow,Freehand,Editing"
        private void stsannot_Line_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {

                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                //oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_line_create);
                                SetToolMode(PDFViewCtrl.ToolMode.e_line_create, MyPDFView.CustomToolMode.e_none);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void stsannot_Rectangle_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                //oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_rect_create);
                                SetToolMode(PDFViewCtrl.ToolMode.e_rect_create, MyPDFView.CustomToolMode.e_none);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void stsannote_Ellipse_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                //oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_oval_create);
                                SetToolMode(PDFViewCtrl.ToolMode.e_oval_create, MyPDFView.CustomToolMode.e_none);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void stsannot_Arrow_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                //oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_arrow_create);
                                SetToolMode(PDFViewCtrl.ToolMode.e_arrow_create, MyPDFView.CustomToolMode.e_none);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void sts_Freehand_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                //oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_ink_create);
                                SetToolMode(PDFViewCtrl.ToolMode.e_ink_create, MyPDFView.CustomToolMode.e_none);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void stsannot_Editing_Click(object sender, EventArgs e)
        {
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                //oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                                SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit, MyPDFView.CustomToolMode.e_none);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion "Sub annots Line,Rectangle,Ellipse,Arrow,Freehand,Editing"

        #region "Dhruv 20100623-> Saving the Annots and Called from pnlPreview_Leave"

        public int AnnotationSaving()
        {
            string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            int result = 1;
            int _ConPageNumber = 0;
            DialogResult save;
            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFDoc != null)
                            {

                                if (oPDFDoc.IsModified())
                                {

                                    save = MessageBox.Show("Would you like to save the changes to the document?", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                                    if (save == DialogResult.Yes)
                                    {
                                        // _IsPagesClick = false;
                                        Cursor.Current = Cursors.WaitCursor;
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);

                                        #region "Container and Document Information"
                                        if (c1Documents != null)
                                        {
                                            if (c1Documents.RowSel > 0)
                                            {
                                                _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                                _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                            }
                                            _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                                        }
                                        #endregion

                                        #region "Generate File to store into Database"
                                        string _FolderPath;
                                        _FolderPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString();
                                        try
                                        {
                                            if (_FolderPath != "")
                                            {
                                                if (System.IO.Directory.Exists(_FolderPath) == false)
                                                {
                                                    System.IO.Directory.CreateDirectory(_FolderPath);
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            _ErrorMessage = ex.ToString();
                                            AuditLogErrorMessage(_ErrorMessage);
                                        }

                                        _FileFullPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString() + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";
                                        if (_FileFullPath != null)
                                        {
                                            if (oPDFDoc != null)
                                            {
                                                oPDFDoc.Save(_FileFullPath, 0);
                                            }
                                        }

                                        if (oPDFView != null)
                                        {
                                            if (oPDFView.GetPageCount() > 1)
                                            {
                                                if (oPDFView.GetCurrentPage() == oPDFView.GetDoc().GetPageCount())
                                                {
                                                    oPDFView.GotoFirstPage();
                                                }
                                                else if (oPDFView.GetCurrentPage() == 1)
                                                {
                                                    oPDFView.GotoLastPage();
                                                }
                                                else
                                                {
                                                    oPDFView.GotoFirstPage();
                                                }
                                                oPDFView.GotoFirstPage();
                                                oPDFView.SetCurrentPage(_ConPageNumber);
                                                oPDFView.Update();
                                            }
                                            else
                                            { //line commented by dipak 20091007 to solve problem of rotating page of single page document.
                                                //oPDFView.SetDoc(oPDFDoc); 
                                            }


                                            oPDFView.Refresh();
                                        }
                                        #endregion

                                        using (gloEDocumentV3.eDocManager.eDocManager DocManager = new gloEDocumentV3.eDocManager.eDocManager())
                                        {
                                            bool _retVal = DocManager.UpdateContainer(_DocumentId, _ContainerId, _FileFullPath, true, _OpenExternalSource);
                                        }

                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation" + " " + DateTime.Now.TimeOfDay);

                                        Cursor.Current = Cursors.Default;
                                        result = 0;

                                    }
                                    else if (save == DialogResult.Cancel)
                                    {

                                        result = 2;
                                        _IsgridCancel = true;
                                    }
                                    else if (save == DialogResult.No)
                                    {

                                        result = 1;
                                    }

                                }
                            }
                        }
                    }


                }

            }


            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


            return result;
        }

        private void stsannot_Save_Click(object sender, EventArgs e)
        {
            AnnotationSaving1();
        }
        private void pnlPreview_Leave(object sender, EventArgs e)
        {
            //_isCloseSave = false;
            _Isgrid = true;
        }
        #endregion

        #region "Inserting signature"

        private void stsannot_Signature_Click(object sender, EventArgs e)
        {
            //string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            int _ConPageNumber = 0;
            Obj stm = null;
            //Rotation Parameter
            double myRotatedAngle = 0;
            int myNewX1 = 15;
            int myNewY1 = 15;
            int myNewX2 = 15;
            int myNewY2 = 15;
            Page.Rotate myRotatedPage = Page.Rotate.e_0;
            //End Rotation Parameter

            //SG: Memory Leaks, moved variable declaration out of try block to handle dispose in finally
            gloEDocumentV3.eDocManager.eDocManager oManager = null;
            pdftron.SDF.SDFDoc sdfdoc = null;
            pdftron.PDF.Page page = null;
            DataTable dt = null;
            Field sig = null;
            pdftron.PDF.Annots.Widget a = null;
            //

            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);


                            #region "Container and Document Information"
                            if (c1Documents != null)
                            {
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                                //Start / Rotation
                                myRotatedPage = oPDFView.GetDoc().GetPage(_ConPageNumber).GetRotation();
                                myRotatedAngle = RotateSignatureImage(myRotatedPage);
                                //End / Rotation
                            }
                            #endregion

                            try
                            {
                                page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no
                                double pgheight = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageHeight();
                                double pgwidth = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageWidth();

                                //SG: Memory Leaks, moved variable declaration out of try block
                                //pdftron.SDF.SDFDoc sdfdoc = oPDFDoc.GetSDFDoc();             //obj of the sdf doc
                                sdfdoc = oPDFDoc.GetSDFDoc();
                                //

                                stm = oPDFDoc.CreateIndirectDict();

                                float MyWidth = 0;
                                float MyHeight = 0;
                                //Integrtaed by Mayuri:20101022-provider sign change
                                //Sanjog - Added On 20101011 for login user signature
                                Int64 _isPrv = Int64.Parse(gloEDocV3Admin.GetProviderDetails());

                                //sanjog
                                if (_isAnnotationPrvClick == true)
                                {
                                    if (_isAnnotationChildPrvClick == false)
                                    {
                                        Int64 _isProvider = 0;
                                        _isProvider = Int64.Parse(gloEDocV3Admin.GetProviderDetails());

                                        bool rslt = false;
                                        rslt = gloEDocV3Admin.GetSignDelegateStatus();

                                        oManager = new gloEDocumentV3.eDocManager.eDocManager();
                                        //DataTable dt = null; //SG: Memory Leaks, moving variable declaration outside of try block
                                        bool _IsSignRight = false;
                                        Int16 i = default(Int16);
                                        //dt = new DataTable(); //SG: Memory Leaks, new is not needed here

                                        if (_isProvider != 0)
                                        {
                                            bool blnResult = false;
                                            string Pat_Provider = null;
                                            //string SelectedProvider = null;
                                            //DialogResult dResult = default(DialogResult);
                                            blnResult = gloEDocV3Admin.CheckpatientProviderStatus(_PatientID, _isProvider);
                                            if (blnSignClick == false)
                                            {
                                                if (blnResult)
                                                {
                                                    //'Selected Provider Is Exam Provider
                                                }
                                                else
                                                {

                                                    Pat_Provider = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                                    Int64 _ProviderID = default(Int64);
                                                    _ProviderID = Int64.Parse(gloEDocV3Admin.GetProviderStatus(_PatientID));
                                                    dt = oManager.GetAllAssignProviders(gloEDocV3Admin.gUserID);
                                                    if (dt == null || dt.Rows.Count == 0) //SG: Memory Leaks, checking null condition
                                                    {
                                                        {
                                                            MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            return;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (dt.Rows.Count > 0)
                                                        {
                                                            for (i = 0; i <= dt.Rows.Count - 1; i++)
                                                            {
                                                                if (_ProviderID.ToString() == dt.Rows[i]["nProviderId"].ToString().Trim())
                                                                {
                                                                    _IsSignRight = true;
                                                                    break;
                                                                }
                                                            }
                                                            if (_IsSignRight == false)
                                                            {
                                                                string strName = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                                                MessageBox.Show("User '" + gloEDocV3Admin.gUserName + "' is not designated as a Signature Delegate for '" + strName + "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                strName = null;
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Int64 _ProviderID = default(Int64);
                                            _ProviderID = Int64.Parse(gloEDocV3Admin.GetProviderStatus(_PatientID));

                                            dt = oManager.GetAllAssignProviders(gloEDocV3Admin.gUserID);
                                            if (dt.Rows.Count == 0)
                                            {
                                                MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return;
                                            }
                                            else
                                            {
                                                if (dt.Rows.Count > 0)
                                                {
                                                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                                                    {
                                                        if (_ProviderID.ToString() == dt.Rows[i]["nProviderId"].ToString().Trim())
                                                        {
                                                            _IsSignRight = true;
                                                        }
                                                    }
                                                    if (_IsSignRight == false)
                                                    {
                                                        string strName = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                                        MessageBox.Show("User '" + gloEDocV3Admin.gUserName + "' is not designated as a Signature Delegate for '" + strName + "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        strName = null;
                                                        return;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }

                                /////sanjog 


                                if (_isAnnotationPrvClick)
                                {
                                    if (_isAnnotationChildPrvClick)
                                    {
                                        ToolStripMenuItem oProviderMenu = (ToolStripMenuItem)sender;
                                        _ProviderID = Int64.Parse(oProviderMenu.Tag.ToString());
                                        stm = CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, _ProviderID, 2, myRotatedAngle, ref myNewX1, ref myNewY1, ref myNewX2, ref myNewY2);//CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, _ProviderID, 2);
                                    }
                                    else
                                    {
                                        stm = CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, _PatientID, 0, myRotatedAngle, ref myNewX1, ref myNewY1, ref myNewX2, ref myNewY2);//CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, _PatientID, 0);
                                    }

                                }
                                else
                                {
                                    if (_isPrv > 0)
                                    {
                                        stm = CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, _isPrv, 2, myRotatedAngle, ref myNewX1, ref myNewY1, ref myNewX2, ref myNewY2);//CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, _isPrv, 2);
                                    }
                                    else
                                    {
                                        stm = CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, gloEDocV3Admin.gUserID, 1, myRotatedAngle, ref myNewX1, ref myNewY1, ref myNewX2, ref myNewY2);// CreateSignatureAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth, out MyWidth, out MyHeight, gloEDocV3Admin.gUserID, 1);
                                    }
                                }

                                //Sanjog - Added On 20101011 for login user signature


                                if (stm != null)
                                {
                                    //**SG: Memory Leaks, moving declaration outside try block

                                    //Field sig = oPDFDoc.FieldCreate("sig", Field.Type.e_signature, stm);
                                    sig = oPDFDoc.FieldCreate("sig", Field.Type.e_signature, stm);

                                    //pdftron.PDF.Annots.Widget a = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(myNewX1, myNewY1, myNewX2, myNewY2), sig); // right corner setting 
                                    a = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(myNewX1, myNewY1, myNewX2, myNewY2), sig);

                                    //**

                                    sig.RefreshAppearance();
                                    //---
                                    temp = a;
                                    //if (stm != null)
                                    // {
                                    if (oPDFView != null)
                                    {
                                        oPDFView.SetProgressiveRendering(false);                            //to reduce the flickering effect.
                                        a.GetAppearance();
                                        a.SetAppearance(stm, Annot.AnnotationState.e_normal);
                                        //00002563 : Concord fax do not showing annotations.
                                        //Problem : 00000147
                                        //Set the annotation flag to print so when page is going to print it will print the annotation as well.
                                        a.SetFlag(Annot.Flag.e_print, true);
                                        page.AnnotPushBack(a);                                              //adding the image to page.
                                        oPDFView.Update();                                                  //required to update
                                        oPDFView.UpdatePageLayout();
                                        oPDFView.Refresh();
                                        oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                                        oPDFView.SetProgressiveRendering(true);                             //to reduce the flickering effect

                                    }

                                }

                                else
                                {
                                    if (_isAnnotationPrvClick)
                                    {
                                        if (_isAnnotationChildPrvClick)
                                        {
                                            MessageBox.Show("Selected Provider has no signature on file. Electronic signature cannot be added.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                        else
                                        {
                                            MessageBox.Show("Patient Provider has no signature on file. Electronic signature cannot be added.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Current user has no signature on file. Electronic signature cannot be added.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    // MessageBox.Show("Signature not available for patient provider", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }

                            }
                            catch (PDFNetException ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                //SG: Memory Leaks, disposing moved variables declaration out of try block 
                if (a != null) { a.Dispose(); a = null; }
                if (sig != null) { sig = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
                if (page != null) { page = null; }
                if (sdfdoc != null) { sdfdoc.Close(); sdfdoc.Dispose(); sdfdoc = null; }
                if (oManager != null) { oManager.Dispose(); oManager = null; }
                if (stm != null) { stm.Dispose(); stm = null; }
                //
            }



        }

        static Obj CreateSignatureAppearance(PDFDoc doc, Int64 _PatientId, Int64 _ContainerId, Int64 _DocumentId, double pgheight, double pgwidth, out float MyWidth, out  float MyHeight, Int64 SignatureId, int flag, double myRotatedAngle, ref int myNewX1, ref int myNewY1, ref int myNewX2, ref int myNewY2)
        {

            #region "variable decalaration"
            //pdftron.PDF.PDFDoc FileContainer = null;
            //  Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDatabaseConnectionString);
            //   Database.DBParameters oParameters = new gloEDocumentV3.Database.DBParameters();
            // DataTable dt = new DataTable();
            bool _signaturefound = false;
            //  string _strSignFileName = "";
            //   string _ProviderName = "";
            string _SignetureText = "";
            //SLR: Added initialization defaults
            string _SignatureFont = "Arial";
            int _SignatureFontSize = 12;
            decimal _MaxScale = 0.6M;
            decimal _MinScale = 0.3M;
            Obj stm = null;


            MyWidth = (int)pgwidth;
            MyHeight = (int)pgheight;

            #endregion

            #region "Generate PDF Doc Object"

            // string _FilePath = "";
            string _FolderPath = gloEDocV3Admin.gTemporaryProcessPath + "\\" + _ContainerId.ToString();

            if (_FolderPath != null)
            {
                if (System.IO.Directory.Exists(_FolderPath) == true)
                {
                    System.IO.Directory.Delete(_FolderPath, true);
                }
                System.IO.Directory.CreateDirectory(_FolderPath);
            }


            #endregion


            //Developer: Yatin N.Bhagat
            //Date:01/29/2012
            //Bug ID/PRD Name/Salesforce Case:Salesforce Case No.GLO2010-0009688-Format Provider Signature
            //Reason: If Condition is added to check the Setting to add login user name in the Sign


            //#region "Creating the Temperory Process pathe"
            //string _FFolderPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath;
            //if (_FFolderPath != null)
            //{
            //    if (System.IO.Directory.Exists(_FFolderPath) == false)
            //    {
            //        System.IO.Directory.CreateDirectory(_FFolderPath);
            //    }
            //}
            //#endregion "Creating the Temperory Process pathe"
            //#region "Retrive Binary Image"
            //if (oDB != null)
            //{
            //    oDB.Connect(false);
            //    //Sanjog - Added On 20101011 for login user signature
            //    oParameters.Add("@nPatientID", SignatureId, ParameterDirection.Input, SqlDbType.BigInt);
            //    oParameters.Add("@nFlag", flag, ParameterDirection.Input, SqlDbType.Int);
            //    //Sanjog - Added On 20101011 for login user signaturel
            //    // oParameters.Add("@nPatientID", _PatientId, ParameterDirection.Input, SqlDbType.BigInt);
            //    oDB.Retrive("gsp_eDoc_GetProviderSignature", oParameters, out dt);
            //    if (dt != null)
            //    {
            //        if (dt.Rows.Count > 0)
            //        {
            //            if (dt.Rows[0][0].GetType() != typeof(System.DBNull))
            //            {
            //                byte[] content = null;
            //                content = (byte[])dt.Rows[0][0];
            //                _strSignFileName = _FFolderPath + "\\" + System.Convert.ToString(DateTime.Now.ToFileTime()) + ".bmp";
            //                MemoryStream oDataStream = new MemoryStream(content);
            //                FileStream fileStream = new FileStream(_strSignFileName, FileMode.Create);
            //                oDataStream.WriteTo(fileStream);
            //                fileStream.Flush();
            //                fileStream.Close();
            //                fileStream.Dispose();
            //                _ProviderName = dt.Rows[0]["ProviderName"].ToString();
            //                //_SignetureText =   _ProviderName + " " + DateTime.Now;
            //                _SignetureText = " Document reviewed by " + _ProviderName + " on " + DateTime.Now;
            //                _signaturefound = true;
            //            }
            //        }
            //    }
            //    if (dt != null)
            //    {
            //        dt.Dispose();
            //        dt = null;
            //    }
            //    if (oDB != null)
            //    {
            //        oDB.Dispose();
            //        oDB = null;
            //    }
            //}
            //#endregion

            eDocManager.eDocManager oManager = new eDocManager.eDocManager();
            ArrayList arSignature = oManager.GetSignatureFormat(SignatureId, flag, 0);
            oManager.Dispose();
            oManager = null;
            //Developer: Mitesh Patel
            //Date:02/21/2012
            //Bug ID: 21306
            if (arSignature.Count > 0)
            {
               // MemoryStream oDataStream = new MemoryStream((byte[])arSignature[0]);
                byte[] arrByte = (byte[])arSignature[0];
                FileStream fileStream = new FileStream(arSignature[1].ToString(), FileMode.Create);
                fileStream.Write(arrByte, 0, arrByte.Length);
                //oDataStream.WriteTo(fileStream);
                fileStream.Flush();
                fileStream.Close();
                fileStream.Dispose();

                //SG: Memory Leaks, disposing data stream
                //oDataStream.Flush();
                //oDataStream.Close();
                //oDataStream.Dispose();
                //

                _SignetureText = arSignature[3].ToString();
                _signaturefound = System.Convert.ToBoolean(arSignature[4].ToString());

                if (arSignature.Count > 5)
                {
                    _SignatureFont = System.Convert.ToString(arSignature[5]);
                    _SignatureFontSize = System.Convert.ToInt32(arSignature[6]);
                    _MaxScale = System.Convert.ToDecimal(arSignature[7]);
                    _MinScale = System.Convert.ToDecimal(arSignature[8]);
                }
            }
            ///////

            if (_signaturefound == true)
            {

                //--Putting the text over the image.

                float MySignatureHeight = 0;
                SizeF internalMySize = new SizeF();
                String StringActual = "";
                if (_SignatureFont == "") _SignatureFont = "Arial";
                if (_SignatureFontSize <= 0) _SignatureFontSize = 12;
                if (_MaxScale <= 0) _MaxScale = 0.6M;
                if (_MinScale <= 0) _MinScale = 0.3M;
                System.Drawing.Font ArialFont = new System.Drawing.Font(_SignatureFont, _SignatureFontSize);

                //RectangleF myRectange = SetAspectRatio(pgwidth, pgheight, _strSignFileName, _SignetureText, ArialFont, out MySignatureHeight, out internalMySize, out StringActual);
                //System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(_strSignFileName);
                RectangleF myRectange = SetAspectRatio(pgwidth, pgheight, arSignature[1].ToString(), _SignetureText, ArialFont, out MySignatureHeight, out internalMySize, out StringActual);
                System.Drawing.Bitmap myBitmap = new System.Drawing.Bitmap(arSignature[1].ToString());
                if (myBitmap != null)
                {

                    System.Drawing.Image myImage = new System.Drawing.Bitmap((int)internalMySize.Width, (int)(myRectange.Height + internalMySize.Height));
                    if (myImage != null)
                    {
                        Graphics myGraphics = Graphics.FromImage(myImage);
                        myGraphics.DrawImage(myBitmap, 0, 0, myRectange.Width, myRectange.Height);
                        // myGraphics.DrawImage(myBitmap,0,0);
                        float myStartY = myRectange.Height + MySignatureHeight / 2;

                        int para_run = 0;
                        int para_run_end;
                        int sentence_end = StringActual.Length;
                        while (para_run < sentence_end)
                        {
                            para_run_end = StringActual.IndexOf('\r', para_run);
                            //if (para_run_end < 0) para_run_end = sentence_end - 1;
                            if (para_run_end < 0) para_run_end = sentence_end;
                            if (para_run_end < 0) para_run_end = sentence_end;
                            string para = StringActual.Substring(para_run, para_run_end - para_run);
                            myGraphics.DrawString(para, ArialFont, Brushes.Red, new PointF(0, myStartY));
                            myStartY += MySignatureHeight;
                            para_run = para_run_end + 1;

                        }
                        //----
                        if (myImage != null)
                        {
                            System.Drawing.Image myRotatedImg = RotateAngle(myImage, myRotatedAngle, ref myNewX1, ref myNewY1, ref myNewX2, ref myNewY2, pgwidth, pgheight, System.Convert.ToDouble(_MaxScale), System.Convert.ToDouble(_MinScale));
                            // pdftron.PDF.Image img = pdftron.PDF.Image.Create(doc, (System.Drawing.Bitmap)myImage);
                            pdftron.PDF.Image img = pdftron.PDF.Image.Create(doc, (System.Drawing.Bitmap)myRotatedImg);
                            if (myImage != null)
                            {
                                myImage.Dispose();
                                myImage = null;
                            }

                            MyWidth = img.GetImageWidth();
                            MyHeight = img.GetImageHeight();
                            //Element img_element = b.CreateImage(img, 0, 0, MyWidth, MyHeight);
                            ElementBuilder b = new ElementBuilder();
                            Element img_element = b.CreateImage(img, new Matrix2D(MyWidth, 0, 0, MyHeight, 0, 0));
                            Rect bbox = new Rect(0, 0, MyWidth, MyHeight);
                            img_element.GetBBox(bbox);
                            if (img != null)
                            {
                                //SG: Memory Leaks, performing dispose and then null
                                img.Dispose();
                                img = null;
                            }
                            ElementWriter w = new ElementWriter();
                            w.Begin(doc);
                            w.WritePlacedElement(img_element);
                            //writing the Element to the end of the page.
                            w.WriteElement(b.CreateTextEnd());
                            stm = w.End();
                            // Set the bounding box 
                            //stm.PutRect("BBox", 0, 0, MyWidth, MyHeight);
                            stm.PutRect("BBox", 0, 0, bbox.Width(), bbox.Height());
                            stm.PutName("Subtype", "Form");
                            stm.FindObj("Subtype");
                            w.Dispose();
                            w = null;
                            b.Dispose();
                            b = null;
                            if (myRotatedImg != null) { myRotatedImg.Dispose(); myRotatedImg = null; }
                        }
                        if (myGraphics != null)
                        {
                            myGraphics.Dispose();
                            myGraphics = null;
                        }
                        if (myImage != null)
                        {
                            myImage.Dispose();
                            myImage = null;
                        }
                    }
                    if (myBitmap != null)
                    {
                        myBitmap.Dispose();
                        myBitmap = null;
                    }
                }
                if (ArialFont != null)
                {
                    ArialFont.Dispose();
                    ArialFont = null;
                }

            }

            return stm;

        }

        #region "RotationFucntion"
        private double RotateSignatureImage(Page.Rotate myRotatedPage)
        {
            double angle = 0;
            switch (myRotatedPage)
            {
                case Page.Rotate.e_0:
                    {
                        angle = 0;
                        break;
                    }
                case Page.Rotate.e_90:
                    {
                        angle = 90;
                        break;
                    }
                case Page.Rotate.e_180:
                    {
                        angle = 180;
                        break;
                    }
                case Page.Rotate.e_270:
                    {
                        angle = 270;
                        break;
                    }
            }
            return angle;
        }
        private static System.Drawing.Image RotateAngle(System.Drawing.Image myImage, double angle, ref int myNewX1, ref int myNewY1, ref int myNewX2, ref int myNewY2, double pgWidth, double pgHeight, double MaxScale = 0.3, double MinScale = 1.0)
        {

            int newX = myNewX1, newY = myNewY1;
            int newX2 = myNewX2, newY2 = myNewY2;
            if (myImage != null)
            {
                const double piBy2 = Math.PI / 2.0;

                double oldWidth = (double)myImage.Width;
                double oldHeight = (double)myImage.Height;
                double angleInRadians = 0;
                if (angle != 0) { angleInRadians = ((double)360 - angle) * Math.PI / 180.0; }


                int lowerLeftX, upperRightX, lowerRightY, upperLeftY;
                double cosAngle = Math.Abs(Math.Cos(angleInRadians));
                double sinAngle = Math.Abs(Math.Sin(angleInRadians));

                if ((angleInRadians >= 0.0 && angleInRadians < piBy2) ||
                    (angleInRadians >= Math.PI && angleInRadians < (Math.PI + piBy2)))
                {
                    lowerLeftX = (int)((cosAngle) * oldWidth + 0.49);
                    upperLeftY = (int)((sinAngle) * oldWidth + 0.49);

                    lowerRightY = (int)((cosAngle) * oldHeight + 0.49);
                    upperRightX = (int)((sinAngle) * oldHeight + 0.49);
                }
                else
                {
                    lowerLeftX = (int)((sinAngle) * oldHeight + 0.49);
                    upperLeftY = (int)((cosAngle) * oldHeight + 0.49);

                    lowerRightY = (int)((sinAngle) * oldWidth + 0.49);
                    upperRightX = (int)((cosAngle) * oldWidth + 0.49);
                }



                int rotationWidth = lowerLeftX + upperRightX;
                int rotationHeight = lowerRightY + upperLeftY;


                float maximgBox = Math.Max(rotationHeight, rotationWidth);
                float maxpgBox = Math.Max((float)pgHeight, (float)pgWidth);

                float pdfResolutionPercentage = Math.Max((float)MinScale, (float)Math.Min((float)maximgBox / maxpgBox, (float)MaxScale));
                //float pdfResolutionPercentage = 0.3f;

                float xPercent = (float)(pgWidth * pdfResolutionPercentage);
                float yPercent = (float)(pgHeight * pdfResolutionPercentage);
                float xScale = xPercent / rotationWidth;
                float yScale = yPercent / rotationHeight;
                if (xScale < yScale)
                {
                    yPercent = rotationHeight * xScale;
                }
                else
                {
                    xPercent = rotationWidth * yScale;
                }

                System.Drawing.Point[] rotationPoints;




                if (angleInRadians >= 0.0 && angleInRadians < piBy2)
                {
                    rotationPoints = new System.Drawing.Point[] { new System.Drawing.Point(upperRightX, 0), new System.Drawing.Point(rotationWidth, upperLeftY), new System.Drawing.Point(0, lowerRightY) };
                    newX = upperRightX + (int)((double)myNewX1 * cosAngle - (double)myNewY1 * sinAngle);
                    newY = (int)((double)myNewY1 * cosAngle + (double)myNewX1 * sinAngle);

                    newX2 = newX + (int)xPercent - myNewX1;
                    newY2 = newY + (int)yPercent - myNewY1;



                }
                else if (angleInRadians >= piBy2 && angleInRadians < Math.PI)
                {
                    rotationPoints = new System.Drawing.Point[] { new System.Drawing.Point(rotationWidth, upperLeftY), new System.Drawing.Point(lowerLeftX, rotationHeight), new System.Drawing.Point(upperRightX, 0) };

                    newX = (int)((double)myNewX1 * cosAngle + (double)myNewY1 * sinAngle);
                    newY = (int)(pgWidth + ((double)myNewY1 * cosAngle - (double)myNewX1 * sinAngle));

                    newX2 = newX + (int)xPercent - myNewX1;
                    newY2 = newY + (int)-yPercent - myNewY1;

                }
                else if (angleInRadians >= Math.PI && angleInRadians < (Math.PI + piBy2))
                {
                    rotationPoints = new System.Drawing.Point[] { new System.Drawing.Point(lowerLeftX, rotationHeight), new System.Drawing.Point(0, lowerRightY), new System.Drawing.Point(rotationWidth, upperLeftY) };
                    newX = (int)(pgWidth + ((double)-myNewX1 * cosAngle + (double)myNewY1 * sinAngle));
                    newY = (int)(pgHeight + ((double)-myNewY1 * cosAngle - (double)myNewX1 * sinAngle));
                    newX2 = newX - (int)xPercent + myNewX1;
                    newY2 = newY - (int)yPercent + myNewY1;


                }
                else
                {
                    rotationPoints = new System.Drawing.Point[] { new System.Drawing.Point(0, lowerRightY), new System.Drawing.Point(upperRightX, 0), new System.Drawing.Point(lowerLeftX, rotationHeight) };

                    newX = (int)(pgHeight + ((double)-myNewX1 * cosAngle - (double)myNewY1 * sinAngle));
                    newY = upperLeftY + (int)((double)-myNewY1 * cosAngle + (double)myNewX1 * sinAngle);

                    newX2 = newX - (int)xPercent + myNewX1;
                    newY2 = newY + (int)yPercent - myNewY1;

                }

                Bitmap rotatedBmp = new Bitmap(rotationWidth, rotationHeight);

                using (Graphics g = Graphics.FromImage(rotatedBmp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


                    g.DrawImage(myImage, rotationPoints);
                    //
                    g.Save();

                    //

                }
                myNewX2 = System.Math.Max(newX2, newX);
                myNewY2 = System.Math.Max(newY2, newY);

                myNewX1 = System.Math.Min(newX2, newX);
                myNewY1 = System.Math.Min(newY2, newY);

                return rotatedBmp;

            }
            else
            {
                return myImage;
            }
        }
        #endregion "RotationFucntion"


        static RectangleF SetAspectRatio(Double PageWidth, Double PageHeight, String MyName, String _SignatureString, System.Drawing.Font myFont, out float MySignatureHeight, out SizeF stringSize, out string _resultString)
        {
            RectangleF MyBounds = new RectangleF(2, 2, 2, 2);
            System.Drawing.Bitmap MyImage = new System.Drawing.Bitmap(MyName);
            RectangleF MarginRect = new RectangleF(2, 10, 2, 2);
            //SizeF stringSize = new SizeF();
            stringSize = new SizeF();
            SizeF mySize = new SizeF();
            //String _resultString = "";
            _resultString = "";
            MySignatureHeight = 0;
            int _NoOfLines = 1;
            try
            {

                Graphics myGraphics = Graphics.FromImage(MyImage);

                MyBounds.Height = (float)PageHeight - (2 * MarginRect.Height);
                MyBounds.Width = (float)PageWidth - (2 * MarginRect.Width);

                GraphicsUnit PageUnit = GraphicsUnit.Pixel;
                RectangleF thisImageBound = MyImage.GetBounds(ref PageUnit);

                float SignatureHeight = thisImageBound.Height * (float)350 / MyImage.VerticalResolution;
                float SignatureWidth = thisImageBound.Width * (float)350 / MyImage.HorizontalResolution;


                #region "MeasureString "
                String MeasureSignature = _SignatureString;
                //System.Drawing.Font stringFont = new System.Drawing.Font("Arial",30);

                int counterheight = myFont.Height; //SLR: Changed this 12 to myFont.height...
                int YTextPos1 = (int)PageHeight;

                stringSize = myGraphics.MeasureString(MeasureSignature, myFont);
                #endregion "MeasureString "

                #region "Word wrap code"
                if (stringSize.Width > PageWidth)
                {
                    int para_run = 0;
                    int para_run_end;
                    int sentence_end = MeasureSignature.Length;
                    while (para_run < sentence_end)
                    {
                        para_run_end = MeasureSignature.IndexOf('\r', para_run);
                        //if (para_run_end < 0) para_run_end = sentence_end - 1;
                        if (para_run_end < 0) para_run_end = sentence_end;
                        string para = MeasureSignature.Substring(para_run, para_run_end - para_run);
                        int para_end = para.Length;
                        int text_run = 0;
                        int text_run_end;

                        double para_width = PageWidth;
                        double cur_width = 0;

                        while (text_run < para_end)
                        {

                            text_run_end = para.IndexOf(' ', text_run);
                            if (text_run_end < 0) text_run_end = para_end - 1;

                            string text = para.Substring(text_run, text_run_end - text_run + 1);
                            mySize = myGraphics.MeasureString(text, myFont);

                            if (cur_width + mySize.Width < para_width)
                            {

                                _resultString += text;
                                cur_width += mySize.Width;
                                text_run = text_run_end + 1;


                            }

                            else
                            {

                                if (cur_width == 0)
                                {

                                    while (text_run < text_run_end)
                                    {


                                        string text1 = para.Substring(text_run, 1);
                                        mySize = myGraphics.MeasureString(text1, myFont);
                                        if (cur_width + mySize.Width < para_width)
                                        {

                                            _resultString += text1;
                                            cur_width += mySize.Width;

                                        }

                                        else
                                        {

                                            _resultString += "\r";//New Line
                                            _NoOfLines += 1;
                                            YTextPos1 = YTextPos1 - counterheight;
                                            #region "Page Break Code"
                                            if (YTextPos1 <= 0)
                                            {


                                                YTextPos1 = (int)PageHeight - counterheight;



                                            }

                                            #endregion
                                            text1 = para.Substring(text_run, 1);
                                            _resultString += text1;
                                            mySize = myGraphics.MeasureString(text1, myFont);
                                            cur_width = mySize.Width;



                                        }
                                        text_run++;
                                    }




                                }
                                else
                                {

                                    _resultString += "\r";// New line
                                    _NoOfLines += 1;
                                    cur_width = 0;
                                    YTextPos1 = YTextPos1 - counterheight;

                                    #region "Page Break Code"
                                    if (YTextPos1 <= 0)
                                    {


                                        YTextPos1 = (int)PageHeight - counterheight;

                                    }
                                    #endregion

                                }

                            }



                        }
                        _resultString += "\r";
                        _NoOfLines += 1;
                        YTextPos1 = YTextPos1 - counterheight;
                        para_run = para_run_end + 2;  // {to skip \n)
                        #region "Page Break Code"
                        if (YTextPos1 <= 0)
                        {


                            YTextPos1 = (int)PageHeight - counterheight;


                        }

                        #endregion

                    }
                }
                else
                {
                    _resultString = MeasureSignature;
                }
                #endregion "Word wrap code"

                #region "Scaling Of the image/Text"
                if ((SignatureWidth > MyBounds.Width) || (SignatureHeight > MyBounds.Height))
                {
                    double ScaleX = MyBounds.Width / SignatureWidth;
                    double ScaleY = MyBounds.Height / SignatureHeight;
                    if ((ScaleX > ScaleY))
                    {
                        SignatureHeight = MyBounds.Height;
                        SignatureWidth = SignatureWidth * (float)ScaleY;
                    }
                    else
                    {
                        SignatureWidth = MyBounds.Width;
                        SignatureHeight = SignatureHeight * (float)ScaleX;
                    }
                }
                MySignatureHeight = stringSize.Height;
                stringSize.Height = stringSize.Height * _NoOfLines + MarginRect.Top;

                //SignatureHeight += stringSize.Height ;
                if (stringSize.Width < SignatureWidth)
                {
                    stringSize.Width = SignatureWidth;
                }
                if (stringSize.Width > PageWidth)
                {
                    stringSize.Width = (int)PageWidth;
                }
                MyBounds.Y = 0;
                MyBounds.X = 0;
                MyBounds.Height = SignatureHeight;
                MyBounds.Width = SignatureWidth;
                //Start :: Disposed if the there is large image there is no space in the cache memory
                if (myGraphics != null)
                {
                    myGraphics.Dispose();
                    myGraphics = null;
                }
                //End :: Disposed if the there is large image there is no space in the cache memory
                #endregion "Scaling Of the image/Text"

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (MyImage != null)
                {
                    MyImage.Dispose();
                    MyImage = null;
                }

            }
            return MyBounds;
            //PrintCard(sender, e)
        }

        #endregion "Inserting signature"

        #region "Overload function when watermark is given to the document on saving one at importsplit and one at InsertSignation Page"
        public void InsertWaterMarkToWholeDcoument1(pdftron.PDF.PDFDoc oPDFDoc)
        {

            // string _FileFullPath = "";
            //Obj stm = null; //SG: Memory Leaks, varible not in use commented

            //SG: Memory Leaks, moving variables outside try for disposing in finally
            ElementBuilder builder = null;
            ElementWriter writer = null;
            //

            try
            {


                if (_IsDocumentsLoading == false)
                {

                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int PageCounter = oPDFDoc.GetPageCount();
                        pdftron.PDF.Page page = oPDFDoc.GetPage(PageCounter); ;//setting the page no
                        double pgheight = oPDFDoc.GetPage(PageCounter).GetPageHeight();//setting the page no
                        double pgwidth = oPDFDoc.GetPage(PageCounter).GetPageWidth();


                        //to obtain the dimension of the media box.
                        Rect bbox = page.GetMediaBox();
                        bbox.Normalize();
                        double Width = bbox.Width();
                        double height = bbox.Height();

                        //SG: Memory Leaks, moving declaration outside try block
                        builder = new ElementBuilder();
                        writer = new ElementWriter();
                        //

                        PageIterator itr = oPDFDoc.GetPageIterator();

                        for (; itr.HasNext(); itr.Next())
                        {

                            writer.Begin(itr.Current());
                            Element element = builder.CreateTextBegin(pdftron.PDF.Font.Create(oPDFDoc, pdftron.PDF.Font.StandardType1Font.e_times_roman), pgwidth / 10);
                            writer.WriteElement(element);
                            element = builder.CreateTextRun("Sample");
                            element.SetTextMatrix(3, 0, 0, 5, pgwidth / 20, pgwidth / 2);
                            element.GetGState().SetFillOpacity(0.2);
                            writer.WriteElement(element);
                            writer.WriteElement(builder.CreateTextEnd());
                            writer.End();

                        }
                        Cursor.Current = Cursors.Default;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - End" + " " + DateTime.Now.TimeOfDay);


                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Error" + " " + DateTime.Now.TimeOfDay + "::" + ex.ToString());

                        //MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                //}
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
                //SG: Memory Leaks, disposing varibles
                if (builder != null) { builder.Dispose(); builder = null; }
                if (writer != null) { writer.Dispose(); writer = null; }
            }

        }
        public void InsertWaterMarkToWholeDcoument1(pdftron.PDF.PDFDoc oPDFDoc, pdftron.PDF.Page page)
        {
            //SG: Memory Leaks, moving variables outside try for disposing in finally
            ElementBuilder builder = null;
            ElementWriter writer = null;
            //

            try
            {


                if (_IsDocumentsLoading == false)
                {

                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);

                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        int PageCounter = oPDFDoc.GetPageCount();
                        page = oPDFDoc.GetPage(PageCounter); ;//setting the page no
                        double pgheight = oPDFDoc.GetPage(PageCounter).GetPageHeight();//setting the page no
                        double pgwidth = oPDFDoc.GetPage(PageCounter).GetPageWidth();


                        //to obtain the dimension of the media box.
                        Rect bbox = page.GetMediaBox();
                        bbox.Normalize();
                        double Width = bbox.Width();
                        double height = bbox.Height();

                        //SG: Memory Leaks, moving declaration outside try block
                        builder = new ElementBuilder();
                        writer = new ElementWriter();
                        //

                        writer.Begin(page);
                        Element element = builder.CreateTextBegin(pdftron.PDF.Font.Create(oPDFDoc, pdftron.PDF.Font.StandardType1Font.e_times_roman), pgwidth / 10);
                        writer.WriteElement(element);
                        element = builder.CreateTextRun("Sample");
                        element.SetTextMatrix(3, 0, 0, 5, pgwidth / 20, pgwidth / 2);
                        element.GetGState().SetFillOpacity(0.2);
                        writer.WriteElement(element);
                        writer.WriteElement(builder.CreateTextEnd());
                        writer.End();

                        Cursor.Current = Cursors.Default;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - End" + " " + DateTime.Now.TimeOfDay);


                    }
                    catch (Exception ex)
                    {
                        _ErrorMessage = ex.ToString();
                        AuditLogErrorMessage(_ErrorMessage);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Error" + " " + DateTime.Now.TimeOfDay + "::" + ex.ToString());
                    }

                }
                //}
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
                //SG: Memory Leaks, disposing varibles
                if (builder != null) { builder.Dispose(); builder = null; }
                if (writer != null) { writer.Dispose(); writer = null; }
            }
        }

        #endregion

        #region "Insert the Check mark"
        private void stsannot_Checkmark_Click(object sender, EventArgs e)
        {
            Create_CheckMark();
        }

        private void Create_CheckMark()
        {

            //string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            Obj stm = null;
            int _ConPageNumber = 0;

            //SG: Memory Leaks, moved varibles outside try block to handle dipose in finally
            pdftron.PDF.Page page = null;
            pdftron.SDF.SDFDoc sdfdoc = null;
            Field chk = null;
            pdftron.PDF.Annots.Widget b = null;
            //

            try
            {

                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {

                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);
                            if (c1Documents != null)
                            {
                                #region "Container and Document Information"
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            }
                                #endregion
                            try
                            {

                                page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no
                                double pgheight = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageHeight();
                                double pgwidth = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageWidth();
                                sdfdoc = oPDFDoc.GetSDFDoc();             //obj of the sdf doc
                                stm = oPDFDoc.CreateIndirectDict();
                                chk = oPDFDoc.FieldCreate("chk", Field.Type.e_check, stm);   //creating the field of type signature
                                //pdftron.PDF.Annots.Widget b = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(500, 100, 100, 200), chk); // right corner setting 
                                b = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(pgwidth / 10, pgheight - 100, pgwidth / 5, pgheight - 70), chk); // right corner setting 
                                chk.RefreshAppearance();
                                stm = CreateCheckMarkAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth);
                                temp = b;

                                if (stm != null)
                                {
                                    oPDFView.SetProgressiveRendering(false);                            //to reduce the flickering effect.

                                    b.GetAppearance();
                                    b.SetAppearance(stm, Annot.AnnotationState.e_normal);
                                    page.AnnotPushBack(b);                                              //adding the image to page.
                                    oPDFView.Update();                                                  //required to update
                                    oPDFView.UpdatePageLayout();
                                    oPDFView.Refresh();
                                    oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                                    oPDFView.SetProgressiveRendering(true);
                                }


                            }
                            catch (PDFNetException ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (PDFNetException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
                //SG: Memory Leaks, moved varibles outside try block to handle dipose in finally
                if (b != null) { b.Dispose(); b = null; }
                if (chk != null) { chk = null; }
                if (sdfdoc != null) { sdfdoc.Dispose(); sdfdoc = null; }
                if (page != null) { page = null; }
                if (stm != null) { stm.Dispose(); stm = null; }
                //
            }

        }
        static Obj CreateCheckMarkAppearance(PDFDoc doc, Int64 _PatientId, Int64 _ContainerId, Int64 _DocumentId, double pgheight, double pgwidth)
        {

            #region "variable decalaration"

            Obj stm = null;
            ElementBuilder builder = new ElementBuilder();
            ElementWriter writer = new ElementWriter();
            writer.Begin(doc);
            #endregion

            #region "Generate PDF Doc Object"

            string _FilePath = "";
            string _FolderPath = gloEDocV3Admin.gTemporaryProcessPath + "\\" + _ContainerId.ToString();
            if (_FilePath != "")
            {
                if (System.IO.Directory.Exists(_FolderPath) == true)
                {
                    System.IO.Directory.Delete(_FolderPath, true);
                }
            }
            #endregion

            // Create a checkmark appearance stream ------------------------------------
            writer.WriteElement(builder.CreateTextBegin());

            Element checkmark = builder.CreateTextRun("3", pdftron.PDF.Font.Create(doc, pdftron.PDF.Font.StandardType1Font.e_zapf_dingbats), 0.60);
            writer.WriteElement(checkmark);
            writer.WriteElement(builder.CreateTextEnd());
            stm = writer.End();

            // Set the bounding box
            stm.PutRect("BBox", -0.2, -0.2, 1, 1);
            stm.PutName("Subtype", "Form");
            // Calling Dispose() on ElementReader/Writer/Builder can result in increased performance and lower memory consumption.
            if (writer != null)
            {
                writer.Dispose();
                writer = null;
            }
            if (builder != null)
            {
                builder.Dispose();
                builder = null;
            }
            return stm;

        }
        #endregion "Insert the Check mark"

        #region "Clear All Annots at one time"
        private void FindAnyAnnots()
        {
            if (oPDFView != null)
            {

                #region "Container and Document Information"
                if (c1Documents.RowSel > 0)
                {
                    Int64 _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                    Int64 _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                }
                int _ConPageNumber = 0;
                if (lvwPages.SelectedItems.Count > 0)
                {
                    _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                }
                else
                {
                    MessageBox.Show("Select the Page.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion


                pdftron.PDF.Page page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no

                Obj annot = page.GetAnnots();
                //SG: Memory Leaks, creating temp variable to hold annot obj. to be removed from page
                Annot _annotTobeRemoved = null;
                //

                if (annot != null)
                {
                    for (int i = annot.Size() - 1; i >= 0; --i)
                    {


                        if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Widget")
                        {
                            pdftron.PDF.Field field1 = new Field(annot.GetAt(i));
                            if (field1.IsAnnot() == true)
                            {
                                int num_annot = annot.Size();

                                //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                _annotTobeRemoved = new Annot(annot.GetAt(i));
                                if (_annotTobeRemoved != null)
                                {
                                    page.AnnotRemove(_annotTobeRemoved);
                                    _annotTobeRemoved.Dispose();
                                    _annotTobeRemoved = null;
                                }

                                //

                                oPDFView.Update();
                                oPDFView.Invalidate();
                                oPDFView.Refresh();

                            }
                            field1 = null;

                        }
                        else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Line")
                        {

                            //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                            //page.AnnotRemove(new Annot(annot.GetAt(i)));

                            _annotTobeRemoved = new Annot(annot.GetAt(i));
                            if (_annotTobeRemoved != null)
                            {
                                page.AnnotRemove(_annotTobeRemoved);
                                _annotTobeRemoved.Dispose();
                                _annotTobeRemoved = null;
                            }
                            //
                            oPDFView.Update();
                            oPDFView.Invalidate();
                            oPDFView.Refresh();


                        }
                        else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Square")
                        {
                            //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                            //page.AnnotRemove(new Annot(annot.GetAt(i)));

                            _annotTobeRemoved = new Annot(annot.GetAt(i));
                            if (_annotTobeRemoved != null)
                            {
                                page.AnnotRemove(_annotTobeRemoved);
                                _annotTobeRemoved.Dispose();
                                _annotTobeRemoved = null;
                            }

                            //
                            oPDFView.Update();
                            oPDFView.Invalidate();
                            oPDFView.Refresh();
                        }
                        else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Ink")
                        {
                            //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                            //page.AnnotRemove(new Annot(annot.GetAt(i)));

                            _annotTobeRemoved = new Annot(annot.GetAt(i));
                            if (_annotTobeRemoved != null)
                            {
                                page.AnnotRemove(_annotTobeRemoved);
                                _annotTobeRemoved.Dispose();
                                _annotTobeRemoved = null;
                            }
                            //
                            oPDFView.Update();
                            oPDFView.Invalidate();
                            oPDFView.Refresh();


                        }
                        else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Circle")
                        {
                            //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                            //page.AnnotRemove(new Annot(annot.GetAt(i)));

                            _annotTobeRemoved = new Annot(annot.GetAt(i));
                            if (_annotTobeRemoved != null)
                            {
                                page.AnnotRemove(_annotTobeRemoved);
                                _annotTobeRemoved.Dispose();
                                _annotTobeRemoved = null;
                            }

                            //
                            oPDFView.Update();
                            oPDFView.Invalidate();
                            oPDFView.Refresh();
                        }
                        else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "FreeText") //added for bugid 92623
                        {
                            //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                            //page.AnnotRemove(new Annot(annot.GetAt(i)));

                            _annotTobeRemoved = new Annot(annot.GetAt(i));
                            if (_annotTobeRemoved != null)
                            {
                                page.AnnotRemove(_annotTobeRemoved);
                                _annotTobeRemoved.Dispose();
                                _annotTobeRemoved = null;
                            }

                            //
                            oPDFView.Update();
                            oPDFView.Invalidate();
                            oPDFView.Refresh();
                        }

                        //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                        //if (_annotTobeRemoved != null) { _annotTobeRemoved.Dispose(); _annotTobeRemoved = null; }
                        //
                    }
                }

                //..dispose annot and annots collections
                if (annot != null) { annot.Dispose(); annot = null; }

            }

        }
        private void stsannot_ClearAll_Click_1(object sender, EventArgs e)
        {
            FindAnyAnnots();
        }
        #endregion "Clear All Annots at one time"

        #region "Undo operation"
        private void UndoUsingSingleIteration()
        {
            if (oPDFDoc != null)
            {
                if (oPDFDoc.IsModified() == true)
                {
                    if (oPDFView != null)
                    {

                        #region "Container and Document Information"
                        if (c1Documents.RowSel > 0)
                        {
                            Int64 _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                            Int64 _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                        }
                        int _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                        #endregion


                        pdftron.PDF.Page page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no

                        Obj annot = page.GetAnnots();
                        //SG: Memory Leaks, creating temp variable to hold annot obj. to be removed from page
                        Annot _annotTobeRemoved = null;
                        //

                        if (annot != null)
                        {
                         for(int i = annot.Size() - 1;i>=0;--i) //added for bugid 94145
                         {
                                 if(i>=0)
                                 {

                                if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Line")
                                {

                                    //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                    //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                    _annotTobeRemoved = new Annot(annot.GetAt(i));
                                    if (_annotTobeRemoved != null)
                                    {
                                        page.AnnotRemove(_annotTobeRemoved);
                                        _annotTobeRemoved.Dispose();
                                        _annotTobeRemoved = null;
                                    }

                                    //
                                    oPDFView.Update();
                                    oPDFView.Invalidate();
                                    oPDFView.Refresh();
                                    break;
                                }


                                else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Square")
                                {

                                    //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                    //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                    _annotTobeRemoved = new Annot(annot.GetAt(i));
                                    if (_annotTobeRemoved != null)
                                    {
                                        page.AnnotRemove(_annotTobeRemoved);
                                        _annotTobeRemoved.Dispose();
                                        _annotTobeRemoved = null;
                                    }

                                    //
                                    oPDFView.Update();
                                    oPDFView.Invalidate();
                                    oPDFView.Refresh();
                                    break;
                                }
                                else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Ink")
                                {

                                    //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                    //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                    _annotTobeRemoved = new Annot(annot.GetAt(i));
                                    if (_annotTobeRemoved != null)
                                    {
                                        page.AnnotRemove(_annotTobeRemoved);
                                        _annotTobeRemoved.Dispose();
                                        _annotTobeRemoved = null;
                                    }

                                    //
                                    oPDFView.Update();
                                    oPDFView.Invalidate();
                                    oPDFView.Refresh();
                                    break;


                                }
                                else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Circle")
                                {

                                    //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                    //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                    _annotTobeRemoved = new Annot(annot.GetAt(i));
                                    if (_annotTobeRemoved != null)
                                    {
                                        page.AnnotRemove(_annotTobeRemoved);
                                        _annotTobeRemoved.Dispose();
                                        _annotTobeRemoved = null;
                                    }

                                    //
                                    oPDFView.Update();
                                    oPDFView.Invalidate();
                                    oPDFView.Refresh();
                                    break;
                                }

                                else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "Widget")
                                {
                                    pdftron.PDF.Field field1 = new Field(annot.GetAt(i));

                                    if (field1.IsAnnot() == true)
                                    {

                                        int num_annot = annot.Size();
                                        //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                        //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                        _annotTobeRemoved = new Annot(annot.GetAt(i));
                                        if (_annotTobeRemoved != null)
                                        {
                                            page.AnnotRemove(_annotTobeRemoved);
                                            _annotTobeRemoved.Dispose();
                                            _annotTobeRemoved = null;
                                        }

                                        //
                                        oPDFView.Update();
                                        oPDFView.Invalidate();
                                        oPDFView.Refresh();
                                        field1 = null;
                                        break;

                                    }
                                    field1 = null;

                                }
                                else if (annot.GetAt(i).Get("Subtype").Value().GetName() == "FreeText") //added for bugid 92623
                                {

                                    _annotTobeRemoved = new Annot(annot.GetAt(i));
                                    if (_annotTobeRemoved != null)
                                    {
                                        page.AnnotRemove(_annotTobeRemoved);
                                        _annotTobeRemoved.Dispose();
                                        _annotTobeRemoved = null;
                                    }

                                    //
                                    oPDFView.Update();
                                    oPDFView.Invalidate();
                                    oPDFView.Refresh();
                             //pdftron.PDF.Field field1 = new Field(annot.GetAt(i));

                                    //if (field1.IsAnnot() == true)
                                    //{

                                    //    int num_annot = annot.Size();
                                    //    //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                    //    //page.AnnotRemove(new Annot(annot.GetAt(i)));

                                    //    _annotTobeRemoved = new Annot(annot.GetAt(i));
                                    //    if (_annotTobeRemoved != null)
                                    //    {
                                    //        page.AnnotRemove(_annotTobeRemoved);
                                    //        _annotTobeRemoved.Dispose();
                                    //        _annotTobeRemoved = null;
                                    //    }

                                    //    //
                                    //    oPDFView.Update();
                                    //    oPDFView.Invalidate();
                                    //    oPDFView.Refresh();
                                    //    field1 = null;
                                    //    //break;

                                    //}
                                    //field1 = null;

                                }
                                //SG: Memory Leaks, getting annot to be removed in temp variable and disposing it after use
                                if (_annotTobeRemoved != null) { _annotTobeRemoved.Dispose(); _annotTobeRemoved = null; }
                                //
                                 }
                            }//....for (int i = annot.Size() - 1; i >= 0; --i)

                        }//......if (annot != null)

                        //..dispose annot and annots collections
                        if (annot != null) { annot.Dispose(); annot = null; }
                    }

                }
            }
        }
        private void stsannot_Undo_Click(object sender, EventArgs e)
        {
            UndoUsingSingleIteration();
        }
        #endregion "Undo operation"

        #endregion

        #region"Sign Button Click"
        private void stb_Signature_with_Acknowledgement_Click(object sender, EventArgs e)
        {
            if (AnnotationSaving() == 2)
            {
                return;
            }
            blnSignClick = true;
            InsertSignatureOverNewPage("Acknowledgement");
            blnSignClick = false;
        }
        private void stb_Signature_with_Notes_Click(object sender, EventArgs e)
        {
            if (AnnotationSaving() == 2)
            {
                return;
            }
            blnSignClick = true;
            InsertSignatureOverNewPage("Notes");
            blnSignClick = false;
        }
        private void stb_Signature_with_Acknowledgement_Notes_Click(object sender, EventArgs e)
        {
            //Added on 20100528-For checking Yes-No-Cancel before inserting Signature_with_Acknowledgement_Notes
            if (AnnotationSaving() == 2)
            {
                return;
            }
            blnSignClick = true;
            InsertSignatureOverNewPage("AcknowledgementNotes");
            blnSignClick = false;
        }
        private void stb_Signature_with_Text_Click(object sender, EventArgs e)
        {
            if (AnnotationSaving() == 2)
            {
                return;
            }
            //InsertSignatureOverNewPage("Signature");
            blnSignClick = true;
            InsertSignatureOverNewPage("Signature");
            blnSignClick = false;
        }
        #endregion"Sign Button Click"

        #region "Dhruv 20100623 -> InsertSignatureOverNewPage"


        private void InsertSignatureOverNewPage(string ButtonClicked)
        {
            #region "Variable"
            eDocManager.eDocManager oManager = null;    //cls edocument_Manager

            //SG: Memory Leaks, Moved variable declaration outside try block for handling dipose in finally
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;
            DataTable dt = null;
            //

            #endregion
            //Integrated by Mayuri:20101022-Provider sign
            try
            {
                if (c1Documents != null)
                {
                    if (c1Documents.RowSel >= 0)
                    {
                        //bool _Result = false;

                        //SG: Memory Leaks, Moving variable declaration outside try block for handling dipose in finally
                        //DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                        //

                        string _DocumentIDs = "";
                        oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);
                        if (oSelectedDocuments != null)
                        {
                            if (oSelectedDocuments.Count > 0)
                            {
                                int _CurRow = c1Documents.RowSel;
                                Int64 _ReturnDocumentID = 0;
                                Int64 _ReturnContainerID = 0;
                                string _Notes = txtNotes.Text;
                                Cursor.Current = Cursors.WaitCursor;

                                if (_IsDocumentsLoading == false)
                                {
                                    if (_CurRow >= 0)
                                    {
                                        if (c1Documents.GetData(_CurRow, COL_COLTYPE) != null)
                                        {
                                            if ((enum_DocumentColumnType)c1Documents.GetData(_CurRow, COL_COLTYPE) == enum_DocumentColumnType.Document)
                                            {
                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Insert Signature - Start" + " " + DateTime.Now.TimeOfDay);
                                                if (eDocManager.eDocValidator.IsDocumentExist(oSelectedDocuments[0].DocumentID, _PatientID, gloEDocV3Admin.gClinicID, _OpenExternalSource))
                                                {
                                                    _ReturnDocumentID = oSelectedDocuments[0].DocumentID;
                                                    // _ReturnContainerID = _ReturnDocumentID;
                                                    _ReturnContainerID = oSelectedDocuments[0].Containers[0].ContainerID;

                                                }


                                                bool _signaturefound = false;


                                                Int64 _isProvider = 0;
                                                //Snjog 20101012 for Provider signature 
                                                _isProvider = Int64.Parse(gloEDocV3Admin.GetProviderDetails());

                                                /////////////////////Sanjog For 
                                                if (blnSignClick == false)
                                                {
                                                    bool rslt = false;
                                                    rslt = gloEDocV3Admin.GetSignDelegateStatus();
                                                    oManager = new gloEDocumentV3.eDocManager.eDocManager();

                                                    //SG: Memory Leaks, moving variable declaration outside try block
                                                    //DataTable dt = null;
                                                    //

                                                    bool _IsSignRight = false;
                                                    Int16 i = default(Int16);
                                                    //dt = new DataTable();
                                                    if (_isProvider != 0)
                                                    {
                                                        bool blnResult = false;
                                                        string Pat_Provider = null;
                                                        blnResult = gloEDocV3Admin.CheckpatientProviderStatus(_PatientID, _isProvider);

                                                        if (blnResult)
                                                        {
                                                            //'Selected Provider Is Exam Provider
                                                        }
                                                        else
                                                        {

                                                            Pat_Provider = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                                            Int64 _ProviderID = default(Int64);
                                                            _ProviderID = Int64.Parse(gloEDocV3Admin.GetProviderStatus(_PatientID));
                                                            dt = oManager.GetAllAssignProviders(gloEDocV3Admin.gUserID);
                                                            if (dt.Rows.Count == 0)
                                                            {
                                                                {
                                                                    MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    return;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (dt.Rows.Count > 0)
                                                                {
                                                                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                                                                    {
                                                                        if (_ProviderID.ToString() == dt.Rows[i]["nProviderId"].ToString().Trim())
                                                                        {
                                                                            _IsSignRight = true;
                                                                            break;
                                                                        }
                                                                    }
                                                                    if (_IsSignRight == false)
                                                                    {
                                                                        //MessageBox.Show("Current user is not designated as a Signature Delegate for selected patient's provider. Signature delegates may be added in Provider setup in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                        string strName = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                                                        MessageBox.Show("User '" + gloEDocV3Admin.gUserName + "' is not designated as a Signature Delegate for '" + strName + "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                        strName = null;
                                                                        return;
                                                                    }
                                                                }
                                                            }

                                                        }

                                                    }
                                                    else
                                                    {
                                                        Int64 _ProviderID = default(Int64);
                                                        _ProviderID = Int64.Parse(gloEDocV3Admin.GetProviderStatus(_PatientID));


                                                        //gloEDocumentV3.eDocManager.eDocManager omanager = new gloEDocumentV3.eDocManager.eDocManager();
                                                        dt = oManager.GetAllAssignProviders(gloEDocV3Admin.gUserID);
                                                        if (dt.Rows.Count == 0)
                                                        {
                                                            //if (_ProviderID != gnLoginProviderID)
                                                            {
                                                                MessageBox.Show("You are not designated as a Signature Delegate for any providers.  No provider signatures are available for you to use. Signature delegates may be added in Provider setup in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                return;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (dt.Rows.Count > 0)
                                                            {
                                                                for (i = 0; i <= dt.Rows.Count - 1; i++)
                                                                {
                                                                    if (_ProviderID.ToString() == dt.Rows[i]["nProviderId"].ToString().Trim())
                                                                    {
                                                                        _IsSignRight = true;
                                                                    }
                                                                }
                                                                if (_IsSignRight == false)
                                                                {
                                                                    //MessageBox.Show("Current user is not designated as a Signature Delegate for selected patient's provider. Signature delegates may be added in Provider setup in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    string strName = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                                                    MessageBox.Show("User '" + gloEDocV3Admin.gUserName + "' is not designated as a Signature Delegate for '" + strName + "'. Signature Delegates may be assigned via the ‘Provider’ option in gloEMR Admin.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                    strName = null;
                                                                    return;
                                                                }
                                                            }
                                                        }

                                                    }


                                                }
                                                //////////////////////sanjog


                                                UnloadDocuments();//Dhruv 20100622
                                                //UnloadDocView();

                                                _IsDocumentsLoading = true;
                                                Application.DoEvents();
                                                #region "Wait Process"
                                                if (oProcessLabel != null)
                                                {
                                                    if (pnlPreview.Controls.Contains(oProcessLabel))
                                                    {
                                                        pnlPreview.Controls.Remove(oProcessLabel);

                                                    }
                                                    oProcessLabel.Dispose(); oProcessLabel = null;
                                                }
                                                oProcessLabel = new Label();
                                                pnlPreview.Controls.Add(oProcessLabel);
                                                oProcessLabel.Dock = DockStyle.Fill;
                                                //oProcessLabel.Image = Properties.Resources.Wait;
                                                //oProcessLabel.ImageAlign = ContentAlignment.MiddleCenter;
                                                oProcessLabel.Location = new System.Drawing.Point(0, 0);
                                                oProcessLabel.ForeColor = Color.Blue;
                                                //oProcessLabel.ForeColor = System.Drawing.Color.FromArgb(75, 175, 253);
                                                oProcessLabel.Font = new System.Drawing.Font("Verdana", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                                                oProcessLabel.TextAlign = ContentAlignment.MiddleCenter;
                                                //oProcessLabel.Text = Environment.NewLine + Environment.NewLine + Environment.NewLine + Environment.NewLine + "Please wait !!!";
                                                oProcessLabel.Text = "Please wait !!!";
                                                oProcessLabel.Name = "lblProcess";
                                                oProcessLabel.Visible = true;
                                                oProcessLabel.BringToFront();

                                                #endregion
                                                Application.DoEvents();
                                                Cursor.Current = Cursors.WaitCursor;//added wait cursor



                                                if (oManager == null) //Checking for the Manager class
                                                {
                                                    oManager = new gloEDocumentV3.eDocManager.eDocManager();
                                                }




                                                if (ButtonClicked == "AcknowledgementNotes")
                                                {
                                                    //Snjog 20101012 for Provider signature 
                                                    if (_isProvider > 0)
                                                    {
                                                        oManager.InsertSignInPDFDocWithNotesAndAcknowledgement(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, _isProvider, 2, out _signaturefound, _Notes, _OpenExternalSource);
                                                    }
                                                    else
                                                    {
                                                        oManager.InsertSignInPDFDocWithNotesAndAcknowledgement(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, gloEDocV3Admin.gUserID, 1, out _signaturefound, _Notes, _OpenExternalSource);
                                                    }
                                                }
                                                else if (ButtonClicked == "Acknowledgement")
                                                {
                                                    if (_isProvider > 0)
                                                    {
                                                        oManager.InsertSignInPDFDocWithAcknowledgement(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, _isProvider, 2, out _signaturefound, _Notes, _OpenExternalSource);
                                                    }
                                                    else
                                                    {
                                                        oManager.InsertSignInPDFDocWithAcknowledgement(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, gloEDocV3Admin.gUserID, 1, out _signaturefound, _Notes, _OpenExternalSource);
                                                    }
                                                }
                                                else if (ButtonClicked == "Notes")
                                                {
                                                    //Snjog 20101012 for Provider signature 
                                                    if (_isProvider > 0)
                                                    {
                                                        oManager.InsertSignInPDFDocWithNotes(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, _isProvider, 2, out _signaturefound, _Notes, _OpenExternalSource);
                                                    }
                                                    else
                                                    {
                                                        oManager.InsertSignInPDFDocWithNotes(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, gloEDocV3Admin.gUserID, 1, out _signaturefound, _Notes, _OpenExternalSource);
                                                    }
                                                }
                                                else if (ButtonClicked == "Signature")
                                                {
                                                    //Snjog 20101012 for Provider signature 
                                                    if (_isProvider > 0)
                                                    {
                                                        oManager.InsertSignInPDFDocWithText(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, _isProvider, 2, out _signaturefound, _OpenExternalSource);
                                                    }
                                                    else
                                                    {
                                                        oManager.InsertSignInPDFDocWithText(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, gloEDocV3Admin.gUserID, 1, out _signaturefound, _OpenExternalSource);
                                                    }
                                                }
                                                else if (ButtonClicked == "ProviderSignature")
                                                {
                                                    oManager.InsertSignInPDFDocWithText(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, _ProviderID, 2, out _signaturefound, _OpenExternalSource);
                                                }
                                                else if (ButtonClicked == "PatientProviderSignature")
                                                {
                                                    oManager.InsertSignInPDFDocWithText(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, _PatientID, 0, out _signaturefound, _OpenExternalSource);
                                                }
                                                else if (ButtonClicked == "UserSignature")
                                                {
                                                    oManager.InsertSignInPDFDocWithText(_PatientID, _ReturnDocumentID, _ReturnContainerID, oSelectedDocuments, gloEDocV3Admin.gUserID, 1, out _signaturefound, _OpenExternalSource);
                                                }
                                                if (oManager != null)
                                                {
                                                    oManager.Dispose();
                                                    oManager = null;
                                                }

                                                #region "Refresh Events"
                                                if (_ReturnContainerID > 0 && _ReturnDocumentID > 0)
                                                {
                                                    #region "Fill Documents"
                                                    lvwPages.BeginUpdate();
                                                    lvwPages.Items.Clear();
                                                    lvwPages.EndUpdate();
                                                    #endregion
                                                    //Sanjog - Added On 2011 Aug 9 to get page count of Document to bind this value to C1document grid
                                                    gloEDocumentV3.eDocManager.eDocManager oMan = new gloEDocumentV3.eDocManager.eDocManager();
                                                    Int32 PagCnt = oMan.GetPageCount(_ReturnDocumentID, _OpenExternalSource);
                                                    if (oMan != null)
                                                    {
                                                        oMan.Dispose();
                                                        oMan = null;
                                                    }
                                                    LoadDocument(_ReturnDocumentID, _ReturnContainerID, _OpenExternalSource);
                                                    c1Documents.SetData(c1Documents.RowSel, COL_PAGENUMBERS, PagCnt);
                                                    //Sanjog - Added On 2011 Aug 9 to get page count of Document to bind this value to C1document grid
                                                    SelectDocumentInGrid(_ReturnDocumentID, _ReturnContainerID);

                                                    _IsDocumentsLoading = false;

                                                    #region "Enabled Buttons"
                                                    if (tsb_Acknowledge.Enabled == false) { tsb_Acknowledge.Enabled = true; }
                                                    if (tsb_Delete.Enabled == false && _OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument) { tsb_Delete.Enabled = true; } // _OpenEDocumentAs CONDITION BY SUDHIR 20090626 //
                                                    if (tsb_InsertSign1.Enabled == false) { tsb_InsertSign1.Enabled = true; }
                                                    if (tsb_Print.Enabled == false) { tsb_Print.Enabled = true; }
                                                    if (tsb_Fax.Enabled == false) { tsb_Fax.Enabled = true; }
                                                    if (tsb_AddNote.Enabled == false) { tsb_AddNote.Enabled = true; }
                                                    if (tsb_AddTags.Enabled == false) { tsb_AddTags.Enabled = true; }
                                                    if (tsb_RotateBack.Enabled == false) { tsb_RotateBack.Enabled = true; }
                                                    if (tsb_RotateForward.Enabled == false) { tsb_RotateForward.Enabled = true; }
                                                    if (tsb_Refresh.Enabled == false) { tsb_Refresh.Enabled = true; }
                                                    if (tsb_Search.Enabled == false) { tsb_Search.Enabled = true; }
                                                    if (tsb_History.Enabled == false) { tsb_History.Enabled = true; }
                                                    #endregion

                                                }

                                                if (_signaturefound == false)
                                                {
                                                    if (ButtonClicked == "ProviderSignature")
                                                    {

                                                        MessageBox.Show("Selected Provider has no signature on file. Electronic signature cannot be added.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                    else if (ButtonClicked == "PatientProviderSignature")
                                                    {
                                                        MessageBox.Show("Patient Provider has no signature on file. Electronic signature cannot be added.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                    else if (ButtonClicked == "UserSignature")
                                                    {
                                                        MessageBox.Show("Current user has no signature on file. Electronic signature cannot be added.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                }
                                                #endregion
                                                tsb_Close.Enabled = true;
                                                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Insert Signature - End" + " " + DateTime.Now.TimeOfDay);
                                            }
                                        }


                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select Page to add signature.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            Cursor.Current = Cursors.Default;
                        }

                    }
                }
            }

            catch
            {

            }
            finally
            {
                if (oManager != null) { oManager.Dispose(); oManager = null; }
                //SG:Memory Leaks, disposing local objects
                if (oSelectedDocuments != null) { oSelectedDocuments.Clear(); oSelectedDocuments.Dispose(); oSelectedDocuments = null; }
                if (dt != null) { dt.Dispose(); dt = null; }
                //
            }
        }
        #endregion "Dhruv 20100623 -> InsertSignatureOverNewPage"



        //Added on 20100527
        int _Result = 2;
        private void c1Documents_BeforeRowColChange(object sender, C1.Win.C1FlexGrid.RangeEventArgs e)
        {
            if (_Isgrid == true)
            {
                _Isgrid = false;
                _Result = AnnotationSaving();
                if (_Result == 2)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
            else
            {
                //if (AnnotationSaving() != 0)
                //{
                _Result = AnnotationSaving();
                if (_Result == 2)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
                //}
            }

        }


        #region "Dhruv 20100623 -> lvPagesMouseClick"

        private void lvwPages_MouseClick(object sender, MouseEventArgs e)
        {
            if (lvwPages != null)
            {
                if (_IsPagesClick == true)
                {
                    lvwPages.ContextMenuStrip = null;
                    return;
                }
                if (e.Button == MouseButtons.Right && _IsDocumentsLoading == false)
                {
                    if (lvwPages.ContextMenuStrip != null) { gloGlobal.cEventHelper.RemoveAllEventHandlers(lvwPages.ContextMenuStrip); lvwPages.ContextMenuStrip.Dispose(); }


                    if (lvwPages.SelectedItems.Count == 0)
                    {
                        if (lvwPages.HitTest(e.X, e.Y).Item != null)
                        {
                            lvwPages.HitTest(e.X, e.Y).Item.Selected = true;
                        }
                        else
                        {
                            if (lvwPages.Items.Count > 0)
                            {
                                lvwPages.Items[0].Selected = true;
                            }
                        }

                    }
                    else if (lvwPages.SelectedItems.Count == 1)
                    {
                        if (lvwPages.HitTest(e.X, e.Y).Item != null)
                        {
                            lvwPages.SelectedItems.Clear();
                            lvwPages.HitTest(e.X, e.Y).Item.Selected = true;
                        }
                    }
                    if (lvwPages.Items.Count > 0)
                    {
                        //FillContextMenu_RCM(_PatientID, gloEDocV3Admin.gClinicID, _SelectedYear, true);

                        if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                        { FillContextMenu_RCM(_PatientID, gloEDocV3Admin.gClinicID, _SelectedYear, true); }
                        else
                        { FillContextMenu(_PatientID, gloEDocV3Admin.gClinicID, _SelectedYear, true); }

                    }
                }
            }

        }
        #endregion "Dhruv 20100623 -> lvPagesMouseClick"


        #region "Dhruv 20100622 -> On FormUnloadDocView + OnformClosing + AuditLogErrorMessage"
        private void UnloadDocView()
        {
            if (oPDFView != null)
            {
                if (pnlPreview.Controls.Contains(oPDFView) == true)
                {
                    pnlPreview.Controls.Remove(oPDFView);
                }
                try
                {
                    oPDFView.MouseWheel -= new MouseEventHandler(oPDFView_MouseWheel);
                    oPDFView.MouseDown -= new MouseEventHandler(oPDFView_MouseDown);
                    oPDFView.MouseUp -= new MouseEventHandler(oPDFView_MouseUp);
                }
                catch
                {
                }

            }
            if (oPDFView != null)
            {
                pdftron.PDF.PDFDoc oPDFDoc = oPDFView.GetDoc();

                if (oPDFDoc != null)
                {
                    oPDFDoc.Dispose();
                    oPDFDoc = null;
                }
                if (oPDFView != null)
                {
                    oPDFView.Dispose();
                    oPDFView = null;
                }
            }

        }
        private void OnFormsClosing()
        {
            if (oPDFHandFreeCursor != null)
            {
                oPDFHandFreeCursor.Dispose();
                oPDFHandFreeCursor = null;

            }

            //SG: Memory Leaks, free resources
            if (MsHandFree != null)
            {
                MsHandFree.Flush();
                MsHandFree.Close();
                MsHandFree.Dispose();
                MsHandFree = null;
            }

            if (oPDFHandHoldCursor != null)
            {
                oPDFHandHoldCursor.Dispose();
                oPDFHandHoldCursor = null;
            }

            //SG: Memory Leaks, free resources
            if (MsHandHold != null)
            {
                MsHandHold.Flush();
                MsHandHold.Close();
                MsHandHold.Dispose();
                MsHandHold = null;
            }

            if (oButtonToolTip != null)
            {
                oButtonToolTip.Dispose();
                oButtonToolTip = null;

            }
            if (oProcessLabel != null)
            {
                oProcessLabel.Dispose();
                oProcessLabel = null;
            }
            if (oNoteToolTip != null)
            {
                oNoteToolTip.Dispose();
                oNoteToolTip = null;
            }

            //SG: Memory Leaks, free resources
            if (ogloSettings != null)
            {
                ogloSettings.Dispose();
                ogloSettings = null;
            }

        }
        private void AuditLogErrorMessage(string _ErrorMessage)
        {
            string _MessageString = "";
            if (_ErrorMessage.Trim() != "")
            {
                _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                _MessageString = "";
            }


        }
        #endregion "Dhruv 20100622 -> On FormUnloadDocView + OnformClosing"


        #region "Dhruv 20100624 - while clicking on the toolstrip it required 2 time clicking"
        private void tls_MaintainDoc_MouseHover(object sender, EventArgs e)
        {
            //Resolved Bug # : 38872
            try
            {
                this.Focus();
            }
            catch (Exception)
            {
            }

        }
        #endregion "Dhruv 20100624 - while clicking on the toolstrip it required 2 time clicking"

        private void tsb_InsertSign_ButtonClick(object sender, EventArgs e)
        {
            if (AnnotationSaving() == 2)
            {
                return;
            }
            blnSignClick = true;
            InsertSignatureOverNewPage("Signature");
            blnSignClick = false;
        }


        public void ShowAsPanel()
        {
            tls_MaintainDoc.Visible = false;
            pnlDocumentsLegends.Visible = false;
            pnlNotes.Visible = false;
        }
        private void stsannot_ProviderSign_Click(object sender, EventArgs e)
        {
            //sanjog 2011 Jan 28 to show drop down if present
            try
            {
                if (stsannot_ProviderSign.DropDown.Items.Count == 0)
                {
                    //patinet Provider Siganture will Insert 
                    if (AnnotationSaving() == 2)
                    {
                        return;
                    }
                    _isAnnotationPrvClick = true;
                    // InsertSignatureOverNewPage("PatientProviderSignature");
                    stsannot_Signature_Click(sender, e);
                    _isAnnotationPrvClick = false;
                }
                else
                {
                    tsb_Annotate.ShowDropDown();
                }
            }
            catch (Exception ex)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //private void tsb_ProviderSign_ButtonClick(object sender, EventArgs e)
        //{

        //}

        private void AddChildMenu()
        {
            try
            {
                if (gloEDocV3Admin.gblnAssociatedProvider == true)
                {
                    Boolean rstl = gloEDocV3Admin.GetSignDelegateStatus();

                    if (rstl == true)
                    {
                        ToolStripMenuItem oMenuItem;

                        //SG: Memory Leaks, dettach events and dispose individual items
                        if (tsb_ProviderSign.DropDownItems != null && tsb_ProviderSign.DropDownItems.Count > 0)
                        {
                            for (int mnuItemIndex = tsb_ProviderSign.DropDownItems.Count - 1; mnuItemIndex >= 0; mnuItemIndex--)
                            {
                                try
                                {
                                    tsb_ProviderSign.DropDownItems[mnuItemIndex].Click -= new EventHandler(InsertSign);
                                    tsb_ProviderSign.DropDownItems[mnuItemIndex].Dispose();
                                }
                                catch //(Exception ex)
                                { }//blank catch


                            }

                            tsb_ProviderSign.DropDownItems.Clear();
                        }

                        //SG: Memory Leaks, new not needed
                        //DataTable dt = new DataTable();
                        DataTable dt = null;
                        //

                        //added sanjog on 2011 Jan 28
                        Int64 LoginPrvId = 0;
                        Int64 PatprvID = 0;
                        //added sanjog on 2011 Jan 28
                        gloEDocumentV3.eDocManager.eDocManager omanager = new gloEDocumentV3.eDocManager.eDocManager();
                        if (omanager != null)
                        {
                            dt = omanager.GetAllAssignProviders(gloEDocV3Admin.gUserID);
                            //added sanjog on 2011 Jan 28
                            LoginPrvId = omanager.GetPatientProviderRights(gloEDocV3Admin.gUserID);
                            PatprvID = System.Convert.ToInt64(gloEDocV3Admin.GetProviderStatus(_PatientID));
                            //added sanjog on 2011 Jan 28
                            //omanager.get
                            omanager.Dispose();
                            omanager = null;
                            if (dt != null)
                            {
                                bool prvPresent = false;
                                string ptProvider = "";
                                ptProvider = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                //string exmProvider ="";

                                Int32 i;
                                for (i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (ptProvider.Trim() == dt.Rows[i][1].ToString().Trim())
                                    {
                                        prvPresent = true;
                                        break;
                                    }
                                }
                                if (prvPresent == true)
                                {
                                    oMenuItem = new ToolStripMenuItem();
                                    oMenuItem.Text = "Patient Provider : " + dt.Rows[i][1].ToString();
                                    oMenuItem.Tag = dt.Rows[i][0].ToString();
                                    oMenuItem.Font = gloGlobal.clsgloFont.gFont;// new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                    oMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                                    tsb_ProviderSign.DropDownItems.Add(oMenuItem);
                                    oMenuItem.Click += new EventHandler(InsertSign);
                                    oMenuItem = null;
                                }
                                //added sanjog on 2011 Jan 28
                                else
                                {
                                    if (LoginPrvId == PatprvID)
                                    {
                                        oMenuItem = new ToolStripMenuItem();
                                        oMenuItem.Text = "Patient Provider : " + ptProvider;
                                        oMenuItem.Tag = PatprvID;
                                        oMenuItem.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        tsb_ProviderSign.DropDownItems.Add(oMenuItem);
                                        oMenuItem.Click += new EventHandler(InsertSign);
                                        oMenuItem = null;
                                    }
                                }
                                //added sanjog on 2011 Jan 28

                                for (i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (ptProvider.Trim() != dt.Rows[i][1].ToString().Trim())
                                    {
                                        oMenuItem = new ToolStripMenuItem();
                                        oMenuItem.Text = dt.Rows[i][1].ToString();
                                        oMenuItem.Tag = dt.Rows[i][0];
                                        oMenuItem.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        tsb_ProviderSign.DropDownItems.Add(oMenuItem);
                                        oMenuItem.Click += new EventHandler(InsertSign);
                                        oMenuItem = null;
                                    }
                                }
                            }
                        }

                        //SG: Memory Leaks, disposing temp teble
                        if (dt != null) { dt.Dispose(); dt = null; }
                        //
                    }

                }

            }
            catch (Exception ex)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void AddChildMenuAnnot()
        {
            try
            {
                if (gloEDocV3Admin.gblnAssociatedProvider == true)
                {
                    Boolean rstl = gloEDocV3Admin.GetSignDelegateStatus();

                    if (rstl == true)
                    {
                        ToolStripMenuItem oMenuItem;

                        //SG: Memory Leaks, dettach events and dispose individual items
                        if (stsannot_ProviderSign.DropDownItems != null && stsannot_ProviderSign.DropDownItems.Count > 0)
                        {
                            for (int mnuItemIndex = stsannot_ProviderSign.DropDownItems.Count - 1; mnuItemIndex >= 0; mnuItemIndex--)
                            {
                                try
                                {
                                    stsannot_ProviderSign.DropDownItems[mnuItemIndex].Click -= new EventHandler(InsertSignAnnot);
                                    stsannot_ProviderSign.DropDownItems[mnuItemIndex].Dispose();
                                }
                                catch //(Exception ex)
                                { } //blank catch

                            }

                            stsannot_ProviderSign.DropDownItems.Clear();
                        }

                        //SG: Memory Leaks, new not needed here
                        //DataTable dt = new DataTable();
                        DataTable dt = null;
                        //

                        //added sanjog on 2011 Jan 28
                        Int64 LoginPrvId = 0;
                        Int64 PatprvID = 0;
                        //added sanjog on 2011 Jan 28
                        gloEDocumentV3.eDocManager.eDocManager omanager = new gloEDocumentV3.eDocManager.eDocManager();
                        if (omanager != null)
                        {
                            dt = omanager.GetAllAssignProviders(gloEDocV3Admin.gUserID);
                            //added sanjog on 2011 Jan 28
                            LoginPrvId = omanager.GetPatientProviderRights(gloEDocV3Admin.gUserID);
                            PatprvID = System.Convert.ToInt64(gloEDocV3Admin.GetProviderStatus(_PatientID));
                            //added sanjog on 2011 Jan 28
                            omanager.Dispose();
                            omanager = null;
                            if (dt != null)
                            {
                                bool prvPresent = false;
                                string ptProvider = "";
                                ptProvider = gloEDocV3Admin.GetPatientProviderName(_PatientID);
                                //string exmProvider ="";

                                Int32 i;
                                for (i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (ptProvider.Trim() == dt.Rows[i][1].ToString().Trim())
                                    {
                                        prvPresent = true;
                                        break;
                                    }
                                }
                                if (prvPresent == true)
                                {
                                    oMenuItem = new ToolStripMenuItem();
                                    oMenuItem.Text = "Patient Provider : " + dt.Rows[i][1].ToString();
                                    oMenuItem.Tag = dt.Rows[i][0].ToString();
                                    oMenuItem.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                    oMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                                    stsannot_ProviderSign.DropDownItems.Add(oMenuItem);
                                    oMenuItem.Click += new EventHandler(InsertSignAnnot);
                                    oMenuItem = null;
                                }
                                //added sanjog on 2011 Jan 28
                                else
                                {
                                    if (LoginPrvId == PatprvID)
                                    {
                                        oMenuItem = new ToolStripMenuItem();
                                        oMenuItem.Text = "Patient Provider : " + ptProvider;
                                        oMenuItem.Tag = PatprvID;
                                        oMenuItem.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        stsannot_ProviderSign.DropDownItems.Add(oMenuItem);
                                        oMenuItem.Click += new EventHandler(InsertSignAnnot);
                                        oMenuItem = null;
                                    }
                                }
                                //added sanjog on 2011 Jan 28
                                for (i = 0; i < dt.Rows.Count; i++)
                                {
                                    if (ptProvider.Trim() != dt.Rows[i][1].ToString().Trim())
                                    {
                                        oMenuItem = new ToolStripMenuItem();
                                        oMenuItem.Text = dt.Rows[i][1].ToString();
                                        oMenuItem.Tag = dt.Rows[i][0];
                                        oMenuItem.Font = new System.Drawing.Font("Tahoma", 9, FontStyle.Regular);
                                        oMenuItem.ForeColor = Color.FromArgb(31, 73, 125);
                                        stsannot_ProviderSign.DropDownItems.Add(oMenuItem);
                                        oMenuItem.Click += new EventHandler(InsertSignAnnot);
                                        oMenuItem = null;
                                    }
                                }

                            }
                        }

                        //SG: Memory Leaks, diposing temp table
                        if (dt != null) { dt.Dispose(); dt = null; }
                        //
                    }
                }
            }
            catch (Exception ex)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InsertSign(object sender, EventArgs args)
        {
            try
            {
                ToolStripMenuItem oProviderMenu = (ToolStripMenuItem)sender;
                _ProviderID = Int64.Parse(oProviderMenu.Tag.ToString());
                Int64 prvID = Int64.Parse(gloEDocV3Admin.GetProviderStatus(_PatientID));
                if (prvID == _ProviderID)
                {
                    blnSignClick = true;
                    InsertSignatureOverNewPage("ProviderSignature");
                    blnSignClick = false;
                }
                else
                {
                    DocumentContextMenu.eContextDocuments oSelectedDocuments = null;//new gloEDocumentV3.DocumentContextMenu.eContextDocuments();
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);
                    if (oSelectedDocuments != null)
                    {
                        if (oSelectedDocuments.Count > 0)
                        {
                            string SelectedPrv = gloEDocV3Admin.GetProviderName(_ProviderID);
                            string PatientPrv = gloEDocV3Admin.GetProviderName(prvID);
                            DialogResult dResult;
                            dResult = MessageBox.Show("Patient provider '" + PatientPrv + "' does not match provider selected for signature '" + SelectedPrv + "'.  Would you like to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dResult == DialogResult.Yes)
                            {
                                blnSignClick = true;
                                InsertSignatureOverNewPage("ProviderSignature");
                                blnSignClick = false;
                            }
                            else
                            {
                                if (oSelectedDocuments != null)
                                {
                                    oSelectedDocuments.Dispose();
                                    oSelectedDocuments = null;
                                }
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select Page to add signature.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    if (oSelectedDocuments != null)
                    {
                        oSelectedDocuments.Dispose();
                        oSelectedDocuments = null;
                    }

                }
            }
            catch (Exception ex)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void InsertSignAnnot(object sender, EventArgs e)
        {
            //SG: Memory Leaks, Moved declaration outside try block for handling dispose in finally
            DocumentContextMenu.eContextDocuments oSelectedDocuments = null;
            //

            try
            {

                if (AnnotationSaving() == 2)
                {
                    return;
                }
                ToolStripMenuItem oProviderMenu = (ToolStripMenuItem)sender;
                _ProviderID = Int64.Parse(oProviderMenu.Tag.ToString());
                Int64 prvID = Int64.Parse(gloEDocV3Admin.GetProviderStatus(_PatientID));
                if (prvID == _ProviderID)
                {
                    _isAnnotationPrvClick = true;
                    _isAnnotationChildPrvClick = true;
                    stsannot_Signature_Click(sender, e);
                    _isAnnotationChildPrvClick = false;
                    _isAnnotationPrvClick = false;
                }
                else
                {
                    //SG: Memory Leaks, Moving declaration outside try block for handling dispose in finally
                    //DocumentContextMenu.eContextDocuments oSelectedDocuments = null;
                    //
                    string _DocumentIDs = "";
                    oSelectedDocuments = GetSelectedDocuments(out _DocumentIDs);
                    if (oSelectedDocuments != null)
                    {
                        if (oSelectedDocuments.Count > 0)
                        {
                            string SelectedPrv = gloEDocV3Admin.GetProviderName(_ProviderID);
                            string PatientPrv = gloEDocV3Admin.GetProviderName(prvID);
                            DialogResult dResult;
                            dResult = MessageBox.Show("Patient provider '" + PatientPrv + "' does not match provider selected for signature '" + SelectedPrv + "'.  Would you like to continue?", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if (dResult == DialogResult.Yes)
                            {
                                _isAnnotationPrvClick = true;
                                _isAnnotationChildPrvClick = true;
                                stsannot_Signature_Click(sender, e);
                                _isAnnotationChildPrvClick = false;
                                _isAnnotationPrvClick = false;
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select Page to add signature.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(ex.ToString());
                MessageBox.Show(ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //SG: Memory Leaks, diposing temp objects
                if (oSelectedDocuments != null) { oSelectedDocuments.Dispose(); oSelectedDocuments = null; }
                //
            }
        }

        private void tsb_ProviderSign_ButtonClick(object sender, EventArgs e)
        {

        }

        private void tsb_ProviderSign_ButtonClick_1(object sender, EventArgs e)
        {
            if (AnnotationSaving() == 2)
            {
                return;
            }

            InsertSignatureOverNewPage("PatientProviderSignature");

        }

        private void bwLoadDocument_DoWork(object sender, DoWorkEventArgs e)
        {

            bwLoadDocument.CancelAsync();

            if (bwLoadDocument.CancellationPending)
            {
                ArrayList alDetails = (ArrayList)e.Argument;
                eDocManager.eDocGetList oList = new gloEDocumentV3.eDocManager.eDocGetList();
                string strFileName;

                strFileName=System.Convert.ToString(alDetails[2]);

                oList.GetContainerStream(System.Convert.ToInt64(alDetails[0]), System.Convert.ToInt64(alDetails[1]), gloEDocV3Admin.gClinicID, ref strFileName, _OpenExternalSource);
                oList.Dispose();
                oList = null;
                ArrayList alResult = new ArrayList();
                alResult.Add(alDetails[0]);
                alResult.Add(alDetails[2]);
                e.Result = alResult;
            }
        }

        private void bwLoadDocument_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (oProcessLabel != null)
            {
                oProcessLabel.Text += "   " + e.ProgressPercentage + "%";
            }
        }

        private void bwLoadDocument_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool _DocumentLoadedFromDatabase = false;

            Int64 DocumentID = System.Convert.ToInt64(((ArrayList)e.Result)[0]);
            string sFilePath = System.Convert.ToString(((ArrayList)e.Result)[1]);

            _DocumentLoadedFromDatabase = System.IO.File.Exists(sFilePath);

            if (_DocumentLoadedFromDatabase == true)
            {
                try
                {

                    if (oPDFView == null)
                    {
                        //oPDFView = new pdftron.PDF.PDFViewCtrl();
                        oPDFView = new MyPDFView(this, 0);
                    }
                    pdftron.PDF.PDFDoc oldDoc = oPDFView.GetDoc();
                    if (oPDFDoc != null)
                    {
                        oPDFDoc.Close();
                        oPDFDoc.Dispose();
                        oPDFDoc = null;
                    }
                    oPDFDoc = new pdftron.PDF.PDFDoc(sFilePath);
                    if (oPDFDoc != null)
                    {
                        if (oPDFDoc.IsModified() == true)
                        {
                            oPDFDoc.Save(sFilePath, 0);
                        }
                    }
                    if (oPDFView == null)
                    {
                        //oPDFView = new pdftron.PDF.PDFViewCtrl();
                        oPDFView = new MyPDFView(this, 0);
                    }
                    if (oPDFDoc != null)
                    {
                        if (oPDFView != null)
                        {
                            oPDFView.Show();
                            oPDFView.SetDoc(oPDFDoc);

                        }
                    }


                    if (oldDoc != null)
                    {
                        oldDoc.Dispose();
                        oldDoc = null;
                    }


                    if (oPDFView != null)
                    {
                        oPDFView.MouseWheel += new MouseEventHandler(oPDFView_MouseWheel);
                        oPDFView.MouseDown += new MouseEventHandler(oPDFView_MouseDown);
                        oPDFView.MouseUp += new MouseEventHandler(oPDFView_MouseUp);
                    }
                    //LoadPages(DocumentID, gloEDocV3Admin.gClinicID);


                    pnlPreview.Controls.Add(oPDFView);
                    oPDFView.Location = new System.Drawing.Point(0, 0);
                    oPDFView.Dock = DockStyle.Fill;
                    oPDFView.BringToFront();
                    oPDFView.SetPagePresentationMode(pdftron.PDF.PDFViewCtrl.PagePresentationMode.e_single_page);
                    //oPDFView.CancelRendering();
                    oPDFView.SetCaching(true);
                    oPDFView.SetProgressiveRendering(true);
                    oPDFView.Visible = true;
                    oPDFView.Refresh();
                    oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_page);
                    oPDFView.SetPageViewMode(pdftron.PDF.PDFViewCtrl.PageViewMode.e_fit_width);
                    //oPDFView.OnScroll(0, 1);
                    LoadPages(DocumentID, gloEDocV3Admin.gClinicID);
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    AuditLogErrorMessage(_ErrorMessage);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                    Application.DoEvents();
                    if (oProcessLabel != null)
                    {
                        if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                        oProcessLabel.Dispose(); oProcessLabel = null;
                    }
                    _IsDocumentsLoading = false;
                    c1Documents.Enabled = true;
                    tls_MaintainDoc.Enabled = true;


                }
            }
            else
            {
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
                if (oProcessLabel != null)
                {
                    if (pnlPreview.Controls.Contains(oProcessLabel) == true) { pnlPreview.Controls.Remove(oProcessLabel); }
                    oProcessLabel.Dispose(); oProcessLabel = null;
                }
                _IsDocumentsLoading = false;
                c1Documents.Enabled = true;
                tls_MaintainDoc.Enabled = true;

                string _strMessage = "";
                _strMessage = "Could not open file because it is either damaged " + Environment.NewLine + "or the file is not saved properly.";

                MessageBox.Show(_strMessage, gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void tsb_CopyDocument_Click(object sender, EventArgs e)
        {
            tsb_Delete.Enabled = false;
            FillDocuments_Immnunization(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);
            tsb_CopyDocument.Enabled = false;
            SelectDocumentInGrid(_ExternalDocumentID, _ExternalContainerID);
            LoadDocument(_ExternalDocumentID, _ExternalContainerID, _OpenExternalSource);
        }
        public void SetToolMode(PDFViewCtrl.ToolMode tool_mode, MyPDFView.CustomToolMode custom_tool_mode)
        {
            MyPDFView._base_tool_mode = tool_mode;
            MyPDFView._tool_mode = custom_tool_mode;
            oPDFView.SetToolMode(tool_mode);
        }

        //SG:Memory Leaks, Method not in use or not referenced yet so not performing memory leaks operations
        private void Create_TextBox()
        {

            //string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            Obj stm = null;
            int _ConPageNumber = 0;
            try
            {

                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {

                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);
                            if (c1Documents != null)
                            {
                                #region "Container and Document Information"
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            }
                                #endregion
                            try
                            {

                                pdftron.PDF.Page page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no
                                double pgheight = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageHeight();
                                double pgwidth = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageWidth();
                                pdftron.SDF.SDFDoc sdfdoc = oPDFDoc.GetSDFDoc();             //obj of the sdf doc
                                stm = oPDFDoc.CreateIndirectDict();
                                Obj annots = page.GetAnnots();
                                if (annots != null)
                                {
                                    annots = oPDFDoc.CreateIndirectArray();
                                    page.GetSDFObj().Put("Annots", annots);
                                }
                                //txt.RefreshAppearance();

                                #region "variable decalaration"
                                Obj text_annot = null;
                                #endregion


                                text_annot = oPDFDoc.CreateIndirectDict();
                                text_annot.PutName("Subtype", "Text");
                                text_annot.PutBool("Open", true);
                                text_annot.PutString("Contents", "The quick brown fox ate the lazy mouse.");
                                text_annot.PutRect("Rect", 266, 116, 430, 204);
                                // stm = CreateTextBoxAppearance(oPDFDoc);
                                oPDFView.UpdatePageLayout();
                                oPDFView.SetProgressiveRendering(false);
                                annots.PushBack(text_annot);
                                oPDFView.Refresh();
                                oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_pan);
                                oPDFView.SetProgressiveRendering(true);

                                //if (stm != null)
                                //{
                                //    oPDFView.SetProgressiveRendering(false);                            //to reduce the flickering effect.

                                //    //stm.GetAppearance();
                                //    //stm.SetAppearance(stm, Annot.AnnotationState.e_normal);
                                //    annots.PushBack(stm);                                              //adding the image to page.
                                //    oPDFView.Update();                                                  //required to update
                                //    oPDFView.UpdatePageLayout();
                                //    oPDFView.Refresh();
                                //    oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_annot_edit);
                                //    oPDFView.SetProgressiveRendering(true);
                                //}


                            }
                            catch (PDFNetException ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (PDFNetException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

        }

        //SG:Memory Leaks, Method not in use or not referenced yet so not performing memory leaks operations
        private void Create_TextBox1()
        {

            //string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            Obj stm = null;
            int _ConPageNumber = 0;
            try
            {

                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {

                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);
                            if (c1Documents != null)
                            {
                                #region "Container and Document Information"
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            }
                                #endregion
                            try
                            {

                                pdftron.PDF.Page page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no
                                double pgheight = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageHeight();
                                double pgwidth = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageWidth();
                                pdftron.SDF.SDFDoc sdfdoc = oPDFDoc.GetSDFDoc();             //obj of the sdf doc
                                stm = oPDFDoc.CreateIndirectDict();
                                Field txt = oPDFDoc.FieldCreate("txt", Field.Type.e_text, oPDFDoc.CreateIndirectString("gloStream"));   //creating the field of type signature
                                //pdftron.PDF.Annots.Widget b = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(500, 100, 100, 200), chk); // right corner setting 
                                pdftron.PDF.Annots.Widget annot1 = pdftron.PDF.Annots.Widget.Create(sdfdoc, new Rect(pgwidth / 10, pgheight - 100, pgwidth / 5, pgheight - 70), txt); // right corner setting 
                                // pdftron.PDF.Font myFonts = new pdftron.PDF.Font(stm);

                                txt.RefreshAppearance();
                                //stm = CreateCheckMarkAppearance(oPDFDoc, _PatientID, _ContainerId, _DocumentId, pgheight, pgwidth);
                                annot1.SetBackgroundColor(new ColorPt(0, 1, 0), 3);
                                annot1.SetColor(new ColorPt(10, 0, 0));

                                temp = annot1;

                                if (stm != null)
                                {
                                    oPDFView.SetProgressiveRendering(false);                            //to reduce the flickering effect.

                                    //annot1.GetAppearance();
                                    //annot1.SetAppearance(stm, Annot.AnnotationState.e_normal);
                                    page.AnnotPushBack(annot1);                                              //adding the image to page.
                                    oPDFView.Update();                                                  //required to update
                                    oPDFView.UpdatePageLayout();
                                    oPDFView.Refresh();
                                    oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_pan);
                                    oPDFView.SetProgressiveRendering(true);
                                }


                            }
                            catch (PDFNetException ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (PDFNetException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }

        }

        private void mnuAddText_Click(object sender, EventArgs e)
        {

            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {

                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                oPDFView.IsTextAdded = true;
                                SetToolMode(PDFViewCtrl.ToolMode.e_custom, MyPDFView.CustomToolMode.e_custom_textBox);
                                // Create_TextBox();
                                // Create_TextBox1();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void mnuModifyText_Click(object sender, EventArgs e)
        {

            try
            {
                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {

                        if (lvwPages.SelectedItems.Count > 0)
                        {
                            if (oPDFView != null)
                            {
                                SetToolMode(PDFViewCtrl.ToolMode.e_text_struct_select, MyPDFView.CustomToolMode.e_custom_modify_textBox);

                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void stamperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Create_Stamper();
        }
        private void Create_Stamper()
        {

            //string _FileFullPath = "";
            Int64 _ContainerId = 0;
            Int64 _DocumentId = 0;
            int _ConPageNumber = 0;
            pdftron.PDF.Page page = null;
            Stamper s = null;

            try
            {

                if (_IsDocumentsLoading == false)
                {
                    if (lvwPages != null)
                    {
                        if (lvwPages.SelectedItems.Count > 0)
                        {

                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Annotation  - Start" + " " + DateTime.Now.TimeOfDay);

                            if (c1Documents != null)
                            {
                                #region "Container and Document Information"
                                if (c1Documents.RowSel > 0)
                                {
                                    _ContainerId = ((Document.eBaseContainers)c1Documents.GetData(c1Documents.RowSel, COL_CONATINERS))[0].EContainerID;
                                    _DocumentId = System.Convert.ToInt64(c1Documents.GetData(c1Documents.RowSel, COL_DOCUMENTID));
                                }
                                _ConPageNumber = System.Convert.ToInt32(lvwPages.SelectedItems[0].SubItems[2].Text);
                            }
                                #endregion
                            try
                            {

                                StringBuilder myString = null;
                                string str = null;


                                myString = CreateTextBoxText("add");
                                str = myString.ToString();


                                if (str == null || str == string.Empty)
                                {
                                    return;
                                }


                                //pdftron.PDF.Page page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no

                                page = oPDFView.GetDoc().GetPage(_ConPageNumber);//setting the page no
                                double pgheight = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageHeight();
                                double pgwidth = oPDFView.GetDoc().GetPage(_ConPageNumber).GetPageWidth();
                                //  pdftron.SDF.SDFDoc sdfdoc = oPDFDoc.GetSDFDoc();             //obj of the sdf doc


                                //SG: Memory Leaks, moving declaration outside try block for handling dispose in finally
                                //Stamper s = new Stamper(Stamper.SizeType.e_relative_scale, 0.5, 0.5);
                                s = new Stamper(Stamper.SizeType.e_relative_scale, 0.5, 0.5);
                                //

                                //pdftron.PDF.Font myFont = new pdftron.PDF.Font();
                                // myFont.Create(oPDFDoc, pdftron.PDF.Font.StandardType1Font.e_times_roman, true)                                
                                // s.SetAsAnnotation(true);                                
                                s.SetAlignment(Stamper.HorizontalAlignment.e_horizontal_center, Stamper.VerticalAlignment.e_vertical_center);
                                //s.SetFont(pdftron.PDF.Font.Create(oPDFView.GetDoc(), pdftron.PDF.Font.StandardType1Font.e_times_roman, true));
                                s.SetFontColor(new ColorPt(0, 0, 3));
                                //s.SetRotation(45);

                                s.StampText(oPDFDoc, str, new PageSet(_ConPageNumber, _ConPageNumber));


                                oPDFView.SetProgressiveRendering(false);                            //to reduce the flickering effect.


                                oPDFView.Update();                                                  //required to update
                                oPDFView.UpdatePageLayout();
                                oPDFView.Refresh();
                                oPDFView.SetToolMode(PDFViewCtrl.ToolMode.e_pan);
                                oPDFView.SetProgressiveRendering(true);



                            }
                            catch (PDFNetException ex)
                            {
                                _ErrorMessage = ex.ToString();
                                AuditLogErrorMessage(_ErrorMessage);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please select the Document.", gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (PDFNetException ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
            }
            finally
            {
                //SG: Memory Leaks, disposing local page variable
                page = null;
                if (s != null) { s.Dispose(); s = null; }
                //
            }

        }

        private StringBuilder CreateTextBoxText(string Modifier)
        {

            StringBuilder str = null;
            if (Modifier == "add")
            {
                TextBox objAddText = new TextBox();
                objAddText.ShowDialog(this);
                //_fFont = objAddText.FFont;  
                str = objAddText.MyStringBld;
                objAddText.Dispose();
                objAddText = null;
            }

            return str;
        }

        //Bug #89321: UI Issue:- date time not clearly visible to end user>>scan docs>>DMS Category >> Form name 
        //Added resize event for splitter and change name coloumn width.
        void splitter1_Resize(object sender, System.EventArgs e)
        {
            try
            {
                c1Documents.Cols[COL_NODENAME].Width = c1Documents.Width - 60;
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);
                MessageBox.Show("ERROR : " + ex.ToString(), gloGlobal.gloPMGlobal.MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void tsb_ShowHideAck_Click(object sender, EventArgs e)
        {
            if (flgShowAckDocs)
            {
                flgShowAckDocs = false;

                tsb_ShowHideAck.Text = "&View Acknowledged";
                tsb_ShowHideAck.Tag = "View Acknowledged";
                tsb_ShowHideAck.ToolTipText = "View Acknowledged";
                tsb_ShowHideAck.Image = global::gloEDocumentV3.Properties.Resources.ShowAcknowledged;
            }
            else
            {
                flgShowAckDocs = true;

                tsb_ShowHideAck.Text = "&Hide Acknowledged";
                tsb_ShowHideAck.Tag = "Hide Acknowledged";
                tsb_ShowHideAck.ToolTipText = "Hide Acknowledged";
                tsb_ShowHideAck.Image = global::gloEDocumentV3.Properties.Resources.HideAcknowledged;
            }

            FillCategories(c1Documents, false, _ExternalDocumentID, _OpenExternalSource);
            FillDocuments(c1Documents, _SelectedYear, _PatientID, _ExternalDocumentID, _OpenExternalSource);

            //ShowHideAckDocuments(flgShowAckDocs);

            Int64 _docid = 0;
            Int64 _contid = 0;
            GetFirstDocumentInGrid(out _docid, out _contid);
            LoadDocument(_docid, _contid, _OpenExternalSource);

        }

        //public void ShowHideAckDocuments(bool flgShowAckDocs)
        //{
        //    if (c1Documents != null)
        //    {
        //        for (int i = 0; i <= c1Documents.Rows.Count - 1; i++)
        //        {

        //            if (System.Convert.ToBoolean(c1Documents.GetData(i, COL_ISACKNOWLEDGE)))
        //            { c1Documents.Rows[i].Visible = flgShowAckDocs; }

        //        }
        //    }
        //}

        public void Navigate(string strstring)
        {

                foreach (Form frm in Application.OpenForms)
                {
                    if (frm.Name == "frmEDocEvent_Fax")
                    {
                        ((frmEDocEvent_Fax)frm).Navigate(strstring);
                        
                    }
                }
            
        }

        private void chkSearch_DocumentName_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearch_DocumentName.Checked == true)
            {
                txtSearch_DocumentName.ReadOnly = false;
                txtSearch_DocumentName.Focus();
            }
            else
            {
                txtSearch_DocumentName.ReadOnly = true;
                txtSearch_DocumentName.Text = "";
            }
        }

        private void tsb_ShowFileArchivedDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                frmEDocumentArchiveViewer ofrm = new frmEDocumentArchiveViewer(_PatientID, _OpenExternalSource);
                ofrm.StartPosition = FormStartPosition.CenterParent;
                ofrm.ShowInTaskbar = false;
                ofrm.ShowDialog(this);
                tsb_Refresh_Click(null, null);
                if (ofrm != null)  //Disposed by Mitesh
                {
                    ofrm.Dispose();
                    ofrm = null;
                }
            }
            catch (Exception ex)
            {
                _ErrorMessage = ex.ToString();
                AuditLogErrorMessage(_ErrorMessage);

            }
        }
     }
}
