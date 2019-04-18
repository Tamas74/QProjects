using System;
using System.Collections.Generic;
using System.Text;


namespace gloRxHub
{
    // Please Add property procedures for Patient
    //Patient First Name, Patient Last Name ,Patient Gender ,Patient DOB
    public class ClsPatient : IDisposable
    {
        #region "Private and Public variables"
        private Int64 nPatientID = 0;
        private string _patientfirstName = "";
        private string _patientMiddleName = "";
        private string _patientlastName = "";
        private string sPatientSuffix = "";
        private string sPatientPrefix = "";
        private string _patientgender = "";
        private DateTime _patientDOB;
        private String _patientSSN = "";
        private ClsMessageHeader objMsgHeader = new ClsMessageHeader();
        private ClsResponse objResponse = new ClsResponse();

        private ClsRxH_271Master oRxH271Master = new ClsRxH_271Master();
        private ClsPharmacy oPharmacy = new ClsPharmacy();
        private ClsProvider oProvider = new ClsProvider();
        List<ClsSubscriber> objSubscriber = new List<ClsSubscriber>();
        List<ClsDrug> objDrug = new List<ClsDrug>();
        List<ClsMedication> objMedications = new List<ClsMedication>();
        private ClsAddress oAddress = new ClsAddress();
        private ClsContactDetails oContact = new ClsContactDetails();


        private bool disposedValue = false;

        public DateTime TransactionDate { get; set; }

        #endregion "Private abd Public variables
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
        #region "Property Procedures for patients"

        public ClsPharmacy Pharmacy
        {
            get { return oPharmacy; }
            set { oPharmacy = value; }

        }
        public List<ClsSubscriber> Subscriber
        {
            get
            {
                return objSubscriber;
            }
            set
            {
                objSubscriber = value;
            }
        }

        public List<ClsDrug> Drug
        {
            get { return objDrug; }
            set { objDrug = value; }
        }
        public List<ClsMedication> Medication
        {
            get
            {
                if (objMedications == null)
                {
                    objMedications = new List<ClsMedication>();
                }
                return objMedications;
            }
            set { objMedications = value; }
        }

        public ClsRxH_271Master RxH271Master
        {
            get
            {
                return oRxH271Master;
            }
            set { oRxH271Master = value; }
        }
        public ClsMessageHeader MessageHeader
        {
            get { return objMsgHeader; }
            set { objMsgHeader = value; }
        }
        public ClsResponse Response
        {
            get { return objResponse; }
            set { objResponse = value; }
        }
        public Int64 PatientID
        {
            get { return nPatientID; }
            set { nPatientID = value; }

        }

        public string FirstName
        {
            get { return _patientfirstName; }
            set { _patientfirstName = value; }
        }

        public string MiddleName
        {
            get { return _patientMiddleName; }
            set { _patientMiddleName = value; }
        }

        public string LastName
        {
            get { return _patientlastName; }
            set { _patientlastName = value; }
        }
        public string Suffix
        {
            get { return sPatientSuffix; }
            set { sPatientSuffix = value; }

        }
        public string Prefix
        {
            get { return sPatientPrefix; }
            set { sPatientPrefix = value; }

        }

        public string Gender
        {
            get { return _patientgender; }
            set { _patientgender = value; }
        }

        public DateTime DOB
        {
            get { return _patientDOB; }
            set { _patientDOB = value; }
        }

        public String SSN
        {
            get { return _patientSSN; }
            set { _patientSSN = value; }
        }

        public ClsAddress PatientAddress
        {
            get { return oAddress; }
            set { oAddress = value; }
        }

        public ClsContactDetails PatientContact
        {
            get { return oContact; }
            set { oContact = value; }
        }

        public ClsProvider Provider
        {
            get { return oProvider; }
            set { oProvider = value; }
        }

        #endregion Property Procedures

