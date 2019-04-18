using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Net;
using gloCommunity.Classes;
using gloOffice;
using System.Configuration;
namespace gloCommunity.UserControls
{
    public partial class UCSmartDX : UserControl
    {
        string strAction = "";
        public UCSmartDX(string _strAction)
        {
            InitializeComponent();
            strAction = _strAction;
        }

        Hashtable hshkey = new Hashtable();
        public string StrCPT = string.Empty;
        public string StrTags = string.Empty;
        public string StrLabs = string.Empty;
        public string StrOrd = string.Empty;
        public string StrRefOrdTgsPE = string.Empty;
        public string StrFlo = string.Empty;
        public string StrPE = string.Empty;
        public string StrDrg = string.Empty;
    //    private bool bParentTrigger = true;
     //   private bool bChildTrigger = true;
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
        public DataTable dtcptc = null;
        public DataTable dtpe = null;
        public DataTable dtpec = null;
        public DataTable dtdrg = null;
        public DataTable dtdrgc = null;

        public bool IsClinicRepository = true;//check IsClinicRepository flag while Show SmartDx from SmartDxXML (gloCommunityDownload only)

        private void ShowXMLFIleData(string XmlFileUrl, string UserName, string Password, string Domain)
        {
            this.Cursor = Cursors.WaitCursor;
            HttpWebRequest request = default(HttpWebRequest);
            HttpWebResponse response = null;
            DataSet ds = new DataSet();
            trvsmartdiag.Nodes.Clear();
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

                //string strName = gstrgloEMRStartupPath + "\\Temp\\" + "SPICD9Associationfile.xml";
                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrsmdxflnm + ".xml"; //"SmartDxAssociation.xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();

                //ds.ReadXml(strName);
                try
                {

                    //ds.ReadXml("f:\\Temp\\SPICD9Associationfile.xml");
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrsmdxflnm + ".xml");
                }
                catch (Exception exp)
                {
                    //clsGeneral.UpdateLog("Error  while Showing XMLData   For SmartDX  in SmartDX Usercontrol : " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    //MessageBox.Show(exp.ToString());
                }
                trvsmartdiag.Nodes.Clear();

                dticd9 = ds.Tables["ICD9"];
                //Dim dticd9c As DataTable = ds.Tables("ICD9C")
                dttags = ds.Tables["Tags"];
                dttagsc = ds.Tables["Tagsc"];
                dtlabs = ds.Tables["LabOrders"];
                dtlabsc = ds.Tables["LabOrdersc"];
                dtorder = ds.Tables["Orders"];
                dtorderc = ds.Tables["Ordersc"];
                dtref = ds.Tables["Referralletter"];
                dtrefc = ds.Tables["Referralletterc"];
                dtcpt = ds.Tables["Cpt"];
                dtcptc = ds.Tables["Cptc"];
                dtpe = ds.Tables["PatientEducation"];
                dtpec = ds.Tables["PatientEducationc"];
                dtdrg = ds.Tables["Drugs"];
                dtdrgc = ds.Tables["Drugsc"];
                dtflo = ds.Tables["flowsheet"];
                dtfloc = ds.Tables["flowsheetc"];
                dtfloc1 = ds.Tables["flowsheetc1"];

                StrCPT = string.Empty;
                //StrTags = String.Empty
                StrLabs = string.Empty;
                //StrOrd = String.Empty
                StrRefOrdTgsPE = string.Empty;
                StrFlo = string.Empty;
                // StrPE = String.Empty
                StrDrg = string.Empty;

