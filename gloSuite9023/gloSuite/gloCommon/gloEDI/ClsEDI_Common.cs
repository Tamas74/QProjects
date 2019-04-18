using System;
using System.Collections.Generic;
using System.Text;

namespace gloEDI
{
    namespace gloEDICommon
    {
        class gloEDICommon
        {
        }

        class EDI837P_Parameter
        {
            public Int64 BatchNumber = 0;
            //public Int64 
        }

        namespace EDIConstant
        {
            public static class EntityType
            {
                public static string Person = "1";
                public static string NonPerson = "2";
            }

            public static class InterchangeIdQualifier
            {
                public static string MutuallyDefined = "ZZ";//Mutually Defined
                public static string HCFACarrierIdNumber = "27";//Carrier Id. No. as assigned by HCFA
                public static string MedicareProviderID = "29";//Medicare Provider & Supplier Id. No. as assigned by HCFA

            }

            public static class FuntionalIdentifierCode
            {
                public static string HelthCareClaim = "HC";
            }

            public static class ProviderCode
            {
                public static string Billing = "BI";
                public static string PayTo = "PT";
                public static string ReferringProvider = "DN";
                public static string PrimaryCareProvider = "P3";
                public static string RenderingProvider = "82";
                public static string PurchaseServiceProvider = "QB";
                public static string SupervisingPhysician = "DQ";
            }

            public static class PayerResponsiblitySeqCode
            {
                public static string Primary = "P";
                public static string Secondary = "S";
                public static string Tertiary = "T";
            }

            public static class DateTimeQualifierCode
            {
                public static string CCYYMMDD = "D8";
            }

            public static class GenderCode
            {
                public static string Female = "F";
                public static string Male = "M";
                public static string Unknown = "U";
            }

            public static class YesNo
            {
                public static string No = "N";
                public static string Yes = "Y";
            }

            public static class ProviderAcceptAssignmentCode
            {
                public static string Assigned = "A";
                public static string AssignmentAcceptedonClinicalLabServicesOnly = "B";
                public static string NotAssigned = "C";
                public static string PatientRefusestoAssignBenefits = "P";
            }

            public static class DTQualifier
            {
                public static string InitialTreatment = "454";
                public static string OnsetofCurrentSymptomsorIllness = "431";
                public static string OnsetofSimilarSymptomsorIllness = "438";
                public static string Accident = "439";
                public static string PeriodLastDayWorked = "297";
                public static string ReturnToWork = "296";
                public static string Admission = "435";
                public static string Discharge = "096";
                public static string Service = "472";


            }

            public static class HCCodeList
            {
                public static string PrincipalDiagnosis = "BK";
                public static string Diagnosis = "BF";
            }

            public static class ServiceQualifier
            {
                public static string HCPCS = "HC"; //Health Care Financing Adminstration Common Procedural Coding System
                public static string HIEC = "IV"; //Home Infusion EDI Coalition
                public static string MutuallyDefined = "ZZ";
            }

            public static class Unit
            {
                public static string InternationalUnit = "F2";
                public static string Minutes = "MJ";
                public static string Units = "UN";
            }
        }
    }
}
