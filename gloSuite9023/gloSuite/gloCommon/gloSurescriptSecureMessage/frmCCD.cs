using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace gloSurescriptSecureMessage
{
    public partial class frmCCD : Form
    {
        long PatientID = 0;
        public frmCCD()
        {
            InitializeComponent();
        }

        public frmCCD(long _PatientID)
        {
            InitializeComponent();
            PatientID = _PatientID;
        }

        private string _path = string.Empty;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }


        private void btnExamCCD_Click(object sender, EventArgs e)
        {
            string strCCDSection = string.Empty;
            string strFilePath = string.Empty;
            string strCCDfilePath = string.Empty;
            gloCCDLibrary.gloCCDInterface ogloInterface = null;

            try
            {
                ogloInterface = new gloCCDLibrary.gloCCDInterface();

                if (dtExamCCD.Value.ToString() == "")
                {
                    System.Windows.MessageBox.Show("Please select date.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    return;
                }

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                strCCDSection = CheckVisitCCDSection();
                strCCDfilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath.ToString();

                if (Directory.Exists(strCCDfilePath))
                {
                    strCCDfilePath = strCCDfilePath + "Visit Summary " + dtExamCCD.Value.ToString("MM-dd-yyyy") + ".xml";
                    strFilePath = ogloInterface.GenerateClinicalInformation(PatientID, 1, strCCDSection, 0, dtExamCCD.Value.ToString("MM-dd-yyyy"),dtExamCCD.Value.ToString("MM-dd-yyyy"), strCCDfilePath);
                    
                    //'inserting record in CCD_Exported_Files table 
                    ogloInterface.SaveExportedCCD(PatientID, strCCDfilePath, "CCD File Saved", "", false);
                    _path = strFilePath;
                    this.Close();
                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
            }
            finally
            {
                if (ogloInterface != null)
                {
                    ogloInterface.Dispose();
                    ogloInterface = null;
                }
                strCCDSection = string.Empty;
                strFilePath = string.Empty;
                strCCDfilePath = string.Empty;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
            }

        }

        private void btnPtCCD_Click(object sender, EventArgs e)
        {            
            string strCCDSection = string.Empty;
            string strFilePath = string.Empty;
            string strCCDfilePath = string.Empty;
            gloCCDLibrary.gloCCDInterface ogloInterface = null;

            try
            {
                ogloInterface = new gloCCDLibrary.gloCCDInterface();
                if (dtFromPtCCD.Value.ToString() == "" && dtToPtCCD.Value.ToString() == "")
                {
                    System.Windows.MessageBox.Show("Please select From and To date.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    return;
                }

                if (dtFromPtCCD.Value.ToString() == "")
                {
                    System.Windows.MessageBox.Show("Please select from and To date.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    return;
                }

                if (dtFromPtCCD.Value.ToString() == "")
                {
                    System.Windows.MessageBox.Show("Please select to date.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Warning);
                    return;
                }

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
                strCCDSection = CheckPatientCCDSections();
                strCCDfilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath.ToString();

                if (Directory.Exists(strCCDfilePath))
                {
                    strCCDfilePath = strCCDfilePath + "CCD " + dtFromPtCCD.Value.ToString("MM-dd-yyyy") + " To " + dtToPtCCD.Value.ToString("MM-dd-yyyy") + ".xml";
                    strFilePath = ogloInterface.GenerateClinicalInformation(PatientID, 1, strCCDSection, 0, dtFromPtCCD.Value.ToString("MM-dd-yyyy"), dtToPtCCD.Value.ToString("MM-dd-yyyy"), strCCDfilePath);
                 
                    //'inserting record in CCD_Exported_Files table 
                    ogloInterface.SaveExportedCCD(PatientID, strCCDfilePath, "CCD File Saved", "", chkPerPtRequest.Checked);
                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Export, "CCD file generated and attached to DIRECT message. ", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    _path = strFilePath;
                    this.Close();

                }
                else
                {
                    System.Windows.MessageBox.Show("Invalid CCD file path. Set a valid CCD path from gloEMR Admin.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
            }
            finally
            {

                if (ogloInterface != null)
                {
                    ogloInterface.Dispose();
                    ogloInterface = null;
                }

                strCCDSection = string.Empty;
                strFilePath = string.Empty;
                strCCDfilePath = string.Empty;

                System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default;
             

            } 
            
            
            
          
            
           

          
        }

        private string CheckPatientCCDSections()
        {
          gloSettings.GeneralSettings oSettings=null; 
          string[] DefaultFullCCD=null;
          Int16 i=0;
          string ccdStr = string.Empty;
          DataTable dt =null;
          string switchValue=string.Empty;
         try
         {
            
            oSettings = new gloSettings.GeneralSettings(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
           // dt= new DataTable();
            dt = oSettings.GetSetting("FULLCCDDEFAULTSECTIONS");
            if (dt!=null)
            {
                if (dt.Rows.Count > 0 )
                {
                    if (Convert.ToString(dt.Rows[0]["sSettingsValue"]) != "")
                    {
                        DefaultFullCCD = Convert.ToString(dt.Rows[0]["sSettingsValue"]).Trim().Split(',');
                        
                        if (DefaultFullCCD.Length > 0)
                        {
                             for (i = 0;i<=DefaultFullCCD.Length- 1;i++)
                                {
                                  switchValue= DefaultFullCCD[i];  
                                  switch(switchValue)
                                       {
                                        case "Fullvitals":
                                              ccdStr = String.Concat(ccdStr, "Vitals");
                                              break; 
                                        case "FullFamHis":
                                              ccdStr = String.Concat(ccdStr, "FamilyHistory");
                                              break; 
                                        case "FullAdDir":
                                              ccdStr = String.Concat(ccdStr, "AdvanceDirectives");
                                              break; 
                                        case "FullLabs":
                                              ccdStr = String.Concat(ccdStr, "Results");
                                              break;
                                        case "FullImmu":
                                              ccdStr = String.Concat(ccdStr, "Immunization");
                                              break;
                                        case "FullProc":
                                              ccdStr = String.Concat(ccdStr, "Procedures");
                                              break;                                     
                                        case "FullMed":
                                              ccdStr = String.Concat(ccdStr, "Medications");
                                              break;
                                        case "Fullencounter":
                                              ccdStr = String.Concat(ccdStr, "Encounter");
                                              break;  
                                        case "FullSocHis":
                                              ccdStr = String.Concat(ccdStr, "SocialHistory");
                                              break;
                                        case "FullAllergy":
                                              ccdStr = String.Concat(ccdStr, "Allergy");
                                              break;
                                        case "FullProb":
                                              ccdStr = String.Concat(ccdStr, "Problems");
                                              break;
                                       }
                                }
                        }
                    }
                }
            }
                       
               
              return ccdStr;
        }
         catch(Exception ex)
         {
            System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
            return ccdStr;
         }
         finally
         {
            if (dt!=null)
            {
                dt.Dispose();
                dt = null;
            }
            ccdStr = string.Empty;
            DefaultFullCCD = null;
           
             if (oSettings!=null)
            {
                oSettings.Dispose();
                oSettings = null;
            }           
         }
    
        }

        private string CheckVisitCCDSection()
        {
            gloSettings.GeneralSettings oSettings = null;
            string[] DefaultFullCCD = null;
            Int16 i = 0;
            string ccdStr = string.Empty;
            DataTable dt = null;
            string switchValue = string.Empty;
            try
            {

                oSettings = new gloSettings.GeneralSettings(gloSurescriptSecureMessage.SecureMessageProperties.DatabaseConnectionString);
               // dt = new DataTable();
                dt = oSettings.GetSetting("VISITCCDDEFAULTSECTIONS");
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["sSettingsValue"].ToString() != "")
                        {
                            DefaultFullCCD = Convert.ToString(dt.Rows[0]["sSettingsValue"]).Trim().Split(',');

                            if (DefaultFullCCD.Length > 0)
                            {
                                for (i = 0; i <= DefaultFullCCD.Length - 1; i++)
                                {
                                    switchValue = DefaultFullCCD[i];
                                    switch (switchValue)
                                    {
                                        case "VisitVitals":
                                            ccdStr = String.Concat(ccdStr, "Vitals");
                                            break;
                                        case "VisitFamHis":
                                            ccdStr = String.Concat(ccdStr, "FamilyHistory");
                                            break;
                                        case "VisitAdDir":
                                            ccdStr = String.Concat(ccdStr, "AdvanceDirectives");
                                            break;
                                        case "VisitLabs":
                                            ccdStr = String.Concat(ccdStr, "Results");
                                            break;
                                        case "visitImmu":
                                            ccdStr = String.Concat(ccdStr, "Immunization");
                                            break;
                                        case "VisitProc":
                                            ccdStr = String.Concat(ccdStr, "Procedures");
                                            break;
                                        case "VisitMed":
                                            ccdStr = String.Concat(ccdStr, "Medications");
                                            break;
                                        case "VisitEncounter":
                                            ccdStr = String.Concat(ccdStr, "Encounter");
                                            break;
                                        case "VisitSocHis":
                                            ccdStr = String.Concat(ccdStr, "SocialHistory");
                                            break;
                                        case "VisitAllegy":
                                            ccdStr = String.Concat(ccdStr, "Allergy");
                                            break;
                                        case "VisitProb":
                                            ccdStr = String.Concat(ccdStr, "Problems");
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }


                return ccdStr;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption);
                return ccdStr;
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                ccdStr = string.Empty;
                DefaultFullCCD = null;

                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }

        }

    }
}
