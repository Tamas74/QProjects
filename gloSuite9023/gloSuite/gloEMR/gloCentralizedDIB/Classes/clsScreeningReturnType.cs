using System;
using System.Collections.Generic;
using System.Linq;
namespace gloCentralizedDIB
{
    public class DIBReturnType
    {
        public List<Interaction> InteractionList { get; set; }
        public List<Interaction> ResultList { get; set; }
        public string DrugAlert { get; set; }

        public DIBReturnType()
        {
            this.InteractionList = new List<Interaction>();
            this.ResultList = new List<Interaction>();
            this.DrugAlert = string.Empty;
        }
    }

    #region DI Screen
    public class DIScreenReturnType : IDisposable
    {
        public string MonographId { get; set; }
        public string Drug1Description { get; set; }
        public string Drug2Description { get; set; }

        public string Description { get; set; }
        public string ScreenMessage { get; set; }

        public List<string> PatientDrugDescription { get; set; }

        public DIScreenReturnType()
        {
            this.PatientDrugDescription = new List<string>();
        }

        public void Dispose()
        {
            if (this.PatientDrugDescription != null)
            {
                this.PatientDrugDescription.Clear();
                this.PatientDrugDescription = null;
            }

            this.MonographId = string.Empty;
            this.Drug1Description = string.Empty;
            this.Drug2Description = string.Empty;
            this.Description = string.Empty;
            this.ScreenMessage = string.Empty;
        }
    }

    public class GroupedDIScreenReturnType : IDisposable
    {
        public List<DIScreenReturnType> DIScreenReturnTypes { get; set; }
        public List<string> MessageText { get; set; }

        public GroupedDIScreenReturnType()
        {
            this.DIScreenReturnTypes = new List<DIScreenReturnType>();
            this.MessageText = new List<string>();
        }

        public void Dispose()
        {
            if (this.DIScreenReturnTypes != null)
            {
                this.DIScreenReturnTypes.Clear();
                this.DIScreenReturnTypes = null;
            }
            if (this.MessageText != null)
            {
                this.MessageText.Clear();
                this.MessageText = null;
            }
        }
    }
    #endregion

    #region DT Screening
    public class GroupedDTScreenReturnType : IDisposable
    {
        public List<DTScreenReturnType> DTScreenReturnTypes { get; set; }
        public List<DTMessage> DTMessageList { get; set; }

        public GroupedDTScreenReturnType()
        {
            this.DTScreenReturnTypes = new List<DTScreenReturnType>();
            this.DTMessageList = new List<DTMessage>();
        }

        public void Dispose()
        {
            if (this.DTMessageList != null)
            {
                this.DTMessageList.Clear();
                this.DTMessageList = null;
            }

            if (this.DTScreenReturnTypes != null)
            {
                this.DTScreenReturnTypes.Clear();
                this.DTScreenReturnTypes = null;
            }
        }
    }

    public class DTMessage
    {
        public string ItemDescription { get; set; }
        public string MessageText { get; set; }
    }

    public class DTScreenReturnType : IDisposable
    {
        public string ClassDescription { get; set; }
        public Int32 Allowance { get; set; }
        public string ScreenMessage { get; set; }

        public string ObjectCodeDescription { get; set; }
        public List<string> Descriptions { get; set; }

        public DTScreenReturnType()
        {
            this.Descriptions = new List<string>();
        }

        public void Dispose()
        {
            if (this.Descriptions != null)
            {
                this.Descriptions.Clear();
                this.Descriptions = null;
            }

            this.ObjectCodeDescription = string.Empty;
            this.ClassDescription = string.Empty;
            this.ScreenMessage = string.Empty;
        }
    }

    public class GroupedDFAReturnType : IDisposable
    {
        public List<DFAReturnType> DFAReturnList { get; set; }
        public List<DTMessage> DTMessageList { get; set; }

        public GroupedDFAReturnType()
        {
            this.DFAReturnList = new List<DFAReturnType>();
            this.DTMessageList = new List<DTMessage>();
        }



        public void Dispose()
        {
            if (this.DFAReturnList != null)
            {
                if (this.DFAReturnList.Any())
                { this.DFAReturnList.Clear(); }

                this.DFAReturnList = null;
            }

            if (this.DTMessageList != null)
            {
                if (this.DTMessageList.Any())
                { this.DTMessageList.Clear(); }

                this.DTMessageList = null;
            }

        }
    }

