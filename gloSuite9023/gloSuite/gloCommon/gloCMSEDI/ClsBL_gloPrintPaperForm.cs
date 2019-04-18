using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace gloCMSEDI
{
    class gloPrintPaperForm
    {
        #region "Constructor & Destructor"

        public gloPrintPaperForm()
        {
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }
            disposed = true;
        }

        ~gloPrintPaperForm()
        {
            Dispose(false);
        }

        #endregion

        Bitmap oSourceHCFA1500 = null;
        Graphics oGraphics = null;
        Font arialRegular = null;
        Font arialBold = null;

        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForClaims && gloGlobal.gloTSPrint.isCopyPrint;

        private gloHCFA1500PaperForm _oHCFA1500Form=null;
        private Boolean _PrintOnForm = false;
        float arialFontSmallHeight = 8.5f;
        float arialFontBigHeight = 24.0f;
        private void AdjustFontHeight()
        {
            float _arialFontSmall = arialRegular.GetHeight(oGraphics);
            float _arialFontBig = arialBold.GetHeight(oGraphics);
            arialRegular = new Font(arialRegular.FontFamily, arialRegular.Size * arialFontSmallHeight / _arialFontSmall, arialRegular.Style);
            arialBold = new Font(arialBold.FontFamily, arialBold.Size * arialFontBigHeight / _arialFontBig, arialBold.Style);
        }
        private void GetFontHeight()
        {
            using (oGraphics = Graphics.FromImage(oSourceHCFA1500))
            {
                arialFontSmallHeight = arialRegular.GetHeight(oGraphics);
                arialFontBigHeight = arialBold.GetHeight(oGraphics);
            }
        }

        private int CreateEMFHCFA1500Form(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                oGraphics = thisGraphics;
                AdjustFontHeight();
                #region "Write Respective Data on Image"
                // Write Data Image region shifted to following Method, 
                // Passing PrintOnForm will indicate whether to write data for Pring Form / Printing Data
                if (_PrintOnForm)
                {
                    oGraphics.DrawImage(oSourceHCFA1500, new RectangleF(0, 0, bmpWidth, bmpHeight));
                }
                else
                {
                    oGraphics.Clear(Color.White);
                }
                WriteRespectiveData(_oHCFA1500Form, _PrintOnForm);
                #endregion

                return 0;
            }
            catch
            {
                return 1;
            }

        }
        private int CreateEMFHCFA1500WriteForm(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                oGraphics = thisGraphics;
                AdjustFontHeight();
                #region "Write Respective Data on Image"
                // Write Data Image region shifted to following Method, 
                if (_PrintOnForm)
                {
                    oGraphics.DrawImage(oSourceHCFA1500, new RectangleF(0, 0, bmpWidth, bmpHeight));
                }
                else
                {
                    oGraphics.Clear(Color.White);
                }

                // Passing PrintOnForm will indicate whether to write data for Pring Form / Printing Data
                WriteDataToForm(_oHCFA1500Form);
                #endregion

                return 0;
            }
            catch
            {
                return 1;
            }

        }
        private int CreateEMFHCFA1500PaperForm(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                oGraphics = thisGraphics;
                AdjustFontHeight();
                #region "Write Respective Data on Image"
                // Write Data Image region shifted to following Method, 
                if (_PrintOnForm)
                {
                    oGraphics.DrawImage(oSourceHCFA1500, new RectangleF(0, 0, bmpWidth, bmpHeight));
                }
                else
                {
                    oGraphics.Clear(Color.White);
                }

                // Passing PrintOnForm will indicate whether to write data for Pring Form / Printing Data
                PrintDataToPaperForm(_oHCFA1500Form);
                #endregion

                return 0;
            }
            catch
            {
                return 1;
            }

        }
        public string PrintHCFA1500Form(gloHCFA1500PaperForm oHCFA1500Form, string SourceFilePath, Boolean PrintOnForm)
        {
            string _result = "";
            string _printFilePath = "";
            string sPath = "";
            _oHCFA1500Form = oHCFA1500Form;
            _PrintOnForm = PrintOnForm;
            try
            {
                sPath = gloSettings.FolderSettings.AppTempFolderPath + "Paper_Claim_Files";

                if (System.IO.Directory.Exists(sPath) == false)
                {
                    System.IO.Directory.CreateDirectory(sPath);
                }
                #region //Delete All Files

                //DirectoryInfo dirInfo = new DirectoryInfo(oFileInfo.DirectoryName + "\\Paper_Claim_Files");
                //FileInfo[] oFile = dirInfo.GetFiles("*.*");
                //if (oFile.Length > 0)
                //{
                //    foreach (FileInfo curFile in oFile)
                //    {
                //        curFile.Delete();
                //    }
                //}

                #endregion

                if (PrintOnForm == true)
                    _printFilePath = sPath + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + (toCreateEMF ? ".emf" : ".tif");
                else
                    _printFilePath = sPath + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + "_BLANK" + (toCreateEMF ? ".emf" : ".tif");

                if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }
                arialRegular = new Font("Arial", 8.25F);
                arialBold = new Font("Arial", 8.25F, FontStyle.Bold);
                if (PrintOnForm == true)
                    oSourceHCFA1500 = new Bitmap(Application.StartupPath.ToString() + "\\CMS1500_NEW.tif");
                else
                    oSourceHCFA1500 = new Bitmap(Application.StartupPath.ToString() + "\\CMS1500_BLANK.tif");

                if (toCreateEMF)
                {
                    GetFontHeight();
                    byte[] emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)oSourceHCFA1500.Width / oSourceHCFA1500.HorizontalResolution, (float)oSourceHCFA1500.Height / oSourceHCFA1500.VerticalResolution, oSourceHCFA1500.Width, oSourceHCFA1500.Height, CreateEMFHCFA1500Form);
                    File.WriteAllBytes(_printFilePath, emfBytes);
                }
                else
                {
                    oGraphics = Graphics.FromImage(oSourceHCFA1500);
                    #region "Write Respective Data on Image"
                    // Write Data Image region shifted to following Method, 
                    // Passing PrintOnForm will indicate whether to write data for Pring Form / Printing Data
                    WriteRespectiveData(oHCFA1500Form, PrintOnForm);
                    #endregion

                    oSourceHCFA1500.Save(_printFilePath);

                    if (oGraphics != null) { oGraphics.Dispose(); oGraphics = null; }
                }
                if (oSourceHCFA1500 != null) { oSourceHCFA1500.Dispose(); oSourceHCFA1500 = null; }

                if (arialRegular != null) { arialRegular.Dispose(); arialRegular = null; }
                if (arialBold != null) { arialBold.Dispose(); arialBold = null; }

                _result = _printFilePath;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
            }
            return _result;
        }

        public string PrintHCFA1500Form(gloHCFA1500PaperForm oHCFA1500Form, string SourceFilePath, string FilePathName, Boolean PrintOnForm)
        {
            string _result = "";
            string _printFilePath = "";
            _oHCFA1500Form = oHCFA1500Form;
            _PrintOnForm = PrintOnForm;
            try
            {
                if (File.Exists(SourceFilePath) == true)
                {
                    FileInfo oFileInfo = new FileInfo(SourceFilePath);

                    if (PrintOnForm == true)
                        _printFilePath = oFileInfo.DirectoryName + "\\" + FilePathName +   (toCreateEMF ? ".emf" : ".tif");
                    else
                        _printFilePath = oFileInfo.DirectoryName + "\\" + FilePathName.Replace(".tif",   "_BLANK"+(toCreateEMF ? ".emf" : ".tif"));

                    if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }

                    oFileInfo = null;


                    if (PrintOnForm == true)
                        oSourceHCFA1500 = new Bitmap(SourceFilePath);
                    else
                        oSourceHCFA1500 = new Bitmap(817 * 2, 1057 * 2);
                    arialRegular = new Font("Arial", 8.25F);
                    arialBold = new Font("Arial", 8.25F, FontStyle.Bold);

                    if (toCreateEMF)
                    {
                        GetFontHeight();
                        byte[] emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)oSourceHCFA1500.Width / oSourceHCFA1500.HorizontalResolution, (float)oSourceHCFA1500.Height / oSourceHCFA1500.VerticalResolution, oSourceHCFA1500.Width, oSourceHCFA1500.Height, CreateEMFHCFA1500WriteForm);
                        File.WriteAllBytes(_printFilePath, emfBytes);
                    }
                    else
                    {
                        oGraphics = Graphics.FromImage(oSourceHCFA1500);
                        WriteDataToForm(oHCFA1500Form);

                        oSourceHCFA1500.Save(_printFilePath);

                        if (oGraphics != null) { oGraphics.Dispose(); oGraphics = null; }
                    }
                    if (oSourceHCFA1500 != null) { oSourceHCFA1500.Dispose(); oSourceHCFA1500 = null; }
                    if (arialRegular != null) { arialRegular.Dispose(); arialRegular = null; }
                    if (arialBold != null) { arialBold.Dispose(); arialBold = null; }

                    _result = _printFilePath;
                }



            }
            catch (Exception ex)
            {
                //Added on 20091205-Mayuri
               gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
            }
            return _result;
        }

        private void WriteDataToForm(gloHCFA1500PaperForm oHCFA1500Form)
        {
            #region "Write Data on Image"

            //.Insuracne Header on Claim Form
            WriteData(oHCFA1500Form.CF_Top_InsuranceHeader.Value, oHCFA1500Form.CF_Top_InsuranceHeader.Location, false);

            //.Insurance Type"
            #region "Print Insurance Type"
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Location, true);
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Location, true);
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Location, true);
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Champva.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Champva.Location, true);
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Location, true);
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_FECA.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_FECA.Location, true);
            WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Other.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Other.Location, true);
            #endregion

            //.Insured's ID Number
            WriteData(oHCFA1500Form.CF_1a_InsuredsIDNumber.Value, oHCFA1500Form.CF_1a_InsuredsIDNumber.Location, false);

            //.Patient's Name
            WriteData(oHCFA1500Form.CF_2_Patient_Name.Value, oHCFA1500Form.CF_2_Patient_Name.Location, false);

            //.Patient's Birth Date
            WriteData(oHCFA1500Form.CF_3_Patient_DOB_MM.Value, oHCFA1500Form.CF_3_Patient_DOB_MM.Location, false);
            WriteData(oHCFA1500Form.CF_3_Patient_DOB_DD.Value, oHCFA1500Form.CF_3_Patient_DOB_DD.Location, false);
            WriteData(oHCFA1500Form.CF_3_Patient_DOB_YY.Value, oHCFA1500Form.CF_3_Patient_DOB_YY.Location, false);

            //.Patient's Sex
            WriteData(oHCFA1500Form.CF_3_Patient_Sex_Male.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Male.Location, true);
            WriteData(oHCFA1500Form.CF_3_Patient_Sex_Female.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Female.Location, true);

            //.Insured's Name
            WriteData(oHCFA1500Form.CF_4_Insureds_Name.Value, oHCFA1500Form.CF_4_Insureds_Name.Location, false);

            //.Patient's Address
            WriteData(oHCFA1500Form.CF_5_Patient_Address.Value, oHCFA1500Form.CF_5_Patient_Address.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_City.Value, oHCFA1500Form.CF_5_Patient_City.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_State.Value, oHCFA1500Form.CF_5_Patient_State.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_Zip.Value, oHCFA1500Form.CF_5_Patient_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Value, oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_Tel_Number.Value, oHCFA1500Form.CF_5_Patient_Tel_Number.Location, false);

            //.Paient Relationship to Insured
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Self.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Self.Location, true);
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Spouse.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Spouse.Location, true);
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Child.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Child.Location, true);
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Other.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Other.Location, true);

            //.Insured's Address
            WriteData(oHCFA1500Form.CF_7_Insureds_Address.Value, oHCFA1500Form.CF_7_Insureds_Address.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_City.Value, oHCFA1500Form.CF_7_Insureds_City.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_State.Value, oHCFA1500Form.CF_7_Insureds_State.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_Zip.Value, oHCFA1500Form.CF_7_Insureds_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Value, oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_Tel_Number.Value, oHCFA1500Form.CF_7_Insureds_Tel_Number.Location, false);

            //.Patient Status
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Single.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Single.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Married.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Married.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Other.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Other.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Employed.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Employed.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Location, true);

            //.Other Insured's Name
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_Name.Value, oHCFA1500Form.CF_9_Other_Insureds_Name.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Location, true);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Location, true);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Value, oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Location, false);

            //.Patient Condition 
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Value, oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Location, false);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Value, oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Location, false);

            //.Insured's Information
            WriteData(oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_DOB_MM.Value, oHCFA1500Form.CF_11_Insureds_DOB_MM.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_DOB_DD.Value, oHCFA1500Form.CF_11_Insureds_DOB_DD.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_DOB_YY.Value, oHCFA1500Form.CF_11_Insureds_DOB_YY.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Male.Location, true);
            WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Female.Location, true);
            WriteData(oHCFA1500Form.CF_11_Insureds_EmployerName.Value, oHCFA1500Form.CF_11_Insureds_EmployerName.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Location, true);

            WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Location, false);
            WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Location, false);

            WriteData(oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Location, false);

            WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Location, false);
            WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Location, false);
            WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Location, false);

            WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Location, false);
            WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Location, false);
            WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Location, false);

            WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_17_ReferringProvider_Name.Value, oHCFA1500Form.CF_17_ReferringProvider_Name.Location, false);
            WriteData(oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Location, false);
            WriteData(oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Location, false);
            WriteData(oHCFA1500Form.CF_17b_ReferringProvider_NPI.Value, oHCFA1500Form.CF_17b_ReferringProvider_NPI.Location, false);

            WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_19_LocalUse_Field.Value, oHCFA1500Form.CF_19_LocalUse_Field.Location, false);

            WriteData(oHCFA1500Form.CF_20_OutsideLab_Yes.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_20_OutsideLab_No.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_No.Location, true);
            WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_1_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_2_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_3_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_4_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_22_MecaidResubmission_Code.Value, oHCFA1500Form.CF_22_MecaidResubmission_Code.Location, false);
            WriteData(oHCFA1500Form.CF_22_Original_Refrence_No.Value, oHCFA1500Form.CF_22_Original_Refrence_No.Location, false);

            WriteData(oHCFA1500Form.CF_23_PriorAuthorization_No.Value, oHCFA1500Form.CF_23_PriorAuthorization_No.Location, false);

            #region " Service Line 1 "

            if (oHCFA1500Form.CF_IsPresent_Line1 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L1_POS_Code.Value, oHCFA1500Form.CF_24B_L1_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L1_EMG_Code.Value, oHCFA1500Form.CF_24C_L1_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L1_Charges_Principal.Value, oHCFA1500Form.CF_24F_L1_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L1_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L1_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L1_Days_Units.Value, oHCFA1500Form.CF_24G_L1_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Location, false);
            }
            #endregion

            #region " Service Line 2 "

            if (oHCFA1500Form.CF_IsPresent_Line2 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L2_POS_Code.Value, oHCFA1500Form.CF_24B_L2_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L2_EMG_Code.Value, oHCFA1500Form.CF_24C_L2_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L2_Charges_Principal.Value, oHCFA1500Form.CF_24F_L2_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L2_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L2_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L2_Days_Units.Value, oHCFA1500Form.CF_24G_L2_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Location, false);
            }
            #endregion

            #region " Service Line 3 "

            if (oHCFA1500Form.CF_IsPresent_Line3 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L3_POS_Code.Value, oHCFA1500Form.CF_24B_L3_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L3_EMG_Code.Value, oHCFA1500Form.CF_24C_L3_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L3_Charges_Principal.Value, oHCFA1500Form.CF_24F_L3_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L3_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L3_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L3_Days_Units.Value, oHCFA1500Form.CF_24G_L3_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Location, false);
            }
            #endregion

            #region " Service Line 4 "

            if (oHCFA1500Form.CF_IsPresent_Line4 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L4_POS_Code.Value, oHCFA1500Form.CF_24B_L4_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L4_EMG_Code.Value, oHCFA1500Form.CF_24C_L4_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L4_Charges_Principal.Value, oHCFA1500Form.CF_24F_L4_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L4_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L4_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L4_Days_Units.Value, oHCFA1500Form.CF_24G_L4_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Location, false);

            }
            #endregion

            #region " Service Line 5 "

            if (oHCFA1500Form.CF_IsPresent_Line5 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L5_POS_Code.Value, oHCFA1500Form.CF_24B_L5_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L5_EMG_Code.Value, oHCFA1500Form.CF_24C_L5_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L5_Charges_Principal.Value, oHCFA1500Form.CF_24F_L5_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L5_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L5_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L5_Days_Units.Value, oHCFA1500Form.CF_24G_L5_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Location, false);
            }
            #endregion

            #region " Service Line 6 "

            if (oHCFA1500Form.CF_IsPresent_Line6 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L6_POS_Code.Value, oHCFA1500Form.CF_24B_L6_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L6_EMG_Code.Value, oHCFA1500Form.CF_24C_L6_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L6_Charges_Principal.Value, oHCFA1500Form.CF_24F_L6_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L6_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L6_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L6_Days_Units.Value, oHCFA1500Form.CF_24G_L6_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Location, false);
            }
            #endregion

            WriteData(oHCFA1500Form.CF_25_FederalTax_ID_No.Value, oHCFA1500Form.CF_25_FederalTax_ID_No.Location, false);
            WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Location, true);
            WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Location, true);

            WriteData(oHCFA1500Form.CF_26_PatientAccount_No.Value, oHCFA1500Form.CF_26_PatientAccount_No.Location, false);

            WriteData(oHCFA1500Form.CF_27_AcceptAssignment_YES.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_YES.Location, true);
            WriteData(oHCFA1500Form.CF_27_AcceptAssignment_NO.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_NO.Location, true);

            WriteData(oHCFA1500Form.CF_28_TotalCharge_Principal.Value, oHCFA1500Form.CF_28_TotalCharge_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_28_TotalCharge_Secondary.Value, oHCFA1500Form.CF_28_TotalCharge_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_29_AmountPaid_Principal.Value, oHCFA1500Form.CF_29_AmountPaid_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_29_AmountPaid_Secondary.Value, oHCFA1500Form.CF_29_AmountPaid_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_30_BalanceDue_Principal.Value, oHCFA1500Form.CF_30_BalanceDue_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_30_BalanceDue_Secondary.Value, oHCFA1500Form.CF_30_BalanceDue_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature.Location, false);
            WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Location, false);
            WriteData(oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Value, oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Location, false);

            WriteData(oHCFA1500Form.CF_32_Service_Facility_Name.Value, oHCFA1500Form.CF_32_Service_Facility_Name.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_City.Value, oHCFA1500Form.CF_32_Service_Facility_City.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_State.Value, oHCFA1500Form.CF_32_Service_Facility_State.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_Zip.Value, oHCFA1500Form.CF_32_Service_Facility_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_32a_Service_Facility_NPI.Value, oHCFA1500Form.CF_32a_Service_Facility_NPI.Location, false);
            WriteData(oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Value, oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Location, false);

            WriteData(oHCFA1500Form.CF_33_BillingProvider_Name.Value, oHCFA1500Form.CF_33_BillingProvider_Name.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_City.Value, oHCFA1500Form.CF_33_BillingProvider_City.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_State.Value, oHCFA1500Form.CF_33_BillingProvider_State.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Zip.Value, oHCFA1500Form.CF_33_BillingProvider_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_33a_BillingProvider_NPI.Value, oHCFA1500Form.CF_33a_BillingProvider_NPI.Location, false);
            WriteData(oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Value, oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Location, true);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Location, false);

            #endregion
        }

        public string PrintHCFA1500Form(gloHCFA1500PaperForm oHCFA1500Form, string SourceFilePath, string DestinationFilePath)
        {
            string _result = "";
            string _printFilePath = "";
            _oHCFA1500Form = oHCFA1500Form;
            _PrintOnForm = true;
            try
            {

                if (File.Exists(SourceFilePath) == true)
                {
                    //FileInfo oFileInfo = new FileInfo(SourceFilePath);
                    //_printFilePath = oFileInfo.DirectoryName + "\\" + DateTime.Now.ToString("yyyyMMddhhmmsstt") + ".tif";
                    //if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }
                    //oFileInfo = null;

                    _printFilePath = DestinationFilePath;
                    if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }

                    if (File.Exists(SourceFilePath) == true)
                    {

                        oSourceHCFA1500 = new Bitmap(SourceFilePath);
                        oGraphics = Graphics.FromImage(oSourceHCFA1500);
                        arialRegular = new Font("Arial", 8.25F);
                        arialBold = new Font("Arial", 8.25F, FontStyle.Bold);
                        if (toCreateEMF)
                        {
                            GetFontHeight();
                            byte[] emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)oSourceHCFA1500.Width / oSourceHCFA1500.HorizontalResolution, (float)oSourceHCFA1500.Height / oSourceHCFA1500.VerticalResolution, oSourceHCFA1500.Width, oSourceHCFA1500.Height,CreateEMFHCFA1500PaperForm);
                            File.WriteAllBytes(_printFilePath, emfBytes);
                        }
                        else
                        {
                            #region "Write Data on Image"


                            PrintDataToPaperForm(oHCFA1500Form);

                            #endregion

                            oSourceHCFA1500.Save(_printFilePath);

                            if (oGraphics != null) { oGraphics.Dispose(); oGraphics = null; }
                        }
                        if (oSourceHCFA1500 != null) { oSourceHCFA1500.Dispose(); oSourceHCFA1500 = null; }
                        if (arialRegular != null) { arialRegular.Dispose(); arialRegular = null; }
                        if (arialBold != null) { arialBold.Dispose(); arialBold = null; }

                        _result = _printFilePath;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
            }
            return _result;
        }

        private void PrintDataToPaperForm(gloHCFA1500PaperForm oHCFA1500Form)
        {

            //.Insurance Type"
                        #region "Print Insurance Type"
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Location, true);
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Location, true);
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Location, true);
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Champva.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Champva.Location, true);
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Location, true);
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_FECA.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_FECA.Location, true);
                        WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Other.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Other.Location, true);
                        #endregion
            //.Insured's ID Number
            WriteData(oHCFA1500Form.CF_1a_InsuredsIDNumber.Value, oHCFA1500Form.CF_1a_InsuredsIDNumber.Location, false);

            //.Patient's Name
            WriteData(oHCFA1500Form.CF_2_Patient_Name.Value, oHCFA1500Form.CF_2_Patient_Name.Location, false);

            //.Patient's Birth Date
            WriteData(oHCFA1500Form.CF_3_Patient_DOB_MM.Value, oHCFA1500Form.CF_3_Patient_DOB_MM.Location, false);
            WriteData(oHCFA1500Form.CF_3_Patient_DOB_DD.Value, oHCFA1500Form.CF_3_Patient_DOB_DD.Location, false);
            WriteData(oHCFA1500Form.CF_3_Patient_DOB_YY.Value, oHCFA1500Form.CF_3_Patient_DOB_YY.Location, false);

            //.Patient's Sex
            WriteData(oHCFA1500Form.CF_3_Patient_Sex_Male.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Male.Location, true);
            WriteData(oHCFA1500Form.CF_3_Patient_Sex_Female.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Female.Location, true);

            //.Insured's Name
            WriteData(oHCFA1500Form.CF_4_Insureds_Name.Value, oHCFA1500Form.CF_4_Insureds_Name.Location, false);

            //.Patient's Address
            WriteData(oHCFA1500Form.CF_5_Patient_Address.Value, oHCFA1500Form.CF_5_Patient_Address.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_City.Value, oHCFA1500Form.CF_5_Patient_City.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_State.Value, oHCFA1500Form.CF_5_Patient_State.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_Zip.Value, oHCFA1500Form.CF_5_Patient_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Value, oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Location, false);
            WriteData(oHCFA1500Form.CF_5_Patient_Tel_Number.Value, oHCFA1500Form.CF_5_Patient_Tel_Number.Location, false);

            //.Paient Relationship to Insured
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Self.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Self.Location, true);
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Spouse.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Spouse.Location, true);
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Child.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Child.Location, true);
            WriteData(oHCFA1500Form.CF_6_PatientRelationship_Other.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Other.Location, true);

            //.Insured's Address
            WriteData(oHCFA1500Form.CF_7_Insureds_Address.Value, oHCFA1500Form.CF_7_Insureds_Address.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_City.Value, oHCFA1500Form.CF_7_Insureds_City.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_State.Value, oHCFA1500Form.CF_7_Insureds_State.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_Zip.Value, oHCFA1500Form.CF_7_Insureds_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Value, oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Location, false);
            WriteData(oHCFA1500Form.CF_7_Insureds_Tel_Number.Value, oHCFA1500Form.CF_7_Insureds_Tel_Number.Location, false);

            //.Patient Status
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Single.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Single.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Married.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Married.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Other.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Other.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_Employed.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Employed.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Location, true);
            WriteData(oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Location, true);

            //.Other Insured's Name
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_Name.Value, oHCFA1500Form.CF_9_Other_Insureds_Name.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Location, true);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Location, true);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Value, oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Location, false);
            WriteData(oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Location, false);

            //.Patient Condition 
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Value, oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Location, false);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Location, true);
            WriteData(oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Value, oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Location, false);

            //.Insured's Information
            WriteData(oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_DOB_MM.Value, oHCFA1500Form.CF_11_Insureds_DOB_MM.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_DOB_DD.Value, oHCFA1500Form.CF_11_Insureds_DOB_DD.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_DOB_YY.Value, oHCFA1500Form.CF_11_Insureds_DOB_YY.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Male.Location, true);
            WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Female.Location, true);
            WriteData(oHCFA1500Form.CF_11_Insureds_EmployerName.Value, oHCFA1500Form.CF_11_Insureds_EmployerName.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Location, false);
            WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Location, true);

            WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Location, false);
            WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Location, false);

            WriteData(oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Location, false);

            WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Location, false);
            WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Location, false);
            WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Location, false);

            WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Location, false);
            WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Location, false);
            WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Location, false);

            WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_17_ReferringProvider_Name.Value, oHCFA1500Form.CF_17_ReferringProvider_Name.Location, false);
            WriteData(oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Location, false);
            WriteData(oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Location, false);
            WriteData(oHCFA1500Form.CF_17b_ReferringProvider_NPI.Value, oHCFA1500Form.CF_17b_ReferringProvider_NPI.Location, false);

            WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Location, false);
            WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Location, false);

            WriteData(oHCFA1500Form.CF_19_LocalUse_Field.Value, oHCFA1500Form.CF_19_LocalUse_Field.Location, false);

            WriteData(oHCFA1500Form.CF_20_OutsideLab_Yes.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_Yes.Location, true);
            WriteData(oHCFA1500Form.CF_20_OutsideLab_No.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_No.Location, true);
            WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_1_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_2_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_3_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_4_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_22_MecaidResubmission_Code.Value, oHCFA1500Form.CF_22_MecaidResubmission_Code.Location, false);
            WriteData(oHCFA1500Form.CF_22_Original_Refrence_No.Value, oHCFA1500Form.CF_22_Original_Refrence_No.Location, false);

            WriteData(oHCFA1500Form.CF_23_PriorAuthorization_No.Value, oHCFA1500Form.CF_23_PriorAuthorization_No.Location, false);

            #region " Service Line 1 "

            if (oHCFA1500Form.CF_IsPresent_Line1 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L1_POS_Code.Value, oHCFA1500Form.CF_24B_L1_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L1_EMG_Code.Value, oHCFA1500Form.CF_24C_L1_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L1_Charges_Principal.Value, oHCFA1500Form.CF_24F_L1_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L1_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L1_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L1_Days_Units.Value, oHCFA1500Form.CF_24G_L1_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Location, false);

                //Line Note
                WriteData(oHCFA1500Form.CF_24A_L1_Note.Value, oHCFA1500Form.CF_24A_L1_Note.Location, false);
            }
            #endregion

            #region " Service Line 2 "

            if (oHCFA1500Form.CF_IsPresent_Line2 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L2_POS_Code.Value, oHCFA1500Form.CF_24B_L2_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L2_EMG_Code.Value, oHCFA1500Form.CF_24C_L2_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L2_Charges_Principal.Value, oHCFA1500Form.CF_24F_L2_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L2_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L2_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L2_Days_Units.Value, oHCFA1500Form.CF_24G_L2_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Location, false);

                //Line Note
                WriteData(oHCFA1500Form.CF_24A_L2_Note.Value, oHCFA1500Form.CF_24A_L2_Note.Location, false);
            }
            #endregion

            #region " Service Line 3 "

            if (oHCFA1500Form.CF_IsPresent_Line3 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L3_POS_Code.Value, oHCFA1500Form.CF_24B_L3_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L3_EMG_Code.Value, oHCFA1500Form.CF_24C_L3_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L3_Charges_Principal.Value, oHCFA1500Form.CF_24F_L3_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L3_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L3_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L3_Days_Units.Value, oHCFA1500Form.CF_24G_L3_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Location, false);

                //Line Note
                WriteData(oHCFA1500Form.CF_24A_L3_Note.Value, oHCFA1500Form.CF_24A_L3_Note.Location, false);
            }
            #endregion

            #region " Service Line 4 "

            if (oHCFA1500Form.CF_IsPresent_Line4 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L4_POS_Code.Value, oHCFA1500Form.CF_24B_L4_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L4_EMG_Code.Value, oHCFA1500Form.CF_24C_L4_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L4_Charges_Principal.Value, oHCFA1500Form.CF_24F_L4_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L4_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L4_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L4_Days_Units.Value, oHCFA1500Form.CF_24G_L4_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Location, false);

                //Line Note
                WriteData(oHCFA1500Form.CF_24A_L4_Note.Value, oHCFA1500Form.CF_24A_L4_Note.Location, false);

            }
            #endregion

            #region " Service Line 5 "

            if (oHCFA1500Form.CF_IsPresent_Line5 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L5_POS_Code.Value, oHCFA1500Form.CF_24B_L5_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L5_EMG_Code.Value, oHCFA1500Form.CF_24C_L5_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L5_Charges_Principal.Value, oHCFA1500Form.CF_24F_L5_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L5_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L5_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L5_Days_Units.Value, oHCFA1500Form.CF_24G_L5_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Location, false);

                //Line Note
                WriteData(oHCFA1500Form.CF_24A_L5_Note.Value, oHCFA1500Form.CF_24A_L5_Note.Location, false);

            }
            #endregion

            #region " Service Line 6 "

            if (oHCFA1500Form.CF_IsPresent_Line6 == true)
            {
                //From Date.
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_From_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_From_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_From_YY.Location, false);

                //To Date
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_To_MM.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_To_DD.Location, false);
                WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_To_YY.Location, false);

                //Place Of Service
                WriteData(oHCFA1500Form.CF_24B_L6_POS_Code.Value, oHCFA1500Form.CF_24B_L6_POS_Code.Location, false);

                //EMG - Emergency
                WriteData(oHCFA1500Form.CF_24C_L6_EMG_Code.Value, oHCFA1500Form.CF_24C_L6_EMG_Code.Location, false);

                //CPT/HCPCS 
                WriteData(oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Location, false);

                //Modifiers
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Location, false);
                WriteData(oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Location, false);

                //Diagnosis Pointers 
                WriteData(oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Location, false);

                //Charges
                WriteData(oHCFA1500Form.CF_24F_L6_Charges_Principal.Value, oHCFA1500Form.CF_24F_L6_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_24F_L6_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L6_Charges_Secondary.Location, false);

                //Days or Units
                WriteData(oHCFA1500Form.CF_24G_L6_Days_Units.Value, oHCFA1500Form.CF_24G_L6_Days_Units.Location, false);

                //EPSDT Family Plan
                WriteData(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Location, false);

                //Rendering Provider ID (NPI)
                WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Location, false);

                //Line Note
                WriteData(oHCFA1500Form.CF_24A_L6_Note.Value, oHCFA1500Form.CF_24A_L6_Note.Location, false);
            }
            #endregion

            WriteData(oHCFA1500Form.CF_25_FederalTax_ID_No.Value, oHCFA1500Form.CF_25_FederalTax_ID_No.Location, false);
            WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Location, true);
            WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Location, true);

            WriteData(oHCFA1500Form.CF_26_PatientAccount_No.Value, oHCFA1500Form.CF_26_PatientAccount_No.Location, false);

            WriteData(oHCFA1500Form.CF_27_AcceptAssignment_YES.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_YES.Location, true);
            WriteData(oHCFA1500Form.CF_27_AcceptAssignment_NO.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_NO.Location, true);

            WriteData(oHCFA1500Form.CF_28_TotalCharge_Principal.Value, oHCFA1500Form.CF_28_TotalCharge_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_28_TotalCharge_Secondary.Value, oHCFA1500Form.CF_28_TotalCharge_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_29_AmountPaid_Principal.Value, oHCFA1500Form.CF_29_AmountPaid_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_29_AmountPaid_Secondary.Value, oHCFA1500Form.CF_29_AmountPaid_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_30_BalanceDue_Principal.Value, oHCFA1500Form.CF_30_BalanceDue_Principal.Location, false);
            WriteData(oHCFA1500Form.CF_30_BalanceDue_Secondary.Value, oHCFA1500Form.CF_30_BalanceDue_Secondary.Location, false);

            WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature.Location, false);
            WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Location, false);

            WriteData(oHCFA1500Form.CF_32_Service_Facility_Name.Value, oHCFA1500Form.CF_32_Service_Facility_Name.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_City.Value, oHCFA1500Form.CF_32_Service_Facility_City.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_State.Value, oHCFA1500Form.CF_32_Service_Facility_State.Location, false);
            WriteData(oHCFA1500Form.CF_32_Service_Facility_Zip.Value, oHCFA1500Form.CF_32_Service_Facility_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_32a_Service_Facility_NPI.Value, oHCFA1500Form.CF_32a_Service_Facility_NPI.Location, false);
            WriteData(oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Value, oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Location, false);

            WriteData(oHCFA1500Form.CF_33_BillingProvider_Name.Value, oHCFA1500Form.CF_33_BillingProvider_Name.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_City.Value, oHCFA1500Form.CF_33_BillingProvider_City.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_State.Value, oHCFA1500Form.CF_33_BillingProvider_State.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Zip.Value, oHCFA1500Form.CF_33_BillingProvider_Zip.Location, false);
            WriteData(oHCFA1500Form.CF_33a_BillingProvider_NPI.Value, oHCFA1500Form.CF_33a_BillingProvider_NPI.Location, false);
            WriteData(oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Value, oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Location, true);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Location, false);
            WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Location, false);
        }

        #region "Code added by Pankaj 23122009 for PrintForm Setup"
        private void WriteRespectiveData(gloHCFA1500PaperForm oHCFA1500Form, Boolean PrintOnForm)
        {
            if (PrintOnForm)
            {
                #region "Write Data For Print Form"

                //.Insuracne Header on Claim Form
                WriteDataForPrintForm(oHCFA1500Form.CF_Top_InsuranceHeader.Value, oHCFA1500Form.CF_Top_InsuranceHeader.Location, false);

                //.Insurance Type"
                #region "Print Insurance Type"
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_Champva.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Champva.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_FECA.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_FECA.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_1_Insuracne_Type_Other.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Other.Location, true);
                #endregion

                //.Insured's ID Number
                WriteDataForPrintForm(oHCFA1500Form.CF_1a_InsuredsIDNumber.Value, oHCFA1500Form.CF_1a_InsuredsIDNumber.Location, false);

                //.Patient's Name
                WriteDataForPrintForm(oHCFA1500Form.CF_2_Patient_Name.Value, oHCFA1500Form.CF_2_Patient_Name.Location, false);

                //.Patient's Birth Date
                WriteDataForPrintForm(oHCFA1500Form.CF_3_Patient_DOB_MM.Value, oHCFA1500Form.CF_3_Patient_DOB_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_3_Patient_DOB_DD.Value, oHCFA1500Form.CF_3_Patient_DOB_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_3_Patient_DOB_YY.Value, oHCFA1500Form.CF_3_Patient_DOB_YY.Location, false);

                //.Patient's Sex
                WriteDataForPrintForm(oHCFA1500Form.CF_3_Patient_Sex_Male.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Male.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_3_Patient_Sex_Female.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Female.Location, true);

                //.Insured's Name
                WriteDataForPrintForm(oHCFA1500Form.CF_4_Insureds_Name.Value, oHCFA1500Form.CF_4_Insureds_Name.Location, false);

                //.Patient's Address
                WriteDataForPrintForm(oHCFA1500Form.CF_5_Patient_Address.Value, oHCFA1500Form.CF_5_Patient_Address.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_5_Patient_City.Value, oHCFA1500Form.CF_5_Patient_City.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_5_Patient_State.Value, oHCFA1500Form.CF_5_Patient_State.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_5_Patient_Zip.Value, oHCFA1500Form.CF_5_Patient_Zip.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Value, oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_5_Patient_Tel_Number.Value, oHCFA1500Form.CF_5_Patient_Tel_Number.Location, false);

                //.Paient Relationship to Insured
                WriteDataForPrintForm(oHCFA1500Form.CF_6_PatientRelationship_Self.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Self.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_6_PatientRelationship_Spouse.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Spouse.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_6_PatientRelationship_Child.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Child.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_6_PatientRelationship_Other.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Other.Location, true);

                //.Insured's Address
                WriteDataForPrintForm(oHCFA1500Form.CF_7_Insureds_Address.Value, oHCFA1500Form.CF_7_Insureds_Address.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_7_Insureds_City.Value, oHCFA1500Form.CF_7_Insureds_City.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_7_Insureds_State.Value, oHCFA1500Form.CF_7_Insureds_State.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_7_Insureds_Zip.Value, oHCFA1500Form.CF_7_Insureds_Zip.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Value, oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_7_Insureds_Tel_Number.Value, oHCFA1500Form.CF_7_Insureds_Tel_Number.Location, false);

                //.Patient Status
                WriteDataForPrintForm(oHCFA1500Form.CF_8_PatientStatus_Single.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Single.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_8_PatientStatus_Married.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Married.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_8_PatientStatus_Other.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Other.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_8_PatientStatus_Employed.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Employed.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Location, true);

                //.Other Insured's Name
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_Name.Value, oHCFA1500Form.CF_9_Other_Insureds_Name.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Value, oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Location, false);

                //.Patient Condition 
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Value, oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Value, oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Location, false);

                //.Insured's Information
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_DOB_MM.Value, oHCFA1500Form.CF_11_Insureds_DOB_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_DOB_DD.Value, oHCFA1500Form.CF_11_Insureds_DOB_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_DOB_YY.Value, oHCFA1500Form.CF_11_Insureds_DOB_YY.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Male.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Female.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_EmployerName.Value, oHCFA1500Form.CF_11_Insureds_EmployerName.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Location, true);

                WriteDataForPrintForm(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_17_ReferringProvider_Name.Value, oHCFA1500Form.CF_17_ReferringProvider_Name.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_17b_ReferringProvider_NPI.Value, oHCFA1500Form.CF_17b_ReferringProvider_NPI.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_19_LocalUse_Field.Value, oHCFA1500Form.CF_19_LocalUse_Field.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_20_OutsideLab_Yes.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_Yes.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_20_OutsideLab_No.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_No.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_1_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_1_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_2_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_2_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_3_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_3_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_4_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_4_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_22_MecaidResubmission_Code.Value, oHCFA1500Form.CF_22_MecaidResubmission_Code.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_22_Original_Refrence_No.Value, oHCFA1500Form.CF_22_Original_Refrence_No.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_23_PriorAuthorization_No.Value, oHCFA1500Form.CF_23_PriorAuthorization_No.Location, false);

                #region " Service Line 1 "

                if (oHCFA1500Form.CF_IsPresent_Line1 == true)
                {
                    //From Date.
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_From_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_From_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_From_YY.Location, false);

                    //To Date
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_To_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_To_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteDataForPrintForm(oHCFA1500Form.CF_24B_L1_POS_Code.Value, oHCFA1500Form.CF_24B_L1_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteDataForPrintForm(oHCFA1500Form.CF_24C_L1_EMG_Code.Value, oHCFA1500Form.CF_24C_L1_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L1_Charges_Principal.Value, oHCFA1500Form.CF_24F_L1_Charges_Principal.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L1_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L1_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteDataForPrintForm(oHCFA1500Form.CF_24G_L1_Days_Units.Value, oHCFA1500Form.CF_24G_L1_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Location, false);


                    //Rendering Provider ID (NPI)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Location, false);

                    // Notes
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L1_Note.Value, oHCFA1500Form.CF_24A_L1_Note.Location, false);
                }
                #endregion

                #region " Service Line 2 "

                if (oHCFA1500Form.CF_IsPresent_Line2 == true)
                {
                    //From Date.
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_From_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_From_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_From_YY.Location, false);

                    //To Date
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_To_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_To_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteDataForPrintForm(oHCFA1500Form.CF_24B_L2_POS_Code.Value, oHCFA1500Form.CF_24B_L2_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteDataForPrintForm(oHCFA1500Form.CF_24C_L2_EMG_Code.Value, oHCFA1500Form.CF_24C_L2_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L2_Charges_Principal.Value, oHCFA1500Form.CF_24F_L2_Charges_Principal.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L2_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L2_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteDataForPrintForm(oHCFA1500Form.CF_24G_L2_Days_Units.Value, oHCFA1500Form.CF_24G_L2_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Location, false);

                    // Notes
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L2_Note.Value, oHCFA1500Form.CF_24A_L2_Note.Location, false);
                }
                #endregion

                #region " Service Line 3 "

                if (oHCFA1500Form.CF_IsPresent_Line3 == true)
                {
                    //From Date.
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_From_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_From_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_From_YY.Location, false);

                    //To Date
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_To_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_To_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteDataForPrintForm(oHCFA1500Form.CF_24B_L3_POS_Code.Value, oHCFA1500Form.CF_24B_L3_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteDataForPrintForm(oHCFA1500Form.CF_24C_L3_EMG_Code.Value, oHCFA1500Form.CF_24C_L3_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L3_Charges_Principal.Value, oHCFA1500Form.CF_24F_L3_Charges_Principal.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L3_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L3_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteDataForPrintForm(oHCFA1500Form.CF_24G_L3_Days_Units.Value, oHCFA1500Form.CF_24G_L3_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Location, false);

                    // Notes
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L3_Note.Value, oHCFA1500Form.CF_24A_L3_Note.Location, false);
                }
                #endregion

                #region " Service Line 4 "

                if (oHCFA1500Form.CF_IsPresent_Line4 == true)
                {
                    //From Date.
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_From_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_From_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_From_YY.Location, false);

                    //To Date
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_To_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_To_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteDataForPrintForm(oHCFA1500Form.CF_24B_L4_POS_Code.Value, oHCFA1500Form.CF_24B_L4_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteDataForPrintForm(oHCFA1500Form.CF_24C_L4_EMG_Code.Value, oHCFA1500Form.CF_24C_L4_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L4_Charges_Principal.Value, oHCFA1500Form.CF_24F_L4_Charges_Principal.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L4_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L4_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteDataForPrintForm(oHCFA1500Form.CF_24G_L4_Days_Units.Value, oHCFA1500Form.CF_24G_L4_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Location, false);

                    // Notes
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L4_Note.Value, oHCFA1500Form.CF_24A_L4_Note.Location, false);

                }
                #endregion

                #region " Service Line 5 "

                if (oHCFA1500Form.CF_IsPresent_Line5 == true)
                {
                    //From Date.
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_From_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_From_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_From_YY.Location, false);

                    //To Date
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_To_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_To_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteDataForPrintForm(oHCFA1500Form.CF_24B_L5_POS_Code.Value, oHCFA1500Form.CF_24B_L5_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteDataForPrintForm(oHCFA1500Form.CF_24C_L5_EMG_Code.Value, oHCFA1500Form.CF_24C_L5_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L5_Charges_Principal.Value, oHCFA1500Form.CF_24F_L5_Charges_Principal.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L5_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L5_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteDataForPrintForm(oHCFA1500Form.CF_24G_L5_Days_Units.Value, oHCFA1500Form.CF_24G_L5_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Location, false);

                    // Notes
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L5_Note.Value, oHCFA1500Form.CF_24A_L5_Note.Location, false);
                }
                #endregion

                #region " Service Line 6 "

                if (oHCFA1500Form.CF_IsPresent_Line6 == true)
                {
                    //From Date.
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_From_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_From_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_From_YY.Location, false);

                    //To Date
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_To_MM.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_To_DD.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteDataForPrintForm(oHCFA1500Form.CF_24B_L6_POS_Code.Value, oHCFA1500Form.CF_24B_L6_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteDataForPrintForm(oHCFA1500Form.CF_24C_L6_EMG_Code.Value, oHCFA1500Form.CF_24C_L6_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteDataForPrintForm(oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L6_Charges_Principal.Value, oHCFA1500Form.CF_24F_L6_Charges_Principal.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24F_L6_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L6_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteDataForPrintForm(oHCFA1500Form.CF_24G_L6_Days_Units.Value, oHCFA1500Form.CF_24G_L6_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteDataForPrintForm(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteDataForPrintForm(oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Location, false);

                    // Notes
                    WriteDataForPrintForm(oHCFA1500Form.CF_24A_L6_Note.Value, oHCFA1500Form.CF_24A_L6_Note.Location, false);
                }
                #endregion

                WriteDataForPrintForm(oHCFA1500Form.CF_25_FederalTax_ID_No.Value, oHCFA1500Form.CF_25_FederalTax_ID_No.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Location, true);

                WriteDataForPrintForm(oHCFA1500Form.CF_26_PatientAccount_No.Value, oHCFA1500Form.CF_26_PatientAccount_No.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_27_AcceptAssignment_YES.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_YES.Location, true);
                WriteDataForPrintForm(oHCFA1500Form.CF_27_AcceptAssignment_NO.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_NO.Location, true);

                WriteDataForPrintForm(oHCFA1500Form.CF_28_TotalCharge_Principal.Value, oHCFA1500Form.CF_28_TotalCharge_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_28_TotalCharge_Secondary.Value, oHCFA1500Form.CF_28_TotalCharge_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_29_AmountPaid_Principal.Value, oHCFA1500Form.CF_29_AmountPaid_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_29_AmountPaid_Secondary.Value, oHCFA1500Form.CF_29_AmountPaid_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_30_BalanceDue_Principal.Value, oHCFA1500Form.CF_30_BalanceDue_Principal.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_30_BalanceDue_Secondary.Value, oHCFA1500Form.CF_30_BalanceDue_Secondary.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_31_Physician_Supplier_Signature.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Value, oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_32_Service_Facility_Name.Value, oHCFA1500Form.CF_32_Service_Facility_Name.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32_Service_Facility_City.Value, oHCFA1500Form.CF_32_Service_Facility_City.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32_Service_Facility_State.Value, oHCFA1500Form.CF_32_Service_Facility_State.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32_Service_Facility_Zip.Value, oHCFA1500Form.CF_32_Service_Facility_Zip.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32a_Service_Facility_NPI.Value, oHCFA1500Form.CF_32a_Service_Facility_NPI.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Value, oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Location, false);

                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_Name.Value, oHCFA1500Form.CF_33_BillingProvider_Name.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_City.Value, oHCFA1500Form.CF_33_BillingProvider_City.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_State.Value, oHCFA1500Form.CF_33_BillingProvider_State.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_Zip.Value, oHCFA1500Form.CF_33_BillingProvider_Zip.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33a_BillingProvider_NPI.Value, oHCFA1500Form.CF_33a_BillingProvider_NPI.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Value, oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Location, false);
                WriteDataForPrintForm(oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Location, false);

                #endregion
            }
            else
            {
                #region "Write Data for Print Data"

                //.Insuracne Header on Claim Form
                WriteData(oHCFA1500Form.CF_Top_InsuranceHeader.Value, oHCFA1500Form.CF_Top_InsuranceHeader.Location, false);

                //.Insurance Type"
                #region "Print Insurance Type"
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicare.Location, true);
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Medicaid.Location, true);
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Tricare.Location, true);
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Champva.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Champva.Location, true);
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_GroupHealthPlan.Location, true);
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_FECA.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_FECA.Location, true);
                WriteData(oHCFA1500Form.CF_1_Insuracne_Type_Other.Value.ToString(), oHCFA1500Form.CF_1_Insuracne_Type_Other.Location, true);
                #endregion

                //.Insured's ID Number
                WriteData(oHCFA1500Form.CF_1a_InsuredsIDNumber.Value, oHCFA1500Form.CF_1a_InsuredsIDNumber.Location, false);

                //.Patient's Name
                WriteData(oHCFA1500Form.CF_2_Patient_Name.Value, oHCFA1500Form.CF_2_Patient_Name.Location, false);

                //.Patient's Birth Date
                WriteData(oHCFA1500Form.CF_3_Patient_DOB_MM.Value, oHCFA1500Form.CF_3_Patient_DOB_MM.Location, false);
                WriteData(oHCFA1500Form.CF_3_Patient_DOB_DD.Value, oHCFA1500Form.CF_3_Patient_DOB_DD.Location, false);
                WriteData(oHCFA1500Form.CF_3_Patient_DOB_YY.Value, oHCFA1500Form.CF_3_Patient_DOB_YY.Location, false);

                //.Patient's Sex
                WriteData(oHCFA1500Form.CF_3_Patient_Sex_Male.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Male.Location, true);
                WriteData(oHCFA1500Form.CF_3_Patient_Sex_Female.Value.ToString(), oHCFA1500Form.CF_3_Patient_Sex_Female.Location, true);

                //.Insured's Name
                WriteData(oHCFA1500Form.CF_4_Insureds_Name.Value, oHCFA1500Form.CF_4_Insureds_Name.Location, false);

                //.Patient's Address
                WriteData(oHCFA1500Form.CF_5_Patient_Address.Value, oHCFA1500Form.CF_5_Patient_Address.Location, false);
                WriteData(oHCFA1500Form.CF_5_Patient_City.Value, oHCFA1500Form.CF_5_Patient_City.Location, false);
                WriteData(oHCFA1500Form.CF_5_Patient_State.Value, oHCFA1500Form.CF_5_Patient_State.Location, false);
                WriteData(oHCFA1500Form.CF_5_Patient_Zip.Value, oHCFA1500Form.CF_5_Patient_Zip.Location, false);
                WriteData(oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Value, oHCFA1500Form.CF_5_Patient_Tel_AreaCode.Location, false);
                WriteData(oHCFA1500Form.CF_5_Patient_Tel_Number.Value, oHCFA1500Form.CF_5_Patient_Tel_Number.Location, false);

                //.Paient Relationship to Insured
                WriteData(oHCFA1500Form.CF_6_PatientRelationship_Self.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Self.Location, true);
                WriteData(oHCFA1500Form.CF_6_PatientRelationship_Spouse.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Spouse.Location, true);
                WriteData(oHCFA1500Form.CF_6_PatientRelationship_Child.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Child.Location, true);
                WriteData(oHCFA1500Form.CF_6_PatientRelationship_Other.Value.ToString(), oHCFA1500Form.CF_6_PatientRelationship_Other.Location, true);

                //.Insured's Address
                WriteData(oHCFA1500Form.CF_7_Insureds_Address.Value, oHCFA1500Form.CF_7_Insureds_Address.Location, false);
                WriteData(oHCFA1500Form.CF_7_Insureds_City.Value, oHCFA1500Form.CF_7_Insureds_City.Location, false);
                WriteData(oHCFA1500Form.CF_7_Insureds_State.Value, oHCFA1500Form.CF_7_Insureds_State.Location, false);
                WriteData(oHCFA1500Form.CF_7_Insureds_Zip.Value, oHCFA1500Form.CF_7_Insureds_Zip.Location, false);
                WriteData(oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Value, oHCFA1500Form.CF_7_Insureds_Tel_AreaCode.Location, false);
                WriteData(oHCFA1500Form.CF_7_Insureds_Tel_Number.Value, oHCFA1500Form.CF_7_Insureds_Tel_Number.Location, false);

                //.Patient Status
                WriteData(oHCFA1500Form.CF_8_PatientStatus_Single.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Single.Location, true);
                WriteData(oHCFA1500Form.CF_8_PatientStatus_Married.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Married.Location, true);
                WriteData(oHCFA1500Form.CF_8_PatientStatus_Other.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Other.Location, true);
                WriteData(oHCFA1500Form.CF_8_PatientStatus_Employed.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_Employed.Location, true);
                WriteData(oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_FullTimeStudent.Location, true);
                WriteData(oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Value.ToString(), oHCFA1500Form.CF_8_PatientStatus_PartTimeStudent.Location, true);

                //.Other Insured's Name
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_Name.Value, oHCFA1500Form.CF_9_Other_Insureds_Name.Location, false);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_9_Other_Insureds_PolicyGroupNo.Location, false);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_MM.Location, false);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_DD.Location, false);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Value, oHCFA1500Form.CF_9_Other_Insureds_DOB_YY.Location, false);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Male.Location, true);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_9_Other_Insureds_Sex_Female.Location, true);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Value, oHCFA1500Form.CF_9_Other_Insureds_EmployerName.Location, false);
                WriteData(oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_9_Other_Insureds_InsuracnePlan.Location, false);

                //.Patient Condition 
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_Yes.Location, true);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_Employement_No.Location, true);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_Yes.Location, true);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_No.Location, true);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Value, oHCFA1500Form.CF_10_PatientConditionTo_AutoAccident_State.Location, false);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_Yes.Location, true);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Value.ToString(), oHCFA1500Form.CF_10_PatientConditionTo_OtherAccident_No.Location, true);
                WriteData(oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Value, oHCFA1500Form.CF_10_PatientConditionTo_ResForLocaluse.Location, false);

                //.Insured's Information
                WriteData(oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Value, oHCFA1500Form.CF_11_Insureds_PolicyGroupNo.Location, false);
                WriteData(oHCFA1500Form.CF_11_Insureds_DOB_MM.Value, oHCFA1500Form.CF_11_Insureds_DOB_MM.Location, false);
                WriteData(oHCFA1500Form.CF_11_Insureds_DOB_DD.Value, oHCFA1500Form.CF_11_Insureds_DOB_DD.Location, false);
                WriteData(oHCFA1500Form.CF_11_Insureds_DOB_YY.Value, oHCFA1500Form.CF_11_Insureds_DOB_YY.Location, false);
                WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Male.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Male.Location, true);
                WriteData(oHCFA1500Form.CF_11_Insureds_Sex_Female.Value.ToString(), oHCFA1500Form.CF_11_Insureds_Sex_Female.Location, true);
                WriteData(oHCFA1500Form.CF_11_Insureds_EmployerName.Value, oHCFA1500Form.CF_11_Insureds_EmployerName.Location, false);
                WriteData(oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Value, oHCFA1500Form.CF_11_Insureds_InsuracnePlan.Location, false);
                WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_Yes.Location, true);
                WriteData(oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Value.ToString(), oHCFA1500Form.CF_11_Insureds_OtherHealthPlan_No.Location, true);

                WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature.Location, false);
                WriteData(oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Value, oHCFA1500Form.CF_12_PatientAuthorizedPersons_Signature_Date.Location, false);

                WriteData(oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Value, oHCFA1500Form.CF_13_InsuredsAuthorizedPersons_Signature.Location, false);

                WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_MM.Location, false);
                WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_DD.Location, false);
                WriteData(oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Value, oHCFA1500Form.CF_14_DateOfCurrent_Illness_Injury_Pregnancy_YY.Location, false);

                WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_MM.Location, false);
                WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_DD.Location, false);
                WriteData(oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Value, oHCFA1500Form.CF_15_FirstDateOfSimilar_Illness_YY.Location, false);

                WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_MM.Location, false);
                WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_DD.Location, false);
                WriteData(oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkFromDate_YY.Location, false);

                WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_MM.Location, false);
                WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_DD.Location, false);
                WriteData(oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Value, oHCFA1500Form.CF_16_UnableToWorkTillDate_YY.Location, false);

                WriteData(oHCFA1500Form.CF_17_ReferringProvider_Name.Value, oHCFA1500Form.CF_17_ReferringProvider_Name.Location, false);
                WriteData(oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherQualifier.Location, false);
                WriteData(oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Value, oHCFA1500Form.CF_17a_ReferringProvider_OtherID.Location, false);
                WriteData(oHCFA1500Form.CF_17b_ReferringProvider_NPI.Value, oHCFA1500Form.CF_17b_ReferringProvider_NPI.Location, false);

                WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_MM.Location, false);
                WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_DD.Location, false);
                WriteData(oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationFromDate_YY.Location, false);

                WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_MM.Location, false);
                WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_DD.Location, false);
                WriteData(oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Value, oHCFA1500Form.CF_18_HospitalizationTillDate_YY.Location, false);

                WriteData(oHCFA1500Form.CF_19_LocalUse_Field.Value, oHCFA1500Form.CF_19_LocalUse_Field.Location, false);

                WriteData(oHCFA1500Form.CF_20_OutsideLab_Yes.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_Yes.Location, true);
                WriteData(oHCFA1500Form.CF_20_OutsideLab_No.Value.ToString(), oHCFA1500Form.CF_20_OutsideLab_No.Location, true);
                WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Value, oHCFA1500Form.CF_20_OutsideLab_Charges_Secondary.Location, false);

                WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_1_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_1_Secondary.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_2_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_2_Secondary.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_3_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_3_Secondary.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Principal.Value, oHCFA1500Form.CF_21_Diagnosis_4_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Value, oHCFA1500Form.CF_21_Diagnosis_4_Secondary.Location, false);

                WriteData(oHCFA1500Form.CF_22_MecaidResubmission_Code.Value, oHCFA1500Form.CF_22_MecaidResubmission_Code.Location, false);
                WriteData(oHCFA1500Form.CF_22_Original_Refrence_No.Value, oHCFA1500Form.CF_22_Original_Refrence_No.Location, false);

                WriteData(oHCFA1500Form.CF_23_PriorAuthorization_No.Value, oHCFA1500Form.CF_23_PriorAuthorization_No.Location, false);

                #region " Service Line 1 "

                if (oHCFA1500Form.CF_IsPresent_Line1 == true)
                {
                    //From Date.
                    WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_From_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_From_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L1_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_From_YY.Location, false);

                    //To Date
                    WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L1_DOS_To_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L1_DOS_To_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L1_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L1_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteData(oHCFA1500Form.CF_24B_L1_POS_Code.Value, oHCFA1500Form.CF_24B_L1_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteData(oHCFA1500Form.CF_24C_L1_EMG_Code.Value, oHCFA1500Form.CF_24C_L1_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteData(oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L1_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteData(oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_1_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_2_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_3_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L1_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteData(oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L1_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteData(oHCFA1500Form.CF_24F_L1_Charges_Principal.Value, oHCFA1500Form.CF_24F_L1_Charges_Principal.Location, false);
                    WriteData(oHCFA1500Form.CF_24F_L1_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L1_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteData(oHCFA1500Form.CF_24G_L1_Days_Units.Value, oHCFA1500Form.CF_24G_L1_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteData(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteData(oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L1_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteData(oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L1_RenderingProvider_OtherQualifiervalue.Location, false);


                    WriteData(oHCFA1500Form.CF_24A_L1_Note.Value, oHCFA1500Form.CF_24A_L1_Note.Location, false);
                }
                #endregion

                #region " Service Line 2 "

                if (oHCFA1500Form.CF_IsPresent_Line2 == true)
                {
                    //From Date.
                    WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_From_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_From_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L2_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_From_YY.Location, false);

                    //To Date
                    WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L2_DOS_To_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L2_DOS_To_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L2_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L2_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteData(oHCFA1500Form.CF_24B_L2_POS_Code.Value, oHCFA1500Form.CF_24B_L2_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteData(oHCFA1500Form.CF_24C_L2_EMG_Code.Value, oHCFA1500Form.CF_24C_L2_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteData(oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L2_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteData(oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_1_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_2_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_3_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L2_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteData(oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L2_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteData(oHCFA1500Form.CF_24F_L2_Charges_Principal.Value, oHCFA1500Form.CF_24F_L2_Charges_Principal.Location, false);
                    WriteData(oHCFA1500Form.CF_24F_L2_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L2_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteData(oHCFA1500Form.CF_24G_L2_Days_Units.Value, oHCFA1500Form.CF_24G_L2_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteData(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteData(oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L2_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteData(oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L2_RenderingProvider_OtherQualifiervalue.Location, false);


                    WriteData(oHCFA1500Form.CF_24A_L2_Note.Value, oHCFA1500Form.CF_24A_L2_Note.Location, false);
                }
                #endregion

                #region " Service Line 3 "

                if (oHCFA1500Form.CF_IsPresent_Line3 == true)
                {
                    //From Date.
                    WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_From_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_From_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L3_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_From_YY.Location, false);

                    //To Date
                    WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L3_DOS_To_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L3_DOS_To_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L3_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L3_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteData(oHCFA1500Form.CF_24B_L3_POS_Code.Value, oHCFA1500Form.CF_24B_L3_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteData(oHCFA1500Form.CF_24C_L3_EMG_Code.Value, oHCFA1500Form.CF_24C_L3_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteData(oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L3_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteData(oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_1_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_2_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_3_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L3_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteData(oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L3_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteData(oHCFA1500Form.CF_24F_L3_Charges_Principal.Value, oHCFA1500Form.CF_24F_L3_Charges_Principal.Location, false);
                    WriteData(oHCFA1500Form.CF_24F_L3_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L3_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteData(oHCFA1500Form.CF_24G_L3_Days_Units.Value, oHCFA1500Form.CF_24G_L3_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteData(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteData(oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L3_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteData(oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L3_RenderingProvider_OtherQualifiervalue.Location, false);


                    WriteData(oHCFA1500Form.CF_24A_L3_Note.Value, oHCFA1500Form.CF_24A_L3_Note.Location, false);
                }
                #endregion

                #region " Service Line 4 "

                if (oHCFA1500Form.CF_IsPresent_Line4 == true)
                {
                    //From Date.
                    WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_From_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_From_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L4_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_From_YY.Location, false);

                    //To Date
                    WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L4_DOS_To_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L4_DOS_To_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L4_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L4_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteData(oHCFA1500Form.CF_24B_L4_POS_Code.Value, oHCFA1500Form.CF_24B_L4_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteData(oHCFA1500Form.CF_24C_L4_EMG_Code.Value, oHCFA1500Form.CF_24C_L4_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteData(oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L4_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteData(oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_1_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_2_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_3_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L4_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteData(oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L4_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteData(oHCFA1500Form.CF_24F_L4_Charges_Principal.Value, oHCFA1500Form.CF_24F_L4_Charges_Principal.Location, false);
                    WriteData(oHCFA1500Form.CF_24F_L4_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L4_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteData(oHCFA1500Form.CF_24G_L4_Days_Units.Value, oHCFA1500Form.CF_24G_L4_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteData(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteData(oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L4_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteData(oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L4_RenderingProvider_OtherQualifiervalue.Location, false);


                    WriteData(oHCFA1500Form.CF_24A_L4_Note.Value, oHCFA1500Form.CF_24A_L4_Note.Location, false);

                }
                #endregion

                #region " Service Line 5 "

                if (oHCFA1500Form.CF_IsPresent_Line5 == true)
                {
                    //From Date.
                    WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_From_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_From_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L5_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_From_YY.Location, false);

                    //To Date
                    WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L5_DOS_To_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L5_DOS_To_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L5_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L5_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteData(oHCFA1500Form.CF_24B_L5_POS_Code.Value, oHCFA1500Form.CF_24B_L5_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteData(oHCFA1500Form.CF_24C_L5_EMG_Code.Value, oHCFA1500Form.CF_24C_L5_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteData(oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L5_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteData(oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_1_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_2_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_3_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L5_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteData(oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L5_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteData(oHCFA1500Form.CF_24F_L5_Charges_Principal.Value, oHCFA1500Form.CF_24F_L5_Charges_Principal.Location, false);
                    WriteData(oHCFA1500Form.CF_24F_L5_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L5_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteData(oHCFA1500Form.CF_24G_L5_Days_Units.Value, oHCFA1500Form.CF_24G_L5_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteData(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteData(oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L5_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteData(oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L5_RenderingProvider_OtherQualifiervalue.Location, false);


                    WriteData(oHCFA1500Form.CF_24A_L5_Note.Value, oHCFA1500Form.CF_24A_L5_Note.Location, false);
                }
                #endregion

                #region " Service Line 6 "

                if (oHCFA1500Form.CF_IsPresent_Line6 == true)
                {
                    //From Date.
                    WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_From_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_From_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L6_DOS_From_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_From_YY.Location, false);

                    //To Date
                    WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_MM.Value, oHCFA1500Form.CF_24A_L6_DOS_To_MM.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_DD.Value, oHCFA1500Form.CF_24A_L6_DOS_To_DD.Location, false);
                    WriteData(oHCFA1500Form.CF_24A_L6_DOS_To_YY.Value, oHCFA1500Form.CF_24A_L6_DOS_To_YY.Location, false);

                    //Place Of Service
                    WriteData(oHCFA1500Form.CF_24B_L6_POS_Code.Value, oHCFA1500Form.CF_24B_L6_POS_Code.Location, false);

                    //EMG - Emergency
                    WriteData(oHCFA1500Form.CF_24C_L6_EMG_Code.Value, oHCFA1500Form.CF_24C_L6_EMG_Code.Location, false);

                    //CPT/HCPCS 
                    WriteData(oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Value, oHCFA1500Form.CF_24D_L6_CPT_HCPCS_Code.Location, false);

                    //Modifiers
                    WriteData(oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_1_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_2_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_3_Code.Location, false);
                    WriteData(oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Value, oHCFA1500Form.CF_24D_L6_Modifier_4_Code.Location, false);

                    //Diagnosis Pointers 
                    WriteData(oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Value, oHCFA1500Form.CF_24E_L6_Diagnosis_Pointers.Location, false);

                    //Charges
                    WriteData(oHCFA1500Form.CF_24F_L6_Charges_Principal.Value, oHCFA1500Form.CF_24F_L6_Charges_Principal.Location, false);
                    WriteData(oHCFA1500Form.CF_24F_L6_Charges_Secondary.Value, oHCFA1500Form.CF_24F_L6_Charges_Secondary.Location, false);

                    //Days or Units
                    WriteData(oHCFA1500Form.CF_24G_L6_Days_Units.Value, oHCFA1500Form.CF_24G_L6_Days_Units.Location, false);

                    //EPSDT Family Plan
                    WriteData(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan_Shaded.Location, false);
                    WriteData(oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Value, oHCFA1500Form.CF_24H_L6_EPSDT_FamilyPlan.Location, false);

                    //Rendering Provider ID (NPI)
                    WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_NPI.Location, false);

                    //Rendering Provider Other ID (Qualifier)
                    WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifier.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifier.Location, false);
                    //Rendering Provider Other ID (Qualifier Value)
                    WriteData(oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Value, oHCFA1500Form.CF_24J_L6_RenderingProvider_OtherQualifiervalue.Location, false);


                    WriteData(oHCFA1500Form.CF_24A_L6_Note.Value, oHCFA1500Form.CF_24A_L6_Note.Location, false);
                }
                #endregion

                WriteData(oHCFA1500Form.CF_25_FederalTax_ID_No.Value, oHCFA1500Form.CF_25_FederalTax_ID_No.Location, false);
                WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_SSN.Location, true);
                WriteData(oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Value.ToString(), oHCFA1500Form.CF_25_FederalTaxID_Qualifier_EIN.Location, true);

                WriteData(oHCFA1500Form.CF_26_PatientAccount_No.Value, oHCFA1500Form.CF_26_PatientAccount_No.Location, false);

                WriteData(oHCFA1500Form.CF_27_AcceptAssignment_YES.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_YES.Location, true);
                WriteData(oHCFA1500Form.CF_27_AcceptAssignment_NO.Value.ToString(), oHCFA1500Form.CF_27_AcceptAssignment_NO.Location, true);

                WriteData(oHCFA1500Form.CF_28_TotalCharge_Principal.Value, oHCFA1500Form.CF_28_TotalCharge_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_28_TotalCharge_Secondary.Value, oHCFA1500Form.CF_28_TotalCharge_Secondary.Location, false);

                WriteData(oHCFA1500Form.CF_29_AmountPaid_Principal.Value, oHCFA1500Form.CF_29_AmountPaid_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_29_AmountPaid_Secondary.Value, oHCFA1500Form.CF_29_AmountPaid_Secondary.Location, false);

                WriteData(oHCFA1500Form.CF_30_BalanceDue_Principal.Value, oHCFA1500Form.CF_30_BalanceDue_Principal.Location, false);
                WriteData(oHCFA1500Form.CF_30_BalanceDue_Secondary.Value, oHCFA1500Form.CF_30_BalanceDue_Secondary.Location, false);

                WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature.Location, false);
                WriteData(oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Value, oHCFA1500Form.CF_31_Physician_Supplier_Signature_Date.Location, false);
                WriteData(oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Value, oHCFA1500Form.CF_31_Physician_Supplier_QualifierValue.Location, false);

                WriteData(oHCFA1500Form.CF_32_Service_Facility_Name.Value, oHCFA1500Form.CF_32_Service_Facility_Name.Location, false);
                WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line1.Location, false);
                WriteData(oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Value, oHCFA1500Form.CF_32_Service_Facility_Address_Line2.Location, false);
                WriteData(oHCFA1500Form.CF_32_Service_Facility_City.Value, oHCFA1500Form.CF_32_Service_Facility_City.Location, false);
                WriteData(oHCFA1500Form.CF_32_Service_Facility_State.Value, oHCFA1500Form.CF_32_Service_Facility_State.Location, false);
                WriteData(oHCFA1500Form.CF_32_Service_Facility_Zip.Value, oHCFA1500Form.CF_32_Service_Facility_Zip.Location, false);
                WriteData(oHCFA1500Form.CF_32a_Service_Facility_NPI.Value, oHCFA1500Form.CF_32a_Service_Facility_NPI.Location, false);
                WriteData(oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Value, oHCFA1500Form.CF_32b_Service_Facility_UPIN_OtherID.Location, false);

                WriteData(oHCFA1500Form.CF_33_BillingProvider_Name.Value, oHCFA1500Form.CF_33_BillingProvider_Name.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line1.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Value, oHCFA1500Form.CF_33_BillingProvider_Address_Line2.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_City.Value, oHCFA1500Form.CF_33_BillingProvider_City.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_State.Value, oHCFA1500Form.CF_33_BillingProvider_State.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_Zip.Value, oHCFA1500Form.CF_33_BillingProvider_Zip.Location, false);
                WriteData(oHCFA1500Form.CF_33a_BillingProvider_NPI.Value, oHCFA1500Form.CF_33a_BillingProvider_NPI.Location, false);
                WriteData(oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Value, oHCFA1500Form.CF_33b_BillingProvider_UPIN_OtherID.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_AreaCode.Location, false);
                WriteData(oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Value, oHCFA1500Form.CF_33_BillingProvider_Tel_Number.Location, false);

                #endregion
            }
        }
        // New method created for PrintForm
        private void WriteDataForPrintForm(string data, PointF location, bool isboolean)
        {
            PointF oPoint = new PointF(location.X * 2, location.Y * 2);
            if (isboolean == false)
            {
                oGraphics.DrawString(data, arialRegular, Brushes.Black, oPoint);
            }
            else
            {
                if (data.ToUpper() == "TRUE")
                {
                    oGraphics.DrawString("X", arialBold, Brushes.Black, oPoint);
                }
            }
        }
        #endregion

        private void WriteData(string data, PointF location, bool isboolean)
        {
            PointF oPoint = new PointF(location.X * 2, location.Y * 2);
            if (isboolean == false)
            {
                oGraphics.DrawString(data, arialRegular, Brushes.Black, oPoint);
            }
            else
            {
                if (data.ToUpper() == "TRUE")
                {
                    oGraphics.DrawString("X", arialBold, Brushes.Black, oPoint);
                }
            }
        }
    }

    class gloPrintUB04PaperForm
    {     

        #region "Constructor & Destructor"
        private string _databaseconnectionstring = "";
        public gloPrintUB04PaperForm()
        {
        }
        public gloPrintUB04PaperForm(string _databaseconnectionstring)
        {
            this._databaseconnectionstring = _databaseconnectionstring;
        }
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                }
            }
            disposed = true;
        }

        ~gloPrintUB04PaperForm()
        {
            Dispose(false);
        }

        #endregion

        Bitmap oSourceUB04 = null;
        Graphics oGraphics = null;
        Font arialRegular = null;
        Font arialBold = null;
        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForClaims && gloGlobal.gloTSPrint.isCopyPrint;

        private ClsBL_UB04PaperForm _oUB04PaperForm = null;
        private Boolean _PrintOnForm = false;
        float arialFontSmallHeight = 8.5f;
        float arialFontBigHeight = 24.0f;
        private void AdjustFontHeight()
        {
            float _arialFontSmall = arialRegular.GetHeight(oGraphics);
            float _arialFontBig = arialBold.GetHeight(oGraphics);
            arialRegular = new Font(arialRegular.FontFamily, arialRegular.Size * arialFontSmallHeight / _arialFontSmall, arialRegular.Style);
            arialBold = new Font(arialBold.FontFamily, arialBold.Size * arialFontBigHeight / _arialFontBig, arialBold.Style);
        }
        private void GetFontHeight()
        {
            using (oGraphics = Graphics.FromImage(oSourceUB04))
            {
                arialFontSmallHeight = arialRegular.GetHeight(oGraphics);
                arialFontBigHeight = arialBold.GetHeight(oGraphics);
            }
        }

        private int CreateEMFUB04Form(Graphics thisGraphics, float bmpWidth, float bmpHeight)
        {
            try
            {
                oGraphics = thisGraphics;
                AdjustFontHeight();
                #region "Write Respective Data on Image"
                // Write Data Image region shifted to following Method, 
                if (_PrintOnForm)
                {
                    oGraphics.DrawImage(oSourceUB04, new RectangleF(0, 0, bmpWidth, bmpHeight));
                }
                else
                {
                    oGraphics.Clear(Color.White);
                }

                // Passing PrintOnForm will indicate whether to write data for Pring Form / Printing Data
                WriteRespectiveData(_oUB04PaperForm, _PrintOnForm);
                #endregion

                return 0;
            }
            catch
            {
                return 1;
            }

        }
        public string PrintUB04Form(ClsBL_UB04PaperForm oUB04PaperForm, string SourceFilePath, Boolean PrintOnForm,Boolean isForPrint = false)
        {
            string _result = "";
            string _printFilePath = "";
            string sPath = "";
            _oUB04PaperForm = oUB04PaperForm;
            _PrintOnForm = PrintOnForm;
            if (!isForPrint)
            {
                toCreateEMF = false;
            }
            try
            {
                sPath = gloSettings.FolderSettings.AppTempFolderPath + "Paper_Claim_Files";               

                if (System.IO.Directory.Exists(sPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(sPath);
                    }
                
                    if (PrintOnForm == true)
                        _printFilePath = sPath + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + (toCreateEMF ? ".emf" : ".tif");
                    else
                        _printFilePath = sPath + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + "_BLANK" + (toCreateEMF ? ".emf" : ".tif");

                    if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }
                  
                    clsgloBilling oclsgloBilling = new clsgloBilling(_databaseconnectionstring);
                    string sFont = oclsgloBilling.GetFontSetupSettingformCmsAndUB("UB04 Font");
                    string sFontSize = oclsgloBilling.GetFontSetupSettingformCmsAndUB("UB04 Font Size");
                    object bIsfontSizeSelectionEnable = null;                   
                    bIsfontSizeSelectionEnable = oclsgloBilling.GetFontSetupSettingformCmsAndUB("EnableUB04FontSizeSelection");
                   
                    if (PrintOnForm == true)
                        oSourceUB04 = new Bitmap(Application.StartupPath.ToString() + "\\UB04.tif");
                    else
                    {
                        if (bIsfontSizeSelectionEnable != null && Convert.ToString( bIsfontSizeSelectionEnable) != "" && Convert.ToBoolean(bIsfontSizeSelectionEnable))
                        {
                            // oSourceUB04 = new Bitmap(2550, 3300);
                            Int64 nWidth = Convert.ToInt32(850.0F * (300.0F / 96.0F));
                            Int64 nHeight = Convert.ToInt32(1100.0F * (300.0F / 96.0F));

                            oSourceUB04 = new Bitmap(Convert.ToInt32(850.0F * (300.0F / 96.0F)), Convert.ToInt32(1100.0F * (300.0F / 96.0F)));
                        }
                        else
                        {
                            oSourceUB04 = new Bitmap(2550, 3300);
                        }
                    }

                 

                    if (oclsgloBilling != null)
                    {
                        oclsgloBilling.Dispose();
                        oclsgloBilling = null;
                    }
                    arialRegular = new Font("Arial", 8.0F, FontStyle.Bold);
                    //arialBold = new Font("Arial", 24.0F, FontStyle.Regular);
                    float fontSize = ((float.Parse(sFontSize) * 300.0F) / 96.0F);
                    if (bIsfontSizeSelectionEnable != null && Convert.ToString(bIsfontSizeSelectionEnable) != "" && Convert.ToBoolean(bIsfontSizeSelectionEnable))
                    {
                        
                        if (CheckFontAvailable(sFont, sFontSize))
                        {
                            arialBold = new Font(sFont == "" ? "Arial" : sFont, sFontSize == "" ? 24.0F : ((float.Parse(sFontSize) * 300.0F) / 96.0F), FontStyle.Regular);
                        }
                        else
                        {
                            string sNavigation = "Install required font on machine OR  Change font setting to default \"Arial\" in gloPM Admin.[Navigation: gloPM Admin> Settings> UB04 Settings] to prevent this dialog in future.";
                            string sMsg = string.Format("Claim form print setting font [Name: \"{0}\", with Style: \"Regular\" or \"Bold\"] is not installed on this machine.\n\n{1}\n\nDo you want to print data with default font[Name: Arial]?", sFont, sNavigation);
                            if (MessageBox.Show(sMsg, "gloPM", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                            {
                                arialBold = new Font("Arial", 24.0F, FontStyle.Regular);
                            }
                            else
                            {
                                return string.Empty;
                            }
                        }
                    }
                    else
                    {
                        arialBold = new Font("Arial", 24.0F, FontStyle.Regular);
                    }
                   

                    if (toCreateEMF)
                    {
                        GetFontHeight();
                        byte[] emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)oSourceUB04.Width / oSourceUB04.HorizontalResolution, (float)oSourceUB04.Height / oSourceUB04.VerticalResolution, oSourceUB04.Width, oSourceUB04.Height, CreateEMFUB04Form);
                        File.WriteAllBytes(_printFilePath, emfBytes);
                    }
                    else
                    {
                        oGraphics = Graphics.FromImage(oSourceUB04);
                        #region "Write Respective Data on Image"

                        WriteRespectiveData(oUB04PaperForm, PrintOnForm);

                        #endregion


                        oSourceUB04.Save(_printFilePath, System.Drawing.Imaging.ImageFormat.Tiff);

                        if (oGraphics != null) { oGraphics.Dispose(); oGraphics = null; }
                    }
                    if (oSourceUB04 != null) { oSourceUB04.Dispose(); oSourceUB04 = null; }
                    if (arialRegular != null) { arialRegular.Dispose(); arialRegular = null; }
                    if (arialBold != null) { arialBold.Dispose(); arialBold = null; }

                    _result = _printFilePath;
              //  }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
            finally
            {
            }
            return _result;
        }
        private bool CheckFontAvailable(string sFontName, string sFontSize)
        {
            Boolean _result = false;
            try
            {
                using (Font fontTester = new Font(sFontName, float.Parse(sFontSize), FontStyle.Regular, GraphicsUnit.Pixel))
                {
                    if (fontTester.Name == sFontName)
                    {
                        _result = true;
                    }
                    else
                    {
                        _result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _result = false;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _result;
        }     
        private void WriteRespectiveData(ClsBL_UB04PaperForm oUBForm, Boolean PrintOnForm)
        {
            float _Height = 0;
            if (PrintOnForm)
            {
       

          //  ''Provider's Details
            WriteDataForPrintForm(oUBForm.UB04_1_ProviderName.Value.ToString(), oUBForm.UB04_1_ProviderName.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_1_ProviderAddress.Value.ToString(), oUBForm.UB04_1_ProviderAddress.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_1a_ProviderCity.Value.ToString(), oUBForm.UB04_1a_ProviderCity.Location, false);
          //  WriteDataForPrintForm(oUBForm.UB04_1b_ProviderState.Value.ToString(), oUBForm.UB04_1b_ProviderState.Location, false);
         //   WriteDataForPrintForm(oUBForm.UB04_1c_ProviderZipCode.Value.ToString(), oUBForm.UB04_1c_ProviderZipCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_1b_ProviderFaxNumber.Value.ToString(), oUBForm.UB04_1b_ProviderFaxNumber.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_1a_ProviderPhone.Value.ToString(), oUBForm.UB04_1a_ProviderPhone.Location, false);


           // ' Payer's Details
            WriteDataForPrintForm(oUBForm.UB04_2_PayToName.Value.ToString(), oUBForm.UB04_2_PayToName.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_2_PayToAddress.Value.ToString(), oUBForm.UB04_2_PayToAddress.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_2a_Pay_toCity.Value.ToString(), oUBForm.UB04_2a_Pay_toCity.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_2b_Pay_toState.Value.ToString(), oUBForm.UB04_2b_Pay_toState.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_2a_Pay_toZip.Value.ToString(), oUBForm.UB04_2a_Pay_toZip.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_2b_ReservedFL02.Value.ToString(), oUBForm.UB04_2b_ReservedFL02.Location, false);

          //  'Patient Control Number
            WriteDataForPrintForm(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, false);           // 'WriteDataForPrintForm(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, false)
          //  'WriteDataForPrintForm(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, false)
            WriteDataForPrintForm(oUBForm.UB04_3b_MedicalHealthRecordNumber.Value.ToString(), oUBForm.UB04_3b_MedicalHealthRecordNumber.Location, false);
         //   'Type Of Bill
            WriteDataForPrintForm(oUBForm.UB04_4_TypeofBill.Value.ToString(), oUBForm.UB04_4_TypeofBill.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_4_TypeofBillFrequencyCode.Value.ToString(), oUBForm.UB04_4_TypeofBillFrequencyCode.Location, false);
          //  'Fedaral Tax Number
            WriteDataForPrintForm(oUBForm.UB04_5_FederalTaxNumber_Upperline.Value.ToString(), oUBForm.UB04_5_FederalTaxNumber_Upperline.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_5_FederalTaxNumber_Lowerline.Value.ToString(), oUBForm.UB04_5_FederalTaxNumber_Lowerline.Location, false);
           // 'Statement Covers Period(From - Through)

            WriteDataForPrintForm(oUBForm.UB04_6a_StatementCoversPeriod_From.Value.ToString(), oUBForm.UB04_6a_StatementCoversPeriod_From.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_6b_StatementCoversPeriod_Through.Value.ToString(), oUBForm.UB04_6b_StatementCoversPeriod_Through.Location, false);
         //   'Reserved

            WriteDataForPrintForm(oUBForm.UB04_7a_ReservedFL07A.Value.ToString(), oUBForm.UB04_7a_ReservedFL07A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_7b_ReservedFL07B.Value.ToString(), oUBForm.UB04_7b_ReservedFL07B.Location, false);
           // 'Patient Identifier ,Patient Social Security Number

           // WriteDataForPrintForm(oUBForm.UB04_8_PatientIdentifier.Value.ToString(), oUBForm.UB04_8_PatientIdentifier.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_8a_PatientSocialSecurityNumber.Value.ToString(), oUBForm.UB04_8a_PatientSocialSecurityNumber.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_8b_PatientName.Value.ToString(), oUBForm.UB04_8b_PatientName.Location, false);
            //'Patient Address

            WriteDataForPrintForm(oUBForm.UB04_9a_PatientStreetAddress.Value.ToString(), oUBForm.UB04_9a_PatientStreetAddress.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_9b_PatientCity.Value.ToString(), oUBForm.UB04_9b_PatientCity.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_9c_PatientState.Value.ToString(), oUBForm.UB04_9c_PatientState.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_9d_PatientZip.Value.ToString(), oUBForm.UB04_9d_PatientZip.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_9e_PatientCountryCode.Value.ToString(), oUBForm.UB04_9e_PatientCountryCode.Location, false);
           // 'Birth Date

            WriteDataForPrintForm(oUBForm.UB04_10_PatientBirthDate.Value.ToString(), oUBForm.UB04_10_PatientBirthDate.Location, false);
          //  'Gender Admission Date,Admission(Hour),Admission(Type),Admission(Source),Discharge(Hour),Discharge(Status)

            WriteDataForPrintForm(oUBForm.UB04_11_PatientGender.Value.ToString(), oUBForm.UB04_11_PatientGender.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_11_PatientMaritalStatus.Value.ToString(), oUBForm.UB04_11_PatientMaritalStatus.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_11_PatientRace.Value.ToString(), oUBForm.UB04_11_PatientRace.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_12_Admission_Visit_StartofCareDate.Value.ToString(), oUBForm.UB04_12_Admission_Visit_StartofCareDate.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_13_Admission_Visit_Hour.Value.ToString(), oUBForm.UB04_13_Admission_Visit_Hour.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_14_AdmissionType.Value.ToString(), oUBForm.UB04_14_AdmissionType.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_15_ReferralSource.Value.ToString(), oUBForm.UB04_15_ReferralSource.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_16_DischargeHour.Value.ToString(), oUBForm.UB04_16_DischargeHour.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_17_DischargeStatus.Value.ToString(), oUBForm.UB04_17_DischargeStatus.Location, false);
           // 'Condition Code

            WriteDataForPrintForm(oUBForm.UB04_18_ConditionCodes.Value.ToString(), oUBForm.UB04_18_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_19_ConditionCodes.Value.ToString(), oUBForm.UB04_19_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_20_ConditionCodes.Value.ToString(), oUBForm.UB04_20_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_21_ConditionCodes.Value.ToString(), oUBForm.UB04_21_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_22_ConditionCodes.Value.ToString(), oUBForm.UB04_22_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_23_ConditionCodes.Value.ToString(), oUBForm.UB04_23_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_24_ConditionCodes.Value.ToString(), oUBForm.UB04_24_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_25_ConditionCodes.Value.ToString(), oUBForm.UB04_25_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_26_ConditionCodes.Value.ToString(), oUBForm.UB04_26_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_27_ConditionCodes.Value.ToString(), oUBForm.UB04_27_ConditionCodes.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_28_ConditionCodes.Value.ToString(), oUBForm.UB04_28_ConditionCodes.Location, false);
          ///  'Accident State;
            WriteDataForPrintForm(oUBForm.UB04_29_AccidentState.Value.ToString(), oUBForm.UB04_29_AccidentState.Location, false);
           // 'Reserved
            WriteDataForPrintForm(oUBForm.UB04_30_ReservedFL30A.Value.ToString(), oUBForm.UB04_30_ReservedFL30A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_30_ReservedFL30B.Value.ToString(), oUBForm.UB04_30_ReservedFL30B.Location, false);
           // 'Occurrence Code Details;
             
            
                
            WriteDataForPrintForm(oUBForm.UB04_31a_OccurrenceCode.Value.ToString(), oUBForm.UB04_31a_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_31a_OccurrenceDate.Value.ToString(), oUBForm.UB04_31a_OccurrenceDate.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_31b_OccurrenceCode.Value.ToString(), oUBForm.UB04_31b_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_31b_OccurrenceDate.Value.ToString(), oUBForm.UB04_31b_OccurrenceDate.Location, false);


            WriteDataForPrintForm(oUBForm.UB04_32a_OccurrenceCode.Value.ToString(), oUBForm.UB04_32a_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_32a_OccurrenceDate.Value.ToString(), oUBForm.UB04_32a_OccurrenceDate.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_32b_OccurrenceCode.Value.ToString(), oUBForm.UB04_32b_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_32b_OccurrenceDate.Value.ToString(), oUBForm.UB04_32b_OccurrenceDate.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_33a_OccurrenceCode.Value.ToString(), oUBForm.UB04_33a_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_33a_OccurrenceDate.Value.ToString(), oUBForm.UB04_33a_OccurrenceDate.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_33b_OccurrenceCode.Value.ToString(), oUBForm.UB04_33b_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_33b_OccurrenceDate.Value.ToString(), oUBForm.UB04_33b_OccurrenceDate.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_34a_OccurrenceCode.Value.ToString(), oUBForm.UB04_34a_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_34a_OccurrenceDate.Value.ToString(), oUBForm.UB04_34a_OccurrenceDate.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_34b_OccurrenceCode.Value.ToString(), oUBForm.UB04_34b_OccurrenceCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_34b_OccurrenceDate.Value.ToString(), oUBForm.UB04_34b_OccurrenceDate.Location, false);

            //WriteDataForPrintForm(oUBForm.UB04_31b_OccurrenceCode.Value.ToString(), oUBForm.UB04_31b_OccurrenceCode.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_31b_OccurrenceDate.Value.ToString(), oUBForm.UB04_31b_OccurrenceDate.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_32a_OccurrenceCode.Value.ToString(), oUBForm.UB04_32a_OccurrenceCode.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_32b_OccurrenceDate.Value.ToString(), oUBForm.UB04_32b_OccurrenceDate.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_33a_OccurrenceCode.Value.ToString(), oUBForm.UB04_33a_OccurrenceCode.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_33b_OccurrenceDate.Value.ToString(), oUBForm.UB04_33b_OccurrenceDate.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_34a_OccurrenceCode.Value.ToString(), oUBForm.UB04_34a_OccurrenceCode.Location, false);
            //WriteDataForPrintForm(oUBForm.UB04_34_OccurrenceDate.Value.ToString(), oUBForm.UB04_34_OccurrenceDate.Location, false);


            WriteDataForPrintForm(oUBForm.UB04_35a_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_35a_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanDateFrom.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_35a_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanDateThrough.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_35b_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_35b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanDateFrom.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_35b_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanDateThrough.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_36a_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_36a_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanDateFrom.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_36a_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanDateThrough.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_36b_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_36b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateFrom.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_36b_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateThrough.Location, false);
           // 'Future Use
    //        WriteDataForPrintForm(oUBForm.UB04_37_ReservedFL37.Value.ToString(), oUBForm.UB04_37_ReservedFL37.Location, false);
           // 'Responsible Party Name/Address
            WriteDataForPrintForm(oUBForm.UB04_38_ResponsiblePartyNameAddress.Value.ToString(), oUBForm.UB04_38_ResponsiblePartyNameAddress.Location, false);
           // 'Value Code,Value Code Amount
            WriteDataForPrintForm(oUBForm.UB04_39a_ValueCode.Value.ToString(), oUBForm.UB04_39a_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39a_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39a_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39b_ValueCode.Value.ToString(), oUBForm.UB04_39b_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39b_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39b_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39c_ValueCode.Value.ToString(), oUBForm.UB04_39c_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39c_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39c_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39d_ValueCode.Value.ToString(), oUBForm.UB04_39d_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39d_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_39d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39d_ValueCodeAmount_Cents.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_40a_ValueCode.Value.ToString(), oUBForm.UB04_40a_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40a_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40a_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40b_ValueCode.Value.ToString(), oUBForm.UB04_40b_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40b_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40b_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40c_ValueCode.Value.ToString(), oUBForm.UB04_40c_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40c_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40c_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40d_ValueCode.Value.ToString(), oUBForm.UB04_40d_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40d_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_40d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40d_ValueCodeAmount_Cents.Location, false);


            WriteDataForPrintForm(oUBForm.UB04_41a_ValueCode.Value.ToString(), oUBForm.UB04_41a_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41a_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41a_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41b_ValueCode.Value.ToString(), oUBForm.UB04_41b_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41b_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41b_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41c_ValueCode.Value.ToString(), oUBForm.UB04_41c_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41c_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41c_ValueCodeAmount_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41d_ValueCode.Value.ToString(), oUBForm.UB04_41d_ValueCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41d_ValueCodeAmount.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_41d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41d_ValueCodeAmount_Cents.Location, false);
         // 'Revenue Code

           
            _Height = 100;

            if ((oUBForm.UB04_ServiceLines != null))
            {
                for (int index = 0; index <(Convert.ToInt32(oUBForm.UB04_ServiceLines.innerlist.Count) - 1); index++)
                {
		            if ((index > 0)) {
			            _Height = oUBForm.UB04_ServiceLines[index].UB04_42_RevenueCode.Location.Y;
		            }
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_42_RevenueCode.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_42_RevenueCode.Location, false);
                    if (oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value.Length  > 36)
                    {
                        oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value = oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value.Substring(0, 36);
                    }
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Location, false);
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_44_RateCodes.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_44_RateCodes.Location, false);
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_45_ServiceDate_visit_.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_45_ServiceDate_visit_.Location, false);
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_46_ServiceUnits.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_46_ServiceUnits.Location, false);
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_47a_TotalCharges_Dollars.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_47a_TotalCharges_Dollars.Location, false);
                    WriteDataForPrintForm(oUBForm.UB04_ServiceLines[index].UB04_47b_TotalCharges_Cents.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_47b_TotalCharges_Cents.Location, false);
                }
            }

           
         //   WriteDataForPrintForm(oUBForm.UB04_42_RevenueCode.Value.ToString(), oUBForm.UB04_42_RevenueCode.Location, false);
         //   WriteDataForPrintForm(oUBForm.UB04_43_RevenueCodeDescription.Value.ToString(), oUBForm.UB04_43_RevenueCodeDescription.Location, false);
         //   WriteDataForPrintForm(oUBForm.UB04_44_RateCodes.Value.ToString(), oUBForm.UB04_44_RateCodes.Location, false);
         ////   WriteDataForPrintForm(oUBForm.UB04_45_ServiceDate_visit.Value.ToString(), oUBForm.UB04_45_ServiceDate_visit.Location, false);
         //   WriteDataForPrintForm(oUBForm.UB04_46_ServiceUnits.Value.ToString(), oUBForm.UB04_46_ServiceUnits.Location, false);
         //   WriteDataForPrintForm(oUBForm.UB04_47a_TotalCharges_Dollars.Value.ToString(), oUBForm.UB04_47a_TotalCharges_Dollars.Location, false);
         //   WriteDataForPrintForm(oUBForm.UB04_47b_TotalCharges_Cents.Value.ToString(), oUBForm.UB04_47b_TotalCharges_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_48a_Non_coveredCharges_Dollars.Value.ToString(), oUBForm.UB04_48a_Non_coveredCharges_Dollars.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_48b_Non_coveredCharges_Cents.Value.ToString(), oUBForm.UB04_48b_Non_coveredCharges_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_49_ReservedFL49.Value.ToString(), oUBForm.UB04_49_ReservedFL49.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_42L23_RevenueCode.Value.ToString(), oUBForm.UB04_42L23_RevenueCode.Location, false);
           // oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars = new FormFieldString(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.X - oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString().Length, oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.Y);
            string temp = oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString();
            oUBForm.UB04_47L23_SummaryTotalCharges_Dollars = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.X - (oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString().Length * 16) + 8, oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.Y);           
            oUBForm.UB04_47L23_SummaryTotalCharges_Dollars = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.X - oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString().Length, oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.Y);
            oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString(), oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_47L23_SummaryTotalCharges_Cents.Value.ToString(), oUBForm.UB04_47L23_SummaryTotalCharges_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars.Value.ToString(), oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_48L23_SummaryNon_coveredCharges_Cents.Value.ToString(), oUBForm.UB04_48L23_SummaryNon_coveredCharges_Cents.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_49L23_Reserved49L23.Value.ToString(), oUBForm.UB04_49L23_Reserved49L23.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_43L23_CurrentPage.Value.ToString(), oUBForm.UB04_43L23_CurrentPage.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_44L23_TotalPages.Value.ToString(), oUBForm.UB04_44L23_TotalPages.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_44L23CreationDate.Value.ToString(), oUBForm.UB04_44L23CreationDate.Location, false);
           // 'Primary,Secondary,Tertiary Payer Name
            WriteDataForPrintForm(oUBForm.UB04_50_PayerName_Primary.Value.ToString(), oUBForm.UB04_50_PayerName_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_50_PayerName_Secondary.Value.ToString(), oUBForm.UB04_50_PayerName_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_50_PayerName_Tertiary.Value.ToString(), oUBForm.UB04_50_PayerName_Tertiary.Location, false);
            //'Helth Plan ID A,B,C,Release of information.,Assignment of benefits.

            WriteDataForPrintForm(oUBForm.UB04_51_HealthPlanIDA.Value.ToString(), oUBForm.UB04_51_HealthPlanIDA.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_51_HealthPlanIDB.Value.ToString(), oUBForm.UB04_51_HealthPlanIDB.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_51_HealthPlanIDC.Value.ToString(), oUBForm.UB04_51_HealthPlanIDC.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_52_InformationRelease_Primary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_52_InformationRelease_Secondary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_52_InformationRelease_Tertiary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Tertiary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_53_BenefitsAssignment_Primary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_53_BenefitsAssignment_Secondary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_53_BenefitsAssignment_Tertiary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Tertiary.Location, false);
           // 'Prior Payment

            temp = oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString();
            oUBForm.UB04_54_PriorPaymentsDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.Y);
            oUBForm.UB04_54_PriorPaymentsDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.X - oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString().Length, oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.Y);
            oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_54_PriorPaymentsCents_Primary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Primary.Location, false);

            temp = oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString();
            oUBForm.UB04_54_PriorPaymentsDollars_Secondary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location.Y);
            oUBForm.UB04_54_PriorPaymentsDollars_Secondary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location.X - oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString().Length, oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location.Y);
            oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_54_PriorPaymentsCents_Secondary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Secondary.Location, false);

            temp = oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString();
            oUBForm.UB04_54_PriorPaymentsDollars_Tertiary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location.Y);
            oUBForm.UB04_54_PriorPaymentsDollars_Tertiary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location.X - oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString().Length, oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location.Y);
            oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_54_PriorPaymentsCents_Tertiary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Tertiary.Location, false);
            //'Estimated amount due.;

            temp = oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString();
            oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location.X - (oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString().Length * 16) + 8, oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location.Y);
            oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location.X - oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString().Length, oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location.Y);
            oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString(), oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_55a_EstimatedAmountDueCents_Primary.Value.ToString(), oUBForm.UB04_55a_EstimatedAmountDueCents_Primary.Location, false);

            temp = oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString();
            oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location.X - (oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString().Length * 16) + 8, oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location.Y);
            oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location.X - oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString().Length, oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location.Y);
            oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString(), oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_55b_EstimatedAmountDueCents_Secondary.Value.ToString(), oUBForm.UB04_55b_EstimatedAmountDueCents_Secondary.Location, false);

            temp = oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString();
            oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location.X - (oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString().Length * 16) + 8, oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location.Y);
            oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location.X - oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString().Length, oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location.Y);
            oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value = temp;
            WriteDataForPrintForm(oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString(), oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_55c_EstimatedAmountDueCents_Tertiary.Value.ToString(), oUBForm.UB04_55c_EstimatedAmountDueCents_Tertiary.Location, false);
           // 'NPI Number
            WriteDataForPrintForm(oUBForm.UB04_56_NationalProviderIdentifier_NPI_.Value.ToString(), oUBForm.UB04_56_NationalProviderIdentifier_NPI_.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_57_OtherProvider_Primary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_57_OtherProvider_Secondary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_57_OtherProvider_Tertiary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Tertiary.Location, false);
           // 'Othere Provider ID,Insured Name Primary,Secondary ,Tertiary,Patient Relationship,Insured Unique ID ,Group Name,Group Number

            WriteDataForPrintForm(oUBForm.UB04_58_InsuredName_Primary.Value.ToString(), oUBForm.UB04_58_InsuredName_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_58_InsuredName_Secondary.Value.ToString(), oUBForm.UB04_58_InsuredName_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_58_InsuredName_Tertiary.Value.ToString(), oUBForm.UB04_58_InsuredName_Tertiary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_59_PatientRelationshipToInsured_Primary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_59_PatientRelationshipToInsured_Secondary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_59_PatientRelationshipToInsured_Tertiary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Tertiary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_60_InsuredUniqueID_Primary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_60_InsuredUniqueID_Secondary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_60_InsuredUniqueID_Tertiary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Tertiary.Location, false);
            if (oUBForm.UB04_61_InsuredGroupName_Primary.Value.Length > 25)
            {
                oUBForm.UB04_61_InsuredGroupName_Primary.Value = oUBForm.UB04_61_InsuredGroupName_Primary.Value.Substring(0, 25);
            }
            WriteDataForPrintForm(oUBForm.UB04_61_InsuredGroupName_Primary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Primary.Location, false);
            if (oUBForm.UB04_61_InsuredGroupName_Secondary.Value.Length > 25)
            {
                oUBForm.UB04_61_InsuredGroupName_Secondary.Value = oUBForm.UB04_61_InsuredGroupName_Secondary.Value.Substring(0, 25);
            }
            WriteDataForPrintForm(oUBForm.UB04_61_InsuredGroupName_Secondary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Secondary.Location, false);
            if (oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.Length > 25)
            {
                oUBForm.UB04_61_InsuredGroupName_Tertiary.Value = oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.Substring(0, 25);
            }
            WriteDataForPrintForm(oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Tertiary.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_62_InsuredGroupNumber_Primary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_62_InsuredGroupNumber_Secondary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_62_InsuredGroupNumber_Tertiary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Tertiary.Location, false);
          //  'Treatment Authorization Code Primary,Secondary,Tertiary,Document Control Number,Employer Name

            WriteDataForPrintForm(oUBForm.UB04_63_TreatmentAuthorizationCode_Primary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_63_TreatmentAuthorizationCode_Secondary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_63_TreatmentAuthorizationCode_Tertiary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Tertiary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_64_DocumentControlNumber_A.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_64_DocumentControlNumber_B.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_64_DocumentControlNumber_C.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_65_EmployerName_Primary.Value.ToString(), oUBForm.UB04_65_EmployerName_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_65_EmployerName_Secondary.Value.ToString(), oUBForm.UB04_65_EmployerName_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_65_EmployerName_Tertiary.Value.ToString(), oUBForm.UB04_65_EmployerName_Tertiary.Location, false);
           // 'ICD Version Indicator,Principal Diagnosis Code,Reserved,Admitting diagnosis.,Patient Visit Reason,PPS(Code),External Cause of Injury Code,Procedure(Code)

            WriteDataForPrintForm(oUBForm.UB04_66_ICDVersionIndicator.Value.ToString(), oUBForm.UB04_66_ICDVersionIndicator.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67_PrincipalDiagnosisCode.Value.ToString(), oUBForm.UB04_67_PrincipalDiagnosisCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67a_OtherDiagnosis_A.Value.ToString(), oUBForm.UB04_67a_OtherDiagnosis_A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67b_OtherDiagnosis_B.Value.ToString(), oUBForm.UB04_67b_OtherDiagnosis_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67c_OtherDiagnosis_C.Value.ToString(), oUBForm.UB04_67c_OtherDiagnosis_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67d_OtherDiagnosis_D.Value.ToString(), oUBForm.UB04_67d_OtherDiagnosis_D.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67e_OtherDiagnosis_E.Value.ToString(), oUBForm.UB04_67e_OtherDiagnosis_E.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67f_OtherDiagnosis_F.Value.ToString(), oUBForm.UB04_67f_OtherDiagnosis_F.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67g_OtherDiagnosis_G.Value.ToString(), oUBForm.UB04_67g_OtherDiagnosis_G.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67h_OtherDiagnosis_H.Value.ToString(), oUBForm.UB04_67h_OtherDiagnosis_H.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67i_OtherDiagnosis_I.Value.ToString(), oUBForm.UB04_67i_OtherDiagnosis_I.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67j_OtherDiagnosis_J.Value.ToString(), oUBForm.UB04_67j_OtherDiagnosis_J.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67k_OtherDiagnosis_K.Value.ToString(), oUBForm.UB04_67k_OtherDiagnosis_K.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67l_OtherDiagnosis_L.Value.ToString(), oUBForm.UB04_67l_OtherDiagnosis_L.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67m_OtherDiagnosis_M.Value.ToString(), oUBForm.UB04_67m_OtherDiagnosis_M.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67n_OtherDiagnosis_N.Value.ToString(), oUBForm.UB04_67n_OtherDiagnosis_N.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67o_OtherDiagnosis_O.Value.ToString(), oUBForm.UB04_67o_OtherDiagnosis_O.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67p_OtherDiagnosis_P.Value.ToString(), oUBForm.UB04_67p_OtherDiagnosis_P.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_67q_OtherDiagnosis_Q.Value.ToString(), oUBForm.UB04_67q_OtherDiagnosis_Q.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_68_Reserved_68A.Value.ToString(), oUBForm.UB04_68_Reserved_68A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_68_Reserved_68B.Value.ToString(), oUBForm.UB04_68_Reserved_68B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_69_AdmittingDiagnosisCode.Value.ToString(), oUBForm.UB04_69_AdmittingDiagnosisCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_70a_PatientVisitReason_A.Value.ToString(), oUBForm.UB04_70a_PatientVisitReason_A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_70b_PatientVisitReason_B.Value.ToString(), oUBForm.UB04_70b_PatientVisitReason_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_70c_PatientVisitReason_C.Value.ToString(), oUBForm.UB04_70c_PatientVisitReason_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_71_PPSCode.Value.ToString(), oUBForm.UB04_71_PPSCode.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_72a_ExternalCauseofInjuryCode_A.Value.ToString(), oUBForm.UB04_72a_ExternalCauseofInjuryCode_A.Location, false); WriteDataForPrintForm(oUBForm.UB04_72b_ExternalCauseofInjuryCode_B.Value.ToString(), oUBForm.UB04_72b_ExternalCauseofInjuryCode_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_72c_ExternalCauseofInjuryCode_C.Value.ToString(), oUBForm.UB04_72c_ExternalCauseofInjuryCode_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_73_ReservedFL73.Value.ToString(), oUBForm.UB04_73_ReservedFL73.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74_ProcedureCode_Principal.Value.ToString(), oUBForm.UB04_74_ProcedureCode_Principal.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74_ProcedureDate_Principal.Value.ToString(), oUBForm.UB04_74_ProcedureDate_Principal.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74a_ProcedureCode_OtherA.Value.ToString(), oUBForm.UB04_74a_ProcedureCode_OtherA.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74a_ProcedureDate_OtherA.Value.ToString(), oUBForm.UB04_74a_ProcedureDate_OtherA.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74b_ProcedureCode_OtherB.Value.ToString(), oUBForm.UB04_74b_ProcedureCode_OtherB.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74b_ProcedureDate_OtherB.Value.ToString(), oUBForm.UB04_74b_ProcedureDate_OtherB.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74c_ProcedureCode_OtherC.Value.ToString(), oUBForm.UB04_74c_ProcedureCode_OtherC.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74c_ProcedureDate_OtherC.Value.ToString(), oUBForm.UB04_74c_ProcedureDate_OtherC.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74d_ProcedureCode_OtherD.Value.ToString(), oUBForm.UB04_74d_ProcedureCode_OtherD.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74d_ProcedureDate_OtherD.Value.ToString(), oUBForm.UB04_74d_ProcedureDate_OtherD.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74e_ProcedureCode_OtherE.Value.ToString(), oUBForm.UB04_74e_ProcedureCode_OtherE.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_74e_ProcedureDate_OtherE.Value.ToString(), oUBForm.UB04_74e_ProcedureDate_OtherE.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_75a_ReservedFL75A.Value.ToString(), oUBForm.UB04_75a_ReservedFL75A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_75b_ReservedFL75B.Value.ToString(), oUBForm.UB04_75b_ReservedFL75B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_75c_ReservedFL75C.Value.ToString(), oUBForm.UB04_75c_ReservedFL75C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_75d_ReservedFL75D.Value.ToString(), oUBForm.UB04_75d_ReservedFL75D.Location, false);
            //''Attending provider and identifiers,Operating provider and identifiers,Other provider name and identifiers.,other provider name and identifiers.


            WriteDataForPrintForm(oUBForm.UB04_76_AttendingNPI.Value.ToString(), oUBForm.UB04_76_AttendingNPI.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_76_AttendingQUAL.Value.ToString(), oUBForm.UB04_76_AttendingQUAL.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_76_AttendingID.Value.ToString(), oUBForm.UB04_76_AttendingID.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_76a_AttendingLast.Value.ToString(), oUBForm.UB04_76a_AttendingLast.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_76b_AttendingFirst.Value.ToString(), oUBForm.UB04_76b_AttendingFirst.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_77_OperatingNPI.Value.ToString(), oUBForm.UB04_77_OperatingNPI.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_77_OperatingQUAL.Value.ToString(), oUBForm.UB04_77_OperatingQUAL.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_77_OperatingID.Value.ToString(), oUBForm.UB04_77_OperatingID.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_77a_OperatingLast.Value.ToString(), oUBForm.UB04_77a_OperatingLast.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_77b_OperatingFirst.Value.ToString(), oUBForm.UB04_77b_OperatingFirst.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_78_OtherNPI.Value.ToString(), oUBForm.UB04_78_OtherNPI.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_78_OtherProvider_QUAL.Value.ToString(), oUBForm.UB04_78_OtherProvider_QUAL.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_78_OtherQUAL.Value.ToString(), oUBForm.UB04_78_OtherQUAL.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_78_OtherID.Value.ToString(), oUBForm.UB04_78_OtherID.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_78_OtherLast.Value.ToString(), oUBForm.UB04_78_OtherLast.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_78_OtherFirst.Value.ToString(), oUBForm.UB04_78_OtherFirst.Location, false);

            WriteDataForPrintForm(oUBForm.UB04_79_OtherNPI.Value.ToString(), oUBForm.UB04_79_OtherNPI.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_79_OtherProvider_QUAL.Value.ToString(), oUBForm.UB04_79_OtherProvider_QUAL.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_79_OtherQUAL.Value.ToString(), oUBForm.UB04_79_OtherQUAL.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_79_OtherID.Value.ToString(), oUBForm.UB04_79_OtherID.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_79_OtherLast.Value.ToString(), oUBForm.UB04_79_OtherLast.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_79_OtherFirst.Value.ToString(), oUBForm.UB04_79_OtherFirst.Location, false);

            WriteDataForPrintForm(oUBForm.PayerCodeA_Primary.Value.ToString(), oUBForm.PayerCodeA_Primary.Location, false);
            WriteDataForPrintForm(oUBForm.PayerCodeB_Secondary.Value.ToString(), oUBForm.PayerCodeB_Secondary.Location, false);
            WriteDataForPrintForm(oUBForm.PayerCodeC_Tertiary.Value.ToString(), oUBForm.PayerCodeC_Tertiary.Location, false);
           // ''Remark
            WriteDataForPrintForm(oUBForm.UB04_80a_Remarks_1.Value.ToString(), oUBForm.UB04_80a_Remarks_1.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_80b_Remarks_2.Value.ToString(), oUBForm.UB04_80b_Remarks_2.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_80c_Remarks_3.Value.ToString(), oUBForm.UB04_80c_Remarks_3.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_80d_Remarks_4.Value.ToString(), oUBForm.UB04_80d_Remarks_4.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81a_Code_Code_QUAL_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_QUAL_A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81a_Code_Code_CODE_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_CODE_A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81a_Code_Code_VALUE_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_VALUE_A.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81b_Code_Code_QUAL_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_QUAL_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81b_Code_Code_CODE_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_CODE_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81b_Code_Code_VALUE_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_VALUE_B.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81c_Code_Code_QUAL_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_QUAL_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81c_Code_Code_CODE_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_CODE_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81c_Code_Code_VALUE_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_VALUE_C.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81d_Code_Code_QUAL_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_QUAL_D.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81d_Code_Code_CODE_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_CODE_D.Location, false);
            WriteDataForPrintForm(oUBForm.UB04_81d_Code_Code_VALUE_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_VALUE_D.Location, false);

                     
            }
            else
            {
                //  ''Provider's Details
                WriteData(oUBForm.UB04_1_ProviderName.Value.ToString(), oUBForm.UB04_1_ProviderName.Location, false);
                WriteData(oUBForm.UB04_1_ProviderAddress.Value.ToString(), oUBForm.UB04_1_ProviderAddress.Location, false);
                WriteData(oUBForm.UB04_1a_ProviderCity.Value.ToString(), oUBForm.UB04_1a_ProviderCity.Location, false);
              //  WriteDataForPrintForm(oUBForm.UB04_1b_ProviderState.Value.ToString(), oUBForm.UB04_1b_ProviderState.Location, false);
              //  WriteDataForPrintForm(oUBForm.UB04_1c_ProviderZipCode.Value.ToString(), oUBForm.UB04_1c_ProviderZipCode.Location, false);
                WriteData(oUBForm.UB04_1b_ProviderFaxNumber.Value.ToString(), oUBForm.UB04_1b_ProviderFaxNumber.Location, false);
                WriteData(oUBForm.UB04_1a_ProviderPhone.Value.ToString(), oUBForm.UB04_1a_ProviderPhone.Location, false);


                // ' Payer's Details
                WriteData(oUBForm.UB04_2_PayToName.Value.ToString(), oUBForm.UB04_2_PayToName.Location, false);
                WriteData(oUBForm.UB04_2_PayToAddress.Value.ToString(), oUBForm.UB04_2_PayToAddress.Location, false);
                WriteData(oUBForm.UB04_2a_Pay_toCity.Value.ToString(), oUBForm.UB04_2a_Pay_toCity.Location, false);
                WriteData(oUBForm.UB04_2b_Pay_toState.Value.ToString(), oUBForm.UB04_2b_Pay_toState.Location, false);
                WriteData(oUBForm.UB04_2a_Pay_toZip.Value.ToString(), oUBForm.UB04_2a_Pay_toZip.Location, false);
                WriteData(oUBForm.UB04_2b_ReservedFL02.Value.ToString(), oUBForm.UB04_2b_ReservedFL02.Location, false);

                //  'Patient Control Number
                WriteData(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, false);           // 'WriteDataForPrintForm(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, false)
                //  'WriteDataForPrintForm(oUBForm.UB04_3a_PatientControlNumber.Value.ToString(), oUBForm.UB04_3a_PatientControlNumber.Location, false)
                WriteData(oUBForm.UB04_3b_MedicalHealthRecordNumber.Value.ToString(), oUBForm.UB04_3b_MedicalHealthRecordNumber.Location, false);
                //   'Type Of Bill
                WriteData(oUBForm.UB04_4_TypeofBill.Value.ToString(), oUBForm.UB04_4_TypeofBill.Location, false);
                WriteData(oUBForm.UB04_4_TypeofBillFrequencyCode.Value.ToString(), oUBForm.UB04_4_TypeofBillFrequencyCode.Location, false);
                //  'Fedaral Tax Number
                WriteData(oUBForm.UB04_5_FederalTaxNumber_Upperline.Value.ToString(), oUBForm.UB04_5_FederalTaxNumber_Upperline.Location, false);
                WriteData(oUBForm.UB04_5_FederalTaxNumber_Lowerline.Value.ToString(), oUBForm.UB04_5_FederalTaxNumber_Lowerline.Location, false);
                // 'Statement Covers Period(From - Through)

                WriteData(oUBForm.UB04_6a_StatementCoversPeriod_From.Value.ToString(), oUBForm.UB04_6a_StatementCoversPeriod_From.Location, false);
                WriteData(oUBForm.UB04_6b_StatementCoversPeriod_Through.Value.ToString(), oUBForm.UB04_6b_StatementCoversPeriod_Through.Location, false);
                //   'Reserved

                WriteData(oUBForm.UB04_7a_ReservedFL07A.Value.ToString(), oUBForm.UB04_7a_ReservedFL07A.Location, false);
                WriteData(oUBForm.UB04_7b_ReservedFL07B.Value.ToString(), oUBForm.UB04_7b_ReservedFL07B.Location, false);
                // 'Patient Identifier ,Patient Social Security Number

               // WriteDataForPrintForm(oUBForm.UB04_8_PatientIdentifier.Value.ToString(), oUBForm.UB04_8_PatientIdentifier.Location, false);
                WriteData(oUBForm.UB04_8a_PatientSocialSecurityNumber.Value.ToString(), oUBForm.UB04_8a_PatientSocialSecurityNumber.Location, false);
                WriteData(oUBForm.UB04_8b_PatientName.Value.ToString(), oUBForm.UB04_8b_PatientName.Location, false);
                //'Patient Address

                WriteData(oUBForm.UB04_9a_PatientStreetAddress.Value.ToString(), oUBForm.UB04_9a_PatientStreetAddress.Location, false);
                WriteData(oUBForm.UB04_9b_PatientCity.Value.ToString(), oUBForm.UB04_9b_PatientCity.Location, false);
                WriteData(oUBForm.UB04_9c_PatientState.Value.ToString(), oUBForm.UB04_9c_PatientState.Location, false);
                WriteData(oUBForm.UB04_9d_PatientZip.Value.ToString(), oUBForm.UB04_9d_PatientZip.Location, false);
                WriteData(oUBForm.UB04_9e_PatientCountryCode.Value.ToString(), oUBForm.UB04_9e_PatientCountryCode.Location, false);
                // 'Birth Date

                WriteData(oUBForm.UB04_10_PatientBirthDate.Value.ToString(), oUBForm.UB04_10_PatientBirthDate.Location, false);
                //  'Gender Admission Date,Admission(Hour),Admission(Type),Admission(Source),Discharge(Hour),Discharge(Status)

                WriteData(oUBForm.UB04_11_PatientGender.Value.ToString(), oUBForm.UB04_11_PatientGender.Location, false);
                WriteData(oUBForm.UB04_11_PatientMaritalStatus.Value.ToString(), oUBForm.UB04_11_PatientMaritalStatus.Location, false);
                WriteData(oUBForm.UB04_11_PatientRace.Value.ToString(), oUBForm.UB04_11_PatientRace.Location, false);
                WriteData(oUBForm.UB04_12_Admission_Visit_StartofCareDate.Value.ToString(), oUBForm.UB04_12_Admission_Visit_StartofCareDate.Location, false);
                WriteData(oUBForm.UB04_13_Admission_Visit_Hour.Value.ToString(), oUBForm.UB04_13_Admission_Visit_Hour.Location, false);
                WriteData(oUBForm.UB04_14_AdmissionType.Value.ToString(), oUBForm.UB04_14_AdmissionType.Location, false);
                WriteData(oUBForm.UB04_15_ReferralSource.Value.ToString(), oUBForm.UB04_15_ReferralSource.Location, false);
                WriteData(oUBForm.UB04_16_DischargeHour.Value.ToString(), oUBForm.UB04_16_DischargeHour.Location, false);
                WriteData(oUBForm.UB04_17_DischargeStatus.Value.ToString(), oUBForm.UB04_17_DischargeStatus.Location, false);
                // 'Condition Code

                WriteData(oUBForm.UB04_18_ConditionCodes.Value.ToString(), oUBForm.UB04_18_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_19_ConditionCodes.Value.ToString(), oUBForm.UB04_19_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_20_ConditionCodes.Value.ToString(), oUBForm.UB04_20_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_21_ConditionCodes.Value.ToString(), oUBForm.UB04_21_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_22_ConditionCodes.Value.ToString(), oUBForm.UB04_22_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_23_ConditionCodes.Value.ToString(), oUBForm.UB04_23_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_24_ConditionCodes.Value.ToString(), oUBForm.UB04_24_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_25_ConditionCodes.Value.ToString(), oUBForm.UB04_25_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_26_ConditionCodes.Value.ToString(), oUBForm.UB04_26_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_27_ConditionCodes.Value.ToString(), oUBForm.UB04_27_ConditionCodes.Location, false);
                WriteData(oUBForm.UB04_28_ConditionCodes.Value.ToString(), oUBForm.UB04_28_ConditionCodes.Location, false);
                ///  'Accident State;
                WriteData(oUBForm.UB04_29_AccidentState.Value.ToString(), oUBForm.UB04_29_AccidentState.Location, false);
                // 'Reserved
                WriteData(oUBForm.UB04_30_ReservedFL30A.Value.ToString(), oUBForm.UB04_30_ReservedFL30A.Location, false);
                WriteData(oUBForm.UB04_30_ReservedFL30B.Value.ToString(), oUBForm.UB04_30_ReservedFL30B.Location, false);
                // 'Occurrence Code Details;

                WriteData(oUBForm.UB04_31a_OccurrenceCode.Value.ToString(), oUBForm.UB04_31a_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_31a_OccurrenceDate.Value.ToString(), oUBForm.UB04_31a_OccurrenceDate.Location, false);
                WriteData(oUBForm.UB04_31b_OccurrenceCode.Value.ToString(), oUBForm.UB04_31b_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_31b_OccurrenceDate.Value.ToString(), oUBForm.UB04_31b_OccurrenceDate.Location, false);


                WriteData(oUBForm.UB04_32a_OccurrenceCode.Value.ToString(), oUBForm.UB04_32a_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_32a_OccurrenceDate.Value.ToString(), oUBForm.UB04_32a_OccurrenceDate.Location, false);
                WriteData(oUBForm.UB04_32b_OccurrenceCode.Value.ToString(), oUBForm.UB04_32b_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_32b_OccurrenceDate.Value.ToString(), oUBForm.UB04_32b_OccurrenceDate.Location, false);

                WriteData(oUBForm.UB04_33a_OccurrenceCode.Value.ToString(), oUBForm.UB04_33a_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_33a_OccurrenceDate.Value.ToString(), oUBForm.UB04_33a_OccurrenceDate.Location, false);
                WriteData(oUBForm.UB04_33b_OccurrenceCode.Value.ToString(), oUBForm.UB04_33b_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_33b_OccurrenceDate.Value.ToString(), oUBForm.UB04_33b_OccurrenceDate.Location, false);

                WriteData(oUBForm.UB04_34a_OccurrenceCode.Value.ToString(), oUBForm.UB04_34a_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_34a_OccurrenceDate.Value.ToString(), oUBForm.UB04_34a_OccurrenceDate.Location, false);
                WriteData(oUBForm.UB04_34b_OccurrenceCode.Value.ToString(), oUBForm.UB04_34b_OccurrenceCode.Location, false);
                WriteData(oUBForm.UB04_34b_OccurrenceDate.Value.ToString(), oUBForm.UB04_34b_OccurrenceDate.Location, false);

                //WriteDataForPrintForm(oUBForm.UB04_31b_OccurrenceCode.Value.ToString(), oUBForm.UB04_31b_OccurrenceCode.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_31b_OccurrenceDate.Value.ToString(), oUBForm.UB04_31b_OccurrenceDate.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_32a_OccurrenceCode.Value.ToString(), oUBForm.UB04_32a_OccurrenceCode.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_32b_OccurrenceDate.Value.ToString(), oUBForm.UB04_32b_OccurrenceDate.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_33a_OccurrenceCode.Value.ToString(), oUBForm.UB04_33a_OccurrenceCode.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_33b_OccurrenceDate.Value.ToString(), oUBForm.UB04_33b_OccurrenceDate.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_34a_OccurrenceCode.Value.ToString(), oUBForm.UB04_34a_OccurrenceCode.Location, false);
                //WriteDataForPrintForm(oUBForm.UB04_34_OccurrenceDate.Value.ToString(), oUBForm.UB04_34_OccurrenceDate.Location, false);


                WriteData(oUBForm.UB04_35a_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanCode.Location, false);
                WriteData(oUBForm.UB04_35a_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanDateFrom.Location, false);
                WriteData(oUBForm.UB04_35a_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35a_OccurrenceSpanDateThrough.Location, false);
                WriteData(oUBForm.UB04_35b_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanCode.Location, false);
                WriteData(oUBForm.UB04_35b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanDateFrom.Location, false);
                WriteData(oUBForm.UB04_35b_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35b_OccurrenceSpanDateThrough.Location, false);

                WriteData(oUBForm.UB04_36a_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanCode.Location, false);
                WriteData(oUBForm.UB04_36a_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanDateFrom.Location, false);
                WriteData(oUBForm.UB04_36a_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36a_OccurrenceSpanDateThrough.Location, false);
                WriteData(oUBForm.UB04_36b_OccurrenceSpanCode.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanCode.Location, false);
                WriteData(oUBForm.UB04_36b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateFrom.Location, false);
                WriteData(oUBForm.UB04_36b_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateThrough.Location, false);
              //  WriteData(oUBForm.UB04_34_OccurrenceDate.Value.ToString(), oUBForm.UB04_34_OccurrenceDate.Location, false);
               
                //WriteData(oUBForm.UB04_35c_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_35c_OccurrenceSpanDateThrough.Location, false);
                //WriteData(oUBForm.UB04_36a_OccurrenceCode.Value.ToString(), oUBForm.UB04_36a_OccurrenceCode.Location, false);
                //WriteData(oUBForm.UB04_36b_OccurrenceSpanDateFrom.Value.ToString(), oUBForm.UB04_36b_OccurrenceSpanDateFrom.Location, false);
                //WriteData(oUBForm.UB04_36c_OccurrenceSpanDateThrough.Value.ToString(), oUBForm.UB04_36c_OccurrenceSpanDateThrough.Location, false);
                //// 'Future Use
                //        WriteData(oUBForm.UB04_37_ReservedFL37.Value.ToString(), oUBForm.UB04_37_ReservedFL37.Location, false);
                // 'Responsible Party Name/Address
                WriteData(oUBForm.UB04_38_ResponsiblePartyNameAddress.Value.ToString(), oUBForm.UB04_38_ResponsiblePartyNameAddress.Location, false);
                // 'Value Code,Value Code Amount
                WriteData(oUBForm.UB04_39a_ValueCode.Value.ToString(), oUBForm.UB04_39a_ValueCode.Location, false);
                WriteData(oUBForm.UB04_39a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39a_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_39a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39a_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_39b_ValueCode.Value.ToString(), oUBForm.UB04_39b_ValueCode.Location, false);
                WriteData(oUBForm.UB04_39b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39b_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_39b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39b_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_39c_ValueCode.Value.ToString(), oUBForm.UB04_39c_ValueCode.Location, false);
                WriteData(oUBForm.UB04_39c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39c_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_39c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39c_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_39d_ValueCode.Value.ToString(), oUBForm.UB04_39d_ValueCode.Location, false);
                WriteData(oUBForm.UB04_39d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_39d_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_39d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_39d_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_40a_ValueCode.Value.ToString(), oUBForm.UB04_40a_ValueCode.Location, false);
                WriteData(oUBForm.UB04_40a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40a_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_40a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40a_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_40b_ValueCode.Value.ToString(), oUBForm.UB04_40b_ValueCode.Location, false);
                WriteData(oUBForm.UB04_40b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40b_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_40b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40b_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_40c_ValueCode.Value.ToString(), oUBForm.UB04_40c_ValueCode.Location, false);
                WriteData(oUBForm.UB04_40c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40c_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_40c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40c_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_40d_ValueCode.Value.ToString(), oUBForm.UB04_40d_ValueCode.Location, false);
                WriteData(oUBForm.UB04_40d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_40d_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_40d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_40d_ValueCodeAmount_Cents.Location, false);


                WriteData(oUBForm.UB04_41a_ValueCode.Value.ToString(), oUBForm.UB04_41a_ValueCode.Location, false);
                WriteData(oUBForm.UB04_41a_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41a_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_41a_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41a_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_41b_ValueCode.Value.ToString(), oUBForm.UB04_41b_ValueCode.Location, false);
                WriteData(oUBForm.UB04_41b_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41b_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_41b_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41b_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_41c_ValueCode.Value.ToString(), oUBForm.UB04_41c_ValueCode.Location, false);
                WriteData(oUBForm.UB04_41c_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41c_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_41c_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41c_ValueCodeAmount_Cents.Location, false);

                WriteData(oUBForm.UB04_41d_ValueCode.Value.ToString(), oUBForm.UB04_41d_ValueCode.Location, false);
                WriteData(oUBForm.UB04_41d_ValueCodeAmount.Value.ToString(), oUBForm.UB04_41d_ValueCodeAmount.Location, false);
                WriteData(oUBForm.UB04_41d_ValueCodeAmount_Cents.Value.ToString(), oUBForm.UB04_41d_ValueCodeAmount_Cents.Location, false);
                // 'Revenue Code

                _Height = 100;

                if ((oUBForm.UB04_ServiceLines != null))
                {
                    for (int index = 0; index <(Convert.ToInt32(oUBForm.UB04_ServiceLines.innerlist.Count) - 1); index++)
                    {
                        if ((index > 0))
                        {
                            _Height = oUBForm.UB04_ServiceLines[index].UB04_42_RevenueCode.Location.Y;
                        }
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_42_RevenueCode.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_42_RevenueCode.Location, false);
                        if (oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value.Length > 40)
                        {
                            oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value = oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value.Substring(0, 40);
                        }
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_43_RevenueCodeDescription.Location, false);
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_44_RateCodes.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_44_RateCodes.Location, false);
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_45_ServiceDate_visit_.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_45_ServiceDate_visit_.Location, false);
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_46_ServiceUnits.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_46_ServiceUnits.Location, false);
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_47a_TotalCharges_Dollars.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_47a_TotalCharges_Dollars.Location, false);
                        WriteData(oUBForm.UB04_ServiceLines[index].UB04_47b_TotalCharges_Cents.Value.ToString(), oUBForm.UB04_ServiceLines[index].UB04_47b_TotalCharges_Cents.Location, false);
                    }
                }


                //WriteData(oUBForm.UB04_42_RevenueCode.Value.ToString(), oUBForm.UB04_42_RevenueCode.Location, false);
                //WriteData(oUBForm.UB04_43_RevenueCodeDescription.Value.ToString(), oUBForm.UB04_43_RevenueCodeDescription.Location, false);
                //WriteData(oUBForm.UB04_44_RateCodes.Value.ToString(), oUBForm.UB04_44_RateCodes.Location, false);
                ////   WriteData(oUBForm.UB04_45_ServiceDate_visit.Value.ToString(), oUBForm.UB04_45_ServiceDate_visit.Location, false);
                //WriteData(oUBForm.UB04_46_ServiceUnits.Value.ToString(), oUBForm.UB04_46_ServiceUnits.Location, false);
                //WriteData(oUBForm.UB04_47a_TotalCharges_Dollars.Value.ToString(), oUBForm.UB04_47a_TotalCharges_Dollars.Location, false);
                //WriteData(oUBForm.UB04_47b_TotalCharges_Cents.Value.ToString(), oUBForm.UB04_47b_TotalCharges_Cents.Location, false);
                WriteData(oUBForm.UB04_48a_Non_coveredCharges_Dollars.Value.ToString(), oUBForm.UB04_48a_Non_coveredCharges_Dollars.Location, false);
                WriteData(oUBForm.UB04_48b_Non_coveredCharges_Cents.Value.ToString(), oUBForm.UB04_48b_Non_coveredCharges_Cents.Location, false);
                WriteData(oUBForm.UB04_49_ReservedFL49.Value.ToString(), oUBForm.UB04_49_ReservedFL49.Location, false);
                WriteData(oUBForm.UB04_42L23_RevenueCode.Value.ToString(), oUBForm.UB04_42L23_RevenueCode.Location, false);
                string temp = oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString();
                oUBForm.UB04_47L23_SummaryTotalCharges_Dollars =new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.X - (oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString().Length * 16)+8, oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location.Y);
                oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value = temp;
                WriteData(oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Value.ToString(), oUBForm.UB04_47L23_SummaryTotalCharges_Dollars.Location, false);
                WriteData(oUBForm.UB04_47L23_SummaryTotalCharges_Cents.Value.ToString(), oUBForm.UB04_47L23_SummaryTotalCharges_Cents.Location, false);
                WriteData(oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars.Value.ToString(), oUBForm.UB04_48L23_SummaryNon_coveredCharges_Dollars.Location, false);
                WriteData(oUBForm.UB04_48L23_SummaryNon_coveredCharges_Cents.Value.ToString(), oUBForm.UB04_48L23_SummaryNon_coveredCharges_Cents.Location, false);
                WriteData(oUBForm.UB04_49L23_Reserved49L23.Value.ToString(), oUBForm.UB04_49L23_Reserved49L23.Location, false);
                WriteData(oUBForm.UB04_43L23_CurrentPage.Value.ToString(), oUBForm.UB04_43L23_CurrentPage.Location, false);
                WriteData(oUBForm.UB04_44L23_TotalPages.Value.ToString(), oUBForm.UB04_44L23_TotalPages.Location, false);
                WriteData(oUBForm.UB04_44L23CreationDate.Value.ToString(), oUBForm.UB04_44L23CreationDate.Location, false);
                // 'Primary,Secondary,Tertiary Payer Name
                WriteData(oUBForm.UB04_50_PayerName_Primary.Value.ToString(), oUBForm.UB04_50_PayerName_Primary.Location, false);
                WriteData(oUBForm.UB04_50_PayerName_Secondary.Value.ToString(), oUBForm.UB04_50_PayerName_Secondary.Location, false);
                WriteData(oUBForm.UB04_50_PayerName_Tertiary.Value.ToString(), oUBForm.UB04_50_PayerName_Tertiary.Location, false);
                //'Helth Plan ID A,B,C,Release of information.,Assignment of benefits.

                WriteData(oUBForm.UB04_51_HealthPlanIDA.Value.ToString(), oUBForm.UB04_51_HealthPlanIDA.Location, false);
                WriteData(oUBForm.UB04_51_HealthPlanIDB.Value.ToString(), oUBForm.UB04_51_HealthPlanIDB.Location, false);
                WriteData(oUBForm.UB04_51_HealthPlanIDC.Value.ToString(), oUBForm.UB04_51_HealthPlanIDC.Location, false);
                WriteData(oUBForm.UB04_52_InformationRelease_Primary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Primary.Location, false);
                WriteData(oUBForm.UB04_52_InformationRelease_Secondary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Secondary.Location, false);
                WriteData(oUBForm.UB04_52_InformationRelease_Tertiary.Value.ToString(), oUBForm.UB04_52_InformationRelease_Tertiary.Location, false);
                WriteData(oUBForm.UB04_53_BenefitsAssignment_Primary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Primary.Location, false);
                WriteData(oUBForm.UB04_53_BenefitsAssignment_Secondary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Secondary.Location, false);
                WriteData(oUBForm.UB04_53_BenefitsAssignment_Tertiary.Value.ToString(), oUBForm.UB04_53_BenefitsAssignment_Tertiary.Location, false);
                // 'Prior Payment

                //temp = oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString();
                //oUBForm.UB04_54_PriorPaymentsDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.Y);
                //oUBForm.UB04_54_PriorPaymentsDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.X - oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString().Length, oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.Y);
                //oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value = temp;

                temp = oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString();
                oUBForm.UB04_54_PriorPaymentsDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location.Y);
                oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value = temp;

                WriteData(oUBForm.UB04_54_PriorPaymentsDollars_Primary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Primary.Location, false);
                WriteData(oUBForm.UB04_54_PriorPaymentsCents_Primary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Primary.Location, false);

                temp = oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString();
                oUBForm.UB04_54_PriorPaymentsDollars_Secondary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location.Y);
                oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value = temp;
                WriteData(oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Secondary.Location, false);
                WriteData(oUBForm.UB04_54_PriorPaymentsCents_Secondary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Secondary.Location, false);

                temp = oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString();
                oUBForm.UB04_54_PriorPaymentsDollars_Tertiary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location.X - (oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString().Length * 16) + 8, oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location.Y);
                oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value = temp;
                WriteData(oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsDollars_Tertiary.Location, false);
                WriteData(oUBForm.UB04_54_PriorPaymentsCents_Tertiary.Value.ToString(), oUBForm.UB04_54_PriorPaymentsCents_Tertiary.Location, false);
                //'Estimated amount due.;

                temp = oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString();
                oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location.X - (oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString().Length * 16) + 8, oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location.Y);
                oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value = temp;
                WriteData(oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Value.ToString(), oUBForm.UB04_55a_EstimatedAmountDueDollars_Primary.Location, false);
                WriteData(oUBForm.UB04_55a_EstimatedAmountDueCents_Primary.Value.ToString(), oUBForm.UB04_55a_EstimatedAmountDueCents_Primary.Location, false);

                temp = oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString();
                oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location.X - (oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString().Length * 16) + 8, oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location.Y);
                oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value = temp;
                WriteData(oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Value.ToString(), oUBForm.UB04_55b_EstimatedAmountDueDollars_Secondary.Location, false);
                WriteData(oUBForm.UB04_55b_EstimatedAmountDueCents_Secondary.Value.ToString(), oUBForm.UB04_55b_EstimatedAmountDueCents_Secondary.Location, false);

                temp = oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString();
                oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary = new ClsBL_UB04PaperForm.FormFieldString(oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location.X - (oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString().Length * 16) + 8, oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location.Y);
                oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value = temp;
                WriteData(oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Value.ToString(), oUBForm.UB04_55c_EstimatedAmountDueDollars_Tertiary.Location, false);
                WriteData(oUBForm.UB04_55c_EstimatedAmountDueCents_Tertiary.Value.ToString(), oUBForm.UB04_55c_EstimatedAmountDueCents_Tertiary.Location, false);
                // 'NPI Number
                WriteData(oUBForm.UB04_56_NationalProviderIdentifier_NPI_.Value.ToString(), oUBForm.UB04_56_NationalProviderIdentifier_NPI_.Location, false);
                WriteData(oUBForm.UB04_57_OtherProvider_Primary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Primary.Location, false);
                WriteData(oUBForm.UB04_57_OtherProvider_Secondary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Secondary.Location, false);
                WriteData(oUBForm.UB04_57_OtherProvider_Tertiary.Value.ToString(), oUBForm.UB04_57_OtherProvider_Tertiary.Location, false);
                // 'Othere Provider ID,Insured Name Primary,Secondary ,Tertiary,Patient Relationship,Insured Unique ID ,Group Name,Group Number

                WriteData(oUBForm.UB04_58_InsuredName_Primary.Value.ToString(), oUBForm.UB04_58_InsuredName_Primary.Location, false);
                WriteData(oUBForm.UB04_58_InsuredName_Secondary.Value.ToString(), oUBForm.UB04_58_InsuredName_Secondary.Location, false);
                WriteData(oUBForm.UB04_58_InsuredName_Tertiary.Value.ToString(), oUBForm.UB04_58_InsuredName_Tertiary.Location, false);
                WriteData(oUBForm.UB04_59_PatientRelationshipToInsured_Primary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Primary.Location, false);
                WriteData(oUBForm.UB04_59_PatientRelationshipToInsured_Secondary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Secondary.Location, false);
                WriteData(oUBForm.UB04_59_PatientRelationshipToInsured_Tertiary.Value.ToString(), oUBForm.UB04_59_PatientRelationshipToInsured_Tertiary.Location, false);
                WriteData(oUBForm.UB04_60_InsuredUniqueID_Primary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Primary.Location, false);
                WriteData(oUBForm.UB04_60_InsuredUniqueID_Secondary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Secondary.Location, false);
                WriteData(oUBForm.UB04_60_InsuredUniqueID_Tertiary.Value.ToString(), oUBForm.UB04_60_InsuredUniqueID_Tertiary.Location, false);
                if (oUBForm.UB04_61_InsuredGroupName_Primary.Value.Length > 30)
                {
                    oUBForm.UB04_61_InsuredGroupName_Primary.Value = oUBForm.UB04_61_InsuredGroupName_Primary.Value.Substring(0, 30);
                }
                WriteData(oUBForm.UB04_61_InsuredGroupName_Primary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Primary.Location, false);
                if (oUBForm.UB04_61_InsuredGroupName_Secondary.Value.Length > 30)
                {
                    oUBForm.UB04_61_InsuredGroupName_Secondary.Value = oUBForm.UB04_61_InsuredGroupName_Secondary.Value.Substring(0, 30);
                }
                WriteData(oUBForm.UB04_61_InsuredGroupName_Secondary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Secondary.Location, false);
                if (oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.Length > 30)
                {
                    oUBForm.UB04_61_InsuredGroupName_Tertiary.Value = oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.Substring(0, 30);
                }             
                WriteData(oUBForm.UB04_61_InsuredGroupName_Tertiary.Value.ToString(), oUBForm.UB04_61_InsuredGroupName_Tertiary.Location, false);

                WriteData(oUBForm.UB04_62_InsuredGroupNumber_Primary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Primary.Location, false);
                WriteData(oUBForm.UB04_62_InsuredGroupNumber_Secondary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Secondary.Location, false);
                WriteData(oUBForm.UB04_62_InsuredGroupNumber_Tertiary.Value.ToString(), oUBForm.UB04_62_InsuredGroupNumber_Tertiary.Location, false);
                //  'Treatment Authorization Code Primary,Secondary,Tertiary,Document Control Number,Employer Name

                WriteData(oUBForm.UB04_63_TreatmentAuthorizationCode_Primary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Primary.Location, false);
                WriteData(oUBForm.UB04_63_TreatmentAuthorizationCode_Secondary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Secondary.Location, false);
                WriteData(oUBForm.UB04_63_TreatmentAuthorizationCode_Tertiary.Value.ToString(), oUBForm.UB04_63_TreatmentAuthorizationCode_Tertiary.Location, false);
                WriteData(oUBForm.UB04_64_DocumentControlNumber_A.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_A.Location, false);
                WriteData(oUBForm.UB04_64_DocumentControlNumber_B.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_B.Location, false);
                WriteData(oUBForm.UB04_64_DocumentControlNumber_C.Value.ToString(), oUBForm.UB04_64_DocumentControlNumber_C.Location, false);
                WriteData(oUBForm.UB04_65_EmployerName_Primary.Value.ToString(), oUBForm.UB04_65_EmployerName_Primary.Location, false);
                WriteData(oUBForm.UB04_65_EmployerName_Secondary.Value.ToString(), oUBForm.UB04_65_EmployerName_Secondary.Location, false);
                WriteData(oUBForm.UB04_65_EmployerName_Tertiary.Value.ToString(), oUBForm.UB04_65_EmployerName_Tertiary.Location, false);
                // 'ICD Version Indicator,Principal Diagnosis Code,Reserved,Admitting diagnosis.,Patient Visit Reason,PPS(Code),External Cause of Injury Code,Procedure(Code)

                WriteData(oUBForm.UB04_66_ICDVersionIndicator.Value.ToString(), oUBForm.UB04_66_ICDVersionIndicator.Location, false);
                WriteData(oUBForm.UB04_67_PrincipalDiagnosisCode.Value.ToString(), oUBForm.UB04_67_PrincipalDiagnosisCode.Location, false);
                WriteData(oUBForm.UB04_67a_OtherDiagnosis_A.Value.ToString(), oUBForm.UB04_67a_OtherDiagnosis_A.Location, false);
                WriteData(oUBForm.UB04_67b_OtherDiagnosis_B.Value.ToString(), oUBForm.UB04_67b_OtherDiagnosis_B.Location, false);
                WriteData(oUBForm.UB04_67c_OtherDiagnosis_C.Value.ToString(), oUBForm.UB04_67c_OtherDiagnosis_C.Location, false);
                WriteData(oUBForm.UB04_67d_OtherDiagnosis_D.Value.ToString(), oUBForm.UB04_67d_OtherDiagnosis_D.Location, false);
                WriteData(oUBForm.UB04_67e_OtherDiagnosis_E.Value.ToString(), oUBForm.UB04_67e_OtherDiagnosis_E.Location, false);
                WriteData(oUBForm.UB04_67f_OtherDiagnosis_F.Value.ToString(), oUBForm.UB04_67f_OtherDiagnosis_F.Location, false);
                WriteData(oUBForm.UB04_67g_OtherDiagnosis_G.Value.ToString(), oUBForm.UB04_67g_OtherDiagnosis_G.Location, false);
                WriteData(oUBForm.UB04_67h_OtherDiagnosis_H.Value.ToString(), oUBForm.UB04_67h_OtherDiagnosis_H.Location, false);
                WriteData(oUBForm.UB04_67i_OtherDiagnosis_I.Value.ToString(), oUBForm.UB04_67i_OtherDiagnosis_I.Location, false);
                WriteData(oUBForm.UB04_67j_OtherDiagnosis_J.Value.ToString(), oUBForm.UB04_67j_OtherDiagnosis_J.Location, false);
                WriteData(oUBForm.UB04_67k_OtherDiagnosis_K.Value.ToString(), oUBForm.UB04_67k_OtherDiagnosis_K.Location, false);
                WriteData(oUBForm.UB04_67l_OtherDiagnosis_L.Value.ToString(), oUBForm.UB04_67l_OtherDiagnosis_L.Location, false);
                WriteData(oUBForm.UB04_67m_OtherDiagnosis_M.Value.ToString(), oUBForm.UB04_67m_OtherDiagnosis_M.Location, false);
                WriteData(oUBForm.UB04_67n_OtherDiagnosis_N.Value.ToString(), oUBForm.UB04_67n_OtherDiagnosis_N.Location, false);
                WriteData(oUBForm.UB04_67o_OtherDiagnosis_O.Value.ToString(), oUBForm.UB04_67o_OtherDiagnosis_O.Location, false);
                WriteData(oUBForm.UB04_67p_OtherDiagnosis_P.Value.ToString(), oUBForm.UB04_67p_OtherDiagnosis_P.Location, false);
                WriteData(oUBForm.UB04_67q_OtherDiagnosis_Q.Value.ToString(), oUBForm.UB04_67q_OtherDiagnosis_Q.Location, false);
                WriteData(oUBForm.UB04_68_Reserved_68A.Value.ToString(), oUBForm.UB04_68_Reserved_68A.Location, false);
                WriteData(oUBForm.UB04_68_Reserved_68B.Value.ToString(), oUBForm.UB04_68_Reserved_68B.Location, false);
                WriteData(oUBForm.UB04_69_AdmittingDiagnosisCode.Value.ToString(), oUBForm.UB04_69_AdmittingDiagnosisCode.Location, false);
                WriteData(oUBForm.UB04_70a_PatientVisitReason_A.Value.ToString(), oUBForm.UB04_70a_PatientVisitReason_A.Location, false);
                WriteData(oUBForm.UB04_70b_PatientVisitReason_B.Value.ToString(), oUBForm.UB04_70b_PatientVisitReason_B.Location, false);
                WriteData(oUBForm.UB04_70c_PatientVisitReason_C.Value.ToString(), oUBForm.UB04_70c_PatientVisitReason_C.Location, false);
                WriteData(oUBForm.UB04_71_PPSCode.Value.ToString(), oUBForm.UB04_71_PPSCode.Location, false);
                WriteData(oUBForm.UB04_72a_ExternalCauseofInjuryCode_A.Value.ToString(), oUBForm.UB04_72a_ExternalCauseofInjuryCode_A.Location, false); WriteData(oUBForm.UB04_72b_ExternalCauseofInjuryCode_B.Value.ToString(), oUBForm.UB04_72b_ExternalCauseofInjuryCode_B.Location, false);
                WriteData(oUBForm.UB04_72c_ExternalCauseofInjuryCode_C.Value.ToString(), oUBForm.UB04_72c_ExternalCauseofInjuryCode_C.Location, false);
                WriteData(oUBForm.UB04_73_ReservedFL73.Value.ToString(), oUBForm.UB04_73_ReservedFL73.Location, false);
                WriteData(oUBForm.UB04_74_ProcedureCode_Principal.Value.ToString(), oUBForm.UB04_74_ProcedureCode_Principal.Location, false);
                WriteData(oUBForm.UB04_74_ProcedureDate_Principal.Value.ToString(), oUBForm.UB04_74_ProcedureDate_Principal.Location, false);
                WriteData(oUBForm.UB04_74a_ProcedureCode_OtherA.Value.ToString(), oUBForm.UB04_74a_ProcedureCode_OtherA.Location, false);
                WriteData(oUBForm.UB04_74a_ProcedureDate_OtherA.Value.ToString(), oUBForm.UB04_74a_ProcedureDate_OtherA.Location, false);
                WriteData(oUBForm.UB04_74b_ProcedureCode_OtherB.Value.ToString(), oUBForm.UB04_74b_ProcedureCode_OtherB.Location, false);
                WriteData(oUBForm.UB04_74b_ProcedureDate_OtherB.Value.ToString(), oUBForm.UB04_74b_ProcedureDate_OtherB.Location, false);
                WriteData(oUBForm.UB04_74c_ProcedureCode_OtherC.Value.ToString(), oUBForm.UB04_74c_ProcedureCode_OtherC.Location, false);
                WriteData(oUBForm.UB04_74c_ProcedureDate_OtherC.Value.ToString(), oUBForm.UB04_74c_ProcedureDate_OtherC.Location, false);
                WriteData(oUBForm.UB04_74d_ProcedureCode_OtherD.Value.ToString(), oUBForm.UB04_74d_ProcedureCode_OtherD.Location, false);
                WriteData(oUBForm.UB04_74d_ProcedureDate_OtherD.Value.ToString(), oUBForm.UB04_74d_ProcedureDate_OtherD.Location, false);
                WriteData(oUBForm.UB04_74e_ProcedureCode_OtherE.Value.ToString(), oUBForm.UB04_74e_ProcedureCode_OtherE.Location, false);
                WriteData(oUBForm.UB04_74e_ProcedureDate_OtherE.Value.ToString(), oUBForm.UB04_74e_ProcedureDate_OtherE.Location, false);
                WriteData(oUBForm.UB04_75a_ReservedFL75A.Value.ToString(), oUBForm.UB04_75a_ReservedFL75A.Location, false);
                WriteData(oUBForm.UB04_75b_ReservedFL75B.Value.ToString(), oUBForm.UB04_75b_ReservedFL75B.Location, false);
                WriteData(oUBForm.UB04_75c_ReservedFL75C.Value.ToString(), oUBForm.UB04_75c_ReservedFL75C.Location, false);
                WriteData(oUBForm.UB04_75d_ReservedFL75D.Value.ToString(), oUBForm.UB04_75d_ReservedFL75D.Location, false);
                //''Attending provider and identifiers,Operating provider and identifiers,Other provider name and identifiers.,other provider name and identifiers.


                WriteData(oUBForm.UB04_76_AttendingNPI.Value.ToString(), oUBForm.UB04_76_AttendingNPI.Location, false);
                WriteData(oUBForm.UB04_76_AttendingQUAL.Value.ToString(), oUBForm.UB04_76_AttendingQUAL.Location, false);
                WriteData(oUBForm.UB04_76_AttendingID.Value.ToString(), oUBForm.UB04_76_AttendingID.Location, false);
                WriteData(oUBForm.UB04_76a_AttendingLast.Value.ToString(), oUBForm.UB04_76a_AttendingLast.Location, false);
                WriteData(oUBForm.UB04_76b_AttendingFirst.Value.ToString(), oUBForm.UB04_76b_AttendingFirst.Location, false);
                WriteData(oUBForm.UB04_77_OperatingNPI.Value.ToString(), oUBForm.UB04_77_OperatingNPI.Location, false);
                WriteData(oUBForm.UB04_77_OperatingQUAL.Value.ToString(), oUBForm.UB04_77_OperatingQUAL.Location, false);
                WriteData(oUBForm.UB04_77_OperatingID.Value.ToString(), oUBForm.UB04_77_OperatingID.Location, false);
                WriteData(oUBForm.UB04_77a_OperatingLast.Value.ToString(), oUBForm.UB04_77a_OperatingLast.Location, false);
                WriteData(oUBForm.UB04_77b_OperatingFirst.Value.ToString(), oUBForm.UB04_77b_OperatingFirst.Location, false);
                WriteData(oUBForm.UB04_78_OtherNPI.Value.ToString(), oUBForm.UB04_78_OtherNPI.Location, false);
                WriteData(oUBForm.UB04_78_OtherProvider_QUAL.Value.ToString(), oUBForm.UB04_78_OtherProvider_QUAL.Location, false);
                WriteData(oUBForm.UB04_78_OtherQUAL.Value.ToString(), oUBForm.UB04_78_OtherQUAL.Location, false);
                WriteData(oUBForm.UB04_78_OtherID.Value.ToString(), oUBForm.UB04_78_OtherID.Location, false);
                WriteData(oUBForm.UB04_78_OtherLast.Value.ToString(), oUBForm.UB04_78_OtherLast.Location, false);
                WriteData(oUBForm.UB04_78_OtherFirst.Value.ToString(), oUBForm.UB04_78_OtherFirst.Location, false);
                WriteData(oUBForm.UB04_79_OtherNPI.Value.ToString(), oUBForm.UB04_79_OtherNPI.Location, false);
                WriteData(oUBForm.UB04_79_OtherProvider_QUAL.Value.ToString(), oUBForm.UB04_79_OtherProvider_QUAL.Location, false);
                WriteData(oUBForm.UB04_79_OtherQUAL.Value.ToString(), oUBForm.UB04_79_OtherQUAL.Location, false);
                WriteData(oUBForm.UB04_79_OtherID.Value.ToString(), oUBForm.UB04_79_OtherID.Location, false);
                WriteData(oUBForm.UB04_79_OtherLast.Value.ToString(), oUBForm.UB04_79_OtherLast.Location, false);
                WriteData(oUBForm.UB04_79_OtherFirst.Value.ToString(), oUBForm.UB04_79_OtherFirst.Location, false);
                WriteData(oUBForm.PayerCodeA_Primary.Value.ToString(), oUBForm.PayerCodeA_Primary.Location, false);
                WriteData(oUBForm.PayerCodeB_Secondary.Value.ToString(), oUBForm.PayerCodeB_Secondary.Location, false);
                WriteData(oUBForm.PayerCodeC_Tertiary.Value.ToString(), oUBForm.PayerCodeC_Tertiary.Location, false);
                // ''Remark
                WriteData(oUBForm.UB04_80a_Remarks_1.Value.ToString(), oUBForm.UB04_80a_Remarks_1.Location, false);
                WriteData(oUBForm.UB04_80b_Remarks_2.Value.ToString(), oUBForm.UB04_80b_Remarks_2.Location, false);
                WriteData(oUBForm.UB04_80c_Remarks_3.Value.ToString(), oUBForm.UB04_80c_Remarks_3.Location, false);
                WriteData(oUBForm.UB04_80d_Remarks_4.Value.ToString(), oUBForm.UB04_80d_Remarks_4.Location, false);
                WriteData(oUBForm.UB04_81a_Code_Code_QUAL_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_QUAL_A.Location, false);
                WriteData(oUBForm.UB04_81a_Code_Code_CODE_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_CODE_A.Location, false);
                WriteData(oUBForm.UB04_81a_Code_Code_VALUE_A.Value.ToString(), oUBForm.UB04_81a_Code_Code_VALUE_A.Location, false);
                WriteData(oUBForm.UB04_81b_Code_Code_QUAL_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_QUAL_B.Location, false);
                WriteData(oUBForm.UB04_81b_Code_Code_CODE_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_CODE_B.Location, false);
                WriteData(oUBForm.UB04_81b_Code_Code_VALUE_B.Value.ToString(), oUBForm.UB04_81b_Code_Code_VALUE_B.Location, false);
                WriteData(oUBForm.UB04_81c_Code_Code_QUAL_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_QUAL_C.Location, false);
                WriteData(oUBForm.UB04_81c_Code_Code_CODE_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_CODE_C.Location, false);
                WriteData(oUBForm.UB04_81c_Code_Code_VALUE_C.Value.ToString(), oUBForm.UB04_81c_Code_Code_VALUE_C.Location, false);
                WriteData(oUBForm.UB04_81d_Code_Code_QUAL_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_QUAL_D.Location, false);
                WriteData(oUBForm.UB04_81d_Code_Code_CODE_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_CODE_D.Location, false);
                WriteData(oUBForm.UB04_81d_Code_Code_VALUE_D.Value.ToString(), oUBForm.UB04_81d_Code_Code_VALUE_D.Location, false);

            }
        }
               
        private void WriteDataForPrintForm(string data, PointF location, bool isboolean)
        {
            PointF oPoint = new PointF(location.X, location.Y);
            if (isboolean == false)
            {
                oGraphics.DrawString(data, arialRegular, Brushes.Black, oPoint);
            }
            else
            {
                if (data.ToUpper() == "TRUE")
                {
                    oGraphics.DrawString("X", arialRegular , Brushes.Black, oPoint);
                }
            }
        }

        private void WriteData(string data, PointF location, bool isboolean)
        {
            PointF oPoint = new PointF(location.X, location.Y);
            if (isboolean == false)
            {

                oGraphics.DrawString(data, arialBold, Brushes.Black, oPoint);
            }
            else
            {
                if (data.ToUpper() == "TRUE")
                {
                    oGraphics.DrawString("X", arialBold, Brushes.Black, oPoint);
                }
            }
        }

    }
}
