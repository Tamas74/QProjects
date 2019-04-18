using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Runtime.InteropServices;
using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.IO;
using System.Net;
using System.Xml;
using System.Collections;
using C1.Win.C1FlexGrid;
using System.Configuration;
using Microsoft.VisualBasic;
using gloWord;

namespace gloCommunity.UserControls
{
    public partial class UCTemplates : UserControl
    {
        gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
        private Wd.Document oCurDoc;
        private Wd.Application oWordApp;
        //isSelect_Clear_All variable declared to avoid recursive call on tree node check change 
        //it will set to true on select or clear all function and set to false in finally block of respective function
     //   bool isSelect_Clear_All = false;
        private string straction = "";
        public int COL_SELECT = 0;
        public int COL_TEMPLATEID = 1;
        public int COL_TEMPLATENAME = 2;
        public int COL_CATEGORY = 3;
        public int COL_PROVIDER = 4;
        public int COL_HiddenTempNm = 5;
        string strSitefolder = "";
        int COL_CNT = 3;
        int COL_CHECK = 0;
        int COL_TemplateName = 1;
        int COL_FileName = 2;
        DataTable dtcontit = null;
        public bool blntempgclinic = true;
        public UCTemplates()
        {
            InitializeComponent();

        }

        public UCTemplates(string action)
        {
            InitializeComponent();
            straction = action;

        }