        public override bool Equals(object obj)
        {
            bool bReturned = false;

            if (obj != null && obj is ClsPatient)            
            {
                Int32 nCompare = -1;

                ClsPatient toCompare = (ClsPatient)obj;

                nCompare = string.Compare(this.FirstName, toCompare.FirstName);

                if (nCompare == 0) { nCompare = string.Compare(this.MiddleName, toCompare.MiddleName); }

                if (nCompare == 0) { nCompare = string.Compare(this.LastName, toCompare.LastName); }

                if (nCompare == 0) { nCompare = string.Compare(this.Gender, toCompare.Gender); }

                if (nCompare == 0) { nCompare = DateTime.Compare(this.DOB, toCompare.DOB); }

                if (nCompare == 0) { nCompare = string.Compare(this.SSN, toCompare.SSN); }

                bReturned = this.Provider.Equals(toCompare.Provider);

                bReturned = (nCompare == 0);               
            }

            return bReturned;
        }

    }

    // Provider First Name ,Provider Last Name ,Provider Address, Provider Contact Details
    public class ClsProvider : IDisposable
    {

        #region " Private & Public variables "

        private string _providerFirstName = "";
        private string _providerLastName = "";
        private string _providerMiddleName = "";
        private string _providergender;
        private string _providerSSN = "";
        private Int64 mProviderID = 0;
        private string mDEA = "";
        private string sClinicName = "";
        private string sAMASpecialtyCode = "";
        private string sOtherSpecialtyCode = "";
        private string sOtherSpecialtyQualifier = "";
        private string sPrefix = "";
        private string sSuffix = "";
        private string sNPI = "";




        private ClsPresciberAgent objPresciberAgent = new ClsPresciberAgent();
        private ClsAddress oAddress = new ClsAddress();
        private ClsContactDetails oContact = new ClsContactDetails();

        private bool disposedValue = false;

        #endregion " Private & Public variables "
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
        #region " Property Procedures for Provider
        public Int64 ProviderID
        {
            get { return mProviderID; }
            set { mProviderID = value; }
        }
        public string Gender
        {
            get { return _providergender; }
            set { _providergender = value; }
        }
        public string ProviderDEA
        {
            get { return mDEA; }
            set { mDEA = value; }
        }
        public string ProviderNPI
        {
            get { return sNPI; }
            set { sNPI = value; }
        }
        public string ProviderFirstName
        {
            get { return _providerFirstName; }
            set { _providerFirstName = value; }
        }

        public string ProviderLastName
        {
            get { return _providerLastName; }
            set { _providerLastName = value; }
        }
        public string ProviderMiddleName
        {
            get { return _providerMiddleName; }
            set { _providerMiddleName = value; }
        }

        public string AMASpecialtyCode
        {
            get { return sAMASpecialtyCode; }
            set { sAMASpecialtyCode = value; }
        }


        public string OtherSpecialtyCode
        {
            get { return sOtherSpecialtyCode; }
            set { sOtherSpecialtyCode = value; }
        }

        public string OtherSpecialtyQualifier
        {
            get { return sOtherSpecialtyQualifier; }
            set { sOtherSpecialtyQualifier = value; }
        }

        public string ClinicName
        {
            get { return sClinicName; }
            set { sClinicName = value; }
        }

        public string ProviderPrefix
        {
            get { return sPrefix; }
            set { sPrefix = value; }

        }

        public string ProviderSuffix
        {
            get { return sSuffix; }
            set { sSuffix = value; }

        }

        public String ProviderSSN
        {
            get { return _providerSSN; }
            set { _providerSSN = value; }
        }

        public ClsPresciberAgent PrescriberAgent
        {
            get { return objPresciberAgent; }
            set { objPresciberAgent = value; }


        }
        public ClsAddress ProviderAddress
        {
            get { return oAddress; }
            set { oAddress = value; }
        }

        public ClsContactDetails ProviderContactDtl
        {
            get { return oContact; }
            set { oContact = value; }
        }
        #endregion

