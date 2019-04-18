
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using gloSettings;

namespace gloPMContacts
{
    public enum ContactType
    {
        Insurance = 1,
        Physician = 2,
        Pharmacy = 3,
        Hospital = 4,
        Miscellaneous = 5,
        MediCare = 6,
        MediCaid = 7,
        Others = 8,
        ColectionAgency = 9
    }

    public enum TypeOfBilling
    {
        None = 0,
        Paper = 2,
        Electronic = 1,
        UB04Electronic=3,
        UB04Paper=4
    }

    public enum TypeOfNotes
    {
        none = 0,
        Billing = 1

    }

    public enum PriorAuthorizationRequired
    {
        No = 0,
        Always = 1,
        UsePlanSetting = 2
    }

    public class ContactAddress
    {
        #region "Constructor & Distructor"

        public ContactAddress()
        {

        }

        //System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        //private string _MessageBoxCaption = "gloPM";
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

        ~ContactAddress()
        {
            Dispose(false);
        }

        #endregion

        #region Property Variables

        #region " ContactAddress Fields"

        private string _AddressLine1 = "";

        private string _AddressLine2 = "";

        private string _City = "";

        private string _State = "";

        private string _Zip = "";

        private string _Phone = "";

        private string _Fax = "";

        private string _Email = "";

        private string _Url = "";


        #endregion


        #region " ContactsAddress Fields Properties"


        public string AddrressLine1
        { get { return _AddressLine1; } set { _AddressLine1 = value; } }

        public string AddrressLine2
        { get { return _AddressLine2; } set { _AddressLine2 = value; } }

        public string City
        { get { return _City; } set { _City = value; } }

        public string State
        { get { return _State; } set { _State = value; } }

        public string ZIP
        { get { return _Zip; } set { _Zip = value; } }

        public string Phone
        { get { return _Phone; } set { _Phone = value; } }

        public string Fax
        { get { return _Fax; } set { _Fax = value; } }

        public string Email
        { get { return _Email; } set { _Email = value; } }

        public string URL
        { get { return _Url; } set { _Url = value; } }



        #endregion

        #endregion

    }

    public class Contact
    {
        #region "Constructor & Distructor"

        public Contact()
        {
            _BusinessMailingAddress = new ContactAddress();
            _CompanyAddress = new ContactAddress();
            _PracticeLocationAddress = new ContactAddress();

            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //_ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _MessageBoxCaption = String.Empty;

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

        ~Contact()
        {
            Dispose(false);
        }

        #endregion

        #region  "Variables"

        private Int64 _ContactID = 0;

        private string _Name = "";

        private string _ContactName = "";

        private string _FirstName = "";

        private string _MiddleName = "";

        private string _LastName = "";

        private string _Gender = "";

        private string _ContactType;

        private ContactAddress _BusinessMailingAddress = null;

        private ContactAddress _PracticeLocationAddress = null;

        private ContactAddress _CompanyAddress = null;

        private string _Mobile = "";

        private string _Pager = "";

        private Int64 _ClinicID = 0;

        private bool _bIsBlocked = false;

        private string _Notes = "";

        #endregion

        #region " Properties "

        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        public string Name
        { get { return _Name; } set { _Name = value; } }

        public string ContactName
        { get { return _ContactName; } set { _ContactName = value; } }

        public string FirstName
        { get { return _FirstName; } set { _FirstName = value; } }

        public string MiddleName
        { get { return _MiddleName; } set { _MiddleName = value; } }

        public string LastName
        { get { return _LastName; } set { _LastName = value; } }

        public string Gender
        { get { return _Gender; } set { _Gender = value; } }

        public string ContactType
        {
            get { return _ContactType; }
            set { _ContactType = value; }
        }

        public ContactAddress BusinessMailingAddress
        { get { return _BusinessMailingAddress; } set { _BusinessMailingAddress = value; } }

        public ContactAddress PracticeLocationAddress
        { get { return _PracticeLocationAddress; } set { _PracticeLocationAddress = value; } }

        public ContactAddress CompanyAddress
        { get { return _CompanyAddress; } set { _CompanyAddress = value; } }

        public string Pager
        { get { return _Pager; } set { _Pager = value; } }

        public string Mobile
        { get { return _Mobile; } set { _Mobile = value; } }

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }

        public bool IsBlocked
        { get { return _bIsBlocked; } set { _bIsBlocked = value; } }

        public string Notes
        { get { return _Notes; } set { _Notes = value; } }

        public String DirectAddress
        { get; set; }

        public String SPI
        { get; set; }

        public String SpecialtyType
        { get; set; }

        public String ClinicName
        { get; set; }


        #endregion

    }

    public class ContactDetail
    {
        #region "Constructor & Distructor"

        public ContactDetail()
        {
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //   _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

            #endregion
        }


        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _MessageBoxCaption = String.Empty;


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

        ~ContactDetail()
        {
            Dispose(false);
        }

        #endregion


        #region " Contacts_Detail Fields"
        //nContactsDetailID, nContactId, sInsuranceCode, sInsuranceDecs, sDescription, sValue, nType
        private Int64 _ClinicID = 0;

        private Int64 _ContactID = 0;

        private Int64 _ContactDetailID = 0;

        //nContactsDetailID, 

        private string _InsuranceCode = "";

        //sInsuranceCode, 

        private string _InsuranceDesc = "";

        //sInsuranceDesc, 

        private string _Description = "";

        //sDescription, 

        private string _Value = "";

        //sValue

        private int _Type = 0;

        //nType

        //nContactsDetailID, nContactId, sInsuranceCode, sInsuranceDecs, sDescription, sValue, nType 
        #endregion



        #region " Contacts_Detail Fields Properties"
        //nContactID, 
        public Int64 ContactID
        { get { return _ContactID; } set { _ContactID = value; } }

        public Int64 ClinicID
        { get { return _ClinicID; } set { _ClinicID = value; } }

        public Int64 ContactDetailID

        { get { return _ContactDetailID; } set { _ContactDetailID = value; } }


        public string InsuranceCode
        { get { return _InsuranceCode; } set { _InsuranceCode = value; } }


        public string InsuranceDesc
        { get { return _InsuranceDesc; } set { _InsuranceDesc = value; } }


        public string Description
        { get { return _Description; } set { _Description = value; } }


        public string Value
        { get { return _Value; } set { _Value = value; } }


        public int Type
        { get { return _Type; } set { _Type = value; } }



        #endregion


    }

    public class ReferringProviderAdditionalQualifierID
    {
        public ReferringProviderAdditionalQualifierID()
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

        ~ReferringProviderAdditionalQualifierID()
        {
            Dispose(false);
        }


        #region " Referring Provider Additional Qualifier ID Properties"

        public Int64 nRefProviderID
        { get; set; }

        public Int64 nQualifierID
        { get; set; }

        public string sValue
        { get; set; } 

        public bool bIsSystem
        { get; set; }

        public Int64 nUserID
        { get; set; }

        public Int64 nClinicID
        { get; set; }

        #endregion


    }
    //Shubhangi 20091104
    public class InsuranceCompanyDetails
    {
        int _nId;
        string _sCode;
        string _sDescription;
        string _nClinicId;


        public int nId
        { get { return _nId; } set { _nId = value; } }

        public string sCode
        { get { return _sCode; } set { _sCode = value; } }

        public string sDescription
        { get { return _sDescription; } set { _sDescription = value; } }

        public string nClinicId
        { get { return _nClinicId; } set { _nClinicId = value; } }
    }
    //End Shubhangi

    public class ContactDetails : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

       // private string _databaseconnectionstring = "";

        public ContactDetails()
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

