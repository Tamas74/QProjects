using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;


namespace gloOffice
{
    public partial class frmWd_PatientExam : Form
    {
        //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
        //[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        //public static extern bool SetDefaultPrinter(string Name);

        #region " Private Variables "
        private string _EMRConnectionString = "";
        private string _PMConnectionString = "";
        private string _MessageBoxCaption = String.Empty;
        private Int64 _ExamID;
        private Int64 _PatientID;
        private String _NewDocumentName = "";

        private Wd.Document oCurDoc;
        private Wd.Application oWordApp;

        private string _EMRType = "";
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Boolean _isRecoverExam = false;
        #endregion

        #region " Constructor "

        public frmWd_PatientExam(String gloPM_ConnectionString, Int64 gloEMR_ExamID)
        {
            _ExamID = gloEMR_ExamID;
            _PMConnectionString = gloPM_ConnectionString;

            // READ gloEMR CONNECTION STRING FROM SETTINGS. //
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _EMRConnectionString = appSettings["EMRConnectionString"];

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


            #region " PM & EMR Connection Setting "


            _EMRType = GetSetting("MigrateToEMRType");

            if (_EMRType == "gloEMR50")
            {
         //    _PMConnectionString = _PMConnectionString;
                _EMRConnectionString = _PMConnectionString;

            }
            else if (_EMRType == "gloEMR40" || _EMRType == "gloEMR40SP2")
            {
          //    _PMConnectionString = _PMConnectionString;
         //       _EMRConnectionString = _EMRConnectionString;
            }
            else
            {
           //     _PMConnectionString = _PMConnectionString;
           //     _EMRConnectionString = _EMRConnectionString;
            }

            #endregion " PM & EMR Connection Setting "
        }
        //Added for Recover Exam Module
        public frmWd_PatientExam(String gloPM_ConnectionString, Int64 gloEMR_AuditExamID, Int64 gloEmrPatientId, bool isRecoverExam)
        {
            _ExamID = gloEMR_AuditExamID;
            _PatientID = gloEmrPatientId;
            _PMConnectionString = gloPM_ConnectionString;

            // READ gloEMR CONNECTION STRING FROM SETTINGS. //
            System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
            _EMRConnectionString = appSettings["EMRConnectionString"];
            _isRecoverExam = isRecoverExam;

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


            #region " PM & EMR Connection Setting "


            _EMRType = GetSetting("MigrateToEMRType");

            if (_EMRType == "gloEMR50")
            {
              //  _PMConnectionString = _PMConnectionString;
                _EMRConnectionString = _PMConnectionString;

            }
            else if (_EMRType == "gloEMR40" || _EMRType == "gloEMR40SP2")
            {
              //  _PMConnectionString = _PMConnectionString;
              //  _EMRConnectionString = _EMRConnectionString;
            }
            else
            {
              //  _PMConnectionString = _PMConnectionString;
              //  _EMRConnectionString = _EMRConnectionString;
            }

            #endregion " PM & EMR Connection Setting "
        }

        #endregion

        private void frmWd_PatientExam_Load(object sender, EventArgs e)
        {
            Supporting.DataBaseConnectionString = _PMConnectionString;
            //_ExamID = 391973367396979201; // HARD CODED VALUE //           
            LoadExam();
        }

