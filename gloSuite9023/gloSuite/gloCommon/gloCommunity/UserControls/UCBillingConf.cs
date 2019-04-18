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
using C1.Win.C1FlexGrid;
using System.Configuration;
namespace gloCommunity.UserControls
{
    public partial class UCBillingConf : UserControl
    {
        clsBillingconf objclsblcf = null;
        string stract = "";
        string WebPath = "";
        public UCBillingConf(string strAction)
        {
            InitializeComponent();
            //  btnLocal.Text = clsGeneral.ClinicRepository;
            // btnCentral.Text = clsGeneral.GlobalRepository;


            objclsblcf = new clsBillingconf();
            stract = strAction;

            if (strAction == "Download")
            {
                FillLocalClinicData();
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = false;
                pnltls.Visible = false;
            }

            foreach (TabPage tp in tabblconf.TabPages)
            {
                switch (tp.Text)
                {

                    case "Specialty":
                        tabblconf.TabPages.Remove(tp);
                        break;
                }
            }

            EventArgs evt = new EventArgs();
            tabblconf_SelectedIndexChanged(tabblconf, evt);
        }

        private void FillLocalClinicData()
        {
            try
            {
                // trvblconf.SearchBox = false;

                //   pnl_btnLocal.Dock = DockStyle.Top;
                this.Cursor = Cursors.WaitCursor;

                WebPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;

                string fileUrl = WebPath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrblconfflnm + "/" + clsGeneral.gstrblconfflnm + ".xml";

                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch (Exception ex)
            {

                //clsGeneral.UpdateLog("gloCommunity:-Error while Clinic Billing Configuration Data Reteriving from glocommunity site");  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                this.Cursor = Cursors.Default;
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
                byte[] read = new byte[50000];

                int count = 1;
                int len = 0;
                while ((count != 0))
                {
                    count = s.Read(read, len, 50000);
                    len += count;
                    Array.Resize(ref read, len + 50000);
                }
                Array.Resize(ref read, len);

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrblconfflnm + ".xml"; //"SmartDxAssociation.xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();
                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrblconfflnm + ".xml");
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(exp.ToString());
                    //clsGeneral.UpdateLog("glocomm Error While Showing XML Data in  Usercontrol Billing Configuration: " + ex.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }

                DataTable showdata = ds.Tables[0];
                fillDownloadedData(showdata);

            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Showing XML Data in  Usercontrol Billing Configuration: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                ds.Dispose();

            }

        }