        public override bool Equals(object obj)
        {
            bool bReturned = false;

            if (obj != null && obj is ClsProvider)
            {
                Int32 nCompare = -1;
                ClsProvider toCompare = (ClsProvider)obj;
                
                nCompare = string.Compare(this.ProviderFirstName, toCompare.ProviderFirstName);

                if (nCompare == 0) { nCompare = string.Compare(this.ProviderMiddleName, toCompare.ProviderMiddleName); }

                if (nCompare == 0) { nCompare = string.Compare(this.ProviderLastName, toCompare.ProviderLastName); }

                if (nCompare == 0) { nCompare = string.Compare(this.ProviderNPI, toCompare.ProviderNPI); }                

                if (nCompare == 0) { nCompare = string.Compare(this.ProviderSSN, toCompare.ProviderSSN); }
                
                if (nCompare == 0) { nCompare = string.Compare(this.ProviderDEA, toCompare.ProviderDEA); }

                if (nCompare == 0) 
                { 
                    bReturned = this.ProviderAddress.Equals(toCompare.ProviderAddress);
                    if (bReturned == false) { nCompare = -1; }
                }
                
                bReturned = (nCompare == 0);
            }

            return bReturned;
        }

    }

    //Subscriber First Name ,Subscriber Last Name , Subscriber Address , Subscriber contact details
    public class ClsSubscriber : IDisposable
    {
        #region "Private and public vaiable"
        private string _subscriberFirstName = "";
        private string _subscriberLastName = "";
        private string _SubscriberMiddleName = "";

        private DateTime _SubscriberDOB ;//GLO2011-0011225
        private string _SubscriberGender = "";//GLO2011-0011225

        private string _subscriberID = "";
        private Int64 mInsuranceID = 0;
        //private string sPayerID = "";
        //private string _subscriberAddress;
        //private Int64 _subscriberContactDtl;
        private ClsAddress oAddress = new ClsAddress();
        private ClsContactDetails oContact = new ClsContactDetails();
        private string sRelationShip = "";
        private string sInsuranceName = "";
        private bool disposedValue = false;
        #endregion "Private and public vaiable"
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

        #region "Property procedures for Subscriber"
        public string SubscriberFirstName
        {
            get { return _subscriberFirstName; }
            set { _subscriberFirstName = value; }
        }

        public string SubscriberLastName
        {
            get { return _subscriberLastName; }
            set { _subscriberLastName = value; }
        }

        public string SubscriberMiddleName
        {
            get { return _SubscriberMiddleName; }
            set { _SubscriberMiddleName = value; }
        }

           public DateTime SubscriberDOB//GLO2011-0011225
        {
            get { return _SubscriberDOB; }
            set { _SubscriberDOB = value; }
        }

        public string SubscriberGender//GLO2011-0011225
        {
            get { return _SubscriberGender; }
            set { _SubscriberGender = value; }
        }


        public string SubscriberID
        {
            get { return _subscriberID; }
            set { _subscriberID = value; }
        }

        public ClsAddress SubscriberAddress
        {
            get { return oAddress; }
            set { oAddress = value; }
        }

        public ClsContactDetails SubscriberContactDtl
        {
            get { return oContact; }
            set { oContact = value; }
        }

        public Int64 InsuranceID
        {
            get { return mInsuranceID; }
            set { mInsuranceID = value; }
        }

        public string RelationShip
        {

            get { return sRelationShip; }
            set { sRelationShip = value; }
        }

        public string InsuranceName
        {
            get { return sInsuranceName; }
            set { sInsuranceName = value; }

        }

        #endregion "Property procedures for Subscriber"

    }

    public class ClsAddress : IDisposable
    {
        #region "Private and public Variable
        private string _sAddressLine1 = "";
        private string _sAddressLine2 = "";
        private string _sCity = "";
        private string _sState = "";
        private string _sZip = "";
        private string sPlaceLocationQualifier = "";
        #endregion "Private and public Variable

        #region "Property procedures
        // IDisposable 
        private bool disposedValue = false;
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
        public string AddressLine1
        {
            get { return _sAddressLine1; }
            set { _sAddressLine1 = value; }
        }

        public string AddressLine2
        {
            get { return _sAddressLine2; }
            set { _sAddressLine2 = value; }
        }

        public string City
        {
            get { return _sCity; }
            set { _sCity = value; }
        }

        public string State
        {
            get { return _sState; }
            set { _sState = value; }
        }

        public string Zip
        {
            get { return _sZip; }
            set { _sZip = value; }
        }
        public string PlaceLocationQualifier
        {
            get { return sPlaceLocationQualifier; }
            set { sPlaceLocationQualifier = value; }

        }

        #endregion "Property procedures