    public class DFAReturnType
    {

        public Int32 DrugClassDuration { get; set; }
        public Int32 FoodClassDuration { get; set; }

        public string FoodClassDesc { get; set; }
        public string DrugDescription { get; set; }
        public string DrugClassDesc { get; set; }
        public string OnsetCodeDescription { get; set; }
        public string SeverityCodeDescription { get; set; }
        public string DocLevelCodeDescription { get; set; }

        public Boolean DrugClassSchedAsNeeded { get; set; }
        public Boolean DrugClassSchedOneTime { get; set; }
        public Boolean DrugClassSchedReg { get; set; }
        public Boolean FoodClassSchedAsNeeded { get; set; }
        public Boolean FoodClassSchedOneTime { get; set; }
        public Boolean FoodClassSchedReg { get; set; }

        public string ScreenMessage { get; set; }
        public string MonographId { get; set; }
        public string DrugID { get; set; }

        public List<DTMessage> MessagesList { get; set; }

        public DFAReturnType()
        {
            this.MessagesList = new List<DTMessage>();
        }
    }


    #endregion

    #region PAR Screening
    public class GroupedPARScreenReturnType : IDisposable
    {
        public List<PARScreenReturnType> PARScreenReturnList { get; set; }
        public List<PARScreenReturnMessageType> PARScreenReturnMessageTypeList { get; set; }

        public GroupedPARScreenReturnType()
        {
            this.PARScreenReturnList = new List<PARScreenReturnType>();
            this.PARScreenReturnMessageTypeList = new List<PARScreenReturnMessageType>();
        }

        public void Dispose()
        {
            if (this.PARScreenReturnList != null)
            {
                this.PARScreenReturnList.Clear();
                this.PARScreenReturnList = null;
            }

            if (this.PARScreenReturnMessageTypeList != null)
            {
                this.PARScreenReturnMessageTypeList.Clear();
                this.PARScreenReturnMessageTypeList = null;
            }
        }
    }

    public class PARScreenReturnType
    {
      //  public Medispan.DIB.AllergyMatchTypes AllergyMatchType { get; set; }
        public string ScreenMessage { get; set; }
        public string AllergyClassDesc { get; set; }
        public string DrugClassDesc { get; set; }

        public Boolean HasIntSymAnemia { get; set; }
        public Boolean HasIntSymAsthma { get; set; }
        public Boolean HasIntSymNausea { get; set; }
        public Boolean HasIntSymOther { get; set; }
        public Boolean HasIntSymRash { get; set; }
        public Boolean HasIntSymShock { get; set; }

        public Boolean HasRptSymAnemia { get; set; }
        public Boolean HasRptSymAsthma { get; set; }
        public Boolean HasRptSymNausea { get; set; }
        public Boolean HasRptSymOther { get; set; }
        public Boolean HasRptSymRash { get; set; }
        public Boolean HasRptSymShock { get; set; }
        public string MonographId { get; set; }
    }

    public class PARScreenReturnMessageType
    {
        public string MessageText { get; set; }
    }


    #endregion

    #region ADEScreening

    public class ADEFillMedicalConditionsReturn
    {
        public Int32 ID { get; set; }
        public string NamePatient { get; set; }
    }

    public class GroupedADEFillMedicalConditionsReturn : IDisposable
    {

        public List<ADEFillMedicalConditionsReturn> Conditions { get; set; }

        public GroupedADEFillMedicalConditionsReturn()
        {
            this.Conditions = new List<ADEFillMedicalConditionsReturn>();
        }

        public void Dispose()
        {
            if (this.Conditions != null)
            {
                this.Conditions.Clear();
                this.Conditions = null;
            }
        }
    }

    public class ADESearchMedicalConditionsReturn
    {
        public Int32 ID { get; set; }
        public string NameProfessional { get; set; }
    }

    public class GroupedADESearchMedicalConditions : IDisposable
    {

        public List<ADESearchMedicalConditionsReturn> Conditions { get; set; }

        public GroupedADESearchMedicalConditions()
        {
            this.Conditions = new List<ADESearchMedicalConditionsReturn>();
        }

        public void Dispose()
        {
            if (this.Conditions != null)
            {
                this.Conditions.Clear();
                this.Conditions = null;
            }
        }
    }

