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
using Janus.Windows.Schedule;
using Janus.Windows.Common;
using C1.Win.C1FlexGrid;
using System.Configuration;
namespace gloCommunity.UserControls
{
   
    public partial class UCAppointConf : UserControl
    {
        clsApptconf objclsappconf = null;
        ArrayList arrdata = new ArrayList();
        string strAction = "";
        string WebPath = "";
        TreeNode TempNode = null;
        public   bool blnappconfgclinic = true; 
        public UCAppointConf()
        {
            InitializeComponent();
          
        }

        public UCAppointConf(string strAct)
        {
            InitializeComponent();
            objclsappconf = new clsApptconf();
            strAction = strAct;

            foreach (TabPage tp in tabblconf.TabPages)
            {
                switch (tp.Text)
                {

                    case "Appointment Status":
                        tabblconf.TabPages.Remove(tp);
                        break;
                    case "Location":
                        tabblconf.TabPages.Remove(tp);
                        break;
                    case "Department":
                        tabblconf.TabPages.Remove(tp);
                        break;
                    case "Resource":
                        tabblconf.TabPages.Remove(tp);
                        break;
                }
            }

            if (strAction == "Download")
            {
                pnlleft.Visible = false;
                try
                {
                    FillLocalClinicData();
                }
                catch(Exception ex)
                {
                  //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.Clinic, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Appointment Configure Downloaded", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Appointment Configure Reteriving Data");  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
            }
            else
            {
                pnltls.Visible = false;   
                pnlleft.Visible = false;
                TempNode = new TreeNode();
                TempNode.Text = "Template";  
                trvAppointmentBook.Nodes.Add(TempNode);  
            }
         EventArgs evt=new EventArgs() ;
         tabblconf_SelectedIndexChanged(tabblconf, evt);
        }

        private void FillLocalClinicData()
        {
            try
            {
               // trvappconf.SearchBox = false;
                WebPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                string fileUrl = WebPath + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrappconfflnm + "/" + clsGeneral.gstrappconfflnm + ".xml";

                ShowXMLFIleData(fileUrl, "", "", "");
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Appointment Configure Reteriving Data");  
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

                string strName = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrblconfflnm + ".xml"; //"SmartDxAssociation.xml";

                strName = clsGeneral.GenerateFile(read, strName);

                s.Close();
                response.Close();
                try
                {
                    ds.ReadXml(gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrblconfflnm + ".xml");
                }
                catch (Exception exp)
                {
                    //MessageBox.Show(exp.ToString(), "Appointment Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //clsGeneral.UpdateLog("glocomm Error While Showing XMLData  in  Usercontrol Appointment Configuration Configuration: " + exp.Message.ToString());  
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, exp.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    // MessageBox.Show(exp.ToString());
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
                DataTable showdata = ds.Tables[0];
                fillDownloadedData(showdata);

            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Showing XMLData  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                ds.Dispose();
                this.Cursor = Cursors.Default;
            }

        }

        public   DataTable dtTemplate = null;
        private void fillDownloadedData(DataTable showdata)
        {

            DataTable dtResource = null;
            DataTable dtFollowup = null;
            DataTable dtProblem = null;
            DataTable dtDept = null;
            DataTable dtApptStat = null;
            DataTable dtApptType = null;
            DataTable dtApptBlk = null;
            DataTable dtLoc = null;
           
            bool bl = false;
            try
            {
                dtResource = showdata.Clone();
                dtFollowup = showdata.Clone();
                dtProblem = showdata.Clone();
                dtDept = showdata.Clone();
                dtApptStat = showdata.Clone();
                dtApptType = showdata.Clone();
                dtApptBlk = showdata.Clone();
                dtLoc = showdata.Clone();
                dtTemplate = showdata.Clone();  
                DataRow[] drres = showdata.Select("[AppointmentCategory]='Resource'");
                DataRow[] drfoll = showdata.Select("[AppointmentCategory]='FollowUp'");
                DataRow[] drprb = showdata.Select("[AppointmentCategory]='Problem'");
                DataRow[] drdept = showdata.Select("[AppointmentCategory]='Department'");
                DataRow[] drapptst = showdata.Select("[AppointmentCategory]='AppointmentStatus'");
                DataRow[] drappt = showdata.Select("[AppointmentCategory]='AppointmentType'");
                DataRow[] drapptblk = showdata.Select("[AppointmentCategory]='AppointmentBlock'");
                DataRow[] drloc = showdata.Select("[AppointmentCategory]='Location'");
                DataRow[] drtemp = showdata.Select("[AppointmentCategory]='Template'");
                for (int len = 0; len < drres.Length; len++)
                {
                    drres[len]["Select"] = bl;
                    dtResource.ImportRow(drres[len]);
                }

                for (int len = 0; len < drfoll.Length; len++)
                {
                    drfoll[len]["Select"] = bl;
                    dtFollowup.ImportRow(drfoll[len]);
                }

                for (int len = 0; len < drprb.Length; len++)
                {
                    drprb[len]["Select"] = bl;
                    dtProblem.ImportRow(drprb[len]);
                }


                for (int len = 0; len < drdept.Length; len++)
                {
                    drdept[len]["Select"] = bl;
                    dtDept.ImportRow(drdept[len]);
                }



                for (int len = 0; len < drapptst.Length; len++)
                {
                    drapptst[len]["Select"] = bl;
                    dtApptStat.ImportRow(drapptst[len]);
                }

                for (int len = 0; len < drappt.Length; len++)
                {
                    drappt[len]["Select"] = bl;
                    drappt[len]["ColorCode1"] = drappt[len]["Color Codes"]; 
                    dtApptType.ImportRow(drappt[len]);
                }

                for (int len = 0; len < drapptblk.Length; len++)
                {
                    drapptblk[len]["Select"] = bl;
                    dtApptBlk.ImportRow(drapptblk[len]);
                }


                for (int len = 0; len < drloc.Length; len++)
                {
                    drloc[len]["Select"] = bl;
                    dtLoc.ImportRow(drloc[len]);
                }

                trvAppointmentBook.Nodes.Clear();
                TreeNode TempNode = new TreeNode();
                TempNode.Text = "Template";
                trvAppointmentBook.Nodes.Add(TempNode);    
                for(int len=0;len<drtemp.Length;len++)
                {
                    drtemp[len]["Select"] = bl;
                    dtTemplate.ImportRow(drtemp[len]);   
                    if (drtemp[len]["ParentAppointmentTemplate"].ToString().Trim() == "")
                    {
                        //TreeNode childnode = new TreeNode();
                       if( TempNode.Nodes.ContainsKey(drtemp[len]["AppointmentTemplates"].ToString())==false)  
                        TempNode.Nodes.Add(drtemp[len]["AppointmentTemplates"].ToString()); 
                    }
                }
                trvAppointmentBook.ExpandAll(); 
                flxRes.DataSource = dtResource.Copy();
                flxfollup.DataSource = dtFollowup.Copy();
                flxPrb.DataSource = dtProblem.Copy();
                flxAppt.DataSource = dtApptType.Copy();
                flxApptstat.DataSource = dtApptStat.Copy();
                flxApptblk.DataSource = dtApptBlk.Copy();
                flxLoc.DataSource = dtLoc.Copy();
                flxDept.DataSource = dtDept.Copy();    
                if (flxRes.DataSource != null)
                {
                    flxRes.Cols[0].DataType = typeof(bool);
                }
                if (flxfollup.DataSource != null)
                {
                    flxfollup.Cols[0].DataType = typeof(bool);
                }
                if (flxPrb.DataSource != null)
                {
                    flxPrb.Cols[0].DataType = typeof(bool);
                }

                if (flxAppt.DataSource != null)
                {
                    flxAppt.Cols[0].DataType = typeof(bool);
                    for (int i = 0; i <= (flxAppt.Rows.Count- 1); i++)
                    {
                        C1.Win.C1FlexGrid.CellStyle cStyle;
                        C1.Win.C1FlexGrid.CellRange rgBubbleValues = flxAppt.GetCellRange(i+1 ,37);
                        string mystring = "BubbleValues" + i.ToString();
                       // cStyle = flxAppt.Styles.Add("BubbleValues"+i.ToString() );
                        try
                        {
                            if (flxAppt.Styles.Contains(mystring))
                            {
                                cStyle = flxAppt.Styles[mystring];
                            }
                            else
                            {
                                cStyle = flxAppt.Styles.Add(mystring);

                            }

                        }
                        catch
                        {
                            cStyle = flxAppt.Styles.Add(mystring);

                        }
                        cStyle.BackColor = Color.FromArgb(Convert.ToInt32(flxAppt.Rows[i+1]["ColorCode1"]));  // Color.Blue;
                        flxAppt.SetData(i + 1, 37, "     ");
                        rgBubbleValues.Style = cStyle;
                    
                    }
                    flxAppt.Cols["ColorCode1"].Caption = "Color Code";  
                }
                if (flxApptstat.DataSource != null)
                {
                    flxApptstat.Cols[0].DataType = typeof(bool);
                }

                if (flxApptblk.DataSource != null)
                {
                    flxApptblk.Cols[0].DataType = typeof(bool);
                }

                if (flxLoc.DataSource != null)
                {
                    flxLoc.Cols[0].DataType = typeof(bool);
                }

                if (flxDept.DataSource != null)
                {
                    flxDept.Cols[0].DataType = typeof(bool);
                }
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Filldownloaddata in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                dtResource.Dispose();
                dtFollowup.Dispose();
                dtProblem.Dispose();
                dtDept.Dispose();
                dtApptStat.Dispose();
                dtApptType.Dispose();
                dtApptBlk.Dispose();
                dtLoc.Dispose();
                dtDept.Dispose();  
                dtResource = null;
                dtFollowup = null;
                dtProblem = null;
                dtDept = null;
                dtApptStat = null;
                dtApptType = null;
                dtApptBlk = null;
                dtLoc = null;
                dtDept = null; 
                EventArgs evt = new EventArgs();
                tabblconf_SelectedIndexChanged(tabblconf, evt);
            }
        }

        private void UCAppointConf_Load(object sender, EventArgs e)
        {
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
        
        private void tabblconf_SelectedIndexChanged(object sender, EventArgs e)
        {
            arrdata.Clear();
            if (strAction == "Upload")
            {
                switch (tabblconf.SelectedTab.Text)
                {
                    case "Resource":

                        if (flxRes.DataSource == null)
                        {
                            flxRes.DataSource = objclsappconf.GetResource();
                            arrdata.Add("code");
                            arrdata.Add("resource name");
                            arrdata.Add("user name");
                            SetGridColumnVisibility(flxRes, arrdata);
                        }
                        break;

                    case "Appointment Block Type":

                        if (flxApptblk.DataSource == null)
                        {
                            flxApptblk.DataSource = objclsappconf.GetAppointmentBlk();
                            arrdata.Add("appointment block type");
                            SetGridColumnVisibility(flxApptblk, arrdata);
                            flxApptblk.SetCellCheck(0, 0, CheckEnum.Unchecked); 
                        }
                        break;
                    case "Problem Type":

                        if (flxPrb.DataSource == null)
                        {
                            flxPrb.DataSource = objclsappconf.GetProblem();
                            arrdata.Add("problem type");
                            SetGridColumnVisibility(flxPrb, arrdata);
                            flxPrb.SetCellCheck(0, 0, CheckEnum.Unchecked); 
                        }
                        break;

                    case "Appointment Type":

                        if (flxAppt.DataSource == null)
                        {
                            flxAppt.DataSource = objclsappconf.GetAppointmenttype();
                            arrdata.Add("appointment type");
                            arrdata.Add("colorcode1");
                            arrdata.Add("duration");
                            arrdata.Add("color code");
                            SetGridColumnVisibility(flxAppt, arrdata);
                            flxAppt.Cols["ColorCode1"].Caption = "Color Code";
                            flxAppt.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                            for (int i = 0; i < (flxAppt.Rows.Count - 1); i++)
                            {
                                C1.Win.C1FlexGrid.CellStyle cStyle;
                                C1.Win.C1FlexGrid.CellRange rgBubbleValues = flxAppt.GetCellRange(i + 1, flxAppt.Cols.Count-1 );
                            //    cStyle = flxAppt.Styles.Add("BubbleValues" + i.ToString());
                                string mystring = "BubbleValues" + i.ToString();
                                try
                                {
                                    if (flxAppt.Styles.Contains(mystring))
                                    {
                                        cStyle = flxAppt.Styles[mystring];
                                    }
                                    else
                                    {
                                        cStyle = flxAppt.Styles.Add(mystring);

                                    }

                                }
                                catch
                                {
                                    cStyle = flxAppt.Styles.Add(mystring);

                                }
                                cStyle.BackColor = Color.FromArgb(Convert.ToInt32(flxAppt.Rows[i + 1]["ColorCode1"]));  // Color.Blue;
                                flxAppt.SetData(i + 1, flxAppt.Cols.Count - 1, "     ");
                                rgBubbleValues.Style = cStyle;
                                TimeSpan tspan = TimeSpan.FromMinutes(Convert.ToInt64(flxAppt.Rows[i+1]["Duration"]));
                                    flxAppt.SetData(i + 1, "Duration", tspan.ToString()  ); 
                            }
                        }
                        break;

                    case "Appointment Status":

                        if (flxApptstat.DataSource == null)
                        {
                            flxApptstat.DataSource = objclsappconf.GetApptStatus();
                            arrdata.Add("appointment status");
                            arrdata.Add("record type");
                            SetGridColumnVisibility(flxApptstat, arrdata);
                        }
                        break;

                    case "Location":

                        if (flxLoc.DataSource == null)
                        {
                            flxLoc.DataSource = objclsappconf.GetLocation();
                            arrdata.Add("location");
                            arrdata.Add("address line1");
                            arrdata.Add("address line2");
                            arrdata.Add("city");
                            arrdata.Add("state");
                            arrdata.Add("zip");
                            arrdata.Add("country");
                            SetGridColumnVisibility(flxLoc, arrdata);
                        }

                        break;

                    case "Department":

                        if (flxDept.DataSource == null)
                        {
                            flxDept.DataSource = objclsappconf.GetDepartment();
                            arrdata.Add("location");
                            arrdata.Add("department");
                            SetGridColumnVisibility(flxDept, arrdata);
                        }

                        break;

                    case "Template":
                  //      if (trvAppointmentBook.Nodes.Count == 1)
                  if(TempNode.Nodes.Count==0)   
                        {
                            bool isnodePresent = true;
                            TreeNode oNode = null;


                            DataTable dtTemplates = objclsappconf.GetTemplate();

                            if (dtTemplates != null)
                            {
                                for (int i = 0; i < dtTemplates.Rows.Count; i++)
                                {
                                    isnodePresent = false;
                                    oNode = new TreeNode();
                                    oNode.Text = Convert.ToString(dtTemplates.Rows[i]["sAppointmentTemplates"]);
                                    oNode.Tag = "Template~" + dtTemplates.Rows[i]["nAppointmentTemplateID"];
                                    oNode.ImageIndex = 10;
                                    oNode.SelectedImageIndex = 10;
                                    if (isnodePresent == false)
                                    {
                                      //  trvAppointmentBook.SelectedNode.Nodes.Add(oNode);
                                        if (TempNode.Nodes.ContainsKey(oNode.Text) == false)
                                        {
                                            TempNode.Nodes.Add(oNode);
                                        }
                                        else
                                        {
                                            //oNode.Dispose();
                                            oNode = null;
                                        }
                                    }
                                }
                                trvAppointmentBook.ExpandAll();   
                            }
                        }
                        break;
                    case "FollowUp":
                        if (flxfollup.DataSource == null)
                        {
                            flxfollup.DataSource = objclsappconf.GetFollowUp();
                            arrdata.Add("follow up name");
                            arrdata.Add("duration");
                            arrdata.Add("scriteria");
                            SetGridColumnVisibility(flxfollup, arrdata);
                            flxfollup.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                            try
                            {
                                flxfollup.Cols["sCriteria"].Caption = "Criteria";
                            }
                            catch
                            {
                           
                            }
                        }

                        break;
                }
            }
            else
            {
                switch (tabblconf.SelectedTab.Text)
                {
                    case "Resource":

                        if (flxRes.DataSource != null)
                        {
                           
                            arrdata.Add("code");
                            arrdata.Add("resource name");
                            arrdata.Add("user name");
                            SetGridColumnVisibility(flxRes, arrdata);
                            flxRes.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                        }
                        break;

                    case "Appointment Block Type":

                        if (flxApptblk.DataSource != null)
                        {
                         
                            arrdata.Add("appointment block type");
                            SetGridColumnVisibility(flxApptblk, arrdata);
                            flxApptblk.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                        }

                        break;
                    case "Problem Type":

                        if (flxPrb.DataSource != null)
                        {
                          
                            arrdata.Add("problem type");
                            SetGridColumnVisibility(flxPrb, arrdata);
                            flxPrb.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                        }
                        break;

                    case "Appointment Type":

                        if (flxAppt.DataSource != null)
                        {
                            arrdata.Add("appointment type");
                            arrdata.Add("colorcode1");
                            arrdata.Add("duration");
                            arrdata.Add("color code");
                            SetGridColumnVisibility(flxAppt, arrdata);
                            flxAppt.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                            for (int i = 0; i < (flxAppt.Rows.Count - 1); i++)
                          {
                              C1.Win.C1FlexGrid.CellStyle cStyle;
                              C1.Win.C1FlexGrid.CellRange rgBubbleValues = flxAppt.GetCellRange(i + 1, flxAppt.Cols.Count - 1);
                            //  cStyle = flxAppt.Styles.Add("BubbleValues" + i.ToString());
                              string mystring = "BubbleValues" + i.ToString();
                              try
                              {
                                  if (flxAppt.Styles.Contains(mystring))
                                  {
                                      cStyle = flxAppt.Styles[mystring];
                                  }
                                  else
                                  {
                                      cStyle = flxAppt.Styles.Add(mystring);

                                  }

                              }
                              catch
                              {
                                  cStyle = flxAppt.Styles.Add(mystring);

                              }
                              cStyle.BackColor = Color.FromArgb(Convert.ToInt32(flxAppt.Rows[i + 1]["Color Codes"]));  // Color.Blue;
                              flxAppt.SetData(i + 1, flxAppt.Cols.Count - 1, "     ");
                              rgBubbleValues.Style = cStyle;

                          }

                          flxAppt.Cols["colorcode1"].Caption = "Color Code";

                  

                        }
                        break;

                    case "Appointment Status":

                        if (flxApptstat.DataSource != null)
                        {
                           arrdata.Add("appointment status");
                            arrdata.Add("record type");
                            SetGridColumnVisibility(flxApptstat, arrdata);
                            flxApptstat.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                        }

                        break;

                    case "Location":

                        if (flxLoc.DataSource != null)
                        {
                            arrdata.Add("location");
                            arrdata.Add("address line1");
                            arrdata.Add("address line2");
                            arrdata.Add("city");
                            arrdata.Add("state");
                            arrdata.Add("zip");
                            arrdata.Add("country");
                            SetGridColumnVisibility(flxLoc, arrdata);
                            flxLoc.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                        }

                        break;

                    case "Department":

                        if (flxDept.DataSource != null)
                        {
                            arrdata.Add("location");
                            arrdata.Add("department");
                            SetGridColumnVisibility(flxDept, arrdata);
                            flxDept.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                        }

                        break;

                    case "Template":
                     //   if (flxtemp.DataSource != null)
                      //  {
                            //flxtemp.DataSource = objclsappconf.GetICD9();
                       // }
                    

                        break;

                    case "FollowUp":
                        if (flxfollup.DataSource != null)
                        {
                            arrdata.Add("follow up name");
                            arrdata.Add("duration");
                            arrdata.Add("scriteria");
                            SetGridColumnVisibility(flxfollup, arrdata);
                            flxfollup.SetCellCheck(0, 0, CheckEnum.Unchecked); 
             
                            //  flxfollup.Cols["sCriteria"].Caption = "Criteria";
                            try
                            {
                                flxfollup.Cols["sCriteria"].Caption = "Criteria";
                            }
                            catch(Exception ex)
                            {
                                //clsGeneral.UpdateLog("glocomm Error While Tab Changing  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  


                                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                            }
                        }

                        break;


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

        private void ClearGridData()
        {

            try
            {
               // flxRes.Clear();
                flxRes.DataSource = null;
               // flxfollup.Clear();
                flxfollup.DataSource = null;
               // flxPrb.Clear();
                flxPrb.DataSource = null;
               // flxAppt.Clear();
                flxAppt.DataSource = null;
               // flxApptstat.Clear();
                flxApptstat.DataSource = null;
               // flxApptblk.Clear();
                flxApptblk.DataSource = null;
               // flxLoc.Clear();
                flxLoc.DataSource = null;

                if (flxRes.Rows.Count >= 2)
                {
                    flxRes.Rows.RemoveRange(1, flxRes.Rows.Count - 1);
                }

                if (flxfollup.Rows.Count >= 2)
                {
                    flxfollup.Rows.RemoveRange(1, flxfollup.Rows.Count - 1);
                }
                if (flxPrb.Rows.Count >= 2)
                {
                    flxPrb.Rows.RemoveRange(1, flxPrb.Rows.Count - 1);
                }

                if (flxAppt.Rows.Count >= 2)
                {
                    flxAppt.Rows.RemoveRange(1, flxAppt.Rows.Count - 1);
                }


                if (flxApptstat.Rows.Count >= 2)
                {
                    flxApptstat.Rows.RemoveRange(1, flxApptstat.Rows.Count - 1);
                }

                if (flxApptblk.Rows.Count >= 2)
                {
                    flxApptblk.Rows.RemoveRange(1, flxApptblk.Rows.Count - 1);
                }


                if (flxLoc.Rows.Count >= 2)
                {
                    flxLoc.Rows.RemoveRange(1, flxLoc.Rows.Count - 1);
                }
                trvAppointmentBook.Nodes.Clear();   
            }
            catch
            {
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
                                string fileUrl = WebPath + "/" + clsGeneral.WebGlobalXmlFolder + "/" + StrName + "/" + clsGeneral.gstrappconfflnm + "/" + clsGeneral.gstrappconfflnm + ".xml";

                                tr.Tag = fileUrl;
                                if (tr.Text.Contains(".aspx") == false)
                                    trvappconf.Nodes.Add(tr);
                            }
                        }
                    }
                }
            }
        }

        private void trvappconf_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //ClearGridData();
            //if (strAction == "Download")
            //{
            //    ShowXMLFIleData(trvappconf.SelectedNode.Tag.ToString(), "", "", "");
            //}
        }

        private void trvAppointmentBook_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //try
            //{
            //    if (e.Node.Level == 1)
            //    {
            //        CalendarTemplate.Appointments.Clear();
            //        if (strAction == "Upload")
            //        {

            //            if (trvAppointmentBook.SelectedNode.Tag != null)
            //            {
            //                gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
            //                AppointmentTemplate oAppointmentTemplate = null;
            //                Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            //                Int64 _nTemplateID = Convert.ToInt64(GetTagElement(trvAppointmentBook.SelectedNode.Tag.ToString(), '~', 2));
            //                oAppointmentTemplate = ogloAppointmentTemplate.GetTemplate(_nTemplateID);
            //                try
            //                {

            //                    for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
            //                    {
            //                        oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
            //                        oJUC_Appointment.Text = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
            //                        oJUC_Appointment.Description = "";
            //                        oJUC_Appointment.Prefix = "";
            //                        oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(oAppointmentTemplate.TemplateDetails[i].ColorCode);

            //                        bool _ErrorFound = false;
            //                        try
            //                        {
            //                            oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].EndTime);
            //                            oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].StartTime);
            //                        }
            //                        catch { _ErrorFound = true; }

            //                        if (_ErrorFound == true)
            //                        {
            //                            try
            //                            {
            //                                oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].StartTime);
            //                                oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].EndTime);
            //                            }
            //                            catch(Exception ex) {

            //                                clsGeneral.UpdateLog("glocomm Error While AppointmentBook Node Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  
                 
            //                            }
            //                        }

            //                        CalendarTemplate.Appointments.Add(oJUC_Appointment);
            //                    }

            //                }

            //                catch (Exception ex)
            //                {
            //                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            DataRow[] drtemp = dtTemplate.Select("ParentAppointmentTemplate= '" + e.Node.Text + "'");

            //            if (drtemp.Length > 0)
            //            {
            //                int len = 0;
            //                Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
            //                while (len < drtemp.Length)
            //                {


            //                    oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
            //                    oJUC_Appointment.Text = drtemp[len]["Description"].ToString();
            //                    oJUC_Appointment.Description = "";
            //                    oJUC_Appointment.Prefix = "";
            //                    oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(Convert.ToInt32(drtemp[len]["ColorCode"]));

            //                    bool _ErrorFound = false;
            //                    try
            //                    {
            //                        oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["EndTime"]));
            //                        oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["StartTime"]));
            //                    }
            //                    catch(Exception ex) {

            //                        clsGeneral.UpdateLog("glocomm Error While AppointmentBook Node Double  Click  in  Usercontrol Appointment Configuration : " + ex.Message.ToString());  
            
            //                        _ErrorFound = true; }

            //                    if (_ErrorFound == true)
            //                    {
            //                        try
            //                        {
            //                            oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["StartTime"]));
            //                            oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["EndTime"]));
            //                        }
            //                        catch(Exception ex) 
            //                        {
            //                            clsGeneral.UpdateLog("glocomm Error While Appointment ConfigurationBook Node  Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  
            
            //                        }
            //                    }

            //                    CalendarTemplate.Appointments.Add(oJUC_Appointment);
            //                    len++;
            //                }
            //            }

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    clsGeneral.UpdateLog("glocomm Error While Appointment Configuration Node Double Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  
                 
            //}
        }

        private object GetTagElement(string TagContent, Char Delimeter, Int64 Position)
        {
            string[] temp;
            if (TagContent.Contains(Delimeter.ToString()))
            {
                temp = TagContent.Split(Delimeter);
                return temp[Position - 1];
            }
            else
            {
                return TagContent;
            }
        }

        private void tlbClinicRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ClearGridData();
                // pnl_btnCentral.Dock = DockStyle.Bottom;
                //pnl_btnLocal.Dock = DockStyle.Top;
                trvappconf.Nodes.Clear();
                pnlleft.Visible = false;
                blnappconfgclinic = true;
                FillLocalClinicData();
            }
            catch (Exception ex)
            {
                clsGeneral.UpdateLog("glocomm Error While Appointment Configuration Clinic repository Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  
            
            }
                this.Cursor = Cursors.Default;
        }