        private void LoadExam()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_EMRConnectionString); // CONNECTION WITH gloEMR Connection String 
            gloTemplate ogloTemplate = new gloTemplate(_PMConnectionString);
            gloWord.gloWord oWord = new gloWord.gloWord();
            DataSet ds = new DataSet();
            object objExam = new object();
            try
            {
                String _Query = "";
                //Added for Recover Exam Module
               // _Query = "SELECT sPatientNotes FROM PatientExams WHERE nExamID = " + _ExamID;
                if (_isRecoverExam == false)
                    _Query = "SELECT sPatientNotes FROM PatientExams WHERE nExamID = " + _ExamID;
                else
                    _Query = "SELECT sPatientNotes FROM PatientExams_Audit WHERE   PatientExamsAuditID = " + _ExamID;
                oDB.Connect(false);
                oDB.Retrive_Query(_Query, out ds);
                oDB.Disconnect();
                if (ds.Tables[0] != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        object missing = System.Reflection.Missing.Value;
                        _NewDocumentName = Supporting.NewDocumentName();
                        objExam = ds.Tables[0].Rows[0]["sPatientNotes"];
                        ogloTemplate.ConvertBinaryToFile(objExam, _NewDocumentName);
                        if (File.Exists(_NewDocumentName))
                        {
                           // wdExam.Open(_NewDocumentName);
                            object thisObject = (object)_NewDocumentName;
                           // Wd.Application oWordApp = null;
                            gloWord.LoadAndCloseWord.OpenDSO(ref wdExam, ref thisObject, ref oCurDoc, ref oWordApp);
                            _NewDocumentName = (string)thisObject;
                            wdExam.Toolbars = false;  // WILL HIDE TOOLBAR. AS IT IS READONLY DOCUMENT //

                            if (oCurDoc.Application.ActiveDocument.ProtectionType != Wd.WdProtectionType.wdAllowOnlyComments)
                            {
                                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyComments, ref missing, ref missing, ref missing, ref missing);
                            }
                        }
                                              

                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (ds != null) { ds.Dispose(); }
                if (ogloTemplate != null) { ogloTemplate.Dispose(); }
            }

        }

        private String GetSetting(String SettingName)
        {
            gloSettings.GeneralSettings ogloSettings = new gloSettings.GeneralSettings(_PMConnectionString);
            object value = new object();
            string _Result = "";
            try
            {

                ogloSettings.GetSetting(SettingName, out value);
                if (value != null && Convert.ToString(value) != "")
                {
                    _Result = Convert.ToString(value);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = "";
            }
            finally
            {
                ogloSettings.Dispose();
                ogloSettings = null;
            }
            return _Result;
        }

        private void ts_Commands_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag.ToString())
            {
                case "Close":
                    {
                        this.Close();
                    }
                    break;
            }
        }

        private void wdExam_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            oCurDoc = (Microsoft.Office.Interop.Word.Document)wdExam.ActiveDocument;
            oWordApp = oCurDoc.Application;
        }

        private void wdExam_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                if (oCurDoc != null)
                {
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }

                //if (oWordApp != null)
                //{
                //  //  Marshal.FinalReleaseComObject(oWordApp);
                //    oWordApp = null;
                //}
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
        }

        private void wdExam_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        {
            try
            {
                if ((oWordApp != null))
                {
                    //oWordApp.WindowSelectionChange -= DDLCBEvent;
                    //oWordApp.WindowBeforeDoubleClick -= OnFormClicked;

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

        private void tsb_Print_Click(object sender, EventArgs e)
        {
            DocumentPrintOut(oCurDoc);
        }
        private void DocumentPrintOut(Wd.Document CurrentDocument)
        {
           // Microsoft.Office.Interop.Word.Application wordApplication = default(Microsoft.Office.Interop.Word.Application);
            Microsoft.Office.Interop.Word.Document aDoc = null;
            //Bug ID: 00000295 (Printing - EMR)
            //Reason: To resolve reported exception.
            //Description: Set background =false referring link http://www.xtremevbtalk.com/showthread.php?t=55010
            //Changed to resolve the office template issue : #63993: Coding and Unit Testing - 00000112
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
        //    wordApplication = new Microsoft.Office.Interop.Word.Application();
            string sFileName = gloOffice.Supporting.NewDocumentName();
            //Bug #63771: 00000624: Patient Statement Print
            //When you click the 'Print' button, nothing happens at all.
            //if (_isFromPatientAccount)
            //{
            //    if (_gloTemplate.TemplateName.Contains("PatientStatement"))
            //    { sFileName = sFileName.Replace(".docx", ".doc"); }
            //}
         
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

                gloWord.LoadAndCloseWord thisLoadWord = new gloWord.LoadAndCloseWord();
                try
                {
                    aDoc = thisLoadWord.LoadWordApplication(sFileName);
                    oFileName = (object)sFileName;
                    oFileFormat = (object)Wd.WdSaveFormat.wdFormatXMLDocument;
                    aDoc.SaveAs(ref oFileName, ref oFileFormat, ref missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing, ref  missing);

                    //Bug #63771: 00000624: Patient Statement Print
                    //When you click the 'Print' button, nothing happens at all.
                    //if (!_isFromPatientAccount)
                    //{
                    //    gloWord.gloWord.CurrentDoc = aDoc;
                    //    gloWord.gloWord.CleanupDocument();
                    //}
                    //  gloWord.gloWord.CurrentDoc = aDoc;
                    //  gloWord.gloWord.CleanupDocument();

                    gloWord.LoadAndCloseWord.CleanupDoc(ref aDoc);

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
                              ref PrintZoomRow, ref missing, ref missing,_PatientID);


                    }
                    else
                    {
                        PrintDialog pd = new PrintDialog();
                        //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                        string strTempDefaultPrinter = "";
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            strTempDefaultPrinter = pd.PrinterSettings.PrinterName;
                        }
                        if (pd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (!gloGlobal.gloTSPrint.isCopyPrint)
                            {
                                //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                                if (strTempDefaultPrinter != pd.PrinterSettings.PrinterName)
                                {
                                    gloGlobal.gloTSPrint.SetDefaultPrinterSettings(pd.PrinterSettings.PrinterName);
                                    Application.DoEvents();
                                }
                                //aDoc.Application.ActivePrinter = pd.PrinterSettings.PrinterName;

                                Copies = pd.PrinterSettings.Copies;
                                PrintToFile = pd.PrinterSettings.PrintToFile;
                            }
                            //aDoc.Application.Options.PrintBackground = true;
                            gloWord.LoadAndCloseWord.PrintDocument(ref aDoc, ref Background, ref missing, ref missing, ref missing,
                                  ref missing, ref missing, ref missing, ref Copies,
                                  ref missing, ref missing, ref PrintToFile, ref Collate,
                                  ref missing, ref ManualDuplexPrint, ref PrintZoomColumn,
                                  ref PrintZoomRow, ref missing, ref missing,_PatientID);

                        }
                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                        {
                            //Bug #82465: 00000904 : Practice gets an error when attempting to print batch templates inside of the PM
                            if (strTempDefaultPrinter != pd.PrinterSettings.PrinterName)
                            {
                                gloGlobal.gloTSPrint.SetDefaultPrinterSettings(strTempDefaultPrinter);
                                Application.DoEvents();
                            }

                            //aDoc.Application.ActivePrinter = strTempDefaultPrinter;//Default
                            Copies = 1; //Default
                            PrintToFile = false;//Default
                        }
                        pd.Dispose();
                        pd = null;
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
            }
        }

    }
}