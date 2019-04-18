using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Xml;
using System.Collections;
using System.Net;
using System.IO;
using System.Configuration;
namespace gloCommunity.UserControls
{
    public partial class UCFormGallery : UserControl
    {
        string straction = "";
        clsformgallery objcls = new clsformgallery();
        public DataTable dtCPTDesc = null;
        public string _strTemplateNames = "";
        string WebPath = "";
        TreeNode RootNode = null;
        public bool gblnfrmglryclinic = true;
        public UCFormGallery()
        {
            InitializeComponent();
        }
        public UCFormGallery(string _straction)
        {
            InitializeComponent();
            straction = _straction;

            // btnLocal.Text = clsGeneral.ClinicRepository;
            // btnCentral.Text = clsGeneral.GlobalRepository;

            //  pnl_btnLocal.Dock = DockStyle.Top;
            //  pnl_btnCentral.Dock = DockStyle.Bottom;
            trvformglry.SearchBox = false;
            RootNode = new TreeNode();
            RootNode.Text = "CPT Association";
            RootNode.ImageIndex = 0;
            RootNode.SelectedImageIndex = 0;
            trvcptassoc.Nodes.Add(RootNode);
            if (_straction == "Upload")
            {
                // objclsfl = new clsFlowSheet();
                //   DataTable dtflow = objclsfl.getFlowsheetName();
                // filltrvflowsheet(dtflow);
                pnlleft.Visible = false;
                pnltls.Visible = false;
            }
            else
            {
                //  dtFlowsheet = new DataTable();
                //  FillLocalClinicData();
                gblnfrmglryclinic = true;
                FillLocalClinicData();
                pnltls.Visible = true;
                pnlleft.Visible = false;
            }
        }

        private void FillLocalClinicData()
        {
            try
            {
                //   trvappconf.SearchBox = false;
                //      pnl_btnLocal.Dock = DockStyle.Top;
                WebPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                string fileUrl = WebPath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrformglry + "/" + clsGeneral.gstrformglry + ".xml";
                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Filling Clinic Data  in  Usercontrol Formgallery: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void ShowXMLFIleData(string XmlFileUrl, string UserName, string Password, string Domain)
        {
            HttpWebRequest request = default(HttpWebRequest);
            HttpWebResponse response = null;
            DataSet ds = new DataSet();
            try
            {
                request = (HttpWebRequest)WebRequest.Create(XmlFileUrl);

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    request.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120730
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        ((System.Net.HttpWebRequest)(request)).CookieContainer = new CookieContainer();

                        if (clsGeneral.oFormCookie == null)
                            ((System.Net.HttpWebRequest)(request)).CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            ((System.Net.HttpWebRequest)(request)).CookieContainer.Add(clsGeneral.oFormCookie);

                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        ((System.Net.HttpWebRequest)(request)).CookieContainer = clsGeneral.oCookie;
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

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrformglry + ".xml"; //"SmartDxAssociation.xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();
                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrformglry + ".xml");
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.ToString(), "Form Gallery", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //clsGeneral.UpdateLog("glocomm Error While getting XML Data for  files in  Usercontrol Formgallery: " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                    // MessageBox.Show(exp.ToString());
                }

                if (ds.Tables.Count > 0)
                {
                    dtCPTDesc = ds.Tables[0];
                    dtCPTDesc.AcceptChanges();
                    trvcpt.Nodes.Clear();
                    foreach (DataRow dr in dtCPTDesc.Rows)
                    {
                        TreeNode trnodecpt = new TreeNode();
                        trnodecpt.Tag = dr[0].ToString();
                        ////Fixed Bug id : 45715 on 20130221
                        trnodecpt.Text = dr[0].ToString() + " - " + dr[1].ToString().Trim();
                        ////End
                        trvcpt.Nodes.Add(trnodecpt);
                    }
                }
                //  fillDownloadedData(showdata);
               // int k = 0;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While getting XML Data for  files in  Usercontrol Formgallery: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                ds.Dispose();

            }
        }

