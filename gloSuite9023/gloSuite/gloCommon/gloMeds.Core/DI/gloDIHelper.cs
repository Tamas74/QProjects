using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.ServiceModel;
using System.Data;
using System.Diagnostics; 

namespace gloMeds.Core.DI
{
    public class gloDIHelper : EventArgs, IDisposable 
    {
        private int maxBufferSize = 2147483647;
        private int maxDepth = int.MaxValue;
        private int maxArrayLength = int.MaxValue;
        private int maxBytesPerRead = int.MaxValue;
        private int maxStringContentLength = int.MaxValue;
        private int maxNameTableCharCount = int.MaxValue;
        private Int64 maxReceivedMessageSize = 2147483647;

        BasicHttpBinding binding = new BasicHttpBinding();
        EndpointAddress endpointAddress = null;

        public DataTable ICDCodes { get; set; }

        public string PatientGender { get; set; }
        public DateTime PatientDOB { get; set; }

        public string SeverityLevel { get; set; }
        public string OnsetLevel { get; set; }

        public string DISeverityLevel { get; set; }
        public string DIDocLevel { get; set; }
        public string DFASeverityLevel { get; set; }
        public string DFADocLevel { get; set; }
        
        public Boolean DrugToDrugEnable { get; set; }
        public Boolean DrugToAllergyEnable { get; set; }
        public Boolean DrugToDiseaseEnable { get; set; }
        public Boolean DrugToFoodEnable { get; set; }
        public Boolean DuplicateTherapyEnable { get; set; }
        public Boolean AdverseDrugEffectEnable { get; set; }

        public delegate void onDIAlertTrigger();
        public event onDIAlertTrigger HideDiProgressPanel;
        
        private GSDD5Service.GenderEnum GSGender
        {
            get
            {
                if (PatientGender == "MALE")
                {
                    return GSDD5Service.GenderEnum.Male;
                }
                else if (PatientGender == "FEMALE")
                {
                    return GSDD5Service.GenderEnum.Female;
                }

                return GSDD5Service.GenderEnum.Both;
            }
        }

        private GSDD5Service.ADRSeverityEnum Severity
        {
            get
            {
                if (SeverityLevel.ToLower() == "mild")
                {
                    return GSDD5Service.ADRSeverityEnum.Mild;
                }
                else if (SeverityLevel.ToLower() == "moderate")
                {
                    return GSDD5Service.ADRSeverityEnum.Moderate;
                }
                else
                {
                    return GSDD5Service.ADRSeverityEnum.Severe;
                }

            }
        }
        private GSDD5Service.ADROnsetEnum AdeOnset
        {
            get
            {
                if (OnsetLevel.ToLower() == "delayed")
                {
                    return GSDD5Service.ADROnsetEnum.Delayed;
                }
                else if (OnsetLevel.ToLower() == "early")
                {
                    return GSDD5Service.ADROnsetEnum.Early;
                }
                else
                {
                    return GSDD5Service.ADROnsetEnum.Rapid;
                }
            }
        }
        public gloDIHelper(string url)
        {
            Uri serviceUri = new Uri(url);
            try
            {
                endpointAddress = new System.ServiceModel.EndpointAddress(serviceUri);

                binding.ReaderQuotas.MaxDepth = maxDepth;
                binding.ReaderQuotas.MaxArrayLength = maxArrayLength;
                binding.ReaderQuotas.MaxBytesPerRead = maxBytesPerRead;
                binding.ReaderQuotas.MaxStringContentLength = maxStringContentLength;
                binding.ReaderQuotas.MaxNameTableCharCount = maxNameTableCharCount;
                binding.MaxReceivedMessageSize = maxReceivedMessageSize;
                binding.MaxBufferSize = maxBufferSize;
                TimeSpan duration = new TimeSpan(0, 5, 0);
                binding.SendTimeout = duration;
                binding.ReceiveTimeout = duration;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "gloDIHelper : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally { serviceUri = null; }
        }

        #region MedicalInstruction

        public StringBuilder PerformScreeningMI(string ndc)
        {
            StringBuilder screeningResult = new StringBuilder();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
             
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {
                    
                    GSDD5Service.DrugIdentifierLanguageType sheetIdentifier = new GSDD5Service.DrugIdentifierLanguageType();
                    GSDD5Service.PatientEducationSheetType miResponse = new GSDD5Service.PatientEducationSheetType();
                    GSDD5Service.CodeDescriptionType[] temobj = null;
                    GSDD5Service.PackageIdentifierType packageIdentifier = new GSDD5Service.PackageIdentifierType();

                    sheetIdentifier.DrugIdentifier = new GSDD5Service.PackageProductConceptGpcType();
                    sheetIdentifier.DrugIdentifier.ItemElementName = GSDD5Service.ItemChoiceType4.Package;
                    sheetIdentifier.DrugIdentifier.Item = packageIdentifier;
                    sheetIdentifier.LanguageCode = "en-US";
                    

                    packageIdentifier.Identifier = ndc;
                    packageIdentifier.IdentifierType = GSDD5Service.PackageIdentifierEnum.NDC11;
                  
                    miResponse = client.ContentPatientEducation(ref sheetIdentifier, out temobj);
                       
                    if (miResponse != null)
                    {
                        screeningResult = ProccessScreeningResultMI(miResponse);
                    }

                    sheetIdentifier = null;
                    miResponse = null;
                    packageIdentifier = null;
                    temobj = null;                    
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve MI information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningMI : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            return screeningResult;
        }

        private StringBuilder ProccessScreeningResultMI(GSDD5Service.PatientEducationSheetType miResponse)
        {
            StringBuilder result = new StringBuilder();

            try
            {
                if (miResponse.SheetName != null)
                {
                    result.Append("GENERIC NAME:");
                    result.Append(miResponse.SheetName);
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);
                }

                if (miResponse.Description != null)
                {
                    result.Append(miResponse.DescriptionHeader.ToUpper());
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);

                    result.Append(miResponse.Description);
                   
                    if (string.IsNullOrEmpty(Convert.ToString(miResponse.DescriptionFooter)) != true)
                    {
                        result.Append(Environment.NewLine);
                        result.Append(miResponse.DescriptionFooter);
                        result.Append(Environment.NewLine);
                    }
                }
                if (miResponse.Contraindications != null)
                {
                    result.Append(Environment.NewLine);        
                    result.Append(Environment.NewLine);
                    result.Append(miResponse.ContraindicationsHeader.ToUpper());
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);

                    result.Append(miResponse.Contraindications);
                    
                    if (string.IsNullOrEmpty(Convert.ToString(miResponse.ContraindicationsFooter)) != true)
                    {
                        result.Append(Environment.NewLine);
                        result.Append(miResponse.ContraindicationsFooter);
                        result.Append(Environment.NewLine);
                    }
                }
                if (miResponse.Administration != null)
                {
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);
                    result.Append(miResponse.AdministrationHeader.ToUpper());
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);

