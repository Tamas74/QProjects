using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace gloCMSEDI
{

    public class UB04Transaction //:Transaction
    {

        #region "Constructor & Destructor"

        public UB04Transaction(Int64 MasterTransactionID, Int64 TransactionID)
        {
            _Transaction = new Transaction();
            _TransactionID = TransactionID;
            _MasterTransactionID = MasterTransactionID;
            BillingProviderFirstName = "";
            BillingProviderLastName = "";
            PrimaryClaimRemittance = "";
            SecondaryClaimRemittance = "";
            TertiaryClaimRemittance = "";
            SecondaryEmpName = "";
            PrimaryEmpName = "";
            TertiaryEmpName = "";
            PrimaryRealeasOfInformation = "";
            SecondaryRealeasOfInformation = "";
            TertiaryRealeasOfInformation = "";
            PrimaryAssignmentOfBenefits = "";
            SecondaryAssignmentOfBenefits = "";
            TertiaryAssignmentOfBenefits = "";
            TypeofBilling = "";
            RevenueCode = "";
            RevenueCodeTotal = "";
            BillingProviderPhoneNo = "";
            ResponsiblePhoneNo = "";
            PrimaryRelShipId = "";
            SecondaryRelShipId = "";
            TertiaryRelShipId = "";
            PrimaryPayerName = "";
            PrimaryPayment = "";
            SecondaryPayment = "";
            TertiaryPayment = "";
            ResponsibleSubcriberID = "";
            ResponsiblesubcriberName = "";
            Responsiblesubstate = "";
            Responsiblesubzip = "";
            Responsiblesubcity = "";
            Responsiblesubadd1 = "";
            Responsiblesubadd2 = "";
            Facilityzip = "";
            Facilitystate = "";
            Facilityphone = "";
            FacilityDescription = "";
            Facilitycity = "";
            Facilityadd1 = "";
            Facilityadd2 = "";
            AdmissionHour = "";
            ResponsibleContactID = "";
            ResponsibleInsuranceID = "";
            PrimaryInsuranceID = "";
            PrimaryContactID = "";
            SecondaryInsuranceID = "";
            SecondaryContactID = "";
            TertiaryInsuranceID = "";
            TertiaryContactID = "";

            Box19Note = "";

            RenderingProviderFName = "";
            RenderingProviderLName = "";
            RenderingProviderNPI = "";
            RenderingProviderOtherID = "";
            RenderingProviderOtherQualifier = "";


            ReferringProviderFName = "";
            ReferringProviderLName = "";
            ReferringProviderNPI = "";
            ReferringProviderOtherID = "";
            ReferringProviderOtherQualifier = "";

            IncludeRendering_Attending = false;

           
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

                    if (bAssignedTransaction)
                    {
                        if (_Transaction != null)
                        {

                            _Transaction.Dispose();
                            _Transaction = null;

                        }
                        bAssignedTransaction = false;
                    }
                    
                }

            }
            disposed = true;
        }

        ~UB04Transaction()
        {
            Dispose(false);
        }

        #endregion

        #region " Declarations "

        private Int64 _TransactionID = 0;
        private Int64 _MasterTransactionID = 0;
        private string _BillingProviderAddressLine1 = "";
        private string _BillingProviderAddressLine2 = "";
        private string _BillingProviderCity = "";
        private string _BillingProviderState = "";
        private string _BillingProviderZip = "";
        private string _BillingProviderNPI = "";

        private string _PatientCode = "";
        //private string Medical/Health Record Number
        private string _BillingProviderTaxID = "";
        private DateTime _MaxDOS;
        private DateTime _MinDOS;

        private string _PatientSSN = "";
        private string _PatientFirstName = "";
        private string _PatientMiddleName = "";
        private string _PatientLastName = "";
        private string _PatientAddressLine1 = "";
        private string _PatientAddressLine2 = "";
        private string _PatientCity = "";
        private string _PatientState = "";
        private string _PatientZip = "";
        private DateTime _PatientDOB;
        private string _PatientGender = "";

        private string _AdmissionTypeCode = "";
        private string _AdmissionSource = "";
        private string _DischargeHour = "";//Not used
        private string _DischargeStatus = "";

        private string _AccidentState = "";

        private string _ResponsiblePayerName = "";
        private string _ResponsiblePayerAddressLine1 = "";
        private string _ResponsiblePayerAddressLine2 = "";
        private string _ResponsiblePayerCity = "";
        private string _ResponsiblePayerState = "";
        private string _ResponsiblePayerZip = "";

        private string _SecondaryPayerName = "";
        private string _TertiaryPayerName = "";

        private string _RealeasOfInformation = "";
        private string _AssignmentOfBenefits = "";


        private string _PrimarySubscriberName = "";
        private string _SecondarySubscriberName = "";
        private string _TertiarySubscriberName = "";

        private string _PrimarySubscriberID = "";
        private string _SecondarySubscriberID = "";
        private string _TertiarySubscriberID = "";


        private string _PrimaryGroupNumber = "";
        private string _SecondaryGroupNumber = "";
        private string _TertiaryGroupNumber = "";

        private string _PrimaryOtherQualifierValue = "";
        private string _SecondaryOtherQualifierValue = "";
        private string _TertiaryOtherQualifierValue = "";

        private string _PrimaryPriorAuthorization = "";
        private string _SecondaryPriorAuthorization = "";
        private string _TertiaryPriorAuthorization = "";


        private string _conditioncode1 = "";
        private string _conditioncode2 = "";
        private string _conditioncode3 = "";
        private string _conditioncode4 = "";
        private string _conditioncode5 = "";
        private string _conditioncode6 = "";
        private string _conditioncode7 = "";
        private string _conditioncode8 = "";
        private string _conditioncode9 = "";
        private string _conditioncode10 = "";
        private string _conditioncode11 = "";
        private string _conditioncode12 = "";


        private string _occurrencecode1 = "";
        private string _occurrencedate1;
        private string _occurrencecode2 = "";
        private string _occurrencedate2;
        private string _occurrencecode3 = "";
        private string _occurrencedate3;
        private string _occurrencecode4 = "";
        private string _occurrencedate4;
        private string _occurrencecode5 = "";
        private string _occurrencedate5;
        private string _occurrencecode6 = "";
        private string _occurrencedate6;
        private string _occurrencecode7 = "";
        private string _occurrencedate7;
        private string _occurrencecode8 = "";
        private string _occurrencedate8;


        private string _occurrencespancode1 = "";
        private string _occurrencespanfromdate1;
        private string _occurrencespantodate1;
        private string _occurrencespancode2 = "";
        private string _occurrencespanfromdate2;
        private string _occurrencespantodate2;
        private string _occurrencespancode3 = "";
        private string _occurrencespanfromdate3;
        private string _occurrencespantodate3;
        private string _occurrencespancode4 = "";
        private string _occurrencespanfromdate4;
        private string _occurrencespantodate4;



        private string _valuecode1 = "";
        private string _valueamount1;
        private string _valuecode2 = "";
        private string _valueamount2;
        private string _valuecode3 = "";
        private string _valueamount3;
        private string _valuecode4 = "";
        private string _valueamount4;
        private string _valuecode5 = "";
        private string _valueamount5;
        private string _valuecode6 = "";
        private string _valueamount6;
        private string _valuecode7 = "";
        private string _valueamount7;
        private string _valuecode8 = "";
        private string _valueamount8;
        private string _valuecode9 = "";
        private string _valueamount9;
        private string _valuecode10 = "";
        private string _valueamount10;
        private string _valuecode11 = "";
        private string _valueamount11;
        private string _valuecode12 = "";
        private string _valueamount12;
        private string _RenderingProviderQualifierMstID = String.Empty;

        private string _FacilityNPI = "";

        private Transaction _Transaction = null;
        #endregion " Declarations "

        #region "Properties"

        public Int64 TransactionID
        {
            get { return _TransactionID; }
            set { _TransactionID = value; }
        }

        public Int64 MasterTransactionID
        {
            get { return _MasterTransactionID; }
            set { _MasterTransactionID = value; }
        }

        public string RevenueCode { get; set; }
        public string RevenueCodeTotal { get; set; }
        public string BillingProviderAddressLine1
        {
            get { return _BillingProviderAddressLine1; }
            set { _BillingProviderAddressLine1 = value; }
        }

        public string BillingProviderAddressLine2
        {
            get { return _BillingProviderAddressLine2; }
            set { _BillingProviderAddressLine2 = value; }
        }

        public string BillingProviderCity
        {
            get { return _BillingProviderCity; }
            set { _BillingProviderCity = value; }
        }

        public string BillingProviderState
        {
            get { return _BillingProviderState; }
            set { _BillingProviderState = value; }
        }

        public string BillingProviderZip
        {
            get { return _BillingProviderZip; }
            set { _BillingProviderZip = value; }
        }

        public string BillingProviderNPI
        {
            get { return _BillingProviderNPI; }
            set { _BillingProviderNPI = value; }
        }

        public string BillingProviderFirstName { get; set; }

        public string BillingProviderLastName { get; set; }

        public string PatientCode
        {
            get { return _PatientCode; }
            set { _PatientCode = value; }
        }


        //private string Medical/Health Record Number

        public string BillingProviderTaxID
        {
            get { return _BillingProviderTaxID; }
            set { _BillingProviderTaxID = value; }
        }

        public DateTime MaxDOS
        {
            get { return _MaxDOS; }
            set { _MaxDOS = value; }
        }

        public DateTime MinDOS
        {
            get { return _MinDOS; }
            set { _MinDOS = value; }
        }

        public string PatientSSN
        {
            get { return _PatientSSN; }
            set { _PatientSSN = value; }
        }

        public string PatientFirstName
        {
            get { return _PatientFirstName; }
            set { _PatientFirstName = value; }
        }

        public string PatientMiddleName
        {
            get { return _PatientMiddleName; }
            set { _PatientMiddleName = value; }
        }

        public string PatientLastName
        {
            get { return _PatientLastName; }
            set { _PatientLastName = value; }
        }

        public string PatientAddressLine1
        {
            get { return _PatientAddressLine1; }
            set { _PatientAddressLine1 = value; }
        }

        public string PatientAddressLine2
        {
            get { return _PatientAddressLine2; }
            set { _PatientAddressLine2 = value; }
        }

        public string PatientCity
        {
            get { return _PatientCity; }
            set { _PatientCity = value; }
        }

        public string PatientState
        {
            get { return _PatientState; }
            set { _PatientState = value; }
        }

        public string PatientZip
        {
            get { return _PatientZip; }
            set { _PatientZip = value; }
        }

        public DateTime PatientDOB
        {
            get { return _PatientDOB; }
            set { _PatientDOB = value; }
        }

        public string PatientGender
        {
            get { return _PatientGender; }
            set { _PatientGender = value; }
        }

        public string AdmissionTypeCode
        {
            get { return _AdmissionTypeCode; }
            set { _AdmissionTypeCode = value; }
        }

        public string AdmissionSource
        {
            get { return _AdmissionSource; }
            set { _AdmissionSource = value; }
        }


        public string DischargeHour
        {
            get { return _DischargeHour; }
            set { _DischargeHour = value; }
        }

        public string AdmissionHour
        {
            get;
            set;
        }

        public string DischargeStatus
        {
            get { return _DischargeStatus; }
            set { _DischargeStatus = value; }
        }

        public string AccidentState
        {
            get { return _AccidentState; }
            set { _AccidentState = value; }
        }

        public string ResponsiblePayerName
        {
            get { return _ResponsiblePayerName; }
            set { _ResponsiblePayerName = value; }
        }

        public string ResponsiblePayerAddressLine1
        {
            get { return _ResponsiblePayerAddressLine1; }
            set { _ResponsiblePayerAddressLine1 = value; }
        }

        public string ResponsiblePayerAddressLine2
        {
            get { return _ResponsiblePayerAddressLine2; }
            set { _ResponsiblePayerAddressLine2 = value; }
        }

        public string ResponsiblePayerCity
        {
            get { return _ResponsiblePayerCity; }
            set { _ResponsiblePayerCity = value; }
        }

        public string ResponsiblePayerState
        {
            get { return _ResponsiblePayerState; }
            set { _ResponsiblePayerState = value; }
        }

        public string ResponsiblePayerZip
        {
            get { return _ResponsiblePayerZip; }
            set { _ResponsiblePayerZip = value; }
        }

        public string ResponsiblePhoneNo { get; set; }

        public string ResponsibleSubcriberID { get; set; }

        public string ResponsiblesubcriberName { get; set; }

        public string Responsiblesubadd1 { get; set; }

        public string Responsiblesubadd2 { get; set; }

        public string Responsiblesubcity { get; set; }

        public string Responsiblesubstate { get; set; }

        public string Responsiblesubzip { get; set; }

        public string ResponsibleInsuranceID { get; set; }

        public string ResponsibleContactID { get; set; }

        public string PrimaryInsuranceID { get; set; }

        public string PrimaryContactID { get; set; }

        public string SecondaryInsuranceID { get; set; }

        public string SecondaryContactID { get; set; }

        public string TertiaryInsuranceID { get; set; }

        public string TertiaryContactID { get; set; }



        public string SecondaryPayerName
        {
            get { return _SecondaryPayerName; }
            set { _SecondaryPayerName = value; }
        }

        public string TertiaryPayerName
        {
            get { return _TertiaryPayerName; }
            set { _TertiaryPayerName = value; }
        }

        public string PrimaryRealeasOfInformation
        {
            get { return _RealeasOfInformation; }
            set { _RealeasOfInformation = value; }
        }

        public string PrimaryPayerName { get; set; }

        public string PrimaryAssignmentOfBenefits
        {
            get { return _AssignmentOfBenefits; }
            set { _AssignmentOfBenefits = value; }
        }

        public string PrimaryClaimRemittance { get; set; }

        public string PrimarySubscriberName
        {
            get { return _PrimarySubscriberName; }
            set { _PrimarySubscriberName = value; }
        }

        public string PrimaryEmpName { get; set; }

        public string PrimaryRelShipId { get; set; }

        public string PrimaryPayment { get; set; }

        public string SecondarySubscriberName
        {
            get { return _SecondarySubscriberName; }
            set { _SecondarySubscriberName = value; }
        }

        public string SecondaryRealeasOfInformation { get; set; }

        public string SecondaryAssignmentOfBenefits { get; set; }

        public string SecondaryClaimRemittance { get; set; }

        public string TertiarySubscriberName
        {
            get { return _TertiarySubscriberName; }
            set { _TertiarySubscriberName = value; }
        }

        public string PrimarySubscriberID
        {
            get { return _PrimarySubscriberID; }
            set { _PrimarySubscriberID = value; }
        }

        public string SecondarySubscriberID
        {
            get { return _SecondarySubscriberID; }
            set { _SecondarySubscriberID = value; }
        }


        public string PrimaryOtherQualifierValue
        {
            get { return _PrimaryOtherQualifierValue; }
            set { this._PrimaryOtherQualifierValue = value; }
        }

        public string SecondaryOtherQualifierValue
        {
            get { return _SecondaryOtherQualifierValue; }
            set { this._SecondaryOtherQualifierValue = value; }
        }
        public string TertiaryOtherQualifierValue
        {
            get { return _TertiaryOtherQualifierValue; }
            set { this._TertiaryOtherQualifierValue = value; }
        }
        public string SecondaryEmpName { get; set; }

        public string SecondaryRelShipId { get; set; }

        public string SecondaryPayment { get; set; }

        public string TertiarySubscriberID
        {
            get { return _TertiarySubscriberID; }
            set { _TertiarySubscriberID = value; }
        }

        public string PrimaryGroupNumber
        {
            get { return _PrimaryGroupNumber; }
            set { _PrimaryGroupNumber = value; }
        }

        public string SecondaryGroupNumber
        {
            get { return _SecondaryGroupNumber; }
            set { _SecondaryGroupNumber = value; }
        }

        public string TertiaryGroupNumber
        {
            get { return _TertiaryGroupNumber; }
            set { _TertiaryGroupNumber = value; }
        }

        public string TertiaryRealeasOfInformation { get; set; }

        public string TertiaryAssignmentOfBenefits { get; set; }

        public string TertiaryClaimRemittance
        {
            get;
            set;
        }

        public string TertiaryPayment { get; set; }

        public string TertiaryEmpName { get; set; }

        public string PrimaryPriorAuthorization
        {
            get { return _PrimaryPriorAuthorization; }
            set { _PrimaryPriorAuthorization = value; }
        }

        public string SecondaryPriorAuthorization
        {
            get { return _SecondaryPriorAuthorization; }
            set { _SecondaryPriorAuthorization = value; }
        }

        public string TertiaryPriorAuthorization
        {
            get { return _TertiaryPriorAuthorization; }
            set { _TertiaryPriorAuthorization = value; }
        }

        public string TertiaryRelShipId { get; set; }

        public string FacilityNPI
        {
            get { return _FacilityNPI; }
            set { _FacilityNPI = value; }
        }

        public string FacilityDescription { get; set; }
        public string Facilityadd1 { get; set; }
        public string Facilityadd2 { get; set; }
        public string Facilitycity { get; set; }
        public string Facilityzip { get; set; }
        public string Facilityphone { get; set; }
        public string Facilitystate { get; set; }

        public string PrimaryHealPlanID { get; set; }
        public string SecondaryHealPlanID { get; set; }
        public string TertiaryHealPlanID { get; set; }
        public Boolean SignatureonFile { get; set; }
        private Boolean bAssignedTransaction = true;
        public Transaction Transaction
        {
            get { return _Transaction; }
            set
            {
                if (bAssignedTransaction)
                {
                    if (_Transaction != null)
                    {

                        _Transaction.Dispose();
                        _Transaction = null;

                    }
                    bAssignedTransaction = false;
                }
                _Transaction = value;
            }
        }

        public string TypeofBilling { get; set; }

        public string BillingProviderPhoneNo { get; set; }



        public string ConditionCode1
        {
            get { return _conditioncode1; }
            set { _conditioncode1 = value; }
        }
        public string ConditionCode2
        {
            get { return _conditioncode2; }
            set { _conditioncode2 = value; }
        }
        public string ConditionCode3
        {
            get { return _conditioncode3; }
            set { _conditioncode3 = value; }
        }
        public string ConditionCode4
        {
            get { return _conditioncode4; }
            set { _conditioncode4 = value; }
        }
        public string ConditionCode5
        {
            get { return _conditioncode5; }
            set { _conditioncode5 = value; }
        }
        public string ConditionCode6
        {
            get { return _conditioncode6; }
            set { _conditioncode6 = value; }
        }
        public string ConditionCode7
        {
            get { return _conditioncode7; }
            set { _conditioncode7 = value; }
        }
        public string ConditionCode8
        {
            get { return _conditioncode8; }
            set { _conditioncode8 = value; }
        }
        public string ConditionCode9
        {
            get { return _conditioncode9; }
            set { _conditioncode9 = value; }
        }
        public string ConditionCode10
        {
            get { return _conditioncode10; }
            set { _conditioncode10 = value; }
        }
        public string ConditionCode11
        {
            get { return _conditioncode11; }
            set { _conditioncode11 = value; }
        }
        public string ConditionCode12
        {
            get { return _conditioncode12; }
            set { _conditioncode12 = value; }
        }


        public string OccurrenceCode1
        {
            get { return _occurrencecode1; }
            set { _occurrencecode1 = value; }
        }
        public string OccurrenceCode2
        {
            get { return _occurrencecode2; }
            set { _occurrencecode2 = value; }
        }
        public string OccurrenceCode3
        {
            get { return _occurrencecode3; }
            set { _occurrencecode3 = value; }
        }
        public string OccurrenceCode4
        {
            get { return _occurrencecode4; }
            set { _occurrencecode4 = value; }
        }
        public string OccurrenceCode5
        {
            get { return _occurrencecode5; }
            set { _occurrencecode5 = value; }
        }
        public string OccurrenceCode6
        {
            get { return _occurrencecode6; }
            set { _occurrencecode6 = value; }
        }
        public string OccurrenceCode7
        {
            get { return _occurrencecode7; }
            set { _occurrencecode7 = value; }
        }
        public string OccurrenceCode8
        {
            get { return _occurrencecode8; }
            set { _occurrencecode8 = value; }
        }

        public string OccurrenceDate1
        {
            get { return _occurrencedate1; }
            set { _occurrencedate1 = value; }
        }
        public string OccurrenceDate2
        {
            get { return _occurrencedate2; }
            set { _occurrencedate2 = value; }
        }
        public string OccurrenceDate3
        {
            get { return _occurrencedate3; }
            set { _occurrencedate3 = value; }
        }
        public string OccurrenceDate4
        {
            get { return _occurrencedate4; }
            set { _occurrencedate4 = value; }
        }
        public string OccurrenceDate5
        {
            get { return _occurrencedate5; }
            set { _occurrencedate5 = value; }
        }
        public string OccurrenceDate6
        {
            get { return _occurrencedate6; }
            set { _occurrencedate6 = value; }
        }
        public string OccurrenceDate7
        {
            get { return _occurrencedate7; }
            set { _occurrencedate7 = value; }
        }
        public string OccurrenceDate8
        {
            get { return _occurrencedate8; }
            set { _occurrencedate8 = value; }
        }



        public string OccurrenceSpanCode1
        {
            get { return _occurrencespancode1; }
            set { _occurrencespancode1 = value; }
        }
        public string OccurrenceSpanCode2
        {
            get { return _occurrencespancode2; }
            set { _occurrencespancode2 = value; }
        }
        public string OccurrenceSpanCode3
        {
            get { return _occurrencespancode3; }
            set { _occurrencespancode3 = value; }
        }
        public string OccurrenceSpanCode4
        {
            get { return _occurrencespancode4; }
            set { _occurrencespancode4 = value; }
        }


        public string OccurrenceSpanFromDate1
        {
            get { return _occurrencespanfromdate1; }
            set { _occurrencespanfromdate1 = value; }
        }
        public string OccurrenceSpanFromDate2
        {
            get { return _occurrencespanfromdate2; }
            set { _occurrencespanfromdate2 = value; }
        }
        public string OccurrenceSpanFromDate3
        {
            get { return _occurrencespanfromdate3; }
            set { _occurrencespanfromdate3 = value; }
        }
        public string OccurrenceSpanFromDate4
        {
            get { return _occurrencespanfromdate4; }
            set { _occurrencespanfromdate4 = value; }
        }

        public string OccurrenceSpanToDate1
        {
            get { return _occurrencespantodate1; }
            set { _occurrencespantodate1 = value; }
        }
        public string OccurrenceSpanToDate2
        {
            get { return _occurrencespantodate2; }
            set { _occurrencespantodate2 = value; }
        }
        public string OccurrenceSpanToDate3
        {
            get { return _occurrencespantodate3; }
            set { _occurrencespantodate3 = value; }
        }
        public string OccurrenceSpanToDate4
        {
            get { return _occurrencespantodate4; }
            set { _occurrencespantodate4 = value; }
        }



        public string ValueCode1
        {
            get { return _valuecode1; }
            set { _valuecode1 = value; }
        }
        public string ValueCode2
        {
            get { return _valuecode2; }
            set { _valuecode2 = value; }
        }
        public string ValueCode3
        {
            get { return _valuecode3; }
            set { _valuecode3 = value; }
        }
        public string ValueCode4
        {
            get { return _valuecode4; }
            set { _valuecode4 = value; }
        }
        public string ValueCode5
        {
            get { return _valuecode5; }
            set { _valuecode5 = value; }
        }
        public string ValueCode6
        {
            get { return _valuecode6; }
            set { _valuecode6 = value; }
        }
        public string ValueCode7
        {
            get { return _valuecode7; }
            set { _valuecode7 = value; }
        }
        public string ValueCode8
        {
            get { return _valuecode8; }
            set { _valuecode8 = value; }
        }
        public string ValueCode9
        {
            get { return _valuecode9; }
            set { _valuecode9 = value; }
        }

        public string ValueCode11
        {
            get { return _valuecode11; }
            set { _valuecode11 = value; }
        }
        public string ValueCode12
        {
            get { return _valuecode12; }
            set { _valuecode12 = value; }
        }


        public string ValueCode10
        {
            get { return _valuecode10; }
            set { _valuecode10 = value; }
        }





        public string ValueAmount1
        {
            get { return _valueamount1; }
            set { _valueamount1 = value; }
        }
        public string ValueAmount2
        {
            get { return _valueamount2; }
            set { _valueamount2 = value; }
        }
        public string ValueAmount3
        {
            get { return _valueamount3; }
            set { _valueamount3 = value; }
        }
        public string ValueAmount4
        {
            get { return _valueamount4; }
            set { _valueamount4 = value; }
        }
        public string ValueAmount5
        {
            get { return _valueamount5; }
            set { _valueamount5 = value; }
        }
        public string ValueAmount6
        {
            get { return _valueamount6; }
            set { _valueamount6 = value; }
        }
        public string ValueAmount7
        {
            get { return _valueamount7; }
            set { _valueamount7 = value; }
        }
        public string ValueAmount8
        {
            get { return _valueamount8; }
            set { _valueamount8 = value; }
        }
        public string ValueAmount9
        {
            get { return _valueamount9; }
            set { _valueamount9 = value; }
        }
        public string ValueAmount10
        {
            get { return _valueamount10; }
            set { _valueamount10 = value; }
        }
        public string ValueAmount11
        {
            get { return _valueamount11; }
            set { _valueamount11 = value; }
        }
        public string ValueAmount12
        {
            get { return _valueamount12; }
            set { _valueamount12 = value; }
        }

        public string Box19Note { get; set; }
        public Int64 RenderingProviderDataBaseID { get; set; }
        public string RenderingProviderFName { get; set; }
        public string RenderingProviderLName { get; set; }
        public string RenderingProviderNPI { get; set; }
        public string RenderingProviderOtherID { get; set; }
        public string RenderingProviderOtherQualifier { get; set; }
        public string RenderingProviderQualifierMstID
        {
            get { return _RenderingProviderQualifierMstID; }
            set { _RenderingProviderQualifierMstID = value; }
        }


        public Int64 ReferringProviderDataBaseID { get; set; }
        public string ReferringProviderFName { get; set; }
        public string ReferringProviderLName { get; set; }
        public string ReferringProviderNPI { get; set; }
        public string ReferringProviderOtherID { get; set; }
        public string ReferringProviderOtherQualifier { get; set; }

        public bool IncludeRendering_Attending { get; set; }
        public string sUBBox77Billing { get; set; }
        public string sUBBox77Rendering { get; set; }

        public string admitDate { get;set ;}
        public string AdmitHour { get;set; }

        public Int32 IcdCodeRevision { get; set; }
        #endregion

    }

    public class gloUB04
    {
        private string _databasestring = "";
        private long _ClinicID;

        #region "Constructor & Destructor"

        public gloUB04(string DatabaseString, long ClinicID)
        {
            _databasestring = DatabaseString;
            _ClinicID = ClinicID;

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

        ~gloUB04()
        {
            Dispose(false);
        }

        #endregion

        #region "Private Methods"

        /// <summary>
        /// Fill the Transaction claim data and admin setting
        /// </summary>
        /// <param name="MasterTransactionID"></param>
        /// <param name="TransactionID"></param>
        /// <returns> UB04 object</returns>
        public UB04Transaction GetUBClaim(Int64 MasterTransactionID, Int64 TransactionID)
        {
            UB04Transaction objUB04 = new UB04Transaction(MasterTransactionID, TransactionID);

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databasestring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataSet dsMst;
            DataTable dtTrans = null;
            DataTable dtTransplan;
            DataTable dtCode;
            DataTable dtUBAdminSetting;
            DataTable dtFacility;
            DataTable dtAllPayment;
            DataTable dtBox19Notes = null;
            DataTable dtRenderingProvider = null;
            DataTable dtReferringProvider = null;
            DataTable dtRendering_Attending = null;

            try
            {
                gloCMSEDI.clsgloBilling objBilling = new gloCMSEDI.clsgloBilling(_databasestring);
                objUB04.Transaction = objBilling.GetChargesClaimDetails(TransactionID, _ClinicID);
                oDB.Connect(false);
                //DONT CHANDE THIS ID TO TRANSACTIONID// I changed this in 6050 Now what?
                oDBParameters.Add("@nMasterTransactionID", objUB04.Transaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", objUB04.Transaction.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", objUB04.Transaction.ResponsibilityNo, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_Insurance_Plan", oDBParameters, out dtTransplan);

                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionMasterID", objUB04.Transaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", objUB04.Transaction.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", objUB04.Transaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_UB_CLAIM", oDBParameters, out dsMst);

                oDBParameters.Clear();
                oDB.Retrive_Query("select sSettingsName,sSettingsValue from Settings WITH(NOLOCK) where sSettingsName like 'UB04%'", out dtUBAdminSetting);

                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", objUB04.Transaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", objUB04.Transaction.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Facility_Transaction", oDBParameters, out dtFacility);

                #region Assign Master DataSet

                if (dsMst != null && dsMst.Tables.Count > 0)
                {
                    dtTrans = dsMst.Tables[0];
                    dtBox19Notes = dsMst.Tables[1];
                    dtRenderingProvider = dsMst.Tables[2];
                    dtReferringProvider = dsMst.Tables[3];
                    dtRendering_Attending = dsMst.Tables[4];
                }

                #endregion
                objUB04.SignatureonFile = GetSignatureonFile(objUB04.Transaction.PatientID);
                if (dtTrans != null && dtTrans.Rows.Count > 0)
                {
                    objUB04.BillingProviderAddressLine1 = Convert.ToString(dtTrans.Rows[0]["sProviderAddressLine1"]);
                    objUB04.BillingProviderAddressLine2 = Convert.ToString(dtTrans.Rows[0]["sProviderAddressLine2"]);
                    objUB04.BillingProviderCity = Convert.ToString(dtTrans.Rows[0]["sProviderCity"]);
                    objUB04.BillingProviderState = Convert.ToString(dtTrans.Rows[0]["sState"]);
                    objUB04.BillingProviderZip = Convert.ToString(dtTrans.Rows[0]["sProviderZIP"]);
                    objUB04.BillingProviderNPI = Convert.ToString(dtTrans.Rows[0]["sProviderNPI"]);
                  //  objUB04.BillingProviderTaxID = Convert.ToString(dtTrans.Rows[0]["sProviderSSN"]); ; //Code remained......
                    objUB04.BillingProviderFirstName = Convert.ToString(dtTrans.Rows[0]["sFirstName"]).Trim();
                    objUB04.BillingProviderLastName = Convert.ToString(dtTrans.Rows[0]["sLastName"]).Trim();
                    objUB04.BillingProviderPhoneNo = Convert.ToString(dtTrans.Rows[0]["sProviderPhoneNo"]).Trim();

                    objUB04.PatientFirstName = Convert.ToString(dtTrans.Rows[0]["sPatientFirstName"]);
                    objUB04.PatientMiddleName = Convert.ToString(dtTrans.Rows[0]["sPatientMiddleName"]);
                    objUB04.PatientLastName = Convert.ToString(dtTrans.Rows[0]["sPatientLastName"]);
                    objUB04.PatientAddressLine1 = Convert.ToString(dtTrans.Rows[0]["sPatientAddressLine1"]);
                    objUB04.PatientAddressLine2 = Convert.ToString(dtTrans.Rows[0]["sPatientAddressLine2"]);
                    objUB04.PatientCity = Convert.ToString(dtTrans.Rows[0]["sPatientCity"]);
                    objUB04.PatientState = Convert.ToString(dtTrans.Rows[0]["sState1"]);
                    objUB04.PatientZip = Convert.ToString(dtTrans.Rows[0]["sPatientZip"]);
                    objUB04.PatientDOB = Convert.ToDateTime(dtTrans.Rows[0]["dtPatientDOB"]);
                    objUB04.PatientSSN = Convert.ToString(dtTrans.Rows[0]["sPatientSSN"]);
                    objUB04.PatientGender = Convert.ToString(dtTrans.Rows[0]["sPatientGender"]);
                    objUB04.PatientCode = Convert.ToString(dtTrans.Rows[0]["sPatientCode"]);

                    objUB04.AdmissionTypeCode = Convert.ToString(dtTrans.Rows[0]["sAdmissionTypeCode"]);
                    objUB04.AdmissionSource = Convert.ToString(dtTrans.Rows[0]["sAdmissionSourceCode"]);
                    objUB04.DischargeHour = Convert.ToString(dtTrans.Rows[0]["Dischargehour"]);
                    objUB04.DischargeStatus = Convert.ToString(dtTrans.Rows[0]["sDischargeStatusCode"]);
                    objUB04.AdmitHour = Convert.ToString(dtTrans.Rows[0]["Admithour"]);
                    objUB04.admitDate = Convert.ToString(dtTrans.Rows[0]["AdmitDate"]);
                    if (objUB04.Transaction.AutoClaim == true)
                    {
                        objUB04.AccidentState = Convert.ToString(objUB04.Transaction.State.Trim());
                    }
                    objUB04.TypeofBilling = Convert.ToString(dtTrans.Rows[0]["sTypeOfbill"]);
                    objUB04.FacilityNPI = Convert.ToString(dtTrans.Rows[0]["sFacilityNPI"]);
                    objUB04.IcdCodeRevision = Convert.ToInt32(dtTrans.Rows[0]["ICDRevision"]);
                    
                }

                #region "PAYER"

                if (dtFacility != null && dtFacility.Rows.Count > 0)
                {
                    objUB04.FacilityDescription = Convert.ToString(dtFacility.Rows[0]["FacilityDescription"]);
                    objUB04.Facilityadd1 = Convert.ToString(dtFacility.Rows[0]["FacilityAddr1"]);
                    objUB04.Facilityadd2 = Convert.ToString(dtFacility.Rows[0]["FacilityAddr2"]);
                    objUB04.Facilitycity = Convert.ToString(dtFacility.Rows[0]["FacilityCity"]);
                    objUB04.Facilityzip = Convert.ToString(dtFacility.Rows[0]["FacilityZip"]);
                    objUB04.Facilityphone = Convert.ToString(dtFacility.Rows[0]["FacilityPhone"]);
                    objUB04.Facilitystate = Convert.ToString(dtFacility.Rows[0]["FacilityState"]);
                }


                if (dtTransplan != null && dtTransplan.Rows.Count > 0)
                {
                    dtAllPayment = GetAllPaymnet(objUB04.Transaction.TransactionID);
                    for (int i = 0; dtTransplan.Rows.Count > i; i++)
                    {
                        if (i == 0)
                        {
                            objUB04.ResponsiblePayerName = Convert.ToString(dtTransplan.Rows[0]["InsuranceName"]);
                            objUB04.ResponsiblePayerAddressLine1 = Convert.ToString(dtTransplan.Rows[0]["PayerAddress1"]);
                            objUB04.ResponsiblePayerAddressLine2 = Convert.ToString(dtTransplan.Rows[0]["PayerAddress2"]);
                            objUB04.ResponsiblePayerCity = Convert.ToString(dtTransplan.Rows[0]["PayerCity"]);
                            objUB04.ResponsiblePayerState = Convert.ToString(dtTransplan.Rows[0]["PayerState"]);
                            objUB04.ResponsiblePayerZip = Convert.ToString(dtTransplan.Rows[0]["PayerZip"]);
                            objUB04.ResponsiblePhoneNo = Convert.ToString(dtTransplan.Rows[0]["sPhone"]);
                            objUB04.ResponsibleSubcriberID = Convert.ToString(dtTransplan.Rows[0]["sSubscriberID"]);
                            objUB04.ResponsiblesubcriberName = Convert.ToString(dtTransplan.Rows[0]["sSubFullName"]);

                            objUB04.Responsiblesubcity = Convert.ToString(dtTransplan.Rows[0]["SubscriberCity"]);
                            objUB04.Responsiblesubadd1 = Convert.ToString(dtTransplan.Rows[0]["SubscriberAddr1"]);
                            objUB04.Responsiblesubadd2 = Convert.ToString(dtTransplan.Rows[0]["SubscriberAddr2"]);
                            objUB04.Responsiblesubstate = Convert.ToString(dtTransplan.Rows[0]["SubscriberState"]);
                            objUB04.Responsiblesubzip = Convert.ToString(dtTransplan.Rows[0]["SubscriberZip"]);

                            objUB04.ResponsibleContactID = Convert.ToString(dtTransplan.Rows[0]["nContactID"]);
                            objUB04.ResponsibleInsuranceID = Convert.ToString(dtTransplan.Rows[0]["nInsuranceID"]);
                            objUB04.RevenueCodeTotal = GetRevenueTotalSetting(objUB04.ResponsibleContactID);
                        }

                        if (Convert.ToString(dtTransplan.Rows[i]["sInsuranceFlag"]) == "Primary")
                        {
                            objUB04.PrimaryPayerName = Convert.ToString(dtTransplan.Rows[i]["InsuranceName"]);
                            if (Convert.ToString(dtTransplan.Rows[i]["RelationshipCode"]) != "18" && Convert.ToBoolean(dtTransplan.Rows[i]["bIsCompnay"]) == true)
                            {
                                objUB04.PrimarySubscriberName = Convert.ToString(dtTransplan.Rows[i]["sCompanyName"]);
                            }
                            else
                            {
                                objUB04.PrimarySubscriberName = Convert.ToString(dtTransplan.Rows[i]["sSubFullName"]);
                            }
                            objUB04.PrimaryGroupNumber = Convert.ToString(dtTransplan.Rows[i]["sGroup"]);
                            objUB04.PrimarySubscriberID = Convert.ToString(dtTransplan.Rows[i]["sSubscriberID"]);

                            objUB04.PrimaryPriorAuthorization = Convert.ToString(objUB04.Transaction.PriorAuthorizationNo);//Check this
                            objUB04.PrimaryAssignmentOfBenefits = Convert.ToString(dtTransplan.Rows[i]["AssignmentofBenifit"]);
                            objUB04.PrimaryClaimRemittance = Convert.ToString(dtTransplan.Rows[i]["sClaimRemittanceRefNo"]);
                            objUB04.PrimaryEmpName = Convert.ToString(dtTransplan.Rows[i]["sEmployer"]);
                            objUB04.PrimaryRelShipId = Convert.ToString(dtTransplan.Rows[i]["RelationshipCode"]);

                            //objUB04.PrimaryPayment = getPayerPaidAmount(TransactionID, Convert.ToString(dtTransplan.Rows[i]["PayerID"]), Convert.ToString(dtTransplan.Rows[i]["nContactID"]), Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]));
                            objUB04.PrimaryPayment = getFilteredPayerPaid(MasterTransactionID, Convert.ToString(dtTransplan.Rows[i]["nContactID"]), Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]), dtAllPayment);

                            objUB04.PrimaryContactID = Convert.ToString(dtTransplan.Rows[i]["nContactID"]);
                            objUB04.PrimaryInsuranceID = Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]);
                            objUB04.PrimaryOtherQualifierValue = GetOtherIDQualifier(Convert.ToInt64(objUB04.Transaction.ProviderID), Convert.ToInt64(objUB04.Transaction.FacilityCode), Convert.ToInt64(objUB04.PrimaryContactID), "Billing Provider Source", "Provider");
                            objUB04.PrimaryHealPlanID = GetOtherHealthPlanIDQualifier(Convert.ToInt64(objUB04.Transaction.ProviderID), Convert.ToInt64(objUB04.PrimaryContactID));
                        }
                        else if (Convert.ToString(dtTransplan.Rows[i]["sInsuranceFlag"]) == "Secondary")
                        {
                            //Secondary
                            objUB04.SecondaryPayerName = Convert.ToString(dtTransplan.Rows[i]["InsuranceName"]);
                            objUB04.SecondarySubscriberID = Convert.ToString(dtTransplan.Rows[i]["sSubscriberID"]);
                            if (Convert.ToString(dtTransplan.Rows[i]["RelationshipCode"]) != "18" && Convert.ToBoolean(dtTransplan.Rows[i]["bIsCompnay"]) == true)
                            {
                                objUB04.SecondarySubscriberName = Convert.ToString(dtTransplan.Rows[i]["sCompanyName"]);
                            }
                            else
                            {
                                objUB04.SecondarySubscriberName = Convert.ToString(dtTransplan.Rows[i]["sSubFullName"]);
                            }
                            objUB04.SecondaryGroupNumber = Convert.ToString(dtTransplan.Rows[i]["sGroup"]);
                            objUB04.SecondaryPriorAuthorization = "";
                            objUB04.SecondaryAssignmentOfBenefits = Convert.ToString(dtTransplan.Rows[i]["AssignmentofBenifit"]);
                            objUB04.SecondaryClaimRemittance = Convert.ToString(dtTransplan.Rows[i]["sClaimRemittanceRefNo"]);
                            objUB04.SecondaryEmpName = Convert.ToString(dtTransplan.Rows[i]["sEmployer"]);
                            objUB04.SecondaryRelShipId = Convert.ToString(dtTransplan.Rows[i]["RelationshipCode"]);
                            objUB04.SecondaryContactID = Convert.ToString(dtTransplan.Rows[i]["nContactID"]);
                            objUB04.SecondaryInsuranceID = Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]);
                            //objUB04.SecondaryPayment = getPayerPaidAmount(TransactionID, Convert.ToString(dtTransplan.Rows[i]["PayerID"]), Convert.ToString(dtTransplan.Rows[i]["nContactID"]), Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]));
                            objUB04.SecondaryPayment = getFilteredPayerPaid(MasterTransactionID, Convert.ToString(dtTransplan.Rows[i]["nContactID"]), Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]), dtAllPayment);
                            objUB04.SecondaryOtherQualifierValue = GetOtherIDQualifier(Convert.ToInt64(objUB04.Transaction.ProviderID), Convert.ToInt64(objUB04.Transaction.FacilityCode), Convert.ToInt64(objUB04.SecondaryContactID), "Billing Provider Source", "Provider");
                            objUB04.SecondaryHealPlanID = GetOtherHealthPlanIDQualifier(Convert.ToInt64(objUB04.Transaction.ProviderID), Convert.ToInt64(objUB04.SecondaryContactID));

                        }
                        else if (Convert.ToString(dtTransplan.Rows[i]["sInsuranceFlag"]) == "Tertiary")
                        {
                            //Tertiry
                            objUB04.TertiaryPayerName = Convert.ToString(dtTransplan.Rows[i]["InsuranceName"]);
                            objUB04.TertiarySubscriberID = Convert.ToString(dtTransplan.Rows[i]["sSubscriberID"]);
                            if (Convert.ToString(dtTransplan.Rows[i]["RelationshipCode"]) != "18" && Convert.ToBoolean(dtTransplan.Rows[i]["bIsCompnay"]) == true)
                            {
                                objUB04.TertiarySubscriberName = Convert.ToString(dtTransplan.Rows[i]["sCompanyName"]);
                            }
                            else
                            {
                                objUB04.TertiarySubscriberName = Convert.ToString(dtTransplan.Rows[i]["sSubFullName"]);
                            }
                            objUB04.TertiaryGroupNumber = Convert.ToString(dtTransplan.Rows[i]["sGroup"]);
                            objUB04.TertiaryPriorAuthorization = "";
                            objUB04.TertiaryAssignmentOfBenefits = Convert.ToString(dtTransplan.Rows[i]["AssignmentofBenifit"]);
                            objUB04.TertiaryClaimRemittance = Convert.ToString(dtTransplan.Rows[i]["sClaimRemittanceRefNo"]);
                            objUB04.TertiaryEmpName = Convert.ToString(dtTransplan.Rows[i]["sEmployer"]);
                            objUB04.TertiaryRelShipId = Convert.ToString(dtTransplan.Rows[i]["RelationshipCode"]);

                            objUB04.TertiaryContactID = Convert.ToString(dtTransplan.Rows[i]["nContactID"]);
                            objUB04.TertiaryInsuranceID = Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]);
                            //objUB04.TertiaryPayment = getPayerPaidAmount(TransactionID, Convert.ToString(dtTransplan.Rows[i]["PayerID"]), Convert.ToString(dtTransplan.Rows[i]["nContactID"]), Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]));
                            objUB04.TertiaryPayment = getFilteredPayerPaid(MasterTransactionID, Convert.ToString(dtTransplan.Rows[i]["nContactID"]), Convert.ToString(dtTransplan.Rows[i]["nInsuranceID"]), dtAllPayment);
                            objUB04.TertiaryOtherQualifierValue = GetOtherIDQualifier(Convert.ToInt64(objUB04.Transaction.ProviderID), Convert.ToInt64(objUB04.Transaction.FacilityCode), Convert.ToInt64(objUB04.TertiaryContactID), "Billing Provider Source", "Provider");
                           
                            objUB04.TertiaryHealPlanID = GetOtherHealthPlanIDQualifier(Convert.ToInt64(objUB04.Transaction.ProviderID),  Convert.ToInt64(objUB04.TertiaryContactID));
                           
                           

                        }
                    }
                }
                objUB04.BillingProviderTaxID = GetFedTaxId(Convert.ToInt64(objUB04.Transaction.ProviderID), Convert.ToInt64(objUB04.ResponsibleContactID)); //Code remained......
                if (dtRendering_Attending != null && dtRendering_Attending.Rows.Count > 0)
                {
                    if (dtRendering_Attending.Rows[0]["bIncludeRendering_Attending"] != DBNull.Value)
                    {
                        objUB04.IncludeRendering_Attending = Convert.ToBoolean(dtRendering_Attending.Rows[0]["bIncludeRendering_Attending"]);
                        objUB04.sUBBox77Billing = Convert.ToString(dtRendering_Attending.Rows[0]["sUBBox77"]);
                        objUB04.sUBBox77Rendering = Convert.ToString(dtRendering_Attending.Rows[0]["sUBBox77Rendering"]);
                    }
                }
                #endregion

                #region MaxDOS AND MinDOS


                DateTime _MaxDate = DateTime.MinValue;
                DateTime _MinDate = DateTime.MaxValue;

                if (objUB04.Transaction.Lines != null)
                {
                    for (int _Count = 0; _Count < objUB04.Transaction.Lines.Count; _Count++)
                    {

                        if (_MaxDate.Date < objUB04.Transaction.Lines[_Count].DateServiceFrom.Date)
                        {
                            _MaxDate = objUB04.Transaction.Lines[_Count].DateServiceFrom;
                        }
                        //if (_MaxDate.Date < objUB04.Transaction.Lines[_Count].DateServiceTill.Date)
                        //{
                        //    _MaxDate = objUB04.Transaction.Lines[_Count].DateServiceTill;
                        //}


                        if (_MinDate.Date > objUB04.Transaction.Lines[_Count].DateServiceFrom.Date)
                        {
                            _MinDate = objUB04.Transaction.Lines[_Count].DateServiceFrom;
                        }
                        //if (_MinDate.Date > objUB04.Transaction.Lines[_Count].DateServiceTill.Date)
                        //{
                        //    _MinDate = objUB04.Transaction.Lines[_Count].DateServiceTill;
                        //}
                    }
                    if (_MaxDate != DateTime.MinValue)
                    {
                        objUB04.MaxDOS = _MaxDate;
                    }
                    if (_MinDate != DateTime.MaxValue)
                    {
                        objUB04.MinDOS = _MinDate;
                    }
                }
                #endregion

                #region "Admin Setting"

                if (dtUBAdminSetting != null && dtUBAdminSetting.Rows.Count > 0)
                {
                    for (int i = 0; dtUBAdminSetting.Rows.Count > i; i++)
                    {
                        //if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_RevenueCode")
                        //{
                        //    objUB04.RevenueCode = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                        //}
                        if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_TypeOfBill")
                        {
                            if (objUB04.TypeofBilling.ToString().Trim() == "")
                            {
                                objUB04.TypeofBilling = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                            }
                        }
                        if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_AdmisionType")
                        {
                            if (objUB04.AdmissionTypeCode.ToString().Trim() == "")
                            {
                                objUB04.AdmissionTypeCode = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                            }
                        }
                        if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_AdmisionSource")
                        {
                            if (objUB04.AdmissionSource.ToString().Trim() == "")
                            {
                                objUB04.AdmissionSource = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                            }
                        }

                        if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_DischargeStatus")
                        {
                            if (objUB04.DischargeStatus.ToString().Trim() == "")
                            {
                                objUB04.DischargeStatus = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                            }
                        }
                        //if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_Admissiontime")
                        //{
                        //    if (objUB04.AdmissionHour.ToString().Trim() == "")
                        //    {
                        //        objUB04.AdmissionHour = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                        //    }
                        //}

                        //if (dtUBAdminSetting.Rows[i]["sSettingsName"].ToString().Trim() == "UB04_Dischargetime")
                        //{
                        //    if (objUB04.DischargeHour.ToString().Trim() == "")
                        //    {
                        //        objUB04.DischargeHour = dtUBAdminSetting.Rows[i]["sSettingsValue"].ToString().Trim();
                        //    }
                        //}


                    }
                }
                #endregion

                #region "Condition ,Value, Occurrence and OccurrenceSpan"

                oDB.Connect(false);
                oDBParameters.Clear();
                //DONT CHANDE THIS ID TO TRANSACTIONID//
                oDBParameters.Add("@nMasterTransactionID", objUB04.Transaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_UB04_Code", oDBParameters, out dtCode);
                if (dtCode != null && dtCode.Rows.Count > 0)
                {
                    //String sTypeofbill = Convert.ToString(dtCode.Rows[0]["sTypeofbill"]);
                    //if (sTypeofbill.Trim().Length > 0)
                    //    objUB04.TypeofBilling = sTypeofbill;

                    objUB04.ConditionCode1 = Convert.ToString(dtCode.Rows[0]["sConditionCode01"]);
                    objUB04.ConditionCode2 = Convert.ToString(dtCode.Rows[0]["sConditionCode02"]);
                    objUB04.ConditionCode3 = Convert.ToString(dtCode.Rows[0]["sConditionCode03"]);
                    objUB04.ConditionCode4 = Convert.ToString(dtCode.Rows[0]["sConditionCode04"]);
                    objUB04.ConditionCode5 = Convert.ToString(dtCode.Rows[0]["sConditionCode05"]);
                    objUB04.ConditionCode6 = Convert.ToString(dtCode.Rows[0]["sConditionCode06"]);
                    objUB04.ConditionCode7 = Convert.ToString(dtCode.Rows[0]["sConditionCode07"]);
                    objUB04.ConditionCode8 = Convert.ToString(dtCode.Rows[0]["sConditionCode08"]);
                    objUB04.ConditionCode9 = Convert.ToString(dtCode.Rows[0]["sConditionCode09"]);
                    objUB04.ConditionCode10 = Convert.ToString(dtCode.Rows[0]["sConditionCode10"]);
                    objUB04.ConditionCode11 = Convert.ToString(dtCode.Rows[0]["sConditionCode11"]);
                    //objUB04.ConditionCode12 = Convert.ToString(dtCode.Rows[0]["sConditionCode12"]);


                    objUB04.OccurrenceCode1 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode01"]);
                    objUB04.OccurrenceCode2 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode02"]);
                    objUB04.OccurrenceCode3 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode03"]);
                    objUB04.OccurrenceCode4 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode04"]);
                    objUB04.OccurrenceCode5 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode05"]);
                    objUB04.OccurrenceCode6 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode06"]);
                    objUB04.OccurrenceCode7 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode07"]);
                    objUB04.OccurrenceCode8 = Convert.ToString(dtCode.Rows[0]["sOccurrenceCode08"]);

                    if ((dtCode.Rows[0]["dtOccurrenceDate01"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate01"].ToString().Length > 0)
                        objUB04.OccurrenceDate1 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate01"]);
                    //if(objUB04.OccurrenceDate1==null || Convert.ToString(objUB04.OccurrenceDate1)=="")
                    //{
                    //    objUB04.OccurrenceDate1 = Convert.ToString(objUB04.MinDOS);
                    //}

                    if ((dtCode.Rows[0]["dtOccurrenceDate02"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate02"].ToString().Length > 0)
                        objUB04.OccurrenceDate2 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate02"]);
                    if ((dtCode.Rows[0]["dtOccurrenceDate03"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate03"].ToString().Length > 0)
                        objUB04.OccurrenceDate3 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate03"]);
                    if ((dtCode.Rows[0]["dtOccurrenceDate04"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate04"].ToString().Length > 0)
                        objUB04.OccurrenceDate4 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate04"]);
                    if ((dtCode.Rows[0]["dtOccurrenceDate05"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate05"].ToString().Length > 0)
                        objUB04.OccurrenceDate5 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate05"]);
                    if ((dtCode.Rows[0]["dtOccurrenceDate06"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate06"].ToString().Length > 0)
                        objUB04.OccurrenceDate6 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate06"]);
                    if ((dtCode.Rows[0]["dtOccurrenceDate07"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate07"].ToString().Length > 0)
                        objUB04.OccurrenceDate7 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate07"]);
                    if ((dtCode.Rows[0]["dtOccurrenceDate08"]) != DBNull.Value && dtCode.Rows[0]["dtOccurrenceDate08"].ToString().Length > 0)
                        objUB04.OccurrenceDate8 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceDate08"]);


                    objUB04.OccurrenceSpanCode1 = Convert.ToString(dtCode.Rows[0]["sOccurrenceSpanCode01"]);
                    objUB04.OccurrenceSpanCode2 = Convert.ToString(dtCode.Rows[0]["sOccurrenceSpanCode02"]);
                    objUB04.OccurrenceSpanCode3 = Convert.ToString(dtCode.Rows[0]["sOccurrenceSpanCode03"]);
                    objUB04.OccurrenceSpanCode4 = Convert.ToString(dtCode.Rows[0]["sOccurrenceSpanCode04"]);


                    objUB04.OccurrenceSpanFromDate1 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceFromSpanDate01"]);
                    objUB04.OccurrenceSpanFromDate2 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceFromSpanDate02"]);
                    objUB04.OccurrenceSpanFromDate3 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceFromSpanDate03"]);
                    objUB04.OccurrenceSpanFromDate4 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceFromSpanDate04"]);

                    objUB04.OccurrenceSpanToDate1 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceTOSpanDate01"]);
                    objUB04.OccurrenceSpanToDate2 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceTOSpanDate02"]);
                    objUB04.OccurrenceSpanToDate3 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceTOSpanDate03"]);
                    objUB04.OccurrenceSpanToDate4 = Convert.ToString(dtCode.Rows[0]["dtOccurrenceTOSpanDate04"]);

                    objUB04.ValueCode1 = Convert.ToString(dtCode.Rows[0]["sValueCode01"]);
                    objUB04.ValueCode2 = Convert.ToString(dtCode.Rows[0]["sValueCode02"]);
                    objUB04.ValueCode3 = Convert.ToString(dtCode.Rows[0]["sValueCode03"]);
                    objUB04.ValueCode4 = Convert.ToString(dtCode.Rows[0]["sValueCode04"]);
                    objUB04.ValueCode5 = Convert.ToString(dtCode.Rows[0]["sValueCode05"]);
                    objUB04.ValueCode6 = Convert.ToString(dtCode.Rows[0]["sValueCode06"]);
                    objUB04.ValueCode7 = Convert.ToString(dtCode.Rows[0]["sValueCode07"]);
                    objUB04.ValueCode8 = Convert.ToString(dtCode.Rows[0]["sValueCode08"]);
                    objUB04.ValueCode9 = Convert.ToString(dtCode.Rows[0]["sValueCode09"]);
                    objUB04.ValueCode10 = Convert.ToString(dtCode.Rows[0]["sValueCode10"]);
                    objUB04.ValueCode11 = Convert.ToString(dtCode.Rows[0]["sValueCode11"]);
                    objUB04.ValueCode12 = Convert.ToString(dtCode.Rows[0]["sValueCode12"]);


                    //if((dtCode.Rows[0]["nValueAmount01"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount01"].ToString().Length >0)
                    objUB04.ValueAmount1 = Convert.ToString(dtCode.Rows[0]["nValueAmount01"]);
                    //if ((dtCode.Rows[0]["nValueAmount02"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount02"].ToString().Length > 0)
                    objUB04.ValueAmount2 = Convert.ToString(dtCode.Rows[0]["nValueAmount02"]);
                    //if ((dtCode.Rows[0]["nValueAmount03"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount03"].ToString().Length > 0)
                    objUB04.ValueAmount3 = Convert.ToString(dtCode.Rows[0]["nValueAmount03"]);
                    //if ((dtCode.Rows[0]["nValueAmount04"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount04"].ToString().Length > 0)
                    objUB04.ValueAmount4 = Convert.ToString(dtCode.Rows[0]["nValueAmount04"]);
                    //if ((dtCode.Rows[0]["nValueAmount05"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount05"].ToString().Length > 0)
                    objUB04.ValueAmount5 = Convert.ToString(dtCode.Rows[0]["nValueAmount05"]);
                    //if ((dtCode.Rows[0]["nValueAmount06"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount06"].ToString().Length > 0)
                    objUB04.ValueAmount6 = Convert.ToString(dtCode.Rows[0]["nValueAmount06"]);
                    //if ((dtCode.Rows[0]["nValueAmount07"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount07"].ToString().Length > 0)
                    objUB04.ValueAmount7 = Convert.ToString(dtCode.Rows[0]["nValueAmount07"]);
                    //if ((dtCode.Rows[0]["nValueAmount08"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount08"].ToString().Length > 0)
                    objUB04.ValueAmount8 = Convert.ToString(dtCode.Rows[0]["nValueAmount08"]);
                    //if ((dtCode.Rows[0]["nValueAmount09"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount09"].ToString().Length > 0)
                    objUB04.ValueAmount9 = Convert.ToString(dtCode.Rows[0]["nValueAmount09"]);
                    //if ((dtCode.Rows[0]["nValueAmount10"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount10"].ToString().Length > 0)
                    objUB04.ValueAmount10 = Convert.ToString(dtCode.Rows[0]["nValueAmount10"]);
                    //if ((dtCode.Rows[0]["nValueAmount11"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount11"].ToString().Length > 0)
                    objUB04.ValueAmount11 = Convert.ToString(dtCode.Rows[0]["nValueAmount11"]);
                    //if ((dtCode.Rows[0]["nValueAmount12"]) != DBNull.Value && dtCode.Rows[0]["nValueAmount12"].ToString().Length > 0)
                    objUB04.ValueAmount12 = Convert.ToString(dtCode.Rows[0]["nValueAmount12"]);


                }
                #endregion

                #region "REMARKS BOX80"

                if (dtBox19Notes != null && dtBox19Notes.Rows.Count > 0)
                {
                    objUB04.Box19Note = Convert.ToString(dtBox19Notes.Rows[0]["sNoteDescription"]);
                }

                #endregion

                #region "Rendering Provider"

                if (dtRenderingProvider != null && dtRenderingProvider.Rows.Count > 0)
                {
                    objUB04.RenderingProviderFName = Convert.ToString(dtRenderingProvider.Rows[0]["sFirstName"]);
                    objUB04.RenderingProviderLName = Convert.ToString(dtRenderingProvider.Rows[0]["sLastName"]);
                    objUB04.RenderingProviderNPI = Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]);
                    objUB04.RenderingProviderOtherID = Convert.ToString(dtRenderingProvider.Rows[0]["QualifierValue"]);
                    objUB04.RenderingProviderOtherQualifier = Convert.ToString(dtRenderingProvider.Rows[0]["Qualifier"]);
                    if (objUB04.RenderingProviderOtherQualifier != null && Convert.ToString(objUB04.RenderingProviderOtherQualifier) != "")
                    {
                        if (Convert.ToString(objUB04.RenderingProviderOtherQualifier) != "0B" &&
                            Convert.ToString(objUB04.RenderingProviderOtherQualifier) != "LU" &&
                            Convert.ToString(objUB04.RenderingProviderOtherQualifier) != "1G")
                            objUB04.RenderingProviderOtherQualifier = "G2";
                    }
                    objUB04.RenderingProviderQualifierMstID = Convert.ToString(dtRenderingProvider.Rows[0]["QualifierMstID"]);
                }

                #endregion

                #region "Referring Provider"

                if (dtReferringProvider != null && dtReferringProvider.Rows.Count > 0)
                {
                    objUB04.ReferringProviderFName = Convert.ToString(dtReferringProvider.Rows[0]["sFirstName"]);
                    objUB04.ReferringProviderLName = Convert.ToString(dtReferringProvider.Rows[0]["sLastName"]);
                    objUB04.ReferringProviderNPI = Convert.ToString(dtReferringProvider.Rows[0]["sNPI"]);
                    objUB04.ReferringProviderOtherID = Convert.ToString(dtReferringProvider.Rows[0]["Value"]);
                    objUB04.ReferringProviderOtherQualifier = Convert.ToString(dtReferringProvider.Rows[0]["Code"]);
                }

                #endregion

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (objUB04 != null)
                {
                    objUB04.Dispose();
                }
            }
            return objUB04;
        }

        private string GetRevenueTotalSetting(string ContactId)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databasestring);
            DataTable dt = new DataTable();
            string _sqlQuery = "";
            Boolean blnIncludeUB04RevenueCodetotal = false;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select blnIncludeUB04RevenueCodetotal from Contacts_Insurance_Dtl where nContactId=" + ContactId + "";
                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();
                if (dt != null && dt.Rows.Count > 0)
                {
                    blnIncludeUB04RevenueCodetotal = Convert.ToBoolean(dt.Rows[0]["blnIncludeUB04RevenueCodetotal"]);
                }
               
                dt = null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dt = null;
                oDB.Dispose();
            }
            if (blnIncludeUB04RevenueCodetotal == true)
            {
                return "001";
            }
            else
            {
                return "";
            }
            
        }


        private string GetOtherIDQualifier(Int64 nProviderId, Int64 nFacilityId, Int64 ContactID, string SettingName, string Type)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databasestring);
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);

                gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
                oDBParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", nFacilityId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sSettingName", SettingName, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Retrive("BL_Get_AlternateID_Settings", oDBParameters, out dtProviders);

                oDB.Disconnect();
                //string temp = "";

                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {

                    #region "Box 57"
                    if (Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim() != "" && Convert.ToBoolean(dtProviders.Rows[0]["Isdefaultother"]) == false && Convert.ToString(dtProviders.Rows[0]["SecondaryQualifier"]).Trim() != "XX")
                    {
                        return Convert.ToString(dtProviders.Rows[0]["SecondaryQualifierValue"]).Trim();
                    }
                    #endregion

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return "";
        }

        private string GetOtherHealthPlanIDQualifier(Int64 nProviderId, Int64 ContactID)
            {
                gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databasestring);
                gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
                string _sqlquery = String.Empty;
                DataTable dtProviders = null;
                
                try
                {
                    oDB.Connect(false);
                    oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nFacilityID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                    oParameters.Add("@sSettingName", "Box 51 - Health Plan ID", ParameterDirection.Input, SqlDbType.VarChar);
                    oParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                    oParameters.Add("@bIsPhysician", 0, ParameterDirection.Input, SqlDbType.BigInt);
                    oDB.Retrive("BL_Get_AlternateID_Settings_UB04_5010", oParameters, out dtProviders);
                    oDB.Disconnect();
                  

                    if (dtProviders != null && dtProviders.Rows.Count > 0)
                    {
                        #region "Box 51"
                        if (Convert.ToString(dtProviders.Rows[0]["sValue"]).Trim() != "")
                            return Convert.ToString(dtProviders.Rows[0]["sCode"]).Trim() + Convert.ToString(dtProviders.Rows[0]["sValue"]).Trim();
                        #endregion

                    }

                }
                catch (gloDatabaseLayer.DBException dbEx)
                {
                    dbEx.ERROR_Log(dbEx.ToString());
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                }

                finally
                {
                    if (oDB != null) { oDB.Dispose(); }
                }
                return "";
            }

        private string GetFedTaxId(Int64 nProviderId, Int64 ContactID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databasestring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            string _sqlquery = String.Empty;
            DataTable dtProviders = null;

            try
            {
                oDB.Connect(false);
                oParameters.Add("@nClinicID", _ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nProviderID", nProviderId, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nFacilityID", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nContactId", ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sSettingName", "Box 5 - FED TAX NO", ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@bIsEDI", 0, ParameterDirection.Input, SqlDbType.Bit);
                oParameters.Add("@bIsPhysician", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_Get_AlternateID_Settings_UB04_5010", oParameters, out dtProviders);
                oDB.Disconnect();


                if (dtProviders != null && dtProviders.Rows.Count > 0)
                {
                    #region "Box 51"
                    if (Convert.ToString(dtProviders.Rows[0]["sValue"]).Trim() != "")
                        return  Convert.ToString(dtProviders.Rows[0]["sValue"]).Trim();
                    #endregion

                }

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
            }
            return "";
        }

        private Boolean GetSignatureonFile(Int64 _PatientID)
        {
         //   string _result = "";
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databasestring);
            DataTable dt = new DataTable();
            string _sqlQuery = "";
            Boolean _IsSignatureOnFileUB = false;
            try
            {
                oDB.Connect(false);
                _sqlQuery = "select ISNULL(bSignatureOnFile,0) as bSignatureOnFile from Patient_OtherDetails WITH(NOLOCK) where bSignatureOnFile =1 and nPatientID=" + _PatientID;
                oDB.Retrive_Query(_sqlQuery, out dt);
                oDB.Disconnect();
                if (dt != null && dt.Rows.Count > 0)
                {
                    _IsSignatureOnFileUB = Convert.ToBoolean(dt.Rows[0]["bSignatureOnFile"]);
                }
                else
                {
                    _IsSignatureOnFileUB = false;
                }
                dt = null;



            }
            catch //(Exception ex)
            {
                //gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                dt = null;
                oDB.Dispose();
            }
            return _IsSignatureOnFileUB;
        }
        /// <summary>
        /// Get Insurance Paid Amount
        /// </summary>
        /// <param name="_TransactionID"></param>
        /// <param name="sPayerID"></param>
        /// <param name="sContactID"></param>
        /// <param name="sInsuranceID"></param>
        /// <returns> Paid Amount </returns>
        public string getPayerPaidAmount(Int64 _TransactionID, string sPayerID, string sContactID, string sInsuranceID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databasestring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPayment = new DataTable();
            string _result = String.Empty;
            Int64 nContactID = 0;
            Int64 nInsuranceID = 0;
            try
            {
                if (sContactID != "")
                {
                    nContactID = Convert.ToInt64(sContactID);
                }
                if (sInsuranceID != "")
                {
                    nInsuranceID = Convert.ToInt64(sInsuranceID);
                }
                ODB.Connect(false);
                oParameters.Add("@nTransactionID", _TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@sPayerID", sPayerID, ParameterDirection.Input, SqlDbType.VarChar);
                oParameters.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nInsuranceID", nInsuranceID, ParameterDirection.Input, SqlDbType.BigInt);

                ODB.Retrive("BL_SELECT_PAYER_PAID", oParameters, out _dtPayment);
                if (_dtPayment != null && _dtPayment.Rows.Count > 0)
                {
                    if (_dtPayment.Rows[0]["TotalPaid"].ToString() != "")
                    {
                        Decimal _result1 = Convert.ToDecimal(_dtPayment.Rows[0]["TotalPaid"]);
                        _result = _result1.ToString("#0.00");
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ODB.Disconnect();
                ODB.Dispose();

            }
            return _result;
        }

        //Filters According to payer
        public string getFilteredPayerPaid(Int64 nMasterTransactionID, string _PayercontactID, string _PayerInsuranceID, DataTable _dtPayment)
        {
            string _result = String.Empty;

            try
            {
                if (_dtPayment != null && _dtPayment.Rows.Count > 0)
                {
                    _result = Convert.ToString(_dtPayment.Compute("SUM(InsPaidAmount)", "nMasterTransactionID=" + nMasterTransactionID + " And ContactID=" + _PayercontactID + " And InsuranceID=" + _PayerInsuranceID + ""));

                    if (_result != "")
                    {
                        Decimal _result1 = Convert.ToDecimal(_result);
                        _result = _result1.ToString("#0.00");
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

        //GetAllPayment
        public DataTable GetAllPaymnet(Int64 nTransactionID)
        {
            gloDatabaseLayer.DBLayer ODB = new gloDatabaseLayer.DBLayer(_databasestring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            DataTable _dtPayment = new DataTable();

            try
            {

                ODB.Connect(false);
                oParameters.Add("@nTransactionID", nTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                ODB.Retrive("BL_SELECT_SVD_DATA", oParameters, out _dtPayment);
                ODB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                ODB.Disconnect();
                ODB.Dispose();

            }
            return _dtPayment;
        }

        #endregion

    }

}