                myTreeNode tricd9Asso = new myTreeNode();
                tricd9Asso.Text = "ICD9 Association";
                tricd9Asso.ImageIndex = 6;
                tricd9Asso.SelectedImageIndex = 6;
                trvsmartdiag.Nodes.Add(tricd9Asso);
                myTreeNode trIcd9 = default(myTreeNode);
                tricd9Asso.ExpandAll();
                if ((dticd9 != null))
                {
                    for (Int32 lenicd9 = 0; lenicd9 <= dticd9.Rows.Count - 1; lenicd9++)
                    {
                        trIcd9 = new myTreeNode();
                        //trIcd9.ImageIndex = 5
                        trIcd9.Text = dticd9.Rows[lenicd9]["Text"].ToString();
                        trIcd9.ImageIndex = 1;
                        trIcd9.SelectedImageIndex = 1;
                        tricd9Asso.Nodes.Add(trIcd9);
                        myTreeNode trvcpt = new myTreeNode();
                        trvcpt.Text = "CPT";
                        trvcpt.ImageIndex = 2;
                        trvcpt.SelectedImageIndex = 2;
                        trIcd9.Nodes.Add(trvcpt);
                        //' code for adding CPT
                        if ((dtcpt != null))
                        {
                            DataRow[] drcpt = dtcpt.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
                            if ((drcpt.Length > 0))
                            {
                                Int32 cpt_id = Convert.ToInt32(drcpt[0]["cpt_id"]);
                                if ((dtcptc != null))
                                {
                                    DataRow[] drcptc = dtcptc.Select("CPT_Id=" + cpt_id);
                                    for (int lencpt = 0; lencpt <= drcptc.Length - 1; lencpt++)
                                    {
                                        myTreeNode cptc = new myTreeNode();
                                        cptc.Text = drcptc[lencpt]["Text"].ToString();
                                        cptc.Tag = cptc.Text.Substring(0, cptc.Text.IndexOf("-") - 1);
                                        cptc.DrugName = drcptc[lencpt]["DrugName"].ToString();
                                        cptc.Dosage = drcptc[lencpt]["Dosage"].ToString();
                                        cptc.DrugForm = drcptc[lencpt]["DrugForm"].ToString();
                                        cptc.DrugQtyQualifier = drcptc[lencpt]["DrugQtyQualifier"].ToString();
                                        cptc.IsNarcotics = Convert.ToInt16(drcptc[lencpt]["IsNarcotics"]);
                                        cptc.DDID = Convert.ToInt64(drcptc[lencpt]["DDID"]);
                                        cptc.NDCCode = drcptc[lencpt]["NDCCode"].ToString();
                                        if (drcptc[lencpt]["Status"] != null)
                                        {
                                            if (drcptc[lencpt]["Status"].ToString() == "True")
                                                cptc.Checked = true;
                                        }

                                        cptc.ImageIndex = 7;
                                        cptc.SelectedImageIndex = 7;
                                        trvcpt.Nodes.Add(cptc);
                                        StrCPT = StrCPT + "'" + cptc.Tag.ToString().Replace("'", "''") + "'" + ",";
                                    }
                                }

                            }
                        }

                        myTreeNode trvdrg = new myTreeNode();
                        //' code for adding Drug
                        trvdrg.Text = "Drugs";
                        trvdrg.ImageIndex = 3;
                        trvdrg.SelectedImageIndex = 3;
                        trIcd9.Nodes.Add(trvdrg);
                        if ((dtdrg != null))
                        {
                            DataRow[] drdrg = dtdrg.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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
                        }

                        myTreeNode trvpe = new myTreeNode();
                        //' code for adding Patient Education
                        trvpe.Text = "Patient Education";
                        trvpe.ImageIndex = 5;
                        trvpe.SelectedImageIndex = 5;
                        trIcd9.Nodes.Add(trvpe);
                        if ((dtpe != null))
                        {
                            DataRow[] drpe = dtpe.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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
                        }



                        myTreeNode trvtag = new myTreeNode();
                        //' code for adding Tags
                        trvtag.Text = "Tags";
                        trvtag.ImageIndex = 4;
                        trvtag.SelectedImageIndex = 4;
                        trIcd9.Nodes.Add(trvtag);
                        if ((dttags != null))
                        {
                            DataRow[] drtag = dttags.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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
                        }


                        myTreeNode trvflo = new myTreeNode();
                        //' code for adding Flowsheet
                        trvflo.Text = "Flowsheet";
                        trvflo.ImageIndex = 8;
                        trvflo.SelectedImageIndex = 8;
                        trIcd9.Nodes.Add(trvflo);
                        if ((dtflo != null))
                        {
                            DataRow[] drflo = dtflo.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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
                        }

                        myTreeNode trvlabord = new myTreeNode();
                        //' code for adding Lab Order
                        trvlabord.Text = "Lab Orders";
                        trvlabord.ImageIndex = 9;
                        trvlabord.SelectedImageIndex = 9;
                        trIcd9.Nodes.Add(trvlabord);
                        if ((dtlabs != null))
                        {
                            DataRow[] drlabs = dtlabs.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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
                        }

                        myTreeNode trvord = new myTreeNode();
                        //' code for adding Order
                        trvord.Text = "Orders";
                        trvord.ImageIndex = 10;
                        trvord.SelectedImageIndex = 10;
                        trIcd9.Nodes.Add(trvord);
                        if ((dtorder != null))
                        {
                            DataRow[] drord = dtorder.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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

                                        //                StrOrd = StrOrd & "'" & ordc.Text & "'" & ","
                                        StrOrd = StrOrd + "'" + ordc.Text.Replace("'", "''") + "'" + ",";
                                    }
                                }
                            }
                        }