        private void UCTemplates_Load(object sender, EventArgs e)
        {
            //btnlocal.Text = clsGeneral.ClinicRepository;
            //   btncentral.Text = clsGeneral.GlobalRepository;

            objgloC1FlexStyle.Style(flxTemplates);
            try
            {
                if (straction == "Upload")
                {
                    cmbCategory.Visible = false;
                    cmbProvider.Visible = true;
                    FillProviders();
                    DesignGrid();
                    Fill_Categories();
                }
                else
                {
                    cmbCategory.Visible = true;
                    cmbProvider.Visible = false;
                    // pnltemplatecat.Visible = false;
                    //pnlbtnlocal.Dock = DockStyle.Top;
                    lbl_pnlProviderName.Text = "Category Name";
                    SetGridStyle();

                    clsTemplate objclsTemplate = new clsTemplate();
                    try
                    {
                        this.Cursor = Cursors.WaitCursor;
                        dtcontit = objclsTemplate.GetContentTitle();
                        tlbClinicRepository_Click(null, null);
                    }
                    catch (Exception ex)
                    {
                        //clsGeneral.UpdateLog("Error  while getting data for Template     in Template UserControl : " + ex.Message.ToString());  
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }
                    finally
                    {
                        this.Cursor = Cursors.Default;
                        objclsTemplate = null;
                    }

                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.Export, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show("Unable to load the template form due to " + ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FillProviders()
        {
            DataTable dtProviders = null;
            clsTemplate objTemplate = new clsTemplate();
            dtProviders = objTemplate.GetAllProvider();
            objTemplate = null;
            if ((dtProviders != null && dtProviders.Rows.Count > 0))
            {
                //' Here we add "All"(indicating All Doctors / Providers) 
                //' To datatable dt which contains Provider Name & ID's 

                DataRow objrow = null;
                objrow = dtProviders.NewRow();
                objrow[0] = 0;
                objrow[1] = "All";
                dtProviders.Rows.Add(objrow);

                //' Attach DataSource to  CmbProvider 
                cmbProvider.DataSource = dtProviders;
                cmbProvider.DisplayMember = dtProviders.Columns[1].ColumnName;
                //Provider Name
                cmbProvider.ValueMember = dtProviders.Columns[0].ColumnName;
                //Provider ID
                cmbProvider.SelectedValue = 0;


                //'Select login provider
                // cmbProvider.SelectedValue =gnLoginProviderID;

            }
        }

        private void DesignGrid()
        {
            var _with1 = flxTemplates;
            _with1.Cols.Count = 6;
            _with1.Rows.Count = 1;
            _with1.Rows.Fixed = 1;
            _with1.SetData(0, COL_SELECT, "Select");
            _with1.SetData(0, COL_TEMPLATEID, "Template ID");
            _with1.SetData(0, COL_TEMPLATENAME, "Template Name");
            _with1.SetData(0, COL_CATEGORY, "Category");
            _with1.SetData(0, COL_PROVIDER, "Provider");
            _with1.SetData(0, COL_HiddenTempNm, "HiddenTempNm");

            _with1.Cols[COL_SELECT].Width = 75;
            _with1.Cols[COL_TEMPLATEID].Width = 0;
            _with1.Cols[COL_TEMPLATENAME].Width = 200;
            _with1.Cols[COL_CATEGORY].Width = 0;
            _with1.Cols[COL_PROVIDER].Width = 0;
            _with1.Cols[COL_HiddenTempNm].Width = 0;

            _with1.Cols[COL_SELECT].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            _with1.Cols[COL_TEMPLATEID].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            _with1.Cols[COL_TEMPLATENAME].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            _with1.Cols[COL_CATEGORY].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            _with1.Cols[COL_PROVIDER].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            _with1.Cols[COL_HiddenTempNm].TextAlignFixed = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;

            _with1.Cols[COL_SELECT].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.CenterCenter;
            _with1.Cols[COL_TEMPLATEID].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            _with1.Cols[COL_TEMPLATENAME].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            _with1.Cols[COL_CATEGORY].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            _with1.Cols[COL_PROVIDER].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
            _with1.Cols[COL_HiddenTempNm].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.LeftCenter;
        }

        private void Fill_Categories()
        {
            var _with1 = trvCategories;
            _with1.BeginUpdate();
            _with1.Nodes.Clear();
            TreeNode trvNde = default(TreeNode);
            DataTable dtTemplates = new DataTable();
            clsTemplate objTemplate = new clsTemplate();
            dtTemplates = objTemplate.GetAllCategory("Template");
            objTemplate = null;
            Int16 nCount = default(Int16);
            for (nCount = 0; nCount <= dtTemplates.Rows.Count - 1; nCount++)
            {
                trvNde = new TreeNode();
                var _with2 = trvNde;
                _with2.Tag = dtTemplates.Rows[nCount][0];
                _with2.Text = dtTemplates.Rows[nCount][1].ToString();
                _with2.ImageIndex = 3;
                _with2.SelectedImageIndex = 3;
                _with1.Nodes.Add(trvNde);
            }
            _with1.ExpandAll();
            _with1.EndUpdate();
        }

        private void Fill_Templates(long CategoryId)
        {
            Int32 nSelectedCategoriesCount = 0;
            DataSet dsAllTemplates = new DataSet();
            Int16 nCount = default(Int16);
            clsTemplate objTemplate = new clsTemplate();

            dsAllTemplates.Merge(objTemplate.GetAllTemplates(CategoryId, Convert.ToInt64(cmbProvider.SelectedValue)), true);
            objTemplate = null;
            var _with1 = flxTemplates;
            _with1.Rows.Count = 1;

            for (nCount = 0; nCount <= dsAllTemplates.Tables[0].Rows.Count - 1; nCount++)
            {
                _with1.Rows.Add();
                _with1.SetData(_with1.Rows.Count - 1, COL_SELECT, false);
                _with1.SetData(_with1.Rows.Count - 1, COL_TEMPLATEID, dsAllTemplates.Tables[0].Rows[nCount][0]);
                _with1.SetData(_with1.Rows.Count - 1, COL_TEMPLATENAME, dsAllTemplates.Tables[0].Rows[nCount][1]);
                _with1.SetData(_with1.Rows.Count - 1, COL_CATEGORY, dsAllTemplates.Tables[0].Rows[nCount][2]);
                _with1.SetData(_with1.Rows.Count - 1, COL_PROVIDER, dsAllTemplates.Tables[0].Rows[nCount][3]);
            }
            if (dsAllTemplates.Tables[0].Rows.Count > 0)
            {
                //ts_btnDocSelectAll.Visible = true;
                //ts_btnDocClearAll.Visible = false;
            }

            if (nSelectedCategoriesCount >= 1)
            {
                //lblTemplatesSummary.Text = "  Total Templates=" + dsAllTemplates.Tables[0].Rows.Count;
            }
            else
            {
                //lblTemplatesSummary.Text = "  Total Templates=0";
            }
            for (int i = 0; i < _with1.Rows.Count; i++)
            {
                _with1.SetCellCheck(i, _with1.Cols[COL_SELECT].Index, CheckEnum.Unchecked);
            }
        }

        private void trvCategories_AfterCheck(object sender, TreeViewEventArgs e)
        {

        }

        private void wdopenfl_OnDocumentClosed(object sender, EventArgs e)
        {
            try
            {
                //UpdateVoiceLog(" wdTemplate_OnDocumentClosed - Start")
                //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "wdTemplate_OnDocumentClosed - Start", gloAuditTrail.ActivityOutCome.Success);

                if ((oCurDoc != null))
                {
                    Marshal.ReleaseComObject(oCurDoc);
                    oCurDoc = null;
                }
          //      if ((oWordApp != null))
          //      {
          ////          Marshal.FinalReleaseComObject(oWordApp);
          //          oWordApp = null;
          //      }
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //GC.Collect();
                //GC.WaitForPendingFinalizers();
                //UpdateVoiceLog(" wdTemplate_OnDocumentClosed - End")
                //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "wdTemplate_OnDocumentClosed - End", gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Word Document Closed event     in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //UpdateVoiceLog(ex.ToString)
            }
        }

        private void wdopenfl_OnDocumentOpened(object sender, AxDSOFramer._DFramerCtlEvents_OnDocumentOpenedEvent e)
        {
            try
            {
                //UpdateVoiceLog(" wdTemplate_OnDocumentOpened Started ")
                //gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Open, "wdTemplate_OnDocumentOpened Started", gloAuditTrail.ActivityOutCome.Success);
                oCurDoc = (Wd.Document)wdopenfl.ActiveDocument;
                oCurDoc.Application.ActiveDocument.Protect(Wd.WdProtectionType.wdAllowOnlyReading);
                oWordApp = oCurDoc.Application;
                //if (!(garrOpenDocument.Contains(oCurDoc.FullName)))
                //{
                //    garrOpenDocument.Add(oCurDoc.FullName);
                //}
                ////oWordApp.WindowSelectionChange += DDLCBEvent;
                ////oWordApp.WindowBeforeDoubleClick += OnFormClicked;
                ////oWordApp.WindowBeforeRightClick += BeforeRightClick;
                oCurDoc.ActiveWindow.SetFocus();
                oCurDoc.Application.Options.ShowDevTools = false;
                //UpdateVoiceLog(" wdTemplate_OnDocumentOpened END ")
                // gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Open, "wdTemplate_OnDocumentOpened END", gloAuditTrail.ActivityOutCome.Success);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Open, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //clsGeneral.UpdateLog("Error  while Word Document Opened event     in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //UpdateVoiceLog(ex.ToString)
            }
        }

        private void flxTemplates_EnterCell(object sender, EventArgs e)
        {

        }

        public ArrayList Downloadcontent = new ArrayList();

        public void SeeDownLoadFile(string strURL, string strFileName)
        {

            HttpWebRequest request = default(HttpWebRequest);

            HttpWebResponse response = null;

            Downloadcontent.Clear();

            try
            {
                string strName = gloSettings.FolderSettings.AppTempFolderPath + trvCategories.SelectedNode.Text + strFileName;
                if (File.Exists(strName) == false)
                {
                    //If Arrtemplate.Contains(strFileName) Then
                    request = (HttpWebRequest)WebRequest.Create(strURL);
                    //request.Credentials = System.Net.CredentialCache.DefaultCredentials;

                    if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                        request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    else
                    {
                        //Added for check which authentication is use for access gloCommunity on 20120801
                        if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                        {
                            request.CookieContainer = new CookieContainer();
                            if (clsGeneral.oFormCookie == null)
                                request.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                            else
                                request.CookieContainer.Add(clsGeneral.oFormCookie);
                        }
                        else
                        {
                            clsGeneral.CheckAuthenticatedCookie();
                            request.CookieContainer = clsGeneral.oCookie;
                        }
                    }

                    request.Timeout = 10000;
                    request.AllowWriteStreamBuffering = false;
                    response = (HttpWebResponse)request.GetResponse();

                    Stream s = response.GetResponseStream();

                    byte[] read = new byte[501];


                    int count = 1;
                    int len = 0;
                    while ((count != 0))
                    {
                        count = s.Read(read, len, 500);
                        len += count;
                        Array.Resize(ref read, len + 500);
                    }
                    Array.Resize(ref read, len);

                    strName = clsGeneral.GenerateFile(read, strName);
                    s.Close();
                    response.Close();

                }



           //     wdopenfl.Open(strName);
                object thisObject = (object)strName;
               // Wd.Application oWordApp = null;
                gloWord.LoadAndCloseWord.OpenDSO(ref wdopenfl, ref thisObject, ref oCurDoc, ref oWordApp);
                strName = (string)thisObject;

                oCurDoc = null;

                oCurDoc = (Wd.Document)wdopenfl.ActiveDocument;




              //  Wd.ContentControl _cntcontrol = null;

                foreach (Wd.ContentControl _cntcontrols in oCurDoc.ContentControls)
                {
                    if ((_cntcontrols.Title != null))
                    {
                        DataRow[] dr = dtcontit.Select("Title='" + _cntcontrols.Title.ToString().Trim() + "' ");
                        if (dr.Length > 0)
                        {
                            _cntcontrols.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorBrightGreen;
                        }
                        else
                        {
                            _cntcontrols.Range.Font.Color = Microsoft.Office.Interop.Word.WdColor.wdColorDarkRed;
                            Downloadcontent.Add(_cntcontrols);
                        }

                    }
                }


            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Opening Word Document File     in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                // MessageBox.Show(ex.Message)

            }
            finally
            {
            }
        }

        private void LoadWordControl(string strFileName)
        {
            //' TRY BY SUDHIR 20090708 ''
            try
            {

             //   wdopenfl.Open(strFileName);
                object thisObject = (object)strFileName;
                //Wd.Application oWordApp = null;
              String strError=  gloWord.LoadAndCloseWord.OpenDSO(ref wdopenfl, ref thisObject, ref oCurDoc, ref oWordApp);
                strFileName = (string)thisObject;
                if (strError != String.Empty)
                {
                    MessageBox.Show("Template cannot be open because there are problems with the contents.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wdopenfl.CreateNew("Word.Document");
                }

            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.WINWORD, gloAuditTrail.ActivityType.Modify, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                MessageBox.Show("Template cannot be open because there are problems with the contents.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wdopenfl.CreateNew("Word.Document");
            }
        }

        private void wdopenfl_BeforeDocumentClosed(object sender, AxDSOFramer._DFramerCtlEvents_BeforeDocumentClosedEvent e)
        {
            try
            {
                if ((oWordApp != null))
                {
                    //CODE ADDED BY DIPAK 20091224 ''' TO HANDLE EMR-PM WORD INSTANCE ''
                    ////garrOpenDocument.Remove(oCurDoc.FullName);
                    //////END CODE ADDED BY DIPAK
                    ////oWordApp.WindowSelectionChange -= DDLCBEvent;
                    ////oWordApp.WindowBeforeDoubleClick -= OnFormClicked;
                    ////oWordApp.WindowBeforeRightClick -= BeforeRightClick;
                    //UpdateVoiceLog("RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp")
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "RemoveHandler for WindowSelectionChange,WindowBeforeDoubleClick  for oWordApp", gloAuditTrail.ActivityOutCome.Success);
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
                                    catch (Exception ex)
                                    {
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                        //clsGeneral.UpdateLog("Error  while Word Document Before Closed event    in Template UserControl : " + ex.Message.ToString());  
                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                        //UpdateVoiceLog(ex.ToString)

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                //clsGeneral.UpdateLog("Error  while Word Document Before Closed event    in Template UserControl : " + ex.Message.ToString());  
                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                //UpdateVoiceLog(ex.ToString)

                            }
                        }
                    }

                    //UpdateVoiceLog("Remove from word recent File list")
                    gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, "Remove from word recent File list", gloAuditTrail.ActivityOutCome.Success);
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.Template, gloAuditTrail.ActivityType.Close, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //clsGeneral.UpdateLog("Error  while Word Document Before Closed event    in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //UpdateVoiceLog(ex.ToString)
            }
        }

        private void trvCategories_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (straction == "Upload")
                {
                    if (e.Node != null)
                    {
                        Fill_Templates(Convert.ToInt64(e.Node.Tag));
                    }
                }
                else
                {
                    List<string> myLIst = null;
                    if (Convert.ToString(e.Node.Tag) == "GlobalFolders")
                    {
                        if (trvCategories.SelectedNode.Nodes.Count > 0)
                        {
                            int _Result = Convert.ToInt32(MessageBox.Show("Templates are already loaded. Do you want to fetch templates again?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                            if (_Result == Convert.ToInt32(DialogResult.Yes))
                            {
                                trvCategories.SelectedNode.Nodes.Clear();
                                GetListOfFilesInSPFolder(clsGeneral.Webpath, clsGeneral.GlobalRepository + "/" + e.Node.Text, e.Node);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                            GetListOfFilesInSPFolder(clsGeneral.Webpath, clsGeneral.GlobalRepository + "/" + e.Node.Text, e.Node);
                    }
                    else if (Convert.ToString(e.Node.Tag) == "Templates")
                    {
                        if (trvCategories.SelectedNode.Nodes.Count > 0)
                        {
                            int _Result = Convert.ToInt32(MessageBox.Show("Templates are already loaded. Do you want to fetch templates again?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                            if (_Result == Convert.ToInt32(DialogResult.Yes))
                            {
                                trvCategories.SelectedNode.Nodes.Clear();
                                GetListOfFilesInSPSubFolder(clsGeneral.Webpath, e.Node.Name, e.Node, "", 1);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else
                            GetListOfFilesInSPSubFolder(clsGeneral.Webpath, e.Node.Name, e.Node, "", 1);
                    }
                    else if (Convert.ToString(e.Node.Tag) == "Categories")
                    {
                        if (blntempgclinic == true)
                            myLIst = GetListOfFilesInSPSubFolder(clsGeneral.Webpath + clsGeneral.gstrClinicName + "/", e.Node.Name, e.Node, "User", 1);
                        else
                            myLIst = GetListOfFilesInSPSubFolder(clsGeneral.Webpath, e.Node.Name, e.Node, "", 1);
                    }

                    FillChild(myLIst);
                    if (cmbCategory.Items.IndexOf(e.Node.Text) > -1)
                        cmbCategory.SelectedIndex = cmbCategory.Items.IndexOf(e.Node.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to fill the templates due to " + ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally { this.Cursor = Cursors.Default; }
        }

        private void SetGridStyle()
        {
         //   string struser = null;
         //   Int16 i = default(Int16);
            flxTemplates.Cols.Count = COL_CNT;
            //column count
            flxTemplates.Rows.Fixed = 1;
            flxTemplates.Rows.Count = 1;
            //.AllowEditing = False
            flxTemplates.AllowAddNew = false;

            //flxTemplates.Stylhes.ClearUnused();
          //  bool bl = false;

            //flxTemplates.AllowEditing = false;
            flxTemplates.Cols[COL_CHECK].AllowEditing = true;
            flxTemplates.Cols[COL_CHECK].Caption = "Select";
            flxTemplates.Cols[COL_TemplateName].Caption = "Template Name";
            flxTemplates.Cols[COL_FileName].Caption = "File Name";
            flxTemplates.Cols[COL_FileName].Visible = false;
        }

        private void FillChild(List<string> myLIst)
        {
            try
            {
                //  flxTemplates.Clear();
                if (flxTemplates.Rows.Count >= 2)
                {
                    flxTemplates.Rows.RemoveRange(1, flxTemplates.Rows.Count - 1);
                }

                if (myLIst != null)
                {
                    String[] myArray = myLIst.ToArray();

                    if (myArray.Length > 0)
                    {
                        for (int i = 0; i <= myArray.Length - 1; i++)
                        {

                            flxTemplates.Rows.Add();
                            int _Row = flxTemplates.Rows.Count - 1;
                            //  flxTemplates.Cols.Count = 3;    
                            flxTemplates.SetData(_Row, 0, false);
                            flxTemplates.SetData(_Row, 1, myArray[i].ToString().Trim().Replace(".docx", ""));
                            flxTemplates.SetData(_Row, 2, myArray[i].ToString().Trim());

                            for (int j = 0; j < flxTemplates.Rows.Count; j++)
                            {
                                flxTemplates.SetCellCheck(j, flxTemplates.Cols[COL_SELECT].Index, CheckEnum.Unchecked);
                            }
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Filling Child   in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }





        }

        private void cmbProvider_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                if (cmbProvider.Items.Count > 0)
                {
                    Fill_Templates(Convert.ToInt64(trvCategories.SelectedNode.Tag));
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Changing Provider    in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
        }

        private void FillTrv(DataTable Dt)
        {
            try
            {
                string RootFolder = clsGeneral.GlobalRepository;
                if (Dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in Dt.Rows)
                    {

                        //if ((dr["Title"].ToString().Contains(".aspx") == false) && (dr["Title"].ToString().Contains(".docx") == false)) 
                        if (dr["ContentType"].ToString().Contains("Document") == false)
                        {
                            //TreeNode tr = trvCategories.Nodes.Add(dr["Title"].ToString().Trim());
                            TreeNode tr = new myTreeNode();
                            tr.Text = dr["Title"].ToString().Trim();
                            tr.Tag = "GlobalFolders";
                            trvCategories.Nodes.Add(tr);
                            // GetListOfFilesInSPFolder(WebPath, "Global Repository/" & dr("Title").ToString().Trim(), tr)
                            if (RootFolder == clsGeneral.ClinicRepository)
                            {

                            }
                            else
                            {
                                //GetListOfFilesInSPFolder(clsGeneral.Webpath, clsGeneral.GlobalRepository + "/" + dr["Title"].ToString().Trim(), tr);
                            }
                        }


                    }
                }
            }
            catch
            {

            }
            // trvData.ExpandAll()
        }

        private void GetListOfFilesInSPFolder(string webpath, string FolderPath, TreeNode trnode)
        {
            pnlProcess.Visible = true;
            Application.DoEvents();
            gloLists.Lists gloList = new gloLists.Lists();

            if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                gloList.UseDefaultCredentials = true;
            else
            {
                //Added for check which authentication is use for access gloCommunity on 20120801
                if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                {
                    gloList.CookieContainer = new CookieContainer();
                    if (clsGeneral.oFormCookie == null)
                        gloList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                    else
                        gloList.CookieContainer.Add(clsGeneral.oFormCookie);
                }
                else
                {
                    clsGeneral.CheckAuthenticatedCookie();
                    gloList.CookieContainer = clsGeneral.oCookie;
                }
            }


            string myList = "";
            string _tempFolderPath = FolderPath;
            try
            {
                if (FolderPath.Contains('/'))
                {
                    myList = FolderPath.Substring(0, FolderPath.LastIndexOf('/'));
                }
                else
                {
                    myList = FolderPath;
                }

                FolderPath = webpath + FolderPath;

                gloList.Url = webpath + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                List<string> _files = new List<string>();

                // set up xml  doc for getting list of files under a folder
                XmlDocument doc = new XmlDocument();
                XmlElement queryOptions = doc.CreateElement("QueryOptions");
                queryOptions.InnerXml = "<Folder>" + FolderPath + "</Folder>";
                // get the list of files

                XmlNode listItemsNode = gloList.GetListItems(strSitefolder, null, null, null, null, queryOptions, null);

                XmlDocument xmlResultsDoc = new XmlDocument();
                xmlResultsDoc.LoadXml(listItemsNode.OuterXml);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlResultsDoc.NameTable);
                ns.AddNamespace("z", "#RowsetSchema");
                TreeNode onode = default(TreeNode);
                foreach (XmlNode row in xmlResultsDoc.SelectNodes("//z:row", ns))
                {
                    try
                    {
                        //_files.Add(row.Attributes("ows_LinkFilename").Value)
                        System.Type st = row.GetType();
                        if (row.Attributes["ows_LinkFilename"].Value.ToString().Contains(".") == false)
                        {
                            //onode = trnode.Nodes.Add(row.Attributes("ows_LinkFilename").Value)
                            onode = new TreeNode();
                            onode.Text = row.Attributes["ows_LinkFilename"].Value;
                            onode.ImageIndex = 0;
                            onode.SelectedImageIndex = 0;
                            onode.ForeColor = Color.Maroon;
                            //Added new for performance
                            onode.Tag = "Templates";
                            onode.Name = _tempFolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;
                            trnode.Nodes.Add(onode);
                            trvCategories.SelectedNode.ExpandAll();
                            //end
                            if (blntempgclinic == false)// For Global Repository
                            {
                                //if (onode.Text == "Templates")
                                //{
                                //    trnode.Nodes.Add(onode);
                                //    onode.Name = FolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;
                                //    GetListOfFilesInSPSubFolder(webpath, _tempFolderPath + "/" + row.Attributes["ows_LinkFilename"].Value, onode);
                                //}
                            }
                            else
                            {
                                trnode.Nodes.Add(onode);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;

                            }
                        }
                    }
                    catch //(Exception ex)
                    {
                        try
                        {
                            //   _files.Add(row.Attributes("ows_Title").Value)
                            trnode.Nodes.Add(row.Attributes["ows_Title"].Value);
                            onode.Name = FolderPath + "/" + row.Attributes["ows_Title"].Value;

                        }
                        catch //(Exception ex1)
                        {
                            //  _files.Add(row.Attributes("ows_NameOrTitle").Value)
                            trnode.Nodes.Add(row.Attributes["ows_NameOrTitle"].Value);
                            onode.Name = FolderPath + "/" + row.Attributes["ows_NameOrTitle"].Value;
                        }
                    }
                }
            }
            catch //(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                gloList.Dispose();
                pnlProcess.Visible = false;
            }

        }
        private List<string> GetListOfSPFilesInInnerSubFolder(string webpath, string FolderPath, TreeNode trnode)
        {
            if (FolderPath.Trim().Length > 0)
                FolderPath = FolderPath.Replace(webpath, "");
            while (FolderPath.IndexOf("/") == 0)
            {
                FolderPath = FolderPath.Substring(1, FolderPath.Length - 1);
            }
            List<string> _files = null;
            gloLists.Lists gloList = new gloLists.Lists();

            if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                gloList.UseDefaultCredentials = true;
            else
            {
                //Added for check which authentication is use for access gloCommunity on 20120801
                if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                {
                    gloList.CookieContainer = new CookieContainer();
                    if (clsGeneral.oFormCookie == null)
                        gloList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                    else
                        gloList.CookieContainer.Add(clsGeneral.oFormCookie);
                }
                else
                {
                    clsGeneral.CheckAuthenticatedCookie();
                    gloList.CookieContainer = clsGeneral.oCookie;
                }
            }

            string myList = "";

            try
            {
                if (FolderPath.Contains('/'))
                {
                    myList = FolderPath.Substring(0, FolderPath.LastIndexOf('/'));
                }
                else
                {
                    myList = FolderPath;
                }


                gloList.Url = webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                _files = new List<string>();

                // set up xml  doc for getting list of files under a folder
                XmlDocument doc = new XmlDocument();
                XmlElement queryOptions = doc.CreateElement("QueryOptions");
                queryOptions.InnerXml = "<Folder>" + FolderPath + "</Folder>";
                // get the list of files
                XmlNode listItemsNode = gloList.GetListItems(strSitefolder, null, null, null, null, queryOptions, null);

                XmlDocument xmlResultsDoc = new XmlDocument();
                xmlResultsDoc.LoadXml(listItemsNode.OuterXml);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlResultsDoc.NameTable);
                ns.AddNamespace("z", "#RowsetSchema");
             //   TreeNode onode = default(TreeNode);

                foreach (XmlNode row in xmlResultsDoc.SelectNodes("//z:row", ns))
                {
                    try
                    {
                        //_files.Add(row.Attributes("ows_LinkFilename").Value)
                        System.Type st = row.GetType();
                        if (row.Attributes["ows_LinkFilename"].Value.ToString().Contains(".") == true)
                        {
                            _files.Add(row.Attributes["ows_LinkFilename"].Value);
                        }
                    }
                    catch //(Exception ex)
                    {
                        try
                        {
                            _files.Add(row.Attributes["ows_Title"].Value);
                        }
                        catch //(Exception ex1)
                        {
                            _files.Add(row.Attributes["ows_NameOrTitle"].Value);
                        }
                    }
                }




            }

            catch (Exception ex)
            {
                //  MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while Getting List of Inner Files in  Folder  in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;

            }
            finally
            {
                gloList.Dispose();
            }
            return _files;
            ///'''''  For gloCommunity Templates as on 20110824 by Ujwala   
        }

        private List<string> GetListOfFilesInSPFolder(string folderpath, string weburl)
        {
            List<string> _files = null;
            try
            {

                string RootFolder = strSitefolder;
                gloLists.Lists listsWS = new gloLists.Lists();

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    listsWS.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        listsWS.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            listsWS.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            listsWS.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        listsWS.CookieContainer = clsGeneral.oCookie;
                    }
                }

                string myList = "";

                if (folderpath.Contains("/"))
                {
                    myList = folderpath.Substring(0, folderpath.LastIndexOf("/"));
                }
                else
                {
                    myList = folderpath;
                }
                // folderPath.Substring((folderPath.LastIndexOf('/') + 1), folderPath.Length - (folderPath.LastIndexOf('/') + 1));

                //folderPath = WebPath & folderPath

                listsWS.Url = weburl;
                _files = new List<string>();

                // set up xml  doc for getting list of files under a folder
                XmlDocument doc = new XmlDocument();
                XmlElement queryOptions = doc.CreateElement("QueryOptions");
                queryOptions.InnerXml = "<Folder>" + folderpath + "</Folder>";
                // get the list of files
                //Dim listItemsNode As XmlNode = listsWS.GetListItems(myList, Nothing, Nothing, Nothing, Nothing, queryOptions, _
                // Nothing)

                XmlNode listItemsNode = listsWS.GetListItems(RootFolder, null, null, null, null, queryOptions, null);

                XmlDocument xmlResultsDoc = new XmlDocument();
                xmlResultsDoc.LoadXml(listItemsNode.OuterXml);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlResultsDoc.NameTable);
                ns.AddNamespace("z", "#RowsetSchema");

                foreach (XmlNode row in xmlResultsDoc.SelectNodes("//z:row", ns))
                {
                    try
                    {
                        _files.Add(row.Attributes["ows_LinkFilename"].Value);
                    }
                    catch //(Exception ex)
                    {
                        try
                        {
                            _files.Add(row.Attributes["ows_Title"].Value);
                        }
                        catch //(Exception ex1)
                        {
                            _files.Add(row.Attributes["ows_NameOrTitle"].Value);

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting List of Files in  Folder  in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                return null;
            }
            return _files;
        }

        private List<string> GetListOfFilesInSPSubFolder(string webpath, string FolderPath, TreeNode trnode, string IsFrom = "", int Imageindex = 0)
        {
            ///'''''  For gloCommunity Templates as on 20110824 by Ujwala   
            pnlProcess.Visible = true;
            Application.DoEvents();

            gloLists.Lists gloList = new gloLists.Lists();
            List<string> _files = null;
            if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                gloList.UseDefaultCredentials = true;
            else
            {
                //Added for check which authentication is use for access gloCommunity on 20120801
                if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                {
                    gloList.CookieContainer = new CookieContainer();
                    if (clsGeneral.oFormCookie == null)
                        gloList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                    else
                        gloList.CookieContainer.Add(clsGeneral.oFormCookie);
                }
                else
                {
                    clsGeneral.CheckAuthenticatedCookie();
                    gloList.CookieContainer = clsGeneral.oCookie;
                }
            }

            string myList = "";
            string _tempFolderPath = FolderPath;


            try
            {
                if (FolderPath.Contains('/'))
                {
                    myList = FolderPath.Substring(0, FolderPath.LastIndexOf('/'));
                }
                else
                {
                    myList = FolderPath;
                }

                FolderPath = webpath + FolderPath;
                if (IsFrom == "User")
                    gloList.Url = clsGeneral.Webpath + clsGeneral.gstrClinicName + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;//webpath + "/" + clsGeneral.ClinicRepository + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                else
                    gloList.Url = webpath + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                _files = new List<string>();

                // set up xml  doc for getting list of files under a folder
                XmlDocument doc = new XmlDocument();
                XmlElement queryOptions = doc.CreateElement("QueryOptions");
                queryOptions.InnerXml = "<Folder>" + FolderPath + "</Folder>";
                // get the list of files
                XmlNode listItemsNode = gloList.GetListItems(strSitefolder, null, null, null, null, queryOptions, null);

                XmlDocument xmlResultsDoc = new XmlDocument();
                xmlResultsDoc.LoadXml(listItemsNode.OuterXml);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlResultsDoc.NameTable);
                ns.AddNamespace("z", "#RowsetSchema");
                TreeNode onode = default(TreeNode);
                foreach (XmlNode row in xmlResultsDoc.SelectNodes("//z:row", ns))
                {
                    try
                    {
                        //_files.Add(row.Attributes("ows_LinkFilename").Value)
                        System.Type st = row.GetType();
                        if (row.Attributes["ows_LinkFilename"].Value.ToString().Contains(".") == false)
                        {
                            //onode = trnode.Nodes.Add(row.Attributes("ows_LinkFilename").Value)
                            onode = new TreeNode();
                            //onode.Text = row.Attributes("ows_LinkFilename").Value.ToString()
                            onode.Text = Path.GetFileNameWithoutExtension(row.Attributes["ows_LinkFilename"].Value.ToString());
                            onode.ImageIndex = Imageindex;
                            onode.SelectedImageIndex = Imageindex;
                            onode.ForeColor = Color.Maroon;
                            onode.Name = _tempFolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;
                            onode.Tag = "Categories";

                            //onode.Name = FolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;
                            // onode.Tag = webpath  + FolderPath + "/" + row.Attributes["ows_Title"].Value;
                            trnode.Nodes.Add(onode);

                            //if (blntempgclinic == true)
                            //{
                            if (cmbCategory.Items.IndexOf(onode.Text) == -1)
                                cmbCategory.Items.Add(onode.Text);
                            //}


                            // if (pnlbtncentral.Dock == DockStyle.Top)
                            //{
                            // GetListOfFilesInSPInnerSubFolder(webpath, _tempFolderPath + "/" + row.Attributes["ows_LinkFilename"].Value, onode );
                            // }  //uncommented  now 15 oct 2011
                        }
                        else
                        {
                            try
                            {
                                _files.Add(row.Attributes["ows_LinkFilename"].Value);
                            }
                            catch //(Exception ex)
                            {
                                try
                                {
                                    _files.Add(row.Attributes["ows_Title"].Value);
                                }
                                catch //(Exception ex1)
                                {
                                    _files.Add(row.Attributes["ows_NameOrTitle"].Value);

                                }
                            }
                        }
                    }
                    catch //(Exception ex)
                    {
                        try
                        {
                            //   _files.Add(row.Attributes("ows_Title").Value)
                            trnode.Nodes.Add(row.Attributes["ows_Title"].Value);
                            onode.Name = FolderPath + "/" + row.Attributes["ows_Title"].Value;

                        }
                        catch //(Exception ex1)
                        {
                            //  _files.Add(row.Attributes("ows_NameOrTitle").Value)
                            trnode.Nodes.Add(row.Attributes["ows_NameOrTitle"].Value);
                            onode.Name = FolderPath + "/" + row.Attributes["ows_NameOrTitle"].Value;

                        }
                    }
                }
                trvCategories.SelectedNode.ExpandAll();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting List of Files in Sub Folder  in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                gloList.Dispose();
                pnlProcess.Visible = false;
                Application.DoEvents();
            }
            ///'''''  For gloCommunity Templates as on 20110824 by Ujwala
            return _files;
        }

        private void GetListOfFilesInSPInnerSubFolder(string webpath, string FolderPath, TreeNode trnode)
        {
            bool central = false;
            if (blntempgclinic == false)
                central = true;

            gloLists.Lists gloList = new gloLists.Lists();

            if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                gloList.UseDefaultCredentials = true;
            else
            {
                //Added for check which authentication is use for access gloCommunity on 20120801
                if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                {
                    gloList.CookieContainer = new CookieContainer();
                    if (clsGeneral.oFormCookie == null)
                        gloList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                    else
                        gloList.CookieContainer.Add(clsGeneral.oFormCookie);
                }
                else
                {
                    clsGeneral.CheckAuthenticatedCookie();
                    gloList.CookieContainer = clsGeneral.oCookie;
                }
            }

            string myList = "";

            try
            {
                if (FolderPath.Contains('/'))
                {
                    myList = FolderPath.Substring(0, FolderPath.LastIndexOf('/'));
                }
                else
                {
                    myList = FolderPath;
                }

                FolderPath = webpath + "/" + FolderPath;

                gloList.Url = webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                List<string> _files = new List<string>();

                // set up xml  doc for getting list of files under a folder
                XmlDocument doc = new XmlDocument();
                XmlElement queryOptions = doc.CreateElement("QueryOptions");
                queryOptions.InnerXml = "<Folder>" + FolderPath + "</Folder>";
                // get the list of files
                XmlNode listItemsNode = gloList.GetListItems(strSitefolder, null, null, null, null, queryOptions, null);

                XmlDocument xmlResultsDoc = new XmlDocument();
                xmlResultsDoc.LoadXml(listItemsNode.OuterXml);
                XmlNamespaceManager ns = new XmlNamespaceManager(xmlResultsDoc.NameTable);
                ns.AddNamespace("z", "#RowsetSchema");
                TreeNode onode = default(TreeNode);
                if (central == true)
                {
                    foreach (XmlNode row in xmlResultsDoc.SelectNodes("//z:row", ns))
                    {
                        try
                        {
                            //_files.Add(row.Attributes("ows_LinkFilename").Value)
                            System.Type st = row.GetType();
                            if (row.Attributes["ows_LinkFilename"].Value.ToString().Contains(".") == true)
                            {
                                //onode = trnode.Nodes.Add(row.Attributes("ows_LinkFilename").Value)
                                onode = new TreeNode();
                                //onode.Text = row.Attributes("ows_LinkFilename").Value.ToString()
                                onode.Text = Path.GetFileNameWithoutExtension(row.Attributes["ows_LinkFilename"].Value.ToString());
                                onode.ImageIndex = 3;
                                onode.SelectedImageIndex = 3;
                                onode.ForeColor = Color.Blue;
                                trnode.Nodes.Add(onode);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;
                                trvCategories.SelectedNode.ExpandAll();
                            }
                        }
                        catch //(Exception ex)
                        {
                            try
                            {
                                //   _files.Add(row.Attributes("ows_Title").Value)
                                trnode.Nodes.Add(row.Attributes["ows_Title"].Value);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_Title"].Value;

                            }
                            catch //(Exception ex1)
                            {
                                //  _files.Add(row.Attributes("ows_NameOrTitle").Value)
                                trnode.Nodes.Add(row.Attributes["ows_NameOrTitle"].Value);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_NameOrTitle"].Value;

                            }
                        }
                    }
                }

                else
                {

                    foreach (XmlNode row in xmlResultsDoc.SelectNodes("//z:row", ns))
                    {
                        try
                        {
                            //_files.Add(row.Attributes("ows_LinkFilename").Value)
                            System.Type st = row.GetType();
                            if (row.Attributes["ows_LinkFilename"].Value.ToString().Contains(".") == false)
                            {
                                //onode = trnode.Nodes.Add(row.Attributes("ows_LinkFilename").Value)
                                onode = new TreeNode();
                                //onode.Text = row.Attributes("ows_LinkFilename").Value.ToString()
                                onode.Text = Path.GetFileNameWithoutExtension(row.Attributes["ows_LinkFilename"].Value.ToString());
                                onode.ImageIndex = 3;
                                onode.SelectedImageIndex = 3;
                                onode.ForeColor = Color.Blue;
                                trnode.Nodes.Add(onode);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_LinkFilename"].Value;
                                trvCategories.SelectedNode.ExpandAll();
                                //if (pnlbtnlocal.Dock == DockStyle.Top)
                                //{
                                //    if (cmbCategory.Items.IndexOf(onode.Text) == -1)
                                //        cmbCategory.Items.Add(onode.Text);
                                //}
                            }
                        }
                        catch //(Exception ex)
                        {
                            try
                            {
                                //   _files.Add(row.Attributes("ows_Title").Value)
                                trnode.Nodes.Add(row.Attributes["ows_Title"].Value);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_Title"].Value;

                            }
                            catch //(Exception ex1)
                            {
                                //  _files.Add(row.Attributes("ows_NameOrTitle").Value)

                                trnode.Nodes.Add(row.Attributes["ows_NameOrTitle"].Value);
                                onode.Name = FolderPath + "/" + row.Attributes["ows_NameOrTitle"].Value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting List of Files  in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                gloList.Dispose();
            }
            ///'''''  For gloCommunity Templates as on 20110824 by Ujwala   
        }

        private void ShowXMLFIleData(string XmlFileUrl, string UserName, string Password, string Domain)
        {

        }

        private void wdopenfl_OnFileCommand(object sender, AxDSOFramer._DFramerCtlEvents_OnFileCommandEvent e)
        {

        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pnlProcess.Visible = true;
            Application.DoEvents();
            trvCategories.Nodes.Clear();
            ClearGridData();
            blntempgclinic = false;
            wdopenfl.Close();
            //string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrsmdxflnm + ".xml";
            // string fileUrl = clsGeneral.Webpath + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrsmdxflnm + ".xml";

            strSitefolder = clsGeneral.GlobalRepository;
            //  pnlbtncentral.Dock = DockStyle.Top;
            // pnlbtnlocal.Dock = DockStyle.Bottom;
            try
            {
                clsgloCommunity objclsgcomm = new clsgloCommunity();
                gloLists.Lists myservice = new gloLists.Lists();

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    myservice.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        myservice.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            myservice.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            myservice.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        myservice.CookieContainer = clsGeneral.oCookie;
                    }
                }

                myservice.Url = clsGeneral.Webpath + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                System.Xml.XmlNode node = myservice.GetListCollection();

                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {


                        if (xmlnode.Attributes["Title"].Value.ToString() == clsGeneral.GlobalRepository)//clsGeneral.WebGlobalXmlFolder)
                        {
                            DataTable dt = new DataTable();
                            dt = objclsgcomm.GetList(xmlnode.Attributes["Title"].Value.ToString(), clsGeneral.Webpath);
                            FillTrv(dt);
                        }
                    }
                }
                trvCategories.ExpandAll();
                cmbCategory.Items.Clear();
                foreach (TreeNode tr in trvCategories.Nodes)
                {
                    tr.ImageIndex = 2;
                    tr.SelectedImageIndex = 2;
                    foreach (TreeNode trc in tr.Nodes)
                    {
                        trc.ImageIndex = 0;
                        trc.SelectedImageIndex = 0;
                        foreach (TreeNode trcc in trc.Nodes)
                        {
                            trcc.ImageIndex = 1;
                            trcc.SelectedImageIndex = 1;
                            if (cmbCategory.Items.IndexOf(trcc.Text) == -1)
                            {
                                cmbCategory.Items.Add(trcc.Text);
                            }
                        }
                    }
                }
                if (cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while  clicking on Global Repository in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                pnlProcess.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pnlProcess.Visible = true;
            Application.DoEvents();
            blntempgclinic = true;
            trvCategories.Nodes.Clear();
            ClearGridData();
            cmbCategory.Items.Clear();
            wdopenfl.Close();
            // string fileUrl = clsGeneral.Webpath + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrsmdxflnm + ".xml"; //commented on 3 dec 2011
            strSitefolder = clsGeneral.ClinicRepository;

            try
            {
                clsgloCommunity objclsgcomm = new clsgloCommunity();
                gloLists.Lists myservice = new gloLists.Lists();

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    myservice.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        myservice.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            myservice.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            myservice.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        myservice.CookieContainer = clsGeneral.oCookie;
                    }
                }

                myservice.Url = clsGeneral.Webpath + clsGeneral.gstrClinicName + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                System.Xml.XmlNode node = myservice.GetListCollection();

                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {


                        if (xmlnode.Attributes["Title"].Value.ToString() == clsGeneral.ClinicRepository)//"Repository")
                        {
                            DataTable dt = new DataTable();
                            dt = objclsgcomm.GetList(xmlnode.Attributes["Title"].Value.ToString(), clsGeneral.Webpath + clsGeneral.gstrClinicName);

                            for (int lenitem = 0; lenitem <= dt.Rows.Count - 1; lenitem++)
                            {
                                string StrName = dt.Rows[lenitem]["title"].ToString();
                                string StrConType = dt.Rows[lenitem]["ContentType"].ToString();
                                string fileUrl2 = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrsmdxflnm + ".xml";


                                // if ((StrName.Contains(".aspx") == false) && (StrName.Contains(".docx") == false))
                                if (StrConType.Contains("Document") == false)
                                {
                                    TreeNode tr = trvCategories.Nodes.Add(StrName);
                                    //  tr.Tag = fileUrl;   //commented on 3 dec 2011

                                    GetListOfFilesInSPSubFolder(clsGeneral.Webpath + clsGeneral.gstrClinicName + "/", clsGeneral.ClinicRepository + "/" + StrName, tr, "User", 2);
                                }
                            }

                            // FillTrv(dt)
                        }
                    }
                }

                trvCategories.ExpandAll();
                if (cmbCategory.Items.Count > 0)
                {
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while  clicking on ClinicRepository in Template UserControl : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                pnlProcess.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void flxTemplates_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bool _CellCheckResult = false;
                var _with1 = flxTemplates;
                int RowIndex = _with1.HitTest(e.X, e.Y).Row;

                if (RowIndex == 0)
                {
                    CheckEnum _CheckUncheck = new CheckEnum();

                    _CheckUncheck = _with1.GetCellCheck(0, 0);

                    if (_CheckUncheck == CheckEnum.Checked)
                        _CellCheckResult = true;
                    else
                        _CellCheckResult = false;

                    for (int i = 1; i < _with1.Rows.Count; i++)
                    {
                        _with1.SetData(i, COL_SELECT, _CellCheckResult);
                    }
                }
            }
        }

        private void flxTemplates_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var _with1 = flxTemplates;
            int _RowIndex = _with1.HitTest(e.X, e.Y).Row;
            if (_with1.Rows.Count > 1)
            {
                if (_RowIndex != 0)
                {
                    if (straction == "Upload")
                    {
                        clsTemplate objclsTemplate = new clsTemplate();
                        string strFileName = "";
                        try
                        {
                            //if COL_HiddenTempNm is empty OR null it means template is not available on physical path then get from database
                            if ((Convert.ToString(flxTemplates.GetData(flxTemplates.RowSel, COL_HiddenTempNm)) == string.Empty) || ((flxTemplates.GetData(flxTemplates.RowSel, COL_HiddenTempNm) == null)))
                            {
                                strFileName = objclsTemplate.Fill_TemplateGallery(Convert.ToInt64(flxTemplates.GetData(flxTemplates.RowSel, COL_TEMPLATEID)));

                                if (strFileName != string.Empty)
                                {
                                    //Assign the template name to the COL_HiddenTempNm

                                    string FNM = strFileName.Substring((strFileName.LastIndexOf('\\') + 1), (strFileName.Length - (strFileName.LastIndexOf('\\') + 1)));
                                    _with1.SetData(flxTemplates.RowSel, COL_HiddenTempNm, FNM);
                                }
                            }
                            else //else get from physical path.
                                strFileName = gloSettings.FolderSettings.AppTempFolderPath + flxTemplates.GetData(flxTemplates.RowSel, COL_HiddenTempNm);

                            LoadWordControl(strFileName);
                        }
                        catch (Exception ex)
                        {
                            //clsGeneral.UpdateLog("Error  while double clicking on Template grid in Template UserControl : " + ex.Message.ToString());  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            //MessageBox.Show(ex.Message); 
                        }
                        finally { objclsTemplate = null; }
                    }
                    else
                    {
                        // clsTemplate objclsTemplate = new clsTemplate();
                        try
                        {

                            // dtcontit = objclsTemplate.GetContentTitle();

                            string filename = flxTemplates.GetData(flxTemplates.RowSel, 2).ToString();
                            string _Path = string.Empty;
                            flxTemplates.SelectionMode = C1.Win.C1FlexGrid.SelectionModeEnum.Row;
                            //   grdTemplateGallery.Rows(grdTemplateGallery.RowSel).Selected = True
                            if (filename.Contains("."))
                            {
                                if (blntempgclinic == false)
                                    _Path = clsGeneral.Webpath + trvCategories.SelectedNode.Name;
                                else
                                    _Path = clsGeneral.Webpath + clsGeneral.gstrClinicName + "/" + trvCategories.SelectedNode.Name;
                                SeeDownLoadFile(_Path + "/" + filename, filename);
                            }

                        }
                        catch (Exception ex)
                        {
                            //clsGeneral.UpdateLog("Error  while double clicking on Template grid in Template UserControl : " + ex.Message.ToString());  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        finally
                        {
                            // objclsTemplate = null;

                        }

                    }
                }//_RowIndex
            }//_with1.Rows.Count > 1
        }

        private void ClearGridData()
        {
           // flxTemplates.Clear();
            flxTemplates.DataSource = null;

            if (flxTemplates.Rows.Count >= 2)
            {
                flxTemplates.Rows.RemoveRange(1, flxTemplates.Rows.Count - 1);
            }
        }
    }
}
