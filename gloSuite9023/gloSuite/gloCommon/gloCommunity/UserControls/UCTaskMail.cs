using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using System.Net;
using System.IO;
using C1.Win.C1FlexGrid;
using System.Configuration;
namespace gloCommunity.UserControls
{
    public partial class UCTaskMail : UserControl
    {
        clsTaskMail objclsTM = null;
        DataTable Resultdata = null;
        string straction = "";
        public UCTaskMail()
        {
            InitializeComponent();
            objclsTM = new clsTaskMail();
            FillFollowUp();
        }

        public UCTaskMail(string stract)
        {
            InitializeComponent();
            straction = stract;
            if (straction == "Download")
            {
                clsGeneral.Webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrTskMlflnm + "/" + clsGeneral.gstrTskMlflnm + ".xml";
                //  pnl_btnICD9.Visible = false;
                //pnl_btnLocal.Dock = DockStyle.Top;    
                ShowXMLFIleData(fileUrl, "", "", "");
                // btnLocal.Text = clsGeneral.ClinicRepository;
                // btnCentral.Text = clsGeneral.GlobalRepository;

            }
            else
            {
                objclsTM = new clsTaskMail();

                FillFollowUp();

                trvTaskMail.Visible = false;
                //pnl_btnICD9.Visible = false;
                //   pnl_btnLocal.Visible = false;
                //   pnl_btnCentral.Visible = false;
                Panel1.Visible = false;
                pnltls.Visible = false;
            }
            flxfollowup.SetCellCheck(0, 0, CheckEnum.Unchecked);
            if (flxfollowup.DataSource != null)
            {
                if (flxfollowup.Cols.Count > 0)
                    flxfollowup.Cols[0].AllowSorting = false;
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

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrTskMlflnm + ".xml"; //"SmartDxAssociation.xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();
                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrTskMlflnm + ".xml");
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.ToString());
                    //clsGeneral.UpdateLog("Error  while getting XML File Data    in Taskmail : " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }

                DataTable showdata = ds.Tables[0];
                fillDownloadedData(showdata);

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while getting XML File Data    in Taskmail : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                ds.Dispose();
                this.Cursor = Cursors.Default;
            }

        }

        private void fillDownloadedData(DataTable showdata)
        {
            DataTable dtFollowup = null;
            DataTable dtPriority = null;
            DataTable dtStat = null;
            bool bl = false;
            try
            {
                dtStat = showdata.Clone();
                dtPriority = showdata.Clone();
                dtFollowup = showdata.Clone();
                DataRow[] drf = showdata.Select("Category='FollowUp'");
                DataRow[] drp = showdata.Select("Category='Priorities'");
                DataRow[] drs = showdata.Select("Category='Status'");

                for (int len = 0; len < drf.Length; len++)
                {

                    drf[len]["Select"] = bl;
                    dtFollowup.ImportRow(drf[len]);
                }

                for (int len = 0; len < drp.Length; len++)
                {
                    drp[len]["Select"] = bl;
                    dtPriority.ImportRow(drp[len]);
                }
                for (int len = 0; len < drs.Length; len++)
                {
                    drs[len]["Select"] = bl;
                    dtStat.ImportRow(drs[len]);
                }

                flxfollowup.DataSource = dtFollowup.Copy();
                flxpritype.DataSource = dtPriority.Copy();
                flxstattype.DataSource = dtStat.Copy();
                if (flxfollowup.DataSource != null)
                {
                    for (int follcol = 1; follcol < flxfollowup.Cols.Count; follcol++)
                    {
                        flxfollowup.Cols[follcol].AllowEditing = false;
                    }
                    //flxfollowup.Cols[0].he  
                    flxfollowup.Cols[0].DataType = typeof(bool);
                    flxfollowup.Cols[2].Visible = false;
                    flxfollowup.Cols[3].Visible = false;
                    flxfollowup.Cols[4].Visible = false;
                }
                if (flxpritype.DataSource != null)
                {
                    for (int follcol = 1; follcol < flxpritype.Cols.Count; follcol++)
                    {
                        flxpritype.Cols[follcol].AllowEditing = false;
                    }
                    flxpritype.Cols[0].DataType = typeof(bool);
                    flxpritype.Cols[2].Visible = false;
                    flxpritype.Cols[3].Visible = false;
                    flxpritype.Cols[4].Visible = false;
                }
                if (flxstattype.DataSource != null)
                {
                    for (int follcol = 1; follcol < flxstattype.Cols.Count; follcol++)
                    {
                        flxstattype.Cols[follcol].AllowEditing = false;
                    }

                    flxstattype.Cols[0].DataType = typeof(bool);
                    flxstattype.Cols[2].Visible = false;
                    flxstattype.Cols[3].Visible = false;
                    flxstattype.Cols[4].Visible = false;
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Filling Downloading Data    in Taskmail : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                dtFollowup.Dispose();
                dtFollowup = null;
                dtPriority.Dispose();
                dtPriority = null;
                dtStat.Dispose();
                dtStat = null;

            }
        }

        private void tskmailtab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (straction == "Download")
            {

            }
            else
            {
                switch (tskmailtab.SelectedTab.Text)
                {

                    case "Follow Up":
                        if (flxfollowup.DataSource == null)
                        {
                            FillFollowUp();
                            // flxfollowup.SetCellCheck(0, 0, CheckEnum.Unchecked); 

                        }
                        break;
                    case "Status Type":
                        if (flxstattype.DataSource == null)
                        {
                            FillStatusType();

                        }
                        break;
                    case "Priority Type":
                        if (flxpritype.DataSource == null)
                        {
                            FillPriorityType();

                        }
                        break;

                }
            }

            flxstattype.SetCellCheck(0, 0, CheckEnum.Unchecked);
            flxpritype.SetCellCheck(0, 0, CheckEnum.Unchecked);
            if (flxstattype.DataSource != null)
            {
                if (flxstattype.Cols.Count > 0)
                    flxstattype.Cols[0].AllowSorting = false;
            }


            if (flxpritype.DataSource != null)
            {
                if (flxpritype.Cols.Count > 0)
                    flxpritype.Cols[0].AllowSorting = false;
            }
        }

        public void FillFollowUp()
        {
            Resultdata = objclsTM.GetFollowUps();
            flxfollowup.DataSource = Resultdata;
            if (flxfollowup.DataSource != null)
            {
                for (int follcol = 1; follcol < flxfollowup.Cols.Count; follcol++)
                {
                    flxfollowup.Cols[follcol].AllowEditing = false;
                }
            }

            flxfollowup.Cols[2].Visible = false;
            flxfollowup.Cols[3].Visible = false;
            flxfollowup.Cols[4].Visible = false;
        }

        public void FillStatusType()
        {
            Resultdata = objclsTM.GetStatuses();
            flxstattype.DataSource = Resultdata;
            if (flxstattype.DataSource != null)
            {
                for (int follcol = 1; follcol < flxstattype.Cols.Count; follcol++)
                {
                    flxstattype.Cols[follcol].AllowEditing = false;
                }
            }

            flxstattype.Cols[2].Visible = false;
            flxstattype.Cols[3].Visible = false;
            flxstattype.Cols[4].Visible = false;
        }

        public void FillPriorityType()
        {
            Resultdata = objclsTM.GetPriorities();
            flxpritype.DataSource = Resultdata;
            if (flxpritype.DataSource != null)
            {
                for (int follcol = 1; follcol < flxpritype.Cols.Count; follcol++)
                {
                    flxpritype.Cols[follcol].AllowEditing = false;
                }
            }

            flxpritype.Cols[2].Visible = false;
            flxpritype.Cols[3].Visible = false;
            flxpritype.Cols[4].Visible = false;
        }

        private void cleargriddata()
        {
          //  flxstattype.Clear();
            flxstattype.DataSource = null;
           // flxpritype.Clear();
            flxpritype.DataSource = null;
           // flxfollowup.Clear();
            flxfollowup.DataSource = null;
            if (flxstattype.Rows.Count > 1)
            {
                flxstattype.Rows.RemoveRange(1, flxstattype.Rows.Count - 1);
            }
            if (flxpritype.Rows.Count > 1)
            {
                flxpritype.Rows.RemoveRange(1, flxpritype.Rows.Count - 1);
            }
            if (flxfollowup.Rows.Count > 1)
            {
                flxfollowup.Rows.RemoveRange(1, flxfollowup.Rows.Count - 1);
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
            myservice.Url = clsGeneral.Webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;// SiteURL + gstrVti_Bin + "/" + gstrListSvc;
            System.Xml.XmlNode node = myservice.GetListCollection();

            foreach (System.Xml.XmlNode xmlnode in node)
            {
                if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                {
                    if (xmlnode.Attributes["Title"].Value.ToString() == clsGeneral.WebGlobalXmlFolder)
                    {
                        DataTable dt = new DataTable();
                        dt = objclsgcomm.GetList(xmlnode.Attributes["Title"].Value.ToString(), clsGeneral.Webpath + "/");

                        for (int lenitem = 0; lenitem <= dt.Rows.Count - 1; lenitem++)
                        {
                            if (dt.Rows[lenitem]["ContentType"].ToString().Trim() == "Folder")
                            {
                                gloUserControlLibrary.myTreeNode tr = new gloUserControlLibrary.myTreeNode();
                                string StrName = dt.Rows[lenitem]["title"].ToString();
                                tr.Text = StrName;
                                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrTskMlflnm + "/" + clsGeneral.gstrTskMlflnm + ".xml";

                                tr.Tag = fileUrl;
                                if (tr.Text.Contains(".aspx") == false)
                                    trvTaskMail.Nodes.Add(tr);
                            }
                        }
                    }
                }
            }
        }

        private void trvTaskMail_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (straction == "Download")
            {
                cleargriddata();

                ShowXMLFIleData(trvTaskMail.SelectedNode.Tag.ToString(), "", "", "");
                flxfollowup.SetCellCheck(0, 0, CheckEnum.Unchecked);
                if (flxfollowup.DataSource != null)
                {
                    if (flxfollowup.Cols.Count > 0)
                        flxfollowup.Cols[0].AllowSorting = false;
                }
            }
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                trvTaskMail.Nodes.Clear();
                cleargriddata();
                clsGeneral.Webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrTskMlflnm + "/" + clsGeneral.gstrTskMlflnm + ".xml";
                //pnl_btnICD9.Visible = false;

                ShowXMLFIleData(fileUrl, "", "", "");
                flxfollowup.SetCellCheck(0, 0, CheckEnum.Unchecked);
                if (flxfollowup.DataSource != null)
                {
                    if (flxfollowup.Cols.Count > 0)
                        flxfollowup.Cols[0].AllowSorting = false;
                }
                Panel1.Visible = false;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Clicking on Clinical Repository   in Taskmail : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                trvTaskMail.Nodes.Clear();
                cleargriddata();
                clsGeneral.Webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.gstrTskMlflnm + "/" + clsGeneral.gstrTskMlflnm + ".xml";
                //pnl_btnICD9.Visible = false;
                GetCentralList();
                Panel1.Visible = true;
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Clicking on Global Repository   in Taskmail : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void SetCellCheck(C1FlexGrid flxgrd, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                bool _CellCheckResult = false;
                var _with = flxgrd;
                int RowIndex = _with.HitTest(e.X, e.Y).Row;

                if (RowIndex == 0)
                {
                    CheckEnum _CheckUncheck = new CheckEnum();

                    _CheckUncheck = _with.GetCellCheck(0, 0);

                    if (_CheckUncheck == CheckEnum.Checked)
                        _CellCheckResult = true;
                    else
                        _CellCheckResult = false;

                    for (int i = 1; i < _with.Rows.Count; i++)
                    {
                        _with.SetData(i, 0, _CellCheckResult);
                    }
                }
            }
        }

        private void flxfollowup_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxstattype_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxpritype_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void trvTaskMail_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (straction == "Download")
                {
                    cleargriddata();

                    ShowXMLFIleData(e.Node.Tag.ToString(), "", "", "");
                    flxfollowup.SetCellCheck(0, 0, CheckEnum.Unchecked);
                    if (flxfollowup.DataSource != null)
                    {
                        if (flxfollowup.Cols.Count > 0)
                            flxfollowup.Cols[0].AllowSorting = false;
                    }
                }
            }
            catch
            {
            }
        }

    }
}