        private void UCFormGallery_Load(object sender, EventArgs e)
        {
            //  btnLocal.Text = clsGeneral.ClinicRepository;
            //   btnCentral.Text = clsGeneral.GlobalRepository;
            if (straction == "Upload")
            {
                DataTable dtCPTDesc = objcls.getCPT();
                // trvformglry.DataSource=dtCPTDesc;
                //  trvformglry.ValueMember = dtCPTDesc.Columns[0].ColumnName;
                //    trvformglry.Tag = dtCPTDesc.Columns[0].ColumnName;
                //   trvformglry.CodeMember = dtCPTDesc.Columns[3].ColumnName;
                //   trvformglry.DescriptionMember = dtCPTDesc.Columns[1].ColumnName;
                //   CPTID , sDescription AS sDescription ,sCPTCode+' - '+sDescription, sCPTCode AS CPTCode
                if (dtCPTDesc != null)
                {
                    foreach (DataRow dr in dtCPTDesc.Rows)
                    {
                        TreeNode trnodecpt = new TreeNode();
                        trnodecpt.Tag = dr[0].ToString();
                        trnodecpt.Text = dr[2].ToString();
                        trvcpt.Nodes.Add(trnodecpt);
                    }
                }
            }
        }

        public ArrayList GetListOfSPFilesInInnerSubFolder(string webpath, ArrayList ArrCatName, string Type)
        {
            string FolderPath = "";
            ArrayList arrtempname = new ArrayList();
            ArrayList ArrFilePath = new ArrayList();
            for (int lencatname = 0; lencatname < ArrCatName.Count; lencatname++)
            {
                try
                {
                    string strSitefolder = "";
                    if (Type == "Clinic")
                    {
                        webpath = clsGeneral.Webpath + clsGeneral.gstrClinicName;
                        //     FolderPath = "Repository/Templates/SOAP";
                        FolderPath = clsGeneral.ClinicWebFolder + "/" + ArrCatName[lencatname].ToString();  // "Soap";
                        strSitefolder = clsGeneral.ClinicRepository;
                        // webpath += "/" + strSitefolder; 
                        if (FolderPath.Trim().Length > 0)
                            FolderPath = FolderPath.Replace(webpath, "");
                        while (FolderPath.IndexOf("/") == 0)
                        {
                            FolderPath = FolderPath.Substring(1, FolderPath.Length - 1);
                        }
                    }
                    else
                    {
                        string gWebpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;

                        string gWebFolder = clsGeneral.GlobalRepository;
                        string gWebSite = clsGeneral.gstrSharepointSiteNm;

                        //   FolderPath = gWebFolder + "/" + "Templates/SOAP"; //+ "/" + clsGeneral.ClinicWebFolder;//+ "/" + "Soap";
                        FolderPath = gWebFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + ArrCatName[lencatname].ToString();  //"Soap";
                        webpath = gWebpath;//+ "/" + gWebFolder+ "/" + FolderPath;
                        strSitefolder = gWebFolder;

                    }
                    // List<string> _files = null;
                    gloLists.Lists gloList = new gloLists.Lists();

                    if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                        gloList.UseDefaultCredentials = true;
                    else
                    {
                        //Added for check which authentication is use for access gloCommunity on 20120730
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
                        //  _files = new List<string>();

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
                                if (Type == "Clinic")
                                {
                                    if (row.Attributes["ows_LinkFilename"].Value.ToString().Contains(".") == true)
                                    {
                                        string filepath = row.Attributes["ows_FileRef"].Value.ToString();
                                        string[] splfilepath = filepath.Split('/');
                                        string strcatname = "";
                                        if (splfilepath.Length >= 2)
                                        {
                                            strcatname = splfilepath[splfilepath.Length - 2];
                                        }
                                        if (arrtempname.Contains(row.Attributes["ows_LinkFilename"].Value.Replace(".docx", "") + "≈" + strcatname) == false)
                                            arrtempname.Add(row.Attributes["ows_LinkFilename"].Value.Replace(".docx", "") + "≈" + strcatname);
                                    }
                                }
                                else
                                {
                                    if (row.Attributes["ows_LinkFilenameNoMenu"].Value.ToString().Contains(".") == true)
                                    {

                                        string filepath = row.Attributes["ows_FileRef"].Value.ToString();
                                        string[] splfilepath = filepath.Split('/');
                                        string strcatname = "";
                                        if (splfilepath.Length >= 2)
                                        {
                                            strcatname = splfilepath[splfilepath.Length - 2];
                                        }

                                        if (arrtempname.Contains(row.Attributes["ows_LinkFilenameNoMenu"].Value.Replace(".docx", "") + "≈" + strcatname) == false)

                                            arrtempname.Add(row.Attributes["ows_LinkFilenameNoMenu"].Value.Replace(".docx", "") + "≈" + strcatname);
                                    }
                                }
                            }
                            catch //(Exception ex)
                            {
                                try
                                {
                                    arrtempname.Add(row.Attributes["ows_Title"].Value + "≈" + ArrCatName[lencatname].ToString());
                                }
                                catch //(Exception ex1)
                                {
                                    arrtempname.Add(row.Attributes["ows_NameOrTitle"].Value + "≈" + ArrCatName[lencatname].ToString());
                                }
                            }
                        }
                    }

                    catch (Exception ex)
                    {
                        //clsGeneral.UpdateLog("glocomm Error While getting inner sub folder files in  Usercontrol Formgallery: " + ex.Message.ToString());  
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        //  MessageBox.Show(ex.Message);
                        // return null;
                    }
                    finally
                    {
                        gloList.Dispose();
                    }
                }
                catch (Exception ex)
                {
                    //clsGeneral.UpdateLog("glocomm Error While Getting list of innerfiles in  Usercontrol Formgallery: " + ex.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }

            }
            return arrtempname;
            ///'''''  For gloCommunity Templates as on 20110824 by Ujwala   
        }

