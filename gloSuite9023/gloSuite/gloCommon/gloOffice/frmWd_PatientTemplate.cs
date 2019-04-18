using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using System.Diagnostics;
using System.ComponentModel;
using System.Drawing;
using Microsoft.Office.Core;
using gloPatient;

using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

namespace gloOffice
{
    public partial class frmWd_PatientTemplate : Form
    {
        //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);

        #region " private Variables "

        String _databaseConnectionString = "";
        private String _MessageBoxCaption = String.Empty;
        private Int64 _TransactionID = 0;
        public bool _isReadOnlyView = false;
        //Bug #63771: 00000624: Patient Statement Print
        //When you click the 'Print' button, nothing happens at all.
        public bool _isFromPatientAccount = false;
        // private String _TemplateName = "";
        //private Int64 _PatientID = 0;
        //private Int64 _AppointmentID = 0;
        //private Int64 _PrimaryID = 0; 
        //private Int64 _DocumentCategory = 0;
        private String _strFileName = "";
        private bool _Print = false;
        //private Int32 _DOS; // DateOfService
        private bool _IsView = false;
        private DialogResult _dlgRst = DialogResult.None;
        public Int64 _AccountID = 0;
        private Wd.Document oCurDoc;
        private Wd.Document oTempDoc;
        private Wd.Application oWordApp;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        #endregion

        gloOffice.gloTemplate _gloTemplate;

        #region " Liquid Data Constants "
        // Sudhir 20090120
        string FieldValue;       // To get the field value from the Window Double click event of Word User Control
        const string _Patient = "Patient";
        const string _PatientInsurance_DTL = "PatientInsurance_DTL";
        const string _Patient_DTL = "Patient_DTL";

        #endregion

        bool _IsFromAppointmentTab = false;
        //
        #region ' Property Procedures '

        public bool IsView
        {
            get { return _IsView; }
            set { _IsView = value; }
        }

        public DialogResult FormResult
        {
            get { return _dlgRst; }
            set { _dlgRst = value; }
        }

        #endregion ' Property Procedures '
        //
        #region " Constructor "
        //public frmWd_PatientTemplate(String DatabaseConnectionString, String TemplateName, Int64 PatientID, Int64 AppointmentID, Int64 PrimaryID, Int64 DocumentCategory)
        //public frmWd_PatientTemplate(String DatabaseConnectionString, gloTemplate ogloTemplate)
        //{
        //    _databaseConnectionString = DatabaseConnectionString;
        //    ogloTemplate = ogloTemplate;
        //    //_PatientID = PatientID;
        //    //_AppointmentID = AppointmentID;
        //    //_PrimaryID = PrimaryID; // TemplateID
        //    //_DocumentCategory = DocumentCategory;
        //    //_TemplateID = TemplateID;
        //    //_TemplateName = TemplateName;

        //    _gloTemplate = new gloTemplate(_databaseConnectionString);
        //    _gloTemplate.PatientID = PatientID;
        //    _gloTemplate.AppointmentID = AppointmentID;
        //    _gloTemplate.PrimeryID = PrimaryID;
        //    _gloTemplate.TemplateName = TemplateName;
        //    _gloTemplate.TemplateID = PrimaryID;
        //    _gloTemplate.DocumentCategory = DocumentCategory;
        //    _gloTemplate.CategoryID = CategoryID;
        //    _gloTemplate.CategoryName = CategoryName;

        //    InitializeComponent();
        //}

