using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TriArqEDIRealTimeClaimStatus
{
    class cls_TRIARQClaim_Model
    {
    }

    public class TRIARQClaim
    {
       
        public Int64   MasterTransactionID { get; set; }
        public Int64   TransactionID { get; set; }
        public string  ClaimNo { get; set; }//10001
        public string  SubClaimNo { get; set; }//1
        public string  MainClaimNo { get; set; }//1
        public string  FormattedClaimNumber
        {
            get
            {
                if (SubClaimNo == null)
                {
                    SubClaimNo = "";
                }
                if (SubClaimNo.Trim() != "" && SubClaimNo.Contains("-") == false)
                {
                    return (FormattedClaimNumberGeneration(Convert.ToString(ClaimNo)) + '-' + Convert.ToString(SubClaimNo));
                }
                else
                {
                    if (SubClaimNo.Contains("-") == true && MainClaimNo != String.Empty)
                    { return (FormattedClaimNumberGeneration(Convert.ToString(ClaimNo)) + '-' + Convert.ToString(MainClaimNo)); }
                    else
                    { return FormattedClaimNumberGeneration(Convert.ToString(ClaimNo)); }
                }
            }
        } //10001-1
        public DateTime TransactionDate { get; set; }
        public Int64   ProviderID { get; set; }
        public Int64   RefferingProviderId { get; set; }
        public Boolean IsSameAsBillingProvider { get; set; }
        public Int64   ReferalProviderID_New { get; set; }
        public Int16   ResponsibilityNo { get; set; }
        public Int64   ClinicID { get; set; }
        public Int64   ContactID { get; set; }
        public string  FacilityCode { get; set; }
        public Int64   FromDOS { get; set; }
        public Int64   ToDOS { get; set; }
        public decimal TotalClaimAmount { get; set; }
        public string  Type { get; set; }
        public Int64   PatientID { get; set; }
        
        public Patient Patient { get; set; }
        public TRIARQ276RequestHeader RequestHeader { get; set; }
        public Clinic ClaimClinic { get; set; }
        public Facility ClaimFacility { get; set; }
        public Provider BillingProvider { get; set; }
        public Insurance ResponsibleParty { get; set; }
        public List<ServiceLine> ClaimServiceLines { get; set; }


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
      
        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class TRIARQ276RequestHeader
    {
        public string ReceiverID { get; set; }
        public string SubmitterID { get; set; }
        public string SenderCode { get; set; }
        public string VenderIDCode { get; set; }
        public string ClearingHouseCode { get; set; }
        public string TypeOfData { get; set; }
        public string SenderQualifier { get; set; }
        public string ReceiverQualifier { get; set; }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Clinic
    {
        public Int64 ID { get; set; }
        public string AUSID { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string NPI { get; set; }
        public string Taxonomy { get; set; }
        public Address Address { get; set; }
       
        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Facility
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string TIN { get; set; }
        public string NPI { get; set; }
        public string Taxonomy { get; set; }
        public int EntityType { get; set; }
        public Address Address { get; set; }
        public string POSCode { get; set; }
        public string POSDesc { get; set; }
        public string Type { get; set; }
        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Provider
    {
        public Int64 ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string TIN { get; set; }
        public string NPI { get; set; }
        public string Taxonomy { get; set; }
        public int EntityType { get; set; }
        public Address Address { get; set; }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Patient
    {
        public Int64 PatientID { get; set; }
        public string Code { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public Address Address { get; set; }
        public string PharmacyNumber { get; set; }
        //public List<Insurance> Insurances { get; set; }

        //public string PatientMaritalStatus { get; set; }
        //public string PatientRace { get; set; }
        //public string PatientEthnicity { get; set; }
        //public string PatientCitizanshipStatusCode { get; set; }
        //public string PatientCountryCode { get; set; }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Insurance
    {
        public Int64 ContactId { get; set; }
        public string PayerID { get; set; }
        public Int64 InsuranceID { get; set; }
        public string InsuranceName { get; set; }
        public string PolicyNumber { get; set; }
        public string GroupNumber { get; set; }
        public string InsuranceFlag { get; set; }
        public string InsuranceTypeCode { get; set; }
        public string InsuranceTypeDesc { get; set; }
        public string TypeOfBilling { get; set; }
        public Int64 BillingTypeId { get; set; }
        public string InsTypeCodeDefault { get; set; }
        public string InsTypeDescriptionDefault { get; set; }
        public string InsTypeCodeMedicare { get; set; }
        public string InsTypeDescriptionMedicare { get; set; }

        public Int32 PatientSubscrbierRelationShipCode { get; set; }
        public string PatientSubscrbierRelationShipDesc { get; set; }
        public DateTime SubscriberDOB { get; set; }
        public string SubscriberFName { get; set; }
        public string SubscriberMName { get; set; }
        public string SubscriberLName { get; set; }
        public string SubscriberSuffix{ get; set; }
        public string SubscriberGender { get; set; }
        public Address SubscriberAddress { get; set; }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string AreaCode { get; set; }
        public string Country { get; set; }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public class ServiceLine
    {
        public Int64 LineID { get; set; }
        public Int64 LineNo { get; set; }

        public DateTime FromDOS { get; set; }
        public bool DateServiceTillIsNull { get; set; }
        public DateTime ToDOS { get; set; }


        public string POSCode { get; set; }
        public string POSDesc { get; set; }

        public string TOSCode { get; set; }
        public string TOSDesc { get; set; }

        public string CPT { get; set; }
        public string CPTDescription { get; set; }
        public string CrosswalkCPTCode { get; set; }

        public string Modifier1 { get; set; }
        public string Modifier2 { get; set; }
        public string Modifier3 { get; set; }
        public string Modifier4 { get; set; }

        public decimal ChargeAmount { get; set; }
        public decimal BilledAmount { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalChargeAmount { get; set; }
        public Int64 RenderingProviderId { get; set; }
        public Provider RenderingProvider { get; set; }

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {

                }
            }
            this.disposedValue = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
