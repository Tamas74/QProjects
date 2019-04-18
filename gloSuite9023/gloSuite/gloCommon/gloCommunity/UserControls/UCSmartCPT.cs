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
using System.Collections;
using System.Configuration;

namespace gloCommunity.UserControls
{
    public partial class UCSmartCPT : UserControl
    {
        string strAction = "";

        //Smart CPT Download variables
        Hashtable hshkey = new Hashtable();
        public string StrICD9 = string.Empty;
        public string StrTags = string.Empty;
        public string StrLabs = string.Empty;
        public string StrOrd = string.Empty;
        public string StrRefOrdTgsPE = string.Empty;
        public string StrFlo = string.Empty;
        public string StrPE = string.Empty;
        public string StrDrg = string.Empty;
    //    private bool bParentTrigger = true;
    //    private bool bChildTrigger = true;
        public DataTable dtflo = null;
        public DataTable dtfloc = null;
        public DataTable dtfloc1 = null;
        public DataTable dticd9 = null;
        public DataTable dttags = null;
        public DataTable dttagsc = null;
        public DataTable dtlabs = null;
        public DataTable dtlabsc = null;
        public DataTable dtorder = null;
        public DataTable dtorderc = null;
        public DataTable dtref = null;
        public DataTable dtrefc = null;
        public DataTable dtcpt = null;
        public DataTable dticd9c = null;
        public DataTable dtpe = null;
        public DataTable dtpec = null;
        public DataTable dtdrg = null;
        public DataTable dtdrgc = null;
        //Download variables End

        public bool IsClinicRepository = true;//check IsClinicRepository flag while Show SmartCPT from SmartCPTXML (gloCommunityDownload only)

        public UCSmartCPT(string _strAction)
        {
            strAction = _strAction;
            InitializeComponent();
        }

        private void UCSmartCPT_Load(object sender, EventArgs e)
        {
            if (strAction == "Upload")
            {
                GetCPTAssociation();

                //If user action is Upload then add Parent Node. 
                myTreeNode associatenode = new myTreeNode();
                associatenode.Key = -1;
                associatenode.Text = "CPT Association";
                associatenode.ImageIndex = 1;
                associatenode.SelectedImageIndex = 1;
                trvCPTAssociation.Nodes.Add(associatenode);
            }
            else
            {
                GloUC_trvCPT.SearchBox = false;
                pnl_btnICD9.Visible = false;
                Panel1.Visible = false;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrSmartCPTflnm + "/" + clsGeneral.gstrSmartCPTflnm + ".xml";

                ShowXMLFIleData(fileUrl, "", "", "");
            }
        }

        #region "Share SmartCPT Upload"

        private bool GetCPTAssociation()
        {
            bool IsGetCPT = false;
            DataTable dtAssociation = new DataTable();
            clsSmartCPT oclsSmartCPT = new clsSmartCPT();

            try
            {
                dtAssociation = oclsSmartCPT.FetchassociatedCPT();
                string strAssociated = "";
                DataView dv = new DataView();
                dv = dtAssociation.DefaultView;

                strAssociated = "isICD9associated = 'true' or isDRUGassociated= 'true' or isTagGassociated='true' or isPatientEducationGassociated='true' or isReferralLetterGassociated = 'true' or isOrdersGassociated = 'true' or isLabOrderGassociated = 'true' or isFlowsheetGassociated='true' ";


                if (dtAssociation.Rows.Count > 0)
                {
                    dv.RowFilter = strAssociated;
                }
                DataTable dt = new DataTable();
                dt = dv.ToTable();


                //Bind filtrated data to control
                GloUC_trvCPT.DataSource = dtAssociation;
                GloUC_trvCPT.ValueMember = dtAssociation.Columns["nCPTID"].ColumnName;
                GloUC_trvCPT.Tag = dtAssociation.Columns["nCPTID"].ColumnName;
                GloUC_trvCPT.DescriptionMember = dtAssociation.Columns["sDescription"].ColumnName;
                GloUC_trvCPT.CodeMember = dtAssociation.Columns["sCPTCode"].ColumnName;
                GloUC_trvCPT.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description;
                GloUC_trvCPT.ImageIndex = 1;
                GloUC_trvCPT.SelectedImageIndex = 1;
                GloUC_trvCPT.FillTreeView();
                IsGetCPT = true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while Getting CPT Association    For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                if (oclsSmartCPT != null)
                    oclsSmartCPT = null;
                if (dtAssociation != null)
                {
                    dtAssociation.Dispose();
                    dtAssociation = null;
                }
            }
            return IsGetCPT;
        }

