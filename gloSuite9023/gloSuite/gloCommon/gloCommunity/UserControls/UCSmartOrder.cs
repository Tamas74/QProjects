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
using System.Configuration;

namespace gloCommunity.UserControls
{
    public partial class UCSmartOrder : UserControl
    {
        string strAction = "";

        public string StrLabs = string.Empty;
        public string StrOrd = string.Empty;
        public string StrRefOrdTgsPE = string.Empty;
        public string StrFlo = string.Empty;
        public string StrDrg = string.Empty;
      //  private bool bParentTrigger = true;
      //  private bool bChildTrigger = true;

        public DataTable dtorderMain = null;

        public DataTable dtflo = null;
        public DataTable dtfloc = null;
        public DataTable dtfloc1 = null;

        public DataTable dtlabs = null;
        public DataTable dtlabsc = null;

        public DataTable dtorder = null;
        public DataTable dtorderc = null;

        public DataTable dtref = null;
        public DataTable dtrefc = null;

        public DataTable dtdrg = null;
        public DataTable dtdrgc = null;

        public bool IsClinicRepository = true;//check IsClinicRepository flag while Show SmartOrder from SmartOrderXML (gloCommunityDownload only)

        public UCSmartOrder(string _strAction)
        {
            strAction = _strAction;
            InitializeComponent();
        }

        private void UCSmartOrder_Load(object sender, EventArgs e)
        {
            if (strAction == "Upload")
            {
                FillOrderSet_New();

                myTreeNode rootnode = new myTreeNode("Orders Association", -1);
                rootnode.ImageIndex = 6;
                rootnode.SelectedImageIndex = 6;
                trOrderAssociation.Nodes.Add(rootnode);
            }
            else
            {
                GloUC_trvOrderSet.SearchBox = false;
                Panel1.Visible = false;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrSmartOrderflnm + "/" + clsGeneral.gstrSmartOrderflnm + ".xml";
                ShowXMLFIleData(fileUrl, "", "", "");
            }
        }

