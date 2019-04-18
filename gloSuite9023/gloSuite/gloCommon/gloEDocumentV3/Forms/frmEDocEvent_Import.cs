using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using pdftron.PDF;
using pdftron.Common;
using System.Data.SqlClient;
using gloEDocumentV3.Enumeration;

    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocEvent_Import : Form
        {
            public enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None;

            public frmEDocEvent_Import()

            {
                InitializeComponent();
            }

            #region "Page Manuipulation Variables"

            public bool oDialogResultIsOK = false;
            public Int64 oDialogDocumentID = 0;
            public Int64 oDialogContainerID = 0;

            public Int64 oPatientID = 0;
            public string oImportInCategory = "";
            public string oImportInSubCategory = "";
            public Int64 oImportInCategoryID = 0;
            public string oImportInYear = "";
            public string oImportInMonth = "";
            public Int64 oClinicID = 0;

            public enum_DocumentEventType oEventType = enum_DocumentEventType.None;

            public string _ErrorMessage = "";
           
            #endregion
            private string invalidCharString = ":" + "*" + "?" + "<" + ">" + "\\" + "//";

            private Int64 _ImageMaxDPI = 300;

            private void frmEDocEvent_Import_Load(object sender, EventArgs e)
            {
                _ImageMaxDPI = gloEDocV3Admin.gImageMaxDPI;

                lvwDocuments.Items.Clear();
                lvwDocuments.Columns.Clear();

                lvwDocuments.Columns.Add("Document");
                lvwDocuments.Columns.Add("Size");
                lvwDocuments.Columns.Add("Path");

                lvwDocuments.Columns[0].Width = 280;
                lvwDocuments.Columns[1].Width = 100;
                lvwDocuments.Columns[2].Width = 0;

                tlb_Remove.Enabled = false;
                chkIsPDF.Checked = false;

                txtDocumentName.Text = eDocManager.eDocValidator.GetNewDocumentName(oPatientID, oImportInCategory, oClinicID, _OpenExternalSource);

                pbDocument.Minimum = 0;
                pbDocument.Maximum = 100;
                pbDocument.Value = 0;
            }

            private void tlb_Cancel_Click(object sender, EventArgs e)
            {
                oDialogResultIsOK = false;
                this.Close();
            }
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

            private void tlb_Ok_Click(object sender, EventArgs e)
            {
                string _TempImportDirectory = gloEDocumentV3.gloEDocV3Admin.gTemporaryProcessPath + "\\" + oPatientID + "-" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff");// +System.Guid.NewGuid().ToString();DateTime.Now.ToString("MMddyyyyHHmmssffff") + System.Guid.NewGuid().ToString();
                eDocManager.eDocManager oDocManager = new gloEDocumentV3.eDocManager.eDocManager();
                bool IsInvalid = false;
                try
                {
                    tlb_Ok.Enabled = false;
                    tlb_Cancel.Enabled = false;
                    tlb_AddImages.Enabled = false;
                    tlb_AddPDF.Enabled = false;
                    tlb_Remove.Enabled = false;

                    Application.DoEvents();

                    if (System.IO.Directory.Exists(_TempImportDirectory) == true)
                    {
                        System.IO.Directory.Delete(_TempImportDirectory, true);

                    }
                    System.IO.Directory.CreateDirectory(_TempImportDirectory);
                    if (System.IO.Directory.Exists(_TempImportDirectory) == false)
                    {
                        _ErrorMessage = "Unable to create a directory at a specified path. " + _TempImportDirectory;
                        ErrorMessagees(_ErrorMessage);
                    }


                    Application.DoEvents();

                    if (string.IsNullOrEmpty(oImportInSubCategory))
                    {
                        oImportInSubCategory = DateTime.Now.ToString("MM dd yyyy");
                    }

                    ArrayList oSourceDocuments = new ArrayList();
                    
                    char[] chars = invalidCharString.ToCharArray();
                   
                    for (int i = 0; i < chars.Length - 1; i++)
                    {
                        string strCharacter = chars[i].ToString();
                        bool _result = txtDocumentName.Text.Contains(strCharacter);
                        if (_result == true)
                        {
                            MessageBox.Show("Document name is not in valid format.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    for (int i = 0; i <= lvwDocuments.Items.Count - 1; i++)
                    {
                        oSourceDocuments.Add(lvwDocuments.Items[i].SubItems[2].Text);
                    }

                    if (oSourceDocuments.Count <= 0)
                    {
                        MessageBox.Show("Select documents to Import.", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsInvalid = true;
                        return;
                    }

                    if (txtDocumentName.Text.Trim() == "")
                    {
                        MessageBox.Show("Enter document name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        IsInvalid = true;
                        return;
                    }

                    if (CheckFileNameExists(txtDocumentName.Text,oImportInCategoryID, oImportInCategory, oPatientID, oImportInYear, oImportInMonth,_OpenExternalSource) > 0)
                    {
                        MessageBox.Show("Document with the same name already exists, Enter different name", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        IsInvalid = true;
                        return;
                    }
                    this.Cursor = Cursors.WaitCursor;
                    pbDocument.Minimum = 0;
                    pbDocument.Maximum = 100 * oSourceDocuments.Count;
                    if (chkIsPDF.Checked)
                    {
                        if (chkSplitFile.Checked)
                        {
                            Application.DoEvents();
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - START" + " " + DateTime.Now.TimeOfDay);
                            oDialogResultIsOK = oDocManager.ImportSplit(oPatientID, oSourceDocuments, txtDocumentName.Text, oImportInCategoryID, oImportInCategory,oImportInSubCategory,  oImportInYear, oImportInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked,pbDocument, _OpenExternalSource);
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - FINISHED" + " " + DateTime.Now.TimeOfDay);
                            Application.DoEvents();
                        }
                        else
                        {
                            Application.DoEvents();
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - START");
                            oDialogResultIsOK = oDocManager.Import(oPatientID, oSourceDocuments, txtDocumentName.Text, oImportInCategoryID, oImportInCategory, oImportInYear, oImportInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked, pbDocument, _OpenExternalSource);
                            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for PDF - FINISHED");
                            Application.DoEvents();
                        }
                    }
                    else
                    {
                        Application.DoEvents();
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for Image - START");
                        oDialogResultIsOK = oDocManager.ImportImages(oPatientID, oSourceDocuments, txtDocumentName.Text.Trim(), oImportInCategoryID, oImportInCategory, oImportInSubCategory, oImportInYear, oImportInMonth, oClinicID, out oDialogContainerID, out oDialogDocumentID, chkUseCompression.Checked, pbDocument, _OpenExternalSource);
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("oDocManager.Import for Image - FINISHED");
                        Application.DoEvents();
                    }

                    if (oDialogDocumentID > 0)
                    {
                        oDialogResultIsOK = true;
                    }

                    if (oDialogResultIsOK == true)
                    {
                        this.Close();
                    }
                    else
                    {
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Import function Aborted or Failed - FINISHED");
                        this.Close();
                    }
                    //pbDocument.Value = 100;
                    pbDocument.Value = pbDocument.Maximum;
                    this.Cursor = Cursors.Default;

                    if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "RCM document(s) imported.", oPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                    else
                    {
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.Import, gloAuditTrail.ActivityType.Add, "Scan document(s) imported.", oPatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success);
                    }
                }
                catch (Exception ex)
                {


                    _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);

                    MessageBox.Show(ex.Message, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (oDocManager != null)
                    {
                        oDocManager.Dispose();
                        oDocManager = null;
                    }
                    if (IsInvalid == true)
                    {
                        tlb_Ok.Enabled = true;
                        tlb_Cancel.Enabled = true;
                        tlb_AddImages.Enabled = true;
                        tlb_AddPDF.Enabled = true;
                        tlb_Remove.Enabled = false;
                    }
                    else
                    {
                        tlb_Ok.Enabled = true;
                        tlb_Cancel.Enabled = true;
                        tlb_AddImages.Enabled = true;
                        tlb_AddPDF.Enabled = true;
                        tlb_Remove.Enabled = true;
                    }
                    //Code added on 2008025 By - Sagar Ghodke
                    //code added to delete the temp import folder & files
                    //
                    if (_TempImportDirectory != null && System.IO.Directory.Exists(_TempImportDirectory))
                    {
                        System.IO.Directory.Delete(_TempImportDirectory, true);
                    }
                    //
                    //End Changes 2008025 By - Sagar Ghodke
                }
            }


            #region "Dhruv 2010 -> CheckFileNameExists "

            public Int32 CheckFileNameExists(string FileName, Int64 CategoryId, string CategoryName, long PatientID, string Year, string Month, enum_OpenExternalSource _OpenExternalSource = enum_OpenExternalSource.None)
            {
                Object _retValue = new object();
                Int32 _DocValue = 0;
                string _sqlQuery = "";

                try
                {
                    using (gloEDocumentV3.Database.DBLayer oDB = new gloEDocumentV3.Database.DBLayer(gloEDocV3Admin.gDMSDatabaseConnectionString))
                    {
                        if (oDB != null)
                        {
                            if (oDB.Connect(false))
                            {
                                if (_OpenExternalSource == enum_OpenExternalSource.RCM)
                                {
                                    _sqlQuery = " SELECT COUNT(*) FROM eDocument_Details_V3_RCM WITH(NOLOCK) " +
                                            " WHERE UPPER(DocumentName) = '" + FileName.Trim().ToUpper() + "' " +
                                            " AND CategoryID = " + CategoryId + " " +
                                            " AND UPPER(Category) = '" + CategoryName.ToUpper() + "' " +
                                            " AND PatientID = " + PatientID + " " +
                                            " AND [Year] = '" + Year.ToUpper() + "' " +
                                            " AND [Month] = '" + Month.ToUpper() + "' ";
                                }
                                else
                                {
                                    _sqlQuery = " SELECT COUNT(*) FROM eDocument_Details_V3 WITH(NOLOCK) " +
                                            " WHERE UPPER(DocumentName) = '" + FileName.Trim().ToUpper() + "' " +
                                            " AND CategoryID = " + CategoryId + " " +
                                            " AND UPPER(Category) = '" + CategoryName.ToUpper() + "' " +
                                            " AND PatientID = " + PatientID + " " +
                                            " AND [Year] = '" + Year.ToUpper() + "' " +
                                            " AND [Month] = '" + Month.ToUpper() + "' ";
                                }
                                

                                _retValue = oDB.ExecuteScalar_Query(_sqlQuery);
                                if (_retValue != null && _retValue != DBNull.Value && System.Convert.ToInt32(_retValue) > 0)
                                {
                                    _DocValue = System.Convert.ToInt32(_retValue);
                                }
                                if (oDB != null)
                                {
                                    oDB.Disconnect();
                                }
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                }
                finally
                {

                    if (_retValue != null)
                    {
                        _retValue = null;
                    }
                }

                return _DocValue;
            }

            #endregion "Dhruv 2010 -> CheckFileNameExists "

            void oDocManager_DocumentProgressEvent(int Percentage, string Message)
            {
                Application.DoEvents();
                if (Percentage <= pbDocument.Maximum) { pbDocument.Value = Percentage; }
            }
            #region "Dhruv 2010 -> InsertTextBox (Commented)"


            #region "Add/Remove Documents"

            private void tlb_AddPDF_Click(object sender, EventArgs e)
            {
                if (lvwDocuments.Items.Count > 0)
                {
                    if (chkIsPDF.Checked == false)
                    {
                        if (MessageBox.Show("Are you sure you want to change Import document type?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            lvwDocuments.Items.Clear();
                            chkIsPDF.Checked = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                OpenFileDialog oDialog = new OpenFileDialog();
                oDialog.Multiselect = true;
                oDialog.Title = "Import PDF Document";
                oDialog.Filter = "PDF Document(*.pdf)|*.pdf";
                string _DuplicateFiles = "";

                if (oDialog.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string CurFile in oDialog.FileNames)
                    {
                        System.IO.FileInfo oFile = new System.IO.FileInfo(CurFile);

                        bool _AddThisFile = true;
                        if (lvwDocuments != null)
                        {
                            for (int i = 0; i <= lvwDocuments.Items.Count - 1; i++)
                            {
                                string strFilename = oFile.FullName.ToUpper();
                                if (lvwDocuments.Items[i].SubItems[2].Text.ToUpper() == strFilename)
                                {
                                    _AddThisFile = false;

                                    if (_DuplicateFiles.Trim() == "")
                                    {
                                        _DuplicateFiles = strFilename;
                                    }
                                    else
                                    {
                                        _DuplicateFiles = _DuplicateFiles + Environment.NewLine + strFilename;
                                    }
                                    break;
                                }

                            }
                        }

                        if (_AddThisFile == true)
                        {
                            if (IsValidExtenstionFile(oFile.Extension, true) == true)
                            {
                                ListViewItem oItem = new ListViewItem();
                                if (oItem != null)
                                {
                                    oItem.Text = oFile.Name;
                                    oItem.SubItems.Add(oFile.Length.ToString());
                                    oItem.SubItems.Add(oFile.FullName);
                                    lvwDocuments.Items.Add(oItem);
                                    if (oItem != null)
                                    {
                                        oItem = null;
                                    }
                                }
                            }
                        }
                        oFile = null;

                    }
                    chkIsPDF.Checked = true;

                    if (_DuplicateFiles.Trim() != "")
                    {
                        MessageBox.Show("Following files are Duplicate..." + Environment.NewLine + _DuplicateFiles, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                oDialog.Dispose();
                oDialog = null;
                if (lvwDocuments.Items.Count > 0)
                {
                    tlb_Remove.Enabled = true;
                }
                else
                {
                    tlb_Remove.Enabled = false;
                }
            }

            private void tlb_AddImages_Click(object sender, EventArgs e)
            {
                if (lvwDocuments.Items.Count > 0)
                {
                    if (chkIsPDF.Checked == true)
                    {
                        if (MessageBox.Show("Are you sure you want to change Import document type?", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            lvwDocuments.Items.Clear();
                            chkIsPDF.Checked = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                }

                OpenFileDialog oDialog = new OpenFileDialog();
                oDialog.Multiselect = true;
                oDialog.Title = "Import Images";
                oDialog.Filter = "JPEG Image (*.JPEG,*.JPG)|*.JPEG;*.JPG|TIFF Image (*.TIFF,*.TIF)|*.TIFF;*.TIF|BITMAP Image (*.BMP)|*.BMP|PNG Image (*.PNG)|*.PNG";
                string _DuplicateFiles = "";

                if (oDialog.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string CurFile in oDialog.FileNames)
                    {
                        System.IO.FileInfo oFile = new System.IO.FileInfo(CurFile);
                        if (oFile != null)
                        {
                            if (IsValidExtenstionFile(oFile.Extension, false) == true)
                            {
                                if (IsValidDPIFile(oFile.FullName) == true)
                                {
                                    bool _AddThisFile = true;
                                    for (int i = 0; i <= lvwDocuments.Items.Count - 1; i++)
                                    {
                                        string strFilename = oFile.FullName.ToUpper();
                                        if (lvwDocuments.Items[i].SubItems[2].Text.ToUpper() == strFilename)
                                        {
                                            _AddThisFile = false;
                                            if (_DuplicateFiles.Trim() == "")
                                            {
                                                _DuplicateFiles = strFilename;
                                            }
                                            else
                                            {
                                                _DuplicateFiles = _DuplicateFiles + Environment.NewLine + strFilename;
                                            }
                                            break;
                                        }
                                    }
                                    if (_AddThisFile == true)
                                    {
                                        ListViewItem oItem = new ListViewItem();
                                        if (oItem != null)
                                        {
                                            oItem.Text = oFile.Name;
                                            oItem.SubItems.Add(oFile.Length.ToString());
                                            oItem.SubItems.Add(oFile.FullName);
                                            lvwDocuments.Items.Add(oItem);
                                            oItem = null;
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Only " + _ImageMaxDPI.ToString() + " DPI and less image file is allowed into DMS.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        if (oFile != null)
                        {
                            oFile = null;
                        }

                    }
                    chkIsPDF.Checked = false;

                    if (_DuplicateFiles.Trim() != "")
                    {
                        MessageBox.Show("Following files are duplicate..." + Environment.NewLine + _DuplicateFiles, "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                oDialog.Dispose();
                oDialog = null;
                if (lvwDocuments.Items.Count > 0)
                {
                    tlb_Remove.Enabled = true;
                }
                else
                {
                    tlb_Remove.Enabled = false;
                }
            }

            private void tlb_Remove_Click(object sender, EventArgs e)
            {
                if (lvwDocuments.SelectedItems.Count > 0)
                {
                    lvwDocuments.SelectedItems[0].Remove();
                }
                else
                {
                    MessageBox.Show("Select document(s)", gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (lvwDocuments.Items.Count <= 0)
                {
                    tlb_Remove.Enabled = false;
                }
            }

            #endregion

            #endregion "Dhruv 2010 -> InsertTextBox (Commented)"




            private bool IsValidExtenstionFile(string Extenstion, bool IsPDFDocument)
            {
                bool _result = false;
                string _intstrExtenstion = Extenstion.ToUpper();

                if (IsPDFDocument == true)
                {
                    if (_intstrExtenstion == ".PDF")
                    {
                        _result = true;
                    }
                }
                else
                {
                    if (_intstrExtenstion == ".JPEG" || _intstrExtenstion == ".JPG" || _intstrExtenstion == ".TIFF" || _intstrExtenstion == ".TIF" || _intstrExtenstion == ".BMP" || _intstrExtenstion == ".PNG")
                    {
                        _result = true;
                    }
                }
                return _result;
            }

            #region "Dhruv 2010 -> IsValidDPIFile "

            //private bool IsValidDPIFile_Old(string ImageFilePath)
            //{
            //    bool _result = false;
            //    try
            //    {
            //        //--
            //        if (System.IO.File.Exists(ImageFilePath) == true)
            //        {
            //            System.Drawing.Bitmap bmp = null;
            //            try
            //            {
            //                bmp = new System.Drawing.Bitmap(ImageFilePath);
            //                float _resHor = 0;
            //                float _resVer = 0;

            //                _resHor = bmp.HorizontalResolution;
            //                _resVer = bmp.VerticalResolution;
            //                if (_resHor <= _ImageMaxDPI && _resVer <= _ImageMaxDPI)
            //                {
            //                    _result = true;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                ex.ToString();
            //            }
            //            finally
            //            {
            //                if (bmp != null)
            //                    bmp.Dispose();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ex.ToString();
            //        _result = false;
            //    }
            //    finally
            //    {
            //    }
            //    return _result;
            //}
            private bool IsValidDPIFile(string ImageFilePath)
            {
                bool _result = false;
                try
                {

                    if (System.IO.File.Exists(ImageFilePath) == true)
                    {
                        System.Drawing.Bitmap bmp = null;
                        try
                        {
                            bmp = new System.Drawing.Bitmap(ImageFilePath);
                            if (bmp == null)
                            {
                                _ErrorMessage = "Bmp value is null. ";
                                ErrorMessagees(_ErrorMessage);
                                return _result;

                            }

                            float _resHor = 0;
                            float _resVer = 0;

                            _resHor = bmp.HorizontalResolution;
                            _resVer = bmp.VerticalResolution;
                            if (_resHor <= _ImageMaxDPI && _resVer <= _ImageMaxDPI)
                            {
                                _result = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            _ErrorMessage = ex.ToString();
                            ErrorMessagees(_ErrorMessage);
                            _result = false;
                        }
                        finally
                        {
                            if (bmp != null)
                            {
                                bmp.Dispose();
                                bmp = null;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMessage = ex.ToString();
                    ErrorMessagees(_ErrorMessage);
                    _result = false;
                }

                return _result;
            }

            #endregion "Dhruv 2010 -> IsValidDPIFile "


            #region "Dhruv 2010 -> CreateImageDocument"

            //private string CreateImageDocument_Old(string OutputDocumentFileName, ArrayList ImagesList)
            //{
            //    PDFDoc doc = new PDFDoc();
            //    pdftron.PDF.Image img = null;
            //    ElementBuilder oElementBuilder = new ElementBuilder();	// Used to build new Element objects
            //    ElementWriter oElementWriter = new ElementWriter();	// Used to write Elements to the page	
            //    string _returnResult = "";
            //    Element element = null;
            //    System.Drawing.Bitmap bmp = null;
            //    Rect oRect = null;
            //    Page page = null;
            //    string _tempProcessPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath + "\\" + OutputDocumentFileName.ToString().Trim() + "-" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".pdf";
            //    //string _outputFilePath = "D:\\Documents and Settings\\sagar.ghodke\\Desktop\\TestFiles\\Output\\" + OutputDocumentFileName + ".pdf";

            //    try
            //    {

            //        for (int i = 0; i < ImagesList.Count; i++)
            //        {
            //            try
            //            {
            //                bmp = new System.Drawing.Bitmap(ImagesList[i].ToString());
            //            }
            //            catch (Exception ex)
            //            {
            //                ex.ToString();
            //                continue;
            //            }

            //            int imgCount = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            //            //img = pdftron.PDF.Image.Create(doc, ImagesList[i].ToString());
            //            //float hr = bmp.HorizontalResolution;
            //            //float vr = bmp.VerticalResolution;
            //            //if (hr > 150 || vr > 150)
            //            //{
            //            //    bmp.SetResolution(150, 150);
            //            //}
            //            //else
            //            //{
            //            //    bmp.SetResolution(hr, vr);
            //            //}

            //            ////Create Rectangle object of Image Size
            //            //oRect = new Rect();
            //            //oRect.x1 = bmp.Width;
            //            //oRect.x2 = 0;
            //            //oRect.y1 = bmp.Height;
            //            //oRect.y2 = 0;

            //            oRect = new Rect();

            //            float _hr = bmp.HorizontalResolution;
            //            float _vr = bmp.VerticalResolution;
            //            float _wd = bmp.Width;
            //            float _ht = bmp.Height;
            //            //bmp.Dispose();

            //            int XWidth = 0;
            //            int YHeight = 0;
            //            bool XYRet = false;

            //            XYRet = GetXWidthYHeight(_hr, _vr, _ht, _wd, out XWidth, out YHeight);

            //            oRect = new Rect();
            //            oRect.x1 = XWidth;// img.GetBitmap().Width;
            //            oRect.x2 = 0;
            //            oRect.y1 = YHeight;// img.GetBitmap().Height;
            //            oRect.y2 = 0;


            //            for (int j = 0; j < imgCount; j++)
            //            {
            //                if (j >= 1)
            //                {
            //                    // Select the current TIFF page using SelectActiveFrame
            //                    bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, j);

            //                }

            //                //Set Page Size as of Rect
            //                page = doc.PageCreate(oRect);
            //                oElementWriter.Begin(page);
            //                img = pdftron.PDF.Image.Create(doc, bmp);
            //                element = oElementBuilder.CreateImage(img, new Matrix2D(XWidth, 0, 0, YHeight, 0, 0));// );//new Matrix2D(612, 0, 0, 794, 0, 0) //
            //                oElementWriter.WritePlacedElement(element);
            //                oElementWriter.End();
            //                doc.PagePushBack(page);

            //            }



            //            if (oRect != null)
            //                oRect = null;

            //            if (page != null)
            //                page = null;

            //            if (bmp != null)
            //                bmp.Dispose();

            //        }


            //        if (System.IO.File.Exists(gloEDocV3Admin.gPDFTronTemporaryProcessPath) == false)
            //        {
            //            System.IO.Directory.CreateDirectory(gloEDocV3Admin.gPDFTronTemporaryProcessPath);
            //        }

            //        doc.Save(_tempProcessPath, pdftron.SDF.SDFDoc.SaveOptions.e_linearized);
            //        doc.Close();
            //        _returnResult = _tempProcessPath;

            //    }
            //    catch (PDFNetException pdfEx)
            //    {
            //        #region " Make Log Entry "

            //        _ErrorMessage = pdfEx.ToString();
            //        //Code added on 7rd October 2008 By - Sagar Ghodke
            //        //Make Log entry in DMSExceptionLog file for any exceptions found
            //        if (_ErrorMessage.Trim() != "")
            //        {
            //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //            _MessageString = "";
            //        }

            //        //End Code add
            //        #endregion " Make Log Entry "
            //    }
            //    catch (Exception ex)
            //    {

            //        #region " Make Log Entry "

            //        _ErrorMessage = ex.ToString();
            //        //Code added on 7rd October 2008 By - Sagar Ghodke
            //        //Make Log entry in DMSExceptionLog file for any exceptions found
            //        if (_ErrorMessage.Trim() != "")
            //        {
            //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //            gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //            _MessageString = "";
            //        }

            //        //End Code add
            //        #endregion " Make Log Entry "

            //        MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        _returnResult = "";
            //    }
            //    finally
            //    {
            //        if (doc != null)
            //            doc.Dispose();

            //        if (oElementBuilder != null)
            //            oElementBuilder.Dispose();

            //        if (oElementBuilder != null)
            //            oElementWriter.Dispose();

            //        if (oRect != null)
            //            oRect = null;

            //        if (page != null)
            //            page = null;

            //    }
            //    return _returnResult;

            //}
            #region "Not in Used :: CreateImageDocument"
            //private string CreateImageDocument(string OutputDocumentFileName, ArrayList ImagesList)
            //{
            //    PDFDoc doc = new PDFDoc();
            //    pdftron.PDF.Image img = null;
            //    ElementBuilder oElementBuilder = new ElementBuilder();	// Used to build new Element objects
            //    ElementWriter oElementWriter = new ElementWriter();	// Used to write Elements to the page	
            //    string _returnResult = "";
            //    Element element = null;
            //    System.Drawing.Bitmap bmp = null;
            //    Rect oRect = null;
            //    Page page = null;
            //    string _tempProcessPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath + "\\" + OutputDocumentFileName.ToString().Trim() + "-" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".pdf";
            //    //string _outputFilePath = "D:\\Documents and Settings\\sagar.ghodke\\Desktop\\TestFiles\\Output\\" + OutputDocumentFileName + ".pdf";

            //    try
            //    {

            //        for (int i = 0; i < ImagesList.Count; i++)
            //        {
            //            try
            //            {
            //                bmp = new System.Drawing.Bitmap(ImagesList[i].ToString());
            //            }
            //            catch (Exception ex)
            //            {
            //                _ErrorMessage = ex.ToString();
            //                ErrorMessagees(_ErrorMessage);
            //                return string.Empty;
            //                //continue;
            //            }

            //            int imgCount = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);
            //            //img = pdftron.PDF.Image.Create(doc, ImagesList[i].ToString());
            //            //float hr = bmp.HorizontalResolution;
            //            //float vr = bmp.VerticalResolution;
            //            //if (hr > 150 || vr > 150)
            //            //{
            //            //    bmp.SetResolution(150, 150);
            //            //}
            //            //else
            //            //{
            //            //    bmp.SetResolution(hr, vr);
            //            //}

            //            ////Create Rectangle object of Image Size
            //            //oRect = new Rect();
            //            //oRect.x1 = bmp.Width;
            //            //oRect.x2 = 0;
            //            //oRect.y1 = bmp.Height;
            //            //oRect.y2 = 0;

            //            //oRect = new Rect();

            //            float _hr = bmp.HorizontalResolution;
            //            float _vr = bmp.VerticalResolution;
            //            float _wd = bmp.Width;
            //            float _ht = bmp.Height;
            //            //bmp.Dispose();
            //            if (imgCount <= 1)
            //            {
            //                bmp.Dispose();
            //                bmp = null;
            //            }

            //            int XWidth = 0;
            //            int YHeight = 0;
            //            bool XYRet = false;

            //            XYRet = GetXWidthYHeight(_hr, _vr, _ht, _wd, out XWidth, out YHeight);

            //            oRect = new Rect();
            //            oRect.x1 = XWidth;// img.GetBitmap().Width;
            //            oRect.x2 = 0;
            //            oRect.y1 = YHeight;// img.GetBitmap().Height;
            //            oRect.y2 = 0;


            //            for (int j = 0; j < imgCount; j++)
            //            {
            //                if ((j >= 1) && (bmp != null))
            //                {
            //                    // Select the current TIFF page using SelectActiveFrame
            //                    bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, j);

            //                }

            //                //Set Page Size as of Rect
            //                page = doc.PageCreate(oRect);
            //                oElementWriter.Begin(page);

            //                if (imgCount <= 1)
            //                {
            //                    img = pdftron.PDF.Image.Create(doc, bmp);
            //                }
            //                else
            //                {
            //                    try
            //                    {
            //                        img = pdftron.PDF.Image.Create(doc, ImagesList[i].ToString());
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                        _ErrorMessage = "Error is due to the img object is null";
            //                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                        _MessageString = "";
            //                        _returnResult = "";
            //                        System.Windows.Forms.MessageBox.Show("Error in Img", ex.ToString());
            //                        break;
            //                    }
            //                }
            //                if (img == null)
            //                {
            //                    _ErrorMessage = "Error is due to the img object is null";
            //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                    _MessageString = "";
            //                    _returnResult = "";
            //                    break;
            //                }
            //                element = oElementBuilder.CreateImage(img, new Matrix2D(XWidth, 0, 0, YHeight, 0, 0));// );//new Matrix2D(612, 0, 0, 794, 0, 0) //
            //                if (element == null)
            //                {
            //                    _ErrorMessage = "Error is due to the element object is null";
            //                    string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "Module : " + "ConvertImagesToPDF" + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //                    _MessageString = "";
            //                    _returnResult = "";
            //                    break;
            //                }

            //                oElementWriter.WritePlacedElement(element);
            //                oElementWriter.End();
            //                doc.PagePushBack(page);

            //            }


            //            if (oRect != null)
            //                oRect = null;

            //            if (page != null)
            //                page = null;

            //            if (bmp != null)
            //            {
            //                bmp.Dispose();
            //                bmp = null;
            //            }


            //        }


            //        if (System.IO.File.Exists(gloEDocV3Admin.gPDFTronTemporaryProcessPath) == false)
            //        {
            //            System.IO.Directory.CreateDirectory(gloEDocV3Admin.gPDFTronTemporaryProcessPath);
            //            if (System.IO.File.Exists(gloEDocV3Admin.gPDFTronTemporaryProcessPath) == false)
            //            {
            //                _ErrorMessage = "Unable to create directory. " + gloEDocV3Admin.gPDFTronTemporaryProcessPath;
            //                ErrorMessagees(_ErrorMessage);
            //            }
            //        }

            //        doc.Save(_tempProcessPath, pdftron.SDF.SDFDoc.SaveOptions.e_linearized);
            //        doc.Close();
            //        _returnResult = _tempProcessPath;

            //    }
            //    catch (PDFNetException pdfEx)
            //    {
            //        _ErrorMessage = pdfEx.ToString();
            //        ErrorMessagees(_ErrorMessage);
            //    }
            //    catch (Exception ex)
            //    {

            //        _ErrorMessage = ex.ToString();
            //        ErrorMessagees(_ErrorMessage);
            //        MessageBox.Show("ERROR : " + ex.ToString(), gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        _returnResult = "";
            //    }
            //    finally
            //    {
            //        if (doc != null)
            //        {
            //            doc.Dispose();
            //            doc = null;
            //        }

            //        if (oElementBuilder != null)
            //        {
            //            oElementBuilder.Dispose();
            //            oElementBuilder = null;
            //        }

            //        if (oElementBuilder != null)
            //        {
            //            oElementWriter.Dispose();
            //            oElementWriter = null;
            //        }

            //        if (oRect != null)
            //        {

            //            oRect = null;
            //        }

            //        if (page != null)
            //        {

            //            page = null;
            //        }

            //    }
            //    return _returnResult;

            //}
            #endregion "Not in Used :: CreateImageDocument"
            #endregion "Dhruv 2010 -> CreateImageDocument "


            private bool GetXWidthYHeight(float HorRes, float VerRes, float pxHeight, float pxWidth, out int XWidth, out int YHeight)
            {
                bool _result = false;
                float _XWidth = 0;
                float _YHeight = 0;
                try
                {
                    float _ResDactor = 0;
                    bool _XMajor = false;
                   // string _Orientation = "S"; // P-Potrait, L-Landscape, S-Same
                    float _XInch = 0;
                    float _YInch = 0;

                    //Factor + Major X or Y
                    if (HorRes > VerRes) { _ResDactor = HorRes / VerRes; _XMajor = true; }
                    else if (HorRes < VerRes) { _ResDactor = VerRes / HorRes; _XMajor = false; }
                    else if (HorRes == VerRes) { _ResDactor = 1; }

                    //Height & Width in Inches
                    _XInch = pxHeight / HorRes;
                    _YInch = pxWidth / VerRes;

                    ////Page Orientation
                    //if (_XInch > _YInch) { _Orientation = "L"; }
                    //else if (_XInch < _YInch) { _Orientation = "P"; }

                    //Calculate Return Height and Width in Pixcel
                    if (_XMajor == true)
                    {
                        _XWidth = pxWidth;
                        _YHeight = pxHeight * _ResDactor;
                    }
                    else
                    {
                        _XWidth = pxWidth * _ResDactor;
                        _YHeight = pxHeight;
                    }

                    _result = true;
                }
                catch (Exception ex)
                {
                    #region " Make Log Entry "

                    _ErrorMessage = ex.ToString();
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
                finally
                {
                }

                XWidth = System.Convert.ToInt32(_XWidth);
                YHeight = System.Convert.ToInt32(_YHeight);
                return _result;
            }



            //private string CreateImageDocument(string OutputDocumentFileName, ArrayList ImagesList)
            //{
            //    PDFDoc doc = new PDFDoc();
            //    pdftron.PDF.Image img = null;
            //    ElementBuilder oElementBuilder = new ElementBuilder();	// Used to build new Element objects
            //    ElementWriter oElementWriter = new ElementWriter();	// Used to write Elements to the page	
            //    string _returnResult = "";
            //    Element element = null;
            //    System.Drawing.Bitmap bmp = null;
            //    Rect oRect = null;
            //    Page page = null;
            //    string _tempProcessPath = gloEDocumentAdmin.gPDFTronTemporaryProcessPath + "\\" + OutputDocumentFileName.ToString().Trim() + "-" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".pdf";
            //    //string _outputFilePath = "D:\\Documents and Settings\\sagar.ghodke\\Desktop\\TestFiles\\Output\\" + OutputDocumentFileName + ".pdf";

            //    try
            //    {

            //        for (int i = 0; i < ImagesList.Count; i++)
            //        {
            //            try
            //            {
            //                bmp = new System.Drawing.Bitmap(ImagesList[i].ToString());
            //            }
            //            catch (Exception ex)
            //            {
            //                continue;
            //            }

            //            int imgCount = bmp.GetFrameCount(System.Drawing.Imaging.FrameDimension.Page);

            //            float hr = bmp.HorizontalResolution;
            //            float vr = bmp.VerticalResolution;
            //            if (hr > 150 || vr > 150)
            //            {
            //                bmp.SetResolution(150, 150);
            //            }
            //            else
            //            {
            //                bmp.SetResolution(hr, vr);
            //            }

            //            //Create Rectangle object of Image Size
            //            oRect = new Rect();
            //            oRect.x1 = bmp.Width;
            //            oRect.x2 = 0;
            //            oRect.y1 = bmp.Height;
            //            oRect.y2 = 0;


            //            for (int j = 0; j < imgCount; j++)
            //            {
            //                if (j >= 1)
            //                {
            //                    // Select the current TIFF page using SelectActiveFrame
            //                    bmp.SelectActiveFrame(System.Drawing.Imaging.FrameDimension.Page, j);

            //                }

            //                //Set Page Size as of Rect
            //                page = doc.PageCreate(oRect);
            //                oElementWriter.Begin(page);
            //                img = pdftron.PDF.Image.Create(doc, bmp);
            //                element = oElementBuilder.CreateImage(img, new Matrix2D(bmp.Width, 0, 0, bmp.Height, 0, 0));// );//new Matrix2D(612, 0, 0, 794, 0, 0) //
            //                oElementWriter.WritePlacedElement(element);
            //                oElementWriter.End();
            //                doc.PagePushBack(page);

            //            }



            //            if (oRect != null)
            //                oRect = null;

            //            if (page != null)
            //                page = null;

            //            if (bmp != null)
            //                bmp.Dispose();

            //        }


            //        if (System.IO.File.Exists(gloEDocumentAdmin.gPDFTronTemporaryProcessPath) == false)
            //        {
            //            System.IO.Directory.CreateDirectory(gloEDocumentAdmin.gPDFTronTemporaryProcessPath);
            //        }

            //        doc.Save(_tempProcessPath, pdftron.SDF.SDFDoc.SaveOptions.e_linearized);
            //        doc.Close();
            //        _returnResult = _tempProcessPath;

            //    }
            //    catch (PDFNetException pdfEx)
            //    {
            //        #region " Make Log Entry "

            //        _ErrorMessage = pdfEx.ToString();
            //        //Code added on 7rd October 2008 By - Sagar Ghodke
            //        //Make Log entry in DMSExceptionLog file for any exceptions found
            //        if (_ErrorMessage.Trim() != "")
            //        {
            //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //            gloEDocument.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //            _MessageString = "";
            //        }

            //        //End Code add
            //        #endregion " Make Log Entry "
            //    }
            //    catch (Exception ex)
            //    {

            //        #region " Make Log Entry "

            //        _ErrorMessage = ex.ToString();
            //        //Code added on 7rd October 2008 By - Sagar Ghodke
            //        //Make Log entry in DMSExceptionLog file for any exceptions found
            //        if (_ErrorMessage.Trim() != "")
            //        {
            //            string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
            //            gloEDocument.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
            //            _MessageString = "";
            //        }

            //        //End Code add
            //        #endregion " Make Log Entry "

            //        MessageBox.Show("ERROR : " + ex.ToString(), gloEDocumentAdmin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        _returnResult = "";
            //    }
            //    finally
            //    {
            //        if (doc != null)
            //            doc.Dispose();

            //        if (oElementBuilder != null)
            //            oElementBuilder.Dispose();

            //        if (oElementBuilder != null)
            //            oElementWriter.Dispose();

            //        if (oRect != null)
            //            oRect = null;

            //        if (page != null)
            //            page = null;

            //    }
            //    return _returnResult;

            //}


            private void txtDocumentName_KeyUp(object sender, KeyEventArgs e)
            {
                try
                {
                    string sFileName = txtDocumentName.Text.Trim();
                    string sValidFileName = "";
                 
                    //Changed by Shweta 20100108
                    //against the bug id:5646
                    //it doesnt allow /,\,:,?,<,>,* character in file name
                    //sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                    sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace("(", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                    //End 20100108
                    if (sFileName != sValidFileName)
                    {
                        txtDocumentName.Text = sValidFileName;
                        txtDocumentName.Select(txtDocumentName.Text.Length, 1);
                    }
                }
                catch (Exception ex)
                {
                    string _ex = ex.Message;
                    ErrorMessagees(_ex);
                }
            }

            private void txtDocumentName_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    string sFileName = txtDocumentName.Text.Trim();
                    string sValidFileName = "";
                    //Changed by Shweta 20100108
                    //against the bug id:5646
                    //it doesnt allow /,\,:,?,<,>,* character in file name
                    //sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace(")", "").Replace("(", "").Replace(".", "").Replace(":", "").Replace(";", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                    sValidFileName = sFileName.Replace("'", "").Replace("/", "").Replace("\\", "").Replace("(", "").Replace(":", "").Replace("<", "").Replace(">", "").Replace("?", "").Replace("*", "").Replace("\"", "");
                    //End 20100108

                    if (sFileName != sValidFileName)
                    {
                        txtDocumentName.Text = sValidFileName;
                        txtDocumentName.Select(txtDocumentName.Text.Length, 1);
                    }
                }
                catch (Exception ex)
                {
                    string _ex = ex.Message;
                    ErrorMessagees(_ex);
                }
            }

            private void panel1_Paint(object sender, PaintEventArgs e)
            {

            }//end - private bool CreateImageDocument(string OutputDocumentFileName,ArrayList oSourceDocument)
        }
    }
