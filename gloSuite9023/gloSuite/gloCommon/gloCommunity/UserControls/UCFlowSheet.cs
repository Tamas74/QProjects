using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Collections;
using System.Net;
using System.IO;
using System.Configuration;
namespace gloCommunity.UserControls
{
    public partial class UCFlowSheet : UserControl
    {
        clsFlowSheet objclsfl = null;
        ArrayList arrfl = new ArrayList();
        public DataTable showflowsheetdata = null;
        public DataTable dtFlowsheet = null;
        string straction = "";
        string WebPath = "";
        TreeNode rootnode = new TreeNode();

        public UCFlowSheet(string _straction)
        {
            InitializeComponent();
            straction = _straction;

            //  btnLocal.Text = clsGeneral.ClinicRepository;
            //  btnCentral.Text = clsGeneral.GlobalRepository;
            // trvflshname.Nodes.Add("Select ALL");   
            rootnode.Text = "Select All";

            trvflshname.Nodes.Add(rootnode);
            rootnode.ExpandAll();
            // trvflconf.SearchBox = false;
            if (_straction == "Upload")
            {
                objclsfl = new clsFlowSheet();
                DataTable dtflow = objclsfl.getFlowsheetName();
                filltrvflowsheet(dtflow);
                pnltls.Visible = false;
            }
            else
            {
                pnlleft.Visible = false;
                dtFlowsheet = new DataTable();
                FillLocalClinicData();
                pnltls.Visible = true;
            }
        }



        private void FillLocalClinicData()
        {
            try
            {
                //   trvappconf.SearchBox = false;

                //pnl_btnLocal.Dock = DockStyle.Top;

                WebPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;

                string fileUrl = WebPath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrflowshflnm + "/" + clsGeneral.gstrflowshflnm + ".xml";

                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch (Exception ex)
            {
                // clsGeneral.UpdateLog("glocomm Error Filling local Clinic Data in  Usercontrol Flowsheet: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }

        }


        private void ShowXMLFIleData(string XmlFileUrl, string UserName, string Password, string Domain)
        {
            this.Cursor = Cursors.WaitCursor;
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
                    //Added for check which authentication is use for access gloCommunity on 20120801
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

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrflowshflnm + ".xml"; //"SmartDxAssociation.xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();
                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrflowshflnm + ".xml");
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.ToString(), "Flowsheet", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //clsGeneral.UpdateLog("glocomm Error While Showing  data from Site  in  Usercontrol Flowsheet: " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    // MessageBox.Show(exp.ToString());
                }
                if (ds.Tables.Count > 0)
                {
                    showflowsheetdata = ds.Tables[0];
                    ArrayList arrflow = new ArrayList();
                    arrflow.Clear();
                    foreach (DataRow dr in showflowsheetdata.Rows)
                    {
                        if (arrflow.Contains(dr["FlowsheetName"].ToString()) == false)
                        {
                            arrflow.Add(dr["FlowsheetName"].ToString());
                            //  trvflshname.Nodes.Add(dr["FlowsheetName"].ToString());
                            rootnode.Nodes.Add(dr["FlowsheetName"].ToString());
                        }
                    }
                }
                // fillDownloadedData(showdata);

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Showing   data from Site in  Usercontrol Flowsheet: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                ds.Dispose();
                this.Cursor = Cursors.Default;
            }

        }



