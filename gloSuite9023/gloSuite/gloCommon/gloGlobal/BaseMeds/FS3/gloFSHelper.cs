using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using gloGlobal.Common;

namespace gloGlobal.FS3
{
    public class gloFSHelper
    {
        #region Constructors and Properties
        public string ServiceURL { get; set; }
        public string gloSuiteConnectionString { get; set; }
        public string DIBServiceURL { get; set; }

        public gloFSHelper(string FormularyURL)
        {
            this.ServiceURL = FormularyURL;
        }

        public gloFSHelper(string FormularyURL, string gloSuiteConnection, string gloDIBServiceURL)
        {
            this.ServiceURL = FormularyURL;
            this.gloSuiteConnectionString = gloSuiteConnection;
            this.DIBServiceURL = gloDIBServiceURL;
        }
 
        #endregion

        #region "Formulary Status"

        public FormularyStatus GetFormularyStatus(string SenderID, string FormularyID, FormularyDrug drug)
        {
            FormularyStatus returnedFormularyStatus = null;
            List<FormularyDrug> drugList = null;

            try
            {
                if (string.IsNullOrEmpty(SenderID) || string.IsNullOrEmpty(FormularyID))
                { returnedFormularyStatus = new FormularyStatus(drug.ddid); }
                else
                {
                    drugList = new List<FormularyDrug>();
                    drugList.Add(drug);

                    returnedFormularyStatus = GetFormularyStatusList(SenderID, FormularyID, drugList).FirstOrDefault<FormularyStatus>();

                    if (returnedFormularyStatus == null)
                    { returnedFormularyStatus = new FormularyStatus(drug.ddid); }
                }
            }
            catch (WebException wex)
            {
                throw wex;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetFormularyStatus : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                drugList = null;
            }

            return returnedFormularyStatus;
        }

