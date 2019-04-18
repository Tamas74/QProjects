using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using gloCommunity.UserControls;
using gloCommunity.Classes;
using System.Collections;
using gloEMRGeneralLibrary.gloEMRDatabase;
using System.Net;
using System.IO;
using oOffice = Microsoft.Office.Core;
using Wd = Microsoft.Office.Interop.Word;
using System.Configuration;
namespace gloCommunity.Forms
{
    public partial class gloCommunityViewData : Form
    {
        UCTemplates ObjucTemplates = null;
        UCSmartDX objUCSmartDX = null;
        UCLiquidData objUCLiquidData = null;
        UCSmartCPT objUCSmartCPT = null;
        UCSmartOrder objUCSmartOrder = null;
        UCHistory objUCHistory = null;
        UCDmSetup objUCDmSetup = null;

    //    myList mylist = default(myList);
        string strItemMenu = "";
        string strAction = "";
        ArrayList arrLocalCatFileNm = new ArrayList();
        clsTemplate objclstemplate = null;
        clsgloCommunity objclsglocomm = null;
        //TaskMail
        DataTable dtTskMlData = null;
        UCTaskMail objuctm = null;
        clsTaskMail objclstm = null;
        //TaskMail

        //IMSetup
        UCIMSetup ObjucIMSetup = null;
        clsIMSetup objclsIMSetup = null;
        DataTable dtIMSetupData;
        //IMSetup


        //CVSetup
        UC_CVSetup ObjucCVSetup = null;
        ClsCVSetup objclsCVSetup = null;
        DataTable dtCVSetupData;
       // DataTable dtCompleteData;
       // DataTable dtCVSetupDataComplete;
      //  DataTable dtCVuploadable;
        //CVSetup

        //UC Billing Configuration
        UCBillingConf objUCBillConf = null;
        DataTable dtBlconfData = null;
        clsBillingconf objblconf = null;
        DataTable dtICD9 = null;
        DataTable dtCPT = null;
        DataTable dtCat = null;
        DataTable dtMod = null;
        DataTable dtPatr = null;
        DataTable dtpln = null;
        DataTable dtspec = null;
        ArrayList arrspec = null;
        //UC Billing Configuration

        //Appointment Configuration
        UCAppointConf objUCAppConf = null;
     //   DataTable dtResource = null;
        DataTable dtFollowup = null;
        DataTable dtProblem = null;
      //  DataTable dtDept = null;
        DataTable dtApptStat = null;
        DataTable dtApptType = null;
        DataTable dtApptBlk = null;
     //   DataTable dtLoc = null;
        DataTable dtAppconfData = null;
        DataTable dtTemplate = null;
        clsApptconf objclsAppconf = null;
        //Appointment Configuration

        //FlowSheet
        UCFlowSheet objUCFlowsheet = null;
        clsFlowSheet objclsflowsheet = null;
        DataTable dtFlowSheet = null;

        //FormGallery
        UCFormGallery objUCFormgallery = null;
        clsformgallery objclsformgallery = null;
        DataTable dtFormgallery = null;