        private void tlbGlobalRepository_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                ClearGridData();
                trvappconf.Nodes.Clear();
                pnlleft.Visible = true;
                blnappconfgclinic = false;
                try
                {
                    GetCentralList();
                }
                catch (Exception ex)
                {
              //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.Clinic, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while global Appointment Configure Downloaded", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    //clsGeneral.UpdateLog("gloCommunity:-Error while global Appointment Configure Downloaded");  


                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
               trvappconf.ExpandAll();
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Appointment Configuration Global repository Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            this.Cursor = Cursors.Default;
        }

        private void flxLoc_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void flxApptblk_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e); 
        }

        private void flxPrb_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e); 
        }

        private void flxAppt_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e); 
        }

        private void flxfollup_MouseDown(object sender, MouseEventArgs e)
        {
            SetCellCheck(sender as C1FlexGrid, e); 
        }

        private void trvAppointmentBook_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void trvAppointmentBook_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            try
            {
                if (e.Node.Level == 1)
                {
                    CalendarTemplate.Appointments.Clear();
                    if (strAction == "Upload")
                    {

                        if (e.Node.Tag != null)
                        {
                            gloAppointmentTemplate ogloAppointmentTemplate = new gloAppointmentTemplate();
                            AppointmentTemplate oAppointmentTemplate = null;
                            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
                            Int64 _nTemplateID = Convert.ToInt64(GetTagElement(e.Node.Tag.ToString(), '~', 2));
                            oAppointmentTemplate = ogloAppointmentTemplate.GetTemplate(_nTemplateID);
                            try
                            {

                                for (int i = 0; i < oAppointmentTemplate.TemplateDetails.Count; i++)
                                {
                                    oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                                    oJUC_Appointment.Text = oAppointmentTemplate.TemplateDetails[i].AppointmentTypeDesc;
                                    oJUC_Appointment.Description = "";
                                    oJUC_Appointment.Prefix = "";
                                    oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(oAppointmentTemplate.TemplateDetails[i].ColorCode);

                                    bool _ErrorFound = false;
                                    try
                                    {
                                        oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].EndTime);
                                        oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].StartTime);
                                    }
                                    catch { _ErrorFound = true; }

                                    if (_ErrorFound == true)
                                    {
                                        try
                                        {
                                            oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].StartTime);
                                            oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, oAppointmentTemplate.TemplateDetails[i].EndTime);
                                        }
                                        catch (Exception ex)
                                        {

                                            //clsGeneral.UpdateLog("glocomm Error While AppointmentBook Node Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());


                                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                        }
                                    }

                                    CalendarTemplate.Appointments.Add(oJUC_Appointment);
                                }

                            }

                            catch (Exception ex)
                            {
                                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                            }
                        }
                    }
                    else
                    {
                        DataRow[] drtemp = dtTemplate.Select("ParentAppointmentTemplate= '" + e.Node.Text + "'");

                        if (drtemp.Length > 0)
                        {
                            int len = 0;
                            Janus.Windows.Schedule.ScheduleAppointment oJUC_Appointment;
                            while (len < drtemp.Length)
                            {


                                oJUC_Appointment = new Janus.Windows.Schedule.ScheduleAppointment();
                                oJUC_Appointment.Text = drtemp[len]["Description"].ToString();
                                oJUC_Appointment.Description = "";
                                oJUC_Appointment.Prefix = "";
                                oJUC_Appointment.FormatStyle.BackColor = Color.FromArgb(Convert.ToInt32(drtemp[len]["ColorCode"]));

                                bool _ErrorFound = false;
                                try
                                {
                                    oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["EndTime"]));
                                    oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["StartTime"]));
                                }
                                catch (Exception ex)
                                {

                                    //clsGeneral.UpdateLog("glocomm Error While AppointmentBook Node Double  Click  in  Usercontrol Appointment Configuration : " + ex.Message.ToString());


                                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                    _ErrorFound = true;
                                }

                                if (_ErrorFound == true)
                                {
                                    try
                                    {
                                        oJUC_Appointment.StartTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["StartTime"]));
                                        oJUC_Appointment.EndTime = gloTime.TimeAsDateTime(DateTime.Now, Convert.ToInt32(drtemp[len]["EndTime"]));
                                    }
                                    catch (Exception ex)
                                    {
                                        //clsGeneral.UpdateLog("glocomm Error While Appointment ConfigurationBook Node  Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());


                                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                                    }
                                }

                                CalendarTemplate.Appointments.Add(oJUC_Appointment);
                                len++;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Appointment Configuration Node Double Click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }


            try
            {
                if (e.Node.Level == 0)
                {
                    foreach (TreeNode tr in e.Node.Nodes)
                    {
                        tr.Checked = e.Node.Checked;
                    }
                }
            }
            catch(Exception ex)
            {
                //clsGeneral.UpdateLog("glocomm Error While Appointment Configuration Node click  in  Usercontrol Appointment Configuration Configuration: " + ex.Message.ToString());  


                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.AppointmentType, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
        }

        private void trvappconf_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ClearGridData();
            if (strAction == "Download")
            {
                ShowXMLFIleData(e.Node.Tag.ToString(), "", "", "");
            }
        }

       
    }
}
