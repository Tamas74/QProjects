using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using gloCCDSchema;
namespace gloSurescriptSecureMessage
{
    public partial class frmCCDA : Form
    {
        long PatientID = 0;
        public frmCCDA()
        {
            InitializeComponent();
        }

        public frmCCDA(long _PatientID)
        {
            InitializeComponent();
            PatientID = _PatientID;
        }

        private string _path = string.Empty;
        private string _FromDate = string.Empty;

        private string _ToDate = string.Empty;
        public string Path
        {
            get { return _path; }
            set { _path = value; }
        }


       

        private void btnPtCCD_Click(object sender, EventArgs e)
        {
            this.Path =  GenerateCDA();

        }

        private string GenerateCDA()
        {
            gloCCDLibrary.gloCDADataExtraction oCDADataExtraction = null;
            string strFilePath = String.Empty;

            try
            {
                if (chkDateRange.Checked == true)
                {
                    _FromDate = dtFromPtCCD.Text;
                    _ToDate = dtToPtCCD.Text;
                   
                }
                else
                {
                    _FromDate = null;
                    _ToDate = null;
                    
                }
                


                if (gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath != "")
                {
                    string strCCDFilePath = gloCCDLibrary.gloLibCCDGeneral.CCDFileGenerationPath;
                    if (Directory.Exists(strCCDFilePath) == true)
                    {
                        gloCDAWriterParameters objCDAWriterParameters = new gloCDAWriterParameters();

                        objCDAWriterParameters.CDAFileType = CDAFileTypeEnum.CareRecordSummary;
                        objCDAWriterParameters.Demographics = true;
                        objCDAWriterParameters.SmokingStatus = true;
                        objCDAWriterParameters.Problems = true;
                        objCDAWriterParameters.Allergies = true;
                        objCDAWriterParameters.CareTeamMember = true;
                        objCDAWriterParameters.Procedures = true;
                        objCDAWriterParameters.CarePlan_GoalsAndInstructions = true;
                        objCDAWriterParameters.VitalSigns = true;
                        objCDAWriterParameters.LaboratoryResult = true;
                        objCDAWriterParameters.LaboratoryTest = true;
                        objCDAWriterParameters.Medications = true;
                        objCDAWriterParameters.EncounterDiagnoses = true;
                        objCDAWriterParameters.Immunizations = true;
                        objCDAWriterParameters.CognitiveStatus = true;
                        objCDAWriterParameters.ReasonForReferral = true;
                        objCDAWriterParameters.ReferringProvider = true;
                        objCDAWriterParameters.FunctionalStatus = true;

                        oCDADataExtraction = new gloCCDLibrary.gloCDADataExtraction();

                        strFilePath = oCDADataExtraction.GenerateClinicalInformation(PatientID, SecureMessageProperties.UserID, objCDAWriterParameters, 0, _FromDate, _ToDate, "");

                        oCDADataExtraction.SaveExportedCDA(PatientID, strFilePath, "CCDA send using secure Message", "", false, false, DateTime.Now.ToString(), "CDA", CDAFileTypeEnum.CareRecordSummary.GetHashCode(), 0, false);
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.SecureMessage, gloAuditTrail.ActivityCategory.GenerateCCD, gloAuditTrail.ActivityType.Export, "CDA file generated and attached to DIRECT message.", PatientID, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        this.Close();
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("Invalid CCDA file path. Set a valid CCD path from gloEMR Admin.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }

                }
                else
                {
                    System.Windows.MessageBox.Show("Please set valid CCDA file path from gloEMR Admin.", gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }


            }
            catch (Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message, gloSurescriptSecureMessage.SecureMessageProperties.MessageBoxCaption, System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
            }
            finally
            {
                if (oCDADataExtraction != null)
                {
                    oCDADataExtraction.Dispose();
                    oCDADataExtraction = null;
                }



            }




            return strFilePath;
        }



    }
}
