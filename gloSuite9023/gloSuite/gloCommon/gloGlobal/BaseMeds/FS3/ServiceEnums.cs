using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gloGlobal.FS3
{
    public enum AltListType
    {
        None,
        Payer,
        DrugSpecific
    }

    public enum URLOption
    {
        GetStatusResponse,
        GetCopayResponse,
        GetAlternativeResponse,
        GetCoverageResponse
        //GetDrugFormularyInfo,
        //GetDrugListFormularyInfo,
        //GetPayerAlternativeInfo,
        //GetDrugListFormularyInfoRevised
    }

    public enum ResourceLinkEnum
    {
        AgeLimits = 1,
        Coapy = 2,
        CoverageExclusion = 3,
        Formulary = 4,
        GeneralInfo = 5,
        GenderLimits = 6,
        PriorAuthorization = 7,
        QuantityLimits = 8,
        StepTherapy = 9
    }

    public enum FormularyQueriedFrom
    {
        None,
        PBMRefresh,
        ShowAlternatives
    }
}
