using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloGlobal.DIB
{
    public enum ScreeningType
    {
        ADE=1,
        PAR=2,
        DI=3,
        DT=4,
        DFA=5,
        MI=7,
        PRC=8,
        Alert=6,
        None=7
    }
    public enum DISeverityType
    {
        None = 0,
        Severe = 1,
        Major = 2,
        Moderate = 3,
        Minor = 4
    }
    public enum CodeSystem
    {
        HL7,
        FDA
    }

    public enum URLOption
    {
        TestService,
        GetNCPDPBillingUnitID,
        GetDrugForms,
        GETQRDANDC,
        GetCoverageResponse,
        GetDrugsInfoFromNDC,
        IsObsolute,
        GetDrugPotencyCode,
        IsNarcotic,
        GetNDCCodes,
        IsRouteExist,
        IsNarcoticFromDrugName,
        GetMarketedProductId,
        GetRxNormCode,
        GetNdccodebyRxnorm,
        IsDrugExist,
        GetnCheckNDCInGSDD,
        GetDrugsInfoGSDDforRx_SavingByDrugName,
        GetDrugsInfoGSDDforRx_SavingByRxNorm,
        GetAllStrengthAlternatives,
        GetRxnormGenericName,
        GetNQFCodes,
        GetProductIngredients,
        GetClassifiedDrugs,
        GetDrugForm,
        GetDIAlert,
        GetMedicalInstructions,
        GetAdverseDrugReactions,
        GetDrugToDrugInteractions,
        GetDrugToFoodInteractions,
        GetDrugToDiseaseInteractions,
        GetDrugToAllergyInteractions,
        GetDuplicateTherapy,
        PerformDoseCheck,
        GetInteractionAlerts,
        GetAlternativeNDCS,
         GetQRDA1Codes,
         GetRxNormInfo,
         GetRxNormDrugAllergyName
    }
}