        public override bool Equals(object obj)
        {
             bool bReturned = false;

            if (obj != null && obj is ClsAddress)
            {
                Int32 nCompare = -1;
                ClsAddress toCompare = (ClsAddress)obj;
                
                nCompare = string.Compare(this.AddressLine1, toCompare.AddressLine1);

                if (nCompare == 0) { nCompare = string.Compare(this.AddressLine2, toCompare.AddressLine2); }                

                if (nCompare == 0) { nCompare = string.Compare(this.State, toCompare.State); }

                if (nCompare == 0) { nCompare = string.Compare(this.City, toCompare.City); }

                if (nCompare == 0) { nCompare = string.Compare(this.Zip, toCompare.Zip); }
                
                if (nCompare == 0) { nCompare = string.Compare(this.PlaceLocationQualifier, toCompare.PlaceLocationQualifier); }
                                
                bReturned = (nCompare == 0);
            }

            return bReturned;        
        }
    }

    public class ClsContactDetails : IDisposable
    {
        #region "Private and public Variable
        private string _sPhone = "";
        private string _sPhoneQualifier = "";
        private string _sMobile = "";
        private string _sEmail = "";
        private string _sFax = "";

        #endregion "Private and public Variable
        // IDisposable 
        private bool disposedValue = false;
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
        #region "Property procedures
        public string Phone
        {
            get { return _sPhone; }
            set { _sPhone = value; }
        }
        public string PhoneQualifier
        {
            get { return _sPhoneQualifier; }
            set { _sPhoneQualifier = value; }
        }

        public string Mobile
        {
            get { return _sMobile; }
            set { _sMobile = value; }
        }

        public string Email
        {
            get { return _sEmail; }
            set { _sEmail = value; }
        }

        public string Fax
        {
            get { return _sFax; }
            set { _sFax = value; }
        }





        #endregion "Property procedures
    }

    public class ClsDrug : IDisposable
    {
        #region "Private and public Variable"

        private string sPBMSourceName = "";
        private Int64 nMedVisitId = 0;
        private string sDrugName = "";
        private string sDosage = "";
        private string sNDCCode = "";
        private string sProductCodeQualifier = "";

        private string sDosageForm = "";
        private string sStrength = "";
        private string sStrengthUnits = "";
        private string sDrugDBCode = "";
        private string sDrugDBCodeQualifier = "";
        private string sDrugQuantityQualifier = "";
        private string sDrugQuantityValue = "";
        private string sDirections = "";
        private string sNote = "";
        private string sRefillsQualifier = "";
        private string sRefillsQuantity = "";
        private string sSubstitutions = "";
        private string sWrittenDate = "";
        private string sExpirationDate = "";
        private string sEffectiveDate = "";
        private string sPeriodEnd = "";
        private string sDiagnosisClinicalInformation = "";
        private string sDiagnosisPriamryQualifier = "";
        private string sDiagnosisPriamryValue = "";
        private string sDiagnosisSecQualifier = "";
        private string sDiagnosisSecValue = "";
        private string sPriorAuthorizationQualifier = "";
        private string sPriorAuthorizationValue = "";

        

   //Columns from Prescription table to be saved in Medication table
    private string _Rx_sRefills = "";
   private string _Rx_sNotes = "";
   private string _Rx_sMethod = "";
   private bool _Rx_bMaySubstitute;
   private Int64 _Rx_nDrugID;
   private bool _Rx_blnflag;
   private string _Rx_sLotNo = "";
   private DateTime _Rx_dtExpirationdate;
  private Int64 _Rx_nProviderId;
  private string _Rx_sChiefComplaints = "";
  private string _Rx_sStatus = "";
  private string _Rx_sRxReferenceNumber = "";
  private string _Rx_sRefillQualifier = "";
  private Int64 _Rx_nPharmacyId;
  private string _Rx_sNCPDPID = "";
  private Int64 _Rx_nContactID;
  private string _Rx_sName = "";
  private string _Rx_sAddressline1 = "";
  private string _Rx_sAddressline2 = "";
  private string _Rx_sCity = "";
  private string _Rx_sState = "";
  private string _Rx_sZip = "";
  private string _Rx_sEmail = "";
  private string _Rx_sFax = "";
  private string _Rx_sPhone = "";
  private string _Rx_sServiceLevel = "";
  private string _Rx_sPrescriberNotes = "";
  private string _Rx_eRxStatus = "";
  private string _Rx_eRxStatusMessage = "";
  //Columns from Prescription table to be saved in Medication table



