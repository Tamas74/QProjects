using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Edidev.FrameworkEDI;
using System.Data;
using gloBilling.Common;
using gloAppointmentBook.Books;
using System.Windows.Forms;
using System.IO;
using gloGlobal;

namespace gloBilling
{
    //public class gloEDI
    //{
    //    #region   Enumerated Data Types  

    //    //public enum EntityType
    //    //{
    //    //    Person = "1",
    //    //    NonPerson = "2"
    //    //}
    //    //public enum InterchangeIdQualifier
    //    //{
    //    //    MutuallyDefined = "ZZ",//Mutually Defined
    //    //    HCFACarrierIdNumber = "27",//Carrier Id. No. as assigned by HCFA
    //    //    MedicareProviderID = "29" //Medicare Provider & Supplier Id. No. as assigned by HCFA

    //    //}
    //    //public enum FuntionalIdentifierCode
    //    //{
    //    //    HelthCareClaim = "HC"
    //    //}
    //    //public enum ProviderCode
    //    //{
    //    //    Billing = "BI",
    //    //    PayTo = "PT",
    //    //    ReferringProvider = "DN",
    //    //    PrimaryCareProvider = "P3",
    //    //    RenderingProvider = "82",
    //    //    PurchaseServiceProvider = "QB",
    //    //    SupervisingPhysician = "DQ",
    //    //}
    //    //public enum PayerResponsiblitySeqCode
    //    //{
    //    //    Primary = "P",
    //    //    Secondary = "S",
    //    //    Tertiary = "T"
    //    //}
    //    //public enum DateTimeQualifierCode
    //    //{
    //    //    CCYYMMDD = "D8"
    //    //}
    //    //public enum GenderCode
    //    //{
    //    //    Female = "F",
    //    //    Male = "M",
    //    //    Unknown = "U"
    //    //}
    //    //public enum YesNo
    //    //{ 
    //    //    No = "N",
    //    //    Yes = "Y"
    //    //}
    //    //public enum ProviderAcceptAssignmentCode
    //    //{ 
    //    //    Assigned = "A",
    //    //    AssignmentAcceptedonClinicalLabServicesOnly = "B",
    //    //    NotAssigned = "C",
    //    //    PatientRefusestoAssignBenefits = "P"
    //    //}
    //    //public enum DTQualifier
    //    //{ 
    //    //    InitialTreatment = "454",
    //    //    OnsetofCurrentSymptomsorIllness = "431",
    //    //    OnsetofSimilarSymptomsorIllness = "438",
    //    //    Accident = "439",
    //    //    PeriodLastDayWorked = "297",
    //    //    ReturnToWork = "296",
    //    //    Admission = "435",
    //    //    Discharge = "096",
    //    //    Service = 472


    //    //}
    //    //public enum HCCodeList
    //    //{
    //    //    PrincipalDiagnosis = "BK",
    //    //    Diagnosis = "BF"
    //    //}
    //    //public enum ServiceQualifier
    //    //{ 
    //    //    HCPCS = "HC", //Health Care Financing Adminstration Common Procedural Coding System
    //    //    HIEC = "IV", //Home Infusion EDI Coalition
    //    //    MutuallyDefined = "ZZ"
    //    //}
    //    //public enum Unit
    //    //{
    //    //    InternationalUnit = "F2",
    //    //    Minutes = "MJ",
    //    //    Unit = "UN"
    //    //}

    //    #endregion

    //    #region   Segment Classes 

    //    public class ISA : IDisposable
    //    {

    //        #region "Constructor & Distructor"

    //        public ISA()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~ISA()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Interchange Segment Varibles

    //        //private InterchangeIdQualifier _SenderIdQualifier = InterchangeIdQualifier.MutuallyDefined;
    //        private EDIConstant.InterchangeIdQualifier _SenderIdQualifier;// = EDIConstant.InterchangeIdQualifier.MutuallyDefined;  
    //        private string _SenderId = "";
    //        //private InterchangeIdQualifier _RecieverIdQualifier = InterchangeIdQualifier.MutuallyDefined;
    //        private EDIConstant.InterchangeIdQualifier _RecieverIdQualifier;// = EDIConstant.InterchangeIdQualifier.MutuallyDefined;
    //        private string _RecieverId = "";
    //        private string _InterchangeDate = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim();
    //        private string _InterchageTime = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString()));
    //        private string _InterchangeControlNo = "";
    //        private string _AcknowledgeRequested = "0";
    //        private string _UsageIndicator = "T"; // T = Test Data


    //        #endregion Interchange Segment Varibles

    //        #region Property Procedures

    //        public EDIConstant.InterchangeIdQualifier SenderIdQualifier
    //        {
    //            get { return _SenderIdQualifier; }
    //            set { _SenderIdQualifier = value; }
    //        }
    //        public string SenderId
    //        {
    //            get { return _SenderId; }
    //            set { _SenderId = value; }
    //        }
    //        public EDIConstant.InterchangeIdQualifier RecieverIdQualifier
    //        {
    //            get { return _RecieverIdQualifier; }
    //            set { _RecieverIdQualifier = value; }
    //        }
    //        public string RecieverId
    //        {
    //            get { return _RecieverId; }
    //            set { _RecieverId = value; }
    //        }
    //        public string InterchangeDate
    //        {
    //            get { return _InterchangeDate; }
    //            set { _InterchangeDate = value; }
    //        }
    //        public string InterchageTime
    //        {
    //            get { return _InterchageTime; }
    //            set { _InterchageTime = value; }
    //        }
    //        public string InterchangeControlNo
    //        {
    //            get { return _InterchangeControlNo; }
    //            set { _InterchangeControlNo = value; }
    //        }
    //        public string AcknowledgeRequested
    //        {
    //            get { return _AcknowledgeRequested; }
    //            set { _AcknowledgeRequested = value; }
    //        }
    //        public string UsageIndicator
    //        {
    //            get { return _UsageIndicator; }
    //            set { _UsageIndicator = value; }
    //        }


    //        #endregion Property Procedures
    //    }

    //    public class GS : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public GS()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~GS()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region  Functional Group Segment Variables

    //        private EDIConstant.FuntionalIdentifierCode _GroupFunctionalIdCode ;//= EDIConstant.FuntionalIdentifierCode.HelthCareClaim;
    //        private string _GroupApplicationSenderCode = "";
    //        private string _GroupApplicationRecieverCode = "";
    //        private string _GroupDate = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim();
    //        private string _GroupTime = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString()));
    //        private string _GroupControlNumber = ""; //nine digit number field
    //        private string _ResponsibleAgencyCode = "X"; //X = Accredited Standards Comittee X12
    //        private string _Version = "004010X098A1";

    //        #endregion  Functional Group Segment Variables

    //        #region  Property Procedures

    //        public EDIConstant.FuntionalIdentifierCode GroupFunctionalIdCode
    //        {
    //            get { return _GroupFunctionalIdCode; }
    //            set { _GroupFunctionalIdCode = value; }
    //        }
    //        public string GroupApplicationSenderCode
    //        {
    //            get { return _GroupApplicationSenderCode; }
    //            set { _GroupApplicationSenderCode = value; }
    //        }
    //        public string GroupApplicationRecieverCode
    //        {
    //            get { return _GroupApplicationRecieverCode; }
    //            set { _GroupApplicationRecieverCode = value; }
    //        }
    //        public string GroupDate
    //        {
    //            get { return _GroupDate; }
    //            set { _GroupDate = value; }
    //        }
    //        public string GroupTime
    //        {
    //            get { return _GroupTime; }
    //            set { _GroupTime = value; }
    //        }
    //        public string GroupControlNumber//nine digit number field
    //        {
    //            get { return _GroupControlNumber; }
    //            set { _GroupControlNumber = value; }
    //        }
    //        public string ResponsibleAgencyCode
    //        {
    //            get { return _ResponsibleAgencyCode; }
    //            set { _ResponsibleAgencyCode = value; }
    //        }
    //        public string Version
    //        {
    //            get { return _Version; }
    //            set { _Version = value; }
    //        }

    //        #endregion
    //    }

    //    public class NM1 : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public NM1()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~NM1()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region " Variable Declarations "

    //        //NM1 - Individual or Organization Name
    //        private string _NM1HierarchyList = "";
    //        private string _EntityIdentifierCode = "";
    //        private string _EntityTypeQualifier = "";
    //        private string _LastNameOROrganization = "";
    //        private string _FirstName = "";
    //        private string _MiddleName = "";
    //        private string _IdentificationCodeQualifier = "";
    //        private string _IdentificationCode = "";

    //        //PER -Administrative Communication Contact
    //        private bool _PER = false;
    //        private string _PERHierachyList = "";
    //        private string _ContactFunctionCode = "";
    //        private string _Name = "";
    //        private string _CommunicationNumberQualifier_1 = "";
    //        private string _CommunicationNumber_1 = "";
    //        private string _CommunicationNumberQualifier_2 = "";
    //        private string _CommunicationNumber_2 = "";
    //        private string _CommunicationNumberQualifier_3 = "";
    //        private string _CommunicationNumber_3 = "";

    //        #endregion " Variable Declarations "

    //        #region " Propety Procedures "

    //        //NM1 - Individual or Organization Name
    //        public string NM1HierarchyList
    //        {
    //            get { return _NM1HierarchyList; }
    //            set { _NM1HierarchyList = value; }
    //        }
    //        public string EntityIdentifierCode
    //        {
    //            get { return _EntityIdentifierCode; }
    //            set { _EntityIdentifierCode = value; }
    //        }
    //        public string EntityTypeQualifier
    //        {
    //            get { return _EntityTypeQualifier; }
    //            set { _EntityTypeQualifier = value; }
    //        }
    //        public string LastNameOROrganization
    //        {
    //            get { return _LastNameOROrganization; }
    //            set { _LastNameOROrganization = value; }
    //        }
    //        public string FirstName
    //        {
    //            get { return _FirstName; }
    //            set { _FirstName = value; }
    //        }
    //        public string MiddleName
    //        {
    //            get { return _MiddleName; }
    //            set { _MiddleName = value; }
    //        }
    //        public string IdentificationCodeQualifier
    //        {
    //            get { return _IdentificationCodeQualifier; }
    //            set { _IdentificationCodeQualifier = value; }
    //        }
    //        public string IdentificationCode
    //        {
    //            get { return _IdentificationCode; }
    //            set { _IdentificationCode = value; }
    //        }

    //        ///PER -Administrative Communication Contact
    //        public bool PER
    //        {
    //            get { return _PER; }
    //            set { _PER = value; }
    //        }
    //        public string PERHierachyList
    //        {
    //            get { return _PERHierachyList; }
    //            set { _PERHierachyList = value; }
    //        }
    //        public string ContactFunctionCode
    //        {
    //            get { return _ContactFunctionCode; }
    //            set { _ContactFunctionCode = value; }
    //        }
    //        public string PERName
    //        {
    //            get { return _Name; }
    //            set { _Name = value; }
    //        }
    //        public string CommunicationNumberQualifier_1
    //        {
    //            get { return CommunicationNumberQualifier_1; }
    //            set { CommunicationNumberQualifier_1 = value; }
    //        }
    //        public string CommunicationNumber_1
    //        {
    //            get { return _CommunicationNumber_1; }
    //            set { _CommunicationNumber_1 = value; }
    //        }
    //        public string CommunicationNumberQualifier_2
    //        {
    //            get { return _CommunicationNumberQualifier_2; }
    //            set { _CommunicationNumberQualifier_2 = value; }
    //        }
    //        public string CommunicationNumber_2
    //        {
    //            get { return _CommunicationNumber_2; }
    //            set { _CommunicationNumber_2 = value; }
    //        }
    //        public string CommunicationNumberQualifier_3
    //        {
    //            get { return _CommunicationNumberQualifier_3; }
    //            set { _CommunicationNumberQualifier_3 = value; }
    //        }
    //        public string CommunicationNumber_3
    //        {
    //            get { return _CommunicationNumber_3; }
    //            set { _CommunicationNumber_3 = value; }
    //        }

    //        #endregion
    //    }

    //    public class N3 : IDisposable
    //    {
    //        //N3 - Adress Information

    //        #region "Constructor & Distructor"

    //        public N3()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~N3()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region " Variable Declarations "

    //        private string _AdressInformation = "";

    //        #endregion " Variable Declarations "

    //        #region " Property Procedures "

    //        public string AdressInformation
    //        {
    //            get { return _AdressInformation; }
    //            set { _AdressInformation = value; }
    //        }

    //        #endregion " Property Procedures "
    //    }

    //    public class N4 : IDisposable
    //    {
    //        //N4 - Geographic Location

    //        #region "Constructor & Distructor"

    //        public N4()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~N4()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region " Variable Declarations "

    //        private string _CityName = "";
    //        private string _StateProvinceCode = "";
    //        private string _PostalCode = "";
    //        private string _CountryCode = "";

    //        #endregion " Variable Declarations "

    //        #region " Property Procedures "

    //        public string CityName
    //        {
    //            get { return _CityName; }
    //            set { _CityName = value; }
    //        }
    //        public string StateProvinceCode
    //        {
    //            get { return _StateProvinceCode; }
    //            set { _StateProvinceCode = value; }
    //        }
    //        public string PostalCode
    //        {
    //            get { return _PostalCode; }
    //            set { _PostalCode = value; }
    //        }
    //        public string CountryCode
    //        {
    //            get { return _CountryCode; }
    //            set { _CountryCode = value; }
    //        }

    //        #endregion " Property Procedures "
    //    }

    //    public class BHT : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public BHT()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~BHT()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region BHT Segment Variables

    //        private string _HierarchicalStructureCode = "0019"; //0019 - Information,Source,Subscriber,Dependent
    //        private string _TransactionSetPurposeCode = "00"; // 00 - Original , 18 - Reissue
    //        private string _BHTRefrenceidentification = "";
    //        private string _BHTDate = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim();
    //        private string _BHTTime = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToShortTimeString()));
    //        private string _BHTTransactionTypeCode = "RP"; // RP - Reporting

    //        #endregion BHT Segment Variables

    //        #region Property Procedures

    //        public string HierarchicalStructureCode
    //        {
    //            get { return _HierarchicalStructureCode; }
    //            set { _HierarchicalStructureCode = value; }
    //        }
    //        public string TransactionSetPurposeCode
    //        {
    //            get { return _TransactionSetPurposeCode; }
    //            set { _TransactionSetPurposeCode = value; }
    //        }
    //        public string BHTRefrenceidentification
    //        {
    //            get { return _BHTRefrenceidentification; }
    //            set { _BHTRefrenceidentification = value; }
    //        }
    //        public string BHTDate
    //        {
    //            get { return _BHTDate; }
    //            set { _BHTDate = value; }
    //        }
    //        public string BHTTime
    //        {
    //            get { return _BHTTime; }
    //            set { _BHTTime = value; }
    //        }
    //        public string BHTTransactionTypeCode
    //        {
    //            get { return _BHTTransactionTypeCode; }
    //            set { _BHTTransactionTypeCode = value; }
    //        }


    //        #endregion
    //    }

    //    public class PRV : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public PRV()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~PRV()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private EDIConstant.ProviderCode _ProviderCode;// = "";
    //        private string _RefrenceIdQualifier = "ZZ"; //ZZ - Mutually  defined
    //        private string _RefrenceIdentification = "";

    //        #endregion

    //        #region Propety Procedure

    //        public EDIConstant.ProviderCode ProviderCode
    //        {
    //            get { return _ProviderCode; }
    //            set { _ProviderCode = value; }
    //        }
    //        public string RefrenceIdQualifier
    //        {
    //            get { return _RefrenceIdQualifier; }
    //            set { _RefrenceIdQualifier = value; }
    //        }
    //        public string RefrenceIdentification
    //        {
    //            get { return _RefrenceIdentification; }
    //            set { _RefrenceIdentification = value; }
    //        }

    //        #endregion
    //    }

    //    public class REF : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public REF()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~REF()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private string _RefIdQualifier = "ZZ";
    //        private string _RefIdentification = "";

    //        #endregion

    //        #region Propety Procedures

    //        public string RefIdQualifier
    //        {
    //            get { return _RefIdQualifier; }
    //            set { RefIdQualifier = value; }
    //        }
    //        public string RefIdentification
    //        {
    //            get { return _RefIdentification; }
    //            set { _RefIdentification = value; }
    //        }

    //        #endregion
    //    }

    //    public class SBR : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public SBR()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~SBR()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private EDIConstant.PayerResponsiblitySeqCode _PayerResponsiblityCode;// = "";
    //        private string _IndividualRelationCode = "18"; //18 - Self
    //        private string _RefrenceIdentification = ""; //Usually we give the Policy number
    //        private string _Name = "";
    //        private string _InsuranceTypeCode = "";
    //        private string _ClaimFilingIndicatorCode = "HM"; //HM - Health Maintenance Organization

    //        #endregion

    //        #region Propety Procedures

    //        public EDIConstant.PayerResponsiblitySeqCode PayerResponsiblityCode
    //        {
    //            get { return _PayerResponsiblityCode; }
    //            set { _PayerResponsiblityCode = value; }
    //        }
    //        public string IndividualRelationCode
    //        {
    //            get { return _IndividualRelationCode; }
    //            set { _IndividualRelationCode = value; }
    //        }
    //        public string RefrenceIdentification
    //        {
    //            get { return _RefrenceIdentification; }
    //            set { _RefrenceIdentification = value; }
    //        }
    //        public string Name
    //        {
    //            get { return _Name; }
    //            set { _Name = value; }
    //        }
    //        public string InsuranceTypeCode
    //        {
    //            get { return _InsuranceTypeCode; }
    //            set { _InsuranceTypeCode = value; }
    //        }
    //        public string ClaimFilingIndicatorCode
    //        {
    //            get { return _ClaimFilingIndicatorCode; }
    //            set { _ClaimFilingIndicatorCode = value; }
    //        }

    //        #endregion

    //    }

    //    public class PAT : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public PAT()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~PAT()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        private string _IndividualRelationCode = "";

    //        public string IndividualRelationCode
    //        {
    //            get { return _IndividualRelationCode; }
    //            set { _IndividualRelationCode = value; }
    //        }

    //    }

    //    public class DMG : IDisposable
    //    {
    //        #region "Constructor & Distructor"


    //        public DMG()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~DMG()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private EDIConstant.DateTimeQualifierCode _DateTimeCode;// = DateTimeQualifierCode.CCYYMMDD;
    //        private string _DateTime = "";
    //        private EDIConstant.GenderCode  _Gender;// = "";

    //        #endregion

    //        #region Property Procedures

    //        public EDIConstant.DateTimeQualifierCode DateTimeCode
    //        {
    //            get { return _DateTimeCode; }
    //            set { _DateTimeCode = value; }
    //        }
    //        public string DateTime
    //        {
    //            get { return _DateTime; }
    //            set { _DateTime = value; }
    //        }
    //        public EDIConstant.GenderCode Gender
    //        {
    //            get { return _Gender; }
    //            set { _Gender = value; }
    //        }

    //        #endregion

    //    }

    //    public class CLM : IDisposable
    //    { 
    //        #region "Constructor & Distructor"

    //        public CLM()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~CLM()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private string _ClaimSubmitterId = ""; //Patient Account Number
    //        private string _MonetaryAmount = "";
    //        private string _FacilityCode = "";
    //        private string _ClaimFrequencyTypeCode = "1";
    //        private EDIConstant.YesNo _ResponseCode_6;// = YesNo.Yes;
    //        private EDIConstant.ProviderAcceptAssignmentCode _ProviderAssignCode;// = ProviderAcceptAssignmentCode.Assigned;
    //        private EDIConstant.YesNo _ResponseCode_8;// = YesNo.Yes;
    //        private string _ReleaseInfoCode = "Y"; // Y - Yes,Provider has signed Statement Permitting Release of Medical Data Related to claim
    //        private string _PatientSignCode = "C"; //C - Signed HCFA 1500 Claim Form on file



    //        #endregion

    //        #region Property Procedures

