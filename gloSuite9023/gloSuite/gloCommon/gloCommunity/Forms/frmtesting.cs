using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using gloCommunity.UserControls;
using gloCommunity.Classes;
using System.IO;
using System.Collections;  
namespace gloCommunity.Forms
{
    public partial class frmtesting : Form
    {
        DataTable dtAppconfData =new DataTable();
        UCAppointConf  objUCAppConf = null;
        clsApptconf  objclsAppconf=new clsApptconf();
        ArrayList arrspec = new ArrayList();  
        public frmtesting()
        {
            InitializeComponent();
           //objuctm = new UCTaskMail();
            objUCAppConf = new UCAppointConf(); 
            objUCAppConf.Dock = DockStyle.Fill;
            this.Controls.Add(objUCAppConf);  
        }
        public frmtesting(string straction)
        {
            InitializeComponent();
            objUCAppConf = new UCAppointConf();// UCAppointConf(straction);
            objUCAppConf.Dock = DockStyle.Fill;
            this.Controls.Add(objUCAppConf); 
            if (straction == "Download")
            {
                btndownload.Visible = true;
                btnclinic.Visible = false;
                btnglobal.Visible = false;
                
            }
            else
            {
                btndownload.Visible = false;
                btnclinic.Visible = true;
                btnglobal.Visible = true;
            }
          }


        DataTable dtResource =null;
        DataTable dtFollowup = null;
        DataTable dtProblem = null;
        DataTable dtDept = null;
        DataTable dtApptStat = null;
        DataTable dtApptType = null;
        DataTable dtApptBlk = null;
        DataTable dtLoc = null;
        
        private void btnclinic_Click(object sender, EventArgs e)
        {

            try
            {
                getdataforAppConfuploading();
            
                UploadShareAppConfToClinicRepository();
            }
           
            catch
            {

            }
                     
        }


        private void getdataforAppConfuploading()
        {
            dtAppconfData.Rows.Clear();
            dtResource= (DataTable)objUCAppConf.flxRes.DataSource;
            if (dtResource != null)
            dtResource.TableName = "Resource";

            dtFollowup = (DataTable)objUCAppConf.flxfollup.DataSource;
            if (dtFollowup != null)
                dtFollowup.TableName = "FollowUp";

            dtProblem = (DataTable)objUCAppConf.flxPrb.DataSource;
            if (dtProblem != null)
                dtProblem.TableName = "Problem";

            dtDept = (DataTable)objUCAppConf.flxDept.DataSource;
            if (dtDept != null)
                dtDept.TableName = "Department";

            dtApptStat  = (DataTable)objUCAppConf.flxApptstat.DataSource;
            if (dtApptStat != null)
                dtApptStat.TableName = "AppointmentStatus";

            dtApptType = (DataTable)objUCAppConf.flxAppt.DataSource;
            if (dtApptType != null)
                dtApptType.TableName = "AppointmentType";

            dtApptBlk= (DataTable)objUCAppConf.flxApptblk.DataSource;
            if (dtApptBlk != null)
                dtApptBlk.TableName = "AppointmentBlock";

            dtLoc = (DataTable)objUCAppConf.flxLoc.DataSource;
            if (dtLoc != null)
                dtLoc.TableName = "Location";  
   

           // dtCPT = (DataTable)objUCAppConf.flxCPT.DataSource;
           //if( dtCPT!=null)
           // dtCPT.TableName = "CPT"; 
           // dtCat = (DataTable)objUCAppConf.flxCat.DataSource;
           // if( dtCat!=null)
           // dtCat.TableName = "Category";
        
           // dtMod = (DataTable)objUCAppConf.flxMod.DataSource;
           // if (dtMod != null)
           // dtMod.TableName = "Modifier"; 
      
           // dtPatr = (DataTable)objUCAppConf.flxPatr.DataSource;
           // if (dtPatr != null) 
           // dtPatr.TableName = "PatientRelation";  
       
           // dtpln = (DataTable)objUCAppConf.flxPln.DataSource;
           // if (dtpln != null)
           // dtpln.TableName = "Plan"; 
           // dtspec = (DataTable)objUCAppConf.flxSpec.DataSource;
           //if(dtspec!=null) 
           // dtspec.TableName = "Speciality";  
            //   dtICD9.Clone();  
            AddAppConfData(dtResource);
            AddAppConfData(dtFollowup);
            AddAppConfData(dtProblem);
            AddAppConfData(dtDept);
            AddAppConfData(dtApptStat);
            AddAppConfData(dtApptType);
            AddAppConfData(dtApptBlk);
            AddAppConfData(dtLoc);
        }


        private bool UploadShareAppConfToClinicRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string AppConfLocal = clsGeneral.gstrappconfflnm + "Local";
            string AppConfLocalSRV = clsGeneral.gstrappconfflnm;//"SmartDxAssociation";
            string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder + "/" + clsGeneral.gstrappconfflnm   + ".xml";

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
                                objclsAppconf.CompareXMlData(dtAppconfData, dt, strName);
                            }
                        }


                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml"; //SmartDxAssociationLocal + ".xml";

                    //Upload Xml to SharePoint
                    string webpath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.gstrClinicName + "/" + clsGeneral.ClinicXmlFolder;
                    string MainPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/";
                    string webSite = clsGeneral.gstrClinicName;
                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "User", FileNmLocal, clsGeneral.gstrClinicName, AppConfLocal, AppConfLocalSRV);// SmartDxAssociationLocal, SmartDxAssociationSRV);

                }
                else
                {
                    this.Cursor = Cursors.Default;
                    return isUploaded = false;
                }

            }
            catch //(Exception ex) 
            { 
                //MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
        }


        private bool UploadShareAppConfToGlobalRepository()
        {
            this.Cursor = Cursors.WaitCursor;
            bool isUploaded = true;
            clsgloCommunity objgloCommunity = new clsgloCommunity();
            string AppConfLocal = clsGeneral.gstrappconfflnm  + "Local";
            string AppConfLocalSRV = clsGeneral.gstrappconfflnm;//"SmartDxAssociation";
        
            string DownloadPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder + "/" + clsGeneral.gstrClinicName + " " + clsGeneral.WebFolder + "/" + clsGeneral.gstrappconfflnm  + ".xml";   //SmartDxAssociationSRV + ".xml";
            try
            {
                string FileNmLocal = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml";
                string FileNMSP = "";

                string strName = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal + ".xml"; //"SmartDxAssociation.xml";
                dtAppconfData.TableName = "AppConf";  
                dtAppconfData.WriteXml(strName);



                //  if (CreateXML(SmartDxAssociationLocal + ".xml") == true)
                // {

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
                                objclsAppconf.CompareXMlData(dtAppconfData, dt, strName);
                            }
                        }

                    }
                    else
                        FileNMSP = gloSettings.FolderSettings.AppTempFolderPath + AppConfLocal+ ".xml";

                    //Upload Xml to SharePoint
                    string webpath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm + "/" + clsGeneral.WebGlobalXmlFolder;
                    string MainPath = "http://" + clsGeneral.gstrSharepointSrvNm + "/" + clsGeneral.gstrSharepointSiteNm;
                    string webSite = clsGeneral.gstrClinicName;

                    bool IsXmlUploaded = objgloCommunity.UploadXMLFileToSP(webpath, FileNmLocal, MainPath, webSite, "Global", FileNmLocal, clsGeneral.gstrClinicName, AppConfLocal, AppConfLocalSRV);

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
            {// MessageBox.Show(ex.Message); 
            }
            finally { objgloCommunity = null; }

            this.Cursor = Cursors.Default;
            return isUploaded;
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
        
        private void btnglobal_Click(object sender, EventArgs e)
        {

            try
            {
                getdataforAppConfuploading();

                UploadShareAppConfToGlobalRepository();
            }

            catch
            {

            }
                
            
            //try
            //{
            //    DataSet objds = new DataSet();
            //    dtBlconfData.Rows.Clear();
            //    DataTable dtfollowup = (DataTable)objuctm.flxfollowup.DataSource;
            //    dtBlconfData = dtfollowup.Clone();
            //    AddData(dtfollowup);
            //    DataTable dtpri = (DataTable)objuctm.flxpritype.DataSource;
            //    if (dtpri != null)
            //        AddData(dtpri);
            //    DataTable dtstat = (DataTable)objuctm.flxstattype.DataSource;
            //    if (dtstat != null)
            //        AddData(dtstat);
            //    UploadShareTaskMailToGlobalRepository();
            //}
            //catch
            //{
            
            //}
          }

        private void btndownload_Click(object sender, EventArgs e)
        {
          
         
         //   bool DataInserted = false; 
            try
            {
              dtResource = null;
              dtFollowup = null;
              dtProblem = null;
              dtDept = null;
              dtApptStat = null;
              dtApptType = null;
              dtApptBlk = null;
              dtLoc = null;

              dtResource = (DataTable)objUCAppConf.flxRes.DataSource;
              dtFollowup = (DataTable)objUCAppConf.flxfollup.DataSource;
              dtProblem = (DataTable)objUCAppConf.flxPrb.DataSource;
              dtDept = (DataTable)objUCAppConf.flxDept.DataSource;
              dtApptStat = (DataTable)objUCAppConf.flxApptstat.DataSource;
              dtApptType = (DataTable)objUCAppConf.flxAppt.DataSource;
              dtApptBlk = (DataTable)objUCAppConf.flxApptblk.DataSource;
              dtLoc = (DataTable)objUCAppConf.flxLoc.DataSource;
        
                 //dtCPT  = (DataTable)objUCAppConf.flxCPT.DataSource;
                 //dtMod = (DataTable)objUCAppConf.flxMod.DataSource;
                 //dtPatr = (DataTable)objUCAppConf.flxPatr.DataSource;
                 //dtpln = (DataTable)objUCAppConf.flxPln.DataSource;
                 //dtspec = (DataTable)objUCAppConf.flxSpec.DataSource;
                 //dtCat= (DataTable)objUCAppConf.flxCat.DataSource;


//              if (dtResource != null)
//               {


//                   DataRow[] drRes = dtResource.Select("Select='true'");
//                   for (int lendr = 0; lendr < drCat.Length; lendr++)
//                   {
//                      // objAppconf.InsertCategory(drCat[lendr]["Description"].ToString(), drCat[lendr]["Category Type"].ToString(),clsGeneral.gClinicID ); 
//                   }


//               }


//              if (dtFollowup != null)
//               {


//                   DataRow[] drfoll = dtFollowup.Select("Select='true'");
//                   for (int lendr = 0; lendr < drfoll.Length; lendr++)
//                   {
//                       string msg = "";// objAppconf.InsertSpeciality(drspec[lendr]["Classification"].ToString(), drspec[lendr]["Taxonomy Code"].ToString(), drspec[lendr]["Taxonomy Description"].ToString(), drspec[lendr]["Code"].ToString(), drspec[lendr]["Description"].ToString(),clsGeneral.gClinicID,false);
//                    if (msg.Trim().Length == 0)
//                     {
//                     DataInserted = true;  
//                     }
//                     else
//                     {
//                     MessageBox.Show(msg.ToString() + " For Code " + drfoll[lendr]["Code"].ToString()); 
//                    }
                   
//                  }


//               }
             

//              if (dtpln != null)
//               {

//                   DataRow[] drpln = dtpln.Select("Select='true'");
//                   for (int lendr = 0; lendr < drpln.Length; lendr++)
//                   {
//                       //objAppconf.InsertPlan(drpln[lendr]["Type Description"].ToString() , drpln[lendr]["Type Code"].ToString() , clsGeneral.gClinicID, false);   
//                   }


//               }
             



//               if (dtPatr!= null)
//               {


//                   DataRow[] drPatr = dtPatr.Select("Select='true'");
//                   for (int lendr = 0; lendr < drPatr.Length; lendr++)
//                   {
                     
//                    //   objAppconf.InsertPatientRelationShip(drPatr[lendr]["Description"].ToString(), drPatr[lendr]["Code"].ToString(), false, clsGeneral.gClinicID);   
//                   }


//               }
             




//               if (dtMod != null)
//               {


//                   DataRow[] drMod = dtMod.Select("Select='true'");
//                   for (int lendr = 0; lendr < drMod.Length; lendr++)
//                   {
                      
//                   //    objAppconf.InsertModifier(drMod[lendr]["Code"].ToString(), drMod[lendr]["Description"].ToString(), clsGeneral.gClinicID);   
              
//                   }


//               }
             
                
                
//                if (dtICD9  != null)
//                {

                   
//                        DataRow[] drICD9 = dtICD9.Select("Select='true'");
//                        for (int lendr = 0; lendr < drICD9.Length; lendr++)
//                        {
//                        bool bln=false;
//                            if( drICD9[lendr]["Status"].ToString().ToLower() =="inactive")
//                                bln=true; 
//                            DataRow []drrspec = dtspec.Select("Description='"+drICD9[lendr]["Speciality"].ToString()+"'");
//                            if (drrspec.Length > 0)
//                            {

//                           //     objAppconf.InsertICD9(drICD9[lendr]["Code"].ToString(), drICD9[lendr]["Description"].ToString(), drICD9[lendr]["Speciality"].ToString(), clsGeneral.gClinicID, bln, drrspec[0]);
//                            }
//                            else
//                            {
//                            //    objAppconf.InsertICD9(drICD9[lendr]["Code"].ToString(), drICD9[lendr]["Description"].ToString(), drICD9[lendr]["Speciality"].ToString(), clsGeneral.gClinicID, bln,null);
                  
//                            }
//                        }
                 
                
//                }



//                if (dtCPT != null)
//                {


//                    DataRow[] drCPT = dtCPT.Select("Select='true'");
//                    for (int lendr = 0; lendr < drCPT.Length; lendr++)
//                    {
////  objblconf.InsertCPT(0, drCPT[lendr]["CPTCode"].ToString(), drCPT[lendr]["Description"].ToString(), SpecialityCode,drCPT[lendr]["Category Type"].ToString(), Categorydesc, CodeTypeCode, CodeTypeDesc, drCPT[lendr]["Modifier1 Code"].ToString(), drCPT[lendr]["Modifier1 Desc"].ToString(), drCPT[lendr]["Modifier2 Code"].ToString(), drCPT[lendr]["Modifier2 Desc"].ToString(), drCPT[lendr]["Modifier3 Code"].ToString(), drCPT[lendr]["Modifier3 Desc"].ToString(), drCPT[lendr]["Modifier4 Code"].ToString(), drCPT[lendr]["Modifier4 Desc"].ToString(), Units, IsCPTDrug, NDCCode, IsTaxable, Rate, Charges, Allowed, IsUseFromFeeSchedule, ClinicFee, Inactive, clsGeneral.gClinicID, StatementDesc, RevenueCode);                        
//                    }


//                }
             
            
       }




          
            
           
            catch
            {
            
            }




        }
    }
}
