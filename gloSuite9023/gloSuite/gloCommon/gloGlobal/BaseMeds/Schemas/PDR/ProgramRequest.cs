using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace gloGlobal.Schemas.PDR
{

    #region Request
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false, ElementName = "request")]
    public partial class ProgramRequest : IDisposable
    {
        #region Variables
        private string rxNumberField;
        private string productRxNormField;
        private string productNDCField;

        private string refillsTotalField;
        private string quantityField;
        private string daysSuppliedField;

        private string sigField;
        private string dosageFormField;
        private string doseField;

        private string frequencyField;
        private string routeOfAdministrationField;
        private string renewalIndicatorField;

        private string prescriptionFormField;
        private string priorAuthorizationField;
        private string sourceField;

        private string portalIDField;
        private bool portalIDFieldSpecified;
        private string pFormatField;

        private requestPatient patientField;
        private requestFinancial financialField;
        private requestPharmacy pharmacyField;

        private requestLocation locationField;
        private requestPrescriber prescriberField;

        private string wFormatField;
        private string outputField;
        #endregion

        #region Properties
        public string rxNumber
        {
            get { return this.rxNumberField; }
            set { this.rxNumberField = value; }
        }

        public string productRxNorm
        {
            get { return this.productRxNormField; }
            set { this.productRxNormField = value; }
        }

        public string productNDC
        {
            get { return this.productNDCField; }
            set { this.productNDCField = value; }
        }

        public string refillsTotal
        {
            get { return this.refillsTotalField; }
            set { this.refillsTotalField = value; }
        }

        public string quantity
        {
            get { return this.quantityField; }
            set { this.quantityField = value; }
        }

        public string daysSupplied
        {
            get { return this.daysSuppliedField; }
            set { this.daysSuppliedField = value; }
        }

        public string sig
        {
            get { return this.sigField; }
            set { this.sigField = value; }
        }

        public string dosageForm
        {
            get
            {
                return this.dosageFormField;
            }
            set
            {
                this.dosageFormField = value;
            }
        }

        public string dose
        {
            get
            {
                return this.doseField;
            }
            set
            {
                this.doseField = value;
            }
        }


        public string frequency
        {
            get
            {
                return this.frequencyField;
            }
            set
            {
                this.frequencyField = value;
            }
        }


        public string routeOfAdministration
        {
            get
            {
                return this.routeOfAdministrationField;
            }
            set
            {
                this.routeOfAdministrationField = value;
            }
        }


        public string renewalIndicator
        {
            get
            {
                return this.renewalIndicatorField;
            }
            set
            {
                this.renewalIndicatorField = value;
            }
        }


        public string prescriptionForm
        {
            get
            {
                return this.prescriptionFormField;
            }
            set
            {
                this.prescriptionFormField = value;
            }
        }


        public string priorAuthorization
        {
            get
            {
                return this.priorAuthorizationField;
            }
            set
            {
                this.priorAuthorizationField = value;
            }
        }


        public requestPatient patient 
        {
            get
            {
                if (this.patientField == null)
                { this.patientField = new requestPatient(); }

                return this.patientField;
            }
            set
            {
                this.patientField = value;
            }
        }


        public requestFinancial financial
        {
            get
            {
                if (this.financialField == null)
                { this.financialField = new requestFinancial(); }

                return this.financialField;
            }
            set
            {
                this.financialField = value;
            }
        }


        public requestPharmacy pharmacy
        {
            get
            {
                if (this.pharmacyField == null)
                { this.pharmacyField = new requestPharmacy(); }

                return this.pharmacyField;
            }
            set
            {
                this.pharmacyField = value;
            }
        }


        public requestLocation location
        {
            get
            {
                if (this.locationField == null)
                { this.locationField = new requestLocation(); }

                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }


        public requestPrescriber prescriber
        {
            get
            {
                if (this.prescriberField == null)
                { this.prescriberField = new requestPrescriber(); }

                return this.prescriberField;
            }
            set
            {
                this.prescriberField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string source
        {
            get
            {
                return this.sourceField;
            }
            set
            {
                this.sourceField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string portalID
        {
            get
            {
                return this.portalIDField;
            }
            set
            {
                if (value != "") { this.portalIDSpecified = true; }
                else { this.portalIDSpecified = false; }

                this.portalIDField = value;
            }
        }


        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool portalIDSpecified
        {
            get
            {
                return this.portalIDFieldSpecified;
            }
            set
            {
                this.portalIDFieldSpecified = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string pFormat
        {
            get
            {
                return this.pFormatField;
            }
            set
            {
                this.pFormatField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string wFormat
        {
            get
            {
                return this.wFormatField;
            }
            set
            {
                this.wFormatField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string output
        {
            get
            {
                return this.outputField;
            }
            set
            {
                this.outputField = value;
            }
        }
        #endregion

        public void Dispose()
        {
            this.daysSupplied = string.Empty;
            this.dosageForm = string.Empty;
            this.dose = string.Empty;
            this.frequency = string.Empty;
            this.output = string.Empty;
            this.pFormat = string.Empty;
            this.portalID = string.Empty;
            this.portalIDSpecified = false;
            this.prescriptionForm = string.Empty;
            this.priorAuthorization = string.Empty;
            this.productNDC = string.Empty;
            this.productRxNorm = string.Empty;
            this.quantity = string.Empty;
            this.refillsTotal = string.Empty;
            this.renewalIndicator = string.Empty;
            this.routeOfAdministration = string.Empty;
            this.rxNumber = string.Empty;
            this.sig = string.Empty;
            this.source = string.Empty;
            this.wFormat = string.Empty;            

            if (this.financial != null)
            {
                this.financial.Dispose();
                this.financial = null;
            }
                        
            if (this.location != null)
            {
                this.location.Dispose();
                this.location = null;
            }

            this.output = string.Empty;
            if (this.patient != null)
            {
                this.patient.Dispose();
                this.patient = null;
            }

        }
    }
    #endregion

    #region Patient
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPatient : IDisposable
    {
        #region Variables
        private string codeField;

        private string medicaidIDField;

        private string firstnameField;

        private string lastnameField;

        private string dobField;

        private string ageField;

        private string genderField;

        private string addressField;

        private string cityField;

        private string stateField;

        private string zipField;

        private string phoneField;

        private string heightField;

        private string weightField;

        private string languageField;

        private string preferenceField;

        private string eligibilityField;

        private List<requestPatientDiagnosis> diagnosisField;

        private List<requestPatientImmunization> immunizationsField;

        private List<requestPatientLab> labsField;

        private List<requestPatientAllergy> allergiesField;
        #endregion

        #region Properties
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }


        public string medicaidID
        {
            get
            {
                return this.medicaidIDField;
            }
            set
            {
                this.medicaidIDField = value;
            }
        }


        public string firstname
        {
            get
            {
                return this.firstnameField;
            }
            set
            {
                this.firstnameField = value;
            }
        }


        public string lastname
        {
            get
            {
                return this.lastnameField;
            }
            set
            {
                this.lastnameField = value;
            }
        }


        public string dob
        {
            get
            {
                return this.dobField;
            }
            set
            {
                this.dobField = value;
            }
        }


        public string age
        {
            get
            {
                return this.ageField;
            }
            set
            {
                this.ageField = value;
            }
        }


        public string gender
        {
            get
            {
                return this.genderField;
            }
            set
            {
                this.genderField = value;
            }
        }


        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }


        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }


        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }


        public string zip
        {
            get
            {
                return this.zipField;
            }
            set
            {
                this.zipField = value;
            }
        }


        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }


        public string height
        {
            get
            {
                return this.heightField;
            }
            set
            {
                this.heightField = value;
            }
        }


        public string weight
        {
            get
            {
                return this.weightField;
            }
            set
            {
                this.weightField = value;
            }
        }


        public string language
        {
            get
            {
                return this.languageField;
            }
            set
            {
                this.languageField = value;
            }
        }


        public string preference
        {
            get
            {
                return this.preferenceField;
            }
            set
            {
                this.preferenceField = value;
            }
        }


        public string eligibility
        {
            get
            {
                return this.eligibilityField;
            }
            set
            {
                this.eligibilityField = value;
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("diagnosis", IsNullable = false)]
        public List<requestPatientDiagnosis> diagnosis
        {
            get
            {
                return this.diagnosisField;
            }
            set
            {
                this.diagnosisField = value;
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("immunization", IsNullable = false)]
        public List<requestPatientImmunization> immunizations
        {
            get
            {
                return this.immunizationsField;
            }
            set
            {
                this.immunizationsField = value;
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("lab", IsNullable = false)]
        public List<requestPatientLab> labs
        {
            get
            {
                return this.labsField;
            }
            set
            {
                this.labsField = value;
            }
        }


        [System.Xml.Serialization.XmlArrayItemAttribute("allergy", IsNullable = false)]
        public List<requestPatientAllergy> allergies
        {
            get
            {
                return this.allergiesField;
            }
            set
            {
                this.allergiesField = value;
            }
        }
        #endregion

        public void Dispose()
        {
            this.address = string.Empty;
            this.age = string.Empty;
            this.city = string.Empty;
            this.code = string.Empty;
            this.language = string.Empty;

            this.dob = string.Empty;
            this.eligibility = string.Empty;
            this.firstname = string.Empty;
            this.gender = string.Empty;
            this.height = string.Empty;
            
            this.language = string.Empty;
            this.medicaidID = string.Empty;
            this.phone = string.Empty;
            this.preference = string.Empty;
            this.state = string.Empty;
            this.weight = string.Empty;
            this.zip = string.Empty;            

            if (this.allergies != null)
            {
                foreach (requestPatientAllergy a in this.allergies)
                {
                    a.Dispose();
                }

                this.allergies.Clear();
                this.allergies = null;
            }

            

            if (this.diagnosis != null)
            {
                foreach (requestPatientDiagnosis d in this.diagnosis)
                {
                    d.Dispose();                    
                }
                this.diagnosis.Clear();
                this.diagnosis = null;
            }

            if (this.labs != null)
            {
                foreach (requestPatientLab l in this.labs)
                {
                    l.Dispose();
                }
                this.labs.Clear();
                this.labs = null;
            }

            if (this.immunizations != null)
            {
                foreach (requestPatientImmunization i in this.immunizations)
                {
                    i.Dispose();
                }
                this.immunizations.Clear();
                this.immunizations = null;
            }

           
            
        }
    }
    #endregion

    #region Diagnosis
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPatientDiagnosis : IDisposable
    {

        #region Variables
        private string codeSystemField;

        private string valueField;

        private bool valueFieldSpecified;

        private string descriptionField;
        #endregion

        #region Properties
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string codeSystem
        {
            get
            {
                return this.codeSystemField;
            }
            set
            {
                this.codeSystemField = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }


        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valueSpecified
        {
            get
            {
                return this.valueFieldSpecified;
            }
            set
            {
                this.valueFieldSpecified = value;
            }
        }


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
        #endregion


        public void Dispose()
        {
            this.codeSystem = string.Empty;
            this.description = string.Empty;
        }
    }
    #endregion

    #region Immunization
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPatientImmunization : IDisposable
    {

        private string valueField;

        private bool valueFieldSpecified;


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }


        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool valueSpecified
        {
            get
            {
                return this.valueFieldSpecified;
            }
            set
            {
                this.valueFieldSpecified = value;
            }
        }

        public void Dispose()
        {
            this.value = string.Empty;
            this.valueSpecified = false;
        }
    }
    #endregion

    #region Labs
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPatientLab : IDisposable
    {

        private string valueField;


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        public void Dispose()
        {
            this.value = string.Empty;
        }
    }
    #endregion

    #region Allergy
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPatientAllergy : IDisposable
    {

        private string valueField;


        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        public void Dispose()
        {
            this.value = string.Empty;
        }
    }
    #endregion

    #region Financial
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancial : IDisposable
    {
        private requestFinancialMedicalBenefit medicalBenefitField;

        private requestFinancialPrescriptionBenefit prescriptionBenefitField;

        public requestFinancialMedicalBenefit medicalBenefit
        {
            get
            {
                if (this.medicalBenefitField == null)
                { this.medicalBenefitField = new requestFinancialMedicalBenefit(); }
                return this.medicalBenefitField;
            }
            set
            {
                this.medicalBenefitField = value;
            }
        }

        public requestFinancialPrescriptionBenefit prescriptionBenefit
        {
            get
            {
                if (this.prescriptionBenefitField == null)
                { this.prescriptionBenefitField = new requestFinancialPrescriptionBenefit(); }
                return this.prescriptionBenefitField;
            }
            set
            {
                this.prescriptionBenefitField = value;
            }
        }

        public void Dispose()
        {
            if (this.medicalBenefit != null) 
            {
                this.medicalBenefit.Dispose();
                this.medicalBenefit = null;
            }

            if (this.prescriptionBenefit != null)
            {
                this.prescriptionBenefit.Dispose();
                this.prescriptionBenefit = null;
            }
        }
    }
    #endregion

    #region Medical Benefit
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancialMedicalBenefit : IDisposable
    {
        private requestFinancialMedicalBenefitPrimary primaryField;
        private requestFinancialMedicalBenefitSecondary secondaryField;

        public requestFinancialMedicalBenefitPrimary primary
        {
            get
            {
                return this.primaryField;
            }
            set
            {
                this.primaryField = value;
            }
        }
        public requestFinancialMedicalBenefitSecondary secondary
        {
            get
            {
                return this.secondaryField;
            }
            set
            {
                this.secondaryField = value;
            }
        }

        public void Dispose()
        {
            if (this.primary != null)
            {
                this.primary.Dispose();
                this.primary = null;
            }

            if (this.secondary != null)
            {
                this.secondary.Dispose();
                this.secondary = null;
            }
        }
    }
    #endregion

    #region Medical Benefit Primary
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancialMedicalBenefitPrimary : IDisposable
    {

        private string claimTypeField;

        private string binField;

        private string pcnField;

        private string planIDField;

        private string planCodeField;

        private string planNameField;

        private string groupIDField;


        public string claimType
        {
            get
            {
                return this.claimTypeField;
            }
            set
            {
                this.claimTypeField = value;
            }
        }


        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }


        public string pcn
        {
            get
            {
                return this.pcnField;
            }
            set
            {
                this.pcnField = value;
            }
        }


        public string planID
        {
            get
            {
                return this.planIDField;
            }
            set
            {
                this.planIDField = value;
            }
        }


        public string planCode
        {
            get
            {
                return this.planCodeField;
            }
            set
            {
                this.planCodeField = value;
            }
        }


        public string planName
        {
            get
            {
                return this.planNameField;
            }
            set
            {
                this.planNameField = value;
            }
        }


        public string groupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }

        public void Dispose()
        {
            this.bin = string.Empty;
            this.claimType = string.Empty;
            this.groupID = string.Empty;
            this.pcn = string.Empty;
            this.planCode = string.Empty;
            this.planID = string.Empty;
            this.planName = string.Empty;            
        }
    }
    #endregion

    #region Medical Benefit Secondary
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancialMedicalBenefitSecondary : IDisposable
    {

        private string claimTypeField;

        private string binField;

        private string pcnField;

        private string planIDField;

        private string planCodeField;

        private string planNameField;

        private string groupIDField;


        public string claimType
        {
            get
            {
                return this.claimTypeField;
            }
            set
            {
                this.claimTypeField = value;
            }
        }


        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }


        public string pcn
        {
            get
            {
                return this.pcnField;
            }
            set
            {
                this.pcnField = value;
            }
        }


        public string planID
        {
            get
            {
                return this.planIDField;
            }
            set
            {
                this.planIDField = value;
            }
        }


        public string planCode
        {
            get
            {
                return this.planCodeField;
            }
            set
            {
                this.planCodeField = value;
            }
        }


        public string planName
        {
            get
            {
                return this.planNameField;
            }
            set
            {
                this.planNameField = value;
            }
        }


        public string groupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }

        public void Dispose()
        {
            this.bin = string.Empty;
            this.claimType = string.Empty;
            this.groupID = string.Empty;
            this.pcn = string.Empty;
            this.planCode = string.Empty;
            this.planID = string.Empty;
            this.planName = string.Empty;            
        }
    }
    #endregion

    #region Prescription Benefit
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancialPrescriptionBenefit : IDisposable
    {
        private requestFinancialPrescriptionBenefitPrimary primaryField;
        private requestFinancialPrescriptionBenefitSecondary secondaryField;


        public requestFinancialPrescriptionBenefitPrimary primary
        {
            get
            {
                return this.primaryField;
            }
            set
            {
                this.primaryField = value;
            }
        }
        public requestFinancialPrescriptionBenefitSecondary secondary
        {
            get
            {
                return this.secondaryField;
            }
            set
            {
                this.secondaryField = value;
            }
        }

        public void Dispose()
        {            
            if (this.primary != null)
            { 
                this.primary.Dispose();
                this.primary = null;
            }

            if (this.secondary != null)
            {
                this.secondary.Dispose();
                this.secondary = null;
            }
        }
    }
    #endregion

    #region Prescription Benefit Primary
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancialPrescriptionBenefitPrimary : IDisposable
    {

        private string claimTypeField;

        private string binField;

        private string pcnField;

        private string planIDField;

        private string planCodeField;

        private string planNameField;

        private string groupIDField;

        private string cardHolderField;


        public string claimType
        {
            get
            {
                return this.claimTypeField;
            }
            set
            {
                this.claimTypeField = value;
            }
        }


        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }


        public string pcn
        {
            get
            {
                return this.pcnField;
            }
            set
            {
                this.pcnField = value;
            }
        }


        public string planID
        {
            get
            {
                return this.planIDField;
            }
            set
            {
                this.planIDField = value;
            }
        }


        public string planCode
        {
            get
            {
                return this.planCodeField;
            }
            set
            {
                this.planCodeField = value;
            }
        }


        public string planName
        {
            get
            {
                return this.planNameField;
            }
            set
            {
                this.planNameField = value;
            }
        }


        public string groupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }


        public string cardHolder
        {
            get
            {
                return this.cardHolderField;
            }
            set
            {
                this.cardHolderField = value;
            }
        }

        public void Dispose()
        {
            this.bin = string.Empty;
            this.claimType = string.Empty;
            this.groupID = string.Empty;
            this.pcn = string.Empty;
            this.planCode = string.Empty;
            this.planID = string.Empty;
            this.planName = string.Empty;            
        }
    }
    #endregion

    #region Prescription Benefit Secondary
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestFinancialPrescriptionBenefitSecondary : IDisposable
    {

        private string claimTypeField;

        private string binField;

        private string pcnField;

        private string planIDField;

        private string planCodeField;

        private string planNameField;

        private string groupIDField;

        private string cardholderField;


        public string claimType
        {
            get
            {
                return this.claimTypeField;
            }
            set
            {
                this.claimTypeField = value;
            }
        }


        public string bin
        {
            get
            {
                return this.binField;
            }
            set
            {
                this.binField = value;
            }
        }


        public string pcn
        {
            get
            {
                return this.pcnField;
            }
            set
            {
                this.pcnField = value;
            }
        }


        public string planID
        {
            get
            {
                return this.planIDField;
            }
            set
            {
                this.planIDField = value;
            }
        }


        public string planCode
        {
            get
            {
                return this.planCodeField;
            }
            set
            {
                this.planCodeField = value;
            }
        }


        public string planName
        {
            get
            {
                return this.planNameField;
            }
            set
            {
                this.planNameField = value;
            }
        }


        public string groupID
        {
            get
            {
                return this.groupIDField;
            }
            set
            {
                this.groupIDField = value;
            }
        }


        public string cardholder
        {
            get
            {
                return this.cardholderField;
            }
            set
            {
                this.cardholderField = value;
            }
        }

        public void Dispose()
        {
            this.bin = string.Empty;
            this.claimType = string.Empty;
            this.groupID = string.Empty;
            this.pcn = string.Empty;
            this.planCode = string.Empty;
            this.planID = string.Empty;
            this.planName = string.Empty;            
        }
    }
    #endregion

    #region Pharmacy
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPharmacy : IDisposable
    {

        private string codeField;

        private string ncpdpField;

        private string medicaidIDField;

        private string nameField;

        private string addressField;

        private string cityField;

        private string stateField;

        private string zipField;

        private string phoneField;

        private string faxField;


        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }


        public string ncpdp
        {
            get
            {
                return this.ncpdpField;
            }
            set
            {
                this.ncpdpField = value;
            }
        }


        public string medicaidID
        {
            get
            {
                return this.medicaidIDField;
            }
            set
            {
                this.medicaidIDField = value;
            }
        }


        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }


        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }


        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }


        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }


        public string zip
        {
            get
            {
                return this.zipField;
            }
            set
            {
                this.zipField = value;
            }
        }


        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }


        public string fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }

        public void Dispose()
        {
            this.address = string.Empty;
            this.city = string.Empty;
            this.code = string.Empty;
            this.fax = string.Empty;
            this.medicaidID = string.Empty;
            this.name = string.Empty;
        }
    }
    #endregion

    #region Location
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestLocation : IDisposable
    {

        private string codeField;

        private string nameField;

        private string addressField;

        private string cityField;

        private string stateField;

        private string zipField;

        private string phoneField;

        private string faxField;


        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }


        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }


        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }


        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }


        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }


        public string zip
        {
            get
            {
                return this.zipField;
            }
            set
            {
                this.zipField = value;
            }
        }


        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }


        public string fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }

        public void Dispose()
        {
            this.address = string.Empty;
            this.city = string.Empty;
            this.code = string.Empty;
            this.fax = string.Empty;
            this.name = string.Empty;
            this.phone = string.Empty;
            this.state = string.Empty;
            this.zip = string.Empty;            
        }
    }
    #endregion

    #region Prescriber
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class requestPrescriber : IDisposable
    {

        private string codeField;

        private string deaField;

        private string medicaidNumberField;

        private string medicaidBillingNumberField;

        private string firstNameField;

        private string lastNameField;

        private string specialtyField;

        private string addressField;

        private string cityField;

        private string stateField;

        private string zipField;

        private string phoneField;

        private string faxField;

        private string emailField;


        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }


        public string dea
        {
            get
            {
                return this.deaField;
            }
            set
            {
                this.deaField = value;
            }
        }


        public string medicaidNumber
        {
            get
            {
                return this.medicaidNumberField;
            }
            set
            {
                this.medicaidNumberField = value;
            }
        }


        public string medicaidBillingNumber
        {
            get
            {
                return this.medicaidBillingNumberField;
            }
            set
            {
                this.medicaidBillingNumberField = value;
            }
        }


        public string firstName
        {
            get
            {
                return this.firstNameField;
            }
            set
            {
                this.firstNameField = value;
            }
        }


        public string lastName
        {
            get
            {
                return this.lastNameField;
            }
            set
            {
                this.lastNameField = value;
            }
        }


        public string specialty
        {
            get
            {
                return this.specialtyField;
            }
            set
            {
                this.specialtyField = value;
            }
        }


        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }


        public string city
        {
            get
            {
                return this.cityField;
            }
            set
            {
                this.cityField = value;
            }
        }


        public string state
        {
            get
            {
                return this.stateField;
            }
            set
            {
                this.stateField = value;
            }
        }


        public string zip
        {
            get
            {
                return this.zipField;
            }
            set
            {
                this.zipField = value;
            }
        }


        public string phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }


        public string fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
            }
        }


        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        public void Dispose()
        {
            this.address = string.Empty;
            this.city = string.Empty;
            this.code = string.Empty;
            this.dea = string.Empty;
            this.email = string.Empty;
            this.fax = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.medicaidBillingNumber = string.Empty;
            this.medicaidNumber = string.Empty;
            this.phone = string.Empty;
            this.specialty = string.Empty;
            this.state = string.Empty;
            this.zip = string.Empty;            
        }
    }
    #endregion

}