        DataTable dtICd = null;
  //      DataTable dtCPT = null;
        DataTable dtMod = null;
        DataTable dtSpec = null;
        DataTable dtplntype = null;
        DataTable dtptrln = null;
        private void fillDownloadedData(DataTable showdata)
        {
     //       DataTable dtICD = null;
            DataTable dtCPT = null;
       //    DataTable dtStat = null;
            DataTable dtCat = null;
            bool bl = false;
            try
            {
                dtICd = showdata.Clone();
                dtCPT = showdata.Clone();
                dtMod = showdata.Clone();
                dtSpec = showdata.Clone();
                dtplntype = showdata.Clone();
                dtptrln = showdata.Clone();
                dtCat = showdata.Clone();
                DataRow[] dricd = showdata.Select("[Billing Category]='ICD9'");
                DataRow[] drcpt = showdata.Select("[Billing Category]='CPT'");
                DataRow[] drmod = showdata.Select("[Billing Category]='Modifier'");
                DataRow[] drspec = showdata.Select("[Billing Category]='Specialty'");//Specility
                DataRow[] drpln = showdata.Select("[Billing Category]='PlanType'");
                DataRow[] drptrln = showdata.Select("[Billing Category]='PatientRelation'");
                DataRow[] drcat = showdata.Select("[Billing Category]='CategoryType'");

                for (int len = 0; len < drcat.Length; len++)
                {

                    drcat[len]["Select"] = bl;
                    dtCat.ImportRow(drcat[len]);
                }

                for (int len = 0; len < dricd.Length; len++)
                {

                    dricd[len]["Select"] = bl;
                    dtICd.ImportRow(dricd[len]);
                }



                //for (int len = 0; len < dricd.Length; len++)
                //{

                //    dricd[len]["Select"] = bl;
                //    dtICd.ImportRow(dricd[len]);
                //}



                for (int len = 0; len < drcpt.Length; len++)
                {
                    drcpt[len]["Select"] = bl;
                    dtCPT.ImportRow(drcpt[len]);
                }
                ////Added for fix bug id 29626 on 20120710
                if (dtCPT != null && dtCPT.Rows.Count > 0)
                {
                    dtCPT.Columns["cptcode"].SetOrdinal(1);
                    dtCPT.Columns["description"].SetOrdinal(2);
                    dtCPT.Columns["statement description"].SetOrdinal(3);
                    dtCPT.Columns["specialty"].SetOrdinal(4);
                    dtCPT.Columns["category"].SetOrdinal(5);
                    dtCPT.Columns["code type"].SetOrdinal(6);
                    dtCPT.Columns["modifier1 code"].SetOrdinal(7);
                    dtCPT.Columns["modifier2 code"].SetOrdinal(8);
                    dtCPT.Columns["modifier3 code"].SetOrdinal(9);
                    dtCPT.Columns["modifier4 code"].SetOrdinal(10);
                    dtCPT.Columns["cpt drug"].SetOrdinal(11);
                    dtCPT.Columns["ndccode"].SetOrdinal(12);
                    dtCPT.Columns["charges"].SetOrdinal(13);
                    dtCPT.Columns["allowed"].SetOrdinal(14);
                    dtCPT.Columns["revenue code"].SetOrdinal(15);
                }
                ////

                for (int len = 0; len < drmod.Length; len++)
                {
                    drmod[len]["Select"] = bl;
                    dtMod.ImportRow(drmod[len]);
                }


                for (int len = 0; len < drspec.Length; len++)
                {
                    drspec[len]["Select"] = bl;
                    dtSpec.ImportRow(drspec[len]);
                }

                for (int len = 0; len < drpln.Length; len++)
                {
                    drpln[len]["Select"] = bl;
                    dtplntype.ImportRow(drpln[len]);
                }


                for (int len = 0; len < drptrln.Length; len++)
                {
                    drptrln[len]["Select"] = bl;
                    dtptrln.ImportRow(drptrln[len]);
                }

                flxICD.DataSource = dtICd.Copy();
                flxCPT.DataSource = dtCPT.Copy();
                flxMod.DataSource = dtMod.Copy();
                flxSpec.DataSource = dtSpec.Copy();
                flxPln.DataSource = dtplntype.Copy();
                flxPatr.DataSource = dtptrln.Copy();
                flxCat.DataSource = dtCat.Copy();
                if (flxICD.DataSource != null)
                {
                    flxICD.Cols[0].DataType = typeof(bool);
                    // MakeGridEditableFalse(flxICD); 
                }


                if (flxCPT.DataSource != null)
                {
                    flxCPT.Cols[0].DataType = typeof(bool);
                    //  MakeGridEditableFalse(flxCPT); 

                }


                if (flxMod.DataSource != null)
                {
                    flxMod.Cols[0].DataType = typeof(bool);
                    //   MakeGridEditableFalse(flxMod); 
                }


                if (flxSpec.DataSource != null)
                {
                    flxSpec.Cols[0].DataType = typeof(bool);
                    // MakeGridEditableFalse(flxSpec); 
                }
                if (flxPln.DataSource != null)
                {
                    flxPln.Cols[0].DataType = typeof(bool);
                    // MakeGridEditableFalse(flxPln); 
                }

                if (flxPatr.DataSource != null)
                {
                    flxPatr.Cols[0].DataType = typeof(bool);
                    //  MakeGridEditableFalse(flxPatr); 
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Filling Data  in  Usercontrol Billing Configuration: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {

                dtICd.Dispose();
                dtCPT.Dispose();
                dtMod.Dispose();
                dtSpec.Dispose();
                dtplntype.Dispose();
                dtptrln.Dispose();
                dtICd = null;
                dtCPT = null;
                dtMod = null;
                dtSpec = null;
                dtplntype = null;
                dtptrln = null;

            }
        }


        //private void MakeGridEditableFalse(C1.Win.C1FlexGrid.C1FlexGrid objflxgrd)
        //{
        //    if (objflxgrd.DataSource != null)
        //    {
        //        for (int col = 1; col < objflxgrd.Cols.Count; col++)
        //        {
        //            objflxgrd.Cols[col].AllowEditing = false;     
        //        }
        //    }
        //}

        private void tabblconf_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList arrdata = new ArrayList();

            try
            {

                if (stract == "Upload")
                {

                    switch (tabblconf.SelectedTab.Text)
                    {
                        case "ICD9":
                            if (flxICD.DataSource == null)
                            {
                                flxICD.DataSource = objclsblcf.GetICD9();
                                flxICD.Cols[0].AllowSorting = false;
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("specialty");
                                arrdata.Add("status");
                                arrdata.Add("select");
                                SetGridColumnVisibility(flxICD, arrdata);
                                flxICD.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                //   flxTemplates.SetCellCheck(j, flxTemplates.Cols[COL_SELECT].Index, CheckEnum.Unchecked);
                            }
                            break;

                        case "CPT":
                            if (flxCPT.DataSource == null)
                            {
                                arrdata.Clear();
                                arrdata.Add("cptcode");
                                arrdata.Add("description");
                                arrdata.Add("statement description");
                                arrdata.Add("specialty");
                                arrdata.Add("category");
                                arrdata.Add("code type");
                                arrdata.Add("modifier1 code");
                                arrdata.Add("modifier2 code");
                                arrdata.Add("modifier3 code");
                                arrdata.Add("modifier4 code");
                                arrdata.Add("cpt drug");
                                arrdata.Add("ndc code");
                                arrdata.Add("charges");
                                arrdata.Add("allowed");
                                arrdata.Add("revenue code");
                                arrdata.Add("select");
                                flxCPT.DataSource = objclsblcf.GetCPT();
                                flxCPT.Cols[0].AllowSorting = false;

                                SetGridColumnVisibility(flxCPT, arrdata);
                                flxCPT.Cols["modifier1 code"].Caption = "Modifier1";
                                flxCPT.Cols["modifier2 code"].Caption = "Modifier2";
                                flxCPT.Cols["modifier3 code"].Caption = "Modifier3";
                                flxCPT.Cols["modifier4 code"].Caption = "Modifier4";
                                flxCPT.SetCellCheck(0, 0, CheckEnum.Unchecked);

                            }
                            break;

                        case "Modifiers":
                            if (flxMod.DataSource == null)
                            {
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("select");
                                flxMod.DataSource = objclsblcf.GetModifiers();
                                SetGridColumnVisibility(flxMod, arrdata);
                                flxMod.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxMod.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Category":
                            if (flxCat.DataSource == null)
                            {
                                arrdata.Clear();
                                arrdata.Add("select");
                                arrdata.Add("description");
                                arrdata.Add("category type");
                                flxCat.DataSource = objclsblcf.GetCategory();
                                SetGridColumnVisibility(flxCat, arrdata);
                                flxCat.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxCat.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Specialty":
                            if (flxSpec.DataSource == null)
                            {
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("taxonomy code");
                                arrdata.Add("taxonomy description");
                                arrdata.Add("classification");
                                arrdata.Add("select");
                                flxSpec.DataSource = objclsblcf.GetSpeciality();
                                SetGridColumnVisibility(flxSpec, arrdata);
                                flxSpec.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxSpec.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Patient Relationship":
                            if (flxPatr.DataSource == null)
                            {
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("select");
                                flxPatr.DataSource = objclsblcf.GetPatRel();
                                SetGridColumnVisibility(flxPatr, arrdata);
                                flxPatr.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxPatr.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Plan Type":
                            if (flxPln.DataSource == null)
                            {
                                arrdata.Clear();
                                arrdata.Add("type code");
                                arrdata.Add("type description");
                                arrdata.Add("select");
                                flxPln.DataSource = objclsblcf.GetPlanType();
                                SetGridColumnVisibility(flxPln, arrdata);
                                flxPln.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxPln.Cols[0].AllowSorting = false;

                            }
                            break;
                    }
                }
                else
                {


                    switch (tabblconf.SelectedTab.Text)
                    {
                        case "ICD9":
                            if (flxICD.DataSource != null)
                            {
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("specialty");
                                arrdata.Add("status");
                                arrdata.Add("select");
                                SetGridColumnVisibility(flxICD, arrdata);
                                flxICD.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxICD.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "CPT":
                            if (flxCPT.DataSource != null)
                            {
                                arrdata.Clear();
                                arrdata.Add("cptcode");
                                arrdata.Add("description");
                                arrdata.Add("statement description");
                                arrdata.Add("specialty");
                                arrdata.Add("category");
                                arrdata.Add("code type");
                                arrdata.Add("modifier1 code");
                                arrdata.Add("modifier2 code");
                                arrdata.Add("modifier3 code");
                                arrdata.Add("modifier4 code");
                                arrdata.Add("cpt drug");
                                arrdata.Add("ndccode");
                                arrdata.Add("charges");
                                arrdata.Add("allowed");
                                arrdata.Add("revenue code");
                                arrdata.Add("select");
                                SetGridColumnVisibility(flxCPT, arrdata);
                                flxCPT.Cols["modifier1 code"].Caption = "Modifier1";
                                flxCPT.Cols["modifier2 code"].Caption = "Modifier2";
                                flxCPT.Cols["modifier3 code"].Caption = "Modifier3";
                                flxCPT.Cols["modifier4 code"].Caption = "Modifier4";
                                flxCPT.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxCPT.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Modifiers":
                            if (flxMod.DataSource != null)
                            {
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("select");

                                SetGridColumnVisibility(flxMod, arrdata);
                                flxMod.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxMod.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Category":
                            if (flxCat.DataSource != null)
                            {
                                arrdata.Clear();
                                arrdata.Add("select");
                                arrdata.Add("description");

                                SetGridColumnVisibility(flxCat, arrdata);
                                flxCat.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxCat.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Specialty":
                            if (flxSpec.DataSource != null)
                            {
                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("taxonomy code");
                                arrdata.Add("taxonomy description");
                                arrdata.Add("classification");
                                arrdata.Add("select");
                                SetGridColumnVisibility(flxSpec, arrdata);
                                flxSpec.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxSpec.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Patient Relationship":
                            if (flxPatr.DataSource != null)
                            {

                                arrdata.Clear();
                                arrdata.Add("code");
                                arrdata.Add("description");
                                arrdata.Add("select");
                                SetGridColumnVisibility(flxPatr, arrdata);
                                flxPatr.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxPatr.Cols[0].AllowSorting = false;

                            }
                            break;

                        case "Plan Type":
                            if (flxPln.DataSource != null)
                            {
                                arrdata.Clear();
                                arrdata.Add("type code");
                                arrdata.Add("type description");
                                arrdata.Add("select");
                                SetGridColumnVisibility(flxPln, arrdata);
                                flxPln.SetCellCheck(0, 0, CheckEnum.Unchecked);
                                flxPln.Cols[0].AllowSorting = false;

                            }
                            break;
                    }
                }

                if (stract == "Upload")
                {
                    if (flxSpec.DataSource == null)
                    {
                        flxSpec.DataSource = objclsblcf.GetSpeciality();
                    }


                }

                if (stract == "Download")
                {
                    if (flxSpec.DataSource == null)
                    {
                        //arrdata.Clear();
                        //arrdata.Add("code");
                        //arrdata.Add("description");
                        //arrdata.Add("taxonomy code");
                        //arrdata.Add("taxonomy description");
                        //arrdata.Add("classification");
                        //arrdata.Add("select");
                        //SetGridColumnVisibility(flxSpec, arrdata);
                        // flxSpec.SetCellCheck(0, 0, CheckEnum.Unchecked);
                        //flxSpec.Cols[0].AllowSorting = false;

                    }
                }
            }

            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Tab Selected Change  in  Usercontrol Billing Configuration: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
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

        private void UCBillingConf_Load(object sender, EventArgs e)
        {
            //if (stract == "Download")
            //    panel1.Visible = true;
            //else
            panel1.Visible = false;
        }

        private void ClearGridData()
        {

            try
            {
                //flxCat.Clear();
                flxCat.DataSource = null;
                //flxPln.Clear();
                flxPln.DataSource = null;
                //flxPatr.Clear();
                flxPatr.DataSource = null;
                //flxICD.Clear();
                flxICD.DataSource = null;
                //flxCPT.Clear();
                flxCPT.DataSource = null;
                //flxMod.Clear();
                flxMod.DataSource = null;
                //flxSpec.Clear();
                flxSpec.DataSource = null;



                if (flxCat.Rows.Count >= 2)
                {
                    flxCat.Rows.RemoveRange(1, flxCat.Rows.Count - 1);
                }

                if (flxPatr.Rows.Count >= 2)
                {
                    flxPatr.Rows.RemoveRange(1, flxPatr.Rows.Count - 1);
                }
                if (flxPln.Rows.Count >= 2)
                {
                    flxPln.Rows.RemoveRange(1, flxPln.Rows.Count - 1);
                }

                if (flxICD.Rows.Count >= 2)
                {
                    flxICD.Rows.RemoveRange(1, flxICD.Rows.Count - 1);
                }


                if (flxCPT.Rows.Count >= 2)
                {
                    flxCPT.Rows.RemoveRange(1, flxCPT.Rows.Count - 1);
                }

                if (flxMod.Rows.Count >= 2)
                {
                    flxMod.Rows.RemoveRange(1, flxMod.Rows.Count - 1);
                }


                if (flxSpec.Rows.Count >= 2)
                {
                    flxSpec.Rows.RemoveRange(1, flxSpec.Rows.Count - 1);
                }
            }
            catch
            {
            }


        }

        private void GetCentralList()
        {
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
                                ////Added if condition for show only Specialty
                                if (dt.Rows[lenitem]["ContentType"].ToString().Trim() == "Folder")
                                {
                                    gloUserControlLibrary.myTreeNode tr = new gloUserControlLibrary.myTreeNode();
                                    string StrName = dt.Rows[lenitem]["title"].ToString();
                                    tr.Text = StrName;
                                    string fileUrl = WebPath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrblconfflnm + "/" + clsGeneral.gstrblconfflnm + ".xml";

                                    tr.Tag = fileUrl;
                                    if (tr.Text.Contains(".aspx") == false)
                                        trvblconf.Nodes.Add(tr);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exp)
            {

                //clsGeneral.UpdateLog("gloCommunity:-Error while Central Billing Configuration Data Reteriving from  glocommunity site");  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        //private void trvblconf_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    this.Cursor = Cursors.WaitCursor;
        //    ClearGridData();
        //    try
        //    {
        //        if (stract == "Download")
        //        {
        //            ShowXMLFIleData(trvblconf.SelectedNode.Tag.ToString(), "", "", "");
        //            EventArgs evt = new EventArgs();
        //            tabblconf_SelectedIndexChanged(tabblconf, evt);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clsGeneral.UpdateLog("glocomm Error While Node Mouse Double click in  Usercontrol Billing Configuration: " + ex.Message.ToString());  

        //    }
        //    this.Cursor = Cursors.Default;
        //}

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                ClearGridData();
                panel1.Visible = false;
                trvblconf.Nodes.Clear();
                FillLocalClinicData();
                ////Added for fix bug id 29623 on 20120707
                if (tabblconf.SelectedIndex == 0)
                    tabblconf_SelectedIndexChanged(null, null);
                else
                    tabblconf.SelectedIndex = 0;
                ////End
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Clinic Repository click in  Usercontrol Billing Configuration: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            try
            {
                ClearGridData();
                trvblconf.Nodes.Clear();
                //    pnl_btnLocal.Dock = DockStyle.Bottom;
                //   pnl_btnCentral.Dock = DockStyle.Top;
                panel1.Visible = true;
                GetCentralList();
                trvblconf.ExpandAll();
                ////Added for fix bug id 29623 on 20120707
                tabblconf.SelectedIndex = 0;
                ////End
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Global Repository click in  Usercontrol Billing Configuration: " + ex.Message.ToString());  
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void flxICD_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }


        private void SetCellCheck(C1FlexGrid flxgrd, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                flxgrd.Cols[0].AllowSorting = false;
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

        private void flxCPT_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxMod_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxCat_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxSpec_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxPatr_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void flxPln_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e);
        }

        private void trvblconf_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ClearGridData();
            try
            {
                if (stract == "Download")
                {
                    ShowXMLFIleData(e.Node.Tag.ToString(), "", "", "");
                    EventArgs evt = new EventArgs();
                    tabblconf_SelectedIndexChanged(tabblconf, evt);

                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Node Mouse Double click in  Usercontrol Billing Configuration: " + ex.Message.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }




    }
}