        private void fillDownloadedData(string flowsheetname)
        {


            try
            {

                dtFlowsheet.Rows.Clear();
                dtFlowsheet = showflowsheetdata.Clone();
                DataRow[] drflow = showflowsheetdata.Select("FlowsheetName='" + flowsheetname.Replace("'", "''") + "'");
                for (int lenflow = 0; lenflow < drflow.Length; lenflow++)
                {
                    dtFlowsheet.ImportRow(drflow[lenflow]);
                }

                flxFlowsheet.DataSource = dtFlowsheet;
                try
                {
                    if (flxFlowsheet.DataSource != null)
                    {
                        flxFlowsheet.Cols["bIsBold"].Visible = false;
                        flxFlowsheet.Cols["bIsItalic"].Visible = false;
                        flxFlowsheet.Cols["bIsUnderline"].Visible = false;
                        flxFlowsheet.Cols["bIsBold"].Caption = "Bold";
                        flxFlowsheet.Cols["bIsItalic"].Caption = "Italic";
                        flxFlowsheet.Cols["bIsUnderline"].Caption = "Underline";



                    }
                }
                catch (Exception ex)
                {
                    //clsGeneral.UpdateLog("glocomm Error While Filling  data from flowsheetname in  Usercontrol Flowsheet: " + ex.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }

                // flxFlowsheet.Cols[0].DataType = typeof(bool);
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Filling  data from flowsheetname in  Usercontrol Flowsheet: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {


            }
        }


        private void ClearGridData()
        {
           // flxFlowsheet.Clear();
            flxFlowsheet.DataSource = null;

            if (flxFlowsheet.Rows.Count >= 2)
            {
                flxFlowsheet.Rows.RemoveRange(1, flxFlowsheet.Rows.Count - 1);
            }
        }

        //private void btnCentral_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        trvflshname.Nodes.Clear();

        //        trvflconf.Nodes.Clear();
        //        ClearGridData();
        //        //pnl_btnLocal.Dock = DockStyle.Bottom;
        //      //  pnl_btnCentral.Dock = DockStyle.Top;
        //        GetCentralList();
        //    }
        //    catch
        //    {

        //    }
        //}


        private void GetCentralList()
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
                            string fileUrl = WebPath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrflowshflnm + "/" + clsGeneral.gstrflowshflnm + ".xml";

                            tr.Tag = fileUrl;
                            if (tr.Text.Contains(".aspx") == false)
                                trvflconf.Nodes.Add(tr);
                        }
                    }
                }
            }
        }


        //private void btnLocal_Click(object sender, EventArgs e)
        //{
        //    trvflshname.Nodes.Clear();
        //    trvflconf.Nodes.Clear();
        //    ClearGridData();
        //    //pnl_btnLocal.Dock = DockStyle.Top;
        //   // pnl_btnCentral.Dock = DockStyle.Bottom;
        //    FillLocalClinicData();

        //}

        private void filltrvflowsheet(DataTable dtflow)
        {
            //Added by kanchan on 20120104
            if (dtflow != null)
            {
                foreach (DataRow dr in dtflow.Rows)
                {
                    TreeNode TempNode = new TreeNode();
                    TempNode.Text = dr["FlowSheetName"].ToString();
                    rootnode.Nodes.Add(TempNode);
                    //trvflshname.Nodes.Add(TempNode); 
                    //   trvflshname.Nodes.Add(dr["FlowSheetName"].ToString());
                }
            }
        }








        private void SetGridColumnVisibility(C1.Win.C1FlexGrid.C1FlexGrid Grd, ArrayList ardata)
        {
            if (Grd.DataSource != null)
            {
                Grd.Cols[0].DataType = typeof(bool);
                Grd.Cols[0].Visible = true;
                for (int len = 1; len < Grd.Cols.Count; len++)
                {
                    Grd.Cols[len].AllowEditing = false;
                    if (ardata.Contains(Grd.Cols[len].Caption.ToLower()))

                        Grd.Cols[len].Visible = true;
                    else
                        Grd.Cols[len].Visible = false;

                }
            }

        }



        private void trvflshname_NodeMouseDoubleClick_1(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void trvflconf_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //  ClearGridData();
            //  trvflshname.Nodes.Clear();     
            rootnode.Nodes.Clear();
            if (straction == "Download")
            {
                ShowXMLFIleData(trvflconf.SelectedNode.Tag.ToString(), "", "", "");
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // trvflshname.Nodes.Clear();
                rootnode.Nodes.Clear();
                trvflconf.Nodes.Clear();
                ClearGridData();
                trvflconf.CheckBoxes = false;
                //pnl_btnLocal.Dock = DockStyle.Bottom;
                // pnl_btnCentral.Dock = DockStyle.Top;
                GetCentralList();
                pnlleft.Visible = true;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While getting XML Data for Global data  in  Usercontrol Flowsheet: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                // trvflshname.Nodes.Clear();
                rootnode.Nodes.Clear();
                trvflconf.Nodes.Clear();
                ClearGridData();
                FillLocalClinicData();
                pnlleft.Visible = false;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While getting XML Data for Clinic in  Usercontrol Flowsheet: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;

        }



        private void trvflshname_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node.Level == 1)
            {
                if (straction == "Upload")
                {
                    flxFlowsheet.DataSource = objclsfl.getallFlowSheetbyParticularName(e.Node.Text);

                    try
                    {
                        if (flxFlowsheet.DataSource != null)
                        {
                            flxFlowsheet.Cols["bIsBold"].Visible = false;
                            flxFlowsheet.Cols["bIsItalic"].Visible = false;
                            flxFlowsheet.Cols["bIsUnderline"].Visible = false;
                            flxFlowsheet.Cols["bIsBold"].Caption = "Bold";
                            flxFlowsheet.Cols["bIsItalic"].Caption = "Italic";
                            flxFlowsheet.Cols["bIsUnderline"].Caption = "Underline";


                        }
                    }
                    catch (Exception ex)
                    {
                        //clsGeneral.UpdateLog("glocomm Error While Clicking on Flowsheet Treeview  Data   in  Usercontrol Flowsheet: " + ex.Message.ToString());
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }

                }
                else
                    fillDownloadedData(e.Node.Text);
            }

            if (e.Node.Level == 0)
            {
                foreach (TreeNode tr in e.Node.Nodes)
                {
                    tr.Checked = e.Node.Checked;
                }
            }
        }

        private void trvflshname_MouseClick(object sender, MouseEventArgs e)
        {
            //string str=
        }

        private void trvflconf_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                rootnode.Nodes.Clear();
                if (straction == "Download")
                {
                    ShowXMLFIleData(e.Node.Tag.ToString(), "", "", "");
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }
    }


}