        private void GloUC_trvOrderSet_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (strAction != "Download")
                {
                    gloUserControlLibrary.myTreeNode oNode = (gloUserControlLibrary.myTreeNode)e.Node;
                    myTreeNode mynode = new myTreeNode();

                    mynode.Key = oNode.ID;
                    mynode.Text = oNode.Text;
                    if ((mynode != null))
                    {
                        AddNode(mynode);
                    }
                }
                else
                {
                    ShowXMLFIleData(GloUC_trvOrderSet.SelectedNode.Tag.ToString(), "", "", "");
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("Error  while Node Mouse Double Click   For Smart Order     in SmartOrder Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        #region "Upload"
        private void FillOrderSet_New()
        {
            ///'Send O to retrive Odrerset
            DataTable dtOrderset = new DataTable();
            clsSmartOrder objclsSmartOrder = new clsSmartOrder();
            try
            {
                dtOrderset = objclsSmartOrder.FillControl(0);
                GloUC_trvOrderSet.Clear();
                if ((dtOrderset != null))
                {
                    GloUC_trvOrderSet.DataSource = dtOrderset;
                    // GloUC_trvOrderSet.ParentMember = dtOrderset.Columns("sCategoryType").ColumnName
                    GloUC_trvOrderSet.ValueMember = dtOrderset.Columns["nCategoryID"].ColumnName;
                    GloUC_trvOrderSet.Tag = dtOrderset.Columns["nCategoryID"].ColumnName;
                    GloUC_trvOrderSet.DescriptionMember = dtOrderset.Columns["sDescription"].ColumnName;
                    GloUC_trvOrderSet.CodeMember = dtOrderset.Columns["nCategoryID"].ColumnName;
                    GloUC_trvOrderSet.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Descripation;
                    GloUC_trvOrderSet.Search = gloUserControlLibrary.gloUC_TreeView.enumSearchType.Instring;
                    GloUC_trvOrderSet.FillTreeView();
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Filling order  For Smart Order  in SmartOrder Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (dtOrderset != null)
                {
                    dtOrderset.Dispose();
                    dtOrderset = null;
                }
                objclsSmartOrder = null;
            }
        }
        #endregion

        #region "Download"
        private void ShowXMLFIleData(string XmlFileUrl, string UserName, string Password, string Domain)
        {
            this.Cursor = Cursors.WaitCursor;
            HttpWebRequest request = default(HttpWebRequest);
            HttpWebResponse response = null;
            DataSet ds = new DataSet();
            trOrderAssociation.Nodes.Clear();
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

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrSmartOrderflnm + ".xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();

                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrSmartOrderflnm + ".xml");
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.ToString());
                    //clsGeneral.UpdateLog("Error  while getting  Data For Smart Order     in SmartOrder Usercontrol : " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                trOrderAssociation.Nodes.Clear();

                dtorderMain = ds.Tables["Order"];

                dtlabs = ds.Tables["Labs"];
                dtlabsc = ds.Tables["Labsc"];

                dtorder = ds.Tables["Orders"];
                dtorderc = ds.Tables["Ordersc"];

                dtref = ds.Tables["Referralletter"];
                dtrefc = ds.Tables["Referralletterc"];

                dtdrg = ds.Tables["Drugs"];
                dtdrgc = ds.Tables["Drugsc"];

                dtflo = ds.Tables["flowsheet"];
                dtfloc = ds.Tables["flowsheetc"];
                dtfloc1 = ds.Tables["flowsheetc1"];

                StrLabs = string.Empty;
                StrRefOrdTgsPE = string.Empty;
                StrFlo = string.Empty;
                StrDrg = string.Empty;

                myTreeNode trOrderAsso = new myTreeNode();
                trOrderAsso.Text = "Order Association";
                trOrderAsso.ImageIndex = 6;
                trOrderAsso.SelectedImageIndex = 6;
                trOrderAssociation.Nodes.Add(trOrderAsso);
                myTreeNode trOrder = default(myTreeNode);
                if ((dtorderMain != null))
                {
                    for (Int32 lenOrder = 0; lenOrder <= dtorderMain.Rows.Count - 1; lenOrder++)
                    {
                        trOrder = new myTreeNode();
                        trOrder.Text = dtorderMain.Rows[lenOrder]["Text"].ToString();
                        trOrder.ImageIndex = 5;
                        trOrder.SelectedImageIndex = 5;
                        trOrderAsso.Nodes.Add(trOrder);

                        //code for adding Lab Order
                        myTreeNode trvlabord = new myTreeNode();
                        trvlabord.Text = "Labs";
                        trvlabord.ImageIndex = 1;
                        trvlabord.SelectedImageIndex = 1;
                        trOrder.Nodes.Add(trvlabord);

                        if ((dtlabs != null))
                        {
                            DataRow[] drlabs = dtlabs.Select("Order_Id=" + dtorderMain.Rows[lenOrder]["Order_Id"]);
                            if ((drlabs.Length > 0))
                            {
                                Int32 lab_id = Convert.ToInt32(drlabs[0]["Labs_Id"]);
                                if ((dtlabsc != null))
                                {
                                    DataRow[] drlabc = dtlabsc.Select("Labs_Id=" + lab_id);
                                    for (int lenlabc = 0; lenlabc <= drlabc.Length - 1; lenlabc++)
                                    {
                                        myTreeNode labc = new myTreeNode();
                                        labc.Text = drlabc[lenlabc]["Text"].ToString();
                                        //labc.Tag = labc.Text.Substring(0, labc.Text.IndexOf("-") - 1)
                                        labc.DrugName = drlabc[lenlabc]["DrugName"].ToString();
                                        labc.Dosage = drlabc[lenlabc]["Dosage"].ToString();
                                        labc.DrugForm = drlabc[lenlabc]["DrugForm"].ToString();
                                        labc.DrugQtyQualifier = drlabc[lenlabc]["DrugQtyQualifier"].ToString();
                                        labc.IsNarcotics = Convert.ToInt16(drlabc[lenlabc]["IsNarcotics"]);
                                        labc.DDID = Convert.ToInt64(drlabc[lenlabc]["DDID"]);
                                        labc.NDCCode = drlabc[lenlabc]["NDCCode"].ToString();
                                        if (drlabc[lenlabc]["Status"] != null)
                                        {
                                            if (drlabc[lenlabc]["Status"].ToString() == "True")
                                                labc.Checked = true;
                                        }

                                        labc.ImageIndex = 4;
                                        labc.SelectedImageIndex = 4;
                                        trvlabord.Nodes.Add(labc);
                                        // hshkey.Add(labc.Text, "LO")
                                        StrLabs = StrLabs + "'" + labc.Text.Replace("'", "''") + "'" + ",";
                                    }
                                }
                            }
                        }//End code for adding Lab Order

                        //code for adding Order
                        myTreeNode trvord = new myTreeNode();
                        trvord.Text = "Orders";
                        trvord.ImageIndex = 2;
                        trvord.SelectedImageIndex = 2;
                        trOrder.Nodes.Add(trvord);

                        if ((dtorder != null))
                        {
                            DataRow[] drord = dtorder.Select("Order_Id=" + dtorderMain.Rows[lenOrder]["Order_Id"]);
                            if ((drord.Length > 0))
                            {
                                Int32 ord_id = Convert.ToInt32(drord[0]["Orders_Id"]);
                                if ((dtorderc != null))
                                {
                                    DataRow[] drordc = dtorderc.Select("Orders_Id=" + ord_id);
                                    for (int lenordc = 0; lenordc <= drordc.Length - 1; lenordc++)
                                    {
                                        myTreeNode ordc = new myTreeNode();
                                        ordc.Text = drordc[lenordc]["Text"].ToString();
                                        //  ordc.Tag = ordc.Text.Substring(0, ordc.Text.IndexOf("-") - 1)
                                        ordc.DrugName = drordc[lenordc]["DrugName"].ToString();
                                        ordc.Dosage = drordc[lenordc]["Dosage"].ToString();
                                        ordc.DrugForm = drordc[lenordc]["DrugForm"].ToString();
                                        ordc.DrugQtyQualifier = drordc[lenordc]["DrugQtyQualifier"].ToString();
                                        ordc.IsNarcotics = Convert.ToInt16(drordc[lenordc]["IsNarcotics"]);
                                        ordc.DDID = Convert.ToInt64(drordc[lenordc]["DDID"]);
                                        ordc.NDCCode = drordc[lenordc]["NDCCode"].ToString();
                                        ordc.Route = drordc[lenordc]["Route"].ToString();

                                        if (drordc[lenordc]["Status"] != null)
                                        {
                                            if (drordc[lenordc]["Status"].ToString() == "True")
                                                ordc.Checked = true;
                                        }

                                        ordc.ImageIndex = 4;
                                        ordc.SelectedImageIndex = 4;
                                        trvord.Nodes.Add(ordc);

                                        StrOrd = StrOrd + "'" + ordc.Text.Replace("'", "''") + "'" + ",";
                                    }
                                }
                            }
                        }//End code for adding Order

                        //code for Referral Letter 
                        myTreeNode trvref = new myTreeNode();
                        trvref.Text = "Referral Letter";
                        trvref.ImageIndex = 7;
                        trvref.SelectedImageIndex = 7;
                        trOrder.Nodes.Add(trvref);

                        if ((dtref != null))
                        {
                            DataRow[] drref = dtref.Select("Order_Id=" + dtorderMain.Rows[lenOrder]["Order_Id"]);
                            if ((drref.Length > 0))
                            {
                                Int32 ref_id = Convert.ToInt32(drref[0]["Referralletter_Id"]);
                                if ((dtrefc != null))
                                {
                                    DataRow[] drrefc = dtrefc.Select("Referralletter_Id=" + ref_id);
                                    for (int lenrefc = 0; lenrefc <= drrefc.Length - 1; lenrefc++)
                                    {
                                        myTreeNode refc = new myTreeNode();
                                        refc.Text = drrefc[lenrefc]["Text"].ToString();
                                        //  refc.Tag = refc.Text.Substring(0, refc.Text.IndexOf("-") - 1)
                                        refc.Text = drrefc[lenrefc]["Text"].ToString();
                                        refc.DrugName = drrefc[lenrefc]["DrugName"].ToString();
                                        refc.Dosage = drrefc[lenrefc]["Dosage"].ToString();
                                        refc.DrugForm = drrefc[lenrefc]["DrugForm"].ToString();
                                        refc.DrugQtyQualifier = drrefc[lenrefc]["DrugQtyQualifier"].ToString();
                                        refc.IsNarcotics = Convert.ToInt16(drrefc[lenrefc]["IsNarcotics"]);
                                        refc.DDID = Convert.ToInt64(drrefc[lenrefc]["DDID"]);
                                        refc.NDCCode = drrefc[lenrefc]["NDCCode"].ToString();

                                        if (drrefc[lenrefc]["Status"] != null)
                                        {
                                            if (drrefc[lenrefc]["Status"].ToString() == "True")
                                                refc.Checked = true;
                                        }

                                        refc.ImageIndex = 4;
                                        refc.SelectedImageIndex = 4;
                                        trvref.Nodes.Add(refc);
                                        // hshkey.Add(refc.Text, "REF")
                                        //                StrRef = StrRef & "'" & refc.Text & "'" & ","
                                        StrRefOrdTgsPE = StrRefOrdTgsPE + "'" + refc.Text.Replace("'", "''") + "'" + ",";
                                    }
                                }
                            }
                        }//End code for Referral Letter

                        //code for adding Drug
                        myTreeNode trvdrg = new myTreeNode();
                        trvdrg.Text = "Drugs";
                        trvdrg.ImageIndex = 8;
                        trvdrg.SelectedImageIndex = 8;
                        trOrder.Nodes.Add(trvdrg);
                        if ((dtdrg != null))
                        {
                            DataRow[] drdrg = dtdrg.Select("Order_Id=" + dtorderMain.Rows[lenOrder]["Order_Id"]);
                            if ((drdrg.Length > 0))
                            {
                                Int32 drg_id = Convert.ToInt32(drdrg[0]["drugs_id"]);
                                if ((dtdrgc != null))
                                {
                                    DataRow[] drdrgc = dtdrgc.Select("drugs_Id=" + drg_id);
                                    for (int lendrgc = 0; lendrgc <= drdrgc.Length - 1; lendrgc++)
                                    {
                                        myTreeNode drgc = new myTreeNode();
                                        drgc.Text = drdrgc[lendrgc]["Text"].ToString();
                                        //   drgc.Tag = drgc.Text.Substring(0, drgc.Text.IndexOf("-") - 1)

                                        drgc.DrugName = drdrgc[lendrgc]["DrugName"].ToString();
                                        drgc.Dosage = drdrgc[lendrgc]["Dosage"].ToString();
                                        drgc.DrugForm = drdrgc[lendrgc]["DrugForm"].ToString();
                                        drgc.DrugQtyQualifier = drdrgc[lendrgc]["DrugQtyQualifier"].ToString();
                                        drgc.IsNarcotics = Convert.ToInt16(drdrgc[lendrgc]["IsNarcotics"]);
                                        drgc.DDID = Convert.ToInt64(drdrgc[lendrgc]["DDID"]);
                                        drgc.NDCCode = drdrgc[lendrgc]["NDCCode"].ToString();
                                        drgc.Route = drdrgc[lendrgc]["Route"].ToString();
                                        drgc.Frequency = drdrgc[lendrgc]["Frequency"].ToString();
                                        drgc.Duration = drdrgc[lendrgc]["Duration"].ToString();
                                        drgc.GenericName = drdrgc[lendrgc]["GenericName"].ToString();
                                        //   drgc.PracticeFavorites   =Convert.ToBoolean (drdrgc[lendrgc]["PracticeFavorites"]);

                                        if (drdrgc[lendrgc]["PracticeFavorites"].ToString().Trim().Length == 0)
                                        {
                                            drgc.PracticeFavorites = false;
                                        }
                                        else
                                            drgc.PracticeFavorites = Convert.ToBoolean(drdrgc[lendrgc]["PracticeFavorites"]);


                                        drgc.Quantity = drdrgc[lendrgc]["Quantity"].ToString();
                                        drgc.BeersList = drdrgc[lendrgc]["BeersList"].ToString();
                                        if (drdrgc[lendrgc]["IsAllergicDrug"].ToString().Trim().Length == 0)
                                        {
                                            drgc.IsAllergicDrug = false;
                                        }
                                        else
                                            drgc.IsAllergicDrug = Convert.ToBoolean(drdrgc[lendrgc]["IsAllergicDrug"]);


                                        if (drdrgc[lendrgc]["Status"] != null)
                                        {
                                            if (drdrgc[lendrgc]["Status"].ToString() == "True")
                                                drgc.Checked = true;
                                        }

                                        drgc.ImageIndex = 4;
                                        drgc.SelectedImageIndex = 4;

                                        trvdrg.Nodes.Add(drgc);
                                        // hshkey.Add(drgc.Text, "DGS")

                                        StrDrg = StrDrg + "'" + drgc.DrugName.Replace("'", "''") + "'" + ",";

                                    }
                                }
                            }
                        }//End code for adding Drug

                        //code for adding Flowsheet
                        myTreeNode trvflo = new myTreeNode();
                        trvflo.Text = "Flowsheet";
                        trvflo.ImageIndex = 9;
                        trvflo.SelectedImageIndex = 9;
                        trOrder.Nodes.Add(trvflo);

                        if ((dtflo != null))
                        {
                            DataRow[] drflo = dtflo.Select("Order_Id=" + dtorderMain.Rows[lenOrder]["Order_Id"]);
                            if ((drflo.Length > 0))
                            {
                                Int32 flow_id = Convert.ToInt32(drflo[0]["Flowsheet_Id"]);
                                if ((dtfloc != null))
                                {
                                    DataRow[] drfloc = dtfloc.Select("Flowsheet_Id=" + flow_id);
                                    for (int lenfloc = 0; lenfloc <= drfloc.Length - 1; lenfloc++)
                                    {
                                        myTreeNode floc = new myTreeNode();

                                        floc.Text = drfloc[lenfloc]["Text"].ToString();
                                        floc.DrugName = drfloc[lenfloc]["DrugName"].ToString();
                                        floc.Dosage = drfloc[lenfloc]["Dosage"].ToString();
                                        floc.DrugForm = drfloc[lenfloc]["DrugForm"].ToString();
                                        floc.DrugQtyQualifier = drfloc[lenfloc]["DrugQtyQualifier"].ToString();
                                        floc.IsNarcotics = Convert.ToInt16(drfloc[lenfloc]["IsNarcotics"]);
                                        floc.DDID = Convert.ToInt64(drfloc[lenfloc]["flowsheet_id"]);
                                        floc.NDCCode = drfloc[lenfloc]["NDCCode"].ToString();
                                        if (drfloc[lenfloc]["Status"] != null)
                                        {
                                            if (drfloc[lenfloc]["Status"].ToString() == "True")
                                                floc.Checked = true;
                                        }

                                        floc.ImageIndex = 4;
                                        floc.SelectedImageIndex = 4;
                                        trvflo.Nodes.Add(floc);
                                        StrFlo = StrFlo + "'" + floc.Text.Replace("'", "''") + "'" + ",";

                                    }
                                }

                            }
                        }//End code for adding Flowsheet
                    }
                }
                trOrderAssociation.CollapseAll();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting  XML Data For Smart Order     in SmartOrder Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                ds.Dispose();
                //  ObjWord = null;
                this.Cursor = Cursors.Default;
            }
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            try
            {
                trOrderAssociation.Nodes.Clear();
                GloUC_trvOrderSet.Nodes.Clear();
                GloUC_trvOrderSet.SearchBox = false;
                IsClinicRepository = true;
                Panel1.Visible = false;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrSmartOrderflnm + "/" + clsGeneral.gstrSmartOrderflnm + ".xml";

                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while getting Clinic Data For Smart Order     in SmartOrder Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            try
            {
                trOrderAssociation.Nodes.Clear();
                GloUC_trvOrderSet.Nodes.Clear();
                IsClinicRepository = false;
                Panel1.Visible = true;
                GetGlobalSmartOrderList();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while getting Global Data For Smart Order     in SmartOrder Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }


        private void GetGlobalSmartOrderList()
        {
            clsgloCommunity objclsgloCommunity = new clsgloCommunity();
            gloLists.Lists myservice = new gloLists.Lists();
            try
            {

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

                myservice.Url = clsGeneral.Webpath + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                System.Xml.XmlNode node = myservice.GetListCollection();

                foreach (System.Xml.XmlNode xmlnode in node)
                {
                    if (xmlnode.Attributes["BaseType"].Value.ToString() == "1")
                    {
                        if (xmlnode.Attributes["Title"].Value.ToString() == clsGeneral.WebGlobalXmlFolder)
                        {
                            DataTable dt = new DataTable();
                            dt = objclsgloCommunity.GetList(xmlnode.Attributes["Title"].Value.ToString(), clsGeneral.Webpath + "/");

                            for (int lenitem = 0; lenitem <= dt.Rows.Count - 1; lenitem++)
                            {
                                if (dt.Rows[lenitem]["ContentType"].ToString().Trim() == "Folder")
                                {
                                    gloUserControlLibrary.myTreeNode tr = new gloUserControlLibrary.myTreeNode();
                                    string StrName = dt.Rows[lenitem]["title"].ToString();
                                    tr.Text = StrName;
                                    string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrSmartOrderflnm + "/" + clsGeneral.gstrSmartOrderflnm + ".xml";
                                    tr.Tag = fileUrl;
                                    tr.ImageIndex = 10;
                                    tr.SelectedImageIndex = 10;
                                    GloUC_trvOrderSet.Nodes.Add(tr);
                                }
                            }
                        }//End xmlnode.Attributes["Title"]
                    }//End xmlnode.Attributes["BaseType"]
                }//End foreach
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while getting Global SmartList     in SmardOrder Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                objclsgloCommunity = null;
                myservice = null;
            }
        }
        #endregion

        private void GloUC_trvOrderSet_NodeAdded(gloUserControlLibrary.myTreeNode ChildNode)
        {
            clsSmartOrder objclsSmartOrder = new clsSmartOrder();
            DataTable dtAssociation = null;
            try
            {
                // To Get Already Associated Template with Selected CPT
                dtAssociation = objclsSmartOrder.FetchOrderforUpdate(Convert.ToInt64(ChildNode.Tag));
                // If Association found then change the Image of Treenode 
                if ((dtAssociation != null))
                {
                    if (dtAssociation.Rows.Count > 0)
                    {
                        ChildNode.ImageIndex = 5;
                        ChildNode.SelectedImageIndex = 5;
                    }
                    else
                        GloUC_trvOrderSet.Nodes.Remove(ChildNode);//remove Unassociated Order set.
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("Error  while Node Adding     in SmartOrder : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void AddNode(myTreeNode mynode)
        {
            //If mynode.Parent Is trvOrderset.Nodes.Item(0) Then
            //Dim str As String

            long str = 0;
            clsSmartOrder objclsSmartOrder = new clsSmartOrder();
            str = mynode.Key;

            foreach (myTreeNode mytragetnode in trOrderAssociation.Nodes[0].Nodes)
            {
                if (mytragetnode.Key == str)
                {
                    return;
                }
            }

            myTreeNode associatenode = default(myTreeNode);

            associatenode = (myTreeNode)mynode.Clone();
            associatenode.Key = mynode.Key;
            associatenode.Text = mynode.Text;
            associatenode.ImageIndex = 5;
            associatenode.SelectedImageIndex = 5;

            trOrderAssociation.Nodes[0].Nodes.Add(associatenode);
            myTreeNode MyChild = new myTreeNode();
            MyChild.Text = "Labs";
            MyChild.Key = -1;
            MyChild.ImageIndex = 1;
            MyChild.SelectedImageIndex = 1;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "Orders";
            MyChild.Key = -1;
            MyChild.ImageIndex = 2;
            MyChild.SelectedImageIndex = 2;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "Referral Letter";
            MyChild.Key = -1;
            MyChild.ImageIndex = 7;
            MyChild.SelectedImageIndex = 7;
            associatenode.Nodes.Add(MyChild);


            MyChild = new myTreeNode();
            MyChild.Text = "Drugs";
            MyChild.Key = -1;
            MyChild.ImageIndex = 8;
            MyChild.SelectedImageIndex = 8;
            associatenode.Nodes.Add(MyChild);

            MyChild = new myTreeNode();
            MyChild.Text = "FlowSheet";
            MyChild.Key = -1;
            MyChild.ImageIndex = 9;
            MyChild.SelectedImageIndex = 9;
            associatenode.Nodes.Add(MyChild);
            associatenode.Expand();

            DataTable dt = new DataTable();
            dt = objclsSmartOrder.FetchOrderforUpdate(associatenode.Key);
            int i = 0;

            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //add Labs 
                //Lab Test              
                if (dt.Rows[i]["AssociateType"].ToString().ToUpper() == "L")
                {
                    string strLabName = null;

                    //'Instead of using ID use name of the LAB 
                    strLabName = dt.Rows[i]["sAssociateName"].ToString();
                    myTreeNode mytreenode ;
                    mytreenode = new myTreeNode(strLabName, Convert.ToInt64(dt.Rows[i]["nAssociateID"]));

                    mytreenode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                    mytreenode.ImageIndex = 4;
                    mytreenode.SelectedImageIndex = 4;
                    associatenode.Nodes[0].Nodes.Add(mytreenode);
                    associatenode.Nodes[0].Expand();
                    //add orders 
                }
                else if (dt.Rows[i]["AssociateType"].ToString().ToUpper() == "R")
                {
                    string strRadiology = null;

                    //'Instead of using ID use name of the Radiology 
                    strRadiology = dt.Rows[i]["sAssociateName"].ToString();
                    myTreeNode mytreenode ;
                    mytreenode = new myTreeNode(strRadiology, Convert.ToInt64(dt.Rows[i]["nAssociateID"]));

                    mytreenode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                    mytreenode.ImageIndex = 4;
                    mytreenode.SelectedImageIndex = 4;
                    associatenode.Nodes[1].Nodes.Add(mytreenode);
                    associatenode.Nodes[1].Expand();
                    //add  referral letter template 
                }
                else if (dt.Rows[i]["AssociateType"].ToString().ToUpper() == "T")
                {
                    string strTemplate = null;

                    //'Instead of using ID use name of the Template 
                    strTemplate = dt.Rows[i]["sAssociateName"].ToString();
                    myTreeNode mytreenode ;
                    mytreenode = new myTreeNode(strTemplate, Convert.ToInt64(dt.Rows[i]["nAssociateID"]));

                    mytreenode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                    mytreenode.ImageIndex = 4;
                    mytreenode.SelectedImageIndex = 4;
                    associatenode.Nodes[2].Nodes.Add(mytreenode);
                    associatenode.Nodes[2].Expand();
                    //'Add drugs 
                }
                else if (dt.Rows[i]["AssociateType"].ToString().ToUpper() == "D")
                {

                    //'Instead of using ID use name of the Drug 
                    myTreeNode oNode = new myTreeNode();

                    //To display both DrugName and Drug Form
                    //To check whether Drug Form is blank or not
                    if (!string.IsNullOrEmpty(dt.Rows[i]["sDrugForm"].ToString()) & !string.IsNullOrEmpty(dt.Rows[i]["sDosage"].ToString()))
                    {
                        oNode.Text = dt.Rows[i]["sAssociateName"].ToString() + " - " + dt.Rows[i]["sDosage"].ToString() + " - " + dt.Rows[i]["sDrugForm"].ToString();
                    }
                    else if (!string.IsNullOrEmpty(dt.Rows[i]["sDosage"].ToString()))
                    {
                        oNode.Text = dt.Rows[i]["sAssociateName"].ToString() + " - " + dt.Rows[i]["sDosage"].ToString();
                    }
                    else if (!string.IsNullOrEmpty(dt.Rows[i]["sDrugForm"].ToString()))
                    {
                        oNode.Text = dt.Rows[i]["sAssociateName"].ToString() + " - " + dt.Rows[i]["sDrugForm"].ToString();
                    }
                    else if (string.IsNullOrEmpty(dt.Rows[i]["sDrugForm"].ToString()) & string.IsNullOrEmpty(dt.Rows[i]["sDosage"].ToString()))
                    {
                        oNode.Text = dt.Rows[i]["sAssociateName"].ToString();

                    }
                    oNode.Key = Convert.ToInt64(dt.Rows[i]["nAssociateID"]);
                    oNode.Dosage = dt.Rows[i]["sDosage"].ToString();
                    oNode.DrugForm = dt.Rows[i]["sDrugForm"].ToString();
                    oNode.Route = dt.Rows[i]["sRoute"].ToString();
                    oNode.Frequency = dt.Rows[i]["sFrequency"].ToString();
                    oNode.NDCCode = dt.Rows[i]["sNDCCode"].ToString();
                    oNode.IsNarcotics = Convert.ToInt16(dt.Rows[i]["nIsNarcotics"]);
                    oNode.Duration = dt.Rows[i]["sDuration"].ToString();
                    oNode.mpid = Convert.ToInt32(dt.Rows[i]["mpid"]);
                    oNode.DrugQtyQualifier = dt.Rows[i]["sDrugQtyQualifier"].ToString();
                    oNode.DrugName = dt.Rows[i]["sAssociateName"].ToString();
                    oNode.ImageIndex = 4;
                    oNode.SelectedImageIndex = 4;

                    oNode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                    associatenode.Nodes[3].Nodes.Add(oNode);
                    associatenode.Nodes[3].Expand();

                    //FlowSheet             
                }
                else if (dt.Rows[i]["AssociateType"].ToString().ToUpper() == "F")
                {
                    string strFlowshName = null;

                    strFlowshName = Convert.ToString(dt.Rows[i]["sAssociateName"]);
                    myTreeNode mytreenode ;
                    mytreenode = new myTreeNode(strFlowshName, Convert.ToInt64(dt.Rows[i]["nAssociateID"]));
                    mytreenode.ImageIndex = 4;
                    mytreenode.SelectedImageIndex = 4;
                    mytreenode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                    associatenode.Nodes[4].Nodes.Add(mytreenode);
                    associatenode.Nodes[4].Expand();
                }

            }
            //trOrderAssociation.ExpandAll()
            trOrderAssociation.Select();
            associatenode.Collapse();
            //Ensure the newly created node is visible to the user and select it
            associatenode.EnsureVisible();
            trOrderAssociation.SelectedNode = associatenode;
            //CheckAllParentNodes();
        }
    }
}
