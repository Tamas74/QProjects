using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace gloRxHub
{
    public class ClsRxH_271Master
    {

        #region "Private and public variables"

        private string sSTLoopCount = "";

        private Int64 nPatientId = 0;
        private string sISA_ControlNumber = "";//to get the ISA_Control number 
        private string sInformationRecieverName = "";//to show in the Information Reciever text box - Physician Name

        private string sNPINumber = "";//added for ANSI 5010 to show -- Physician Name

        private string sInformationRecieverSuffix = "";//to show in the Information Reciever Suffix text box - Physician Suffix
        private string sInformationSourceName = "";//to show in the Information Source text box
        private string sPayerParticipantId = "";//to show in the PayerParticipantId text box
        private string sCardHolderId = "";//to show in the CardhilderId text box
        private string sHealthPlanName = "";//to show in the Health Plan Name

        private string sHealthPlanBenefitCoverageName = "";//Added for ANSI 5010

        private string sIsDependentdemoChange = "";//to show in the IsDependentdemoChange change information flag

        private string sIsRetailPharmacyEligible = "";//to show whether Retail pharmacy eligible or not --from ANSI 5010
        private string sRetailPharmacyCoveragePlanName = "";//to show pharmacy coverage plan name--from ANSI 5010
        private string sRetailPharmacyEligiblityorBenefitInfo = "";//to show whether Retail pharmacy eligible or not --from ANSI 5010
        private string sIsMailOrdRxDrugEligible = "";//to show whether Mail order prescription eligible or not
        private string sMailOrdRxDrugCoveragePlanName = "";//to show Mail order prescription coverage plan name
        private string sMailOrdRxDrugEligiblityorBenefitInfo = "";//to show Mail Ord Rx Drug eligiblity or benefit info( active coverage / inactive etc etc)

        private string sHealthPlanBenefitEligibilityInfo = "";//from ANSI 5010 to show Active/Inactive etc


        private string sConsent = "";
        
        private string sMessageType = "";//whether the message is 271 or 997. if nothing then it is TA1

        private string sMessageId ="";
        private string sPayerName = "";
        private string sMemberId = "";
        private string sHealthPlanNumber = "";
        private string sRelationshipCode = "";
        private string sRelationshipDescription = "";//this will show the description ie either Self - 18, else it is Dependant
        private string sPersonCode = "";
        private string sCardHolderName = "";
        private string sGroupId = "";
        private string sGroupName = "";
        private string sEmployeeId = "";//this comes under the A6 element tag of 271 response file
        private string sFormularyListId = "";
        private string sAlternativeListId = "";
        private string sCoverageId = "";
        private string sBINNumber = "";
        private string sCopayId = "";
        private string sDFirstName = "";
        private string sDMiddleName = "";
        private string sDLastName = "";
        private string sDGender = "";
        private string sDDOB = "";
        private string sDSSN = "";
        private string sDAddress1 = "";
        private string sDAddress2 = "";
        private string sDCity = "";
        private string sDState = "";
        private string sDZip = "";
        private string sEligiblityDate = "";//Eligiblity date returned n 271 in DTP section
        private string sServiceDate = "";//Service date returned n 271 in DTP section

        private string sSocialSecurityNumber = "";//Added for ANSI 5010
        private string sPatientAccountNumber = "";//Added for ANSI 5010

        private string sLTCPhEligiblityorBenefitInfo = "";//Added for ANSI 5010
        private string sSpecialityPhEligiblityorBenefitInfo = "";//Added for ANSI 5010
        private string sLTCPharmCovName = "";//Added for ANSI 5010
        private string sSpecialtyPharmCovName = "";//Added for ANSI 5010
        private string sHlthPlnBenftCovEligibilityDate = "";//Added for ANSI 5010
        private string sHlthPlnBenftCovServiceDate = "";//Added for ANSI 5010
        private string sRetailOrdPhrmEligibilityDate = "";//Added for ANSI 5010
        private string sRetailPhrmServiceDate = "";//Added for ANSI 5010
        private string sMailOrdPhrmEligibilityDate = "";//Added for ANSI 5010
        private string sMailOrdPhrmServiceDate = "";//Added for ANSI 5010
        private string sLTCPhrmEligDate = "";//Added for ANSI 5010
        private string sLTCPhrmServiceDate = "";//Added for ANSI 5010
        private string sSpecialtyPhrmEligDate = "";//Added for ANSI 5010
        private string sSpecialtyPhrmServiceDate = "";//Added for ANSI 5010

        private string sDependentdemochgFirstName	= "";
        private string sDependentdemoChgMiddleName	= "";
        private string sDependentdemoChgLastName	= "";
        private string sDependentdemoChgGender	= "";
        private string sDependentdemoChgDOB	= "";
        private string sDependentdemoChgSSN	= "";
        private string sDependentdemoChgAddress1= "";	
        private string sDependentdemoChgAddress2	= "";
        private string sDependentdemoChgCity	= "";
        private string sDependentdemoChgState	= "";
        private string sDependentdemoChgZip = "";
        List<ClsRxH_271Details> objRxH_271Details = new List<ClsRxH_271Details>();

        private bool disposedValue = false;

        //New Fields for Subscriber
        private string sMailOrderInsTypeCode = "";
        private string sRetailInsTypeCode = "";
        private string sMailOrderMonetaryAmount = "";
        private string sRetailMonetaryAmount = "";
        
        //New Fields Added for Contracted Provider(13) And Primary Payer(PRP)
        private string sIsContractedProvider = "";
        private string sContractedProviderName = "";
        private string sContractedProviderNumber = "";
        private string sContProvMailOrderEligible = "";
        private string sContProvMailOrderCoverageInfo = "";
        private string sContProvMailOrderInsTypeCode = "";
        private string sContProvMailOrderMonetaryAmt = "";
        private string sContProvRetailsEligible = "";
        private string sContProvRetailCoverageInfo = "";
        private string sContProvRetailInsTypeCode = "";
        private string sContProvRetailMonetaryAmt = "";

        //New Fields Added for Primary Payer(PRP)
        private string sIsPrimaryPayer = "";
        private string sPrimaryPayerName = "";
        private string sPrimaryPayerNumber = "";
        private string sPrimaryPayerMailOrderEligible = "";
        private string sPrimaryPayerMailOrderCoverageInfo = "";
        private string sPrimaryPayerMailOrderInsTypeCode = "";
        private string sPrimaryPayerMailOrderMonetaryAmt = "";
        private string sPrimaryPayerRetailsEligible = "";
        private string sPrimaryPayerRetailCoverageInfo = "";
        private string sPrimaryPayerRetailInsTypeCode = "";
        private string sPrimaryPayerRetailMonetaryAmt = "";

        private string sHlthPlnCovInsTypeCode = "";//Added from ANSI 5010
        private string sLTCPharmacyInsTypeCode = "";//Added from ANSI 5010
        private string sSpecialtyPharmacyInsTypeCode = "";//Added from ANSI 5010

        private string sTRNReferenceIdentification = "";//Added from ANSI 5010
        private string sTRNOrignationCompanyIdentifier = "";//Added from ANSI 5010
        private string sTRNDivisionorGroup = "";//Added from ANSI 5010

         #endregion "Private and public veriables"
        // IDisposable 

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion 


        #region "Property Procedures for Rxh_271Master"

        //to save in the 271ResponceDetails table 
        //will be either 271/997. if blanke means it is TA1

        public string STLoopCount
        {
            get { return sSTLoopCount; }
            set { sSTLoopCount = value; }

        }


        public string MessageType
        {
            get { return sMessageType; }
            set { sMessageType = value; }

        }

        public string Consent
        {
            get { return sConsent; }
            set { sConsent = value; }
        }
               

        public Int64 PatientId
        {
            get { return nPatientId; }
            set { nPatientId = value; }

        }

        public string ISA_ControlNumber
        {
            get { return sISA_ControlNumber; }
            set { sISA_ControlNumber = value; }

        }

        //Physician Name
        public string InformationRecieverName
        {
            get { return sInformationRecieverName; }
            set { sInformationRecieverName = value; }

        }

        //Added for ANSI 5010 --Physician NPI number
        public string NPINumber
        {
            get { return sNPINumber; }
            set { sNPINumber = value; }

        }

        //Physician Suffix
        public string InformationRecieverSuffix
        {
            get { return sInformationRecieverSuffix; }
            set { sInformationRecieverSuffix = value; }

        }


        public string InformationSourceName
        {
            get { return sInformationSourceName; }
            set { sInformationSourceName = value; }

        }

        public string PayerParticipantId
        {
            get { return sPayerParticipantId; }
            set { sPayerParticipantId = value; }

        }
        public string CardHolderId
        {
            get { return sCardHolderId; }
            set { sCardHolderId = value; }

        }

        public string SocialSecurityNumber
        {
            get { return sSocialSecurityNumber; }
            set { sSocialSecurityNumber = value; }

        }
        public string PatientAccountNumber
        {
            get { return sPatientAccountNumber; }
            set { sPatientAccountNumber = value; }

        }
        //public string IsDemographicsChanged
        //{
        //    get { return sIsDemographicsChanged; }
        //    set { sIsDemographicsChanged = value; }

        //}
      //Eligiblity date returned n 271 in DTP section
       
        public string EligiblityDate
        {
            get { return sEligiblityDate; }
            set { sEligiblityDate = value; }

        }
 //Service date returned n 271 in DTP section
        public string ServiceDate
        {
            get { return sServiceDate; }
            set { sServiceDate = value; }

        }


        //Added from ANSI 5010
        public string LTCPharmCovName
        {
            get { return sLTCPharmCovName; }
            set { sLTCPharmCovName = value; }

        }
        //Added from ANSI 5010 for LTC Status
        public string LTCPhEligiblityorBenefitInfo
        {
            get { return sLTCPhEligiblityorBenefitInfo; }
            set { sLTCPhEligiblityorBenefitInfo = value; }

        }
        //Added from ANSI 5010 for Speciality Status
        public string SpecialityPhEligiblityorBenefitInfo
        {
            get { return sSpecialityPhEligiblityorBenefitInfo; }
            set { sSpecialityPhEligiblityorBenefitInfo = value; }

        }
        //Added from ANSI 5010
        public string SpecialtyPharmCovName
        {
            get { return sSpecialtyPharmCovName; }
            set { sSpecialtyPharmCovName = value; }

        }

        //Added from ANSI 5010
        public string HlthPlnBenftCovEligibilityDate
        {
            get { return sHlthPlnBenftCovEligibilityDate; }
            set { sHlthPlnBenftCovEligibilityDate = value; }

        }

        //Added from ANSI 5010
        public string HlthPlnBenftCovServiceDate
        {
            get { return sHlthPlnBenftCovServiceDate; }
            set { sHlthPlnBenftCovServiceDate = value; }

        }

        //Added from ANSI 5010
        public string RetailOrdPhrmEligibilityDate
        {
            get { return sRetailOrdPhrmEligibilityDate; }
            set { sRetailOrdPhrmEligibilityDate = value; }

        }

        //Added from ANSI 5010
        public string RetailPhrmServiceDate
        {
            get { return sRetailPhrmServiceDate; }
            set { sRetailPhrmServiceDate = value; }

        }

        //Added from ANSI 5010
        public string MailOrdPhrmEligibilityDate
        {
            get { return sMailOrdPhrmEligibilityDate; }
            set { sMailOrdPhrmEligibilityDate = value; }

        }

        //Added from ANSI 5010
        public string MailOrdPhrmServiceDate
        {
            get { return sMailOrdPhrmServiceDate; }
            set { sMailOrdPhrmServiceDate = value; }

        }

        //Added from ANSI 5010
        public string LTCPhrmEligDate
        {
            get { return sLTCPhrmEligDate; }
            set { sLTCPhrmEligDate = value; }

        }

        //Added from ANSI 5010
        public string LTCPhrmServiceDate
        {
            get { return sLTCPhrmServiceDate; }
            set { sLTCPhrmServiceDate = value; }

        }


        //Added from ANSI 5010
        public string SpecialtyPhrmEligDate
        {
            get { return sSpecialtyPhrmEligDate; }
            set { sSpecialtyPhrmEligDate = value; }

        }
        //Added from ANSI 5010
        public string SpecialtyPhrmServiceDate
        {
            get { return sSpecialtyPhrmServiceDate; }
            set { sSpecialtyPhrmServiceDate = value; }

        }



        public string IsDependentdemoChange
        {
            get { return sIsDependentdemoChange; }
            set { sIsDependentdemoChange = value; }

        }


        public string IsRetailPharmacyEligible
        {
            get { return sIsRetailPharmacyEligible; }
            set { sIsRetailPharmacyEligible = value; }

        }

        public string RetailPharmacyCoveragePlanName
        {
            get { return sRetailPharmacyCoveragePlanName; }
            set { sRetailPharmacyCoveragePlanName = value; }

        }
        public string RetailPharmacyEligiblityorBenefitInfo
        {
            get { return sRetailPharmacyEligiblityorBenefitInfo; }
            set { sRetailPharmacyEligiblityorBenefitInfo = value; }

        }

        public string HealthPlanBenefitEligibilityInfo
        {
            get { return sHealthPlanBenefitEligibilityInfo; }
            set { sHealthPlanBenefitEligibilityInfo = value; }

        }


        public string IsMailOrdRxDrugEligible
        {
            get { return sIsMailOrdRxDrugEligible; }
            set { sIsMailOrdRxDrugEligible = value; }

        }

        public string MailOrdRxDrugCoveragePlanName
        {
            get { return sMailOrdRxDrugCoveragePlanName; }
            set { sMailOrdRxDrugCoveragePlanName = value; }

        }
        public string MailOrdRxDrugEligiblityorBenefitInfo
        {
            get { return sMailOrdRxDrugEligiblityorBenefitInfo; }
            set { sMailOrdRxDrugEligiblityorBenefitInfo = value; }

        }

        public string MessageID
        {
            get { return sMessageId; }
            set { sMessageId = value; }
                 
        }
        public string PayerName
        {
            get { return sPayerName; }
            set { sPayerName = value; }
        }

        //PBM_Payer Unique member ID
        public string MemberID
        {
            get { return sMemberId; }
            set { sMemberId = value; }
        
        }

        public string HealthPlanNumber
        {
            get { return sHealthPlanNumber; }
            set { sHealthPlanNumber = value; }
        }

        public string HealthPlanName
        {
            get { return sHealthPlanName; }
            set { sHealthPlanName = value; }
        }

        //Added for ANSI 5010
        public string HealthPlanBenefitCoverageName
        {
            get { return sHealthPlanBenefitCoverageName; }
            set { sHealthPlanBenefitCoverageName = value; }
        }
        public string RelationshipCode
        {
            get { return sRelationshipCode; }
            set { sRelationshipCode = value; }
        }

        public string RelationshipDescription
        {
            get { return sRelationshipDescription; }
            set { sRelationshipDescription = value; }
        }
        public string PersonCode
        {
            get { return sPersonCode; }
            set { sPersonCode = value; }
        }
        public string CardHolderName
        {
            get { return sCardHolderName; }
            set { sCardHolderName = value; }
        }
        public string GroupId
        {
            get { return sGroupId; }
            set { sGroupId = value; }
        }
        public string GroupName
        {
            get { return sGroupName; }
            set { sGroupName = value; }
        }
        
         public string EmployeeId
        {
            get { return sEmployeeId; }
            set { sEmployeeId= value; }
        }
        public string FormularyListId
        {
            get { return sFormularyListId; }
            set { sFormularyListId = value; }
        }
        public string AlternativeListId
        {
            get { return sAlternativeListId; }
            set { sAlternativeListId = value; }
        }
        public string CoverageId
        {
            get { return sCoverageId; }
            set { sCoverageId = value; }
        }
        public string BINNumber
        {
            get { return sBINNumber; }
            set { sBINNumber = value; }
        }
        public string CopayId
        {
            get { return sCopayId; }
            set { sCopayId = value; }
        }
      

        public string DFirstName
        {
            get { return sDFirstName; }
            set { sDFirstName = value; }
        }

        public string DLastName
        {
            get { return sDLastName; }
            set { sDLastName = value; }
        }

        public string DMiddleName
        {
            get { return sDMiddleName; }
            set { sDMiddleName = value; }
        }
        public string DGender
        {
            get { return sDGender; }
            set { sDGender = value; }
        }
        public string DDOB
        {
            get { return sDDOB; }
            set { sDDOB = value; }
        }
        public string DSSN
        {
            get { return sDSSN; }
            set { sDSSN = value; }
        }
        public string DAddress1
        {
            get { return sDAddress1; }
            set { sDAddress1 = value; }
        }

         public string DAddress2
        {
            get { return sDAddress2; }
            set { sDAddress2 = value; }
        }

        public string DCity
        {
            get { return sDCity; }
            set { sDCity = value; }
        }

        public string DState
        {
            get { return sDState; }
            set { sDState = value; }
        }

        public string DZip
        {
            get { return sDZip; }
            set { sDZip = value; }
        }

        
       //Dependant Demographic changed
        	

        public string DependentdemochgFirstName
        {
            get { return sDependentdemochgFirstName; }
            set { sDependentdemochgFirstName = value; }
        }

        public string DependentdemoChgMiddleName
        {
            get { return sDependentdemoChgMiddleName; }
            set { sDependentdemoChgMiddleName = value; }
        }

	
        public string DependentdemoChgLastName
        {
            get { return sDependentdemoChgLastName; }
            set { sDependentdemoChgLastName = value; }
        }

                	
	    public string DependentdemoChgGender
        {
            get { return sDependentdemoChgGender; }
            set { sDependentdemoChgGender = value; }
        }

                    	
	    public string DependentdemoChgDOB
        {
            get { return sDependentdemoChgDOB; }
            set { sDependentdemoChgDOB = value; }
        }
	
	   public string DependentdemoChgSSN
        {
            get { return sDependentdemoChgSSN; }
            set { sDependentdemoChgSSN = value; }
        }

          public string DependentdemoChgAddress1
        {
            get { return sDependentdemoChgAddress1; }
            set { sDependentdemoChgAddress1 = value; }
        }
	
	 public string DependentdemoChgAddress2
        {
            get { return sDependentdemoChgAddress2; }
            set { sDependentdemoChgAddress2 = value; }
        }
	
         public string DependentdemoChgCity
        {
            get { return sDependentdemoChgCity; }
            set { sDependentdemoChgCity = value; }
        }
	
        
         public string DependentdemoChgState
        {
            get { return sDependentdemoChgState; }
            set { sDependentdemoChgState = value; }
        }

        public string DependentdemoChgZip
        {
            get { return sDependentdemoChgZip; }
            set { sDependentdemoChgZip = value; }
        }


        //Dependant Demographic changed

        //New Fields for Subscriber
    
             public string MailOrderInsTypeCode
        {
            get { return sMailOrderInsTypeCode; }
            set { sMailOrderInsTypeCode = value; }
        }
        public string RetailInsTypeCode
        {
            get { return sRetailInsTypeCode; }
            set { sRetailInsTypeCode = value; }
        }
        public string MailOrderMonetaryAmount
        {
            get { return sMailOrderMonetaryAmount; }
            set { sMailOrderMonetaryAmount = value; }
        }
        public string RetailMonetaryAmount
        {
            get { return sRetailMonetaryAmount; }
            set { sRetailMonetaryAmount = value; }
        }


        //New Fields Added for Contracted Provider(13) And Primary Payer(PRP)
        public string IsContractedProvider
        {
            get { return sIsContractedProvider; }
            set { sIsContractedProvider = value; }
        }
        public string ContractedProviderName
        {
            get { return sContractedProviderName; }
            set { sContractedProviderName = value; }
        }
        public string ContractedProviderNumber
        {
            get { return sContractedProviderNumber; }
            set { sContractedProviderNumber = value; }
        }
         public string ContProvMailOrderEligible
        {
            get { return sContProvMailOrderEligible; }
            set { sContProvMailOrderEligible = value; }
        }
        public string ContProvMailOrderCoverageInfo
        {
            get { return sContProvMailOrderCoverageInfo; }
            set { sContProvMailOrderCoverageInfo = value; }
        }
         public string ContProvMailOrderInsTypeCode
        {
            get { return sContProvMailOrderInsTypeCode; }
            set { sContProvMailOrderInsTypeCode = value; }
        }
        public string ContProvMailOrderMonetaryAmt
        {
            get { return sContProvMailOrderMonetaryAmt; }
            set { sContProvMailOrderMonetaryAmt = value; }
        }
         public string ContProvRetailsEligible
        {
            get { return sContProvRetailsEligible; }
            set { sContProvRetailsEligible = value; }
        }
        public string ContProvRetailCoverageInfo
        {
            get { return sContProvRetailCoverageInfo; }
            set { sContProvRetailCoverageInfo = value; }
        }
          public string ContProvRetailInsTypeCode
        {
            get { return sContProvRetailInsTypeCode; }
            set { sContProvRetailInsTypeCode = value; }
        }
        public string ContProvRetailMonetaryAmt
        {
            get { return sContProvRetailMonetaryAmt; }
            set { sContProvRetailMonetaryAmt = value; }
        }

        //New Fields Added for Primary Payer(PRP)
        public string IsPrimaryPayer
        {
            get { return sIsPrimaryPayer; }
            set { sIsPrimaryPayer = value; }
        }
         public string PrimaryPayerName
        {
            get { return sPrimaryPayerName; }
            set { sPrimaryPayerName = value; }
        }
          public string PrimaryPayerNumber
        {
            get { return sPrimaryPayerNumber; }
            set { sPrimaryPayerNumber = value; }
        }
         public string PrimaryPayerMailOrderEligible
        {
            get { return sPrimaryPayerMailOrderEligible; }
            set { sPrimaryPayerMailOrderEligible = value; }
        }
          public string PrimaryPayerMailOrderCoverageInfo
        {
            get { return sPrimaryPayerMailOrderCoverageInfo; }
            set { sPrimaryPayerMailOrderCoverageInfo = value; }
        }
        public string PrimaryPayerMailOrderInsTypeCode
        {
            get { return sPrimaryPayerMailOrderInsTypeCode; }
            set { sPrimaryPayerMailOrderInsTypeCode = value; }
        }
        public string PrimaryPayerMailOrderMonetaryAmt
        {
            get { return sPrimaryPayerMailOrderMonetaryAmt; }
            set { sPrimaryPayerMailOrderMonetaryAmt = value; }
        }
         public string PrimaryPayerRetailsEligible
        {
            get { return sPrimaryPayerRetailsEligible; }
            set { sPrimaryPayerRetailsEligible = value; }
        }
              public string PrimaryPayerRetailCoverageInfo
        {
            get { return sPrimaryPayerRetailCoverageInfo; }
            set { sPrimaryPayerRetailCoverageInfo = value; }
        }
         public string PrimaryPayerRetailInsTypeCode
        {
            get { return sPrimaryPayerRetailInsTypeCode; }
            set { sPrimaryPayerRetailInsTypeCode = value; }
        }
         public string PrimaryPayerRetailMonetaryAmt
        {
            get { return sPrimaryPayerRetailMonetaryAmt; }
            set { sPrimaryPayerRetailMonetaryAmt = value; }
        }


        public string HlthPlnCovInsTypeCode
        {
            get { return sHlthPlnCovInsTypeCode; }
            set { sHlthPlnCovInsTypeCode = value; }
        }

        public string LTCPharmacyInsTypeCode
        {
            get { return sLTCPharmacyInsTypeCode; }
            set { sLTCPharmacyInsTypeCode = value; }
        }

        public string SpecialtyPharmacyInsTypeCode
        {
            get { return sSpecialtyPharmacyInsTypeCode; }
            set { sSpecialtyPharmacyInsTypeCode = value; }
        }


        public string TRNReferenceIdentification
        {
            get { return sTRNReferenceIdentification; }
            set { sTRNReferenceIdentification = value; }
        }

        public string TRNOrignationCompanyIdentifier
        {
            get { return sTRNOrignationCompanyIdentifier; }
            set { sTRNOrignationCompanyIdentifier = value; }
        }


        public string TRNDivisionorGroup
        {
            get { return sTRNDivisionorGroup; }
            set { sTRNDivisionorGroup = value; }
        }


        public List<ClsRxH_271Details> RxH_271Details
        {
            get { return objRxH_271Details; }
            set { objRxH_271Details = value; }
        }
   #endregion "Property Procedures for rxh_271Master"

        


    }

    public partial class Cls271Information : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Destructor"

        public Cls271Information()
        {
            _innerlist = new ArrayList();
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


        ~Cls271Information()
        {
            Dispose(false);
        }
        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(ClsRxH_271Master item)
        {
            _innerlist.Add(item);
        }

        //public void Add(ClsRxH_271Master obj271Master)
        //{
        //    ClsRxH_271Master oItem = new ClsRxH_271Master();
        //    _innerlist.Add(oItem);
        //    oItem = null;
        //}

        //public bool Remove(ClsRxH_271Master item)
        //{
        //    //bool result = false;
        //    //Category obj;

        //    //for (int i = 0; i < _innerlist.Count; i++)
        //    //{
        //    //    //store current index being checked
        //    //    obj = new Category();
        //    //    obj = (Category)_innerlist[i];
        //    //    if (obj.CategoryID == item.CategoryID)
        //    //    {
        //    //        _innerlist.RemoveAt(i);
        //    //        result = true;
        //    //        break;
        //    //    }
        //    //    obj = null;
        //    //}

        //    //return result;
        //}

        //public bool RemoveAt(int index)
        //{
        //    //bool result = false;
        //    //_innerlist.RemoveAt(index);
        //    //result = true;
        //    //return result;
        //}

        public void Clear()
        {
            _innerlist.Clear();
        }

        public ClsRxH_271Master this[int index]
        {
            get
            {
                return (ClsRxH_271Master)_innerlist[index];
            }
        }

        //public bool Contains(Category item)
        //{
        //    return _innerlist.Contains(item);
        //}

        public int IndexOf(ClsRxH_271Master item)
        {
            return _innerlist.IndexOf(item);
        }

        //public void CopyTo(Category[] array, int index)
        //{
        //    _innerlist.CopyTo(array, index);
        //}
    }
}
