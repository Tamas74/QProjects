using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Xml;
using System.Text;
using System.Windows.Forms;
//using gloDatabaseLayer;
//using gloWord;
using gloOffice;
using gloEMRGeneralLibrary.gloEMRDatabase;
using gloCommunity.Classes;
using System.Text.RegularExpressions;
using System.Configuration;
//using gloEMR.gloEMRWord;
//using gloEMRGeneralLibrary.gloEMRDatabase;

namespace gloCommunity
{
    public class clsgloCommunity
    {

        ArrayList arrSPCatFileNm = new ArrayList();
        ArrayList arrGlobalCatFileNm = new ArrayList();

        ArrayList arrLocalCatFileNm = new ArrayList();

        public bool UploadFileToDocumentLibrary(string webpath, string FolderNM, string FileNM, string MainPath, string webSite, string webFolder, string ClinicGblFolder = "")
        {
            bool isUploaded = true;
            try
            {
                string FNM = FileNM.Substring((FileNM.LastIndexOf('\\') + 1), (FileNM.Length - (FileNM.LastIndexOf('\\') + 1)));
                string webUrl = webpath + "/" + FolderNM + "/" + FNM;
                string Path = "";

                if (ClinicGblFolder != string.Empty)
                {
                    if (ClinicGblFolder.Contains("/"))
                    {
                        string[] arrFldNm = ClinicGblFolder.Split('/');
                        Path = MainPath + webSite + "/" + webFolder + "/" + arrFldNm[0];
                        if (IsExists(Path) == true)
                        {
                            UpdateListItemCreateFolder(MainPath + webSite, arrFldNm[0], webFolder);
                        }
                    }

                    Path = MainPath + webSite + "/" + webFolder + "/" + ClinicGblFolder;
                    if (IsExists(Path) == true)
                    {
                        UpdateListItemCreateFolder(MainPath + webSite, ClinicGblFolder, webFolder);
                    }
                }


                Path = MainPath + webSite + "/" + webFolder + "/" + FolderNM;
                if (IsExists(Path) == true)
                {
                    UpdateListItemCreateFolder(MainPath + webSite, FolderNM, webFolder);
                }

                WebRequest request = WebRequest.Create(webUrl);

                //provide network credentials used for authentication
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    request.Credentials = CredentialCache.DefaultCredentials;
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
                    //End
                }

                request.Method = "PUT";

                byte[] fileBuffer = new byte[5024];

                using (Stream stream = request.GetRequestStream())
                {
                    using (FileStream fs = File.Open(FileNM, FileMode.Open, FileAccess.Read))
                    {

                        int startBuffer = fs.Read(fileBuffer, 0, fileBuffer.Length);

                        int i = startBuffer;
                        while (i > 0)
                        {
                            stream.Write(fileBuffer, 0, i);
                            i = fs.Read(fileBuffer, 0, fileBuffer.Length);
                        }

                    }
                }

                WebResponse response = request.GetResponse();

                response.Close();
                //Delete temp file after upload
                if (File.Exists(FileNM))
                    File.Delete(FileNM);

                request = null;

            }
            catch //(WebException ex)
            {
                isUploaded = false;
            }

            return isUploaded;
        }

        private bool IsExists(string sPath)
        {

            System.Net.WebRequest oRequest = null;
            try
            {
                oRequest = System.Net.WebRequest.Create(sPath);
                return true;
            }
            catch //(WebException generatedExceptionName)
            {
                return false;
            }
            finally
            {
                oRequest = null;
            }
        }

        //'get folder contains for Templates
        //'Public Shared
        private void UpdateListItemCreateFolder(string webpath, string folderName, string BatchNm)
        {
            gloLists.Lists listProxy = new gloLists.Lists();
            try
            {
                string xmlconst = "<Batch OnError='Continue'><Method ID='1' Cmd='New'><Field Name='ID'>New</Field><Field Name='FSObjType'>1</Field><Field Name='BaseName'>!@foldername</Field></Method></Batch>";

                listProxy.Url = webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    listProxy.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120730
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        listProxy.CookieContainer = new CookieContainer();

                        if (clsGeneral.oFormCookie == null)
                            listProxy.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            listProxy.CookieContainer.Add(clsGeneral.oFormCookie);

                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        listProxy.CookieContainer = clsGeneral.oCookie;
                    }
                }
                XmlDocument doc = new XmlDocument();
                string xmlFolder = xmlconst.Replace("!@foldername", folderName);
                doc.LoadXml(xmlFolder);

                XmlNode batchNode = doc.SelectSingleNode("/Batch");