        private string sCodeListQualifier = "";
        private string sDaysSupply = "";
        private DateTime lastFillDate;
        private string sQuantityQualifier = "";
        private string sQuantityValue = "";
        private bool disposedValue = false;
        private ClsProvider oProvider = new ClsProvider();

        #endregion "Private and public Variable"

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


        #region "Property procedure for drugs"

        public Int64 MedVisitId
        {
            get { return nMedVisitId; }
            set { nMedVisitId = value; }

        }

        public string PBMSourceName
        {
            get { return sPBMSourceName; }
            set { sPBMSourceName = value; }

        }

        public string Rx_sRefills
        {
            get { return _Rx_sRefills; }
            set { _Rx_sRefills = value; }

        }

        public string Rx_sNotes
        {
            get { return _Rx_sNotes; }
            set { _Rx_sNotes = value; }

        }

        public string Rx_sMethod
        {
            get { return _Rx_sMethod; }
            set { _Rx_sMethod = value; }

        }

      
        public bool Rx_bMaySubstitute
        {
            get { return _Rx_bMaySubstitute; }
            set { _Rx_bMaySubstitute = value; }

        }
      
        public Int64 Rx_nDrugID
        {
            get { return _Rx_nDrugID; }
            set { _Rx_nDrugID = value; }

        }

      
        public bool Rx_blnflag
        {
            get { return _Rx_blnflag; }
            set { _Rx_blnflag = value; }

        }


        public string Rx_sLotNo
        {
            get { return _Rx_sLotNo; }
            set { _Rx_sLotNo = value; }

        }

        
        public DateTime Rx_dtExpirationdate
        {
            get { return _Rx_dtExpirationdate; }
            set { _Rx_dtExpirationdate = value; }

        }
        
        public Int64 Rx_nProviderId
        {
            get { return _Rx_nProviderId; }
            set { _Rx_nProviderId = value; }

        }

       
        public string Rx_sChiefComplaints
        {
            get { return _Rx_sChiefComplaints; }
            set { _Rx_sChiefComplaints = value; }

        }

        public string Rx_sStatus
        {
            get { return _Rx_sStatus; }
            set { _Rx_sStatus = value; }

        }

        public string Rx_sRxReferenceNumber
        {
            get { return _Rx_sRxReferenceNumber; }
            set { _Rx_sRxReferenceNumber = value; }

        }

        public string Rx_sRefillQualifier
        {
            get { return _Rx_sRefillQualifier; }
            set { _Rx_sRefillQualifier = value; }

        }

        
        public Int64 Rx_nPharmacyId
        {
            get { return _Rx_nPharmacyId; }
            set { _Rx_nPharmacyId = value; }

        }

        
        public string Rx_sNCPDPID
        {
            get { return _Rx_sNCPDPID; }
            set { _Rx_sNCPDPID = value; }

        }

     
        public Int64 Rx_nContactID
        {
            get { return _Rx_nContactID; }
            set { _Rx_nContactID = value; }

        }

        public string Rx_sName
        {
            get { return _Rx_sName; }
            set { _Rx_sName = value; }

        }

       
        public string Rx_sAddressline1
        {
            get { return _Rx_sAddressline1; }
            set { _Rx_sAddressline1 = value; }

        }

        public string Rx_sAddressline2
        {
            get { return _Rx_sAddressline2; }
            set { _Rx_sAddressline2 = value; }

        }

        
        public string Rx_sCity
        {
            get { return _Rx_sCity; }
            set { _Rx_sCity = value; }

        }

       
        public string Rx_sState
        {
            get { return _Rx_sState; }
            set { _Rx_sState = value; }

        }

        public string Rx_sZip
        {
            get { return _Rx_sZip; }
            set { _Rx_sZip = value; }

        }

        
        public string Rx_sEmail
        {
            get { return _Rx_sEmail; }
            set { _Rx_sEmail = value; }

        }

       
        public string Rx_sFax
        {
            get { return _Rx_sFax; }
            set { _Rx_sFax = value; }

        }

        
        public string Rx_sPhone
        {
            get { return _Rx_sPhone; }
            set { _Rx_sPhone = value; }

        }

        
        public string Rx_sServiceLevel
        {
            get { return _Rx_sServiceLevel; }
            set { _Rx_sServiceLevel = value; }

        }

