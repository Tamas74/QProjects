using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;

namespace ChargeRules
{
    public class Claim
    {
        #region "Constructor, Distructor and Dispose"

        public Claim()
        {
            this.InsuranceList = new List<Insurance>();
            this.CPTCodes = new List<CPT_Code>();
            this.ClaimModfiers = new List<Claim_Modfier>();
            this.RenderringProviderNames =new List<RenderringProvider_Name>();
            this.RenderringProviderNPIs = new List<RenderringProvider_NPI>();
            this.ClaimDiagnosis = new List<Claim_Diagnosis>();
          
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Claim()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                //Release all the managed resources
                this.InsurancePlanName = null;                
                this.BillingProviderName = null;                
                this.CPTCode = null;
                this.Dx1Code = null;
                this.Dx2Code = null;

                if (this.InsuranceList != null)
                {
                    this.InsuranceList.Clear();
                    this.InsuranceList = null;
                }

                if (this.CPTCodes != null)
                {
                    this.CPTCodes.Clear();
                    this.CPTCodes = null;
                }
            }
        }  

        #endregion "Constructor, Distructor and Dispose"

        public List<Insurance> InsuranceList { get; set; }
        public List<CPT_Code> CPTCodes { get; set; }

        public List<Claim_Modfier> ClaimModfiers { get; set; }
        public List<RenderringProvider_Name> RenderringProviderNames { get; set; }
        public List<RenderringProvider_NPI> RenderringProviderNPIs { get; set; }
        public List<Claim_Diagnosis> ClaimDiagnosis { get; set; }

        public Claim_Facility ClaimFacility { get; set; }

        private string _ClaimNumber = "";
        public string ClaimNumber
        {
            get
            {
                if (_ClaimNumber == null)
                { return ""; }
                else
                { return _ClaimNumber; }
            }
            set { this._ClaimNumber = value; }
        }

        [DefaultValue(0)]
        public Int64 TransactionMasterID { get; set; }

        [DefaultValue(0)]
        public Int64 TransactionID { get; set; }

        [DefaultValue(0)]
        public Int64 PatientId { get; set; }

        [DefaultValue(0)]
        public Int32 AgeYears { get; set; }

        [DefaultValue(0)]
        public Int32 AgeMonths { get; set; }

        [DefaultValue(0)]
        public Int32 AgeDays { get; set; }

        private string _PatientGender = "";
        public string PatientGender
        {
            get
            {
                if (_PatientGender == null)
                { return ""; }
                else
                { return _PatientGender; }
            }
            set { this._PatientGender = value; }
        }

        private string _PatientRelationshipToSubscriber = "";
        public string PatientRelationshipToSubscriber
        {
            get
            {
                if (_PatientRelationshipToSubscriber == null)
                { return ""; }
                else
                { return _PatientRelationshipToSubscriber; }
            }
            set { this._PatientRelationshipToSubscriber = value; }
        }

        private string _InsuranceCompanyName = "";
        public string InsuranceCompanyName
        {
            get
            {
                if (_InsuranceCompanyName == null)
                { return ""; }
                else
                { return _InsuranceCompanyName; }
            }
            set { this._InsuranceCompanyName = value; }
        }

        private string _InsuranceCompanyReportingCategory = "";
       
        public string InsuranceCompanyReportingCategory
        {
            get
            {
                if (_InsuranceCompanyReportingCategory == null)
                { return ""; }
                else
                { return _InsuranceCompanyReportingCategory; }
            }
            set { this._InsuranceCompanyReportingCategory = value; }
        }

        private string _InsuranceReportingCategory = "";

        public string InsuranceReportingCategory
        {
            get
            {
                if (_InsuranceReportingCategory == null)
                { return ""; }
                else
                { return _InsuranceReportingCategory; }
            }
            set { this._InsuranceReportingCategory = value; }
        }

        //private string _ReportingCategory = "";
        //public string ReportingCategory
        //{
        //    get
        //    {
        //        if (_ReportingCategory == null)
        //        { return ""; }
        //        else
        //        { return _ReportingCategory; }
        //    }
        //    set { this._ReportingCategory = value; }
        //}