    //        public string ClaimSubmitterId
    //        {
    //            get { return _ClaimSubmitterId; }
    //            set { _ClaimSubmitterId = value; }
    //        }
    //        public string MonetaryAmount
    //        {
    //            get { return _MonetaryAmount; }
    //            set { _MonetaryAmount = value; }
    //        }
    //        public string FacilityCode
    //        {
    //            get { return _FacilityCode; }
    //            set { _FacilityCode = value; }
    //        }
    //        public string ClaimFrequencyTypeCode
    //        {
    //            get { return _ClaimFrequencyTypeCode; }
    //            set { _ClaimFrequencyTypeCode = value; }
    //        }
    //        public EDIConstant.YesNo ResponseCode_6
    //        {
    //            get {return _ResponseCode_6;}
    //            set {_ResponseCode_6 = value;}
    //        }
    //        public EDIConstant.ProviderAcceptAssignmentCode ProviderAssignCode
    //        {
    //            get { return _ProviderAssignCode; }
    //            set { _ProviderAssignCode = value; }
    //        }
    //        public EDIConstant.YesNo ResponseCode_8
    //        {
    //            get { return _ResponseCode_8; }
    //            set { _ResponseCode_8 = value; }
    //        }
    //        public string ReleaseInfoCode
    //        {
    //            get { return _ReleaseInfoCode; }
    //            set { _ReleaseInfoCode = value; }
    //        }
    //        public string PatientSignCode
    //        {
    //            get { return _PatientSignCode; }
    //            set { _PatientSignCode = value; }
    //        }

    //        #endregion

    //    }

    //    public class DTP : IDisposable
    //    { 
    //        #region "Constructor & Distructor"

    //        public DTP()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~DTP()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private EDIConstant.DTQualifier _DTQualifier;// = "";
    //        private EDIConstant.DateTimeQualifierCode _DTFormatQalifier;// = DateTimeQualifierCode.CCYYMMDD;
    //        private string _DateTime = ""; 

    //        #endregion

    //        #region Property Procedures

    //        public EDIConstant.DTQualifier DTQualifier
    //        {
    //            get { return _DTQualifier; }
    //            set { _DTQualifier = value; }
    //        }
    //        public EDIConstant.DateTimeQualifierCode DTFormatQalifier
    //        {
    //            set { _DTFormatQalifier = value; }
    //        }
    //        public string DateTime
    //        {
    //            get { return _DateTime; }
    //            set { _DateTime = value; }
    //        }

    //        #endregion


    //    }

    //    public class HICodeInfo : IDisposable 
    //    {
    //        #region "Constructor & Distructor"

    //        public HICodeInfo()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~HICodeInfo()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declarations

    //        private EDIConstant.HCCodeList _CodeQualifier;//="";
    //        private string _IndustryCode = ""; 

    //        #endregion

    //        #region Property Procedures

    //        public EDIConstant.HCCodeList CodeQualifier
    //        {
    //            get {return _CodeQualifier;}
    //            set {_CodeQualifier = value;}
    //        }
    //        public string IndustryCode
    //        {
    //            get{return _IndustryCode;}
    //            set {_IndustryCode =value ;}
    //        }

    //        #endregion


    //    }

    //    public class HI : IDisposable
    //    { 
    //        #region "Constructor & Distructor"

    //        public HI()
    //        {
    //            _HCCodeInfo_1 = new HICodeInfo();
    //            _HCCodeInfo_2 = new HICodeInfo();
    //            _HCCodeInfo_3 = new HICodeInfo();
    //            _HCCodeInfo_4 = new HICodeInfo();
    //            _HCCodeInfo_5 = new HICodeInfo();
    //            _HCCodeInfo_6 = new HICodeInfo();
    //            _HCCodeInfo_7 = new HICodeInfo();
    //            _HCCodeInfo_8 = new HICodeInfo();

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~HI()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private int _itemCount = 0;
    //        private HICodeInfo _HCCodeInfo_1;
    //        private HICodeInfo _HCCodeInfo_2;
    //        private HICodeInfo _HCCodeInfo_3;
    //        private HICodeInfo _HCCodeInfo_4;
    //        private HICodeInfo _HCCodeInfo_5;
    //        private HICodeInfo _HCCodeInfo_6;
    //        private HICodeInfo _HCCodeInfo_7;
    //        private HICodeInfo _HCCodeInfo_8; 

    //        #endregion

    //        #region Property Procedures

    //        public int Count
    //        {
    //            get { return _itemCount; }
    //            set { _itemCount = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_1
    //        {
    //            get { return _HCCodeInfo_1; }
    //            set { _HCCodeInfo_1 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_2
    //        {
    //            get { return _HCCodeInfo_2; }
    //            set { _HCCodeInfo_2 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_3
    //        {
    //            get { return _HCCodeInfo_3; }
    //            set { _HCCodeInfo_3 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_4
    //        {
    //            get { return _HCCodeInfo_4; }
    //            set { _HCCodeInfo_4 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_5
    //        {
    //            get { return _HCCodeInfo_5; }
    //            set { _HCCodeInfo_5 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_6
    //        {
    //            get { return _HCCodeInfo_6; }
    //            set { _HCCodeInfo_6 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_7
    //        {
    //            get { return _HCCodeInfo_7; }
    //            set { _HCCodeInfo_7 = value; }
    //        }
    //        public HICodeInfo HCCodeInfo_8
    //        {
    //            get { return _HCCodeInfo_8; }
    //            set { _HCCodeInfo_8 = value; }
    //        } 
    //        #endregion


    //    }

    //    public class CompositeProcedure : IDisposable
    //    { 
    //        #region "Constructor & Distructor"

    //        public CompositeProcedure()
    //        {

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~CompositeProcedure()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        private EDIConstant.ServiceQualifier _ServiceQualifier;// = ServiceQualifier.HCPCS;
    //        private string _SeriviceId = "";
    //        private string _Modifier1 = "";
    //        private string _Modifier2 = "";
    //        private string _Modifier3 = "";
    //        private string _Modifier4 = "";

    //        public EDIConstant.ServiceQualifier ServiceQualifier
    //        {
    //            get { return _ServiceQualifier; }
    //            set { _ServiceQualifier = value; }
    //        }
    //        public string SeriviceId
    //        {
    //            get { return _SeriviceId; }
    //            set { _SeriviceId = value; }
    //        }
    //        public string Modifier1
    //        {
    //            get { return _Modifier1; }
    //            set { _Modifier1 = value; }
    //        }
    //        public string Modifier2
    //        {
    //            get { return _Modifier2; }
    //            set { _Modifier2 = value; }
    //        }
    //        public string Modifier3
    //        {
    //            get { return _Modifier3; }
    //            set { _Modifier3 = value; }
    //        }
    //        public string Modifier4
    //        {
    //            get { return _Modifier4; }
    //            set { _Modifier4 = value; }
    //        }

    //    }

    //    public class DiagnosisPointer : IDisposable
    //    { 
    //        #region "Constructor & Distructor"

    //        public DiagnosisPointer()
    //        {
    //            //_CompositeProcedure = new CompositeProcedure();
    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~DiagnosisPointer()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        private string _CodePointer1 = "";
    //        private string _CodePointer2 = "";
    //        private string _CodePointer3 = "";
    //        private string _CodePointer4 = "";

    //        public string CodePointer1
    //        {
    //            get { return _CodePointer1; }
    //            set { _CodePointer1 = value; }
    //        }
    //        public string CodePointer2
    //        {
    //            get { return _CodePointer2; }
    //            set { _CodePointer2 = value; }
    //        }
    //        public string CodePointer3
    //        {
    //            get { return _CodePointer3; }
    //            set { _CodePointer3 = value; }
    //        }
    //        public string CodePointer4
    //        {
    //            get { return _CodePointer4; }
    //            set { _CodePointer4 = value; }
    //        }
    //    }

    //    public class SV1 : IDisposable
    //    { 
    //        //Professional Service

    //        #region "Constructor & Distructor"

    //        public SV1()
    //        {
    //            _CompositeProcedure = new CompositeProcedure();
    //            _DiagnosisPointer = new DiagnosisPointer();
    //            _ServiceDate = new DTP();
    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~SV1()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region Variable Declaration

    //        private CompositeProcedure _CompositeProcedure;
    //        private string _MonetaryAmount = "0";
    //        private EDIConstant.Unit _UnitCode;
    //        private string _Quantity = "0";
    //        private string _FacilityCodeValue = "";
    //        private DiagnosisPointer _DiagnosisPointer;
    //        private DTP _ServiceDate; 

    //        #endregion

    //        #region Property Procedures

    //        public CompositeProcedure CompositeProcedure
    //        {
    //            get { return _CompositeProcedure; }
    //            set { _CompositeProcedure = value; }
    //        }
    //        public string MonetaryAmount
    //        {
    //            get { return _MonetaryAmount; }
    //            set { _MonetaryAmount = value; }
    //        }
    //        public EDIConstant.Unit UnitCode
    //        {
    //            get { return _UnitCode; }
    //            set { _UnitCode = value; }
    //        }
    //        public string Quantity
    //        {
    //            get { return _Quantity; }
    //            set { _Quantity = value; }
    //        }
    //        public string FacilityCodeValue
    //        {
    //            get { return _FacilityCodeValue; }
    //            set { _FacilityCodeValue = value; }

    //        }
    //        public DiagnosisPointer DiagnosisPointer
    //        {
    //            get { return _DiagnosisPointer; }
    //            set { _DiagnosisPointer = value; }
    //        }
    //        public DTP ServiceDate
    //        {
    //            get { return _ServiceDate; }
    //            set { _ServiceDate = value; }
    //        } 

    //        #endregion

    //    }

    //    public class ServiceLines : IDisposable
    //    {

    //        protected ArrayList _innerlist;

    //        #region "Constructor & Distructor"



    //        public ServiceLines()
    //        {
    //            _innerlist = new ArrayList();

    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {

    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }


    //        ~ServiceLines()
    //        {
    //            Dispose(false);
    //        }
    //        #endregion


    //        // Methods Add, Remove, Count , Item of TransactionLine
    //        public int Count
    //        {
    //            get { return _innerlist.Count; }
    //        }

    //        public void Add(SV1 item)
    //        {
    //            _innerlist.Add(item);
    //        }


    //        public bool Remove(SV1 item)
    //        //Remark - Work Remining for comparision
    //        {
    //            bool result = false;
    //            return result;
    //        }

    //        public bool RemoveAt(int index)
    //        {
    //            bool result = false;
    //            _innerlist.RemoveAt(index);
    //            result = true;
    //            return result;
    //        }

    //        public void Clear()
    //        {
    //            _innerlist.Clear();
    //        }

    //        public SV1 this[int index]
    //        {
    //            get
    //            { return (SV1)_innerlist[index]; }
    //        }

    //        public bool Contains(SV1 item)
    //        {
    //            return _innerlist.Contains(item);
    //        }

    //        public int IndexOf(SV1 item)
    //        {
    //            return _innerlist.IndexOf(item);
    //        }

    //        public void CopyTo(SV1[] array, int index)
    //        {
    //            _innerlist.CopyTo(array, index);
    //        }

    //    } // Collection class for Service Lines(SV1)

    //    #endregion

    //    public class Claim : IDisposable
    //    {
    //        #region "Constructor & Distructor"

    //        public Claim()
    //        {
    //            ISA = new ISA();
    //            GS = new GS();
    //            _SubmitterNM1 = new NM1();
    //            _RecieverNM1 = new NM1();
    //            _BillingProviderNM1 = new NM1();
    //        }

    //        private bool disposed = false;

    //        public void Dispose()
    //        {
    //            Dispose(true);
    //            GC.SuppressFinalize(this);
    //        }
    //        protected virtual void Dispose(bool disposing)
    //        {
    //            if (!this.disposed)
    //            {
    //                if (disposing)
    //                {

    //                }
    //            }
    //            disposed = true;
    //        }

    //        ~Claim()
    //        {
    //            Dispose(false);
    //        }

    //        #endregion

    //        #region " Variable Declarations "

    //        private ISA _ISA;
    //        private GS _GS;
    //        private NM1 _SubmitterNM1;
    //        private NM1 _RecieverNM1;
    //        private NM1 _BillingProviderNM1;

    //        #endregion " Variable Declarations "

    //        #region " Propety Procedures "

    //        public ISA ISA
    //        {
    //            get { return _ISA; }
    //            set { _ISA = value; }
    //        }
    //        public GS GS
    //        {
    //            get { return _GS; }
    //            set { _GS = value; }
    //        }
    //        public NM1 SubmitterNM1
    //        {
    //            get { return _SubmitterNM1; }
    //            set { _SubmitterNM1 = value; }
    //        }
    //        public NM1 RecieverNM1
    //        {
    //            get { return _RecieverNM1; }
    //            set { _RecieverNM1 = value; }
    //        }
    //        public NM1 BillingProviderNM1
    //        {
    //            get { return _BillingProviderNM1; }
    //            set { _BillingProviderNM1 = value; }
    //        }

    //        #endregion " Propety Procedures "


    //    }

    //}


    public class gloEDIGeneration
    {
        #region "Constructor & Destructor"

        private string _databaseconnectionstring = "";
        private string _messageBoxCaption = "";


        public gloEDIGeneration(string DatabaseConnectionString, Int64 UserID, Int64 BatchID)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            _UserID = UserID;
            _BatchID = BatchID;

            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }

            #endregion
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

        ~gloEDIGeneration()
        {
            Dispose(false);
        }

        #endregion

        #region " Private and Public Variables for EDI"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        private Int64 _UserID = 0;
        private Int64 _BatchID = 0;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        private Int32 nHlCount = 0;
        private Int32 nHlProvParent = 0;
        private Int32 nHlSubscriberParent = 0;
        private Int32 iItemCount = 0;
     //   gloPatient.Referrals oReferral = new gloPatient.Referrals();
        ediDocument oEdiDoc = null;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionset = null;
        ediDataSegment oSegment = null;
        ediAcknowledgment oAck = null;
        ediSchema oSchema = null;
        ediSchemas oSchemas = null;
        //ediWarnings oWarnings = null;
        //ediWarning oWarning = null;

        string sSEFFile = "";
        string sEdiFile = "";
        string sPath = "";

        string sSEFFile1 = "";
        string sEdiFile1 = "";
        //private bool bSecondaryInsurance = false;

        #endregion " Private and Public Variables for EDI"

        #region " EDI Generation Private Methods "

        public void LoadEDIObject()
        {
            try
            {
                //Here Interchange Loop should come
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                //sPath = appSettings["StartupPath"].ToString();
                sSEFFile = "837_X098A1.SEF";     //ToDO :Give the file name at runtime, since it can change
                sEdiFile = "837A1.x12";

                sSEFFile1 = "276_X093A1.SEF";
                sEdiFile1 = "276OUTPUT.x12";
                if (oEdiDoc != null)
                {
                    oEdiDoc.Dispose();
                    oEdiDoc = null;
                }
                oEdiDoc = new ediDocument();
                ediDocument.Set(ref oEdiDoc, new ediDocument());
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                oSchemas.EnableStandardReference = false;

                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sSEFFile, 0));
                ediSchema.Set(ref oSchema, (ediSchema)oEdiDoc.LoadSchema(sSEFFile1, 0));

                System.IO.FileInfo ofile = new System.IO.FileInfo(sPath + sSEFFile);
                System.IO.FileInfo ofile1 = new System.IO.FileInfo(sPath + sSEFFile1);