        public string Rx_sPrescriberNotes
        {
            get { return _Rx_sPrescriberNotes; }
            set { _Rx_sPrescriberNotes = value; }

        }

      
        public string Rx_eRxStatus
        {
            get { return _Rx_eRxStatus; }
            set { _Rx_eRxStatus = value; }

        }

       
        public string Rx_eRxStatusMessage
        {
            get { return _Rx_eRxStatusMessage; }
            set { _Rx_eRxStatusMessage = value; }

        }


        public string DrugName
        {
            get { return sDrugName; }
            set { sDrugName = value; }

        }

        public string Dosage
        {
            get { return sDosage; }
            set { sDosage = value; }

        }

        public string NDCCode
        {
            get { return sNDCCode; }
            set { sNDCCode = value; }
        }

        public string ProductCodeQualifier
        {
            get { return sProductCodeQualifier; }
            set { sProductCodeQualifier = value; }
        }

        public string DosageForm
        {
            get { return sDosageForm; }
            set { sDosageForm = value; }

        }

        public string Strength
        {
            get { return sStrength; }
            set { sStrength = value; }

        }

        public string StrengthUnits
        {
            get { return sStrengthUnits; }
            set { sStrengthUnits = value; }

        }

        public string DrugDBCode
        {
            get { return sDrugDBCode; }
            set { sDrugDBCode = value; }

        }

        public string DrugDBCodeQualifier
        {
            get { return sDrugDBCodeQualifier; }
            set { sDrugDBCodeQualifier = value; }

        }

        public string DrugQuantityQualifier
        {
            get { return sDrugQuantityQualifier; }
            set { sDrugQuantityQualifier = value; }

        }

        public string DrugQuantityValue
        {
            get { return sDrugQuantityValue; }
            set { sDrugQuantityValue = value; }
        }


        public string Directions
        {
            get { return sDirections; }
            set { sDirections = value; }

        }

        public string Note
        {
            get { return sNote; }
            set { sNote = value; }
        }


        public string RefillsQualifier
        {
            get { return sRefillsQualifier; }
            set { sRefillsQualifier = value; }
        }
        public string RefillsQuantity
        {
            get { return sRefillsQuantity; }
            set { sRefillsQuantity = value; }
        }

        public string Substitutions
        {
            get { return sSubstitutions; }
            set { sSubstitutions = value; }

        }
        public string WrittenDate
        {
            get { return sWrittenDate; }
            set { sWrittenDate = value; }
        }

        public string ExpirationDate
        {
            get { return sExpirationDate; }
            set { sExpirationDate = value; }

        }

        public string EffectiveDate
        {
            get { return sEffectiveDate; }
            set { sEffectiveDate = value; }

        }

        public string PeriodEnd
        {
            get { return sPeriodEnd; }
            set { sPeriodEnd = value; }

        }

        public string DiagnosisClinicalInformation
        {
            get { return sDiagnosisClinicalInformation; }
            set { sDiagnosisClinicalInformation = value; }
        }

        public string DiagnosisPriamryQualifier
        {
            get { return sDiagnosisPriamryQualifier; }
            set { sDiagnosisPriamryQualifier = value; }

        }

        public string DiagnosisPriamryValue
        {
            get { return sDiagnosisPriamryValue; }
            set { sDiagnosisPriamryValue = value; }

        }

        public string DiagnosisSecQualifier
        {
            get { return sDiagnosisSecQualifier; }
            set { sDiagnosisSecQualifier = value; }

        }

        public string DiagnosisSecValue
        {
            get { return sDiagnosisSecValue; }
            set { sDiagnosisSecValue = value; }

        }