        private string _InsurancePlanName = "";
        public string InsurancePlanName
        {
            get
            {
                if (_InsurancePlanName == null)
                { return ""; }
                else
                { return _InsurancePlanName; }
            }
            set { this._InsurancePlanName = value; }
        }

       
        private string _InsurancePayerID = "";
        public string InsurancePayerID
        {
            get
            {
                if (_InsurancePayerID == null)
                { return ""; }
                else
                { return _InsurancePayerID; }
            }
            set { this._InsurancePayerID = value; }
        }

        private string _BillingProviderName = "";
        public string BillingProviderName
        {
            get
            {
                if (_BillingProviderName == null)
                { return ""; }
                else
                { return _BillingProviderName; }
            }
            set { this._BillingProviderName = value; }
        }

        private string _BillingProviderNPI = "";
        public string BillingProviderNPI
        {
            get
            {
                if (_BillingProviderNPI == null)
                { return ""; }
                else
                { return _BillingProviderNPI; }
            }
            set { this._BillingProviderNPI = value; }
        }


        private string _OrderingProviderName = "";
        public string OrderingProviderName
        {
            get
            {
                if (_OrderingProviderName == null)
                { return ""; }
                else
                { return _OrderingProviderName; }
            }
            set { this._OrderingProviderName = value; }
        }

        [DefaultValue(0)]
        public Int64 OrderingProviderID { get; set; }

        
        private string _OrderingProviderNPI = "";
        public string OrderingProviderNPI
        {
            get
            {
                if (_OrderingProviderNPI == null)
                { return ""; }
                else
                { return _OrderingProviderNPI; }
            }
            set { this._OrderingProviderNPI = value; } 
        }

        
        private string _SupervisingProviderName = "";
        public string SupervisingProviderName 
        {
            get 
            {
                if (_SupervisingProviderName == null)
                { return ""; }
                else
                { return _SupervisingProviderName; }
            }
            set { this._SupervisingProviderName = value; } 
        }

        [DefaultValue(0)]
        public Int64 SupervisingProviderID { get; set; }

        private string _SupervisingProviderNPI = "";
        public string SupervisingProviderNPI
        {
            get
            {
                if (_SupervisingProviderNPI == null)
                { return ""; }
                else
                { return _SupervisingProviderNPI; }
            }
            set { this._SupervisingProviderNPI = value; }
        }

        private string _FacilityName = "";
        public string FacilityName
        {
            get
            {
                if (_FacilityName == null)
                { return ""; }
                else
                { return _FacilityName; }
            }
            set { this._FacilityName = value; }
        }

        private string _ReferringProviderName = "";
        public string ReferringProviderName
        {
            get
            {
                if (_ReferringProviderName == null)
                { return ""; }
                else
                { return _ReferringProviderName; }
            }
            set { this._ReferringProviderName = value; }
        }

        [DefaultValue(0)]
        public Int64 ReferringProviderID { get; set; }

        private string _ReferringProviderNPI = "";
        public string ReferringProviderNPI
        {
            get
            {
                if (_ReferringProviderNPI == null)
                { return ""; }
                else
                { return _ReferringProviderNPI; }
            }
            set { this._ReferringProviderNPI = value; }
        }

        public DateTime HospitalizationFromDOS { get; set; }
        public DateTime HospitalizationToDOS { get; set; }

        public DateTime ClaimDate { get; set; }
        public DateTime OtherClaimDate { get; set; }

        private string _ClaimDateQualifier = "";
        public string ClaimDateQualifier
        {
            get
            {
                if (_ClaimDateQualifier == null)
                { return ""; }
                else
                { return _ClaimDateQualifier; }
            }
            set { this._ClaimDateQualifier = value; }
        }

        private string _OtherClaimDateQualifier = "";
        public string OtherClaimDateQualifier
        {
            get
            {
                if (_OtherClaimDateQualifier == null)
                { return ""; }
                else
                { return _OtherClaimDateQualifier; }
            }
            set { this._OtherClaimDateQualifier = value; }
        }