        public frmWd_PatientTemplate(String DatabaseConnectionString, gloTemplate ogloTemplate)
        {
            _databaseConnectionString = DatabaseConnectionString;
            if (_gloTemplate == null)
            {
                _gloTemplate = new gloTemplate(_databaseConnectionString); //SLR: new is not needed
                _gloTemplate = ogloTemplate;
            }
            else
            {
                _gloTemplate = null;
                _gloTemplate = new gloTemplate(_databaseConnectionString); //SLR: new is not needed
                _gloTemplate = ogloTemplate;
            }


            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmWd_PatientTemplate(String DatabaseConnectionString, gloTemplate ogloTemplate, bool Print)
        {
            _databaseConnectionString = DatabaseConnectionString;

            if (_gloTemplate == null)
            {
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _gloTemplate = ogloTemplate;
            }
            else
            {
                _gloTemplate = null;
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _gloTemplate = ogloTemplate;
            }

            //_gloTemplate.AppointmentID = AppointmentID;
            //_gloTemplate.PrimeryID = PrimaryID;
            //_gloTemplate.TemplateName = TemplateName;
            //_gloTemplate.TemplateID = PrimaryID;
            //_gloTemplate.DocumentCategory = DocumentCategory;

            _Print = Print;
            if (_Print == true)
            {
                this.Opacity = 0;
            }
            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmWd_PatientTemplate(String DatabaseConnectionString, Int64 TransactionID)
        {
            _databaseConnectionString = DatabaseConnectionString;
            if (_gloTemplate == null)
            {
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _TransactionID = TransactionID;
            }
            else
            {
                _gloTemplate = null;
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _TransactionID = TransactionID;
            }


            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmWd_PatientTemplate(String DatabaseConnectionString, Int64 TransactionID, bool _isFromPatAcct)
        {
            _databaseConnectionString = DatabaseConnectionString;
            if (_gloTemplate == null)
            {
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _TransactionID = TransactionID;
                _isReadOnlyView = _isFromPatAcct;
                //Bug #63771: 00000624: Patient Statement Print
                //When you click the 'Print' button, nothing happens at all.
                _isFromPatientAccount = _isFromPatAcct;
            }
            else
            {
                _gloTemplate = null;
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _TransactionID = TransactionID;
                _isReadOnlyView = _isFromPatAcct;
                //Bug #63771: 00000624: Patient Statement Print
                //When you click the 'Print' button, nothing happens at all.
                _isFromPatientAccount = _isFromPatAcct;
            }

            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        public frmWd_PatientTemplate(String DatabaseConnectionString, bool IsFromAppointmentTab, gloTemplate ogloTemplate)
        {
            _databaseConnectionString = DatabaseConnectionString;
            if (_gloTemplate == null)
            {
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _gloTemplate = ogloTemplate;
                _IsFromAppointmentTab = IsFromAppointmentTab;
            }
            else
            {
                _gloTemplate = null;
                _gloTemplate = new gloTemplate(_databaseConnectionString);
                _gloTemplate = ogloTemplate;
                _IsFromAppointmentTab = IsFromAppointmentTab;
            }

            InitializeComponent();

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }
        #endregion

        public delegate void FormClosed(object sender, ToolStripItemClickedEventArgs e);
        public event FormClosed Form_Closed;


        private void frmWd_PatientTemplate_Load(object sender, EventArgs e)
        {
            //Bug #55198: 00000516 : Receipt print performance Issue
            Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            Microsoft.Office.Interop.Word.Document aDoc = null;

            object saveOptions = default(object);
            object oFileName = default(object);
            object missing = default(object);
            object oFileFormat = default(object);
            try
            {

                if (IsView == true)
                {
                    tmrDocProtect.Enabled = true;
                    tsb_InsertFile.Visible = false;
                    tsb_InsertImage.Visible = false;
                    tsb_Print.Visible = true;
                    tsb_Save.Visible = false;
                }
                this.Cursor = Cursors.WaitCursor;
                if (_TransactionID == 0)
                {
                    #region "Old Logic"
                    //NewPatientTemplate();

                    //// SUDHIR - 20090121 //
                    //// To Open WordFile in Temp Word Control //
                    //wdTemp.Open(_strFileName);
                    //oTempDoc = (Microsoft.Office.Interop.Word.Document)wdTemp.ActiveDocument;
                    //oWordApp = oTempDoc.Application;
                    //gloOffice.Supporting.WdApplication = oWordApp;
                    //gloOffice.Supporting.CurrentDocument = oTempDoc;
                    //// ----------- //
                    //gloOffice.Supporting.GetFormFieldData(null, _AccountID); // Perform this function on TempDocument //
                    //_AccountID = gloOffice.Supporting.FieldID1; 
                    //// To Save Temperory Document //
                    //String strNewFileName = gloOffice.Supporting.NewDocumentName();
                    //object oFileName = (object)strNewFileName;
                    //object missing = System.Reflection.Missing.Value;
                    //object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                    //oTempDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                    //wdTemp.Close();
                    ////wdTemp.Dispose();
                    //// -------------- //

                    //wdTemplate.Open(strNewFileName); // To open file in Visible Word Control //
                    //Supporting.CurrentDocument = oCurDoc;

                    //if (_Print == true)
                    //{
                    //    gloWord.gloWord.CurrentDoc = oCurDoc;
                    //    gloWord.gloWord.CleanupDocument();
                    //    SavePatientTemplate();
                    //    wdTemplate.PrintOut();
                    //    this.Close();
                    //}
                    #endregion
                    try
                    {
                        wordApplication = new Microsoft.Office.Interop.Word.Application();
                        NewPatientTemplate();

                        object sFileName = _strFileName;


                        object newTemplate = false;
                        object docType = 0;
                        object isVisible = true;
                        object isLockComment = false;
                        object missing_new = Type.Missing;
                        oFileName = (object)sFileName;
                        missing = System.Reflection.Missing.Value;
                        oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                        saveOptions = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;

                        // Create a new Document, by calling the Add function in the Documents collection
                        aDoc = wordApplication.Documents.Add(ref oFileName, ref newTemplate, ref docType, ref isVisible);
                        gloOffice.Supporting.WdApplication = wordApplication;
                        gloOffice.Supporting.CurrentDocument = aDoc;
                        // ----------- //

                        if (_gloTemplate.isFromDashboradAppt)
                        {
                            gloOffice.Supporting.isFromBatchPrint = true;
                        }

                        gloOffice.Supporting.GetFormFieldData(null, _AccountID); // Perform this function on TempDocument //

                        gloOffice.Supporting.isFromBatchPrint = false;

                        _AccountID = gloOffice.Supporting.FieldID1;
                        //gloWord.gloWord.CurrentDoc = aDoc;
                        //commented against incident #00021856 to keep dropdown list control in word doc
                        //gloWord.gloWord.CleanupDocument();

                        aDoc.SaveAs(ref oFileName, ref oFileFormat, ref isLockComment, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                        try
                        {
                            aDoc.Close(ref saveOptions, ref missing_new, ref missing_new);

                            System.Runtime.InteropServices.Marshal.ReleaseComObject(aDoc);

                            aDoc = null;
                        }
                        catch
                        {
                        }
                        //Incident #65062: 00025736
                        if (wordApplication != null)
                        {
                            wordApplication.Application.Quit(ref saveOptions, ref missing, ref missing);
                            try
                            {
                                System.Runtime.InteropServices.Marshal.ReleaseComObject(wordApplication);
                                wordApplication = null;
                            }
                            catch
                            {
                            }
                        }
                        //   wdTemplate.Open(_strFileName);

                        object thisObject = (object)oFileName;
                        //    Wd.Application oWordApp = null;
                        gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                        _strFileName = (string)thisObject;
                        Supporting.CurrentDocument = oCurDoc;

                        if (_Print == true)
                        {
                            //gloWord.gloWord.CurrentDoc = oCurDoc;
                            //gloWord.gloWord.CleanupDocument();
                            //oCurDoc = gloWord.gloWord.CurrentDoc;
                            gloWord.LoadAndCloseWord.CleanupDoc(ref oCurDoc);
                            SavePatientTemplate();

                            //oCurDoc.Application.Options.PrintBackground = true;
                            //oCurDoc.PrintOut(Background: true);

                            gloWord.LoadAndCloseWord.PrintWordDocument(ref oCurDoc, false, false, gloOffice.Supporting.PatientID);
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                    }
                    finally
                    {

                        if (aDoc != null) { aDoc = null; }

                        if (oFileName != null) { oFileName = null; }
                        if (missing != null) { missing = null; }
                        if (oFileFormat != null) { oFileFormat = null; }
                    }
                }
                else
                {
                    DataTable dttemp = null;//SLR: new is not needed
                    gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
                    dttemp = ogloTemplate.GetPatientTemplate(_TransactionID);

                    if (dttemp != null && dttemp.Rows.Count > 0)
                    {
                        //nPatientID, nTemplateID , sTemplateName , nFromDate, nToDate, nProviderID, iTemplate, nCount, nClinicID
                        //if (dttemp.Rows[0]["iTemplate"] != null && Convert.ToString(dttemp.Rows[0]["iTemplate"].ToString()) != string.Empty)
                        //{
                        _gloTemplate.PatientID = Convert.ToInt64(dttemp.Rows[0]["nPatientID"].ToString());
                        _gloTemplate.PrimeryID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
                        _gloTemplate.CategoryID = Convert.ToInt64(dttemp.Rows[0]["nCategoryID"]);
                        _gloTemplate.CategoryName = dttemp.Rows[0]["sCategoryName"].ToString();
                        _gloTemplate.TemplateID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
                        _gloTemplate.TemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
                        _gloTemplate.FromDate = Convert.ToInt32(dttemp.Rows[0]["nFromDate"]);

                        //Set the File to control
                        string strNewDocumentName = "";
                        strNewDocumentName = gloOffice.Supporting.NewDocumentName();

                        object objTemplateDocument;

                        if (dttemp.Rows[0]["iTemplate"] != null)
                        {
                            //As we save pateint statement in .doc file in db because of ExportToStream so need to replace .docx by .doc for newly created file.
                            if (_gloTemplate.TemplateName.Contains("PatientStatement"))
                            { strNewDocumentName = strNewDocumentName.Replace(".docx", ".doc"); }

                            objTemplateDocument = dttemp.Rows[0]["iTemplate"];
                            ogloTemplate.ConvertBinaryToFile(objTemplateDocument, strNewDocumentName);

                            #region "Changes for Office 2013"

                            try
                            {
                                Microsoft.Office.Interop.Word.Application mywordApplication = new Microsoft.Office.Interop.Word.Application();

                                if (mywordApplication != null)
                                {
                                    string mysFileName = gloOffice.Supporting.NewDocumentName();

                                    object myoFileName = (object)mysFileName;
                                    object myoFileFormat = (object)Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatXMLDocument;
                                    object mysaveoptions = (object)Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;

                                    Microsoft.Office.Interop.Word.Document myDoc = mywordApplication.Documents.Add(strNewDocumentName);

                                    if (myDoc != null)
                                    {
                                        myDoc.SaveAs(mysFileName, myoFileFormat);
                                        strNewDocumentName = mysFileName;

                                        myDoc.Close(mysaveoptions);
                                        mywordApplication.Application.Quit(mysaveoptions);

                                        try { Marshal.ReleaseComObject(myDoc); }
                                        catch { }
                                    }

                                    try { Marshal.ReleaseComObject(mywordApplication); }
                                    catch { }
                                }
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while loading the Statement document. Please check the exception log" + ex.Message, true);
                            }

                            #endregion

                            //  wdTemplate.Open(strNewDocumentName);
                            object thisObject = (object)strNewDocumentName;
                            //  Wd.Application oWordApp = null;
                            gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                            strNewDocumentName = (string)thisObject;



                        }
                        //}
                        //else
                        //{
                        //     MessageBox.Show("Record cannot be opened as there are problems with the contents.",_MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //     this.Close();
                        //}
                        //SLR: Free dtTemp, ogloTemplate
                        if (dttemp != null)
                        {
                            dttemp = null;
                        }
                        if (ogloTemplate != null)
                        {
                            ogloTemplate = null;
                        }

                    }
                }

                if (oCurDoc != null)
                {
                    gloWord.gloWord.CurrentDoc = oCurDoc;
                    gloWord.gloWord.GoToBegin();
                }
            }
            catch (Exception ex)
            {
                if (_Print == true)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            this.Cursor = Cursors.Default;
            //  
        }


        //Added by Roopali . 19 Aug 2010 To print template without view(Direct print).
        //Added to print batch from patient batch history form.
        public void PrintTemplate()
        {
            DataTable dttemp = null;//SLR: new is not needed
            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            try
            {
                dttemp = ogloTemplate.GetPatientTemplate(_TransactionID);
                if (dttemp != null && dttemp.Rows.Count > 0)
                {
                    //nPatientID, nTemplateID , sTemplateName , nFromDate, nToDate, nProviderID, iTemplate, nCount, nClinicID
                    _gloTemplate.PatientID = Convert.ToInt64(dttemp.Rows[0]["nPatientID"].ToString());
                    _gloTemplate.PrimeryID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
                    _gloTemplate.CategoryID = Convert.ToInt64(dttemp.Rows[0]["nCategoryID"]);
                    _gloTemplate.CategoryName = dttemp.Rows[0]["sCategoryName"].ToString();
                    _gloTemplate.TemplateID = Convert.ToInt64(dttemp.Rows[0]["nTemplateID"]);
                    _gloTemplate.TemplateName = dttemp.Rows[0]["sTemplateName"].ToString();
                    _gloTemplate.FromDate = Convert.ToInt32(dttemp.Rows[0]["nFromDate"]);

                    //Set the File to control
                    string strNewDocumentName = "";
                    strNewDocumentName = gloOffice.Supporting.NewDocumentName();

                    object objTemplateDocument;

                    if (dttemp.Rows[0]["iTemplate"] != null)
                    {
                        objTemplateDocument = dttemp.Rows[0]["iTemplate"];

                        //Bug #63771: 00000624: Patient Statement Print
                        //When you click the 'Print' button, nothing happens at all.
                        //While click on Reprint batch nothing is happens.
                        if (_gloTemplate.TemplateName.Contains("PatientStatement"))
                        { strNewDocumentName = strNewDocumentName.Replace(".docx", ".doc"); }

                        ogloTemplate.ConvertBinaryToFile(objTemplateDocument, strNewDocumentName);
                        //   wdTemplate.Open(strNewDocumentName);
                        object thisObject = (object)strNewDocumentName;
                        //   Wd.Application oWordApp = null;
                        gloWord.LoadAndCloseWord.OpenDSO(ref wdTemplate, ref thisObject, ref oCurDoc, ref oWordApp);
                        strNewDocumentName = (string)thisObject;
                        //wdTemplate.PrintOut();
                        Wd.Document wordDocument = wdTemplate.ActiveDocument as Wd.Document;
                        //wordDocument.Application.Options.PrintBackground = true;
                        //wordDocument.PrintOut(Background:true);
                        gloWord.LoadAndCloseWord.PrintWordDocument(ref wordDocument, false, false, _gloTemplate.PatientID);
                        //            gloWord.gloWord. wdTemplate.ActiveDocument
                        //               oTempDoc.Application.Options.PrintBackground = True
                        //oTempDoc.PrintOut(Background:=True)
                        //wordDocument.Close();
                        wdTemplate.Close();
                        wordDocument = null;

                        wdTemplate = null;
                    }
                }
            }
            catch (Exception Ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(Ex.ToString(), false);
                Ex = null;
            }
            finally
            {
                dttemp.Dispose();
                ogloTemplate.Dispose();
            }
        }



        private void NewPatientTemplate()
        {
            gloOffice.Supporting.AppointmentID = 0;
            gloOffice.Supporting.DataBaseConnectionString = _databaseConnectionString;
            gloOffice.Supporting.PatientID = _gloTemplate.PatientID;
            gloOffice.Supporting.FieldID2 = _gloTemplate.TransactionID;
            gloOffice.Supporting.FieldID3 = _gloTemplate.TransactionMstID;
            gloOffice.Supporting.PrimaryID = _gloTemplate.TemplateID; // TemplateID 
            //gloOffice.Supporting.PrimaryID = _gloTemplate.TemplateID; // TemplateID
            //gloOffice.Supporting.CurrentDocument = oCurDoc;
            //gloOffice.Supporting.CurrentDocument = oTempDoc;
            if (_IsFromAppointmentTab == true)
            {
                gloOffice.Supporting.FromDate = _gloTemplate.FromDate;
            }
            else
            {
                gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(dtp_DOS.Value.ToString("MM/dd/yyyy"));
            }

            //Bug #92723: 00001067: Appointment 
            if (_IsFromAppointmentTab == true)
            {
                gloOffice.Supporting.ToDate = _gloTemplate.ToDate;
            }
            else
            {
                gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(dtp_DOS.Value.ToString("MM/dd/yyyy"));
            }

            //gloOffice.Supporting.DOS = gloDateMaster.gloDate.DateAsNumber(dtp_DOS.Value.ToShortDateString());

            _strFileName = gloOffice.Supporting.GenerateDocumentFile();
            gloOffice.Supporting.PrimaryID = _gloTemplate.PrimeryID; // TemplateID 

            // GLO2010-0010515 : check in template patient information sheet not filling out history items once history is checked (Bug #4427)
            // We were sending Visit Id as zero, so history liquid links was not populating in check-in template
            gloOffice.Supporting.VisitID = _gloTemplate.VisitID;

            //wdTemplate.Open(_strFileName);
            //gloOffice.Supporting.CurrentDocument = oCurDoc;
            //sDescription
        }

        private void wdTemplate_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdTemplate.ActiveDocument;
            oWordApp = oCurDoc.Application;
            gloWord.gloWord.ActiveDocumentName = oCurDoc.FullName.ToString();

            try
            { // Sudhir 20090120
                oWordApp.WindowSelectionChange -= DDLCBEvent;
                oWordApp.WindowBeforeDoubleClick -= OnFormClicked;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            // Bug #63380: 00000112 : Word Modules
            oWordApp.WindowSelectionChange += DDLCBEvent;
            oWordApp.WindowBeforeDoubleClick += OnFormClicked;
            try
            {
                if (_IsView == true)
                {
                    object noReset = false;
                    object password = System.String.Empty;
                    object useIRM = false;
                    object enforceStyleLock = false;

                    oWordApp.ActiveDocument.Protect(Microsoft.Office.Interop.Word.WdProtectionType.wdAllowOnlyReading, ref noReset, ref password, ref useIRM, ref enforceStyleLock);
                }

            }
            catch (Exception)// Ex)
            {
                //Ex.ToString();
                //Ex = null;
            }

        }

        private void wdTemplate_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                //SLR: Free event handlers: Already done in beforedocumentclosed..
                //if (oCurDoc != null)
                //{
                //    oCurDoc.Application.WindowSelectionChange -= DDLCBEvent;
                //    oCurDoc.Application.WindowBeforeDoubleClick -= OnFormClicked;
                //    foreach (Wd.RecentFile oFile in oCurDoc.Application.RecentFiles)
                //    {
                //        if (oFile.Path == gloSettings.FolderSettings.AppTempFolderPath)
                //        {
                //            oFile.Delete();
                //        }
                //    }
                //}

                if ((oCurDoc != null))
                {
                    //' RemoveHandler oCurDoc1.ContentControlOnExit, AddressOf onCtrlExit 
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
                if ((oTempDoc != null))
                {
                    Marshal.ReleaseComObject(oTempDoc);
                    oTempDoc = null;
                }

                if (wdTemplate != null)
                {
                    Marshal.ReleaseComObject(wdTemplate);
                    wdTemplate = null;
                }

                //if (wdTemp != null)
                //{
                //    Marshal.ReleaseComObject(wdTemp);
                //    wdTemp = null;
                //}

                //if ((oWordApp != null))
                //{
                //    //oWordApp.WindowSelectionChange -= DDLCBEvent;
                //    //oWordApp.WindowBeforeDoubleClick -= OnFormClicked;
                //    //UpdateVoiceLog("RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick for oWordApp") 
                ////    Marshal.FinalReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}

            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                //GC.Collect(); //code change for problem 00000591
                //GC.WaitForPendingFinalizers();

            }

        }

        #region " Liquid Data "
        // Sudhir 20090120 - Liquid Data //
        public void OnFormClicked(Wd.Selection Sel, ref bool Cancel)
        {
            if (gloWord.gloWord.ActiveDocumentName != gloWord.gloWord.GetActiveDocumentName())
            {
                return;
            }
            Wd.Range r = null;
            try
            {
                r = Sel.Range;

            }
            catch (Exception)
            {
            }
            if (((r == null)))
            {
                return;
            }
            try
            {
                r.SetRange(Sel.Start, Sel.End + 1);

            }
            catch (Exception)
            {
            }
            if (((r == null)))
            {
                return;
            }


            if (r.FormFields != null && r.FormFields.Count >= 1)
            {
                //  object om = System.Reflection.Missing.Value;
                Wd.FormField f = default(Wd.FormField);
                Object o = 1;
                //f = r.FormFields.Item(1);
                f = r.FormFields.get_Item(ref o);
                if (f.Type == Wd.WdFieldType.wdFieldFormTextInput)
                {
                    FieldValue = f.StatusText;
                    //'To implement liquid in same thread context
                    AccessControl();
                    Cancel = true;
                }
                else
                {
                    if (f.Type == Wd.WdFieldType.wdFieldFormDropDown)
                    {
                        Cancel = true;
                    }
                    else
                    {
                        if (f.Type == Wd.WdFieldType.wdFieldFormCheckBox)
                        {
                            Cancel = true;
                        }
                    }
                }
            }
        }

        private void DDLCBEvent(Wd.Selection Sel)
        {
            try
            {
                if (Sel == null)
                {
                    return;
                }
                if (Sel.Start == Sel.End)
                {
                    Wd.Range r = null;
                    try
                    {
                        r = Sel.Range;

                    }
                    catch (Exception)
                    {
                    }
                    if (((r == null)))
                    {
                        return;
                    }
                    try
                    {
                        r.SetRange(Sel.Start, Sel.End + 1);

                    }
                    catch (Exception)
                    {
                    }
                    if (((r == null)))
                    {
                        return;
                    }

                    //r.SetRange(Sel.Start, Sel.End + 1);
                    if (r.FormFields != null && r.FormFields.Count >= 1)
                    {
                        object om = System.Reflection.Missing.Value;
                        Wd.FormField f = null;
                        try
                        {
                            object o = 1;
                            f = r.FormFields.get_Item(ref o);
                            o = null;
                        }
                        catch
                        {

                        }
                        if (f != null)
                        {
                            if (f.Type == Wd.WdFieldType.wdFieldFormCheckBox)
                            {
                                f.CheckBox.Value = !f.CheckBox.Value;
                                object oUnit = Wd.WdUnits.wdCharacter;
                                object oCnt = 1;
                                object oMove = Wd.WdMovementType.wdMove;
                                Sel.MoveRight(ref oUnit, ref oCnt, ref oMove);
                            }
                        }
                    }
                }
            }
            catch (Exception) // ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void AccessControl()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new MethodInvoker(AccessControl));
            }
            else
            {
                OpenLink(FieldValue);
            }
        }

        private void OpenLink(string strFormFieldResult)
        {
            ArrayList tableNames = new ArrayList();
            String[] strTable = strFormFieldResult.Split('.');


            switch (strTable[0])
            {
                case _Patient:
                case _Patient_DTL:
                case _PatientInsurance_DTL: // To Call Patient Registration Form

                    GetPatientRegistration();

                    tableNames.Add(_Patient);
                    tableNames.Add(_Patient_DTL);
                    tableNames.Add(_PatientInsurance_DTL);
                    break;

            }

            // To Refresh Selected FormFields.
            if (tableNames.Count > 0)
            {
                Supporting.CurrentDocument = oCurDoc;
                Supporting.GetFormFieldData(tableNames);
            }
            //SLR: tablenames memory to be freeed
            if (tableNames != null)
            {
                tableNames = null;
            }
        }

        private void GetPatientRegistration()
        {
            frmSetupPatient oPatientRegistration = new frmSetupPatient(_gloTemplate.PatientID, _databaseConnectionString);
            oPatientRegistration.ShowDialog(this);
            //SLR: Free oPatientRegistration
            if (oPatientRegistration != null)
            {
                oPatientRegistration.Dispose();
            }
        }

        #endregion

        #region " Tool Strip Event "

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                // SUDHIR 20091103 // TO MAINTAIN FOCUS OF EXTERNAL WORD //
                if (gloWord.gloWord.CheckActiveWord(oCurDoc) == false)
                {
                    return;
                }

                switch (e.ClickedItem.Tag.ToString())
                {
                    case "Save":
                        {
                            _Print = false;
                            if (SavePatientTemplate() == true)
                            {
                                _dlgRst = DialogResult.OK;
                                wdTemplate.Close();
                                if (Form_Closed != null)
                                {
                                    Form_Closed(sender, e);
                                }
                                this.Close();
                            }
                        }
                        break;
                    case "Close":
                        //{ this.Close(); }
                        //break;
                        //code Added by mitesh --
                        {
                            if (oCurDoc != null && !oCurDoc.ReadOnly && !oCurDoc.Saved)
                            {
                                switch (MessageBox.Show("Do you want to save the changes to template?", _MessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
                                {
                                    case DialogResult.Yes:
                                        {
                                            if (SavePatientTemplate())
                                            {
                                                _dlgRst = DialogResult.OK;
                                                this.Close();
                                            }
                                            break;
                                        }

                                    case DialogResult.No:
                                        {
                                            _dlgRst = DialogResult.Cancel;
                                            this.Close();
                                            break;
                                        }
                                }
                            }
                            else
                            {
                                if (_dlgRst != DialogResult.OK)
                                {
                                    _dlgRst = DialogResult.Cancel;
                                }
                                this.Close();
                            }
                        }
                        break;
                    //------x------
                    case "Print":
                        {
                            _Print = true;
                            SavePatientTemplate();
                            //Bug #55198: 00000516 : Receipt print performance Issue
                            //Print();
                            _dlgRst = DialogResult.OK;
                        }
                        break;
                    case "InsertFile":
                        {
                            InsertFile("InsertFile");
                        }
                        break;
                    case "InsertImage":
                        {
                            InsertFile("InsertImage");
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region " Tool Strip Methods "

        public bool SavePatientTemplate()
        {
            //Bug #55198: 00000516 : Receipt print performance Issue
            //  Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            // Microsoft.Office.Interop.Word.Document aDoc = null;

            gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
            DataTable dtTemplateDetails = default(DataTable);
            //object saveOptions = default(object); 
            //object oFileName = default(object);
            //object missing = default(object);
            //object oFileFormat = default(object);
            //object newTemplate = default(object);
            //object docType = default(object);
            //object isVisible = default(object);
            String sFileName = string.Empty;

            bool IsDocumentPrint = false;

            try
            {
                ogloTemplate.ClinicID = _gloTemplate.ClinicID;
                ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                ogloTemplate.TemplateID = _gloTemplate.TemplateID;
                ogloTemplate.PatientID = _gloTemplate.PatientID;

                if ((_gloTemplate.TemplateName == "") || (_gloTemplate.CategoryID == 0) || (_gloTemplate.CategoryName == ""))
                {
                    dtTemplateDetails = ogloTemplate.GetTemplateDetails(_gloTemplate.TemplateID);
                    if (dtTemplateDetails != null && dtTemplateDetails.Rows.Count > 0)
                    {
                        ogloTemplate.TemplateName = dtTemplateDetails.Rows[0]["sTemplateName"].ToString();
                        ogloTemplate.CategoryID = Convert.ToInt64(dtTemplateDetails.Rows[0]["nCategoryID"]);
                        ogloTemplate.CategoryName = dtTemplateDetails.Rows[0]["sCategoryName"].ToString();
                    }
                    else
                    {
                        ogloTemplate.TemplateName = _gloTemplate.TemplateName;
                        ogloTemplate.CategoryID = _gloTemplate.CategoryID;
                        ogloTemplate.CategoryName = _gloTemplate.CategoryName;
                    }
                }
                else
                {
                    ogloTemplate.TemplateName = _gloTemplate.TemplateName;
                    ogloTemplate.CategoryID = _gloTemplate.CategoryID;
                    ogloTemplate.CategoryName = _gloTemplate.CategoryName;
                }


                //   wordApplication = new Microsoft.Office.Interop.Word.Application();

                sFileName = gloOffice.Supporting.NewDocumentName();
                //oFileName = (object)sFileName;
                //missing = System.Reflection.Missing.Value;
                //oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                //newTemplate = false;
                //docType = 0;
                //isVisible = true;
                //string stempFileName = gloOffice.Supporting.NewDocumentName();
                try
                {
                    oCurDoc.SaveAs(oCurDoc.FullName);

                    System.IO.File.Copy(oCurDoc.FullName, sFileName);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while copy file in savePatientTemplate. " + ex.Message, false);
                    throw;
                }

                //oFileName = (object)sFileName;

                //  aDoc = wordApplication.Documents.Add(ref oFileName, ref newTemplate, ref docType, ref isVisible);
                //    aDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                //  aDoc.Close();

                ogloTemplate.TemplateFilePath = sFileName;

                _TransactionID = ogloTemplate.SavePatientTemplate(_TransactionID);
                if (_TransactionID > 0)
                {
                    if (_Print == true)
                    {
                        //Incident #62932: 00024847
                        //Description: When printing check in templates, it prints the labels for any liquid links that aren't populated
                        //Resolution: Commented old code. Added a new method for printing template.
                        //wdTemplate.PrintOut();
                        //wdTemplate.Open(sFileName);
                        DocumentPrintOut(ref oCurDoc);
                    }

                    IsDocumentPrint = true;
                }
                else
                {
                    IsDocumentPrint = false;
                }

                #region "Old logic"
                //gloOffice.gloTemplate ogloTemplate = new gloTemplate(_databaseConnectionString);
                //ogloTemplate.ClinicID = _gloTemplate.ClinicID;
                //ogloTemplate.FromDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                //ogloTemplate.ToDate = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.Date.ToShortDateString());
                //ogloTemplate.TemplateID = _gloTemplate.TemplateID;
                //ogloTemplate.PatientID = _gloTemplate.PatientID;
                //if ((_gloTemplate.TemplateName == "") || (_gloTemplate.CategoryID == 0) || (_gloTemplate.CategoryName == ""))
                //{
                //    DataTable dtTemplateDetails = new DataTable();
                //    dtTemplateDetails = ogloTemplate.GetTemplateDetails(_gloTemplate.TemplateID);
                //    if (dtTemplateDetails != null && dtTemplateDetails.Rows.Count > 0)
                //    {
                //        ogloTemplate.TemplateName = dtTemplateDetails.Rows[0]["sTemplateName"].ToString();
                //        ogloTemplate.CategoryID = Convert.ToInt64(dtTemplateDetails.Rows[0]["nCategoryID"]);
                //        ogloTemplate.CategoryName = dtTemplateDetails.Rows[0]["sCategoryName"].ToString();
                //    }
                //    else
                //    {
                //        ogloTemplate.TemplateName = _gloTemplate.TemplateName;
                //        ogloTemplate.CategoryID = _gloTemplate.CategoryID;
                //        ogloTemplate.CategoryName = _gloTemplate.CategoryName;
                //    }
                //}
                //else
                //{
                //    ogloTemplate.TemplateName = _gloTemplate.TemplateName;
                //    ogloTemplate.CategoryID = _gloTemplate.CategoryID;
                //    ogloTemplate.CategoryName = _gloTemplate.CategoryName;
                //}

                //String sFileName = gloOffice.Supporting.NewDocumentName();

                //object oFileName = (object)sFileName;
                //object missing = System.Reflection.Missing.Value;
                //object oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                //oCurDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);
                //wdTemplate.Close();
                //ogloTemplate.TemplateFilePath = sFileName;

                //_TransactionID = ogloTemplate.SavePatientTemplate(_TransactionID);
                //if (_TransactionID > 0)
                //{
                //    if (_Print == true)
                //    {
                //        wdTemplate.Open(sFileName);
                //    }

                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
                if (dtTemplateDetails != null) { dtTemplateDetails.Dispose(); }
                //if (wordApplication != null) { wordApplication.Application.Quit(ref saveOptions, ref missing, ref missing); }
                //if (aDoc != null) { aDoc = null; }

                //if (oFileName != null) { oFileName = null; }
                //if (missing != null) { missing = null; }
                //if (oFileFormat != null) { oFileFormat = null; }
                //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                //Delete file created for temporary purpose. 
                if (System.IO.File.Exists(sFileName)) { File.Delete(sFileName); }
                if (!string.IsNullOrEmpty(sFileName)) { sFileName = string.Empty; }
                //if (newTemplate != null) { newTemplate = null; }
                //if (docType != null) { docType = null; }
                //if (isVisible != null) { isVisible = null; }
            }
            return IsDocumentPrint;
        }

        //Incident #62932: 00024847
        //Description: When printing check in templates, it prints the labels for any liquid links that aren't populated
        //Resolution: Creates new document, clear the data for liquid link and print the sane document.
        private void DocumentPrintOut(ref Wd.Document CurrentDocument)
        {
            //Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            Microsoft.Office.Interop.Word.Document aDoc = null;
            //Bug ID: 00000295 (Printing - EMR)
            //Reason: To resolve reported exception.
            //Description: Set background =false referring link http://www.xtremevbtalk.com/showthread.php?t=55010
            //Changed to resolve the office template issue : #63993: Coding and Unit Testing - 00000112
            gloWord.LoadAndCloseWord myLoadWord = null;
            object Background = true;
            object Range = Wd.WdPrintOutRange.wdPrintAllDocument;
            object Copies = 1;
            object PageType = Wd.WdPrintOutPages.wdPrintAllPages;
            object PrintToFile = false;
            object Collate = false;
            object ActivePrinterMacGX = Type.Missing;
            object ManualDuplexPrint = false;
            object PrintZoomColumn = 1;
            object PrintZoomRow = 1;
            object missing = Type.Missing;

            object oFileFormat = default(object);
            object oFileName = default(object);
            //wordApplication = new Microsoft.Office.Interop.Word.Application();
            string sFileName = gloOffice.Supporting.NewDocumentName();
            //Bug #63771: 00000624: Patient Statement Print
            //When you click the 'Print' button, nothing happens at all.
            if (_isFromPatientAccount)
            {
                if (_gloTemplate.TemplateName.Contains("PatientStatement"))
                { sFileName = sFileName.Replace(".docx", ".doc"); }
            }
            try
            {
                try
                {
                    System.IO.File.Copy(CurrentDocument.FullName, sFileName);
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog("Exception while copy file in DocumentPrintOut. " + ex.Message, false);
                    throw;
                }
                //oFileName = (object)sFileName;

                //oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;

                //aDoc = wordApplication.Documents.Add(oFileName);
                //aDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);

                //Bug #63771: 00000624: Patient Statement Print
                //When you click the 'Print' button, nothing happens at all.

                gloWord.LoadAndCloseWord thisLoadWord = GetMyLoadWordApplication();
                try
                {
                    aDoc = thisLoadWord.LoadWordApplication(sFileName);
                    oFileName = (object)sFileName;
                    oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                    aDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);

                    if (!_isFromPatientAccount)
                    {
                        // gloWord.gloWord.CurrentDoc = aDoc;
                        // gloWord.gloWord.CleanupDocument();
                        gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);
                    }
                    //aDoc.Application.Options.PrintBackground = true;
                    //aDoc.PrintOut(ref Background, ref missing, ref missing, ref missing,
                    //    ref missing, ref missing, ref missing, ref Copies,
                    //    ref missing, ref missing, ref PrintToFile, ref Collate,
                    //    ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                    //    ref PrintZoomRow, ref missing, ref missing);


                    bool DefaultPrinter = gloGlobal.gloTSPrint.IsDefaultPrinterOn();
                    if (DefaultPrinter)
                    {
                        //aDoc.Application.Options.PrintBackground = true;
                        gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                         ref missing, ref missing, ref missing, ref Copies,
                         ref missing, ref missing, ref PrintToFile, ref Collate,
                         ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                         ref PrintZoomRow, ref missing, ref missing);

                    }
                    else
                    {

                        myLoadWord = GetMyLoadWordApplication();
                        PrintWordDocument(ref myLoadWord, aDoc.FullName.ToString(), true, _gloTemplate.PatientID, 0, ref aDoc, false, "");
                    }
                    thisLoadWord.CloseWordOnly(ref aDoc);
                }
                catch (Exception ex1)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex1.Message, false);
                }
                thisLoadWord.CloseApplicationOnly();
                thisLoadWord = null;

                //aDoc.Close();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
            }
            finally
            {

                if (Background != null) { Background = null; }
                if (Range != null) { Range = null; }
                //if (wordApplication != null) { wordApplication.Application.Quit(ref missing, ref missing, ref missing); }
                if (aDoc != null) { aDoc = null; }

                if (oFileName != null) { oFileName = null; }
                if (missing != null) { missing = null; }
                if (oFileFormat != null) { oFileFormat = null; }
                //Bug #56871: Automation: Receipt Print Issue: gloPM Application Hanged after 178 Iteration
                //Delete file created for temporary purpose. 
                if (System.IO.File.Exists(sFileName)) { File.Delete(sFileName); }
                if (!string.IsNullOrEmpty(sFileName)) { sFileName = string.Empty; }
                if (PageType != null) { PageType = null; }
                if (myLoadWord != null)
                {
                    myLoadWord.CloseApplicationOnly();
                    myLoadWord = null;
                }
                CloseMyLoadWordApplication();
            }
        }
        private gloWord.LoadAndCloseWord pLoadWordApplication = null;
        private gloWord.LoadAndCloseWord GetMyLoadWordApplication()
        {
            if (((pLoadWordApplication == null)))
            {
                pLoadWordApplication = new gloWord.LoadAndCloseWord();
                pLoadWordApplication.LoadApplicationOnly();
            }
            else
            {
                if ((pLoadWordApplication.CheckWordApplicationLocked()))
                {
                    pLoadWordApplication = new gloWord.LoadAndCloseWord();
                    pLoadWordApplication.LoadApplicationOnly();
                }
            }
            return pLoadWordApplication;
        }

        private void CloseMyLoadWordApplication()
        {
            if (((pLoadWordApplication == null) == false))
            {
                pLoadWordApplication.CloseApplicationOnly();
                pLoadWordApplication = null;
            }
        }
        gloPrintDialog.gloPrintDialog oDialogAll = null;
        string strAllOldPrinterName = "";
        System.Drawing.Printing.PrintDocument DefaultPrintDocument = new System.Drawing.Printing.PrintDocument();
        public void PrintWordDocument(ref gloWord.LoadAndCloseWord myLoadWord, string sFileName, bool bIsPrintFlag, long m_patientID, int totalPages,
            ref Wd.Document wdDoc, bool blnPrintCancel, string _PreviousUsedPrinter, int PageNo = 0, bool UseDirectFaxName = false, IWin32Window iOwner = null, int PrintDocno = 0, bool blnShowPrinterDialog = true)
        {
            string oDialogAllPrinterName = "";
            //if ((blnAllIsCancel == false))
            //{
            Wd.Document oTempDoc = null;
            //bool Opened = false;
            string sFileNameForPrintOrFax = "";
            object Missing = System.Reflection.Missing.Value;
            Microsoft.Office.Interop.Word.WdStatistic PageCountStat = Microsoft.Office.Interop.Word.WdStatistic.wdStatisticPages;

            //if ((UseDirectFaxName))
            //{
            //    sFileNameForPrintOrFax = sFileName;
            //    if (!File.Exists(sFileNameForPrintOrFax))
            //    {
            //        MessageBox.Show("Error while printing or faxing. Please try again.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }

            //}
            // else
            // {
            sFileNameForPrintOrFax = gloGlobal.clsFileExtensions.NewDocumentName(gloSettings.FolderSettings.AppTempFolderPath, ".docx", "MMddyyyyHHmmssffff");

            try
            {
                File.Copy(sFileName, sFileNameForPrintOrFax);

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.Notes, gloAuditTrail.ActivityType.General, "Unable to Copy file before printing. Source path:= '" + sFileName + "' ; Destination Path :='" + sFileNameForPrintOrFax + "'" + " Exception: " + ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

            if (!File.Exists(sFileNameForPrintOrFax))
            {
                MessageBox.Show("Error while printing . Please try again.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((totalPages == 0))
            {
                try
                {
                    oTempDoc = myLoadWord.LoadWordApplication(sFileNameForPrintOrFax);
                    oTempDoc.Application.Visible = false;
                    totalPages = oTempDoc.ComputeStatistics(PageCountStat, Missing);
                    //'added for bugid 96435

                    myLoadWord.CloseWordOnly(ref oTempDoc);
                }
                catch (Exception)
                {
                }

            }
            // }


            if (bIsPrintFlag)
            {
                string strpatname = "";

                //if (gblnPageNo == true)
                //{
                //    strpatname = GetPatientDetails(m_patientID);

                //}
                gloWord.clsPrintWordQueue obj = new gloWord.clsPrintWordQueue();
                obj.FilePath = sFileNameForPrintOrFax;


                try
                {

                    //'  Using oDialog As New gloPrintDialog.gloPrintDialog(True)
                    if (oDialogAll == null)
                    {
                        oDialogAll = new gloPrintDialog.gloPrintDialog(true);
                    }
                    // Dim strOldPrinterName As String = String.Empty
                    oDialogAll.ConnectionString = _databaseConnectionString;

                    oDialogAll.TopMost = true;
                    oDialogAll.ShowPrinterProfileDialog = true;
                    oDialogAll.ModuleName = "WordPrinting";

                    oDialogAll.RegistryModuleName = "WordPrinting";

                    if (oDialogAll != null)
                    {
                        //Dim printDocument1 As New System.Drawing.Printing.PrintDocument()

                        if ((!gloGlobal.gloTSPrint.isCopyPrint))
                        {

                            oDialogAll.AllowSomePages = true;
                            if ((PrintDocno == 0))
                            {
                                oDialogAll.PrinterSettings = DefaultPrintDocument.PrinterSettings;
                            }

                            if ((totalPages == 0)) //Bug #104599  Modify print window should show correct page numbers.
                            {
                                try
                                {
                                    oTempDoc = myLoadWord.LoadWordApplication(sFileNameForPrintOrFax);
                                    oTempDoc.Application.Visible = false;
                                    totalPages = oTempDoc.ComputeStatistics(PageCountStat, Missing);
                                    //'added for bugid 96435
                                    myLoadWord.CloseWordOnly(ref oTempDoc);

                                }
                                catch (Exception)
                                {
                                }
                            }


                            oDialogAll.PrinterSettings.ToPage = totalPages;

                            oDialogAll.PrinterSettings.FromPage = 1;
                            oDialogAll.PrinterSettings.MaximumPage = oDialogAll.PrinterSettings.MaximumPage;

                            oDialogAll.PrinterSettings.MinimumPage = 1;
                        }
                        oDialogAll.bEnableLocalPrinter = gloGlobal.gloTSPrint.isCopyPrint;



                        if ((PrintDocno == 0))
                        {

                            if ((!gloGlobal.gloTSPrint.isCopyPrint))
                            {


                                try
                                {
                                    strAllOldPrinterName = oDialogAll.PrinterSettings.PrinterName;

                                }
                                catch (Exception)
                                {
                                }
                            }

                            if (oDialogAll.ShowDialog(iOwner) == System.Windows.Forms.DialogResult.OK)
                            {
                                if ((oDialogAll.bUseDefaultPrinter == true))
                                {
                                    oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                    oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = true;
                                }
                                if (gloGlobal.gloTSPrint.isCopyPrint && (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false) == false))
                                {
                                    oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                    oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = false;
                                }
                                if ((!gloGlobal.gloTSPrint.isCopyPrint))
                                {
                                    oDialogAllPrinterName = oDialogAll.PrinterSettings.PrinterName;
                                    if (oDialogAllPrinterName != strAllOldPrinterName)
                                    {
                                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(oDialogAllPrinterName);
                                        Application.DoEvents();
                                    }
                                }
                                gloWord.frmgloPrintQueueController objogloPrintProgressController = new gloWord.frmgloPrintQueueController(oDialogAll.PrinterSettings, oDialogAll.CustomPrinterExtendedSettings);
                                objogloPrintProgressController.gblnPageNo = true;
                                objogloPrintProgressController.strpatname = strpatname;
                                objogloPrintProgressController.oldPrinterName = oDialogAllPrinterName;
                                objogloPrintProgressController.lstgloTemplate.Add(obj);
                                objogloPrintProgressController.lnPatientId = m_patientID.ToString();

                                if (oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint)
                                {
                                    if (oDialogAll.CustomPrinterExtendedSettings.IsShowProgress)
                                    {
                                        objogloPrintProgressController.Show();
                                    }
                                    else
                                    {
                                        objogloPrintProgressController.Show();
                                    }
                                }
                                else
                                {
                                    Form myCtrl = null;
                                    try
                                    {
                                        IntPtr handle = this.Handle;// GetActiveWindow();
                                        myCtrl = (Form)Control.FromHandle(handle);
                                    }
                                    catch (Exception)
                                    {

                                    }


                                    objogloPrintProgressController.TopMost = true;
                                    objogloPrintProgressController.ShowInTaskbar = false;

                                    if ((myCtrl != null))
                                    {
                                        objogloPrintProgressController.ShowDialog(myCtrl);
                                    }
                                    else
                                    {
                                        objogloPrintProgressController.ShowDialog();
                                    }
                                    if (objogloPrintProgressController != null)
                                    {
                                        objogloPrintProgressController.Dispose();
                                    }
                                    objogloPrintProgressController = null;


                                }
                                //if
                            }
                            else
                            {
                                //  blnAllIsCancel = true;

                            }

                        }
                        else
                        {
                            if ((oDialogAll.bUseDefaultPrinter == true))
                            {
                                oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = true;
                            }
                            if (gloGlobal.gloTSPrint.isCopyPrint && (gloGlobal.gloTSPrint.IsDefaultPrinterOn(chkTSPrintSetting: false) == false))
                            {
                                oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint = true;
                                oDialogAll.CustomPrinterExtendedSettings.IsShowProgress = false;
                            }
                            if ((!gloGlobal.gloTSPrint.isCopyPrint))
                            {
                                if ((oDialogAllPrinterName != string.Empty))
                                {
                                    oDialogAll.PrinterSettings.PrinterName = oDialogAllPrinterName;
                                }
                            }
                            gloWord.frmgloPrintQueueController objogloPrintProgressController = new gloWord.frmgloPrintQueueController(oDialogAll.PrinterSettings, oDialogAll.CustomPrinterExtendedSettings);
                            objogloPrintProgressController.gblnPageNo = true;
                            objogloPrintProgressController.strpatname = strpatname;
                            objogloPrintProgressController.oldPrinterName = oDialogAllPrinterName;
                            objogloPrintProgressController.lstgloTemplate.Add(obj);
                            objogloPrintProgressController.lnPatientId = m_patientID.ToString();

                            if (oDialogAll.CustomPrinterExtendedSettings.IsBackGroundPrint)
                            {
                                if (oDialogAll.CustomPrinterExtendedSettings.IsShowProgress)
                                {
                                    objogloPrintProgressController.Show();
                                }
                                else
                                {
                                    objogloPrintProgressController.Show();
                                }
                            }
                            else
                            {
                                Form myCtrl = null;
                                try
                                {
                                    IntPtr handle = this.Handle;
                                    myCtrl = (Form)Control.FromHandle(handle);
                                }
                                catch (Exception)
                                {

                                }


                                objogloPrintProgressController.TopMost = true;
                                objogloPrintProgressController.ShowInTaskbar = false;

                                if ((myCtrl != null))
                                {
                                    objogloPrintProgressController.ShowDialog(myCtrl);
                                }
                                else
                                {
                                    objogloPrintProgressController.ShowDialog();
                                }
                                if (objogloPrintProgressController != null)
                                {
                                    objogloPrintProgressController.Dispose();
                                }
                                objogloPrintProgressController = null;


                            }
                            //if
                        }
                        //' End Using
                        //'oDialogAll IsNot Nothing 
                    }
                    else
                    {
                        string _ErrorMessage = "Error in Showing Print Dialog";

                        if (!string.IsNullOrEmpty(_ErrorMessage.Trim()))
                        {
                            string _MessageString = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") + _ErrorMessage;
                            //      gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                            _MessageString = "";
                        }


                        MessageBox.Show(_ErrorMessage, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                catch (Exception ex)
                {

                    string _ErrorMessage = ex.ToString();

                    if (!string.IsNullOrEmpty(_ErrorMessage.Trim()))
                    {
                        string _MessageString = Convert.ToString("Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : ") + _ErrorMessage;
                        //  gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }



                    MessageBox.Show(ex.Message, _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex = null;



                }

                finally
                {
                    if (oDialogAllPrinterName != strAllOldPrinterName)
                    {
                        gloGlobal.gloTSPrint.SetDefaultPrinterSettings(strAllOldPrinterName);
                        Application.DoEvents();
                    }
                }

            }

        }

        private void InsertFile(String InsertItem)
        {
            try
            {
                OpenFileDialog oFileDialog = new OpenFileDialog();
                object missing = System.Reflection.Missing.Value;

                if (InsertItem == "InsertFile")
                {
                    oFileDialog.Filter = "Text Files (*.txt)|*.txt|Word 97-2003 Documents (*.doc)|*.doc|Word Documents (*.docx)|*.docx|Rich Text Format (*.rtf)|*.rtf";
                    oFileDialog.FilterIndex = 3;
                    oFileDialog.Title = "Insert External Documents";
                    oFileDialog.Multiselect = false;
                    if (oFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        FileInfo oFile = new FileInfo(oFileDialog.FileName);
                        if (oFile.Extension.ToUpper() == ".TXT" || oFile.Extension.ToUpper() == ".DOC" || oFile.Extension.ToUpper() == ".DOCX" || oFile.Extension.ToUpper() == ".RTF")
                        {
                            oCurDoc.Application.Selection.InsertFile(oFile.FullName, ref missing, ref missing, ref missing, ref missing);
                        }
                    }
                }
                else if (InsertItem == "InsertImage")
                {
                    oFileDialog.Filter = "JPEG Image (*.JPEG,*.JPG)|*.JPEG;*.JPG|TIFF Image (*.TIFF,*.TIF)|*.TIFF;*.TIF|BITMAP Image (*.BMP)|*.BMP|PNG Image (*.PNG)|*.PNG";
                    oFileDialog.FilterIndex = 1;
                    oFileDialog.Title = "Insert External Image";
                    oFileDialog.Multiselect = true;
                    if (oFileDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        FileInfo oFile;
                        foreach (String CurFile in oFileDialog.FileNames)
                        {
                            oFile = new FileInfo(CurFile);
                            if (oFile.Extension.ToUpper() == ".JPEG" || oFile.Extension.ToUpper() == ".JPG" || oFile.Extension.ToUpper() == ".TIFF" || oFile.Extension.ToUpper() == ".TIF" || oFile.Extension.ToUpper() == ".BMP" || oFile.Extension.ToUpper() == ".PNG")
                            {
                                oCurDoc.Application.Selection.InlineShapes.AddPicture(oFile.FullName, ref missing, ref missing, ref missing);
                            }
                        }
                    }
                }

                if (oFileDialog != null)
                {
                    oFileDialog.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Opening File " + ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #endregion

        private void dtp_DOS_ValueChanged(object sender, EventArgs e)
        {
            gloOffice.Supporting.FromDate = gloDateMaster.gloDate.DateAsNumber(dtp_DOS.Value.ToString("MM/dd/yyyy"));
            gloOffice.Supporting.ToDate = gloDateMaster.gloDate.DateAsNumber(dtp_DOS.Value.ToString("MM/dd/yyyy"));
            gloOffice.Supporting.GetFormFieldData(null);
        }

        private void wdTemplate_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        {
            try
            {
                if (oWordApp != null)
                {
                    oWordApp.WindowSelectionChange -= DDLCBEvent;
                    oWordApp.WindowBeforeDoubleClick -= OnFormClicked;

                    foreach (Wd.RecentFile oFile in oWordApp.RecentFiles)
                    {
                        if (oFile != null)
                        {
                            try
                            {
                                if (oFile.Path == gloSettings.FolderSettings.AppTempFolderPath)
                                {
                                    try
                                    {
                                        oFile.Delete();
                                    }
                                    catch //(Exception ex)
                                    {
                                        // gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure);
                                        // ex = null;
                                    }
                                }
                            }
                            catch //(Exception ex)
                            {
                                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.VoiceCommands, gloAuditTrail.ActivityType.General, ex.ToString, gloAuditTrail.ActivityOutCome.Failure);
                                //ex = null;
                            }
                        }
                    }


                }
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }


        }

        private void tmrDocProtect_Tick(object sender, EventArgs e)
        {

            try
            {
                tmrDocProtect.Stop();
                Wd.TaskPane protectPane = oCurDoc.ActiveWindow.Application.TaskPanes[Wd.WdTaskPanes.wdTaskPaneDocumentProtection];
                if (protectPane != null)
                {
                    protectPane.Visible = false;
                    try
                    {
                        Marshal.ReleaseComObject(protectPane);
                        protectPane = null;
                    }
                    catch
                    {
                    }
                }


                //  oCurDoc.Application.TaskPanes[Wd.WdTaskPanes.wdTaskPaneDocumentProtection].Visible = false;
            }
            catch
            { }
            finally
            {
                tmrDocProtect.Start();
            }

        }

        //public 
    }
}