using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloBilling
{
    public enum EDIFileType
    {
        None = 0,
        InBox_271EligibilityResponse = 1,
        InBox_277ClaimStatusResponse = 2,
        InBox_835RemittanceAdvice = 3,
        InBox_997Acknowledgement = 4,
        OutBox_276EligibilityEnquiry = 5,
        OutBox_837PClaimSubmission = 6,
        OutBox_997Acknowledgement = 7,
        General_CSRReports = 8,
        General_Letters = 9,
        General_Reports = 10,
        General_Statements = 11,
        General_WorkedTransaction = 12,

        //MaheshB 20091213 For Statements
        OutBox_Statements = 13,
        //*************************************************
        //Added By Debasish Das on 12th Feb 2010
        //After Having discussion with Mahesh.
        //*************************************************
        InBox_277ClaimStatusResponse_Processed = 14,
        InBox_835RemittanceAdvice_Processed = 15,
        InBox_997Acknowledgement_Processed = 16,
        OutBox_837PClaim_Submitted = 17,
        OutBox_837P_PaperClaimSubmission = 18,
        OutBox_Statements_Submitted = 19,
        OutBox_837P_PaperClaimSubmitted = 20,
        //**************** Ends Here **********************
    }

    public static class ClsGeneralClaimManager
    {
        public static string DatabaseConnectionString = "";
        public static Int64 ClinicID = 1;
        public static string PM_ServerPath = "";
        public static string PM_ClaimManagement_Path = "";
        public static string PM_ClaimManagement_InBox = "";
        public static string PM_ClaimManagement_OutBox = "";
        public static string PM_ClaimManagement_General = "";

        public static string PM_ClaimManagement_InBox_271EligibilityResponse = "";
        public static string PM_ClaimManagement_InBox_277ClaimStatusResponse = "";
        public static string PM_ClaimManagement_InBox_277ClaimStatusResponse_Processed = "";
        public static string PM_ClaimManagement_InBox_835RemittanceAdvice = "";
        public static string PM_ClaimManagement_InBox_835RemittanceAdvice_Processed = "";
        public static string PM_ClaimManagement_InBox_997Acknowledgement = "";
        public static string PM_ClaimManagement_InBox_997Acknowledgement_Processed = "";


        public static string PM_ClaimManagement_OutBox_276EligibilityEnquiry = "";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmission_Electronic = "";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmission_Paper = "";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmission_Sent = "";
        public static string PM_ClaimManagement_OutBox_997Acknowledgement = "";
        //Added by MaheshB
        public static string PM_ClaimManagement_OutBox_276EligibilityEnquiry_Sent = "";
        public static string PM_ClaimManagement_OutBox_997Acknowledgement_Sent = "";

        //MaheshB 20091213
        public static string PM_ClaimManagement_OutBox_Statements = "";
        public static string PM_ClaimManagement_OutBox_Statements_Sent = "";

        public static string PM_ClaimManagement_General_CSRReports = "";
        public static string PM_ClaimManagement_General_Letters = "";
        public static string PM_ClaimManagement_General_Reports = "";
        public static string PM_ClaimManagement_General_Statements = "";
        public static string PM_ClaimManagement_General_WorkedTransaction = "";

        public static string PM_ClaimManagement_InBox_271EligibilityResponse_Label = "271 Eligibility Response";
        public static string PM_ClaimManagement_InBox_277ClaimStatusResponse_Label = "277 Claim Status Response";
        public static string PM_ClaimManagement_InBox_277ClaimStatusResponse_Processed_Label = "277 Claim Status Response Processed";
        public static string PM_ClaimManagement_InBox_835RemittanceAdvice_Label = "835 Remittance Advice";
        public static string PM_ClaimManagement_InBox_835RemittanceAdvice_Processed_Label = "835 Remittance Advice Processed";
        public static string PM_ClaimManagement_InBox_997Acknowledgement_Label = "997 Acknowledgement";
        public static string PM_ClaimManagement_InBox_997Acknowledgement_Processed_Label = "997 Acknowledgement Processed";


        public static string PM_ClaimManagement_OutBox_276EligibilityEnquiry_Label = "276 Eligibility Enquiry";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmissionElectronic_Label = "837 Electronic Claim submission ";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmissionPaper_Label = "837 Paper Claim submission ";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmittedPaper_Label = "837 Paper Claim Printed";
        public static string PM_ClaimManagement_OutBox_837PClaimSubmission_Sent_Label = "837 Claim Submitted";
        public static string PM_ClaimManagement_OutBox_997Acknowledgement_Label = "997 Acknowledgement";

        public static string PM_ClaimManagement_OutBox_Statements_Label = "Statements";
        public static string PM_ClaimManagement_OutBox_Statements_Sent_Label = "Statements submitted";

        public static string PM_ClaimManagement_General_CSRReports_Label = "CSR Reports";
        public static string PM_ClaimManagement_General_Letters_Label = "Letters";
        public static string PM_ClaimManagement_General_Reports_Label = "Reports";
        public static string PM_ClaimManagement_General_Statements_Label = "Statements";
        public static string PM_ClaimManagement_General_WorkedTransaction_Label = "Worked Transaction";

    }
}
