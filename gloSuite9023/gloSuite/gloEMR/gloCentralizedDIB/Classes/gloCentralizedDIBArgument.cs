using System;
using System.Collections.Generic;
//using Medispan.DIB;
using System.Linq;

namespace gloCentralizedDIB
{
    #region Screening
    public class gloCentralizedDIBArgument : IDisposable
    {
        //public Medispan.DIB.DocLevel MajorDocLevel { get; set; }
        //public Medispan.DIB.DocLevel ModerateDocLevel { get; set; }
        //public Medispan.DIB.DocLevel MinorDocLevel { get; set; }

        //public Medispan.DIB.DocLevel DIScreenResults_MajorDocLevel { get; set; }
        //public Medispan.DIB.DocLevel DIScreenResults_ModerateDocLevel { get; set; }
        //public Medispan.DIB.DocLevel DIScreenResults_MinorDocLevel { get; set; }
        //public Medispan.DIB.CodeTypes CodeTypes { get; set; }

        //public Medispan.DIB.DrugNameTypeFilter DrugNameTypeFilter { get; set; }

        //public Medispan.DIB.DFAInteractionType DFAInteractionType { get; set; }
        //public PRCMgmtLevelFilter PRCMgmtLevelFilter { get; set; }
        //public INDProxyLevelFilter INDProxyLevelFilter { get; set; }
        //public ADESeverityFilter ADESeverityfilter { get; set; }

        public Int32 Age { get; set; }

        public string NDCCode { get; set; }
        //public PatientProfile PatientProfile { get; set; }

        public List<string> DrugsList;
        public List<string> AllergiesList;
        public List<Int32> MedicalConditions;

        public gloCentralizedDIBArgument()
        {
            this.DrugsList = new List<string>();
            this.AllergiesList = new List<string>();
            this.MedicalConditions = new List<Int32>();
        }


        public void Dispose()
        {
            this.NDCCode = string.Empty;

            //if (this.PatientProfile != null)
            //{ this.PatientProfile = null; }

            if (this.DrugsList != null)
            {
                DrugsList.Clear();
                DrugsList = null;
            }

            if (this.AllergiesList != null)
            {
                AllergiesList.Clear();
                AllergiesList = null;
            }

            if (this.MedicalConditions != null)
            {
                MedicalConditions.Clear();
                MedicalConditions = null;
            }
        }
    }

    public class gloCentralizedDIBPatientEducationArgument
    {
        public string Language { get; set; }
        public string ID { get; set; }
    }

    public class gloCentralizedADEArgument : IDisposable
    {
        public List<string> EffectTypeCode { get; set; }
       // public CodeTypes CodeType { get; set; }

        public gloCentralizedADEArgument()
        { this.EffectTypeCode = new List<string>(); }
        public void Dispose()
        {
            if (this.EffectTypeCode != null)
            {
                this.EffectTypeCode.Clear();
                this.EffectTypeCode = null;
            }
        }
    }

    public class ADEAdverseEffectsArg
    {
        //public ADESeverityFilter ADESeverityFilter { get; set; }
        public Int32 ID { get; set; }
        //public ADEIncidenceFilter ADEIncidenceFilter { get; set; }
        //public ADEDocLevelFilter ADEDocLevelFilter { get; set; }
    }

    public class Interaction
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class ADESearchMedicalConditionsArgument
    {
        public string SubString { get; set; }
        //public SearchMethod SearchMethod { get; set; }
        //public PhoneticSearch PhoneticSearch { get; set; }
        //public MedCondNameSearchType MedCondNameSearchType { get; set; }
        public Boolean IncludeSymtoms { get; set; }
        public Boolean HasDrugsToTreatOnly { get; set; }
    }

    public class MonogramArgument : IDisposable
    {
        public string Language { get; set; }
        public string ID { get; set; }

        public string Drug1 { get; set; }
        public string Drug2 { get; set; }

        public MonogramType MonogramType { get; set; }

        public void Dispose()
        {
            this.Language = string.Empty;
            this.ID = string.Empty;
            this.Drug1 = string.Empty;
            this.Drug2 = string.Empty;
        }
    }

    public enum MonogramType
    {
        DI,
        DFA,
        PAR
    } 
    #endregion

    public enum URL
    { 
        PARScreening,
        RxHGetTherapeuticAlternatives_DIB_Formulary,
        ShowMonograph,
        DIScreening,
        DTScreening,
        DFAScreening,
        PatientEducation,
        ADEScreening,
        PRCScreening,
        GetEffectTypeCodeList,
        ADEGetScreeningForDrugs,    
        ADEAdverseEffectsForMedicalCondition,
        ADESearchMedicalConditions,
        ADEFillMedicalConditions,
        GetGPI,
        GetMedicationGenericName,
        CheckURLConnectivity,
        GetnCheckNDCInDrugPack,
        GetDoseCheckGPI,
        GetEffectAndCommentCodes,
        GetDrugsForms,
        GetDrugQtyQualifier,
        GetDrugQualifier,
        FetchClassifiedDrugs,
        FetchClassifiedDrugsExtended
    }

   
#region "DIB Query Calls"
    public class DoseCheckGPIArgument
    {
        public string GPI { get; set; }
        public string RecordType { get; set; }
        public string RecordSubType { get; set; }
    }

    public class FetchClassifiedDrugsArgument
    {
        public Int64 ClassID { get; set; }
        public string Substring { get; set; }
    }

    public class FetchClassifiedDrugsExtendedArgument : IDisposable
    {
        public Int64 ClassID { get; set; }
        public List<Int32> mpid { get; set; }

        public FetchClassifiedDrugsExtendedArgument()
        { this.mpid = new List<int>(); }

        public void Dispose()
        {
            if (this.mpid != null)
            {
                this.mpid.Clear();
                this.mpid = null;
            }
        }
    }
#endregion
}
