using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.Classes;
using C1.Win.C1FlexGrid;
using System.IO;
using System.Xml;
using System.Net;
using System.Configuration;

namespace gloCommunity.UserControls
{
    public partial class UCIMSetup : UserControl
    {
        #region Varibale declarations
        string strAction = "";
        gloLists.Lists gloList;
        bool _blnRootAdded = false;
        bool _blnIsClinicRepository = false;
        //Grid columns


        private int COL_ID = 0;
        private int COL_Cn = 1;
        //private int COL_HOWMANY = 2;
        private int COL_SKU = 2;
        private int COL_Location = 3;
        private int col_Vaccine = 4;
        private int COL_TRADENAME = 5;
        private int COL_Category = 6;
        private int COL_LOtNo = 7;
        private int COL_Mfr = 8;
        private int COL_OnHand = 9;
        private int COL_Status = 10;
        private int COL_Funding = 11;
        private int COL_ExpiryDate = 12;
        private int COL_Comments = 13;
        private int COL_Select = 14;
        private int COL_COUNT = 15;
        //
        #endregion

        public UCIMSetup(string _strAction)
        {
            strAction = _strAction;
            InitializeComponent();
        }

        private void UCIMSetup_Load(object sender, EventArgs e)
        {
            this.cmbStatus.SelectedIndexChanged -= new System.EventHandler(cmbStatus_SelectedIndexChanged);
            //this.cmbLocation.SelectedIndexChanged -= new System.EventHandler(cmbLocation_SelectedIndexChanged);

            cmbStatus.Items.Add("All");
            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = 0;

            if (strAction == "Upload")
            {
                trvCategory.Visible = false;
                pnlLeft.Visible = false;
                //pnlRepository.Visible = false;
                gloC1FlexStyle objgloC1FlexStyle = new gloC1FlexStyle();
                objgloC1FlexStyle.Style(C1IMView);
                FillLocation();
                BindGrid();
            }
            else
            {
                pnltls.Visible = true;
                tlbClinicRepository_Click(null, null);
            }

            this.cmbStatus.SelectedIndexChanged += new System.EventHandler(cmbStatus_SelectedIndexChanged);
            //this.cmbLocation.SelectedIndexChanged += new System.EventHandler(cmbLocation_SelectedIndexChanged);
        }