                XmlNode resultNode = listProxy.UpdateListItems(BatchNm, batchNode);
            }
            catch //(Exception ex1)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex1.ToString());
            }
            finally
            {
                listProxy = null;
            }
        }

        //'get folder contains for Association
        public static void UpdateListItemCreateFolderForXml(string webpath, string BatchNm, string folderName)
        {
            gloLists.Lists listProxy = new gloLists.Lists();
            try
            {
                string xmlconst = "<Batch OnError='Continue'><Method ID='1' Cmd='New'><Field Name='ID'>New</Field><Field Name='FSObjType'>1</Field><Field Name='BaseName'>!@foldername</Field></Method></Batch>";
                listProxy.Url = webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    listProxy.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120730
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        listProxy.CookieContainer = new CookieContainer();

                        if (clsGeneral.oFormCookie == null)
                            listProxy.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            listProxy.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        listProxy.CookieContainer = clsGeneral.oCookie;
                    }
                }

                XmlDocument doc = new XmlDocument();
                string xmlFolder = xmlconst.Replace("!@foldername", folderName);
                doc.LoadXml(xmlFolder);
                XmlNode batchNode = doc.SelectSingleNode("/Batch");

                XmlNode resultNode = listProxy.UpdateListItems(BatchNm, batchNode);

            }
            catch //(System.Web.Services.Protocols.SoapException ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Detail.InnerText);
            }
            //catch (Exception ex1)
            //{
            //    //commented by kanchan on 20120105
            //    //MessageBox.Show(ex1.StackTrace.ToString());
            //}
            finally
            {
                if (listProxy != null)
                {
                    listProxy.Dispose();
                    listProxy = null;
                }
            }
        }

        public DataTable GetList(string listName, string temppath)
        {
            XmlNode MyNode = GetItems(temppath, listName);
            DataTable _dt = new DataTable();
            XmlDocument _wdoc = new XmlDocument();
            Boolean process = false;
            string tstx = String.Empty;
            try
            {
                if (MyNode == null)
                {
                    return _dt;
                }

                _wdoc.LoadXml(MyNode.InnerXml);

                foreach (XmlNode ChldNode in _wdoc.DocumentElement.ChildNodes)
                {
                    XmlDocument _wdoc1 = new XmlDocument();
                    _wdoc1.LoadXml(ChldNode.OuterXml);
                    int attrcnt = _wdoc1.DocumentElement.Attributes.Count;
                    XmlAttributeCollection coll = _wdoc1.DocumentElement.Attributes;

                    if (_dt.Columns.Count == 0)
                    {
                        foreach (XmlAttribute attr in coll)
                        {
                            switch (attr.Name)
                            {
                                case "ows_Title":
                                case "ows_ID":
                                case "ows_ContentType":
                                    //case "ows_EncodedAbsUrl":
                                    try
                                    {
                                        _dt.Columns.Add(attr.Name.Substring(4, (attr.Name.Length - 4)));
                                    }
                                    catch //(Exception ex)
                                    {
                                    }

                                    break; // TODO: might not be correct. Was : Exit Select

                                    //break;
                                case "ows_LinkFilename":
                                    try
                                    {
                                        _dt.Columns.Add("Title");
                                    }
                                    catch //(Exception ex)
                                    {
                                    }
                                    break; // TODO: might not be correct. Was : Exit Select

//                                    break;
                            }
                        }
                    }

                    DataRow dr = _dt.NewRow();


                    foreach (XmlAttribute attr in coll)
                    {
                        tstx = attr.Name;
                        switch (tstx)
                        {
                            case "ows_Title":
                            case "ows_LinkFilename":
                                dr["Title"] = attr.Value;
                                process = true;
                                break; // TODO: might not be correct. Was : Exit Select


                                //break;
                            case "ows_ID":
                                dr["ID"] = Convert.ToInt32(attr.Value);
                                process = true;
                                break; // TODO: might not be correct. Was : Exit Select


                               // break;
                            case "ows_ContentType":
                                dr["ContentType"] = attr.Value;
                                process = true;
                                break; // TODO: might not be correct. Was : Exit Select

//                                break;
                            default:

                                break; // TODO: might not be correct. Was : Exit Select

       //                         break;
                        }
                    }
                    if (process)
                    {
                        _dt.Rows.Add(dr);
                        _dt.AcceptChanges();
                        process = false;
                    }
                }
                return (_dt);
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
                _dt = null;
            }
            return _dt;
        }

        private System.Xml.XmlNode GetItems(string webPath, string listName)
        {
            System.Xml.XmlNode items = null;
            gloLists.Lists listsWS = new gloLists.Lists();
            try
            {
                listsWS.Url = webPath + clsGeneral.gstr_Layouts + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;

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
                    //End
                }
                ////listsWS.Credentials = new System.Net.NetworkCredential("Administrator", "glodom2009", "glodom");

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();

                doc.LoadXml("<Document><Query /><ViewFields /><QueryOptions /></Document>");

                System.Xml.XmlNode listQuery = doc.SelectSingleNode("//Query");

                System.Xml.XmlNode listViewFields = doc.SelectSingleNode("//ViewFields");

                System.Xml.XmlNode listQueryOptions = doc.SelectSingleNode("//QueryOptions");

                Guid g = GetWebID1(webPath);

                items = listsWS.GetListItems(listName, string.Empty, listQuery, listViewFields, string.Empty, listQueryOptions, g.ToString());

            }
            catch //(Exception ex)
            {
            }
            finally
            {
                if (listsWS != null)
                {
                    listsWS.Dispose();
                    listsWS = null;
                }

            }
            return items;
        }

        private Guid GetWebID1(string webPath)
        {
            gloSiteData.SiteData siteDataWS = new gloSiteData.SiteData();
            Guid g;
            try
            {
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    siteDataWS.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120801
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        siteDataWS.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            siteDataWS.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            siteDataWS.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        siteDataWS.CookieContainer = clsGeneral.oCookie;
                    }
                }

                gloSiteData._sWebMetadata webMetaData = default(gloSiteData._sWebMetadata);

                gloSiteData._sWebWithTime[] arrWebWithTime = null;

                gloSiteData._sListWithTime[] arrListWithTime = null;

                gloSiteData._sFPUrl[] arrUrls = null;

                string roles = null;
                string[] roleUsers = null;
                string[] roleGroups = null;

                siteDataWS.Url = webPath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrSiteDataSvc;

                uint i = siteDataWS.GetWeb(out webMetaData, out arrWebWithTime, out arrListWithTime, out arrUrls, out roles, out roleUsers, out roleGroups);

                g = new Guid(webMetaData.WebID);
                return g;

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
                g = Guid.Empty;
            }
            finally
            {
                siteDataWS.Dispose();
            }
            return g;
        }

        #region "Association"

        public bool DownloadXML(string temppath)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            string strDestinationPath = gloSettings.FolderSettings.AppTempFolderPath;
            string filename = temppath.Substring(temppath.LastIndexOf("/") + 1);
            try
            {
                request = (HttpWebRequest)WebRequest.Create(temppath);

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

                //Write to disk
                FileStream fs = new FileStream(strDestinationPath + "\\" + filename, FileMode.Create);

                byte[] read = new byte[256];
                int count = s.Read(read, 0, read.Length);
                while (count > 0)
                {
                    fs.Write(read, 0, count);
                    count = s.Read(read, 0, read.Length);
                }

                //Close everything
                fs.Close();
                s.Close();
                response.Close();
                return true;
            }
            catch (System.Net.WebException)
            {
                if (response != null)
                    response.Close();
                return false;
            }
        }

        public bool CompareXML(string filepathLocal, string filepathSP, string TableName)
        {

            DataSet dsLocal = new DataSet();
            FileStream fsLocal = null;
            DataSet dsSP = new DataSet();
            FileStream fsSP = null;
            //bool bisPresent = false;
            bool blnAssociationUserResult = false;
            //bool blnIsOverwrite = false;
            try
            {
                //Read Local(gloEMR) Xml
                fsLocal = new FileStream(filepathLocal, FileMode.Open);
                dsLocal.ReadXml(fsLocal);

                //Ream SharePoint Xml
                fsSP = new FileStream(filepathSP, FileMode.Open);
                dsSP.ReadXml(fsSP);

                fsLocal.Close();
                fsSP.Close();

                DataTable dtLocal = dsLocal.Tables[TableName];
                DataTable dtSP = dsSP.Tables[TableName];

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepathSP);

                XmlDocument xmlDocumentLocal = new XmlDocument();
                xmlDocumentLocal.Load(filepathLocal);

                string _Code = "";
                string _Association = "";
                if (TableName == "ICD9")
                {
                    _Code = "ICD9Code";
                    _Association = "ICD9Association";
                }
                else if (TableName == "CPT")
                {
                    _Code = "CPTCode";
                    _Association = "CPTAssociation";
                }
                else
                {
                    _Code = "OrderCode";
                    _Association = "OrderAssociation";
                }

                if ((dtLocal != null))
                {
                    foreach (DataRow drLocal in dtLocal.Rows)
                    {
                        foreach (DataRow drSP in dtSP.Rows)
                        {
                            if (drLocal[_Code].ToString().Trim() == drSP[_Code].ToString().Trim())
                            {
                                int _Result = Convert.ToInt32(MessageBox.Show('"' + drSP["Text"].ToString().Trim() + '"' + " Already exists on the site? Do you Want to Overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                                if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite ICD9.
                                {
                                    //_Result is Yes - delete the node from Server Xml
                                    blnAssociationUserResult = true;
                                    //blnIsOverwrite = true;//Association is change,upload the xml file. 
                                    XmlNode node = null;
                                    node = xmlDocument.SelectSingleNode("/" + _Association + "/" + TableName + "[@" + _Code + "='" + drSP[_Code].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node);
                                        xmlDocument.Save(filepathSP);
                                    }
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Cancel upload.
                                {
                                    blnAssociationUserResult = false;
                                    return blnAssociationUserResult;
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.No))//No - Current ICD9 should not overwrite.
                                {
                                    //_Result is No - delete the node from Local Xml
                                    XmlNode node = null;
                                    node = xmlDocumentLocal.SelectSingleNode("/" + _Association + "/" + TableName + "[@" + _Code + "='" + drSP[_Code].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node); ;
                                        xmlDocumentLocal.Save(filepathLocal);
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                //At the end Unique ICD9 in both Xml(server & local),then copy.
                CopyNode(filepathLocal, filepathSP, _Association);

                if (blnAssociationUserResult == false)
                {
                    //if blnAssociationUserResult flag is false then check local Xml(filepathLocal) is there new Association i.e. not available on gloCommunity.
                    if (xmlDocumentLocal.DocumentElement.ChildNodes.Count > 0)
                        blnAssociationUserResult = true;
                }

                //if (blnIsOverwrite == true)
                //    return blnIsOverwrite;
                //else
                return blnAssociationUserResult;
            }
            catch //(Exception ex)
            {
                blnAssociationUserResult = false;
                //blnIsOverwrite = false;
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                dsLocal.Dispose();
                dsLocal = null;

                dsSP.Dispose();
                dsSP = null;

                fsLocal.Dispose();
                fsLocal = null;

                fsSP.Dispose();
                fsSP = null;
            }
            return blnAssociationUserResult;
        }

        private bool CopyNode(string filepathLocal, string filepathSP, string Association)
        {
            bool blnCopynode = false;
            try
            {
                blnCopynode = true;
                //Open the reader with the source XML file
                XmlTextReader myReader = new XmlTextReader(filepathLocal);
                //Load the source of the XML file into an XmlDocument
                XmlDocument mySourceDoc = new XmlDocument();
                //Load the source XML file into the first document
                mySourceDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Open the reader with the destination XML file
                myReader = new XmlTextReader(filepathSP);
                //Load the source of the XML file into an XmlDocument
                XmlDocument myDestDoc = new XmlDocument();
                //Load the destination XML file into the first document
                myDestDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Store the root node of the destination document into an XmlNode
                //The 1 in ChildNodes[1] is the index of the node to be copied (where 0 is the first node)
                XmlNode rootDest = myDestDoc[Association];
                //Store the node to be copied into an XmlNode
                for (int i = 0; i <= mySourceDoc[Association].ChildNodes.Count - 1; i++)
                {
                    XmlNode nodeOrig = mySourceDoc[Association].ChildNodes[i];
                    // Store the copy of the original node into an XmlNode
                    XmlNode nodeDest = myDestDoc.ImportNode(nodeOrig, true);
                    //Append the node being copied to the root of the destination document
                    rootDest.AppendChild(nodeDest);
                }
                //Open the writer
                XmlTextWriter myWriter = new XmlTextWriter(filepathSP, Encoding.UTF8);
                //Indented for easy reading
                myWriter.Formatting = Formatting.Indented;
                //Write the file
                myDestDoc.WriteTo(myWriter);
                //Close the writer
                myWriter.Close();
            }
            catch //(Exception ex)
            {
                blnCopynode = false;
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            return blnCopynode;
        }

        public bool UploadXMLFileToSP(string webpath, string FileNM, string MainPath, string webSite, string _From, string FileNmLocal, string FolderNM, string XmlLocal, string XmlSRV, string XmlFolderNm = "")
        {
            bool isUploaded = true;
            try
            {
                string FNM = FileNM.Substring((FileNM.LastIndexOf('\\') + 1), (FileNM.Length - (FileNM.LastIndexOf('\\') + 1)));
                FNM = FNM.Replace(XmlLocal, XmlSRV);
                string webUrl = "";
                string Path = "";
                string _RootFolder = "";
                string _SpecialtyFolder = "";
                if (_From == "Global")
                {
                    webUrl = webpath + "/" + clsGeneral.WebFolder + "/" + XmlFolderNm + "/" + FNM;
                    Path = MainPath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + XmlFolderNm;
                    _RootFolder = clsGeneral.WebGlobalXmlFolder;
                    _SpecialtyFolder = clsGeneral.WebFolder + "/" + XmlFolderNm;
                }
                else
                {
                    webUrl = webpath + "/" + XmlFolderNm + "/" + FNM;
                    Path = webpath + "/" + clsGeneral.gstrClinicName + "/" + XmlFolderNm;
                    MainPath = MainPath + clsGeneral.gstrClinicName;
                    _RootFolder = clsGeneral.ClinicXmlFolder;
                    _SpecialtyFolder = XmlFolderNm;
                }

                if (_From == "Global")
                {
                    if (IsExists(Path) == true)
                    {
                        UpdateListItemCreateFolderForXml(MainPath, _RootFolder, clsGeneral.WebFolder);
                    }
                }
                if (IsExists(Path) == true)
                {
                    UpdateListItemCreateFolderForXml(MainPath, _RootFolder, _SpecialtyFolder);
                }

                WebRequest request = WebRequest.Create(webUrl);

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

                request.Method = "PUT";

                byte[] fileBuffer = new byte[5024];

                using (Stream stream = request.GetRequestStream())
                {
                    using (FileStream fs = File.Open(FileNM, FileMode.Open, FileAccess.Read))
                    {
                        int startBuffer = fs.Read(fileBuffer, 0, fileBuffer.Length);
                        int i = startBuffer;
                        while (i > 0)
                        {
                            stream.Write(fileBuffer, 0, i);
                            i = fs.Read(fileBuffer, 0, fileBuffer.Length);
                        }
                    }
                }

                WebResponse response = request.GetResponse();
                response.Close();
                if (File.Exists(FileNM))
                    File.Delete(FileNM);//'Delete temp(SP) file after upload
                if (File.Exists(FileNmLocal))
                    File.Delete(FileNmLocal);//'Delete temp(Local) file after upload
                request = null;
            }
            catch //(WebException ex)
            {
                isUploaded = false;
            }

            return isUploaded;
        }

        public void UploadTemplates(bool IsXmlUploaded, string IsFrom, ArrayList arrLocalCatFileNm, string DisplayMsg = "Association")
        {
            //getting SharePoint Templates list that are available on SharePoint
            //GetSPTemplatesList();

            string Sitepath = "";
            string wfolder = "";
            string ClinicGblFolder = "";

            try
            {
                if (IsFrom == "User")
                {
                    Sitepath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName;
                    wfolder = clsGeneral.ClinicWebFolder;

                    if (DisplayMsg == "Association")
                    {
                        GetTemplateList(clsGeneral.ClinicWebFolder + "/Referral Letter", Sitepath, clsGeneral.ClinicRepository, IsFrom);
                        GetTemplateList(clsGeneral.ClinicWebFolder + "/Tags", Sitepath, clsGeneral.ClinicRepository, IsFrom);
                        GetTemplateList(clsGeneral.ClinicWebFolder + "/Patient Education", Sitepath, clsGeneral.ClinicRepository, IsFrom);
                    }
                    else //DmSetup
                    {
                        GetTemplateList(clsGeneral.ClinicWebFolder + "/Referral Letter", Sitepath, clsGeneral.ClinicRepository, IsFrom);
                        GetTemplateList(clsGeneral.ClinicWebFolder + "/Guidelines", Sitepath, clsGeneral.ClinicRepository, IsFrom);
                    }
                }
                else
                {
                    Sitepath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "";
                    wfolder = clsGeneral.GlobalRepository;


                    //GetTemplateList("/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Referral Letter", Sitepath, wfolder, IsFrom);
                    //GetTemplateList("/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Tags", Sitepath, wfolder, IsFrom);
                    //GetTemplateList("/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Patient Education", Sitepath, wfolder, IsFrom);
                    if (DisplayMsg == "Association")
                    {
                        GetTemplateList("/" + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Referral Letter", Sitepath, wfolder, IsFrom);
                        GetTemplateList("/" + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Tags", Sitepath, wfolder, IsFrom);
                        GetTemplateList("/" + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Patient Education", Sitepath, wfolder, IsFrom);
                    }
                    else //DmSetup
                    {
                        GetTemplateList("/" + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Referral Letter", Sitepath, wfolder, IsFrom);
                        GetTemplateList("/" + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/Guidelines", Sitepath, wfolder, IsFrom);
                    }
                }



                bool isUploaded = true;
                int SPTempCnt = 0;
                int GlobalTempCnt = 0;
                int LocalTempCnt = 0;

                if (IsFrom == "User")
                {
                    //Filter Templates that are not available on SharePoint
                    for (SPTempCnt = 0; SPTempCnt <= arrSPCatFileNm.Count - 1; SPTempCnt++)
                    {
                        for (LocalTempCnt = 0; LocalTempCnt <= arrLocalCatFileNm.Count - 1; LocalTempCnt++)
                        {
                            string _CompareStr = arrLocalCatFileNm[LocalTempCnt].ToString();
                            _CompareStr = _CompareStr.Substring(0, _CompareStr.LastIndexOf(" ≈ "));
                            if (arrSPCatFileNm[SPTempCnt].ToString().Trim() == _CompareStr.Trim())
                            {
                                arrLocalCatFileNm.RemoveAt(LocalTempCnt);
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }

                }
                else
                {
                    //Filter Templates that are not available on SharePoint(Global Repository)
                    for (GlobalTempCnt = 0; GlobalTempCnt <= arrGlobalCatFileNm.Count - 1; GlobalTempCnt++)
                    {
                        for (LocalTempCnt = 0; LocalTempCnt <= arrLocalCatFileNm.Count - 1; LocalTempCnt++)
                        {
                            string _CompareStr = arrLocalCatFileNm[LocalTempCnt].ToString();
                            _CompareStr = _CompareStr.Substring(0, _CompareStr.LastIndexOf(" ≈ "));
                            if (arrGlobalCatFileNm[GlobalTempCnt].ToString().Trim() == _CompareStr.Trim())
                            {
                                arrLocalCatFileNm.RemoveAt(LocalTempCnt);
                                break; // TODO: might not be correct. Was : Exit For
                            }
                        }
                    }

                }

                //List of Unavailable Templates to Upload SharePoint
                for (LocalTempCnt = 0; LocalTempCnt <= arrLocalCatFileNm.Count - 1; LocalTempCnt++)
                {
                    string FileNm = "";
                    string[] _TempDtls = arrLocalCatFileNm[LocalTempCnt].ToString().Split('≈');
                    FileNm = clsGeneral.getTemplateName(_TempDtls[2].ToString().Trim(), _TempDtls[1].ToString().Trim());

                    if (FileNm == null | string.IsNullOrEmpty(FileNm) | FileNm == string.Empty)
                    {
                        //If File name is empty OR can't get file from database(DBNull Value),in that case only Association Uploaded.
                        //if (IsXmlUploaded == true)
                        //    MessageBox.Show("Association Uploaded Successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }
                    string FolderNM = "";
                    string webpath = "";
                    dynamic WebFolderTemp = "";
                    string Wpath = "";
                    string MainPath = "";
                    string siteName = "";
                    string strTempNm = "";
                    if (IsFrom == "User")
                    {
                        FolderNM = wfolder + "/" + _TempDtls[0].Trim();
                        webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName;
                        WebFolderTemp = clsGeneral.ClinicRepository;
                        Wpath = webpath + "/" + WebFolderTemp;
                        MainPath = clsGeneral.gstrSharepointSrvNm + "/";
                        //& gstrSharepointSiteNm & "/"
                        siteName = clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName;
                        strTempNm = wfolder;
                        ClinicGblFolder = clsGeneral.ClinicWebFolder;
                    }
                    else
                    {
                        //FolderNM = clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + _TempDtls[0].Trim();
                        FolderNM = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + _TempDtls[0].Trim();
                        webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                        WebFolderTemp = clsGeneral.GlobalRepository;
                        Wpath = webpath + "/" + WebFolderTemp;
                        MainPath = clsGeneral.gstrSharepointSrvNm + "/";
                        //gstrSharepointSiteNm & "/"
                        siteName = clsGeneral.gstrSharepointSiteNm;
                        //ClinicGblFolder = clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder;
                        //Masters/Templates"
                        ClinicGblFolder = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder;
                    }

                    if (UploadFileToDocumentLibrary(Wpath, FolderNM, FileNm, MainPath, siteName, WebFolderTemp, ClinicGblFolder) != true)
                    {
                        isUploaded = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                if (arrLocalCatFileNm.Count != 0)
                {
                    if (isUploaded == true & IsXmlUploaded == true)
                    {
                        MessageBox.Show(DisplayMsg + " & Template(s) Uploaded Successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (isUploaded == false)
                    {
                        MessageBox.Show("Failed to Upload the Template(s)", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (IsXmlUploaded == false)
                    {
                        MessageBox.Show("Failed to Upload the " + DisplayMsg, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (IsXmlUploaded == true)
                {
                    MessageBox.Show(DisplayMsg + " Uploaded Successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to Upload the " + DisplayMsg, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                arrGlobalCatFileNm.Clear();
                arrLocalCatFileNm.Clear();
                arrSPCatFileNm.Clear();
            }

        }

        private void GetTemplateList(string Category, string siteUrl, string documentLibraryName, string IsFrom, string Wfolder = "")
        {
            string Categ = Category;

            gloLists.Lists wsList = new gloLists.Lists();

            try
            {

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    wsList.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120730
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        wsList.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            wsList.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            wsList.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        wsList.CookieContainer = clsGeneral.oCookie;
                    }
                }
                wsList.Url = siteUrl + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                //get a list of all top level lists

                XmlNode allLists = wsList.GetListCollection();

                //load into an XML document so we can use XPath to query content

                XmlDocument allListsDoc = new XmlDocument();

                allListsDoc.LoadXml(allLists.OuterXml);

                ////allListsDoc.Save(@"c:\allListsDoc.xml"); // for debug

                XmlNamespaceManager ns = new XmlNamespaceManager(allListsDoc.NameTable);

                ns.AddNamespace("d", allLists.NamespaceURI);
                //now get the GUID of the document library we are looking for

                XmlNode dlNode = allListsDoc.SelectSingleNode("/d:Lists/d:List[@Title='" + documentLibraryName + "']", ns);

                if (dlNode == null)
                {
                }
                else
                {
                    //obtain the GUID for the document library and the webID

                    string documentLibraryGUID = dlNode.Attributes["ID"].Value;

                    string webId = dlNode.Attributes["WebId"].Value;
                    //create ViewFields CAML

                    XmlDocument viewFieldsDoc = new XmlDocument();

                    XmlNode ViewFields = AddXmlElement(viewFieldsDoc, "ViewFields", "");

                    AddFieldRef(ViewFields, "GUID");

                    AddFieldRef(ViewFields, "ContentType");

                    AddFieldRef(ViewFields, "BaseName");

                    AddFieldRef(ViewFields, "Modified");

                    AddFieldRef(ViewFields, "EncodedAbsUrl");

                    //create QueryOptions CAML
                    XmlDocument queryOptionsDoc = new XmlDocument();

                    XmlNode QueryOptions = AddXmlElement(queryOptionsDoc, "QueryOptions", "");

                    if (IsFrom == "User")
                    {
                        AddXmlElement(QueryOptions, "Folder", documentLibraryName + "/" + Categ);
                    }
                    else
                    {
                        AddXmlElement(QueryOptions, "Folder", documentLibraryName + Categ);
                    }

                    AddXmlElement(QueryOptions, "IncludeMandatoryColumns", "False");

                    //this element is the key to getting the full recusive list

                    XmlNode node = AddXmlElement(QueryOptions, "ViewAttributes", "");

                    AddXmlAttribute(node, "Scope", "Recursive");
                    ////

                    //obtain the list of items in the document library

                    XmlNode listContent = wsList.GetListItems(documentLibraryGUID, null, null, ViewFields, null, QueryOptions, webId);

                    XmlDocument xmlResultsDoc = new XmlDocument();

                    xmlResultsDoc.LoadXml(listContent.OuterXml);

                    ns = new XmlNamespaceManager(xmlResultsDoc.NameTable);

                    ns.AddNamespace("z", "#RowsetSchema");

                    XmlNodeList rows = xmlResultsDoc.SelectNodes("//z:row", ns);

                    foreach (XmlNode row in rows)
                    {
                        if (row.Attributes["ows_ContentType"].Value == "Document")
                        {
                            if (IsFrom == "User")
                            {
                                arrSPCatFileNm.Add((Category.Substring(Category.LastIndexOf("/") + 1) + " ≈ ") + row.Attributes["ows_BaseName"].Value);
                            }
                            else
                            {
                                arrGlobalCatFileNm.Add((Category.Substring(Category.LastIndexOf("/") + 1) + " ≈ ") + row.Attributes["ows_BaseName"].Value);
                            }
                        }
                    }

                    if (rows.Count == 0)
                    {
                    }
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (wsList != null)
                {
                    wsList.Dispose();
                    wsList = null;
                }

            }
        }

        private void AddFieldRef(XmlNode viewFields, string fieldName)
        {
            XmlNode fieldRef = AddXmlElement(viewFields, "FieldRef", "");

            AddXmlAttribute(fieldRef, "Name", fieldName);

        }

        private XmlNode AddXmlAttribute(XmlNode element, string attrName, string attrValue)
        {

            XmlNode attr = element.Attributes.Append((XmlAttribute)element.OwnerDocument.CreateNode(XmlNodeType.Attribute, attrName, ""));


            if (!string.IsNullOrEmpty(attrValue))
            {
                attr.Value = attrValue;
            }

            return (attr);

        }

        private XmlNode AddXmlElement(XmlDocument parent, string elementName, string elementValue)
        {

            XmlNode element = parent.AppendChild(parent.CreateNode(XmlNodeType.Element, elementName, ""));


            if (!string.IsNullOrEmpty(elementValue))
            {
                element.InnerText = elementValue;
            }

            return (element);

        }

        private XmlNode AddXmlElement(XmlNode parent, string elementName, string elementValue)
        {

            XmlNode element = parent.AppendChild(parent.OwnerDocument.CreateNode(XmlNodeType.Element, elementName, ""));


            if (!string.IsNullOrEmpty(elementValue))
            {
                element.InnerText = elementValue;
            }

            return (element);

        }

        #endregion

        #region "Liquid Data"






        public bool CompareLiquidDataXML(string filepathLocal, string filepathSP)
        {
            //  DataBaseLayer oDB = new DataBaseLayer();
            DataSet dsLocal = new DataSet();
            FileStream fsLocal = null;
            DataSet dsSP = new DataSet();
            FileStream fsSP = null;
          //  DataTable dtMain = null;
            DataBaseLayer oDB = new DataBaseLayer();
            bool blnAssociationUserResult = true;
           // string sval = "";

            try
            {
                //Read Local gloEMR Liquid Data Xml
                fsLocal = new FileStream(filepathLocal, FileMode.Open);
                dsLocal.ReadXml(fsLocal);

                //Read SharePoint gloEMR Liquid Data Xml
                fsSP = new FileStream(filepathSP, FileMode.Open);
                dsSP.ReadXml(fsSP);

                fsLocal.Close();
                fsSP.Close();

                DataTable dtLocal = dsLocal.Tables["Table"];
                DataTable dtSP = dsSP.Tables["Table"];

                ArrayList ArrGrpName = new ArrayList(); //It is Array for Getting only Group Name
                ArrGrpName.Clear();
                DataRow[] drelename = dtLocal.Select("nGroupID = '0'");
                for (int len = 0; len < drelename.Length; len++)
                {
                    ArrGrpName.Add(drelename[len]["sElementName"].ToString());
                }

                int newno = 1;  //It is use for checking whether with Specific Name Element Exist in Site if Exist then Increment by 1 

                for (int len = 0; len < ArrGrpName.Count; len++)
                {
                    DataRow[] drsp = null;
                    string strelename = "";
                    newno = 1;
                    drsp = dtSP.Select("sElementName Like '" + ArrGrpName[len].ToString() + "%' AND nGroupID = '0'");  //checking whether 

                    if (drsp.Length == 0)
                    {
                        drsp = dtSP.Select("sElementName Like '" + ArrGrpName[len].ToString().Replace("˜", "≈") + "%' AND nGroupID = '0'");
                        strelename = ArrGrpName[len].ToString().Replace("˜", "≈");
                    }
                    if (drsp.Length > 0)
                    {
                        string sSQL = "";
                        strelename = ArrGrpName[len].ToString().Replace("˜", "≈") + "≈";
                        sSQL = "(selementname like  '" + strelename + "%' and ngroupid=0) or (selementname like  '" + strelename.ToString().Replace("≈", "˜") + "%' and ngroupid=0)";


                        DataRow[] dr = dtSP.Select(sSQL);
                        if (dr.Length > 0)
                        {
                            ArrayList arrelename = new ArrayList();
                            int val = 1;

                            for (int lendr = 0; lendr < dr.Length; lendr++)
                            {
                                arrelename.Add(dr[lendr]["selementname"].ToString().Replace("˜", "≈"));

                                string[] splstr = dr[lendr]["selementname"].ToString().Replace("˜", "≈").Split('≈');
                                try
                                {
                                    if (val <= Convert.ToInt32(splstr[splstr.Length - 1]))
                                    {
                                        val = Convert.ToInt32(splstr[splstr.Length - 1]) + 1;
                                    }

                                }
                                catch
                                {

                                }

                            }
                            newno = val;

                        }

                        DataRow[] drlocal = dtLocal.Select("sElementName Like '" + ArrGrpName[len].ToString() + "%' AND nGroupID = '0'");
                        if (drlocal.Length > 0)
                        {
                            int indofrow = dtLocal.Rows.IndexOf(drlocal[0]);
                            if ((dr.Length > 0) || (drsp.Length > 0))//if specific element Exist in glocommunity Data
                                dtLocal.Rows[indofrow]["sElementName"] = ArrGrpName[len].ToString().Replace("˜", "≈") + "≈" + newno.ToString();
                            else //if specific element Not Exist in glocommunity Data
                                dtLocal.Rows[indofrow]["sElementName"] = ArrGrpName[len].ToString().Replace("˜", "≈");

                            long PrevEid = 0;
                            long eID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(0)", false));
                            PrevEid = Convert.ToInt64(dtLocal.Rows[indofrow]["nelementid"]);
                            dtLocal.Rows[indofrow]["nelementid"] = eID;

                            foreach (DataRow drc in dtLocal.Select("nGroupID=" + PrevEid + ""))
                            {
                                int childrowno = dtLocal.Rows.IndexOf(drc);
                                long subID = 0;
                                if (subID == 0)
                                    subID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(" + eID + ")", false));
                                else
                                    subID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(" + subID + ")", false));

                                dtLocal.Rows[childrowno]["nElementID"] = subID;
                                dtLocal.Rows[childrowno]["nGroupID"] = eID;
                            }
                            dtLocal.AcceptChanges();
                        }

                    }
                }
                dtLocal.WriteXml(filepathLocal);
                CopyLiquidDataNode(filepathLocal, filepathSP);

                return blnAssociationUserResult;
            }


            catch //(Exception ex)
            {
                blnAssociationUserResult = false;
            }
            finally
            {
                dsLocal = null;
                dsSP = null;
               // dtMain = null;
            }
            return blnAssociationUserResult;
        }






        //commented CompareLiquidDataXML function because of not proper logic on 20-jan-2012

        //public bool CompareLiquidDataXML(string filepathLocal, string filepathSP)
        //{
        //    DataBaseLayer oDB = new DataBaseLayer();
        //    DataSet dsLocal = new DataSet();
        //    FileStream fsLocal = null;
        //    DataSet dsSP = new DataSet();
        //    FileStream fsSP = null;
        //    DataTable dtMain = null;
        //    bool blnAssociationUserResult = true;
        //    string sval = "";

        //    try
        //    {
        //        //Read Local gloEMR Liquid Data Xml
        //        fsLocal = new FileStream(filepathLocal, FileMode.Open);
        //        dsLocal.ReadXml(fsLocal);

        //        //Read SharePoint gloEMR Liquid Data Xml
        //        fsSP = new FileStream(filepathSP, FileMode.Open);
        //        dsSP.ReadXml(fsSP);

        //        fsLocal.Close();
        //        fsSP.Close();

        //        DataTable dtLocal = dsLocal.Tables["Table"];
        //        DataTable dtSP = dsSP.Tables["Table"];



        //        XmlDocument xmlDocumentLocal = new XmlDocument();
        //        xmlDocumentLocal.Load(filepathLocal);

        //        if ((dtLocal != null))
        //        {
        //            dtMain = new DataTable();
        //            dtMain = dtLocal.Clone();

        //            foreach (DataRow drLocal in dtLocal.Rows)
        //            {
        //                bool _IsCategoryPresent = false;
        //                if (drLocal["sElementName"].ToString().Trim() != string.Empty && Convert.ToInt64(drLocal["nGroupID"]) == 0)
        //                {
        //                    string _strElementName = "";

        //                    if (drLocal["sElementName"].ToString().Trim().Contains("˜"))
        //                        _strElementName = drLocal["sElementName"].ToString().Trim().Replace('˜', '≈');
        //                    else
        //                        _strElementName = drLocal["sElementName"].ToString().Trim();

        //                    //Fetch the Same OR Like Liquid Data Category from gloCommunity(from dtSP Datatable)
        //                    DataRow[] drLiquidCategory;//
        //                    if (_strElementName.Trim().Contains("≈"))
        //                    {
        //                        string _sElementName = _strElementName.Trim().Substring(0, _strElementName.Trim().LastIndexOf("≈"));
        //                        drLiquidCategory = dtSP.Select("sElementName Like '" + _sElementName.Replace('"', ' ') + "%' AND nGroupID = '0'");
        //                    }
        //                    else
        //                        drLiquidCategory = dtSP.Select("sElementName = '" + _strElementName.Replace('"', ' ') + "' OR sElementName Like '" + _strElementName.Replace('"', ' ') + "%' OR sElementName Like '" + _strElementName.Replace('"', ' ') + "≈%' AND nGroupID = '0'");
        //                    //

        //                    //Add the Same OR Like Liquid Data Category into dtMain Datatable
        //                    foreach (DataRow dr in drLiquidCategory)
        //                    {
        //                        if (dtMain.Rows.Count > 0)
        //                        {
        //                            //Check Liquid Data category is already added into Datatable.
        //                            foreach (DataRow drMain in dtMain.Rows)
        //                            {
        //                                if (drMain["nElementID"].ToString().Trim() == dr["nElementID"].ToString().Trim())
        //                                    _IsCategoryPresent = true;
        //                            }
        //                            //
        //                        }

        //                        if (_IsCategoryPresent == false)//Liquid Data category not present in dtMain Datatable then add.
        //                            dtMain.ImportRow(dr);
        //                    }
        //                    //

        //                    long eID = 0;
        //                    long subID = 0;

        //                    //Select Liquid Data Category & its Child from dtLoacal Datatable.
        //                    DataRow[] drLocalData = dtLocal.Select("nGroupID = " + drLocal["nElementID"] + "OR  nElementID = " + drLocal["nElementID"], "nGroupID");
        //                    //

        //                    foreach (DataRow dr in drLocalData)
        //                    {
        //                        if (Convert.ToInt64(dr["nGroupID"]) == 0)
        //                        {
        //                            eID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(0)", false));

        //                            if (dr["sElementName"].ToString().Trim().Contains("˜"))
        //                                _strElementName = dr["sElementName"].ToString().Trim().Replace('˜', '≈');
        //                            else
        //                                _strElementName = dr["sElementName"].ToString().Trim();

        //                            if (_strElementName.Trim().Contains("≈"))
        //                            {
        //                                //get the count of Same OR Like Liquid Data Category
        //                                string _sElementName = _strElementName.Trim().Substring(0, _strElementName.Trim().LastIndexOf("≈"));
        //                                drLiquidCategory = dtMain.Select("sElementName Like '" + _sElementName.Replace('"', ' ') + "%' AND nGroupID = '0'");
        //                                //

        //                                if (drLiquidCategory.Length > 0)
        //                                {
        //                                    sval = "≈" + drLiquidCategory.Length;
        //                                    string schk = _strElementName.Remove(0, _strElementName.IndexOf("≈"));

        //                                    dr["selementname"] = Regex.Replace(_strElementName, schk, sval);
        //                                }
        //                                dr["nElementID"] = eID;
        //                                dtLocal.AcceptChanges();
        //                                dtMain.ImportRow(dr);//Add newly created Liquid Data Category into dtMain Datatable.
        //                            }
        //                            else
        //                            {
        //                                //get the count of Same OR Like Liquid Data Category
        //                                drLiquidCategory = dtMain.Select("sElementName Like '" + _strElementName.Trim().Replace('"', ' ') + "%' AND nGroupID = '0'");
        //                                //

        //                                if (drLiquidCategory.Length > 0)
        //                                {
        //                                    sval = "≈" + drLiquidCategory.Length;
        //                                    dr["sElementName"] = _strElementName.Trim() + sval;
        //                                }
        //                                dr["nElementID"] = eID;
        //                                dtLocal.AcceptChanges();
        //                                dtMain.ImportRow(dr);//Add newly created Liquid Data Category into dtMain Datatable.
        //                            }
        //                        }
        //                        else
        //                        {
        //                            if (eID != 0)
        //                            {
        //                                if (subID == 0)
        //                                    subID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(" + eID + ")", false));
        //                                else
        //                                    subID = Convert.ToInt64(oDB.GetDataValue("select dbo.GetPrefixTransactionID(" + subID + ")", false));

        //                                dr["nElementID"] = subID;
        //                                dr["nGroupID"] = eID;
        //                                dtLocal.AcceptChanges();
        //                            }
        //                        }
        //                    }//End foreach

        //                }//End if drLocal["sElementName"].ToString().Trim() != string.Empty
        //            }

        //            dtLocal.WriteXml(filepathLocal);
        //            CopyLiquidDataNode(filepathLocal, filepathSP);

        //            return blnAssociationUserResult;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        blnAssociationUserResult = false;
        //    }
        //    finally
        //    {
        //        dsLocal = null;
        //        dsSP = null;
        //        dtMain = null;
        //    }
        //    return blnAssociationUserResult;
        //}





        private void CopyLiquidDataNode(string filepathLocal, string filepathSP)
        {
            try
            {
                //Open the reader with the source XML file
                XmlTextReader myReader = new XmlTextReader(filepathLocal);
                //Load the source of the XML file into an XmlDocument
                XmlDocument mySourceDoc = new XmlDocument();
                //Load the source XML file into the first document
                mySourceDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Open the reader with the destination XML file
                myReader = new XmlTextReader(filepathSP);
                //Load the source of the XML file into an XmlDocument
                XmlDocument myDestDoc = new XmlDocument();
                //Load the destination XML file into the first document
                myDestDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Store the root node of the destination document into an XmlNode
                //The 1 in ChildNodes[1] is the index of the node to be copied (where 0 is the first node)
                XmlNode rootDest = myDestDoc["DocumentElement"];
                //Store the node to be copied into an XmlNode
                for (int i = 0; i <= mySourceDoc["DocumentElement"].ChildNodes.Count - 1; i++)
                {
                    XmlNode nodeOrig = mySourceDoc["DocumentElement"].ChildNodes[i];
                    //Store the copy of the original node into an XmlNode
                    XmlNode nodeDest = myDestDoc.ImportNode(nodeOrig, true);
                    //Append the node being copied to the root of the destination document
                    rootDest.AppendChild(nodeDest);
                }
                //Open the writer
                XmlTextWriter myWriter = new XmlTextWriter(filepathSP, Encoding.UTF8);
                //Indented for easy reading
                myWriter.Formatting = Formatting.Indented;
                //Write the file
                myDestDoc.WriteTo(myWriter);
                //Close the writer
                myWriter.Close();
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region "History"
        public bool CompareHistoryXML(string filepathLocal, string filepathSP, string TableName)
        {
            DataSet dsLocal = new DataSet();
            FileStream fsLocal = null;
            DataSet dsSP = new DataSet();
            FileStream fsSP = null;
            bool blnAssociationUserResult = false;

            try
            {
                //Read Local(gloEMR) Xml
                fsLocal = new FileStream(filepathLocal, FileMode.Open);
                dsLocal.ReadXml(fsLocal);

                //Read SharePoint Xml
                fsSP = new FileStream(filepathSP, FileMode.Open);
                dsSP.ReadXml(fsSP);

                fsLocal.Close();
                fsSP.Close();

                DataTable dtLocal = dsLocal.Tables["Category"];
                DataTable dtSP = dsSP.Tables["Category"];

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepathSP);

                XmlDocument xmlDocumentLocal = new XmlDocument();
                xmlDocumentLocal.Load(filepathLocal);

                if ((dtLocal != null))
                {
                    foreach (DataRow drLocal in dtLocal.Rows)
                    {
                        foreach (DataRow drSP in dtSP.Rows)
                        {
                            if (drLocal["CategoryName"].ToString().Trim() == drSP["CategoryName"].ToString().Trim() && drLocal["Description"].ToString().Trim() == drSP["Description"].ToString().Trim())
                            {
                                int _Result = Convert.ToInt32(MessageBox.Show('"' + drSP["Description"].ToString().Trim() + '"' + " Already exists on the site? Do you Want to Overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                                if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite History Item.
                                {
                                    //_Result is Yes - delete the node from Server Xml
                                    blnAssociationUserResult = true;
                                    XmlNode node = null;
                                    node = xmlDocument.SelectSingleNode("/" + "History" + "/" + "Category" + "[@" + "Description" + "='" + drSP["Description"].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node);
                                        xmlDocument.Save(filepathSP);
                                    }
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Cancel upload.
                                {
                                    blnAssociationUserResult = false;
                                    return blnAssociationUserResult;
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.No))//No - Current History Item should not overwrite.
                                {
                                    //_Result is No - delete the node from Local Xml
                                    XmlNode node = null;
                                    node = xmlDocumentLocal.SelectSingleNode("/" + "History" + "/" + "Category" + "[@" + "Description" + "='" + drSP["Description"].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node); ;
                                        xmlDocumentLocal.Save(filepathLocal);
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                //At the end Unique History Item in both Xml(server & local),then copy.
                CopyHistoryNode(filepathLocal, filepathSP);

                if (blnAssociationUserResult == false)
                {
                    //if blnAssociationUserResult flag is false then check local Xml(filepathLocal) is there new History Item i.e. not available on gloCommunity.
                    if (xmlDocumentLocal.DocumentElement.ChildNodes.Count > 0)
                        blnAssociationUserResult = true;
                }
                return blnAssociationUserResult;
            }
            catch //(Exception ex)
            {
                blnAssociationUserResult = false;
                //blnIsOverwrite = false;
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                dsLocal.Dispose();
                dsLocal = null;

                dsSP.Dispose();
                dsSP = null;

                fsLocal.Dispose();
                fsLocal = null;

                fsSP.Dispose();
                fsSP = null;
            }
            return blnAssociationUserResult;
        }

        private void CopyHistoryNode(string filepathLocal, string filepathSP)
        {
            try
            {
                //Open the reader with the source XML file
                XmlTextReader myReader = new XmlTextReader(filepathLocal);
                //Load the source of the XML file into an XmlDocument
                XmlDocument mySourceDoc = new XmlDocument();
                //Load the source XML file into the first document
                mySourceDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Open the reader with the destination XML file
                myReader = new XmlTextReader(filepathSP);
                //Load the source of the XML file into an XmlDocument
                XmlDocument myDestDoc = new XmlDocument();
                //Load the destination XML file into the first document
                myDestDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Store the root node of the destination document into an XmlNode
                //The 1 in ChildNodes[1] is the index of the node to be copied (where 0 is the first node)
                XmlNode rootDest = myDestDoc["History"];
                //Store the node to be copied into an XmlNode
                for (int i = 0; i <= mySourceDoc["History"].ChildNodes.Count - 1; i++)
                {
                    XmlNode nodeOrig = mySourceDoc["History"].ChildNodes[i];
                    //Store the copy of the original node into an XmlNode
                    XmlNode nodeDest = myDestDoc.ImportNode(nodeOrig, true);
                    //Append the node being copied to the root of the destination document
                    rootDest.AppendChild(nodeDest);
                }
                //Open the writer
                XmlTextWriter myWriter = new XmlTextWriter(filepathSP, Encoding.UTF8);
                //Indented for easy reading
                myWriter.Formatting = Formatting.Indented;
                //Write the file
                myDestDoc.WriteTo(myWriter);
                //Close the writer
                myWriter.Close();
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region "DmSetup"
        public bool CompareDmSetupXML(string filepathLocal, string filepathSP, string TableName)
        {

            DataSet dsLocal = new DataSet();
            FileStream fsLocal = null;
            DataSet dsSP = new DataSet();
            FileStream fsSP = null;
            //bool bisPresent = false;
            bool blnAssociationUserResult = false;
            //bool blnIsOverwrite = false;
            try
            {
                //Read Local(gloEMR) Xml
                fsLocal = new FileStream(filepathLocal, FileMode.Open);
                dsLocal.ReadXml(fsLocal);

                //Ream SharePoint Xml
                fsSP = new FileStream(filepathSP, FileMode.Open);
                dsSP.ReadXml(fsSP);

                fsLocal.Close();
                fsSP.Close();

                DataTable dtLocal = dsLocal.Tables[TableName];
                DataTable dtSP = dsSP.Tables[TableName];

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepathSP);

                XmlDocument xmlDocumentLocal = new XmlDocument();
                xmlDocumentLocal.Load(filepathLocal);

                string _Code = "";
                string _Association = "";

                _Code = "PatientCriteriaName";
                _Association = "DmSetup";

                if ((dtLocal != null))
                {
                    foreach (DataRow drLocal in dtLocal.Rows)
                    {
                        foreach (DataRow drSP in dtSP.Rows)
                        {
                            if (drLocal[_Code].ToString().Trim() == drSP[_Code].ToString().Trim())
                            {
                                int _Result = Convert.ToInt32(MessageBox.Show('"' + drSP[_Code].ToString().Trim() + '"' + " Already exists on the site? Do you Want to Overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                                if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite ICD9.
                                {
                                    //_Result is Yes - delete the node from Server Xml
                                    blnAssociationUserResult = true;
                                    //blnIsOverwrite = true;//Association is change,upload the xml file. 
                                    XmlNode node = null;
                                    node = xmlDocument.SelectSingleNode("/" + _Association + "/" + TableName + "[@" + _Code + "='" + drSP[_Code].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node);
                                        xmlDocument.Save(filepathSP);
                                    }
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Cancel upload.
                                {
                                    blnAssociationUserResult = false;
                                    return blnAssociationUserResult;
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.No))//No - Current ICD9 should not overwrite.
                                {
                                    //_Result is No - delete the node from Local Xml
                                    XmlNode node = null;
                                    node = xmlDocumentLocal.SelectSingleNode("/" + _Association + "/" + TableName + "[@" + _Code + "='" + drSP[_Code].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node); ;
                                        xmlDocumentLocal.Save(filepathLocal);
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                //At the end Unique ICD9 in both Xml(server & local),then copy.
                CopyNode(filepathLocal, filepathSP, _Association);

                if (blnAssociationUserResult == false)
                {
                    //if blnAssociationUserResult flag is false then check local Xml(filepathLocal) is there new Association i.e. not available on gloCommunity.
                    if (xmlDocumentLocal.DocumentElement.ChildNodes.Count > 0)
                        blnAssociationUserResult = true;
                }

                //if (blnIsOverwrite == true)
                //    return blnIsOverwrite;
                //else
                return blnAssociationUserResult;
            }
            catch //(Exception ex)
            {
                blnAssociationUserResult = false;
                //blnIsOverwrite = false;
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                dsLocal.Dispose();
                dsLocal = null;

                dsSP.Dispose();
                dsSP = null;

                fsLocal.Dispose();
                fsLocal = null;

                fsSP.Dispose();
                fsSP = null;
            }
            return blnAssociationUserResult;
        }
        #endregion

        #region "CVSetup"
        public bool CompareCVSetupXML(string filepathLocal, string filepathSP, string TableName)
        {

            DataSet dsLocal = new DataSet();
            FileStream fsLocal = null;
            DataSet dsSP = new DataSet();
            FileStream fsSP = null;
            //bool bisPresent = false;
            bool blnAssociationUserResult = false;
            //bool blnIsOverwrite = false;
            try
            {
                //Read Local(gloEMR) Xml
                fsLocal = new FileStream(filepathLocal, FileMode.Open);
                dsLocal.ReadXml(fsLocal);

                //Ream SharePoint Xml
                fsSP = new FileStream(filepathSP, FileMode.Open);
                dsSP.ReadXml(fsSP);

                fsLocal.Close();
                fsSP.Close();

                DataTable dtLocal = dsLocal.Tables[TableName];
                DataTable dtSP = dsSP.Tables[TableName];

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepathSP);

                XmlDocument xmlDocumentLocal = new XmlDocument();
                xmlDocumentLocal.Load(filepathLocal);

                string _Code = "";
                string _Association = "";

                _Code = "CVCriteriaName";
                _Association = "CVSetup";

                if ((dtLocal != null))
                {
                    foreach (DataRow drLocal in dtLocal.Rows)
                    {
                        foreach (DataRow drSP in dtSP.Rows)
                        {
                            if (drLocal[_Code].ToString().Trim() == drSP[_Code].ToString().Trim())
                            {
                                int _Result = Convert.ToInt32(MessageBox.Show('"' + drSP[_Code].ToString().Trim() + '"' + " Already exists on the site? Do you Want to Overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                                if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite ICD9.
                                {
                                    //_Result is Yes - delete the node from Server Xml
                                    blnAssociationUserResult = true;
                                    //blnIsOverwrite = true;//Association is change,upload the xml file. 
                                    XmlNode node = null;
                                    node = xmlDocument.SelectSingleNode("/" + _Association + "/" + TableName + "[@" + _Code + "='" + drSP[_Code].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node);
                                        xmlDocument.Save(filepathSP);
                                    }
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Cancel upload.
                                {
                                    blnAssociationUserResult = false;
                                    return blnAssociationUserResult;
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.No))//No - Current ICD9 should not overwrite.
                                {
                                    //_Result is No - delete the node from Local Xml
                                    XmlNode node = null;
                                    node = xmlDocumentLocal.SelectSingleNode("/" + _Association + "/" + TableName + "[@" + _Code + "='" + drSP[_Code].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node); ;
                                        xmlDocumentLocal.Save(filepathLocal);
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                //At the end Unique ICD9 in both Xml(server & local),then copy.
                CopyNode(filepathLocal, filepathSP, _Association);

                if (blnAssociationUserResult == false)
                {
                    //if blnAssociationUserResult flag is false then check local Xml(filepathLocal) is there new Association i.e. not available on gloCommunity.
                    if (xmlDocumentLocal.DocumentElement.ChildNodes.Count > 0)
                        blnAssociationUserResult = true;
                }

                //if (blnIsOverwrite == true)
                //    return blnIsOverwrite;
                //else
                return blnAssociationUserResult;
            }
            catch //(Exception ex)
            {
                blnAssociationUserResult = false;
                //blnIsOverwrite = false;
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                dsLocal.Dispose();
                dsLocal = null;

                dsSP.Dispose();
                dsSP = null;

                fsLocal.Dispose();
                fsLocal = null;

                fsSP.Dispose();
                fsSP = null;
            }
            return blnAssociationUserResult;
        }
        #endregion

        #region "ImSetup"
        public bool CompareImSetupXML(string filepathLocal, string filepathSP, string TableName)
        {
            DataSet dsLocal = new DataSet();
            FileStream fsLocal = null;
            DataSet dsSP = new DataSet();
            FileStream fsSP = null;
            bool blnAssociationUserResult = false;

            try
            {
                //Read Local(gloEMR) Xml
                fsLocal = new FileStream(filepathLocal, FileMode.Open);
                dsLocal.ReadXml(fsLocal);

                //Read SharePoint Xml
                fsSP = new FileStream(filepathSP, FileMode.Open);
                dsSP.ReadXml(fsSP);

                fsLocal.Close();
                fsSP.Close();

                DataTable dtLocal = dsLocal.Tables["ImmunizationSetup"];
                DataTable dtSP = dsSP.Tables["ImmunizationSetup"];

                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(filepathSP);

                XmlDocument xmlDocumentLocal = new XmlDocument();
                xmlDocumentLocal.Load(filepathLocal);

                if ((dtLocal != null))
                {
                    foreach (DataRow drLocal in dtLocal.Rows)
                    {
                        foreach (DataRow drSP in dtSP.Rows)
                        {
                            if (drLocal["Lot"].ToString().Trim() == drSP["Lot"].ToString().Trim())
                            {
                                int _Result = Convert.ToInt32(MessageBox.Show('"' + drSP["Lot"].ToString().Trim() + '"' + " Lot# already exists on the site? Do you Want to Overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3));
                                if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite Immunization.
                                {
                                    //_Result is Yes - delete the node from Server Xml
                                    blnAssociationUserResult = true;
                                    XmlNode node = null;
                                    node = xmlDocument.SelectSingleNode("/" + "ImSetup" + "/" + "ImmunizationSetup" + "[@" + "Lot" + "='" + drSP["Lot"].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node);
                                        xmlDocument.Save(filepathSP);
                                    }
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Cancel upload.
                                {
                                    blnAssociationUserResult = false;
                                    return blnAssociationUserResult;
                                }
                                else if (_Result == Convert.ToInt32(DialogResult.No))//No - Current History Item should not overwrite.
                                {
                                    //_Result is No - delete the node from Local Xml
                                    XmlNode node = null;
                                    node = xmlDocumentLocal.SelectSingleNode("/" + "ImSetup" + "/" + "ImmunizationSetup" + "[@" + "Lot" + "='" + drSP["Lot"].ToString().Trim() + "']");
                                    if (node != null)
                                    {
                                        node.ParentNode.RemoveChild(node); ;
                                        xmlDocumentLocal.Save(filepathLocal);
                                    }
                                    continue;
                                }
                            }
                        }
                    }
                }
                //At the end Unique History Item in both Xml(server & local),then copy.
                CopyImSetupNode(filepathLocal, filepathSP);

                if (blnAssociationUserResult == false)
                {
                    //if blnAssociationUserResult flag is false then check local Xml(filepathLocal) is there new History Item i.e. not available on gloCommunity.
                    if (xmlDocumentLocal.DocumentElement.ChildNodes.Count > 0)
                        blnAssociationUserResult = true;
                }
                return blnAssociationUserResult;
            }
            catch //(Exception ex)
            {
                blnAssociationUserResult = false;
            }
            finally
            {
                dsLocal.Dispose();
                dsLocal = null;

                dsSP.Dispose();
                dsSP = null;

                fsLocal.Dispose();
                fsLocal = null;

                fsSP.Dispose();
                fsSP = null;
            }
            return blnAssociationUserResult;
        }

        private void CopyImSetupNode(string filepathLocal, string filepathSP)
        {
            try
            {
                //Open the reader with the source XML file
                XmlTextReader myReader = new XmlTextReader(filepathLocal);
                //Load the source of the XML file into an XmlDocument
                XmlDocument mySourceDoc = new XmlDocument();
                //Load the source XML file into the first document
                mySourceDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Open the reader with the destination XML file
                myReader = new XmlTextReader(filepathSP);
                //Load the source of the XML file into an XmlDocument
                XmlDocument myDestDoc = new XmlDocument();
                //Load the destination XML file into the first document
                myDestDoc.Load(myReader);
                //Close the reader
                myReader.Close();

                //Store the root node of the destination document into an XmlNode
                //The 1 in ChildNodes[1] is the index of the node to be copied (where 0 is the first node)
                XmlNode rootDest = myDestDoc["ImSetup"];
                //Store the node to be copied into an XmlNode
                for (int i = 0; i <= mySourceDoc["ImSetup"].ChildNodes.Count - 1; i++)
                {
                    XmlNode nodeOrig = mySourceDoc["ImSetup"].ChildNodes[i];
                    //Store the copy of the original node into an XmlNode
                    XmlNode nodeDest = myDestDoc.ImportNode(nodeOrig, true);
                    //Append the node being copied to the root of the destination document
                    rootDest.AppendChild(nodeDest);
                }
                //Open the writer
                XmlTextWriter myWriter = new XmlTextWriter(filepathSP, Encoding.UTF8);
                //Indented for easy reading
                myWriter.Formatting = Formatting.Indented;
                //Write the file
                myDestDoc.WriteTo(myWriter);
                //Close the writer
                myWriter.Close();
            }
            catch //(Exception ex)
            {
            }
        }
        #endregion
    }
}




