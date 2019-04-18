using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using gloGlobal.DI;
using gloGlobal.DIB;
using gloGlobal.Common;

namespace gloGlobal.DI
{
    public class gloDIServiceHelper : IDisposable
    {
        public string ServiceURL { get; set; }

        public gloDIServiceHelper(string _ServiceURL)
        {
            this.ServiceURL = _ServiceURL;
        }

        public string GetMedicalInstructions(string ndc)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(ndc, URLOption.GetMedicalInstructions);
        }

        public string GetAdverseDrugReactions(DIRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetAdverseDrugReactions);
        }

        public string GetDrugToDrugInteractions(DIRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetDrugToDrugInteractions);
        }

        public string GetDrugToFoodInteractions(DIRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetDrugToFoodInteractions);
        }

        public string GetDrugToDiseaseInteractions(DIRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetDrugToDiseaseInteractions);
        }

        public string GetDrugToAllergyInteractions(DIRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetDrugToAllergyInteractions);
        }

        public string GetDuplicateTherapy(DIRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetDuplicateTherapy);
        }

        public string PerformDoseCheck(DoseCheckRequest req)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.PerformDoseCheck);
        }

        public AlertResponse GetInteractionAlerts(AlertRequest req)
        {
            BaseServiceHelper<AlertResponse, DIB.URLOption> helper = new BaseServiceHelper<AlertResponse, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(req, URLOption.GetInteractionAlerts);
        }

        public void Dispose()
        {

        }
    }
}