        ~ContactDetails()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(ContactDetail item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(ContactDetail item)
        {
            bool result = false;
            ContactDetail obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new ContactDetail();
                obj = (ContactDetail)_innerlist[i];
                if (obj.ContactDetailID == item.ContactDetailID)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public ContactDetail this[int index]
        {
            get
            {
                return (ContactDetail)_innerlist[index];
            }
        }

        public bool Contains(ContactDetail item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(ContactDetail item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(ContactDetail[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class ReferringProviderAdditionalQualifierIDs : IDisposable
    {
        protected ArrayList _innerlist;

        #region "Constructor & Distructor"

        // private string _databaseconnectionstring = "";

        public ReferringProviderAdditionalQualifierIDs()
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

        ~ReferringProviderAdditionalQualifierIDs()
        {
            Dispose(false);
        }

        #endregion

        public int Count
        {
            get { return _innerlist.Count; }
        }

        public void Add(ReferringProviderAdditionalQualifierID item)
        {
            _innerlist.Add(item);
        }

        public bool Remove(ReferringProviderAdditionalQualifierID item)
        {
            bool result = false;
            ReferringProviderAdditionalQualifierID obj;

            for (int i = 0; i < _innerlist.Count; i++)
            {
                //store current index being checked
                obj = new ReferringProviderAdditionalQualifierID();
                obj = (ReferringProviderAdditionalQualifierID)_innerlist[i];
                if (obj.nQualifierID == item.nQualifierID)
                {
                    _innerlist.RemoveAt(i);
                    result = true;
                    break;
                }
                obj = null;
            }

            return result;
        }

        public bool RemoveAt(int index)
        {
            bool result = false;
            _innerlist.RemoveAt(index);
            result = true;
            return result;
        }

        public void Clear()
        {
            _innerlist.Clear();
        }

        public ReferringProviderAdditionalQualifierID this[int index]
        {
            get
            {
                return (ReferringProviderAdditionalQualifierID)_innerlist[index];
            }
        }

        public bool Contains(ReferringProviderAdditionalQualifierID item)
        {
            return _innerlist.Contains(item);
        }

        public int IndexOf(ReferringProviderAdditionalQualifierID item)
        {
            return _innerlist.IndexOf(item);
        }

        public void CopyTo(ReferringProviderAdditionalQualifierID[] array, int index)
        {
            _innerlist.CopyTo(array, index);
        }
    }

    public class Physician : Contact
    {
        #region "Constructor & Distructor"

        public Physician()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~Physician()
        {
            Dispose(false);
        }

        #endregion

        #region Property Variables


        private Int64 _SpecialtyID = 0;


        private string _HospitalAffiliation = "";


        private string _ExternalCode = "";


        private string _Degree = "";


        private string _Taxonomy = "";


        private string _TaxonomyDesc = "";


        private string _TaxID = "";


        private string _UPIN = "";


        private string _NPI = "";


        //private string _Mobile = "";


        //private string _Pager = "";
        //'Sandip Darade  Case GLO2010-0004426 
        //add prefix and suffix while registering a provider 
        //degree wille be prefix
        private string _sPrefix= "";
        private string _sSuffix = ""; //Bug #68135: 00000693: Prefix Suffix not showing in Physician modify screen

        #endregion

        #region Properties


        public Int64 SpecialtyID
        { get { return _SpecialtyID; } set { _SpecialtyID = value; } }


        public string HospitalAffiliation
        { get { return _HospitalAffiliation; } set { _HospitalAffiliation = value; } }


        public string ExternalCode
        { get { return _ExternalCode; } set { _ExternalCode = value; } }


        public string Degree
        { get { return _Degree; } set { _Degree = value; } }


        public string Taxonomy
        { get { return _Taxonomy; } set { _Taxonomy = value; } }


        public string TaxonomyDesc
        { get { return _TaxonomyDesc; } set { _TaxonomyDesc = value; } }


        public string TaxID
        { get { return _TaxID; } set { _TaxID = value; } }


        public string UPIN
        { get { return _UPIN; } set { _UPIN = value; } }


        public string NPI
        { get { return _NPI; } set { _NPI = value; } }


        //public string Pager
        //{ get { return _Pager; } set { _Pager = value; } }


        //public string Mobile
        //{ get { return _Mobile; } set { _Mobile = value; } }

        public string Prefix
        { get { return _sPrefix; } set { _sPrefix = value; } }

        //Bug #68135: 00000693: Prefix Suffix not showing in Physician modify screen
        public string Suffix
        { get { return _sSuffix; } set { _sSuffix = value; } }

        public PriorAuthorizationRequired PARequired
        { get; set; }


        #endregion

    }

    public class Insurance : Contact
    {
        #region "Constructor & Distructor"

        public Insurance()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~Insurance()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables"

        private string _sInsuranceTypeCode = "";
        private string _sInsuranceTypeDesc = "";
        private string _sPayerID = "";
        private bool _bAccessAssignment = false;
        private bool _bStatementToPatient = false;
        private bool _bMedigap = false;
        private bool _bReferringIDInBox19 = false;
        private bool _bNameOfacilityinBox33 = false;
        private bool _bDoNotPrintFacility = false;
        private bool _b1stPointer = false;
        private bool _bBox31Blank = false;
        private bool _bShowPayment = false;
        private TypeOfBilling _nTypeOBilling = TypeOfBilling.None;
        private Int64 _nClearingHouse = 0;

        private bool _bIsClaims = false;
        private bool _bIsRemittanceAdvice = false;
        private bool _bIsRealTimeEligibility = false;
        private bool _bIsElectronicCOB = false;
        private bool _bIsRealTimeClaimStatus = false;
        private bool _bIsOTAFAmount = false; // added on 27/04/2010
        private bool _bIsEnrollmentRequired = false;
        private string _sPayerPhone = "";
        private string _sWebsite = "";
        private string _sServicingState = "";
        private string _sComments = "";
        private string _sPayerPhoneExtn = "";

        private string _sBox32 = "";
        private string _sBox32A = "";
        private string _sBox32B = "";

        private string _sBox33 = "";
        private string _sBox33A = "";
        private string _sBox33B = "";

        //Sandip Darade 20091211
        private string _sDoNotPrintFacility = "";


        //Added by Anil on 20090714
        private bool _bNotesInBox19 = false;
        private string _sOfficeID = "";

        private Int64 _nCPTCrosswalkID = 0;
        private bool _bIsInstitutionalBilling = false;

        private string _sEligibiltiContact = "";
        private string _sEligibilityPhone = "";
        private string _sEligibilitywebsite= "";
        private string _sEligibilityNote = "";
        private bool _bIsWorkercomp = false;
        private int _nBox11bSettingID = 0;
        private bool _bIsGroupMandatory = false;

        #endregion

        #region "Properties"

        public int nBox11bSettingID
        { get { return _nBox11bSettingID; } set { _nBox11bSettingID = value; } }

        public string InsuranceTypeCode
        { get { return _sInsuranceTypeCode; } set { _sInsuranceTypeCode = value; } }

        public string InsuranceTypeDesc
        { get { return _sInsuranceTypeDesc; } set { _sInsuranceTypeDesc = value; } }

        public string sPayerID
        { get { return _sPayerID; } set { _sPayerID = value; } }

        public bool bAccessAssignment
        { get { return _bAccessAssignment; } set { _bAccessAssignment = value; } }

        public bool bStatementToPatient
        { get { return _bStatementToPatient; } set { _bStatementToPatient = value; } }

        public bool bMedigap
        { get { return _bMedigap; } set { _bMedigap = value; } }

        public bool bReferringIDInBox19
        { get { return _bReferringIDInBox19; } set { _bReferringIDInBox19 = value; } }

        public bool bNameOfacilityinBox33
        { get { return _bNameOfacilityinBox33; } set { _bNameOfacilityinBox33 = value; } }

        public bool bDoNotPrintFacility
        { get { return _bDoNotPrintFacility; } set { _bDoNotPrintFacility = value; } }

        public bool b1stPointer
        { get { return _b1stPointer; } set { _b1stPointer = value; } }

        public bool bBox31Blank
        { get { return _bBox31Blank; } set { _bBox31Blank = value; } }

        public bool bShowPayment
        { get { return _bShowPayment; } set { _bShowPayment = value; } }

        public TypeOfBilling nTypeOBilling
        { get { return _nTypeOBilling; } set { _nTypeOBilling = value; } }

        public Int64 nClearingHouse
        { get { return _nClearingHouse; } set { _nClearingHouse = value; } }


        public bool bIsClaims
        { get { return _bIsClaims; } set { _bIsClaims = value; } }


        public bool bIsRemittanceAdvice
        { get { return _bIsRemittanceAdvice; } set { _bIsRemittanceAdvice = value; } }


        public bool bIsRealTimeEligibility
        { get { return _bIsRealTimeEligibility; } set { _bIsRealTimeEligibility = value; } }


        public bool bIsElectronicCOB
        { get { return _bIsElectronicCOB; } set { _bIsElectronicCOB = value; } }


        public bool bIsRealTimeClaimStatus
        { get { return _bIsRealTimeClaimStatus; } set { _bIsRealTimeClaimStatus = value; } }

        //Added on 27/04/2010
        public bool bIsOTAFAmount 
        { get { return _bIsOTAFAmount; } set { _bIsOTAFAmount = value; } }

        public bool bIsEnrollmentRequired
        { get { return _bIsEnrollmentRequired; } set { _bIsEnrollmentRequired = value; } }

        public string sPayerPhone
        { get { return _sPayerPhone; } set { _sPayerPhone = value; } }

        public string sWebsite
        { get { return _sWebsite; } set { _sWebsite = value; } }

        public string sServicingState
        { get { return _sServicingState; } set { _sServicingState = value; } }

        public string sComments
        { get { return _sComments; } set { _sComments = value; } }

        public string sPayerPhoneExtn
        { get { return _sPayerPhoneExtn; } set { _sPayerPhoneExtn = value; } }

        //Added by Anil on 20070904
        public bool bNotesInBox19
        { get { return _bNotesInBox19; } set { _bNotesInBox19 = value; } }

        public string OfficeID
        { get { return _sOfficeID; } set { _sOfficeID = value; } }

        public string Box32
        { get { return _sBox32; } set { _sBox32 = value; } }

        public string Box32A
        { get { return _sBox32A; } set { _sBox32A = value; } }

        public string Box32B
        { get { return _sBox32B; } set { _sBox32B = value; } }

        public string Box33
        { get { return _sBox33; } set { _sBox33 = value; } }

        public string Box33A
        { get { return _sBox33A; } set { _sBox33A = value; } }

        public string Box33B
        { get { return _sBox33B; } set { _sBox33B = value; } }
        //Sandip Darade 20091211
        public string sDoNotPrintFacility
        { get { return _sDoNotPrintFacility; } set { _sDoNotPrintFacility = value; } }

        //gloPM5060 MaheshB
        public Int64 CPTCrosswalkID
        { get { return _nCPTCrosswalkID; } set { _nCPTCrosswalkID = value; } }

        public bool PARequired
        { get; set; }

        //gloPM5070 Abhisekh 
        public bool IsCorrectedReplacement
        { get; set; }


        public bool IsInstitutionalBilling
        { get { return _bIsInstitutionalBilling; } set { _bIsInstitutionalBilling = value; } }

        //gloPM5070 Debasish  
        public string InsuranceEligibilityProviderID { get; set; }
        //public bool IsDefaultPriorAuthorizationRequired { get; set; }
        public bool IsIncludeTaxonomyforPaper
        { get; set; }

        public bool IsIncludeTaxonomyforElectronic
        { get; set; }

        public string sQualifier
        { get; set; }

        public string sBillClaimOfficeNo
        { get; set; }

        public string sEDIAltPayerIDType
        { get; set; }

        public string sBox19DefaultNote
        { get; set; }

        public bool IncludeRenderingProvider
        { get; set; }

        public object IncludePriorPatientPayment
        { get; set; }

        public bool IncludeOrderingProvider
        { get; set; }

        public bool IncludeServiceFacility
        { get; set; }

        public bool IncludeSubscriberAddress
        { get; set; }

        public string InsuranceEligibilityProvType { get; set; }
        public string InsuranceEligibilityProvSecID { get; set; }
        public string InsuranceEligibilityProvSecType { get; set; }
        public bool bIDInBox31 { get; set; }
        public bool bIncludePlanName { get; set; }
        public bool bPaperDisplayMailingAddress { get; set; }
        public bool bSwap1a9a1aMCare { get; set; }
        public bool bIncludeRendering_Attending { get; set; }

        public bool bIncludeUB04DischargeHour { get; set; }
        public bool bIncludeUB04AdmissionHour { get; set; }
        public bool bIncludeUB04RevenueCodeTotal { get; set; }
        public bool bIncludeSecondaryPayerAddress { get; set; }
        public bool bDefaultOccuranceDOS { get; set; }


        public bool bPaperRenderingTaxonomy { get; set; }
        public bool bPaperBillingTaxonomy { get; set; }
        public bool bElectronicRenderingTaxonomy     { get; set; }
        public bool bElectronicBillingTaxonomy { get; set; }
        
        public string EligibiltiContact
        { get { return _sEligibiltiContact; } set { _sEligibiltiContact = value; } }
        public string EligibilityPhone
        { get { return _sEligibilityPhone; } set { _sEligibilityPhone = value; } }
        public string Eligibilitywebsite
        { get { return _sEligibilitywebsite; } set { _sEligibilitywebsite = value; } }
        public string EligibilityNote
        { get { return _sEligibilityNote; } set { _sEligibilityNote = value; } }

        //gloPM7002 EPSDT Settings Abhisekh 
        public bool bIsBillEPSDTorFamilyPlanning{ get; set; }
        public bool bIsEDIIncludeSV{ get; set; }
        public bool bIsEDIIncludeCRC{ get; set; }
        public bool bIsPaperIncludeReferralCode{ get; set; }
        public string sPaperClaimEPSDTCode { get; set; }
        public string sPaperClaimEPSDTCodeBox { get; set; }
        public string sPaperClaimFamilyPlanningCode { get; set; }
        public string sPaperClaimFamilyPlanningCodeBox { get; set; }
        public string sBillUnitsAs { get; set; }
        public Int32 nMinutesPerUnits { get; set; }
        public bool bIsSupressRenderEPSDTClaimOnPaperEDI{ get; set; }

        //gloPM7004 EMG As X Setting Added By Shweta
        public bool bEMGAsX { get; set; }
        public bool bShowClaim { get; set; }
        public bool bIsWorkerComp
        { get { return _bIsWorkercomp; } set { _bIsWorkercomp = value; } }
        public bool bIsClaimFrequencyOne { get; set; }
        public bool bIncludeMedicareClaimRef { get; set; }
        //public string FedTaxNoBox5 { get; set; }
        public string operationgProviderBox77 { get; set; }
        //public Int64 UB51BillingProvderOtherID { get; set; }
        public bool bIncludeEdiAltPayerID { get; set; } //Property added for include EDI Alt. Payer ID on secondary claims 06May2014 - Sameer

        public bool bIncludeReferring_supervising { get; set; }
        public bool bIncludeReferring_ordering { get; set; }
        public string sCMSDateFormat { get; set; }
        public bool bReportClinicName { get; set; }
        public string Box77RenderingProvider { get; set; }
        public bool bIncludePatientSSN { get; set; }
        public bool bIncludeMod_in_SVD { get; set; }  // Property added to add modifiers in svd segment 06-23-2017
        public bool bIncludePrimaryDxInBox69 { get; set; }  //Code chages for - CAS-13783-L6P0Q9 - to send the data optionally in box no 69 for UB04.
        public bool bIsGroupMandatory
        {
            get { return _bIsGroupMandatory; }
            set { _bIsGroupMandatory = value; }
        }
        #endregion

    }

    public class Pharmacy : Contact
    {

        #region "Constructor & Distructor"

        public Pharmacy()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~Pharmacy()
        {
            Dispose(false);
        }

        #endregion
        #region "Added Variables For ePharmacy for gloEMR 5.0"

        private string _sNCPDPID = "";
        private string _sServiceLevel = "";
        private DateTime? _dtActiveStartTime = null;
        private DateTime? _dtActiveEndTime = null;
        private string _sPharmacyStatus = "";

        public string NCPDPID
        { get { return _sNCPDPID; } set { _sNCPDPID = value; } }

        public string ServiceLevel
        { get { return _sServiceLevel; } set { _sServiceLevel = value; } }

        public DateTime? ActiveStartTime
        { get { return _dtActiveStartTime; } set { _dtActiveStartTime = value; } }

        public DateTime? ActiveEndTime
        { get { return _dtActiveEndTime; } set { _dtActiveEndTime = value; } }

        public string PharmacyStatus
        { get { return _sPharmacyStatus; } set { _sPharmacyStatus = value; } }



        #endregion

    }

    public class Hospital : Contact
    {

        #region "Constructor & Distructor"

        public Hospital()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~Hospital()
        {
            Dispose(false);
        }

        #endregion

        #region " Private variables "


        private string _sHospitalNPI = "";

        #endregion

        #region " Property Procedures "

        public string HospitalNPI
        { get { return _sHospitalNPI; } set { _sHospitalNPI = value; } }


        #endregion
    }
    //Shubhangi
    public class Other : Contact
    {

        #region "Constructor & Distructor"

        public Other()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~Other()
        {
            Dispose(false);
        }

        #endregion

        #region " Private variables "


        private string _sOtherNPI = "";

        #endregion

        #region " Property Procedures "

        public string OtherNPI
        { get { return _sOtherNPI; } set { _sOtherNPI = value; } }


        #endregion
    }
    //End
    public class gloContacts
    {

        #region "Constructor & Distructor"

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private string _MessageBoxCaption = String.Empty;

        private Int64 _ClinicID = 0;

        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }

        private string _databaseconnectionstring = "";

        public gloContacts(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //Sandip Darade 27 Feb 09
            //clinicId set to 1 to avoid database inconsistancies in gloEMR 5.0
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 1; }
            }
            else
            { _ClinicID = 1; }
            //Sandip Darade  20090428
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _MessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _MessageBoxCaption = "gloPM";
                }
            }
            else
            { _MessageBoxCaption = "gloPM"; }

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

        ~gloContacts()
        {
            Dispose(false);
        }

        #endregion

        #region "Enums"

        public enum MidLevelSettingsType
        {
            AllProvidersAllPlans = 1,
            SpecificProviderAllPlans = 2,
            SpecificPlan = 3,
            SpecificPlanSpecificProvider = 4
        } 

        #endregion

        #region " ADD Methods"

        public Int64 Add(Contact oContact)
        {
            Int64 _result = 0;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {
                object _intresult = 0;
                oDBParameters.Add("@ContactID", oContact.ContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@Name", oContact.Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Contact", oContact.ContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@AddressLine1", oContact.CompanyAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@AddressLine2", oContact.CompanyAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@City", oContact.CompanyAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@State", oContact.CompanyAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ZIP", oContact.CompanyAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Phone", oContact.CompanyAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Fax", oContact.CompanyAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Email", oContact.CompanyAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@URL", oContact.CompanyAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMAddressLine1", oContact.BusinessMailingAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMAddressLine2", oContact.BusinessMailingAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMCity", oContact.BusinessMailingAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMState", oContact.BusinessMailingAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMZIP", oContact.BusinessMailingAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMPhone", oContact.BusinessMailingAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMFax", oContact.BusinessMailingAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMEmail", oContact.BusinessMailingAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@BMURL", oContact.BusinessMailingAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracAddressLine1", oContact.PracticeLocationAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracAddressLine2", oContact.PracticeLocationAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracCity", oContact.PracticeLocationAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracState", oContact.PracticeLocationAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracZIP", oContact.PracticeLocationAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracPhone", oContact.PracticeLocationAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracFax", oContact.PracticeLocationAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracEmail", oContact.PracticeLocationAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@PracURL", oContact.PracticeLocationAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Mobile", oContact.Mobile, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Pager", oContact.Pager, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Notes", oContact.Notes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@FirstName", oContact.FirstName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@MiddleName", oContact.MiddleName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@LastName", oContact.LastName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@Gender", oContact.Gender, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ContactType", oContact.ContactType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@ClinicID", oContact.ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@IsBlocked", oContact.IsBlocked, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);

                oDBParameters.Add("@SPI", oContact.SPI, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@DirectAddress", oContact.DirectAddress, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);

                oDBParameters.Add("@SpecialtyType", oContact.SpecialtyType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
              

                int result = oDB.Execute("CO_INUP_ContactMST", oDBParameters, out  _intresult);

                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                            oContact.ContactID = _result;
                        }
                    }

                    if (Convert.ToString(oContact.ContactType) == "Physician")
                    {
                        if (AddPhysician((Physician)oContact) == true)
                        {
                            return _result;
                        }
                    }
                    if (Convert.ToString(oContact.ContactType) == "Insurance")
                    {
                        AddInsurance((Insurance)oContact);
                    }
                    if (Convert.ToString(oContact.ContactType) == "Pharmacy")
                    {
                        AddPharmacy((Pharmacy)oContact);
                    }
                    if (Convert.ToString(oContact.ContactType) == "Hospital")
                    {
                        AddHospital((Hospital)oContact);
                    }

                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;

        }

        public Int64 AddMidLevelSettings(Int64 nProviderID, Int64 nSettingsID, Int64 nContactID, Int64 nInsuranceID, MidLevelSettingsType eSettingsType,Int64 nUserID,Int64 nClinicID,DateTime dtlastUpdated,DateTime dtCreated)
        {
            Int64 _result = 0;
            object _intresult = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {

                oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nInsuranceID", nInsuranceID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", nProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nSettingsID", nSettingsID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@nBillingSettingType", (int)eSettingsType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                oDBParameters.Add("@dtCreatedDateTime", dtCreated, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@dtModifiedDateTime", dtlastUpdated, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@nClinicID", nClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nUserID", nUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);

                int result = oDB.Execute("BL_INUP_MidLevel_Settings", oDBParameters, out  _intresult);
                              

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;

        }

        public Int64 AddBillingTaxonomy(Int64 nProviderID, string sProviderTaxonomy, Int64 nContactID, Int64 nUserID, Int64 nClinicID, DateTime dtlastUpdated, DateTime dtCreated)
        {
            Int64 _result = 0;
            object _intresult = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);
            try
            {

                oDBParameters.Add("@nID", 0, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", nContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", nProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sProviderTaxonomy", sProviderTaxonomy, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nUserID", nUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@dtCreatedDateTime", dtCreated, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@dtModifiedDateTime", dtlastUpdated, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                oDBParameters.Add("@nClinicID", nClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
              
                int result = oDB.Execute("BL_INUP_Billing_Taxonomy", oDBParameters, out  _intresult);


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;

        }

        private bool AddPhysician(Physician oPhysician)
        {
            Int64 _result = 0;
            String strSql = "";
            Int64 _nContactID = oPhysician.ContactID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                //if (IsExistsPhysician(oPhysician.ContactID) == true)
                //{
                strSql = "DELETE   FROM Contacts_Physician_DTL WHERE nContactID = '" + _nContactID + "'";
                oDB.Execute_Query(strSql);
                //}
                //Bug #68135: 00000693: Prefix Suffix not showing in Physician modify screen
                strSql = "INSERT INTO Contacts_Physician_DTL (nContactID, sTaxonomy, sTaxonomyDesc, sTaxID, sUPIN, sNPI, nSpecialtyID, sHospitalAffiliation, sExternalCode, sDegree,sPrefix,sSuffix ,nClinicID, nIsPARequired,sClinicName)" +
                    "VALUES (" + oPhysician.ContactID + ",'" + oPhysician.Taxonomy + "','" + oPhysician.TaxonomyDesc + "','" + oPhysician.TaxID.Replace("'", "''") + "','" + oPhysician.UPIN.Replace("'", "''") + "','" + oPhysician.NPI.Replace("'", "''") + "','" + oPhysician.SpecialtyID + "'," +
                    "'" + oPhysician.HospitalAffiliation.Replace("'", "''") + "','" + oPhysician.ExternalCode.Replace("'", "''") + "','" + oPhysician.Degree.Replace("'", "''") + "','" + oPhysician.Prefix.Replace("'", "''") + "','" + oPhysician.Suffix.Replace("'", "''") + "'," + _ClinicID + "," + oPhysician.PARequired.GetHashCode() + ",'" + oPhysician.ClinicName + "')";
                _result = oDB.Execute_Query(strSql);
                oDB.Disconnect();
                if (_result > 0)
                    return true;
                else
                    return false;

                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return false;

        }

        private bool AddInsurance(Insurance oInsurance)
        {
            Int64 _result = 0;
            String strSql = "";
            Int64 _nContactID = oInsurance.ContactID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //add new column bIsOTAFAmount on 27/04/2010
            try
            {
                oDB.Connect(false);
                //bIsClaims, bIsRemittanceAdvice, bIsRealTimeEligibility, bIsElectronicCOB, bIsRealTimeClaimStatus, bIsEnrollmentRequired, sPayerPhone, sWebsite, sServicingState, sComments, sPayerPhoneExtn
                //if (IsExistsInsurance(oInsurance.ContactID)==true)
                {
                    strSql = "delete  FROM Contacts_Insurance_DTL where nContactID = '" + _nContactID + "' AND nClinicID = " + _ClinicID + "";
                    oDB.Execute_Query(strSql);
                }
                strSql = "INSERT INTO Contacts_Insurance_DTL (nContactID, sInsuranceTypeCode, sInsuranceTypeDesc, sPayerId," +
                    " bAccessAssignment, bStatementToPatient, bMedigap, bReferringIDInBox19, bNameOfacilityinBox33, bDoNotPrintFacility," +
                    " b1stPointer, bBox31Blank, bShowPayment, nTypeOBilling, nClearingHouse,nClinicID,bIsClaims, bIsRemittanceAdvice, bIsRealTimeEligibility," +
                    " bIsElectronicCOB, bIsRealTimeClaimStatus, bIsEnrollmentRequired, sPayerPhone, sWebsite, sServicingState, sComments, sPayerPhoneExtn,bNotesInBox19," +
                    " sOfficeID,sBox32,sBox32A,sBox32B,sBox33,sBox33A,sBox33B ,sIncludeFacilitieswithPOS11onClaim, bIsOTAFAmount,nCPTMappingID, bIsPARequired,bIsInstitutionalBilling," +
                    " sInsEligibilityProviderID,bIncludeTaxonomyForPaper,bIncludeTaxonomyForElectronic,sTaxonomyQualifier,sClaimOfficeNumber,sEDIAltPayerIDType,  " +
                    " bIncludeRendering,bIncludeServiceFacility,bIncludeSubscriberAddress," +
                    " sInsEligibilityProviderType,sInsEligibilityProvSecID,sInsEligibilityProviSecType,bIDInBox31,bIncludePlanname,bPaperDisplayMailingAddress,bSwap1a9a1aMCare," +
                    " bIncludeRendering_Attending,bDefaultOccuranceDOS,bPaperRenderingTaxonomy,bPaperBillingTaxonomy,bElectronicRenderingTaxonomy,bElectronicBillingTaxonomy,sInsEligibiltyContact," +
                    " sInsEligibiltyPhone,sInsEligibiltyWesite,sInsEligibiltyNote,bBillEPSDTorFamilyPlanning,bEDIIncludeSV,bEDIIncludeCRC,bPaperIncludeReferralCode," +
                    " sPaperClaimEPSDTCode,sPaperClaimEPSDTCodeBox,sPaperClaimFamilyPlanningCode,sPaperClaimFamilyPlanningCodeBox,bSupressRenderEPSDTClaimOnPaperEDI,bEMGAsX,bShowClaimNo,sBillUnitsAs, "+
                    " nMinutesPerUnits,bIsWorkerComp,bIsClaimFrequencyOne,bIncludeMedicareClaimRef,sUBBox77,sBox19DefaultNote,nBox11bSettingID,bIncludeOrderingProvider,bIncludeEdiAltPayerID, "+
                    "bIncludeReferring_supervising,bIncludeReferring_ordering,sCMSDateFormat,bReportClinicName,sUBBox77Rendering,bIncludeUB04DischargeHour, "+
                   "bIncludeUB04AdmissionHour,bIncludePriorPatPayment,blnIncludeUB04RevenuecodeTotal,bIncludeSecondaryPayerAddress,bIncludePatientSSN,bIncludeModInSVD,bIncludePrimaryDxInBox69,bIsGroupMandatory) " +
                    "VALUES (" + oInsurance.ContactID + ",'" + oInsurance.InsuranceTypeCode.Replace("'", "''") + "','" + oInsurance.InsuranceTypeDesc.Replace("'", "''") + "'," +
                "'" + oInsurance.sPayerID.Replace("'", "''") + "','" + oInsurance.bAccessAssignment + "','" + oInsurance.bStatementToPatient + "'," +
                "'" + oInsurance.bMedigap + "','" + oInsurance.bReferringIDInBox19 + "','" + oInsurance.bNameOfacilityinBox33 + "','" + oInsurance.bDoNotPrintFacility + "'," +
                "'" + oInsurance.b1stPointer + "','" + oInsurance.bBox31Blank + "','" + oInsurance.bShowPayment + "','" + oInsurance.nTypeOBilling.GetHashCode() + "','" + oInsurance.nClearingHouse + "', " + _ClinicID + " ," +
                "'" + oInsurance.bIsClaims + "','" + oInsurance.bIsRemittanceAdvice + "', '" + oInsurance.bIsRealTimeEligibility + "' , '" + oInsurance.bIsElectronicCOB + "', " +
                "'" + oInsurance.bIsRealTimeClaimStatus + "', '" + oInsurance.bIsEnrollmentRequired + "', '" + oInsurance.sPayerPhone.Replace("'", "''") + "','" + oInsurance.sWebsite.Replace("'", "''") + "', '" + oInsurance.sServicingState.Replace("'", "''") + "', " +
                "'" + oInsurance.sComments.Replace("'", "''") + "', '" + oInsurance.sPayerPhoneExtn.Replace("'", "''") + "', '" + oInsurance.bNotesInBox19 + "', '" + oInsurance.OfficeID.Replace("'", "''") + "', '" + oInsurance.Box32.Replace("'", "''") + "', " +
                "'" + oInsurance.Box32A.Replace("'", "''") + "', '" + oInsurance.Box32B.Replace("'", "''") + "', '" + oInsurance.Box33.Replace("'", "''") + "', '" + oInsurance.Box33A.Replace("'", "''") + "', '" + oInsurance.Box33B.Replace("'", "''") + "','" + oInsurance.sDoNotPrintFacility + "','" + oInsurance.bIsOTAFAmount + "'," + oInsurance.CPTCrosswalkID +
                ",'" + oInsurance.PARequired + "','" + oInsurance.IsInstitutionalBilling + "','" + oInsurance.InsuranceEligibilityProviderID.Replace("'", "''") + "','" + oInsurance.IsIncludeTaxonomyforPaper + "','" + oInsurance.IsIncludeTaxonomyforElectronic + "','" + oInsurance.sQualifier.Replace("'", "''") + "','" + oInsurance.sBillClaimOfficeNo.Replace("'", "''") + "','" + oInsurance.sEDIAltPayerIDType.Replace("'", "''") +
                "','" + oInsurance.IncludeRenderingProvider + "','" + oInsurance.IncludeServiceFacility + "','" + oInsurance.IncludeSubscriberAddress +
                "','" + oInsurance.InsuranceEligibilityProvType.Replace("'", "''") + "','" + oInsurance.InsuranceEligibilityProvSecID.Replace("'", "''") + "','" + oInsurance.InsuranceEligibilityProvSecType + "','" + oInsurance.bIDInBox31 + "','" + oInsurance.bIncludePlanName + "','" + oInsurance.bPaperDisplayMailingAddress + "','" + oInsurance.bSwap1a9a1aMCare + "', '" + oInsurance.bIncludeRendering_Attending + "', '" + oInsurance.bDefaultOccuranceDOS + "', " +
                "'" + oInsurance.bPaperRenderingTaxonomy + "','" + oInsurance.bPaperBillingTaxonomy + "','" + oInsurance.bElectronicRenderingTaxonomy + "','" + oInsurance.bElectronicBillingTaxonomy + "','" + oInsurance.EligibiltiContact.Replace("'", "''") + "','" + oInsurance.EligibilityPhone.Replace("'", "''") + "','" + oInsurance.Eligibilitywebsite.Replace("'", "''") + "','" + oInsurance.EligibilityNote.Replace("'", "''") + "', " +
                "'" + oInsurance.bIsBillEPSDTorFamilyPlanning + "','" + oInsurance.bIsEDIIncludeSV + "','" + oInsurance.bIsEDIIncludeCRC + "','" + oInsurance.bIsPaperIncludeReferralCode + "','" + oInsurance.sPaperClaimEPSDTCode.Trim().Replace("'", "''") + "','" + oInsurance.sPaperClaimEPSDTCodeBox + "','" + oInsurance.sPaperClaimFamilyPlanningCode.Trim().Replace("'", "''") + "','" + oInsurance.sPaperClaimFamilyPlanningCodeBox + "','" + oInsurance.bIsSupressRenderEPSDTClaimOnPaperEDI + "'," +
                "'" + oInsurance.bEMGAsX + "','" + oInsurance.bShowClaim + "','" + oInsurance.sBillUnitsAs + "','" + oInsurance.nMinutesPerUnits + "','" + oInsurance.bIsWorkerComp + "','" + oInsurance.bIsClaimFrequencyOne + "','" + oInsurance.bIncludeMedicareClaimRef + "','" + oInsurance.operationgProviderBox77.Trim().Replace("'", "''") + "','" + oInsurance.sBox19DefaultNote.Trim().Replace("'", "''") + "'," + oInsurance.nBox11bSettingID + ",'" + oInsurance.IncludeOrderingProvider + "','" + oInsurance.bIncludeEdiAltPayerID +
               "','" + oInsurance.bIncludeReferring_supervising + "','" + oInsurance.bIncludeReferring_ordering + "','" + oInsurance.sCMSDateFormat + "','" + oInsurance.bReportClinicName + "','" + oInsurance.Box77RenderingProvider + "','" + oInsurance.bIncludeUB04DischargeHour + "','" + oInsurance.bIncludeUB04AdmissionHour + "','" + oInsurance.IncludePriorPatientPayment + "','" + oInsurance.bIncludeUB04RevenueCodeTotal + "','" + oInsurance.bIncludeSecondaryPayerAddress + "','" + oInsurance.bIncludePatientSSN + "','" + oInsurance.bIncludeMod_in_SVD + "','" + oInsurance.bIncludePrimaryDxInBox69 + "','" + oInsurance.bIsGroupMandatory + "') ";
                _result = oDB.Execute_Query(strSql);

                if (oInsurance.IncludePriorPatientPayment == null || oInsurance.IncludePriorPatientPayment == DBNull.Value)
                {
                    strSql = "UPDATE Contacts_Insurance_DTL SET bIncludePriorPatPayment=NULL where nContactID = '" + _nContactID + "' AND nClinicID = " + _ClinicID + "";
                    oDB.Execute_Query(strSql);
                }
                if (_result > 0)
                    return true;
                else
                    return false;


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
                }
            }
            return false;
        }

        private bool AddPharmacy(Pharmacy oPharmacy)
        {
            //Int64 _result = 0;
            //String strSql = "";
            ////Int64 _nContactID = oPhysician.ContactID;
            //gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            //try
            //{
            //    oDB.Connect(false);

            //    if (_nContactID != 0)
            //    {
            //        strSql = "delete  FROM Contacts_Physician_DTL where nContactID = '" + _nContactID + "'";
            //        oDB.Execute_Query(strSql);
            //    }
            //    strSql = "INSERT INTO Contacts_Physician_DTL (nContactID, sTaxonomy, sTaxonomyDesc, sTaxID, sUPIN, sNPI, nSpecialtyID, sHospitalAffiliation, sExternalCode, sDegree)" +
            //        "VALUES (" + oPhysician.ContactID + ",'" + oPhysician.Taxonomy + "','" + oPhysician.TaxonomyDesc + "','" + oPhysician.TaxID + "','" + oPhysician.UPIN + "','" + oPhysician.NPI + "','" + oPhysician.SpecialtyID + "'," +
            //        "'" + oPhysician.HospitalAffiliation + "','" + oPhysician.ExternalCode + "','" + oPhysician.Degree + "'";
            //    _result = oDB.Execute_Query(strSql);

            //    if (_result > 0)
            //        return true;
            //    else
            //        return false;


            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);

            //}
            return true;
        }

        private bool AddHospital(Hospital oHospital)
        {
            Int64 _result = 0;
            String strSql = "";
            Int64 _nContactID = oHospital.ContactID;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            //gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                if (_nContactID != 0)
                {
                    strSql = "delete  FROM Contacts_Hospital_DTL where nContactID = '" + _nContactID + "'";
                    oDB.Execute_Query(strSql);
                }
                strSql = "INSERT INTO Contacts_Hospital_DTL (nContactID, sNPI, nClinicID, sUnknown1, sUnknown2, sUnknown3, sUnknown4, sUnknown5)" +
                    "VALUES (" + oHospital.ContactID + ",'" + oHospital.HospitalNPI.Replace("'", "''")  + "','" + _ClinicID + "','','','','','')";
                _result = oDB.Execute_Query(strSql);

                if (_result > 0)
                    return true;
                else
                    return false;

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
                }
            }
            return false;
        }

        public bool AddorModifyHoldInfo(gloPMContacts.PlanHold oPlanHold)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                if (oPlanHold.HoldReason != "")
                {
                    oDBParameters.Clear();
                    oDBParameters.Add("@nContactID", oPlanHold.ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nInsCompanyID", oPlanHold.InsCompanyID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@bIsHold", oPlanHold.IsHold, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@bIsModified", oPlanHold.HoldModified, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Bit);
                    oDBParameters.Add("@sHoldReason", oPlanHold.HoldReason, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@dtHoldDateTime", oPlanHold.HoldDateTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                    oDBParameters.Add("@nHoldUserID", oPlanHold.HoldUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nUnholdUserID", oPlanHold.UnHoldUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@nUnHoldDateTime", oPlanHold.UnHoldDateTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.DateTime);
                    oDBParameters.Add("@MachineID", oDB.GetPrefixTransactionID(0), ParameterDirection.Input, SqlDbType.BigInt);

                    int result = oDB.Execute("BL_INSERT_INSURANCE_PLANHOLD", oDBParameters);

                }
               


            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        #region "Gives Claim Count Under Batch Status."

        //By Debasish Das on 3rd May 2010
        //Gives Claim Count Under Batch Status.
        public Int32 ClaimCountUnderBatch(Int64 _nContactID)
        {
            gloDatabaseLayer.DBLayer oDB = null;
            DataTable dtTrasactionIDs = null;
            string sQuery = "";
            Int16 nCount = 0;
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);

                sQuery = " SELECT COUNT(DISTINCT BL_Transaction_Claim_MST.nTransactionID) AS TransactionCount "
                            + " FROM BL_Transaction_Claim_MST with (nolock) INNER JOIN "
                            + " BL_Transaction_Batch_DTL with (nolock) ON BL_Transaction_Claim_MST.nTransactionID = BL_Transaction_Batch_DTL.nTransactionID "
                            + " WHERE (ISNULL(BL_Transaction_Claim_MST.nContactID, 0) = " + _nContactID + ") AND (ISNULL(BL_Transaction_Claim_MST.nClaimStatus, 0) = 1) AND "
                            + " (ISNULL(BL_Transaction_Claim_MST.bIsVoid, 0) <> 1) AND (BL_Transaction_Claim_MST.nStatus IN (3)) ";

                //dtTrasactionIDs = new DataTable();
                oDB.Retrive_Query(sQuery, out dtTrasactionIDs);

                if (dtTrasactionIDs != null && dtTrasactionIDs.Rows.Count > 0)
                {
                    nCount = Convert.ToInt16(dtTrasactionIDs.Rows[0]["TransactionCount"]);
                }
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
                }

                if (dtTrasactionIDs != null)
                {
                    dtTrasactionIDs.Dispose();
                }
            }
            return nCount;
        } 

        #endregion

        //20100427 MaheshB
        public void RemoveClaimsForHold(Int64 _nContactID)
        {
            //this function will remove claims from Batch and will delete whole batch if single claim is there.
            gloDatabaseLayer.DBLayer oDB = null;
            gloDatabaseLayer.DBParameters oDBParameters = null;
            DataTable dtTrasactionIDs = null;
            //Int64 _BatchClaimCounter = 0;
            string _strBatchClaimsCount = String.Empty;
            Object objCountClaims = null;
            try
            {

                oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                oDB.Connect(false);
                oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nContactID", Convert.ToInt64(_nContactID), ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_REMOVE_CLAIMS_FROM_BATCH", oDBParameters, out dtTrasactionIDs);

                if (dtTrasactionIDs != null && dtTrasactionIDs.Rows.Count > 0)
                {
                    for (int nTransID = 0; nTransID < dtTrasactionIDs.Rows.Count; nTransID++)
                    {
                        _strBatchClaimsCount = "SELECT BL_Transaction_Batch.nBatchID, BL_Transaction_Batch.sBatchName, BL_Transaction_Batch.nClaimCounter " +
                                                    " FROM BL_Transaction_Batch_DTL WITH (NOLOCK) INNER JOIN " +
                                                    " BL_Transaction_Batch WITH (NOLOCK) ON BL_Transaction_Batch_DTL.nBatchID = BL_Transaction_Batch.nBatchID " +
                                                    " where BL_Transaction_Batch_DTL.nTransactionID= " + Convert.ToInt64(dtTrasactionIDs.Rows[nTransID]["nTransactionID"]) + "";
                        DataTable dtBatch =  null;
                        oDB.Retrive_Query(_strBatchClaimsCount, out dtBatch);
                        if (dtBatch != null && dtBatch.Rows.Count > 0)
                        {
                            if (Convert.ToString(dtTrasactionIDs.Rows[nTransID]["nStatus"]).Trim() == "3")
                            {
                                _strBatchClaimsCount = "Delete BL_Transaction_Batch_DTL where nTransactionID=" + Convert.ToInt64(dtTrasactionIDs.Rows[nTransID]["nTransactionID"]) + "  ";
                                _strBatchClaimsCount = _strBatchClaimsCount + " Update BL_Transaction_Claim_MST set nStatus=2 where nTransactionID=" + Convert.ToInt64(dtTrasactionIDs.Rows[nTransID]["nTransactionID"]) + " and nStatus Not IN(2,20,17,16,0)";
                                oDB.Execute_Query(_strBatchClaimsCount);
                            }
                           

                            #region

                            string _strQuery= "SELECT Count(nTransactionID) " +
                                             " FROM BL_Transaction_Batch_DTL WITH (NOLOCK) " +
                                             " where nBatchID=" + Convert.ToInt64(dtBatch.Rows[0]["nBatchID"]) + "";
                            objCountClaims = oDB.ExecuteScalar_Query(_strQuery);

                            #endregion
                            if (objCountClaims != null && Convert.ToInt32(objCountClaims)==0)
                            {
                                _strBatchClaimsCount = "";
                                _strBatchClaimsCount = " Delete BL_Transaction_Batch  where nBatchID=" + Convert.ToInt64(dtBatch.Rows[0]["nBatchID"]) + "  ";
                                oDB.Execute_Query(_strBatchClaimsCount);
                            }
                           

                        }
                        _strBatchClaimsCount = "";
                        objCountClaims = null;

                    }
                }

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
                }
                if (oDBParameters != null)
                {
                    oDBParameters.Dispose();
                }
                if (dtTrasactionIDs != null)
                {
                    dtTrasactionIDs.Dispose();
                }
            }
        }
        
        public Int64 AddDetails(ContactDetails ocdet)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                if (ocdet.Count > 0)
                    oDB.Execute_Query("DELETE FROM Contacts_Detail  WHERE nContactId = " + ocdet[0].ContactID);

                //@ContactsDetailID,@ContactId, @InsuranceCode, @InsuranceDecs, @Description, @Value, @Type
                for (int i = 0; i < ocdet.Count; i++)
                {
                    oDBParameters.Clear();
                    object _intresult = 0;
                    oDBParameters.Add("@ContactID", ocdet[i].ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@ContactsDetailID", ocdet[i].ContactDetailID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                    oDBParameters.Add("@InsuranceCode", ocdet[i].InsuranceCode, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@InsuranceDesc", ocdet[i].InsuranceDesc, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Description", ocdet[i].Description, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Value", ocdet[i].Value, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                    oDBParameters.Add("@Type", ocdet[i].Type, System.Data.ParameterDirection.Input, System.Data.SqlDbType.Int);
                    int result = oDB.Execute("CO_INUP_CONTACTS_DETAIL", oDBParameters, out  _intresult);
                    //

                    if (_intresult != null)
                    {
                        if (_intresult.ToString().Trim() != "")
                        {
                            if (Convert.ToInt64(_intresult) > 0)
                            {
                                _result = Convert.ToInt64(_intresult);
                            }
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }

        public void AddQualifierDetails(ReferringProviderAdditionalQualifierIDs objQualifierDet)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {
                if (objQualifierDet != null && objQualifierDet.Count > 0)
                {
                    oDB.Execute_Query("DELETE FROM ReferringProvider_ID_Qualifiers  WHERE nProviderID = " + objQualifierDet[0].nRefProviderID);
                    for (int i = 0; i < objQualifierDet.Count; i++)
                    {
                        oDBParameters.Clear();
                        oDBParameters.Add("@nProviderID", objQualifierDet[i].nRefProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@nQualifierID", objQualifierDet[i].nQualifierID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                        oDBParameters.Add("@sValue", objQualifierDet[i].sValue, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@bIsSystem", objQualifierDet[i].bIsSystem, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@nUserID", objQualifierDet[i].nUserID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDBParameters.Add("@nClinicID", objQualifierDet[i].nClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                        oDB.Execute("BL_INSERT_RefferingProvider_ID_Qualifiers", oDBParameters);
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                gloAuditTrail.gloAuditTrail.ExceptionLog(DBErr.ToString(), true);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            //return _result;
        }

        public bool IsExistsPhysician(Int64 ContactId)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "SELECT Count(nContactID) FROM Contacts_Physician_DTL WHERE nContactID = '" + ContactId + "'";


                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        public bool IsExistsInsurance(Int64 ContactId)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "SELECT Count(nContactID) FROM Contacts_Insurance _DTL WHERE nContactID = '" + ContactId + "'";


                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        public bool GetEnableWorkercompSetting()
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "SELECT ISNULL(sSettingsValue,0) FROM  dbo.Settings WHERE sSettingsName='EnableWorkersCompBilling'";


                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        return Convert.ToBoolean(_intresult);
                       
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        #region //Added By MaheshB
        //Shubhangi 20091106
        //IsExist function for Insurance company
        public bool IsExistsInsurancecmpny(string _Insurancecmpny, Int64 _nID)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _strsql = "";
                _strsql = "Select count(*) from Contacts_InsuranceCompany_MST where ( sDescription='" + _Insurancecmpny.Replace("'", "''") + "' ) and nID<>'" + _nID + "'";
                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_strsql);
                if (_intresult != null)
                {
                    if (Convert.ToInt64(_intresult) > 0)
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;


        }

        //End Shubhangi

        //abhisekh pandey 10/02/2010
        //IsExist function for Insurance Reporting Category
        public bool IsExistsInsuranceRepCtgry(string _InsuranceRepCtgry, Int64 _nID)
        {
            bool _result = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _strsql = "";
                _strsql = "Select count(*) from Contacts_InsuranceReportingCategory_MST where ( sDescription='" + _InsuranceRepCtgry.Replace("'", "''") + "' ) and nID<>'" + _nID + "'";
                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_strsql);
                if (_intresult != null)
                {
                    if (Convert.ToInt64(_intresult) > 0)
                    {
                        _result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;


        }

        //End abhisekh
        public bool IsExistsHospital(string _HospitalName, Int64 _ContactID, bool IsOtherConatct)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";
                if (IsOtherConatct == true)
                {
                    _sqlQuery = "Select Count(*) from Contacts_MST where sName='" + _HospitalName.Replace("'", "''") + "' and sContactType = 'Others' and bIsBlocked!=1 and nContactID!='" + _ContactID + "'";
                }
                else
                {
                    _sqlQuery = "Select Count(*) from Contacts_MST where sName='" + _HospitalName.Replace("'", "''") + "' and sContactType = 'Hospital' and bIsBlocked!=1 and nContactID!='" + _ContactID + "'";
                }




                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool IsExistsPhysician(string _PhysicianFirstName, string _PhysicianMiddleName, string _PhysicianLastName, Int64 _ContactID,string _sSPI)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "Select Count(*) from Contacts_MST where sFirstName='" + _PhysicianFirstName.Replace("'", "''") + "' and sMiddleName='" + _PhysicianMiddleName.Replace("'", "''") + "' and sLastName='" + _PhysicianLastName.Replace("'", "''") + "' and sContactType = 'Physician' and bIsBlocked!=1 and nContactID!='" + _ContactID + "' and sSPI ='" + _sSPI + "'";


                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool IsExistsInsurance(string _Insurance, Int64 _ContactID, Int64 _nCompanyID)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";
                if (_nCompanyID > 0)
                {
                    _sqlQuery = "SELECT COUNT(*) AS nCount FROM Contacts_MST INNER JOIN Contact_InsurancePlan_Association ON Contacts_MST.nContactID = Contact_InsurancePlan_Association.nContactId WHERE Contacts_MST.sContactType = 'Insurance' AND(Contact_InsurancePlan_Association.nCompanyId =" + _nCompanyID + ") AND Contacts_MST.sName ='" + _Insurance.Replace("'", "''") + "' And Contact_InsurancePlan_Association.nContactId!=" + _ContactID + "AND Contacts_MST.bIsBlocked !=1 ";
                }
                else
                {
                    _sqlQuery = "Select Count(*) from Contacts_MST where sName='" + _Insurance.Replace("'", "''") + "' and sContactType = 'Insurance' and bIsBlocked!=1 and nContactID!='" + _ContactID + "'";
                    /// _sqlQuery = "SELECT COUNT(*) AS nCount FROM Contacts_MST INNER JOIN Contact_InsurancePlan_Association ON Contacts_MST.nContactID = Contact_InsurancePlan_Association.nContactId WHERE Contacts_MST.sContactType = 'Insurance' AND Contacts_MST.sName ='" + _Insurance.Replace("'","''")  + "' And Contact_InsurancePlan_Association.nContactId!=" + _ContactID; 
                }



                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool IsExistsPharmacy(string _Pharmacy, Int64 _ContactID)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "";

                _sqlQuery = "Select Count(*) from Contacts_MST where sName='" + _Pharmacy.Replace("'", "''") + "' and sContactType = 'Pharmacy' and bIsBlocked!=1 and nContactID!='" + _ContactID + "'";


                object _intresult = null;
                _intresult = oDB.ExecuteScalar_Query(_sqlQuery);
                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = true;
                        }
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ToString();
                DBErr = null;
            }
            catch (Exception)// ex)
            {
                //ex.ToString();
                //ex = null;
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        #endregion

        #endregion

        #region "retrieve Contact Information  "

        public Physician SelectPhysician(Int64 contactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Physician oPhysician = new Physician();
            DataTable dt;
            try
            {
                oDB.Connect(false);
                //Problem 750 : We have added this condition in order to handle suffix a. Contacts_MST.Degree b.Contacts_Physician_DTL.Suffix c. Contacts_Physician_DTL.Degree
                //Bug #68135: 00000693: Prefix Suffix not showing in Physician modify screen
                string SqlQuery = "SELECT ISNULL(Contacts_MST.sAddressLine1,'') AS sAddressLine1,ISNULL(Contacts_MST.sAddressLine2,'') AS sAddressLine2, " +
               "ISNULL(Contacts_MST.sCity,'') AS sCity,ISNULL(Contacts_MST.sState,'') AS sState, ISNULL(Contacts_MST.sZIP,'')AS sZIP, ISNULL(Contacts_MST.sPhone,'') AS sPhone, " +
               "ISNULL( Contacts_MST.sFax,'') AS sFax,ISNULL(Contacts_MST.sEmail,'') AS sEmail,ISNULL(Contacts_MST.sURL,'') AS sURL, " +
               "ISNULL(Contacts_MST.sBMAddressLine1,'') AS sBMAddressLine1, ISNULL(Contacts_MST.sBMAddressLine2,'') AS sBMAddressLine2, " +
               "ISNULL(Contacts_MST.sBMCity,'') AS sBMCity,ISNULL(Contacts_MST.sBMState,'') AS sBMState, ISNULL(Contacts_MST.sBMZip,'') AS sBMZip , " +
               "ISNULL(Contacts_MST.sBMPhone,'') AS sBMPhone, ISNULL(Contacts_MST.sBMFax,'') AS  sBMFax, " +
               "ISNULL(Contacts_MST.sBMEail,'') AS sBMEail,ISNULL(Contacts_MST.sContactType,'') AS sContactType,ISNULL(Contacts_MST.sBMURL,'') AS sBMURL," +
               "ISNULL(Contacts_MST.sPracAddressLine1,'') AS sPracAddressLine1,ISNULL(Contacts_MST.sPracAddressLine2,'') AS sPracAddressLine2, ISNULL(Contacts_MST.sPracCity,'') AS sPracCity,  " +
               "ISNULL(Contacts_MST.sPracState,'') AS sPracState, ISNULL(Contacts_MST.sPracZIP,'') AS sPracZIP,ISNULL(Contacts_MST.sPracPhone,'') AS sPracPhone,  " +
               "ISNULL(Contacts_MST.sPracFax,'') AS sPracFax, ISNULL(Contacts_MST.sPracEmail,'') AS sPracEmail, ISNULL(Contacts_MST.sPracURL,'') AS sPracURL, " +
               "ISNULL(Contacts_MST.sMobile,'') AS  sMobile, ISNULL(Contacts_MST.sPager,'') As sPager,ISNULL(Contacts_MST.sNotes,'') As sNotes,ISNULL(Contacts_MST.sFirstName,'') AS sFirstName, " +
               "ISNULL(Contacts_MST.sMiddleName,'') AS sMiddleName , ISNULL(Contacts_MST.sLastName,'') AS sLastName ,ISNULL(Contacts_MST.sGender,'') AS  sGender ,  " +
               "ISNULL(Contacts_Physician_DTL.sHospitalAffiliation,'') AS sHospitalAffiliation, ISNULL(Contacts_Physician_DTL.sExternalCode,'') AS sExternalCode , " +
               "ISNULL(dbo.Contacts_MST.sDegree, '') AS sDegree ,ISNULL(Contacts_Physician_DTL.sTaxonomy,'') AS sTaxonomy , ISNULL(Contacts_Physician_DTL.sTaxonomyDesc,'') AS sTaxonomyDesc, " +
               "ISNULL(Contacts_Physician_DTL.sTaxID,'') AS sTaxID,ISNULL(Contacts_Physician_DTL.sUPIN,'') AS sUPIN, ISNULL(Contacts_Physician_DTL.sNPI,'') AS sNPI , " +
               "ISNULL(Contacts_Physician_DTL.nSpecialtyID,0) AS  nSpecialtyID  " +
               ",ISNULL(Contacts_Physician_DTL.sPrefix,'') AS sPrefix,ISNULL(Contacts_Physician_DTL.sSuffix,'') AS sSuffix,ISNULL(Contacts_Physician_DTL.sDegree,'') AS sPhysicianDegree,ISNULL(Contacts_MST.sSPI,'') AS sSPI,ISNULL(sDirectAddress,'') AS sDirectAddress,  " + //sSuffix field added by Sandip Darade case GLO2010-0004426              
             "ISNULL(Contacts_MST.sSpecialtytype1,'') AS [Specialty Type1],ISNULL(Contacts_Physician_DTL.sClinicName,'') AS [Clinic Name]"+  
             " FROM Contacts_MST left outer join Contacts_Physician_DTL ON Contacts_MST.nContactID = Contacts_Physician_DTL.nContactID  " +
               "left outer join Contacts_Detail ON Contacts_MST.nContactID =Contacts_Detail.nContactID WHERE  Contacts_MST.nContactID ='" + contactID + "' AND ISNULL(Contacts_MST.nClinicID,1)=" + ClinicID + " AND ISNULL(bIsBlocked,0)= 0";

                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oPhysician.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                        oPhysician.Pager = Convert.ToString(dt.Rows[0]["sPager"]);
                        oPhysician.Notes = Convert.ToString(dt.Rows[0]["sNotes"]);
                        oPhysician.FirstName = Convert.ToString(dt.Rows[0]["sFirstName"]);
                        oPhysician.MiddleName = Convert.ToString(dt.Rows[0]["sMiddleName"]);
                        oPhysician.LastName = Convert.ToString(dt.Rows[0]["sLastName"]);
                        oPhysician.Gender = Convert.ToString(dt.Rows[0]["sGender"]);
                        oPhysician.SpecialtyID = Convert.ToInt64(dt.Rows[0]["nSpecialtyID"]);
                        oPhysician.HospitalAffiliation = Convert.ToString(dt.Rows[0]["sHospitalAffiliation"]);
                        oPhysician.ContactType = Convert.ToString(dt.Rows[0]["sContactType"]);
                        oPhysician.ExternalCode = Convert.ToString(dt.Rows[0]["sExternalCode"]);
                        
                       
                        oPhysician.Taxonomy = Convert.ToString(dt.Rows[0]["sTaxonomy"]);
                        oPhysician.TaxonomyDesc = Convert.ToString(dt.Rows[0]["sTaxonomyDesc"]);
                        oPhysician.TaxID = Convert.ToString(dt.Rows[0]["sTaxID"]);
                        oPhysician.UPIN = Convert.ToString(dt.Rows[0]["sUPIN"]);
                        oPhysician.NPI = Convert.ToString(dt.Rows[0]["sNPI"]);

                        oPhysician.CompanyAddress.AddrressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                        oPhysician.CompanyAddress.AddrressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                        oPhysician.CompanyAddress.City = Convert.ToString(dt.Rows[0]["sCity"]);
                        oPhysician.CompanyAddress.State = Convert.ToString(dt.Rows[0]["sState"]);
                        oPhysician.CompanyAddress.ZIP = Convert.ToString(dt.Rows[0]["sZip"]);
                        oPhysician.CompanyAddress.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                        oPhysician.CompanyAddress.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                        oPhysician.CompanyAddress.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                        oPhysician.CompanyAddress.URL = Convert.ToString(dt.Rows[0]["sURL"]);

                        oPhysician.BusinessMailingAddress.AddrressLine1 = Convert.ToString(dt.Rows[0]["sBMAddressLine1"]);
                        oPhysician.BusinessMailingAddress.AddrressLine2 = Convert.ToString(dt.Rows[0]["sBMAddressLine2"]);
                        oPhysician.BusinessMailingAddress.City = Convert.ToString(dt.Rows[0]["sBMCity"]);
                        oPhysician.BusinessMailingAddress.State = Convert.ToString(dt.Rows[0]["sBMState"]);
                        oPhysician.BusinessMailingAddress.ZIP = Convert.ToString(dt.Rows[0]["sBMZip"]);
                        oPhysician.BusinessMailingAddress.Phone = Convert.ToString(dt.Rows[0]["sBMPhone"]);
                        oPhysician.BusinessMailingAddress.Fax = Convert.ToString(dt.Rows[0]["sBMFax"]);
                        oPhysician.BusinessMailingAddress.Email = Convert.ToString(dt.Rows[0]["sBMEail"]);
                        oPhysician.BusinessMailingAddress.URL = Convert.ToString(dt.Rows[0]["sBMURL"]);

                        oPhysician.PracticeLocationAddress.AddrressLine1 = Convert.ToString(dt.Rows[0]["sPracAddressLine1"]);
                        oPhysician.PracticeLocationAddress.AddrressLine2 = Convert.ToString(dt.Rows[0]["sPracAddressLine2"]);
                        oPhysician.PracticeLocationAddress.City = Convert.ToString(dt.Rows[0]["sPracCity"]);
                        oPhysician.PracticeLocationAddress.State = Convert.ToString(dt.Rows[0]["sPracState"]);
                        oPhysician.PracticeLocationAddress.ZIP = Convert.ToString(dt.Rows[0]["sPracZip"]);
                        oPhysician.PracticeLocationAddress.Phone = Convert.ToString(dt.Rows[0]["sPracPhone"]);
                        oPhysician.PracticeLocationAddress.Fax = Convert.ToString(dt.Rows[0]["sPracFax"]);
                        oPhysician.PracticeLocationAddress.Email = Convert.ToString(dt.Rows[0]["sPracEmail"]);
                        oPhysician.PracticeLocationAddress.URL = Convert.ToString(dt.Rows[0]["sPracURL"]);

                        oPhysician.Prefix = Convert.ToString(dt.Rows[0]["sPrefix"]);//sSuffix field added by Sandip Darade case GLO2010-0004426 
                        //Bug #68135: 00000693: Prefix Suffix not showing in Physician modify screen
                        //Problem 750 : We have added this condition in order to handle suffix a. Contacts_MST.Degree b.Contacts_Physician_DTL.Suffix c. Contacts_Physician_DTL.Degree
                        oPhysician.Suffix = "";
                        oPhysician.Degree = "";
                        if (Convert.ToString(dt.Rows[0]["sDegree"]) != "")  //Contacts_MST.sDegree
                        {
                            oPhysician.Suffix = Convert.ToString(dt.Rows[0]["sDegree"]);
                            oPhysician.Degree = Convert.ToString(dt.Rows[0]["sDegree"]);
                        }
                        else if (Convert.ToString(dt.Rows[0]["sPhysicianDegree"]) != "") //Contacts_Physician_dtl.sDegree
                        {
                            oPhysician.Suffix = Convert.ToString(dt.Rows[0]["sPhysicianDegree"]);
                            oPhysician.Degree = Convert.ToString(dt.Rows[0]["sPhysicianDegree"]);
                        }
                        else if (Convert.ToString(dt.Rows[0]["sSuffix"])!="") //Contacts_physician_dtl.sSuffix
                        {
                            oPhysician.Suffix = Convert.ToString(dt.Rows[0]["sSuffix"]);
                            oPhysician.Degree = Convert.ToString(dt.Rows[0]["sSuffix"]);
                        }
                                                
                        oPhysician.DirectAddress = Convert.ToString(dt.Rows[0]["sDirectAddress"]);
                        oPhysician.SPI = Convert.ToString(dt.Rows[0]["sSPI"]);
                        oPhysician.SpecialtyType = Convert.ToString(dt.Rows[0]["Specialty Type1"]);
                        oPhysician.ClinicName = Convert.ToString(dt.Rows[0]["Clinic Name"]);

                        // oPhysician.PARequired = (PriorAuthorizationRequired) Convert.ToInt16(dt.Rows[0]["nIsPARequired"]);
                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oPhysician;
        }

        public ContactDetails SelectPhysicianDetails(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters odbparam = new gloDatabaseLayer.DBParameters();
            string strSql = " SELECT  ISNULL(sInsuranceCode,'') AS sInsuranceCode , ISNULL(sInsuranceDecs,'') AS sInsuranceDecs, ISNULL(sDescription,'') AS sDescription ,ISNULL(sValue,'') AS  sValue,ISNULL( nType,0) AS nType  FROM Contacts_Detail  WHERE nContactId = " + ContactID + "";
            ContactDetail oCDetail = new ContactDetail();
            ContactDetails ocdet = new ContactDetails();

            DataTable dt;
            try
            {
                oDB.Connect(false);
                oDB.Retrive_Query(strSql, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dt.Rows.Count - 1; i++)
                        {
                            oCDetail = new ContactDetail();
                            oCDetail.InsuranceCode = dt.Rows[i]["sInsuranceCode"].ToString();
                            oCDetail.InsuranceDesc = dt.Rows[i]["sInsuranceDecs"].ToString();
                            oCDetail.Description = dt.Rows[i]["sDescription"].ToString();
                            oCDetail.Value = dt.Rows[i]["sValue"].ToString();
                            oCDetail.Type = Convert.ToInt16(dt.Rows[i]["nType"]);
                            ocdet.Add(oCDetail);
                        }

                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();

            }
            return ocdet;
        }


        public Insurance SelectInsurance(Int64 contactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Insurance oInsurance = new Insurance();
            DataTable dt;
            try
            {
                oDB.Connect(false);

                string SqlQuery = " SELECT  ISNULL(Contacts_MST.sAddressLine1,'') as sAddressLine1, ISNULL(Contacts_MST.sAddressLine2,'') as sAddressLine2, ISNULL(Contacts_MST.sCity,'') as sCity, ISNULL(Contacts_MST.sState,'') as sState," +
                                " ISNULL(Contacts_MST.sZIP,'') as sZIP,ISNULL(Contacts_MST.sPhone,'')as sPhone,ISNULL(Contacts_MST.sFax,'') as sFax, ISNULL(Contacts_MST.sEmail,'') as sEmail, ISNULL(Contacts_MST.sURL,'') as sURL, ISNULL(Contacts_MST.sMobile,'') as sMobile, ISNULL(Contacts_MST.sPager,'') as sPager," +
                                " ISNULL(Contacts_MST.sContact,'') as sContact, ISNULL(Contacts_MST.sName,'') as sName,ISNULL(Contacts_Insurance_DTL.sInsuranceTypeCode,'') as sInsuranceTypeCode, ISNULL(Contacts_Insurance_DTL.sInsuranceTypeDesc,'') as sInsuranceTypeDesc," +
                                " ISNULL(Contacts_Insurance_DTL.sPayerId,'') as sPayerId, ISNULL(Contacts_Insurance_DTL.bAccessAssignment,'false') AS bAccessAssignment, ISNULL(Contacts_Insurance_DTL.bStatementToPatient,'false') AS bStatementToPatient, ISNULL(Contacts_Insurance_DTL.bMedigap,'false') AS bMedigap ," +
                                " ISNULL(Contacts_Insurance_DTL.bReferringIDInBox19,'false') AS bReferringIDInBox19, ISNULL(Contacts_Insurance_DTL.bNameOfacilityinBox33,'false') AS bNameOfacilityinBox33,ISNULL(Contacts_Insurance_DTL.bDoNotPrintFacility,'false') AS bDoNotPrintFacility,ISNULL(Contacts_Insurance_DTL.b1stPointer,'false') AS b1stPointer,ISNULL(Contacts_Insurance_DTL.bBox31Blank,'false') AS bBox31Blank, ISNULL(Contacts_Insurance_DTL.bShowPayment,'false') AS bShowPayment, isnull(Contacts_Insurance_DTL.nTypeOBilling,0) as nTypeOBilling, isnull(Contacts_Insurance_DTL.nClearingHouse,0 ) as nClearingHouse ," +
                                "ISNULL(Contacts_Insurance_DTL.bIsClaims,'false') AS  bIsClaims,ISNULL(Contacts_Insurance_DTL.bIsRemittanceAdvice,'false') AS bIsRemittanceAdvice ,ISNULL(Contacts_Insurance_DTL.bIsRealTimeEligibility,'false') AS  bIsRealTimeEligibility," +
                                "ISNULL(Contacts_Insurance_DTL.bIsElectronicCOB,'false') AS  bIsElectronicCOB,ISNULL(Contacts_Insurance_DTL.bIsRealTimeClaimStatus,'false') AS bIsRealTimeClaimStatus ,ISNULL(Contacts_Insurance_DTL.bIsEnrollmentRequired,'false') AS bIsEnrollmentRequired ,  " +
                                "ISNULL(Contacts_Insurance_DTL.sPayerPhone,'') AS sPayerPhone ,ISNULL(Contacts_Insurance_DTL.sWebsite,'') AS  sWebsite,ISNULL(Contacts_Insurance_DTL.sServicingState,'') AS sServicingState , " +
                                "ISNULL(Contacts_Insurance_DTL.sComments,'') AS  sComments,ISNULL(Contacts_Insurance_DTL.sPayerPhoneExtn,'') AS  sPayerPhoneExtn, ISNULL(Contacts_Insurance_DTL.bNotesInBox19,'false') AS bNotesInBox19, ISNULL(Contacts_Insurance_DTL.sOfficeID,'') AS sOfficeID," +
                                "ISNULL(Contacts_Insurance_DTL.sBox32,'') AS  sBox32,ISNULL(Contacts_Insurance_DTL.sBox32A,'') AS  sBox32A,ISNULL(Contacts_Insurance_DTL.sBox32B,'') AS  sBox32B," +
                                "ISNULL(Contacts_Insurance_DTL.sBox33,'') AS  sBox33,ISNULL(Contacts_Insurance_DTL.sBox33A,'') AS  sBox33A,ISNULL(Contacts_Insurance_DTL.sBox33B,'') AS  sBox33B ," +
                                " ISNULL(Contacts_Insurance_DTL.sIncludeFacilitieswithPOS11onClaim,'') As sIncludeFacilitieswithPOS11onClaim ," +
                                " ISNULL(Contacts_Insurance_DTL.bIsOTAFAmount,'false') AS  bIsOTAFAmount,ISNULL(Contacts_Insurance_DTL.sInsEligibilityProviderID,'') AS  sInsEligibilityProviderID,ISNULL(nCPTMappingID,0) as nCPTMappingID, ISNULL(bIsPARequired,0) AS bIsPARequired,ISNULL(bIsInstitutionalBilling,0) AS bIsInstitutionalBilling,ISNULL(bIncludeTaxonomyForPaper,0) AS bIncludeTaxonomyForPaper,ISNULL(bIncludeTaxonomyForElectronic,0) AS bIncludeTaxonomyForElectronic,ISNULL(sTaxonomyQualifier,'') AS sTaxonomyQualifier,ISNULL(sClaimOfficeNumber,'') AS sClaimOfficeNumber,ISNULL(bIncludeRendering,'FALSE') AS bIncludeRendering ,ISNULL(bIncludeServiceFacility,'FALSE') AS bIncludeServiceFacility ,ISNULL(bIncludeSubscriberAddress,'FALSE') AS bIncludeSubscriberAddress , " +
                                 //" FROM  Contacts_MST left outer  join Contacts_Insurance_DTL ON  Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID WHERE  Contacts_MST.nContactID ='" + contactID + "' AND ISNULL(Contacts_MST.nClinicID,1)=" + ClinicID + " AND ISNULL(bIsBlocked,0)= 0";
                                " ISNULL(sInsEligibilityProviderType,'') AS sInsEligibilityProviderType, ISNULL(Contacts_Insurance_DTL.sInsEligibilityProvSecID,'') AS sInsEligibilityProvSecID,ISNULL(sInsEligibilityProviSecType,'') AS sInsEligibilityProviSecType,ISNULL(bIDInBox31,'FALSE') AS bIDInBox31,ISNULL(bIncludePlanname,'FALSE') AS bIncludePlanname," +
                                "ISNULL(bPaperDisplayMailingAddress,'FALSE') AS bPaperDisplayMailingAddress,ISNULL(bSwap1a9a1aMCare,'FALSE') AS bSwap1a9a1aMCare,ISNULL(bIncludeRendering_Attending,0) as bIncludeRendering_Attending," +
                                "ISNULL(bPaperRenderingTaxonomy,0) as bPaperRenderingTaxonomy,ISNULL(bPaperBillingTaxonomy,0) as bPaperBillingTaxonomy,ISNULL(bElectronicRenderingTaxonomy,0) as bElectronicRenderingTaxonomy,ISNULL(bElectronicBillingTaxonomy,0) as bElectronicBillingTaxonomy ,ISNULL(Contacts_Insurance_DTL.sInsEligibiltyContact,'') AS InsEligibiltyContact ,ISNULL(Contacts_Insurance_DTL.sInsEligibiltyPhone,'') AS InsEligibiltyPhone ,ISNULL(Contacts_Insurance_DTL.sInsEligibiltyWesite,'') AS InsEligibiltyWesite,ISNULL(Contacts_Insurance_DTL.sInsEligibiltyNote,'') AS InsEligibiltyNote," +
                                "ISNULL(bBillEPSDTorFamilyPlanning,0) as bBillEPSDTorFamilyPlanning,ISNULL(bEDIIncludeSV,0) as bEDIIncludeSV,ISNULL(bEDIIncludeCRC,0) as bEDIIncludeCRC,ISNULL(bPaperIncludeReferralCode,0) as bPaperIncludeReferralCode,ISNULL(sPaperClaimEPSDTCode,'') as sPaperClaimEPSDTCode,ISNULL(sPaperClaimEPSDTCodeBox,'') as sPaperClaimEPSDTCodeBox,ISNULL(sPaperClaimFamilyPlanningCode,'') as sPaperClaimFamilyPlanningCode,ISNULL(sPaperClaimFamilyPlanningCodeBox,'') as sPaperClaimFamilyPlanningCodeBox,ISNULL(bSupressRenderEPSDTClaimOnPaperEDI,0) as bSupressRenderEPSDTClaimOnPaperEDI,ISNULL(bEMGAsX,0) as bEMGAsX ,ISNULL(bShowClaimNo,0) AS bShowClaimNo, ISNULL(sBillUnitsAs,'') AS sBillUnitsAs,ISNULL(nMinutesPerUnits,0) AS nMinutesPerUnits,ISNULL(bIsWorkerComp,0) AS bIsWorkerComp,ISNULL(bIsClaimFrequencyOne,0) AS bIsClaimFrequencyOne,ISNULL(bIncludeMedicareClaimRef,0) AS bIncludeMedicareClaimRef,ISNULL(bDefaultOccuranceDOS,0) AS bDefaultOccuranceDOS,ISNULL(sEDIAltPayerIDType,'') As sEDIAltPayerIDType,ISNULL(sBox19DefaultNote,'') As sBox19DefaultNote,ISNULL(sUBBox5,'') As sUBBox5,ISNULL(sUBBox77,'') As sUBBox77 ,ISNULL(nUB51BillingProvderOtherID,0) AS nUB51BillingProvderOtherID,ISNULL(bIncludeOrderingProvider,0) As bIncludeOrderingProvider" +
                                ",isnull(Contacts_Insurance_DTL.nBox11bSettingID,1) as nBox11bSettingID, isnull(bIncludeEdiAltPayerID,0) as bIncludeEdiAltPayerID,isnull(bIncludeReferring_supervising,0) as bIncludeReferring_supervising,isnull(bIncludeReferring_ordering,0) as bIncludeReferring_ordering,isnull(sCMSDateFormat,'YYYY') as sCMSDateFormat, ISNULL(bReportClinicName,0) as bReportClinicName,ISNULL(sUBBox77Rendering,'') as sUBBox77Rendering,ISNULL(bIncludeUB04DischargeHour,0) AS bIncludeUB04DischargeHour,ISNULL(bIncludeUB04AdmissionHour,0) AS bIncludeUB04AdmissionHour,bIncludePriorPatPayment AS bIncludePriorPatPayment,  "+
                                            "isnull(blnIncludeUB04RevenuecodeTotal,0) as blnIncludeUB04RevenuecodeTotal,ISNULL([bIncludeSecondaryPayerAddress], 0) AS [bIncludeSecondaryPayerAddress],ISNULL(bIncludePatientSSN,0) AS bIncludePatientSSN, isnull(bIncludeModInSVD,0) as bIncludeModInSVD, isnull(bIncludePrimaryDxInBox69,1) as bIncludePrimaryDxInBox69,ISNULL(bIsGroupMandatory,0)as bIsGroupMandatory  FROM  Contacts_MST left outer  join Contacts_Insurance_DTL ON  Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID WHERE  Contacts_MST.nContactID ='" + contactID + "' AND ISNULL(Contacts_MST.nClinicID,1)=" + ClinicID;

                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oInsurance.Mobile = Convert.ToString(dt.Rows[0]["sMobile"]);
                        oInsurance.Pager = Convert.ToString(dt.Rows[0]["sPager"]);

                        oInsurance.CompanyAddress.AddrressLine1 = Convert.ToString(dt.Rows[0]["sAddressLine1"]);
                        oInsurance.CompanyAddress.AddrressLine2 = Convert.ToString(dt.Rows[0]["sAddressLine2"]);
                        oInsurance.CompanyAddress.City = Convert.ToString(dt.Rows[0]["sCity"]);
                        oInsurance.CompanyAddress.State = Convert.ToString(dt.Rows[0]["sState"]);
                        oInsurance.CompanyAddress.ZIP = Convert.ToString(dt.Rows[0]["sZip"]);
                        oInsurance.CompanyAddress.Phone = Convert.ToString(dt.Rows[0]["sPhone"]);
                        oInsurance.CompanyAddress.Fax = Convert.ToString(dt.Rows[0]["sFax"]);
                        oInsurance.CompanyAddress.Email = Convert.ToString(dt.Rows[0]["sEmail"]);
                        oInsurance.CompanyAddress.URL = Convert.ToString(dt.Rows[0]["sURL"]);

                        oInsurance.Name = Convert.ToString(dt.Rows[0]["sName"]);
                        oInsurance.ContactName = Convert.ToString(dt.Rows[0]["sContact"]);
                        //nContactID, sInsuranceTypeCode, sInsuranceTypeDesc, sPayerId, bAccessAssignment, bStatementToPatient, bMedigap, 
                        //bReferringIDInBox19, bNameOfacilityinBox33, bDoNotPrintFacility, b1stPointer, bBox31Blank, bShowPayment, nTypeOBilling, 
                        //nClearingHouse, nClinicID
                        oInsurance.InsuranceTypeDesc = Convert.ToString(dt.Rows[0]["sInsuranceTypeDesc"]);
                        oInsurance.InsuranceTypeCode = Convert.ToString(dt.Rows[0]["sInsuranceTypeCode"]);


                        oInsurance.sPayerID = Convert.ToString(dt.Rows[0]["sPayerId"]);
                        oInsurance.bBox31Blank = Convert.ToBoolean(dt.Rows[0]["bBox31Blank"]);
                        oInsurance.b1stPointer = Convert.ToBoolean(dt.Rows[0]["b1stPointer"]);
                        oInsurance.bAccessAssignment = Convert.ToBoolean(dt.Rows[0]["bAccessAssignment"]);
                        oInsurance.bIsWorkerComp = Convert.ToBoolean(dt.Rows[0]["bIsWorkerComp"]);
                        oInsurance.bDoNotPrintFacility = Convert.ToBoolean(dt.Rows[0]["bDoNotPrintFacility"]);
                        oInsurance.bMedigap = Convert.ToBoolean(dt.Rows[0]["bMedigap"]);
                        oInsurance.bNameOfacilityinBox33 = Convert.ToBoolean(dt.Rows[0]["bNameOfacilityinBox33"]);
                        oInsurance.bReferringIDInBox19 = Convert.ToBoolean(dt.Rows[0]["bReferringIDInBox19"]);
                        oInsurance.bShowPayment = Convert.ToBoolean(dt.Rows[0]["bShowPayment"]);
                        oInsurance.bStatementToPatient = Convert.ToBoolean(dt.Rows[0]["bStatementToPatient"]);
                        oInsurance.nClearingHouse = Convert.ToInt64(dt.Rows[0]["nClearingHouse"]);
                        oInsurance.bIsGroupMandatory = Convert.ToBoolean(dt.Rows[0]["bIsGroupMandatory"]);

                        oInsurance.sWebsite = Convert.ToString(dt.Rows[0]["sWebsite"]);
                        oInsurance.sServicingState = Convert.ToString(dt.Rows[0]["sServicingState"]);
                        oInsurance.sPayerPhoneExtn = Convert.ToString(dt.Rows[0]["sPayerPhoneExtn"]);
                        oInsurance.sPayerPhone = Convert.ToString(dt.Rows[0]["sPayerPhone"]);
                        oInsurance.sComments = Convert.ToString(dt.Rows[0]["sComments"]);
                        oInsurance.bIsClaims = Convert.ToBoolean(dt.Rows[0]["bIsClaims"]);
                        oInsurance.bIsElectronicCOB = Convert.ToBoolean(dt.Rows[0]["bIsElectronicCOB"]);
                        oInsurance.bIsEnrollmentRequired = Convert.ToBoolean(dt.Rows[0]["bIsEnrollmentRequired"]);
                        oInsurance.bIsRealTimeClaimStatus = Convert.ToBoolean(dt.Rows[0]["bIsRealTimeClaimStatus"]);
                        oInsurance.bIsRealTimeEligibility = Convert.ToBoolean(dt.Rows[0]["bIsRealTimeEligibility"]);
                        oInsurance.bIsRemittanceAdvice = Convert.ToBoolean(dt.Rows[0]["bIsRemittanceAdvice"]);
                        //Added on 27/04/2010
                        oInsurance.bIsOTAFAmount = Convert.ToBoolean(dt.Rows[0]["bIsOTAFAmount"]);
                        //Added by Anil 20090704
                        oInsurance.bNotesInBox19 = Convert.ToBoolean(dt.Rows[0]["bNotesInBox19"]);
                        oInsurance.OfficeID = Convert.ToString(dt.Rows[0]["sOfficeID"]);

                        oInsurance.nTypeOBilling = (TypeOfBilling)Convert.ToInt32((dt.Rows[0]["nTypeOBilling"]));

                        oInsurance.Box32 = Convert.ToString(dt.Rows[0]["sBox32"]);
                        oInsurance.Box32A = Convert.ToString(dt.Rows[0]["sBox32A"]);
                        oInsurance.Box32B = Convert.ToString(dt.Rows[0]["sBox32B"]);

                        oInsurance.Box33 = Convert.ToString(dt.Rows[0]["sBox33"]);
                        oInsurance.Box33A = Convert.ToString(dt.Rows[0]["sBox33A"]);
                        oInsurance.Box33B = Convert.ToString(dt.Rows[0]["sBox33B"]);

                        oInsurance.sDoNotPrintFacility = Convert.ToString(dt.Rows[0]["sIncludeFacilitieswithPOS11onClaim"]);

                        oInsurance.CPTCrosswalkID = Convert.ToInt64(dt.Rows[0]["nCPTMappingID"]);
                        oInsurance.PARequired = Convert.ToBoolean(dt.Rows[0]["bIsPARequired"]);
                        oInsurance.IsInstitutionalBilling = Convert.ToBoolean(dt.Rows[0]["bIsInstitutionalBilling"]);
                       // oInsurance.FedTaxNoBox5 = Convert.ToString(dt.Rows[0]["sUBBox5"]);
                        oInsurance.operationgProviderBox77 = Convert.ToString(dt.Rows[0]["sUBBox77"]);
                        oInsurance.Box77RenderingProvider = Convert.ToString(dt.Rows[0]["sUBBox77Rendering"]);
                        //oInsurance.UB51BillingProvderOtherID = Convert.ToInt64(dt.Rows[0]["nUB51BillingProvderOtherID"]);

                        //By Debasish on 11/19/2010
                        oInsurance.InsuranceEligibilityProviderID = Convert.ToString(dt.Rows[0]["sInsEligibilityProviderID"]);
                        oInsurance.EligibiltiContact = Convert.ToString(dt.Rows[0]["InsEligibiltyContact"]);
                        oInsurance.EligibilityPhone = Convert.ToString(dt.Rows[0]["InsEligibiltyPhone"]);
                        oInsurance.Eligibilitywebsite = Convert.ToString(dt.Rows[0]["InsEligibiltyWesite"]);
                        oInsurance.EligibilityNote = Convert.ToString(dt.Rows[0]["InsEligibiltyNote"]);
                        //oInsurance.IsDefaultPriorAuthorizationRequired = Convert.ToBoolean(dt.Rows[0]["bIsDefaultPriorAutReq"]);
                        //oInsurance.IsIncludeTaxonomyforPaper = Convert.ToBoolean(dt.Rows[0]["bIncludeTaxonomyForPaper"]);
                        //oInsurance.IsIncludeTaxonomyforElectronic = Convert.ToBoolean(dt.Rows[0]["bIncludeTaxonomyForElectronic"]);
                        oInsurance.bPaperRenderingTaxonomy = Convert.ToBoolean(dt.Rows[0]["bPaperRenderingTaxonomy"]);
                        oInsurance.bPaperBillingTaxonomy = Convert.ToBoolean(dt.Rows[0]["bPaperBillingTaxonomy"]);
                        oInsurance.bElectronicRenderingTaxonomy = Convert.ToBoolean(dt.Rows[0]["bElectronicRenderingTaxonomy"]);
                        oInsurance.bElectronicBillingTaxonomy = Convert.ToBoolean(dt.Rows[0]["bElectronicBillingTaxonomy"]);
                        oInsurance.IncludeOrderingProvider = Convert.ToBoolean(dt.Rows[0]["bIncludeOrderingProvider"]);
                        oInsurance.sQualifier = Convert.ToString(dt.Rows[0]["sTaxonomyQualifier"]);
                        oInsurance.sBillClaimOfficeNo = Convert.ToString(dt.Rows[0]["sClaimOfficeNumber"]);
                        oInsurance.IncludeRenderingProvider = Convert.ToBoolean(dt.Rows[0]["bIncludeRendering"]);
                        oInsurance.IncludeServiceFacility = Convert.ToBoolean(dt.Rows[0]["bIncludeServiceFacility"]);
                        oInsurance.IncludeSubscriberAddress = Convert.ToBoolean(dt.Rows[0]["bIncludeSubscriberAddress"]);
                        oInsurance.InsuranceEligibilityProvType = Convert.ToString(dt.Rows[0]["sInsEligibilityProviderType"]);
                        oInsurance.InsuranceEligibilityProvSecID = Convert.ToString(dt.Rows[0]["sInsEligibilityProvSecID"]);
                        oInsurance.InsuranceEligibilityProvSecType = Convert.ToString(dt.Rows[0]["sInsEligibilityProviSecType"]);
                        oInsurance.bIDInBox31 = Convert.ToBoolean(dt.Rows[0]["bIDInBox31"]);
                        oInsurance.bIncludePlanName = Convert.ToBoolean(dt.Rows[0]["bIncludePlanname"]);
                        oInsurance.bDefaultOccuranceDOS = Convert.ToBoolean(dt.Rows[0]["bDefaultOccuranceDOS"]);
                        oInsurance.bPaperDisplayMailingAddress = Convert.ToBoolean(dt.Rows[0]["bPaperDisplayMailingAddress"]);
                        oInsurance.bSwap1a9a1aMCare = Convert.ToBoolean(dt.Rows[0]["bSwap1a9a1aMCare"]);
                        oInsurance.bIncludeRendering_Attending = Convert.ToBoolean(dt.Rows[0]["bIncludeRendering_Attending"]);
                        oInsurance.bIsBillEPSDTorFamilyPlanning = Convert.ToBoolean(dt.Rows[0]["bBillEPSDTorFamilyPlanning"]);
                        oInsurance.bIsEDIIncludeSV = Convert.ToBoolean(dt.Rows[0]["bEDIIncludeSV"]);
                        oInsurance.bIsEDIIncludeCRC = Convert.ToBoolean(dt.Rows[0]["bEDIIncludeCRC"]);
                        oInsurance.bIsPaperIncludeReferralCode = Convert.ToBoolean(dt.Rows[0]["bPaperIncludeReferralCode"]);
                        oInsurance.sPaperClaimEPSDTCode = Convert.ToString(dt.Rows[0]["sPaperClaimEPSDTCode"]);
                        oInsurance.sPaperClaimEPSDTCodeBox = Convert.ToString(dt.Rows[0]["sPaperClaimEPSDTCodeBox"]);
                        oInsurance.sPaperClaimFamilyPlanningCode = Convert.ToString(dt.Rows[0]["sPaperClaimFamilyPlanningCode"]);
                        oInsurance.sPaperClaimFamilyPlanningCodeBox = Convert.ToString(dt.Rows[0]["sPaperClaimFamilyPlanningCodeBox"]);
                        oInsurance.bIsSupressRenderEPSDTClaimOnPaperEDI = Convert.ToBoolean(dt.Rows[0]["bSupressRenderEPSDTClaimOnPaperEDI"]);
                        oInsurance.bEMGAsX = Convert.ToBoolean(dt.Rows[0]["bEMGAsX"]);
                        oInsurance.bShowClaim = Convert.ToBoolean(dt.Rows[0]["bShowClaimNo"]);
                        oInsurance.sBillUnitsAs = Convert.ToString(dt.Rows[0]["sBillUnitsAs"]);
                        oInsurance.nMinutesPerUnits = Convert.ToInt32(dt.Rows[0]["nMinutesPerUnits"]);
                        oInsurance.bIsClaimFrequencyOne = Convert.ToBoolean(dt.Rows[0]["bIsClaimFrequencyOne"]);
                        oInsurance.bIncludeMedicareClaimRef = Convert.ToBoolean(dt.Rows[0]["bIncludeMedicareClaimRef"]);
                        oInsurance.sEDIAltPayerIDType = Convert.ToString(dt.Rows[0]["sEDIAltPayerIDType"]);
                        oInsurance.sBox19DefaultNote = Convert.ToString(dt.Rows[0]["sBox19DefaultNote"]);
                        oInsurance.nBox11bSettingID = Convert.ToInt32(dt.Rows[0]["nBox11bSettingID"]);
                        oInsurance.bIncludeEdiAltPayerID = Convert.ToBoolean(dt.Rows[0]["bIncludeEdiAltPayerID"]);//Code added for include EDI Alt. Payer ID on secondary claims 06May2014 - Sameer
                        oInsurance.bIncludeReferring_supervising = Convert.ToBoolean(dt.Rows[0]["bIncludeReferring_supervising"]);
                        oInsurance.bIncludeReferring_ordering = Convert.ToBoolean(dt.Rows[0]["bIncludeReferring_ordering"]);
                        oInsurance.sCMSDateFormat = Convert.ToString(dt.Rows[0]["sCMSDateFormat"]);
                        oInsurance.bReportClinicName = Convert.ToBoolean(dt.Rows[0]["bReportClinicName"]);
                        oInsurance.bIncludeUB04DischargeHour = Convert.ToBoolean(dt.Rows[0]["bIncludeUB04DischargeHour"]);
                        oInsurance.bIncludeUB04AdmissionHour = Convert.ToBoolean(dt.Rows[0]["bIncludeUB04AdmissionHour"]);
                        oInsurance.IncludePriorPatientPayment = dt.Rows[0]["bIncludePriorPatPayment"];
                        oInsurance.bIncludeUB04RevenueCodeTotal = Convert.ToBoolean(dt.Rows[0]["blnIncludeUB04RevenuecodeTotal"]);
                        oInsurance.bIncludeSecondaryPayerAddress = Convert.ToBoolean(dt.Rows[0]["bIncludeSecondaryPayerAddress"]);
                        oInsurance.bIncludePatientSSN = Convert.ToBoolean(dt.Rows[0]["bIncludePatientSSN"]);
                        oInsurance.bIncludeMod_in_SVD = Convert.ToBoolean(dt.Rows[0]["bIncludeModInSVD"]);
                        oInsurance.bIncludePrimaryDxInBox69 = Convert.ToBoolean(dt.Rows[0]["bIncludePrimaryDxInBox69"]);
                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oInsurance;
        }

        public Pharmacy SelectPharmacy(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Pharmacy oPharmacy = new Pharmacy();
            DataTable dt;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT sName,sContact,sAddressLine1,sAddressLine2,sCity,sState,sZIP,sPhone,sFax,sEmail,sURL,sMobile,sPager  " +
                                  " ,ActiveStartTime, ActiveEndTime, ISNULL(sServiceLevel,'') AS sServiceLevel, ISNULL(sPharmacyStatus,'') AS sPharmacyStatus, ISNULL(sNCPDPID,'') AS sNCPDPID   " +
                                  "FROM Contacts_MST WHERE  sContactType = '" + ContactType.Pharmacy.ToString() + "'  AND nContactID = " + ContactID + " AND ISNULl(nClinicID,1)=" + ClinicID + " AND ISNULL(bIsBlocked,0)= 0";


                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oPharmacy.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oPharmacy.Pager = dt.Rows[0]["sPager"].ToString();

                        oPharmacy.CompanyAddress.AddrressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oPharmacy.CompanyAddress.AddrressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oPharmacy.CompanyAddress.City = dt.Rows[0]["sCity"].ToString();
                        oPharmacy.CompanyAddress.State = dt.Rows[0]["sState"].ToString();
                        oPharmacy.CompanyAddress.ZIP = dt.Rows[0]["sZip"].ToString();
                        oPharmacy.CompanyAddress.Phone = dt.Rows[0]["sPhone"].ToString();
                        oPharmacy.CompanyAddress.Fax = dt.Rows[0]["sFax"].ToString();
                        oPharmacy.CompanyAddress.Email = dt.Rows[0]["sEmail"].ToString();
                        oPharmacy.CompanyAddress.URL = dt.Rows[0]["sURL"].ToString();

                        oPharmacy.Name = dt.Rows[0]["sName"].ToString();
                        oPharmacy.ContactName = dt.Rows[0]["sContact"].ToString();
                        //Fields for ePharmacy
                        //ActiveStartTime ActiveEndTime sServiceLevel sPharmacyStatus sNCPDPID
                        if (dt.Rows[0]["ActiveStartTime"] != DBNull.Value)
                        {
                            oPharmacy.ActiveStartTime = Convert.ToDateTime(dt.Rows[0]["ActiveStartTime"]);
                        }
                        if (dt.Rows[0]["ActiveEndTime"] != DBNull.Value)
                        {
                            oPharmacy.ActiveEndTime = Convert.ToDateTime(dt.Rows[0]["ActiveEndTime"]);
                        }
                        oPharmacy.ServiceLevel = dt.Rows[0]["sServiceLevel"].ToString();
                        oPharmacy.PharmacyStatus = dt.Rows[0]["sPharmacyStatus"].ToString();
                        oPharmacy.NCPDPID = dt.Rows[0]["sNCPDPID"].ToString();
                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oPharmacy;
        }

        public Hospital SelectHospital(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Hospital oHospital = new Hospital();
            DataTable dt;
            try
            {
                oDB.Connect(false);

                //string SqlQuery = "SELECT sName,sContact,sAddressLine1,sAddressLine2,sCity,sState,sZIP,sPhone,sFax,sEmail,sURL,sMobile,sPager  " +
                //                  "FROM Contacts_MST WHERE  sContactType = '" + ContactType.Hospital.ToString() + "'  AND nContactID = " + ContactID + " AND ISNULl(nClinicID,1)=" + ClinicID + " AND ISNULL(bIsBlocked,0)= 0";

                string SqlQuery = " SELECT     Contacts_MST.sName, Contacts_MST.sContact, Contacts_MST.sAddressLine1, Contacts_MST.sAddressLine2, Contacts_MST.sCity,  " +
                      " Contacts_MST.sState, Contacts_MST.sZIP, Contacts_MST.sPhone, Contacts_MST.sFax, Contacts_MST.sEmail, Contacts_MST.sURL,  " +
                      " Contacts_MST.sMobile, Contacts_MST.sPager, Contacts_Hospital_DTL.sNPI  " +
                      " FROM Contacts_MST LEFT OUTER JOIN  " +
                      " Contacts_Hospital_DTL ON Contacts_MST.nContactID = Contacts_Hospital_DTL.nContactID  " +
                      " WHERE (Contacts_MST.sContactType = '" + ContactType.Hospital.ToString() + "') AND (Contacts_MST.nContactID = " + ContactID + " ) AND (ISNULL(Contacts_MST.nClinicID, 1) = " + ClinicID + ") AND " +
                      " (ISNULL(Contacts_MST.bIsBlocked, 0) = 0)";

                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oHospital.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oHospital.Pager = dt.Rows[0]["sPager"].ToString();

                        oHospital.CompanyAddress.AddrressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oHospital.CompanyAddress.AddrressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oHospital.CompanyAddress.City = dt.Rows[0]["sCity"].ToString();
                        oHospital.CompanyAddress.State = dt.Rows[0]["sState"].ToString();
                        oHospital.CompanyAddress.ZIP = dt.Rows[0]["sZip"].ToString();
                        oHospital.CompanyAddress.Phone = dt.Rows[0]["sPhone"].ToString();
                        oHospital.CompanyAddress.Fax = dt.Rows[0]["sFax"].ToString();
                        oHospital.CompanyAddress.Email = dt.Rows[0]["sEmail"].ToString();
                        oHospital.CompanyAddress.URL = dt.Rows[0]["sURL"].ToString();
                        oHospital.HospitalNPI = dt.Rows[0]["sNPI"].ToString();
                        oHospital.Name = dt.Rows[0]["sName"].ToString();
                        oHospital.ContactName = dt.Rows[0]["sContact"].ToString();


                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oHospital;
        }


        public Hospital SelectOthers(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Hospital oHospital = new Hospital();
            DataTable dt;
            try
            {
                oDB.Connect(false);

                //string SqlQuery = "SELECT sName,sContact,sAddressLine1,sAddressLine2,sCity,sState,sZIP,sPhone,sFax,sEmail,sURL,sMobile,sPager  " +
                //                  "FROM Contacts_MST WHERE  sContactType = '" + ContactType.Hospital.ToString() + "'  AND nContactID = " + ContactID + " AND ISNULl(nClinicID,1)=" + ClinicID + " AND ISNULL(bIsBlocked,0)= 0";

                string SqlQuery = " SELECT     Contacts_MST.sName, Contacts_MST.sContact, Contacts_MST.sAddressLine1, Contacts_MST.sAddressLine2, Contacts_MST.sCity,  " +
                      " Contacts_MST.sState, Contacts_MST.sZIP, Contacts_MST.sPhone, Contacts_MST.sFax, Contacts_MST.sEmail, Contacts_MST.sURL,  " +
                      " Contacts_MST.sMobile, Contacts_MST.sPager" +
                      " FROM Contacts_MST " +
                      " WHERE (Contacts_MST.sContactType = '" + ContactType.Others.ToString() + "') AND (Contacts_MST.nContactID = " + ContactID + " ) AND (ISNULL(Contacts_MST.nClinicID, 1) = " + ClinicID + ") AND " +
                      " (ISNULL(Contacts_MST.bIsBlocked, 0) = 0)";

                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oHospital.Mobile = dt.Rows[0]["sMobile"].ToString();
                        oHospital.Pager = dt.Rows[0]["sPager"].ToString();

                        oHospital.CompanyAddress.AddrressLine1 = dt.Rows[0]["sAddressLine1"].ToString();
                        oHospital.CompanyAddress.AddrressLine2 = dt.Rows[0]["sAddressLine2"].ToString();
                        oHospital.CompanyAddress.City = dt.Rows[0]["sCity"].ToString();
                        oHospital.CompanyAddress.State = dt.Rows[0]["sState"].ToString();
                        oHospital.CompanyAddress.ZIP = dt.Rows[0]["sZip"].ToString();
                        oHospital.CompanyAddress.Phone = dt.Rows[0]["sPhone"].ToString();
                        oHospital.CompanyAddress.Fax = dt.Rows[0]["sFax"].ToString();
                        oHospital.CompanyAddress.Email = dt.Rows[0]["sEmail"].ToString();
                        oHospital.CompanyAddress.URL = dt.Rows[0]["sURL"].ToString();
                        //oHospital.HospitalNPI = dt.Rows[0]["sNPI"].ToString();
                        oHospital.Name = dt.Rows[0]["sName"].ToString();
                        oHospital.ContactName = dt.Rows[0]["sContact"].ToString();


                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oHospital;
        }

        //abhisekh 11/02/2010
        //Add to delete the Insurance Reporting Category

        public bool DeleteInsuranceReportingCategory(Int64 nContactID)
        {
            bool _Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            

            try
            {
                string _Query;
                oDB.Connect(false);
                _Query  = "DELETE FROM Contacts_InsuranceReportingCategory_MST WHERE nID = " + nContactID;
                oDB.Execute_Query(_Query);

                _Query = " DELETE FROM Contact_InsurancePlanReportingCat_Association WHERE nReportingCategoryId = " + nContactID;
                oDB.Execute_Query(_Query);
                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
            }
            return _Result;

        }
        //End abhisekh

        //Shubhangi 20091104
        //Add to delete the Insurance company

        public bool DeleteInsuranceCompany(Int64 nContactID)
        {
            bool _Result = true;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);

            try
            {
                string _Query;
                oDB.Connect(false);

                _Query = " DELETE FROM Contacts_InsuranceCompany_MST WHERE nID =  " + nContactID;
                oDB.Execute_Query(_Query);

                _Query = " DELETE FROM Contact_InsurancePlan_Association WHERE nCompanyId = " + nContactID;
                oDB.Execute_Query(_Query);

                oDB.Disconnect();

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                _Result = false;
            }
            finally
            {
                if (oDB != null)
                { oDB.Dispose(); oDB = null; }
            }
            return _Result;

        }
        //End Shubhangi
        public bool IscontactAssignToPatient(Int64 contactid)
        {
            bool _result = false;

            DataTable dt = null;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {

                string SqlQuery = "SELECT nContactId FROM Patient_DTL WHERE nContactId = " + contactid;

                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        _result = true;
                    }
                }

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                dt.Dispose();
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool Block(Int64 contactid, string ContactType)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "UPDATE Contacts_MST SET bIsBlocked = 1 WHERE nContactId =" + contactid;
                int _intresult = 0;
                _intresult = oDB.Execute_Query(_sqlQuery);
                if (_intresult > 0)
                {
                    if (ContactType == gloPMContacts.ContactType.Physician.ToString())
                    {
                        _sqlQuery = "DELETE  Contacts_Physician_DTL  WHERE nContactId =" + contactid + " AND nClinicID = " + _ClinicID + " ";
                        _intresult = oDB.Execute_Query(_sqlQuery);

                        _sqlQuery = "DELETE  ReferringProvider_ID_Qualifiers  WHERE nProviderID =" + contactid + " AND nClinicID = " + _ClinicID + " ";
                        _intresult = oDB.Execute_Query(_sqlQuery);

                        _sqlQuery = "DELETE  Contacts_Detail  WHERE nContactId =" + contactid + " ";
                        _intresult = oDB.Execute_Query(_sqlQuery);
                    }
                    if (ContactType == gloPMContacts.ContactType.Insurance.ToString())
                    {
                        _sqlQuery = "DELETE  Contacts_Insurance_DTL  WHERE nContactId =" + contactid + " AND nClinicID = " + _ClinicID + " ";
                        _intresult = oDB.Execute_Query(_sqlQuery);
                    }
                    _intresult = oDB.Execute_Query(_sqlQuery);
                    if (_intresult > 0)
                    {
                        _result = true;
                    }
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }

        public bool UnBlock(Int64 contactid, string ContactType)
        {
            bool _result = false;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            oDB.Connect(false);
            try
            {
                string _sqlQuery = "UPDATE Contacts_MST SET bIsBlocked = 0 WHERE nContactId =" + contactid;
                int _intresult = 0;
                _intresult = oDB.Execute_Query(_sqlQuery);
                //if (_intresult > 0)
                //{
                //    if (ContactType == gloPMContacts.ContactType.Physician.ToString())
                //    {
                //        _sqlQuery = "DELETE  Contacts_Physician_DTL  WHERE nContactId =" + contactid + " AND nClinicID = " + _ClinicID + " ";
                //        _intresult = oDB.Execute_Query(_sqlQuery);

                //        _sqlQuery = "DELETE  Contacts_Detail  WHERE nContactId =" + contactid + " ";
                //        _intresult = oDB.Execute_Query(_sqlQuery);
                //    }
                //    if (ContactType == gloPMContacts.ContactType.Insurance.ToString())
                //    {
                //        _sqlQuery = "DELETE  Contacts_Insurance_DTL  WHERE nContactId =" + contactid + " AND nClinicID = " + _ClinicID + " ";
                //        _intresult = oDB.Execute_Query(_sqlQuery);
                //    }
                //    _intresult = oDB.Execute_Query(_sqlQuery);
                //    if (_intresult > 0)
                //    {
                //        _result = true;
                //    }
                //}
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPMS", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _result;
        }
        //public bool AddNotes(PatientNotes oPatientNotes)
        //{
        //    Int64 _result = 0;
        //    String strSql = "";
        //    Int64 _ID = oPatientNotes.nPNotesID;
        //    gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
        //    gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

        //    try
        //    {
        //        oDB.Connect(false);
        //        if (_ID == 0)
        //        {
        //            strSql = "Select ISNULL(MAX(nPNotesID),0) +1 FROM Patient_Notes ";
        //        _ID = Convert.ToInt64(oDB.ExecuteScalar_Query(strSql));
        //        }
        //        if (_ID == 0)
        //        {
        //            strSql = "delete  FROM Patient_Notes where nPNotesID = '" + _ID + "' ";
        //            oDB.Execute_Query(strSql);
        //        }
        //        strSql = "INSERT INTO Patient_Notes (nPNotesID, nPatientID, nDate, nTime, sNotes, nNotesType)"
        //                 + "VALUES(" + _ID + "," + oPatientNotes.nPatientID + "," + oPatientNotes.nDate + "," + oPatientNotes.nTime+",'"+oPatientNotes.sNotes+"',"+oPatientNotes.nNotesType.GetHashCode()+")";
        //      _result = Convert.ToInt64(oDB.Execute_Query(strSql));

        //        if (_result > 0)
        //            return true;
        //        else
        //            return false;


        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

        //    }
        //    return false;
        //}

        public Int64 AddNote(PatientNotes oPatientNotes)
        {
            Int64 _result = 0;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            oDB.Connect(false);

            try
            {

                oDBParameters.Clear();
                object _intresult = 0;
                oDBParameters.Add("@nPNotesID", oPatientNotes.nPNotesID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", oPatientNotes.nPatientID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nDate", oPatientNotes.nDate, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nTime", oPatientNotes.nTime, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sNotes", oPatientNotes.sNotes, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@nNotesType", oPatientNotes.nNotesType.GetHashCode(), System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", _ClinicID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                int result = oDB.Execute("gsp_INUP_Patient_Notes", oDBParameters, out  _intresult);


                if (_intresult != null)
                {
                    if (_intresult.ToString().Trim() != "")
                    {
                        if (Convert.ToInt64(_intresult) > 0)
                        {
                            _result = Convert.ToInt64(_intresult);
                        }
                    }
                }
            }

            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDBParameters.Dispose();
                oDB.Dispose();
            }
            return _result;
        }
        #endregion


        public gloPMContacts.PlanHold SetHoldInfo(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            PlanHold oPlanHold = new PlanHold();
            DataTable dt;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT nContactID,nInsCompanyID,bIsHold,sHoldReason,dtHoldDateTime,nHoldUserID,nUnholdUserID,dtUnholdDateTime FROM BL_INSURANCE_PLANHOLD WHERE nContactID = " + ContactID;


                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        oPlanHold = new PlanHold();
                        oPlanHold.ContactID = Convert.ToInt64(dt.Rows[0]["nContactID"]);
                        oPlanHold.HoldDateTime = Convert.ToDateTime(dt.Rows[0]["dtHoldDateTime"]);
                        oPlanHold.HoldModDateTime = Convert.ToDateTime(dt.Rows[0]["dtUnholdDateTime"]);
                        oPlanHold.HoldReason = Convert.ToString(dt.Rows[0]["sHoldReason"]);
                        oPlanHold.HoldUserID = Convert.ToInt64(dt.Rows[0]["nHoldUserID"]);
                        oPlanHold.InsCompanyID = Convert.ToInt64(dt.Rows[0]["nInsCompanyID"]);
                        oPlanHold.IsHold = Convert.ToBoolean(dt.Rows[0]["bIsHold"]);
                        oPlanHold.UnHoldDateTime = Convert.ToDateTime(dt.Rows[0]["dtUnholdDateTime"]);
                        oPlanHold.UnHoldUserID = Convert.ToInt64(dt.Rows[0]["nUnholdUserID"]);
                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oPlanHold;
        }

        public gloPMContacts.PlanCorrectedReplacement SetCorrectedReplacementInfo(Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            PlanCorrectedReplacement oCorrectedReplacement = new PlanCorrectedReplacement();
            DataTable dtCorrectedReplacement;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT nContactID,nInsCompanyID,bIsCorrectRplmnt,dtCreatedDateTime,nCreatedUserID FROM BL_CorrectedReplacement_Plan WHERE nContactID = " + ContactID;


                oDB.Retrive_Query(SqlQuery, out  dtCorrectedReplacement);
                if (dtCorrectedReplacement != null)
                {
                    if (dtCorrectedReplacement.Rows.Count > 0)
                    {
                        //oCorrectedReplacement = new PlanCorrectedReplacement();
                        oCorrectedReplacement.nContactID = Convert.ToInt64(dtCorrectedReplacement.Rows[0]["nContactID"]);
                        oCorrectedReplacement.nInsCompanyID = Convert.ToInt64(dtCorrectedReplacement.Rows[0]["nInsCompanyID"]);
                        oCorrectedReplacement.dtCreatedDateTime = Convert.ToDateTime(dtCorrectedReplacement.Rows[0]["dtCreatedDateTime"]);
                        oCorrectedReplacement.bIsCorrectedReplacement = Convert.ToBoolean(dtCorrectedReplacement.Rows[0]["bIsCorrectRplmnt"]);
                        oCorrectedReplacement.nCreatedUserID = Convert.ToInt64(dtCorrectedReplacement.Rows[0]["nCreatedUserID"]); 
                    }
                }
                dtCorrectedReplacement.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oCorrectedReplacement;
        }


        public PatientNotes GetnotesHistory(Int64 PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            PatientNotes oPatientNotes = new PatientNotes();
            DataTable dt;
            try
            {
                oDB.Connect(false);

                string SqlQuery = "SELECT nPNotesID,nDate, nTime, sNotes, nNotesType " +
                                  "FROM Patient_Notes WHERE  nPatientID  = " + PatientID + " AND nClinicID=" + ClinicID + " ";


                oDB.Retrive_Query(SqlQuery, out  dt);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        oPatientNotes.nPNotesID = Convert.ToInt64(dt.Rows[0]["nPNotesID"]);
                        oPatientNotes.nDate = Convert.ToInt64(dt.Rows[0]["nDate"]);
                        oPatientNotes.nTime = Convert.ToInt32(dt.Rows[0]["nTime"]);
                        oPatientNotes.sNotes = dt.Rows[0]["sNotes"].ToString();
                        oPatientNotes.nNotesType = (TypeOfNotes)Convert.ToInt64(dt.Rows[0]["nNotesType"]);

                    }
                }
                dt.Dispose();

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
                System.Windows.Forms.MessageBox.Show(DBErr.ToString(), "gloPM", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return oPatientNotes;


        }


        internal bool IsPatientInsurance(long ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Boolean _Result = false;
            string SqlQuery = null;
            Object NoOfRec = null;
            //DataTable dt;
            try
            {
                oDB.Connect(false);

                SqlQuery = "SELECT COUNT(*) FROM  PatientInsurance_DTL WHERE nContactID = " + ContactID;
                NoOfRec = oDB.ExecuteScalar_Query(SqlQuery);
                if (Convert.ToInt64(NoOfRec) > 0)
                {
                    _Result = true;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                SqlQuery = null;
                NoOfRec = null;
            }
            return _Result;
        }

        internal void ModifyPatientInsurance(Insurance oInsurance)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string SqlQuery = null;
            try
            {
                oDB.Connect(false);

                SqlQuery = "UPDATE PatientInsurance_DTL "
                + " SET  sInsuranceName = '" + oInsurance.Name.Replace("'", "''") + "',sAddressLine1 = '" + oInsurance.CompanyAddress.AddrressLine1.Replace("'", "''") + "',sAddressLine2 = '" + oInsurance.CompanyAddress.AddrressLine2.Replace("'", "''") + "', "
                + " 	 sCity = '" + oInsurance.CompanyAddress.City.Replace("'", "''") + "', sState = '" + oInsurance.CompanyAddress.State.Replace("'", "''") + "', sZIP = '" + oInsurance.CompanyAddress.ZIP.Replace("'", "''") + "', "
                + " 	 sInsurancePhone = '" + oInsurance.CompanyAddress.Phone.Replace("'", "''") + "', sFax = '" + oInsurance.CompanyAddress.Fax.Replace("'", "''") + "', sEmail = '" + oInsurance.CompanyAddress.Email.Replace("'", "''") + "', "
                + " 	 sURL = '" + oInsurance.CompanyAddress.URL.Replace("'", "''") + "', sInsuranceTypeCode = '" + oInsurance.InsuranceTypeCode.Replace("'", "''") + "',sInsuranceTypeDesc = '" + oInsurance.InsuranceTypeDesc.Replace("'", "''") + "', "
                + " 	 sPayerID = '" + oInsurance.sPayerID.Replace("'", "''") + "', bAccessAssignment = '" + oInsurance.bAccessAssignment + "', bStatementToPatient = '" + oInsurance.bStatementToPatient + "', "
                + " 	 bMedigap = '" + oInsurance.bMedigap + "', bReferringIDInBox19 = '" + oInsurance.bReferringIDInBox19 + "',bNameOfacilityinBox33 = '" + oInsurance.bNameOfacilityinBox33 + "', "
                + " 	 bDoNotPrintFacility = '" + oInsurance.bDoNotPrintFacility + "', b1stPointer = '" + oInsurance.b1stPointer + "', bBox31Blank = '" + oInsurance.bBox31Blank + "', "
                + " 	 bShowPayment = '" + oInsurance.bShowPayment + "', nTypeOBilling = " + oInsurance.nTypeOBilling.GetHashCode() + ", nClearingHouse = " + oInsurance.nClearingHouse + ",  "
                + " 	 bIsClaims = '" + oInsurance.bIsClaims + "', bIsRemittanceAdvice = '" + oInsurance.bIsRemittanceAdvice + "', bIsRealTimeEligibility = '" + oInsurance.bIsRealTimeEligibility + "',  "
                + " 	 bIsElectronicCOB = '" + oInsurance.bIsElectronicCOB + "', bIsRealTimeClaimStatus = '" + oInsurance.bIsRealTimeClaimStatus + "', bIsEnrollmentRequired = '" + oInsurance.bIsEnrollmentRequired + "',  "
                + " 	 sPayerPhone = '" + oInsurance.sPayerPhone.Replace("'", "''") + "', sWebsite = '" + oInsurance.sWebsite.Replace("'", "''") + "', sServicingState = '" + oInsurance.sServicingState.Replace("'", "''") + "',  "
                + " 	 sComments = '" + oInsurance.sComments.Replace("'", "''") + "', sPayerPhoneExtn = '" + oInsurance.sPayerPhoneExtn.Replace("'", "''") + "', bNotesInBox19 ='" + oInsurance.bNotesInBox19 + "', sOfficeID='" + oInsurance.OfficeID.Replace("'", "''") + "'"
                + " WHERE nContactID = " + oInsurance.ContactID;

                oDB.Execute_Query(SqlQuery);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                SqlQuery = null;
            }
        }

        internal bool IsPatientPhysician(long ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Boolean _Result = false;
            string SqlQuery = null;
            Object NoOfRec = null;
          //  DataTable dt;
            try
            {
                oDB.Connect(false);

                SqlQuery = "SELECT COUNT(*) FROM  Patient_DTL WHERE nContactID = " + ContactID;
                NoOfRec = oDB.ExecuteScalar_Query(SqlQuery);
                if (Convert.ToInt64(NoOfRec) > 0)
                {
                    _Result = true;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                SqlQuery = null;
                NoOfRec = null;
            }
            return _Result;
        }

        internal bool IsPatientPharmacy(long ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            Boolean _Result = false;
            string SqlQuery = null;
            Object NoOfRec = null;
            //  DataTable dt;
            try
            {
                oDB.Connect(false);

                SqlQuery = "SELECT COUNT(*) FROM  Patient_DTL WHERE nContactID = " + ContactID;
                NoOfRec = oDB.ExecuteScalar_Query(SqlQuery);
                if (Convert.ToInt64(NoOfRec) > 0)
                {
                    _Result = true;
                }
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                SqlQuery = null;
                NoOfRec = null;
            }
            return _Result;
        }
        internal void ModifyPatientPhysician(Physician oPhysician)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string SqlQuery = null;
            try
            {
                oDB.Connect(false);
                           

                SqlQuery = "UPDATE Patient_DTL SET "
               + " sName = '" + oPhysician.Name.Replace("'", "''") + "', sContact = '" + oPhysician.ContactName.Replace("'", "''") + "', sAddressLine1 = '" + oPhysician.CompanyAddress.AddrressLine1.Replace("'", "''") + "', sAddressLine2 = '" + oPhysician.CompanyAddress.AddrressLine2.Replace("'", "''") + "', "
               + " sCity = '" + oPhysician.CompanyAddress.City.Replace("'", "''") + "', sState = '" + oPhysician.CompanyAddress.State.Replace("'", "''") + "', sZIP = '" + oPhysician.CompanyAddress.ZIP.Replace("'", "''") + "', sPhone = '" + oPhysician.CompanyAddress.Phone.Replace("'", "''") + "', sFax = '" + oPhysician.CompanyAddress.Fax.Replace("'", "''") + "', "
               + " sEmail = '" + oPhysician.CompanyAddress.Email.Replace("'", "''") + "', sURL = '" + oPhysician.CompanyAddress.URL.Replace("'", "''") + "', sMobile = '" + oPhysician.Mobile.Replace("'", "''") + "', sPager = '" + oPhysician.Pager.Replace("'", "''") + "', sNotes = '" + oPhysician.Notes.Replace("'", "''") + "',"
               + " sFirstName = '" + oPhysician.FirstName.Replace("'", "''") + "', sMiddleName = '" + oPhysician.MiddleName.Replace("'", "''") + "', sLastName = '" + oPhysician.LastName.Replace("'", "''") + "', sGender = '" + oPhysician.Gender.Replace("'", "''") + "',"
               + " sTaxonomy = '" + oPhysician.Taxonomy.Replace("'", "''") + "', sTaxonomyDesc = '" + oPhysician.TaxonomyDesc.Replace("'", "''") + "', sTaxID = '" + oPhysician.TaxID.Replace("'", "''") + "', sUPIN = '" + oPhysician.UPIN.Replace("'", "''") + "',"
               + " sNPI = '" + oPhysician.NPI.Replace("'", "''") + "', sHospitalAffiliation = '" + oPhysician.HospitalAffiliation.Replace("'", "''") + "', sExternalCode = '" + oPhysician.ExternalCode.Replace("'", "''") + "', "
               + " sDegree = '" + oPhysician.Degree.Replace("'", "''") + "',"
               + " sPrefix = '" + oPhysician.Prefix.Replace("'", "''") + "'"
               + " WHERE nContactId = " + oPhysician.ContactID + " AND nClinicID = " + ClinicID;

                oDB.Execute_Query(SqlQuery);
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                SqlQuery = null;
            }
        }

        internal void ModifyPatientPharmacy(Pharmacy oPharmacy)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDB.Connect(false);

                object _intresult = 0;
                oDBParameters.Add("@nContactId", oPharmacy.ContactID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@sName", oPharmacy.Name, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sContact", oPharmacy.ContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine1", oPharmacy.CompanyAddress.AddrressLine1, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sAddressLine2", oPharmacy.CompanyAddress.AddrressLine2, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sCity", oPharmacy.CompanyAddress.City, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sState", oPharmacy.CompanyAddress.State, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sZIP", oPharmacy.CompanyAddress.ZIP, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPhone", oPharmacy.CompanyAddress.Phone, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sFax", oPharmacy.CompanyAddress.Fax, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sEmail", oPharmacy.CompanyAddress.Email, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sURL", oPharmacy.CompanyAddress.URL, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                //oDBParameters.Add("@ContactType", oPharmacy.ContactType, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                oDBParameters.Add("@sPager", oPharmacy.Pager, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                
                int result = oDB.Execute("gsp_UpdatePatientPharmacy", oDBParameters);

                
                

            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                DBErr.ERROR_Log(DBErr.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
            }
        }
        //Added by Mukesh on 17-Sep-2009 for Remove Duplicate Contact
        // ContactTypes : Insurance, Physician, Pharmacy, Hospital
        // Remove Duplicate Insurance : Tables affected (Contacts_MST,Contacts_Insurance_DTL,PatientInsurance_DTL)        
        // Remove Duplicate Pharmacy : Tables affected (Contacts_MST,Contacts_Pharmacy_DTL,Patient_DTL)        
        // Remove Duplicate Physician : Tables affected (Contacts_MST,Contacts_Physician_DTL,Patient_DTL)        
        // Remove Duplicate Hospital : Tables affected (Contacts_MST,Contacts_Hospital_DTL)        
        public bool RemoveDuplicateContacts(ContactType ContactTypes)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = null;
            oDB.Connect(false);
            DataTable dt;
            string SqlQuery = "";
            try
            {
                switch (ContactTypes)
                {
                    case ContactType.Insurance:
                        #region " Insurance "
                        // Get List of Duplicate Insurance Contact 
                        SqlQuery = "Select IsNull(sName,'') as sName,Min(nContactID) as nContactID,Count(sName) as nCount from Contacts_MST where sContactType = '" + ContactType.Insurance.ToString() + "' Group By sName Having Count(sName) > 1 ";
                        oDB.Retrive_Query(SqlQuery, out  dt);
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                String sContactName = Convert.ToString(dr["sName"]);
                                Int64 nContactID = Convert.ToInt64(dr["nContactID"]);
                                if (sContactName != "")
                                {
                                    // Delete Duplicate Insurance and update Contacts_MST,Contacts_Insurance_DTL and PatientInsurance_DTL Tables
                                    object _intresult = 0;
                                    oDBParameters = new gloDatabaseLayer.DBParameters();
                                    oDBParameters.Add("@ContactID", nContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@ContactName", sContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                                    int result = oDB.Execute("CO_RemoveDuplicateInsurance", oDBParameters, out  _intresult);
                                    if (_intresult == null)
                                    {
                                        if (Convert.ToInt64(_intresult) == 0)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                    case ContactType.Pharmacy:
                        #region " Pharmacy "
                        // Get List of Duplicate Pharmacy Contact 
                        SqlQuery = "Select IsNull(sName,'') as sName,Min(nContactID) as nContactID,Count(sName) as nCount from Contacts_MST where sContactType = '" + ContactType.Pharmacy.ToString() + "' Group By sName Having Count(sName) > 1 ";
                        oDB.Retrive_Query(SqlQuery, out  dt);
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                String sContactName = Convert.ToString(dr["sName"]);
                                Int64 nContactID = Convert.ToInt64(dr["nContactID"]);
                                if (sContactName != "")
                                {
                                    // Delete Duplicate Pharmacy and update Contacts_MST,Contacts_Pharmacy_DTL and Patient_DTL Tables
                                    object _intresult = 0;
                                    oDBParameters = new gloDatabaseLayer.DBParameters();
                                    oDBParameters.Add("@ContactID", nContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@ContactName", sContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                                    int result = oDB.Execute("CO_RemoveDuplicatePharmacy", oDBParameters, out  _intresult);
                                    if (_intresult == null)
                                    {
                                        if (Convert.ToInt64(_intresult) == 0)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                       // break;
                    case ContactType.Physician:
                        #region " Physician "
                        // Get List of Duplicate Physician Contact 
                        SqlQuery = "select ISNULL(sFirstName,'') + ' ' + ISNULL(sMiddleName,'') + ' ' + ISNULL(sLastName,'') as sName,count(nContactID) as nCount,Min(nContactID) as nContactID " +
                                   " from Contacts_MST where sContactType='" + ContactType.Physician.ToString() + "' group by ISNULL(sFirstName,'') + ' ' + ISNULL(sMiddleName,'') + ' ' + ISNULL(sLastName,'') " +
                                   " having count(nContactID) > 1";
                        oDB.Retrive_Query(SqlQuery, out  dt);
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                String sContactName = Convert.ToString(dr["sName"]);
                                Int64 nContactID = Convert.ToInt64(dr["nContactID"]);
                                if (sContactName != "")
                                {
                                    // Delete Duplicate Physician and update Contacts_MST,Contacts_Physician_DTL and Patient_DTL Tables
                                    object _intresult = 0;
                                    oDBParameters = new gloDatabaseLayer.DBParameters();
                                    oDBParameters.Add("@ContactID", nContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@ContactName", sContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                                    int result = oDB.Execute("CO_RemoveDuplicatePhysician", oDBParameters, out  _intresult);
                                    if (_intresult == null)
                                    {
                                        if (Convert.ToInt64(_intresult) == 0)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        #endregion
                       break;
                    case ContactType.Hospital:
                        #region " Hospital "
                        // Get List of Duplicate Hospital Contact 
                        SqlQuery = "Select IsNull(sName,'') as sName,Min(nContactID) as nContactID,Count(sName) as nCount from Contacts_MST where sContactType = '" + ContactType.Hospital.ToString() + "' Group By sName Having Count(sName) > 1 ";
                        oDB.Retrive_Query(SqlQuery, out  dt);
                        if (dt != null)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                String sContactName = Convert.ToString(dr["sName"]);
                                Int64 nContactID = Convert.ToInt64(dr["nContactID"]);
                                if (sContactName != "")
                                {
                                    // Delete Duplicate Hospital and update Contacts_MST and Contacts_Hospital_DTL Tables
                                    object _intresult = 0;
                                    oDBParameters = new gloDatabaseLayer.DBParameters();
                                    oDBParameters.Add("@ContactID", nContactID, System.Data.ParameterDirection.InputOutput, System.Data.SqlDbType.BigInt);
                                    oDBParameters.Add("@ContactName", sContactName, System.Data.ParameterDirection.Input, System.Data.SqlDbType.VarChar);
                                    int result = oDB.Execute("CO_RemoveDuplicateHospital", oDBParameters, out  _intresult);
                                    if (_intresult == null)
                                    {
                                        if (Convert.ToInt64(_intresult) == 0)
                                        {
                                            return false;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                        #endregion
                       // break;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oDBParameters != null) { oDBParameters.Dispose(); oDBParameters = null; }
                SqlQuery = null;
            }
            return true;
        }

        #region "UB04 "

        public bool IsenableUB04(Int64 ClinicID)
        {
            bool _Isenable = false;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _sqlQuery = "";
            object oUB04Enable = new object();

            try
            {
                oDB.Connect(false);

                //nTransactionID, nTransactionDetailID, nTransactionLineNo, nStatusDate, nStatusTime, sStatusNote, nClinicID, nStatusID
                if (ClinicID > 0)
                {
                    _sqlQuery = " select sSettingsValue from Settings where  sSettingsName='UB04_EnableBilling' and nClinicID=" + ClinicID + "";

                    oUB04Enable = oDB.ExecuteScalar_Query(_sqlQuery);
                    if (oUB04Enable != null && Convert.ToString(oUB04Enable) != "")
                    {
                        if (Convert.ToString(oUB04Enable).ToUpper() == "TRUE")
                            _Isenable = true;
                        else
                            _Isenable = false;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                ex = null;
                _Isenable = false;
            }
            finally
            {
                if (oDB != null) { oDB.Disconnect(); oDB.Dispose(); }
                if (oUB04Enable != null) { oUB04Enable = null; }
                _sqlQuery = null;
            }
            return _Isenable;

        }

        #endregion
    }

    public class PatientNotes
    {

        #region "Constructor & Distructor"

        public PatientNotes()
        {
        }

        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

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

        ~PatientNotes()
        {
            Dispose(false);
        }

        #endregion

        #region "Variables"

        private Int64 _nPNotesID = 0;
        private Int64 _nPatientID = 0;
        private Int64 _nDate = 0;
        private Int32 _nTime = 0;
        private string _sNotes = "";
        private TypeOfNotes _nNotesType = TypeOfNotes.none;

        #endregion

        #region "Properties "
        public Int64 nPNotesID
        {
            get { return _nPNotesID; }
            set { _nPNotesID = value; }
        }
        public Int64 nPatientID
        {
            get { return _nPatientID; }
            set { _nPatientID = value; }
        }
        public Int64 nDate
        {
            get { return _nDate; }
            set { _nDate = value; }
        }
        public Int32 nTime
        {
            get { return _nTime; }
            set { _nTime = value; }
        }
        public string sNotes
        {
            get { return _sNotes; }
            set { _sNotes = value; }
        }
        public TypeOfNotes nNotesType
        {
            get { return _nNotesType; }
            set { _nNotesType = value; }
        }
        #endregion





    }

    public class PlanHold:Contact
    {

        #region "Constructor & Destructor"

        public PlanHold()
        {
        }

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~PlanHold()
        {
            Dispose(false);
        }

        #endregion

        #region Declaration

        private Int64 _nContactID = 0;
        private Int64 _nInsCompanyID = 0;
        private bool _bIsHold = false;
        private Boolean _bHoldModified = false;
        private string _sHoldReason = "";
        private DateTime _dtHoldDateTime =DateTime.Now;
        private Int64 _nHoldUserID = 0;
        private DateTime _dtUnHoldDateTime = DateTime.Now;
        private DateTime _dtHoldModDateTime = DateTime.Now;
        private Int64 _nUnHoldUserID = 0;

        #endregion

        #region Properties

        public DateTime HoldModDateTime
        {
            get { return _dtHoldModDateTime; }
            set { _dtHoldModDateTime = value; }
        }
        public Int64 ContactID
        {
            get { return _nContactID; }
            set { _nContactID = value; }
        }

        public Int64 InsCompanyID
        {
            get { return _nInsCompanyID; }
            set { _nInsCompanyID = value; }
        }

        public bool IsHold
        {
            get { return _bIsHold; }
            set { _bIsHold = value; }
        }

        public Boolean HoldModified
        {
            get { return _bHoldModified; }
            set { _bHoldModified = value; }
        }

        public string HoldReason
        {
            get { return _sHoldReason; }
            set { _sHoldReason = value; }
        }

        public DateTime HoldDateTime
        {
            get { return _dtHoldDateTime; }
            set { _dtHoldDateTime = value; }
        }

        public Int64 HoldUserID
        {
            get { return _nHoldUserID; }
            set { _nHoldUserID = value; }
        }

        public DateTime UnHoldDateTime
        {
            get { return _dtUnHoldDateTime; }
            set { _dtUnHoldDateTime = value; }
        }

        public Int64 UnHoldUserID
        {
            get { return _nUnHoldUserID; }
            set { _nUnHoldUserID = value; }
        }

        #endregion

    }

    public class PlanCorrectedReplacement : Contact
    {

        #region "Constructor & Destructor"

        public PlanCorrectedReplacement()
        {
        }

        private bool disposed = false;

        public void Disposer()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected override void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
            base.Dispose(disposing);
        }

        ~PlanCorrectedReplacement()
        {
            Dispose(false);
        }

        #endregion


        #region Properties

        public Int64 nContactID { get; set; }
        public Int64 nInsCompanyID { get; set; }
        public Boolean bIsCorrectedReplacement { get; set; }
        public DateTime  dtCreatedDateTime { get; set; }
        public Int64 nCreatedUserID { get; set; }
        public DateTime dtModifyDateTime { get; set; }

       

        #endregion

    }

}