        private string _InsurancePlanType = "";
        public string InsurancePlanType
        {
            get
            {
                if (_InsurancePlanType == null)
                { return ""; }
                else
                { return _InsurancePlanType; }
            }
            set { this._InsurancePlanType = value; }
        }

        private string _CPTCode = "";
        public string CPTCode
        {
            get
            {
                if (_CPTCode == null)
                { return ""; }
                else
                { return _CPTCode; }
            }
            set { this._CPTCode = value; }
        }

        private string _POS = "";
        public string POS
        {
            get
            {
                if (_POS == null)
                { return ""; }
                else
                { return _POS; }
            }
            set { this._POS = value; }
        }

        private string _Dx1Code = "";
        public string Dx1Code
        {
            get
            {
                if (_Dx1Code == null)
                { return ""; }
                else
                { return _Dx1Code; }
            }
            set { this._Dx1Code = value; }
        }

        private string _Dx2Code = "";
        public string Dx2Code
        {
            get
            {
                if (_Dx2Code == null)
                { return ""; }
                else
                { return _Dx2Code; }
            }
            set { this._Dx2Code = value; }
        }

        private string _Dx3Code = "";
        public string Dx3Code
        {
            get
            {
                if (_Dx3Code == null)
                { return ""; }
                else
                { return _Dx3Code; }
            }
            set { this._Dx3Code = value; }
        }

        private string _Dx4Code = "";
        public string Dx4Code
        {
            get
            {
                if (_Dx4Code == null)
                { return ""; }
                else
                { return _Dx4Code; }
            }
            set { this._Dx4Code = value; }
        }

        private string _Mod1Code = "";
        public string Mod1Code
        {
            get
            {
                if (_Mod1Code == null)
                { return ""; }
                else
                { return _Mod1Code; }
            }
            set { this._Mod1Code = value; }
        }

        private string _Mod2Code = "";
        public string Mod2Code
        {
            get
            {
                if (_Mod2Code == null)
                { return ""; }
                else
                { return _Mod2Code; }
            }
            set { this._Mod2Code = value; }
        }

        private string _Mod3Code = "";
        public string Mod3Code
        {
            get
            {
                if (_Mod3Code == null)
                { return ""; }
                else
                { return _Mod3Code; }
            }
            set { this._Mod3Code = value; }
        }

        private string _Mod4Code = "";
        public string Mod4Code
        {
            get
            {
                if (_Mod4Code == null)
                { return ""; }
                else
                { return _Mod4Code; }
            }
            set { this._Mod4Code = value; }
        }

        [DefaultValue(0)]
        public decimal Unit { get; set; }

        public DateTime ChargeFromDOS { get; set; }
        public DateTime ChargeToDOS { get; set; }

        private string _PriorAuthorization = "";
        public string PriorAuthorization
        {
            get
            {
                if (_PriorAuthorization == null)
                { return ""; }
                else
                { return _PriorAuthorization; }
            }
            set { this._PriorAuthorization = value; }
        }
      
        [DefaultValue(0)]
        public Int64 RenderringProviderID { get; set; }
        
        private string _RenderringProviderName = "";
        public string RenderingProviderName
        {
            get
            {
                if (_RenderringProviderName == null)
                { return ""; }
                else
                { return _RenderringProviderName; }
            }
            set { this._RenderringProviderName = value; }
        }