        #region Upload
        private void BindGrid()
        {
            clsIMSetup oIM = new clsIMSetup();
            clsLiquidData_Download objDlLiquidData = null;
           // int PatientID = 0;
            DataView dvIM = null;
            DataColumn ColSelect = null;
            try
            {
                if (strAction == "Upload")
                {
                    DataTable dtIMDetails = oIM.ImmunizationList(cmbLocation.Text.Trim(), cmbStatus.Text.Trim());
                    if (dtIMDetails != null && dtIMDetails.Rows.Count > 0)
                    {
                        //Add Select column for selecting History Item.
                        ColSelect = new DataColumn("Select", typeof(System.Boolean));
                        dtIMDetails.Columns.Add(ColSelect);
                        //End

                        dvIM = dtIMDetails.DefaultView;
                        dvIM.RowFilter = "[Lot#] <> ''";

                        C1IMView.DataSource = dvIM;
                        DesignGrid(strAction);
                    }
                    else
                    {
                     //   C1IMView.Clear();
                        C1IMView.DataSource = null;
                        if (C1IMView.Rows.Count >= 2)
                        {
                            C1IMView.Rows.RemoveRange(1, C1IMView.Rows.Count - 1);
                        }
                    }
                }
                else
                {
                    if (C1IMView.DataSource != null && C1IMView.Rows.Count > 0)
                    {
                        string _strRowfilter = string.Empty;

                        string _TableName = "ImmunizationSetup";
                        objDlLiquidData = new Classes.clsLiquidData_Download();
                        string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml";
                        DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);

                        dvIM = dt.DefaultView;
                        if (cmbLocation.Text.Trim().ToLower() != "all" || cmbStatus.Text.Trim().ToLower() != "all")
                        {
                            if (cmbLocation.Text.Trim().ToLower() == "all")
                            {
                                _strRowfilter = "Status = '" + cmbStatus.Text.Trim() + "'";
                            }
                            else if (cmbStatus.Text.Trim().ToLower() == "all")
                            {
                                _strRowfilter = "Location = '" + cmbLocation.Text.Trim() + "'";
                            }
                            else
                                _strRowfilter = "Location = '" + cmbLocation.Text.Trim() + "' AND " + "Status = '" + cmbStatus.Text.Trim() + "'";

                            if (_blnIsClinicRepository == true)
                                dvIM.RowFilter = _strRowfilter;
                            else
                            {
                                if (trvCategory.SelectedNode != null)

                                    dvIM.RowFilter = _strRowfilter + " AND Specialty = '" + trvCategory.SelectedNode.Text + "'";
                            }
                        }
                        else
                        {
                            if (trvCategory.SelectedNode != null)
                                dvIM.RowFilter = "Lot <> '' AND Specialty = '" + trvCategory.SelectedNode.Text + "'";
                        }

                        C1IMView.DataSource = dvIM;
                        DesignGrid(strAction);
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                if (oIM != null)
                    oIM = null;
                if (dvIM != null)
                    dvIM = null;
                if (objDlLiquidData != null)
                    objDlLiquidData = null;
            }
        }

        private void DesignGrid(string IsFrom)
        {
            try
            {
                C1IMView.ShowCellLabels = false;
                C1IMView.AllowSorting = C1.Win.C1FlexGrid.AllowSortingEnum.None;

                float _Width = 0;
                _Width = C1IMView.Width;

                if (IsFrom == "Upload")
                {
                    #region Upload design
                    if (C1IMView.Cols.Count == COL_COUNT)
                    {
                        //  C1IMView.Cols(COL_NAME).Width = _Width * 0.6
                        //C1IMView.Cols[COL_Cn].Width = Convert.ToInt32(_Width * 0.07);

                        C1IMView.Cols[COL_ID].Visible = false;
                        C1IMView.Cols[COL_Cn].Visible = false;

                        C1IMView.Cols[COL_Select].DataType = typeof(System.Boolean);
                        C1IMView.Cols[COL_Select].AllowEditing = true;

                        C1IMView.Cols[COL_SKU].Width = Convert.ToInt32(_Width * 0.07);
                        C1IMView.Cols[COL_SKU].AllowEditing = false;

                        C1IMView.Cols[COL_Location].Width = Convert.ToInt32(_Width * 0.12);
                        C1IMView.Cols[COL_Location].AllowEditing = false;

                        C1IMView.Cols[col_Vaccine].Width = Convert.ToInt32(_Width * 0.12);
                        C1IMView.Cols[col_Vaccine].AllowEditing = false;

                        //C1IMView.Cols[COL_HOWMANY].Width = 0;
                        //C1IMView.Cols[COL_HOWMANY].AllowEditing = false;

                        C1IMView.Cols[COL_TRADENAME].Width = Convert.ToInt32(_Width * 0.12);
                        C1IMView.Cols[COL_TRADENAME].AllowEditing = false;

                        C1IMView.Cols[COL_LOtNo].Width = Convert.ToInt32(_Width * 0.05);
                        C1IMView.Cols[COL_LOtNo].AllowEditing = false;

                        C1IMView.Cols[COL_Mfr].Width = Convert.ToInt32(_Width * 0.17);
                        C1IMView.Cols[COL_Mfr].AllowEditing = false;

                        C1IMView.Cols[COL_OnHand].Width = Convert.ToInt32(_Width * 0.14);
                        C1IMView.Cols[COL_OnHand].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                        C1IMView.Cols[COL_OnHand].AllowEditing = false;
                        C1IMView.Cols[COL_OnHand].Caption = "Total doses in inventory";

                        C1IMView.Cols[COL_Status].Width = Convert.ToInt32(_Width * 0.05);
                        C1IMView.Cols[COL_Status].AllowEditing = false;

                        C1IMView.Cols[COL_Funding].Width = Convert.ToInt32(_Width * 0.06);
                        C1IMView.Cols[COL_Funding].AllowEditing = false;

                        C1IMView.Cols[COL_ExpiryDate].Width = Convert.ToInt32(_Width * 0.08);
                        C1IMView.Cols[COL_ExpiryDate].AllowEditing = false;

                        C1IMView.Cols[COL_Comments].Width = Convert.ToInt32(_Width * 0.12);
                        C1IMView.Cols[COL_Comments].AllowEditing = false;

                        //Added new Category field in IMsetup master on 20121002
                        C1IMView.Cols[COL_Category].Width = Convert.ToInt32(_Width * 0.12);
                        C1IMView.Cols[COL_Category].AllowEditing = false;
                        C1IMView.Cols[COL_Category].Move(6);
                        //End
                        
                    }
                    #endregion
                }
                else
                {
                    #region Download design
                    C1IMView.Cols["Count"].Visible = false;
                    C1IMView.Cols["CPTCode"].Visible = false;
                    C1IMView.Cols["ICD9"].Visible = false;
                    C1IMView.Cols["NDCCode"].Visible = false;
                    C1IMView.Cols["ClinicName"].Visible = false;
                    C1IMView.Cols["Specialty"].Visible = false;
                    C1IMView.Cols["IMSetup_ID"].Visible = false;
                    C1IMView.Cols["ReceivedDate"].Visible = false;
                    C1IMView.Cols["VialCount"].Visible = false;
                    C1IMView.Cols["DosesperVial"].Visible = false;

                    C1IMView.Cols["Select"].Width = Convert.ToInt32(_Width * 0.07);
                    C1IMView.Cols["Select"].DataType = typeof(System.Boolean);
                    C1IMView.Cols["Select"].Caption = "Select";

                    C1IMView.Cols["SKU"].Width = Convert.ToInt32(_Width * 0.07);
                    C1IMView.Cols["SKU"].AllowEditing = false;

                    C1IMView.Cols["Location"].Width = Convert.ToInt32(_Width * 0.07);
                    C1IMView.Cols["Location"].Move(3);
                    C1IMView.Cols["Location"].AllowEditing = false;

                    C1IMView.Cols["Vaccine"].Width = Convert.ToInt32(_Width * 0.12);
                    C1IMView.Cols["Vaccine"].AllowEditing = false;

                    C1IMView.Cols["Count"].Width = 0;
                    C1IMView.Cols["Count"].AllowEditing = false;

                    C1IMView.Cols["TradeName"].Width = Convert.ToInt32(_Width * 0.12);
                    C1IMView.Cols["TradeName"].AllowEditing = false;
                    //Fixed bug id 46291 on 20130611
                    C1IMView.Cols["TradeName"].Caption = "Trade Name";
                    //end

                    //Added new Category field in IMsetup master on 20121002
                    if (C1IMView.Cols["Category"] != null)
                    {
                        C1IMView.Cols["Category"].Width = Convert.ToInt32(_Width * 0.12);
                        C1IMView.Cols["Category"].AllowEditing = false;
                        C1IMView.Cols["Category"].Caption = "Category";
                        C1IMView.Cols["Category"].Move(6);
                    }
                    //End

                    C1IMView.Cols["Lot"].Width = Convert.ToInt32(_Width * 0.05);
                    C1IMView.Cols["Lot"].AllowEditing = false;
                    C1IMView.Cols["Lot"].Caption = "Lot#";

                    C1IMView.Cols["Mfr."].Width = Convert.ToInt32(_Width * 0.17);
                    C1IMView.Cols["Mfr."].AllowEditing = false;

                    C1IMView.Cols["OnHand"].Width = Convert.ToInt32(_Width * 0.14);
                    C1IMView.Cols["OnHand"].AllowEditing = false;
                    C1IMView.Cols["OnHand"].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                    C1IMView.Cols["OnHand"].Caption = "Total doses in inventory";

                    C1IMView.Cols["Status"].Width = Convert.ToInt32(_Width * 0.05);
                    C1IMView.Cols["Status"].AllowEditing = false;

                    C1IMView.Cols["Funding"].Width = Convert.ToInt32(_Width * 0.06);
                    C1IMView.Cols["Funding"].AllowEditing = false;

                    C1IMView.Cols["ExpirationDate"].Width = Convert.ToInt32(_Width * 0.08);
                    C1IMView.Cols["ExpirationDate"].AllowEditing = false;
                    C1IMView.Cols["ExpirationDate"].Caption = "Expiration date";

                    C1IMView.Cols["Comments"].Width = Convert.ToInt32(_Width * 0.12);
                    C1IMView.Cols["Comments"].AllowEditing = false;
                    #endregion
                }

                C1IMView.Cols["Select"].Move(0);//First Display Select column.
                C1IMView.SetCellCheck(0, C1IMView.Cols["Select"].Index, CheckEnum.Unchecked);

                CellRange orange = C1IMView.GetCellRange(1, C1IMView.Cols["Select"].Index, C1IMView.Rows.Count - 1, C1IMView.Cols["Select"].Index);
                C1IMView.SetData(orange, false);
            }
            catch (Exception)
            {
            }
            finally
            {

            }
        }
        #endregion

        #region Download
        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            _blnIsClinicRepository = true;
            this.Cursor = Cursors.WaitCursor;
            trvCategory.Nodes.Clear();
         //   C1IMView.Clear();
            C1IMView.DataSource = null;
            pnlLeft.Visible = false;
            try
            {
                string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrIMSetupflnm + "/" + clsGeneral.gstrIMSetupflnm + ".xml";
                DownloadImXml(DownloadPath, "User", clsGeneral.gstrClinicName);
                FillLocation();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            _blnIsClinicRepository = false;
            this.Cursor = Cursors.WaitCursor;
            trvCategory.Nodes.Clear();
           // C1IMView.Clear();
            C1IMView.DataSource = null;
            pnlLeft.Visible = true;
            string strSitepath = "";
            string strSitefolder = "";
            string strFrom = "";
            _blnRootAdded = false;
            clsgloCommunity ObjclsgloCommunity = new clsgloCommunity();
            clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
            try
            {
                strSitepath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                strSitefolder = clsGeneral.WebGlobalXmlFolder;
                strFrom = "Global";
                gloList = new gloLists.Lists();

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    gloList.UseDefaultCredentials = true;

                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120822
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
                    //End
                }

                gloList.Url = strSitepath + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                gloList.Timeout = int.MaxValue;
                System.Xml.XmlNode node = gloList.GetListCollection();
                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {
                        if (xmlnode.Attributes["Title"].Value.ToString() == strSitefolder)
                        {
                            DataTable dtGlobalRepository = new DataTable();
                            dtGlobalRepository = ObjclsgloCommunity.GetList(xmlnode.Attributes["Title"].Value.ToString(), strSitepath);
                            if (dtGlobalRepository.Rows.Count > 0 && dtGlobalRepository != null)
                            {
                                foreach (DataRow dr in dtGlobalRepository.Rows)
                                {
                                    if (dr["ContentType"].ToString().Trim() == "Folder")
                                    {
                                        string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + dr["Title"].ToString().Trim() + "/" + clsGeneral.gstrIMSetupflnm + "/" + clsGeneral.gstrIMSetupflnm + ".xml";
                                        DownloadImXml(DownloadPath, strFrom, dr["Title"].ToString().Trim());
                                    }
                                }
                            }
                        }
                    }
                }
                
                if (File.Exists(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + "Global.xml") == true)
                {
                    if (File.Exists(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml"))
                    {
                        File.Delete(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml");
                        File.Move(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + "Global.xml", gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml");
                    }
                }

                FillLocation();
                //Fixed bug id 46292 on 20130613
                if (trvCategory.Nodes[0] != null && trvCategory.Nodes[0].Nodes.Count > 0)
                    trvCategory.SelectedNode = trvCategory.Nodes[0].Nodes[0];
                //End
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

            }
            finally
            {
                gloList = null;
                ObjclsgloCommunity = null;
                objDlLiquidData = null;
            }
            this.Cursor = Cursors.Default;
        }

        private bool DownloadImXml(string DownloadPath, string IsFrom, string Title)
        {
            string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml";
            bool IsDownloadXml = false;
            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
            //

            try
            {
                if (IsFrom == "Global")
                {
                    if (File.Exists(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + "Global.xml") == true)
                    {
                        IsDownloadXml = DownloadXML(DownloadPath, clsGeneral.gstrIMSetupflnm + ".xml");

                        if (IsDownloadXml == true)
                        {
                            //show Specialty
                            string _TableName = "ImmunizationSetup";
                            DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                myTreeNode Parentnode = default(myTreeNode);
                                myTreeNode mynode = default(myTreeNode);
                                if (IsFrom == "Global")
                                {
                                    if (_blnRootAdded == false)
                                    {
                                        Parentnode = new myTreeNode("Immunization Setup", -1);
                                        Parentnode.ImageIndex = 0;
                                        Parentnode.SelectedImageIndex = 0;
                                        trvCategory.Nodes.Add(Parentnode);
                                        _blnRootAdded = true;
                                    }
                                    mynode = new myTreeNode(Title, -1);
                                    mynode.ImageIndex = 1;
                                    mynode.SelectedImageIndex = 1;
                                    trvCategory.Nodes[0].Nodes.Add(mynode);

                                    //this.trvCategory.AfterSelect -= new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
                                    //trvCategory.SelectedNode = trvCategory.Nodes[0].Nodes[0];
                                    //this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);

                                    DataView dvIM = dt.DefaultView;
                                    dvIM.RowFilter = "Lot <> ''";

                                    C1IMView.DataSource = dvIM;
                                    DesignGrid(strAction);
                                }
                            }
                            //end

                            XmlDocument oDocFirst = new XmlDocument();
                            oDocFirst.Load(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + "Global.xml");

                            XmlDocument oDocSecond = new XmlDocument();
                            oDocSecond.Load(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml");

                            XmlNode oNodeWhereInsert = oDocFirst.SelectSingleNode("/ImSetup");
                            foreach (XmlNode oNode in oDocSecond.SelectNodes("/ImSetup/ImmunizationSetup"))
                            {
                                oNodeWhereInsert.AppendChild(oDocFirst.ImportNode(oNode, true));
                            }
                            oDocFirst.Save(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + "Global.xml");
                            //end
                        }
                    }
                    else
                    {
                        //execute only ones when ImSetupGlobal.xml not exist
                        IsDownloadXml = DownloadXML(DownloadPath, clsGeneral.gstrIMSetupflnm + "Global.xml");
                        if (IsDownloadXml == true)
                        {
                            string _TableName = "ImmunizationSetup";
                            DataTable dt = objDlLiquidData.GetXmlData(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + "Global.xml", _TableName);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                myTreeNode Parentnode = default(myTreeNode);
                                myTreeNode mynode = default(myTreeNode);
                                if (IsFrom == "Global")
                                {
                                    if (_blnRootAdded == false)
                                    {
                                        Parentnode = new myTreeNode("Immunization Setup", -1);
                                        Parentnode.ImageIndex = 0;
                                        Parentnode.SelectedImageIndex = 0;
                                        trvCategory.Nodes.Add(Parentnode);
                                        _blnRootAdded = true;
                                    }
                                    mynode = new myTreeNode(Title, -1);
                                    mynode.ImageIndex = 1;
                                    mynode.SelectedImageIndex = 1;
                                    trvCategory.Nodes[0].Nodes.Add(mynode);

                                    //this.trvCategory.AfterSelect -= new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);
                                    //trvCategory.SelectedNode = trvCategory.Nodes[0].Nodes[0];
                                    //this.trvCategory.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvCategory_AfterSelect);

                                    DataView dvIM = dt.DefaultView;
                                    dvIM.RowFilter = "Lot <> ''";

                                    C1IMView.DataSource = dvIM;
                                    DesignGrid(strAction);
                                }
                            }
                        }
                        //end
                    }
                }
                else
                {
                    IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);
                    if (IsDownloadXml == true)
                    {
                        string _TableName = "ImmunizationSetup";
                        DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                        if (dt.Rows.Count > 0 && dt != null)
                        {
                            DataView dvIM = dt.DefaultView;
                            dvIM.RowFilter = "Lot <> ''";

                            C1IMView.DataSource = dvIM;
                            DesignGrid(strAction);
                        }
                    }
                }
                trvCategory.ExpandAll();
            }
            catch (Exception ex)
            {
                IsDownloadXml = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objgloCommunity = null;
                objDlLiquidData = null;
            }
            return IsDownloadXml;
        }

        public bool DownloadXML(string temppath, string filename)
        {
            HttpWebRequest request;
            HttpWebResponse response = null;
            string strDestinationPath = gloSettings.FolderSettings.AppTempFolderPath;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(temppath);
                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    request.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120822
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
                request.Timeout = int.MaxValue;
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
            catch //(System.Net.WebException ex)
            {
                //Exception 404 occurs when xml file not available.
                if (response != null)
                    response.Close();
                return false;
            }
        }
        #endregion

        #region Events
        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void C1IMView_MouseDown(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    bool _CellCheckResult = false;
                    //var _with1 = C1IMView;
                    int RowIndex = C1IMView.HitTest(e.X, e.Y).Row;
                    if (RowIndex == 0)
                    {
                        CheckEnum _CheckUncheck = new CheckEnum();

                        _CheckUncheck = C1IMView.GetCellCheck(0, 0);

                        if (_CheckUncheck == CheckEnum.Checked)
                            _CellCheckResult = true;
                        else
                            _CellCheckResult = false;

                        //for (int i = 1; i < _with1.Rows.Count; i++)
                        //{
                        //    _with1.SetData(i, COL_Select, _CellCheckResult);
                        //}

                        CellRange orange1 =  C1IMView.GetCellRange(1, C1IMView.Cols["Select"].Index, C1IMView.Rows.Count - 1, C1IMView.Cols["Select"].Index);
                        C1IMView.SetData(orange1, _CellCheckResult);
                    }
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

        private void trvCategory_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                if (e.Node != null)
                {
                    string _TableName = "ImmunizationSetup";
                    clsLiquidData_Download objDlLiquidData = new Classes.clsLiquidData_Download();
                    string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml";
                    DataTable dt = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataView dvIM = dt.DefaultView;
                        dvIM.RowFilter = "Lot <> '' AND Specialty = '" + e.Node.Text + "'";
                        C1IMView.DataSource = dvIM;
                        DesignGrid(strAction);
                        cmbStatus.SelectedIndex = 0;
                    }
                }
            }
            catch
            {
            }
        }
        #endregion