                if (ofile.Exists == false)
                {
                    MessageBox.Show("837 SEF file is not present in the base directory.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                //if (ofile1.Exists == false)
                //{
                //    MessageBox.Show("276 SEF file is not present in the base directory.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    return;
                //}
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        public bool IsValidICD9(string ICD9Code)//Used for generation of EDI
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object ReturnValue = new object();
            string _sqlQuery = "";
            bool _retVal = true;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sICD9Code,'') AS sICD9Code from ICD9_InvalidEDI WITH (NOLOCK) where UPPER(sICD9Code) = '" + ICD9Code.ToUpper() + "'";
                ReturnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (ReturnValue != null && ReturnValue != DBNull.Value && Convert.ToString(ReturnValue) != "")
                {
                    string _message = "ICD9 is Invalid." + Environment.NewLine + "Code : " + ICD9Code + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "
                    if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    {
                        _retVal = false;
                    }
                }
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (ReturnValue != null) { ReturnValue = null; }
            }
            return _retVal;
        }

        public bool IsValidICD9Code(string ICD9Code)//Used For Validation
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            object ReturnValue = new object();
            string _sqlQuery = "";
            bool _retVal = true;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sICD9Code,'') AS sICD9Code from ICD9_InvalidEDI WITH (NOLOCK) where UPPER(sICD9Code) = '" + ICD9Code.ToUpper() + "'";
                ReturnValue = oDB.ExecuteScalar_Query(_sqlQuery);
                if (ReturnValue != null && ReturnValue != DBNull.Value && Convert.ToString(ReturnValue) != "")
                {
                    //string _message = "ICD9 is Invalid." + Environment.NewLine + "Code : " + ICD9Code + "  " + Environment.NewLine + "Do you want to Continue? ";//" + Environment.NewLine + ""Description : " + Convert.ToString(ReturnValue) + "
                    //if (MessageBox.Show(_message, _messageBoxCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                    //{
                    _retVal = false;
                    //}
                }
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (ReturnValue != null) { ReturnValue = null; }
            }
            return _retVal;
        }

        //Mahesh Nawal 20100601 Code Optimization

        public string IsValidICD9Code_New(string ICD9Code)//Used For Validation
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable _DtIcd9 = new DataTable();
            string _sqlQuery = "";
            string _retVal = "";

            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(sICD9Code,'') AS sICD9Code from ICD9_InvalidEDI WITH (NOLOCK) where UPPER(sICD9Code) in(" + ICD9Code.ToUpper() + ")";

                oDB.Retrive_Query(_sqlQuery, out  _DtIcd9);

                for (int i = 0; i < _DtIcd9.Rows.Count; i++)
                {
                    _retVal += " " + Convert.ToString(_DtIcd9.Rows[i][0].ToString().Trim()) + ", ";
                }
            
                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
            }
            return _retVal.Trim();
        }

        private string FormattedTime(string TimeFormat)
        {
            int _length = 0;
            _length = TimeFormat.Length;
            if (_length == 0)
            {
                TimeFormat = "0000";
            }
            if (_length == 1)
            {
                TimeFormat = "000" + TimeFormat;
            }
            else if (_length == 2)
            {
                TimeFormat = "00" + TimeFormat;
            }
            else if (_length == 3)
            {
                TimeFormat = "0" + TimeFormat;
            }
            else if (_length == 4)
            {
                //TimeFormat = TimeFormat;
            }
            return TimeFormat;
        }

        private string GetPriorAuthorizationNumber(Int64 PatientID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            Object _result = null;
            string _PriorAuthorizationNo = "";
            try
            {
                _strSQL = "SELECT sAuthorizationNumber FROM PatientPriorAuthorization WITH (NOLOCK) WHERE nPatientID=" + PatientID + "  AND nInsuranceID=" + InsuranceID + " ";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    _PriorAuthorizationNo = Convert.ToString(_result);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
            return _PriorAuthorizationNo;
        }

        private bool ValidateEDIData(ArrayList SelectedTrans)
        {
            DataTable dtClearingHouse = null;
            DataTable dtSubmitter = null;
          //  DataTable dtReceiver = new DataTable();
          //  DataTable dtBillingProvider = new DataTable();
         //   DataTable dtRenderingProvider = new DataTable();
            DataTable dtFacility = null;
            DataTable dtPatientInsurances = null;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
            string _Message = "";
            Transaction oTransaction = null;
            string strMissingText = "";
            string _MessageHeader = "";
            //string _FilePath = AppDomain.CurrentDomain.BaseDirectory;
            string _FilePath = gloSettings.FolderSettings.AppTempFolderPath;
            try
            {
                _MessageHeader += "";

                //Get Clearing House Information in Datatable
           //     dtClearingHouse = new DataTable();
               


                #region " Clearing House "
                //ISA and GS Settings
                dtClearingHouse = ogloBilling.GetClearingHouseSettings();
                if (Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim() == "")
                {
                    strMissingText += "Sender ID" + Environment.NewLine + "" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim() == "")
                {
                    strMissingText += "Receiver ID" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim() == "")
                {
                    strMissingText += "Sender Code" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim() == "")
                {
                    strMissingText += "Receiver Code" + Environment.NewLine + "";
                }
                dtClearingHouse.Dispose();
                dtClearingHouse = null;

                #endregion " Clearing House "

                #region " Submitter "
                //Submitter
                if (SelectedTrans != null)
                {
                    if (SelectedTrans.Count > 0)
                    {
                        for (int i = 0; i < SelectedTrans.Count; i++)
                        {
                            //oTransaction = new Transaction();
                            //TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTrans[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    //Get Submitter Information in Datatable
                                    //  dtSubmitter = new DataTable();
                                    if (dtSubmitter != null)
                                    {
                                        dtSubmitter.Dispose();
                                        dtSubmitter = null;
                                    }
                                    dtSubmitter = ogloBilling.GetSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                }

                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim() == "")
                {
                    strMissingText += "Submitter Name" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim() == "")
                {
                    //strMissingText += "Submitter Contact Person Name" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim() == "")
                {
                    strMissingText += "Submitter Contact Person Number" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterCity"]).Trim() == "")
                {
                    strMissingText += "Submitter City" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterState"]).Trim() == "")
                {
                    strMissingText += "Submitter State" + Environment.NewLine + "";
                }
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterZIP"]).Trim() == "")
                {
                    strMissingText += "Submitter Zip" + Environment.NewLine + "";
                }
                //if (_SubmitterETIN == "")
                //{
                //    strMissingText += "Submitter ETIN" + Environment.NewLine + "";
                //}
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress1"]).Trim() + " " + Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress2"]).Trim() == "")
                {
                    strMissingText += "Submitter Address" + Environment.NewLine + "";
                }
                if (dtSubmitter != null)
                {
                    dtSubmitter.Dispose();
                    dtSubmitter = null;
                }
                #endregion " Submitter "

                if (strMissingText.Trim() != "")
                {
                    _MessageHeader = _MessageHeader + strMissingText;
                }
                else
                {
                    _MessageHeader = "";
                }


                if (SelectedTrans != null)
                {
                    if (SelectedTrans.Count > 0)
                    {
                        for (int i = 0; i < SelectedTrans.Count; i++)
                        {
                            string strMessage = "";
                            //oTransaction = new Transaction();
                            //TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTrans[i]), _ClinicID);
                            string _ClaimMessageHeader = "";
                           
                            Provider _Provider = null;
                            gloPatient.Patient oPatient = null;
                           // gloPatient.Referrals oReferral = new gloPatient.Referrals();
                            //gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                        
                            Object _objResult = null;
                            string strBillingSetting = "";

                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    if (Convert.ToInt64(oTransaction.ProviderID) != 0 && oTransaction.ProviderID.ToString() != "")
                                    {
                                        Resource oResource = new Resource(_databaseconnectionstring);
                                        _Provider = oResource.GetProviderDetail(Convert.ToInt64(oTransaction.ProviderID));
                                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                                        dtPatientInsurances = ogloPatient.getPatientInsurances(oTransaction.PatientID);

                                        dtFacility = ogloBilling.GetFacilityInfo(oTransaction.FacilityCode, oTransaction.ProviderID);
                                        oPatient = ogloPatient.GetPatient(oTransaction.PatientID);
                                        ogloPatient.Dispose();
                                        ogloPatient = null;
                                        oResource.Dispose();
                                        oResource = null;
                                    }

                                    _ClaimMessageHeader = " " + Environment.NewLine + "For Patient: " + oPatient.DemographicsDetail.PatientFirstName.Trim() + " " + oPatient.DemographicsDetail.PatientLastName.Trim() + "  and Claim Number: " + oTransaction.ClaimNo.ToString() + " " + Environment.NewLine + "" + Environment.NewLine + "";
                                    for (int j = 0; j < oTransaction.Lines.Count; j++)
                                    {
                                        #region " ICD9 Validation "
                                        if (Convert.ToString(oTransaction.Lines[j].Dx1Code).Trim() != "")
                                        {
                                            if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx1Code.Trim())) == false)
                                            {
                                                strMessage += "Invalid ICD9 Code1: " + Convert.ToString(oTransaction.Lines[j].Dx1Code.Trim()) + "" + Environment.NewLine + "";
                                            }
                                        }
                                        if (Convert.ToString(oTransaction.Lines[j].Dx2Code).Trim() != "")
                                        {
                                            if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx2Code.Trim())) == false)
                                            {
                                                strMessage += "Invalid ICD9 Code2: " + Convert.ToString(oTransaction.Lines[j].Dx2Code.Trim()) + "" + Environment.NewLine + "";
                                            }
                                        }
                                        if (Convert.ToString(oTransaction.Lines[j].Dx3Code).Trim() != "")
                                        {
                                            if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx3Code.Trim())) == false)
                                            {
                                                strMessage += "Invalid ICD9 Code3: " + Convert.ToString(oTransaction.Lines[j].Dx3Code.Trim()) + "" + Environment.NewLine + "";
                                            }
                                        }
                                        if (Convert.ToString(oTransaction.Lines[j].Dx4Code).Trim() != "")
                                        {
                                            if (IsValidICD9Code(Convert.ToString(oTransaction.Lines[j].Dx4Code.Trim())) == false)
                                            {
                                                strMessage += "Invalid ICD9 Code4: " + Convert.ToString(oTransaction.Lines[j].Dx4Code.Trim()) + "" + Environment.NewLine + "";
                                            }
                                        }
                                        #endregion " ICD9 Validation "
                                    }
                                }
                            //    oTransaction.Dispose();
                            //    oTransaction = null;
                                
                            }

                            #region " Billing Provider "
                            //Billing Provider
                            if (_Provider != null)
                            {
                                gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                                oSettings.GetSetting("BillingSetting", oTransaction.ProviderID, _ClinicID, out _objResult);
                                oSettings.Dispose();
                                oSettings = null;
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strBillingSetting = Convert.ToString(_objResult);
                                }
                                string _BillingAddress = "";
                                string _BillingCity = "";
                                string _BillingState = "";
                                string _BillingZIP = "";
                                switch (strBillingSetting)
                                {
                                    case "Business":
                                        {
                                            _BillingAddress = _Provider.BMAddress1;
                                            _BillingCity = _Provider.BMCity;
                                            _BillingState = _Provider.BMState;
                                            _BillingZIP = _Provider.BMZIP;
                                        } break;
                                    case "Practice":
                                        {
                                            _BillingAddress = _Provider.BPracAddress1;
                                            _BillingCity = _Provider.BPracCity;
                                            _BillingState = _Provider.BPracState;
                                            _BillingZIP = _Provider.BPracZIP;
                                        } break;
                                    case "Company":
                                        {
                                            _BillingAddress = _Provider.CompanyAddress1;
                                            _BillingCity = _Provider.CompanyCity;
                                            _BillingState = _Provider.CompanyState;
                                            _BillingZIP = _Provider.CompanyZip;
                                        } break;
                                    default:
                                        _BillingAddress = _Provider.BMAddress1;
                                        _BillingCity = _Provider.BMCity;
                                        _BillingState = _Provider.BMState;
                                        _BillingZIP = _Provider.BMZIP;
                                        break;
                                }

                                if (_Provider.FirstName.Trim() == "")
                                {
                                    strMessage += "Billing Provider First Name" + Environment.NewLine + "";
                                }
                                if (_Provider.LastName.Trim() == "")
                                {
                                    strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                                }
                                if (_Provider.MiddleName.Trim() == "")
                                {
                                    //strMessage += "Billing Provider Middle Name"+Environment.NewLine+"";
                                }
                                if (_BillingCity.Trim() == "")
                                {
                                    strMessage += "Billing Provider City" + Environment.NewLine + "";
                                }
                                if (_BillingState.Trim() == "")
                                {
                                    strMessage += "Billing Provider State" + Environment.NewLine + "";
                                }
                                if (_BillingAddress.Trim() == "")
                                {
                                    strMessage += "Billing Provider Address" + Environment.NewLine + "";
                                }
                                if (_BillingZIP.Trim() == "")
                                {
                                    strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                                }
                                if (_Provider.NPI.Trim() == "")
                                {
                                    strMessage += "Billing Provider NPI" + Environment.NewLine + "";
                                }
                                if (_Provider.SSN.Trim() == "")
                                {
                                    //strMessage += "Billing Provider SSN" + Environment.NewLine + "";
                                }
                                if (_Provider.EmployerID.Trim() == "")
                                {
                                    strMessage += "Billing Provider Employer ID" + Environment.NewLine + "";
                                }
                                if (_Provider.StateMedicalNo.Trim() == "")
                                {
                                    //strMessage += "Billing Provider State Medical No" + Environment.NewLine + "";
                                }
                                if (_Provider.Taxonomy.Trim() == "")
                                {
                                    strMessage += "Billing Provider Taxonomy" + Environment.NewLine + "";
                                }
                            }

                            #endregion " Billing Provider "

                            #region " Facility "
                            //Facility Information
                            if (oTransaction.FacilityCode.Trim() != "")
                            {
                                if (dtFacility != null && dtFacility.Rows.Count > 0)
                                {
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityName"]).Trim() == "")
                                    {
                                        strMessage += "Facility Name" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityAddress1"]).Trim() == "")
                                    {
                                        strMessage += "Facility Address" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityCity"]).Trim() == "")
                                    {
                                        strMessage += "Facility City" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityState"]).Trim() == "")
                                    {
                                        strMessage += "Facility State" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityZip"]).Trim() == "")
                                    {
                                        strMessage += "Facility Zip" + Environment.NewLine + "";
                                    }
                                    if (Convert.ToString(dtFacility.Rows[0]["FacilityNPI"]).Trim() == "")
                                    {
                                        strMessage += "Facility NPI" + Environment.NewLine + "";
                                    }
                                }
                            }

                            //Receiver
                            //if (_ReceiverName == "")
                            //{
                            //    strMessage += "Receiver Name" + Environment.NewLine + "";
                            //}
                            //if (_ReceiverETIN == "")
                            //{
                            //    strMessage += "Receiver ETIN" + Environment.NewLine + "";
                            //}
                            if (dtFacility != null)
                            {
                                dtFacility.Dispose();
                                dtFacility = null;
                            }
                            #endregion " Facility "

                            #region " Subscriber "
                            //Subscriber
                            if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                            {
                                for (int _InsRow = 0; _InsRow < dtPatientInsurances.Rows.Count; _InsRow++)
                                {
                                    if (_InsRow == 0)
                                    {
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubLName"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Last Name" + Environment.NewLine + "";
                                        }
                                        //if (_SubscriberInsurancePST == "")
                                        //{
                                        //    //strMessage += "Subscriber Insurance Type(P/S/T)" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Relationship Code" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Insurance Type" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubFName"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber First Name" + Environment.NewLine + "";
                                        }
                                        //if (_SubscriberMName == "")
                                        //{
                                        //    // strMessage += "Subscriber Middle Name"+Environment.NewLine+"";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Insurance ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sGroup"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Group ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Zip" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["dtDOB"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberGender"]).Trim() == "")
                                        {
                                            strMessage += "Subscriber Gender" + Environment.NewLine + "";
                                        }

                                        //Payer
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceName"]).Trim() == "")
                                        {
                                            strMessage += "Payer/Insurance Name" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerID"]).Trim() == "")
                                        {
                                            strMessage += "Payer ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerAddress1"]).Trim() == "")
                                        {
                                            //strMessage += "Payer Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerCity"]).Trim() == "")
                                        {
                                            //strMessage += "Payer City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerState"]).Trim() == "")
                                        {
                                            //strMessage += "Payer State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerZip"]).Trim() == "")
                                        {
                                            //strMessage += "Payer Zip" + Environment.NewLine + "";
                                        }

                                    }
                                    if (_InsRow == 1)
                                    {
                                        //Other Insurance
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubLName"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Subscriber Last Name" + Environment.NewLine + "";
                                        }
                                        //if (_OtherInsurancePST == "")
                                        //{
                                        //    //strMessage += "Secondary Insurance Type" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Type" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Subscriber Relationship Code" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sGroup"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Group ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Address" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubFName"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Subscriber First Name" + Environment.NewLine + "";
                                        }
                                        //if (_OtherInsuranceSubscriberMName == "")
                                        //{
                                        //    //strMessage += "Secondary Insurance Subscriber Middle Name" + Environment.NewLine + "";
                                        //}
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["InsuranceName"]) == "")
                                        {
                                            strMessage += "Secondary Insurance Name" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["PayerID"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Payer ID" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance City" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance State" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Zip" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["dtDOB"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                                        }
                                        if (Convert.ToString(dtPatientInsurances.Rows[_InsRow]["sSubscriberGender"]).Trim() == "")
                                        {
                                            strMessage += "Secondary Insurance Subscriber Gender" + Environment.NewLine + "";
                                        }
                                    }
                                }
                            }
                            #endregion " Subscriber "

                            #region " Patient Information "

                            //Patient Information
                            if (oPatient != null)
                            {
                                if (Convert.ToString(oTransaction.ClaimNo).Trim() == "")
                                {
                                    strMessage += "Patient Account No" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientLastName.Trim() == "")
                                {
                                    strMessage += "Patient Last Name" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientFirstName.Trim() == "")
                                {
                                    strMessage += "Patient First Name" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientMiddleName.Trim() == "")
                                {
                                    //strMessage += "Patient Middle Name" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientSSN.Trim() == "")
                                {
                                    strMessage += "Patient SSN" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientGender.Trim() == "")
                                {
                                    strMessage += "Patient Gender" + Environment.NewLine + "";
                                }
                                if (Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString())).Trim() == "")
                                {
                                    strMessage += "Patient Date of Birth" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientAddress1.Trim() == "")
                                {
                                    strMessage += "Patient Address" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientCity.Trim() == "")
                                {
                                    strMessage += "Patient City" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientState.Trim() == "")
                                {
                                    strMessage += "Patient State" + Environment.NewLine + "";
                                }
                                if (oPatient.DemographicsDetail.PatientZip.Trim() == "")
                                {
                                    strMessage += "Patient Zip" + Environment.NewLine + "";
                                }
                            }

                            #endregion " Patient Information "

                            #region " Rendering Provider "

                            _Provider = null;
                            Resource aResource = new Resource(_databaseconnectionstring);
                            _Provider = aResource.GetProviderDetail(oTransaction.Lines[0].RefferingProviderId);
                            aResource.Dispose();
                            aResource = null;

                            if (_Provider != null)
                            {
                                if (_Provider.LastName.Trim() == "")
                                {
                                    strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                                }
                                if (_Provider.FirstName.Trim() == "")
                                {
                                    strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                                }
                                if (_Provider.NPI.Trim() == "")
                                {
                                    strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                                }
                                if (_Provider.Taxonomy.Trim() == "")
                                {
                                    strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                                }
                            }

                            //Prior Authorization Number
                            //if (_PriorAuthorizationNo == "")
                            //{
                            //    //strMessage += "Prior Authorization Number" + Environment.NewLine + "";
                            //}

                            #endregion " Rendering Provider "

                            if (strMessage.Trim() != "")
                            {
                                _MessageHeader += _ClaimMessageHeader + strMessage;
                            }
                            if (oTransaction != null)
                            {
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                            if (oPatient != null)
                            {
                                oPatient.Dispose();
                                oPatient = null;
                            }
                            if (dtPatientInsurances != null)
                            {
                                dtPatientInsurances.Dispose();
                                dtPatientInsurances = null;
                            }
                        }

                    }
                    if (_MessageHeader != "")
                    {
                        _Message = "";
                        _Message = _MessageHeader;
                    }
                }

                if (_Message.Trim() != "")
                {
                    string _Header = "Following fields are missing in database:" + Environment.NewLine + "" + Environment.NewLine + "";
                    _Header += _Message;
                    _FilePath = _FilePath + "EDIValidation.txt";
                    System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                    oStreamWriter.WriteLine(_Header);
                    oStreamWriter.Close();
                    oStreamWriter.Dispose();
                    System.Diagnostics.Process.Start(_FilePath);
                    return false;
                }
                else
                {
                    //MessageBox.Show("All mandatory data is present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return false;
            }
            finally
            {
                if (dtClearingHouse != null) { dtClearingHouse.Dispose(); }
                if (dtSubmitter != null) { dtSubmitter.Dispose(); }
              //  if (dtReceiver != null) { dtReceiver.Dispose(); }
              //  if (dtBillingProvider != null) { dtBillingProvider.Dispose(); }
              //  if (dtRenderingProvider != null) { dtRenderingProvider.Dispose(); }
                if (dtFacility != null) { dtFacility.Dispose(); }
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }

        }

        private string FormattedClaimNumberGeneration(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
               // NumberSize = NumberSize;
            }
            return NumberSize;
        }

        private string ControlNumberGeneration(string HeaderType)
        {
            string strNumber = DateTime.Now.ToString("hhmmss");
            int _length = 0;
            string NumberSize = "";
            _length = strNumber.Trim().Length;
            if (_length == 5)
            {
                NumberSize = "000" + strNumber;
            }
            else if (_length == 6)
            {
                NumberSize = "00" + strNumber;
            }
            else if (_length == 7)
            {
                NumberSize = "0" + strNumber;
            }
            else if (_length == 8)
            {
                NumberSize = strNumber;
            }
            NumberSize = HeaderType + NumberSize;
            return NumberSize;
        }

        private DataTable GetDistinctDiagnosis(Int64 TransactionID, Int64 ClinicID, Int64 ClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtDX = new DataTable();
            try
            {
                oDB.Connect(false);

                strSQL = " Select sDx1Code AS DX from BL_Transaction_Lines WITH (NOLOCK) " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " AND sDx1Code IS NOT NULL AND sDx1Code <> '' " +
                         " Union " +
                         " Select sDx2Code AS DX from BL_Transaction_Lines WITH (NOLOCK)  " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " AND sDx2Code IS NOT NULL AND sDx2Code <> '' " +
                         " Union  " +
                         " Select sDx3Code AS DX from BL_Transaction_Lines WITH (NOLOCK) " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " AND sDx3Code IS NOT NULL AND sDx3Code <> '' " +
                         " Union  " +
                         " Select sDx4Code AS DX from BL_Transaction_Lines WITH (NOLOCK)  " +
                         " WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") AND (nClaimNumber = " + ClaimNo + ") " +
                         " AND sDx4Code IS NOT NULL AND sDx4Code <> '' ";

                oDB.Retrive_Query(strSQL, out dtDX);
                if (dtDX != null)
                {
                    return dtDX;
                }
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }
            }
        }

        private string GetEDIFileName(string DirectoryFullPath, string StartName)
        {
            string _result = "";
            try
            {
                //string _EDIFileName = StartName + DateTime.Now.ToString("MM dd yyyy hh mm ss tt") + System.Guid.NewGuid().ToString();
                //string _FileName = DirectoryFullPath + "\\" + _EDIFileName + "." + "X12";
                //int i = 0;
                //bool _DocNameFound = true;

                //while (_DocNameFound == true)
                //{
                //    _DocNameFound = File.Exists(_FileName);
                //    i++;
                //    _FileName = DirectoryFullPath + "\\" + _EDIFileName + "-" + i.ToString() + "." + "X12";
                //}
                //_result = _FileName;
                return gloGlobal.clsFileExtensions.NewDocumentName(DirectoryFullPath, ".X12", "MMddyyyyHHmmssffff");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = "";
            }
            finally
            {
            }
            return _result;
        }

        #endregion " EDI Generation Private Methods "

        public string EDI837Generation(ArrayList SelectedTransactions)
        {
            DataTable dtClearingHouse = null;
            DataTable dtSubmitter = null;
           // DataTable dtReceiver = new DataTable();
          //  DataTable dtBillingProvider = new DataTable();
          //  DataTable dtRenderingProvider = new DataTable();
            DataTable dtFacility = null;
            DataTable dtPatientInsurances = null;
            DataTable dtReferral = null;
            string _ClaimStatus = "1";
            string _result = "";
            string InterchangeHeader = "";
            string FunctionalGroupHeader = "";
            string TransactionSetHeader = "";

            //if (ValidateEDIData(SelectedTransactions))
            //{
            #region " Generate EDI "

            //string sEntity = "";
            string sInstance = "";
            //string _strSQL = "";
            //DataTable dt;
            //string _BillingProviderDetails = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");
           // bool IsSecondaryInsurance = false;
            Transaction oTransaction = null;
            //string _result = "";

            //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
            string sReceiverQualifier = "ZZ";
            string sSenderQualifier = "ZZ";
            try
            {
                //Get Clearing House Information in Datatable

               // dtClearingHouse = new DataTable();
                dtClearingHouse = ogloBilling.GetClearingHouseSettings();
                if (dtClearingHouse == null && dtClearingHouse.Rows.Count < 1)
                {
                    MessageBox.Show("Clearing House information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return "";
                }
                else
                {
                    //7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                    for (int i = 0; i < dtClearingHouse.Rows.Count; i++)
                    {
                        if (dtClearingHouse.Rows[i]["bIsDefault"].ToString() == "1")
                        {
                            if (Convert.ToString(dtClearingHouse.Rows[i]["sSenderIDQualifier"]) != "")
                            { sSenderQualifier = Convert.ToString(dtClearingHouse.Rows[i]["sSenderIDQualifier"]);}

                            if (Convert.ToString(dtClearingHouse.Rows[i]["sReceiverIDQualifier"]) != "")
                            { sReceiverQualifier = Convert.ToString(dtClearingHouse.Rows[i]["sReceiverIDQualifier"]); }
                        }
                    }
                }

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                           // oTransaction = new Transaction();
                            //TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    //Get Submitter Information in Datatable
                                  //  dtSubmitter = new DataTable();
                                    if (dtSubmitter != null) { dtSubmitter.Dispose(); }
                                    dtSubmitter = ogloBilling.GetSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);
                                    if (dtSubmitter == null && dtSubmitter.Rows.Count < 1)
                                    {
                                        MessageBox.Show("Submitter information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return "";
                                    }
                                }
                                oTransaction.Dispose();
                                oTransaction = null;
                            }
                        }
                    }
                }
                if (oEdiDoc == null)
                {
                    oEdiDoc = new ediDocument();
                }
                oEdiDoc.New();
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);

                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";


                #region " Interchange Segment "
                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "00");
                oSegment.set_DataElementValue(5, 0, sSenderQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim());//_SenderID.Trim());//"1234545");//
                oSegment.set_DataElementValue(7, 0, sReceiverQualifier);//7022Items: ISA Segment change Lymphedema Clearing house claim Changes(ISA)
                oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim());//_ReceiverID.Trim());//"V2EL");//
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                InterchangeHeader = ControlNumberGeneration("1");
                oSegment.set_DataElementValue(13, 0, InterchangeHeader);//"000000020");//
                oSegment.set_DataElementValue(14, 0, "0");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion " Interchange Segment "

                #region " Functional Group "

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X098A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HC");
                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim());////_SenderName);
                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim());//// _ReceiverCode.Trim());//"ClarEDI");
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());
                FunctionalGroupHeader = ControlNumberGeneration("2");
                oSegment.set_DataElementValue(6, 0, FunctionalGroupHeader);
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X098A1");

                #endregion " Functional Group "

                #region ST - TRANSACTION SET HEADER

                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("837"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                TransactionSetHeader = ControlNumberGeneration("3");
                oSegment.set_DataElementValue(2, 0, TransactionSetHeader); //"00021");//"ControlNo"

                #endregion ST - TRANSACTION SET HEADER

                #region BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                //Begining Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0019"); //Herarchical Structure Code
                oSegment.set_DataElementValue(2, 0, "00"); //00-Original, 01-Re-issue
                oSegment.set_DataElementValue(3, 0, "1234"); //Reference identification
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//Date of claim
                string BHT_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString())).Trim();
                oSegment.set_DataElementValue(5, 0, FormattedTime(BHT_Time).Trim()); //"1230");
                oSegment.set_DataElementValue(6, 0, "CH"); //CH-Chargeable, RP-Reporting
                #endregion BHT - BEGINNING OF HIERARCHICAL TRANSACTION

                #region REF - TRANSMISSION TYPE IDENTIFICATION

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("REF"));
                oSegment.set_DataElementValue(1, 0, "87");
                oSegment.set_DataElementValue(2, 0, "004010X098A1");//"ReferenceID"

                #endregion REF - TRANSMISSION TYPE IDENTIFICATION

                #region NM1 - SUBMITTER


                //1000A SUBMITTER
                //NM1 SUBMITTER

                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\NM1"));
                oSegment.set_DataElementValue(1, 0, "41");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim());//_SubmitterName);//cmbClinic.Text.Trim());// clinic name
                oSegment.set_DataElementValue(8, 0, "46"); // Identification Code Qualifier 
                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim());//"C0923");//_SubmitterETIN);//txtSubIdentificationCode.Text.Trim());//clinic code or Electronic Transmitter Identification No.


                //PER SUBMITTER EDI CONTACT INFORMATION
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1\\PER"));
                oSegment.set_DataElementValue(1, 0, "IC");
                if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim() == "")
                {
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim());//txtSubmitterContactName.Text.Trim());//Contact person name of clinic
                }
                else
                {
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim());
                }

                oSegment.set_DataElementValue(3, 0, "TE");
                oSegment.set_DataElementValue(4, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim());//txtSubmitterPhone.Text.Trim().Replace("(", "").Replace(")", "").Replace("-", ""));//clinic phone


                #endregion NM1 - SUBMITTER

                #region NM1 - RECEIVER NAME

                //1000B RECEIVER
                //NM1 RECEIVER NAME
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("NM1(2)\\NM1"));
                oSegment.set_DataElementValue(1, 0, "40");
                oSegment.set_DataElementValue(2, 0, "2");
                oSegment.set_DataElementValue(3, 0, "GatewayEDI");//clearing house or contractor or carrier or FI name
                oSegment.set_DataElementValue(8, 0, "46");// Identification Code Qualifier
                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]));//"V2093");//code of carrier/contractor/FI or Electronic Transmitter Identification No.

                #endregion NM1 - RECEIVER NAME

                nHlCount = 0;

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                            //oTransaction = new Transaction();
                            TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    //FillAllDetails(oTransaction);
                                    
                                    Provider _Provider = null;
                                    gloPatient.Patient oPatient = null;
                                   // gloPatient.Referrals oReferral = new gloPatient.Referrals();
                                    if (Convert.ToInt64(oTransaction.ProviderID) != 0 && oTransaction.ProviderID.ToString() != "")
                                    {
                                        gloPatient.gloPatient ogloPatient=null;
                                        Resource oResource = new Resource(_databaseconnectionstring);
                                        try
                                        {
                                            _Provider = oResource.GetProviderDetail(Convert.ToInt64(oTransaction.ProviderID));
                                            if (_Provider == null)
                                            {
                                                MessageBox.Show("Provider information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }


                                            ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                                            dtPatientInsurances = ogloPatient.getPatientInsurances(oTransaction.PatientID);
                                            if (dtPatientInsurances == null && dtPatientInsurances.Rows.Count < 1)
                                            {
                                                MessageBox.Show("Patient Insurance information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }
                                            dtFacility = ogloBilling.GetFacilityInfo(oTransaction.FacilityCode, oTransaction.ProviderID);
                                            oPatient = ogloPatient.GetPatient(oTransaction.PatientID);
                                            if (oPatient == null)
                                            {
                                                MessageBox.Show("Patient information is not present.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                return "";
                                            }
                                        }
                                        catch
                                        {
                                            return "";
                                        }
                                        finally
                                        {
                                            if (oResource != null)
                                            {
                                                oResource.Dispose();
                                                oResource = null;
                                            }
                                            if (ogloPatient != null)
                                            {
                                                ogloPatient.Dispose();
                                                ogloPatient = null;
                                            }
                                        }
                                    }

                                    for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                    {
                                        //**** BILLING/PAY-TO PROVIDER HIERARCHICAL LEVEL *******************************************

                                        nHlCount = nHlCount + 1;
                                        nHlProvParent = nHlCount;
                                        //2000A BILLING/PAY-TO PROVIDER HL LOOP
                                        //HL-BILLING PROVIDER

                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                        oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
                                        oSegment.set_DataElementValue(3, 0, "20");
                                        oSegment.set_DataElementValue(4, 0, "1");

                                        #region Billing Provider

                                        //2010AA BILLING PROVIDER
                                        //NM1 BILLING PROVIDER NAME
                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                        oSegment.set_DataElementValue(1, 0, "85");
                                        oSegment.set_DataElementValue(2, 0, "1");
                                        oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim());//Billing provider name
                                        oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim());
                                        oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim());

                                        oSegment.set_DataElementValue(8, 0, "XX");
                                        if (_Provider.NPI.Trim() != "")
                                        {
                                            oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim());//Billing provider ID/NPI
                                        }

                                        //Get the Address Setting for Billing Provider
                                        gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
                                        Object _objResult = null;
                                        string strBillingSetting = "";
                                        oSettings.GetSetting("BillingSetting", Convert.ToInt64(oTransaction.ProviderID), _ClinicID, out _objResult);
                                        oSettings.Dispose();
                                        oSettings = null;

                                        if (_objResult != null)
                                        {
                                            // |Company|Practice|Business"
                                            strBillingSetting = Convert.ToString(_objResult);
                                        }
                                        switch (strBillingSetting)
                                        {
                                            case "Business":
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, _Provider.BMAddress1);//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, _Provider.BMCity);////Provider City
                                                    oSegment.set_DataElementValue(2, 0, _Provider.BMState);//Provider state
                                                    oSegment.set_DataElementValue(3, 0, _Provider.BMZIP);//Provider ZIP

                                                } break;
                                            case "Practice":
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, _Provider.BPracAddress1);//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, _Provider.BPracCity);////Provider City
                                                    oSegment.set_DataElementValue(2, 0, _Provider.BPracState);//Provider state
                                                    oSegment.set_DataElementValue(3, 0, _Provider.BPracZIP);//Provider ZIP

                                                } break;
                                            case "Company":
                                                {
                                                    //N3 BILLING PROVIDER ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, _Provider.CompanyAddress1);//Provider Address

                                                    //N4 BILLING PROVIDER LOCATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, _Provider.CompanyCity);////Provider City
                                                    oSegment.set_DataElementValue(2, 0, _Provider.CompanyState);//Provider state
                                                    oSegment.set_DataElementValue(3, 0, _Provider.CompanyZip);//Provider ZIP

                                                } break;
                                            default:
                                                //N3 BILLING PROVIDER ADDRESS
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, _Provider.BMAddress1);//Provider Address

                                                //N4 BILLING PROVIDER LOCATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, _Provider.BMCity);////Provider City
                                                oSegment.set_DataElementValue(2, 0, _Provider.BMState);//Provider state
                                                oSegment.set_DataElementValue(3, 0, _Provider.BMZIP);//Provider ZIP

                                                break;
                                        }

                                        //REF 
                                        if (_Provider.EmployerID.Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "EI");//Reference Identification Qualifier("EI" stands for-> Employer ID)
                                            if (_Provider.EmployerID.Length > 9)
                                            {
                                                oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim().Substring(0, 9));
                                            }
                                            oSegment.set_DataElementValue(2, 0, _Provider.EmployerID.Trim());
                                        }
                                        //REF 
                                        else if (_Provider.SSN.Trim() != "")
                                        {
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                            oSegment.set_DataElementValue(1, 0, "SY");//Reference Identification Qualifier("SY" stands for-> Social Security Number)
                                            if (_Provider.SSN.Trim().Length > 9)
                                            {
                                                oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim().Substring(0, 9));
                                            }
                                            oSegment.set_DataElementValue(2, 0, _Provider.SSN.Trim());
                                        }
                                        //REF 
                                        ////if (_BillingStateMedicalNo.Trim() != "")
                                        ////{
                                        ////    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\REF"));
                                        ////    oSegment.set_DataElementValue(1, 0, "0B");//Reference Identification Qualifier("0B" stands for-> State Licence No)
                                        ////    if (_BillingStateMedicalNo.Length > 9)
                                        ////    {
                                        ////        _BillingStateMedicalNo = _BillingStateMedicalNo.Substring(0, 9);
                                        ////    }
                                        ////    oSegment.set_DataElementValue(2, 0, _BillingStateMedicalNo);
                                        ////}
                                        #endregion

                                        //'******************************************************************************************************
                                        //'******* SUBSCRIBER HIERARCHICAL LEVEL ****************************************************************
                                        //'******************************************************************************************************
                                        #region Subscriber
                                        if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                        {

                                            #region Subscriber HL Loop - 2000B

                                            nHlCount = nHlCount + 1;
                                            nHlSubscriberParent = nHlCount;

                                            //2000B SUBSCRIBER HL LOOP
                                            //HL-SUBSCRIBER
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                            oSegment.set_DataElementValue(1, 0, nHlCount.ToString().Trim());
                                            oSegment.set_DataElementValue(2, 0, nHlProvParent.ToString().Trim());
                                            oSegment.set_DataElementValue(3, 0, "22");

                                            if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                            {
                                                oSegment.set_DataElementValue(4, 0, "0");
                                            }
                                            else
                                            {
                                                oSegment.set_DataElementValue(4, 0, "1");

                                            }

                                            //SBR SUBSCRIBER INFORMATION
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\SBR"));
                                            oSegment.set_DataElementValue(1, 0, "P");//_SubscriberInsurancePST);//"P");
                                            if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                            {
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim());
                                            }
                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]));//"HM");

                                            //2010BA SUBSCRIBER
                                            //NM1 SUBSCRIBER NAME
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                            oSegment.set_DataElementValue(1, 0, "IL");
                                            oSegment.set_DataElementValue(2, 0, "1");
                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim());//"SubscriberLastOrgName"
                                            oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim());//"SubscriberFirstname"
                                            oSegment.set_DataElementValue(8, 0, "MI");
                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim());//"Insurance Id"

                                            //N3 SUBSCRIBER ADDRESS
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                            oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]).Trim());//"SubscriberAddress"

                                            //N4 SUBSCRIBER CITY
                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                            oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]).Trim());//"SubscriberCity"
                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]).Trim());//"SubscrberState"
                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]).Trim());//"SubscriberZip"

                                            #endregion SubscriberHL Loop - 2000B

                                            if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                            {

                                                //DMG SUBSCRIBER DEMOGRAPHIC INFORMATION
                                                string _SubscriberGender = "";
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                oSegment.set_DataElementValue(1, 0, "D8");
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim() != "")
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"SubscriberDOB"
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim() != "")
                                                {
                                                    _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim();
                                                    if (_SubscriberGender.Trim().ToUpper() == "OTHER")
                                                    {
                                                        _SubscriberGender = "U";
                                                    }
                                                    oSegment.set_DataElementValue(3, 0, _SubscriberGender.Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                                }


                                                #region Payer Information Loop 2010BB
                                                //2010BB SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim();
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Length > 35)
                                                {
                                                    _ModifiedPayerName = "";
                                                    _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Substring(0, 34);
                                                }
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim());//"PayerLastOrgName"
                                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                                ////////N3 SUBSCRIBER ADDRESS
                                                ////ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N3"));
                                                ////oSegment.set_DataElementValue(1, 0, _PayerAddress.Trim());//"InsuranceAddress"

                                                ////////N4 SUBSCRIBER CITY
                                                ////ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\N4"));
                                                ////oSegment.set_DataElementValue(1, 0, _PayerCity.Trim());//"InsuranceCity"
                                                ////oSegment.set_DataElementValue(2, 0, _PayerState.Trim());//"InsuranceState"
                                                ////oSegment.set_DataElementValue(3, 0, _PayerZip.Trim());//"InsuranceZip"

                                                #endregion


                                                //******* SUBSCRIBER CLAIM INFORMATION ***************************************************************
                                                //TODO: Get Details in DATATABLE for the fields to be entered in EDI file.
                                                string _FirstPOS = "";
                                                string _NewPOS = "";
                                                string _ClaimTotal = "";
                                                iItemCount = 0;
                                                decimal _claimAmount = 0;
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                   // oTransLine = new TransactionLine();
                                                    oTransLine = oTransaction.Lines[nLine];
                                                    _claimAmount = _claimAmount + oTransLine.Total;

                                                    _FirstPOS = oTransaction.Lines[0].POSCode;
                                                    _NewPOS = oTransLine.POSCode;
                                                }

                                                _ClaimTotal = _claimAmount.ToString("#0.00");

                                                if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                }
                                                else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                }
                                                //if (_FirstPOS ==_NewPOS)
                                                //{
                                                #region Claim Details - Loop 2300
                                                //2300 CLAIM
                                                //CLM CLAIM LEVEL INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                                oSegment.set_DataElementValue(1, 0, FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo).Trim())); //Patient Account no         
                                                oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_TOTAL))); //Claim Amount

                                                oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim()); //21 - Inpatient Hospital

                                                if (oTransaction.Transaction_Status == TransactionStatus.Batch)
                                                {
                                                    _ClaimStatus = "1";
                                                }
                                                else if (oTransaction.Transaction_Status == TransactionStatus.ReBatch)
                                                {
                                                    _ClaimStatus = "6";
                                                }
                                                oSegment.set_DataElementValue(5, 3, _ClaimStatus);
                                                oSegment.set_DataElementValue(6, 0, "Y");
                                                oSegment.set_DataElementValue(7, 0, "A");
                                                oSegment.set_DataElementValue(8, 0, "Y");
                                                oSegment.set_DataElementValue(9, 0, "Y");
                                                oSegment.set_DataElementValue(10, 0, "C");
                                                if (oTransaction.AutoClaim == true)
                                                {
                                                    if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                    {
                                                        oSegment.set_DataElementValue(11, 1, "AA");
                                                        oSegment.set_DataElementValue(11, 4, oTransaction.State.Trim());
                                                    }
                                                }

                                                string OnsetDate = "";
                                                if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "" || oTransaction.AccidentDate.ToString() != "")
                                                {
                                                    if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                        ////DTP DATE OF ONSET OF CURRENT SYMPTOMS OR ILLNESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "431");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                    else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                        ////DTP DATE OF CURRENT INJURY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "431");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                    if (oTransaction.AutoClaim == true)
                                                    {
                                                        if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                        {
                                                            OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                            ////DTP DATE OF ACCIDENT 
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                            oSegment.set_DataElementValue(1, 0, "439");
                                                            oSegment.set_DataElementValue(2, 0, "D8");
                                                            oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                        }
                                                    }
                                                }

                                                //DTP DATE OF ONSET of similar symptoms or illness
                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                //oSegment.set_DataElementValue(1, 0, "438");
                                                //oSegment.set_DataElementValue(2, 0, "D8");
                                                //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetoSimilarSyptomsorillness.Value.ToShortDateString())).Trim());
                                                //
                                                if (_FirstPOS.Trim() != "11")
                                                {
                                                    if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                    {
                                                        //DTP DATE OF Hospitalization (Admission) 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "435");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }

                                                    if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                    {
                                                        ////DTP DATE OF Discharge 
                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        //oSegment.set_DataElementValue(1, 0, "096");
                                                        //oSegment.set_DataElementValue(2, 0, "D8");
                                                        //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                        ////
                                                    }
                                                }

                                                if (oTransaction.WorkersComp == true)
                                                {
                                                    if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                                    {
                                                        //DTP DATE OF (Intial Disability period last day worked)
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "297");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
                                                        //
                                                    }

                                                    if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                                    {
                                                        //DTP DATE OF (Intial Disability period return to work)
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "296");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
                                                        //
                                                    }
                                                }
                                                if (GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim() != "")
                                                {
                                                    //REF CLEARING HOUSE CLAIM NUMBER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "G1");
                                                    oSegment.set_DataElementValue(2, 0, GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim()); //Claim No
                                                }



                                                #endregion

                                                #region HI - Diagnosis


                                                //HI HEALTH CARE DIAGNOSIS CODES
                                                #region Commented code
                                                //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));
                                                //oSegment.set_DataElementValue(1, 1, "BK");
                                                //if (oTransaction.Lines[0].Dx1Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(1, 2, oTransaction.Lines[0].Dx1Code.ToString().Replace(".", "").Trim());// Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE)));  // "0340");
                                                //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx1Code.Trim())) == false)
                                                //    {
                                                //        return;
                                                //    }
                                                //}
                                                //else
                                                //{
                                                //    //MessageBox.Show("Principle Diagnosis is not given.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                //    //return;
                                                //}
                                                //if (oTransaction.Lines[0].Dx2Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(2, 1, "BF");
                                                //    oSegment.set_DataElementValue(2, 2, oTransaction.Lines[0].Dx2Code.ToString().Replace(".", "").Trim());//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DX1_CODE))); // oCase.CaseDiagnosis[0].DiagnosisCode.ToString().Replace(".", "").Trim());
                                                //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx2Code.Trim())) == false)
                                                //    {
                                                //        return;
                                                //    }
                                                //}
                                                //if (oTransaction.Lines[0].Dx3Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(3, 1, "BF");
                                                //    oSegment.set_DataElementValue(3, 2, oTransaction.Lines[0].Dx3Code.ToString().Replace(".", "").Trim());
                                                //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx3Code.Trim())) == false)
                                                //    {
                                                //        return;
                                                //    }
                                                //}
                                                //if (oTransaction.Lines[0].Dx4Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(4, 1, "BF");
                                                //    oSegment.set_DataElementValue(4, 2, oTransaction.Lines[0].Dx4Code.ToString().Replace(".", "").Trim());
                                                //    if (IsValidICD9(Convert.ToString(oTransaction.Lines[0].Dx4Code.Trim())) == false)
                                                //    {
                                                //        return;
                                                //    }
                                                //}
                                                //if (oTransaction.Lines[0].Dx5Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(5, 1, "BF");
                                                //    oSegment.set_DataElementValue(5, 2, oTransaction.Lines[0].Dx5Code.ToString().Replace(".", "").Trim());
                                                //}
                                                //if (oTransaction.Lines[0].Dx6Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(6, 1, "BF");
                                                //    oSegment.set_DataElementValue(6, 2, oTransaction.Lines[0].Dx6Code.ToString().Replace(".", "").Trim());
                                                //}
                                                //if (oTransaction.Lines[0].Dx7Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(7, 1, "BF");
                                                //    oSegment.set_DataElementValue(7, 2, oTransaction.Lines[0].Dx7Code.ToString().Replace(".", "").Trim());
                                                //}
                                                //if (oTransaction.Lines[0].Dx8Code.ToString().Trim() != "")
                                                //{
                                                //    oSegment.set_DataElementValue(8, 1, "BF");
                                                //    oSegment.set_DataElementValue(8, 2, oTransaction.Lines[0].Dx8Code.ToString().Replace(".", "").Trim());
                                                //}
                                                ////} 
                                                #endregion Commented code
                                                DataTable dtDx =  GetDistinctDiagnosis(oTransaction.TransactionID, oTransaction.ClinicID, oTransaction.ClaimNo);

                                                if (dtDx != null && dtDx.Rows.Count > 0)
                                                {


                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));

                                                    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    {
                                                        if (DxIndex == 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() != "")
                                                            {
                                                                if (IsValidICD9(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim()) == false)
                                                                {
                                                                    return "";
                                                                }
                                                                oSegment.set_DataElementValue(1, 1, "BK");
                                                                oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim());
                                                            }
                                                        }
                                                        if (DxIndex > 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() != "")
                                                            {
                                                                if (IsValidICD9(Convert.ToString(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim())) == false)
                                                                {
                                                                    return "";
                                                                }
                                                                oSegment.set_DataElementValue(DxIndex + 1, 1, "BF");
                                                                oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim());//
                                                            }
                                                        }
                                                    }
                                                }
                                                if (dtDx != null)
                                                {
                                                    dtDx.Dispose();
                                                    dtDx = null;
                                                }
                                                #endregion

                                                #region Referring Provider - 2310A
                                                gloPatient.Referrals oReferral;
                                                oReferral = oPatient.Referrals;
                                                if (oReferral.Count > 0)
                                                {
                                                    if (oDB == null)
                                                    {
                                                        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                    }
                                                    dtReferral = new DataTable();
                                                    string _sqlQuery = "";

                                                    oDB.Connect(false);
                                                    _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                                                " FROM Contacts_MST WITH (NOLOCK)  " +
                                                                " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                                                    oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                                    if (dtReferral != null && dtReferral.Rows.Count > 0)
                                                    {
                                                        //2310B Referring PROVIDER
                                                        //NM1 Referring PROVIDER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "DN");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sLastName"].ToString().Trim()); //"ReferringLastname"
                                                        oSegment.set_DataElementValue(4, 0, dtReferral.Rows[0]["sFirstName"].ToString().Trim());//"ReferringFirstname"
                                                        oSegment.set_DataElementValue(5, 0, dtReferral.Rows[0]["sMiddleName"].ToString().Trim());
                                                        oSegment.set_DataElementValue(8, 0, "XX");
                                                        oSegment.set_DataElementValue(9, 0, dtReferral.Rows[0]["sNPI"].ToString().Trim());//"NPI"

                                                        //PRV REFERRING PROVIDER INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        oSegment.set_DataElementValue(1, 0, "RF");
                                                        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sTaxonomy"].ToString().Trim());//Reference Identification

                                                        //REF
                                                        if (dtReferral.Rows[0]["sTaxID"].ToString().Trim() != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                            oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim());//"1039255");// 
                                                        }
                                                        else //if (_ReferralSSN.Trim() != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                            oSegment.set_DataElementValue(2, 0, "232929");//"1039255");// 
                                                        }
                                                    }
                                                    if (dtReferral != null)
                                                    {
                                                        dtReferral.Dispose();
                                                        dtReferral = null;
                                                    }
                                                }
                                                #endregion Referring Provider

                                                #region Rendering Provider - 2310B

                                                _Provider = null;
                                                Resource aResource = new Resource(_databaseconnectionstring);
                                                _Provider = aResource.GetProviderDetail(oTransaction.Lines[0].RefferingProviderId);
                                                aResource.Dispose();
                                                aResource = null;

                                                if (_Provider != null)
                                                {
                                                    //2310B RENDERING PROVIDER
                                                    //NM1 RENDERING PROVIDER NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "82");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    //FillProviderDetails(oTransaction.Lines[0].RefferingProviderId, ProviderType.RenderingProvider);
                                                    oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim());//Billing provider name
                                                    oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim());
                                                    oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim());
                                                    oSegment.set_DataElementValue(8, 0, "XX");
                                                    oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


                                                    //PRV RENDERING PROVIDER INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                    oSegment.set_DataElementValue(1, 0, "PE");
                                                    oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                    oSegment.set_DataElementValue(3, 0, _Provider.Taxonomy.Trim());//Reference Identification
                                                }


                                                #endregion

                                                #region Facility - 2310D

                                                //2310D SERVICE LOCATION
                                                //NM1 SERVICE FACILITY LOCATION
                                                if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "77");
                                                    oSegment.set_DataElementValue(2, 0, "2");
                                                    oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityName"].ToString().Trim());//"FacilityName"
                                                    oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                                    oSegment.set_DataElementValue(9, 0, dtFacility.Rows[0]["FacilityNPI"].ToString().Trim());//NPI

                                                    //N3 SERVICE FACILITY ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityAddress1"].ToString().Trim());//"FacilityAddr"

                                                    //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityCity"].ToString().Trim());//"FacilityCity"
                                                    oSegment.set_DataElementValue(2, 0, dtFacility.Rows[0]["FacilityState"].ToString().Trim());//"FacilityState"
                                                    oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityZip"].ToString().Trim());//"FacilityZip"
                                                }
                                                #endregion

                                                for (int _Insrow = 0; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                {
                                                    #region Subscriber Secondary Insurance - Loop 2320

                                                    //LOOP - 2320
                                                    if (_Insrow == 1)
                                                    {

                                                        #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                        //1.Payer Resposibilty Sequence No.
                                                        oSegment.set_DataElementValue(1, 0, "S");//_OtherInsurancePST.Trim()); //P - Primary

                                                        //2.Individual Relationship code
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim());//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                        //3.Refrence identification
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim());//"22145");///Policy no

                                                        //5.Insurance Type Code
                                                        oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


                                                        //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

                                                        ////8.Employment Status Code(Not Used)
                                                        //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

                                                        //9.Claim Filing Indicator
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim()); //Commercial Insurance company

                                                        #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        #region CAS - Claim Adjustment

                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
                                                        //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
                                                        //oSegment.set_DataElementValue(2, 0, "96");
                                                        //oSegment.set_DataElementValue(3, 0, "300");

                                                        #endregion CAS - Claim Adjustment

                                                        #region AMT - Amount

                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                        //oSegment.set_DataElementValue(1, 0, "D");
                                                        //oSegment.set_DataElementValue(2, 0, "0");

                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                        //oSegment.set_DataElementValue(1, 0, "F2");
                                                        //oSegment.set_DataElementValue(2, 0, "100");

                                                        #endregion AMT - Amount

                                                        #region MOA - Medicare Outpatient Adjudication

                                                        //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
                                                        //oSegment.set_DataElementValue(1,0,"20");
                                                        //oSegment.set_DataElementValue(2,0,"300");
                                                        //oSegment.set_DataElementValue(3,0,"125");

                                                        #endregion

                                                        #region DMG  - Demographic

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                        oSegment.set_DataElementValue(1, 0, "D8");
                                                        oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"])).ToString());//"SubscriberDOB"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]));//"SubscriberGender"

                                                        #endregion DMG  - Demographic

                                                        #region OI - Other Insurance

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                        oSegment.set_DataElementValue(3, 0, "Y");
                                                        oSegment.set_DataElementValue(4, 0, "C");
                                                        oSegment.set_DataElementValue(6, 0, "Y");

                                                        #endregion OI - Other Insurance

                                                        //2330A SUBSCRIBER
                                                        #region NM1 SUBSCRIBER NAME - 2330A

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "IL");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim());//"SubscriberLastOrgName"
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim());//"SubscriberFirstname"
                                                        oSegment.set_DataElementValue(8, 0, "MI");
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim());//"SubscriberMemberID"

                                                        //N3 SUBSCRIBER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim());//"SubscriberAddress"

                                                        //N4 SUBSCRIBER CITY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim());//"SubscriberCity"
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim());//"SubscrberState"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim());//"SubscriberZip"

                                                        #endregion NM1 SUBSCRIBER NAME

                                                        #region Payer Information - 2330B

                                                        //2330B SUBSCRIBER/PAYER
                                                        //NM1 PAYER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "PR");
                                                        oSegment.set_DataElementValue(2, 0, "2");

                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim());//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim());//"PayerLastOrgName"

                                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                        //}

                                                        #endregion Payer Information

                                                    }

                                                    #endregion Subscriber Secondary Insurance
                                                }//End for loop of Patient Insurance 
                                                //}//end of IF loop for POS
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    iItemCount = 1;
                                                    iItemCount = iItemCount + nLine;
                                                   // oTransLine = new TransactionLine();
                                                    oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                    //2400 SERVICE LINE
                                                    sInstance = iItemCount.ToString().Trim();
                                                    //LX SERVICE LINE COUNTER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    //SV1 PROFESSIONAL SERVICE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                    oSegment.set_DataElementValue(1, 1, "HC");
                                                    oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    if (oTransLine.Mod1Code.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    }
                                                    if (oTransLine.Mod2Code.ToString().Trim() != "")
                                                    {
                                                        if (oTransLine.Mod1Code.ToString().Trim() == "")
                                                        {
                                                            oSegment.set_DataElementValue(1, 3, oTransLine.Mod2Code.ToString());
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                        }
                                                    }
                                                    string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    }
                                                    else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                    }
                                                    oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                    oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity

                                                    if (dtDx != null && dtDx.Rows.Count > 0)
                                                    {
                                                        int _CompTerminatorPos = 0;

                                                        for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx1Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "1");
                                                            }
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx2Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "2");
                                                            }
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx3Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "3");
                                                            }
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx4Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "4");
                                                            }
                                                        }
                                                    }



                                                    //DTP DATE - SERVICE DATE(S)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "472");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                    #endregion
                                                }
                                        #endregion " Subscriber "

                                            }//end of if loop for Subscriber as Patient
                                            else
                                            {
                                                #region "Dependent Loop"

                                                //////*****************************************************************************************************
                                                //////******* DEPENDENT HIERARCHICAL LEVEL ****************************************************************
                                                //////*****************************************************************************************************
                                                ////TODO: Get the datatable for dependent info to add fields of service in EDI file.

                                                #region Payer Information Loop 2010BB
                                                //2010BB SUBSCRIBER/PAYER
                                                //NM1 PAYER NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1(2)\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");
                                                oSegment.set_DataElementValue(2, 0, "2");
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim());//"PayerLastOrgName"
                                                oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID

                                                #endregion

                                                nHlCount = nHlCount + 1;

                                                //2000B DEPENDENT HL LOOP
                                                //HL-DEPENDENT
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCount.ToString());
                                                oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());
                                                oSegment.set_DataElementValue(3, 0, "23");
                                                oSegment.set_DataElementValue(4, 0, "0");

                                                //PAT - PATIENT/DEPENDENT INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\PAT"));
                                                oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim()); //01 - Spouse 19 - Child

                                                #region " Patient Info"

                                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "QC");
                                                oSegment.set_DataElementValue(2, 0, "1");
                                                oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientLastName.Trim());//Patient Last Name
                                                oSegment.set_DataElementValue(4, 0, oPatient.DemographicsDetail.PatientFirstName.Trim());//Patient First Name

                                                //N3 - ADDRESS INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N3"));
                                                oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientAddress1.Trim());//"Address"

                                                //N4 - GEOGRAPHIC LOCATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\N4"));
                                                oSegment.set_DataElementValue(1, 0, oPatient.DemographicsDetail.PatientCity.Trim());//"City"
                                                oSegment.set_DataElementValue(2, 0, oPatient.DemographicsDetail.PatientState.Trim());//"State"
                                                oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientZip.Trim());//"Zip"

                                                //DMG - DEMOGRAPHIC INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\DMG"));
                                                oSegment.set_DataElementValue(1, 0, "D8");
                                                oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString())));
                                                oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientGender.Trim());

                                                #endregion " Patient Info"

                                                //******* DEPENDENT CLAIM INFORMATION *************************************************************
                                                //TODO: Get the datatable for Claim info to add fields of service in EDI file
                                                string _FirstPOS = "";
                                                string _NewPOS = "";
                                                string _ClaimTotal = "";
                                                iItemCount = 0;
                                                iItemCount = 1;
                                                decimal _claimAmount = 0;
                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                   // oTransLine = new TransactionLine();
                                                    oTransLine = oTransaction.Lines[nLine];
                                                    _claimAmount = _claimAmount + oTransLine.Total;

                                                    _FirstPOS = oTransaction.Lines[0].POSCode;
                                                    _NewPOS = oTransLine.POSCode;
                                                }
                                                _ClaimTotal = _claimAmount.ToString("#0.00");
                                                if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                                }
                                                else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                                {
                                                    _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                                }

                                                #region "Dependent Claim Level"
                                                //2300 CLAIM
                                                //CLM CLAIM LEVEL INFORMATION
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\CLM"));
                                                oSegment.set_DataElementValue(1, 0, FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo).Trim())); //Patient Account no         
                                                oSegment.set_DataElementValue(2, 0, _ClaimTotal.Trim()); //Claim Amount
                                                oSegment.set_DataElementValue(5, 1, _FirstPOS.Trim()); //21 - Inpatient Hospital

                                                if (oTransaction.Transaction_Status == TransactionStatus.Batch)
                                                {
                                                    _ClaimStatus = "1";
                                                }
                                                else if (oTransaction.Transaction_Status == TransactionStatus.ReBatch)
                                                {
                                                    _ClaimStatus = "6";
                                                }
                                                oSegment.set_DataElementValue(5, 3, _ClaimStatus);
                                                oSegment.set_DataElementValue(6, 0, "Y");
                                                oSegment.set_DataElementValue(7, 0, "A");
                                                oSegment.set_DataElementValue(8, 0, "Y");
                                                oSegment.set_DataElementValue(9, 0, "Y");
                                                oSegment.set_DataElementValue(10, 0, "C");

                                                string OnsetDate = "";
                                                if (oTransaction.InjuryDate.ToString() != "" || oTransaction.OnsiteDate.ToString() != "")
                                                {
                                                    if (oTransaction.InjuryDate.ToString() != "" && oTransaction.InjuryDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.InjuryDate);
                                                        ////DTP DATE OF ONSET of current symptoms or illness
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "431");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                    else if (oTransaction.OnsiteDate.ToString() != "" && oTransaction.OnsiteDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.OnsiteDate);
                                                        ////DTP DATE OF ONSET of current symptoms or illness
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "431");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }

                                                    if (oTransaction.AccidentDate.ToString() != "" && oTransaction.AccidentDate > 0)
                                                    {
                                                        OnsetDate = Convert.ToString(oTransaction.AccidentDate);
                                                        ////DTP DATE OF ACCIDENT 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "439");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, OnsetDate);//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpOnsetofCurrentSymptomsorillness.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }
                                                }

                                                //DTP DATE OF ONSET of similar symptoms or illness
                                                if (oTransaction.OnsiteDate > 0 && oTransaction.OnsiteDate.ToString() != "")
                                                {
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "438");
                                                    //oSegment.set_DataElementValue(2, 0, "D8");
                                                    //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransaction.OnsiteDate.ToString())).Trim());
                                                }
                                                //
                                                if (_FirstPOS.Trim() != "11")
                                                {
                                                    if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateFrom.ToString() != "")
                                                    {
                                                        //DTP DATE OF Hospitalization (Admission) 
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "435");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateFrom.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalizationDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                    }

                                                    if (oTransaction.HospitalizationDateTo > 0 && oTransaction.HospitalizationDateTo.ToString() != "")
                                                    {
                                                        ////DTP DATE OF Discharge 
                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        //oSegment.set_DataElementValue(1, 0, "096");
                                                        //oSegment.set_DataElementValue(2, 0, "D8");
                                                        //oSegment.set_DataElementValue(3, 0, oTransaction.HospitalizationDateTo.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpHospitalDischargeDate.Value.ToShortDateString())).Trim());     //Claim Date
                                                        ////
                                                    }
                                                }
                                                if (oTransaction.WorkersComp == true)
                                                {
                                                    if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkFromDate.ToString() != "")
                                                    {
                                                        //DTP DATE OF (Intial Disability period last day worked)
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "297");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkFromDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpLastDayWorked.Value.ToShortDateString())).Trim());     //Claim Date
                                                        //
                                                    }

                                                    if (oTransaction.UnableToWorkTillDate > 0 && oTransaction.UnableToWorkTillDate.ToString() != "")
                                                    {
                                                        //DTP DATE OF (Intial Disability period return to work)
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "296");
                                                        oSegment.set_DataElementValue(2, 0, "D8");
                                                        oSegment.set_DataElementValue(3, 0, oTransaction.UnableToWorkTillDate.ToString());//Convert.ToString(gloDateMaster.gloDate.DateAsNumber(dtpReturnToWork.Value.ToShortDateString())).Trim());     //Claim Date
                                                        //
                                                    }
                                                }

                                                //REF CLEARING HOUSE CLAIM NUMBER
                                                if (GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim() != "")
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\REF"));
                                                    oSegment.set_DataElementValue(1, 0, "G1");
                                                    oSegment.set_DataElementValue(2, 0, GetPriorAuthorizationNumber(oTransaction.PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"])).Trim()); //Claim No
                                                }
                                                #endregion "Dependent Claim Level"

                                                #region HI - Diagnosis for Dependent
                                                //HI HEALTH CARE DIAGNOSIS CODES

                                                DataTable dtDx =  GetDistinctDiagnosis(oTransaction.TransactionID, oTransaction.ClinicID, oTransaction.ClaimNo);

                                                if (dtDx != null && dtDx.Rows.Count > 0)
                                                {
                                                    
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\HI"));

                                                    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    {
                                                        if (DxIndex == 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() != "")
                                                            {
                                                                if (IsValidICD9(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim()) == false)
                                                                {
                                                                    return "";
                                                                }
                                                                oSegment.set_DataElementValue(1, 1, "BK");
                                                                oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Replace(".", "").Trim());
                                                            }
                                                        }
                                                        if (DxIndex > 0)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex][0]).Trim() != "")
                                                            {
                                                                if (IsValidICD9(Convert.ToString(Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim())) == false)
                                                                {
                                                                    return "";
                                                                }
                                                                oSegment.set_DataElementValue(DxIndex + 1, 1, "BF");
                                                                oSegment.set_DataElementValue(DxIndex + 1, 2, Convert.ToString(dtDx.Rows[DxIndex]["DX"]).ToString().Replace(".", "").Trim());//
                                                            }
                                                        }
                                                    }
                                                }
                                                if (dtDx != null)
                                                {
                                                    dtDx.Dispose();
                                                    dtDx = null;
                                                }
                                                #endregion

                                                #region Referring Provider - 2310A
                                                gloPatient.Referrals oReferral;
                                                oReferral = oPatient.Referrals;
                                                if (oReferral.Count > 0)
                                                {
                                                    if (oDB == null)
                                                    {
                                                        oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                                                    }
                                                    dtReferral = new DataTable();
                                                    string _sqlQuery = "";

                                                    oDB.Connect(false);
                                                    _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                                                " FROM Contacts_MST WITH (NOLOCK) " +
                                                                " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                                                    oDB.Retrive_Query(_sqlQuery, out dtReferral);
                                                    if (dtReferral != null && dtReferral.Rows.Count > 0)
                                                    {
                                                        //2310B Referring PROVIDER
                                                        //NM1 Referring PROVIDER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "DN");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sLastName"].ToString().Trim()); //"ReferringLastname"
                                                        oSegment.set_DataElementValue(4, 0, dtReferral.Rows[0]["sFirstName"].ToString().Trim());//"ReferringFirstname"
                                                        oSegment.set_DataElementValue(5, 0, dtReferral.Rows[0]["sMiddleName"].ToString().Trim());
                                                        oSegment.set_DataElementValue(8, 0, "XX");
                                                        oSegment.set_DataElementValue(9, 0, dtReferral.Rows[0]["sNPI"].ToString().Trim());//"NPI"

                                                        //PRV REFERRING PROVIDER INFORMATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                        oSegment.set_DataElementValue(1, 0, "RF");
                                                        oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                        oSegment.set_DataElementValue(3, 0, dtReferral.Rows[0]["sTaxonomy"].ToString().Trim());//Reference Identification

                                                        //REF
                                                        if (dtReferral.Rows[0]["sTaxID"].ToString().Trim() != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "EI");//// Employer Identification Number
                                                            oSegment.set_DataElementValue(2, 0, dtReferral.Rows[0]["sTaxID"].ToString().Trim());//"1039255");// 
                                                        }
                                                        else //if (_ReferralSSN.Trim() != "")
                                                        {
                                                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\REF"));
                                                            oSegment.set_DataElementValue(1, 0, "SY");//// Social Security Number
                                                            oSegment.set_DataElementValue(2, 0, "32432432");//dtReferral.Rows[0]["sTaxID"].ToString().Trim());//"1039255");// 
                                                        }
                                                    }
                                                    if (dtReferral != null)
                                                    {
                                                        dtReferral.Dispose();
                                                        dtReferral = null;
                                                    }
                                                }
                                                #endregion Referring Provider

                                                #region Rendering Provider - 2310B

                                                _Provider = null;
                                                Resource aResource = new Resource(_databaseconnectionstring);
                                                _Provider = aResource.GetProviderDetail(oTransaction.Lines[0].RefferingProviderId);
                                                aResource.Dispose();
                                                aResource = null;
                                                if (_Provider != null)
                                                {

                                                    //2310B RENDERING PROVIDER
                                                    //NM1 RENDERING PROVIDER NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "82");
                                                    oSegment.set_DataElementValue(2, 0, "1");
                                                    //FillProviderDetails(oTransaction.Lines[0].RefferingProviderId, ProviderType.RenderingProvider);
                                                    oSegment.set_DataElementValue(3, 0, _Provider.LastName.Trim());//Billing provider name
                                                    oSegment.set_DataElementValue(4, 0, _Provider.FirstName.Trim());
                                                    oSegment.set_DataElementValue(5, 0, _Provider.MiddleName.Trim());
                                                    oSegment.set_DataElementValue(8, 0, "XX");
                                                    oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim());//oProviderDetails.NPI);//Billing provider ID/NPI


                                                    //PRV RENDERING PROVIDER INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1\\PRV"));
                                                    oSegment.set_DataElementValue(1, 0, "PE");
                                                    oSegment.set_DataElementValue(2, 0, "ZZ");//Mutually Defined
                                                    oSegment.set_DataElementValue(3, 0, _Provider.Taxonomy.Trim());//Reference Identification
                                                }

                                                #endregion

                                                #region Facility - 2310D

                                                //2310D SERVICE LOCATION
                                                //NM1 SERVICE FACILITY LOCATION
                                                if (dtFacility != null && dtFacility.Rows.Count > 0)
                                                {
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "77");
                                                    oSegment.set_DataElementValue(2, 0, "2");
                                                    oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityName"].ToString().Trim());//"FacilityName"
                                                    oSegment.set_DataElementValue(8, 0, "XX");//NPI code
                                                    oSegment.set_DataElementValue(9, 0, dtFacility.Rows[0]["FacilityNPI"].ToString().Trim());//NPI

                                                    //N3 SERVICE FACILITY ADDRESS
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N3"));
                                                    oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityAddress1"].ToString().Trim());//"FacilityAddr"

                                                    //N4 SERVICE FACILITY CITY/STATE/ZIP
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\NM1(2)\\N4"));
                                                    oSegment.set_DataElementValue(1, 0, dtFacility.Rows[0]["FacilityCity"].ToString().Trim());//"FacilityCity"
                                                    oSegment.set_DataElementValue(2, 0, dtFacility.Rows[0]["FacilityState"].ToString().Trim());//"FacilityState"
                                                    oSegment.set_DataElementValue(3, 0, dtFacility.Rows[0]["FacilityZip"].ToString().Trim());//"FacilityZip"
                                                }
                                                #endregion

                                                for (int _Insrow = 0; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                                {
                                                    #region Subscriber Secondary Insurance - Loop 2320

                                                    //LOOP - 2320
                                                    if (_Insrow == 1)
                                                    {

                                                        #region SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR"));
                                                        //1.Payer Resposibilty Sequence No.
                                                        oSegment.set_DataElementValue(1, 0, "S");//_OtherInsurancePST.Trim()); //P - Primary

                                                        //2.Individual Relationship code
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["RelationshipCode"]).Trim());//"18"); // Hard coded(Individual Relationship code) 18 - Self

                                                        //3.Refrence identification
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sGroup"]).Trim());//"22145");///Policy no

                                                        //5.Insurance Type Code
                                                        oSegment.set_DataElementValue(5, 0, "C1"); // C1 - Commercial (Insurance Type Code)


                                                        //oSegment.set_DataElementValue(6, 0, "6"); // 6 - No Co-ordination of Benefit

                                                        ////8.Employment Status Code(Not Used)
                                                        //oSegment.set_DataElementValue(8, 0, "AC"); // Employment status (AC - Active)

                                                        //9.Claim Filing Indicator
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceTypeCode"]).Trim()); //Commercial Insurance company

                                                        #endregion SBR - SUBSCRIBER INFORMATION for Secondary Information

                                                        #region CAS - Claim Adjustment

                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\CAS"));
                                                        //oSegment.set_DataElementValue(1, 0, "PI");//PR - Patient Responsibility
                                                        //oSegment.set_DataElementValue(2, 0, "96");
                                                        //oSegment.set_DataElementValue(3, 0, "300");

                                                        #endregion CAS - Claim Adjustment

                                                        #region AMT - Amount

                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                        //oSegment.set_DataElementValue(1, 0, "D");
                                                        //oSegment.set_DataElementValue(2, 0, "0");

                                                        //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\AMT"));
                                                        //oSegment.set_DataElementValue(1, 0, "F2");
                                                        //oSegment.set_DataElementValue(2, 0, "100");

                                                        #endregion AMT - Amount

                                                        #region MOA - Medicare Outpatient Adjudication

                                                        //ediDataSegment.Set(ref oSegment,(ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\MOA"));
                                                        //oSegment.set_DataElementValue(1,0,"20");
                                                        //oSegment.set_DataElementValue(2,0,"300");
                                                        //oSegment.set_DataElementValue(3,0,"125");

                                                        #endregion

                                                        #region DMG  - Demographic

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\DMG"));
                                                        oSegment.set_DataElementValue(1, 0, "D8");
                                                        oSegment.set_DataElementValue(2, 0, gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[_Insrow]["dtDOB"])).ToString());//"SubscriberDOB"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberGender"]));//"SubscriberGender"

                                                        #endregion DMG  - Demographic

                                                        #region OI - Other Insurance

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\OI"));
                                                        oSegment.set_DataElementValue(3, 0, "Y");
                                                        oSegment.set_DataElementValue(4, 0, "C");
                                                        oSegment.set_DataElementValue(6, 0, "Y");

                                                        #endregion OI - Other Insurance

                                                        //2330A SUBSCRIBER
                                                        #region NM1 SUBSCRIBER NAME - 2330A

                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "IL");
                                                        oSegment.set_DataElementValue(2, 0, "1");
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubLName"]).Trim());//"SubscriberLastOrgName"
                                                        oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubFName"]).Trim());//"SubscriberFirstname"
                                                        oSegment.set_DataElementValue(8, 0, "MI");
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["sSubscriberID"]).Trim());//"SubscriberMemberID"

                                                        //N3 SUBSCRIBER ADDRESS
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N3"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberAddr1"]).Trim());//"SubscriberAddress"

                                                        //N4 SUBSCRIBER CITY
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\N4"));
                                                        oSegment.set_DataElementValue(1, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberCity"]).Trim());//"SubscriberCity"
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberState"]).Trim());//"SubscrberState"
                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["SubscriberZip"]).Trim());//"SubscriberZip"

                                                        #endregion NM1 SUBSCRIBER NAME

                                                        #region Payer Information - 2330B

                                                        //2330B SUBSCRIBER/PAYER
                                                        //NM1 PAYER NAME
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\SBR\\NM1\\NM1"));
                                                        oSegment.set_DataElementValue(1, 0, "PR");
                                                        oSegment.set_DataElementValue(2, 0, "2");

                                                        oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["InsuranceName"]).Trim());//dtInsurance.Rows[0]["sSubscriberName"].ToString().Trim());//"PayerLastOrgName"

                                                        oSegment.set_DataElementValue(8, 0, "PI");//This is the payer ID of Medicare for Michigan
                                                        oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[_Insrow]["PayerID"]).Trim());//dtInsurance.Rows[0]["sPayerID"].ToString());//PayerID
                                                        //}

                                                        #endregion Payer Information

                                                    }

                                                    #endregion Subscriber Secondary Insurance
                                                }//End for loop of Patient Insurance 

                                                for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                                {
                                                    //iItemCount = 1;
                                                    //iItemCount = iItemCount + nLine;
                                                    //oTransLine = new TransactionLine();
                                                    //oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    ////******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    ////TODO: Get the datatable for service info to add fields of service in EDI file.
                                                    ////2400 SERVICE LINE
                                                    //sInstance = iItemCount.ToString().Trim();
                                                    ////LX SERVICE LINE COUNTER
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    //oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    ////SV1 PROFESSIONAL SERVICE
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                    //oSegment.set_DataElementValue(1, 1, "HC");
                                                    //oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    //if (oTransLine.Mod1Code.ToString() != "")
                                                    //{
                                                    //    oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    //}
                                                    //if (oTransLine.Mod2Code.ToString() != "")
                                                    //{
                                                    //    oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                    //}
                                                    //string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    //if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    //{
                                                    //    _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    //}
                                                    //oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    //oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                    //oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity


                                                    ////if (oTransLine.Dx1Ptr.ToString() == "True")
                                                    ////{
                                                    ////    oSegment.set_DataElementValue(7, 1, "1");//"Diagnosis Pointer1" (Must Use)
                                                    ////    if (oTransLine.Dx2Ptr.ToString() == "True")
                                                    ////    {
                                                    ////        oSegment.set_DataElementValue(7, 2, "2");//"Diagnosis Pointer2"
                                                    ////    }
                                                    ////    if (oTransLine.Dx3Ptr.ToString() == "True")
                                                    ////    {
                                                    ////        oSegment.set_DataElementValue(7, 3, "3");//"Diagnosis Pointer3"
                                                    ////    }
                                                    ////    if (oTransLine.Dx4Ptr.ToString() == "True")
                                                    ////    {
                                                    ////        oSegment.set_DataElementValue(7, 4, "4");//"Diagnosis Pointer4"
                                                    ////    }
                                                    ////}
                                                    ////else if (oTransLine.Dx2Ptr.ToString() == "True")
                                                    ////{
                                                    ////    oSegment.set_DataElementValue(7, 1, "2");//"Diagnosis Pointer1"
                                                    ////}
                                                    ////else if (oTransLine.Dx3Ptr.ToString() == "True")
                                                    ////{
                                                    ////    oSegment.set_DataElementValue(7, 1, "3");//"Diagnosis Pointer1"
                                                    ////}
                                                    ////else if (oTransLine.Dx4Ptr.ToString() == "True")
                                                    ////{
                                                    ////    oSegment.set_DataElementValue(7, 1, "4");//"Diagnosis Pointer1"
                                                    ////}

                                                    ////////oSegment.set_DataElementValue(9, 0, "N");////Y=Yes, N=No

                                                    //if (dtDx != null && dtDx.Rows.Count > 0)
                                                    //{
                                                    //    for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                    //    {
                                                    //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx1Code.Trim())
                                                    //        {
                                                    //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                    //        }
                                                    //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx2Code.Trim())
                                                    //        {
                                                    //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                    //        }
                                                    //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx3Code.Trim())
                                                    //        {
                                                    //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                    //        }
                                                    //        if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx4Code.Trim())
                                                    //        {
                                                    //            oSegment.set_DataElementValue(7, DxIndex + 1, Convert.ToString(DxIndex + 1));
                                                    //        }
                                                    //    }
                                                    //}

                                                    ////DTP DATE - SERVICE DATE(S)
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "472");
                                                    //oSegment.set_DataElementValue(2, 0, "D8");
                                                    //oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                    #endregion

                                                    iItemCount = 1;
                                                    iItemCount = iItemCount + nLine;
                                                   // oTransLine = new TransactionLine();
                                                    oTransLine = oTransaction.Lines[nLine];

                                                    #region Service Line
                                                    //******* SUBSCRIBER SERVICE LINE *************************************************************
                                                    //TODO: Get the datatable for service info to add fields of service in EDI file.
                                                    //2400 SERVICE LINE
                                                    sInstance = iItemCount.ToString().Trim();
                                                    //LX SERVICE LINE COUNTER
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\LX"));
                                                    oSegment.set_DataElementValue(1, 0, iItemCount.ToString());

                                                    //SV1 PROFESSIONAL SERVICE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\SV1"));
                                                    oSegment.set_DataElementValue(1, 1, "HC");
                                                    oSegment.set_DataElementValue(1, 2, oTransLine.CPTCode.ToString().Replace(".", ""));//"ServiceID"
                                                    if (oTransLine.Mod1Code.ToString().Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(1, 3, oTransLine.Mod1Code.ToString());//Modifier 1
                                                    }
                                                    if (oTransLine.Mod2Code.ToString().Trim() != "")
                                                    {
                                                        if (oTransLine.Mod1Code.ToString().Trim() == "")
                                                        {
                                                            oSegment.set_DataElementValue(1, 3, oTransLine.Mod2Code.ToString());
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(1, 4, oTransLine.Mod2Code.ToString());//Modifier 2
                                                        }
                                                    }
                                                    string _ClaimLineCharges = Convert.ToString(oTransLine.Total);

                                                    if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 2, 2) == "00")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 3);
                                                    }
                                                    else if (_ClaimLineCharges.Substring(_ClaimLineCharges.Length - 1, 1) == "0")
                                                    {
                                                        _ClaimLineCharges = _ClaimLineCharges.Substring(0, _ClaimLineCharges.Length - 1);
                                                    }
                                                    oSegment.set_DataElementValue(2, 0, _ClaimLineCharges);//"ServiceAmount"
                                                    oSegment.set_DataElementValue(3, 0, "UN");//UN stands for UNITS
                                                    oSegment.set_DataElementValue(4, 0, oTransLine.Unit.ToString());//Unit/Quantity
                                                    if (dtDx != null && dtDx.Rows.Count > 0)
                                                    {
                                                        int _CompTerminatorPos = 0;

                                                        for (int DxIndex = 0; DxIndex < dtDx.Rows.Count; DxIndex++)
                                                        {
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx1Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "1");
                                                            }
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx2Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "2");
                                                            }
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx3Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "3");
                                                            }
                                                            if (Convert.ToString(dtDx.Rows[DxIndex]["DX"]).Trim() == oTransaction.Lines[nLine].Dx4Code.Trim())
                                                            {
                                                                _CompTerminatorPos = _CompTerminatorPos + 1;
                                                                oSegment.set_DataElementValue(7, _CompTerminatorPos, "4");
                                                            }
                                                        }
                                                    }

                                                    //DTP DATE - SERVICE DATE(S)
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\CLM\\LX(" + sInstance + ")\\DTP"));
                                                    oSegment.set_DataElementValue(1, 0, "472");
                                                    oSegment.set_DataElementValue(2, 0, "D8");
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oTransLine.DateServiceFrom.ToShortDateString())));//Convert.ToString(c1Transaction.GetData(c1Transaction.RowSel, COL_DATEFROM)));//"ServiceDate"

                                                    #endregion
                                                }

                                                #endregion " Dependent "
                                            }//end of else loop for dependent

                                        }//If loop for Patient Insurance
                                        //Transaction Line Loop
                                    }//Transaction SETS Loop
                                }
                                if (oTransaction != null)
                                {
                                    oTransaction.Dispose();
                                    oTransaction = null;
                                }
                             
                            }
                            if (dtFacility != null)
                            {
                                dtFacility.Dispose();
                                dtFacility = null;
                            }
                            if (dtPatientInsurances != null)
                            {
                                dtPatientInsurances.Dispose();
                                dtPatientInsurances = null;
                            }
                        }

                        #region " Save EDI File "

                        sPath = "";                        
                        sPath = gloSettings.FolderSettings.AppTempFolderPath + "837 EDI\\";
                        if (System.IO.Directory.Exists(sPath) == false) { System.IO.Directory.CreateDirectory(sPath); }

                        sEdiFile = GetEDIFileName(sPath, "837_");
                        oEdiDoc.Save(sEdiFile);
                        System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile);
                        string strData;
                        strData = oReader.ReadToEnd();
                        oReader.Close();
                        oReader.Dispose();
                        oReader = null;
                        System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile);
                        oWriter.Write(strData);
                        oWriter.Close();
                        oWriter.Dispose();
                        oWriter = null;
                        _result = sEdiFile;
                        //MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}


                        #endregion " Save EDI File "

                        #region " Update Claim Manager Table "
                        Int64 _date = 0;
                        Int64 _time = 0;
                        _date = gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToString());
                        _time = gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToString());
                        gloClaimManager ogloClaimManager = new gloClaimManager(_databaseconnectionstring, "");
                        Int64 _id = ogloClaimManager.InsertUpdateClaimManager(0, _BatchID, oTransaction.TransactionID, oTransaction.ClaimNo, oTransaction.PatientID, Convert.ToInt64(dtClearingHouse.Rows[0]["nClearingHouseID"]), InterchangeHeader, TransactionSetHeader, FunctionalGroupHeader, _date, _time, _UserID, gloPatient.TypeOfBilling.Electronic.GetHashCode(), this.ClinicID);
                        ogloClaimManager.Dispose();
                        #endregion

                        //DESTROYS OBJECTS
                        oSegment.Dispose();
                        oTransactionset.Dispose();
                        oGroup.Dispose();
                        oInterchange.Dispose();

                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _result = "";
            }
            finally
            {
                if (dtClearingHouse != null) { dtClearingHouse.Dispose(); }
                if (dtSubmitter != null) { dtSubmitter.Dispose(); }
              //  if (dtReceiver != null) { dtReceiver.Dispose(); }
             //   if (dtBillingProvider != null) { dtBillingProvider.Dispose(); }
             //   if (dtRenderingProvider != null) { dtRenderingProvider.Dispose(); }
                if (dtFacility != null) { dtFacility.Dispose(); }
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
                if (dtReferral != null) { dtReferral.Dispose(); }
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
                //if (oEdiDoc != null)
                //{
                //    oEdiDoc.Dispose();
                //    oEdiDoc = null;
                //}
            }
            #endregion " Generate EDI "

            // }//Validation IF loop
            return _result;
        }

        public string Generate276EDI(ArrayList SelectedTransactions)
        {
            DataTable dtClearingHouse = null;
            DataTable dtSubmitter = null;
            DataTable dtPatientInsurances = null;

            Transaction oTransaction = null;
            gloBilling ogloBilling = new gloBilling(_databaseconnectionstring, "");

            int nHlCounter = 0;
            int nHlInfoReceiverParent;
            int nHlServiceProviderParent;
            int nHlSubscriberParent;
            int nHlDependentParent;
            string _result = "";
            try
            {
                //Get Clearing House Information in Datatable
                //dtClearingHouse = new DataTable();
                dtClearingHouse = ogloBilling.GetClearingHouseSettings();

                //This New method clears the oEdiDoc object except the schema loaded
                oEdiDoc.New();
                //Set the properties for oEdiDoc object
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.set_Property(DocumentPropertyIDConstants.Property_DocumentBufferIO, 2000);
                oEdiDoc.SegmentTerminator = "~\r\n";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = ":";


                #region Interchange Segment

                //Create the interchange segment
                ediInterchange.Set(ref oInterchange, (ediInterchange)oEdiDoc.CreateInterchange("X", "004010"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oInterchange.GetDataSegmentHeader());

                oSegment.set_DataElementValue(1, 0, "00");
                oSegment.set_DataElementValue(3, 0, "00");
                oSegment.set_DataElementValue(5, 0, "12");
                oSegment.set_DataElementValue(6, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]));//txtSenderID1.Text.Trim());// "Sender");
                oSegment.set_DataElementValue(7, 0, "12");
                oSegment.set_DataElementValue(8, 0, Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]));//txtReceiverID1.Text.Trim());//"ReceiverID");
                string ISA_Date = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString()));
                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2));//txtEnquiryDate.Text.Trim());//"010821");
                string ISA_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(10, 0, FormattedTime(ISA_Time).Trim());//txtEnquiryTime.Text.Trim());//"1548");
                oSegment.set_DataElementValue(11, 0, "U");
                oSegment.set_DataElementValue(12, 0, "00401");
                oSegment.set_DataElementValue(13, 0, "00020");//txtControlNo.Text.Trim());//"000000020");
                oSegment.set_DataElementValue(14, 0, "0");
                oSegment.set_DataElementValue(15, 0, "T");
                oSegment.set_DataElementValue(16, 0, ":");

                #endregion Interchange Segment

                #region Functional Group

                //Create the functional group segment
                ediGroup.Set(ref oGroup, (ediGroup)oInterchange.CreateGroup("004010X093A1"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oGroup.GetDataSegmentHeader());
                oSegment.set_DataElementValue(1, 0, "HR");//txtFunctionID.Text.Trim());
                oSegment.set_DataElementValue(2, 0, Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim());//txtSenderDept.Text.Trim());//"SenderDept");
                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim());//txtReceiverDept.Text.Trim());//"ReceiverDept");
                string GS_Time = Convert.ToString(gloDateMaster.gloTime.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())));//txtFunGroupDate.Text.Trim());//"20010821");
                oSegment.set_DataElementValue(5, 0, FormattedTime(GS_Time).Trim());//txtFuncGroupTime.Text.Trim());//"1548");
                oSegment.set_DataElementValue(6, 0, "11000");//txtControlNo.Text.Trim());//"000001");
                oSegment.set_DataElementValue(7, 0, "X");
                oSegment.set_DataElementValue(8, 0, "004010X093A1");

                #endregion Functional Group

                #region Transaction Set
                //HEADER
                //ST TRANSACTION SET HEADER
                ediTransactionSet.Set(ref oTransactionset, (ediTransactionSet)oGroup.CreateTransactionSet("276"));
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.GetDataSegmentHeader());
                oSegment.set_DataElementValue(2, 0, "000020");
                #endregion Transaction Set

                #region BHT Segment

                //Begining of Herarchical Transaction Segment 
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("BHT"));
                oSegment.set_DataElementValue(1, 0, "0010");
                oSegment.set_DataElementValue(2, 0, "13");//Code 13=Request
                oSegment.set_DataElementValue(4, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(DateTime.Now.ToShortDateString())).Trim());//txtBHTDate.Text.Trim());//"19990501");//Date

                #endregion BHT Segment

                nHlCounter = 0;

                if (SelectedTransactions != null)
                {
                    if (SelectedTransactions.Count > 0)
                    {
                        for (int i = 0; i < SelectedTransactions.Count; i++)
                        {
                           // oTransaction = new Transaction();
                            //TransactionLine oTransLine = null;
                            oTransaction = ogloBilling.GetTransactionDetails(Convert.ToInt64(SelectedTransactions[i]), _ClinicID);
                            if (oTransaction != null)
                            {
                                if (oTransaction.Lines.Count > 0)
                                {
                                    //Get Submitter Information in Datatable
                                    //dtSubmitter = new DataTable();
                                    if (dtSubmitter != null) { dtSubmitter.Dispose(); }
                                    dtSubmitter = ogloBilling.GetSubmitterInfo(Convert.ToInt64(_ClinicID), oTransaction.ProviderID);

                                    //FillAllDetails(oTransaction);
                                   
                                    Provider _Provider = null;
                                    gloPatient.Patient oPatient = null;
                                    if (Convert.ToInt64(oTransaction.ProviderID) != 0 && oTransaction.ProviderID.ToString() != "")
                                    {
                                        Resource oResource = new Resource(_databaseconnectionstring);
                                        _Provider = oResource.GetProviderDetail(Convert.ToInt64(oTransaction.ProviderID));
                                        gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                                        dtPatientInsurances = ogloPatient.getPatientInsurances(oTransaction.PatientID);
                                        oPatient = ogloPatient.GetPatient(oTransaction.PatientID);
                                        ogloPatient.Dispose();
                                        ogloPatient = null;
                                        oResource.Dispose();
                                        oResource = null;
                                    }
                                    for (int nTransactionSet = 1; nTransactionSet <= 1; nTransactionSet++)
                                    {

                                        #region Calculate Claim Amount

                                        string _ClaimTotal = "";

                                        decimal _claimAmount = 0;
                                        for (int nLine = 0; nLine <= oTransaction.Lines.Count - 1; nLine++)
                                        {
                                            _claimAmount = _claimAmount + oTransaction.Lines[nLine].Total;
                                        }

                                        _ClaimTotal = _claimAmount.ToString("#0.00");

                                        if (_ClaimTotal.Substring(_ClaimTotal.Length - 2, 2) == "00")
                                        {
                                            _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 3);
                                        }
                                        else if (_ClaimTotal.Substring(_ClaimTotal.Length - 1, 1) == "0")
                                        {
                                            _ClaimTotal = _ClaimTotal.Substring(0, _ClaimTotal.Length - 1);
                                        }
                                        #endregion " Calculate Claim Amount "

                                        if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                                        {
                                            for (int _Insrow = 0; _Insrow < dtPatientInsurances.Rows.Count; _Insrow++)
                                            {

                                                #region HL Loop
                                                //'*************************************************************************************************
                                                //'DETAIL INFORMATION SOURCE LEVEL
                                                //Do While nInfoSourceCounter <= nInfoSources

                                                nHlCounter = nHlCounter + 1;
                                                nHlInfoReceiverParent = nHlCounter;
                                                //'HL - HIERARCHICAL LEVEL
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                                oSegment.set_DataElementValue(3, 0, "20");//txtInfoSourceLevel.Text.Trim());//"20");      //Hierarchical Level Code
                                                oSegment.set_DataElementValue(4, 0, "1");//txtInfoSourceLevel.Text.Trim());//"1");     //Hierarchical Child Code

                                                #endregion HL Loop

                                                #region Payer Loop

                                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                string _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim();
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Length > 35)
                                                {
                                                    _ModifiedPayerName = "";
                                                    _ModifiedPayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]).Trim().Substring(0, 34);
                                                }
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "PR");  //Entity Identifier Code - PAYER
                                                oSegment.set_DataElementValue(2, 0, "2");    //Entity Type Qualifier
                                                oSegment.set_DataElementValue(3, 0, _ModifiedPayerName.Trim());//"Payer Name");     //Name Last or Organization Name
                                                oSegment.set_DataElementValue(8, 0, "PI");    //Identification Code Qualifier
                                                oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]).Trim());//"12345");     //Identification Code

                                                #endregion Payer Loop

                                                #region HL Loop
                                                //*************************************************************************************************
                                                //DETAIL INFORMATION RECEIVER LEVEL
                                                //Do While nInfoReceiverCounter <= nInfoReceivers

                                                nHlCounter = nHlCounter + 1;
                                                nHlServiceProviderParent = nHlCounter;
                                                //HL - HIERARCHICAL LEVEL
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                                oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString());      //Hierarchical Parent ID Number
                                                oSegment.set_DataElementValue(3, 0, "21");    //Hierarchical Level Code
                                                oSegment.set_DataElementValue(4, 0, "1");    //Hierarchical Child Code
                                                #endregion HL Loop

                                                #region Submitter
                                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "41");     //Entity Identifier Code - SUBMITTER
                                                oSegment.set_DataElementValue(2, 0, "2");   //Entity Type Qualifier
                                                oSegment.set_DataElementValue(3, 0, Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim().Trim());//"Receiver Name");     //Name Last or Organization Name
                                                oSegment.set_DataElementValue(8, 0, "46");     //Identification Code Qualifier
                                                oSegment.set_DataElementValue(9, 0, "C0923");//_SubmitterETIN.Trim());//"X67E");    //Identification Code
                                                #endregion Submitter

                                                #region HL Loop
                                                //*************************************************************************************************
                                                //DETAIL SERVICE PROVIDER LEVEL
                                                //Do While nServiceProviderCounter <= nServiceProviders

                                                nHlCounter = nHlCounter + 1;
                                                nHlSubscriberParent = nHlCounter;
                                                //HL - HIERARCHICAL LEVEL
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                                oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString());     //Hierarchical Parent ID Number
                                                oSegment.set_DataElementValue(3, 0, "19");     //Hierarchical Level Code
                                                oSegment.set_DataElementValue(4, 0, "1");   //Hierarchical Child Code
                                                #endregion HL Loop

                                                #region Provider/Receiver Loop
                                                //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                oSegment.set_DataElementValue(1, 0, "1P");     //Entity Identifier Code - PROVIDER
                                                oSegment.set_DataElementValue(2, 0, "1");    //Entity Type Qualifier
                                                oSegment.set_DataElementValue(3, 0, _Provider.FirstName.Trim() + " " + _Provider.MiddleName.Trim() + " " + _Provider.LastName.Trim());     //Name Last or Organization Name
                                                oSegment.set_DataElementValue(8, 0, "XX");   //Identification Code Qualifier
                                                oSegment.set_DataElementValue(9, 0, _Provider.NPI.Trim());//"987666");    //Identification Code
                                                #endregion Provider/Receiver Loop

                                                #region HL Loop
                                                //*************************************************************************************************
                                                //DETAIL SUBSCRIBER LEVEL
                                                //Do While nSubscriberCounter <= nSubscribers

                                                nHlCounter = nHlCounter + 1;
                                                nHlDependentParent = nHlCounter;

                                                //nDependents = Val(txtNoDependents.Lines(nSubscriberCounter - 1))
                                                //HL - HIERARCHICAL LEVEL
                                                ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                                oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString());   //Hierarchical Parent ID Number
                                                oSegment.set_DataElementValue(3, 0, "22");      //Hierarchical Level Code
                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                                    oSegment.set_DataElementValue(4, 0, "0");     //Hierarchical Child Code
                                                else
                                                    oSegment.set_DataElementValue(4, 0, "1");//Hierarchical Child Code
                                                #endregion HL Loop

                                                if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                                {
                                                    #region Subscriber Demographics

                                                    //DMG - DEMOGRAPHIC INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");    //Date Time Period Format Qualifier
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]).Trim() != "")
                                                        oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]))));//"19201210");     //Date Time Period
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim() != "")
                                                    {
                                                        if (Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().ToUpper() == "OTHER")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]).Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                                        }
                                                    }
                                                    #endregion Subscriber Demographics

                                                    #region Subscriber
                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                                        oSegment.set_DataElementValue(1, 0, "QC");    //Entity Identifier Code
                                                    else
                                                        oSegment.set_DataElementValue(1, 0, "IL");      //Entity Identifier Code

                                                    oSegment.set_DataElementValue(2, 0, "1");     //Entity Type Qualifier
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim());//"Subscriber Last Name");     //Name Last or Organization Name
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim());//"Subscriber First Name");      //Name First
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim());
                                                    }
                                                    oSegment.set_DataElementValue(8, 0, "MI");     //Identification Code Qualifier
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim());//"123456789A");       //Identification Code
                                                    #endregion Subscriber

                                                    #region Trace Number
                                                    //TRN - TRACE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
                                                    oSegment.set_DataElementValue(1, 0, "1");    //Trace Type Code
                                                    oSegment.set_DataElementValue(2, 0, FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo)));//"1625032606");     //Reference Identification
                                                    #endregion Trace Number

                                                    #region Reference ID
                                                    //REF - REFERENCE IDENTIFICATION
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
                                                    //oSegment.set_DataElementValue(1, 0, "BLT");    //Reference Identification Qualifier
                                                    //oSegment.set_DataElementValue(2, 0, "111");//"111");     //Reference Identification
                                                    #endregion Reference ID

                                                    #region AMT Loop
                                                    //AMT - MONETARY AMOUNT
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
                                                    oSegment.set_DataElementValue(1, 0, "T3");     //Amount Qualifier Code
                                                    oSegment.set_DataElementValue(2, 0, _ClaimTotal);//"8513.88");      //Monetary Amount
                                                    #endregion AMT Loop

                                                    #region Service Date Loop
                                                    ////DTP - DATE OR TIME OR PERIOD
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
                                                    //oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
                                                    //oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[0].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[oTransaction.Lines.Count-1].DateServiceTill.ToShortDateString()));//"19960831-19960906");    //Date Time Period

                                                    #endregion Service Date Loop

                                                    for (int j = 0; j < oTransaction.Lines.Count; j++)
                                                    {
                                                        #region Service Information Loop
                                                        ////SVC Service Information
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
                                                        oSegment.set_DataElementValue(1, 1, "HC");     //Product/Service ID Qualifier
                                                        oSegment.set_DataElementValue(1, 2, oTransaction.Lines[j].CPTCode.Trim());     //Product/Service ID
                                                        string _charges = "";
                                                        if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 2, 2) == "00")
                                                        {
                                                            _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 3);
                                                        }
                                                        oSegment.set_DataElementValue(2, 0, _charges);     //Monetary Amount
                                                        #endregion Service Information Loop

                                                        #region REF Loop
                                                        ////REF - REFERENCE IDENTIFICATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, "FJ");     //Reference Identification Qualifier
                                                        oSegment.set_DataElementValue(2, 0, "02");      //Reference Identification
                                                        #endregion REF Loop

                                                        #region Service Line Date
                                                        ////DTP - DATE OR TIME OR PERIOD
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
                                                        oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
                                                        oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960931-19961030");     //Date Time Period

                                                        #endregion Service Line Date
                                                    }
                                                }
                                                else
                                                {
                                                    #region " DETAIL DEPENDENT LEVEL "

                                                    #region Subscriber
                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]).Trim() == "18")
                                                        oSegment.set_DataElementValue(1, 0, "QC");    //Entity Identifier Code
                                                    else
                                                        oSegment.set_DataElementValue(1, 0, "IL");      //Entity Identifier Code

                                                    oSegment.set_DataElementValue(2, 0, "1");     //Entity Type Qualifier
                                                    oSegment.set_DataElementValue(3, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]).Trim());//"Subscriber Last Name");     //Name Last or Organization Name
                                                    oSegment.set_DataElementValue(4, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]).Trim());//"Subscriber First Name");      //Name First
                                                    if (Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim() != "")
                                                    {
                                                        oSegment.set_DataElementValue(5, 0, Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]).Trim());
                                                    }
                                                    oSegment.set_DataElementValue(8, 0, "MI");     //Identification Code Qualifier
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]).Trim());//"123456789A");       //Identification Code
                                                    #endregion Subscriber

                                                    #region HL Loop

                                                    ////DETAIL DEPENDENT LEVEL
                                                    //Do While nDependentCounter <= nDependents
                                                    nHlCounter = nHlCounter + 1;

                                                    //HL - HIERARCHICAL LEVEL
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\HL"));
                                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString());     //Hierarchical ID Number
                                                    oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString());     //Hierarchical Parent ID Number
                                                    oSegment.set_DataElementValue(3, 0, "23");    //Hierarchical Level Code

                                                    #endregion HL Loop

                                                    #region Patient Demographics

                                                    //DMG - DEMOGRAPHIC INFORMATION
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\DMG"));
                                                    oSegment.set_DataElementValue(1, 0, "D8");     //Date Time Period Format Qualifier
                                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(gloDateMaster.gloDate.DateAsNumber(Convert.ToString(oPatient.DemographicsDetail.PatientDOB))).Trim());     //Date Time Period
                                                    if (Convert.ToString(oPatient.DemographicsDetail.PatientGender).Trim() != "")
                                                    {
                                                        if (Convert.ToString(oPatient.DemographicsDetail.PatientGender).Trim().ToUpper() == "OTHER")
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, "U");//"SubscriberGender"
                                                        }
                                                        else
                                                        {
                                                            oSegment.set_DataElementValue(3, 0, Convert.ToString(oPatient.DemographicsDetail.PatientGender).Trim().Substring(0, 1).ToUpper());//"SubscriberGender"
                                                        }
                                                    }
                                                    #endregion Patient Demographics

                                                    #region Patient Info
                                                    //NM1 - INDIVIDUAL OR ORGANIZATIONAL NAME\
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\NM1\\NM1"));
                                                    oSegment.set_DataElementValue(1, 0, "QC");                        //Entity Identifier Code
                                                    oSegment.set_DataElementValue(2, 0, "1");                         //Entity Type Qualifier
                                                    oSegment.set_DataElementValue(3, 0, oPatient.DemographicsDetail.PatientLastName.Trim());     //Name Last or Organization Name
                                                    oSegment.set_DataElementValue(4, 0, oPatient.DemographicsDetail.PatientFirstName.Trim());    //Name First
                                                    if (oPatient.DemographicsDetail.PatientMiddleName.Trim() != "")
                                                        oSegment.set_DataElementValue(4, 0, oPatient.DemographicsDetail.PatientMiddleName.Trim());    //Name First
                                                    oSegment.set_DataElementValue(8, 0, "MI");                        //Identification Code Qualifier
                                                    oSegment.set_DataElementValue(9, 0, oPatient.DemographicsDetail.PatientSSN.Trim());            //"9876453B");      //Identification Code
                                                    #endregion Patient Info

                                                    #region Trace Loop
                                                    //TRN - TRACE
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\TRN"));
                                                    oSegment.set_DataElementValue(1, 0, "1");               //Trace Type Code
                                                    oSegment.set_DataElementValue(2, 0, FormattedClaimNumberGeneration(Convert.ToString(oTransaction.ClaimNo).Trim()));//"1347897353");      //Reference Identification
                                                    #endregion Trace Loop

                                                    #region REF Loop
                                                    ////REF - REFERENCE IDENTIFICATION
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\REF"));
                                                    //oSegment.set_DataElementValue(1, 0, "BLT");      //Reference Identification Qualifier
                                                    //oSegment.set_DataElementValue(2, 0, "111");      //Reference Identification
                                                    #endregion REF Loop

                                                    #region AMT Loop
                                                    //AMT - MONETARY AMOUNT
                                                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\AMT"));
                                                    oSegment.set_DataElementValue(1, 0, "T3");       //Amount Qualifier Code
                                                    oSegment.set_DataElementValue(2, 0, _ClaimTotal);      //Monetary Amount
                                                    #endregion AMT Loop

                                                    #region Date Time Loop
                                                    //DTP - DATE OR TIME OR PERIOD
                                                    //ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\DTP"));
                                                    //oSegment.set_DataElementValue(1, 0, "232");    //Date/Time Qualifier
                                                    //oSegment.set_DataElementValue(2, 0, "RD8");  //Date Time Period Format Qualifier
                                                    //oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[0].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[oTransaction.Lines.Count - 1].DateServiceTill.ToShortDateString()));//"19960831-19960906");    //Date Time Period

                                                    #endregion Date Time Loop

                                                    for (int j = 0; j < oTransaction.Lines.Count; j++)
                                                    {
                                                        #region SVC Loop
                                                        //SVC Service Information
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\SVC"));
                                                        oSegment.set_DataElementValue(1, 1, "HC");     //Product/Service ID Qualifier
                                                        oSegment.set_DataElementValue(1, 2, oTransaction.Lines[j].CPTCode.Trim());     //Product/Service ID
                                                        string _charges = "";
                                                        if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 2, 2) == "00")
                                                        {
                                                            _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 3);
                                                        }
                                                        else if (Convert.ToString(oTransaction.Lines[j].Total).Substring(Convert.ToString(oTransaction.Lines[j].Total).Length - 1, 1) == "0")
                                                        {
                                                            _charges = Convert.ToString(oTransaction.Lines[j].Total).Substring(0, Convert.ToString(oTransaction.Lines[j].Total).Length - 1);
                                                        }

                                                        oSegment.set_DataElementValue(2, 0, _charges);     //Monetary Amount

                                                        #endregion SVC Loop

                                                        #region REF Loop
                                                        //REF - REFERENCE IDENTIFICATION
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\REF"));
                                                        oSegment.set_DataElementValue(1, 0, "FJ");      //Reference Identification Qualifier
                                                        oSegment.set_DataElementValue(2, 0, "78");      //Reference Identification
                                                        #endregion REF Loop

                                                        #region Date Time Loop for Service Line

                                                        //DTP - DATE OR TIME OR PERIOD
                                                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oTransactionset.CreateDataSegment("HL\\TRN\\SVC\\DTP"));
                                                        oSegment.set_DataElementValue(1, 0, "472");     //Date/Time Qualifier
                                                        oSegment.set_DataElementValue(2, 0, "RD8");      //Date Time Period Format Qualifier
                                                        oSegment.set_DataElementValue(3, 0, gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceFrom.ToShortDateString()) + "-" + gloDateMaster.gloDate.DateAsNumber(oTransaction.Lines[j].DateServiceTill.ToShortDateString()));//"19960931-19961030");     //Date Time Period
                                                        // }
                                                        #endregion Date Time Loop for Service Line
                                                    }
                                                    #endregion " DETAIL DEPENDENT LEVEL "
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (dtSubmitter != null) 
                            { 
                                dtSubmitter.Dispose();
                                dtSubmitter = null;
                            }
                            if (dtPatientInsurances != null)
                            {
                                dtPatientInsurances.Dispose();
                                dtPatientInsurances = null;
                            }
                        }

                        #region Save EDI File

                        //oEdiDoc.Save(sPath + sEdiFile);
                        //SaveFileDialog oSave = new SaveFileDialog();
                        //oSave.Filter = "TEXT Files (*.txt)|*.txt|EDI Files (*.edi)|*.edi";
                        //if (oSave.ShowDialog() == DialogResult.OK)
                        //{
                        //    System.IO.StreamReader oReader = new System.IO.StreamReader(sPath + sEdiFile);
                        //    string strData;
                        //    strData = oReader.ReadToEnd();
                        //    oReader.Close();

                        //    System.IO.StreamWriter oWriter = new System.IO.StreamWriter(oSave.FileName);
                        //    oWriter.Write(strData);
                        //    oWriter.Close();
                        //    //MessageBox.Show("File Created Successfully", "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}

                        sEdiFile1 = GetEDIFileName(sPath, "276_");
                        oEdiDoc.Save(sEdiFile1);
                        System.IO.StreamReader oReader = new System.IO.StreamReader(sEdiFile1);
                        string strData;
                        strData = oReader.ReadToEnd();
                        oReader.Close();
                        oReader.Dispose();
                        oReader = null;
                        System.IO.StreamWriter oWriter = new System.IO.StreamWriter(sEdiFile1);
                        oWriter.Write(strData);
                        oWriter.Close();
                        oWriter.Dispose();
                        oWriter = null;

                        _result = sEdiFile1;
                        #endregion Save EDI

                        //DESTROY OBJECTS
                        oSegment.Dispose();
                        oTransactionset.Dispose();
                        oGroup.Dispose();
                        oInterchange.Dispose();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                _result = "";
            }
            finally
            {
                if (dtClearingHouse != null) { dtClearingHouse.Dispose(); }
                if (dtSubmitter != null) { dtSubmitter.Dispose(); }
                if (dtPatientInsurances != null) { dtPatientInsurances.Dispose(); }
                if (ogloBilling != null)
                {
                    ogloBilling.Dispose();
                    ogloBilling = null;
                }
            }
            return _result;
        }

        public void TranslateEDI277ClaimStatus()
        {
            try
            {
                string sSegmentID = "";
                string sLoopSection = "";
               // string sLXID = "";
                //string sPath = "";
                //string sEntity = "";
                //string Qlfr = "";

                //string strRejectionCode = "";
                //string strFollowupCode = "";

                int nArea;
                string sLoopQlfr = "";
                string sValue = "";

                // Gets the first data segment in the EDI files
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc.FirstDataSegment);  //oSegment = (ediDataSegment) oEdiDoc.FirstDataSegment

                while (oSegment != null)
                {    //DATA SEGMENTS WILL BE IDENTIFIED BY THEIR ID, THE LOOP SECTION AND AREA
                    //(OR TABLE) NUMBER THAT THEY ARE IN.
                    sSegmentID = oSegment.ID;
                    sLoopSection = oSegment.LoopSection;
                    nArea = oSegment.Area;

                    if (nArea == 0)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ISA")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Authorization Information Qualifier
                                sValue = oSegment.get_DataElementValue(2, 0);     //Authorization Information
                                sValue = oSegment.get_DataElementValue(3, 0);     //Security Information Qualifier
                                sValue = oSegment.get_DataElementValue(4, 0);     //Security Information
                                sValue = oSegment.get_DataElementValue(5, 0);     //Interchange ID Qualifier
                                sValue = oSegment.get_DataElementValue(6, 0);     //Interchange Sender ID
                                sValue = oSegment.get_DataElementValue(7, 0);     //Interchange ID Qualifier
                                sValue = oSegment.get_DataElementValue(8, 0);     //Interchange Receiver ID
                                sValue = oSegment.get_DataElementValue(9, 0);     //Interchange Date
                                sValue = oSegment.get_DataElementValue(10, 0);     //Interchange Time
                                sValue = oSegment.get_DataElementValue(11, 0);     //Interchange Control Standards Identifier
                                sValue = oSegment.get_DataElementValue(12, 0);     //Interchange Control Version Number
                                sValue = oSegment.get_DataElementValue(13, 0);     //Interchange Control Number
                                sValue = oSegment.get_DataElementValue(14, 0);     //Acknowledgment Requested
                                sValue = oSegment.get_DataElementValue(15, 0);     //Usage Indicator
                                sValue = oSegment.get_DataElementValue(16, 0);     //Component Element Separator
                            }
                            else if (sSegmentID == "GS")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Functional Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Application Sender's Code
                                sValue = oSegment.get_DataElementValue(3, 0);     //Application Receiver's Code
                                sValue = oSegment.get_DataElementValue(4, 0);     //Date
                                sValue = oSegment.get_DataElementValue(5, 0);     //Time
                                sValue = oSegment.get_DataElementValue(6, 0);     //Group Control Number
                                sValue = oSegment.get_DataElementValue(7, 0);     //Responsible Agency Code
                                sValue = oSegment.get_DataElementValue(8, 0);     //Version / Release / Industry Identifier Code
                            }   //sSegmentID
                        }    //sLoopSection
                    }
                    else if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "ST")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Transaction Set Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Transaction Set Control Number
                            }
                            else if (sSegmentID == "BHT")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Hierarchical Structure Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Transaction Set Purpose Code
                                sValue = oSegment.get_DataElementValue(3, 0);     //Reference Identification
                                sValue = oSegment.get_DataElementValue(4, 0);     //Date
                                sValue = oSegment.get_DataElementValue(5, 0);     //Time
                                sValue = oSegment.get_DataElementValue(6, 0);     //Transaction Type Code
                            }   //sSegmentID
                        }    //sLoopSection
                    }
                    else if (nArea == 2)
                    {
                        if (sLoopSection == "HL")
                        {
                            //if loop has more that one instance, then you should check for the qualifier that differentiates the loop instances here e.g.
                            //if (sSegmentID == "HL")
                            //{
                            //    sLoopHLQlfr = oSegment.DataElementValue(3);  //In most cases the loop qualifier is the first element of the first segment in the loop, but not necessarily
                            //}
                            //if (sLoopHLQlfr == "QualifierValue")
                            //{

                            if (sSegmentID == "HL")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Hierarchical ID Number
                                sValue = oSegment.get_DataElementValue(2, 0);     //Hierarchical Parent ID Number
                                sValue = oSegment.get_DataElementValue(3, 0);     //Hierarchical Level Code
                                sValue = oSegment.get_DataElementValue(4, 0);     //Hierarchical Child Code
                            }   //Segment ID
                            else if (sSegmentID == "DMG")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Date Quaifier
                                sValue = oSegment.get_DataElementValue(2, 0);     //Date of Birth
                                sValue = oSegment.get_DataElementValue(3, 0);     //Gender
                            }
                        }
                        else if (sLoopSection == "HL;NM1")
                        {
                            //if loop has more that one instance, then you should check for the qualifier that differentiates the loop instances here e.g.
                            if (sSegmentID == "NM1")
                            {
                                sLoopQlfr = oSegment.get_DataElementValue(1);   //In most cases the loop qualifier is the first element of the first segment in the loop, but not necessarily
                            }
                            if (sLoopQlfr == "PR")
                            {
                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                            else if (sLoopQlfr == "41")
                            {

                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                            else if (sLoopQlfr == "1P")
                            {
                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                            else if (sLoopQlfr == "QC")
                            {
                                //sValue = oSegment.get_DataElementValue(1, 0);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Entity Type Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Name Last or Organization Name
                                sValue = oSegment.get_DataElementValue(4, 0);     //Name First
                                sValue = oSegment.get_DataElementValue(5, 0);     //Name Middle
                                sValue = oSegment.get_DataElementValue(6, 0);     //Name Prefix
                                sValue = oSegment.get_DataElementValue(7, 0);     //Name Suffix
                                sValue = oSegment.get_DataElementValue(8, 0);     //Identification Code Qualifier
                                sValue = oSegment.get_DataElementValue(9, 0);     //Identification Code
                            }
                        }
                        else if (sLoopSection == "HL;TRN")
                        {
                            if (sSegmentID == "TRN")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Trace Type Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Reference Identification
                            }
                            else if (sSegmentID == "STC")
                            {
                                sValue = oSegment.get_DataElementValue(1, 1);     //Industry Code
                                sValue = oSegment.get_DataElementValue(1, 2);     //Industry Code
                                sValue = oSegment.get_DataElementValue(1, 3);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(2, 0);     //Date
                                sValue = oSegment.get_DataElementValue(3, 0);     //Action Code
                                sValue = oSegment.get_DataElementValue(4, 0);     //Monetary Amount
                                sValue = oSegment.get_DataElementValue(5, 0);     //Monetary Amount
                                sValue = oSegment.get_DataElementValue(6, 0);     //Date
                                sValue = oSegment.get_DataElementValue(7, 0);     //Payment Method Code
                                sValue = oSegment.get_DataElementValue(8, 0);     //Date
                                sValue = oSegment.get_DataElementValue(9, 0);     //Check Number
                                sValue = oSegment.get_DataElementValue(10, 1);     //Industry Code
                                sValue = oSegment.get_DataElementValue(10, 2);     //Industry Code
                                sValue = oSegment.get_DataElementValue(10, 3);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(11, 1);     //Industry Code
                                sValue = oSegment.get_DataElementValue(11, 2);     //Industry Code
                                sValue = oSegment.get_DataElementValue(11, 3);     //Entity Identifier Code
                                sValue = oSegment.get_DataElementValue(12, 0);     //Free-Form Message Text
                            }
                            else if (sSegmentID == "DTP")
                            {
                                sValue = oSegment.get_DataElementValue(1, 0);     //Date/Time Qualifier
                                sValue = oSegment.get_DataElementValue(2, 0);     //Date Time Period Format Qualifier
                                sValue = oSegment.get_DataElementValue(3, 0);     //Date Time Period
                            }   //sSegmentID

                        }  //sLoopSection

                    }   //nArea

                    //GETS THE NEXT DATA SEGMENT
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());  //oSegment = (ediDataSegment) oSegment.Next();

                }//while

                // Checks the 997 acknowledgment file just created.
                // The 997 file is an EDI file, so the logic to read the 997 Functional Acknowledgemnt file is similar
                // to translating any other EDI file.

                // Gets the first segment of the 997 acknowledgment file
                ediDataSegment.Set(ref oSegment, (ediDataSegment)oAck.GetFirst997DataSegment());	//oSegment = (ediDataSegment) oAck.GetFirst997DataSegment();

                while (oSegment != null)
                {
                    nArea = oSegment.Area;
                    sLoopSection = oSegment.LoopSection;
                    sSegmentID = oSegment.ID;

                    if (nArea == 1)
                    {
                        if (sLoopSection == "")
                        {
                            if (sSegmentID == "AK9")
                            {
                                if (oSegment.get_DataElementValue(1, 0) == "R")
                                {
                                    //MessageBox.Show("Rejected",_messageBoxCaption,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                }
                            }
                        }	// sLoopSection == ""
                    }	//nArea == 1
                    ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());	//oSegment = (ediDataSegment) oSegment.Next();
                }	//oSegment != null

                //save the acknowledgment
                //string sDirectoryPath = AppDomain.CurrentDomain.BaseDirectory + "997_277\\";
                string sDirectoryPath =appSettings["StartupPath"].ToString()+ "\\" + "997_277\\";
                if (System.IO.Directory.Exists(sDirectoryPath) == false) { System.IO.Directory.CreateDirectory(sDirectoryPath); }
                oAck.Save(sDirectoryPath + "997_270.X12");

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null; 
            }
        }

        public void Load277EDIObject()
        {
            try
            {
                string sSefFile = "";

                ediDocument.Set(ref oEdiDoc, new ediDocument());    // oEdiDoc = new ediDocument();
                sPath = AppDomain.CurrentDomain.BaseDirectory;
                sSefFile = "277_X093A1.SEF";
                sEdiFile = "277.X12";
                // Disabling the internal standard reference library to makes sure that 
                // FREDI uses only the SEF file provided
                ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());    //oSchemas = (ediSchemas) oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;

                // This makes certain that the EDI file must use the same version SEF file, otherwise
                // the process will stop.
                oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);

                // By setting the cursor type to ForwardOnly, FREDI does not load the entire file into memory, which
                // improves performance when processing larger EDI files.
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;

                // If an acknowledgment file has to be generated, an acknowledgment object must be created, and its 
                // property must be enabled before loading the EDI file.
                oAck = (ediAcknowledgment)oEdiDoc.GetAcknowledgment();
                oAck.EnableFunctionalAcknowledgment = true;

                // Set the starting point of the control numbers in the acknowledgment
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartInterchangeControlNum, 1001);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartGroupControlNum, 1);
                oAck.set_Property(AcknowledgmentPropertyIDConstants.PropertyAck_StartTransactionSetControlNum, 1);

                // Error codes that are not automatically mapped to an acknowlegment error number by FREDI can be set by
                // using the MapDataElementLevelError method.
                oAck.MapDataElementLevelError(13209, 5, "", "", "", "");

                oEdiDoc.LoadSchema(sSefFile, 0);
                oEdiDoc.LoadSchema("997_X12-4010.SEF", 0);	//for Ack (997) file

                if (oEdiDoc != null)
                {
                    oEdiDoc.Dispose();
                    oEdiDoc = null;
                }
                // Loads EDI file and the corresponding SEF file
                oEdiDoc = new ediDocument();
                OpenFileDialog oDialog = new OpenFileDialog();
                if (oDialog.ShowDialog(System.Windows.Forms.Form.ActiveForm) == DialogResult.OK)
                {
                    string _FileName = "";
                    _FileName = oDialog.FileName;
                    if (System.IO.File.Exists(_FileName) == true)
                    {
                        sEdiFile = _FileName;
                        oEdiDoc.LoadEdi(sEdiFile);
                    }
                }
                oDialog.Dispose();
                oDialog = null;
                //sEdiFile = "EligibilityResponse.X12";
                //oEdiDoc.LoadEdi(sPath + sEdiFile);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        public string GetServerPath()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Object retVal = new object();
            string _serverPath = "";
            string _sqlQuery = "";
            //bool _isValidPath = false;

            try
            {
                oDB.Connect(false);
                _sqlQuery = "SELECT sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = 'SERVERPATH'";
                retVal = oDB.ExecuteScalar_Query(_sqlQuery);
                if (retVal != null && retVal != DBNull.Value)
                {
                    _serverPath = Convert.ToString(retVal);
                    try
                    {
                        if (System.IO.Directory.Exists(_serverPath) == true)
                        {
                            //_isValidPath = true;
                            return _serverPath;
                        }
                    }
                    catch (Exception ex)
                    {
                        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                        ex = null; 
                        //_isValidPath = false; 
                        return ""; }
                }
                else
                { _serverPath = ""; }
                oDB.Disconnect();
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
                return "";
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, false);
                return "";
            }
            finally
            {
                if (oDB != null) { oDB.Dispose(); oDB = null; }
                if (retVal != null) { retVal = null; }
            }
            return _serverPath;
        }

        public void Load277EDIFile()
        {
            try
            {
                string _ServerPath = GetServerPath();
                string _BaseFolder = "Claim Management";
                string _OutInFolder = "Inbox";
                string _ClaimFolder = "277 Claim Status Response";
                string _claimFolderPath = "";


                _claimFolderPath = _ServerPath + "\\" + _BaseFolder + "\\" + _OutInFolder + "\\" + _ClaimFolder;

                if (System.IO.Directory.Exists(_claimFolderPath) == false)
                {
                    MessageBox.Show("Path for claim status response does not exist.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {

                    string[] _ClainStatusFiles = System.IO.Directory.GetFiles(_claimFolderPath, "*.277");
                    if (_ClainStatusFiles != null && _ClainStatusFiles.Length > 0)
                    {
                        sEdiFile = _ClainStatusFiles[0];
                        if (File.Exists(sEdiFile) == true)
                        {
                            ediDocument.Set(ref oEdiDoc, new ediDocument());
                            oEdiDoc.LoadSchema("277_X093A1.SEF", 0);
                            oEdiDoc.New();
                            ediSchemas.Set(ref oSchemas, (ediSchemas)oEdiDoc.GetSchemas());
                            oSchemas.set_Option(SchemasOptionIDConstants.OptSchemas_VersionRestrict, 1);
                            oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;
                            oEdiDoc.LoadEdi(sEdiFile);
                        }
                        else
                        { MessageBox.Show("277 EDI File does not exist.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                    else
                    { MessageBox.Show("No new claim status response files found.  ", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information); }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }
        }

        public byte[] ConvertFileToBinary(string sFileName)
        {
            if (File.Exists(sFileName) == false)
                return null;

            try
            {
                FileStream oFile = default(FileStream);
                BinaryReader oReader = default(BinaryReader);

                //'Please uncomment the following line of code to read the file, even the file is in use by same or another process
                //oFile = New FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 8, FileOptions.Asynchronous)

                //'To read the file only when it is not in use by any process
                oFile = new FileStream(sFileName, FileMode.Open, FileAccess.Read);

                oReader = new BinaryReader(oFile);
                byte[] bytesRead = oReader.ReadBytes(Convert.ToInt32(oFile.Length));


                oFile.Close();
                oReader.Close();
                oReader.Dispose();
                oReader = null;
                oFile.Dispose();
                oFile = null;
                return bytesRead;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }




        }


    }
    public class EDIConstant
    {
        public class EntityType
        {
            public const string Person = "1";
            public const string NonPerson = "2";
        }
        public class InterchangeIdQualifier
        {
            public const string MutuallyDefined = "ZZ";//Mutually Defined
            public const string HCFACarrierIdNumber = "27";//Carrier Id. No. as assigned by HCFA
            public const string MedicareProviderID = "29";//Medicare Provider & Supplier Id. No. as assigned by HCFA

        }
        public class FuntionalIdentifierCode
        {
            public const string HelthCareClaim = "HC";
        }
        public class ProviderCode
        {
            public const string Billing = "BI";
            public const string PayTo = "PT";
            public const string ReferringProvider = "DN";
            public const string PrimaryCareProvider = "P3";
            public const string RenderingProvider = "82";
            public const string PurchaseServiceProvider = "QB";
            public const string SupervisingPhysician = "DQ";
        }
        public class PayerResponsiblitySeqCode
        {
            public const string Primary = "P";
            public const string Secondary = "S";
            public const string Tertiary = "T";
        }
        public class DateTimeQualifierCode
        {
            public const string CCYYMMDD = "D8";
        }
        public class GenderCode
        {
            public const string Female = "F";
            public const string Male = "M";
            public const string Unknown = "U";
        }
        public class YesNo
        {
            public const string No = "N";
            public const string Yes = "Y";
        }
        public class ProviderAcceptAssignmentCode
        {
            public const string Assigned = "A";
            public const string AssignmentAcceptedonClinicalLabServicesOnly = "B";
            public const string NotAssigned = "C";
            public const string PatientRefusestoAssignBenefits = "P";
        }
        public class DTQualifier
        {
            public const string InitialTreatment = "454";
            public const string OnsetofCurrentSymptomsorIllness = "431";
            public const string OnsetofSimilarSymptomsorIllness = "438";
            public const string Accident = "439";
            public const string PeriodLastDayWorked = "297";
            public const string ReturnToWork = "296";
            public const string Admission = "435";
            public const string Discharge = "096";
            public const string Service = "472";


        }
        public class HCCodeList
        {
            public const string PrincipalDiagnosis = "BK";
            public const string Diagnosis = "BF";
        }
        public class ServiceQualifier
        {
            public const string HCPCS = "HC"; //Health Care Financing Adminstration Common Procedural Coding System
            public const string HIEC = "IV"; //Home Infusion EDI Coalition
            public const string MutuallyDefined = "ZZ";
        }
        public class Unit
        {
            public const string InternationalUnit = "F2";
            public const string Minutes = "MJ";
            public const string Units = "UN";
        }

    }
}
