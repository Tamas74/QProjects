using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using gloGlobal.DI;
using gloGlobal.Common;

namespace gloGlobal.DIB
{
    public class gloGSHelper : IDisposable
    {
        public string ServiceURL { get; set; }
        public gloGSHelper(string _ServiceURL)
        {
          this.ServiceURL = _ServiceURL; 
        }

        #region "DIB Functions"


        public string TestService()
        {
            string res = string.Empty;

            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            try
            {
                res = helper.GetResponse(null, URLOption.TestService);
                return res; 
            }
            catch (Exception Ex)
            { 
                res = Ex.ToString();
                return res; 
            }
        }

        public Int32 GetMarketedProductId(String sNdcCode, Int32 mpid=0)
        {
            using (NdcMpidReq requestObject = new NdcMpidReq())
            {
                requestObject.ndc = sNdcCode;
                requestObject.mpid = mpid;
                BaseServiceHelper<Int32, DIB.URLOption> helper = new BaseServiceHelper<Int32, DIB.URLOption>(ServiceURL);

                return helper.GetResponse(requestObject, URLOption.GetMarketedProductId);
            }
        }

        public List<string> GetDrugFormList()
        {
            BaseServiceHelper<List<string>, DIB.URLOption> helper = new BaseServiceHelper<List<string>, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(null, URLOption.GetDrugForms);
          
        }

        public bool IsRouteExist(string routeDescription)
        {
            BaseServiceHelper<bool, DIB.URLOption> helper = new BaseServiceHelper<bool, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(routeDescription, URLOption.IsRouteExist);
           
        }

        public  string GetNCPDPBillingUnitID(String ndc)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(ndc, URLOption.GetNCPDPBillingUnitID);
          
        }
        
        public bool IsObsolute(string drugNDC)
        {
            BaseServiceHelper<bool, DIB.URLOption> helper = new BaseServiceHelper<bool, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(drugNDC, URLOption.IsObsolute);
          
        }

        public List<Int32> GetClassifiedDrugs(int level)
        {
            using (ClassifiedDrugRequest RequestParam = new ClassifiedDrugRequest())
            {
                RequestParam.Code = level;

                BaseServiceHelper<List<Int32>, DIB.URLOption> helper = new BaseServiceHelper<List<Int32>, DIB.URLOption>(ServiceURL);

                return helper.GetResponse(RequestParam, URLOption.GetClassifiedDrugs);

            }
        }
       