        private void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void FillLocation()
        {
            this.cmbLocation.SelectedIndexChanged -= new System.EventHandler(cmbLocation_SelectedIndexChanged);
            DataTable dtLocation = null;
            clsIMSetup oclsIMSetup = null;
            Classes.clsLiquidData_Download objDlLiquidData = null;
            cmbLocation.Items.Clear();
            try
            {
                dtLocation = new DataTable();
                cmbLocation.Items.Add("All");
                Int16 i = default(Int16);

                if (strAction == "Upload")
                {
                    oclsIMSetup = new clsIMSetup();
                    dtLocation = oclsIMSetup.GetLocation();
                    if ((dtLocation == null) == false)
                    {
                        if (dtLocation.Rows.Count > 0)
                        {
                            for (i = 0; i <= dtLocation.Rows.Count - 1; i++)
                            {
                                if (Convert.ToString(dtLocation.Rows[i][1]) != string.Empty)
                                    cmbLocation.Items.Add(dtLocation.Rows[i][1].ToString());
                            }
                        }
                    }
                }
                else
                {
                    string _fileName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrIMSetupflnm + ".xml";
                    if (File.Exists(_fileName))
                    {
                        objDlLiquidData = new Classes.clsLiquidData_Download();
                        dtLocation = objDlLiquidData.GetXmlData(_fileName, "ImmunizationSetup");
                        DataView dv = dtLocation.DefaultView;
                        dtLocation = dv.ToTable(true, "Location");
                        if (dtLocation != null & dtLocation.Rows.Count > 0)
                        {
                            for (i = 0; i <= dtLocation.Rows.Count - 1; i++)
                            {
                                if (Convert.ToString(dtLocation.Rows[i][0]) != string.Empty)
                                    cmbLocation.Items.Add(dtLocation.Rows[i][0].ToString());
                            }
                        }
                    }
                }
                cmbLocation.SelectedIndex = cmbLocation.FindStringExact("All");
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Immunization, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (dtLocation != null)
                {
                    dtLocation.Dispose();
                    dtLocation = null;
                }
                if (oclsIMSetup != null)
                    oclsIMSetup = null;
                this.cmbLocation.SelectedIndexChanged += new System.EventHandler(cmbLocation_SelectedIndexChanged);
            }
        }
    }
}