        public gloCommunityViewData(string _strItemMenu, string _strAction)
        {
            InitializeComponent();
            strItemMenu = _strItemMenu;
            strAction = _strAction;



            switch (strItemMenu)
            {
                case "Template":
                    {
                        this.Text = "gloCommunity Template " + strAction;
                        break;
                    }
                case "SmartDx":
                    {
                        this.Text = "gloCommunity SmartDiagnosis " + strAction;
                        break;
                    }
                case "LiquidData":
                    {
                        this.Text = "gloCommunity LiquidData " + strAction;


                        break;
                    }
                case "TaskMail":
                    {
                        this.Text = "gloCommunity TaskMail " + strAction;

                        break;
                    }
                case "IMSetup":
                    {
                        try
                        {

                            this.Text = this.Text + " Immunization " + strAction;
                        }
                        catch //(Exception ex)
                        {
                            //commented by kanchan on 20120105
                            //MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                case "CPT":
                    {
                        this.Text = "gloCommunity SmartTreatment " + strAction;


                        break;
                    }
                case "BillingConf":
                    {
                        this.Text = "gloCommunity Billing Configuration " + strAction;

                        break;
                    }
                case "Order":
                    {
                        this.Text = "gloCommunity SmartOrder " + strAction;

                        break;
                    }
                case "History":
                    {
                        this.Text = "gloCommunity History " + strAction;


                        break;
                    }

                case "AppointmentConfigure":
                    {
                        this.Text = "gloCommunity Appointment Configuration " + strAction;



                        break;
                    }

                case "Flowsheet":
                    {
                        this.Text = "gloCommunity FlowSheet " + strAction;



                        break;
                    }


                case "CVSetup":
                    {
                        try
                        {

                            this.Text = this.Text + " Cardio Vascular " + strAction;
                        }
                        catch //(Exception ex)
                        {
                            //commented by kanchan on 20120105
                            //MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                case "Formgallery":
                    {

                        this.Text = this.Text + " Form gallery " + strAction;


                    }
                    break;
                case "DmSetUp":
                    {
                        this.Text = "gloCommunity Disease Management Criteria " + strAction;


                        break;
                    }
            }//End Switch
        }

        private bool CheckAuthentication()
        {
            clsEncryption oclsencryption = new clsEncryption();
            clsGetADUser oclsgloCommunity = new clsGetADUser();
            bool _IsUserClose = false;
            try
            {
                //'Added for check Login user available on SharePoint server if not then add on 20120727

                if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                {
                    gloCommunity.Classes.clsgloCommunityUsers clsGCUSer = new gloCommunity.Classes.clsgloCommunityUsers();
                    DataTable dtUSer = clsGCUSer.getGCUser(clsGeneral.gnLoginID, clsGeneral.gblnIscommunityStaging);
                    if ((dtUSer != null) & dtUSer.Rows.Count > 0)
                    {
                        //'get gloCommunity username & password as per login id
                        string _encryptionKey = "12345678";
                        string _strEncryptPWD = oclsencryption.DecryptFromBase64String(dtUSer.Rows[0]["gc_sPassword"].ToString(), _encryptionKey);
                        gloCommunity.Classes.clsGeneral.gstrGCUserName = dtUSer.Rows[0]["gc_sUserName"].ToString();
                        gloCommunity.Classes.clsGeneral.gstrGCPassword = _strEncryptPWD;
                    }
                    else
                    {
                        //'add gloCommunity user
                        //mnuSharepoint.HideDropDown();
                        gloCommunity.Forms.frmAddGCUser frmAddUser = new gloCommunity.Forms.frmAddGCUser();
                        // this.Visible = false;
                        try
                        {
                            if (frmAddUser.ShowDialog(frmAddUser.Parent) != System.Windows.Forms.DialogResult.OK)
                            {
                                _IsUserClose = true;
                                //MessageBox.Show("Login User did not register to gloCommunity. Please close this form and register again", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                dtUSer = clsGCUSer.getGCUser(clsGeneral.gnLoginID, clsGeneral.gblnIscommunityStaging);
                                if ((dtUSer != null) & dtUSer.Rows.Count > 0)
                                {
                                    //'get gloCommunity username & password as per login id
                                    string _encryptionKey = "12345678";
                                    string _strEncryptPWD = oclsencryption.DecryptFromBase64String(dtUSer.Rows[0]["gc_sPassword"].ToString(), _encryptionKey);
                                    gloCommunity.Classes.clsGeneral.gstrGCUserName = dtUSer.Rows[0]["gc_sUserName"].ToString();
                                    gloCommunity.Classes.clsGeneral.gstrGCPassword = _strEncryptPWD;
                                }
                            }
                        }
                        catch (InvalidOperationException)
                        {
                        }
                        catch //(Exception ex)
                        {
                            throw;
                        }
                        finally
                        {
                            if (frmAddUser != null)
                            {
                                frmAddUser.Dispose();
                                frmAddUser = null;
                            }
                            if (oclsencryption != null)
                                oclsencryption = null;
                            if (oclsgloCommunity != null)
                                oclsgloCommunity = null;
                        }

                        //'mnuSharepoint.ShowDropDown()
                    }
                }
                else
                {
                    if (oclsgloCommunity.CheckADUserEmail() == false)
                    {
                        //mnuSharepoint.HideDropDown();
                    }
                }
            }
            //'End
            catch (Exception)
            {
            }
            finally
            {
            }
            return _IsUserClose;
        }

        private void gloCommunityViewData_Load(object sender, EventArgs e)
        {
            clsGeneral.Webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
            clsGeneral.WebSite = clsGeneral.gstrClinicName;
            if (strAction == "Upload")
            {
                ts_gloCommunityDownload.Visible = false;
                ts_gloCommunityUpload.Visible = true;
            }
            else
            {
                ts_gloCommunityDownload.Visible = true;
                ts_gloCommunityUpload.Visible = false;
            }
            ////Added for check which authentication is use for access gloCommunity on 20120803
            //if (CheckAuthentication() == true)
            //{
            //    //MessageBox.Show("Login User did not register to gloCommunity. Please close this form and register again", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            ////End

            switch (strItemMenu)
            {
                case "Template":
                    {
                        //  this.Text = "gloCommunity Template " + strAction;
                        ObjucTemplates = new UCTemplates(strAction);
                        this.Controls.Add(ObjucTemplates);
                        ObjucTemplates.BringToFront();
                        ObjucTemplates.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            ObjucTemplates.pnltls.Visible = false;
                        }
                        else
                        {
                            ObjucTemplates.pnltls.Visible = true;
                        }

                        break;
                    }
                case "SmartDx":
                    {
                        // this.Text = "gloCommunity SmartDiagnosis " + strAction;
                        objUCSmartDX = new UCSmartDX(strAction);
                        this.Controls.Add(objUCSmartDX);
                        objUCSmartDX.BringToFront();
                        objUCSmartDX.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            objUCSmartDX.pnltls.Visible = false;
                        }
                        else
                        {
                            objUCSmartDX.pnltls.Visible = true;
                        }

                        break;
                    }
                case "LiquidData":
                    {
                        // this.Text = "gloCommunity LiquidData " + strAction;
                        objUCLiquidData = new UCLiquidData(strAction);
                        this.Controls.Add(objUCLiquidData);
                        objUCLiquidData.BringToFront();
                        objUCLiquidData.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            objUCLiquidData.pnltls.Visible = false;
                        }
                        else
                        {
                            objUCLiquidData.pnltls.Visible = true;
                        }

                        break;
                    }
                case "TaskMail":
                    {
                        //  this.Text = "gloCommunity TaskMail " + strAction;
                        objuctm = new UCTaskMail(strAction);
                        dtTskMlData = new DataTable();
                        objclstm = new clsTaskMail();
                        this.Controls.Add(objuctm);
                        objuctm.BringToFront();
                        objuctm.Dock = DockStyle.Fill;

                        break;
                    }
                case "IMSetup":
                    {
                        try
                        {
                            dtIMSetupData = new DataTable();
                            objclsIMSetup = new clsIMSetup();
                            ObjucIMSetup = new UCIMSetup(strAction);
                            this.Controls.Add(ObjucIMSetup);
                            ObjucIMSetup.BringToFront();
                            ObjucIMSetup.Dock = DockStyle.Fill;
                            //this.Text = this.Text + " Immunization " + strAction;
                        }
                        catch //(Exception ex)
                        {
                            //commented by kanchan on 20120105
                            //MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                case "CPT":
                    {
                        //this.Text = "gloCommunity SmartTreatment " + strAction;
                        objUCSmartCPT = new UCSmartCPT(strAction);
                        this.Controls.Add(objUCSmartCPT);
                        objUCSmartCPT.BringToFront();
                        objUCSmartCPT.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            objUCSmartCPT.pnltls.Visible = false;
                        }
                        else
                        {
                            objUCSmartCPT.pnltls.Visible = true;
                        }

                        break;
                    }
                case "BillingConf":
                    {
                        //  this.Text = "gloCommunity Billing Configuration " + strAction;
                        dtBlconfData = new DataTable();
                        objUCBillConf = new UCBillingConf(strAction);
                        objblconf = new clsBillingconf();
                        arrspec = new ArrayList();
                        this.Controls.Add(objUCBillConf);
                        objUCBillConf.BringToFront();
                        objUCBillConf.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            //    objUCBillConf.pnl_btnLocal.Visible = false;
                            // objUCBillConf.pnl_btnCentral.Visible = false;
                            objUCBillConf.pnltls.Visible = false;

                        }
                        else
                        {
                            //  objUCBillConf.pnl_btnLocal.Visible = true;
                            // objUCBillConf.pnl_btnCentral.Visible = true;
                            objUCBillConf.pnltls.Visible = true;
                        }

                        break;
                    }
                case "Order":
                    {
                        // this.Text = "gloCommunity SmartOrder " + strAction;
                        objUCSmartOrder = new UCSmartOrder(strAction);
                        this.Controls.Add(objUCSmartOrder);
                        objUCSmartOrder.BringToFront();
                        objUCSmartOrder.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            objUCSmartOrder.pnltls.Visible = false;
                        }
                        else
                        {
                            objUCSmartOrder.pnltls.Visible = true;
                        }

                        break;
                    }
                case "History":
                    {
                        //  this.Text = "gloCommunity History " + strAction;
                        objUCHistory = new UCHistory(strAction);
                        this.Controls.Add(objUCHistory);
                        objUCHistory.BringToFront();
                        objUCHistory.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            objUCHistory.pnltls.Visible = false;
                        }
                        else
                        {
                            objUCHistory.pnltls.Visible = true;
                        }

                        break;
                    }

                case "AppointmentConfigure":
                    {
                        //   this.Text = "Appointment Configuration " + strAction;
                        objUCAppConf = new UCAppointConf(strAction);
                        this.Controls.Add(objUCAppConf);
                        objUCAppConf.BringToFront();
                        objUCAppConf.Dock = DockStyle.Fill;
                        dtAppconfData = new DataTable();
                        objclsAppconf = new clsApptconf();

                        if (strAction == "Upload")
                        {
                            //  objUCAppConf.pnl_btnLocal.Visible = false;
                            // objUCAppConf.pnl_btnCentral.Visible = false;
                            objUCAppConf.pnlleft.Visible = false;
                            dtTemplate = new DataTable();
                        }
                        // else
                        // {
                        //   objUCAppConf.pnl_btnLocal.Visible = true;
                        //  objUCAppConf.pnl_btnCentral.Visible = true;
                        //   objUCAppConf.pnlleft.Visible = true;
                        //}



                        break;
                    }

                case "Flowsheet":
                    {
                        // this.Text = "FlowSheet " + strAction;
                        objUCFlowsheet = new UCFlowSheet(strAction);
                        this.Controls.Add(objUCFlowsheet);
                        objUCFlowsheet.BringToFront();
                        objUCFlowsheet.Dock = DockStyle.Fill;
                        dtFlowSheet = new DataTable();
                        objclsflowsheet = new clsFlowSheet();

                        if (strAction == "Upload")
                        {
                            //  objUCAppConf.pnl_btnLocal.Visible = false;
                            // objUCAppConf.pnl_btnCentral.Visible = false;
                            objUCFlowsheet.pnlleft.Visible = false;
                            dtTemplate = new DataTable();
                        }
                        //  else
                        // {
                        //   objUCAppConf.pnl_btnLocal.Visible = true;
                        //  objUCAppConf.pnl_btnCentral.Visible = true;
                        //  objUCFlowsheet.pnlleft.Visible = true;
                        // }



                        break;
                    }


                case "CVSetup":
                    {
                        try
                        {
                            dtCVSetupData = new DataTable();
                            objclsCVSetup = new ClsCVSetup();
                            ObjucCVSetup = new UC_CVSetup(strAction);
                            this.Controls.Add(ObjucCVSetup);
                            ObjucCVSetup.BringToFront();
                            ObjucCVSetup.Dock = DockStyle.Fill;
                            //  this.Text = this.Text + " Cardio Vascular " + strAction;
                        }
                        catch //(Exception ex)
                        {
                            //commented by kanchan on 20120105
                            //MessageBox.Show(ex.Message.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        break;
                    }
                case "Formgallery":
                    {

                        // this.Text = "Form gallery " + strAction;

                        objUCFormgallery = new UCFormGallery(strAction);
                        objclsformgallery = new clsformgallery();
                        this.Controls.Add(objUCFormgallery);
                        objUCFormgallery.BringToFront();
                        objUCFormgallery.Dock = DockStyle.Fill;
                        dtFormgallery = new DataTable();
                        dtFormgallery.Columns.Add("CPTCode");
                        dtFormgallery.Columns.Add("Description");
                        dtFormgallery.Columns.Add("TemplateNames");
                    }
                    break;
                case "DmSetUp":
                    {
                        // this.Text = "gloCommunity Disease Management Criteria " + strAction;
                        objUCDmSetup = new UCDmSetup(strAction);
                        this.Controls.Add(objUCDmSetup);
                        objUCDmSetup.BringToFront();
                        objUCDmSetup.Dock = DockStyle.Fill;
                        if (strAction == "Upload")
                        {
                            objUCDmSetup.pnltls.Visible = false;
                        }
                        else
                        {
                            objUCDmSetup.pnltls.Visible = true;
                        }

                        break;
                    }
            }//End Switch
        }
        public static Boolean getInstance(string formName, string frmtitle)
        {
            try
            {

                gloCommunity.Classes.clsGeneral.Isopen = false;


                foreach (Form f in Application.OpenForms)
                {
                    if ((f.Name == formName) && (f.Text == frmtitle))
                    {
                        gloCommunity.Classes.clsGeneral.Isopen = true;
                        f.BringToFront();
                        return gloCommunity.Classes.clsGeneral.Isopen;
                    }

                }
                return gloCommunity.Classes.clsGeneral.Isopen;
            }
            catch //(Exception ex)
            {
                return gloCommunity.Classes.clsGeneral.Isopen;
            }


        }
        private void mnuMainUploadGlobalRepository_Click(object sender, EventArgs e)
        {
            switch (strItemMenu)
            {
                case "Template":
                    {
                        try
                        {
                            UploadShareTempToGlobalRepository();
                        }
                        catch (Exception ex)
                        {


                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.None , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Template Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central Template Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "SmartDx":
                    {
                        try
                        {
                            UploadShareSmartDxToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None , gloAuditTrail.ActivityCategory.None , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central SmartDX Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central SmartDX Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                        }


                        break;
                    }
                case "LiquidData":
                    {
                        try
                        {
                            UploadShareLiquidDataToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None  , gloAuditTrail.ActivityCategory.None , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Liquid Data Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central Liquid Data Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }

                        break;
                    }

                case "TaskMail":
                    {
                        try
                        {
                            getTaskMailGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task   , gloAuditTrail.ActivityCategory.None , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central TaskMail Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central TaskMail Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "IMSetup":
                    {
                        try
                        {
                            UploadShareIMSetupToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "CPT":
                    {
                        try
                        {
                            UploadShareSmartCPTToGlobalRepository();
                        }
                        catch
                        {
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.LoadSmartTreatment, "gloCommunity:-Error while Central CPT Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central CPT Upload");  

                        }
                        break;
                    }

                case "BillingConf":
                    {
                        try
                        {
                            getdataforblConfuploading();
                            UploadShareBillConfToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing , gloAuditTrail.ActivityCategory.BillingCharges , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Billing Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central BillingConfiguration Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "Order":
                    {
                        try
                        {
                            UploadShareSmartOrderToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None  , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Order Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central Order Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "AppointmentConfigure":
                    {
                        try
                        {
                            getdataforAppConfuploading();

                            UploadShareAppConfToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //     gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment , gloAuditTrail.ActivityCategory.None  , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Appointment Configure Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central AppointmentConfiguration Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "History":
                    {
                        try
                        {
                            UploadShareHistoryToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //      gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.History , gloAuditTrail.ActivityCategory.History   , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central History Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central History Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "CVSetup":
                    {
                        try
                        {
                            UploadShareCVSetupToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            //   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular  , gloAuditTrail.ActivityCategory.None   , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Cardio Vascular Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central CVSetup Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }

                case "Flowsheet":
                    {
                        try
                        {
                            if (getdataforflowsheetuploading() == true)
                            {
                                UploadShareFLowsheetToGlobalRepository();
                            }
                            else
                            {
                                MessageBox.Show("Select at least one Flowsheet to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                        catch (Exception ex)
                        {
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Flowsheet   , gloAuditTrail.ActivityCategory.None   , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Flowsheet Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central Flowsheet Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }

                        break;
                    }
                case "Formgallery":
                    try
                    {

                        if (getdataforformgalleryuploading("Global") == false)
                            MessageBox.Show("Select at least one CPT Code to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            UploadShareFormGalleryToGlobalRepository();
                        }

                    }
                    catch (Exception ex)
                    {
                        //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Formgallery    , gloAuditTrail.ActivityCategory.None   , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central Formgallery Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        //clsGeneral.UpdateLog("gloCommunity:-Error while Central Formgallery Upload");  
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    }


                    break;
                case "DmSetUp":
                    {
                        try
                        {
                            UploadShareDmSetupToGlobalRepository();
                        }
                        catch (Exception ex)
                        {
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.DiseaseManagement     , gloAuditTrail.ActivityCategory.None   , gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Central  Disease Management Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Central DmSetUp Upload");  
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }

                        break;
                    }


            }//end switch
        }

        private bool getdataforflowsheetuploading()
        {
            bool datatoupload = false;
            dtFlowSheet.Rows.Clear();

            TreeNode rootnode = objUCFlowsheet.trvflshname.Nodes[0];
            foreach (TreeNode trvnode in rootnode.Nodes)//objUCFlowsheet.trvflshname.Nodes)
            {
                if (trvnode.Checked == true)
                {
                    DataTable dtflow = objclsflowsheet.getallFlowSheetbyParticularName(trvnode.Text);
                    if (dtFlowSheet.Columns.Count == 0)
                        dtFlowSheet = dtflow.Clone();
                    foreach (DataRow dr in dtflow.Rows)
                    {
                        dtFlowSheet.ImportRow(dr);
                        datatoupload = true;
                    }
                }
            }
            dtFlowSheet.TableName = "Flowsheet";

            return datatoupload;

        }

        private bool getdataforformgalleryuploading(string type)
        {
            bool datatoupload = false;
            dtFormgallery.Rows.Clear();
            string strlistoftempname = "";
            string[] listoftempname = null;
            ArrayList ArrCategoryName = new ArrayList();
            TreeNode RootNode = objUCFormgallery.trvcptassoc.Nodes[0];
            foreach (TreeNode trvnode in RootNode.Nodes)
            {
                if (trvnode.Checked == true)
                {
                    // trvnode.
                    //    DataTable dtform = objclsformgallery.getCorresTemplate(Convert.ToInt64(trvnode.Tag));
                    string strtempname = "";
                    // if (dtFormgallery.Columns.Count > 0)
                    // {
                    //foreach (DataRow dr in dtform.Rows)
                    foreach (TreeNode childtrvnode in trvnode.Nodes)
                    {
                        //  strtempname  += dr[0].ToString() +"≈" + dr[1].ToString()+  ";";
                        //   strlistoftempname += dr[0].ToString() + "≈" + dr[1].ToString() + ";";
                        //   if (ArrCategoryName.Contains(dr[1].ToString()) == false)   
                        //  ArrCategoryName.Add(dr[1].ToString());
                        strtempname += childtrvnode.Text.ToString() + "≈" + childtrvnode.Tag.ToString() + ";";

                        strlistoftempname += childtrvnode.Text.ToString() + "≈" + childtrvnode.Tag.ToString() + ";";
                        if (ArrCategoryName.Contains(childtrvnode.Tag.ToString()) == false)
                            ArrCategoryName.Add(childtrvnode.Tag.ToString());
                    }
                    // }
                    if (strtempname.Trim().Length > 0)
                        strtempname = strtempname.Substring(0, strtempname.Length - 1);

                    DataRow drformgal = dtFormgallery.NewRow();
                    //   dtFormgallery.Columns.Add("CPTCode");
                    //   dtFormgallery.Columns.Add("Description");
                    //   dtFormgallery.Columns.Add("TemplateNames");
                    drformgal["CPTCode"] = trvnode.Text.Substring(0, trvnode.Text.IndexOf("-") - 1); //trvnode.Tag.ToString();
                    drformgal["Description"] = trvnode.Text.Substring(trvnode.Text.IndexOf("-") + 1, (trvnode.Text.Length - (trvnode.Text.IndexOf("-") + 1)));
                    drformgal["TemplateNames"] = strtempname;
                    dtFormgallery.Rows.Add(drformgal);

                    datatoupload = true;
                }
            }
            //dtFlowSheet.TableName = "Flowsheet";
            if (datatoupload == true)
            {
                ArrayList arry = objUCFormgallery.GetListOfSPFilesInInnerSubFolder("", ArrCategoryName, type);
                listoftempname = strlistoftempname.Split(';');
                ArrayList TemptoUpload = new ArrayList();

                for (int lenarr = 0; lenarr < listoftempname.Length; lenarr++)
                {
                    if (arry.Contains(listoftempname[lenarr]) == false)
                    {
                        if (TemptoUpload.Contains(listoftempname[lenarr]) == false)
                            if (listoftempname[lenarr].ToString().Trim().Length >= 2)
                                TemptoUpload.Add(listoftempname[lenarr]);
                    }
                }

                DataTable TemplateData = objclsformgallery.getTemplateData(TemptoUpload);
                clsgloCommunity objgloCommunity = new clsgloCommunity();
                if (TemplateData != null)
                {
                    string FolderNm = "";
                    string Wpath = "";
                    string gWebpath = "";

                    string gWebFolder = "";

                    string ClinicGblFolder = "";
                    string gWebSite = "";

                    //foreach (DataRow dr in TemplateData.Rows)
                    for (int lentemptoupload = 0; lentemptoupload < TemptoUpload.Count; lentemptoupload++)
                    {

                        string TempName = TemptoUpload[lentemptoupload].ToString().Substring(0, TemptoUpload[lentemptoupload].ToString().IndexOf("≈"));
                        string CatName = TemptoUpload[lentemptoupload].ToString().Substring(TemptoUpload[lentemptoupload].ToString().IndexOf("≈") + 1, (TemptoUpload[lentemptoupload].ToString().Length - (TemptoUpload[lentemptoupload].ToString().IndexOf("≈") + 1)));

                        DataRow[] dr = TemplateData.Select("TemplateName='" + TempName + "' And CategoryName='" + CatName + "'");
                        if (type == "Clinic")
                        {

                            //  FolderNm=      clsGeneral.ClinicWebFolder + "/" + "Soap";
                            FolderNm = clsGeneral.ClinicWebFolder + "/" + CatName;
                            Wpath = clsGeneral.Webpath + clsGeneral.WebSite + "/" + clsGeneral.ClinicRepository;
                        }
                        else
                        {

                            //     strSitepath = "http://" & gstrSharepointSrvNm & "/" & gstrSharepointSiteNm & ""
                            gWebpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;

                            gWebFolder = clsGeneral.GlobalRepository;

                            FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + CatName;


                            ClinicGblFolder = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder;



                            Wpath = gWebpath + "/" + gWebFolder;

                        }



                        string FileNm = clsGeneral.getFileName(dr[0][0].ToString());
                        FileNm = clsGeneral.GenerateFile((byte[])dr[0][1], FileNm);

                        if (type == "Clinic")
                        {


                            if (objgloCommunity.UploadFileToDocumentLibrary(Wpath, FolderNm, FileNm, clsGeneral.Webpath, clsGeneral.WebSite, clsGeneral.ClinicRepository, clsGeneral.ClinicWebFolder) != true)
                            {

                            }
                        }
                        else
                        {
                            if (objgloCommunity.UploadFileToDocumentLibrary(Wpath, FolderNm, FileNm, gWebpath, gWebSite, gWebFolder, ClinicGblFolder) != true)
                            {
                            }
                        }



                    }// For
                }
            }
            return datatoupload;
        }

        private void getdataforAppConfuploading()
        {
            dtAppconfData.Rows.Clear();
            dtTemplate.Rows.Clear();
            //dtResource = (DataTable)objUCAppConf.flxRes.DataSource;
            //if (dtResource != null)
            //{
            //    dtResource.TableName = "Resource";
            //dtTemplate=dtResource.Clone();   
            //}
            dtFollowup = (DataTable)objUCAppConf.flxfollup.DataSource;
            if (dtFollowup != null)
            {
                dtFollowup.TableName = "FollowUp";
                dtTemplate = dtFollowup.Clone();
            }
            dtProblem = (DataTable)objUCAppConf.flxPrb.DataSource;
            if (dtProblem != null)
            {
                dtProblem.TableName = "Problem";
                dtTemplate = dtProblem.Clone();
            }

            //dtDept = (DataTable)objUCAppConf.flxDept.DataSource;
            //if (dtDept != null)
            //{
            //    dtDept.TableName = "Department";
            //    dtTemplate=dtDept.Clone();    
            //}

            dtApptStat = (DataTable)objUCAppConf.flxApptstat.DataSource;
            if (dtApptStat != null)
            {
                dtApptStat.TableName = "AppointmentStatus";
                dtTemplate = dtApptStat.Clone();
            }

            dtApptType = (DataTable)objUCAppConf.flxAppt.DataSource;
            if (dtApptType != null)
            {
                dtApptType.TableName = "AppointmentType";
                dtTemplate = dtApptType.Clone();
            }

            dtApptBlk = (DataTable)objUCAppConf.flxApptblk.DataSource;
            if (dtApptBlk != null)
            {
                dtApptBlk.TableName = "AppointmentBlock";
                dtTemplate = dtApptBlk.Clone();
            }
            //dtLoc = (DataTable)objUCAppConf.flxLoc.DataSource;
            //if (dtLoc != null)
            //{
            //    dtLoc.TableName = "Location";
            //    dtTemplate=dtLoc.Clone();  
            //}


            //    AddAppConfData(dtResource);
            AddAppConfData(dtFollowup);
            AddAppConfData(dtProblem);
            //  AddAppConfData(dtDept);
            //   AddAppConfData(dtApptStat);
            AddAppConfData(dtApptType);
            AddAppConfData(dtApptBlk);
            //   AddAppConfData(dtLoc);

            // Adding Template Data
            try
            {
                bool takedatafortemp = true;
                if (dtAppconfData.Columns.Count > 0)
                    takedatafortemp = false;
                // else
                //  dtTemplate.Rows.Clear();  
                foreach (TreeNode trtempnode in objUCAppConf.trvAppointmentBook.Nodes[0].Nodes)
                {

                    if (trtempnode.Checked == true)
                    {
                        if (takedatafortemp == true)
                        {
                            dtAppconfData = objclsAppconf.GetSchemaForTemplate();
                        }
                        takedatafortemp = false;
                        dtAppconfData = objclsAppconf.ProcessTemplates(trtempnode, dtAppconfData);
                    }
                }
            }
            catch
            {

            }

        }

        public void AddAppConfData(DataTable dt)
        {
            if (dt != null)
            {
                dt.AcceptChanges();
                if (dtAppconfData.Rows.Count == 0)

                    dtAppconfData = dt.Clone();

                DataRow[] dr = dt.Select("Select='True'");
                for (int len = 0; len < dr.Length; len++)
                {
                    dtAppconfData.ImportRow(dr[len]);
                }

            }
        }

        private bool UploadShareFLowsheetToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string FlowshLocal = clsGeneral.gstrflowshflnm + "Local";
            string FlowshSRV = clsGeneral.gstrflowshflnm;//"SmartDxAssociation";

            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrflowshflnm + "/" + clsGeneral.gstrflowshflnm + ".xml";
            bool changedata = true;
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + FlowshLocal + ".xml";
                string FileNMSP = "";
                string strName = gloSettings.FolderSettings.AppTempFolderPath + FlowshLocal + ".xml";
                dtFlowSheet.TableName = "Flowsheet";
                dtFlowSheet.WriteXml(strName);
                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FlowshSRV + ".xml";//SmartDxAssociationSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                //   changedata = objclsAppconf.CompareXMlData(dtAppconfData, dt, strName);
                                changedata = objclsflowsheet.CompareXMlData(dtFlowSheet, dt, strName);
                            }
                        }


                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FlowshLocal + ".xml"; //SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, FlowshLocal, FlowshSRV, clsGeneral.gstrflowshflnm);// SmartDxAssociationLocal, SmartDxAssociationSRV);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Flowsheet Uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to Uploaded Flowsheet", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }



            }
            catch //(Exception ex)
            {
                //  MessageBox.Show(ex.Message); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString(), " Flowsheet ", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareFormglryToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string FrmglryLocal = clsGeneral.gstrformglry + "Local";
            string FrmglrySRV = clsGeneral.gstrformglry;//"SmartDxAssociation";

            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrformglry + "/" + clsGeneral.gstrformglry + ".xml";
            bool changedata = true;
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + FrmglryLocal + ".xml";
                string FileNMSP = "";
                string strName = gloSettings.FolderSettings.AppTempFolderPath + FrmglryLocal + ".xml";
                dtFormgallery.TableName = "Formglry";
                dtFormgallery.WriteXml(strName);
                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FrmglrySRV + ".xml";//SmartDxAssociationSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                //   changedata = objclsAppconf.CompareXMlData(dtAppconfData, dt, strName);
                                changedata = objclsformgallery.CompareXMlData(dtFormgallery, ref dt, strName);
                            }
                        }


                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FrmglryLocal + ".xml"; //SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, FrmglryLocal, FrmglrySRV, clsGeneral.gstrformglry);// SmartDxAssociationLocal, SmartDxAssociationSRV);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Form Gallery uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Form Gallery", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }



            }
            catch //(Exception ex)
            {
                //  MessageBox.Show(ex.Message); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString(), " Form Gallery ", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareFLowsheetToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool changedata = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string FlowshLocal = clsGeneral.gstrflowshflnm + "Local";
            string FlowshSRV = clsGeneral.gstrflowshflnm;//"SmartDxAssociation";

            //  string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrflowshflnm + ".xml";   //SmartDxAssociationSRV + ".xml";

            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrflowshflnm + "/" + clsGeneral.gstrflowshflnm + ".xml";


            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + FlowshLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + FlowshLocal + ".xml"; //"SmartDxAssociation.xml";
                dtFlowSheet.TableName = "Flowsheet";
                dtFlowSheet.WriteXml(strName);

                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {

                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FlowshSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                changedata = objclsflowsheet.CompareXMlData(dtFlowSheet, dt, strName);
                            }
                        }

                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FlowshLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, FlowshLocal, FlowshSRV, clsGeneral.gstrflowshflnm);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Flowsheet uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Flowsheet", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    //   objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one  to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareFormGalleryToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool changedata = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string FormglryLocal = clsGeneral.gstrformglry + "Local";
            string FormglrySRV = clsGeneral.gstrformglry;

            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrformglry + "/" + clsGeneral.gstrformglry + ".xml";   //SmartDxAssociationSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + FormglryLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + FormglryLocal + ".xml";
                dtFormgallery.TableName = "FormGallery";
                dtFormgallery.WriteXml(strName);
                DataTable serverdt = null;
                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {

                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FormglrySRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                serverdt = serverdata.Tables[0];
                                changedata = objclsformgallery.CompareXMlData(dtFormgallery, ref serverdt, strName);

                            }
                        }

                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + FormglryLocal + ".xml";
                    if (serverdt == null)
                        serverdt = dtFormgallery;
                    //    UploadTemplateforFormglrytoglobal(serverdt);
                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, FormglryLocal, FormglrySRV, clsGeneral.gstrformglry);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Form Gallery uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Form Gallery", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                }
                else
                {
                    MessageBox.Show("Select at least one  to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        //public bool DownloadXML(string temppath)
        //{
        //    string filename = "";
        //    string strDestinationPath = "";
        //    strDestinationPath = clsGeneral.gstrgloEMRStartupPath + "\\Temp";
        //    WebClient fileReader = new WebClient();
        //    try
        //    {
        //  // fileReader.
        //        filename = temppath.Substring(temppath.LastIndexOf("/") + 1);
        //        //filename = "AssociationSP.xml"
        //        // fileReader.Credentials = New System.Net.NetworkCredential("administrator", "glodom2009", "glodom")
        //        fileReader.Credentials = CredentialCache.DefaultCredentials;
        //        fileReader.DownloadFile(temppath, strDestinationPath + "\\" + filename);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        //if file is not downloaded return false.
        //        return false;
        //    }
        //    finally { fileReader.Dispose(); }
        //}

        private void UploadTemplateforFormglrytoglobal(DataTable serverdt)
        {






            string FolderNm = "";
           // string FileNm = "";
            string Wpath = "";

            //FolderNm = clsGeneral.ClinicWebFolder + "/" + ObjucTemplates.flxTemplates.Rows[i][ObjucTemplates.COL_CATEGORY].ToString();
            FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + "Consent Form";// ObjucTemplates.flxTemplates.Rows[i][ObjucTemplates.COL_CATEGORY].ToString();
            Wpath = clsGeneral.Webpath + clsGeneral.WebSite + "/" + clsGeneral.ClinicRepository;

            //Before Uploading Template check whether IsExist.If not then upload & If Exist then ask user. 
            // string _strFileName = ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString() + ".docx";
            string webUrl = Wpath + "/" + FolderNm;// "/" + _strFileName;
            DirectoryInfo dr = new DirectoryInfo(webUrl);

            FileInfo[] flinfo = dr.GetFiles();
            for (int fllenght = 0; fllenght < flinfo.Length; fllenght++)
            {
                string filename = flinfo[fllenght].Name;
            }

        }

        private bool UploadShareAppConfToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool changedata = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string AppConfLocal = clsGeneral.gstrappconfflnm + "Local";
            string AppConfLocalSRV = clsGeneral.gstrappconfflnm;//"SmartDxAssociation";

            //     string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.gstrappconfflnm + ".xml";   //SmartDxAssociationSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrappconfflnm + "/" + clsGeneral.gstrappconfflnm + ".xml";   //SmartDxAssociationSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml"; //"SmartDxAssociation.xml";
                dtAppconfData.TableName = "AppConf";
                dtAppconfData.WriteXml(strName);

                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {

                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocalSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                changedata = objclsAppconf.CompareXMlData(dtAppconfData, dt, strName);
                            }
                        }

                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, AppConfLocal, AppConfLocalSRV, clsGeneral.gstrappconfflnm);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Appointment Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Appointment Configuration", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    //   objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareAppConfToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string AppConfLocal = clsGeneral.gstrappconfflnm + "Local";
            string AppConfLocalSRV = clsGeneral.gstrappconfflnm;//"SmartDxAssociation";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrappconfflnm + "/" + clsGeneral.gstrappconfflnm + ".xml";
            bool changedata = true;
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml";
                string FileNMSP = "";
                string strName = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml";
                dtAppconfData.TableName = "AppConfData";
                dtAppconfData.WriteXml(strName);
                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocalSRV + ".xml";//SmartDxAssociationSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                changedata = objclsAppconf.CompareXMlData(dtAppconfData, dt, strName);
                            }
                        }


                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml"; //SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, AppConfLocal, AppConfLocalSRV, clsGeneral.gstrappconfflnm);// SmartDxAssociationLocal, SmartDxAssociationSRV);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Appointment Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Appointment Configuration", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }



            }
            catch //(Exception ex)
            {
                //  MessageBox.Show(ex.Message); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString(), "Appointment Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private void getdataforblConfuploading()
        {
            dtBlconfData.Rows.Clear();

            dtICD9 = (DataTable)objUCBillConf.flxICD.DataSource;
            if (dtICD9 != null)
                dtICD9.TableName = "ICD9";

            dtCPT = (DataTable)objUCBillConf.flxCPT.DataSource;
            if (dtCPT != null)
                dtCPT.TableName = "CPT";
            dtCat = (DataTable)objUCBillConf.flxCat.DataSource;
            if (dtCat != null)
                dtCat.TableName = "Category";

            dtMod = (DataTable)objUCBillConf.flxMod.DataSource;
            if (dtMod != null)
                dtMod.TableName = "Modifier";

            dtPatr = (DataTable)objUCBillConf.flxPatr.DataSource;
            if (dtPatr != null)
                dtPatr.TableName = "PatientRelation";

            dtpln = (DataTable)objUCBillConf.flxPln.DataSource;
            if (dtpln != null)
                dtpln.TableName = "Plan";
            dtspec = (DataTable)objUCBillConf.flxSpec.DataSource;
            if (dtspec != null)
                dtspec.TableName = "Speciality";
            //   dtICD9.Clone();  

            AddblConfData(dtICD9);
            AddblConfData(dtCPT);
            AddblConfData(dtCat);
            AddblConfData(dtMod);
            AddblConfData(dtPatr);
            AddblConfData(dtpln);
            AddblConfData(dtspec);
        }

        public void AddblConfData(DataTable dt)
        {
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {

                    dt.AcceptChanges();
                    if (dtBlconfData.Rows.Count == 0)

                        dtBlconfData = dt.Clone();

                    DataRow[] dr = dt.Select("Select='True'");
                    for (int len = 0; len < dr.Length; len++)
                    {
                        dtBlconfData.ImportRow(dr[len]);
                        if ((dt.TableName == "ICD9") || (dt.TableName == "CPT"))
                        {
                            if (arrspec.Contains(dr[len]["Specialty"].ToString().Trim()) == false)
                                arrspec.Add(dr[len]["Specialty"].ToString().Trim());

                        }
                        else
                        {
                            if (dt.TableName == "Speciality")
                            {
                                arrspec.Remove(dr[len]["Description"].ToString().Trim());
                            }
                        }
                    }

                    if (dt.TableName == "Speciality")
                    {
                        if (arrspec.Count > 0)
                        {
                            for (int lenarrcnt = 0; lenarrcnt < arrspec.Count; lenarrcnt++)
                            {
                                DataRow[] drr = dt.Select("Description='" + arrspec[lenarrcnt].ToString() + "'");
                                if (drr.Length > 0)
                                    dtBlconfData.ImportRow(drr[0]);
                            }
                        }
                    }
                }
            }
        }

        private void mnuMainUploadgloCommunity_Click(object sender, EventArgs e)
        {
            switch (strItemMenu)
            {
                case "Template":
                    {

                        try
                        {
                            UploadShareTempToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.TemplateGallery, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Template Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Template Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "SmartDx":
                    {
                        try
                        {
                            UploadShareSmartDxToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical SmartDX Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);  
                            //   gloAuditTrail.gloAuditTrail.UpdateLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical SmartDX Upload", gloAuditTrail.ActivityOutCome.Failure);

                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical SmartDX Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                        }
                        break;

                    }
                case "LiquidData":
                    {
                        try
                        {
                            UploadShareLiquidDataToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Liquid Data Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);            
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Liquid Data Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }

                case "TaskMail":
                    {
                        try
                        {
                            getTaskMailClinicalData();
                        }
                        catch (Exception ex)
                        {
                            // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Task, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical TaskMail Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical TaskMail Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "IMSetup":
                    {
                        try
                        {
                            UploadShareIMSetupToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "CPT":
                    {
                        try
                        {
                            UploadShareSmartCPTToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //     gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.None, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.LoadSmartTreatment, "gloCommunity:-Error while Clinical CPT Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical CPT Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "BillingConf":
                    {
                        try
                        {
                            getdataforblConfuploading();
                            UploadShareBillConfToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Billing, gloAuditTrail.ActivityCategory.BillingCharges, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Billing Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                  
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Billing Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "Order":
                    {
                        try
                        {
                            UploadShareSmartOrderToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Orders, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Order Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                     
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Order Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                        }
                        break;
                    }
                case "History":
                    {
                        try
                        {
                            UploadShareHistoryToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical History Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }

                case "AppointmentConfigure":
                    {

                        try
                        {
                            getdataforAppConfuploading();

                            UploadShareAppConfToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //  gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Appointment, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Appointment Configure Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                     
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Appointment Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;

                    }
                case "CVSetup":
                    {
                        try
                        {
                            UploadShareCVSetupToClinicRepository();
                        }
                        catch (Exception ex)
                        {
                            //   gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.CardioVascular, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.None, "gloCommunity:-Error while Clinical Cardio Vascular Upload", 0, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);                     
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical CVSetup Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }

                case "Flowsheet":
                    {
                        try
                        {
                            if (getdataforflowsheetuploading() == true)
                            {


                                UploadShareFLowsheetToClinicRepository();
                            }
                            else
                            {
                                MessageBox.Show("Select at least one Flowsheet to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }



                        }
                        catch (Exception ex)
                        {
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Flowsheet Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                        }
                        break;
                    }
                case "DmSetUp":
                    {
                        try
                        {
                            UploadShareDmSetupToClinicRepository();

                        }
                        catch (Exception ex)
                        {
                            //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical DmSetUp Upload");
                            gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                        }
                        break;
                    }


                case "Formgallery":
                    try
                    {
                        if (getdataforformgalleryuploading("Clinic") == false)
                            MessageBox.Show("Select at least one CPT Code to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                        {
                            UploadShareFormglryToClinicRepository();
                        }

                    }
                    catch (Exception ex)
                    {
                        //clsGeneral.UpdateLog("gloCommunity:-Error while Clinical Formgallery Upload");
                        gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.gloCommunity, gloAuditTrail.ActivityCategory.None, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);

                    }

                    break;
            }
        }

        private bool UploadShareBillConfToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool changedata = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string BillConfLocal = clsGeneral.gstrblconfflnm + "Local";
            string BillConfLocalSRV = clsGeneral.gstrblconfflnm;//"SmartDxAssociation";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrblconfflnm + "/" + clsGeneral.gstrblconfflnm + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocal + ".xml";
                string FileNMSP = "";
                string strName = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocal + ".xml";
                dtBlconfData.TableName = "BillConfData";
                dtBlconfData.WriteXml(strName);
                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocalSRV + ".xml";//SmartDxAssociationSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                changedata = objblconf.CompareXMlData(dtBlconfData, dt, strName);
                            }
                        }


                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocal + ".xml"; //SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, BillConfLocal, BillConfLocalSRV, clsGeneral.gstrblconfflnm);// SmartDxAssociationLocal, SmartDxAssociationSRV);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Billing Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Billing Configuration", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);



                    }
                    else
                    {
                        MessageBox.Show("No new data for uploading", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                // MessageBox.Show(ex.Message); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString(), "Billing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareBillConfToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool changedata = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string BillConfLocal = clsGeneral.gstrblconfflnm + "Local";
            string BillConfLocalSRV = clsGeneral.gstrblconfflnm;//"SmartDxAssociation";

            //   string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrTskMlflnm + ".xml";   //SmartDxAssociationSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrblconfflnm + "/" + clsGeneral.gstrblconfflnm + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocal + ".xml"; //"SmartDxAssociation.xml";
                dtBlconfData.TableName = "BillConfData";
                dtBlconfData.WriteXml(strName);



                //  if (CreateXML(SmartDxAssociationLocal + ".xml") == true)
                // {

                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {

                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocalSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            if (serverdata.Tables.Count > 0)
                            {
                                DataTable dt = serverdata.Tables[0];
                                changedata = objblconf.CompareXMlData(dtBlconfData, dt, strName);
                            }

                        }

                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + BillConfLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, BillConfLocal, BillConfLocalSRV, clsGeneral.gstrblconfflnm);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("Billing Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded Billing Configuration", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    //   objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //  MessageBox.Show(ex.Message); 
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString(), "Billing Configuration", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                objgloCommunity = null;
            }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private void ts_gloCommunityDownload_ButtonClick(object sender, EventArgs e)
        {
            if (strItemMenu == "SmartDx")
            {
                SaveAssociation();
            }

            if (strItemMenu == "Template")
            {
                objclstemplate = new clsTemplate();
                if (ObjucTemplates.chkdncon.Checked == true)
                    DownloadContent();

                DownloadTemplate();
            }

            if (strItemMenu == "LiquidData")
            {
                if (ImportLiquidDataFromgloCommunity() == true)
                    MessageBox.Show("Liquid Data download successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Fail to download Liquid Data", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (strItemMenu == "TaskMail")
            {
                DownloadTaskMailData();
            }

            if (strItemMenu == "IMSetup")
            {
                SaveImSetup();
            }

            if (strItemMenu == "CPT")
            {
                SaveSmartCPTAssociation();
            }

            if (strItemMenu == "Order")
            {
                SaveSmartOrderAssociation();
            }

            if (strItemMenu == "BillingConf")
            {
                DownloadBillingConf();
            }

            if (strItemMenu == "AppointmentConfigure")
            {
                DownloadAppointmentConf();
            }
            if (strItemMenu == "CVSetup")
            {
                SaveCVSetup();
            }

            if (strItemMenu == "History")
            {
                SaveHistory();
            }
            if (strItemMenu == "Flowsheet")
            {
                //   DataRow[] drflow = showflowsheetdata.Select("FlowsheetName='" + flowsheetname + "'");
                SaveFlowsheet();
            }
            if (strItemMenu == "Formgallery")
            {
                //   DataRow[] drflow = showflowsheetdata.Select("FlowsheetName='" + flowsheetname + "'");
                SaveFormgallery();
            }
            if (strItemMenu == "DmSetUp")
            {
                SaveDmSetup();
            }
        }

        private void SaveFormgallery()
        {
            objclsglocomm = new clsgloCommunity();
            string strTempName = "";
            string strInsCptTemplate = "";
            bool datatodownload = false;
            bool Datainserted = false;
            DataRow[] dr = null;
            DataRow[] drcode = null;
            DialogResult Result;
            ArrayList ArrCptCode = new ArrayList(); //taken for not inserting Associated CPT Code 
            try
            {
                if (objUCFormgallery.dtCPTDesc != null)
                {
                    TreeNode RootNode = objUCFormgallery.trvcptassoc.Nodes[0];
                    DataTable dtAssoCptCode = objclsformgallery.getAssociatedCPTCode();
                    foreach (TreeNode trvnode in RootNode.Nodes)
                    {
                        if (trvnode.Checked == true)
                        {
                            drcode = dtAssoCptCode.Select("CPTCode='" + trvnode.Tag.ToString().Replace("'", "''") + "'");


                            if (drcode.Length >= 1)//If CPT Code already associated with template then Ask for Overwrite
                            {
                                Result = MessageBox.Show("For CPT Code '" + trvnode.Tag.ToString() + "'  already associated do you want to overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                                if (Result == DialogResult.Yes)
                                {
                                    dr = objUCFormgallery.dtCPTDesc.Select("CPTCode='" + trvnode.Tag.ToString().Replace("'", "''") + "'");
                                    if (dr.Length >= 1)
                                    {
                                        strTempName += dr[0][2].ToString() + ";";
                                        datatodownload = true;
                                        strInsCptTemplate = dr[0][2].ToString() + ";";
                                    }
                                }

                                else
                                {
                                    ArrCptCode.Add(trvnode.Tag.ToString());


                                }
                            }
                            else
                            {






                                dr = objUCFormgallery.dtCPTDesc.Select("CPTCode='" + trvnode.Tag.ToString().Replace("'", "''") + "'");
                                if (dr.Length >= 1)
                                {
                                    strTempName += dr[0][2].ToString() + ";";
                                    datatodownload = true;
                                    strInsCptTemplate = dr[0][2].ToString() + ";";
                                }

                            }


                        }
                    }
                    if ((datatodownload == false) && (ArrCptCode.Count == 0))
                    {
                        MessageBox.Show("Select at least one CPT Code to download ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else
                    {
                        if (strTempName.Trim() != "")
                        {
                            string[] splstrtempname = strTempName.Split(';');
                            string TempNames = "";
                            for (int len = 0; len < splstrtempname.Length; len++)
                            {
                                if (splstrtempname[len].Trim() != "")
                                {
                                    string TemplateName = splstrtempname[len].ToString().Substring(0, splstrtempname[len].ToString().IndexOf("≈"));
                                    string CategoryName = splstrtempname[len].ToString().Substring(splstrtempname[len].ToString().IndexOf("≈") + 1, (splstrtempname[len].ToString().Length - (splstrtempname[len].ToString().IndexOf("≈") + 1)));

                                    TempNames += "'" + TemplateName + "'" + ",";
                                }
                            }

                            if (TempNames.Length >= 1)
                            {
                                TempNames = TempNames.Substring(0, TempNames.Length - 1);
                                DataTable TemplateList = objclsformgallery.getTemplateNames(TempNames);
                                if (objUCFormgallery.gblnfrmglryclinic == true)

                                    Datainserted = DownloadFormgalleryTemplate(splstrtempname, TemplateList, "Clinic");
                                else
                                    Datainserted = DownloadFormgalleryTemplate(splstrtempname, TemplateList, "Global");

                            }
                        }

                    }




                    //Inserting Relation For CPTTemplate Table

                    if (objUCFormgallery.dtCPTDesc != null)
                    {
                        RootNode = objUCFormgallery.trvcptassoc.Nodes[0];
                    //    string TempNames = "";
                        foreach (TreeNode trvnode in RootNode.Nodes)
                        {
                            if (trvnode.Checked == true)
                            {
                                if (ArrCptCode.Contains(trvnode.Tag.ToString()) == false)
                                {
                                    dr = objUCFormgallery.dtCPTDesc.Select("CPTCode='" + trvnode.Tag.ToString().Replace("'", "''") + "'");
                                    if (dr.Length >= 1)
                                    {

                                        if (Datainserted == false)
                                        {
                                            Datainserted = objclsformgallery.InsertCptTemplate(dr[0][2].ToString().Replace('≈', '~'), trvnode.Tag.ToString().Replace("'", "''"));

                                        }
                                        else
                                        {
                                            Datainserted = objclsformgallery.InsertCptTemplate(dr[0][2].ToString().Replace('≈', '~'), trvnode.Tag.ToString().Replace("'", "''"));
                                            Datainserted = true;


                                        }
                                    }
                                }

                            }

                        }
                    }








                }
                if (Datainserted == true)
                {
                    MessageBox.Show("Form gallery downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            catch
            {

            }
            finally
            {

                ArrCptCode.Clear();
                ArrCptCode = null;
                objclsglocomm = null;

            }

        }

        private bool DownloadFormgalleryTemplate(string[] strtempname, DataTable TemplateList, string Type)
        {

            ArrayList arrtemp = new ArrayList();
            bool DataDownloaded = false;
        //    DialogResult Result;
            for (int len = 0; len < strtempname.Length; len++)
            {
                if ((arrtemp.Contains(strtempname[len]) == false) && (strtempname[len].Trim().Length > 0))
                {

                    string TemplateName = strtempname[len].ToString().Substring(0, strtempname[len].ToString().IndexOf("≈"));
                    string CategoryName = strtempname[len].ToString().Substring(strtempname[len].ToString().IndexOf("≈") + 1, (strtempname[len].ToString().Length - (strtempname[len].ToString().IndexOf("≈") + 1)));


                    arrtemp.Add(strtempname[len]);
                    DataRow[] dr = TemplateList.Select("TemplateName='" + TemplateName.Replace("'", "''") + "' and CategoryName='" + CategoryName.Replace("'", "''") + "'");
                    if (dr.Length == 0)
                    {
                        //    Result = MessageBox.Show("Template '" + strtempname[len].ToString() + "' Already Exist     Already Exist Do you want to Overwrite ?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                        //    if (Result == DialogResult.Yes)
                        //    {
                        //        DownloadFormgalleryTemplateFromSite(strtempname[len].ToString(), Type, dr[0][0].ToString());
                        //    }
                        //}
                        //else
                        //{
                        if (DataDownloaded == true)
                        {
                            DataDownloaded = DownloadFormgalleryTemplateFromSite(strtempname[len].ToString(), Type);

                            DataDownloaded = true;
                        }
                        else
                        {
                            DataDownloaded = DownloadFormgalleryTemplateFromSite(strtempname[len].ToString(), Type);

                        }

                    }
                }
            }
            if (DataDownloaded == true)
            {
                // MessageBox.Show("Form gallery Downloaded Successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            return DataDownloaded;

        }

        private bool DownloadFormgalleryTemplateFromSite(string FileName, string Type, string tempid = "0")
        {
            bool inserted = false;
            string TemplateName = FileName.ToString().Substring(0, FileName.ToString().IndexOf("≈"));
            string CategoryName = FileName.ToString().Substring(FileName.ToString().IndexOf("≈") + 1, (FileName.ToString().Length - (FileName.ToString().IndexOf("≈") + 1)));



            string FolderNm = "";
            string Wpath = "";
            string gWebpath = "";
            string gWebFolder = "";
            string ClinicGblFolder = "";
            if (Type == "Clinic")
            {
                FolderNm = CategoryName + "/" + TemplateName + ".docx";
                Wpath = clsGeneral.Webpath + clsGeneral.WebSite + "/" + clsGeneral.ClinicRepository + "/" + clsGeneral.ClinicWebFolder;
            }
            else
            {

                //     strSitepath = "http://" & gstrSharepointSrvNm & "/" & gstrSharepointSiteNm & ""
                gWebpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;

                gWebFolder = clsGeneral.GlobalRepository;


                // gWebSite = clsGeneral.gstrSharepointSiteNm;  

                //  FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + CategoryName + "/" + FileName + ".docx";
                FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + CategoryName + "/" + TemplateName + ".docx";




                ClinicGblFolder = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder;



                Wpath = gWebpath + "/" + gWebFolder;


                //     FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.GlobalRepository  + "/" + "Soap";
                //  Wpath = clsGeneral.Webpath + clsGeneral.WebSite + "/" + clsGeneral.GlobalRepository;

            }

            string strURL = Wpath + "/" + FolderNm;
            HttpWebRequest request = default(HttpWebRequest);
            HttpWebResponse response = null;
            try
            {

                request = (HttpWebRequest)WebRequest.Create(strURL);
                request.Credentials = System.Net.CredentialCache.DefaultCredentials;
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
                //objcls.AddNewTemplateGallery(0, strFileName.Substring(0, strFileName.IndexOf(".")), 0, GloUC_trvCategory.SelectedNode.Text, 0, read)
                // objclstemplate.AddNewTemplateGallery(0, strFileName.Substring(0, strFileName.IndexOf(".")), 0, ObjucTemplates.cmbCategory.Text, 0, read);
                s.Close();
                response.Close();
                //bool inserted=   objclsformgallery.AddNewTemplateGallery(Convert.ToInt64(tempid), FileName, "SOAP", 0, read);
                inserted = objclsformgallery.AddNewTemplateGallery(Convert.ToInt64(tempid), TemplateName, CategoryName, 0, read);

                return inserted;
            }
            catch //(Exception ex)
            {
                // MessageBox.Show(ex.Message);
                return false;
            }








        }

        private void SaveFlowsheet()
        {
            //objclsflowsheet 
            List<FlowSheetFields> objclsfield = new List<FlowSheetFields>();
            TreeNode rootnode = objUCFlowsheet.trvflshname.Nodes[0];
            foreach (TreeNode trnode in rootnode.Nodes)// objUCFlowsheet.trvflshname.Nodes)
            {
                if (trnode.Checked == true)
                {
                    DataRow[] drflow = objUCFlowsheet.showflowsheetdata.Select("FlowsheetName= '" + trnode.Text.Trim().Replace("'", "''") + "'");

                    for (int cnt = 0; cnt < drflow.Length; cnt++)
                    {
                        FlowSheetFields objfl = new FlowSheetFields();
                        objfl.Alignment = Convert.ToString(drflow[cnt]["Alignment"]);
                        objfl.BackColor = Convert.ToInt64(drflow[cnt]["BackColor"]);
                        objfl.bIsBold = Convert.ToBoolean(drflow[cnt]["bIsBold"]);
                        objfl.bIsItalic = Convert.ToBoolean(drflow[cnt]["bIsItalic"]);
                        objfl.bIsUnderline = Convert.ToBoolean(drflow[cnt]["bIsUnderline"]);
                        objfl.ForeColor = Convert.ToDouble(drflow[cnt]["ForeColor"]);
                        objfl.FontSize = Convert.ToInt32(drflow[cnt]["FontSize"]);
                        objfl.FontName = Convert.ToString(drflow[cnt]["FontName"]);
                        objfl.Width = Convert.ToSingle(drflow[cnt]["Width"]);
                        objfl.Format = Convert.ToString(drflow[cnt]["Format"]);
                        objfl.ColumnName = Convert.ToString(drflow[cnt]["ColumnName"]);
                        objfl.Cols = Convert.ToInt32(drflow[cnt]["Cols"]);
                        objfl.FlowsheetName = Convert.ToString(drflow[cnt]["FlowsheetName"]);
                        objfl.ColNumber = Convert.ToInt32(drflow[cnt]["Column Number"]);

                        objclsfield.Add(objfl);

                    }

                }
            }
            if (objclsfield.Count > 0)
            {
                objclsflowsheet.InsertFlowSheetData(objclsfield);
            }
            else
            {
                //Added by kanchan on 20120105
                if (rootnode.Nodes.Count > 0)
                {
                    MessageBox.Show("Select at least one Flowsheet to download.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }

        }

        private void DownloadAppointmentConf()
        {


            bool DataInserted = false;
            try
            {


                DataTable dtResource = null;
                DataTable dtFollowup = null;
                DataTable dtProblem = null;
                DataTable dtDept = null;
                DataTable dtApptStat = null;
                DataTable dtApptType = null;
                DataTable dtApptBlk = null;
                DataTable dtLoc = null;

                dtResource = (DataTable)objUCAppConf.flxRes.DataSource;
                dtFollowup = (DataTable)objUCAppConf.flxfollup.DataSource;
                dtProblem = (DataTable)objUCAppConf.flxPrb.DataSource;
                dtApptType = (DataTable)objUCAppConf.flxAppt.DataSource;
                dtApptStat = (DataTable)objUCAppConf.flxApptstat.DataSource;
                dtApptBlk = (DataTable)objUCAppConf.flxApptblk.DataSource;
                dtLoc = (DataTable)objUCAppConf.flxLoc.DataSource;
                dtDept = (DataTable)objUCAppConf.flxDept.DataSource;


                //dtDept.AcceptChanges();
                //dtApptStat.AcceptChanges();
                //dtApptType.AcceptChanges();  
                //dtApptBlk.AcceptChanges();
                //dtLoc.AcceptChanges();
                string msg = "";

                if (dtResource != null)
                {
                    dtResource.AcceptChanges();

                    DataRow[] drRes = dtResource.Select("Select='true'");
                    for (int lendr = 0; lendr < drRes.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertResource(drRes[lendr]["Code"].ToString(), drRes[lendr]["Resource Name"].ToString(), Convert.ToInt64(drRes[lendr]["nResourceTypeID"]), false, drRes[lendr]["User Name"].ToString(), clsGeneral.gClinicID);
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Resource name '" + drRes[lendr]["Resource Name"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }


                }



                if (dtFollowup != null)
                {
                    dtFollowup.AcceptChanges();

                    DataRow[] drFollup = dtFollowup.Select("Select='true'");
                    for (int lendr = 0; lendr < drFollup.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertFollowup(drFollup[lendr]["Follow Up Name"].ToString(), Convert.ToInt64(drFollup[lendr]["Duration"]), drFollup[lendr]["sCriteria"].ToString());
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("FolloWup '" + drFollup[lendr]["Follow Up Name"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }


                }


                if (dtProblem != null)
                {
                    dtProblem.AcceptChanges();

                    DataRow[] drProb = dtProblem.Select("Select='true'");
                    for (int lendr = 0; lendr < drProb.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertProblem(drProb[lendr]["Problem Type"].ToString(), Convert.ToInt32(drProb[lendr]["Duration"]), drProb[lendr]["Color Codes"].ToString(), Convert.ToInt32(drProb[lendr]["nAppProcType"]), Convert.ToInt32(drProb[lendr]["nAppointmentTypeFlag"]));
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Problem type '" + drProb[lendr]["Problem Type"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }


                }

                if (dtApptType != null)
                {
                    dtApptType.AcceptChanges();
                    DataRow[] drAppttype = dtApptType.Select("Select='true'");
                    for (int lendr = 0; lendr < drAppttype.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertAppointmentType(drAppttype[lendr]["Appointment Type"].ToString(), drAppttype[lendr]["Duration"].ToString(), drAppttype[lendr]["Color Codes"].ToString(), Convert.ToInt32(drAppttype[lendr]["nAppProcType"]), Convert.ToInt32(drAppttype[lendr]["nAppointmentTypeFlag"]));

                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Appointment type '" + drAppttype[lendr]["Appointment Type"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }

                }


                if (dtApptStat != null)
                {
                    dtApptStat.AcceptChanges();
                    DataRow[] drApptStat = dtApptStat.Select("Select='true'");
                    for (int lendr = 0; lendr < drApptStat.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertAppointmentstatus(drApptStat[lendr]["Appointment Status"].ToString());//,, Convert.ToInt32(drApptStat[lendr]["Duration"]), drAppttype[lendr]["Color Code"].ToString(),Convert.ToInt32 (drAppttype[lendr]["nAppProcType"]), Convert.ToInt32(drAppttype[lendr]["nAppointmentTypeFlag"])););
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Appointment status '" + drApptStat[lendr]["Appointment Status"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }

                }

                if (dtApptBlk != null)
                {

                    dtApptBlk.AcceptChanges();
                    DataRow[] drApptBlk = dtApptBlk.Select("Select='true'");
                    for (int lendr = 0; lendr < drApptBlk.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertAppointmentBlock(drApptBlk[lendr]["Appointment Block Type"].ToString());//,, Convert.ToInt32(drApptStat[lendr]["Duration"]), drAppttype[lendr]["Color Code"].ToString(),Convert.ToInt32 (drAppttype[lendr]["nAppProcType"]), Convert.ToInt32(drAppttype[lendr]["nAppointmentTypeFlag"])););
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Appointment block '" + drApptBlk[lendr]["Appointment Block Type"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }

                }


                if (dtLoc != null)
                {
                    dtLoc.AcceptChanges();
                    DataRow[] drLoc = dtLoc.Select("Select='true'");
                    for (int lendr = 0; lendr < drLoc.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertLocation(drLoc[lendr]["Location"].ToString(), drLoc[lendr]["Address Line1"].ToString(), drLoc[lendr]["Address Line2"].ToString(), drLoc[lendr]["City"].ToString(), drLoc[lendr]["State"].ToString(), drLoc[lendr]["Zip"].ToString(), drLoc[lendr]["Country"].ToString(), drLoc[lendr]["County"].ToString());//,, Convert.ToInt32(drApptStat[lendr]["Duration"]), drAppttype[lendr]["Color Code"].ToString(),Convert.ToInt32 (drAppttype[lendr]["nAppProcType"]), Convert.ToInt32(drAppttype[lendr]["nAppointmentTypeFlag"])););
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Location '" + drLoc[lendr]["Location"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }


                }

                if (dtDept != null)
                {
                    dtDept.AcceptChanges();
                    DataRow[] drDept = dtDept.Select("Select='true'");
                    for (int lendr = 0; lendr < drDept.Length; lendr++)
                    {
                        msg = objclsAppconf.InsertDepartment(drDept[lendr]["Department"].ToString(), Convert.ToInt64(drDept[lendr]["nLocationID"]));
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        if (msg.Trim().Length > 0)
                        {
                            MessageBox.Show("Department '" + drDept[lendr]["Department"].ToString() + "'already exist  ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }


                }






                gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate();
                try
                {
                    foreach (TreeNode trtempnode in objUCAppConf.trvAppointmentBook.Nodes[0].Nodes)
                    {


                        if (trtempnode.Checked == true)
                        {

                            AppointmentTemplate oTemplate = new AppointmentTemplate();
                            oTemplate.TemplateID = 0;
                            oTemplate.TemplateName = trtempnode.Text.Trim();

                            DataRow[] drr = objUCAppConf.dtTemplate.Select("ParentAppointmentTemplate='" + trtempnode.Text.Trim() + "'");
                            for (int len = 0; len < drr.Length; len++)
                            {

                                AppointmentTemplateLine oAppType = new AppointmentTemplateLine();
                                oAppType.AppointmentTypeID = 0;//Convert.ToInt64(drr[len]["AppointmentTypeID"].ToString());
                                oAppType.AppointmentTypeCode = drr[len]["Code"].ToString();
                                oAppType.AppointmentTypeDesc = drr[len]["Description"].ToString();
                                //   if (Convert.ToString( drr[len]["TemplateLineNo"].ToString().Trim()) != "")
                                //{
                                //    oAppType.TemplateLineNo = Convert.ToInt64(drr[len]["TemplateLineNo"].ToString());
                                // }
                                // else
                                //  {
                                oAppType.TemplateLineNo = 0;
                                // }


                                // oAppType.EndTime = Convert.ToInt32(Convert.ToString(c1Template.GetData(i, 4)).Replace(":", ""));
                                oAppType.StartTime = Convert.ToInt32(drr[len]["StartTime"].ToString());
                                oAppType.EndTime = Convert.ToInt32(drr[len]["EndTime"].ToString());//gloTime.TimeAsNumber(Convert.ToString(drr[len]["EndTime"].ToString()));
                                oAppType.ColorCode = Convert.ToInt32(drr[len]["ColorCode"].ToString()); //gloTime.TimeAsNumber(Convert.ToString(drr[len]["ColorCode"].ToString())); //c1Template.GetCellRange(i, 5).Style.BackColor.ToArgb();
                                oAppType.LocationID = Convert.ToInt64(drr[len]["nLocationID"]);
                                oAppType.LocationName = Convert.ToString(drr[len]["Location"]);
                                oAppType.DepartmentID = -1;//Convert.ToInt64(drr[len]["DepartmentID"]);
                                oAppType.DepartmentName = Convert.ToString(drr[len]["Department"]);
                                oTemplate.TemplateDetails.Add(oAppType);



                            }

                            if (drr.Length > 0)
                            {
                                //  DataInserted = true;
                                ogloTemplate.Add(oTemplate);
                            }
                        }//if
                    }

                }
                catch
                {

                }

            }

            catch
            {

            }
            if (DataInserted == true)
            {


                MessageBox.Show("Appointment configuration downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


            }
        }

        //private bool SaveTemplate()
        //{
        //    bool Result = false;
        //    try
        //    {
        //       // if (ValidateData() == true)
        //     //   {
        //            gloAppointmentTemplate ogloTemplate = new gloAppointmentTemplate( );
        //            AppointmentTemplate oTemplate = new AppointmentTemplate();
        //            oTemplate.TemplateID = _templateID;
        //            oTemplate.TemplateName = txtTemplateName.Text.Trim();
        //            for (int i = 1; i < c1Template.Rows.Count; i++)
        //            {
        //                //Appointment type selected (Checked)
        //                //if (Convert.ToBoolean(c1Template.GetData(i, 1)) == true)
        //                //{
        //                AppointmentTemplateLine oAppType = new AppointmentTemplateLine();
        //                oAppType.AppointmentTypeID = Convert.ToInt64(c1Template.GetData(i, 0));
        //                oAppType.AppointmentTypeCode = Convert.ToString(c1Template.GetData(i, 10));
        //                oAppType.AppointmentTypeDesc = Convert.ToString(c1Template.GetData(i, 2));
        //                if (Convert.ToString((c1Template.GetData(i, 1))).Trim() != "")
        //                {
        //                    oAppType.TemplateLineNo = Convert.ToInt64(c1Template.GetData(i, 1));
        //                }
        //                else
        //                {
        //                    oAppType.TemplateLineNo = 0;
        //                }


        //                // oAppType.EndTime = Convert.ToInt32(Convert.ToString(c1Template.GetData(i, 4)).Replace(":", ""));
        //                oAppType.StartTime = gloTime.TimeAsNumber(Convert.ToString((c1Template.GetData(i, 3))));
        //                oAppType.EndTime = gloTime.TimeAsNumber(Convert.ToString((c1Template.GetData(i, 4))));
        //                oAppType.ColorCode = c1Template.GetCellRange(i, 5).Style.BackColor.ToArgb();
        //                oAppType.LocationID = Convert.ToInt64(c1Template.GetData(i, 6));
        //                oAppType.LocationName = Convert.ToString(c1Template.GetData(i, 7));
        //                oAppType.DepartmentID = Convert.ToInt64(c1Template.GetData(i, 8));
        //                oAppType.DepartmentName = Convert.ToString(c1Template.GetData(i, 9));
        //                oTemplate.TemplateDetails.Add(oAppType);
        //                // }
        //            }
        //            Int64 ID = ogloTemplate.Add(oTemplate);
        //            if (ID > 0)
        //            {
        //            //    gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Template, ActivityType.Add, "Add Template", 0, ID, 0, ActivityOutCome.Success);

        //                Result = true;
        //            }
        //            else
        //            {
        //             //   gloAuditTrail.gloAuditTrail.CreateAuditLog(ActivityModule.AppointmentBook, ActivityCategory.Template, ActivityType.Add, "Add Template", 0, ID, 0, ActivityOutCome.Failure);

        //            }
        //      //  }
        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
        //    }
        //    return Result;
        //}

        private void DownloadBillingConf()
        {

            bool DataInserted = false;
            try
            {
                dtICD9 = null;
                dtCPT = null;
                dtCat = null;
                dtMod = null;
                dtPatr = null;
                dtpln = null;
                dtspec = null;

                dtICD9 = (DataTable)objUCBillConf.flxICD.DataSource;
                dtCPT = (DataTable)objUCBillConf.flxCPT.DataSource;
                dtMod = (DataTable)objUCBillConf.flxMod.DataSource;
                dtPatr = (DataTable)objUCBillConf.flxPatr.DataSource;
                dtpln = (DataTable)objUCBillConf.flxPln.DataSource;
                dtspec = (DataTable)objUCBillConf.flxSpec.DataSource;
                dtCat = (DataTable)objUCBillConf.flxCat.DataSource;


                dtICD9.AcceptChanges();
                dtCPT.AcceptChanges();
                dtCat.AcceptChanges();
                dtMod.AcceptChanges();
                dtPatr.AcceptChanges();
                dtpln.AcceptChanges();
                dtspec.AcceptChanges();

                if (dtCat != null)
                {


                    DataRow[] drCat = dtCat.Select("Select='true'");
                    for (int lendr = 0; lendr < drCat.Length; lendr++)
                    {
                        string msg = objblconf.InsertCategory(drCat[lendr]["Description"].ToString(), drCat[lendr]["Category Type"].ToString(), clsGeneral.gClinicID);


                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        else
                        {
                            MessageBox.Show("Category already exist for description '" + Convert.ToString(drCat[lendr]["Description"]) + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }
                }


                if (dtspec != null)
                {


                    DataRow[] drspec = dtspec.Select("Select='true'");
                    for (int lendr = 0; lendr < drspec.Length; lendr++)
                    {
                        string msg = objblconf.InsertSpeciality(drspec[lendr]["Classification"].ToString(), drspec[lendr]["Taxonomy Code"].ToString(), drspec[lendr]["Taxonomy Description"].ToString(), drspec[lendr]["Code"].ToString(), drspec[lendr]["Description"].ToString(), clsGeneral.gClinicID, false);
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        else
                        {
                            MessageBox.Show("Specialty already exist for taxonomy code '" + Convert.ToString(drspec[lendr]["Taxonomy Code"]) + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                    }


                }


                if (dtpln != null)
                {

                    DataRow[] drpln = dtpln.Select("Select='true'");
                    for (int lendr = 0; lendr < drpln.Length; lendr++)
                    {
                        string msg = objblconf.InsertPlan(drpln[lendr]["Type Description"].ToString(), drpln[lendr]["Type Code"].ToString(), clsGeneral.gClinicID, false);

                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }

                        else
                        {
                            MessageBox.Show("Plan already exist for code '" + Convert.ToString(drpln[lendr]["Type Code"]) + "' and type description '" + drpln[lendr]["Type Description"].ToString() + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }


                }




                if (dtPatr != null)
                {


                    DataRow[] drPatr = dtPatr.Select("Select='true'");
                    for (int lendr = 0; lendr < drPatr.Length; lendr++)
                    {
                        //objblconf.InsertPatientRelationShip(RelnDesc,RelnCode,isblk,ClinicID); 
                        string msg = objblconf.InsertPatientRelationShip(drPatr[lendr]["Description"].ToString(), drPatr[lendr]["Code"].ToString(), false, clsGeneral.gClinicID);
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        else
                        {
                            MessageBox.Show("Patient relationship already exist for code '" + Convert.ToString(drPatr[lendr]["Code"]) + "' and description '" + drPatr[lendr]["Description"].ToString() + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }


                }





                if (dtMod != null)
                {


                    DataRow[] drMod = dtMod.Select("Select='true'");
                    for (int lendr = 0; lendr < drMod.Length; lendr++)
                    {
                        //objblconf.InsertModifier(modifcode, Description, ClinicID); 
                        string msg = objblconf.InsertModifier(drMod[lendr]["Code"].ToString(), drMod[lendr]["Description"].ToString(), clsGeneral.gClinicID);
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }
                        else
                        {
                            MessageBox.Show("Modifier already exist for code '" + Convert.ToString(drMod[lendr]["Code"]) + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }


                }



                if (dtICD9 != null)
                {


                    DataRow[] drICD9 = dtICD9.Select("Select='true'");
                    for (int lendr = 0; lendr < drICD9.Length; lendr++)
                    {
                        bool bln = false;
                        if (drICD9[lendr]["Status"].ToString().ToLower() == "inactive")
                            bln = true;
                        DataRow[] drrspec = dtspec.Select("Description='" + drICD9[lendr]["Specialty"].ToString() + "'");
                        if (drrspec.Length > 0)
                        {

                            string msg = objblconf.InsertICD9(drICD9[lendr]["Code"].ToString(), drICD9[lendr]["Description"].ToString(), drICD9[lendr]["Specialty"].ToString(), clsGeneral.gClinicID, bln, drrspec[0]);
                            if (msg.Trim().Length == 0)
                            {
                                DataInserted = true;
                            }
                            else
                            {
                                MessageBox.Show("ICD9 code already exist for code '" + Convert.ToString(drICD9[lendr]["Code"]) + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                        else
                        {
                            string msg = objblconf.InsertICD9(drICD9[lendr]["Code"].ToString(), drICD9[lendr]["Description"].ToString(), drICD9[lendr]["Specialty"].ToString(), clsGeneral.gClinicID, bln, null);
                            if (msg.Trim().Length == 0)
                            {
                                DataInserted = true;
                            }
                            else
                            {
                                MessageBox.Show("ICD9 code already exist for code '" + Convert.ToString(drICD9[lendr]["Code"]) + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }

                        }
                    }


                }



                if (dtCPT != null)
                {


                    DataRow[] drCPT = dtCPT.Select("Select='true'");
                    for (int lendr = 0; lendr < drCPT.Length; lendr++)
                    {
                        if (drCPT[lendr]["Charges"] == DBNull.Value)
                        {
                            drCPT[lendr]["Charges"] = "0.0";
                        }
                        if (drCPT[lendr]["nUnits"] == DBNull.Value)
                        {
                            drCPT[lendr]["nUnits"] = "0.0";
                        }
                        if (drCPT[lendr]["nRate"] == DBNull.Value)
                        {
                            drCPT[lendr]["nRate"] = "0.0";
                        }
                        if (drCPT[lendr]["nClinicFee"] == DBNull.Value)
                        {
                            drCPT[lendr]["nClinicFee"] = "0.0";
                        }

                        DataRow[] drrspec = dtspec.Select("Description='" + Convert.ToString(drCPT[lendr]["specialty"]) + "'");
                        string msg = "";
                        if (drrspec.Length == 0)
                        {

                            msg = objblconf.InsertCPT(Convert.ToString(drCPT[lendr]["CPTCode"]), Convert.ToString(drCPT[lendr]["Description"]), Convert.ToString(drCPT[lendr]["specialty"]), Convert.ToString(drCPT[lendr]["Category Type"]), Convert.ToString(drCPT[lendr]["Category"]), Convert.ToString(drCPT[lendr]["Code Type"]), Convert.ToString(drCPT[lendr]["Code Type"]), Convert.ToString(drCPT[lendr]["Modifier1 Code"]), Convert.ToString(drCPT[lendr]["Modifier2 Code"]), Convert.ToString(drCPT[lendr]["Modifier3 Code"]), Convert.ToString(drCPT[lendr]["Modifier4 Code"]), Convert.ToDouble(drCPT[lendr]["nUnits"]), Convert.ToString(drCPT[lendr]["CPT Drug"]), Convert.ToString(drCPT[lendr]["NDCCode"]), Convert.ToString(drCPT[lendr]["bIsTaxable"]), Convert.ToDouble(drCPT[lendr]["nRate"]), Convert.ToDouble(drCPT[lendr]["Charges"]), Convert.ToDouble(drCPT[lendr]["Allowed"]), "", Convert.ToDouble(drCPT[lendr]["nClinicFee"]), Convert.ToString(drCPT[lendr]["bInactive"]), clsGeneral.gClinicID, Convert.ToString(drCPT[lendr]["Statement Description"]), Convert.ToString(drCPT[lendr]["Revenue Code"]));
                        }
                        else
                        {
                            msg = objblconf.InsertCPT(Convert.ToString(drCPT[lendr]["CPTCode"]), Convert.ToString(drCPT[lendr]["Description"]), Convert.ToString(drCPT[lendr]["specialty"]), Convert.ToString(drCPT[lendr]["Category Type"]), Convert.ToString(drCPT[lendr]["Category"]), Convert.ToString(drCPT[lendr]["Code Type"]), Convert.ToString(drCPT[lendr]["Code Type"]), Convert.ToString(drCPT[lendr]["Modifier1 Code"]), Convert.ToString(drCPT[lendr]["Modifier2 Code"]), Convert.ToString(drCPT[lendr]["Modifier3 Code"]), Convert.ToString(drCPT[lendr]["Modifier4 Code"]), Convert.ToDouble(drCPT[lendr]["nUnits"]), Convert.ToString(drCPT[lendr]["CPT Drug"]), Convert.ToString(drCPT[lendr]["NDCCode"]), Convert.ToString(drCPT[lendr]["bIsTaxable"]), Convert.ToDouble(drCPT[lendr]["nRate"]), Convert.ToDouble(drCPT[lendr]["Charges"]), Convert.ToDouble(drCPT[lendr]["Allowed"]), "", Convert.ToDouble(drCPT[lendr]["nClinicFee"]), Convert.ToString(drCPT[lendr]["bInactive"]), clsGeneral.gClinicID, Convert.ToString(drCPT[lendr]["Statement Description"]), Convert.ToString(drCPT[lendr]["Revenue Code"]), drrspec[0]);

                        }
                        if (msg.Trim().Length == 0)
                        {
                            DataInserted = true;
                        }

                        else
                        {
                            MessageBox.Show("CPT code already exist for code '" + Convert.ToString(drCPT[lendr]["CPTCode"]) + "'", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                    }


                }

                if (DataInserted == true)
                {
                    MessageBox.Show("Billing Configuration downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }


            catch
            {

            }






        }

        private void ts_btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gloCommunityViewData_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (strItemMenu)
            {
                case "Template":
                    {
                        ObjucTemplates.Dispose();
                        ObjucTemplates = null;
                        break;
                    }
                case "SmartDx":
                    {
                        objUCSmartDX.Dispose();
                        objUCSmartDX = null;
                        break;
                    }
                case "LiquidData":
                    {
                        objUCLiquidData.Dispose();
                        objUCLiquidData = null;
                        break;
                    }

                case "TaskMail":
                    {
                        dtTskMlData.Dispose();
                        objuctm.Dispose();
                        dtTskMlData = null;
                        objuctm = null;
                        objclstm = null;
                        break;
                    }
                case "IMSetup":
                    {
                        dtIMSetupData.Dispose();
                        ObjucIMSetup.Dispose();
                        break;
                    }
                case "CPT":
                    {
                        objUCSmartCPT.Dispose();
                        objUCSmartCPT = null;
                        break;
                    }
                case "Order":
                    {
                        objUCSmartOrder.Dispose();
                        objUCSmartOrder = null;
                        break;
                    }
                case "BillingConf":
                    {
                        objUCBillConf.Dispose();
                        objUCBillConf = null;
                        break;
                    }
                case "History":
                    {
                        if (objUCHistory.oHistories != null)//clear the Histories collection
                        {
                            objUCHistory.oHistories.Dispose();
                            objUCHistory.oHistories = null;
                        }
                        objUCHistory.Dispose();
                        objUCHistory = null;
                        break;
                    }
                case "CVSetup":
                    {
                        dtCVSetupData.Dispose();
                        ObjucCVSetup.Dispose();
                        break;
                    }
                case "DmSetUp":
                    {
                        objUCDmSetup.Dispose();
                        objUCDmSetup = null;
                        break;
                    }
            }
        }

        #region "gloCommunity Share Template"

        private bool UploadShareTempToGlobalRepository()
        {
            bool isUploaded = false;
            bool isChecked = false;

            if (ObjucTemplates.flxTemplates.Rows.Count <= 0)
            {
                MessageBox.Show("Select at least one template category to Upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return isUploaded = false;
            }

            this.Cursor = Cursors.WaitCursor;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();

            string gWebpath = clsGeneral.gstrSharepointSrvNm + "/";
            string gWebFolder = clsGeneral.GlobalRepository;
            string gWebSite = clsGeneral.gstrSharepointSiteNm;

            try
            {
                for (int i = 0; i < ObjucTemplates.flxTemplates.Rows.Count; i++)
                {
                    if (ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_SELECT).ToString() == "True")
                    {
                        isChecked = true;
                        string FolderNm = "";
                        string FileNm = "";
                        string Wpath = ""; //'Clinic's Template folder in Global Repository.
                        string ClinicGblFolder = "";

                        //FolderNm = clsGeneral.WebSite + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + ObjucTemplates.flxTemplates.Rows[i][ObjucTemplates.COL_CATEGORY].ToString();
                        FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + ObjucTemplates.flxTemplates.Rows[i][ObjucTemplates.COL_CATEGORY].ToString();
                        //ClinicGblFolder = clsGeneral.WebSite + " " + clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder;
                        ClinicGblFolder = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder;
                        Wpath = gWebpath + gWebSite + "/" + gWebFolder;

                        //Before Uploading Template check whether IsExist.If not then upload & If Exist then ask user. 
                        string _strFileName = ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString() + ".docx";
                        string webUrl = Wpath + "/" + FolderNm + "/" + _strFileName;
                        bool IsTemplateExist = objgloCommunity.DownloadXML(webUrl);//Check for Template exist or not.

                        FileNm = clsGeneral.getTemplateName(ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATEID).ToString(), ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString());

                        if (IsTemplateExist == true)
                        {
                            int _Result = Convert.ToInt32(MessageBox.Show('"' + ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString() + '"' + " already exists on the site? do you want to upload?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));
                            if (_Result == Convert.ToInt32(DialogResult.Yes))
                            {
                                //Overwrite Template
                                isUploaded = true;
                                if (objgloCommunity.UploadFileToDocumentLibrary(Wpath, FolderNm, FileNm, gWebpath, gWebSite, gWebFolder, ClinicGblFolder) != true)
                                {
                                    MessageBox.Show("Failed to upload the template", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    isUploaded = false;
                                    this.Cursor = Cursors.Default;
                                    return isUploaded;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }//IsTemplateExist if end
                        else
                        {
                            //If not present then upload Template.
                            isUploaded = true;
                            if (objgloCommunity.UploadFileToDocumentLibrary(Wpath, FolderNm, FileNm, gWebpath, gWebSite, gWebFolder, ClinicGblFolder) != true)
                            {
                                MessageBox.Show("Failed to upload the template", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                isUploaded = false;
                                this.Cursor = Cursors.Default;
                                return isUploaded;
                            }
                        }//IsTemplateExist else end
                    }//if end
                }//for end

                if (isChecked == true)
                {
                    if (isUploaded == true)
                        //////////MessageBox.Show("Template(s) Uploaded Successfully. \n Please Note: Template(s) will be send to Review and will not be available for other users until approved by the gloCommunity Administrator.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        MessageBox.Show("Template(s) uploaded successfully. \nPlease note: Template(s) will be sent for review and will not be available for other user until approved by the gloCommunity administrator.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Select template(s) for upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }
        private bool UploadShareTempToClinicRepository()
        {
            bool isUploaded = false;
            bool isChecked = false;
            if (ObjucTemplates.flxTemplates.Rows.Count <= 0)
            {
                MessageBox.Show("Select at least one template category to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return isUploaded = false;
            }

            this.Cursor = Cursors.WaitCursor;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();

            try
            {
                for (int i = 0; i < ObjucTemplates.flxTemplates.Rows.Count; i++)
                {
                    if (ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_SELECT).ToString() == "True")
                    {
                        isChecked = true;
                        string FolderNm = "";
                        string FileNm = "";
                        string Wpath = "";

                        FolderNm = clsGeneral.ClinicWebFolder + "/" + ObjucTemplates.flxTemplates.Rows[i][ObjucTemplates.COL_CATEGORY].ToString();
                        // FolderNm = clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder + "/" + ObjucTemplates.flxTemplates.Rows[i][ObjucTemplates.COL_CATEGORY].ToString();

                        Wpath = clsGeneral.Webpath + clsGeneral.WebSite + "/" + clsGeneral.ClinicRepository;

                        //Before Uploading Template check whether IsExist.If not then upload & If Exist then ask user. 
                        string _strFileName = ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString() + ".docx";
                        string webUrl = Wpath + "/" + FolderNm + "/" + _strFileName;
                        bool IsTemplateExist = objgloCommunity.DownloadXML(webUrl);//Check for Template exist or not.

                        FileNm = clsGeneral.getTemplateName(ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATEID).ToString(), ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString());

                        if (IsTemplateExist == true)
                        {
                            int _Result = Convert.ToInt32(MessageBox.Show('"' + ObjucTemplates.flxTemplates.GetData(i, ObjucTemplates.COL_TEMPLATENAME).ToString() + '"' + " already exists on the site? do you want to upload?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2));
                            if (_Result == Convert.ToInt32(DialogResult.Yes))
                            {
                                //Overwrite Template
                                isUploaded = true;
                                if (objgloCommunity.UploadFileToDocumentLibrary(Wpath, FolderNm, FileNm, clsGeneral.Webpath, clsGeneral.WebSite, clsGeneral.ClinicRepository, clsGeneral.ClinicWebFolder) != true)
                                {
                                    MessageBox.Show("Failed to upload the template", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    isUploaded = false;
                                    this.Cursor = Cursors.Default;
                                    return isUploaded;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }//IsTemplateExist if end
                        else
                        {
                            //If not present then upload Template.
                            isUploaded = true;
                            if (objgloCommunity.UploadFileToDocumentLibrary(Wpath, FolderNm, FileNm, clsGeneral.Webpath, clsGeneral.WebSite, clsGeneral.ClinicRepository, clsGeneral.ClinicWebFolder) != true)//clsGeneral.WebFolder + "/" + clsGeneral.ClinicWebFolder
                            {
                                MessageBox.Show("Failed to upload the template", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                isUploaded = false;
                                this.Cursor = Cursors.Default;
                                return isUploaded;
                            }
                        }//IsTemplateExist else end
                    }//if end
                }//for end
                if (isChecked == true)
                {
                    if (isUploaded == true)
                        MessageBox.Show("Template(s) uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Select template(s) for upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        #endregion

        #region "gloCommunity Share - Smart Diagnosis"

        private bool UploadShareSmartDxToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string SmartDxAssociationLocal = clsGeneral.gstrsmdxflnm + "Local";
            string SmartDxAssociationSRV = clsGeneral.gstrsmdxflnm;//"SmartDxAssociation";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrsmdxflnm + "/" + SmartDxAssociationSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + SmartDxAssociationLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartDx) Xml.
                if (CreateXML(SmartDxAssociationLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartDxAssociationSRV + ".xml";
                        string _TableName = "ICD9";
                        bool blnAssociationUserResult = objgloCommunity.CompareXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, SmartDxAssociationLocal, SmartDxAssociationSRV, clsGeneral.gstrsmdxflnm);

                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "User", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                objgloCommunity = null;
                arrLocalCatFileNm.Clear();
            }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareSmartDxToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string SmartDxAssociationLocal = clsGeneral.gstrsmdxflnm + "Local";
            string SmartDxAssociationSRV = clsGeneral.gstrsmdxflnm;//"SmartDxAssociation";
            //string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + SmartDxAssociationSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrsmdxflnm + "/" + SmartDxAssociationSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + SmartDxAssociationLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartDx) Xml.
                if (CreateXML(SmartDxAssociationLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartDxAssociationSRV + ".xml";
                        string _TableName = "ICD9";
                        bool blnAssociationUserResult = objgloCommunity.CompareXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, SmartDxAssociationLocal, SmartDxAssociationSRV, clsGeneral.gstrsmdxflnm);

                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool CreateXML(string XmlNm)
        {
            bool IsXmlCreate = false;
            if (objUCSmartDX.trvsmartdiag.Nodes[0].GetNodeCount(false) > 0)
            {
                bool IsRootCreated = false;
                bool IsICD9Created = false;
                DataBaseLayer oDB = new DataBaseLayer();
                clsSmartDx_Upload objclsSmartDx_Upload = new clsSmartDx_Upload();
                string _sGenericName = "";
                string _sQuantity = "";
                string _PracticeFavorites = "false";
                string _BeersList = "0";
                string _IsAllergicDrug = "false";

                try
                {
                    //'MessageBox.Show(ICD9Code)
                    XmlTextWriter xmlwriter = null;
                    string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;
                    string[] strIcd9Text = null;
                    string strICD9Code = string.Empty;
                    int i = 0;
                    for (i = 0; i <= objUCSmartDX.trvsmartdiag.Nodes[0].GetNodeCount(false) - 1; i++)
                    {
                        myTreeNode ICD9Node = null;
                        ICD9Node = (myTreeNode)objUCSmartDX.trvsmartdiag.Nodes[0].Nodes[i];
                        if (ICD9Node.Checked == true)//XML create only for ICD9 Node is checked.
                        {
                            strIcd9Text = ICD9Node.Text.Split('-');
                            if (strIcd9Text.Length > 0)
                            {
                                strICD9Code = strIcd9Text[0].Trim();
                            }
                            //'Checking of Node has child node, if not then Exit Sub.
                            foreach (myTreeNode oNode in ICD9Node.Nodes)
                            {
                                if (oNode.Nodes.Count > 0)
                                {
                                    IsXmlCreate = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            if (IsXmlCreate == false)
                            {
                                //If trICD9Association.Nodes.Item(0).GetNodeCount(False) - 1 > 0 Then
                                //    Exit For
                                //Else
                                //    Exit Sub
                                //End If
                                //Exit Sub
                                continue;
                                //GoTo NextNode 

                            }

                            if (IsRootCreated == false)
                            {
                                xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                xmlwriter.WriteStartDocument();
                                xmlwriter.WriteStartElement("ICD9Association");
                                //'Open the ICD9Association Main Parent Node 
                                xmlwriter.WriteAttributeString("Text", "ICD9 Association");
                                IsRootCreated = true;
                            }


                            if (ICD9Node.GetNodeCount(true) > 0)
                            {
                                int k = 0;
                                ArrayList arrlist = new ArrayList();
                                for (k = 0; k <= 7; k++)
                                {
                                    myTreeNode AssociateNode = default(myTreeNode);
                                    AssociateNode = (myTreeNode)ICD9Node.Nodes[k];

                                    if (AssociateNode.GetNodeCount(false) - 1 != -1)
                                    {
                                        if (IsICD9Created == false)
                                        {
                                            IsICD9Created = true;
                                            xmlwriter.WriteStartElement("ICD9");
                                            //'Start ICD9
                                            xmlwriter.WriteAttributeString("Text", ICD9Node.Text);
                                            xmlwriter.WriteAttributeString("ID", ICD9Node.Key.ToString());
                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);
                                        }
                                        switch (AssociateNode.Text)
                                        {
                                            case "CPT":
                                                xmlwriter.WriteStartElement("CPT");
                                                //'Start CPT
                                                xmlwriter.WriteAttributeString("Text", "CPT");
                                                break;
                                            case "Drugs":
                                                xmlwriter.WriteStartElement("Drugs");
                                                //'Start Drugs
                                                xmlwriter.WriteAttributeString("Text", "Drugs");
                                                break;
                                            case "Patient Education":
                                                xmlwriter.WriteStartElement("PatientEducation");
                                                //'Start Patient Education
                                                xmlwriter.WriteAttributeString("Text", "Patient Education");
                                                break;
                                            case "Tags":
                                                xmlwriter.WriteStartElement("Tags");
                                                //'Start Tags
                                                xmlwriter.WriteAttributeString("Text", "Tags");
                                                break;
                                            case "Flowsheet":
                                                xmlwriter.WriteStartElement("Flowsheet");
                                                //'Start Flowsheet
                                                xmlwriter.WriteAttributeString("Text", "Flowsheet");
                                                break;
                                            case "Lab Orders":
                                                xmlwriter.WriteStartElement("LabOrders");
                                                //'Start Lab Orders
                                                xmlwriter.WriteAttributeString("Text", "Lab Orders");
                                                break;
                                            case "Orders":
                                                xmlwriter.WriteStartElement("Orders");
                                                //'Start Orders 
                                                xmlwriter.WriteAttributeString("Text", "Orders");
                                                break;
                                            case "Referral Letter":
                                                xmlwriter.WriteStartElement("ReferralLetter");
                                                //'Start Referral Letters
                                                xmlwriter.WriteAttributeString("Text", "Referral Letter");
                                                break;
                                        }
                                    }
                                    int j = 0;


                                    for (j = 0; j <= AssociateNode.GetNodeCount(false) - 1; j++)
                                    {



                                        if (AssociateNode.Text == "CPT")
                                        {
                                            xmlwriter.WriteStartElement("CPTc");
                                            //'Start CPT_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End CPT_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End CPT
                                            }

                                        }
                                        else if (AssociateNode.Text == "Drugs")
                                        {
                                            xmlwriter.WriteStartElement("Drugsc");
                                            //'Start Drugs_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            DataTable dtDrugMst = objclsSmartDx_Upload.FetchDrugByNDCCodepdate(((myTreeNode)AssociateNode.Nodes[j]).NDCCode);

                                            if (dtDrugMst.Rows.Count > 0 && dtDrugMst != null)
                                            {
                                                _sGenericName = dtDrugMst.Rows[0]["sGenericName"].ToString();
                                                _sQuantity = dtDrugMst.Rows[0]["Quantity"].ToString();
                                                _PracticeFavorites = dtDrugMst.Rows[0]["Practice Favorites"].ToString().ToLower();
                                                _BeersList = dtDrugMst.Rows[0]["Beers List"].ToString();
                                                _IsAllergicDrug = dtDrugMst.Rows[0]["bIsAllergicDrug"].ToString().ToLower();
                                            }

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End Drugs_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Drugs
                                            }


                                        }
                                        else if (AssociateNode.Text == "Patient Education")
                                        {
                                            //Add Template information to upload on gloCommunity
                                            string _PECatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _PECatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            //
                                            xmlwriter.WriteStartElement("PatientEducationc");
                                            //'Start PatientEducation_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End PatientEducation_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Patient Education
                                            }


                                        }
                                        else if (AssociateNode.Text == "Tags")
                                        {
                                            //Add Template information to upload on gloCommunity
                                            string _TagsCatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _TagsCatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            //
                                            xmlwriter.WriteStartElement("Tagsc");
                                            //'Start Tags_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End Tags_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Tags
                                            }


                                        }
                                        else if (AssociateNode.Text == "Flowsheet")
                                        {
                                            string _strQry = "select isnull(nColNumber,0)as ColNumber,isnull(sColumnName,'') as ColumnName,isnull(sFormat,'')as Format,isnull(dWidth,0.00)as Width,isnull(sAlignment,0.00)as Alignment,isnull(nForeColor,0)as ForeColor,isnull(nBackColor,0)as BackColor from FlowSheet_MST where nFlowSheetID = " + ((myTreeNode)AssociateNode.Nodes[j]).Key;
                                            DataTable dtFlowsheet = oDB.GetDataTable_Query(_strQry);

                                            xmlwriter.WriteStartElement("Flowsheetc");
                                            //'Start Flowsheet_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            if ((dtFlowsheet != null))
                                            {
                                                foreach (DataRow drFlosheet in dtFlowsheet.Rows)
                                                {
                                                    xmlwriter.WriteStartElement("Flowsheetc1");
                                                    //'Start Flowsheetc1
                                                    xmlwriter.WriteAttributeString("ColNumber", drFlosheet[0].ToString());
                                                    xmlwriter.WriteAttributeString("ColumnName", drFlosheet[1].ToString());
                                                    xmlwriter.WriteAttributeString("Format", drFlosheet[2].ToString());
                                                    xmlwriter.WriteAttributeString("Width", drFlosheet[3].ToString());
                                                    xmlwriter.WriteAttributeString("Alignment", drFlosheet[4].ToString());
                                                    xmlwriter.WriteAttributeString("ForeColor", drFlosheet[5].ToString());
                                                    xmlwriter.WriteAttributeString("BackColor", drFlosheet[6].ToString());
                                                    xmlwriter.WriteEndElement();
                                                    //'End Flowsheetc1
                                                }
                                            }

                                            xmlwriter.WriteEndElement();
                                            //'End Flowsheet_chil

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Flowsheet
                                            }


                                        }
                                        else if (AssociateNode.Text == "Lab Orders")
                                        {
                                            xmlwriter.WriteStartElement("LabOrdersc");
                                            //'Start LabOrders_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End LabOrders_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Lab Orders
                                            }

                                        }
                                        else if (AssociateNode.Text == "Orders")
                                        {
                                            string _strQry = "select LMCAT.lm_category_Description as Category,(select  lm_test_Name as GroupName from LM_Test where  LM.lm_test_GroupNo = lm_test_ID  ) as GroupName from LM_Test LM inner join LM_Category LMCAT on LM.lm_test_CategoryID = LMCAT.lm_Category_ID where lm_test_ID = " + ((myTreeNode)AssociateNode.Nodes[j]).Key;
                                            DataTable dtOrder = oDB.GetDataTable_Query(_strQry);

                                            xmlwriter.WriteStartElement("Ordersc");
                                            //'Start Orders_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);

                                            if ((dtOrder != null))
                                            {
                                                if (!string.IsNullOrEmpty(dtOrder.Rows[0][0].ToString()) & !string.IsNullOrEmpty(dtOrder.Rows[0][1].ToString()))
                                                {
                                                    xmlwriter.WriteAttributeString("DrugForm", dtOrder.Rows[0][0].ToString());
                                                    //'Category
                                                    xmlwriter.WriteAttributeString("Route", dtOrder.Rows[0][1].ToString());
                                                    //'GroupName
                                                }

                                            }

                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End Orders_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Orders
                                            }


                                        }
                                        else if (AssociateNode.Text == "Referral Letter")
                                        {
                                            //Add Template information to upload on gloCommunity
                                            string _RefCatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _RefCatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            //
                                            xmlwriter.WriteStartElement("ReferralLetterc");
                                            //'Start ReferralLetter_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("ICD9Code", strICD9Code);

                                            xmlwriter.WriteEndElement();
                                            //'End ReferralLetter_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Referral Letter
                                            }

                                        }
                                    }
                                }
                            }
                            //'ICD9Node.GetNodeCount(True) > 0
                            if (IsICD9Created == true)
                            {
                                xmlwriter.WriteEndElement();
                                //'End of ICD9
                                IsICD9Created = false;
                            }
                        }//End ICD9Node.Checked
                    }//End for loop
                    if (IsRootCreated == true)
                    {
                        xmlwriter.WriteEndElement();
                        //'End of ICD9Association Main Parent Node 
                        xmlwriter.WriteEndDocument();
                        xmlwriter.Close();
                        //'End of Creating Xml
                    }

                }
                catch //(Exception ex)
                {
                    //commented by kanchan on 20120105
                    //MessageBox.Show(ex.ToString());
                }
            }

            return IsXmlCreate;
        }

        private void SaveAssociation()
        {
            //Get node count of child nodes in trICD9Associates
            bool ICD9Downloaded = false;
            bool notshowmsg = false;
            TreeView trvsmartdiag = objUCSmartDX.trvsmartdiag;
            bool localsite = false;
            if (objUCSmartDX.IsClinicRepository == true)
            {
                localsite = true;
            }
            try
            {

                if (trvsmartdiag.Nodes[0].GetNodeCount(false) > 0)
                {
                    ClsICD9AssociationDBLayer objICD9AssociationDBLayer = new ClsICD9AssociationDBLayer();
                    clsSmartDX objSPSmartDiag = new clsSmartDX();

                    DataTable DtCPT = null;
                    DataTable DtREffTgsPE = null;
                    DataTable DtLab = null;
                    DataTable DtOrd = null;
                    DataTable DtDrg = null;
                 //   DataTable DtPE = null;
                    DataTable DtFl = null;
                  //  DataTable DtTgs = null;
                    int i = 0;
                    if (objUCSmartDX.StrCPT.Length > 0)
                    {
                        DtCPT = objSPSmartDiag.GetCPTIDFromCodes(objUCSmartDX.StrCPT.Substring(0, objUCSmartDX.StrCPT.Length - 1));
                    }
                    if (objUCSmartDX.StrRefOrdTgsPE.Length > 0)
                    {
                        DtREffTgsPE = objSPSmartDiag.GetRefTgsPEIDFromCodes(objUCSmartDX.StrRefOrdTgsPE.Substring(0, objUCSmartDX.StrRefOrdTgsPE.Length - 1));
                    }
                    if (objUCSmartDX.StrLabs.Length > 0)
                    {
                        DtLab = objSPSmartDiag.GetLabIDFromCodes(objUCSmartDX.StrLabs.Substring(0, objUCSmartDX.StrLabs.Length - 1));
                    }

                    if (objUCSmartDX.StrOrd.Length > 0)
                    {
                        DtOrd = objSPSmartDiag.GetOrdIDFromCodes(objUCSmartDX.StrOrd.Substring(0, objUCSmartDX.StrOrd.Length - 1));
                    }

                    if (objUCSmartDX.StrDrg.Length > 0)
                    {
                        DtDrg = objSPSmartDiag.GetDrgFromCodes(objUCSmartDX.StrDrg.Substring(0, objUCSmartDX.StrDrg.Length - 1));
                    }



                    if (objUCSmartDX.StrFlo.Length > 0)
                    {
                        DtFl = objSPSmartDiag.GetFloIDFromCodes(objUCSmartDX.StrFlo.Substring(0, objUCSmartDX.StrFlo.Length - 1));
                    }


                    for (i = 0; i <= trvsmartdiag.Nodes[0].GetNodeCount(false) - 1; i++)
                    {
                        myTreeNode ICD9Node = default(myTreeNode);
                        //get the ICD9Node associated sequentially
                        ////ICD9Node = trvsmartdiag.Nodes.Item(0).Nodes.Item(i);
                        ICD9Node = (myTreeNode)trvsmartdiag.Nodes[0].Nodes[i];

                        if (ICD9Node.Checked == true)
                        {

                            if (ICD9Node.GetNodeCount(true) > 0)
                            {
                                string ICD9code = ICD9Node.Text.Substring(0, ICD9Node.Text.Trim().IndexOf("-") - 1);
                                string ICD9Desc = ICD9Node.Text.Substring(ICD9Node.Text.Trim().IndexOf("-") + 1, ICD9Node.Text.Length - (ICD9Node.Text.Trim().IndexOf("-") + 1));
                                string ICD9ID = objICD9AssociationDBLayer.GetICD9IDFromCode(ICD9code.Trim(), ICD9Desc.Trim());
                                if (objICD9AssociationDBLayer.CheckICD9Associated(Convert.ToInt64(ICD9ID)) >= 1)
                                {
                                    DialogResult reply = MessageBox.Show(ICD9Node.Text + " already associated? do you want to associate new", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (reply == System.Windows.Forms.DialogResult.Yes)
                                    {
                                    }
                                    else
                                    {
                                        notshowmsg = true;
                                        goto endicd9;

                                    }
                                }
                                int k = 0;
                                ArrayList arrlist = new ArrayList();
                                for (k = 0; k < ICD9Node.Nodes.Count; k++)
                                {
                                    myTreeNode AssociateNode = default(myTreeNode);
                                    //AssociateNode = ICD9Node.Nodes.Item(k);
                                    AssociateNode = (myTreeNode)ICD9Node.Nodes[k];
                                    int j = 0;
                                    for (j = 0; j <= AssociateNode.GetNodeCount(false) - 1; j++)
                                    {
                                        if (AssociateNode.Text == "CPT")
                                        {
                                            string StrCPTs = ((myTreeNode)AssociateNode.Nodes[j]).Text;
                                            string strCode = StrCPTs.Substring(0, StrCPTs.IndexOf("-") - 1);
                                            //DataRow[] dr = DtCPT.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Tag + "'");
                                            DataRow[] dr = DtCPT.Select("Code='" + strCode + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {

                                                string strDesc = StrCPTs.Substring(StrCPTs.IndexOf("-") + 1, StrCPTs.Length - (StrCPTs.IndexOf("-") + 1));
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertCPTMSTData(strCode, strDesc, 1, false);
                                            }

                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "c", AssociateNode.Nodes[j].Checked, 0));


                                            //For De-Normalization

                                        }
                                        else if (AssociateNode.Text == "Drugs") //added for Drugs
                                        {
                                            ////arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).Dosage, CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugForm))
                                            DataRow[] dr = DtDrg.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertDrugsMSTData(((myTreeNode)AssociateNode.Nodes[j]).DrugName, ((myTreeNode)AssociateNode.Nodes[j]).GenericName, ((myTreeNode)AssociateNode.Nodes[j]).Dosage, ((myTreeNode)AssociateNode.Nodes[j]).Route, ((myTreeNode)AssociateNode.Nodes[j]).Frequency, ((myTreeNode)AssociateNode.Nodes[j]).Duration, ((myTreeNode)AssociateNode.Nodes[j]).PracticeFavorites, ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics, ((myTreeNode)AssociateNode.Nodes[j]).mpid, ((myTreeNode)AssociateNode.Nodes[j]).IsAllergicDrug,
                                               clsGeneral.gClinicID, false, ((myTreeNode)AssociateNode.Nodes[j]).NDCCode, ((myTreeNode)AssociateNode.Nodes[j]).DrugForm, ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier, "", Convert.ToInt32(((myTreeNode)AssociateNode.Nodes[j]).BeersList), false, ((myTreeNode)AssociateNode.Nodes[j]).Quantity);

                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "d", ((myTreeNode)AssociateNode.Nodes[j]).DrugName, ((myTreeNode)AssociateNode.Nodes[j]).Dosage, ((myTreeNode)AssociateNode.Nodes[j]).DrugForm, ((myTreeNode)AssociateNode.Nodes[j]).Route, ((myTreeNode)AssociateNode.Nodes[j]).Frequency, ((myTreeNode)AssociateNode.Nodes[j]).NDCCode, ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics, ((myTreeNode)AssociateNode.Nodes[j]).Duration,
                                                ((myTreeNode)AssociateNode.Nodes[j]).mpid, ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier, AssociateNode.Nodes[j].Checked));
                                            ////For De-Normalization

                                        }
                                        else if (AssociateNode.Text == "Patient Education")
                                        {
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + " And Category='Patient Education'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Patient Education", false, localsite);
                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "p", AssociateNode.Nodes[j].Checked, 0));


                                        }
                                        else if (AssociateNode.Text == "Tags")
                                        {
                                            //// arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "t", AssociateNode.Nodes.Item(j).Checked, 0))
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + " And Category='Tags'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Tags", false, localsite);

                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "t", AssociateNode.Nodes[j].Checked, 0, ((myTreeNode)AssociateNode.Nodes[j]).Text));

                                        }
                                        else if (AssociateNode.Text == "Flowsheet")
                                        {
                                            DataRow[] dr = DtFl.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();

                                            }
                                            else
                                            {
                                                DataRow[] drflp = objUCSmartDX.dtfloc.Select("Text='" + AssociateNode.Nodes[j].Text + "'");
                                                string flid = drflp[0]["flowsheetc_id"].ToString();
                                                //   DataRow[] drc = objUCSmartDX.dtfloc1.Select("flowsheetc_id='" + ((myTreeNode)AssociateNode.Nodes[j]).mpid + "'" + "And ColNumber<>''" + "And ColumnName<>''");
                                                DataRow[] drc = objUCSmartDX.dtfloc1.Select("flowsheetc_id='" + flid + "'" + "And ColNumber<>''" + "And ColumnName<>''");
                                                decimal Flowsheetid = 0;
                                                // decimal Prevflowid=0;
                                                for (int lendrc = 0; lendrc <= drc.Length - 1; lendrc++)
                                                {
                                                    if ((drc[lendrc]["ColNumber"].ToString().Trim() != string.Empty))
                                                    {
                                                        if ((drc[lendrc]["ColumnName"] != null))
                                                        {

                                                            Flowsheetid = Convert.ToDecimal(objSPSmartDiag.InsertFlowsheetMSTData(Flowsheetid, AssociateNode.Nodes[j].Text, drc.Length, Convert.ToInt16(drc[lendrc]["ColNumber"]), drc[lendrc]["ColumnName"].ToString().Trim(), drc[lendrc]["Format"].ToString().Trim(), Convert.ToDecimal(drc[lendrc]["Width"]), drc[lendrc]["Alignment"].ToString().Trim(), Convert.ToDecimal(drc[lendrc]["ForeColor"]), Convert.ToDecimal(drc[lendrc]["BackColor"]),
                                                           false));


                                                        }
                                                    }
                                                }
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = Flowsheetid.ToString();
                                            }

                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "f", AssociateNode.Nodes[j].Checked, 0));


                                        }
                                        else if (AssociateNode.Text == "Lab Orders")
                                        {
                                            DataRow[] dr = DtLab.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertLabTestMSTData(((myTreeNode)AssociateNode.Nodes[j]).Text, ((myTreeNode)AssociateNode.Nodes[j]).Text, false);


                                            }

                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "l", AssociateNode.Nodes[j].Checked, 0));


                                        }
                                        else if (AssociateNode.Text == "Orders")
                                        {
                                            DataRow[] dr = DtOrd.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                long _CategoryId = objSPSmartDiag.GetCategoryID(((myTreeNode)AssociateNode.Nodes[j]).DrugForm);

                                                long _mGroupID = objSPSmartDiag.GetGroupID(((myTreeNode)AssociateNode.Nodes[j]).Route, _CategoryId);
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertTest(_CategoryId, _mGroupID, ((myTreeNode)AssociateNode.Nodes[j]).Text);



                                                string strordid = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Orders", false, localsite);


                                            }
                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "o", AssociateNode.Nodes[j].Checked, 0));

                                        }
                                        else if (AssociateNode.Text == "Referral Letter")
                                        {
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + "And Category='Referral Letter'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Referral Letter", false, localsite);

                                            }
                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "r", AssociateNode.Nodes[j].Checked, 0));

                                        }

                                    }

                                }


                                objICD9AssociationDBLayer.AddData(Convert.ToInt64(ICD9ID), ICD9Node.Text, arrlist);
                                ICD9Downloaded = true;
                            }
                        endicd9:
                            ;
                            //int aa = 0;
                        }
                    } //for


                }//if

            }//try
            catch //(Exception ex)
            {
            }

            finally
            {
                if (ICD9Downloaded == true)
                {
                    MessageBox.Show("ICD9 association downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (notshowmsg == false)
                    {
                        //Added by kanchan on 20120105
                        if (trvsmartdiag.Nodes.Count > 0)
                            MessageBox.Show("Select at least one ICD9 to download", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                //  trvsmartdiag.Nodes.Clear();
            }




        }

        string[] Arrtemplate = null;
        private void DownloadTemplate()
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {

                Array.Resize(ref Arrtemplate, 0);
                for (int i = 1; i <= ObjucTemplates.flxTemplates.Rows.Count - 1; i++)
                {
                    if (((bool)ObjucTemplates.flxTemplates.GetData(i, 0)) == true)
                    {
                        //  Dim obj = grdTemplateGallery.GetData(i, 0)
                        Array.Resize(ref Arrtemplate, Arrtemplate.Count() + 1);
                        Arrtemplate[Arrtemplate.Length - 1] = ObjucTemplates.flxTemplates.Rows[i][2].ToString();
                    }
                }

                //DownloadFile("http://tfs05:5020", "http://tfs05:5020/Shared Documents/" & GloUC_trvCategory.SelectedNode.Name, "Shared Documents")
                if (Arrtemplate.Length == 0)
                {
                    //Added by kanchan on 20120105
                    if (ObjucTemplates.flxTemplates.Rows.Count > 1)
                    {
                        MessageBox.Show("Select at least one template to download", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    if ((ObjucTemplates.trvCategories.SelectedNode != null))
                    {
                        if (!string.IsNullOrEmpty(ObjucTemplates.trvCategories.SelectedNode.Name.ToString().Trim()))
                        {
                            string[] strspl = ObjucTemplates.trvCategories.SelectedNode.Name.Split('/');
                            string strsend = strspl[strspl.Length - 2];
                            DownloadFile(ObjucTemplates.trvCategories.SelectedNode.Name, strsend);
                        }
                    }
                }

            }
            catch //(Exception ex)
            {
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        public bool DownloadFile(string folderPath, string myList)
        {
            string WebUrl = "";
            string RootFolder = "";
            System.Xml.XmlDocument resdoc = new System.Xml.XmlDocument();
            string strURL = "";
            string strFileName = "";
            ////////string folderPath = "http://glosvr04/sites/DefaultCollection/test/Test/Upload/";
            try
            {
                gloLists.Lists objLists = new gloLists.Lists();
                //objLists.Credentials = System.Net.CredentialCache.DefaultCredentials;
                //objLists.Url = "http://[SITENAME]:34028/sites/TestSite/_vti_bin/lists.asmx"; // change the URL to your sharepoint site

                if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    objLists.UseDefaultCredentials = true;
                else
                {
                    //Added for check which authentication is use for access gloCommunity on 20120802
                    if (clsGeneral.gstrgloCommunityAuthentication.ToLower() == "form")
                    {
                        objLists.CookieContainer = new CookieContainer();
                        if (clsGeneral.oFormCookie == null)
                            objLists.CookieContainer.Add(clsGeneral.QueryToSharePoint(clsGeneral.gstrAuthenticationWSAddress, clsGeneral.gstrGCUserName, clsGeneral.gstrGCPassword));
                        else
                            objLists.CookieContainer.Add(clsGeneral.oFormCookie);
                    }
                    else
                    {
                        clsGeneral.CheckAuthenticatedCookie();
                        objLists.CookieContainer = clsGeneral.oCookie;
                    }
                }

                if (ObjucTemplates.blntempgclinic == true)
                {
                    WebUrl = clsGeneral.Webpath + clsGeneral.gstrClinicName + "/" + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                    RootFolder = clsGeneral.ClinicRepository;
                }
                if (ObjucTemplates.blntempgclinic == false)
                {
                    WebUrl = clsGeneral.Webpath + clsGeneral.gstrVti_Bin + "/" + clsGeneral.gstrListSvc;
                    RootFolder = clsGeneral.GlobalRepository;
                }

                objLists.Url = WebUrl;

                XmlDocument xmlDoc = new System.Xml.XmlDocument();

                XmlNode ndQuery = xmlDoc.CreateNode(XmlNodeType.Element, "Query", "");

                XmlNode ndViewFields = xmlDoc.CreateNode(XmlNodeType.Element, "ViewFields", "");

                XmlNode ndQueryOptions = xmlDoc.CreateNode(XmlNodeType.Element, "QueryOptions", "");

                //ndQueryOptions.InnerXml =
                //"<IncludeAttachmentUrls>TRUE</IncludeAttachmentUrls>";
                ndQueryOptions.InnerXml = "<Folder>" + folderPath + "/" + "</Folder>";
                ndViewFields.InnerXml = "";
                ndQuery.InnerXml = "";

                try
                {
                    //Dim ndListItems As XmlNode = objLists.GetListItems(myList, Nothing, ndQuery, ndViewFields, Nothing, ndQueryOptions, _
                    // Nothing)

                    XmlNode ndListItems = objLists.GetListItems(RootFolder, null, ndQuery, ndViewFields, null, ndQueryOptions, null);
                    // you can change the document library name to your custom document library name 
                    XmlNodeList oNodes = ndListItems.ChildNodes;

                    foreach (XmlNode node in oNodes)
                    {
                        XmlNodeReader objReader = new XmlNodeReader(node);
                        while (objReader.Read())
                        {
                            if (objReader["ows_EncodedAbsUrl"] != null && objReader["ows_LinkFilename"] != null)
                            {
                                strURL = objReader["ows_EncodedAbsUrl"].ToString();
                                strFileName = objReader["ows_LinkFilename"].ToString();
                                DownLoadAttachment(strURL, strFileName);
                            }
                        }
                    }

                    if (Arrtemplate.Length > 0)
                    {
                        MessageBox.Show("Template downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }
                }
                catch //(System.Web.Services.Protocols.SoapException ex)
                {
                    // MessageBox.Show((("Message:" + Constants.vbLf + ex.Message + Constants.vbLf + "Detail:" + Constants.vbLf) + ex.Detail.InnerText + Constants.vbLf + "StackTrace:" + Constants.vbLf) + ex.StackTrace);
                }
            }
            catch //(Exception ex)
            {
            }
            return true;
        }

        public void DownLoadAttachment(string strURL, string strFileName)
        {

            HttpWebRequest request = default(HttpWebRequest);
            HttpWebResponse response = null;
            try
            {
                if (Arrtemplate.Contains(strFileName))
                {
                    request = (HttpWebRequest)WebRequest.Create(strURL);

                    if (ConfigurationManager.AppSettings["Environment"].ToLower() == "staging")
                    {
                        request.Credentials = System.Net.CredentialCache.DefaultCredentials;
                    }
                    else
                    {
                        //Added for check which authentication is use for access gloCommunity on 20120802
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
                    //objcls.AddNewTemplateGallery(0, strFileName.Substring(0, strFileName.IndexOf(".")), 0, GloUC_trvCategory.SelectedNode.Text, 0, read)
                    objclstemplate.AddNewTemplateGallery(0, strFileName.Substring(0, strFileName.IndexOf(".")), 0, ObjucTemplates.cmbCategory.Text, 0, read);

                    s.Close();
                    response.Close();
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);

            }
            finally
            {

            }
        }

        //ArrayList Downloadcontent() = null;
        private void DownloadContent()
        {

            Wd.ContentControl datacon = null;
           // object temp = null;
            string[] strValue = null;
          //  int grpid = 0;
            Int64 _ElementId = 0;

            List<clsTemplate> a = new List<clsTemplate>();
            ArrayList m_Fieldvalues = null;
            myList m_list = default(myList);
            string ElementName = "";
            //Dim cls As clssharepointtemp = Nothing
            string category = "";

            string oData = string.Empty;
            Wd.Cell ocell = default(Wd.Cell);
            string m_elementId = null;
            string m_DataType = null;
            bool m_Required = false;
            //string Item = null;

            for (Int32 Len = 0; Len <= ObjucTemplates.Downloadcontent.Count - 1; Len++)
            {
                datacon = (Wd.ContentControl)ObjucTemplates.Downloadcontent[Len];
                //  datacon = (Wd.ContentControl)temp;
                datacon.ToString().Contains("Text");
                //' for textbox
                strValue = datacon.Tag.Split('|');

                if (strValue.Length == 4)
                {
                    m_elementId = strValue[0];
                    m_DataType = strValue[1];
                    if (strValue[2] == "false")
                        m_Required = false;
                    else
                        m_Required = true;
                    category = strValue[3];

                    if (m_DataType == "Table" | m_DataType == "Group")
                    {
                        ElementName = datacon.Title;

                        foreach (Wd.Table otble in ((Wd.ContentControl)ObjucTemplates.Downloadcontent[Len]).Range.Tables)
                        {
                            m_Fieldvalues = null;
                            m_Fieldvalues = new ArrayList();

                            foreach (Wd.Row oRow in otble.Rows)
                            {
                                if ( (oRow.Index != 1) && ( otble.Columns.Count >=1) )
                                {

                                    ocell = otble.Cell(oRow.Index, 1);
                                    string[] oAssociateCat = null;
                                    string[] oass = null;
                                    int cnt = 0;
                                    if (otble.Columns.Count >= 2)
                                    {
                                        foreach (Wd.FormField ofield in otble.Cell(oRow.Index, 2).Range.FormFields)
                                        {

                                            m_list = new myList();
                                            m_list.HistoryCategory = ocell.Range.Text.Trim();
                                            if (ofield.Type == Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormCheckBox)
                                            {
                                                m_list.ControlType = clsGeneral.ControlType.CheckBox;
                                                oAssociateCat = ofield.HelpText.Split('|');
                                                m_list.AssociatedProperty = oAssociateCat[1];

                                                oass = ofield.StatusText.Split('-');

                                                if (oass.Length == 2)
                                                {
                                                    m_list.HistoryCategory = oass[1];
                                                    m_list.HistoryItem = oass[0];
                                                    m_list.AssociatedItem = oass[0];
                                                    m_list.AssociatedCategory = oass[1];

                                                }
                                                else
                                                {
                                                    m_list.HistoryCategory = ocell.Range.Text.Trim();
                                                    m_list.HistoryItem = oass[0];
                                                    m_list.AssociatedItem = oass[0];
                                                    m_list.AssociatedCategory = ocell.Range.Text.Trim();
                                                }
                                            }

                                            if (ofield.Type == Microsoft.Office.Interop.Word.WdFieldType.wdFieldAddin)
                                            {
                                            }



                                            m_Fieldvalues.Add(m_list);
                                            cnt = cnt + 1;




                                        }
                                    }

                                    //here



                                }

                            }



                        }
                    }//If Group//Table
                    //here
                    if (m_DataType == "Multiple Selection")
                    {
                        foreach (Wd.Table otble in ((Wd.ContentControl)ObjucTemplates.Downloadcontent[Len]).Range.Tables)
                        {
                            m_Fieldvalues = null;
                            m_Fieldvalues = new ArrayList();

                            foreach (Wd.Row oRow in otble.Rows)
                            {
                                m_list = new myList();
                                //SLR: is '0' allowed?
                                ocell = otble.Cell(oRow.Index, 0);
                                if (otble.Columns.Count >= 1)
                                {
                                    m_list.Value = otble.Cell(oRow.Index, 1).Range.Text.Replace("", "").Trim();
                                    if (oRow.Index == 1)
                                    {
                                        ElementName = otble.Cell(oRow.Index, 1).Range.Text.Replace("", "").Trim();

                                    }
                                }
                          //      string strAssoc = "";
                            //    int ctrl = 0;
                                if (otble.Columns.Count >= 2)
                                {
                                    foreach (Wd.FormField ofield in otble.Cell(oRow.Index, 2).Range.FormFields)
                                    {
                                        if (ofield.Type == Microsoft.Office.Interop.Word.WdFieldType.wdFieldFormCheckBox)
                                        {
                                            string[] strhptext = ofield.HelpText.ToString().Split('|');
                                            if ((strhptext[1] != null))
                                            {
                                                m_list.AssociatedProperty = strhptext[1];
                                            }
                                            else
                                            {
                                                m_list.AssociatedProperty = "";
                                            }
                                            m_list.ControlType = clsGeneral.ControlType.CheckBox;
                                        }
                                        else if (ofield.Type == Microsoft.Office.Interop.Word.WdFieldType.wdFieldAddin)
                                        {
                                            string[] strhptext = ofield.HelpText.ToString().Split('|');
                                            if ((strhptext[1] != null))
                                            {
                                                m_list.AssociatedProperty = strhptext[1];
                                            }
                                            else
                                            {
                                                m_list.AssociatedProperty = "";
                                            }
                                            m_list.ControlType = clsGeneral.ControlType.Text;
                                        }
                                    }
                                }

                                if (oRow.Index != 1)
                                {
                                    m_Fieldvalues.Add(m_list);
                                }



                            }


                        }


                    }

                    // clsTemplateGallery objTemplate = new clsTemplateGallery();


                    _ElementId = objclstemplate.AddDataFieldValue(_ElementId, 0, ElementName, m_DataType, m_Required, null, category);

                    if (strValue[1] == "Text")
                    {
                        //    If strDataType = "Text" Then
                        //        If strFieldValue <> "" Then
                        //objTemplate.AddDataFieldValue(0, _ElementId, strFieldValue, cls.ElementType, cls._bIsMand)
                    }

                    //Problem : 00000163
                    //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                    //Change : Parameter nSequenceNo passed in below function call objclstemplate.AddDataFieldValue() for data types "Table", "Multiple Selection" data types to maintain sequence set by user.

                    if (strValue[1] == "Table")
                    {
                        for (Int32 _inc = 0; _inc <= m_Fieldvalues.Count - 1; _inc++)
                        {
                            Int64 k = objclstemplate.AddDataFieldValue(0, _ElementId, "", m_DataType, m_Required, (myList)m_Fieldvalues[_inc], "", _inc + 1);
                        }
                    }

                    if (strValue[1] == "Group")
                    {
                        //For _inc As Int32 = 0 To m_Fieldvalues.Count - 1
                        //    objTemplate.AddDataFieldValue(0, _ElementId, txtCaption.Text.Trim, cls.ElementType, cls._bIsMand, CType(m_Fieldvalues.Item(_inc), myList))
                        //Next
                    }
                    if (strValue[1] == "Multiple Selection")
                    {
                        for (Int32 _inc = 0; _inc <= m_Fieldvalues.Count - 1; _inc++)
                        {
                            Int64 k = objclstemplate.AddDataFieldValue(0, _ElementId, ElementName, m_DataType, m_Required, (myList)m_Fieldvalues[_inc], "", _inc + 1);
                        }
                    }


                }
            }//Outer For
        }// Content function 

        #endregion

        #region "gloCommunity Share - Liquid Data"

        private bool UploadShareLiquidDataToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;

            string LiquidDataLocal = clsGeneral.gstrLiquidDataFNm + "Local";
            string LiquidDataSRV = clsGeneral.gstrLiquidDataFNm;
            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string LocalXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataLocal + ".xml";
            string ServerXmlPath = "";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrLiquidDataFNm + "/" + LiquidDataSRV + ".xml";
            bool blnLiquidXmlResult = false;
            bool IsXmlUploaded = false;

            try
            {
                if (IsLiquidDataSelected() == true)
                {
                    bool IsXmlCreated = CreateLiquidDataXML(LiquidDataLocal + ".xml", LocalXmlPath);
                    if (IsXmlCreated == true)
                    {
                        bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                        if (IsDownloadXml == true)
                        {
                            ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataSRV + ".xml";
                            blnLiquidXmlResult = objgloCommunity.CompareLiquidDataXML(LocalXmlPath, ServerXmlPath);

                            if (blnLiquidXmlResult == false)
                            {
                                this.Cursor = Cursors.Default;
                                return IsXmlUploaded = false;
                            }
                        }
                        else
                        {
                            ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataLocal + ".xml";
                        }

                        //'Upload Xml to SharePoint
                        string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                        string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                        string webSite = clsGeneral.gstrClinicName;
                        IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, ServerXmlPath, MainPath, webSite, "User", LocalXmlPath, clsGeneral.gstrClinicName, LiquidDataLocal, LiquidDataSRV, clsGeneral.gstrLiquidDataFNm);

                        if (IsXmlUploaded == true)
                            MessageBox.Show("Liquid Data uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Fail to upload Liquid Data", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//end IsLiquidDataSelected
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Select at least one Liquid Data to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
                return IsXmlUploaded = false;
            }
            finally
            {
                objgloCommunity = null;
            }
            this.Cursor = Cursors.Default;
            return IsXmlUploaded;
        }

        private bool UploadShareLiquidDataToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;

            string LiquidDataLocal = clsGeneral.gstrLiquidDataFNm + "Local";
            string LiquidDataSRV = clsGeneral.gstrLiquidDataFNm;
            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //

            string LocalXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataLocal + ".xml";
            string ServerXmlPath = "";
            //string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + LiquidDataSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrLiquidDataFNm + "/" + LiquidDataSRV + ".xml";
            bool blnLiquidXmlResult = false;
            bool IsXmlUploaded = false;

            try
            {
                if (IsLiquidDataSelected() == true)
                {
                    bool IsXmlCreated = CreateLiquidDataXML(LiquidDataLocal + ".xml", LocalXmlPath);
                    if (IsXmlCreated == true)
                    {
                        bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                        if (IsDownloadXml == true)
                        {
                            ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataSRV + ".xml";
                            blnLiquidXmlResult = objgloCommunity.CompareLiquidDataXML(LocalXmlPath, ServerXmlPath);

                            if (blnLiquidXmlResult == false)
                            {
                                this.Cursor = Cursors.Default;
                                return IsXmlUploaded = false;
                            }
                        }
                        else
                        {
                            ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + LiquidDataLocal + ".xml";
                        }

                        //'Upload Xml to SharePoint
                        string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                        string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                        string webSite = clsGeneral.gstrClinicName;
                        IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, ServerXmlPath, MainPath, webSite, "Global", LocalXmlPath, clsGeneral.gstrClinicName, LiquidDataLocal, LiquidDataSRV, clsGeneral.gstrLiquidDataFNm);

                        if (IsXmlUploaded == true)
                            MessageBox.Show("Liquid Data uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("Fail to upload Liquid Data", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }//end IsLiquidDataSelected
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Select at least one Liquid Data to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
                return IsXmlUploaded = false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
            }

            return IsXmlUploaded;
        }

        private bool CreateLiquidDataXML(string XmlNm, string XmlPath)
        {
            DataTable dt = new DataTable();
            DataTable dtMerge = new DataTable();
            string sSQL = null;
            DataBaseLayer oDB = new DataBaseLayer();
            bool IsXmlCreated = false;
            try
            {
                foreach (TreeNode nd in objUCLiquidData.trvDiscrete.Nodes)
                {
                    int i = 0;
                    //string _ColName = "nElementID,sElementName,sElementType,bIsMandatory,nGroupID,nColumnID,sCategoryName,sItemName,nControlType,sAssociatedCategory,sAssociateditem,sAssociatedProperty,'" + clsGeneral.gstrClinicName + "' as ClinicName,'" + clsGeneral.WebFolder + "' as Specialty";
                    string _ColName = "nElementID,sElementName,sElementType,bIsMandatory,nGroupID,nColumnID,sCategoryName,sItemName,nControlType,sAssociatedCategory,sAssociateditem,sAssociatedProperty,'" + clsGeneral.WebFolder + "' as Specialty";
                    //'DirectCast(nd, gloEMR.myTreeNode).children.length
                    for (i = 0; i <= nd.Nodes.Count - 1; i++)
                    {
                        if (nd.Nodes[i].Checked == true)
                        {
                            //Problem : 00000163
                            //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                            //Change : Order By nSequenceNo clause added to retrieve data as per sequence maintained by user.

                            sSQL = "select " + _ColName + " FROM LiquidData_MST where nelementid =" + ((myTreeNode)nd.Nodes[i]).Key + " or ngroupid =" + ((myTreeNode)nd.Nodes[i]).Key + " Order by nSequenceNo";
                            dt = oDB.GetDataTable_Query(sSQL);
                            if ((dt != null))
                            {
                                dtMerge.Merge(dt);
                            }
                        }
                    }
                }
                if (dtMerge != null && dtMerge.Rows.Count > 0)
                {
                    DataRow[] drCatNm = dtMerge.Select("nGroupID = 0");
                    foreach (DataRow dr in drCatNm)
                    {
                        switch ((clsGeneral.CategoryType)Convert.ToInt16(dr[6]))
                        {

                            case clsGeneral.CategoryType.None:
                                dr["sCategoryName"] = clsGeneral.CategoryType.None.ToString();
                                break;
                            case clsGeneral.CategoryType.General:
                                dr["sCategoryName"] = clsGeneral.CategoryType.General.ToString();
                                break;
                            case clsGeneral.CategoryType.Hitory:
                                dr["sCategoryName"] = clsGeneral.CategoryType.Hitory.ToString();
                                break;
                            case clsGeneral.CategoryType.Physical_Examination:
                                dr["sCategoryName"] = clsGeneral.CategoryType.Physical_Examination.ToString();
                                break;
                            case clsGeneral.CategoryType.Medical_Decision_Making:
                                dr["sCategoryName"] = clsGeneral.CategoryType.Medical_Decision_Making.ToString();
                                break;
                            case clsGeneral.CategoryType.HPI:
                                dr["sCategoryName"] = clsGeneral.CategoryType.HPI.ToString();
                                break;
                            case clsGeneral.CategoryType.Management_option:
                                dr["sCategoryName"] = clsGeneral.CategoryType.Management_option.ToString();
                                break;
                            case clsGeneral.CategoryType.Labs:
                                dr["sCategoryName"] = clsGeneral.CategoryType.Labs.ToString();
                                break;
                            case clsGeneral.CategoryType.X_Ray_Radiology:
                                dr["sCategoryName"] = clsGeneral.CategoryType.X_Ray_Radiology.ToString();
                                break;
                            case clsGeneral.CategoryType.Other_Diagonsis_Tests:
                                dr["sCategoryName"] = clsGeneral.CategoryType.Other_Diagonsis_Tests.ToString();
                                break;
                            case clsGeneral.CategoryType.ROS:
                                dr["sCategoryName"] = clsGeneral.CategoryType.ROS.ToString();
                                break;
                            case clsGeneral.CategoryType.DB_History:
                                dr["sCategoryName"] = clsGeneral.CategoryType.DB_History.ToString();
                                break;
                        }
                    }
                    dtMerge.AcceptChanges();
                    dtMerge.TableName = "Table";
                    dtMerge.WriteXml(XmlPath, false);
                    IsXmlCreated = true;
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString(), clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                IsXmlCreated = false;
            }
            return IsXmlCreated;
        }

        private bool ImportLiquidDataFromgloCommunity()
        {
            this.Cursor = Cursors.WaitCursor;
            bool IsImportLiquidData = false;
            DataTable objdata = null;
            clsLiquidData_Download objDlLiquidData;
            try
            {
                objDlLiquidData = new clsLiquidData_Download();
                objdata = new DataTable();
                string ServerXmlPath = gloSettings.FolderSettings.AppTempFolderPath + clsGeneral.gstrLiquidDataFNm + ".xml";
                //Crete XML for Selected Liquid data category.
                string _TableName = "Table";
                string _ParentValue = "";//variable for Clinic name or Specialty according to Clinic/Gloabl Repository
                DataTable dtXml = objDlLiquidData.GetXmlData(ServerXmlPath, _TableName);
                objdata = dtXml.Clone();
                if (dtXml.Rows.Count > 0 && dtXml != null)
                {
                    //DataRow[] drr = null;
                    //if (objUCLiquidData.IsClinicRepository ==false )
                    //{
                    //    drr = dtXml.Select("Specialty='" + objUCLiquidData.trvDiscrete.SelectedNode.Parent.Parent.Text + "'");
                    //}
                    //else
                    //{
                    //    drr = dtXml.Select("");

                    //}
                    // foreach (DataRow dr in dtXml.Rows)
                    //for(int len=0;len<drr.Length;len++) 
                    // {
                    foreach (TreeNode nd in objUCLiquidData.trvDiscrete.Nodes)//get Parent node('Clinic Name' Text)
                    {
                        foreach (TreeNode nd1 in nd.Nodes)//get Sub Parent node('Liquid Data' Text)
                        {
                            foreach (TreeNode ndChild in nd1.Nodes)//get child node('Liquid Data Category')
                            {

                                if (ndChild.Checked == true)
                                {
                                    DataRow[] drr = null;
                                    if (objUCLiquidData.IsClinicRepository == false)
                                    {
                                        drr = dtXml.Select("Specialty='" + ndChild.Parent.Parent.Text + "'");
                                    }
                                    else
                                    {
                                        drr = dtXml.Select("");

                                    }

                                    for (int len = 0; len < drr.Length; len++)
                                    {
                                        string _strElementName = "";

                                        if (drr[len]["sElementName"].ToString().Trim().Contains("˜"))
                                            _strElementName = drr[len]["sElementName"].ToString().Trim().Replace('˜', '≈');
                                        else
                                            _strElementName = drr[len]["sElementName"].ToString().Trim();

                                        if (objUCLiquidData.IsClinicRepository == true)
                                            _ParentValue = ndChild.Parent.Parent.Text; //drr[len]["ClinicName"].ToString().Trim();
                                        else
                                            _ParentValue = drr[len]["Specialty"].ToString().Trim();

                                        if (ndChild.Checked == true && _strElementName == ndChild.Text.Trim() && ndChild.Parent.Parent.Text == _ParentValue)
                                        {

                                            objdata = new DataTable();
                                            objdata = dtXml.Clone();
                                            //DataRow[] drXml = dtXml.Select("nGroupID = " + dr["nElementID"] + " OR sElementName = '" + ndChild.Text.Trim() + "'");
                                            DataRow[] drXml = null;
                                            if (objUCLiquidData.IsClinicRepository == false)
                                            {
                                                drXml = dtXml.Select("(nGroupID = " + drr[len]["nElementID"] + " and Specialty='" + ndChild.Parent.Parent.Text + "') OR (nElementID = " + drr[len]["nElementID"] + " and  Specialty='" + ndChild.Parent.Parent.Text + "')");
                                                // drr = dtXml.Select("Specialty='" + ndChild.Parent.Parent.Text + "'");
                                            }
                                            else
                                            {
                                                drXml = dtXml.Select("nGroupID = " + drr[len]["nElementID"] + " OR nElementID = " + drr[len]["nElementID"]);
                                            }
                                            for (int i = 0; i < drXml.Length; i++)
                                            {
                                                //Change CategoryName Text to Enum.
                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.None.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.None.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.General.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.General.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.Hitory.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.Hitory.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.Physical_Examination.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.Physical_Examination.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.Medical_Decision_Making.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.Medical_Decision_Making.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.HPI.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.HPI.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.Management_option.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.Management_option.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.Labs.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.Labs.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.X_Ray_Radiology.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.X_Ray_Radiology.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.Other_Diagonsis_Tests.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.Other_Diagonsis_Tests.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.ROS.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.ROS.GetHashCode();

                                                if (drXml[i]["sCategoryName"].ToString().Trim() == clsGeneral.CategoryType.DB_History.ToString().Trim())
                                                    drXml[i]["sCategoryName"] = clsGeneral.CategoryType.DB_History.GetHashCode();
                                                //End

                                                objdata.ImportRow(drXml[i]);
                                            }

                                            //Create Xml & Insert into database according to Selected Liquid data category.

                                            if (objdata.Rows.Count > 0 && objdata != null)
                                            {
                                                string ImportXmlPath = gloSettings.FolderSettings.AppTempFolderPath + ndChild.Text.Trim() + ".xml";

                                                //Problem : 00000163
                                                //Issue : When creating liquid data fields, the sorting does not order itself in a logical way (it's completely random). 
                                                //Change : Commented the below logic which sorts the datatable to maintain the original sequence.
                                                // To be verified by Pramod

                                                //DataView dv = new DataView();
                                                //dv.Table = objdata;
                                                //dv.Sort = "sCategoryName";

                                                //objdata = dv.ToTable();
                                                //objdata.AcceptChanges();

                                                objdata.WriteXml(ImportXmlPath, false);
                                                objDlLiquidData.ImportFromXML(ImportXmlPath);
                                                objdata = null;
                                            }
                                            //End
                                            IsImportLiquidData = true;
                                        }

                                    }//new for added
                                }//new for added for drr
                            }//if condition nochild chedcked
                        }//Liquid Data Category node
                    }//'Liquid Data' Text
                    //}//trvDiscrete.Nodes
                    //}
                }
                //End Crete XML for Selected Liquid data category.
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
                return IsImportLiquidData = false;
            }
            finally
            {
                objDlLiquidData = null;
                this.Cursor = Cursors.Default;
            }
            return IsImportLiquidData;
        }

        private bool IsLiquidDataSelected()
        {
            bool _IsSelected = false;
            foreach (TreeNode nd in objUCLiquidData.trvDiscrete.Nodes)
            {
                for (int i = 0; i <= nd.Nodes.Count - 1; i++)
                {
                    if (nd.Nodes[i].Checked == true)
                    {
                        _IsSelected = true;
                    }
                }
            }
            return _IsSelected;
        }
        #endregion

        #region "gloCommunity Share - Smart Treatment"

        private bool UploadShareSmartCPTToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string SmartCPTAssociationLocal = clsGeneral.gstrSmartCPTflnm + "Local";
            string SmartCPTAssociationSRV = clsGeneral.gstrSmartCPTflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrSmartCPTflnm + "/" + SmartCPTAssociationSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + SmartCPTAssociationLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartCPT) Xml.
                if (CreateSmartCPTXML(SmartCPTAssociationLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartCPTAssociationSRV + ".xml";
                        string _TableName = "CPT";
                        bool blnAssociationUserResult = objgloCommunity.CompareXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartCPTAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, SmartCPTAssociationLocal, SmartCPTAssociationSRV, clsGeneral.gstrSmartCPTflnm);

                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "User", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                objgloCommunity = null;
                arrLocalCatFileNm.Clear();
            }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareSmartCPTToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string SmartCPTAssociationLocal = clsGeneral.gstrSmartCPTflnm + "Local";
            string SmartCPTAssociationSRV = clsGeneral.gstrSmartCPTflnm;
            //string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + SmartCPTAssociationSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrSmartCPTflnm + "/" + SmartCPTAssociationSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + SmartCPTAssociationLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartCPT) Xml.
                if (CreateSmartCPTXML(SmartCPTAssociationLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartCPTAssociationSRV + ".xml";
                        string _TableName = "CPT";
                        bool blnAssociationUserResult = objgloCommunity.CompareXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartCPTAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, SmartCPTAssociationLocal, SmartCPTAssociationSRV, clsGeneral.gstrSmartCPTflnm);

                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                // MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool CreateSmartCPTXML(string XmlNm)
        {
            bool IsXmlCreate = false;
            if (objUCSmartCPT.trvCPTAssociation.Nodes[0].GetNodeCount(false) > 0)
            {
                bool IsRootCreated = false;
                bool IsCPTCreated = false;
                DataBaseLayer oDB = new DataBaseLayer();
                clsSmartDx_Upload objclsSmartDx_Upload = new clsSmartDx_Upload();
                string _sGenericName = "";
                string _sQuantity = "";
                string _PracticeFavorites = "false";
                string _BeersList = "0";
                string _IsAllergicDrug = "false";

                try
                {
                    //'MessageBox.Show(CPTCode)
                    XmlTextWriter xmlwriter = null;
                    string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;
                    string[] strCPTText = null;
                    string strCPTCode = string.Empty;
                    int i = 0;
                    for (i = 0; i <= objUCSmartCPT.trvCPTAssociation.Nodes[0].GetNodeCount(false) - 1; i++)
                    {
                        myTreeNode CPTNode = null;
                        CPTNode = (myTreeNode)objUCSmartCPT.trvCPTAssociation.Nodes[0].Nodes[i];
                        if (CPTNode.Checked == true)//XML create only for ICD9 Node is checked.
                        {
                            strCPTText = CPTNode.Text.Split('-');
                            if (strCPTText.Length > 0)
                            {
                                strCPTCode = strCPTText[0].Trim();
                            }
                            //'Checking of Node has child node, if not then Exit Sub.
                            foreach (myTreeNode oNode in CPTNode.Nodes)
                            {
                                if (oNode.Nodes.Count > 0)
                                {
                                    IsXmlCreate = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            if (IsXmlCreate == false)
                            {
                                //If trICD9Association.Nodes.Item(0).GetNodeCount(False) - 1 > 0 Then
                                //    Exit For
                                //Else
                                //    Exit Sub
                                //End If
                                //Exit Sub
                                continue;
                                //GoTo NextNode 

                            }

                            if (IsRootCreated == false)
                            {
                                xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                xmlwriter.WriteStartDocument();
                                xmlwriter.WriteStartElement("CPTAssociation");
                                //'Open the CPTAssociation Main Parent Node 
                                xmlwriter.WriteAttributeString("Text", "CPT Association");
                                IsRootCreated = true;
                            }


                            if (CPTNode.GetNodeCount(true) > 0)
                            {
                                int k = 0;
                                ArrayList arrlist = new ArrayList();
                                for (k = 0; k <= 7; k++)
                                {
                                    myTreeNode AssociateNode = default(myTreeNode);
                                    AssociateNode = (myTreeNode)CPTNode.Nodes[k];

                                    if (AssociateNode.GetNodeCount(false) - 1 != -1)
                                    {
                                        if (IsCPTCreated == false)
                                        {
                                            IsCPTCreated = true;
                                            xmlwriter.WriteStartElement("CPT");
                                            //'Start CPT
                                            xmlwriter.WriteAttributeString("Text", CPTNode.Text);
                                            xmlwriter.WriteAttributeString("ID", CPTNode.Key.ToString());
                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);
                                        }
                                        switch (AssociateNode.Text)
                                        {
                                            case "ICD9":
                                                xmlwriter.WriteStartElement("ICD9");
                                                //'Start CPT
                                                xmlwriter.WriteAttributeString("Text", "ICD9");
                                                break;
                                            case "Drugs":
                                                xmlwriter.WriteStartElement("Drugs");
                                                //'Start Drugs
                                                xmlwriter.WriteAttributeString("Text", "Drugs");
                                                break;
                                            case "Patient Education":
                                                xmlwriter.WriteStartElement("PatientEducation");
                                                //'Start Patient Education
                                                xmlwriter.WriteAttributeString("Text", "Patient Education");
                                                break;
                                            case "Tags":
                                                xmlwriter.WriteStartElement("Tags");
                                                //'Start Tags
                                                xmlwriter.WriteAttributeString("Text", "Tags");
                                                break;
                                            case "Flowsheet":
                                                xmlwriter.WriteStartElement("Flowsheet");
                                                //'Start Flowsheet
                                                xmlwriter.WriteAttributeString("Text", "Flowsheet");
                                                break;
                                            case "Lab Orders":
                                                xmlwriter.WriteStartElement("LabOrders");
                                                //'Start Lab Orders
                                                xmlwriter.WriteAttributeString("Text", "Lab Orders");
                                                break;
                                            case "Orders":
                                                xmlwriter.WriteStartElement("Orders");
                                                //'Start Orders 
                                                xmlwriter.WriteAttributeString("Text", "Orders");
                                                break;
                                            case "Referral Letter":
                                                xmlwriter.WriteStartElement("ReferralLetter");
                                                //'Start Referral Letters
                                                xmlwriter.WriteAttributeString("Text", "Referral Letter");
                                                break;
                                        }
                                    }
                                    int j = 0;


                                    for (j = 0; j <= AssociateNode.GetNodeCount(false) - 1; j++)
                                    {



                                        if (AssociateNode.Text == "ICD9")
                                        {
                                            xmlwriter.WriteStartElement("ICD9c");
                                            //'Start ICD9_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End CPT_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End CPT
                                            }

                                        }
                                        else if (AssociateNode.Text == "Drugs")
                                        {
                                            xmlwriter.WriteStartElement("Drugsc");
                                            //'Start Drugs_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            DataTable dtDrugMst = objclsSmartDx_Upload.FetchDrugByNDCCodepdate(((myTreeNode)AssociateNode.Nodes[j]).NDCCode);

                                            if (dtDrugMst.Rows.Count > 0 && dtDrugMst != null)
                                            {
                                                _sGenericName = dtDrugMst.Rows[0]["sGenericName"].ToString();
                                                _sQuantity = dtDrugMst.Rows[0]["Quantity"].ToString();
                                                _PracticeFavorites = dtDrugMst.Rows[0]["Practice Favorites"].ToString().ToLower();
                                                _BeersList = dtDrugMst.Rows[0]["Beers List"].ToString();
                                                _IsAllergicDrug = dtDrugMst.Rows[0]["bIsAllergicDrug"].ToString().ToLower();
                                            }

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End Drugs_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Drugs
                                            }


                                        }
                                        else if (AssociateNode.Text == "Patient Education")
                                        {
                                            string _PECatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _PECatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());

                                            xmlwriter.WriteStartElement("PatientEducationc");
                                            //'Start PatientEducation_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End PatientEducation_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Patient Education
                                            }


                                        }
                                        else if (AssociateNode.Text == "Tags")
                                        {
                                            string _TagsCatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _TagsCatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());

                                            xmlwriter.WriteStartElement("Tagsc");
                                            //'Start Tags_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End Tags_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Tags
                                            }


                                        }
                                        else if (AssociateNode.Text == "Flowsheet")
                                        {
                                            string _strQry = "select isnull(nColNumber,0)as ColNumber,isnull(sColumnName,'') as ColumnName,isnull(sFormat,'')as Format,isnull(dWidth,0.00)as Width,isnull(sAlignment,0.00)as Alignment,isnull(nForeColor,0)as ForeColor,isnull(nBackColor,0)as BackColor from FlowSheet_MST where nFlowSheetID = " + ((myTreeNode)AssociateNode.Nodes[j]).Key;
                                            DataTable dtFlowsheet = oDB.GetDataTable_Query(_strQry);

                                            xmlwriter.WriteStartElement("Flowsheetc");
                                            //'Start Flowsheet_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            if ((dtFlowsheet != null))
                                            {
                                                foreach (DataRow drFlosheet in dtFlowsheet.Rows)
                                                {
                                                    xmlwriter.WriteStartElement("Flowsheetc1");
                                                    //'Start Flowsheetc1
                                                    xmlwriter.WriteAttributeString("ColNumber", drFlosheet[0].ToString());
                                                    xmlwriter.WriteAttributeString("ColumnName", drFlosheet[1].ToString());
                                                    xmlwriter.WriteAttributeString("Format", drFlosheet[2].ToString());
                                                    xmlwriter.WriteAttributeString("Width", drFlosheet[3].ToString());
                                                    xmlwriter.WriteAttributeString("Alignment", drFlosheet[4].ToString());
                                                    xmlwriter.WriteAttributeString("ForeColor", drFlosheet[5].ToString());
                                                    xmlwriter.WriteAttributeString("BackColor", drFlosheet[6].ToString());
                                                    xmlwriter.WriteEndElement();
                                                    //'End Flowsheetc1
                                                }
                                            }

                                            xmlwriter.WriteEndElement();
                                            //'End Flowsheet_chil

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Flowsheet
                                            }


                                        }
                                        else if (AssociateNode.Text == "Lab Orders")
                                        {
                                            xmlwriter.WriteStartElement("LabOrdersc");
                                            //'Start LabOrders_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End LabOrders_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Lab Orders
                                            }

                                        }
                                        else if (AssociateNode.Text == "Orders")
                                        {
                                            string _strQry = "select LMCAT.lm_category_Description as Category,(select  lm_test_Name as GroupName from LM_Test where  LM.lm_test_GroupNo = lm_test_ID  ) as GroupName from LM_Test LM inner join LM_Category LMCAT on LM.lm_test_CategoryID = LMCAT.lm_Category_ID where lm_test_ID = " + ((myTreeNode)AssociateNode.Nodes[j]).Key;
                                            DataTable dtOrder = oDB.GetDataTable_Query(_strQry);

                                            xmlwriter.WriteStartElement("Ordersc");
                                            //'Start Orders_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);

                                            if ((dtOrder != null))
                                            {
                                                if (!string.IsNullOrEmpty(dtOrder.Rows[0][0].ToString()) & !string.IsNullOrEmpty(dtOrder.Rows[0][1].ToString()))
                                                {
                                                    xmlwriter.WriteAttributeString("DrugForm", dtOrder.Rows[0][0].ToString());
                                                    //'Category
                                                    xmlwriter.WriteAttributeString("Route", dtOrder.Rows[0][1].ToString());
                                                    //'GroupName
                                                }

                                            }

                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End Orders_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Orders
                                            }


                                        }
                                        else if (AssociateNode.Text == "Referral Letter")
                                        {
                                            string _RefCatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _RefCatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());

                                            xmlwriter.WriteStartElement("ReferralLetterc");
                                            //'Start ReferralLetter_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("CPTCode", strCPTCode);

                                            xmlwriter.WriteEndElement();
                                            //'End ReferralLetter_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Referral Letter
                                            }

                                        }
                                    }
                                }
                            }
                            //'CPTNode.GetNodeCount(True) > 0
                            if (IsCPTCreated == true)
                            {
                                xmlwriter.WriteEndElement();
                                //'End of ICD9
                                IsCPTCreated = false;
                            }
                            ////NextNode:
                        }//End CPTNode.Checked
                    }//End for loop
                    if (IsRootCreated == true)
                    {
                        xmlwriter.WriteEndElement();
                        //'End of CPTAssociation Main Parent Node 
                        xmlwriter.WriteEndDocument();
                        xmlwriter.Close();
                        //'End of Creating Xml
                    }

                }
                catch //(Exception ex)
                {
                    //commented by kanchan on 20120105
                    //MessageBox.Show(ex.ToString());
                }
            }

            return IsXmlCreate;
        }

        private void SaveSmartCPTAssociation()
        {
            //Get node count of child nodes in trICD9Associates
            bool CPTDownloaded = false;
            bool notshowmsg = false;
            TreeView trvSmartCPT = objUCSmartCPT.trvCPTAssociation;
            bool localsite = false;
            if (objUCSmartCPT.IsClinicRepository == true)
            {
                localsite = true;
            }
            try
            {
                if (trvSmartCPT.Nodes[0].GetNodeCount(false) > 0)
                {
                    clsCPTAssociationDBLayer objclsCPTAssociationDBLayer = new clsCPTAssociationDBLayer();
                    clsSmartDX objSPSmartDiag = new clsSmartDX();

                    DataTable DtICD9 = null;
                    DataTable DtREffTgsPE = null;
                    DataTable DtLab = null;
                    DataTable DtOrd = null;
                    DataTable DtDrg = null;
                   // DataTable DtPE = null;
                    DataTable DtFl = null;
                   // DataTable DtTgs = null;
                    int i = 0;
                    if (objUCSmartCPT.StrICD9.Length > 0)
                    {
                        DtICD9 = objSPSmartDiag.GetICD9IDFromCodes(objUCSmartCPT.StrICD9.Substring(0, objUCSmartCPT.StrICD9.Length - 1));
                    }
                    if (objUCSmartCPT.StrRefOrdTgsPE.Length > 0)
                    {
                        DtREffTgsPE = objSPSmartDiag.GetRefTgsPEIDFromCodes(objUCSmartCPT.StrRefOrdTgsPE.Substring(0, objUCSmartCPT.StrRefOrdTgsPE.Length - 1));
                    }
                    if (objUCSmartCPT.StrLabs.Length > 0)
                    {
                        DtLab = objSPSmartDiag.GetLabIDFromCodes(objUCSmartCPT.StrLabs.Substring(0, objUCSmartCPT.StrLabs.Length - 1));
                    }

                    if (objUCSmartCPT.StrOrd.Length > 0)
                    {
                        DtOrd = objSPSmartDiag.GetOrdIDFromCodes(objUCSmartCPT.StrOrd.Substring(0, objUCSmartCPT.StrOrd.Length - 1));
                    }

                    if (objUCSmartCPT.StrDrg.Length > 0)
                    {
                        DtDrg = objSPSmartDiag.GetDrgFromCodes(objUCSmartCPT.StrDrg.Substring(0, objUCSmartCPT.StrDrg.Length - 1));
                    }


                    if (objUCSmartCPT.StrFlo.Length > 0)
                    {
                        DtFl = objSPSmartDiag.GetFloIDFromCodes(objUCSmartCPT.StrFlo.Substring(0, objUCSmartCPT.StrFlo.Length - 1));
                    }


                    for (i = 0; i <= trvSmartCPT.Nodes[0].GetNodeCount(false) - 1; i++)
                    {
                        myTreeNode CPTNode = default(myTreeNode);
                        //get the CPTNode associated sequentially
                        ////CPTNode = trvSmartCPT.Nodes.Item(0).Nodes.Item(i);
                        CPTNode = (myTreeNode)trvSmartCPT.Nodes[0].Nodes[i];

                        if (CPTNode.Checked == true)
                        {

                            if (CPTNode.GetNodeCount(true) > 0)
                            {
                                string CPTcode = CPTNode.Text.Substring(0, CPTNode.Text.Trim().IndexOf("-") - 1);
                                string CPTDesc = CPTNode.Text.Substring(CPTNode.Text.Trim().IndexOf("-") + 1, CPTNode.Text.Length - (CPTNode.Text.Trim().IndexOf("-") + 1));
                                string CPTID = objclsCPTAssociationDBLayer.GetCPTIDFromCode(CPTcode.Trim(), CPTDesc.Trim());
                                if (objclsCPTAssociationDBLayer.CheckCPTAssociated(Convert.ToInt64(CPTID)) >= 1)
                                {
                                    DialogResult reply = MessageBox.Show(CPTNode.Text + " already associated? do you want to associate new", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (reply == System.Windows.Forms.DialogResult.Yes)
                                    {
                                    }
                                    else
                                    {
                                        notshowmsg = true;
                                        goto endicd9;

                                    }
                                }
                                int k = 0;
                                ArrayList arrlist = new ArrayList();
                                for (k = 0; k < CPTNode.Nodes.Count; k++)
                                {
                                    myTreeNode AssociateNode = default(myTreeNode);
                                    //AssociateNode = CPTNode.Nodes.Item(k);
                                    AssociateNode = (myTreeNode)CPTNode.Nodes[k];
                                    int j = 0;
                                    for (j = 0; j <= AssociateNode.GetNodeCount(false) - 1; j++)
                                    {
                                        if (AssociateNode.Text == "ICD9")
                                        {
                                            string StrCPTs = ((myTreeNode)AssociateNode.Nodes[j]).Text;
                                            string strCode = StrCPTs.Substring(0, StrCPTs.IndexOf("-") - 1);
                                            //DataRow[] dr = DtICD9.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Tag + "'");
                                            DataRow[] dr = DtICD9.Select("Code='" + strCode + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {

                                                string strDesc = StrCPTs.Substring(StrCPTs.IndexOf("-") + 1, StrCPTs.Length - (StrCPTs.IndexOf("-") + 1));
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objclsCPTAssociationDBLayer.InsertICD9MSTData(strCode, strDesc, 1, false);
                                            }

                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "i", AssociateNode.Nodes[j].Checked, 0));


                                            //For De-Normalization

                                        }
                                        else if (AssociateNode.Text == "Drugs") //added for Drugs
                                        {
                                            ////arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).Dosage, CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugForm))
                                            DataRow[] dr = DtDrg.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertDrugsMSTData(((myTreeNode)AssociateNode.Nodes[j]).DrugName, ((myTreeNode)AssociateNode.Nodes[j]).GenericName, ((myTreeNode)AssociateNode.Nodes[j]).Dosage, ((myTreeNode)AssociateNode.Nodes[j]).Route, ((myTreeNode)AssociateNode.Nodes[j]).Frequency, ((myTreeNode)AssociateNode.Nodes[j]).Duration, ((myTreeNode)AssociateNode.Nodes[j]).PracticeFavorites, ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics, ((myTreeNode)AssociateNode.Nodes[j]).mpid, ((myTreeNode)AssociateNode.Nodes[j]).IsAllergicDrug,
                                               clsGeneral.gClinicID, false, ((myTreeNode)AssociateNode.Nodes[j]).NDCCode, ((myTreeNode)AssociateNode.Nodes[j]).DrugForm, ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier, "", Convert.ToInt32(((myTreeNode)AssociateNode.Nodes[j]).BeersList), false, ((myTreeNode)AssociateNode.Nodes[j]).Quantity);

                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "d", ((myTreeNode)AssociateNode.Nodes[j]).DrugName, ((myTreeNode)AssociateNode.Nodes[j]).Dosage, ((myTreeNode)AssociateNode.Nodes[j]).DrugForm, ((myTreeNode)AssociateNode.Nodes[j]).Route, ((myTreeNode)AssociateNode.Nodes[j]).Frequency, ((myTreeNode)AssociateNode.Nodes[j]).NDCCode, ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics, ((myTreeNode)AssociateNode.Nodes[j]).Duration,
                                                ((myTreeNode)AssociateNode.Nodes[j]).mpid, ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier, AssociateNode.Nodes[j].Checked));
                                            ////For De-Normalization

                                        }
                                        else if (AssociateNode.Text == "Patient Education")
                                        {
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + " And Category='Patient Education'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Patient Education", false, localsite);
                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "p", AssociateNode.Nodes[j].Checked, 0));


                                        }
                                        else if (AssociateNode.Text == "Tags")
                                        {
                                            //// arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "t", AssociateNode.Nodes.Item(j).Checked, 0))
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + " And Category='Tags'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Tags", false, localsite);

                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "t", AssociateNode.Nodes[j].Checked, 0, ((myTreeNode)AssociateNode.Nodes[j]).Text));

                                        }
                                        else if (AssociateNode.Text == "Flowsheet")
                                        {
                                            DataRow[] dr = DtFl.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();

                                            }
                                            else
                                            {
                                                DataRow[] drflp = objUCSmartCPT.dtfloc.Select("Text='" + AssociateNode.Nodes[j].Text + "'");
                                                string flid = drflp[0]["flowsheetc_id"].ToString();
                                                //   DataRow[] drc = objUCSmartCPT.dtfloc1.Select("flowsheetc_id='" + ((myTreeNode)AssociateNode.Nodes[j]).mpid + "'" + "And ColNumber<>''" + "And ColumnName<>''");
                                                DataRow[] drc = objUCSmartCPT.dtfloc1.Select("flowsheetc_id='" + flid + "'" + "And ColNumber<>''" + "And ColumnName<>''");
                                                decimal Flowsheetid = 0;
                                                // decimal Prevflowid=0;
                                                for (int lendrc = 0; lendrc <= drc.Length - 1; lendrc++)
                                                {
                                                    if ((drc[lendrc]["ColNumber"].ToString().Trim() != string.Empty))
                                                    {
                                                        if ((drc[lendrc]["ColumnName"] != null))
                                                        {

                                                            Flowsheetid = Convert.ToDecimal(objSPSmartDiag.InsertFlowsheetMSTData(Flowsheetid, AssociateNode.Nodes[j].Text, drc.Length, Convert.ToInt16(drc[lendrc]["ColNumber"]), drc[lendrc]["ColumnName"].ToString().Trim(), drc[lendrc]["Format"].ToString().Trim(), Convert.ToDecimal(drc[lendrc]["Width"]), drc[lendrc]["Alignment"].ToString().Trim(), Convert.ToDecimal(drc[lendrc]["ForeColor"]), Convert.ToDecimal(drc[lendrc]["BackColor"]),
                                                           false));


                                                        }
                                                    }
                                                }
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = Flowsheetid.ToString();
                                            }

                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "f", AssociateNode.Nodes[j].Checked, 0));


                                        }
                                        else if (AssociateNode.Text == "Lab Orders")
                                        {
                                            DataRow[] dr = DtLab.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertLabTestMSTData(((myTreeNode)AssociateNode.Nodes[j]).Text, ((myTreeNode)AssociateNode.Nodes[j]).Text, false);


                                            }

                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "l", AssociateNode.Nodes[j].Checked, 0));


                                        }
                                        else if (AssociateNode.Text == "Orders")
                                        {
                                            DataRow[] dr = DtOrd.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                long _CategoryId = objSPSmartDiag.GetCategoryID(((myTreeNode)AssociateNode.Nodes[j]).DrugForm);

                                                long _mGroupID = objSPSmartDiag.GetGroupID(((myTreeNode)AssociateNode.Nodes[j]).Route, _CategoryId);
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertTest(_CategoryId, _mGroupID, ((myTreeNode)AssociateNode.Nodes[j]).Text);



                                                string strordid = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Orders", false, localsite);


                                            }
                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "o", AssociateNode.Nodes[j].Checked, 0));

                                        }
                                        else if (AssociateNode.Text == "Referral Letter")
                                        {
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + "And Category='Referral Letter'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Referral Letter", false, localsite);

                                            }
                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "r", AssociateNode.Nodes[j].Checked, 0));

                                        }

                                    }

                                }

                                objclsCPTAssociationDBLayer.AddData(Convert.ToInt64(CPTID), CPTNode.Text, arrlist);
                                CPTDownloaded = true;
                            }
                        endicd9: ;
                            //int aa = 0;
                        }
                    } //for


                }//if

            }//try
            catch //(Exception ex)
            {
            }

            finally
            {
                if (CPTDownloaded == true)
                {
                    MessageBox.Show("CPT downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (notshowmsg == false)
                    {
                        //Added by kanchan on 20120105
                        if (trvSmartCPT.Nodes.Count > 0)
                            MessageBox.Show("Select at least one CPT to download", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                //  trvSmartCPT.Nodes.Clear();
            }




        }

        #endregion

        #region "gloCommunity Share - Smart Order"
        private bool UploadShareSmartOrderToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string SmartOrderAssociationLocal = clsGeneral.gstrSmartOrderflnm + "Local";
            string SmartOrderAssociationSRV = clsGeneral.gstrSmartOrderflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrSmartOrderflnm + "/" + SmartOrderAssociationSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + SmartOrderAssociationLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartOrder) Xml.
                if (CreateSmartOrderXML(SmartOrderAssociationLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartOrderAssociationSRV + ".xml";
                        string _TableName = "Order";
                        bool blnAssociationUserResult = objgloCommunity.CompareXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartOrderAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, SmartOrderAssociationLocal, SmartOrderAssociationSRV, clsGeneral.gstrSmartOrderflnm);

                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "User", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                objgloCommunity = null;
                arrLocalCatFileNm.Clear();
            }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareSmartOrderToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string SmartOrderAssociationLocal = clsGeneral.gstrSmartOrderflnm + "Local";
            string SmartOrderAssociationSRV = clsGeneral.gstrSmartOrderflnm;
            //string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + SmartOrderAssociationSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrSmartOrderflnm + "/" + SmartOrderAssociationSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + SmartOrderAssociationLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartDx) Xml.
                if (CreateSmartOrderXML(SmartOrderAssociationLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartOrderAssociationSRV + ".xml";
                        string _TableName = "Order";
                        bool blnAssociationUserResult = objgloCommunity.CompareXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + SmartOrderAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, SmartOrderAssociationLocal, SmartOrderAssociationSRV, clsGeneral.gstrSmartOrderflnm);

                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool CreateSmartOrderXML(string XmlNm)
        {
            bool IsXmlCreate = false;
            if (objUCSmartOrder.trOrderAssociation.Nodes[0].GetNodeCount(false) > 0)
            {
                bool IsRootCreated = false;
                bool IsOrderCreated = false;
                DataBaseLayer oDB = new DataBaseLayer();
                clsSmartDx_Upload objclsSmartDx_Upload = new clsSmartDx_Upload();
                string _sGenericName = "";
                string _sQuantity = "";
                string _PracticeFavorites = "false";
                string _BeersList = "0";
                string _IsAllergicDrug = "false";

                try
                {
                    //'MessageBox.Show(OrderCode)
                    XmlTextWriter xmlwriter = null;
                    string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;
                    string[] strOrderText = null;
                    string strOrderCode = string.Empty;
                    int i = 0;
                    for (i = 0; i <= objUCSmartOrder.trOrderAssociation.Nodes[0].GetNodeCount(false) - 1; i++)
                    {
                        myTreeNode OrderNode = null;
                        OrderNode = (myTreeNode)objUCSmartOrder.trOrderAssociation.Nodes[0].Nodes[i];
                        if (OrderNode.Checked == true)//XML create only for ICD9 Node is checked.
                        {
                            strOrderText = OrderNode.Text.Split('-');
                            if (strOrderText.Length > 0)
                            {
                                strOrderCode = strOrderText[0].Trim();
                            }
                            //'Checking of Node has child node, if not then Exit Sub.
                            foreach (myTreeNode oNode in OrderNode.Nodes)
                            {
                                if (oNode.Nodes.Count > 0)
                                {
                                    IsXmlCreate = true;
                                    break; // TODO: might not be correct. Was : Exit For
                                }
                            }
                            if (IsXmlCreate == false)
                            {
                                //If trICD9Association.Nodes.Item(0).GetNodeCount(False) - 1 > 0 Then
                                //    Exit For
                                //Else
                                //    Exit Sub
                                //End If
                                //Exit Sub
                                continue;
                                //GoTo NextNode 

                            }

                            if (IsRootCreated == false)
                            {
                                xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                xmlwriter.WriteStartDocument();
                                xmlwriter.WriteStartElement("OrderAssociation");
                                //'Open the ICD9Association Main Parent Node 
                                xmlwriter.WriteAttributeString("Text", "Order Association");
                                IsRootCreated = true;
                            }


                            if (OrderNode.GetNodeCount(true) > 0)
                            {
                                int k = 0;
                                ArrayList arrlist = new ArrayList();
                                for (k = 0; k <= 4; k++)
                                {
                                    myTreeNode AssociateNode = default(myTreeNode);
                                    AssociateNode = (myTreeNode)OrderNode.Nodes[k];

                                    if (AssociateNode.GetNodeCount(false) - 1 != -1)
                                    {
                                        if (IsOrderCreated == false)
                                        {
                                            IsOrderCreated = true;
                                            xmlwriter.WriteStartElement("Order");
                                            //'Start ICD9
                                            xmlwriter.WriteAttributeString("Text", OrderNode.Text);
                                            xmlwriter.WriteAttributeString("ID", OrderNode.Key.ToString());
                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);
                                        }
                                        switch (AssociateNode.Text)
                                        {
                                            case "CPT":
                                                xmlwriter.WriteStartElement("CPT");
                                                //'Start CPT
                                                xmlwriter.WriteAttributeString("Text", "CPT");
                                                break;
                                            case "Drugs":
                                                xmlwriter.WriteStartElement("Drugs");
                                                //'Start Drugs
                                                xmlwriter.WriteAttributeString("Text", "Drugs");
                                                break;
                                            case "Patient Education":
                                                xmlwriter.WriteStartElement("PatientEducation");
                                                //'Start Patient Education
                                                xmlwriter.WriteAttributeString("Text", "Patient Education");
                                                break;
                                            case "Tags":
                                                xmlwriter.WriteStartElement("Tags");
                                                //'Start Tags
                                                xmlwriter.WriteAttributeString("Text", "Tags");
                                                break;
                                            case "FlowSheet":
                                                xmlwriter.WriteStartElement("FlowSheet");
                                                //'Start FlowSheet
                                                xmlwriter.WriteAttributeString("Text", "FlowSheet");
                                                break;
                                            case "Labs":
                                                xmlwriter.WriteStartElement("Labs");
                                                //'Start Lab Orders
                                                xmlwriter.WriteAttributeString("Text", "Labs");
                                                break;
                                            case "Orders":
                                                xmlwriter.WriteStartElement("Orders");
                                                //'Start Orders 
                                                xmlwriter.WriteAttributeString("Text", "Orders");
                                                break;
                                            case "Referral Letter":
                                                xmlwriter.WriteStartElement("ReferralLetter");
                                                //'Start Referral Letters
                                                xmlwriter.WriteAttributeString("Text", "Referral Letter");
                                                break;
                                        }
                                    }
                                    int j = 0;


                                    for (j = 0; j <= AssociateNode.GetNodeCount(false) - 1; j++)
                                    {



                                        if (AssociateNode.Text == "CPT")
                                        {
                                            xmlwriter.WriteStartElement("CPTc");
                                            //'Start CPT_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End CPT_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End CPT
                                            }

                                        }
                                        else if (AssociateNode.Text == "Drugs")
                                        {
                                            xmlwriter.WriteStartElement("Drugsc");
                                            //'Start Drugs_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            DataTable dtDrugMst = objclsSmartDx_Upload.FetchDrugByNDCCodepdate(((myTreeNode)AssociateNode.Nodes[j]).NDCCode);

                                            if (dtDrugMst.Rows.Count > 0 && dtDrugMst != null)
                                            {
                                                _sGenericName = dtDrugMst.Rows[0]["sGenericName"].ToString();
                                                _sQuantity = dtDrugMst.Rows[0]["Quantity"].ToString();
                                                _PracticeFavorites = dtDrugMst.Rows[0]["Practice Favorites"].ToString().ToLower();
                                                _BeersList = dtDrugMst.Rows[0]["Beers List"].ToString();
                                                _IsAllergicDrug = dtDrugMst.Rows[0]["bIsAllergicDrug"].ToString().ToLower();
                                            }

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End Drugs_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Drugs
                                            }


                                        }
                                        else if (AssociateNode.Text == "Patient Education")
                                        {
                                            string _PECatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _PECatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());

                                            xmlwriter.WriteStartElement("PatientEducationc");
                                            //'Start PatientEducation_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End PatientEducation_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Patient Education
                                            }


                                        }
                                        else if (AssociateNode.Text == "Tags")
                                        {
                                            string _TagsCatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _TagsCatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());

                                            xmlwriter.WriteStartElement("Tagsc");
                                            //'Start Tags_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End Tags_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Tags
                                            }


                                        }
                                        else if (AssociateNode.Text == "FlowSheet")
                                        {
                                            string _strQry = "select isnull(nColNumber,0)as ColNumber,isnull(sColumnName,'') as ColumnName,isnull(sFormat,'')as Format,isnull(dWidth,0.00)as Width,isnull(sAlignment,0.00)as Alignment,isnull(nForeColor,0)as ForeColor,isnull(nBackColor,0)as BackColor from FlowSheet_MST where nFlowSheetID = " + ((myTreeNode)AssociateNode.Nodes[j]).Key;
                                            DataTable dtFlowsheet = oDB.GetDataTable_Query(_strQry);

                                            xmlwriter.WriteStartElement("Flowsheetc");
                                            //'Start Flowsheet_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            if ((dtFlowsheet != null))
                                            {
                                                foreach (DataRow drFlosheet in dtFlowsheet.Rows)
                                                {
                                                    xmlwriter.WriteStartElement("Flowsheetc1");
                                                    //'Start Flowsheetc1
                                                    xmlwriter.WriteAttributeString("ColNumber", drFlosheet[0].ToString());
                                                    xmlwriter.WriteAttributeString("ColumnName", drFlosheet[1].ToString());
                                                    xmlwriter.WriteAttributeString("Format", drFlosheet[2].ToString());
                                                    xmlwriter.WriteAttributeString("Width", drFlosheet[3].ToString());
                                                    xmlwriter.WriteAttributeString("Alignment", drFlosheet[4].ToString());
                                                    xmlwriter.WriteAttributeString("ForeColor", drFlosheet[5].ToString());
                                                    xmlwriter.WriteAttributeString("BackColor", drFlosheet[6].ToString());
                                                    xmlwriter.WriteEndElement();
                                                    //'End Flowsheetc1
                                                }
                                            }

                                            xmlwriter.WriteEndElement();
                                            //'End Flowsheet_chil

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Flowsheet
                                            }


                                        }
                                        else if (AssociateNode.Text == "Labs")
                                        {
                                            xmlwriter.WriteStartElement("Labsc");
                                            //'Start LabOrders_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End LabOrders_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Lab Orders
                                            }

                                        }
                                        else if (AssociateNode.Text == "Orders")
                                        {
                                            string _strQry = "select LMCAT.lm_category_Description as Category,(select  lm_test_Name as GroupName from LM_Test where  LM.lm_test_GroupNo = lm_test_ID  ) as GroupName from LM_Test LM inner join LM_Category LMCAT on LM.lm_test_CategoryID = LMCAT.lm_Category_ID where lm_test_ID = " + ((myTreeNode)AssociateNode.Nodes[j]).Key;
                                            DataTable dtOrder = oDB.GetDataTable_Query(_strQry);

                                            xmlwriter.WriteStartElement("Ordersc");
                                            //'Start Orders_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);

                                            if (dtOrder != null && dtOrder.Rows.Count > 0)
                                            {
                                                if (!string.IsNullOrEmpty(dtOrder.Rows[0][0].ToString()) & !string.IsNullOrEmpty(dtOrder.Rows[0][1].ToString()))
                                                {
                                                    xmlwriter.WriteAttributeString("DrugForm", dtOrder.Rows[0][0].ToString());
                                                    //'Category
                                                    xmlwriter.WriteAttributeString("Route", dtOrder.Rows[0][1].ToString());
                                                    //'GroupName
                                                }

                                            }

                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End Orders_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Orders
                                            }


                                        }
                                        else if (AssociateNode.Text == "Referral Letter")
                                        {
                                            string _RefCatFnm = ((myTreeNode)AssociateNode.Nodes[j]).FullPath;
                                            string[] _myCatFnm = _RefCatFnm.Split('\\');
                                            arrLocalCatFileNm.Add(_myCatFnm[2] + " ≈ " + _myCatFnm[3] + " ≈ " + ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());

                                            xmlwriter.WriteStartElement("ReferralLetterc");
                                            //'Start ReferralLetter_child
                                            xmlwriter.WriteAttributeString("Text", ((myTreeNode)AssociateNode.Nodes[j]).Text);
                                            xmlwriter.WriteAttributeString("ID", ((myTreeNode)AssociateNode.Nodes[j]).Key.ToString());
                                            xmlwriter.WriteAttributeString("DrugName", ((myTreeNode)AssociateNode.Nodes[j]).DrugName);
                                            xmlwriter.WriteAttributeString("Dosage", ((myTreeNode)AssociateNode.Nodes[j]).Dosage);
                                            xmlwriter.WriteAttributeString("DrugForm", ((myTreeNode)AssociateNode.Nodes[j]).DrugForm);
                                            xmlwriter.WriteAttributeString("Route", ((myTreeNode)AssociateNode.Nodes[j]).Route);
                                            xmlwriter.WriteAttributeString("NDCCode", ((myTreeNode)AssociateNode.Nodes[j]).NDCCode);
                                            xmlwriter.WriteAttributeString("IsNarcotics", ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics.ToString());
                                            xmlwriter.WriteAttributeString("mpid", ((myTreeNode)AssociateNode.Nodes[j]).mpid.ToString());
                                            xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier);
                                            xmlwriter.WriteAttributeString("Status", ((myTreeNode)AssociateNode.Nodes[j]).Checked.ToString());
                                            xmlwriter.WriteAttributeString("Duration", ((myTreeNode)AssociateNode.Nodes[j]).Duration.ToString());
                                            xmlwriter.WriteAttributeString("Frequency", ((myTreeNode)AssociateNode.Nodes[j]).Frequency.ToString());

                                            xmlwriter.WriteAttributeString("GenericName", _sGenericName);
                                            xmlwriter.WriteAttributeString("Quantity", _sQuantity);
                                            xmlwriter.WriteAttributeString("PracticeFavorites", _PracticeFavorites);
                                            xmlwriter.WriteAttributeString("BeersList", _BeersList);
                                            xmlwriter.WriteAttributeString("IsAllergicDrug", _IsAllergicDrug);

                                            xmlwriter.WriteAttributeString("OrderCode", strOrderCode);

                                            xmlwriter.WriteEndElement();
                                            //'End ReferralLetter_child

                                            if (j == AssociateNode.GetNodeCount(false) - 1)
                                            {
                                                xmlwriter.WriteEndElement();
                                                //'End Referral Letter
                                            }

                                        }
                                    }
                                }
                            }
                            //'ICD9Node.GetNodeCount(True) > 0
                            if (IsOrderCreated == true)
                            {
                                xmlwriter.WriteEndElement();
                                //'End of ICD9
                                IsOrderCreated = false;
                            }
                            ////NextNode:
                        }//End ICD9Node.Checked
                    }//End for loop
                    if (IsRootCreated == true)
                    {
                        xmlwriter.WriteEndElement();
                        //'End of ICD9Association Main Parent Node 
                        xmlwriter.WriteEndDocument();
                        xmlwriter.Close();
                        //'End of Creating Xml  
                    }

                }
                catch //(Exception ex)
                {
                    //commented by kanchan on 20120105
                    //MessageBox.Show(ex.ToString());
                }
            }

            return IsXmlCreate;
        }

        private void SaveSmartOrderAssociation()
        {
            //Get node count of child nodes in trICD9Associates
            bool OrderDownloaded = false;
            bool notshowmsg = false;
            TreeView trvSmartOrder = objUCSmartOrder.trOrderAssociation;
            bool localsite = false;
            if (objUCSmartOrder.IsClinicRepository == true)
            {
                localsite = true;
            }
            try
            {
                if (trvSmartOrder.Nodes[0].GetNodeCount(false) > 0)
                {
                    clsSmartOrder objclsSmartOrder = new clsSmartOrder();
                    clsSmartDX objSPSmartDiag = new clsSmartDX();

                    //DataTable DtICD9 = null;
                    DataTable DtREffTgsPE = null;
                    DataTable DtLab = null;
                    DataTable DtOrd = null;
                    DataTable DtDrg = null;
                    //DataTable DtPE = null;
                    DataTable DtFl = null;
                    //DataTable DtTgs = null;
                    int i = 0;
                    if (objUCSmartOrder.StrRefOrdTgsPE.Length > 0)
                    {
                        DtREffTgsPE = objSPSmartDiag.GetRefTgsPEIDFromCodes(objUCSmartOrder.StrRefOrdTgsPE.Substring(0, objUCSmartOrder.StrRefOrdTgsPE.Length - 1));
                    }
                    if (objUCSmartOrder.StrLabs.Length > 0)
                    {
                        DtLab = objSPSmartDiag.GetLabIDFromCodes(objUCSmartOrder.StrLabs.Substring(0, objUCSmartOrder.StrLabs.Length - 1));
                    }

                    if (objUCSmartOrder.StrOrd.Length > 0)
                    {
                        DtOrd = objSPSmartDiag.GetOrdIDFromCodes(objUCSmartOrder.StrOrd.Substring(0, objUCSmartOrder.StrOrd.Length - 1));
                    }

                    if (objUCSmartOrder.StrDrg.Length > 0)
                    {
                        DtDrg = objSPSmartDiag.GetDrgFromCodes(objUCSmartOrder.StrDrg.Substring(0, objUCSmartOrder.StrDrg.Length - 1));
                    }


                    if (objUCSmartOrder.StrFlo.Length > 0)
                    {
                        DtFl = objSPSmartDiag.GetFloIDFromCodes(objUCSmartOrder.StrFlo.Substring(0, objUCSmartOrder.StrFlo.Length - 1));
                    }


                    for (i = 0; i <= trvSmartOrder.Nodes[0].GetNodeCount(false) - 1; i++)
                    {
                        myTreeNode OrderNode = default(myTreeNode);
                        //get the OrderNode associated sequentially
                        ////OrderNode = trvSmartOrder.Nodes.Item(0).Nodes.Item(i);
                        OrderNode = (myTreeNode)trvSmartOrder.Nodes[0].Nodes[i];

                        if (OrderNode.Checked == true)
                        {

                            if (OrderNode.GetNodeCount(true) > 0)
                            {
                                string OrderCode = OrderNode.Text;
                                //string CPTDesc = OrderNode.Text.Substring(OrderNode.Text.Trim().IndexOf("-") + 1, OrderNode.Text.Length - (OrderNode.Text.Trim().IndexOf("-") + 1));
                                string OrderID = objclsSmartOrder.GetOrderIDFromCode(OrderCode.Trim(), "Orderset", clsGeneral.gnClinicID);
                                if (objclsSmartOrder.CheckOrderAssociated(Convert.ToInt64(OrderID)) >= 1)
                                {
                                    DialogResult reply = MessageBox.Show('"' + OrderNode.Text + '"' + " already associated? do you want to associate new", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (reply == System.Windows.Forms.DialogResult.Yes)
                                    {
                                    }
                                    else
                                    {
                                        notshowmsg = true;
                                        goto endicd9;

                                    }
                                }
                                int k = 0;
                                ArrayList arrlist = new ArrayList();
                                for (k = 0; k < OrderNode.Nodes.Count; k++)
                                {
                                    myTreeNode AssociateNode = default(myTreeNode);
                                    //AssociateNode = OrderNode.Nodes.Item(k);
                                    AssociateNode = (myTreeNode)OrderNode.Nodes[k];
                                    int j = 0;
                                    for (j = 0; j <= AssociateNode.GetNodeCount(false) - 1; j++)
                                    {
                                        if (AssociateNode.Text == "Drugs") //added for Drugs
                                        {
                                            ////arrlist.Add(New myList(CType(AssociateNode.Nodes.Item(j), myTreeNode).Key, "d", CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugName, CType(AssociateNode.Nodes.Item(j), myTreeNode).Dosage, CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugForm))
                                            DataRow[] dr = DtDrg.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertDrugsMSTData(((myTreeNode)AssociateNode.Nodes[j]).DrugName, ((myTreeNode)AssociateNode.Nodes[j]).GenericName, ((myTreeNode)AssociateNode.Nodes[j]).Dosage, ((myTreeNode)AssociateNode.Nodes[j]).Route, ((myTreeNode)AssociateNode.Nodes[j]).Frequency, ((myTreeNode)AssociateNode.Nodes[j]).Duration, ((myTreeNode)AssociateNode.Nodes[j]).PracticeFavorites, ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics, ((myTreeNode)AssociateNode.Nodes[j]).mpid, ((myTreeNode)AssociateNode.Nodes[j]).IsAllergicDrug,
                                               clsGeneral.gnClinicID, false, ((myTreeNode)AssociateNode.Nodes[j]).NDCCode, ((myTreeNode)AssociateNode.Nodes[j]).DrugForm, ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier, "", Convert.ToInt32(((myTreeNode)AssociateNode.Nodes[j]).BeersList), false, ((myTreeNode)AssociateNode.Nodes[j]).Quantity);

                                            }


                                            arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "D", ((myTreeNode)AssociateNode.Nodes[j]).DrugName, ((myTreeNode)AssociateNode.Nodes[j]).Dosage, ((myTreeNode)AssociateNode.Nodes[j]).DrugForm, ((myTreeNode)AssociateNode.Nodes[j]).Route, ((myTreeNode)AssociateNode.Nodes[j]).Frequency, ((myTreeNode)AssociateNode.Nodes[j]).NDCCode, ((myTreeNode)AssociateNode.Nodes[j]).IsNarcotics, ((myTreeNode)AssociateNode.Nodes[j]).Duration,
                                                ((myTreeNode)AssociateNode.Nodes[j]).mpid, ((myTreeNode)AssociateNode.Nodes[j]).DrugQtyQualifier, AssociateNode.Nodes[j].Checked));
                                            ////For De-Normalization

                                        }
                                        else if (AssociateNode.Text == "Flowsheet")
                                        {
                                            DataRow[] dr = DtFl.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();

                                            }
                                            else
                                            {
                                                DataRow[] drflp = objUCSmartOrder.dtfloc.Select("Text='" + AssociateNode.Nodes[j].Text + "'");
                                                string flid = drflp[0]["flowsheetc_id"].ToString();
                                                //   DataRow[] drc = objUCSmartOrder.dtfloc1.Select("flowsheetc_id='" + ((myTreeNode)AssociateNode.Nodes[j]).mpid + "'" + "And ColNumber<>''" + "And ColumnName<>''");
                                                DataRow[] drc = objUCSmartOrder.dtfloc1.Select("flowsheetc_id='" + flid + "'" + "And ColNumber<>''" + "And ColumnName<>''");
                                                decimal Flowsheetid = 0;
                                                // decimal Prevflowid=0;
                                                for (int lendrc = 0; lendrc <= drc.Length - 1; lendrc++)
                                                {
                                                    if ((drc[lendrc]["ColNumber"].ToString().Trim() != string.Empty))
                                                    {
                                                        if ((drc[lendrc]["ColumnName"] != null))
                                                        {

                                                            Flowsheetid = Convert.ToDecimal(objSPSmartDiag.InsertFlowsheetMSTData(Flowsheetid, AssociateNode.Nodes[j].Text, drc.Length, Convert.ToInt16(drc[lendrc]["ColNumber"]), drc[lendrc]["ColumnName"].ToString().Trim(), drc[lendrc]["Format"].ToString().Trim(), Convert.ToDecimal(drc[lendrc]["Width"]), drc[lendrc]["Alignment"].ToString().Trim(), Convert.ToDecimal(drc[lendrc]["ForeColor"]), Convert.ToDecimal(drc[lendrc]["BackColor"]),
                                                           false));


                                                        }
                                                    }
                                                }
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = Flowsheetid.ToString();
                                            }

                                            //arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "f", AssociateNode.Nodes[j].Checked, 0));
                                            arrlist.Add(new myList("F", ((myTreeNode)AssociateNode.Nodes[j]).Text, Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), AssociateNode.Nodes[j].Checked));

                                        }
                                        else if (AssociateNode.Text == "Labs")
                                        {
                                            DataRow[] dr = DtLab.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertLabTestMSTData(((myTreeNode)AssociateNode.Nodes[j]).Text, ((myTreeNode)AssociateNode.Nodes[j]).Text, false);


                                            }

                                            //arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "l", AssociateNode.Nodes[j].Checked, 0));
                                            arrlist.Add(new myList("L", ((myTreeNode)AssociateNode.Nodes[j]).Text, Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), AssociateNode.Nodes[j].Checked));

                                        }
                                        else if (AssociateNode.Text == "Orders")
                                        {
                                            DataRow[] dr = DtOrd.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                long _CategoryId = objSPSmartDiag.GetCategoryID(((myTreeNode)AssociateNode.Nodes[j]).DrugForm);

                                                long _mGroupID = objSPSmartDiag.GetGroupID(((myTreeNode)AssociateNode.Nodes[j]).Route, _CategoryId);
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.InsertTest(_CategoryId, _mGroupID, ((myTreeNode)AssociateNode.Nodes[j]).Text);



                                                string strordid = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Orders", false, localsite);


                                            }
                                            //arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "o", AssociateNode.Nodes[j].Checked, 0));
                                            arrlist.Add(new myList("R", ((myTreeNode)AssociateNode.Nodes[j]).Text, Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), AssociateNode.Nodes[j].Checked));

                                        }
                                        else if (AssociateNode.Text == "Referral Letter")
                                        {
                                            DataRow[] dr = DtREffTgsPE.Select("Code='" + ((myTreeNode)AssociateNode.Nodes[j]).Text + "'" + "And Category='Referral Letter'");

                                            if (dr.Length > 0)
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = dr[0]["ID"].ToString();
                                            }
                                            else
                                            {
                                                ((myTreeNode)AssociateNode.Nodes[j]).Tag = objSPSmartDiag.GetIDFromData(((myTreeNode)AssociateNode.Nodes[j]).Text, 0, "Referral Letter", false, localsite);

                                            }
                                            //arrlist.Add(new myList(Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), "r", AssociateNode.Nodes[j].Checked, 0));
                                            arrlist.Add(new myList("T", ((myTreeNode)AssociateNode.Nodes[j]).Text, Convert.ToInt64(((myTreeNode)AssociateNode.Nodes[j]).Tag), AssociateNode.Nodes[j].Checked));
                                        }

                                    }

                                }

                                objclsSmartOrder.AddData(Convert.ToInt64(OrderID), OrderNode.Text, arrlist);
                                OrderDownloaded = true;
                            }
                        endicd9: ;
                            //int aa = 0;
                        }
                    } //for


                }//if

            }//try
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }

            finally
            {
                if (OrderDownloaded == true)
                {
                    MessageBox.Show("Order downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (notshowmsg == false)
                    {
                        //Added by kanchan on 20120105
                        if (trvSmartOrder.Nodes.Count > 0)
                            MessageBox.Show("Select At least one Order to download", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                //  trvSmartOrder.Nodes.Clear();
            }
        }
        #endregion

        #region "gloCommunity Share - History"
        private bool UploadShareHistoryToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string HistoryLocal = clsGeneral.gstrHistoryflnm + "Local";
            string HistorySRV = clsGeneral.gstrHistoryflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrHistoryflnm + "/" + HistorySRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + HistoryLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR History) Xml.
                if (CreateHistoryXML(HistoryLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);
                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + HistorySRV + ".xml";
                        string _TableName = "History";
                        bool blnAssociationUserResult = objgloCommunity.CompareHistoryXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + HistoryLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, HistoryLocal, HistorySRV, clsGeneral.gstrHistoryflnm);

                    if (IsXmlUploaded == true)
                        MessageBox.Show("History uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Fail to upload History", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select at least one Category to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                //added by kanchan on 20120105
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
                arrLocalCatFileNm.Clear();
            }


            return isUploaded;
        }

        private bool CreateHistoryXML(string XmlNm)
        {

            clsHistory objclsHistory = new clsHistory();
            bool IsXmlCreate = false;
            if (objUCHistory.trvCategory.Nodes[0].GetNodeCount(false) > 0)
            {
                bool IsRootCreated = false;
                try
                {
                    XmlTextWriter xmlwriter = null;
                    string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;

                    //if (objUCHistory.dgHistory.RowCount >= 1)
                    //{
                    //if (objUCHistory.dgHistory.DataSource != null && objUCHistory.dgHistory.Rows.Count > 0)
                    //{
                    if (objUCHistory.oHistories.Count > 0)
                    {
                        for (int i = 0; i < objUCHistory.oHistories.Count; i++)
                        {
                            if (objUCHistory.oHistories[i].Select.ToString().ToLower() == "true")
                            {
                                if (IsRootCreated == false)
                                {
                                    xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("History"); //start the History Main Parent Node 
                                    xmlwriter.WriteAttributeString("Text", "History");
                                    IsRootCreated = true;
                                }

                                xmlwriter.WriteStartElement("Category");//start Category
                                xmlwriter.WriteAttributeString("CategoryName", objUCHistory.oHistories[i].CategoryName);
                                xmlwriter.WriteAttributeString("Type", "History");

                                //Adding History Item
                                xmlwriter.WriteAttributeString("Description", objUCHistory.oHistories[i].Description);
                                xmlwriter.WriteAttributeString("Comments", objUCHistory.oHistories[i].Comments);
                                xmlwriter.WriteAttributeString("ConceptID", objUCHistory.oHistories[i].ConceptID);
                                xmlwriter.WriteAttributeString("DescriptionID", objUCHistory.oHistories[i].DescriptionID);
                                xmlwriter.WriteAttributeString("SnoMedID", objUCHistory.oHistories[i].SnoMedID);
                                xmlwriter.WriteAttributeString("SnomedDescription", objUCHistory.oHistories[i].SnomedDescription);
                                xmlwriter.WriteAttributeString("TranID1", objUCHistory.oHistories[i].TranID1);
                                xmlwriter.WriteAttributeString("TranID2", objUCHistory.oHistories[i].TranID2);
                                xmlwriter.WriteAttributeString("TranID3", objUCHistory.oHistories[i].TranID3);
                                xmlwriter.WriteAttributeString("ICD9", objUCHistory.oHistories[i].ICD9);
                                xmlwriter.WriteAttributeString("SnomedDefination", objUCHistory.oHistories[i].SnomedDefination);
                                //Added new cloumn in History master on 20121003
                                if (objUCHistory.oHistories[i].CPTCode != null)
                                    xmlwriter.WriteAttributeString("CPTCode", objUCHistory.oHistories[i].CPTCode);
                                if (objUCHistory.oHistories[i].HistoryType != null)
                                    xmlwriter.WriteAttributeString("HistoryType", objUCHistory.oHistories[i].HistoryType);
                                //End //Added new cloumn in History master.
                                xmlwriter.WriteAttributeString("ClinicName", clsGeneral.gstrClinicName);
                                xmlwriter.WriteAttributeString("Specialty", clsGeneral.WebFolder);
                                //

                                xmlwriter.WriteEndElement();//end of Category
                            }
                        }
                    }
                    //}
                    if (IsRootCreated == true)
                    {
                        xmlwriter.WriteEndElement();//end of History Main Parent Node 
                        xmlwriter.WriteEndDocument();//end document
                        xmlwriter.Close();//end of Creating Xml
                        IsXmlCreate = true;
                    }
                    //}

                }//end try
                catch //(Exception ex)
                {
                    //commented by kanchan on 20120105
                    //MessageBox.Show(ex.ToString());
                }
                finally
                {
                    objclsHistory = null;
                }
            }//end if

            return IsXmlCreate;
        }

        private bool UploadShareHistoryToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string HistoryLocal = clsGeneral.gstrHistoryflnm + "Local";
            string HistorySRV = clsGeneral.gstrHistoryflnm;
            //string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + HistorySRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrHistoryflnm + "/" + HistorySRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + HistoryLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR History) Xml.
                if (CreateHistoryXML(HistoryLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + HistorySRV + ".xml";
                        string _TableName = "History";
                        bool blnAssociationUserResult = objgloCommunity.CompareHistoryXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + HistoryLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, HistoryLocal, HistorySRV, clsGeneral.gstrHistoryflnm);

                    if (IsXmlUploaded == true)
                        MessageBox.Show("History Uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Fail to upload History", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Select at least one association to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //this.Cursor = Cursors.Default; 
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
            }


            return isUploaded;
        }

        private long SaveHistory()
        {
            this.Cursor = Cursors.WaitCursor;
            clsHistory objclsHistory = new clsHistory();
            long Id = 0;
            bool IsDownloaded = false;
            try
            {
                //if (objUCHistory.dgHistory.DataSource != null && objUCHistory.dgHistory.Rows.Count > 0)
                //{
                //    for (int i = 0; i < objUCHistory.dgHistory.Rows.Count; i++)
                //    {
                if (objUCHistory.oHistories.Count > 0)
                {
                    for (int HistoriesCnt = 0; HistoriesCnt < objUCHistory.oHistories.Count; HistoriesCnt++)
                    {
                        if (objUCHistory.oHistories[HistoriesCnt].Select == true)
                        {
                            Id = objclsHistory.InsertHistory(objUCHistory.oHistories[HistoriesCnt].CategoryName, objUCHistory.oHistories[HistoriesCnt].Description, objUCHistory.oHistories[HistoriesCnt].Comments, objUCHistory.oHistories[HistoriesCnt].Type, objUCHistory.oHistories[HistoriesCnt].ConceptID, objUCHistory.oHistories[HistoriesCnt].SnomedDescription, objUCHistory.oHistories[HistoriesCnt].SnomedDefination, objUCHistory.oHistories[HistoriesCnt].CPTCode, objUCHistory.oHistories[HistoriesCnt].HistoryType, objUCHistory.oHistories[HistoriesCnt].ICD9);

                            if (Id == -1)
                            {
                                MessageBox.Show("Duplicate " + '"' + objUCHistory.oHistories[HistoriesCnt].Description + '"' + " description of category " + '"' + objUCHistory.oHistories[HistoriesCnt].CategoryName + '"', clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                IsDownloaded = false;
                            }
                            else if (Id == 0)
                            {
                                MessageBox.Show("Failed to download " + '"' + objUCHistory.oHistories[HistoriesCnt].Description + '"', clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                IsDownloaded = false;
                            }
                            else
                                IsDownloaded = true;
                        }
                    }
                    //}
                    if (IsDownloaded == true)
                        MessageBox.Show("History downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //}
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objclsHistory = null;
            }

            return Id;
        }
        #endregion

        #region "gloCommunity Share - DmSetup"
        private bool UploadShareDmSetupToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string DmSetupLocal = clsGeneral.gstrDmSetupflnm + "Local";
            string DmSetupSRV = clsGeneral.gstrDmSetupflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrDmSetupflnm + "/" + DmSetupSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + DmSetupLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR History) Xml.
                if (CreateDmSetUpXML(DmSetupLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);
                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + DmSetupSRV + ".xml";
                        string _TableName = "CriteriaName";
                        bool blnAssociationUserResult = objgloCommunity.CompareDmSetupXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + DmSetupLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, DmSetupLocal, DmSetupSRV, clsGeneral.gstrDmSetupflnm);

                    //Upload only those Templates(Referral Letter,Guidlines) that are not available on gloCommunity.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "User", arrLocalCatFileNm, "DmSetup");
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one Patient Criteria to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
                arrLocalCatFileNm.Clear();
            }


            return isUploaded;
        }

        private bool UploadShareDmSetupToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            arrLocalCatFileNm.Clear();//Clear array containing Templates information
            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string DmSetupLocal = clsGeneral.gstrDmSetupflnm + "Local";
            string DmSetupSRV = clsGeneral.gstrDmSetupflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrDmSetupflnm + "/" + DmSetupSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + DmSetupLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartDx) Xml.
                if (CreateDmSetUpXML(DmSetupLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + DmSetupSRV + ".xml";
                        string _TableName = "CriteriaName";
                        bool blnAssociationUserResult = objgloCommunity.CompareDmSetupXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + DmSetupLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, DmSetupLocal, DmSetupSRV, clsGeneral.gstrDmSetupflnm);

                    //Upload only those Templates(Referral Letter,Guidelines) that are not available on SharePoint.
                    objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm, "DmSetup");
                    //End
                }
                else
                {
                    MessageBox.Show("Select at least one Patient Criteria to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
            }


            return isUploaded;
        }

        private bool CreateDmSetUpXML(string XmlNm)
        {
            bool IsXmlCreate = false;
            bool IsRootCreated = false;
            var _with1 = objUCDmSetup.c1PatientCriteria;
            gloStream.DiseaseManagement.Supporting.Criteria oCriteria = null;
            gloStream.DiseaseManagement.DiseaseManagement oDM = null;
            XmlTextWriter xmlwriter = null;
            string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;

            try
            {
                for (int i = 1; i < _with1.Rows.Count; i++)
                {
                    if (_with1.GetData(i, "Select").ToString() == "True")
                    {
                        long _CriteriaId = Convert.ToInt64(_with1.GetData(i, "dm_mst_ID"));
                        string _CriteriaName = _with1.GetData(i, "dm_mst_CriteriaName").ToString();

                        oCriteria = new gloStream.DiseaseManagement.Supporting.Criteria();
                        oDM = new gloStream.DiseaseManagement.DiseaseManagement();

                        if (_CriteriaId > 0 && !string.IsNullOrEmpty(_CriteriaName.Trim()))
                        {
                            oCriteria = oDM.GetCriteria(_CriteriaId, 0);
                            if (oCriteria != null)
                            {
                                if (IsRootCreated == false)
                                {
                                    xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("DmSetup");//Start the DmSetup Main Parent Node 
                                    xmlwriter.WriteAttributeString("Text", "DmSetup");
                                    IsRootCreated = true;
                                }//end IsRootCreated

                                xmlwriter.WriteStartElement("CriteriaName");//Start the Criteria Name Main SubParent Node
                                xmlwriter.WriteAttributeString("PatientCriteriaName", _CriteriaName);

                                #region "Start Demographics node"
                                xmlwriter.WriteStartElement("Demographics");
                                xmlwriter.WriteAttributeString("Text", "Patient Demographics Details");

                                xmlwriter.WriteAttributeString("AgeMin", oCriteria.AgeMinimum.ToString());
                                xmlwriter.WriteAttributeString("AgeMax", oCriteria.AgeMaximum.ToString());
                                xmlwriter.WriteAttributeString("Gender", oCriteria.Gender);
                                xmlwriter.WriteAttributeString("Race", oCriteria.Race);
                                xmlwriter.WriteAttributeString("MaritalStatus", oCriteria.MaritalStatus);
                                xmlwriter.WriteAttributeString("City", oCriteria.City);
                                xmlwriter.WriteAttributeString("State", oCriteria.State);
                                xmlwriter.WriteAttributeString("Zip", oCriteria.Zip);
                                xmlwriter.WriteAttributeString("EmplyementStatus", oCriteria.EmployeeStatus);

                                xmlwriter.WriteEndElement();
                                #endregion

                                #region "Start Vitals node"
                                xmlwriter.WriteStartElement("Vitals");//Start Vitals node
                                xmlwriter.WriteAttributeString("Text", "Patient Vitals Details");

                                xmlwriter.WriteAttributeString("HeightMin", oCriteria.HeightMinimum);
                                xmlwriter.WriteAttributeString("HeightMax", oCriteria.HeightMaximum);
                                xmlwriter.WriteAttributeString("WeightMin", oCriteria.WeightMinimum.ToString());
                                xmlwriter.WriteAttributeString("WeightMax", oCriteria.WeightMaximum.ToString());
                                xmlwriter.WriteAttributeString("BMIMin", oCriteria.BMIMinimum.ToString());
                                xmlwriter.WriteAttributeString("BMIMax", oCriteria.BMIMaximum.ToString());
                                xmlwriter.WriteAttributeString("TemperatureMin", oCriteria.TempratureMinumum.ToString());
                                xmlwriter.WriteAttributeString("TemperatureMax", oCriteria.TempratureMaximum.ToString());
                                xmlwriter.WriteAttributeString("PulseMin", oCriteria.PulseMinimum.ToString());
                                xmlwriter.WriteAttributeString("PulseMax", oCriteria.PulseMaximum.ToString());
                                xmlwriter.WriteAttributeString("PulseOxMin", oCriteria.PulseOXMinimum.ToString());
                                xmlwriter.WriteAttributeString("PulseOxMax", oCriteria.PulseOXMaximum.ToString());
                                xmlwriter.WriteAttributeString("BPSittingMin", oCriteria.BPSittingMinimum.ToString());
                                xmlwriter.WriteAttributeString("BPSittingMax", oCriteria.BPSittingMaximum.ToString());
                                xmlwriter.WriteAttributeString("BPStandingMin", oCriteria.BPStandingMinimum.ToString());
                                xmlwriter.WriteAttributeString("BPStandingMax", oCriteria.BPStandingMaximum.ToString());
                                xmlwriter.WriteAttributeString("DisplayMessage", oCriteria.DisplayMessage);

                                xmlwriter.WriteEndElement();//end Vitals node
                                #endregion

                                #region "Start History node"
                                xmlwriter.WriteStartElement("History");//Start History node
                                xmlwriter.WriteAttributeString("Text", "History Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "History")
                                    {
                                        xmlwriter.WriteStartElement("Historyc");//Start Historyc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Historyc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end History node
                                #endregion

                                #region "Start RxDrugs node"
                                xmlwriter.WriteStartElement("Drugs");//Start Drugs node
                                xmlwriter.WriteAttributeString("Text", "Drugs Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Medication")
                                    {
                                        xmlwriter.WriteStartElement("Drugsc");//Start Drugsc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Drugsc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Drugs node
                                #endregion

                                #region "Start ICD9 node"
                                xmlwriter.WriteStartElement("ICD9");//Start ICD9 node
                                xmlwriter.WriteAttributeString("Text", "ICD9 Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "ICD9")
                                    {
                                        xmlwriter.WriteStartElement("ICD9c");//Start ICD9c Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end ICD9c Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end ICD9 node
                                #endregion

                                #region "Start CPT node"
                                xmlwriter.WriteStartElement("CPT");//Start CPT node
                                xmlwriter.WriteAttributeString("Text", "CPT Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "CPT")
                                    {
                                        xmlwriter.WriteStartElement("CPTc");//Start CPTc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end CPTc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end CPT node
                                #endregion

                                #region "Start Lab node"
                                xmlwriter.WriteStartElement("Lab");//Start Lab node
                                xmlwriter.WriteAttributeString("Text", "Lab Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Lab")
                                    {
                                        xmlwriter.WriteStartElement("Labc");//Start Labc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Labc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Lab node
                                #endregion

                                #region "Start Order node"
                                xmlwriter.WriteStartElement("Order");//Start Order node
                                xmlwriter.WriteAttributeString("Text", "Order Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Order")
                                    {
                                        xmlwriter.WriteStartElement("Orderc");//Start Orderc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Orderc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Order node
                                #endregion

                                #region "Start Problemlist node"
                                xmlwriter.WriteStartElement("Problemlist");//Start Problemlist node
                                xmlwriter.WriteAttributeString("Text", "Problemlist Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Problemlist")
                                    {
                                        xmlwriter.WriteStartElement("Problemlistc");//Start Problemlistc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Problemlistc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Problemlist node
                                #endregion

                                #region "Start Orders to be given node"
                                xmlwriter.WriteStartElement("OrdersAssociation");
                                xmlwriter.WriteAttributeString("Text", "Orders to be Given");

                                //Add Labs details
                                for (int cntLabs = 1; cntLabs <= oCriteria.LabOrders.Count; cntLabs++)
                                {
                                    if (cntLabs == 1)
                                        xmlwriter.WriteStartElement("Labs");//Start Labs node

                                    xmlwriter.WriteStartElement("Labsc");//Start Labsc Child
                                    xmlwriter.WriteAttributeString("DMTemplateName", ((myList)(oCriteria.LabOrders[cntLabs])).DMTemplateName);
                                    xmlwriter.WriteAttributeString("CategoryID", "1");
                                    xmlwriter.WriteAttributeString("DrugName", ((myList)(oCriteria.LabOrders[cntLabs])).DrugName);
                                    xmlwriter.WriteAttributeString("DrugForm", ((myList)(oCriteria.LabOrders[cntLabs])).DrugForm);
                                    xmlwriter.WriteAttributeString("Dosage", ((myList)(oCriteria.LabOrders[cntLabs])).Dosage);
                                    xmlwriter.WriteAttributeString("Route", ((myList)(oCriteria.LabOrders[cntLabs])).Route);
                                    xmlwriter.WriteAttributeString("Frequency", ((myList)(oCriteria.LabOrders[cntLabs])).Frequency);
                                    xmlwriter.WriteAttributeString("NDCCode", ((myList)(oCriteria.LabOrders[cntLabs])).NDCCode);
                                    xmlwriter.WriteAttributeString("IsNarcotic", ((myList)(oCriteria.LabOrders[cntLabs])).IsNarcotic.ToString());
                                    xmlwriter.WriteAttributeString("Duration", ((myList)(oCriteria.LabOrders[cntLabs])).Duration);
                                    xmlwriter.WriteAttributeString("mpid", ((myList)(oCriteria.LabOrders[cntLabs])).mpid.ToString());
                                    xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myList)(oCriteria.LabOrders[cntLabs])).DrugQtyQualifier);
                                    xmlwriter.WriteEndElement();//end Labsc Child

                                    if (cntLabs == oCriteria.LabOrders.Count)
                                        xmlwriter.WriteEndElement();//end Labs node
                                }//end Labs

                                //Add RadiologyOrders details
                                for (int cntRadiologyOrd = 1; cntRadiologyOrd <= oCriteria.RadiologyOrders.Count; cntRadiologyOrd++)
                                {
                                    if (cntRadiologyOrd == 1)
                                        xmlwriter.WriteStartElement("Orders");//Start Orders node

                                    xmlwriter.WriteStartElement("Ordersc");//Start Ordersc Child node
                                    xmlwriter.WriteAttributeString("DMTemplateName", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).DMTemplateName);
                                    xmlwriter.WriteAttributeString("CategoryID", "2");
                                    xmlwriter.WriteAttributeString("DrugName", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).DrugName);
                                    xmlwriter.WriteAttributeString("DrugForm", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).DrugForm);
                                    xmlwriter.WriteAttributeString("Dosage", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).Dosage);
                                    xmlwriter.WriteAttributeString("Route", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).Route);
                                    xmlwriter.WriteAttributeString("Frequency", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).Frequency);
                                    xmlwriter.WriteAttributeString("NDCCode", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).NDCCode);
                                    xmlwriter.WriteAttributeString("IsNarcotic", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).IsNarcotic.ToString());
                                    xmlwriter.WriteAttributeString("Duration", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).Duration);
                                    xmlwriter.WriteAttributeString("mpid", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).mpid.ToString());
                                    xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myList)(oCriteria.RadiologyOrders[cntRadiologyOrd])).DrugQtyQualifier);
                                    xmlwriter.WriteEndElement();//end Ordersc Child node

                                    if (cntRadiologyOrd == oCriteria.RadiologyOrders.Count)
                                        xmlwriter.WriteEndElement();//end Orders node
                                }
                                //end RadiologyOrders

                                //Add Referrals details
                                for (int cntReferrals = 1; cntReferrals <= oCriteria.Referrals.Count; cntReferrals++)
                                {
                                    if (cntReferrals == 1)
                                        xmlwriter.WriteStartElement("Referrals");//Start Referrals node
                                    //Add Template information to upload on gloCommunity
                                    arrLocalCatFileNm.Add("Referral Letter" + " ≈ " + ((myList)(oCriteria.Referrals[cntReferrals])).DMTemplateName + " ≈ " + ((myList)(oCriteria.Referrals[cntReferrals])).ID.ToString());
                                    //
                                    xmlwriter.WriteStartElement("Referralsc");//Start Referralsc Child node
                                    xmlwriter.WriteAttributeString("DMTemplateName", ((myList)(oCriteria.Referrals[cntReferrals])).DMTemplateName);
                                    xmlwriter.WriteAttributeString("CategoryID", "3");
                                    xmlwriter.WriteAttributeString("DrugName", ((myList)(oCriteria.Referrals[cntReferrals])).DrugName);
                                    xmlwriter.WriteAttributeString("DrugForm", ((myList)(oCriteria.Referrals[cntReferrals])).DrugForm);
                                    xmlwriter.WriteAttributeString("Dosage", ((myList)(oCriteria.Referrals[cntReferrals])).Dosage);
                                    xmlwriter.WriteAttributeString("Route", ((myList)(oCriteria.Referrals[cntReferrals])).Route);
                                    xmlwriter.WriteAttributeString("Frequency", ((myList)(oCriteria.Referrals[cntReferrals])).Frequency);
                                    xmlwriter.WriteAttributeString("NDCCode", ((myList)(oCriteria.Referrals[cntReferrals])).NDCCode);
                                    xmlwriter.WriteAttributeString("IsNarcotic", ((myList)(oCriteria.Referrals[cntReferrals])).IsNarcotic.ToString());
                                    xmlwriter.WriteAttributeString("Duration", ((myList)(oCriteria.Referrals[cntReferrals])).Duration);
                                    xmlwriter.WriteAttributeString("mpid", ((myList)(oCriteria.Referrals[cntReferrals])).mpid.ToString());
                                    xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myList)(oCriteria.Referrals[cntReferrals])).DrugQtyQualifier);
                                    xmlwriter.WriteEndElement();//end Referralsc Child node

                                    if (cntReferrals == oCriteria.Referrals.Count)
                                        xmlwriter.WriteEndElement();//end Referrals node
                                }
                                //end Referrals

                                //Add Rx details
                                for (int cntRxDrugs = 1; cntRxDrugs <= oCriteria.RxDrugs.Count; cntRxDrugs++)
                                {
                                    if (cntRxDrugs == 1)
                                        xmlwriter.WriteStartElement("Rx");//Start Rx node

                                    xmlwriter.WriteStartElement("Rxc");//Start Rxc Child node
                                    xmlwriter.WriteAttributeString("DMTemplateName", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).DMTemplateName);
                                    xmlwriter.WriteAttributeString("CategoryID", "4");
                                    xmlwriter.WriteAttributeString("DrugName", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).DrugName);
                                    xmlwriter.WriteAttributeString("DrugForm", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).DrugForm);
                                    xmlwriter.WriteAttributeString("Dosage", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).Dosage);
                                    xmlwriter.WriteAttributeString("Route", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).Route);
                                    xmlwriter.WriteAttributeString("Frequency", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).Frequency);
                                    xmlwriter.WriteAttributeString("NDCCode", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).NDCCode);
                                    xmlwriter.WriteAttributeString("IsNarcotic", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).IsNarcotic.ToString());
                                    xmlwriter.WriteAttributeString("Duration", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).Duration);
                                    xmlwriter.WriteAttributeString("mpid", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).mpid.ToString());
                                    xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myList)(oCriteria.RxDrugs[cntRxDrugs])).DrugQtyQualifier);
                                    xmlwriter.WriteEndElement();//end Rxc Child node

                                    if (cntRxDrugs == oCriteria.RxDrugs.Count)
                                        xmlwriter.WriteEndElement();//end Rx node
                                }//end Rx

                                //Add Guidelines details
                                for (int cntGuidelines = 1; cntGuidelines <= oCriteria.Guidelines.Count; cntGuidelines++)
                                {
                                    if (cntGuidelines == 1)
                                        xmlwriter.WriteStartElement("Guidelines");//Start Guidelines node

                                    //Add Template information to upload on gloCommunity
                                    arrLocalCatFileNm.Add("Guidelines" + " ≈ " + ((myList)(oCriteria.Guidelines[cntGuidelines])).DMTemplateName + " ≈ " + ((myList)(oCriteria.Guidelines[cntGuidelines])).ID.ToString());
                                    //

                                    xmlwriter.WriteStartElement("Guidelinesc");//Start Guidelinesc Child node
                                    xmlwriter.WriteAttributeString("DMTemplateName", ((myList)(oCriteria.Guidelines[cntGuidelines])).DMTemplateName);
                                    xmlwriter.WriteAttributeString("CategoryID", "0");
                                    xmlwriter.WriteAttributeString("DrugName", ((myList)(oCriteria.Guidelines[cntGuidelines])).DrugName);
                                    xmlwriter.WriteAttributeString("DrugForm", ((myList)(oCriteria.Guidelines[cntGuidelines])).DrugForm);
                                    xmlwriter.WriteAttributeString("Dosage", ((myList)(oCriteria.Guidelines[cntGuidelines])).Dosage);
                                    xmlwriter.WriteAttributeString("Route", ((myList)(oCriteria.Guidelines[cntGuidelines])).Route);
                                    xmlwriter.WriteAttributeString("Frequency", ((myList)(oCriteria.Guidelines[cntGuidelines])).Frequency);
                                    xmlwriter.WriteAttributeString("NDCCode", ((myList)(oCriteria.Guidelines[cntGuidelines])).NDCCode);
                                    xmlwriter.WriteAttributeString("IsNarcotic", ((myList)(oCriteria.Guidelines[cntGuidelines])).IsNarcotic.ToString());
                                    xmlwriter.WriteAttributeString("Duration", ((myList)(oCriteria.Guidelines[cntGuidelines])).Duration);
                                    xmlwriter.WriteAttributeString("mpid", ((myList)(oCriteria.Guidelines[cntGuidelines])).mpid.ToString());
                                    xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myList)(oCriteria.Guidelines[cntGuidelines])).DrugQtyQualifier);
                                    xmlwriter.WriteEndElement();//end Guidelinesc Child node

                                    if (cntGuidelines == oCriteria.Guidelines.Count)
                                        xmlwriter.WriteEndElement();//end Guidelines node
                                }
                                //end Guidelines

                                //Add IM details
                                for (int cntIM = 1; cntIM <= oCriteria.IMlst.Count; cntIM++)
                                {
                                    if (cntIM == 1)
                                        xmlwriter.WriteStartElement("IM");//Start IM node

                                    xmlwriter.WriteStartElement("IMc");//Start IMc Child node
                                    xmlwriter.WriteAttributeString("DMTemplateName", ((myList)(oCriteria.IMlst[cntIM])).DMTemplateName);
                                    xmlwriter.WriteAttributeString("CategoryID", "5");
                                    xmlwriter.WriteAttributeString("DrugName", ((myList)(oCriteria.IMlst[cntIM])).DrugName);
                                    xmlwriter.WriteAttributeString("DrugForm", ((myList)(oCriteria.IMlst[cntIM])).DrugForm);
                                    xmlwriter.WriteAttributeString("Dosage", ((myList)(oCriteria.IMlst[cntIM])).Dosage);
                                    xmlwriter.WriteAttributeString("Route", ((myList)(oCriteria.IMlst[cntIM])).Route);
                                    xmlwriter.WriteAttributeString("Frequency", ((myList)(oCriteria.IMlst[cntIM])).Frequency);
                                    xmlwriter.WriteAttributeString("NDCCode", ((myList)(oCriteria.IMlst[cntIM])).NDCCode);
                                    xmlwriter.WriteAttributeString("IsNarcotic", ((myList)(oCriteria.IMlst[cntIM])).IsNarcotic.ToString());
                                    xmlwriter.WriteAttributeString("Duration", ((myList)(oCriteria.IMlst[cntIM])).Duration);
                                    xmlwriter.WriteAttributeString("mpid", ((myList)(oCriteria.IMlst[cntIM])).mpid.ToString());
                                    xmlwriter.WriteAttributeString("DrugQtyQualifier", ((myList)(oCriteria.IMlst[cntIM])).DrugQtyQualifier);
                                    xmlwriter.WriteEndElement();//end IMc Child node

                                    if (cntIM == oCriteria.IMlst.Count)
                                        xmlwriter.WriteEndElement();//end IM node
                                }
                                //end IM

                                xmlwriter.WriteEndElement();//end Orders Association
                                #endregion

                                xmlwriter.WriteEndElement();//end Criteria Name Main SubParent Node.
                            }//end oCriteria != null
                        }//end _CriteriaId & _CriteriaName
                    }//end IsChecked 
                }//end for

                if (IsRootCreated == true)
                {
                    xmlwriter.WriteEndElement();
                    //End of DmSetup Main Parent Node
                    xmlwriter.WriteEndDocument();
                    xmlwriter.Close();
                    IsXmlCreate = true;
                    //End of Creating Xml
                }

            }//end try
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (oDM != null)
                    oDM = null;
                if (oCriteria != null)
                    oCriteria = null;
            }

            return IsXmlCreate;
        }

        private bool SaveDmSetup()
        {
            this.Cursor = Cursors.WaitCursor;
            bool _IsSavedDmSetup = false;
            bool _IsDownlaod = false;
            gloStream.DiseaseManagement.DiseaseManagement oDM = null;
            var _with1 = objUCDmSetup.c1PatientCriteria;
            //string _UploadedCriteriaNames = "";
            //string _FailedCriteriaNames = "";
            //string _Msg = "";
            try
            {
                for (int i = 1; i < _with1.Rows.Count; i++)
                {
                    if (_with1.GetData(i, "Select").ToString() == "True")
                    {

                        //Check Criteria Name isExist Or Not
                        oDM = new gloStream.DiseaseManagement.DiseaseManagement();
                        int _Result = -1;
                        long _CriteriaId = oDM.IsExistCriteria(_with1.GetData(i, "PatientCriteriaName").ToString());
                        if (_CriteriaId > 0)
                        {
                            _Result = Convert.ToInt32(MessageBox.Show("Criteria " + '"' + _with1.GetData(i, "PatientCriteriaName").ToString() + '"' + " already exist, Do you want to Overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation));
                            if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite Criteria.
                            {
                                _IsDownlaod = true;
                                _IsSavedDmSetup = objUCDmSetup.AssignValToCriteria(_with1.GetData(i, "PatientCriteriaName").ToString(), _CriteriaId, "Modify");
                                //if (_IsSavedDmSetup == true)
                                //{
                                //    if (_UploadedCriteriaNames == "")
                                //        _UploadedCriteriaNames = _with1.GetData(i, "PatientCriteriaName").ToString();
                                //    else
                                //        _UploadedCriteriaNames += ", " + _with1.GetData(i, "PatientCriteriaName").ToString();
                                //}
                                //else
                                //{
                                //    if (_FailedCriteriaNames == "")
                                //        _FailedCriteriaNames = _with1.GetData(i, "PatientCriteriaName").ToString();
                                //    else
                                //        _FailedCriteriaNames += ", " + _with1.GetData(i, "PatientCriteriaName").ToString();
                                //}
                            }
                            else if (_Result == Convert.ToInt32(DialogResult.No))//No - don't Overwrite Criteria.
                            {
                                continue;
                            }
                            else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Exit
                            {
                                break;
                            }
                            //end Check Criteria
                        }
                        else
                        {
                            //Save DmSetup
                            _IsDownlaod = true;
                            _IsSavedDmSetup = objUCDmSetup.AssignValToCriteria(_with1.GetData(i, "PatientCriteriaName").ToString(), 0, "Download");
                            //if (_IsSavedDmSetup == true)
                            //{
                            //    if (_UploadedCriteriaNames == "")
                            //        _UploadedCriteriaNames = _with1.GetData(i, "PatientCriteriaName").ToString();
                            //    else
                            //        _UploadedCriteriaNames += ", " + _with1.GetData(i, "PatientCriteriaName").ToString();
                            //}
                            //else
                            //{
                            //    if (_FailedCriteriaNames == "")
                            //        _FailedCriteriaNames = _with1.GetData(i, "PatientCriteriaName").ToString();
                            //    else
                            //        _FailedCriteriaNames += ", " + _with1.GetData(i, "PatientCriteriaName").ToString();
                            //}
                        }
                    }
                }

                //if (_UploadedCriteriaNames != "")
                //{
                //    _Msg = "Successfully Downloaded DmSetup(s) : " + _UploadedCriteriaNames;
                //}
                //if (_FailedCriteriaNames != "")
                //{
                //    if (_UploadedCriteriaNames != "")
                //        _Msg += Environment.NewLine;
                //    _Msg += "Failed to Download DmSetup(s) : " + _FailedCriteriaNames;
                //}

                //if (_Msg != "")
                //    MessageBox.Show(_Msg, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //fixed bug id 37266
                if (_IsDownlaod == true)
                {
                    if (_IsSavedDmSetup == true)
                        MessageBox.Show("DM setup(s) downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to download DM setup(s)", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (oDM != null)
                    oDM = null;
            }

            return _IsSavedDmSetup;
        }
        #endregion

        #region  "gloCommunity Share - CVSetup"
        private bool UploadShareCVSetupToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string CVSetupLocal = clsGeneral.gstrCVSetupflnm + "Local";
            string CVSetupSRV = clsGeneral.gstrCVSetupflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrCVSetupflnm + "/" + CVSetupSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + CVSetupLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR SmartDx) Xml.
                if (CreateCVSetUpXML(CVSetupLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + CVSetupSRV + ".xml";
                        string _TableName = "CriteriaName";
                        bool blnAssociationUserResult = objgloCommunity.CompareCVSetupXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + CVSetupLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    if (objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, CVSetupLocal, CVSetupSRV, clsGeneral.gstrCVSetupflnm) == true)
                        MessageBox.Show("CVSetup(s) uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to upload the CVSetup(s)", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Select at least one Patient Criteria to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
            }


            return isUploaded;
        }

        private bool UploadShareCVSetupToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;

            //gloCommunity class instance.
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            //
            string CVSetupLocal = clsGeneral.gstrCVSetupflnm + "Local";
            string CVSetupSRV = clsGeneral.gstrCVSetupflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrCVSetupflnm + "/" + CVSetupSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + CVSetupLocal + ".xml";
                string FileNMSP = "";

                //Create Local(gloEMR History) Xml.
                if (CreateCVSetUpXML(CVSetupLocal + ".xml") == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);
                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + CVSetupSRV + ".xml";
                        string _TableName = "CriteriaName";
                        bool blnAssociationUserResult = objgloCommunity.CompareCVSetupXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnAssociationUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + CVSetupLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;

                    if (objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, CVSetupLocal, CVSetupSRV, clsGeneral.gstrCVSetupflnm) == true)
                        MessageBox.Show("CVSetup(s) uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to upload the CVSetup(s)", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Select at least one Patient Criteria to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
                arrLocalCatFileNm.Clear();
            }


            return isUploaded;
        }

        private bool CreateCVSetUpXML(string XmlNm)
        {
            bool IsXmlCreate = false;
            bool IsRootCreated = false;
            var _with1 = ObjucCVSetup.flxCVSetup;

            ClsCVSetup oDM = null;
            Criteria oCriteria = null;

            XmlTextWriter xmlwriter = null;
            string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;

            try
            {
                for (int i = 1; i < _with1.Rows.Count; i++)
                {
                    if (_with1.GetData(i, "Select").ToString() == "1")
                    {
                        long _CriteriaId = Convert.ToInt64(_with1.GetData(i, "ID"));
                        string _CriteriaName = _with1.GetData(i, "CriteriaName").ToString();

                        oCriteria = new Criteria();
                        oDM = new Classes.ClsCVSetup();

                        if (_CriteriaId > 0 && !string.IsNullOrEmpty(_CriteriaName.Trim()))
                        {
                            oCriteria = oDM.GetCriteria(_CriteriaId);
                            if (oCriteria != null)
                            {
                                if (IsRootCreated == false)
                                {
                                    xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("CVSetup");//Start the DmSetup Main Parent Node 
                                    xmlwriter.WriteAttributeString("Text", "CVSetup");
                                    IsRootCreated = true;
                                }//end IsRootCreated

                                xmlwriter.WriteStartElement("CriteriaName");//Start the Criteria Name Main SubParent Node
                                xmlwriter.WriteAttributeString("CVCriteriaName", _CriteriaName);

                                #region "Start Demographics node"
                                xmlwriter.WriteStartElement("Demographics");
                                xmlwriter.WriteAttributeString("Text", "Patient Demographics Details");

                                xmlwriter.WriteAttributeString("AgeMin", oCriteria.AgeMinimum.ToString());
                                xmlwriter.WriteAttributeString("AgeMax", oCriteria.AgeMaximum.ToString());
                                xmlwriter.WriteAttributeString("Gender", oCriteria.Gender);

                                xmlwriter.WriteEndElement();
                                #endregion

                                #region "Start Vitals node"
                                xmlwriter.WriteStartElement("Vitals");//Start Vitals node
                                xmlwriter.WriteAttributeString("Text", "Patient Vitals Details");

                                xmlwriter.WriteAttributeString("HeightMin", oCriteria.HeightMinimum);
                                xmlwriter.WriteAttributeString("HeightMax", oCriteria.HeightMaximum);
                                xmlwriter.WriteAttributeString("WeightMin", oCriteria.WeightMinimum.ToString());
                                xmlwriter.WriteAttributeString("WeightMax", oCriteria.WeightMaximum.ToString());
                                xmlwriter.WriteAttributeString("BMIMin", oCriteria.BMIMinimum.ToString());
                                xmlwriter.WriteAttributeString("BMIMax", oCriteria.BMIMaximum.ToString());
                                xmlwriter.WriteAttributeString("TemperatureMin", oCriteria.TempratureMinumum.ToString());
                                xmlwriter.WriteAttributeString("TemperatureMax", oCriteria.TempratureMaximum.ToString());
                                xmlwriter.WriteAttributeString("PulseMin", oCriteria.PulseMinimum.ToString());
                                xmlwriter.WriteAttributeString("PulseMax", oCriteria.PulseMaximum.ToString());
                                xmlwriter.WriteAttributeString("PulseOxMin", oCriteria.PulseOXMinimum.ToString());
                                xmlwriter.WriteAttributeString("PulseOxMax", oCriteria.PulseOXMaximum.ToString());
                                xmlwriter.WriteAttributeString("BPSittingMin", oCriteria.BPSittingMinimum.ToString());
                                xmlwriter.WriteAttributeString("BPSittingMax", oCriteria.BPSittingMaximum.ToString());
                                xmlwriter.WriteAttributeString("BPStandingMin", oCriteria.BPStandingMinimum.ToString());
                                xmlwriter.WriteAttributeString("BPStandingMax", oCriteria.BPStandingMaximum.ToString());
                                xmlwriter.WriteAttributeString("DisplayMessage", oCriteria.DisplayMessage);

                                xmlwriter.WriteEndElement();//end Vitals node
                                #endregion

                                #region "Start History node"
                                xmlwriter.WriteStartElement("History");//Start History node
                                xmlwriter.WriteAttributeString("Text", "History Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "History")
                                    {
                                        xmlwriter.WriteStartElement("Historyc");//Start Historyc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Historyc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end History node
                                #endregion

                                #region "Start RxDrugs node"
                                xmlwriter.WriteStartElement("Drugs");//Start Drugs node
                                xmlwriter.WriteAttributeString("Text", "Drugs Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Medication")
                                    {
                                        xmlwriter.WriteStartElement("Drugsc");//Start Drugsc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Drugsc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Drugs node
                                #endregion

                                #region "Start Lab node"
                                xmlwriter.WriteStartElement("Lab");//Start Lab node
                                xmlwriter.WriteAttributeString("Text", "Lab Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Lab")
                                    {
                                        xmlwriter.WriteStartElement("Labc");//Start Labc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Labc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Lab node
                                #endregion

                                #region "Start Order node"
                                xmlwriter.WriteStartElement("Order");//Start Order node
                                xmlwriter.WriteAttributeString("Text", "Order Details");

                                for (int cntOtherDtl = 1; cntOtherDtl <= oCriteria.OtherDetails.Count; cntOtherDtl++)
                                {
                                    if (oCriteria.OtherDetails[cntOtherDtl].DetailType.ToString().Trim() == "Order")
                                    {
                                        xmlwriter.WriteStartElement("Orderc");//Start Orderc Child node
                                        xmlwriter.WriteAttributeString("CategoryName", oCriteria.OtherDetails[cntOtherDtl].CategoryName);
                                        xmlwriter.WriteAttributeString("ItemName", oCriteria.OtherDetails[cntOtherDtl].ItemName);
                                        xmlwriter.WriteAttributeString("Operator", oCriteria.OtherDetails[cntOtherDtl].OperatorName);
                                        xmlwriter.WriteAttributeString("DetailType", oCriteria.OtherDetails[cntOtherDtl].DetailType.GetHashCode().ToString());
                                        xmlwriter.WriteEndElement();//end Orderc Child node
                                    }
                                }

                                xmlwriter.WriteEndElement();//end Order node
                                #endregion

                                xmlwriter.WriteEndElement();//end Criteria Name Main SubParent Node.
                            }//end oCriteria != null
                        }//end _CriteriaId & _CriteriaName
                    }//end IsChecked 
                }//end for

                if (IsRootCreated == true)
                {
                    xmlwriter.WriteEndElement();
                    //End of DmSetup Main Parent Node
                    xmlwriter.WriteEndDocument();
                    xmlwriter.Close();
                    IsXmlCreate = true;
                    //End of Creating Xml
                }

            }//end try
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.ToString());
            }
            finally
            {
                if (oDM != null)
                    oDM = null;
                if (oCriteria != null)
                    oCriteria = null;
            }

            return IsXmlCreate;
        }

        private bool SaveCVSetup()
        {
            this.Cursor = Cursors.WaitCursor;
            bool _IsSavedCVSetup = false;
            bool _IsDownlaod = false;
            ClsCVSetup oDM = null;
            Criteria oCriteria = null;
            var _with1 = ObjucCVSetup.flxCVSetup;
            //string _UploadedCriteriaNames = "";
            //string _FailedCriteriaNames = "";
            //string _Msg = "";
            try
            {
                for (int i = 1; i < _with1.Rows.Count; i++)
                {
                    if (_with1.GetData(i, "Select").ToString() == "True")
                    {

                        //Check Criteria Name isExist Or Not
                        oDM = new ClsCVSetup();
                        oCriteria = new Classes.Criteria();
                        int _Result = -1;
                        long _CriteriaId = oCriteria.IsExistCriteria(_with1.GetData(i, "CVCriteriaName").ToString());
                        if (_CriteriaId > 0)
                        {
                            _Result = Convert.ToInt32(MessageBox.Show("Criteria " + '"' + _with1.GetData(i, "CVCriteriaName").ToString() + '"' + " already exist, do you want to overwrite?", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation));
                            if (_Result == Convert.ToInt32(DialogResult.Yes))//Yes - Overwrite Criteria.
                            {
                                _IsDownlaod = true;
                                _IsSavedCVSetup = ObjucCVSetup.AssignValToCriteria(_with1.GetData(i, "CVCriteriaName").ToString(), _CriteriaId, "Modify");
                                //if (_IsSavedCVSetup == true)
                                //{
                                //    if (_UploadedCriteriaNames == "")
                                //        _UploadedCriteriaNames = _with1.GetData(i, "CVCriteriaName").ToString();
                                //    else
                                //        _UploadedCriteriaNames += ", " + _with1.GetData(i, "CVCriteriaName").ToString();
                                //}
                                //else
                                //{
                                //    if (_FailedCriteriaNames == "")
                                //        _FailedCriteriaNames = _with1.GetData(i, "CVCriteriaName").ToString();
                                //    else
                                //        _FailedCriteriaNames += ", " + _with1.GetData(i, "CVCriteriaName").ToString();
                                //}
                            }
                            else if (_Result == Convert.ToInt32(DialogResult.No))//No - don't Overwrite Criteria.
                            {
                                continue;
                            }
                            else if (_Result == Convert.ToInt32(DialogResult.Cancel))//Cancel - Exit
                            {
                                break;
                            }
                            //end Check Criteria
                        }
                        else
                        {
                            //Save DmSetup
                            _IsDownlaod = true;
                            _IsSavedCVSetup = ObjucCVSetup.AssignValToCriteria(_with1.GetData(i, "CVCriteriaName").ToString(), 0, "Download");
                            //if (_IsSavedCVSetup == true)
                            //{
                            //    if (_UploadedCriteriaNames == "")
                            //        _UploadedCriteriaNames = _with1.GetData(i, "CVCriteriaName").ToString();
                            //    else
                            //        _UploadedCriteriaNames += ", " + _with1.GetData(i, "CVCriteriaName").ToString();
                            //}
                            //else
                            //{
                            //    if (_FailedCriteriaNames == "")
                            //        _FailedCriteriaNames = _with1.GetData(i, "CVCriteriaName").ToString();
                            //    else
                            //        _FailedCriteriaNames += ", " + _with1.GetData(i, "CVCriteriaName").ToString();
                            //}
                        }
                    }
                }

                //if (_UploadedCriteriaNames != "")
                //{
                //    _Msg = "Successfully Downloaded CVSetup(s) : " + _UploadedCriteriaNames;
                //}
                //if (_FailedCriteriaNames != "")
                //{
                //    if (_UploadedCriteriaNames != "")
                //        _Msg += Environment.NewLine;
                //    _Msg += "Failed to Download CVSetup(s) : " + _FailedCriteriaNames;
                //}

                //if (_Msg != "")
                //    MessageBox.Show(_Msg, clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //fixed bug id 37266
                if (_IsDownlaod == true)
                {
                    if (_IsSavedCVSetup == true)
                        MessageBox.Show("CV setup(s) downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Failed to download CV setup(s)", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //this.Cursor = Cursors.Default;
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (oDM != null)
                    oDM = null;
            }

            return _IsSavedCVSetup;
        }
        #endregion

        #region "gloCommunity Share - IMSetup"
        private bool UploadShareIMSetupToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string IMSetupLocal = clsGeneral.gstrIMSetupflnm + "Local";
            string IMSetupLocalSRV = clsGeneral.gstrIMSetupflnm;
            bool IsXmlUploaded = true;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrIMSetupflnm + "/" + clsGeneral.gstrIMSetupflnm + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocal + ".xml";

                if (CreateImSetUpXML(IMSetupLocal + ".xml") == true)
                {
                    if (File.Exists(strName) == true)
                    {
                        bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                        if (IsDownloadXml == true)
                        {
                            FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocalSRV + ".xml";

                            string _TableName = "ImSetup";
                            bool blnImUserResult = objgloCommunity.CompareImSetupXML(FileNmLocal, FileNMSP, _TableName);

                            if (blnImUserResult == false)
                            {
                                this.Cursor = Cursors.Default;
                                return isUploaded = false;
                            }
                        }
                        else
                            FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocal + ".xml";

                        //Upload Xml to SharePoint
                        string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                        string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                        // string webSite = clsGeneral.gstrClinicName;
                        string webSite = clsGeneral.gstrClinicName;

                        IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, IMSetupLocal, IMSetupLocalSRV, clsGeneral.gstrIMSetupflnm);


                        if (IsXmlUploaded == true)
                            MessageBox.Show("Immunization Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                this.Cursor = Cursors.Default;
            }
            finally
            {
                objgloCommunity = null;
            }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool UploadShareIMSetupToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool IsXmlUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string IMSetupLocal = clsGeneral.gstrIMSetupflnm + "Local";
            string IMSetupLocalSRV = clsGeneral.gstrIMSetupflnm;
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrIMSetupflnm + "/" + clsGeneral.gstrIMSetupflnm + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocal + ".xml";
                string FileNMSP = "";
                //string strName = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocal + ".xml";

                if (CreateImSetUpXML(IMSetupLocal + ".xml") == true)
                {
                    //if (File.Exists(strName) == true)
                    //{
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocalSRV + ".xml";

                        string _TableName = "ImSetup";
                        bool blnImUserResult = objgloCommunity.CompareImSetupXML(FileNmLocal, FileNMSP, _TableName);

                        if (blnImUserResult == false)
                        {
                            this.Cursor = Cursors.Default;
                            return isUploaded = false;
                        }
                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + IMSetupLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNMSP, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, IMSetupLocal, IMSetupLocalSRV, clsGeneral.gstrIMSetupflnm);// SmartDxAssociationLocal, SmartDxAssociationSRV);
                    if (IsXmlUploaded == true)
                    {
                        MessageBox.Show("Immunization Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    //}
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                objgloCommunity = null;
            }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }

        private bool CreateImSetUpXML(string XmlNm)
        {
            bool IsXmlCreate = false;
            var _with1 = ObjucIMSetup.C1IMView;
            XmlTextWriter xmlwriter = null;
            bool IsRootCreated = false;
            string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + XmlNm;
            clsIMSetup oclsIMSetup = null;
            bool IsImSelected = false;

            try
            {
                for (int i = 1; i < _with1.Rows.Count; i++)
                {
                    if (_with1.GetData(i, "Select").ToString().ToLower() == "true")
                    {
                        IsImSelected = true;
                        oclsIMSetup = new clsIMSetup();
                        DataTable dtIm = oclsIMSetup.GetImmunizationByID(Convert.ToInt64(_with1.GetData(i, "ID")));
                        if (dtIm != null)
                        {
                            foreach (DataRow drIm in dtIm.Rows)
                            {
                                if (IsRootCreated == false)
                                {
                                    xmlwriter = new XmlTextWriter(FileNmLocal, null);
                                    xmlwriter.WriteStartDocument();
                                    xmlwriter.WriteStartElement("ImSetup"); //start the ImSetup Main Parent Node 
                                    xmlwriter.WriteAttributeString("Text", "Immunization");
                                    IsRootCreated = true;
                                }

                                xmlwriter.WriteStartElement("ImmunizationSetup");//Start ImmunizationSetup

                                xmlwriter.WriteAttributeString("Select", "0");
                                xmlwriter.WriteAttributeString("Count", drIm["Count"].ToString());
                                xmlwriter.WriteAttributeString("SKU", drIm["SKU"].ToString());
                                xmlwriter.WriteAttributeString("Vaccine", drIm["Vaccine"].ToString());
                                xmlwriter.WriteAttributeString("TradeName", drIm["Trade Name"].ToString());
                                xmlwriter.WriteAttributeString("Lot", drIm["Lot#"].ToString());
                                xmlwriter.WriteAttributeString("Mfr.", drIm["Mfr."].ToString());
                                xmlwriter.WriteAttributeString("OnHand", drIm["On Hand"].ToString());
                                xmlwriter.WriteAttributeString("Status", drIm["Status"].ToString());
                                xmlwriter.WriteAttributeString("Funding", drIm["Funding"].ToString());
                                xmlwriter.WriteAttributeString("ExpirationDate", drIm["Expiration Date"].ToString());
                                xmlwriter.WriteAttributeString("Comments", drIm["Comments"].ToString());
                                xmlwriter.WriteAttributeString("CPTCode", drIm["CPTCode"].ToString());
                                xmlwriter.WriteAttributeString("ICD9", drIm["ICD9"].ToString());
                                xmlwriter.WriteAttributeString("NDCCode", drIm["NDCCode"].ToString());
                                xmlwriter.WriteAttributeString("ReceivedDate", drIm["ReceivedDate"].ToString());
                                xmlwriter.WriteAttributeString("VialCount", drIm["im_item_VialCount"].ToString());
                                xmlwriter.WriteAttributeString("DosesperVial", drIm["im_DosesperVial"].ToString());
                                xmlwriter.WriteAttributeString("Location", _with1.GetData(i, "Location").ToString());

                                //Added new Category field in IMsetup master on 20121002
                                if (_with1.GetData(i, "Category") != null)
                                    xmlwriter.WriteAttributeString("Category", _with1.GetData(i, "Category").ToString());
                                //End

                                xmlwriter.WriteAttributeString("ClinicName", clsGeneral.gstrClinicName);
                                xmlwriter.WriteAttributeString("Specialty", clsGeneral.WebFolder);

                                xmlwriter.WriteEndElement();//end of ImmunizationSetup
                            }//end foreach
                        }//end if dtIm
                    }//end IsChecked 
                }//end for

                if (IsRootCreated == true)
                {
                    xmlwriter.WriteEndElement();//end of ImSetup- Main Parent Node 
                    xmlwriter.WriteEndDocument();//end document
                    xmlwriter.Close();//end of Creating Xml
                    IsXmlCreate = true;
                }
                //Fixed Bug # : 38008 on 20120928
                if (IsImSelected == false)
                    MessageBox.Show("Select at least one IM setup to upload.", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //End
            }//end try
            catch //(Exception ex)
            {
            }
            finally
            {
                if (oclsIMSetup != null)
                    oclsIMSetup = null;
            }
            return IsXmlCreate;
        }

        private long SaveImSetup()
        {
            this.Cursor = Cursors.WaitCursor;
            clsIMSetup oclsIMSetup = null;
        //    clsLiquidData_Download objDlLiquidData = null;
            long Id = 0;
            bool IsDownloaded = false;
            var _with1 = ObjucIMSetup.C1IMView;
            try
            {
                for (int i = 1; i < _with1.Rows.Count; i++)
                {
                    if (_with1.GetData(i, "Select").ToString() == "True")
                    {
                        oclsIMSetup = new clsIMSetup();

                        long cnt = oclsIMSetup.CheckVaccineExist(_with1.GetData(i, "Vaccine").ToString(), _with1.GetData(i, "Lot").ToString());
                        if (cnt == 0)
                        {
                            string[] arrVaccine = _with1.GetData(i, "Vaccine").ToString().Split('-');
                            long _Id = 0;
                            _Id = oclsIMSetup.InsertCategory(arrVaccine[0].Trim(), arrVaccine[1].Trim(), "Vaccine");
                            if (_Id != -1)
                            {
                                //Added new Category field in IMsetup master on 20121002
                                //Insert category of type "Immunization Inventory Category"
                                _Id = 0;
                                _Id = oclsIMSetup.InsertCategory("", _with1.GetData(i, "Category").ToString(), "Immunization Inventory Category");
                                if (_Id != -1)
                                {
                                    oclsIMSetup.IsExists(_with1.GetData(i, "Location").ToString(), clsGeneral.clinicID);

                                    long id = oclsIMSetup.SaveImmunization(Convert.ToInt32(_with1.GetData(i, "Count")), _with1.GetData(i, "CPTCode").ToString(), arrVaccine[0].Trim(), "", "", "", "", "", "", "", _with1.GetData(i, "SKU").ToString(), _with1.GetData(i, "ReceivedDate").ToString(), _with1.GetData(i, "Status").ToString(), _with1.GetData(i, "Vaccine").ToString(), _with1.GetData(i, "Mfr.").ToString(), _with1.GetData(i, "TradeName").ToString(), _with1.GetData(i, "Lot").ToString(), _with1.GetData(i, "ExpirationDate").ToString(), Convert.ToDecimal(_with1.GetData(i, "VialCount")), Convert.ToDecimal(_with1.GetData(i, "DosesperVial")), Convert.ToDecimal(_with1.GetData(i, "OnHand")), "", "", "", _with1.GetData(i, "NDCCode").ToString(), _with1.GetData(i, "Funding").ToString(), _with1.GetData(i, "Comments").ToString(), 0, 0, _with1.GetData(i, "Location").ToString(), _Id);
                                    if (id == -1)
                                    {
                                        IsDownloaded = false;//Fixed Bug # : 37263 on 20120912
                                        MessageBox.Show("Failed to download " + '"' + _with1.GetData(i, "Vaccine").ToString() + '"', clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                        IsDownloaded = true;//Fixed Bug # : 37263 on 20120912
                                    //MessageBox.Show("Immunization downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    IsDownloaded = false;
                                    MessageBox.Show("Failed to download " + '"' + _with1.GetData(i, "Vaccine").ToString() + '"', clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                //End Added new Category field in IMsetup master on 20121002
                            }
                            else
                            {
                                IsDownloaded = false;//Fixed Bug # : 37263 on 20120912
                                MessageBox.Show("Failed to download " + '"' + _with1.GetData(i, "Vaccine").ToString() + '"', clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                        else
                        {
                            IsDownloaded = false;//Fixed Bug # : 37263 on 20120912
                            MessageBox.Show('"' + _with1.GetData(i, "Lot").ToString() + '"' + " Lot# already exists", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                if (IsDownloaded == true)//Fixed Bug # : 37263 on 20120912
                    MessageBox.Show("Immunization downloaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch //(Exception ex)
            {

            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

            return Id;
        }
        #endregion

        //Task Mail upload to Clinic Site
        #region "TaskMail"
        private void getTaskMailClinicalData()
        {
            try
            {

                dtTskMlData.Rows.Clear();
                DataTable dtfollowup = (DataTable)objuctm.flxfollowup.DataSource;
                dtTskMlData = dtfollowup.Clone();
                AddData(dtfollowup);
                DataTable dtpri = (DataTable)objuctm.flxpritype.DataSource;
                if (dtpri != null)
                    AddData(dtpri);
                DataTable dtstat = (DataTable)objuctm.flxstattype.DataSource;
                if (dtstat != null)
                    AddData(dtstat);

                if (dtTskMlData.Rows.Count > 0)
                {
                    UploadShareTaskMailToClinicRepository();
                    dtTskMlData.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Select TaskMail Configuration  to upload", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch
            {

            }
        }

        private void getTaskMailGlobalRepository()
        {

            try
            {
                dtTskMlData.Rows.Clear();
                DataTable dtfollowup = (DataTable)objuctm.flxfollowup.DataSource;
                dtTskMlData = dtfollowup.Clone();
                AddData(dtfollowup);
                DataTable dtpri = (DataTable)objuctm.flxpritype.DataSource;
                if (dtpri != null)
                    AddData(dtpri);
                DataTable dtstat = (DataTable)objuctm.flxstattype.DataSource;
                if (dtstat != null)
                    AddData(dtstat);
                if (dtTskMlData.Rows.Count > 0)
                {
                    UploadShareTaskMailToGlobalRepository();
                    dtTskMlData.Rows.Clear();
                }
                else
                {
                    MessageBox.Show("Select TaskMail Configuration to upload", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }
            catch
            {

            }

        }

        private bool UploadShareTaskMailToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string TaskMailLocal = clsGeneral.gstrTskMlflnm + "Local";
            string TaskMailLocalSRV = clsGeneral.gstrTskMlflnm;//"SmartDxAssociation";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrTskMlflnm + "/" + clsGeneral.gstrTskMlflnm + ".xml";
            bool changedata = true;
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocal + ".xml";
                string FileNMSP = "";
                string strName = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocal + ".xml";
                dtTskMlData.WriteXml(strName);
                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocalSRV + ".xml";//SmartDxAssociationSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            DataTable dt = serverdata.Tables[0];
                            changedata = objclstm.CompareXMlData(dtTskMlData, dt, strName);

                        }


                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocal + ".xml"; //SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, TaskMailLocal, TaskMailLocalSRV, clsGeneral.gstrTskMlflnm);// SmartDxAssociationLocal, SmartDxAssociationSRV);
                        if (IsXmlUploaded == true)
                        {
                            MessageBox.Show("TaskMail Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        else

                            MessageBox.Show("Fail to uploaded TaskMail Configuration", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }

                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
            }


            return isUploaded;
        }

        private bool UploadShareTaskMailToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            bool changedata = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string TaskMailLocal = clsGeneral.gstrTskMlflnm + "Local";
            string TaskMailLocalSRV = clsGeneral.gstrTskMlflnm;
            // string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.gstrTskMlflnm + ".xml";   //SmartDxAssociationSRV + ".xml";
            string DownloadPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.WebFolder + "/" + clsGeneral.gstrTskMlflnm + "/" + clsGeneral.gstrTskMlflnm + ".xml";   //SmartDxAssociationSRV + ".xml";

            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocal + ".xml"; //"SmartDxAssociation.xml";
                dtTskMlData.WriteXml(strName);

                if (File.Exists(strName) == true)
                {
                    bool IsDownloadXml = objgloCommunity.DownloadXML(DownloadPath);

                    if (IsDownloadXml == true)
                    {

                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocalSRV + ".xml";

                        if (File.Exists(FileNMSP) == true)
                        {
                            DataSet serverdata = new DataSet();
                            serverdata.ReadXml(FileNMSP);
                            DataTable dt = serverdata.Tables[0];
                            changedata = objclstm.CompareXMlData(dtTskMlData, dt, strName);

                        }

                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + TaskMailLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    if (changedata == true)
                    {
                        bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, TaskMailLocal, TaskMailLocalSRV, clsGeneral.gstrTskMlflnm);
                        if (IsXmlUploaded == true)
                            MessageBox.Show("TaskMail Configuration uploaded successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else

                            MessageBox.Show("Fail to uploaded TaskMail Configuration", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);


                    }


                    //Upload only those Templates(Patient Education,Referral Letter,Tags) that are not available on SharePoint.
                    //   objgloCommunity.UploadTemplates(IsXmlUploaded, "Global", arrLocalCatFileNm);
                    //End
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex)
            {
                //commented by kanchan on 20120105
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                objgloCommunity = null;
            }


            return isUploaded;
        }



        private void AddData(DataTable dt)
        {

            dt.AcceptChanges();

            DataRow[] dr = dt.Select("Select='True'");
            for (int len = 0; len < dr.Length; len++)
            {
                dtTskMlData.ImportRow(dr[len]);
            }
            dr = null;

        }

        private void DownloadTaskMailData()
        {
            //Insert FollowUp Data
            bool dataDownloaded = false;
            bool msgshown = false;
            string msgdesc = "";
            try
            {


                DataTable dtflow = (DataTable)objuctm.flxfollowup.DataSource;
                DataTable dtorigflow = objclstm.GetFollowUps();
                dtflow.AcceptChanges();
                dtorigflow.AcceptChanges();

                if (dtflow != null)
                {
                    for (int len = 0; len < dtflow.Rows.Count; len++)
                    {
                        if (dtflow.Rows[len][0].ToString().ToLower() == "true")
                        {
                            DataRow[] dr = dtorigflow.Select("Description='" + dtflow.Rows[len]["Description"].ToString().Replace('"', ' ') + "' and Category='Followup'");
                            if (dr.Length == 0)
                            {
                                objclstm.InsertFollowUps(dtflow.Rows[len]["Description"].ToString(), false, clsGeneral.gClinicID, false);
                                dataDownloaded = true;
                            }
                            else
                            {
                                msgdesc += dtflow.Rows[len]["Description"].ToString().Replace('"', ' ') + " , ";
                            }
                        }

                    }

                    if (msgdesc.Length > 2)
                    {
                        msgdesc = msgdesc.Substring(0, msgdesc.Length - 2);
                        MessageBox.Show("For Category Follow Up '" + msgdesc + "' already exist", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msgshown = true;
                    }
                    msgdesc = "";
                }



                //Insert Priority  Data

                DataTable dtpri = (DataTable)objuctm.flxpritype.DataSource;
                DataTable dtorigpri = objclstm.GetPriorities();
                dtpri.AcceptChanges();
                dtorigpri.AcceptChanges();
                if (dtpri != null)
                {
                    for (int len = 0; len < dtpri.Rows.Count; len++)
                    {
                        if ((dtpri.Rows[len][0].ToString().ToLower() == "true"))
                        {
                            DataRow[] dr = dtorigpri.Select("Description='" + dtpri.Rows[len]["Description"].ToString().Replace('"', ' ') + "' and Category='Priorities'");
                            if (dr.Length == 0)
                            {
                                objclstm.InsertPriorities(dtpri.Rows[len]["Description"].ToString(), false, clsGeneral.gClinicID, false, Convert.ToInt64(dtpri.Rows[len]["nlevel"]));
                                dataDownloaded = true;
                            }
                            else
                            {
                                msgdesc += dtpri.Rows[len]["Description"].ToString().Replace('"', ' ') + " , ";
                            }
                        }

                    }

                    if (msgdesc.Length > 2)
                    {
                        msgdesc = msgdesc.Substring(0, msgdesc.Length - 2);
                        MessageBox.Show("For Category Priority  '" + msgdesc + "' already exist", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msgshown = true;
                    }
                    msgdesc = "";

                }

                //Insert Status Data


                DataTable dtstat = (DataTable)objuctm.flxstattype.DataSource;
                DataTable dtorigstat = objclstm.GetStatuses();
                dtstat.AcceptChanges();
                dtorigstat.AcceptChanges();
                if (dtstat != null)
                {
                    for (int len = 0; len < dtstat.Rows.Count; len++)
                    {
                        if (dtstat.Rows[len][0].ToString().ToLower() == "true")
                        {
                            DataRow[] dr = dtorigstat.Select("Description='" + dtstat.Rows[len]["Description"].ToString().Replace('"', ' ') + "' and Category='Status'");
                            if (dr.Length == 0)
                            {
                                objclstm.InsertStatuses(dtstat.Rows[len]["Description"].ToString(), false, clsGeneral.gClinicID, false);
                                dataDownloaded = true;
                            }
                            else
                            {
                                msgdesc += dtstat.Rows[len]["Description"].ToString().Replace('"', ' ') + " , ";
                            }

                        }

                    }
                    if (msgdesc.Length > 2)
                    {
                        msgdesc = msgdesc.Substring(0, msgdesc.Length - 2);
                        MessageBox.Show("For Category Status '" + msgdesc + "' already exist", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        msgshown = true;
                    }
                    msgdesc = "";

                }

                if (dataDownloaded == true)
                {
                    MessageBox.Show("TaskMail Configuration download successfully", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (msgshown == false)
                    {
                        MessageBox.Show("Select TaskMail Configuration to download ", clsGeneral.gstrMessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }

            }


            catch
            {

            }


            finally
            {

            }


        }

        #endregion   //Task Mail

    }//Class
}//NameSpace
