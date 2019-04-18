using System;
using System.Collections.Generic;
using System.Text;
using gloEDocumentV3.Enumeration;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.SqlClient;
namespace gloEDocumentV3
{
    public class gloEDocV3Management : IDisposable
    {
        public delegate void RefreshDocuments();
        public event RefreshDocuments EvnRefreshDocuments;
        public delegate void ActivateReciveFax(Int64 PatientID);
        public event ActivateReciveFax EvnActivateReciveFax;
        public delegate void AcknowledgeTask();
        public event AcknowledgeTask EvnAcknowledgeTask;
        private string _ErrorMessage = "";
        bool _HasError = false;
       
       

        #region "Constructor & Destructor"

        public gloEDocV3Management()
        {
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                    oPatientExam = null;
                    oPatientLetters = null;
                    oPatientMessages = null;
                    oNurseNotes = null;
                    oHistory = null;
                    oLabs = null;
                    oDMS = null;
                    oRxmed = null;
                    oOrders = null;
                    oProblemList = null;
                    oCriteria = null;
                    oWord = null;
                    dMdi = null;



                    //            DisposeObjects();
                }
            }
            disposed = true;
        }
        //private void DisposeObjects()
        //{


             
        //}
        ~gloEDocV3Management()
        {
            Dispose(false);
        }

        #endregion

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; }
        }

        public bool HasError
        {
            get { return _HasError; }
            set { _HasError = value; }
        }
        DataTable _dtFaxDetails = null;
        public DataTable dtFaxDetails
        {
            get { return _dtFaxDetails; }
            set { _dtFaxDetails = value; }
        }
        public bool DocumentFaxed { get; set; }
       
        #region "Dhruv 2010 -> ShowEDocumentCategory"

        public void ShowEDocumentCategory(enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
        {
            using (Forms.frmEDocEvent_AddCategory ofrmCategory = new Forms.frmEDocEvent_AddCategory())
            {
                if (ofrmCategory != null)
                {
                    ofrmCategory._OpenExternalSource = _OpenExternalSource;
                    ofrmCategory.ShowDialog(ofrmCategory.Parent);
                    
                }
            }
        }
        #endregion "Dhruv 2010 -> ShowEDocumentCategory"



        #region "Dhruv 2010 -> ShowEMigration"
       
        public bool ShowEMigration(Int64 PatientID, string DMSV1Path, bool StartAutomatic)
        {
            bool oResult = false;
            //gloEDocumentV3.eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
            gloDMSMigration.ClsDMSMigrationGeneral.BufferSize = gloEDocV3Admin.gBufferSize;
            gloDMSMigration.ClsDMSMigrationGeneral.ConnectionString = gloEDocV3Admin.gDatabaseConnectionString;
            gloDMSMigration.ClsDMSMigrationGeneral.DeleteAfterMigration = gloEDocV3Admin.gDeleteAfterMigration;
            gloDMSMigration.ClsDMSMigrationGeneral.DMSOutputFilepathV1toV3 = DMSV1Path;
            gloDMSMigration.ClsDMSMigrationGeneral.gFileSizeMax = gloEDocV3Admin.gnDMSV3FileSizeMax;
            gloDMSMigration.ClsDMSMigrationGeneral.gFileSizeMin = gloEDocV3Admin.gnDMSV3FileSizeMin;
            gloDMSMigration.ClseDocV3MigrationV2ToV3 oMigrationV2ToV3 = new gloDMSMigration.ClseDocV3MigrationV2ToV3(gloDMSMigration.ClsDMSMigrationGeneral.ConnectionString);
                if (oMigrationV2ToV3 != null)
                {
                    System.Data.DataTable dt = oMigrationV2ToV3.CanMigrateDocuments(PatientID);

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            using (Forms.frmEDocEvent_Migrate oEDocumentMigration = new gloEDocumentV3.Forms.frmEDocEvent_Migrate(PatientID, DMSV1Path, StartAutomatic))
                            {
                                if (oEDocumentMigration != null)
                                {
                                    oEDocumentMigration.dtMigrateTable = dt;
                                    oEDocumentMigration.ShowDialog(oEDocumentMigration.Parent);
                                    oResult = oEDocumentMigration.oDialogResultIsOK;
                                   
                                }
                            }
                        }
                        else
                        {
                            oResult = true;
                        }

                    }
                    else
                    {
                        oResult = true;
                    }
                    if (oMigrationV2ToV3 != null)
                    {
                        oMigrationV2ToV3 = null;
                    }
            }
            return oResult;
        }


        #endregion "Dhruv 2010 -> ShowEMigration"


        #region "Dhruv 2010 -> ShowEReceivedFax"
        public void ShowEReceivedFax(string ReceivedFaxFilesFolderPath, string Category, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
        {
            gloEDocumentV3.Forms.frmEDocEvent_ReceivedFax oERecFax = new gloEDocumentV3.Forms.frmEDocEvent_ReceivedFax(ReceivedFaxFilesFolderPath, Category);
            oERecFax.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocEvent_ReceivedFax.RefreshDocuments(oEDocument_EvnRefreshDocuments);
            oERecFax.EvnActivateReciveFax += new gloEDocumentV3.Forms.frmEDocEvent_ReceivedFax.ActivateReciveFax(oEDocument_EvnEvnActivateReciveFax);
            oERecFax.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oERecFax.MdiParent = oMdiParent;
            oERecFax._OpenExternalSource = _OpenExternalSource;
           
            oERecFax.Show();
            
        }

        #endregion "Dhruv 2010 -> ShowEReceivedFax"
        #region "Mayuri 2015 -> ShowEDMSFromDirect"
        public void ShowEDMSFromDirect(string strNonXMLPath, enum_OpenExternalSource OpenExternalSource, Int64 DMSPatientID)
        {
            gloEDocumentV3.Forms.frmEDocEvent_ReceivedFax oERecFax = new gloEDocumentV3.Forms.frmEDocEvent_ReceivedFax();
           oERecFax.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oERecFax._OpenExternalSource = OpenExternalSource;
           
                oERecFax.DMSPatientID = DMSPatientID;
           
            oERecFax.strNonXMLPath = strNonXMLPath;
            oERecFax.ShowDialog();
            if (oERecFax != null)
            {
                oERecFax.Dispose();
                oERecFax = null;
            }

        }

        #endregion "Mayuri 2015 -> ShowEDMSFromDirect"
        #region "Dhruv 2010 -> ShowECleanDocument"

        public bool ShowECleanDocument(Int64 PatientID)
        {
            bool _Result = false;
            using (Forms.frmEDocEvent_CleanPatientDocument oDocCleanEvents = new gloEDocumentV3.Forms.frmEDocEvent_CleanPatientDocument())
            {
                if (oDocCleanEvents != null)
                {
                  
                    oDocCleanEvents.oPatientID = PatientID;
                    oDocCleanEvents.ShowDialog(oDocCleanEvents.Parent);

                    if (oDocCleanEvents.oDialogResultIsOK == true)
                    {
                        _Result = true;
                    }
                    //oDocCleanEvents.Dispose();
                 
                }
   
            }
            return _Result;
        }

        #endregion "Dhruv 2010 -> ShowECleanDocument"

        public bool ValidateScanInCurrentYear(string _SelectedYear,string FormName)
        {
            bool _Result = false;
            string _message = string.Empty;
            try
            {
                if (Convert.ToInt16(_SelectedYear) < Convert.ToInt16(DateTime.Now.Year))
                {
                    if (string.Compare(FormName, "Scan", true) == 0)
                    {
                        _message = "You have chosen to scan into a previous year," + _SelectedYear + "." + Environment.NewLine + "Would you like to continue?";
                    }
                    else
                    {
                        _message = "You have chosen to import into a previous year," + _SelectedYear + "." + Environment.NewLine + "Would you like to continue?";
                    }


                    if (MessageBox.Show(_message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        _Result = true;
                    }
                }
                else
                    if (Convert.ToInt16(_SelectedYear) > Convert.ToInt16(DateTime.Now.Year))
                    {
                        if (string.Compare(FormName, "Scan", true) == 0)
                        {
                            _message = "You have chosen to scan into a next year," + _SelectedYear + "." + Environment.NewLine + "Would you like to continue?";
                        }
                        else
                        {
                            _message = "You have chosen to import into a next year," + _SelectedYear + "." + Environment.NewLine + "Would you like to continue?";
                        }


                        if (MessageBox.Show(_message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            _Result = true;
                        }
                    } 

                else
                {
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                string _ErrorMessage = ex.ToString();
                ErrorMessagees(_ErrorMessage);
            }
             return _Result;
        }


        #region "Dhruv 2010 -> ShowEScannerForImages"
       
        public bool ShowEScannerForImages(Int64 PatientID, out ArrayList ScanImageFiles)
        {
            
            
                ArrayList _ScanImageFiles = new ArrayList();
                bool _Result = false;
                try
                {
                    using (Forms.frmEDocEvent_ScanNSend_PS oDocEvents = new gloEDocumentV3.Forms.frmEDocEvent_ScanNSend_PS(true))
                    {
                        if (oDocEvents != null)
                        {
                            oDocEvents.oPatientID = PatientID;
                            oDocEvents.ShowDialog(oDocEvents.Parent);
                            if (oDocEvents.oDialogResultIsOK == true)
                            {
                                _Result = true;
                                _ScanImageFiles = oDocEvents.oDialogScanImages;
                            }
                            //oDocEvents.Dispose();
                           // ScanImageFiles = _ScanImageFiles;
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                ScanImageFiles = _ScanImageFiles;
                return _Result;
            
        }

        #endregion "Dhruv 2010 -> ShowEScannerForImages"

        #region "Dhruv 2010 -> ShowEScanner"
        
        public bool ShowEScanner(Int64 PatientID, string ScanInCategory, string ScanInYear, string ScanInMonth, Int64 ClinicID, enum_DocumentEventType EventType, out Int64 OutPutContainerID, out Int64 OutPutDocumentID)
        {
           // ArrayList _ScanImageFiles = new ArrayList();
            bool _Result = false;
            OutPutContainerID = 0;
            OutPutDocumentID = 0;
            using (gloEDocumentV3.Forms.frmEDocEvent_ScanNSend_PS oDocEvents = new gloEDocumentV3.Forms.frmEDocEvent_ScanNSend_PS(false))
            {
                
               try
                {

                    if (oDocEvents != null)
                    {
                        oDocEvents.oPatientID = PatientID;
                        oDocEvents.oScanInCategory = ScanInCategory;
                        oDocEvents.oScanInCategoryID = gloEDocumentV3.eDocManager.eDocValidator.GetCategoryId(ScanInCategory, ClinicID);
                        oDocEvents.oScanInYear = ScanInYear;
                        oDocEvents.oScanInMonth = ScanInMonth;
                        oDocEvents.oClinicID = ClinicID;
                        oDocEvents.oEventType = EventType;
                        oDocEvents.ShowDialog(oDocEvents.Parent);
                        if (oDocEvents.oDialogResultIsOK == true)
                        {
                            _Result = true;
                            OutPutContainerID = oDocEvents.oDialogContainerID;
                            OutPutDocumentID = oDocEvents.oDialogDocumentID;
                        }
                    }
                }
                catch (Exception ex)
                {
                    string _ErrorMessage =  ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                }

                return _Result;
            }
        }
        #endregion "Dhruv 2010 -> ShowEScanner"

        private void ErrorMessagees(string _ErrorMessage)
        {
            #region " Make Log Entry "
            try
            {
                if (_ErrorMessage.Trim() != "")
                {
                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                    _MessageString = "";
                }
            }
            catch (Exception ex)
            {
                string _ErrorHere = ex.ToString();
                MessageBox.Show("Unable to update Log with " + _ErrorMessage, _ErrorHere);
            }

            //End Code add
            #endregion " Make Log Entry "

        }

        public void oEDocument_EvnEvnActivateReciveFax(Int64 PatientID)
        {
            EvnActivateReciveFax(PatientID);
        }
        public void oEDocument_EvnRefreshDocuments()
        {
            try
            {
                EvnRefreshDocuments();
            }
            catch //(Exception ex)
            { }
        }

        public void oEDocument_EvnAckTask() //added for task incident if task acknowledge then refresh task screen depend on setting
        {
            try
            {
                EvnAcknowledgeTask();
            }
            catch //(Exception ex)
            { }
        }
        
        #region "Dhruv 2010 -> ShowEDocument"
        public object oPatientExam { get; set; }        
        public object oPatientLetters { get; set; }
        public object oPatientMessages { get; set; }
        public object oNurseNotes { get; set; }
        public object oHistory { get; set; }
        public object oLabs { get; set; }
        public object oDMS { get; set; }
        public object oRxmed { get; set; }
        public object oOrders { get; set; }
        public object oProblemList { get; set; }
        public object oCriteria { get; set; }
        public object oWord { get; set; }
        public bool IsSplitScreenShown = true;
        public dynamic dMdi { get; set; }
       
       
        public bool ShowEDocument(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int64 ExternalDocumentID,bool _ISChildView=false)
        {
            if (OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {
                using (Forms.frmEDocumentViewer oEDocument = new gloEDocumentV3.Forms.frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID))
                {
                    if (oEDocument != null)
                    {
                    oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);
                    oEDocument.EvnAcknowledgeTask += new gloEDocumentV3.Forms.frmEDocumentViewer.AcknowledgeTask(oEDocument_EvnAckTask);
                 
                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized ;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ScanDocument;
                        ////oEDocument.Text = "Scan Documents";
                        oEDocument.ShowInTaskbar = true;
                        oEDocument.objPatientExam = oPatientExam;

                        oEDocument.objPatientMessages=oPatientMessages;
                        oEDocument.objPatientLetters = oPatientLetters;
                        oEDocument.objNurseNotes= oNurseNotes;
                        oEDocument.objHistory =oHistory ;
                        oEDocument.objLabs=oLabs ;
                        oEDocument.objDMS =oDMS ;
                        oEDocument.objRxmed = oRxmed;
                        oEDocument.objOrders = oOrders;
                        oEDocument.objProblemList = oProblemList;

                        oEDocument.objCriteria = oCriteria;
                        oEDocument.objWord = oWord;
                        oEDocument.dMdi = dMdi;
                        oEDocument.IsSplitScreenShown = IsSplitScreenShown;
                       // oEDocument.ShowDialog();
                        oEDocument.ShowDialog((IWin32Window)oMdiParent);                        
                        oEDocument.EvnRefreshDocuments -= new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);
  

                  //Reason: code change to set the owner for scanner form which resolve the scan focus issue
                   //Bug #51056: 00000444 : Scanning
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            else if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocument)
            {
                Forms.frmEDocumentViewer oEDocument;
                oEDocument = Forms.frmEDocumentViewer.GetInstance(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);

                if (oEDocument != null)
                {
                   
                 

                    oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ViewDocument;
                    ////oEDocument.Text = "View Document";

                    oEDocument.ShowInTaskbar = true;

                    oEDocument.objPatientExam = oPatientExam;

                    oEDocument.objPatientMessages = oPatientMessages;
                    oEDocument.objPatientLetters = oPatientLetters;
                    oEDocument.objNurseNotes = oNurseNotes;
                    oEDocument.objHistory = oHistory;
                    oEDocument.objLabs = oLabs;
                    oEDocument.objDMS = oDMS;
                    oEDocument.objRxmed = oRxmed;
                    oEDocument.objOrders = oOrders;
                    oEDocument.objProblemList = oProblemList;

                    oEDocument.objCriteria = oCriteria;
                    oEDocument.objWord = oWord;
                    oEDocument.dMdi = dMdi;
                    oEDocument.IsSplitScreenShown = IsSplitScreenShown;
                    oEDocument.Focus();
                    oEDocument.Show();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule)
            {
                Forms.frmEDocumentViewer oEDocument;
                oEDocument = Forms.frmEDocumentViewer.GetInstance(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);
                oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);
                oEDocument.EvnAcknowledgeTask += new gloEDocumentV3.Forms.frmEDocumentViewer.AcknowledgeTask(oEDocument_EvnAckTask);
                if (oEDocument != null)
                {
                    oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                    oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ViewDocumentForExternalModule;
                    ////oEDocument.Text = "View Document";
                    oEDocument.ShowInTaskbar = true;

                    oEDocument.objPatientExam = oPatientExam;

                    oEDocument.objPatientMessages = oPatientMessages;
                    oEDocument.objPatientLetters = oPatientLetters;
                    oEDocument.objNurseNotes = oNurseNotes;
                    oEDocument.objHistory = oHistory;
                    oEDocument.objLabs = oLabs;
                    oEDocument.objDMS = oDMS;
                    oEDocument.objRxmed = oRxmed;
                    oEDocument.objOrders = oOrders;
                    oEDocument.objProblemList = oProblemList;
                
                    oEDocument.objCriteria = oCriteria;
                    oEDocument.objWord = oWord;
                    oEDocument.dMdi = dMdi;
                    //if (oEDocument.uiPanSplitScreen != null)
                    //{
                    //    oEDocument.clsSplit.loadSplitControlData(PatientID, 0, oEDocument.uiPanSplitScreen.SelectedPanel.Name, oCriteria, oWord, gloEDocV3Admin.gClinicID);
                    //}
                    oEDocument.IsSplitScreenShown = IsSplitScreenShown;
                    oEDocument.Focus();
                    if (_ISChildView)
                    {
                        oEDocument.MdiParent = oMdiParent;
                        oEDocument.ShowInTaskbar = false;
                    }
                    oEDocument.Show();
                    return false;
                }
                else
                {
                    return false;
                }
            }
            else if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewAllDocuments)
            {
                
                Forms.frmEDocumentViewer oEDocument = new gloEDocumentV3.Forms.frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);
                
                    if (oEDocument != null)
                    {
                    oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);
                  
                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ViewAllDocuments;
                        ////oEDocument.Text = "View Documents";
                        oEDocument.ShowInTaskbar = true;

                        oEDocument.objPatientExam = oPatientExam;

                        oEDocument.objPatientMessages = oPatientMessages;
                        oEDocument.objPatientLetters = oPatientLetters;
                        oEDocument.objNurseNotes = oNurseNotes;
                        oEDocument.objHistory = oHistory;
                        oEDocument.objLabs = oLabs;
                        oEDocument.objDMS = oDMS;
                        oEDocument.objRxmed = oRxmed;
                        oEDocument.objOrders = oOrders;
                        oEDocument.objProblemList = oProblemList;

                        oEDocument.objCriteria = oCriteria;
                        oEDocument.objWord = oWord;
                        oEDocument.dMdi = dMdi;
                        oEDocument.IsSplitScreenShown = IsSplitScreenShown;
                        oEDocument.Focus();
                        if (_ISChildView)
                        {
                            oEDocument.MdiParent = oMdiParent;
                            oEDocument.ShowInTaskbar = false;
                        }
                        oEDocument.Show();
                        return false;
                    }
                    else
                    {
                        return false;
                    }

                  
            }
            return true;
        }

        public Boolean CheckInstance()
        {
            try
            {
                
                Forms.frmEDocumentViewer oEDocument;
                oEDocument = Forms.frmEDocumentViewer.GetAnyInstance();

                if (oEDocument != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }

        }

        public Forms.frmEDocumentViewer GetInstance()
        {
            try
            {

                Forms.frmEDocumentViewer oEDocument;
                oEDocument = Forms.frmEDocumentViewer.GetAnyInstance();
                             
                    return oEDocument;
               
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }

        }


        public bool ShowEDocument_Immunization(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int64 ExternalDocumentID, out Int64 OutputContainderID, out Int64 OutPutDocumentID, out Int64 SelectedDocumentID ,Boolean IsShowCopyExisting =true,Boolean  _IsDeleteEnabledFromImmunization =true)
        {
            OutPutDocumentID = 0;
            OutputContainderID = 0;
            SelectedDocumentID = 0;
            bool _Result = false;
            if (OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {
                using (Forms.frmEDocumentViewer oEDocument = new gloEDocumentV3.Forms.frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, 0))
                {
                    if (oEDocument != null)
                    {
                        oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ScanDocument;
                        ////oEDocument.Text = "Scan Documents";
                        oEDocument.ShowInTaskbar = true;
                        oEDocument.IsShowCopyExisting = IsShowCopyExisting;
                        oEDocument.IsDeleteEnabledFromImmunization = _IsDeleteEnabledFromImmunization;
                        //Added by Mayuri:20121207-Split Screen changes
                        oEDocument.objPatientExam = oPatientExam;

                        oEDocument.objPatientMessages = oPatientMessages;
                        oEDocument.objPatientLetters = oPatientLetters;
                        oEDocument.objNurseNotes = oNurseNotes;
                        oEDocument.objHistory = oHistory;
                        oEDocument.objLabs = oLabs;
                        oEDocument.objDMS = oDMS;
                        oEDocument.objRxmed = oRxmed;
                        oEDocument.objOrders = oOrders;
                        oEDocument.objProblemList = oProblemList;

                        oEDocument.objCriteria = oCriteria;
                        oEDocument.objWord = oWord;
                        oEDocument.ShowDialog(oEDocument.Parent);
                        OutPutDocumentID = oEDocument._DocumentIDForImmnunization;
                        OutputContainderID = oEDocument._ContainerIDForImmnunization;
                        SelectedDocumentID = oEDocument._selectDocumentIDForImmunization;
                        _Result = true;
                        oEDocument.EvnRefreshDocuments -= new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                        
                    }
                   

                }
           
            }
            else
                if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocument)
                {
                    Forms.frmEDocumentViewer oEDocument;
                    oEDocument = Forms.frmEDocumentViewer.GetInstance(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);

                    if (oEDocument != null)
                    {
                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized  ;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ViewDocument;
                        ////oEDocument.Text = "View Document";
                        oEDocument.ShowInTaskbar = true;
                        oEDocument.IsDeleteEnabledFromImmunization = _IsDeleteEnabledFromImmunization;
                        //Added by Mayuri:20121207-Split Screen changes
                        oEDocument.objPatientExam = oPatientExam;

                        oEDocument.objPatientMessages = oPatientMessages;
                        oEDocument.objPatientLetters = oPatientLetters;
                        oEDocument.objNurseNotes = oNurseNotes;
                        oEDocument.objHistory = oHistory;
                        oEDocument.objLabs = oLabs;
                        oEDocument.objDMS = oDMS;
                        oEDocument.objRxmed = oRxmed;
                        oEDocument.objOrders = oOrders;
                        oEDocument.objProblemList = oProblemList;

                        oEDocument.objCriteria = oCriteria;
                        oEDocument.objWord = oWord;
                        oEDocument.Focus();
                        oEDocument.ShowDialog(oEDocument.Parent);
                        OutPutDocumentID = oEDocument._DocumentIDForImmnunization;
                        OutputContainderID = oEDocument._ContainerIDForImmnunization;
                        SelectedDocumentID = oEDocument._selectDocumentIDForImmunization;
                        _Result = true;
                    }
                   
                }
            return _Result;
            
        }
    
        public System.Windows.Forms.Form GetEDocumentWindow(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int64 ExternalDocumentID)
        {
            System.Windows.Forms.Form oViewForm = null;

            try
            {
                if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule)
                {
                    Forms.frmEDocumentViewer oEDocument;
                    oEDocument = Forms.frmEDocumentViewer.GetInstance(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);
                    oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                    if (oEDocument != null)
                    {
                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ViewDocumentForExternalModule;
                        ////oEDocument.Text = "View Document";
                        oEDocument.ShowInTaskbar = false;
                        oEDocument.FormBorderStyle = FormBorderStyle.None;
                        oEDocument.Focus();
                        oEDocument.ShowAsPanel();
                        oViewForm = oEDocument;
                    }
                    else
                    {
                        oViewForm = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return null;
            }

            return oViewForm;

        }

        public System.Windows.Forms.Form GetEDocumentPanelWindow(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int64 ExternalDocumentID)
        {
            System.Windows.Forms.Form oViewForm = null;

            try
            {
                if (OpenEDocumentAs == enum_OpenEDocumentAs.ViewDocumentForExternalModule)
                {
                    Forms.frmEDocumentViewer oEDocument = new gloEDocumentV3.Forms.frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);
                    //(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, ExternalDocumentID);
                    oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                    if (oEDocument != null)
                    {
                        oEDocument.UnloadDocuments();
                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Normal;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ViewDocumentForExternalModule;
                        ////oEDocument.Text = "View Document";
                        oEDocument.ShowInTaskbar = false;
                        oEDocument.FormBorderStyle = FormBorderStyle.None;
                        oEDocument.Focus();
                        oEDocument.ShowAsPanel();
                        oViewForm = oEDocument;
                    }
                    else
                    {
                        oViewForm = null;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return null;
            }

            return oViewForm;

        }

        public bool ShowEDocument_IntuitMessage(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int16 AttachmentCnt, out ArrayList OutputFilepaths,out ArrayList outputDocumentName)
        {
            bool _Result = false;
            OutputFilepaths = null;
            outputDocumentName = null;
            if (OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {
                using (Forms.frmEDocumentViewer oEDocument = new gloEDocumentV3.Forms.frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, 0))
                {
                    if (oEDocument != null)
                    {
                        oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ScanDocument;
                        ////oEDocument.Text = "Scan Documents";
                        oEDocument.ShowInTaskbar = true;
                        //oEDocument.IsShowCopyExisting = IsShowCopyExisting;
                        //oEDocument.IsDeleteEnabledFromImmunization = _IsDeleteEnabledFromImmunization;
                        oEDocument.iAttachmentCnt = AttachmentCnt;

                        //Added for split screen
                        oEDocument.objPatientExam = oPatientExam;
                        oEDocument.objPatientMessages = oPatientMessages;
                        oEDocument.objPatientLetters = oPatientLetters;
                        oEDocument.objNurseNotes = oNurseNotes;
                        oEDocument.objHistory = oHistory;
                        oEDocument.objLabs = oLabs;
                        oEDocument.objDMS = oDMS;
                        oEDocument.objRxmed = oRxmed;
                        oEDocument.objOrders = oOrders;
                        oEDocument.objProblemList = oProblemList;
                        oEDocument.objCriteria = oCriteria;
                        oEDocument.objWord = oWord;

                        oEDocument.ShowDialog(oEDocument.Parent);
                        if (OpenExternalSource == enum_OpenExternalSource.IntuitMessage)
                        {
                        OutputFilepaths = oEDocument.sIntuitDocumentPath;
                        outputDocumentName = oEDocument.sIntuitDocumentName;
                        }
                        else if (OpenExternalSource == enum_OpenExternalSource.SecureMessage)
                        {
                            OutputFilepaths = oEDocument.sSecureMsgDocumentPath;
                            outputDocumentName = oEDocument.sSecureMsgDocumentName;
                        }
                        //OutputContainderID = oEDocument._ContainerIDForImmnunization;
                        //SelectedDocumentID = oEDocument._selectDocumentIDForImmunization;
                        _Result = true;
                        oEDocument.EvnRefreshDocuments -= new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);


                    }


                }

            }
          
            return _Result;

        }

        public bool ShowEDocument_LabOrder(Int64 PatientID, enum_OpenEDocumentAs OpenEDocumentAs, System.Windows.Forms.Form oMdiParent, enum_OpenExternalSource OpenExternalSource, Int16 AttachmentCnt, out ArrayList OutputDMSIDs)
        {
            bool _Result = false;
            OutputDMSIDs = null;
            
            if (OpenEDocumentAs == enum_OpenEDocumentAs.ScanDocument)
            {
                using (Forms.frmEDocumentViewer oEDocument = new gloEDocumentV3.Forms.frmEDocumentViewer(PatientID, OpenEDocumentAs, oMdiParent, OpenExternalSource, 0))
                {
                    if (oEDocument != null)
                    {
                        oEDocument.EvnRefreshDocuments += new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                        oEDocument.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                        oEDocument.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                        oEDocument.OpenEDocumentAs = enum_OpenEDocumentAs.ScanDocument;
                        ////oEDocument.Text = "Scan Documents";
                        oEDocument.ShowInTaskbar = true;
                       
                       // oEDocument.iAttachmentCnt = AttachmentCnt;

                        ////Added for split screen
                        oEDocument.objPatientExam = oPatientExam;
                        oEDocument.objPatientMessages = oPatientMessages;
                        oEDocument.objPatientLetters = oPatientLetters;
                        oEDocument.objNurseNotes = oNurseNotes;
                        oEDocument.objHistory = oHistory;
                        oEDocument.objLabs = oLabs;
                        oEDocument.objDMS = oDMS;
                        oEDocument.objRxmed = oRxmed;
                        oEDocument.objOrders = oOrders;
                        oEDocument.objProblemList = oProblemList;
                        oEDocument.objCriteria = oCriteria;
                        oEDocument.objWord = oWord;
                        //Bug #80137: gloEMR: Scan Docs- Null reference exception on acknowledge
                        oEDocument.dMdi = dMdi;
                        oEDocument.ShowDialog(oEDocument.Parent);
                        if (OpenExternalSource == enum_OpenExternalSource.LabOrder)                        
                        {
                            if (oEDocument.iLabDMSIDs != null)
                            {
                                OutputDMSIDs = oEDocument.iLabDMSIDs;
                                //oEDocument.iLabDMSIDs.Clear();
                                //oEDocument.iLabDMSIDs = null;
                            }
 
                        }
                        //else if (OpenExternalSource == enum_OpenExternalSource.SecureMessage)
                        //{
                        //    OutputFilepaths = oEDocument.sSecureMsgDocumentPath;
                        //    outputDocumentName = oEDocument.sSecureMsgDocumentName;
                        //}
                       
                        _Result = true;
                        oEDocument.EvnRefreshDocuments -= new gloEDocumentV3.Forms.frmEDocumentViewer.RefreshDocuments(oEDocument_EvnRefreshDocuments);

                    }


                }

            }

            return _Result;

        }

        #endregion "Dhruv 2010 -> ShowEDocument"

        #region "Sanjog 1/10/2011 -> ShowDMSFax Added for clinical Charts"
        public bool   ShowDMSFax(Int64 PatientID,gloEDocumentV3.Forms.enmFAXDocType DocumentType, string PDFFilePath)
        {
            bool _Result = false;
            gloEDocumentV3.Forms.frmEDocEvent_Fax  oFax = new gloEDocumentV3.Forms.frmEDocEvent_Fax( DocumentType , PDFFilePath );
            oFax.PatientID = PatientID;
            oFax.StartPosition = FormStartPosition.CenterScreen;
            oFax.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            oFax.dtFaxDetails = this.dtFaxDetails;
            oFax.ShowDialog(oFax.Parent);
            _Result = oFax.oDialogResultIsOK;
            this.dtFaxDetails = oFax.dtFaxDetails;
            this.DocumentFaxed = oFax.DocumentFaxed;
            oFax.Dispose();
            oFax = null;
            return _Result;
        }

        #endregion "Sanjog 1/10/2011 -> ShowEReceivedFax"

    }
}