    public class EffectTypeCodeDescriptionList : IDisposable
    {
        public List<string> EffectDescriptionList { get; set; }

        public EffectTypeCodeDescriptionList()
        { this.EffectDescriptionList = new List<string>(); }

        public void Dispose()
        {
            if (this.EffectDescriptionList != null)
            {
                this.EffectDescriptionList.Clear();
                this.EffectDescriptionList = null;
            }
        }
    }

    public class GroupedADELookupResult : IDisposable
    {
        public List<ADELookupResultProperties> Results { get; set; }

        public GroupedADELookupResult()
        {
            this.Results = new List<ADELookupResultProperties>();
        }

        public void Dispose()
        {
            if (this.Results != null)
            {
                this.Results.Clear();
                this.Results = null;
            }
        }
    }

    public class ADELookupResultProperties : IDisposable
    {
        public string MedCondNameProf { get; set; }
        public Int32 MedCondId { get; set; }

        public string DispensableDrugDescription { get; set; }
        public Int32 DispensableDrugId { get; set; }

        public void Dispose()
        {
            this.MedCondNameProf = string.Empty;
            this.MedCondId = 0;

            this.DispensableDrugDescription = string.Empty;
            this.DispensableDrugId = 0;
        }
    }

    public class GroupedADEResults : IDisposable
    {
        public List<ADEReturnType> ADEReturnList { get; set; }
        public List<ADEMessageType> ADEMessageList { get; set; }

        public GroupedADEResults()
        {
            this.ADEMessageList = new List<ADEMessageType>();
            this.ADEReturnList = new List<ADEReturnType>();
        }

        public void Dispose()
        {
            if (this.ADEReturnList != null)
            {
                this.ADEReturnList.Clear();
                this.ADEReturnList = null;
            }

            if (this.ADEMessageList != null)
            {
                this.ADEMessageList.Clear();
                this.ADEMessageList = null;
            }
        }
    }

    public class ADEReturnType
    {
        public string ScreenMessage { get; set; }
        public string EffectTypeCodeDescription { get; set; }
        public string DocLevelCodeDescription { get; set; }
        public string IncidenceCodeDescription { get; set; }
        public string OnsetCodeDescription { get; set; }
        public string SeverityCodeDescription { get; set; }
    }

    public class ADEMessageType
    {
       // public Medispan.DIB.MessageItemType MessageItemType { get; set; }
        public string MessageText { get; set; }
        public string ItemDescription { get; set; }
    }

    #endregion

    #region PRCScreening

    public class PRCScreeningReturnType : IDisposable
    {
        public string DrugDescription { get; set; }
        public Int32 AgeInDaysFrom { get; set; }
        public Int32 AgeInDaysThrough { get; set; }

      //  public Medispan.DIB.PrecautionType PrecautionType { get; set; }

        public string BreastFeedingAAPCode { get; set; }
        public string BreastFeedingAAPCodeDescription { get; set; }

        public string BreastFeedingExcretedCode { get; set; }
        public string BreastFeedingExcretedCodeDescription { get; set; }

        public string BreastFeedingRatingCode { get; set; }
        public string BreastFeedingRatingCodeDescription { get; set; }

        public string BriggsRatingCode { get; set; }
        public string BriggsRatingCodeDescription { get; set; }

        public string FDARiskFactorCode { get; set; }
        public string FDARiskFactorCodeDescription { get; set; }

        public string ManagementLevelCode { get; set; }
        public string ManagementLevelCodeDescription { get; set; }

       // public Medispan.DIB.MedicalCondition RestrictionCondition { get; set; }
        public string RestrictionConditionNamePatient { get; set; }

       // public Medispan.DIB.MedicalCondition ProxyCondition { get; set; }
        public string ProxyConditionNamePatient { get; set; }
        public string ProxyConditionNameProfessional { get; set; }

        public Boolean ProxyConditionPregnancy { get; set; }
        //public Medispan.DIB.MedicalCondition PredictableCondition { get; set; }

        public string PredictableConditionNamePatient { get; set; }
        public string PredictableConditionNameProfessional { get; set; }
        public Boolean PredictableConditionPregnancy { get; set; }

       // public Medispan.DIB.MedicalCondition RelatedCondition { get; set; }
        public string RelatedConditionNamePatient { get; set; }
        public string RelatedConditionNameProfessional { get; set; }
        public Boolean RelatedConditionPregnancy { get; set; }