                    result.Append(miResponse.Administration);
                   
                    if (string.IsNullOrEmpty(Convert.ToString(miResponse.AdministrationFooter)) != true)
                    {
                        result.Append(Environment.NewLine);
                        result.Append(miResponse.AdministrationFooter);
                        result.Append(Environment.NewLine);
                    }
                }
                if (miResponse.SideEffects != null)
                {
                    result.Append(Environment.NewLine);
                    result.Append(miResponse.SideEffects);
                    result.Append(Environment.NewLine);
                    //result.Append(Environment.NewLine);
                }
                if (miResponse.Interactions != null)
                {
                    result.Append(Environment.NewLine);
                    result.Append(miResponse.InteractionsHeader.ToUpper());
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);

                    result.Append(miResponse.Interactions);
                   
                    if (string.IsNullOrEmpty(Convert.ToString(miResponse.InteractionsFooter)) != true)
                    {
                        result.Append(Environment.NewLine);
                        result.Append(miResponse.InteractionsFooter);
                        result.Append(Environment.NewLine);
                    }
                }
                if (miResponse.MissedDose != null)
                {                   
                    result.Append(Environment.NewLine);
                    result.Append(miResponse.MissedDoseHeader.ToUpper());
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);

                    result.Append(miResponse.MissedDose);
                    result.Append(Environment.NewLine);
                    if (string.IsNullOrEmpty(Convert.ToString(miResponse.MissedDoseFooter)) != true)
                    {
                        result.Append(Environment.NewLine);
                        result.Append(miResponse.MissedDoseFooter);
                        result.Append(Environment.NewLine);
                    }
                }

                if (miResponse.SheetName != null)
                {
                    result.Append("LAST UPDATED:");
                    result.Append(miResponse.LastUpdated.Date.ToString());
                    result.Append(Environment.NewLine);
                    result.Append(Environment.NewLine);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve MI information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "ProccessScreeningResultMI : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            return result;
        }

        #endregion

        #region "ADR"

        public StringBuilder PerformScreeningADR(List<string> oPrescribing, List<string> oPrescribed)
        {
            StringBuilder screeningResult = new StringBuilder();
            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
            GSDD5Service.ClinicalOperationAdverseReactionType requestADR = new GSDD5Service.ClinicalOperationAdverseReactionType();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {

                    bool ignorePrescribed = false;

                    if (oPrescribed != null)
                    {                       
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }


                    prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType();
                    patientDemographics = null;



                    requestedOperations.DuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType() { Request = false };
                    requestedOperations.Allergy = false;                   
                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType { Request = false };
                    requestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = false, InformationOnly = false };

                    requestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };
                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType { Request = false };
                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    requestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };

                    requestADR.Request = true;
                    requestADR.Filter = new GSDD5Service.AdverseReactionFilterType
                    {
                        Severity = Severity,
                        Onset = AdeOnset,
                        SeveritySpecified = true,
                        OnsetSpecified = true

                    };

                    requestedOperations.AdverseReactions = requestADR;

                    packages = client.Clinical(
                        prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                        out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                        out filterPreferencesResponse);


                    screeningResult = ProccessScreeningResultADR(products, marketedProducts, genericProductClinicals);


                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningADR : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
                requestedOperations = null;
                requestADR = null;
            }
            return screeningResult;
        }

        private StringBuilder ProccessScreeningResultAlerts(GSDD5Service.ClinicalInteractionProductType[] products, GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts, GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals, ScreningResultType resultType = ScreningResultType.Details)
        {
            StringBuilder screeningResult = new StringBuilder();
            if (products != null)
            {
                
                    if (DrugToDrugEnable)
                    { screeningResult.Append(ProccessDrugToDrugResults(products, marketedProducts, genericProductClinicals, resultType));  }
                   
                    if (AdverseDrugEffectEnable)
                    { screeningResult.Append(ProccessScreeningResultADR(products, marketedProducts, genericProductClinicals, resultType)); }
                    
                    if (DrugToAllergyEnable)
                    { screeningResult.Append(ProccessAllergyResults(products, resultType)); screeningResult.Append(Environment.NewLine); }
                    
                    if (DrugToDiseaseEnable)
                    { screeningResult.Append(ProccessDrugToDiseaseResults(products, marketedProducts, genericProductClinicals, resultType)); }
                    
                    if (DrugToFoodEnable)
                    { screeningResult.Append(ProccessDrugToFoodinteractionResults(products, resultType)); screeningResult.Append(Environment.NewLine); }
                    
                    if (DuplicateTherapyEnable)
                    { screeningResult.Append(ProccessDuplicateTherapyResults(products, marketedProducts, genericProductClinicals, resultType)); }
               
            }
            return screeningResult;
        }


        private StringBuilder ProccessScreeningResultADR(GSDD5Service.ClinicalInteractionProductType[] products, GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts, GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals, ScreningResultType resultType = ScreningResultType.Details)
        {
            StringBuilder screeningResult = new StringBuilder();
            if (products != null)
            {
                foreach (var product in products)
                {                   
                    if (product.AdverseReactions != null)
                    {
                        if (resultType == ScreningResultType.Details)
                        {
                            if (product.AdverseReactions.AdverseReaction != null || product.AdverseReactions.Message != null)
                            {
                                if (string.IsNullOrEmpty(Convert.ToString(screeningResult)) != true)
                                {
                                    screeningResult.Append(Environment.NewLine);
                                }

                                screeningResult.Append(Environment.NewLine + string.Format(
                                    "Adverse Drug Reaction for {0}"
                                    , product.ProductName));

                                screeningResult.Append(GetADRMessages(product.AdverseReactions));
                            }
                        }
                        else
                        {
                            screeningResult.Append("Adverse Drug Effect was found for drug :" + product.ProductName + Environment.NewLine);
                        }
                    }
                }
            }

            return screeningResult;


            //if (screeningResult.Length > 0)
            //{
            //    return screeningResult;

            //}
            ////else if (allergyCount == 0)
            ////{
            ////    return "No ADR returned from GSDD for the entered drugs.";
            ////}
            //else
            //{
            //    return "";
            //}
        }

        StringBuilder GetADRMessages(GSDD5Service.ClinicalAdverseReactionResponseType ADRresponce)
        {
            StringBuilder screeningResult = new StringBuilder();

            if (ADRresponce.AdverseReaction != null)
            {
                foreach (var ADRInteraction in ADRresponce.AdverseReaction)
                {
                    if (ADRInteraction.AdverseReactionIdentifier != null)
                    {
                        if (ADRInteraction.BlackBoxWarning)
                        {
                            screeningResult.Append(Environment.NewLine + "- " + ADRInteraction.Description.ToString() + "*");
                        }
                        else
                        {
                            screeningResult.Append(Environment.NewLine + "- " + ADRInteraction.Description.ToString() + " (" + ADRInteraction.Severity.ToString() + ", " + ADRInteraction.Onset.ToString() + ")");
                        }
                    }
                }
            }
            else if (ADRresponce.Message != null)
            {
                screeningResult.Append(Environment.NewLine + "- " + ADRresponce.Message);
            }
            return screeningResult;
        }
        #endregion

        #region DuplicateTherapy
        public StringBuilder PerformScreeningDT(List<string> oPrescribing, List<string> oPrescribed)
        {
            StringBuilder screeningResult = new StringBuilder();
            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
            GSDD5Service.ClinicalOperationDuplicateTherapyType requestDT = new GSDD5Service.ClinicalOperationDuplicateTherapyType();
            try
            {
               
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {

                    bool ignorePrescribed = false;
                   
                    if (oPrescribed != null)
                    {
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }

                  
                    prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType();
                    patientDemographics.DateOfBirth = PatientDOB; //Convert.ToDateTime("1981-08-02");
                    patientDemographics.Gender = GSGender;


                    requestedOperations.Allergy = false;
                    requestedOperations.AdverseReactions = new GSDD5Service.ClinicalOperationAdverseReactionType() { Request = false };
                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType { Request = false };
                    requestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = false, InformationOnly = false };

                    requestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };
                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType { Request = false };
                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    requestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };
                  

                    requestDT.Request = true;
                    requestDT.ControlledSubstanceMaxOccurrences = 0;
                    requestedOperations.DuplicateTherapy = requestDT;

                    

                    packages = client.Clinical(
                        prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                        out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                        out filterPreferencesResponse);

                    screeningResult = ProccessDuplicateTherapyResults(products, marketedProducts, genericProductClinicals);



                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningDT : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
                requestedOperations = null;
                requestDT = null;
            }
            return screeningResult;
        }
        private StringBuilder ProccessDuplicateTherapyResults(GSDD5Service.ClinicalInteractionProductType[] products, GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts, GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals, ScreningResultType resultType = ScreningResultType.Details)
        {
             StringBuilder screeningResult = new StringBuilder();
             if (products != null)
             {
                 if (resultType == ScreningResultType.Details)
                 {
                     screeningResult.Append(GetDTMessagesRevised(products));
                 }
                 else
                 {
                     foreach (var product in products)
                     {
                         if (product.DuplicateTherapy != null && product.DuplicateTherapy.TherapeuticClassDuplication != null)
                         {
                             screeningResult.Append("Duplicate Therapy was found for drug :" + product.ProductName + Environment.NewLine);
                         }
                     }
                 }
             }
             return screeningResult;
        }

        public class ProductInfo
        {
            public string productID;
            public string name;
        }


        public StringBuilder GetDTMessagesRevised(GSDD5Service.ClinicalInteractionProductType[] products)
        {
            var prescribed = from p in products
                             select new ProductInfo
                             {
                                 productID = p.ProductIdentifier.Identifier,
                                 name = p.ProductName
                             };
            StringBuilder screeningResult = new StringBuilder();

            foreach (var product in products)
            {
                if (product.DuplicateTherapy != null)
                {
                    if (string.IsNullOrEmpty(Convert.ToString(screeningResult)) != true)
                    {
                        screeningResult.Append(Environment.NewLine);
                    }
                    GSDD5Service.DuplicateTherapyType duplicateTherapy = product.DuplicateTherapy;
                    if (duplicateTherapy != null && duplicateTherapy.TherapeuticClassDuplication != null)
                    {
                        foreach (var DTInteraction in duplicateTherapy.TherapeuticClassDuplication)
                        {
                            if (DTInteraction.Product != null)
                            {
                                screeningResult.Append(Environment.NewLine + "Duplication Class : " + DTInteraction.Name.ToString());
                                screeningResult.Append(" (" + "Max Occurances " + DTInteraction.MaxOccurances.ToString() + ")" + Environment.NewLine);
                                screeningResult.Append(Environment.NewLine);
                                screeningResult.Append("Use of " + product.ProductName + " ");

                                foreach (GSDD5Service.ProductIdentifierType p in DTInteraction.Product)
                                {
                                    string nm = prescribed.Where(s => s.productID == p.Identifier).FirstOrDefault().name;
                                    screeningResult.Append(", " + nm);
                                }
                                screeningResult.Append(" may represent a duplication in therapy based on their association to the therapeutic drug class " + DTInteraction.Name.ToString());
                                screeningResult.Append(Environment.NewLine);
                            }
                        }
                    }
                }
            }

            return screeningResult;
        }
        #endregion

        #region DrugToDrugInteraction

        public StringBuilder PerformScreeningDTD(List<string> oPrescribing, List<string> oPrescribed)
        {

            StringBuilder screeningResult = new StringBuilder();
            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();

            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {
                    bool ignorePrescribed = false;

                    if (oPrescribed != null)
                    {
                        // AddPrescribedDrugsList(oPrescribed, ref prescribedProductid);
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }


                    prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType();
                    patientDemographics.Gender = GSGender;
                    patientDemographics.DateOfBirth = PatientDOB;


                    requestedOperations.DuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType() { Request = false };
                    requestedOperations.Allergy = false;
                    requestedOperations.AdverseReactions = new GSDD5Service.ClinicalOperationAdverseReactionType() { Request = false };                    
                    requestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = false, InformationOnly = false };

                    requestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };                   
                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    requestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };

                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType
                    {
                        Request = true,
                        IncludeConsumerNotes = false,
                        IncludeProfessionalNotes = true
                    };


                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType
                    {
                        Request = false,
                        IncludeConsumerNotes = false,
                        IncludeProfessionalNotes = false
                    };

                    packages = client.Clinical(
                        prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                        out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                        out filterPreferencesResponse);

                    screeningResult = ProccessDrugToDrugResults(products, marketedProducts, genericProductClinicals);

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningDTD : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
                requestedOperations = null;  
            }
            return screeningResult;
        }
        private StringBuilder ProccessDrugToDrugResults(GSDD5Service.ClinicalInteractionProductType[] rsponceDTD, GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts, GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals, ScreningResultType resultType = ScreningResultType.Details)
        {
            StringBuilder screeningResult = new StringBuilder();
            if (rsponceDTD.Count() > 0)
            {
                foreach (var product in rsponceDTD)
                {
                    if (product.Drug != null)
                    {
                        if (resultType == ScreningResultType.Details)
                        {
                            screeningResult.Append(GetDIMessages(product, product.Drug));
                        }
                        else
                        {
                            if (product.Drug.Product != null)
                            {
                                bool isfound = false;
                                foreach (var DiInteraction in product.Drug.Product)
                                {
                                    if (DiInteraction.Interaction != null)
                                    {
                                        isfound = true;
                                        break;
                                    }
                                }
                                if (isfound == true)
                                {
                                    screeningResult.Append("Drug To Drug Interaction was found for drug :" + product.ProductName + Environment.NewLine);
                                }
                            }
                        }
                    }
                }
            }
            return screeningResult;
        }

        public int GetRankOfDISeverity(string severity)
        {
            if (severity.ToUpper() == Convert.ToString("Severe").ToUpper())
            {
                return 1;
            }
            else if (severity.ToUpper() == Convert.ToString("Major").ToUpper())
            {
                return 2;
            }
            else if (severity.ToUpper() == Convert.ToString("Moderate").ToUpper())
            {
                return 3;
            }
            else if (severity.ToUpper() == Convert.ToString("Minor").ToUpper())
            {
                return 4;
            }
            return 0;
        }

        public int GetRankOfDIDocumentLevel(string DOcLevel)
        {
            if (DOcLevel.ToUpper() == Convert.ToString("Established").ToUpper())
            {
                return 1;
            }
            else if (DOcLevel.ToUpper() == Convert.ToString("Likely Established").ToUpper())
            {
                return 2;
            }
            else if (DOcLevel.ToUpper() == Convert.ToString("Not Established").ToUpper())
            {
                return 3;
            }

            return 0;
        }

        public String GetSeveritysplit(string splitstr)
        {
            if (Convert.ToString(splitstr).Contains("Severe"))
            {
                return "Severe";
            }
            else if (Convert.ToString(splitstr).Contains("Major"))
            {
                return "Major";
            }
            else if (Convert.ToString(splitstr).Contains("Moderate"))
            {
                return "Moderate";
            }
            else if (Convert.ToString(splitstr).Contains("Minor"))
            {
                return "Minor";
            }
            else
            {
                return "None";
            }

        }

        StringBuilder GetDIMessages(GSDD5Service.ClinicalInteractionProductType responceDI, GSDD5Service.ClinicalDrugInteractionType drugInteraction)
        {
            StringBuilder screeningResult = new StringBuilder();

            if (drugInteraction != null)
            {
                if (drugInteraction.Product != null)
                {
                    foreach (var DiInteraction in drugInteraction.Product)
                    {

                        if (DiInteraction.Interaction != null)
                        {
                            int SeverityRankDI = GetRankOfDISeverity(DISeverityLevel);
                            int DocLevelRankDI = GetRankOfDIDocumentLevel(DIDocLevel);

                            var resulset = from s in DiInteraction.Interaction
                                           where s.Severity.SeverityRanking <= SeverityRankDI
                                                 && s.Documentation.DocumentationRanking <= DocLevelRankDI
                                           select s;

                            foreach (var _Interaction in resulset.ToList())
                            {
                                foreach (var _Ingrediant in _Interaction.InteractingClass.Ingredient)
                                {
                                    screeningResult.Append(responceDI.ProductName + " is interacting with " + _Ingrediant.Name);
                                }
                                screeningResult.Append(", Severity " + Convert.ToString(GetSeveritysplit(_Interaction.Severity.SeverityText)));
                                screeningResult.Append(Environment.NewLine + Environment.NewLine + Convert.ToString(_Interaction.ProfessionalNotesAbbr) + Environment.NewLine);

                                if (_Interaction.ClinicalManagementStatement.Count() > 1)
                                {
                                    screeningResult.Append(Environment.NewLine + "Notes : ");
                                }
                                foreach (var _statement in _Interaction.ClinicalManagementStatement)
                                {
                                    screeningResult.Append(Environment.NewLine + "- " + _statement);
                                }
                                screeningResult.Append(Environment.NewLine + Environment.NewLine + Environment.NewLine);
                            }
                        }
                    }
                }
            }


            return screeningResult;
        }
        #endregion

        #region DrugToFoodInteraction
        public StringBuilder PerformScreeningDTF(List<string> oPrescribing, List<string> oPrescribed)
        {

            StringBuilder screeningResult = new StringBuilder();
            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {
                    bool ignorePrescribed = false;
                    
                    if (oPrescribed != null)
                    {                       
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }
                   
                    prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType();
                    patientDemographics.Gender = GSGender;
                    patientDemographics.DateOfBirth = PatientDOB;

                    requestedOperations.DuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType() { Request = false };
                    requestedOperations.Allergy = false;
                    requestedOperations.AdverseReactions = new GSDD5Service.ClinicalOperationAdverseReactionType() { Request = false };
                    requestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = false, InformationOnly = false };

                    requestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };
                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    requestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };

                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType
                    {
                        Request = false,
                        IncludeConsumerNotes = false,
                        IncludeProfessionalNotes = false
                    };


                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType
                    {
                        Request = true,
                        IncludeConsumerNotes = false,
                        IncludeProfessionalNotes = true
                    };

                                        
                           packages = client.Clinical(
                            prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                            out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                            out filterPreferencesResponse);

                           screeningResult = ProccessDrugToFoodinteractionResults(products);

                    
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningDTF : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
                requestedOperations = null;         
            }
            return screeningResult;
        }
        private StringBuilder ProccessDrugToFoodinteractionResults(GSDD5Service.ClinicalInteractionProductType[] products, ScreningResultType resultType = ScreningResultType.Details)
        {
            StringBuilder screeningResult = new StringBuilder();
            if (products.Count() > 0)
            {
                
                    if (resultType == ScreningResultType.Details)
                    {
                        screeningResult.Append(GetDTFMessages(products));
                    }
                    else
                    {
                        foreach (var product in products)
                        {
                            if (product.Lifestyle != null)
                            {
                                int SeverityRankDI = GetRankOfDISeverity(DFASeverityLevel);
                                int DocLevelRankDI = GetRankOfDIDocumentLevel(DFADocLevel);

                                var resulset = from s in product.Lifestyle
                                               where s.Severity.SeverityRanking <= SeverityRankDI
                                                    && s.Documentation.DocumentationRanking <= DocLevelRankDI
                                               select s;
                                if (resulset.ToList().Count > 0)
                                {
                                    screeningResult.Append("Drug To Food Interaction was found for drug :" + product.ProductName + Environment.NewLine);
                                }
                            }

                        }
                    }
               
            }

            return screeningResult;

        }

        
        StringBuilder GetDTFMessages(GSDD5Service.ClinicalInteractionProductType[] responseDTF)
        {
            StringBuilder screeningResult = new StringBuilder();
            foreach (var product in responseDTF)
            {
                if (product.Lifestyle != null)
                {

                    int SeverityRankDI = GetRankOfDISeverity(DFASeverityLevel);
                    int DocLevelRankDI = GetRankOfDIDocumentLevel(DFADocLevel);

                    var resulset = from s in product.Lifestyle
                                   where s.Severity.SeverityRanking <= SeverityRankDI
                                        && s.Documentation.DocumentationRanking <= DocLevelRankDI
                                   select s;
                    if (resulset.ToList().Count > 0)
                    {
                        screeningResult.Append(Environment.NewLine);
                        screeningResult.Append("Screening for " + product.ProductName + Environment.NewLine);
                    }
                    foreach (var DFInteraction in resulset.ToList())
                    {                        
                            screeningResult.Append(Environment.NewLine + Convert.ToString(DFInteraction.ConceptName) + " in " + DFInteraction.LifestyleDrugClass.Name + " interacts with Drug " + product.ProductName + " ( " + Convert.ToString(GetSeveritysplit(DFInteraction.Severity.SeverityText)) + ", " + Convert.ToString(DFInteraction.Documentation.DocumentationText + ")") + Environment.NewLine);
                            screeningResult.Append( Environment.NewLine + Convert.ToString(DFInteraction.ProfessionalNotesAbbr) + Environment.NewLine + Environment.NewLine);
                    }
                   
                }
            }
            return screeningResult;
        }
        #endregion""

        #region Drug to Disease

        public StringBuilder PerformScreeningDrugToDisease(List<string> oPrescribing, List<string> oPrescribed)
        {
            StringBuilder screeningResult = new StringBuilder();
            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {

                    bool ignorePrescribed = false;

                    if (oPrescribed != null)
                    {
                        // AddPrescribedDrugsList(oPrescribed, ref prescribedProductid);
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }


                    prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType();
                    patientDemographics.Gender = GSGender;
                    patientDemographics.GenderSpecified = true;
                    patientDemographics.DateOfBirth = PatientDOB; //Convert.ToDateTime("1992-03-04"); 
                    patientDemographics.DateOfBirthSpecified = true;


                    List<GSDD5Service.ConditionIdentifierType> Diagnosed = new List<GSDD5Service.ConditionIdentifierType>();
                    Diagnosed = GetDxList(ICDCodes);

                    //requestedOperations.Allergy = false;
                    //requestedOperations.DuplicateTherapy = null;
                    //requestedOperations.AdverseReactions = null;
                    //requestedOperations.DrugToDrug = null;
                    //requestedOperations.Lifestyle = null;
                    //requestedOperations.Pregnancy = false;
                    //requestedOperations.PregnancyAndLactation = null;

                    requestedOperations.DuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType() { Request = false };
                    requestedOperations.Allergy = false;                    
                    requestedOperations.AdverseReactions = new GSDD5Service.ClinicalOperationAdverseReactionType() { Request = false };
                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType { Request = false };
                    requestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = false, InformationOnly = false };

                    requestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };
                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType { Request = false };
                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    requestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };



                    requestedOperations.DrugDisease = new GSDD5Service.ClinicalOperationDrugDiseaseType
                    {
                        Request = true,
                        DiagnosingCode = Diagnosed.ToArray(),
                        DiagnosedCode = Diagnosed.ToArray(),
                        IgnoreDiagnosed = false
                    };


                    packages = client.Clinical(
                        prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                        out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                        out filterPreferencesResponse);


                    screeningResult = ProccessDrugToDiseaseResults(products, marketedProducts, genericProductClinicals);

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningDrugToDisease : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
                requestedOperations = null;
            }
            return screeningResult;
        }

        private StringBuilder ProccessDrugToDiseaseResults(GSDD5Service.ClinicalInteractionProductType[] responcedDTD, GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts, GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals, ScreningResultType resultType = ScreningResultType.Details)
        {
            StringBuilder screeningResult = new StringBuilder();
            if (responcedDTD.Count() > 0)
            {
                foreach (var product in responcedDTD)
                {
                    if (product.DrugDisease != null)
                    {
                        if (product.DrugDisease.Disease != null || product.DrugDisease.Message != null)
                        {
                            if (resultType == ScreningResultType.Details)
                            {
                                screeningResult.Append(Environment.NewLine + string.Format(
                                    "Drug : {0}"
                                    , product.ProductName));

                                screeningResult.Append(Environment.NewLine + GetDrugToDiseaseMessages(product.DrugDisease) + Environment.NewLine + Environment.NewLine);
                            }
                            else
                            {
                                screeningResult.Append("Drug To Disease Interaction was found for drug :" + product.ProductName + Environment.NewLine);
                            }
                        }
                    }
                }
            }

            return screeningResult;
        }
        StringBuilder GetDrugToDiseaseMessages(GSDD5Service.ClinicalDrugDiseaseScreeningResponseType drugToDisease)
        {
            StringBuilder screeningResult = new StringBuilder();
            if (drugToDisease != null)
            {
                if (drugToDisease.Message != null || drugToDisease.Disease != null)
                {
                    if (drugToDisease.Message != null)
                    {
                        screeningResult.Append(drugToDisease.Message);
                    }
                    else
                    {
                        if (drugToDisease.Disease != null)
                        {
                            foreach (var DTDInteraction in drugToDisease.Disease)
                            {
                                if (DTDInteraction.Drug != null)
                                {                                    
                                        if (DTDInteraction.Drug.LevelOfEvidence != null)
                                        {
                                            foreach (var _LevelOfEvidence in DTDInteraction.Drug.LevelOfEvidence)
                                            {
                                                screeningResult.Append(Environment.NewLine + "Rating : " + _LevelOfEvidence.Rating);
                                                screeningResult.Append(Environment.NewLine + "Strength Of Recommendation : " + _LevelOfEvidence.StrengthOfRecommendation);
                                                screeningResult.Append( Environment.NewLine + DTDInteraction.Drug.LabelStatus);
                                            }
                                        }
                                   
                                }
                                screeningResult.Append(Environment.NewLine);

                                if (DTDInteraction.Condition != null)
                                {
                                    screeningResult.Append( DTDInteraction.Condition.Description.ToString());
                                    foreach (var _AgeBand in DTDInteraction.AgeBand)
                                    {
                                        screeningResult.Append(Environment.NewLine + "AgeBand : " + _AgeBand.AgeBandName);
                                        screeningResult.Append(Environment.NewLine + "Gender : " + _AgeBand.Gender);
                                    }
                                    screeningResult.Append(Environment.NewLine);
                                }
                            }
                        }
                    }
                }
            }
            return screeningResult;
        }
        #endregion

        #region Drug to Allergy
        public StringBuilder PerformScreeningDTA(List<string> oPrescribing, List<string> oPrescribed, List<gloGlobal.DIB.PIResp> ProductAllergies)
        {
            StringBuilder screeningResult = new StringBuilder();
            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
            List<int> _AllergiesReq = new List<int>();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {
                    bool ignorePrescribed = false;


                    if (oPrescribed != null)
                    {
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }

                    prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);


                    List<GSDD5Service.ProductIdentifierType> Allergies = new List<GSDD5Service.ProductIdentifierType>();
                    

                    GetProductAllergies(ProductAllergies, ref Allergies, ref _AllergiesReq);

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType
                    {
                        Gender = GSGender,
                        DateOfBirth = PatientDOB,
                        Allergy = new GSDD5Service.AllergyRequestType
                        {
                            Product = Allergies.ToArray(),
                            Ingredient = _AllergiesReq.ToArray()

                        }
                    };

                    requestedOperations.DuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType() { Request = false };                   
                    requestedOperations.AdverseReactions = new GSDD5Service.ClinicalOperationAdverseReactionType() { Request = false };
                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType { Request = false };
                    requestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = false, InformationOnly = false };

                    requestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };
                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType { Request = false };
                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    requestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };

                    requestedOperations.Allergy = true;

                    packages = client.Clinical(
                        prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                        out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                        out filterPreferencesResponse);

                    screeningResult = ProccessAllergyResults(products);



                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningDTA : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
                requestedOperations = null;
                _AllergiesReq = null;
            }
            return screeningResult;
        }

        private StringBuilder ProccessAllergyResults(GSDD5Service.ClinicalInteractionProductType[] responseDTA, ScreningResultType resultType = ScreningResultType.Details)
        {
            StringBuilder screeningResult = new StringBuilder();

            if (responseDTA.Count() > 0)
            {
                if (resultType == ScreningResultType.Details)
                {
                    screeningResult.Append(GetAllergyMessages(responseDTA));
                }
                else
                {
                    foreach (var product in responseDTA)
                    {
                        if (product.Allergy != null)
                        {
                            var allergyIngredient = from i in product.Allergy.Ingredient
                                                    select i;
                            if (allergyIngredient.ToList().Count > 0)
                            {
                                screeningResult.Append("Drug To Allergy was found for drug :" + product.ProductName + Environment.NewLine);
                            }
                        }
                    }
                }
            }
          
            return screeningResult;
        }

        StringBuilder GetAllergyMessages(GSDD5Service.ClinicalInteractionProductType[] products)
        {

            StringBuilder screeningResult = new StringBuilder();

            foreach (var product in products)
            {
                GSDD5Service.ClinicalAllergyResponseType allergy = product.Allergy;
                if (allergy != null)
                {
                    var allergyIngredient = from i in allergy.Ingredient
                                            select i;
                    if (allergyIngredient.ToList().Count > 0)
                    {
                        screeningResult.Append("Cross Reaction Match " + Environment.NewLine);
                        foreach (var PIngredient in allergy.Ingredient)
                        {
                            screeningResult.Append(Environment.NewLine + "The Use of " + product.ProductName + " may result in a Cross-Sensitivity reaction based on a reported history of allergy to " + PIngredient.Name);
                            screeningResult.Append("" + Environment.NewLine);
                            if (PIngredient.AllergyType.CrossSensitiveAllergy != null)
                            {
                                foreach (var _CrossSensitiveClass in PIngredient.AllergyType.CrossSensitiveAllergy)
                                {
                                    screeningResult.Append(Environment.NewLine + string.Format("Product Cross Sensitive Allergy Name : " + _CrossSensitiveClass.Name));
                                }
                            }

                        }
                    }

                }
            }
            return screeningResult;
        }

        private void GetProductAllergies(List<gloGlobal.DIB.PIResp> _ProductIDs, ref  List<GSDD5Service.ProductIdentifierType> _Allergies, ref  List<int> _AllergiesReq)
        {
            if (_ProductIDs != null)
            {
                foreach (var item in _ProductIDs)
                {
                    _Allergies.Add(new GSDD5Service.ProductIdentifierType
                    {
                        Identifier = item.pid.ToString(),
                        IdentifierType = GSDD5Service.ProductIdentifierEnum.ProductID
                    }

                    );
                    foreach (var iid in item.iid)
                    {
                        _AllergiesReq.Add(iid);
                    }
                }
            }
        }

        #endregion

        #region "DoseCheck"
        public StringBuilder PerformDoseCheck(string NdcCode, Int64 Quantity, Int16 Frequency, int Interval, string DIBServiceURL)
        {

            string unitCode = "";
            GSDD5Service.NCPDPUnitEnum valNCPDPUnitEnum = GSDD5Service.NCPDPUnitEnum.ea;
            using (gloGlobal.DIB.gloGSHelper oGSHelper = new gloGlobal.DIB.gloGSHelper(DIBServiceURL))
            {
                unitCode = oGSHelper.GetNCPDPBillingUnitID(NdcCode);
            }
            if (unitCode == "1")
            {
                valNCPDPUnitEnum = GSDD5Service.NCPDPUnitEnum.gm;
            }
            else if (unitCode == "2")
            {
                valNCPDPUnitEnum = GSDD5Service.NCPDPUnitEnum.ml;
            }
            else if (unitCode == "3")
            {
                valNCPDPUnitEnum = GSDD5Service.NCPDPUnitEnum.ea;
            }
            else if (unitCode == "")
            {
                valNCPDPUnitEnum = GSDD5Service.NCPDPUnitEnum.ea;
            }


            StringBuilder stringResponce = new StringBuilder();
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalRequestedOperationsType _RequestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
            GSDD5Service.ClinicalPrescribedType _Prescribing = null;
            List<GSDD5Service.PackageIdentifierClinicalType> _PrescribedDrugsList = new List<GSDD5Service.PackageIdentifierClinicalType>();
            GSDD5Service.ClinicalPrescribedType _PrescribedDrugs = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.PackageIdentifierClinicalType _Package = new GSDD5Service.PackageIdentifierClinicalType();
            stringResponce.Append("");
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {
                    
                    _Package.PackageIdentifier = new GSDD5Service.PackageIdentifierType();
                    _Package.PackageIdentifier.Identifier = NdcCode;
                    _Package.PackageIdentifier.IdentifierType = GSDD5Service.PackageIdentifierEnum.NDC11;

                    _Package.Dose = new GSDD5Service.DoseInputChoiceType();
                    _Package.Dose.DoseRequestType = GSDD5Service.DoseMeasurementEnum.SpecificProduct;
                    GSDD5Service.SpecificProductDoseInputType _SpecificProductDoseInput = new GSDD5Service.SpecificProductDoseInputType { Quantity = Quantity, Frequency = Frequency, Interval = Interval, UnitCode = valNCPDPUnitEnum, PatientExperience = GSDD5Service.PatientExperienceEnum.Experienced };

                    _Package.Dose.Item = _SpecificProductDoseInput;
                    _PrescribedDrugsList.Add(_Package);
                    _PrescribedDrugs.Package = _PrescribedDrugsList.ToArray();
                   
                    GSDD5Service.PatientDemographicsType _Demographics = new GSDD5Service.PatientDemographicsType();
                    _Demographics.DateOfBirth = PatientDOB;
                    _Demographics.GenderSpecified = true;
                    _Demographics.DateOfBirthSpecified = true;

                   
                    _RequestedOperations.DuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType() { Request = false };
                    _RequestedOperations.Allergy = false;
                    _RequestedOperations.DrugDisease = new GSDD5Service.ClinicalOperationDrugDiseaseType { Request = false };
                    _RequestedOperations.AdverseReactions = new GSDD5Service.ClinicalOperationAdverseReactionType() { Request = false };
                    _RequestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType { Request = false };
                    _RequestedOperations.DoseCheck = new GSDD5Service.ClinicalOperationDoseCheckType { Request = true, InformationOnly = false };

                    _RequestedOperations.IVCompatibility = new GSDD5Service.ClinicalOperationIVCompatibilityType { Request = false };
                    _RequestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType { Request = false };
                    _RequestedOperations.Pregnancy = false;
                    _RequestedOperations.PregnancyAndLactation = new GSDD5Service.ClinicalOperationPregnancyAndLactationType { Request = false };
                    _RequestedOperations.WarningLabels = new GSDD5Service.ClinicalOperationWarningLabelType { Request = false };

                    
                    packages = client.Clinical(_PrescribedDrugs, _Prescribing, _Demographics, _RequestedOperations, false, null,
                                out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                                out filterPreferencesResponse);


                    if (packages != null || packages.Length > 0)
                    {
                        stringResponce.Append(ProcessDosagecheckresult(packages[0].DoseCheck));
                    }

                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Interaction information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformScreeningDTA : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {               
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                _RequestedOperations = null;
                _PrescribedDrugsList = null;
                _PrescribedDrugs = null;
                _Package = null;

            }
            return stringResponce;

        }

        public string ProcessDosagecheckresult(GSDD5Service.DoseCheckResponseType responceDosecheck)
        {
            string DosecheckMessage = "";


            if (responceDosecheck != null)
            {
                if (responceDosecheck.AgeGroup != null)
                {
                    foreach (var AgeGroupItem in responceDosecheck.AgeGroup)
                    {

                        if (AgeGroupItem != null)
                        {
                            if (AgeGroupItem.Items1 != null)
                            {
                                foreach (GSDD5Service.DoseCheckDosingInfoType _DoseInfo in AgeGroupItem.Items1)
                                {
                                    if (_DoseInfo.Message != null)
                                    {
                                        if (_DoseInfo.Message.Identifier != 2)
                                        {
                                            DosecheckMessage += _DoseInfo.Message.Message;
                                            return DosecheckMessage;
                                        }
                                        else
                                        {
                                            return "";
                                        }

                                    }
                                }
                            }
                            else
                            {
                                if (AgeGroupItem.Message != null)
                                {
                                    DosecheckMessage += AgeGroupItem.Message.Message;
                                }
                            }

                        }


                    }
                }
                if (responceDosecheck.Message != null && DosecheckMessage.Trim().ToString() == "")
                {
                    var result = from s in responceDosecheck.Message
                                 select s;
                    foreach (var item in result.ToList())
                    {
                        DosecheckMessage += " " + item.Message;

                    }

                }

            }

            return DosecheckMessage;
        }


        #endregion

        private GSDD5Service.ProductIdentifierClinicalType[] AddPrescribingDrugsList(List<string> productIDs)
        {
            var productList = new List<GSDD5Service.ProductIdentifierClinicalType>();
            try
            {
                if (productIDs != null)
                {

                    foreach (var productid in productIDs)
                    {
                        var productIdentifierClinicalType = new GSDD5Service.ProductIdentifierClinicalType();
                        productIdentifierClinicalType.Dose = null;
                        productIdentifierClinicalType.ProductIdentifier = new GSDD5Service.ProductIdentifierType()
                        {
                            Identifier = productid.ToString(),
                            IdentifierType = GSDD5Service.ProductIdentifierEnum.ProductID
                        };
                        productList.Add(productIdentifierClinicalType);
                    }
                    //if (productList.Count > 0)
                    //{
                    //    prescribing.Product = productList.ToArray();
                    //}
                    return productList.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;              
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "AddPrescribingDrugsList : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
            //finally
            //{
            //    productList = null;
            //}
        }

        private GSDD5Service.ProductIdentifierClinicalType[] AddPrescribedDrugsList(List<string> productIDs)
        {
            var productList = new List<GSDD5Service.ProductIdentifierClinicalType>();
            try
            {
                if (productIDs != null)
                {
                    foreach (var productid in productIDs)
                    {
                        var productIdentifierClinicalType = new GSDD5Service.ProductIdentifierClinicalType();
                        productIdentifierClinicalType.Dose = null;
                        productIdentifierClinicalType.ProductIdentifier = new GSDD5Service.ProductIdentifierType()
                        {
                            Identifier = productid.ToString(),
                            IdentifierType = GSDD5Service.ProductIdentifierEnum.ProductID
                        };
                        productList.Add(productIdentifierClinicalType);
                    }

                    return productList.ToArray();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;               
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "AddPrescribedDrugsList : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return null;
            }
           
        }

        private List<GSDD5Service.ConditionIdentifierType> GetDxList(DataTable ICDCodeList)
        {
            List<GSDD5Service.ConditionIdentifierType> Diagnosed = new List<GSDD5Service.ConditionIdentifierType>();
            if (ICDCodeList != null)
            {
                for (int i = 0; i <= ICDCodeList.Rows.Count - 1; i++)
                {
                    GSDD5Service.ConditionIdentifierType dxcode = new GSDD5Service.ConditionIdentifierType();

                    dxcode.Identifier = ICDCodeList.Rows[i]["ICDCode"].ToString();
                    if (ICDCodeList.Rows[i]["ICDType"].ToString() == "9")
                    {
                        dxcode.IdentifierType = GSDD5Service.ConditionEnum.ICD9;
                    }
                    else
                    {
                        dxcode.IdentifierType = GSDD5Service.ConditionEnum.ICD10;
                    }

                    Diagnosed.Add(dxcode);
                }
            }
            return Diagnosed;
        }

        public void Dispose()
        {
            if (binding != null) { binding = null; }
            if (endpointAddress != null) { endpointAddress = null; }
        }

        public enum ScreningResultType
        { 
            Details,
            AlertOnly
        }

        public StringBuilder PerformDrugAlert(List<string> oPrescribing, List<string> oPrescribed, List<gloGlobal.DIB.PIResp> ProductAllergies, DataTable dtICD, ScreningResultType resultType)
        {
            StringBuilder screeningResult = new StringBuilder();

            GSDD5Service.FilterComboPreferencesType filterPreferences = null;
            GSDD5Service.ClinicalInteractionPackageType[] packages;
            GSDD5Service.ClinicalInteractionProductType[] products;
            GSDD5Service.ClinicalMarketedSpecificProductType[] marketedProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] specificProducts;
            GSDD5Service.ClinicalMarketedSpecificProductType[] genericProductClinicals;
            GSDD5Service.ClinicalIVCompatibilityResponseType ivCompatibility;
            GSDD5Service.NotCheckedType notChecked;
            GSDD5Service.FilterComboPreferencesResponseType filterPreferencesResponse;
            GSDD5Service.ClinicalPrescribedType prescribedProductid = new GSDD5Service.ClinicalPrescribedType();
            GSDD5Service.ClinicalPrescribedType prescribingProductid = new GSDD5Service.ClinicalPrescribedType();

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                using (var client = new GSDD5Service.GSDDServiceSoapClient(binding, endpointAddress))
                {
                    bool ignorePrescribed = false;

                    if (oPrescribed != null)
                    {
                        prescribedProductid.Product = AddPrescribedDrugsList(oPrescribed);
                    }
                    else
                    {
                        prescribedProductid = null;
                        ignorePrescribed = true;
                    }

                    if (oPrescribing != null)
                    {
                        prescribingProductid.Product = AddPrescribingDrugsList(oPrescribing);
                    }
                    else
                    {
                        prescribingProductid = null;
                    }

                    GSDD5Service.PatientDemographicsType patientDemographics = new GSDD5Service.PatientDemographicsType();
                    patientDemographics.Gender = GSGender;
                    patientDemographics.GenderSpecified = true;
                    patientDemographics.DateOfBirth = PatientDOB;
                    patientDemographics.DateOfBirthSpecified = true;

                    GSDD5Service.ClinicalRequestedOperationsType requestedOperations = new GSDD5Service.ClinicalRequestedOperationsType();
                    //Allergy
                    List<GSDD5Service.ProductIdentifierType> _Allergies = new List<GSDD5Service.ProductIdentifierType>();
                    List<int> _AllergiesReq = new List<int>();
                    List<GSDD5Service.ProductIdentifierType> Allergies = new List<GSDD5Service.ProductIdentifierType>();
                    GetProductAllergies(ProductAllergies, ref Allergies, ref _AllergiesReq);

                    patientDemographics.Allergy = new GSDD5Service.AllergyRequestType
                    {
                        Product = _Allergies.ToArray(),
                        Ingredient = _AllergiesReq.ToArray()
                    };
                    requestedOperations.Allergy = DrugToAllergyEnable;

                    //Drug To Drug
                    requestedOperations.DrugToDrug = new GSDD5Service.ClinicalOperationDrugToDrugType
                    {
                        Request = DrugToDrugEnable,
                        IncludeConsumerNotes = DrugToDrugEnable,
                        IncludeProfessionalNotes = DrugToDrugEnable

                    };

                    //Drug To Food
                    requestedOperations.Lifestyle = new GSDD5Service.ClinicalOperationLifestyleType
                    {
                        Request = DrugToFoodEnable,
                        IncludeConsumerNotes = DrugToFoodEnable,
                        IncludeProfessionalNotes = DrugToFoodEnable
                    };

                    //DT
                    GSDD5Service.ClinicalOperationDuplicateTherapyType ReqDuplicateTherapy = new GSDD5Service.ClinicalOperationDuplicateTherapyType();
                    ReqDuplicateTherapy.Request = DuplicateTherapyEnable;
                    ReqDuplicateTherapy.ControlledSubstanceMaxOccurrences = 0;
                    requestedOperations.DuplicateTherapy = ReqDuplicateTherapy;


                    //ADR
                    GSDD5Service.ClinicalOperationAdverseReactionType ReqAdverseReaction = new GSDD5Service.ClinicalOperationAdverseReactionType();

                    ReqAdverseReaction.Request = AdverseDrugEffectEnable;
                    ReqAdverseReaction.Filter = new GSDD5Service.AdverseReactionFilterType
                    {
                        Severity = Severity, //GSDD5Service.ADRSeverityEnum.Severe,
                        Onset = AdeOnset,
                        SeveritySpecified = true,
                        OnsetSpecified = true
                    };
                    requestedOperations.AdverseReactions = ReqAdverseReaction;

                    //Drug to Diseas

                    List<GSDD5Service.ConditionIdentifierType> Diagnosed = new List<GSDD5Service.ConditionIdentifierType>();
                    Diagnosed = GetDxList(dtICD);

                    requestedOperations.DrugDisease = new GSDD5Service.ClinicalOperationDrugDiseaseType
                    {
                        Request = DrugToDiseaseEnable,
                        DiagnosingCode = Diagnosed.ToArray(),
                        DiagnosedCode = Diagnosed.ToArray(),
                        IgnoreDiagnosed = false
                    };

                    requestedOperations.Pregnancy = false;
                    requestedOperations.PregnancyAndLactation = null;


                    packages = client.Clinical(
                        prescribedProductid, prescribingProductid, patientDemographics, requestedOperations, ignorePrescribed, filterPreferences,
                        out products, out marketedProducts, out specificProducts, out genericProductClinicals, out ivCompatibility, out notChecked,
                        out filterPreferencesResponse);
                    Cursor.Current = Cursors.Default;

                    string productidentifier = string.Empty;
                    if (prescribingProductid != null)
                    {
                        productidentifier = Convert.ToString(prescribingProductid.Product[0].ProductIdentifier.Identifier);
                    }
                    else
                    {
                        if (prescribedProductid != null)
                        {
                            productidentifier = Convert.ToString(prescribedProductid.Product[0].ProductIdentifier.Identifier);
                        }
                    }
                    if (productidentifier != "")
                    {
                        if (DrugInteractionAlertExist(products, productidentifier))
                        {
                            if (resultType == ScreningResultType.Details)
                            {
                                //Hode Progress bar

                                HideDiProgressPanel();

                                //if (handler != null)
                                //{
                                //    handler(null,null);
                                //}
                               

                                if (MessageBox.Show("Drug interaction found for newly prescribed drug", "gloEMR", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                {
                                    screeningResult = ProccessScreeningResultAlerts(products, marketedProducts, genericProductClinicals, resultType);
                                }
                                else
                                {
                                    screeningResult = null;
                                }
                            }
                            else
                            {
                                screeningResult = ProccessScreeningResultAlerts(products, marketedProducts, genericProductClinicals, resultType);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Unable to retrieve Drug Alert information.", "Error Calling Clinical Method!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.DrugInteraction, gloAuditTrail.ActivityType.None, "PerformDrugAlert : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                Cursor.Current = Cursors.Default;

                filterPreferences = null;
                packages = null;
                products = null;
                marketedProducts = null;
                specificProducts = null;
                genericProductClinicals = null;
                notChecked = null;
                filterPreferencesResponse = null;
                prescribedProductid = null;
                prescribingProductid = null;
            }
            return screeningResult;
        }


        private bool DrugInteractionAlertExist(GSDD5Service.ClinicalInteractionProductType[] products, string prescribingDrug) 
        {
            foreach (GSDD5Service.ClinicalInteractionProductType product in products)
            {
                if (product != null)
                {
                    if (product.ProductIdentifier.Identifier.ToString().Trim() == prescribingDrug.Trim())
                    {
                        if (product.Allergy != null) { return true; }
                        if (product.Drug != null) { return true; }
                        if (product.DrugDisease != null)
                        {
                            if (product.DrugDisease.Disease != null || product.DrugDisease.Message != null) { return true; }
                        }
                        if (product.AdverseReactions != null)
                        {
                            if (product.AdverseReactions.AdverseReaction != null) {return true; }
                        }
                        if (product.DuplicateTherapy != null)  {return true; }
                        if (product.Lifestyle != null)  {return true; }
                    }
                }
            }
            return false;
        }
    }

}