        private string _RenderringProviderNPI = "";
        public string RenderingProviderNPI
        {
            get
            {
                if (_RenderringProviderNPI == null)
                { return ""; }
                else
                { return _RenderringProviderNPI; }
            }
            set { this._RenderringProviderNPI = value; }
        }
    }

    public class Insurance
    {       
        private string _sInsurancePayerID = "";
        public string InsurancePayerID
        {
            get
            {
                if (_sInsurancePayerID == null)
                { return ""; }
                else
                { return _sInsurancePayerID; }
            }
            set { this._sInsurancePayerID = value; }
        }

        private string _InsurancePlanName = "";
        public string InsurancePlanName
        {
            get
            {
                if (_InsurancePlanName == null)
                { return ""; }
                else
                { return _InsurancePlanName; }
            }
            set { this._InsurancePlanName = value; }
        }

        private string _InsurancePlanType = "";
        public string InsurancePlanType
        {
            get
            {
                if (_InsurancePlanType == null)
                { return ""; }
                else
                { return _InsurancePlanType; }
            }
            set { this._InsurancePlanType = value; }
        }

        private string _InsuranceReportingCategory = "";
        public string InsuranceReportingCategory
        {
            get
            {
                if (_InsuranceReportingCategory == null)
                { return ""; }
                else
                { return _InsuranceReportingCategory; }
            }
            set { this._InsuranceReportingCategory = value; }
        }

        private string _InsuranceCompanyName = "";
        public string InsuranceCompanyName
        {
            get
            {
                if (_InsuranceCompanyName == null)
                { return ""; }
                else
                { return _InsuranceCompanyName; }
            }
            set { this._InsuranceCompanyName = value; }
        }

        private string _InsuranceCompanyReportingCategory = "";
        public string InsuranceCompanyReportingCategory
        {
            get
            {
                if (_InsuranceCompanyReportingCategory == null)
                { return ""; }
                else
                { return _InsuranceCompanyReportingCategory; }
            }
            set { this._InsuranceCompanyReportingCategory = value; }
        }       

        public Int64 _nContactID = 0;
        public Int64 ContactID
        {
            get { return _nContactID; }
            set { this._nContactID = value; }
        }

        public Int64 _nInsuranceID = 0;
        public Int64 InsuranceID
        {
            get { return _nInsuranceID; }
            set { this._nInsuranceID = value; }
        }
    }

    public class CPT_Code
    {
        private string sCPTCode = "";
        public string CPTCode
        {
            get
            {
                if (sCPTCode == null)
                { return ""; }
                else
                { return sCPTCode; }
            }
            set { this.sCPTCode = value; }
        }
    }

    public class Claim_Modfier
    {
        private string sClaimModifier = "";
        public string ClaimModifier
        {
            get
            {
                if (sClaimModifier == null)
                { return ""; }
                else
                { return sClaimModifier; }
            }
            set { this.sClaimModifier = value; }
        }
    }

    public class RenderringProvider_Name
    {
        private string sRenderringProviderName = "";
        public string RenderingProviderName
        {
            get
            {
                if (sRenderringProviderName == null)
                { return ""; }
                else
                { return sRenderringProviderName; }
            }
            set { this.sRenderringProviderName = value; }
        }
    }

    public class RenderringProvider_NPI
    {
        private string sRenderringProviderNPI = "";
        public string RenderingProviderNPI
        {
            get
            {
                if (sRenderringProviderNPI == null)
                { return ""; }
                else
                { return sRenderringProviderNPI; }
            }
            set { this.sRenderringProviderNPI = value; }
        }
    }

    public class Claim_Diagnosis
    {
        private string sClaimDiagnosis = "";
        public string ClaimDiagnosis
        {
            get
            {
                if (sClaimDiagnosis == null)
                { return ""; }
                else
                { return sClaimDiagnosis; }
            }
            set { this.sClaimDiagnosis = value; }
        }
    }

    public class Claim_Facility
    {
        private string sClaimFacility = "";
        public string ClaimFacility
        {
            get
            {
                if (sClaimFacility == null)
                { return ""; }
                else
                { return sClaimFacility; }
            }
            set { this.sClaimFacility = value; }
        }

        private string sAUSID = "";
        public string AUSID
        {
            get
            {
                if (sAUSID == null)
                { return ""; }
                else
                { return sAUSID; }
            }
            set { this.sAUSID = value; }
        }

        private string sFacilityState = "";
        public string FacilityState
        {
            get
            {
                if (sFacilityState == null)
                { return ""; }
                else
                { return sFacilityState; }
            }
            set { this.sFacilityState = value; }
        }

        private string sFacilityTaxonomy = "";
        public string FacilityTaxonomy
        {
            get
            {
                if (sFacilityTaxonomy == null)
                { return ""; }
                else
                { return sFacilityTaxonomy; }
            }
            set { this.sFacilityTaxonomy = value; }
        }
    }
    public enum enumGender
    { 
        Male,
        Female,
        Other
    }

  
}