        public string PriorAuthorizationQualifier
        {
            get { return sPriorAuthorizationQualifier; }
            set { sPriorAuthorizationQualifier = value; }

        }
        public string PriorAuthorizationValue
        {
            get { return sPriorAuthorizationValue; }
            set { sPriorAuthorizationValue = value; }

        }
        public string CodeListQualifier
        {
            get { return sCodeListQualifier; }
            set { sCodeListQualifier = value; }

        }
        public string DaysSupply
        {
            get { return sDaysSupply; }
            set { sDaysSupply = value; }
        }
        public DateTime LastFillDate
        {
            get { return lastFillDate; }
            set { lastFillDate = value; }
        }
        public string QuantityQualifier
        {
            get { return sQuantityQualifier; }
            set { sQuantityQualifier = value; }
        }
        public string QuantityValue
        {
            get { return sQuantityValue; }
            set { sQuantityValue = value; }
        }

        public ClsProvider DrugProvider
        {
            get { return oProvider; }
            set { oProvider = value; }

        }

        #endregion "Private and public Variable"
    }

    public class ClsPharmacy : IDisposable
    {
        #region "Private and public Variable"
        private Int64 nNCPDPID = 0;
        private string sStoreName = "";
        private string sPharmacistFirstName = "";
        private string sPharmacistLastName = "";
        private string sPharmacistMiddleName = "";
        private string sPharmacistSuffix = "";
        private string sPharmacistPrefix = "";
        private ClsAddress oaddress = new ClsAddress();
        private ClsContactDetails oContact = new ClsContactDetails();
        private bool disposedValue = false;
        #endregion "Private and public Variable"

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
        #region "Property procedure for pharmacy"
        public string StoreName
        {
            get { return sStoreName; }
            set { sStoreName = value; }
        }

        public Int64 NCPDPID
        {
            get { return nNCPDPID; }
            set { nNCPDPID = value; }
        }

        public string PharmacistFirstName
        {
            get { return sPharmacistFirstName; }
            set { sPharmacistFirstName = value; }
        }

        public string PharmacistLastName
        {
            get { return sPharmacistLastName; }
            set { sPharmacistLastName = value; }
        }

        public string PharmacistMiddleName
        {
            get { return sPharmacistMiddleName; }
            set { sPharmacistMiddleName = value; }
        }

        public string PharmacistSuffix
        {
            get { return sPharmacistSuffix; }
            set { sPharmacistSuffix = value; }
        }
        public string PharmacistPrefix
        {
            get { return sPharmacistPrefix; }
            set { sPharmacistPrefix = value; }
        }

        public ClsAddress PhramacyAddress
        {
            get { return oaddress; }
            set { oaddress = value; }
        }

        public ClsContactDetails PharmacyContactDetails
        {
            get { return oContact; }
            set { oContact = value; }
        }

        #endregion "Property procedure for pharmacy"
    }

    public class ClsMedication : IDisposable
    {
        #region "Private and public Variable"
        private ClsDrug oDrugs = new ClsDrug();
        private ClsPharmacy oPhrmacy = new ClsPharmacy();
        private ClsProvider oProvider = new ClsProvider();
        private bool disposedValue = false;
        #endregion "Private and public Variable"

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

        #region "Property procedure for Medication"
        public ClsDrug MedicationDrug
        {
            get
            {
                if (oDrugs == null)
                {
                    oDrugs = new ClsDrug();
                }
                return oDrugs;
            }
            set { oDrugs = value; }
        }

        public ClsPharmacy MedicationPharmacy
        {
            get { return oPhrmacy; }
            set { oPhrmacy = value; }
        }

        //public ClsProvider MedicationProvider
        //{
        //    get { return oProvider;}
        //    set { oProvider = value; }
        //}

        #endregion "Private and public Variable"
    }


    public class ClsPresciberAgent : IDisposable
    {
        #region "Private and public Variable"
        private string sPresciberAgentFirstName = "";
        private string sPresciberAgentLastName = "";
        private string sPresciberAgentMiddleName = "";
        private string sPresciberAgentSuffix = "";
        private string sPresciberAgentPrefix = "";


        private bool disposedValue = false;
        #endregion "Private and public Variable"

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

        #region "Property Procedure"
        public string PresciberAgentFirstName
        {
            get { return sPresciberAgentFirstName; }
            set { sPresciberAgentFirstName = value; }
        }

        public string PresciberAgentLastName
        {
            get { return sPresciberAgentLastName; }
            set { sPresciberAgentLastName = value; }
        }