        private void trvcpt_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //RootNode.Nodes.Clear();   
            RootNode.Expand();
            if (straction == "Upload")
            {
                bool nodefound = false;

                foreach (TreeNode trnode in RootNode.Nodes)
                {
                    trnode.Collapse();
                    if (trnode.Text == e.Node.Text)
                    {
                        nodefound = true;
                        break;
                    }
                }
                if (nodefound == false)
                {
                    TreeNode CPTNode = new TreeNode();
                    CPTNode.Text = e.Node.Text;
                    CPTNode.Tag = e.Node.Tag;
                    CPTNode.ImageIndex = 1;
                    CPTNode.SelectedImageIndex = 1;
                    RootNode.Nodes.Add(CPTNode);
                    CPTNode.Collapse();
                    dtCPTDesc = objcls.getCorresTemplate(Convert.ToInt64(e.Node.Tag));

                    foreach (DataRow dr in dtCPTDesc.Rows)
                    {
                        TreeNode ChildNode = new TreeNode();
                        ChildNode.Text = dr[0].ToString();
                        ChildNode.Tag = dr[1].ToString();
                        ChildNode.ImageIndex = 2;
                        ChildNode.SelectedImageIndex = 2;
                        CPTNode.Nodes.Add(ChildNode);
                    }
                }
                //  flxtemplate.DataSource = dtCPTDesc;
                // flxtemplate.Cols[1].Visible = false;    
            }
            else
            {
                bool nodefound = false;
                TreeNode CPTNode = null;
                foreach (TreeNode trnode in RootNode.Nodes)
                {
                    trnode.Collapse();
                    if (trnode.Text == e.Node.Text)
                    {
                        nodefound = true;
                        break;
                    }
                }
                if (nodefound == false)
                {
                    CPTNode = new TreeNode();
                    CPTNode.Text = e.Node.Text;
                    CPTNode.ImageIndex = 1;
                    CPTNode.SelectedImageIndex = 1;
                    CPTNode.Tag = e.Node.Tag;
                    CPTNode.Collapse();
                    RootNode.Nodes.Add(CPTNode);

                    DataRow[] dr = dtCPTDesc.Select("CPTCODE='" + e.Node.Tag.ToString() + "'");
                    string Templatename = dr[0][2].ToString();
                    string[] splTempname = Templatename.Split(';');
                    //flxtemplate.Cols.Add(); 
                    // flxtemplate.Cols[0].Caption = "Template Name";
                    // flxtemplate.Cols.Add();
                    // flxtemplate.Cols[1].Caption = "Category Name";
                    // flxtemplate.Cols[1].Visible = false;  
                    for (int templen = 0; templen < splTempname.Length; templen++)
                    {
                        // flxtemplate.Rows.Add();
                        string TemplateName = splTempname[templen].ToString().Substring(0, splTempname[templen].ToString().IndexOf("≈"));
                        string CategoryName = splTempname[templen].ToString().Substring(splTempname[templen].ToString().IndexOf("≈") + 1, (splTempname[templen].ToString().Length - (splTempname[templen].ToString().IndexOf("≈") + 1)));

                        _strTemplateNames += "'" + TemplateName + "',";

                        //  flxtemplate.SetData(flxtemplate.Rows.Count - 1, 0, TemplateName);
                        //  flxtemplate.SetData(flxtemplate.Rows.Count - 1, 1, CategoryName);

                        TreeNode ChildNode = new TreeNode();
                        ChildNode.Text = TemplateName;
                        ChildNode.Tag = CategoryName;
                        ChildNode.ImageIndex = 2;
                        ChildNode.SelectedImageIndex = 2;
                        CPTNode.Nodes.Add(ChildNode);
                    }
                    if (_strTemplateNames.Trim().Length > 0)
                        _strTemplateNames = _strTemplateNames.Substring(0, _strTemplateNames.Length - 1);
                }
            }