        public string GetDrugPotencyCode(string NDCCode)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(NDCCode, URLOption.GetDrugPotencyCode);
           
        }
                
        public Int16 IsNarcotic(String sNdccode)
        {
            BaseServiceHelper<Int16, DIB.URLOption> helper = new BaseServiceHelper<Int16, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(sNdccode, URLOption.IsNarcotic);
           
        }
               
        public Int16 IsNarcoticFromDrugName(String DrugName)
        {
            BaseServiceHelper<Int16, DIB.URLOption> helper = new BaseServiceHelper<Int16, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(DrugName, URLOption.IsNarcoticFromDrugName);
         
        }
           
        public DrugDetails GetDrugsInfoGSDDforRx_Saving(string sNdC11)
        {

            BaseServiceHelper<DrugDetails, DIB.URLOption> helper = new BaseServiceHelper<DrugDetails, DIB.URLOption>(ServiceURL);

                return helper.GetResponse(sNdC11, URLOption.GetDrugsInfoFromNDC);       
         }
        
        public DrugDetails GetDrugsInfoGSDDforRx_SavingByDrugName(string sDrugName)
        {
            BaseServiceHelper<DrugDetails, DIB.URLOption> helper = new BaseServiceHelper<DrugDetails, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(sDrugName, URLOption.GetDrugsInfoGSDDforRx_SavingByDrugName);
            
        }
       
        public DrugDetails GetDrugsInfoGSDDforRx_SavingByRxNorm(string sRxNorm)
        {
            BaseServiceHelper<DrugDetails, DIB.URLOption> helper = new BaseServiceHelper<DrugDetails, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(sRxNorm, URLOption.GetDrugsInfoGSDDforRx_SavingByRxNorm);

        }
     
        public List<string> GetNDCCodeFromGSDDDB(string productcode)
        {
            BaseServiceHelper<List<string>, DIB.URLOption> helper = new BaseServiceHelper<List<string>, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(productcode, URLOption.GetNDCCodes);
           
        }
       
        public Boolean IsDrugExist(String sNdcCode)
        {
            BaseServiceHelper<Boolean, DIB.URLOption> helper = new BaseServiceHelper<Boolean, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(sNdcCode, URLOption.IsDrugExist);
        
        }
        
        public List<string> GetnCheckNDCInGSDD(List<string> DrgNDCCode)
        {

            BaseServiceHelper<List<string>, DIB.URLOption> helper = new BaseServiceHelper<List<string>, DIB.URLOption>(ServiceURL);

            return helper.GetResponse(DrgNDCCode, URLOption.GetnCheckNDCInGSDD);


        }

        public DrugInfo GetNdccodebyRxnorm(string Code, bool IsRxnorm)
        {
            using (NDCandDrugNameRequest RequestParam = new NDCandDrugNameRequest())
            {
                RequestParam.Code = Code;
                RequestParam.IsRxnorm = IsRxnorm;
                BaseServiceHelper<DrugInfo, DIB.URLOption> helper = new BaseServiceHelper<DrugInfo, DIB.URLOption>(ServiceURL);
                
                return helper.GetResponse(RequestParam, URLOption.GetNdccodebyRxnorm);
                
            }
        }
       
        public RxnormFlagInfo GetRxNormCode(string Code)
        {
            using (RxnormFlagRequest RequestParam = new RxnormFlagRequest())
            {
                RequestParam.Code = Code;
                RequestParam.flag = 1;
                // Flag should be 1 as ser
                BaseServiceHelper<RxnormFlagInfo, DIB.URLOption> helper = new BaseServiceHelper<RxnormFlagInfo, DIB.URLOption>(ServiceURL);

                return helper.GetResponse(RequestParam, URLOption.GetRxNormInfo);

            }
           
        }        

        public DrugInfo GETQRDANDC(string Code, bool resultfound, string valueset)
        {
            using (QRDANDCRequest RequestParam = new QRDANDCRequest())
            {
                RequestParam.Code = Code;
                RequestParam.Resultfound = resultfound;
                RequestParam.Valueset = valueset;
                BaseServiceHelper<DrugInfo, DIB.URLOption> helper = new BaseServiceHelper<DrugInfo, DIB.URLOption>(ServiceURL);

                return helper.GetResponse(RequestParam, URLOption.GETQRDANDC);
            }
        }
      
        public List<AlternativeDrugDetails> GetAllStrengthAlternatives(Int64 mpid)
        {
            BaseServiceHelper<List<AlternativeDrugDetails>, DIB.URLOption> helper = new BaseServiceHelper<List<AlternativeDrugDetails>, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(mpid, URLOption.GetAllStrengthAlternatives);
        }

        public ResultSetRxnorm GetRxnormGenericName(List<string> DrgNDCCode)
        {
            BaseServiceHelper<ResultSetRxnorm, DIB.URLOption> helper = new BaseServiceHelper<ResultSetRxnorm, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(DrgNDCCode, URLOption.GetRxnormGenericName);
        }

        public CQMResult CQMServiceCallGSDD(string MUMesure)
        {
            BaseServiceHelper<CQMResult, DIB.URLOption> helper = new BaseServiceHelper<CQMResult, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(MUMesure, URLOption.GetNQFCodes);     
        }
        public List<QRDAResult> GetQRDA1CodesList(string MUMesure)
        {
            BaseServiceHelper<List<QRDAResult>, DIB.URLOption> helper = new BaseServiceHelper<List<QRDAResult>, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(MUMesure, URLOption.GetQRDA1Codes);
        }
        public List<PIResp> GetProductIngredients(List<string> DrgNDCCode)
        {
            BaseServiceHelper<List<PIResp>, DIB.URLOption> helper = new BaseServiceHelper<List<PIResp>, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(DrgNDCCode, URLOption.GetProductIngredients);
        }

        public string GetDrugForm(string NDCCode)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(NDCCode, URLOption.GetDrugForm);
        }

        public List<string> GetAlternativeNDCS(Int32 mpid, string ndc)
        {
            using (NdcMpidReq requestObject = new NdcMpidReq())
            {
                requestObject.ndc = ndc;
                requestObject.mpid = mpid;
                BaseServiceHelper<List<string>, DIB.URLOption> helper = new BaseServiceHelper<List<string>, DIB.URLOption>(ServiceURL);

                return helper.GetResponse(requestObject, URLOption.GetAlternativeNDCS);
            }
        }

        public string GetRxNormDrugAllergyName(string RxNormCode)
        {
            BaseServiceHelper<string, DIB.URLOption> helper = new BaseServiceHelper<string, DIB.URLOption>(ServiceURL);
            return helper.GetResponse(RxNormCode, URLOption.GetRxNormDrugAllergyName);
        }


        #endregion

        //public string GetDIAlert(DIAlertRequest req)
        //{
        //    BaseServiceHelper<string> helper = new BaseServiceHelper<string>(ServiceURL);
        //    return helper.GetResponse(req, URLOption.GetDIAlert);
        //}
        
        public void Dispose()
        {
            
        }
    }

  
}
