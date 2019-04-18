using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;

namespace gloBilling
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
        Font arialFontSmall = null;
        Font arialFontBig = null;


        bool toCreateEMF = gloGlobal.gloTSPrint.UseEMFForClaims;

        private Common.gloHCFA1500PaperForm _oHCFA1500Form = null;
        private Boolean _PrintOnForm = false;

        float arialFontSmallHeight = 8.5f;
        float arialFontBigHeight = 24.0f;
        private void AdjustFontHeight()
        {
            float _arialFontSmall = arialFontSmall.GetHeight(oGraphics);
            float _arialFontBig = arialFontBig.GetHeight(oGraphics);
            arialFontSmall = new Font(arialFontSmall.FontFamily, arialFontSmall.Size * arialFontSmallHeight / _arialFontSmall, arialFontSmall.Style);
            arialFontBig = new Font(arialFontBig.FontFamily, arialFontBig.Size * arialFontBigHeight / _arialFontBig, arialFontBig.Style);
        }
        private void GetFontHeight()
        {
            using (oGraphics = Graphics.FromImage(oSourceHCFA1500))
            {
                arialFontSmallHeight = arialFontSmall.GetHeight(oGraphics);
                arialFontBigHeight = arialFontBig.GetHeight(oGraphics);
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
                WriteRespectiveData(_oHCFA1500Form);
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
        public string PrintHCFA1500Form(Common.gloHCFA1500PaperForm oHCFA1500Form, string SourceFilePath)
        {
            string _result = "";
            string _printFilePath = "";
            _oHCFA1500Form = oHCFA1500Form;
            _PrintOnForm = true;
            try
            {

                if (File.Exists(SourceFilePath) == true)
                {
                    FileInfo oFileInfo = new FileInfo(SourceFilePath);
                    _printFilePath = oFileInfo.DirectoryName + "\\" + gloGlobal.clsFileExtensions.GetUniqueDateString("MMddyyyyHHmmssffff") + (toCreateEMF ? ".emf" : ".tif");
                    if (File.Exists(_printFilePath) == true) { File.Delete(_printFilePath); }

                    oFileInfo = null;

                    if (File.Exists(SourceFilePath) == true)
                    {
                        arialFontSmall = new Font("Arial", 7);
                        arialFontBig = new Font("Arial", 10, FontStyle.Bold);
                        oSourceHCFA1500 = new Bitmap(SourceFilePath);
                        if (toCreateEMF)
                        {
                            GetFontHeight();
                            byte[] emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)oSourceHCFA1500.Width / oSourceHCFA1500.HorizontalResolution, (float)oSourceHCFA1500.Height / oSourceHCFA1500.VerticalResolution, oSourceHCFA1500.Width, oSourceHCFA1500.Height, CreateEMFHCFA1500Form);
                            File.WriteAllBytes(_printFilePath, emfBytes);
                        }
                        else
                        {
                            oGraphics = Graphics.FromImage(oSourceHCFA1500);
                            WriteRespectiveData(oHCFA1500Form);

                            oSourceHCFA1500.Save(_printFilePath);

                            if (oGraphics != null) { oGraphics.Dispose(); oGraphics = null; }
                        }
                        if (oSourceHCFA1500 != null) { oSourceHCFA1500.Dispose(); oSourceHCFA1500 = null; }
                        if (arialFontSmall != null) { arialFontSmall.Dispose(); arialFontSmall = null; }
                        if (arialFontBig != null) { arialFontBig.Dispose(); arialFontBig = null; }

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


        private void WriteRespectiveData(Common.gloHCFA1500PaperForm oHCFA1500Form)
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
            WriteData(oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Value, oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Location, false);
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

        public string PrintHCFA1500Form(Common.gloHCFA1500PaperForm oHCFA1500Form, string SourceFilePath, string DestinationFilePath)
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
                        arialFontSmall = new Font("Arial", 7);
                        arialFontBig = new Font("Arial", 10, FontStyle.Bold);
                        oSourceHCFA1500 = new Bitmap(SourceFilePath);
                        if (toCreateEMF)
                        {
                            GetFontHeight();
                            byte[] emfBytes = gloGlobal.CreateEMF.GetEMFBytes((float)oSourceHCFA1500.Width / oSourceHCFA1500.HorizontalResolution, (float)oSourceHCFA1500.Height / oSourceHCFA1500.VerticalResolution, oSourceHCFA1500.Width, oSourceHCFA1500.Height, CreateEMFHCFA1500PaperForm);
                            File.WriteAllBytes(_printFilePath, emfBytes);
                        }
                        else
                        {
                            oGraphics = Graphics.FromImage(oSourceHCFA1500);

                            PrintDataToPaperForm(oHCFA1500Form);

                            oSourceHCFA1500.Save(_printFilePath);

                            if (oGraphics != null) { oGraphics.Dispose(); oGraphics = null; }
                        }
                        if (oSourceHCFA1500 != null) { oSourceHCFA1500.Dispose(); oSourceHCFA1500 = null; }
                        if (arialFontSmall != null) { arialFontSmall.Dispose(); arialFontSmall = null; }
                        if (arialFontBig != null) { arialFontBig.Dispose(); arialFontBig = null; }

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

        private void PrintDataToPaperForm(Common.gloHCFA1500PaperForm oHCFA1500Form)
        {
            #region "Write Data on Image"
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
            WriteData(oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Value, oHCFA1500Form.CF_17a_ReferringProvider_UnknownField.Location, false);
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

            #endregion
        }

        private void WriteData(string data, PointF location, bool isboolean)
        {
            if (isboolean == false)
            {

                oGraphics.DrawString(data, arialFontSmall, Brushes.Black, location);
            }
            else
            {
                if (data.ToUpper() == "TRUE")
                {
                    oGraphics.DrawString("X", arialFontBig, Brushes.Black, location);
                }
            }
        }
    }
}