        public List<FormularyStatus> GetFormularyStatusList(String SenderID, string FormularyID, List<FormularyDrug> drugList)
        {
            List<FormularyStatus> statusList = new List<FormularyStatus>();
            BaseServiceHelper<StatusResponse, FS3.URLOption> helper = null;

            try
            {
                if (!string.IsNullOrEmpty(SenderID) && !string.IsNullOrEmpty(FormularyID) && drugList != null)
                {
                    using (StatusRequest RequestParam = new StatusRequest())
                    {
                        RequestParam.sid = SenderID;
                        RequestParam.fid = FormularyID;

                        RequestParam.ddids.AddRange(drugList.Where(f => f.ddid > 0).Select(s => s.ddid).ToList<Int64>());

                        if (RequestParam.ddids.Any())
                        {
                            helper = new BaseServiceHelper<StatusResponse, FS3.URLOption>(ServiceURL);
                            using (StatusResponse response = helper.GetResponse(RequestParam, URLOption.GetStatusResponse))
                            {
                                foreach (FormularyDrug drug in drugList)
                                {
                                    using (FormularyStatus status = new FormularyStatus(drug.ddid))
                                    {
                                        if (response.ls.Any(p => p.id == drug.ddid))
                                        {
                                            status.fs = response.ls.FirstOrDefault(s => s.id == drug.ddid).fs;
                                        }
                                        else
                                        {
                                            status.fs = GetNonListedFormularyStatus(drug.rxtype, response.nls);
                                        }
                                        statusList.Add(status);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetFormularyStatusList : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                helper = null;
            }
            return statusList;
        }

        private string GetNonListedFormularyStatus(string RxType, NonListedStatus NonListedStatus)
        {
            string nlStatus = "-1";

            try
            {
                if (NonListedStatus != null)
                {
                    switch (RxType.ToLower())
                    {
                        case "generic":
                            nlStatus = NonListedStatus.GEN; ;
                            break;
                        case "supplyotc":
                            nlStatus = NonListedStatus.SUP;
                            break;
                        case "brand":
                            nlStatus = NonListedStatus.BRD;
                            break;
                        case "genericotc":
                            nlStatus = NonListedStatus.OTCG;
                            break;
                    }
                }
                else { nlStatus = "-1"; }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetNonListedFormularyStatus : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            

            return nlStatus;
        }

        #endregion

        #region "Copay"

        public FormularyCopay GetCopay(string SenderID, string CopayID, FormularyStatus status)
        {
            FormularyCopay returned = null;
            List<FormularyStatus> statusList = null;
            try
            {
                if (!string.IsNullOrEmpty(SenderID) && !string.IsNullOrEmpty(CopayID) && status != null)
                {
                    Int32 nFormularyStatus;
                    if (int.TryParse(status.fs, out nFormularyStatus) && nFormularyStatus > 0)
                    {
                        statusList = new List<FormularyStatus>();
                        statusList.Add(status);

                        returned = GetCopayList(SenderID, CopayID, statusList).FirstOrDefault<FormularyCopay>();
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetCopay : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            { statusList = null; }
            
            
            return returned;            
        }

        public List<FormularyCopay> GetCopayList(string SenderID, string CopayID, List<FormularyStatus> statusList)
        {
            List<FormularyCopay> copayList = new List<FormularyCopay>();
            BaseServiceHelper<CopayResponse,FS3.URLOption> helper = null;

            try
            {
                if (!string.IsNullOrEmpty(SenderID) && !string.IsNullOrEmpty(CopayID) && statusList != null)
                {
                    using (CopayRequest RequestParam = new CopayRequest())
                    {
                        RequestParam.sid = SenderID;
                        RequestParam.copid = CopayID;

                        RequestParam.ddids.AddRange(statusList.Select(s => s.id).ToList<Int64>());
                        RequestParam.fsl.AddRange(statusList.Select(s => s.fs).Distinct().ToList<string>());

                        helper = new BaseServiceHelper<CopayResponse, FS3.URLOption>(ServiceURL);
                        using (CopayResponse response = helper.GetResponse(RequestParam, URLOption.GetCopayResponse))
                        {
                            foreach (FormularyStatus drug in statusList)
                            {
                                using (FormularyCopay copayFactor = new FormularyCopay())
                                {
                                    copayFactor.id = drug.id;
                                    if (response.ds.Any(p => p.ddid == drug.id))
                                    {
                                        var s = from c in response.ds
                                                where c.ddid == drug.id
                                                select c;


                                        copayFactor.cop.AddRange(s.ToList<CopayFactor>());
                                        copayList.Add(copayFactor);
                                    }
                                    else if (response.sl.Any(p => p.fs == drug.fs))
                                    {
                                        var s = from c in response.sl
                                                where c.fs == drug.fs
                                                select c;

                                        copayFactor.cop.AddRange(s.ToList<CopayFactor>());
                                        copayList.Add(copayFactor);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetCopayList : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            { helper = null; }
            
            return copayList;
        }

        #endregion

        #region "Payer Alternatives"

        public FormularyAlternatives GetAlternativeList(string SenderID, string FormularyID, string CopayID, string AlternativeID, Int64 mpid, string DrugRxType, string FormularyStatus)
        {
            string type = DrugRxType.ToUpper();
            FormularyAlternatives altList = new FormularyAlternatives();
            List<gloGlobal.DIB.AlternativeDrugDetails> alternativeList = null;
            BaseServiceHelper<AlternativeResponse, FS3.URLOption> helper = null;
            List<FormularyDrug> drugList = null;
            List<FormularyStatus> StatusList = null;

            try
            {
                if (!string.IsNullOrEmpty(SenderID))
                {
                    int iStatus = -1;
                    if (int.TryParse(FormularyStatus, out iStatus))
                    {
                        if (iStatus >= 0 && iStatus <= 2)
                        {
                            alternativeList = new List<gloGlobal.DIB.AlternativeDrugDetails>();

                            using (AlternativeRequest RequestParam = new AlternativeRequest())
                            {
                                RequestParam.sid = SenderID;
                                RequestParam.fid = FormularyID;
                                RequestParam.aid = AlternativeID;
                                RequestParam.ddid = mpid;

                                helper = new BaseServiceHelper<AlternativeResponse, FS3.URLOption>(ServiceURL);
                                using (AlternativeResponse response = helper.GetResponse(RequestParam, URLOption.GetAlternativeResponse))
                                {
                                    using (gloSuiteDBHelper dbHelper = new gloSuiteDBHelper(gloSuiteConnectionString))
                                    {
                                        if (response.alt != null && response.alt.Count() > 0)
                                        {
                                            altList.ListType = AltListType.Payer;
                                            alternativeList = dbHelper.GetAllStrengthAlternatives(response.alt);
                                        }
                                        else
                                        {
                                            if (!response.IsFilePresent && !type.Contains("SUPPY") && !type.Contains("OTC"))
                                            {
                                                altList.ListType = AltListType.DrugSpecific;
                                                //alternativeList = dbHelper.GetAllStrengthAlternatives(DDID);
                                                using (gloGlobal.DIB.gloGSHelper oDIBGSHelper = new gloGlobal.DIB.gloGSHelper(DIBServiceURL))
                                                {
                                                    alternativeList = oDIBGSHelper.GetAllStrengthAlternatives(mpid);
                                                }
                                            }
                                        }
                                    }
                                }

                                if (alternativeList != null && alternativeList.Count() > 0)
                                {
                                    drugList = new List<FormularyDrug>();
                                    foreach (gloGlobal.DIB.AlternativeDrugDetails altDrug in alternativeList)
                                    {
                                        drugList.Add(new FormularyDrug(altDrug.id, altDrug.RxType));
                                    }

                                    StatusList = this.GetFormularyStatusList(SenderID, FormularyID, drugList);
                                    if (StatusList != null && StatusList.Count() > 0)
                                    {
                                        List<FormularyCopay> CopayList = this.GetCopayList(SenderID, CopayID, StatusList);

                                        foreach (FormularyStatus status in StatusList)
                                        {
                                            int _iStatus = -1;
                                            if (int.TryParse(status.fs, out _iStatus))
                                            {
                                                if (_iStatus > iStatus)
                                                {
                                                    using (gloGlobal.DIB.AlternativeDrugDetails alternativeDrug = alternativeList.FirstOrDefault(p => p.id == status.id))
                                                    {
                                                        if (alternativeDrug != null)
                                                        {
                                                            alternativeDrug.fs = status.fs;
                                                            using (StatusConverter formularyStatus = new StatusConverter(status.fs))
                                                            {
                                                                alternativeDrug.FormularyStatus = formularyStatus.DisplayText;
                                                            }

                                                            using (FormularyCopay formularyCopay = CopayList.FirstOrDefault(p => p.id == status.id))
                                                            {
                                                                if (formularyCopay != null)
                                                                {
                                                                    using (CopayConverter abrcopay = new CopayConverter(formularyCopay.cop))
                                                                    {
                                                                        alternativeDrug.AbbrivatedCopay = abrcopay.DisplayText;
                                                                    }
                                                                }
                                                            }

                                                            altList.AlternativeList.Add(alternativeDrug);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                altList.AlternativeList = altList.AlternativeList
                                                                    .OrderByDescending(p => Convert.ToInt32(p.pl))
                                                                    .ThenByDescending(p => Convert.ToInt32(p.fs))
                                                                    .ThenBy(p => Convert.ToString(p.DrugName))
                                                                    .ToList();
                            }
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetAlternativeList : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            {
                alternativeList = null;
                helper = null;
                drugList = null;
                StatusList = null;
            }
            return altList;
        }

        #endregion

        #region Coverage

        public CoverageFactor GetCoverage(string SenderID, string CoverageID, Int64 mpid)
        {
            CoverageFactor returnedCoverage = null;
            BaseServiceHelper<CoverageResponse, FS3.URLOption> helper = null;

            try
            {
                if (!string.IsNullOrEmpty(SenderID) && !string.IsNullOrEmpty(CoverageID) && mpid != 0)
                {
                    using (CoverageRequest RequestParam = new CoverageRequest())
                    {
                        RequestParam.sid = SenderID;
                        RequestParam.covid = CoverageID;
                        RequestParam.ddid = mpid;

                        helper = new BaseServiceHelper<CoverageResponse, FS3.URLOption>(ServiceURL);
                        using (CoverageResponse response = helper.GetResponse(RequestParam, URLOption.GetCoverageResponse))
                        {
                            if (response != null)
                            {
                                returnedCoverage = new CoverageFactor();
                                returnedCoverage = response.cov;

                                //if (returnedCoverage != null)
                                //{
                                //    if (returnedCoverage.sm.Any())
                                //    {
                                //        using (gloSuiteDBHelper dbHelper = new gloSuiteDBHelper(gloSuiteConnectionString))
                                //        {
                                //            foreach (StepMeds sm in returnedCoverage.sm)
                                //            {
                                //              ////  sm.smid = dbHelper.GetStepMedication(sm.smid);
                                //            }
                                //        }
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
            catch (WebException wex)
            {
                throw wex;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.Formulary30, gloAuditTrail.ActivityType.None, "FS3_GetCoverage : " + (ex.InnerException ?? ex).ToString(), gloAuditTrail.ActivityOutCome.Failure);
            }
            finally
            { helper = null; }

            return returnedCoverage;
        } 

        #endregion
              
    }
}