        public string PresciberAgentMiddleName
        {
            get { return sPresciberAgentMiddleName; }
            set { sPresciberAgentMiddleName = value; }
        }

        public string PresciberAgentSuffix
        {
            get { return sPresciberAgentSuffix; }
            set { sPresciberAgentSuffix = value; }
        }
        public string PresciberAgentPrefix
        {
            get { return sPresciberAgentPrefix; }
            set { sPresciberAgentPrefix = value; }
        }
        #endregion "Property Procedure"
    }

    public class ClsMessageHeader : IDisposable
    {
        #region "Private and public Variable"
        private string sTo = "";
        private string sFrom = "";
        private string sMessageID = "";
        private string sSentTime = "";
        private string sSecurity = "";
        private string sMailBox = "";
        private string sTestMessage = "";
        private string sMessageDescription = "";
        private string sSender = "";
        private string sReceiver = "";
        private string sSecurityUserName = "";
        private string sSecuritySecondaryID = "";
        private string sSecurityTertiaryID = "";
        private string sSecurityReceiverSecID = "";
        private string sSecurityReceiverTerID = "";



        private bool disposedValue = false;
        #endregion "Private and public Variable"

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

        #region "Property Procedures"
        public string To
        {
            get { return sTo; }
            set { sTo = value; }

        }
        public string From
        {
            get { return sFrom; }
            set { sFrom = value; }

        }
        public string MessageID
        {
            get { return sMessageID; }
            set { sMessageID = value; }

        }
        public string SentTime
        {
            get { return sSentTime; }
            set { sSentTime = value; }

        }
        public string Security
        {
            get { return sSecurity; }
            set { sSecurity = value; }

        }
        public string MailBox
        {
            get { return sMailBox; }
            set { sMailBox = value; }

        }
        public string TestMessage
        {
            get { return sTestMessage; }
            set { sTestMessage = value; }

        }
        public string MessageDescription
        {
            get { return sMessageDescription; }
            set { sMessageDescription = value; }

        }

        public string Sender
        {
            get { return sSender; }
            set { sSender = value; }

        }

        public string Receiver
        {
            get { return sReceiver; }
            set { sReceiver = value; }

        }


        public string SecurityUserName
        {
            get { return sSecurityUserName; }
            set { sSecurityUserName = value; }

        }


        public string SecuritySecondaryID
        {
            get { return sSecuritySecondaryID; }
            set { sSecuritySecondaryID = value; }

        }


        public string SecurityTertiaryID
        {
            get { return sSecurityTertiaryID; }
            set { sSecurityTertiaryID = value; }

        }

        public string SecurityReceiverSecID
        {
            get { return sSecurityReceiverSecID; }
            set { sSecurityReceiverSecID = value; }

        }

        public string SecurityReceiverTerID
        {
            get { return sSecurityReceiverTerID; }
            set { sSecurityReceiverTerID = value; }

        }

        #endregion "Property Procedures"




    }

    public class ClsResponse : IDisposable
    {

        #region "Private and public Variable"
        private string sApprovalReasonCode = "";
        private string sApprovedReferenceNumber = "";
        private string sApprovedNote = "";
        private string sDenialReasonCode = "";
        private string sDeniedReferenceNumber = "";
        private string sDenialReason = "";


        private bool disposedValue = false;
        #endregion "Private and public Variable"

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


        #region "Property Procedures"
        public string ApprovalReasonCode
        {
            get { return sApprovalReasonCode; }
            set { sApprovalReasonCode = value; }

        }

        public string ApprovedReferenceNumber
        {
            get { return sApprovedReferenceNumber; }
            set { sApprovedReferenceNumber = value; }

        }

        public string ApprovedNote
        {
            get { return sApprovedNote; }
            set { sApprovedNote = value; }

        }

        public string DenialReasonCode
        {
            get { return sDenialReasonCode; }
            set { sDenialReasonCode = value; }

        }
        public string DeniedReferenceNumber
        {
            get { return sDeniedReferenceNumber; }
            set { sDeniedReferenceNumber = value; }

        }

        public string DenialReason
        {
            get { return sDenialReason; }
            set { sDenialReason = value; }

        }


        #endregion "Property Procedures"
    }

}