                        myTreeNode trvref = new myTreeNode();
                        //' code for Referral Letter 
                        trvref.Text = "Referral Letter";
                        trvref.ImageIndex = 11;
                        trvref.SelectedImageIndex = 11;
                        trIcd9.Nodes.Add(trvref);
                        if ((dtref != null))
                        {
                            DataRow[] drref = dtref.Select("ICD9_Id=" + dticd9.Rows[lenicd9]["ICD9_Id"]);
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
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Shoing XMLData   For SmartDX  in SmartDX Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                //  trvsmartdiag.ExpandAll();
                ds.Dispose();
                //  ObjWord = null;
                this.Cursor = Cursors.Default;
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

            // Loop through XML response and parse out the value of the
            // Title attribute for each list.
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
                            gloUserControlLibrary.myTreeNode tr = new gloUserControlLibrary.myTreeNode();
                            string StrName = dt.Rows[lenitem]["title"].ToString();
                            tr.Text = StrName;
                            string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrsmdxflnm + "/" + clsGeneral.gstrsmdxflnm + ".xml";

                            tr.Tag = fileUrl;
                            tr.ImageIndex = 13;
                            tr.SelectedImageIndex = 13;

                            if (tr.Text.Contains(".aspx") == false)
                                gloUC_TreeView2.Nodes.Add(tr);

                        }
                        // FillTrv(dt)
                    }
                }
            }
        }

        private void trvsmartdiag_AfterCheck(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //try
            //{

            //    if (bChildTrigger)
            //    {
            //        CheckAllChildren(e.Node, e.Node.Checked);
            //    }
            //    if (bParentTrigger)
            //    {
            //        CheckMyParent(e.Node, e.Node.Checked);
            //    }
            //}
            //catch
            //{

            //}
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            try
            {
                trvsmartdiag.Nodes.Clear();
                gloUC_TreeView2.Nodes.Clear();
                IsClinicRepository = true;
                Panel1.Visible = false;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrsmdxflnm + "/" + clsGeneral.gstrsmdxflnm + ".xml";

                // clsGeneral. 
                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Clicking on Clinic Repository   For SmartDX  in SmartDX Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            try
            {
                trvsmartdiag.Nodes.Clear();
                gloUC_TreeView2.Nodes.Clear();
                IsClinicRepository = false;
                Panel1.Visible = true;
                GetCentralList();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Clicking on Global Repository   For SmartDX  in SmartDX Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void UCSmartDX_Load(object sender, EventArgs e)
        {
            clsGeneral.Webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;

            if (strAction == "Upload")
            {
                //Fetching Associated ICD9
                GetAssociatedICD9();
                //

                //If user action is Upload then add Parent Node. 
                myTreeNode associatenode = new myTreeNode();
                associatenode.Key = -1;
                associatenode.Text = "ICD9 Association";
                associatenode.ImageIndex = 6;
                associatenode.SelectedImageIndex = 6;
                trvsmartdiag.Nodes.Add(associatenode);
                //
            }
            else
            {
                gloUC_TreeView2.SearchBox = false;
                Panel1.Visible = false;
                string fileUrl = clsGeneral.Webpath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrsmdxflnm + "/" + clsGeneral.gstrsmdxflnm + ".xml";
                ShowXMLFIleData(fileUrl, "", "", "");
            }
        }

        private bool GetAssociatedICD9()
        {
            bool IsGetAssociatedICD9 = false;
            clsSmartDx_Upload objclsSmartDx_Upload = new clsSmartDx_Upload();
            DataTable dtAssociation;
            try
            {
                dtAssociation = objclsSmartDx_Upload.FetchAssociatedICD9();
                string strAssociated = "";
                DataView dvFilter = dtAssociation.DefaultView;
                strAssociated = "isCPTassociated = 'true' or isDRUGassociated= 'true' or isTagGassociated='true' or isPatientEducationGassociated='true' or isReferralLetterGassociated = 'true' or isOrdersGassociated = 'true' or isLabOrderGassociated = 'true' or isFlowsheetGassociated = 'true' ";

                if (dtAssociation != null && dtAssociation.Rows.Count > 0)
                    dvFilter.RowFilter = strAssociated;
                dvFilter.Sort = "sDescription DESC";
                DataTable dtSorted = dvFilter.ToTable();

                gloUC_TreeView2.DataSource = dtSorted;
                gloUC_TreeView2.ValueMember = dtSorted.Columns["nICD9ID"].ColumnName;
                gloUC_TreeView2.Tag = dtSorted.Columns[0].ColumnName;
                gloUC_TreeView2.DescriptionMember = dtSorted.Columns["sDescription"].ColumnName;
                gloUC_TreeView2.CodeMember = dtSorted.Columns["ICD9Code"].ColumnName;
                gloUC_TreeView2.DisplayType = gloUserControlLibrary.gloUC_TreeView.enumDisplayType.Code_Description;
                gloUC_TreeView2.ImageIndex = 6;
                gloUC_TreeView2.SelectedImageIndex = 6;
                gloUC_TreeView2.FillTreeView();

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("Error  while Getting Associated ICD9   For SmartDX  in SmartDX Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (objclsSmartDx_Upload != null)
                    objclsSmartDx_Upload = null;
            }
            return IsGetAssociatedICD9;
        }

        private void gloUC_TreeView2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            try
            {
                if (strAction != "Download")
                {
                    gloUserControlLibrary.myTreeNode oNode = (gloUserControlLibrary.myTreeNode)e.Node;
                    if (oNode != null)
                    {
                        //Add Selected Associated node to 'trvsmartdiag'
                        AddNode(oNode);
                        //
                    }
                }

                if (strAction == "Download")
                {
                    ShowXMLFIleData(gloUC_TreeView2.SelectedNode.Tag.ToString(), "", "", "");
                }
            }
            catch (Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Exam, gloAuditTrail.ActivityCategory.SmartDiagnosis, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                //MessageBox.Show(ex.Message, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                //clsGeneral.UpdateLog("Error  while Node Mouse Double Click   For SmartDX  in SmartDX Usercontrol : " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void AddNode(gloUserControlLibrary.myTreeNode oNode)
        {
            string str = null;
            str = oNode.ID.ToString();
            //myTreeNode oTragetNode = default(myTreeNode);
            foreach (myTreeNode oTragetNode in trvsmartdiag.Nodes[0].Nodes)//gloUC_TreeView2.Nodes[0].Nodes
            {
                if (oTragetNode.Key.ToString() == str)
                {
                    return;
                }
            }

            //Add CPT/Drugs/PE to icd9 node
            //trICD9.SelectedNode.Remove()

            myTreeNode associatenode ;
            associatenode = (myTreeNode)trvsmartdiag.Nodes[0];

            //Adding ICD9 code(e.g.948.11) node
            myTreeNode MyChild = new myTreeNode();
            MyChild.Text = oNode.Text;
            MyChild.Key = oNode.ID;
            MyChild.ImageIndex = 1;
            MyChild.SelectedImageIndex = 1;
            associatenode.Nodes.Add(MyChild);
            //

            myTreeNode MysubChild ;
            MysubChild = new myTreeNode();
            MysubChild.Text = "CPT";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 2;
            MysubChild.SelectedImageIndex = 2;
            MyChild.Nodes.Add(MysubChild);

            MysubChild = new myTreeNode();
            MysubChild.Text = "Drugs";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 3;
            MysubChild.SelectedImageIndex = 3;
            MyChild.Nodes.Add(MysubChild);

            MysubChild = new myTreeNode();
            MysubChild.Text = "Patient Education";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 5;
            MysubChild.SelectedImageIndex = 5;
            MyChild.Nodes.Add(MysubChild);

            MysubChild = new myTreeNode();
            MysubChild.Text = "Tags";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 4;
            MysubChild.SelectedImageIndex = 4;
            MyChild.Nodes.Add(MysubChild);

            //'Added Rahul on 20101013
            MysubChild = new myTreeNode();
            MysubChild.Text = "Flowsheet";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 8;
            MysubChild.SelectedImageIndex = 8;
            MyChild.Nodes.Add(MysubChild);

            MysubChild = new myTreeNode();
            MysubChild.Text = "Lab Orders";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 9;
            MysubChild.SelectedImageIndex = 9;
            MyChild.Nodes.Add(MysubChild);

            MysubChild = new myTreeNode();
            MysubChild.Text = "Orders";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 10;
            MysubChild.SelectedImageIndex = 10;
            MyChild.Nodes.Add(MysubChild);

            MysubChild = new myTreeNode();
            MysubChild.Text = "Referral Letter";
            MysubChild.Key = -1;
            MysubChild.ImageIndex = 11;
            MysubChild.SelectedImageIndex = 11;
            MyChild.Nodes.Add(MysubChild);

            //MysubChild = new myTreeNode();
            //MysubChild.Text = "Template";
            //MysubChild.Key = -1;
            //MysubChild.ImageIndex = 12;
            //MysubChild.SelectedImageIndex = 12;
            //MyChild.Nodes.Add(MysubChild);
            //'
            MyChild.Expand();

            DataTable dt = null;
            clsSmartDx_Upload objclsSmartDx_Upload = new clsSmartDx_Upload();
            dt = objclsSmartDx_Upload.FetchICD9forUpdate(MyChild.Key);
            int i = 0;
            for (i = 0; i <= dt.Rows.Count - 1; i++)
            {
                //'Checking Null condition of dt.Rows(i).Item(1) for Fixed Bug Id 6115
                if (!string.IsNullOrEmpty(dt.Rows[i][1].ToString().Trim()))
                {
                    //add cpt items to cpt node in icd9
                    if (dt.Rows[i][2].ToString() == "C")
                    {
                        //CPT Description    CPTID

                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        //'associatenode.Nodes.Item(0).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        myTreeNode tempnode = default(myTreeNode);
                        tempnode = new myTreeNode();
                        tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                        tempnode.Text = dt.Rows[i][1].ToString();
                        ///''Description
                        tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);
                        ///''CPT ID
                        MyChild.Nodes[0].Nodes.Add(tempnode);
                        MyChild.Nodes[0].Expand();

                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011

                        //add drug items to drug node in icd9
                        //Drugname + Dosage                              DrugID              Drugname        
                    }
                    else if (dt.Rows[i][2].ToString() == "D")
                    {
                        //associatenode.Nodes.Item(1).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1) & "-" & dt.Rows(i).Item(3), dt.Rows(i).Item(0), CType(dt.Rows(i).Item(1), String)))

                        //For De-Normalization '\\Added by suraj 20090128
                        myTreeNode tempnode = default(myTreeNode);
                        tempnode = new myTreeNode();
                        //tempnode.Key = oNode.Key
                        //SHUBHANGI 20100805 ASSOCIATE THE ID OF DRUG TO THAT NODE AS KEY
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
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        //Code Added by Mayuri:20091009
                        //To check whether Drugform is blank
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

                        MyChild.Nodes[1].Nodes.Add(tempnode);
                        MyChild.Nodes[1].Expand();

                        //add PE items to PE node in icd9
                    }
                    else if (dt.Rows[i][2].ToString() == "P")
                    {
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        myTreeNode tempnode = default(myTreeNode);
                        tempnode = new myTreeNode();
                        tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                        tempnode.Text = dt.Rows[i][1].ToString();
                        ///''Description
                        tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);
                        ///''PE ID

                        MyChild.Nodes[2].Nodes.Add(tempnode);
                        MyChild.Nodes[2].Expand();

                        //'associatenode.Nodes.Item(2).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        //add Tags items to Tags node in icd9
                    }
                    else if (dt.Rows[i][2].ToString() == "T")
                    {
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
                        myTreeNode tempnode = default(myTreeNode);
                        tempnode = new myTreeNode();
                        tempnode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);
                        tempnode.Text = dt.Rows[i][1].ToString();
                        ///''Description
                        tempnode.Key = Convert.ToInt64(dt.Rows[i][0]);
                        ///''Tag ID
                        string strnodename = dt.Rows[i][1].ToString();
                        //'Description
                        int ind = strnodename.LastIndexOf("-");
                        if (ind > -1)
                        {
                            strnodename = strnodename.Substring(0, ind);
                        }
                        tempnode.NodeName = strnodename;
                        //tempnode.NodeName = dt.Rows(i).Item(1) '''''Description

                        MyChild.Nodes[3].Nodes.Add(tempnode);
                        MyChild.Nodes[3].Expand();

                        //'associatenode.Nodes.Item(3).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011

                        //'Added Rahul on 20101013
                    }
                    else if (dt.Rows[i][2].ToString() == "F")
                    {
                        // associatenode.Nodes.Item(4).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        myTreeNode cptmynode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                        cptmynode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                        MyChild.Nodes[4].Nodes.Add(cptmynode);
                        MyChild.Nodes[4].Expand();
                    }
                    else if (dt.Rows[i][2].ToString() == "L")
                    {
                        //  associatenode.Nodes.Item(5).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        myTreeNode cptmynode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                        cptmynode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                        MyChild.Nodes[5].Nodes.Add(cptmynode);
                        MyChild.Nodes[5].Expand();
                    }
                    else if (dt.Rows[i][2].ToString() == "O")
                    {
                        // associatenode.Nodes.Item(6).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))

                        myTreeNode cptmynode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                        cptmynode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                        MyChild.Nodes[6].Nodes.Add(cptmynode);
                        MyChild.Nodes[6].Expand();
                        //associatenode.Nodes[6].Nodes.Add(cptmynode);
                        //associatenode.Nodes[6].Expand();
                    }
                    else if (dt.Rows[i][2].ToString() == "R")
                    {
                        // associatenode.Nodes.Item(7).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        myTreeNode cptmynode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                        cptmynode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                        MyChild.Nodes[7].Nodes.Add(cptmynode);
                        MyChild.Nodes[7].Expand();
                    }
                    else if (dt.Rows[i][2].ToString() == "TM")
                    {
                        // associatenode.Nodes.Item(8).Nodes.Add(New myTreeNode(dt.Rows(i).Item(1), dt.Rows(i).Item(0)))
                        myTreeNode cptmynode = new myTreeNode(dt.Rows[i][1].ToString(), Convert.ToInt64(dt.Rows[i][0]));
                        cptmynode.Checked = Convert.ToBoolean(dt.Rows[i]["Status"]);

                        MyChild.Nodes[8].Nodes.Add(cptmynode);
                        MyChild.Nodes[8].Expand();

                        //add Tags items to Tags node 
                        //'
                    }

                }
                //'End Checking Null condition of dt.Rows(i).Item(1)

            }
            //trICD9Association.ExpandAll()
            gloUC_TreeView2.Select();
            // code added for removing template tag from  TRICD9Association 30 january
            // associatenode.Nodes[8].Remove();
            //treeindex = -1
            //End If
            associatenode.Collapse();
            //Ensure the newly created node is visible to the user and select it
            MyChild.EnsureVisible();
            trvsmartdiag.SelectedNode = associatenode;
            //treeindex = mynode.Index
            ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
            //CheckAllParentNodes();
            ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes  - as on 20101011
        }

        private void gloUC_TreeView2_Click(object sender, EventArgs e)
        {

        }

        private void gloUC_TreeView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        //private void CheckAllParentNodes()
        //{
        //    ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        //    bool innerchildflag = false;
        //    bool outerchildflag = false;
        //    bool parentflag = false;

        //    foreach (TreeNode ptn in gloUC_TreeView2.Nodes[0].Nodes)
        //    {
        //        foreach (TreeNode otherptn in ptn.Nodes)
        //        {
        //            foreach (TreeNode ootherptn in otherptn.Nodes)
        //            {
        //                if (ootherptn.Checked == false)
        //                {
        //                    innerchildflag = true;
        //                    break; // TODO: might not be correct. Was : Exit For
        //                }
        //            }
        //            if (innerchildflag == false & otherptn.Nodes.Count > 0)
        //            {
        //                otherptn.Checked = true;


        //            }
        //            else
        //            {
        //                outerchildflag = true;
        //            }
        //            innerchildflag = false;
        //        }

        //        if (outerchildflag == false & ptn.Nodes.Count > 0)
        //        {
        //            ptn.Checked = true;

        //        }
        //        else
        //        {
        //            parentflag = true;
        //        }
        //        outerchildflag = false;
        //    }
        //    ///'''''''''''' Added by Ujwala - Smart Diagnosis Changes Integration - as on 20101011
        //}
        //private void CheckAllChildren(TreeNode tn, Boolean bCheck)
        //{
        //    foreach (TreeNode ctn in tn.Nodes)
        //    {
        //        ctn.Checked = bCheck;
        //        CheckAllChildren(ctn, bCheck);
        //    }
        //}

        //private void CheckMyParent(TreeNode tn, Boolean bCheck)
        //{
        //    if (tn == null)
        //    {
        //        return;
        //    }
        //    if (tn.Parent == null)
        //    {
        //        return;
        //    }
        //    bChildTrigger = false;
        //    bParentTrigger = false;

        //    if (bCheck)
        //    {
        //        bool bNodeFound = false;
        //        foreach (TreeNode _Node in tn.Parent.Nodes)
        //        {
        //            if (_Node.Checked == false)
        //            {
        //                tn.Parent.Checked = false;
        //                bNodeFound = true;
        //                break; // TODO: might not be correct. Was : Exit For
        //            }
        //        }
        //        if (bNodeFound == false)
        //        {
        //            tn.Parent.Checked = true;
        //        }
        //    }
        //    else
        //    {
        //        tn.Parent.Checked = bCheck;
        //    }

        //    CheckMyParent(tn.Parent, bCheck);
        //    bParentTrigger = true;
        //    bChildTrigger = true;
        //}
    }
}