        public List<PRCMCSpecialConditionReturnType> PRCMCSpecialConditions { get; set; }
        public List<PRCCommentReturnType> PRCComments { get; set; }

        public PRCScreeningReturnType()
        {
            this.PRCMCSpecialConditions = new List<PRCMCSpecialConditionReturnType>();
            this.PRCComments = new List<PRCCommentReturnType>();
        }

        public void Dispose()
        {

            this.RelatedConditionNamePatient = string.Empty;
            this.RelatedConditionNameProfessional = string.Empty;
            this.PredictableConditionNameProfessional = string.Empty;
            this.PredictableConditionNamePatient = string.Empty;
            this.ProxyConditionNameProfessional = string.Empty;
            this.ProxyConditionNamePatient = string.Empty;
            this.RestrictionConditionNamePatient = string.Empty;
            this.ManagementLevelCodeDescription = string.Empty;
            this.ManagementLevelCode = string.Empty;
            this.FDARiskFactorCodeDescription = string.Empty;
            this.FDARiskFactorCode = string.Empty;
            this.BriggsRatingCodeDescription = string.Empty;
            this.BriggsRatingCode = string.Empty;
            this.BreastFeedingRatingCodeDescription = string.Empty;
            this.BreastFeedingRatingCode = string.Empty;
            this.BreastFeedingExcretedCodeDescription = string.Empty;
            this.BreastFeedingExcretedCode = string.Empty;
            this.BreastFeedingAAPCodeDescription = string.Empty;
            this.BreastFeedingAAPCode = string.Empty;
            this.DrugDescription = string.Empty;

            if (this.PRCMCSpecialConditions != null)
            {
                this.PRCMCSpecialConditions.Clear();
                this.PRCMCSpecialConditions = null;
            }

            if (this.PRCComments != null)
            {
                this.PRCComments.Clear();
                this.PRCComments = null;
            }
        }
    }

    public class PRCMCSpecialConditionReturnType
    {
        public string IncidenceCode { get; set; }
        public string NamePatient { get; set; }
        public string NameProf { get; set; }
    }

    public class PRCCommentReturnType
    {
        public string Text { get; set; }
    }

    public class GroupedPRCReturnType : IDisposable
    {
        public List<PRCScreeningReturnType> PRCScreeningReturnType { get; set; }

        public GroupedPRCReturnType()
        {
            this.PRCScreeningReturnType = new List<PRCScreeningReturnType>();
        }

        public void Dispose()
        {
            if (this.PRCScreeningReturnType != null)
            {
                this.PRCScreeningReturnType.Clear();
                this.PRCScreeningReturnType = null;
            }
        }
    }

    #endregion

    #region DIB Query Calls

    public interface IPPID
    {
        string p { get; set; }
    }

    public class PPID_Column : IPPID
    {

        public PPID_Column() { }

        private string _PPID;
        public string p
        {
            get
            { return this._PPID; }
            set
            { this._PPID = value; }
        }
    }

    public class DoseCheckGPI
    {
        public string a { get; set; } //RecType
        public string b { get; set; } //RecSType
        public decimal c { get; set; } //MinDDU
        public decimal d { get; set; } //MaxDDU
        public decimal e { get; set; } //MaxInDose
    }

    public class GetEffectAndCommentCodes
    {
        public string a { get; set; } //ComCode
        public string b { get; set; } //Comment
        public string c { get; set; } //RecType
        public string d { get; set; } //RecStype
        public string e { get; set; } //GPI
        public string f { get; set; } //EffCode
    }
    #endregion

    #region "Drugs Master Inner Join Calls"

    public class DIBGeneralizedReturn
    {
        public int a { get; set; } 
        public string b { get; set; } //Description
    }

    public class DIBGetDrugQtyQualifier
    {
        public int a { get; set; } //dfid
        public int b { get; set; } 
    }

    public class DIBClassifiedDrugs
    {
        public string a { get; set; } //ClassID
        public string b { get; set; } //DescDisplay
    }

    public class DIBClassifiedDrugsExtended
    {
        public string a { get; set; } //ClassID
        public Int16 b { get; set; } //DrugType
        public Int32 c { get; set; } 

        public string d { get; set; } //descdisplay
        public string e { get; set; } //DrugName
        public string f { get; set; } //gpi

        public string g { get; set; } //IsNarcotics
    }

    #endregion
}