        private void GloUC_trvCPT_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (strAction != "Download")
                {
                    gloUserControlLibrary.myTreeNode oNode = (gloUserControlLibrary.myTreeNode)e.Node;
                    myTreeNode mynode = new myTreeNode();

                    mynode.Key = oNode.ID;
                    mynode.Text = oNode.Text;
                    if ((oNode != null))
                    {
                        AddNode(mynode);
                    }
                }
                else
                {
                   ShowXMLFIleData(GloUC_trvCPT.SelectedNode.Tag.ToString(), "", "", "");
                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Formgallery, gloAuditTrail.ActivityCategory.Setup, gloAuditTrail.ActivityType.Add, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("Error  while Node Double Click    For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void AddNode(myTreeNode mynode)
        {
            this.Cursor = Cursors.WaitCursor;
            string str = null;
            str = mynode.Key.ToString();

            try
            {
                foreach (myTreeNode mytragetnode in trvCPTAssociation.Nodes[0].Nodes)
                {
                    if (mytragetnode.Key.ToString().Trim() == str.Trim())
                    {
                        return;
                    }
                }

                //Add ICD9/Drugs/PE/Tags to CPT node
                //trvCPT.SelectedNode.Remove()
                clsSmartCPT oclsSmartCPT = new clsSmartCPT();
                myTreeNode associatenode = default(myTreeNode);

                associatenode = (myTreeNode)mynode.Clone();
                associatenode.Key = mynode.Key;
                associatenode.Text = mynode.Text;

                associatenode.NodeName = mynode.Text;
                associatenode.ImageIndex = 1;
                associatenode.SelectedImageIndex = 1;


                //'For De-Normalization
                //Dim tempNode As New myTreeNode
                //tempNode.DrugName = mynode.DrugName
                //tempNode.Dosage = mynode.Dosage
                //tempNode.DrugForm = mynode.DrugForm
                //For De-Normalization

                associatenode.ImageIndex = 1;
                associatenode.SelectedImageIndex = 1;

                trvCPTAssociation.Nodes[0].Nodes.Add(associatenode);
                myTreeNode MyChild = new myTreeNode();
                MyChild.Text = "ICD9";
                MyChild.Key = -1;
                MyChild.ImageIndex = 2;
                MyChild.SelectedImageIndex = 2;
                associatenode.Nodes.Add(MyChild);

                MyChild = new myTreeNode();
                MyChild.Text = "Drugs";
                MyChild.Key = -1;
                MyChild.ImageIndex = 3;
                MyChild.SelectedImageIndex = 3;
                associatenode.Nodes.Add(MyChild);

                MyChild = new myTreeNode();
                MyChild.Text = "Patient Education";
                MyChild.Key = -1;
                MyChild.ImageIndex = 5;
                MyChild.SelectedImageIndex = 5;
                associatenode.Nodes.Add(MyChild);

                MyChild = new myTreeNode();
                MyChild.Text = "Tags";
                MyChild.Key = -1;
                MyChild.ImageIndex = 4;
                MyChild.SelectedImageIndex = 4;
                associatenode.Nodes.Add(MyChild);

                //'Added Rahul For new Association (Referral Letter,Order,LabOrder,Flowsheet) on 20101014
                MyChild = new myTreeNode();
                MyChild.Text = "Flowsheet";
                MyChild.Key = -1;
                MyChild.ImageIndex = 10;
                MyChild.SelectedImageIndex = 10;
                associatenode.Nodes.Add(MyChild);


                MyChild = new myTreeNode();
                MyChild.Text = "Lab Orders";
                MyChild.Key = -1;
                MyChild.ImageIndex = 11;
                MyChild.SelectedImageIndex = 11;
                associatenode.Nodes.Add(MyChild);

                MyChild = new myTreeNode();
                MyChild.Text = "Orders";
                MyChild.Key = -1;
                MyChild.ImageIndex = 12;
                MyChild.SelectedImageIndex = 12;
                associatenode.Nodes.Add(MyChild);

                MyChild = new myTreeNode();
                MyChild.Text = "Referral Letter";
                MyChild.Key = -1;
                MyChild.ImageIndex = 13;
                MyChild.SelectedImageIndex = 13;
                associatenode.Nodes.Add(MyChild);
                associatenode.Expand();
                //'End

                DataTable dt = null;
                dt = oclsSmartCPT.FetchICD9forUpdate(associatenode.Key);
                int i = 0;
                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {

                    //For Default tag change
                    ///'''''''''''''''If condition  - To skip blank node addition
                    if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString().Trim()))
                    {

                        //add cpt items to cpt node in icd9
                        //CPT Description    CPTID
                        if (dt.Rows[i][2].ToString() == "I")
                        {
                            myTreeNode tempnode = default(myTreeNode);
                            tempnode = new myTreeNode();
                            tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            tempnode.Text = dt.Rows[i][1].ToString();
                            ///''Description
                            tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);
                            ///''CPT ID
                            associatenode.Nodes[0].Nodes.Add(tempnode);
                            associatenode.Nodes[0].Expand();
                            //add drug items to drug node in icd9
                            //Drugname + Dosage   DrugID  Drugname        
                        }
                        else if (dt.Rows[i][2].ToString() == "D")
                        {
                            //For De-Normalization 
                            myTreeNode tempnode = default(myTreeNode);
                            tempnode = new myTreeNode();
                            //tempnode.Key = mynode.Key
                            tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);

                            tempnode.DrugName = dt.Rows[i][1].ToString();
                            tempnode.Dosage = dt.Rows[i][3].ToString();
                            tempnode.DrugForm = dt.Rows[i][4].ToString();

                            tempnode.Route = dt.Rows[i][5].ToString();
                            tempnode.Frequency = dt.Rows[i][6].ToString();
                            tempnode.NDCCode = dt.Rows[i][7].ToString();
                            tempnode.IsNarcotics = Convert.ToInt16(dt.Rows[i][8]);
                            tempnode.Duration = dt.Rows[i][9].ToString();
                            tempnode.mpid = Convert.ToInt32(dt.Rows[i][10]);
                            tempnode.DrugQtyQualifier = dt.Rows[i][11].ToString();


                            tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                            //To display both DrugName and DrugForm
                            //To check whether drugform is blank or not
                            if (!string.IsNullOrEmpty(dt.Rows[i][4].ToString()) & !string.IsNullOrEmpty(dt.Rows[i][3].ToString()))
                            {
                                tempnode.Text = tempnode.DrugName + " - " + tempnode.Dosage + " - " + tempnode.DrugForm;
                            }
                            else if (!string.IsNullOrEmpty(dt.Rows[i][3].ToString()))
                            {
                                tempnode.Text = tempnode.DrugName + " - " + tempnode.Dosage;
                            }
                            else if (!string.IsNullOrEmpty(dt.Rows[i][4].ToString()))
                            {
                                tempnode.Text = tempnode.DrugName + " - " + tempnode.DrugForm;
                            }
                            else
                            {
                                tempnode.Text = tempnode.DrugName + tempnode.Dosage + tempnode.DrugForm;
                            }
                            associatenode.Nodes[1].Nodes.Add(tempnode);
                            associatenode.Nodes[1].Expand();

                            //add PE items to PE node in icd9

                        }
                        else if (dt.Rows[i][2].ToString() == "P")
                        {
                            myTreeNode tempnode = default(myTreeNode);
                            tempnode = new myTreeNode();
                            tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            tempnode.Text = dt.Rows[i][1].ToString();
                            ///''Description
                            tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);
                            ///''PE ID
                            associatenode.Nodes[2].Nodes.Add(tempnode);
                            associatenode.Nodes[2].Expand();

                            //add Tags items to Tags node in icd9
                        }
                        else if (dt.Rows[i][2].ToString() == "T")
                        {
                            myTreeNode tempnode = default(myTreeNode);
                            tempnode = new myTreeNode();
                            tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            tempnode.Text = dt.Rows[i][1].ToString();
                            ///''Description
                            tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);
                            ///''Tags ID
                            string strnodename = dt.Rows[i][1].ToString();
                            //'Description
                            int ind = strnodename.LastIndexOf("-");
                            if (ind > -1)
                            {
                                strnodename = strnodename.Substring(0, ind);
                            }
                            tempnode.NodeName = strnodename;


                            // tempnode.NodeName = dt.Rows(i).Item(1) '''''Description
                            associatenode.Nodes[3].Nodes.Add(tempnode);
                            associatenode.Nodes[3].Expand();
                        }
                        else if (dt.Rows[i][2].ToString() == "F")
                        {
                            myTreeNode flownode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                            flownode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            associatenode.Nodes[4].Nodes.Add(flownode);
                            associatenode.Nodes[4].Expand();
                            // associatenode.Nodes.Item(4).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                            //add Tags items to Tags node in icd9
                        }
                        else if (dt.Rows[i][2].ToString() == "L")
                        {
                            myTreeNode labnode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                            labnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            associatenode.Nodes[5].Nodes.Add(labnode);
                            associatenode.Nodes[5].Expand();
                            // associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        }
                        else if (dt.Rows[i][2].ToString() == "O")
                        {
                            myTreeNode ordnode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                            ordnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            associatenode.Nodes[6].Nodes.Add(ordnode);
                            associatenode.Nodes[6].Expand();
                            //associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                            //add Tags items to Tags node in icd9
                        }
                        else if (dt.Rows[i][2].ToString() == "R")
                        {
                            myTreeNode reffnode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                            reffnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                            associatenode.Nodes[7].Nodes.Add(reffnode);
                            associatenode.Nodes[7].Expand();
                            // associatenode.Nodes.Item(7).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                            //'End
                        }
                    }

                }
                //trvCPTAssociation.ExpandAll()
                trvCPTAssociation.Select();
                associatenode.Collapse();
                //Ensure the newly created node is visible to the user and select it
                associatenode.EnsureVisible();
                trvCPTAssociation.SelectedNode = associatenode;
                