            if (RootNode.Nodes.Count == 1)
                RootNode.Expand();
        }

        private void btnCentral_Click(object sender, EventArgs e)
        {
            try
            {
                trvcpt.Nodes.Clear();
                trvformglry.Nodes.Clear();
                RootNode.Nodes.Clear();
                GetCentralList();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Clicking on Central Repository  in  Usercontrol Formgallery: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void GetCentralList()
        {
            clsgloCommunity objclsgcomm = new clsgloCommunity();
            gloLists.Lists myservice = new gloLists.Lists();

            if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                myservice.UseDefaultCredentials = true;
            else
            {
                //Added for check which authentication is use for access gloCommunity on 20120730
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

            myservice.Url = WebPath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;// SiteURL + gstrVti_Bin + "/" + gstrListSvc;
            System.Xml.XmlNode node = myservice.GetListCollection();

            // Loop through XML response and parse out the value of the
            // Title attribute for each list.
            foreach (System.Xml.XmlNode xmlnode in node)
            {
                if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                {
                    if (xmlnode.Attributes["Title"].Value.ToString() == clsGeneral.WebGlobalXmlFolder)
                    {
                        DataTable dt = new DataTable();
                        dt = objclsgcomm.GetList(xmlnode.Attributes["Title"].Value.ToString(), WebPath + "/");

                        for (int lenitem = 0; lenitem <= dt.Rows.Count - 1; lenitem++)
                        {
                            gloUserControlLibrary.myTreeNode tr = new gloUserControlLibrary.myTreeNode();
                            string StrName = dt.Rows[lenitem]["title"].ToString();
                            tr.Text = StrName;

                            string fileUrl = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrformglry + "/" + clsGeneral.gstrformglry + ".xml";   //SmartDxAssociationSRV + ".xml";

                            // string fileUrl = WebPath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrformglry  + ".xml";

                            tr.Tag = fileUrl;
                            if (tr.Text.Contains(".aspx") == false)
                                trvformglry.Nodes.Add(tr);
                        }
                    }
                }
            }
        }

        private void btnLocal_Click(object sender, EventArgs e)
        {
            // ClearGridData();
            trvcpt.Nodes.Clear();
            trvformglry.Nodes.Clear();
            RootNode.Nodes.Clear();
            FillLocalClinicData();
        }

        private void trvformglry_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            trvcpt.Nodes.Clear();
            RootNode.Nodes.Clear();
            if (straction == "Download")
            {
                ShowXMLFIleData(trvformglry.SelectedNode.Tag.ToString(), "", "", "");
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //  ClearGridData();

                trvcpt.Nodes.Clear();
                trvformglry.Nodes.Clear();
                RootNode.Nodes.Clear();
                gblnfrmglryclinic = false;
                pnlleft.Visible = true;
                //  trvblconf.Nodes.Clear();

                // trvcpt.Nodes.Clear();  
                //foreach (DataRow dr in dtCPTDesc.Rows)
                //{
                //    TreeNode trnodecpt = new TreeNode();
                //    trnodecpt.Tag = dr[0].ToString();
                //    trnodecpt.Text = dr[2].ToString();
                //    trvcpt.Nodes.Add(trnodecpt);
                //}
                GetCentralList();
                // trvblconf.ExpandAll();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Clicking on Global Repository  in  Usercontrol Formgallery: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                gblnfrmglryclinic = true;
                // ClearGridData();
                pnlleft.Visible = false;
                trvcpt.Nodes.Clear();
                trvformglry.Nodes.Clear();
                RootNode.Nodes.Clear();
                FillLocalClinicData();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Clicking on Clinical Repository  in  Usercontrol Formgallery: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void trvcptassoc_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if ((e.Node.Level == 0) && (e.Node.Nodes.Count > 0))
            {
                bool chk = e.Node.Checked;
                foreach (TreeNode trnode in e.Node.Nodes)
                {
                    trnode.Checked = chk;
                }
            }
        }
    }
}