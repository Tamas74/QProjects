using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using pdftron.PDF;
using gloEDocumentV3.DocumentContextMenu;
using gloEDocumentV3.Enumeration;

    namespace gloEDocumentV3.Forms
    {
        partial class frmEDocumentArchiveViewer : Form
        {

            private string documentsystemtype = string.Empty;
            public string DocumentTypeSystem
            { get { return documentsystemtype; } }

            private string patientid = string.Empty;
            public string PatientID
            { get { return patientid; } }

            private string qsuitedbconnectionstring = string.Empty;
            public string QSuiteDatabaseConnectionString
            {
                get { return qsuitedbconnectionstring; }

            }

            private string loginuserid = string.Empty;
            public string LoginUserId
            {
                get { return loginuserid; }

            }

            private string ausid = string.Empty;
            public string AUSID
            {
                get { return ausid; }

            }

            private int _PatientStripMAXHeight;
            public int PatientStripMAXHeight
            {
                get { return _PatientStripMAXHeight; }
                set { _PatientStripMAXHeight = value; }
            }

            private int _PatientStripMINHeight;
            public int PatientStripMINHeight
            {
                get { return _PatientStripMINHeight; }
                set { _PatientStripMINHeight = value; }
            }

            private Int64 currentpatientid = 0;
            
            private enum_OpenExternalSource opendocsource = enum_OpenExternalSource.None;

            private string documentarchiveUri = string.Empty;
            private static System.Drawing.Printing.PrinterSettings myPrinterSetting = null;

            #region " Constructor "

            public frmEDocumentArchiveViewer(Int64 patientid, enum_OpenExternalSource source)
            {
                try
                {
                    InitializeComponent();
                    
                    opendocsource = source;

                    if (source == enum_OpenExternalSource.RCM)
                    { 
                        currentpatientid = 0; 
                        this.patientid = "0";
                        this.documentsystemtype = "RCM";
                    }
                    else
                    {
                        this.documentsystemtype = "DMS";
                        if (patientid > 0)
                        { currentpatientid = patientid; this.patientid = System.Convert.ToString(patientid); }
                        else
                        { throw new ArgumentException("Invalid patientid parameter"); }
                    }

                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                    {
                        if (myPrinterSetting == null)
                        {
                            myPrinterSetting = new System.Drawing.Printing.PrinterSettings();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            } 

            #endregion
            
            private void frmEDocEvent_Print_Load(object sender, EventArgs e)
            {
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    if (opendocsource == enum_OpenExternalSource.RCM)
                    { pnlPatients.Visible = false; }
                    else { LoadPatientStrip(); }
                    

                    this.loginuserid = System.Convert.ToString(gloGlobal.gloPMGlobal.UserID);
                    
                    this.ausid = System.Convert.ToString(gloGlobal.gloPMGlobal.AusID);
                    if (string.IsNullOrEmpty(this.ausid))
                    { this.ausid = System.Windows.Forms.SystemInformation.ComputerName; }

                    this.qsuitedbconnectionstring = System.Convert.ToString(gloEDocV3Admin.gDatabaseConnectionString);

                    SetBrowserControl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally { this.Cursor = Cursors.Default; }
            }

            private void tlb_Cancel_Click(object sender, EventArgs e)
            {
                this.Close();
            }

            private void tlb_Print_Click(object sender, EventArgs e)
            {
                string surl = string.Empty;
                string _FileFullPath = string.Empty;
                try
                {
                    
                    var objIFrameTag = webBrowserArchiveDocument.Document.GetElementById("pdfFrame");
                    if (objIFrameTag == null)
                    {
                        var objObjectTag = webBrowserArchiveDocument.Document.GetElementById("pdfObject");
                        if (objObjectTag != null && objObjectTag.Children != null)
                        {
                            foreach (System.Windows.Forms.HtmlElement item in objObjectTag.Children)
                            {
                                if (item.InnerHtml == "here")
                                {
                                    var sHtml = item.OuterHtml.ToString();
                                    string[] sinnerHtml = sHtml.Substring(sHtml.IndexOf("<")).Split(' ');
                                    string sTagName = sinnerHtml[0].Replace("<", "");
                                    if (sTagName == "a")
                                    {
                                        string[] text = sHtml.Substring(sHtml.IndexOf("href")).Split('=');
                                        string str = text[1].Remove(1, 1);
                                        surl = documentarchiveUri + str.Replace("\"", "");
                                        surl = surl.Replace("#toolbar", "");
                                    }
                                }
                            }
                        }
                        else
                        { MessageBox.Show("Select document to print", gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                    else
                    {
                        var ifrm = webBrowserArchiveDocument.Document.Window.Frames[0].WindowFrameElement.OuterHtml;

                        string[] text = ifrm.Substring(ifrm.IndexOf("src")).Split('=');
                        string str = text[1].Remove(1, 1);

                        surl = documentarchiveUri + str.Replace("\"", "");
                        surl = surl.Replace("#toolbar", "");
                    }

                    if (surl != "")
                    {
                        using (var client = new System.Net.WebClient())
                        {
                            _FileFullPath = gloEDocV3Admin.gPDFTronTemporaryProcessPath.ToString() + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + ".pdf";

                            if (_FileFullPath != null)
                            {
                                client.DownloadFile(surl, _FileFullPath);
                                Print_Click(_FileFullPath);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    tlb_Print.Enabled = true;
                    tlb_Cancel.Enabled = true;
                }
            }

            void oDocManager_DocumentProgressEvent(int Percentage, string Message)
            {
               
            }

            private void rbCustomDPI_CheckedChanged(object sender, EventArgs e)
            {
               
            }

            //#CR380 Added Code While printing from scan doc DPI Setting not respecting
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

            private void SetBrowserControl()
            {
                try
                {
                    
                    gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(gloEDocV3Admin.gDatabaseConnectionString);
                    object outputObject = null;

                    oSettings.GetSetting("DocumentArchiveUri", out outputObject);
                    documentarchiveUri = System.Convert.ToString(outputObject);

                    if (string.IsNullOrEmpty(documentarchiveUri) == false && Uri.IsWellFormedUriString(documentarchiveUri, UriKind.Absolute) == true)
                    {
                        var uriBuilder = new UriBuilder(documentarchiveUri);
                        var values = System.Web.HttpUtility.ParseQueryString(string.Empty);

                        gloSecurity.ClsEncryption oEncrypt = new gloSecurity.ClsEncryption();
                        string passkey = "g!0$tream";
                        values["DocType"] = oEncrypt.EncryptToBase64String(this.DocumentTypeSystem, passkey);
                        values["patientID"] = oEncrypt.EncryptToBase64String(this.PatientID, passkey);
                        values["connectionstring"] = oEncrypt.EncryptToBase64String(this.QSuiteDatabaseConnectionString, passkey);
                        values["UserID"] = oEncrypt.EncryptToBase64String(this.LoginUserId, passkey);
                        values["AUSID"] = oEncrypt.EncryptToBase64String(this.AUSID, passkey);

                        uriBuilder.Query = values.ToString();

                        if (Uri.IsWellFormedUriString(uriBuilder.Uri.ToString(), UriKind.Absolute) == true)
                        {
                            webBrowserArchiveDocument.Url = new Uri(uriBuilder.Uri.OriginalString);
                            //Console.WriteLine("URL: " + uriBuilder.Uri.OriginalString);
                        }
                        else
                        { MessageBox.Show("Invalid archive document Uri", gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }

                    }
                    else
                    { MessageBox.Show("Empty or invalid archive document Uri", gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error); }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            #region "Patient Strip"

            gloUserControlLibrary.gloUC_PatientStrip oPatientStrip = null;
            private void LoadPatientStrip()
            {
                //for (int i = panel19.Controls.Count - 1; i >= 0; i--)
                //{
                //    if (panel19.Controls[i].GetType() == typeof(gloUserControlLibrary.gloUC_PatientStrip))
                //    {
                //        panel19.Controls.RemoveAt(i);
                //    }
                //}
                if (oPatientStrip != null)
                {
                    if (panel19.Controls.Contains(oPatientStrip))
                    {
                        panel19.Controls.Remove(oPatientStrip);
                    }
                    try
                    {
                        oPatientStrip.ControlSizeChanged -= new gloUserControlLibrary.gloUC_PatientStrip.ControlSizeChangedEventHandler(oPatientStrip_ControlSizeChanged);
                    }
                    catch
                    {
                    }
                    oPatientStrip.Dispose();
                    oPatientStrip = null;
                }
                oPatientStrip = new gloUserControlLibrary.gloUC_PatientStrip();

                oPatientStrip.Visible = true;
                oPatientStrip.Dock = DockStyle.Top;
                oPatientStrip.Padding = new Padding(0);
                oPatientStrip.ShowDetail(this.currentpatientid, gloUserControlLibrary.gloUC_PatientStrip.enumFormName.ScanDocuments, 0, 0, 0, false, false, false, "", false);
                panel19.Controls.Add(oPatientStrip);
                //lblPatients.Text = oPatientStrip.PatientCode + " - " + oPatientStrip.PatientName + " - " + oPatientStrip.PatientDateOfBirth.ToString("MM/dd/yyyy");
                _PatientStripMAXHeight = oPatientStrip.Height + 28;
                _PatientStripMINHeight = 28;
                
                oPatientStrip.ControlSizeChanged += new gloUserControlLibrary.gloUC_PatientStrip.ControlSizeChangedEventHandler(oPatientStrip_ControlSizeChanged);
                panel19.BringToFront();

            }

            void oPatientStrip_ControlSizeChanged()
            {
                pnlPatients.Height = oPatientStrip.Height;
            }
            #endregion

            private void frmEDocumentArchiveViewer_Shown(object sender, EventArgs e)
            {
                Console.WriteLine(String.Format("WebBrowser: Height - {0}, Width - {1}, pnlPatients: Height - {2}, Width - {3}", webBrowserArchiveDocument.Height.ToString(), webBrowserArchiveDocument.Width.ToString(), pnlPatients.Height.ToString(), pnlPatients.Width.ToString()));
            }

            private void webBrowserArchiveDocument_Navigated(object sender, WebBrowserNavigatedEventArgs e)
            {
                this.Cursor = Cursors.Default;
            }

            private void webBrowserArchiveDocument_Navigating(object sender, WebBrowserNavigatingEventArgs e)
            {
                this.Cursor = Cursors.WaitCursor;
            }

            private void webBrowserArchiveDocument_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
            {
                this.Cursor = Cursors.Default;
            }

            private void frmEDocumentArchiveViewer_MouseDown(object sender, MouseEventArgs e)
            {
                try
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        SendKeys.Send("{Esc}");
                    }
                }
                catch
                { }
            }

            private void Print_Click(string printfilepath)
            {
                try
                {
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Print  - Start" + " " + DateTime.Now.TimeOfDay);
                    Application.DoEvents();
                    tlb_Print.Enabled = false;
                    tlb_Cancel.Enabled = false;
                    Application.DoEvents();


                    gloPrintDialog.gloPrintProgressController ogloPrintProgressController = null;

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

                    if (printfilepath != null && printfilepath.Trim().Length > 0)
                    {
                        if (System.IO.File.Exists(printfilepath) == true)
                        {
                            using (gloPrintDialog.gloPrintDialog oDialog = new gloPrintDialog.gloPrintDialog())
                            {
                                oDialog.TopMost = true;
                                oDialog.ShowPrinterProfileDialog = true;
                                oDialog.ConnectionString = gloEDocumentV3.gloEDocV3Admin.gDatabaseConnectionString;

                                if (DocumentTypeSystem == "RCM")
                                {
                                    oDialog.ModuleName = "PrintRCMDocuments";
                                    oDialog.RegistryModuleName = "RCMDocuments";
                                }
                                else if (DocumentTypeSystem == "DMS")
                                {
                                    oDialog.ModuleName = "PrintDMSDocuments";
                                    oDialog.RegistryModuleName = "DMSDocuments";
                                }

                                if (oDialog != null)
                                {
                                    //doc.Lock();
                                    //int maxPage = doc.GetPageCount();

                                    if (!gloGlobal.gloTSPrint.isCopyPrint)
                                    {
                                        oDialog.PrinterSettings = myPrinterSetting; //printDocument1.PrinterSettings;

                                        oDialog.AllowSomePages = true;

                                        //oDialog.PrinterSettings.ToPage = maxPage;
                                        oDialog.PrinterSettings.FromPage = 1;
                                        //oDialog.PrinterSettings.MaximumPage = maxPage;
                                        oDialog.PrinterSettings.MinimumPage = 1;

                                    }
                                    //26-May-16 Aniket: Resolving Bug #96494: gloEMR : RCM Docs (Background printing) : application loses focus as user click on cancel button
                                    if (oDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                    {
                                        if (!gloGlobal.gloTSPrint.isCopyPrint)
                                        {
                                            myPrinterSetting = oDialog.PrinterSettings;
                                        }

                                        ogloPrintProgressController = new gloPrintDialog.gloPrintProgressController(
                                            printfilepath, myPrinterSetting, oDialog.CustomPrinterExtendedSettings);
                                        ogloPrintProgressController.bIsFromScanDoc = true;
                                        ogloPrintProgressController.ShowProgress(this);

                                        {
                                            if (DocumentTypeSystem == "RCM")
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RCMDMS, gloAuditTrail.ActivityCategory.PrintDocument, gloAuditTrail.ActivityType.Print, "Archived RCM document(s) printed.", System.Convert.ToInt64(PatientID), 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                            }
                                            else if (DocumentTypeSystem == "DMS")
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DMS, gloAuditTrail.ActivityCategory.PrintDocument, gloAuditTrail.ActivityType.Print, "Archived Document(s) printed.", System.Convert.ToInt64(PatientID), 0, 0, gloAuditTrail.ActivityOutCome.Success);
                                            }
                                        }

                                    }//if

                                    //doc.Unlock();
                                    //   PrintDialog1.Dispose();
                                    //   PrintDialog1 = null;
                                }
                                else
                                {
                                    string _ErrorMessage = "Archive: Error in Showing Print Dialog";

                                    if (_ErrorMessage.Trim() != "")
                                    {
                                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                                        _MessageString = "";
                                    }


                                    MessageBox.Show(_ErrorMessage, gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }

                            }//using
                        }
                    }
                }
                catch (Exception ex)
                {
                    #region " Make Log Entry "

                    string _ErrorMessage = ex.ToString();
                    
                    if (_ErrorMessage.Trim() != "")
                    {
                        string _MessageString = "Date Time : " + DateTime.Now.ToString() + Environment.NewLine + "ERROR : " + _ErrorMessage;
                        gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog(_MessageString);
                        _MessageString = "";
                    }

                    #endregion " Make Log Entry "

                    MessageBox.Show(ex.Message, gloEDocumentV3.gloEDocV3Admin.gMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    tlb_Print.Enabled = true;
                    tlb_Cancel.Enabled = true;
                    gloEDocumentV3.eDocManager.eDocValidator.UpdateExceptionLog("Print  - End" + " " + DateTime.Now.TimeOfDay);
                }
            }//
           
        }
    }