                //CheckAllParentNodes();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while AddNode    For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        #endregion

        #region "Share SmartCPT Download"

        private void ShowXMLFIleData(string XmlFileUrl, string UserName, string Password, string Domain)
        {
            this.Cursor = Cursors.WaitCursor;
            HttpWebRequest request = default(HttpWebRequest);
            HttpWebResponse response = null;
            DataSet ds = new DataSet();
            trvCPTAssociation.Nodes.Clear();
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

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrSmartCPTflnm + ".xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();

                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrSmartCPTflnm + ".xml");
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.ToString());
                    //clsGeneral.UpdateLog("Error  while Showing XML Data    For SmartCPT  in SmartCPT Usercontrol : " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                trvCPTAssociation.Nodes.Clear();

                dticd9 = ds.Tables["ICD9"];
                dttags = ds.Tables["Tags"];
                dttagsc = ds.Tables["Tagsc"];
                dtlabs = ds.Tables["LabOrders"];
                dtlabsc = ds.Tables["LabOrdersc"];
                dtorder = ds.Tables["Orders"];
                dtorderc = ds.Tables["Ordersc"];
                dtref = ds.Tables["Referralletter"];
                dtrefc = ds.Tables["Referralletterc"];
                dtcpt = ds.Tables["Cpt"];
                dticd9c = ds.Tables["ICD9c"];
                dtpe = ds.Tables["PatientEducation"];
                dtpec = ds.Tables["PatientEducationc"];
                dtdrg = ds.Tables["Drugs"];
                dtdrgc = ds.Tables["Drugsc"];
                dtflo = ds.Tables["flowsheet"];
                dtfloc = ds.Tables["flowsheetc"];
                dtfloc1 = ds.Tables["flowsheetc1"];

                StrICD9 = string.Empty;
                StrLabs = string.Empty;
                StrRefOrdTgsPE = string.Empty;
                StrFlo = string.Empty;
                StrDrg = string.Empty;

                myTreeNode trCPTAsso = new myTreeNode();
                trCPTAsso.Text = "CPT Association";
                trCPTAsso.ImageIndex = 1;
                trCPTAsso.SelectedImageIndex = 1;
                trvCPTAssociation.Nodes.Add(trCPTAsso);
                myTreeNode trCPT = default(myTreeNode);
                if ((dtcpt != null))
                {
                    for (Int32 lenicd9 = 0; lenicd9 <= dtcpt.Rows.Count - 1; lenicd9++)
                    {
                        trCPT = new myTreeNode();
                        trCPT.Text = dtcpt.Rows[lenicd9]["Text"].ToString();
                        trCPT.ImageIndex = 1;
                        trCPT.SelectedImageIndex = 1;
                        trCPTAsso.Nodes.Add(trCPT);

                        myTreeNode trvICD9 = new myTreeNode();
                        trvICD9.Text = "ICD9";
                        trvICD9.ImageIndex = 2;
                        trvICD9.SelectedImageIndex = 2;
                        trCPT.Nodes.Add(trvICD9);

                        //code for adding ICD9
                        if ((dtcpt != null))
                        {
                            DataRow[] drICD9 = dticd9.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
                            if ((drICD9.Length > 0))
                            {
                                Int32 ICD9_id = Convert.ToInt32(drICD9[0]["ICD9_id"]);
                                if ((dticd9c != null))
                                {
                                    DataRow[] dricd9c = dticd9c.Select("ICD9_id=" + ICD9_id);
                                    for (int lencpt = 0; lencpt <= dricd9c.Length - 1; lencpt++)
                                    {
                                        myTreeNode ICD9c = new myTreeNode();
                                        ICD9c.Text = dricd9c[lencpt]["Text"].ToString();
                                        ICD9c.Tag = ICD9c.Text.Substring(0, ICD9c.Text.IndexOf("-") - 1);
                                        ICD9c.DrugName = dricd9c[lencpt]["DrugName"].ToString();
                                        ICD9c.Dosage = dricd9c[lencpt]["Dosage"].ToString();
                                        ICD9c.DrugForm = dricd9c[lencpt]["DrugForm"].ToString();
                                        ICD9c.DrugQtyQualifier = dricd9c[lencpt]["DrugQtyQualifier"].ToString();
                                        ICD9c.IsNarcotics = Convert.ToInt16(dricd9c[lencpt]["IsNarcotics"]);
                                        ICD9c.DDID = Convert.ToInt64(dricd9c[lencpt]["DDID"]);
                                        ICD9c.NDCCode = dricd9c[lencpt]["NDCCode"].ToString();
                                        if (dricd9c[lencpt]["Status"] != null)
                                        {
                                            if (dricd9c[lencpt]["Status"].ToString() == "True")
                                                ICD9c.Checked = true;
                                        }

                                        ICD9c.ImageIndex = 7;
                                        ICD9c.SelectedImageIndex = 7;

                                        trvICD9.Nodes.Add(ICD9c);
                                        StrICD9 = StrICD9 + "'" + ICD9c.Tag.ToString().Replace("'", "''") + "'" + ",";
                                    }
                                }

                            }
                        }
                        //End code for adding ICD9

                        //code for adding Drug
                        myTreeNode trvdrg = new myTreeNode();
                        trvdrg.Text = "Drugs";
                        trvdrg.ImageIndex = 3;
                        trvdrg.SelectedImageIndex = 3;
                        trCPT.Nodes.Add(trvdrg);
                        if ((dtdrg != null))
                        {
                            DataRow[] drdrg = dtdrg.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
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

                                        drgc.ImageIndex = 7;
                                        drgc.SelectedImageIndex = 7;

                                        trvdrg.Nodes.Add(drgc);
                                        // hshkey.Add(drgc.Text, "DGS")

                                        StrDrg = StrDrg + "'" + drgc.DrugName.Replace("'", "''") + "'" + ",";

                                    }
                                }
                            }
                        }//End code for adding Drug

                        //code for adding Patient Education
                        myTreeNode trvpe = new myTreeNode();
                        trvpe.Text = "Patient Education";
                        trvpe.ImageIndex = 5;
                        trvpe.SelectedImageIndex = 5;
                        trCPT.Nodes.Add(trvpe);

                        if ((dtpe != null))
                        {
                            DataRow[] drpe = dtpe.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
                            if ((drpe.Length > 0))
                            {
                                Int32 pe_id = Convert.ToInt32(drpe[0]["patienteducation_id"]);
                                if ((dtpec != null))
                                {
                                    DataRow[] drpec = dtpec.Select("patienteducation_Id=" + pe_id);
                                    for (int lenpec = 0; lenpec <= drpec.Length - 1; lenpec++)
                                    {
                                        myTreeNode trpec = new myTreeNode();
                                        trpec.Text = drpec[lenpec]["Text"].ToString();
                                        //    trpec.Tag = trpec.Text.Substring(0, trpec.Text.IndexOf("-") - 1)

                                        trpec.DrugName = drpec[lenpec]["DrugName"].ToString();
                                        trpec.Dosage = drpec[lenpec]["Dosage"].ToString();
                                        trpec.DrugForm = drpec[lenpec]["DrugForm"].ToString();
                                        trpec.DrugQtyQualifier = drpec[lenpec]["DrugQtyQualifier"].ToString();
                                        trpec.IsNarcotics = Convert.ToInt16(drpec[lenpec]["IsNarcotics"]);
                                        trpec.DDID = Convert.ToInt64(drpec[lenpec]["DDID"]);
                                        trpec.NDCCode = drpec[lenpec]["NDCCode"].ToString();
                                        if (drpec[lenpec]["Status"] != null)
                                        {
                                            if (drpec[lenpec]["Status"].ToString() == "True")
                                                trpec.Checked = true;
                                        }

                                        trpec.ImageIndex = 7;
                                        trpec.SelectedImageIndex = 7;
                                        trvpe.Nodes.Add(trpec);
                                        //  hshkey.Add(trpec.Text, "PE")
                                        StrRefOrdTgsPE = StrRefOrdTgsPE + "'" + trpec.Text.Replace("'", "''") + "'" + ",";

                                    }
                                }
                            }
                        }//End code for adding Patient Education


                        //code for adding Tags
                        myTreeNode trvtag = new myTreeNode();
                        trvtag.Text = "Tags";
                        trvtag.ImageIndex = 4;
                        trvtag.SelectedImageIndex = 4;
                        trCPT.Nodes.Add(trvtag);

                        if ((dttags != null))
                        {
                            DataRow[] drtag = dttags.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
                            if ((drtag.Length > 0))
                            {
                                Int32 Tags_id = Convert.ToInt32(drtag[0]["Tags_Id"]);
                                if ((dttagsc != null))
                                {
                                    DataRow[] drtagsc = dttagsc.Select("Tags_Id=" + Tags_id);
                                    for (int lentagsc = 0; lentagsc <= drtagsc.Length - 1; lentagsc++)
                                    {
                                        myTreeNode tagsc = new myTreeNode();
                                        tagsc.Text = drtagsc[lentagsc]["Text"].ToString();
                                        //  tagsc.Tag = tagsc.Text.Substring(0, tagsc.Text.IndexOf("-") - 1)

                                        tagsc.DrugName = drtagsc[lentagsc]["DrugName"].ToString();
                                        tagsc.Dosage = drtagsc[lentagsc]["Dosage"].ToString();
                                        tagsc.DrugForm = drtagsc[lentagsc]["DrugForm"].ToString();
                                        tagsc.DrugQtyQualifier = drtagsc[lentagsc]["DrugQtyQualifier"].ToString();
                                        tagsc.IsNarcotics = Convert.ToInt16(drtagsc[lentagsc]["IsNarcotics"]);
                                        tagsc.DDID = Convert.ToInt64(drtagsc[lentagsc]["DDID"]);
                                        tagsc.NDCCode = drtagsc[lentagsc]["NDCCode"].ToString();
                                        if (drtagsc[lentagsc]["Status"] != null)
                                        {
                                            if (drtagsc[lentagsc]["Status"].ToString() == "True")
                                                tagsc.Checked = true;
                                        }

                                        tagsc.ImageIndex = 7;
                                        tagsc.SelectedImageIndex = 7;
                                        trvtag.Nodes.Add(tagsc);
                                        // hshkey.Add(tagsc.Text, "TGS")
                                        //                StrTags &= StrTags & "'" & tagsc.Text & "'" & ","
                                        StrRefOrdTgsPE = StrRefOrdTgsPE + "'" + tagsc.Text.Replace("'", "''") + "'" + ",";

                                    }
                                }

                            }
                        }//End code for adding Tags

                        //code for adding Flowsheet
                        myTreeNode trvflo = new myTreeNode();
                        trvflo.Text = "Flowsheet";
                        trvflo.ImageIndex = 10;
                        trvflo.SelectedImageIndex = 10;
                        trCPT.Nodes.Add(trvflo);

                        if ((dtflo != null))
                        {
                            DataRow[] drflo = dtflo.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
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

                                        floc.ImageIndex = 7;
                                        floc.SelectedImageIndex = 7;
                                        trvflo.Nodes.Add(floc);
                                        StrFlo = StrFlo + "'" + floc.Text.Replace("'", "''") + "'" + ",";

                                    }
                                }

                            }
                        }//End code for adding Flowsheet

                        //code for adding Lab Order
                        myTreeNode trvlabord = new myTreeNode();
                        trvlabord.Text = "Lab Orders";
                        trvlabord.ImageIndex = 11;
                        trvlabord.SelectedImageIndex = 11;
                        trCPT.Nodes.Add(trvlabord);

                        if ((dtlabs != null))
                        {
                            DataRow[] drlabs = dtlabs.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
                            if ((drlabs.Length > 0))
                            {
                                Int32 lab_id = Convert.ToInt32(drlabs[0]["LabOrders_Id"]);
                                if ((dtlabsc != null))
                                {
                                    DataRow[] drlabc = dtlabsc.Select("LabOrders_Id=" + lab_id);
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

                                        labc.ImageIndex = 7;
                                        labc.SelectedImageIndex = 7;
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
                        trvord.ImageIndex = 12;
                        trvord.SelectedImageIndex = 12;
                        trCPT.Nodes.Add(trvord);

                        if ((dtorder != null))
                        {
                            DataRow[] drord = dtorder.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
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

                                        ordc.ImageIndex = 7;
                                        ordc.SelectedImageIndex = 7;
                                        trvord.Nodes.Add(ordc);

                                        StrOrd = StrOrd + "'" + ordc.Text.Replace("'", "''") + "'" + ",";
                                    }
                                }
                            }
                        }//End code for adding Order

                        //code for Referral Letter 
                        myTreeNode trvref = new myTreeNode();
                        trvref.Text = "Referral Letter";
                        trvref.ImageIndex = 13;
                        trvref.SelectedImageIndex = 13;
                        trCPT.Nodes.Add(trvref);

                        if ((dtref != null))
                        {
                            DataRow[] drref = dtref.Select("CPT_Id=" + dtcpt.Rows[lenicd9]["CPT_Id"]);
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

                                        refc.ImageIndex = 7;
                                        refc.SelectedImageIndex = 7;
                                        trvref.Nodes.Add(refc);
                                        // hshkey.Add(refc.Text, "REF")
                                        //                StrRef = StrRef & "'" & refc.Text & "'" & ","
                                        StrRefOrdTgsPE = StrRefOrdTgsPE + "'" + refc.Text.Replace("'", "''") + "'" + ",";

                                    }
                                }

                            }
                        }//End code for Referral Letter
                    }
                }
                trvCPTAssociation.CollapseAll();
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Showing  SmartCPT Data  For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {   
                ds.Dispose();
                //  ObjWord = null;
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            try
            {
                IsClinicRepository = true;
                Panel1.Visible = false;
                trvCPTAssociation.Nodes.Clear();
                GloUC_trvCPT.Nodes.Clear();
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrSmartCPTflnm + "/" + clsGeneral.gstrSmartCPTflnm + ".xml";

                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while Clicking on Clinic SmartCPT button   For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            try
            {
                IsClinicRepository = false;
                Panel1.Visible = true;
                trvCPTAssociation.Nodes.Clear();
                GloUC_trvCPT.Nodes.Clear();
                GetGlobalSmartCPTList();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //clsGeneral.UpdateLog("Error  while Clicking on Global SmartCPT button   For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void GetGlobalSmartCPTList()
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
                                    string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrSmartCPTflnm + "/" + clsGeneral.gstrSmartCPTflnm + ".xml";
                                    tr.Tag = fileUrl;
                                    tr.ImageIndex = 14;
                                    tr.SelectedImageIndex = 14;
                                    GloUC_trvCPT.Nodes.Add(tr);
                                }
                            }
                        }//End xmlnode.Attributes["Title"]
                    }//End xmlnode.Attributes["BaseType"]
                }//End foreach
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Clicking on Global SmartCPT List   For SmartCPT  in SmartCPT Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                objclsgloCommunity = null;
                myservice = null;
            }
        }

        #endregion

        

        
    }
}
