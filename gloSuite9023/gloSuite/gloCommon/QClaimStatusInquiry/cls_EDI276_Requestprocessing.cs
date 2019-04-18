using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Edidev.FrameworkEDI;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;
using TriArqEDIRealTimeClaimStatus.EDI_276;
using System.IO;

namespace TriArqEDIRealTimeClaimStatus
{
    public class cls_EDI276_Requestprocessing
    {
        private string _MessageBoxCaption;
        private string _DataBaseConnectionString;
        private string _EDIDataBaseConnectionString;

        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;

        ediDocument oEdiDoc;
        ediSchemas oSchemas;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionSet = null;
        ediDataSegment oSegment = null;

        string ClaimNumber = string.Empty;
        string AUSID = string.Empty;

        string seffilepath_276_005010X214_SemRef = string.Empty;
        string sPath = AppDomain.CurrentDomain.BaseDirectory;
        string x276RequestFilePath = string.Empty;
        string TempFolderpath = gloSettings.FolderSettings.AppTempFolderPath + "EDIFiles";

        Cls_276X212_ISA o276_ISA;
        Cls_276X212_GS o276_GS;
        Cls_276X212_ST o276_ST;
        Cls_276X212_BHT o276_BHT;
        Cls_276X212_HL o276_HL;
        Cls_276X212_NM o276_NM;
        Cls_276X212_DMG o276_DMG;
        Cls_276X212_TRN o276_TRN;
        Cls_276X212_REF o276_REF;
        Cls_276X212_AMT o276_AMT;
        Cls_276X212_DTP o276_DTP;
        Cls_276X212_SVC o276_SVC;

        long ERAFileID = 0;
        long ISAID = 0;

        long CurrentSTID = 0;
        long CurrentHLId = 0;
        long CurrentTRNId = 0;
        long CurrentSVCId = 0;

        long SourceHLId = 0;
        long ReceiverHLId = 0;
        long ProviderHLId = 0;
        long SubscriberHLId = 0;
        long DependentHLId = 0;

        int UniqueIdCounter = 0;
        int SegmentCounter = 0;

        DataTable dtUniqueIDs = null;
        DataTable dtISA, dtGS, dtST, dtBHT, dtHL, dtNM, dtDMG, dtAMT, dtTRN, dtSVC,  dtREF, dtDTP = null;

        private enum Segment
        {
            ISA = 1,
            GS = 2,
            ST = 3,
            BHT = 4,
            HL = 5,
            NM = 6,
            DMG = 7,
            AMT = 8,
            TRN = 9,
            SVC = 10,
            REF = 11,
            DTP = 12
        }

        private enum RequestType
        {
            RealTime = 1,
            Batch = 2
        }

        DataTable dtClearingHouse = null;
        DataTable dtSubmitter = null;
        DataTable dtEDISetting = null;
        DataTable dtAlphaSetting = null;
        DataTable dtClaimData = null;
        DataTable dtClaimLines = null;
        DataTable dtClaimPatient = null;
        DataTable dtClaimClinic = null;
        DataTable dtPatientInsurance = null;
        DataTable dtFacility = null;
        DataTable dtBillingProvider = null;
        DataTable dtRefferingProvider = null;
        DataTable dtRenderingProvider = null;
        DataTable dtMidLevelID = null;
        DataTable dtBillingProviderTaxonomy = null;
        DataTable dtMasterSetting = null;


        public cls_EDI276_Requestprocessing(string _ClaimNumber, string ConnString = "")
        {
            _DataBaseConnectionString = ConnString;
            if (_DataBaseConnectionString == "")
            {
                _DataBaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[1].ToString();
            }
            ClaimNumber = _ClaimNumber;
            //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.RequestProcessing, "EDI 276 File generation request processing starts.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
        }

        #region "Generate EDI Request 276 File "

        public TRIARQClaim FillTriarqClaimData()
        {
            //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.RequestProcessing, "Started filling Claim Model object.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
            DataSet dsClaimData = new DataSet();
            DataSet dsMasterEDIData = new DataSet();
            DataSet dsClearingHouse = new DataSet();
            try
            {
                TRIARQClaim oTriarqClaim = new TRIARQClaim();

                dsClearingHouse = GetClearingHouseData();
                dtClearingHouse = dsClearingHouse.Tables["ClearingHouse"];
                dtSubmitter = dsClearingHouse.Tables["Submitter"];
                dtEDISetting = dsClearingHouse.Tables["EDISetting"];
                dtAlphaSetting = dsClearingHouse.Tables["AlphaSetting"];

                #region Request Header Data

                if (dsClearingHouse.Tables["ClearingHouse"] != null)
                {
                    if (dsClearingHouse.Tables["ClearingHouse"].Rows.Count > 0)
                    {
                        oTriarqClaim.RequestHeader = new TRIARQ276RequestHeader();
                        oTriarqClaim.RequestHeader.ReceiverID = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sReceiverID"]);
                        oTriarqClaim.RequestHeader.SubmitterID = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sSubmitterID"]);
                        oTriarqClaim.RequestHeader.SenderCode = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sSenderCode"]);
                        oTriarqClaim.RequestHeader.VenderIDCode = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sVenderIDCode"]);
                        oTriarqClaim.RequestHeader.ClearingHouseCode = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sClearingHouseCode"]);

                        if (Convert.ToInt32(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["nTypeOfData"]) == 1)
                        {
                            oTriarqClaim.RequestHeader.TypeOfData = "T";
                        }
                        else if (Convert.ToInt32(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["nTypeOfData"]) == 2)
                        {
                            oTriarqClaim.RequestHeader.TypeOfData = "P";
                        }

                        oTriarqClaim.RequestHeader.SenderQualifier = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sSenderIDQualifier"]);
                        oTriarqClaim.RequestHeader.ReceiverQualifier = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sReceiverIDQualifier"]);
                    }
                }

                #endregion

                dsClaimData = GetClaimData();
                dtClaimData = dsClaimData.Tables["ClaimData"];
                dtClaimLines = dsClaimData.Tables["ClaimLines"];
                dtClaimPatient = dsClaimData.Tables["ClaimPatient"];
                dtClaimClinic = dsClaimData.Tables["ClaimClinic"];

                #region "Claim Master Data "
                oTriarqClaim.MasterTransactionID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionMasterID"]);
                oTriarqClaim.TransactionID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionID"]);
                oTriarqClaim.PatientID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nPatientID"]);
                oTriarqClaim.ClaimNo = Convert.ToString(dsClaimData.Tables["ClaimData"].Rows[0]["nClaimNo"]);
                oTriarqClaim.SubClaimNo = Convert.ToString(dsClaimData.Tables["ClaimData"].Rows[0]["nSubClaimNo"]);
                oTriarqClaim.TransactionDate = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionDate"]));
                oTriarqClaim.ProviderID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionProviderID"]);
                oTriarqClaim.IsSameAsBillingProvider = Convert.ToBoolean(dsClaimData.Tables["ClaimData"].Rows[0]["bSameAsBillingProvider"]);
                oTriarqClaim.ReferalProviderID_New = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nReferralProviderID"]);
                oTriarqClaim.ResponsibilityNo = Convert.ToInt16(dsClaimData.Tables["ClaimData"].Rows[0]["nResponsibilityNo"]);
                oTriarqClaim.ClinicID = Convert.ToInt16(dsClaimData.Tables["ClaimData"].Rows[0]["ClinicID"]);
                oTriarqClaim.ContactID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nContactID"]);
                oTriarqClaim.FacilityCode = Convert.ToString(dsClaimData.Tables["ClaimData"].Rows[0]["sFacilityCode"]);
                oTriarqClaim.FromDOS = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionDate"]);
                oTriarqClaim.ToDOS = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionDate"]);
                oTriarqClaim.TotalClaimAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimData"].Rows[0]["ClaimTotalCharges"]);
                #endregion

                #region "Claim Line Data"

                oTriarqClaim.ClaimServiceLines = new List<ServiceLine>();

                if (dsClaimData.Tables["ClaimLines"] != null)
                {
                    if (dsClaimData.Tables["ClaimLines"].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsClaimData.Tables["ClaimLines"].Rows.Count; i++)
                        {
                            ServiceLine oServiceline = new ServiceLine();
                            oServiceline.LineID = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nTransactionDetailID"]);
                            oServiceline.LineNo = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nTransactionLineNo"]);

                            oServiceline.FromDOS = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nFromDate"]));
                            if (dsClaimData.Tables["ClaimLines"].Rows[i]["nToDate"] != null && Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nToDate"]) > 0)
                            {
                                oServiceline.ToDOS = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nToDate"]));
                            }
                            else
                            {
                                oServiceline.DateServiceTillIsNull = true;
                                oServiceline.ToDOS = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nFromDate"]));
                            }
                            oServiceline.POSCode = dsClaimData.Tables["ClaimLines"].Rows[i]["sPOSCode"].ToString();
                            oServiceline.POSDesc = "";
                            oServiceline.TOSCode = "";
                            oServiceline.TOSDesc = "";
                            oServiceline.CPT = dsClaimData.Tables["ClaimLines"].Rows[i]["sCPTCode"].ToString();
                            oServiceline.CPTDescription = dsClaimData.Tables["ClaimLines"].Rows[i]["sCPTDescription"].ToString();
                            oServiceline.CrosswalkCPTCode = dsClaimData.Tables["ClaimLines"].Rows[i]["sCrossWalkCPTCode"].ToString();
                            oServiceline.Modifier1 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod1Code"].ToString();
                            oServiceline.Modifier2 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod2Code"].ToString();
                            oServiceline.Modifier3 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod3Code"].ToString();
                            oServiceline.Modifier4 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod4Code"].ToString();
                            oServiceline.ChargeAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dCharges"]);
                            oServiceline.BilledAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dBilliedAmount"]);
                            oServiceline.Quantity = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dUnit"]);
                            oServiceline.TotalChargeAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dTotal"]);
                            oServiceline.RenderingProviderId = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nProvider"]);
                            oServiceline.RenderingProvider = new Provider();
                            oServiceline.RenderingProvider.FirstName = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sFirstName"]);
                            oServiceline.RenderingProvider.MiddleName = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sMiddleName"]);
                            oServiceline.RenderingProvider.LastName = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sLastname"]);
                            oServiceline.RenderingProvider.ID = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nProvider"]);
                            oServiceline.RenderingProvider.NPI = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sNPI"]);
                            oServiceline.RenderingProvider.Suffix = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sSuffix"]);
                            oServiceline.RenderingProvider.TIN = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sTaxID"]);
                            oServiceline.RenderingProvider.Taxonomy = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sTaxonomy"]);
                            oServiceline.RenderingProvider.Address = new Address();
                            oServiceline.RenderingProvider.Address.AddressLine1 = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessAddressLine1"]);
                            oServiceline.RenderingProvider.Address.AddressLine2 = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessAddressLine2"]);
                            oServiceline.RenderingProvider.Address.City = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessCity"]);
                            oServiceline.RenderingProvider.Address.State = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessState"]);
                            oServiceline.RenderingProvider.Address.Zip = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessZip"]);

                            oTriarqClaim.ClaimServiceLines.Add(oServiceline);
                            oServiceline.Dispose();
                            oServiceline = null;
                        }
                    }
                }
                #endregion

                #region "Claim Patient"
                if (dsClaimData.Tables["ClaimPatient"] != null)
                {
                    if (dsClaimData.Tables["ClaimPatient"].Rows.Count > 0)
                    {
                        oTriarqClaim.Patient = new Patient();
                        oTriarqClaim.Patient.PatientID = oTriarqClaim.PatientID;
                        oTriarqClaim.Patient.Code = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientCode"]);
                        oTriarqClaim.Patient.FirstName = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientFirstName"]);
                        oTriarqClaim.Patient.MiddleName = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientMiddleName"]);
                        oTriarqClaim.Patient.LastName = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientLastName"]);
                        oTriarqClaim.Patient.Suffix = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientSuffix"]);
                        oTriarqClaim.Patient.Gender = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientGender"]);
                        oTriarqClaim.Patient.DOB = Convert.ToDateTime(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientDOB"]);
                        oTriarqClaim.Patient.PharmacyNumber = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["Pharmacy"]);
                        oTriarqClaim.Patient.Address = new Address();
                        oTriarqClaim.Patient.Address.AddressLine1 = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientAddress1"]);
                        oTriarqClaim.Patient.Address.AddressLine2 = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientAddress2"]);
                        oTriarqClaim.Patient.Address.City = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientCity"]); ;
                        oTriarqClaim.Patient.Address.State = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientState"]); ;
                        oTriarqClaim.Patient.Address.Country = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientCountry"]); ;
                        oTriarqClaim.Patient.Address.Zip = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientZip"]); ;
                        oTriarqClaim.Patient.Address.AreaCode = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["AreaCode"]); ;
                    }
                }
                #endregion

                #region Claim Clinic

                if (dsClaimData.Tables["ClaimClinic"] != null)
                {
                    if (dsClaimData.Tables["ClaimClinic"].Rows.Count > 0)
                    {
                        oTriarqClaim.ClaimClinic = new Clinic();
                        oTriarqClaim.ClaimClinic.ID = Convert.ToInt64(dsClaimData.Tables["ClaimClinic"].Rows[0]["nClinicID"]);
                        oTriarqClaim.ClaimClinic.AUSID = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["AUSID"]);
                        oTriarqClaim.ClaimClinic.Name = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sClinicName"]);
                        oTriarqClaim.ClaimClinic.NPI = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sClinicNPI"]); ;
                        oTriarqClaim.ClaimClinic.TIN = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sTaxId"]);
                        oTriarqClaim.ClaimClinic.Taxonomy = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sTaxonomyCode"]);
                        oTriarqClaim.ClaimClinic.Address = new Address();
                        oTriarqClaim.ClaimClinic.Address.AddressLine1 = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sAddress1"]);
                        oTriarqClaim.ClaimClinic.Address.AddressLine2 = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sStreet"]);
                        oTriarqClaim.ClaimClinic.Address.City = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sCity"]);
                        oTriarqClaim.ClaimClinic.Address.State = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sState"]);
                        oTriarqClaim.ClaimClinic.Address.Zip = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sZip"]);
                        oTriarqClaim.ClaimClinic.Address.Country = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sCountry"]);
                        oTriarqClaim.ClaimClinic.Address.AreaCode = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sAreaCode"]);
                    }
                }
                #endregion


                if (oTriarqClaim != null)
                {
                    if (oTriarqClaim.ClaimServiceLines.Count > 0)
                    {
                        dsMasterEDIData = GetMasterEDIData(oTriarqClaim);

                        dtMidLevelID = dsMasterEDIData.Tables["MidLevelID"];  //Rendering ProviderId,Billing Provider Id
                        dtPatientInsurance = dsMasterEDIData.Tables["PatientInsurance"];
                        dtFacility = dsMasterEDIData.Tables["Facility"];
                        dtBillingProvider = dsMasterEDIData.Tables["BillingProvider"];
                        dtRefferingProvider = dsMasterEDIData.Tables["RefferingProvider"];
                        dtRenderingProvider = dsMasterEDIData.Tables["RenderingProvider"];
                        dtBillingProviderTaxonomy = dsMasterEDIData.Tables["BillingProviderTaxonomy"];
                        dtMasterSetting = dsMasterEDIData.Tables["MasterSetting"];

                        #region "Responsible Party"
                        if (dsMasterEDIData.Tables["PatientInsurance"] != null)
                        {
                            if (dsMasterEDIData.Tables["PatientInsurance"].Rows.Count > 0)
                            {
                                oTriarqClaim.ResponsibleParty = new Insurance();
                                oTriarqClaim.ResponsibleParty.ContactId = Convert.ToInt64(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["nContactID"]);
                                oTriarqClaim.ResponsibleParty.PayerID = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["PayerID"]);
                                oTriarqClaim.ResponsibleParty.InsuranceID = Convert.ToInt64(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceID"]);
                                oTriarqClaim.ResponsibleParty.InsuranceName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceName"]);
                                oTriarqClaim.ResponsibleParty.PolicyNumber = "";
                                oTriarqClaim.ResponsibleParty.GroupNumber = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["sGroup"]); ;
                                oTriarqClaim.ResponsibleParty.InsuranceFlag = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceFlag"]); ;
                                oTriarqClaim.ResponsibleParty.InsuranceTypeCode = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceTypeCode"]);
                                oTriarqClaim.ResponsibleParty.InsuranceTypeDesc = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeDescriptionDefault"]); ;
                                oTriarqClaim.ResponsibleParty.TypeOfBilling = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["BillingType"]);
                                oTriarqClaim.ResponsibleParty.BillingTypeId = Convert.ToInt64(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["BillingTypeId"]);
                                oTriarqClaim.ResponsibleParty.InsTypeCodeDefault = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeCodeDefault"]); ;
                                oTriarqClaim.ResponsibleParty.InsTypeDescriptionDefault = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceTypeDesc"]); ;
                                oTriarqClaim.ResponsibleParty.InsTypeCodeMedicare = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeCodeMedicare"]);
                                oTriarqClaim.ResponsibleParty.InsTypeDescriptionMedicare = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeDescriptionMedicare"]);

                                oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode = Convert.ToInt32(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["RelationshipCode"]);
                                oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipDesc = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["Relationship"]);
                                oTriarqClaim.ResponsibleParty.SubscriberDOB = Convert.ToDateTime(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["dtDOB"]);
                                oTriarqClaim.ResponsibleParty.SubscriberFName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubFName"]);
                                oTriarqClaim.ResponsibleParty.SubscriberMName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubMName"]);
                                oTriarqClaim.ResponsibleParty.SubscriberLName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubLName"]);
                                oTriarqClaim.ResponsibleParty.SubscriberSuffix = "";
                                oTriarqClaim.ResponsibleParty.SubscriberGender = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberGender"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress = new Address();
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.AddressLine1 = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberAddr1"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.AddressLine2 = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberAddr2"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.City = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberCity"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.State = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberState"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.Country = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberCountryCode"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.Zip = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberZip"]);
                                oTriarqClaim.ResponsibleParty.SubscriberAddress.AreaCode = "";

                            }
                        }
                        #endregion

                        #region "Billing Provider"
                        if (dsMasterEDIData.Tables["BillingProvider"] != null)
                        {
                            if (dsMasterEDIData.Tables["BillingProvider"].Rows.Count > 0)
                            {
                                oTriarqClaim.BillingProvider = new Provider();
                                oTriarqClaim.BillingProvider.FirstName = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["FirstName"]);
                                oTriarqClaim.BillingProvider.MiddleName = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["MiddleName"]);
                                oTriarqClaim.BillingProvider.LastName = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["LastName"]);
                                oTriarqClaim.BillingProvider.Suffix = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["sSuffix"]);
                                oTriarqClaim.BillingProvider.TIN = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["SecondaryQualifierValue"]);
                                oTriarqClaim.BillingProvider.NPI = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PrimaryQualifierValue"]);
                                oTriarqClaim.BillingProvider.Taxonomy = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["Taxonomy"]);
                                oTriarqClaim.BillingProvider.EntityType = Convert.ToInt16(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["EntityType"]);
                                oTriarqClaim.BillingProvider.Address = new Address();
                                oTriarqClaim.BillingProvider.Address.AddressLine1 = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyAddline1"]);
                                oTriarqClaim.BillingProvider.Address.AddressLine2 = "";
                                oTriarqClaim.BillingProvider.Address.City = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyCity"]);
                                oTriarqClaim.BillingProvider.Address.State = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyState"]);
                                oTriarqClaim.BillingProvider.Address.Zip = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyZIP"]);
                                oTriarqClaim.BillingProvider.Address.Country = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["CountryCode"]);
                                oTriarqClaim.BillingProvider.Address.AreaCode = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyAreaCode"]);
                            }
                        }
                        #endregion

                        #region "Facility"
                        if (dsMasterEDIData.Tables["Facility"] != null)
                        {
                            if (dsMasterEDIData.Tables["Facility"].Rows.Count > 0)
                            {
                                oTriarqClaim.ClaimFacility = new Facility();
                                oTriarqClaim.ClaimFacility.Type = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["SecondaryQualifierId"]);
                                oTriarqClaim.ClaimFacility.Name = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["LastName"]);
                                oTriarqClaim.ClaimFacility.NPI = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["PrimaryQualifierValue"]);
                                oTriarqClaim.ClaimFacility.TIN = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["SecondaryQualifierValue"]);
                                oTriarqClaim.ClaimFacility.Taxonomy = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Taxonomy"]);
                                oTriarqClaim.ClaimFacility.EntityType = Convert.ToInt16(dsMasterEDIData.Tables["Facility"].Rows[0]["EntityType"]);
                                oTriarqClaim.ClaimFacility.POSCode = "";
                                oTriarqClaim.ClaimFacility.POSDesc = "";
                                oTriarqClaim.ClaimFacility.Address = new Address();
                                oTriarqClaim.ClaimFacility.Address.AddressLine1 = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Address1"]);
                                oTriarqClaim.ClaimFacility.Address.AddressLine2 = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Address2"]);
                                oTriarqClaim.ClaimFacility.Address.City = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["City"]);
                                oTriarqClaim.ClaimFacility.Address.State = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["State"]);
                                oTriarqClaim.ClaimFacility.Address.Country = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["CountryCode"]);
                                oTriarqClaim.ClaimFacility.Address.Zip = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Zip"]);
                                oTriarqClaim.ClaimFacility.Address.AreaCode = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["AreaCode"]);
                            }
                        }
                        #endregion
                    }
                }





                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.RequestProcessing, "Completed filling Claim Model object.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
                return oTriarqClaim;
            }
            catch (Exception ex)
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.RequestProcessing, "Exception while filling Claim Model object.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FillClaimModel, ex, ActivityOutCome.Failure);
                return null;
            }
            finally
            {
                if (dsClaimData != null)
                {
                    dsClaimData.Dispose();
                    dsClaimData = null;
                }

                if (dsMasterEDIData != null)
                {
                    dsMasterEDIData.Dispose();
                    dsMasterEDIData = null;
                }

                if (dsClearingHouse != null)
                {
                    dsClearingHouse.Dispose();
                    dsClearingHouse = null;
                }

            }
        }

        private bool ValidateConnectionString(string _AlphaIIAuthentication, string _AlphaIIServerName, string _AlphaIIDatabase, string _AlphaIIUserName, string _AlphaIIPassword)
        {
            Boolean _Result = false;
            SqlConnection _connection = new SqlConnection();
            try
            {
                string _connstring = "";
                if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                {
                    _connstring = "Integrated Security=SSPI; Persist Security Info=False; Data Source=" + _AlphaIIServerName + "; Initial Catalog=" + _AlphaIIDatabase + "; Connection Timeout = 0";
                }
                else
                {
                    _connstring = "Persist Security Info=False;Data Source=" + _AlphaIIServerName + ";Initial Catalog=" + _AlphaIIDatabase + ";User ID=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                }
                _connection.ConnectionString = _connstring;
                _connection.Open();
                _connection.Close();
                _Result = true;
            }
            catch //(Exception ex)
            {
                _Result = false;
            }
            return _Result;
        }

        public bool ValidateTriarqClaimData(TRIARQClaim oTriarqClaim)
        {
            bool ValidClaim = true;

            string _Message = string.Empty;
            string strMissingText = string.Empty;
            string _MessageHeader = string.Empty;
            string strMessage = string.Empty;
            string _ClaimMessageHeader = string.Empty;

            bool _IsClaimNumberAdded = false;
            string strBillingSetting = string.Empty;
            string PrimaryBillingProviderID = string.Empty;

            string _FilePath = Application.StartupPath;

            ClsEDIValidation Edisetting = new ClsEDIValidation();
            ClsAlphaValidation AlphaSetting = new ClsAlphaValidation();

            Edisetting.GetEDIValidation(dtEDISetting);
            AlphaSetting.GetAlphaValidation(dtAlphaSetting);


            _MessageHeader += "";

            #region "Alpha Settings"
            if (AlphaSetting.ClaimValidationSetting == "None")
            {
                if (AlphaSetting.ShowMessageForValidation == true)
                {
                    MessageBox.Show("You have not selected any validation setting, claims may go with invalid data.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else if (AlphaSetting.ClaimValidationSetting == "Alpha2")
            {
                if (!ValidateConnectionString(AlphaSetting.AlphaAuthentication, AlphaSetting.AlphaServerName, AlphaSetting.AlphaDatabaseName, AlphaSetting.AlphaUserName, AlphaSetting.AlphaPassword))
                {
                    MessageBox.Show("Connection for Alpha II cannot be establish, please do the setting from gloPM Admin.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            #endregion


            if (dtClearingHouse == null || dtClearingHouse.Rows.Count < 1)
            {
                MessageBox.Show("Clearing House information is not present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (oTriarqClaim != null)
            {
                if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                {
                    MessageBox.Show("Submitter/Provider information is not present.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }



            #region " Clearing House "
            //ISA and GS Settings

            if (Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim() == "")
            {
                if (Edisetting.SenderID == true)
                    strMissingText += "Sender ID" + Environment.NewLine + "" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim() == "")
            {
                if (Edisetting.ReceiverID == true)
                    strMissingText += "Receiver ID" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim() == "")
            {
                if (Edisetting.SenderCode == true)
                    strMissingText += "Sender Code" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim() == "")
            {
                if (Edisetting.ReceiverCode == true)
                    strMissingText += "Receiver Code" + Environment.NewLine + "";
            }
            #endregion " Clearing House "

            #region " Submitter "
            //Submitter


            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim() == "")
            {
                if (Edisetting.SubmitterName == true)
                    strMissingText += "Submitter Name" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim() == "")
            {
                if (Edisetting.SubmitterContactName == true)
                    strMissingText += "Submitter Contact Person Name" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim() == "")
            {
                if (Edisetting.SubmitterPhone == true)
                    strMissingText += "Submitter Contact Person Number" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterCity"]).Trim() == "")
            {
                if (Edisetting.SubscriberCity == true)
                    strMissingText += "Submitter City" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterState"]).Trim() == "")
            {
                if (Edisetting.SubmitterState == true)
                    strMissingText += "Submitter State" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterZIP"]).Trim() == "")
            {
                if (Edisetting.SubmitterZIP == true)
                    strMissingText += "Submitter Zip" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress1"]).Trim() + " " + Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress2"]).Trim() == "")
            {
                if (Edisetting.SubmitterAddress1 == true)
                    strMissingText += "Submitter Address" + Environment.NewLine + "";
            }
            #endregion " Submitter "


            if (strMissingText.Trim() != "")
            {
                _MessageHeader = _MessageHeader + strMissingText;
            }
            else
            {
                _MessageHeader = "";
            }

            #region " Patient Information "

            //Patient Information
            if (oTriarqClaim.Patient != null)
            {
                //if (Convert.ToString(oTransaction.ClaimNumber).Trim() == "")
                //{
                //    strMessage += "Patient Account No" + Environment.NewLine + "";
                //}
                if (oTriarqClaim.Patient.LastName.Trim() == "")
                {
                    //if (GetValidationFieldsSettings("Patient Last Name"))
                    strMessage += "Patient Last Name" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.FirstName.Trim() == "")
                {
                    if (Edisetting.PatientFirstName == true)
                        strMessage += "Patient First Name" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.MiddleName.Trim() == "")
                {
                    if (Edisetting.PatientMiddleName == true)
                        strMessage += "Patient Middle Name" + Environment.NewLine + "";
                }
                //if (oTransaction.PatientSSN.Trim() == "")
                //{
                //    if (Edisetting.PatientSSN == true)
                //        strMessage += "Patient SSN" + Environment.NewLine + "";
                //}
                if (oTriarqClaim.Patient.Gender.Trim() == "")
                {
                    // if (GetValidationFieldsSettings("Patient Gender"))
                    strMessage += "Patient Gender" + Environment.NewLine + "";
                }
                if (Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.Patient.DOB.ToShortDateString())).Trim() == "")
                {
                    //if (GetValidationFieldsSettings("Patient Date of Birth"))
                    strMessage += "Patient Date of Birth" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.Address.AddressLine1.Trim() == "")
                {
                    if (Edisetting.PatientAddress == true)
                        strMessage += "Patient Address line 1" + Environment.NewLine + "";
                }

                //if (oTriarqClaim.Patient.Address.AddressLine2.Trim() == "")
                //{
                //    if (Edisetting.PatientAddress == true)
                //        strMessage += "Patient Address line 2" + Environment.NewLine + "";
                //}


                if (oTriarqClaim.Patient.Address.City.Trim() == "")
                {
                    //  if (GetValidationFieldsSettings("Patient City"))
                    strMessage += "Patient City" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.Address.State.Trim() == "")
                {
                    // if (GetValidationFieldsSettings("Patient State"))
                    strMessage += "Patient State" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.Address.Zip.Trim() == "")
                {
                    //   if (GetValidationFieldsSettings("Patient Zip"))
                    strMessage += "Patient Zip" + Environment.NewLine + "";
                }
            }
            else
            {
                MessageBox.Show("Patient information is not present for claim number " + oTriarqClaim.ClaimNo.ToString().Trim() + ".  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            #endregion " Patient Information "

            if (oTriarqClaim != null)
            {
                if (oTriarqClaim.ClaimServiceLines.Count > 0)
                {
                    if (dtFacility == null)
                    {
                        MessageBox.Show("Facility information is not present for claim number " + oTriarqClaim.ClaimNo.ToString().Trim() + ".  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (Convert.ToInt64(oTriarqClaim.ProviderID) != 0 && oTriarqClaim.ProviderID.ToString() != "")
                    {
                        if (dtBillingProvider == null || dtBillingProvider.Rows.Count == 0)
                        {
                            MessageBox.Show("Provider information is not present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"]).Trim() == "")// && IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " is using Billing Type " + dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + " which has no ID Qualifier Code. \n Batch will not send.  Please review Billing ID Qualifier Setup.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"]).ToString().Trim() == "")//&& IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " is using missing " + dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + ".\n Batch will not send.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (dtBillingProvider.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "" && dtBillingProvider.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "-1" && dtBillingProvider.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "0" && Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifiervalue"]) == "" && Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "")//&& IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " is using " + dtBillingProvider.Rows[0]["Setting"].ToString().Trim() + " which has mismatch in source and other ID type.\nBatch will not send.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (dtPatientInsurance == null || dtPatientInsurance.Rows.Count < 1)
                        {
                            if (_IsClaimNumberAdded == false)
                            {

                                if (Edisetting.SubscriberLastName == true)
                                    strMessage += "Subscriber Last Name" + Environment.NewLine + "";

                                //if (Edisetting.SubscriberRelationship  == true )
                                strMessage += "Subscriber Relationship" + Environment.NewLine + "";


                                if (Edisetting.PlanType == true)
                                    strMessage += "Plan Type" + Environment.NewLine + "";


                                if (Edisetting.SubscriberFirstName == true)
                                    strMessage += "Subscriber First Name" + Environment.NewLine + "";


                                //     if (GetValidationFieldsSettings("Subscriber Insurance ID"))
                                strMessage += "Insurance ID" + Environment.NewLine + "";


                                if (Edisetting.SubscriberAddress == true)
                                    strMessage += "Subscriber Address" + Environment.NewLine + "";

                                if (Edisetting.SubscriberGroupID == true)
                                    strMessage += "Subscriber Group ID" + Environment.NewLine + "";

                                if (Edisetting.SubscriberCity == true)
                                    strMessage += "Subscriber City" + Environment.NewLine + "";

                                if (Edisetting.SubmitterState == true)
                                    strMessage += "Subscriber State" + Environment.NewLine + "";

                                if (Edisetting.SubmitterZIP == true)
                                    strMessage += "Subscriber Zip" + Environment.NewLine + "";

                                //    if (GetValidationFieldsSettings("Subscriber Date of Birth"))
                                strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";

                                //   if (GetValidationFieldsSettings("Subscriber Gender"))
                                strMessage += "Subscriber Gender" + Environment.NewLine + "";

                                if (Edisetting.PayerName == true)
                                    strMessage += "Payer/Insurance Name" + Environment.NewLine + "";


                                if (Edisetting.PayerId == true)
                                    strMessage += "Payer ID" + Environment.NewLine + "";

                                if (Edisetting.PayerAddress == true)
                                    strMessage += "Payer Address" + Environment.NewLine + "";

                                if (Edisetting.PayerCity == true)
                                    strMessage += "Payer City" + Environment.NewLine + "";

                                if (Edisetting.PayerState == true)
                                    strMessage += "Payer State" + Environment.NewLine + "";

                                if (Edisetting.PayerZip == true)
                                    strMessage += "Payer Zip" + Environment.NewLine + "";

                                _IsClaimNumberAdded = true;
                            }
                        }
                    }

                    _ClaimMessageHeader = " " + Environment.NewLine + "For Patient: " + oTriarqClaim.Patient.FirstName.Trim() + " " + oTriarqClaim.Patient.LastName.Trim() + "  and Claim Number: " + oTriarqClaim.ClaimNo.Trim() + " " + Environment.NewLine + "" + Environment.NewLine + "";


                    string _strMessage1 = "";
                    if (AlphaSetting.IsCheckInvalidICD9 == true)
                    {
                        _strMessage1 = Convert.ToString(dtMasterSetting.Rows[0]["InvalidICD9"]);
                    }

                    if (_strMessage1.Trim() != "")
                    {
                        _strMessage1 = _strMessage1.Substring(0, _strMessage1.Length - 1);
                        strMessage += "Invalid ICD9's " + _strMessage1 + Environment.NewLine + "";
                    }

                }
                else
                {
                    MessageBox.Show("Transaction Lines are not there in selected transaction(s). ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            #region " Billing Provider "
            //Billing Provider
            if (dtBillingProvider != null || dtBillingProvider.Rows.Count > 0)
            {
                string _BillingAddress = "";
                string _BillingCity = "";
                string _BillingState = "";
                string _BillingZIP = "";
                string _BillingNPI = "";

                _BillingAddress = dtBillingProvider.Rows[0]["Address1"].ToString().Trim();
                _BillingCity = dtBillingProvider.Rows[0]["City"].ToString().Trim();
                _BillingState = dtBillingProvider.Rows[0]["State"].ToString().Trim();
                _BillingZIP = dtBillingProvider.Rows[0]["Zip"].ToString().Trim();
                _BillingNPI = dtBillingProvider.Rows[0]["PrimaryQualifierValue"].ToString().Trim();



                if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Facility"))
                {
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.FacilityName == true)
                            strMessage += "Facility Name" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.FacilityAddress1 == true)
                            strMessage += "Facility Address" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        //   if (GetValidationFieldsSettings("Facility City"))
                        strMessage += "Facility City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        // if (GetValidationFieldsSettings("Facility State"))
                        strMessage += "Facility State" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        //  if (GetValidationFieldsSettings("Facility Zip"))
                        strMessage += "Facility Zip" + Environment.NewLine + "";
                    }
                    if (_BillingNPI.Trim() == "")
                    {
                        if (Edisetting.FacilityNPI == true)
                            strMessage += "Facility NPI" + Environment.NewLine + "";
                    }
                }
                else if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Clinic"))
                {
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingLastName == true)
                            strMessage += "Clinic Name" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.BillingAddress == true)
                            strMessage += "Clinic Address" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        if (Edisetting.BillingCity == true)
                            strMessage += "Clinic City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        if (Edisetting.BillingState == true)
                            strMessage += "Clinic State" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        if (Edisetting.BillingZIP == true)
                            strMessage += "Clinic Zip" + Environment.NewLine + "";
                    }
                    if (_BillingNPI.Trim() == "")
                    {
                        if (Edisetting.BillingNPI == true)
                            strMessage += "Clinic NPI" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() == "")
                        {
                            if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "34" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "24" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "SY" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "EI")
                            {
                                strMessage += "Billing Provider " + dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }
                }
                else if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Company"))
                {
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingLastName == true)
                            strMessage += "Company Name" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.BillingAddress == true)
                            strMessage += "Company Address" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        if (Edisetting.BillingCity == true)
                            strMessage += "Company City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        if (Edisetting.BillingState == true)
                            strMessage += "Company State" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        if (Edisetting.BillingZIP == true)
                            strMessage += "Company Zip" + Environment.NewLine + "";
                    }
                    if (_BillingNPI.Trim() == "")
                    {
                        if (Edisetting.BillingNPI == true)
                            strMessage += "Company NPI" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() == "")
                        {
                            if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "34" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "24" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "SY" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "EI")
                            {
                                strMessage += "Billing Provider " + dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }
                }
                else
                {

                    if (dtBillingProvider.Rows[0]["FirstName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingFirstName == true)
                            strMessage += "Billing Provider First Name" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingLastName == true)
                            strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["MiddleName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingMiddleName == true)
                            strMessage += "Billing Provider Middle Name" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        if (Edisetting.BillingCity == true)
                            strMessage += "Billing Provider City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        if (Edisetting.BillingState == true)
                            strMessage += "Billing Provider State" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.BillingAddress == true)
                            strMessage += "Billing Provider Address" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        if (Edisetting.BillingZIP == true)
                            strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["PrimaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["PrimaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (_BillingNPI.Trim() == "")
                        {
                            strMessage += strBillingSetting + "Billing Provider " + dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                        }
                    }

                    if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() == "")
                        {
                            if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "34" &&
                                dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "24" &&
                                dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "SY" &&
                                dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "EI")
                            {
                                strMessage += "Billing Provider " + dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }

                    if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Facility") != true || dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Clinic") != true)
                    {
                        if (dtBillingProvider.Rows[0]["Taxonomy"].ToString().Trim() == "")
                        {
                            if (Edisetting.BillingTaxonomy == true)
                                strMessage += "Billing Provider Taxonomy" + Environment.NewLine + "";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Billing provider information is not present for claim number " + oTriarqClaim.ClaimNo.Trim() + ".\nClaim Status will not send. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            #endregion " Billing Provider "

            #region "Facility "
            //Billing Provider
            if (dtFacility != null && dtFacility.Rows.Count > 0)
            {
                bool IsincludeFacility = false;
                IsincludeFacility = Convert.ToBoolean(dtFacility.Rows[0]["bIncludeFacility"]);
                if (Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") != PrimaryBillingProviderID || (Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") == PrimaryBillingProviderID && IsincludeFacility == true))
                {

                    if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bISOtherID"]) == true)
                    {
                        if (dtFacility.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "" && dtFacility.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "0" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifiervalue"]) == "" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]).Trim() == "")//&& IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.Trim() + " for Patient " + oTriarqClaim.Patient.Code.Trim() + " is using " + dtFacility.Rows[0]["Setting"].ToString().Trim() + " which has mismatch in source and other ID type.\nBatch will not send.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierID"].ToString().Trim()) == "0" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifiervalue"]) == "" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]).Trim() == "")
                        {
                            strMessage += "Facility Tax ID" + Environment.NewLine + "";
                        }
                    }

                    string _FacilityAddress = "";
                    string _FacilityCity = "";
                    string _FacilityState = "";
                    string _FacilityZIP = "";
                    string _FacilityNPI = "";



                    _FacilityAddress = dtFacility.Rows[0]["Address1"].ToString().Trim();
                    _FacilityCity = dtFacility.Rows[0]["City"].ToString().Trim();
                    _FacilityState = dtFacility.Rows[0]["State"].ToString().Trim();
                    _FacilityZIP = dtFacility.Rows[0]["Zip"].ToString().Trim();
                    _FacilityNPI = dtFacility.Rows[0]["PrimaryQualifierValue"].ToString().Trim();



                    if (dtFacility.Rows[0]["sSource"].ToString().Trim().Contains("Facility"))
                    {
                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Facility Name" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Facility Address" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            //   if (GetValidationFieldsSettings("Facility City"))
                            strMessage += "Facility City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Facility State"))
                            strMessage += "Facility State" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            //  if (GetValidationFieldsSettings("Facility Zip"))
                            strMessage += "Facility Zip" + Environment.NewLine + "";
                        }
                        if (_FacilityNPI.Trim() == "")
                        {
                            if (Edisetting.FacilityNPI == true)
                                strMessage += "Facility NPI" + Environment.NewLine + "";
                        }
                    }
                    else if (dtFacility.Rows[0]["sSource"].ToString().Trim().Contains("Clinic"))
                    {
                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Clinic Name" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Clinic Address" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            if (Edisetting.FacilityCity == true)
                                strMessage += "Clinic City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            if (Edisetting.FacilityState == true)
                                strMessage += "Clinic State" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            if (Edisetting.FacilityZip == true)
                                strMessage += "Clinic Zip" + Environment.NewLine + "";
                        }
                        if (_FacilityNPI.Trim() == "")
                        {
                            if (Edisetting.FacilityNPI == true)
                                strMessage += "Clinic NPI" + Environment.NewLine + "";
                        }
                    }
                    else if (dtFacility.Rows[0]["sSource"].ToString().Trim().Contains("Company"))
                    {
                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Company Name" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Company Address" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            if (Edisetting.FacilityCity == true)
                                strMessage += "Company City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            if (Edisetting.FacilityState == true)
                                strMessage += "Company State" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            if (Edisetting.FacilityZip == true)
                                strMessage += "Company Zip" + Environment.NewLine + "";
                        }
                        if (_FacilityNPI.Trim() == "")
                        {
                            if (Edisetting.FacilityNPI == true)
                                strMessage += "Company NPI" + Environment.NewLine + "";
                        }
                    }
                    else
                    {

                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            if (Edisetting.BillingCity == true)
                                strMessage += "Billing Provider City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            if (Edisetting.FacilityState == true)
                                strMessage += "Billing Provider State" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Billing Provider Address" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            if (Edisetting.FacilityZip == true)
                                strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                        }
                        if (dtFacility.Rows[0]["PrimaryQualifier"].ToString().Trim() != "" || dtFacility.Rows[0]["PrimaryQualifierValue"].ToString().Trim() != "" || dtFacility.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() != "")
                        {
                            if (_FacilityNPI.Trim() == "")
                            {
                                //if (GetValidationFieldsSettings("Billing Provider NPI"))

                                strMessage += strBillingSetting + "Billing Provider " + dtFacility.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Facility information is not present for claim number " + oTriarqClaim.ClaimNo.Trim() + ".\nClaim Status will not send. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            #endregion " Facility "

            #region " Subscriber "
            //Subscriber
            if (dtPatientInsurance != null && dtPatientInsurance.Rows.Count > 0)
            {
                for (int _InsRow = 0; _InsRow < dtPatientInsurance.Rows.Count; _InsRow++)
                {
                    #region " Primary Insurance "
                    if (_InsRow == 0)
                    {
                        string _strRelation = "";
                        _strRelation = dtPatientInsurance.Rows[0]["RelationshipCode"].ToString().Trim();

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubLName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SubscriberLastName == true)
                                strMessage += "Subscriber Last Name" + Environment.NewLine + "";
                        }

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                        {
                            //if (GetValidationFieldsSettings("Subscriber Relationship"))
                            strMessage += "Subscriber Relationship" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                        {

                            if (Edisetting.PlanType == true)
                                strMessage += "Plan Type" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubFName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SubscriberFirstName == true)
                                strMessage += "Subscriber First Name" + Environment.NewLine + "";
                        }

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Subscriber Insurance ID"))
                            strMessage += "Insurance ID" + Environment.NewLine + "";
                        }


                        if (Convert.ToBoolean(dtPatientInsurance.Rows[0]["bIncludeSubscriberAddress"]) == true || _strRelation == "18")
                        {
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberAddress == true)
                                    strMessage += "Subscriber Address" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sGroup"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberGroupID == true)
                                    strMessage += "Subscriber Group ID" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberCity == true)
                                    strMessage += "Subscriber City" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberState == true)
                                    strMessage += "Subscriber State" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberZip == true)
                                    strMessage += "Subscriber Zip" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["dtDOB"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                            {
                                //if (GetValidationFieldsSettings("Subscriber Date of Birth"))
                                strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberGender"]).Trim() == "" &&
                                Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                            {
                                //if (GetValidationFieldsSettings("Subscriber Gender"))
                                strMessage += "Subscriber Gender" + Environment.NewLine + "";
                            }
                        }

                        if (dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"].ToString() == "0" || dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"].ToString().Trim() == "")
                        {
                            if (Edisetting.InsuranceTypeCode == true)
                                strMessage += "Insurance Type Code" + Environment.NewLine + "";
                        }

                        //Payer
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceName"]).Trim() == "")
                        {
                            if (Edisetting.PayerName == true)
                                strMessage += "Payer/Insurance Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerID"]).Trim() == "")
                        {
                            if (Edisetting.PayerId == true)
                                strMessage += "Payer ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerAddress1"]).Trim() == "")
                        {
                            if (Edisetting.PayerAddress == true)
                                strMessage += "Payer Address" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerCity"]).Trim() == "")
                        {
                            if (Edisetting.PayerCity == true)
                                strMessage += "Payer City" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerState"]).Trim() == "")
                        {
                            if (Edisetting.PayerState == true)
                                strMessage += "Payer State" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerZip"]).Trim() == "")
                        {
                            if (Edisetting.PayerZip == true)
                                strMessage += "Payer Zip" + Environment.NewLine + "";
                        }

                    }

                    #endregion " Primary Insurance "

                    #region " Secondary Insurance "
                    if (_InsRow == 1)
                    {
                        //Other Insurance
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubLName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SecondarySubLName == true)
                                strMessage += "Secondary Insurance Subscriber Last Name" + Environment.NewLine + "";
                        }

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryPlanType == true)
                                strMessage += "Secondary Insurance Plan Type" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Relationship"))
                            strMessage += "Secondary Insurance Subscriber Relationship" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance ID"))
                            strMessage += "Secondary Insurance ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sGroup"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryGroupId == true)
                                strMessage += "Secondary Insurance Group ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryInsAddress == true)
                                strMessage += "Secondary Insurance Address" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubFName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SecondarySubFName == true)
                                strMessage += "Secondary Insurance Subscriber First Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceName"]) == "")
                        {
                            if (Edisetting.SecondaryInsName == true)
                                strMessage += "Secondary Insurance Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerID"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryInsPayerID == true)
                                strMessage += "Secondary Insurance Payer ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubCity == true)
                                strMessage += "Secondary Insurance City" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubState == true)
                                strMessage += "Secondary Insurance State" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubZip == true)
                                strMessage += "Secondary Insurance Zip" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["dtDOB"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Date of Birth"))
                            strMessage += "Secondary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberGender"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Gender"))
                            strMessage += "Secondary Insurance Subscriber Gender" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "0" ||
                            Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == string.Empty ||
                            Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubInsType == true)
                                strMessage += "Secondary Insurance Type Code" + Environment.NewLine + "";
                        }
                    }

                    #endregion " Secondary Insurance "

                    #region " Tertiary Insurance "
                    if (_InsRow == 2)
                    {
                        //Other Insurance
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubLName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.TertiarySubLName == true)
                                strMessage += "Tertiary Insurance Subscriber Last Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryPlanType == true)
                                strMessage += "Tertiary Plan Type" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Relationship"))
                            strMessage += "Tertiary Insurance Subscriber Relationship" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Tertiary Insurance ID"))
                            strMessage += "Tertiary Insurance ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sGroup"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryGroupId == true)
                                strMessage += "Tertiary Insurance Group ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryInsAddress == true)
                                strMessage += "Tertiary Insurance Address" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubFName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.TertiarySubFName == true)
                                strMessage += "Tertiary Insurance Subscriber First Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceName"]) == "")
                        {
                            if (Edisetting.TertiaryInsName == true)
                                strMessage += "Tertiary Insurance Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerID"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryInsPayerID == true)
                                strMessage += "Tertiary Insurance Payer ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubCity == true)
                                strMessage += "Tertiary Insurance City" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubState == true)
                                strMessage += "Tertiary Insurance State" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubZip == true)
                                strMessage += "Tertiary Insurance Zip" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["dtDOB"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            // if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Date of Birth"))
                            strMessage += "Tertiary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberGender"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            //   if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Gender"))
                            strMessage += "Tertiary Insurance Subscriber Gender" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "0" || Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == string.Empty || Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubInsType == true)
                                strMessage += "Tertiary Insurance Type Code" + Environment.NewLine + "";
                        }
                    }

                    #endregion " Tertiary Insurance "
                }
            }
            #endregion " Subscriber "

            #region " Rendering Provider "

            if (dtRenderingProvider != null && Convert.ToString(dtRenderingProvider.Rows[0]["QualifierMstID"]).Trim() == "0" && Convert.ToString(dtRenderingProvider.Rows[0]["QualifierID"]).Trim() != "0")//&& IsEDIgeneration == true
            {
                MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " having mismatch in Electronic Rendering ProviderID Type.\nBatch will not send.  Please review Billing ID Qualifier Setup.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            bool IsincludeRenderingProvider = false;
            IsincludeRenderingProvider = Convert.ToBoolean(dtRenderingProvider.Rows[0]["bIncludeRenderingProvider"]);
            if (Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") != PrimaryBillingProviderID || (Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") == PrimaryBillingProviderID && IsincludeRenderingProvider == true))
            {
                if (dtRenderingProvider != null && dtRenderingProvider.Rows.Count > 0)
                {
                    if (Convert.ToString(dtRenderingProvider.Rows[0]["sLastName"]).Trim() == "")
                    {
                        if (Edisetting.RenderingProLastName == true)
                            strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                    }
                    if (Convert.ToString(dtRenderingProvider.Rows[0]["sFirstName"]).Trim() == "")
                    {
                        if (Edisetting.RenderingProFirstName == true)
                            strMessage += "Rendering Provider First Name" + Environment.NewLine + "";
                    }

                    if (Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]).Trim() == "")
                    {
                        if (Edisetting.RenderingProNPI == true)
                            strMessage += "Rendering Provider NPI" + Environment.NewLine + "";
                    }
                }
            }


            #endregion " Rendering Provider "

            #region " Referring Provider "

            if (oTriarqClaim.ReferalProviderID_New > 0 || oTriarqClaim.IsSameAsBillingProvider)
            {
                if (dtRefferingProvider != null && dtRefferingProvider.Rows.Count > 0)
                {
                    //2310B Referring PROVIDER
                    //NM1 Referring PROVIDER NAME

                    if (dtRefferingProvider.Rows[0]["sLastName"].ToString().Trim().Replace("*", "") == "")
                    {
                        if (Edisetting.ReferringProLastName == true)
                            strMessage += "Referring Provider Last Name" + Environment.NewLine + "";
                    }
                    if (dtRefferingProvider.Rows[0]["sFirstName"].ToString().Trim().Replace("*", "") == "")
                    {
                        if (Edisetting.ReferringProFirstName == true)
                            strMessage += "Referring Provider First Name" + Environment.NewLine + "";
                    }
                    if (dtRefferingProvider.Rows[0]["sNPI"].ToString().Trim().Replace("*", "") == "")
                    {
                        if (Edisetting.ReferringProNPI == true)
                            strMessage += "Referring Provider NPI" + Environment.NewLine + "";
                    }
                }

            }

            #endregion " Referring Provider "

            if (_MessageHeader != "")
            {
                _Message = "";
                _Message = _MessageHeader;
            }


            if (_Message.Trim() != "")
            {
                string _Header = "Following fields are missing in database:" + Environment.NewLine + "" + Environment.NewLine + "";
                _Header += _Message;
                _FilePath = _FilePath + "EDI_276_Validation.txt";
                System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                oStreamWriter.WriteLine(_Header);
                oStreamWriter.Close();
                oStreamWriter.Dispose();
                System.Diagnostics.Process.Start(_FilePath);
                return false;
            }
            else
            {
                //if (ValidClaim == true)
                //{
                //    MessageBox.Show("All data is present and valid.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                return ValidClaim;
            }

        }

        private DataSet GetClearingHouseData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsClearingHouse = null;
            try
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Started fetching Claim clearing house.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                oDB.Connect(false);
                oDBParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_SELECT_ClearingHouse_EDI_276", oDBParameters, out dsClearingHouse);
                oDB.Disconnect();
                dsClearingHouse.Tables[0].TableName = "ClearingHouse";
                dsClearingHouse.Tables[1].TableName = "Submitter";
                dsClearingHouse.Tables[2].TableName = "EDISetting";
                dsClearingHouse.Tables[3].TableName = "AlphaSetting";

                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Completed fetching Claim clearing house.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                dsClearingHouse = null;
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                dsClearingHouse = null;
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Exception while Fetching Claim clearing house.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {

                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dsClearingHouse;
        }

        private DataSet GetClaimData()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsTransaction = null;
            try
            {
                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Started fetching Claim Data.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                oDB.Connect(false);
                oDBParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_SELECT_Claim_EDI_276", oDBParameters, out dsTransaction);
                oDB.Disconnect();
                dsTransaction.Tables[0].TableName = "ClaimData";
                dsTransaction.Tables[1].TableName = "ClaimLines";
                dsTransaction.Tables[3].TableName = "ClaimPatient";
                dsTransaction.Tables[5].TableName = "ClaimClinic";
                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Completed fetching Claim Data.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                dsTransaction = null;
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                dsTransaction = null;
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Exception while Fetching Claim Data.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {

                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dsTransaction;
        }

        private DataSet GetMasterEDIData(TRIARQClaim oTriarqClaim)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsMasterEDI = null;
            try
            {
                //  clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Started fetching Claim Master Data.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                oDBParameters.Add("@nRenderingProviderID", oTriarqClaim.ClaimServiceLines[0].RenderingProviderId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nBillingProviderID", oTriarqClaim.ProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", oTriarqClaim.ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", oTriarqClaim.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransMasterID", oTriarqClaim.MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", oTriarqClaim.FacilityCode, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", oTriarqClaim.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsPhysician ", oTriarqClaim.IsSameAsBillingProvider, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@TransactionID", oTriarqClaim.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@RefferingProvider", oTriarqClaim.ReferalProviderID_New, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsSecondary", false, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Master_EDI_276_5010", oDBParameters, out dsMasterEDI);
                oDB.Disconnect();
                dsMasterEDI.Tables[0].TableName = "MidLevelID";
                dsMasterEDI.Tables[1].TableName = "PatientInsurance";
                dsMasterEDI.Tables[2].TableName = "Facility";
                dsMasterEDI.Tables[3].TableName = "BillingProvider";
                dsMasterEDI.Tables[4].TableName = "RefferingProvider";
                dsMasterEDI.Tables[5].TableName = "RenderingProvider";
                dsMasterEDI.Tables[6].TableName = "BillingProviderTaxonomy";
                dsMasterEDI.Tables[7].TableName = "MasterSetting";
                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Exception while Completed fetching Claim Data.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                dsMasterEDI = null;
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                dsMasterEDI = null;
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Fetching Claim Data.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {

                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dsMasterEDI;
        }

        public void GetSetting(string SettingName, string ClinicId, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = '" + SettingName.Trim().ToUpper() + "' AND nClinicID = " + ClinicId + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                //oDBParameters.Dispose();
                //oDBParameters = null;

                oDB.Dispose();
                oDB = null;
            }
        }

        //public string Generate276RequestFile(TRIARQClaim oTriarqClaim)
        //{
        //    string InterchangeHeader = "";
        //    bool isAllDataAvilable = true;
        //    string sEdiFileName = "";
        //    string AusId = "";
        //    if (oTriarqClaim != null)
        //    {

        //        if (oTriarqClaim.ClaimClinic != null)
        //        {
        //            AusId = oTriarqClaim.ClaimClinic.AUSID;
        //        }
        //        sEdiFileName = sPath + "\\TRIARQ_276_ClaimNumber_" + oTriarqClaim.FormattedClaimNumber + "_" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".txt.bci";
        //        seffilepath_276_005010X214_SemRef = sPath + "\\SEF\\276_005010X212.SemRef.SEF";
        //        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation starts.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
        //        //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.RequestFileGeneration);

        //        try
        //        {

        //            if (oTriarqClaim.ResponsibleParty == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Responsible party information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }
        //            if (oTriarqClaim.RequestHeader == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to claim Header information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }
        //            if (oTriarqClaim.ClaimServiceLines == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Service lines information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }
        //            if (oTriarqClaim.ClaimFacility == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim facility information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }
        //            if (oTriarqClaim.ClaimClinic == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim Clinic information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }
        //            if (oTriarqClaim.BillingProvider == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Billing Provider information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }
        //            if (oTriarqClaim.Patient == null)
        //            {
        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim Patient information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                isAllDataAvilable = false;
        //            }

        //            if (isAllDataAvilable == true)
        //            {
        //                LoadEdiSchema();

        //                int nHlCounter = 0;
        //                int nHlInfoReceiverParent = 0;
        //                int nHlServiceProviderParent = 0;
        //                int nHlSubscriberParent = 0;
        //                int nHlDependentParent = 0;

        //                ediInterchange.Set(ref oInterchange, oEdiDoc.CreateInterchange("X", "005010"));
        //                ediDataSegment.Set(ref oSegment, oInterchange.GetDataSegmentHeader());
        //                #region "ISA"

        //                string ISA_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
        //                string ISA_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
        //                InterchangeHeader = clsGeneral.ControlNumberGeneration();
        //                oSegment.set_DataElementValue(1, 0, "00"); 		// Authorization Information Qualifier (I01) 
        //                oSegment.set_DataElementValue(2, 0, "          "); 		// Authorization Information (I02) 
        //                oSegment.set_DataElementValue(3, 0, "00"); 		// Security Information Qualifier (I03) 
        //                oSegment.set_DataElementValue(4, 0, "          "); 		// Security Information (I04) 
        //                oSegment.set_DataElementValue(5, 0, oTriarqClaim.RequestHeader.SenderQualifier); 		// Interchange ID Qualifier (I05) 
        //                oSegment.set_DataElementValue(6, 0, Convert.ToString(oTriarqClaim.RequestHeader.SubmitterID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Sender ID (I06) 
        //                oSegment.set_DataElementValue(7, 0, oTriarqClaim.RequestHeader.ReceiverQualifier); 		// Interchange ID Qualifier (I05) 
        //                oSegment.set_DataElementValue(8, 0, Convert.ToString(oTriarqClaim.RequestHeader.ReceiverID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Receiver ID (I07) 
        //                oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2)); 		// Interchange Date (I08) 
        //                oSegment.set_DataElementValue(10, 0, clsGeneral.FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Time (I09) 
        //                oSegment.set_DataElementValue(11, 0, ":"); 		// Repetition Separator (I65) 
        //                oSegment.set_DataElementValue(12, 0, "00501"); 		// Interchange Control Version Number (I11) 
        //                oSegment.set_DataElementValue(13, 0, InterchangeHeader); 		// Interchange Control Number (I12) 
        //                oSegment.set_DataElementValue(14, 0, "0"); 		// Acknowledgment Requested (I13) 
        //                oSegment.set_DataElementValue(15, 0, oTriarqClaim.RequestHeader.TypeOfData); 		// Usage Indicator (I14) 
        //                oSegment.set_DataElementValue(16, 0, "!"); 		// Component Element Separator (I15) 

        //                #endregion

        //                ediGroup.Set(ref oGroup, oInterchange.CreateGroup("005010X212"));
        //                ediDataSegment.Set(ref oSegment, oGroup.GetDataSegmentHeader());
        //                #region "GS"
        //                string GS_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
        //                string GS_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));

        //                oSegment.set_DataElementValue(1, 0, "HR"); 		// Functional Identifier Code (479) 
        //                oSegment.set_DataElementValue(2, 0, oTriarqClaim.RequestHeader.SenderCode); 		// Application Sender's Code (142) 
        //                oSegment.set_DataElementValue(3, 0, oTriarqClaim.RequestHeader.VenderIDCode); 		// Application Receiver's Code (124) 
        //                oSegment.set_DataElementValue(4, 0, GS_Date); 		// Date (373) 
        //                oSegment.set_DataElementValue(5, 0, GS_Time); 		// Time (337) 
        //                oSegment.set_DataElementValue(6, 0, "1"); 		// Group Control Number (28) 
        //                oSegment.set_DataElementValue(7, 0, "X"); 		// Responsible Agency Code (455) 
        //                oSegment.set_DataElementValue(8, 0, "005010X212"); 		// Version / Release / Industry Identifier Code (480) 
        //                #endregion

        //                // Creates the Transaction Set header segment (ST)
        //                ediTransactionSet.Set(ref oTransactionSet, oGroup.CreateTransactionSet("276"));
        //                ediDataSegment.Set(ref oSegment, oTransactionSet.GetDataSegmentHeader());
        //                #region "ST"
        //                oSegment.set_DataElementValue(1, 0, "276"); 		// Transaction Set Identifier Code (143) 
        //                oSegment.set_DataElementValue(2, 0, "0001"); 		// Transaction Set Control Number (329) 
        //                oSegment.set_DataElementValue(3, 0, "005010X212"); 		// Implementation Convention Reference (1705) 
        //                #endregion

        //                //BHT - Beginning of Hierarchical Transaction 
        //                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment("BHT"));
        //                #region "BHT"
        //                string BHT_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
        //                string BHT_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
        //                InterchangeHeader = clsGeneral.ControlNumberGeneration();
        //                oSegment.set_DataElementValue(1, 0, "0010"); 		// Hierarchical Structure Code (1005) 
        //                oSegment.set_DataElementValue(2, 0, "13"); 		// Transaction Set Purpose Code (353) 
        //                oSegment.set_DataElementValue(3, 0, InterchangeHeader); 		// Reference Identification (127) 
        //                oSegment.set_DataElementValue(4, 0, BHT_Date); 		// Date (373) 
        //                oSegment.set_DataElementValue(5, 0, BHT_Time); 		// Time (337) 
        //                #endregion
        //                int nInfoSources = 1;
        //                int nInfoSourceCtr = 1;

        //                while (nInfoSourceCtr <= nInfoSources)    //2000A
        //                {
        //                    nHlCounter = nHlCounter + 1;       //increment HL loop
        //                    nHlInfoReceiverParent = nHlCounter;   //The value of this HL counter is the HL parent for the HL subscriber loop

        //                    //HL - Information Source Level
        //                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
        //                    #region "HL 2000A SOURCE"
        //                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
        //                    oSegment.set_DataElementValue(3, 0, "20"); 		// Hierarchical Level Code (735) 
        //                    oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
        //                    #endregion

        //                    //2100A PAYER NAME
        //                    //NM1 - Payer Name
        //                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
        //                    #region "NM 2100A SOURCE"
        //                    oSegment.set_DataElementValue(1, 0, "PR"); 		// Entity Identifier Code (98) 
        //                    oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
        //                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.InsuranceName); 		// Name Last or Organization Name (1035) 
        //                    oSegment.set_DataElementValue(8, 0, "PI"); 		// Identification Code Qualifier (66) 
        //                    oSegment.set_DataElementValue(9, 0, oTriarqClaim.ResponsibleParty.PayerID); 		// Identification Code (67) 
        //                    #endregion
        //                    int nInfoReceivers = 1;
        //                    int nInfoReceiverCtr = 1;
        //                    // 2000B INFORMATION RECEIVER LEVEL
        //                    while (nInfoReceiverCtr <= nInfoReceivers)    //2000B
        //                    {
        //                        nHlCounter = nHlCounter + 1;
        //                        nHlServiceProviderParent = nHlCounter;

        //                        //HL - Information Receiver Level
        //                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
        //                        #region "HL 2000B RECEIVER"
        //                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
        //                        oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString()); 		// Hierarchical Parent ID Number (734) 
        //                        oSegment.set_DataElementValue(3, 0, "21"); 		// Hierarchical Level Code (735) 
        //                        oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
        //                        #endregion
        //                        //2100B INFORMATION RECEIVER NAME
        //                        //NM1 - Information Receiver Name
        //                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
        //                        #region "NM 2100B RECEIVER"
        //                        oSegment.set_DataElementValue(1, 0, "41"); 		// Entity Identifier Code (98) 
        //                        oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
        //                        oSegment.set_DataElementValue(3, 0, oTriarqClaim.ClaimClinic.Name); 		// Name Last or Organization Name (1035) 
        //                        oSegment.set_DataElementValue(4, 0, ""); 		// Name First (1036) 
        //                        oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
        //                        oSegment.set_DataElementValue(8, 0, "46"); 		// Identification Code Qualifier (66) 
        //                        oSegment.set_DataElementValue(9, 0, oTriarqClaim.ClaimClinic.TIN); 		// Identification Code (67) 
        //                        #endregion

        //                        int nServiceProviders = 1;
        //                        int nServiceProviderCtr = 1;
        //                        //2000C SERVICE PROVIDER LEVEL
        //                        while (nServiceProviderCtr <= nServiceProviders)    //2000C
        //                        {
        //                            nHlCounter = nHlCounter + 1;
        //                            nHlSubscriberParent = nHlCounter;

        //                            //HL - Service Provider Level
        //                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
        //                            #region "HL 2000C BILLING PROVIDER"
        //                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
        //                            oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString()); 		// Hierarchical Parent ID Number (734) 
        //                            oSegment.set_DataElementValue(3, 0, "19"); 		// Hierarchical Level Code (735) 
        //                            oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
        //                            #endregion

        //                            //2100C PROVIDER NAME
        //                            //NM1 - Provider Name
        //                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
        //                            #region "NM 2100C BILLING PROVIDER"
        //                            oSegment.set_DataElementValue(1, 0, "1P"); 		// Entity Identifier Code (98) 
        //                            oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
        //                            oSegment.set_DataElementValue(3, 0, oTriarqClaim.BillingProvider.LastName); 		// Name Last or Organization Name (1035) 
        //                            oSegment.set_DataElementValue(4, 0, oTriarqClaim.BillingProvider.FirstName); 		// Name First (1036) 
        //                            oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
        //                            oSegment.set_DataElementValue(7, 0, ""); 		// Name Suffix (1039) 
        //                            oSegment.set_DataElementValue(8, 0, "XX"); 		// Identification Code Qualifier (66) 
        //                            oSegment.set_DataElementValue(9, 0, oTriarqClaim.BillingProvider.NPI); 		// Identification Code (67) 
        //                            #endregion

        //                            int nSubscribers = 1;
        //                            int nSubscriberCtr = 1;

        //                            //2000D SUBSCRIBER LEVEL
        //                            while (nSubscriberCtr <= nSubscribers)    //2000D
        //                            {
        //                                nHlCounter = nHlCounter + 1;
        //                                nHlDependentParent = nHlCounter;

        //                                //HL - Subscriber Level
        //                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
        //                                #region "HL 2000D 2000D SUBSCRIBER
        //                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
        //                                oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString()); 		// Hierarchical Parent ID Number (734) 
        //                                oSegment.set_DataElementValue(3, 0, "22"); 		// Hierarchical Level Code (735) 
        //                                if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) == "18")
        //                                {
        //                                    oSegment.set_DataElementValue(4, 0, "0"); 		// Hierarchical Child Code (736) 
        //                                }
        //                                else
        //                                {
        //                                    oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
        //                                }
        //                                #endregion

        //                                //DMG - Subscriber Demographic Information 
        //                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
        //                                #region "DMG 2000D SUBSCRIBER"
        //                                string DMG_DOB_D = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.ResponsibleParty.SubscriberDOB.ToString()));
        //                                oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
        //                                oSegment.set_DataElementValue(2, 0, DMG_DOB_D); 		// Date Time Period (1251) 
        //                                switch (oTriarqClaim.ResponsibleParty.SubscriberGender)// Gender Code (1068) 
        //                                {
        //                                    case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
        //                                    case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
        //                                    case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
        //                                    case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
        //                                }
        //                                #endregion

        //                                //2100D SUBSCRIBER NAME
        //                                if (true)
        //                                {
        //                                    // Subscriber Name
        //                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
        //                                    #region "NM 2100D SUBSCRIBER"
        //                                    oSegment.set_DataElementValue(1, 0, "IL"); 		// Entity Identifier Code (98) 
        //                                    oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
        //                                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.SubscriberLName); 		// Name Last or Organization Name (1035) 
        //                                    oSegment.set_DataElementValue(4, 0, oTriarqClaim.ResponsibleParty.SubscriberFName); 		// Name First (1036) 
        //                                    oSegment.set_DataElementValue(5, 0, oTriarqClaim.ResponsibleParty.SubscriberMName); 		// Name Middle (1037) 
        //                                    oSegment.set_DataElementValue(7, 0, oTriarqClaim.ResponsibleParty.SubscriberSuffix); 		// Name Suffix (1039) 
        //                                    oSegment.set_DataElementValue(8, 0, "MI"); 		// Identification Code Qualifier (66) 
        //                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(oTriarqClaim.ResponsibleParty.InsuranceID)); 		// Identification Code (67) 
        //                                    #endregion

        //                                }//2100D 

        //                                Gen_ClaimStatus(oTransactionSet, oTriarqClaim);
        //                                if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) != "18")
        //                                {
        //                                    int nDependents = 1;
        //                                    int nDependentCtr = 1;
        //                                    //2000E DEPENDENT LEVEL
        //                                    while (nDependentCtr <= nDependents)    //2000E
        //                                    {
        //                                        nHlCounter = nHlCounter + 1;

        //                                        //HL - Dependent Level
        //                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
        //                                        #region HL 2000E DEPENDENT
        //                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
        //                                        oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString()); 		// Hierarchical Parent ID Number (734) 
        //                                        oSegment.set_DataElementValue(3, 0, "23"); 		// Hierarchical Level Code (735) 
        //                                        #endregion

        //                                        //DMG - Dependent Demographic Information
        //                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
        //                                        #region DMG 2000E DEPENDENT
        //                                        string DMG_DOB_E = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.Patient.DOB.ToString()));
        //                                        oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
        //                                        oSegment.set_DataElementValue(2, 0, DMG_DOB_E); 		// Date Time Period (1251) 
        //                                        switch (oTriarqClaim.Patient.Gender)// Gender Code (1068) 
        //                                        {
        //                                            case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
        //                                            case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
        //                                            case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
        //                                            case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
        //                                        }
        //                                        #endregion

        //                                        //2100E DEPENDENT NAME
        //                                        //NM1 - Dependent Name
        //                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
        //                                        #region NM 2100E DEPENDENT
        //                                        oSegment.set_DataElementValue(1, 0, "QC"); 		// Entity Identifier Code (98) 
        //                                        oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
        //                                        oSegment.set_DataElementValue(3, 0, oTriarqClaim.Patient.LastName); 		// Name Last or Organization Name (1035) 
        //                                        oSegment.set_DataElementValue(4, 0, oTriarqClaim.Patient.FirstName); 		// Name First (1036) 
        //                                        oSegment.set_DataElementValue(5, 0, oTriarqClaim.Patient.MiddleName); 		// Name Middle (1037) 
        //                                        oSegment.set_DataElementValue(7, 0, oTriarqClaim.Patient.Suffix); 		// Name Suffix (1039) 
        //                                        #endregion

        //                                        //2200E CLAIM STATUS TRACKING NUMBER
        //                                        Gen_ClaimStatus(oTransactionSet, oTriarqClaim);

        //                                        nDependentCtr++;
        //                                    }//2000E
        //                                }
        //                                nSubscriberCtr++;

        //                            }//2000D

        //                            nServiceProviderCtr++;

        //                        }//2000C

        //                        nInfoReceiverCtr++;
        //                    }//2000B

        //                    nInfoSourceCtr++;
        //                }//2000A
        //                string strEDI276 = oEdiDoc.GetEdiString();
        //                oEdiDoc.Save(sEdiFileName, 0);

        //                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
        //                //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.ReadyToUpload);
        //            }
        //            else
        //            {
        //                sEdiFileName = "";
        //                //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.Failed);
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            sEdiFileName = "";
        //            //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.Failed);
        //            //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "Exception while EDI 276 File generation.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
        //            clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
        //        }
        //        finally
        //        {
        //            if (oSegment != null)
        //            {
        //                oSegment.Dispose();
        //            }
        //            if (oTransactionSet != null)
        //            {
        //                oTransactionSet.Dispose();
        //            }
        //            if (oGroup != null)
        //            {
        //                oGroup.Dispose();
        //            }
        //            if (oInterchange != null)
        //            {
        //                oInterchange.Dispose();
        //            }
        //            if (oSchemas != null)
        //            {
        //                oSchemas.Dispose();
        //            }
        //            if (oEdiDoc != null)
        //            {
        //                oEdiDoc.Dispose();
        //            }
        //            if (sEdiFileName != "")
        //            {
        //                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation.", ActivityOutCome.Success, "", ActivityReference.EDI276File, sEdiFileName);
        //            }
        //            Parse276(sEdiFileName);
        //        }
        //    }
        //    return sEdiFileName;

        //}

        public Tuple<string, string> Generate276RequestString(TRIARQClaim oTriarqClaim)
        {
            Tuple<string, string> ClaimStatusRequest = null;

            string InterchangeHeader = "";
            bool isAllDataAvilable = true;
            string sEdiFileName = "";
            string sEdiDocString = "";
            string AusId = "";
            if (oTriarqClaim != null)
            {

                if (oTriarqClaim.ClaimClinic != null)
                {
                    AusId = oTriarqClaim.ClaimClinic.AUSID;
                }
                sEdiFileName = TempFolderpath + "\\TRIARQ_276_ClaimNumber_" + oTriarqClaim.FormattedClaimNumber + "_" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".txt.bci";
                seffilepath_276_005010X214_SemRef = sPath + "\\SEF\\276_005010X212.SemRef.SEF";
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation starts.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.RequestFileGeneration);

                try
                {

                    if (oTriarqClaim.ResponsibleParty == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Responsible party information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.RequestHeader == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to claim Header information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.ClaimServiceLines == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Service lines information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.ClaimFacility == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim facility information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.ClaimClinic == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim Clinic information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.BillingProvider == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Billing Provider information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.Patient == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim Patient information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }

                    if (isAllDataAvilable == true)
                    {
                        LoadEdiSchema();

                        int nHlCounter = 0;
                        int nHlInfoReceiverParent = 0;
                        int nHlServiceProviderParent = 0;
                        int nHlSubscriberParent = 0;
                        int nHlDependentParent = 0;

                        ediInterchange.Set(ref oInterchange, oEdiDoc.CreateInterchange("X", "005010"));
                        ediDataSegment.Set(ref oSegment, oInterchange.GetDataSegmentHeader());
                        #region "ISA"

                        string ISA_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                        string ISA_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        InterchangeHeader = clsGeneral.ControlNumberGeneration();
                        oSegment.set_DataElementValue(1, 0, "00"); 		// Authorization Information Qualifier (I01) 
                        oSegment.set_DataElementValue(2, 0, "          "); 		// Authorization Information (I02) 
                        oSegment.set_DataElementValue(3, 0, "00"); 		// Security Information Qualifier (I03) 
                        oSegment.set_DataElementValue(4, 0, "          "); 		// Security Information (I04) 
                        oSegment.set_DataElementValue(5, 0, oTriarqClaim.RequestHeader.SenderQualifier); 		// Interchange ID Qualifier (I05) 
                        oSegment.set_DataElementValue(6, 0, Convert.ToString(oTriarqClaim.RequestHeader.SubmitterID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Sender ID (I06) 
                        oSegment.set_DataElementValue(7, 0, oTriarqClaim.RequestHeader.ReceiverQualifier); 		// Interchange ID Qualifier (I05) 
                        oSegment.set_DataElementValue(8, 0, Convert.ToString(oTriarqClaim.RequestHeader.ReceiverID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Receiver ID (I07) 
                        oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2)); 		// Interchange Date (I08) 
                        oSegment.set_DataElementValue(10, 0, clsGeneral.FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Time (I09) 
                        oSegment.set_DataElementValue(11, 0, ":"); 		// Repetition Separator (I65) 
                        oSegment.set_DataElementValue(12, 0, "00501"); 		// Interchange Control Version Number (I11) 
                        oSegment.set_DataElementValue(13, 0, InterchangeHeader); 		// Interchange Control Number (I12) 
                        oSegment.set_DataElementValue(14, 0, "0"); 		// Acknowledgment Requested (I13) 
                        oSegment.set_DataElementValue(15, 0, oTriarqClaim.RequestHeader.TypeOfData); 		// Usage Indicator (I14) 
                        oSegment.set_DataElementValue(16, 0, "!"); 		// Component Element Separator (I15) 

                        #endregion

                        ediGroup.Set(ref oGroup, oInterchange.CreateGroup("005010X212"));
                        ediDataSegment.Set(ref oSegment, oGroup.GetDataSegmentHeader());
                        #region "GS"
                        string GS_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                        string GS_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));

                        oSegment.set_DataElementValue(1, 0, "HR"); 		// Functional Identifier Code (479) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.RequestHeader.SenderCode); 		// Application Sender's Code (142) 
                        oSegment.set_DataElementValue(3, 0, oTriarqClaim.RequestHeader.VenderIDCode); 		// Application Receiver's Code (124) 
                        oSegment.set_DataElementValue(4, 0, GS_Date); 		// Date (373) 
                        oSegment.set_DataElementValue(5, 0, GS_Time); 		// Time (337) 
                        oSegment.set_DataElementValue(6, 0, "1"); 		// Group Control Number (28) 
                        oSegment.set_DataElementValue(7, 0, "X"); 		// Responsible Agency Code (455) 
                        oSegment.set_DataElementValue(8, 0, "005010X212"); 		// Version / Release / Industry Identifier Code (480) 
                        #endregion

                        // Creates the Transaction Set header segment (ST)
                        ediTransactionSet.Set(ref oTransactionSet, oGroup.CreateTransactionSet("276"));
                        ediDataSegment.Set(ref oSegment, oTransactionSet.GetDataSegmentHeader());
                        #region "ST"
                        oSegment.set_DataElementValue(1, 0, "276"); 		// Transaction Set Identifier Code (143) 
                        oSegment.set_DataElementValue(2, 0, "0001"); 		// Transaction Set Control Number (329) 
                        oSegment.set_DataElementValue(3, 0, "005010X212"); 		// Implementation Convention Reference (1705) 
                        #endregion

                        //BHT - Beginning of Hierarchical Transaction 
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment("BHT"));
                        #region "BHT"
                        string BHT_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                        string BHT_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        InterchangeHeader = clsGeneral.ControlNumberGeneration();
                        oSegment.set_DataElementValue(1, 0, "0010"); 		// Hierarchical Structure Code (1005) 
                        oSegment.set_DataElementValue(2, 0, "13"); 		// Transaction Set Purpose Code (353) 
                        oSegment.set_DataElementValue(3, 0, InterchangeHeader); 		// Reference Identification (127) 
                        oSegment.set_DataElementValue(4, 0, BHT_Date); 		// Date (373) 
                        oSegment.set_DataElementValue(5, 0, BHT_Time); 		// Time (337) 
                        #endregion
                        int nInfoSources = 1;
                        int nInfoSourceCtr = 1;

                        while (nInfoSourceCtr <= nInfoSources)    //2000A
                        {
                            nHlCounter = nHlCounter + 1;       //increment HL loop
                            nHlInfoReceiverParent = nHlCounter;   //The value of this HL counter is the HL parent for the HL subscriber loop

                            //HL - Information Source Level
                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                            #region "HL 2000A SOURCE"
                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                            oSegment.set_DataElementValue(3, 0, "20"); 		// Hierarchical Level Code (735) 
                            oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                            #endregion

                            //2100A PAYER NAME
                            //NM1 - Payer Name
                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                            #region "NM 2100A SOURCE"
                            oSegment.set_DataElementValue(1, 0, "PR"); 		// Entity Identifier Code (98) 
                            oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
                            oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.InsuranceName); 		// Name Last or Organization Name (1035) 
                            oSegment.set_DataElementValue(8, 0, "PI"); 		// Identification Code Qualifier (66) 
                            oSegment.set_DataElementValue(9, 0, oTriarqClaim.ResponsibleParty.PayerID); 		// Identification Code (67) 
                            #endregion
                            int nInfoReceivers = 1;
                            int nInfoReceiverCtr = 1;
                            // 2000B INFORMATION RECEIVER LEVEL
                            while (nInfoReceiverCtr <= nInfoReceivers)    //2000B
                            {
                                nHlCounter = nHlCounter + 1;
                                nHlServiceProviderParent = nHlCounter;

                                //HL - Information Receiver Level
                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                #region "HL 2000B RECEIVER"
                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                oSegment.set_DataElementValue(3, 0, "21"); 		// Hierarchical Level Code (735) 
                                oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                #endregion
                                //2100B INFORMATION RECEIVER NAME
                                //NM1 - Information Receiver Name
                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                #region "NM 2100B RECEIVER"
                                oSegment.set_DataElementValue(1, 0, "41"); 		// Entity Identifier Code (98) 
                                oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
                                oSegment.set_DataElementValue(3, 0, oTriarqClaim.ClaimClinic.Name); 		// Name Last or Organization Name (1035) 
                                oSegment.set_DataElementValue(4, 0, ""); 		// Name First (1036) 
                                oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
                                oSegment.set_DataElementValue(8, 0, "46"); 		// Identification Code Qualifier (66) 
                                oSegment.set_DataElementValue(9, 0, oTriarqClaim.ClaimClinic.TIN); 		// Identification Code (67) 
                                #endregion

                                int nServiceProviders = 1;
                                int nServiceProviderCtr = 1;
                                //2000C SERVICE PROVIDER LEVEL
                                while (nServiceProviderCtr <= nServiceProviders)    //2000C
                                {
                                    nHlCounter = nHlCounter + 1;
                                    nHlSubscriberParent = nHlCounter;

                                    //HL - Service Provider Level
                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                    #region "HL 2000C BILLING PROVIDER"
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                    oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                    oSegment.set_DataElementValue(3, 0, "19"); 		// Hierarchical Level Code (735) 
                                    oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                    #endregion

                                    //2100C PROVIDER NAME
                                    //NM1 - Provider Name
                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                    #region "NM 2100C BILLING PROVIDER"
                                    oSegment.set_DataElementValue(1, 0, "1P"); 		// Entity Identifier Code (98) 
                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.BillingProvider.EntityType)); 		// Entity Type Qualifier (1065) 
                                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.BillingProvider.LastName); 		// Name Last or Organization Name (1035) 
                                    oSegment.set_DataElementValue(4, 0, oTriarqClaim.BillingProvider.FirstName); 		// Name First (1036) 
                                    oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
                                    oSegment.set_DataElementValue(7, 0, ""); 		// Name Suffix (1039) 
                                    oSegment.set_DataElementValue(8, 0, "XX"); 		// Identification Code Qualifier (66) 
                                    oSegment.set_DataElementValue(9, 0, oTriarqClaim.BillingProvider.NPI); 		// Identification Code (67) 
                                    #endregion

                                    int nSubscribers = 1;
                                    int nSubscriberCtr = 1;

                                    //2000D SUBSCRIBER LEVEL
                                    while (nSubscriberCtr <= nSubscribers)    //2000D
                                    {
                                        nHlCounter = nHlCounter + 1;
                                        nHlDependentParent = nHlCounter;

                                        //HL - Subscriber Level
                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                        #region "HL 2000D SUBSCRIBER
                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                        oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                        oSegment.set_DataElementValue(3, 0, "22"); 		// Hierarchical Level Code (735) 
                                        if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) == "18")
                                        {
                                            oSegment.set_DataElementValue(4, 0, "0"); 		// Hierarchical Child Code (736) 
                                        }
                                        else
                                        {
                                            oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                        }
                                        #endregion

                                        //DMG - Subscriber Demographic Information 
                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
                                        #region "DMG 2000D SUBSCRIBER"
                                        string DMG_DOB = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.ResponsibleParty.SubscriberDOB.ToString()));
                                        oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                                        oSegment.set_DataElementValue(2, 0, DMG_DOB); 		// Date Time Period (1251) 
                                        switch (oTriarqClaim.ResponsibleParty.SubscriberGender)// Gender Code (1068) 
                                        {
                                            case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
                                            case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
                                            case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
                                            case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
                                        }
                                        #endregion

                                        //2100D SUBSCRIBER NAME
                                        if (true)
                                        {
                                            // Subscriber Name
                                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                            #region "NM 2100D SUBSCRIBER"
                                            oSegment.set_DataElementValue(1, 0, "IL"); 		// Entity Identifier Code (98) 
                                            oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
                                            oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.SubscriberLName); 		// Name Last or Organization Name (1035) 
                                            oSegment.set_DataElementValue(4, 0, oTriarqClaim.ResponsibleParty.SubscriberFName); 		// Name First (1036) 
                                            oSegment.set_DataElementValue(5, 0, oTriarqClaim.ResponsibleParty.SubscriberMName); 		// Name Middle (1037) 
                                            oSegment.set_DataElementValue(7, 0, oTriarqClaim.ResponsibleParty.SubscriberSuffix); 		// Name Suffix (1039) 
                                            oSegment.set_DataElementValue(8, 0, "MI"); 		// Identification Code Qualifier (66) 
                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(oTriarqClaim.ResponsibleParty.InsuranceID)); 		// Identification Code (67) 
                                            #endregion

                                        }//2100D 

                                        Gen_ClaimStatus(oTransactionSet, oTriarqClaim);
                                        if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) != "18")
                                        {
                                            int nDependents = 1;
                                            int nDependentCtr = 1;
                                            //2000E DEPENDENT LEVEL
                                            while (nDependentCtr <= nDependents)    //2000E
                                            {
                                                nHlCounter = nHlCounter + 1;

                                                //HL - Dependent Level
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                                #region HL 2000E DEPENDENT
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                                oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                                oSegment.set_DataElementValue(3, 0, "23"); 		// Hierarchical Level Code (735) 
                                                #endregion

                                                //DMG - Dependent Demographic Information
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
                                                #region DMG 2000E DEPENDENT
                                                string DMG_DOB_E = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.Patient.DOB.ToString()));
                                                oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                                                oSegment.set_DataElementValue(2, 0, DMG_DOB_E); 		// Date Time Period (1251) 
                                                switch (oTriarqClaim.Patient.Gender)// Gender Code (1068) 
                                                {
                                                    case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
                                                    case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
                                                    case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
                                                    case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
                                                }
                                                #endregion

                                                //2100E DEPENDENT NAME
                                                //NM1 - Dependent Name
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                                #region NM 2100E DEPENDENT
                                                oSegment.set_DataElementValue(1, 0, "QC"); 		// Entity Identifier Code (98) 
                                                oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
                                                oSegment.set_DataElementValue(3, 0, oTriarqClaim.Patient.LastName); 		// Name Last or Organization Name (1035) 
                                                oSegment.set_DataElementValue(4, 0, oTriarqClaim.Patient.FirstName); 		// Name First (1036) 
                                                oSegment.set_DataElementValue(5, 0, oTriarqClaim.Patient.MiddleName); 		// Name Middle (1037) 
                                                oSegment.set_DataElementValue(7, 0, oTriarqClaim.Patient.Suffix); 		// Name Suffix (1039) 
                                                #endregion

                                                //2200E CLAIM STATUS TRACKING NUMBER
                                                Gen_ClaimStatus(oTransactionSet, oTriarqClaim);

                                                nDependentCtr++;
                                            }//2000E
                                        }
                                        nSubscriberCtr++;

                                    }//2000D

                                    nServiceProviderCtr++;

                                }//2000C

                                nInfoReceiverCtr++;
                            }//2000B

                            nInfoSourceCtr++;
                        }//2000A
                        sEdiDocString = oEdiDoc.GetEdiString();
                        oEdiDoc.Save(sEdiFileName, 0);

                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
                        //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.ReadyToUpload);
                    }
                    else
                    {
                        sEdiDocString = "";
                        sEdiFileName = "";
                        //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.Failed);
                    }

                }
                catch (Exception ex)
                {
                    sEdiDocString = "";
                    sEdiFileName = "";
                    //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.Failed);
                    //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "Exception while EDI 276 File generation.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                    clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
                }
                finally
                {
                    if (oSegment != null)
                    {
                        oSegment.Dispose();
                    }
                    if (oTransactionSet != null)
                    {
                        oTransactionSet.Dispose();
                    }
                    if (oGroup != null)
                    {
                        oGroup.Dispose();
                    }
                    if (oInterchange != null)
                    {
                        oInterchange.Dispose();
                    }
                    if (oSchemas != null)
                    {
                        oSchemas.Dispose();
                    }
                    if (oEdiDoc != null)
                    {
                        oEdiDoc.Dispose();
                    }
                    if (sEdiFileName != "")
                    {
                        // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation.", ActivityOutCome.Success, "", ActivityReference.EDI276File, sEdiFileName);
                    }
                    Parse276(sEdiFileName);
                }
            }

            ClaimStatusRequest = new Tuple<string, string>(sEdiFileName, sEdiDocString);
            return ClaimStatusRequest;
        }

        //For QPM Integration : START
        public Tuple<string, string, long> Generate276RealTimeCSIString(TRIARQClaim oTriarqClaim, long CSIRequestID)
        {
            Tuple<string, string, long> ClaimStatusRequest = null;

            string InterchangeHeader = "";
            bool isAllDataAvilable = true;
            string sEdiFileName = "";
            string sEdiDocString = "";
            string AusId = "";

            if (oTriarqClaim != null)
            {

                if (oTriarqClaim.ClaimClinic != null)
                {
                    AusId = oTriarqClaim.ClaimClinic.AUSID;
                }

                //string path = gloSettings.FolderSettings.AppTempFolderPath + "EDIFiles";
                if (!Directory.Exists(TempFolderpath))
                {
                    Directory.CreateDirectory(TempFolderpath);
                }

                sEdiFileName = TempFolderpath + "\\TRIARQ_276_ClaimNumber_" + oTriarqClaim.FormattedClaimNumber + "_" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".txt.bci";
                seffilepath_276_005010X214_SemRef = sPath + "\\SEF\\276_005010X212.SemRef.SEF";
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation starts.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.RequestFileGeneration);

                try
                {

                    if (oTriarqClaim.ResponsibleParty == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Responsible party information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.RequestHeader == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to claim Header information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.ClaimServiceLines == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Service lines information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.ClaimFacility == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim facility information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.ClaimClinic == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim Clinic information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.BillingProvider == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Billing Provider information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }
                    if (oTriarqClaim.Patient == null)
                    {
                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation halted due to Claim Patient information is not present.", ActivityOutCome.Stopped, "", ActivityReference.ClaimNumber, ClaimNumber);
                        isAllDataAvilable = false;
                    }

                    if (isAllDataAvilable == true)
                    {
                        LoadEdiSchema();

                        int nHlCounter = 0;
                        int nHlInfoReceiverParent = 0;
                        int nHlServiceProviderParent = 0;
                        int nHlSubscriberParent = 0;
                        int nHlDependentParent = 0;

                        ediInterchange.Set(ref oInterchange, oEdiDoc.CreateInterchange("X", "005010"));
                        ediDataSegment.Set(ref oSegment, oInterchange.GetDataSegmentHeader());
                        #region "ISA"

                        string ISA_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                        string ISA_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        InterchangeHeader = clsGeneral.ControlNumberGeneration();
                        oSegment.set_DataElementValue(1, 0, "00"); 		// Authorization Information Qualifier (I01) 
                        oSegment.set_DataElementValue(2, 0, "          "); 		// Authorization Information (I02) 
                        oSegment.set_DataElementValue(3, 0, "00"); 		// Security Information Qualifier (I03) 
                        oSegment.set_DataElementValue(4, 0, "          "); 		// Security Information (I04) 
                        oSegment.set_DataElementValue(5, 0, oTriarqClaim.RequestHeader.SenderQualifier); 		// Interchange ID Qualifier (I05) 
                        oSegment.set_DataElementValue(6, 0, Convert.ToString(oTriarqClaim.RequestHeader.SubmitterID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Sender ID (I06) 
                        oSegment.set_DataElementValue(7, 0, oTriarqClaim.RequestHeader.ReceiverQualifier); 		// Interchange ID Qualifier (I05) 
                        oSegment.set_DataElementValue(8, 0, Convert.ToString(oTriarqClaim.RequestHeader.ReceiverID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Receiver ID (I07) 
                        oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2)); 		// Interchange Date (I08) 
                        oSegment.set_DataElementValue(10, 0, clsGeneral.FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Time (I09) 
                        oSegment.set_DataElementValue(11, 0, ":"); 		// Repetition Separator (I65) 
                        oSegment.set_DataElementValue(12, 0, "00501"); 		// Interchange Control Version Number (I11) 
                        oSegment.set_DataElementValue(13, 0, InterchangeHeader); 		// Interchange Control Number (I12) 
                        oSegment.set_DataElementValue(14, 0, "0"); 		// Acknowledgment Requested (I13) 
                        oSegment.set_DataElementValue(15, 0, oTriarqClaim.RequestHeader.TypeOfData); 		// Usage Indicator (I14) 
                        oSegment.set_DataElementValue(16, 0, "!"); 		// Component Element Separator (I15) 

                        #endregion

                        ediGroup.Set(ref oGroup, oInterchange.CreateGroup("005010X212"));
                        ediDataSegment.Set(ref oSegment, oGroup.GetDataSegmentHeader());
                        #region "GS"
                        string GS_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                        string GS_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));

                        oSegment.set_DataElementValue(1, 0, "HR"); 		// Functional Identifier Code (479) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.RequestHeader.SenderCode); 		// Application Sender's Code (142) 
                        oSegment.set_DataElementValue(3, 0, oTriarqClaim.RequestHeader.VenderIDCode); 		// Application Receiver's Code (124) 
                        oSegment.set_DataElementValue(4, 0, GS_Date); 		// Date (373) 
                        oSegment.set_DataElementValue(5, 0, GS_Time); 		// Time (337) 
                        oSegment.set_DataElementValue(6, 0, "1"); 		// Group Control Number (28) 
                        oSegment.set_DataElementValue(7, 0, "X"); 		// Responsible Agency Code (455) 
                        oSegment.set_DataElementValue(8, 0, "005010X212"); 		// Version / Release / Industry Identifier Code (480) 
                        #endregion

                        // Creates the Transaction Set header segment (ST)
                        ediTransactionSet.Set(ref oTransactionSet, oGroup.CreateTransactionSet("276"));
                        ediDataSegment.Set(ref oSegment, oTransactionSet.GetDataSegmentHeader());
                        #region "ST"
                        oSegment.set_DataElementValue(1, 0, "276"); 		// Transaction Set Identifier Code (143) 
                        oSegment.set_DataElementValue(2, 0, "0001"); 		// Transaction Set Control Number (329) 
                        oSegment.set_DataElementValue(3, 0, "005010X212"); 		// Implementation Convention Reference (1705) 
                        #endregion

                        //BHT - Beginning of Hierarchical Transaction 
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment("BHT"));
                        #region "BHT"
                        string BHT_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                        string BHT_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                        InterchangeHeader = clsGeneral.ControlNumberGeneration();
                        oSegment.set_DataElementValue(1, 0, "0010"); 		// Hierarchical Structure Code (1005) 
                        oSegment.set_DataElementValue(2, 0, "13"); 		// Transaction Set Purpose Code (353) 
                        oSegment.set_DataElementValue(3, 0, InterchangeHeader); 		// Reference Identification (127) 
                        oSegment.set_DataElementValue(4, 0, BHT_Date); 		// Date (373) 
                        oSegment.set_DataElementValue(5, 0, BHT_Time); 		// Time (337) 
                        #endregion
                        int nInfoSources = 1;
                        int nInfoSourceCtr = 1;

                        while (nInfoSourceCtr <= nInfoSources)    //2000A
                        {
                            nHlCounter = nHlCounter + 1;       //increment HL loop
                            nHlInfoReceiverParent = nHlCounter;   //The value of this HL counter is the HL parent for the HL subscriber loop

                            //HL - Information Source Level
                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                            #region "HL 2000A SOURCE"
                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                            oSegment.set_DataElementValue(3, 0, "20"); 		// Hierarchical Level Code (735) 
                            oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                            #endregion

                            //2100A PAYER NAME
                            //NM1 - Payer Name
                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                            #region "NM 2100A SOURCE"
                            oSegment.set_DataElementValue(1, 0, "PR"); 		// Entity Identifier Code (98) 
                            oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
                            oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.InsuranceName); 		// Name Last or Organization Name (1035) 
                            oSegment.set_DataElementValue(8, 0, "PI"); 		// Identification Code Qualifier (66) 
                            oSegment.set_DataElementValue(9, 0, oTriarqClaim.ResponsibleParty.PayerID); 		// Identification Code (67) 
                            #endregion
                            int nInfoReceivers = 1;
                            int nInfoReceiverCtr = 1;
                            // 2000B INFORMATION RECEIVER LEVEL
                            while (nInfoReceiverCtr <= nInfoReceivers)    //2000B
                            {
                                nHlCounter = nHlCounter + 1;
                                nHlServiceProviderParent = nHlCounter;

                                //HL - Information Receiver Level
                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                #region "HL 2000B RECEIVER"
                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                oSegment.set_DataElementValue(3, 0, "21"); 		// Hierarchical Level Code (735) 
                                oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                #endregion
                                //2100B INFORMATION RECEIVER NAME
                                //NM1 - Information Receiver Name
                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                #region "NM 2100B RECEIVER"
                                oSegment.set_DataElementValue(1, 0, "41"); 		// Entity Identifier Code (98) 
                                oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
                                oSegment.set_DataElementValue(3, 0, oTriarqClaim.ClaimClinic.Name); 		// Name Last or Organization Name (1035) 
                                oSegment.set_DataElementValue(4, 0, ""); 		// Name First (1036) 
                                oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
                                oSegment.set_DataElementValue(8, 0, "46"); 		// Identification Code Qualifier (66) 
                                oSegment.set_DataElementValue(9, 0, oTriarqClaim.ClaimClinic.TIN); 		// Identification Code (67) 
                                #endregion

                                int nServiceProviders = 1;
                                int nServiceProviderCtr = 1;
                                //2000C SERVICE PROVIDER LEVEL
                                while (nServiceProviderCtr <= nServiceProviders)    //2000C
                                {
                                    nHlCounter = nHlCounter + 1;
                                    nHlSubscriberParent = nHlCounter;

                                    //HL - Service Provider Level
                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                    #region "HL 2000C BILLING PROVIDER"
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                    oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                    oSegment.set_DataElementValue(3, 0, "19"); 		// Hierarchical Level Code (735) 
                                    oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                    #endregion

                                    //2100C PROVIDER NAME
                                    //NM1 - Provider Name
                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                    #region "NM 2100C BILLING PROVIDER"
                                    oSegment.set_DataElementValue(1, 0, "1P"); 		// Entity Identifier Code (98) 
                                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.BillingProvider.EntityType)); 		// Entity Type Qualifier (1065) 
                                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.BillingProvider.LastName); 		// Name Last or Organization Name (1035) 
                                    oSegment.set_DataElementValue(4, 0, oTriarqClaim.BillingProvider.FirstName); 		// Name First (1036) 
                                    oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
                                    oSegment.set_DataElementValue(7, 0, ""); 		// Name Suffix (1039) 
                                    oSegment.set_DataElementValue(8, 0, "XX"); 		// Identification Code Qualifier (66) 
                                    oSegment.set_DataElementValue(9, 0, oTriarqClaim.BillingProvider.NPI); 		// Identification Code (67) 
                                    #endregion

                                    int nSubscribers = 1;
                                    int nSubscriberCtr = 1;

                                    //2000D SUBSCRIBER LEVEL
                                    while (nSubscriberCtr <= nSubscribers)    //2000D
                                    {
                                        nHlCounter = nHlCounter + 1;
                                        nHlDependentParent = nHlCounter;

                                        //HL - Subscriber Level
                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                        #region "HL 2000D SUBSCRIBER
                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                        oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                        oSegment.set_DataElementValue(3, 0, "22"); 		// Hierarchical Level Code (735) 
                                        if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) == "18")
                                        {
                                            oSegment.set_DataElementValue(4, 0, "0"); 		// Hierarchical Child Code (736) 
                                        }
                                        else
                                        {
                                            oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                        }
                                        #endregion

                                        //DMG - Subscriber Demographic Information 
                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
                                        #region "DMG 2000D SUBSCRIBER"
                                        string DMG_DOB = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.ResponsibleParty.SubscriberDOB.ToString()));
                                        oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                                        oSegment.set_DataElementValue(2, 0, DMG_DOB); 		// Date Time Period (1251) 
                                        switch (oTriarqClaim.ResponsibleParty.SubscriberGender)// Gender Code (1068) 
                                        {
                                            case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
                                            case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
                                            case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
                                            case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
                                        }
                                        #endregion

                                        //2100D SUBSCRIBER NAME
                                        if (true)
                                        {
                                            // Subscriber Name
                                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                            #region "NM 2100D SUBSCRIBER"
                                            oSegment.set_DataElementValue(1, 0, "IL"); 		// Entity Identifier Code (98) 
                                            oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
                                            oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.SubscriberLName); 		// Name Last or Organization Name (1035) 
                                            oSegment.set_DataElementValue(4, 0, oTriarqClaim.ResponsibleParty.SubscriberFName); 		// Name First (1036) 
                                            oSegment.set_DataElementValue(5, 0, oTriarqClaim.ResponsibleParty.SubscriberMName); 		// Name Middle (1037) 
                                            oSegment.set_DataElementValue(7, 0, oTriarqClaim.ResponsibleParty.SubscriberSuffix); 		// Name Suffix (1039) 
                                            oSegment.set_DataElementValue(8, 0, "MI"); 		// Identification Code Qualifier (66) 
                                            oSegment.set_DataElementValue(9, 0, Convert.ToString(oTriarqClaim.ResponsibleParty.InsuranceID)); 		// Identification Code (67) 
                                            #endregion

                                        }//2100D 

                                        Gen_ClaimStatus(oTransactionSet, oTriarqClaim);
                                        if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) != "18")
                                        {
                                            int nDependents = 1;
                                            int nDependentCtr = 1;
                                            //2000E DEPENDENT LEVEL
                                            while (nDependentCtr <= nDependents)    //2000E
                                            {
                                                nHlCounter = nHlCounter + 1;

                                                //HL - Dependent Level
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                                #region HL 2000E DEPENDENT
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                                oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                                oSegment.set_DataElementValue(3, 0, "23"); 		// Hierarchical Level Code (735) 
                                                #endregion

                                                //DMG - Dependent Demographic Information
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
                                                #region DMG 2000E DEPENDENT
                                                string DMG_DOB_E = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.Patient.DOB.ToString()));
                                                oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                                                oSegment.set_DataElementValue(2, 0, DMG_DOB_E); 		// Date Time Period (1251) 
                                                switch (oTriarqClaim.Patient.Gender)// Gender Code (1068) 
                                                {
                                                    case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
                                                    case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
                                                    case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
                                                    case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
                                                }
                                                #endregion

                                                //2100E DEPENDENT NAME
                                                //NM1 - Dependent Name
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                                #region NM 2100E DEPENDENT
                                                oSegment.set_DataElementValue(1, 0, "QC"); 		// Entity Identifier Code (98) 
                                                oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
                                                oSegment.set_DataElementValue(3, 0, oTriarqClaim.Patient.LastName); 		// Name Last or Organization Name (1035) 
                                                oSegment.set_DataElementValue(4, 0, oTriarqClaim.Patient.FirstName); 		// Name First (1036) 
                                                oSegment.set_DataElementValue(5, 0, oTriarqClaim.Patient.MiddleName); 		// Name Middle (1037) 
                                                oSegment.set_DataElementValue(7, 0, oTriarqClaim.Patient.Suffix); 		// Name Suffix (1039) 
                                                #endregion

                                                //2200E CLAIM STATUS TRACKING NUMBER
                                                Gen_ClaimStatus(oTransactionSet, oTriarqClaim);

                                                nDependentCtr++;
                                            }//2000E
                                        }
                                        nSubscriberCtr++;

                                    }//2000D

                                    nServiceProviderCtr++;

                                }//2000C

                                nInfoReceiverCtr++;
                            }//2000B

                            nInfoSourceCtr++;
                        }//2000A
                        sEdiDocString = oEdiDoc.GetEdiString();
                        oEdiDoc.Save(sEdiFileName, 0);

                        //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
                        //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.ReadyToUpload);
                    }
                    else
                    {
                        sEdiDocString = "";
                        sEdiFileName = "";
                        //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.Failed);
                    }

                }
                catch (Exception ex)
                {
                    sEdiDocString = "";
                    sEdiFileName = "";
                    //cls_EDIOperation_Queue.UpdateQueueStatus(oTriarqClaim.FormattedClaimNumber, AusId, QueueStatus.Failed);
                    //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "Exception while EDI 276 File generation.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                    clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
                }
                finally
                {
                    if (oSegment != null)
                    {
                        oSegment.Dispose();
                    }
                    if (oTransactionSet != null)
                    {
                        oTransactionSet.Dispose();
                    }
                    if (oGroup != null)
                    {
                        oGroup.Dispose();
                    }
                    if (oInterchange != null)
                    {
                        oInterchange.Dispose();
                    }
                    if (oSchemas != null)
                    {
                        oSchemas.Dispose();
                    }
                    if (oEdiDoc != null)
                    {
                        oEdiDoc.Dispose();
                    }
                    if (sEdiFileName != "")
                    {
                        // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, "EDI 276 File generation.", ActivityOutCome.Success, "", ActivityReference.EDI276File, sEdiFileName);
                    }
                    ERAFileID = SaveCSIRequestFile(CSIRequestID, sEdiDocString);
                    Parse276(sEdiFileName);
                }
            }

            ClaimStatusRequest = new Tuple<string, string, long>(sEdiFileName, sEdiDocString, ERAFileID);
            return ClaimStatusRequest;
        }

        private long SaveCSIRequestFile(long CSIRequestID, string CSIRequestString)
        {
            //INUP_CSI_RequestFile
            long CSIRequestFileId = 0;
            object _result = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIRequestFileId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestId", CSIRequestID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestFile", CSIRequestString, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestFileCreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_RequestFile", oDBPara, out _result);
                    if ((_result != null) && (Convert.ToString(_result) != ""))
                    {
                        CSIRequestFileId = Convert.ToInt64(_result);
                    }
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return CSIRequestFileId;
        }
        //For QPM Integration : END

        private void LoadEdiSchema()
        {
            try
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, "EDI 276 File Initialization.", ActivityOutCome.Started);
                oEdiDoc = new ediDocument();
                oSchemas = oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.SegmentTerminator = "~{13:10}";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = "!";
                oEdiDoc.LoadSchema(seffilepath_276_005010X214_SemRef, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, "EDI 276 File Initialization.", ActivityOutCome.Complete);
            }
            catch (Exception ex)
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, "Exception while EDI 276 File Initialization.", ActivityOutCome.Failure);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
            }
        }

        private void Gen_ClaimStatus(ediTransactionSet oTransactionSet, TRIARQClaim oTriarqClaim)
        {
            ediDataSegment oSegment = null;
            string InterchangeHeader = "";
            try
            {
                object objSettingValue = null;
                GetSetting("sClaimPrefix", Convert.ToString(oTriarqClaim.ClinicID), out objSettingValue);

                //2200D / 2200E CLAIM STATUS TRACKING NUMBER
                for (int nTrnCounter = 1; nTrnCounter <= 1; nTrnCounter++)
                {
                    //TRN - Claim Status Tracking Number
                    InterchangeHeader = clsGeneral.ControlNumberGeneration();
                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\TRN"));
                    oSegment.set_DataElementValue(1, 0, "1"); 		// Trace Type Code (481) 
                    oSegment.set_DataElementValue(2, 0, InterchangeHeader); 		// Reference Identification (127)

                    //REF - Payer Claim Control Number
                    if (oTriarqClaim.FormattedClaimNumber != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "1K"); 		// Reference Identification Qualifier (128) 
                        //oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127)  //Claim Number
                        if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                        {
                            oSegment.set_DataElementValue(2, 0, String.Concat(Convert.ToString(objSettingValue), oTriarqClaim.FormattedClaimNumber)); 		// Reference Identification (127) //Payers Claim Number
                        }
                        else
                        {
                            oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127) //Payers Claim Number
                        }
                    }
                    ////REF - Institutional Bill Type Identification
                    //if (oTriarqClaim.Type != "")
                    //{
                    //    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                    //    oSegment.set_DataElementValue(1, 0, "BLT"); 		// Reference Identification Qualifier (128) 
                    //    oSegment.set_DataElementValue(2, 0, oTriarqClaim.Type); 		// Reference Identification (127) 
                    //}

                    //REF - Application or Location System Identifier
                    if (oTriarqClaim.ClaimFacility.POSCode != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "LU"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.ClaimFacility.POSCode); 		// Reference Identification (127) 
                    }

                    //REF - Group Number
                    if (oTriarqClaim.ResponsibleParty.GroupNumber != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "6P"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.ResponsibleParty.GroupNumber); 		// Reference Identification (127) 
                    }

                    //REF - Patient Control Number
                    if (oTriarqClaim.Patient.Code != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "EJ"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.Patient.Code); 		// Reference Identification (127) 
                    }
                    ////REF - Pharmacy Prescription Number
                    //if (oTriarqClaim.Patient.PharmacyNumber != "")
                    //{
                    //    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                    //    oSegment.set_DataElementValue(1, 0, "XZ"); 		// Reference Identification Qualifier (128) 
                    //    oSegment.set_DataElementValue(2, 0, oTriarqClaim.Patient.PharmacyNumber); 		// Reference Identification (127) 
                    //}
                    //REF - Claim Identification Number For Clearinghouses and Other Transmission Intermediaries
                    if (oTriarqClaim.FormattedClaimNumber != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "D9"); 		// Reference Identification Qualifier (128) 
                        //oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127)  //Claim Number
                        if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                        {
                            oSegment.set_DataElementValue(2, 0, String.Concat(Convert.ToString(objSettingValue), oTriarqClaim.FormattedClaimNumber)); 		// Reference Identification (127) //Payers Claim Number
                        }
                        else
                        {
                            oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127) //Payers Claim Number
                        }
                    }
                    //AMT - Claim Submitted Charges
                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\AMT"));
                    oSegment.set_DataElementValue(1, 0, "T3"); 		// Amount Qualifier Code (522) 
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.TotalClaimAmount)); 		// Monetary Amount (782) 

                    //DTP - Claim Service Date
                    string TRN_DTP = Convert.ToString(clsGeneral.DateAsNumber(Convert.ToString(oTriarqClaim.TransactionDate)));
                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\DTP"));
                    oSegment.set_DataElementValue(1, 0, "472"); 		// Date/Time Qualifier (374) 
                    oSegment.set_DataElementValue(2, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                    oSegment.set_DataElementValue(3, 0, TRN_DTP); 		// Date Time Period (1251) 

                    //2200D / 2210E SERVICE LINE INFORMATION
                    for (int nSvcCounter = 1; nSvcCounter <= oTriarqClaim.ClaimServiceLines.Count; nSvcCounter++)
                    {
                        //SVC - Service Line Information
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\SVC\SVC"));
                        oSegment.set_DataElementValue(1, 1, "HC"); 		// Product/Service ID Qualifier (235) 
                        oSegment.set_DataElementValue(1, 2, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].CPT); 		// Product/Service ID (234) 
                        oSegment.set_DataElementValue(1, 3, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier1); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(1, 4, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier2); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(1, 5, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier3); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(1, 6, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier4); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].TotalChargeAmount)); 		// Monetary Amount (782) 
                        oSegment.set_DataElementValue(4, 0, ""); 		// Product/Service ID (234) 
                        oSegment.set_DataElementValue(7, 0, Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Quantity)); 		// Quantity (380) 

                        //REF - Service Line Item Identification
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\SVC\REF"));
                        oSegment.set_DataElementValue(1, 0, "FJ"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].LineID)); 		// Reference Identification (127) 

                        // Service Line Date
                        string SVC_DTP = Convert.ToString(clsGeneral.DateAsNumber(Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].FromDOS)));
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\SVC\DTP"));
                        oSegment.set_DataElementValue(1, 0, "472"); 		// Date/Time Qualifier (374) 
                        oSegment.set_DataElementValue(2, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                        oSegment.set_DataElementValue(3, 0, SVC_DTP); 		// Date Time Period (1251)                                 
                    }//2200D / 2210E

                }//2200D / 2200E
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oSegment != null)
                    oSegment.Dispose();
                InterchangeHeader = string.Empty;
            }

        }//Proc_ClaimStatus

        #endregion

        #region "Parse EDI Request 276 File"

        public void Parse276(string FileName276)
        {
            seffilepath_276_005010X214_SemRef = sPath + "\\SEF\\276_005010X212.SemRef.SEF";
            x276RequestFilePath = FileName276;
            try
            {
                ediDocument.Set(ref oEdiDoc, new ediDocument());
                ediSchemas.Set(ref  oSchemas, new ediSchemas());
                oSchemas.EnableStandardReference = false;
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;
                oEdiDoc.ImportSchema(seffilepath_276_005010X214_SemRef, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);
                Parse276X212RequestFile();
                dtUniqueIDs = getUniQueIDS();
                SetIDforSegments();
                RemoveUnwantedColumnFromERATables();
                Save276X212InDB();
            }
            catch (ediException ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = string.Empty;
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, null, ActivityOutCome.Failure);
            }
        }

        private void Parse276X212RequestFile()
        {
            ediDataSegment oSegment = null;
            string requestFilePath = string.Empty;
            string sSegmentID;
            string sLoopSection;
            int nArea;
            //string sValue;
            string sHlQlfr = "";
            //string sQlfr = "";


            try
            {
                //requestFilePath = @"I:\Developer Working Folder\gloSuite9010\gloSuite\gloPM\gloPM\bin\x86\Debug\TRIARQ_276_ClaimNumber_00098_02152018051229PM.txt.bci";// x276RequestFilePath;
                requestFilePath = x276RequestFilePath;
                if (System.IO.File.Exists(requestFilePath) == true)
                {
                    if (oEdiDoc != null)
                    {
                        oEdiDoc.LoadEdi(requestFilePath);
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc.FirstDataSegment);
                        while (oSegment != null)
                        {
                            sSegmentID = oSegment.ID;
                            sLoopSection = oSegment.LoopSection;
                            nArea = oSegment.Area;
                            if (nArea == 0)
                            {
                                if (sLoopSection == "")
                                {
                                    if (sSegmentID == "ISA")
                                    {
                                        #region "ISA"
                                        if (o276_ISA == null)
                                        {
                                            o276_ISA = new Cls_276X212_ISA();
                                            SegmentCounter++;
                                        }
                                        o276_ISA.ISA01_AuthorInfoQual = oSegment.get_DataElementValue(1, 0);                //Authorization Information Qualifier
                                        o276_ISA.ISA02_AuthorInfo = oSegment.get_DataElementValue(2, 0);                    //Authorization Information
                                        o276_ISA.ISA03_SecurityInfoQual = oSegment.get_DataElementValue(3, 0);              //Security Information Qualifier
                                        o276_ISA.ISA04_SecurityInfo = oSegment.get_DataElementValue(4, 0);                  //Security Information
                                        o276_ISA.ISA05_IntrChngIDQual = oSegment.get_DataElementValue(5, 0);                //Interchange ID Qualifier  
                                        o276_ISA.ISA06_IntrChngSenderID = oSegment.get_DataElementValue(6, 0);              //Interchange Sender ID
                                        o276_ISA.ISA07_IntrChngIDQual = oSegment.get_DataElementValue(7, 0);                //Interchange ID Qualifier
                                        o276_ISA.ISA08_IntrChngReceiverID = oSegment.get_DataElementValue(8, 0);            //Interchange Receiver ID
                                        o276_ISA.ISA09_IntrChngDate = oSegment.get_DataElementValue(9, 0);                  //Interchange Date
                                        o276_ISA.ISA10_IntrChngTime = oSegment.get_DataElementValue(10, 0);                 //Interchange Time
                                        o276_ISA.ISA11_IntrChngRepetitionSeperator = oSegment.get_DataElementValue(11, 0);  //Repetition Separator
                                        o276_ISA.ISA12_IntrChngControlVersionNo = oSegment.get_DataElementValue(12, 0);     //Interchange Control Version Number
                                        o276_ISA.ISA13_IntrChngControlNo = oSegment.get_DataElementValue(13, 0);            //Interchange Control Number
                                        o276_ISA.ISA14_AckwRequested = oSegment.get_DataElementValue(14, 0);                //Acknowledgment Requested
                                        o276_ISA.ISA15_UsageIndicator = oSegment.get_DataElementValue(15, 0);               //Interchange Usage Indicator
                                        o276_ISA.ISA16_ComponentElementSeparator = oSegment.get_DataElementValue(16, 0);    //Component Element Separator                                       
                                        #endregion
                                    }
                                    else if (sSegmentID == "GS")
                                    {
                                        #region "GS"
                                        if (o276_GS == null)
                                        {
                                            o276_GS = new Cls_276X212_GS();
                                            SegmentCounter++;
                                        }
                                        o276_GS.GS01_StatusNotification = oSegment.get_DataElementValue(1, 0);              //Functional Identifier Code
                                        o276_GS.GS02_SenderID = oSegment.get_DataElementValue(2, 0);                        //Application Sender's Code
                                        o276_GS.GS03_ReceiverID = oSegment.get_DataElementValue(3, 0);                      //Application Receiver's Code
                                        o276_GS.GS04_FunctionalGroupDate = oSegment.get_DataElementValue(4, 0);             //Date
                                        o276_GS.GS05_FunctionalGroupTime = oSegment.get_DataElementValue(5, 0);             //Time
                                        o276_GS.GS06_GroupControlNumber = oSegment.get_DataElementValue(6, 0);              //Group Control Number
                                        o276_GS.GS07_ResponsibleAgencyCode = oSegment.get_DataElementValue(7, 0);           //Responsible Agency Code
                                        o276_GS.GS08_VersionORIndustryIdentifier = oSegment.get_DataElementValue(8, 0);     //Version / Release / Industry IdentifierCode
                                        if (o276_ISA != null)
                                        {
                                            o276_ISA.ISA_GS = o276_GS;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else if (nArea == 1)
                            {
                                if (sLoopSection == "")
                                {
                                    if (sSegmentID == "ST")
                                    {
                                        #region "ST"
                                        if (o276_ST == null)
                                        {
                                            o276_ST = new Cls_276X212_ST();
                                            SegmentCounter++;
                                        }
                                        o276_ST.ST01_IdentifierCode = oSegment.get_DataElementValue(1, 0);              //Transaction Set Identifier Code
                                        o276_ST.ST02_TransactioNSetControlNumber = oSegment.get_DataElementValue(2, 0); //Transaction Set Control Number
                                        o276_ST.ST03_ConventionReference = oSegment.get_DataElementValue(3, 0);         //Implementation Convention Reference
                                        #endregion
                                    }
                                    if (sSegmentID == "BHT")
                                    {
                                        #region "BHT"
                                        if (o276_BHT == null)
                                        {
                                            o276_BHT = new Cls_276X212_BHT();
                                            SegmentCounter++;
                                        }
                                        o276_BHT.BHT01_StructureCode = oSegment.get_DataElementValue(1, 0);             //Hierarchical Structure Code
                                        o276_BHT.BHT02_PurposeCode = oSegment.get_DataElementValue(2, 0);               //Transaction Set Purpose Code
                                        o276_BHT.BHT03_ReferenceIdentification = oSegment.get_DataElementValue(3, 0);   //Reference Identification
                                        o276_BHT.BHT04_TransactionDate = oSegment.get_DataElementValue(4, 0);           //Date
                                        o276_BHT.BHT05_TransactionTime = oSegment.get_DataElementValue(5, 0);           //Time
                                        o276_BHT.BHT06_TransactionTypeCode = oSegment.get_DataElementValue(6, 0);       //Transaction Type Code
                                        if (o276_ST != null)
                                        {
                                            o276_ST.ST_BHT = o276_BHT;
                                        }
                                        o276_BHT.Dispose();
                                        o276_BHT = null;
                                        #endregion
                                    }
                                }
                            }
                            else if (nArea == 2)
                            {
                                if (sLoopSection == "HL" && sSegmentID == "HL")
                                {
                                    sHlQlfr = oSegment.get_DataElementValue(3, 0);
                                }
                                else if (sLoopSection == "" && sSegmentID == "SE")
                                {
                                    if (o276_ST != null)
                                    {
                                        if (o276_HL != null)
                                        {
                                            if (o276_TRN != null)
                                            {
                                                if (o276_HL.HL_TRN == null)
                                                {
                                                    o276_HL.HL_TRN = new List<Cls_276X212_TRN>();
                                                }
                                                o276_HL.HL_TRN.Add(o276_TRN);
                                                o276_TRN.Dispose();
                                                o276_TRN = null;
                                            }
                                            if (o276_ST.ST_HL == null)
                                            {
                                                o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                            }
                                            o276_ST.ST_HL.Add(o276_HL);
                                            o276_HL.Dispose();
                                            o276_HL = null;

                                            if (o276_ISA != null)
                                            {
                                                if (o276_ISA.ISA_ST == null)
                                                {
                                                    o276_ISA.ISA_ST = new List<Cls_276X212_ST>();
                                                }
                                                o276_ISA.ISA_ST.Add(o276_ST);
                                                o276_ST.Dispose();
                                                o276_ST = null;
                                            }
                                        }
                                    }
                                }

                                if (sHlQlfr == "20")                                                                    //2000A INFORMATION SOURCE LEVEL
                                {
                                    #region "2000A INFORMATION SOURCEL LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")                                                         //Information Source Level
                                        {
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code
                                        }
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                  //2100A PAYER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);               //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);        //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                   //Name Last or Organization Name                                                    
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);//Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);         //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    #endregion
                                }
                                if (sHlQlfr == "21")                                                                    //2000B INFORMATION RECEIVER LEVEL
                                {
                                    #region "2000B INFORMATION RECEIVER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")                                                         //Information Receiver Level
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_ST.ST_HL == null)
                                                {
                                                    o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                }
                                                if (o276_HL != null)
                                                {
                                                    o276_ST.ST_HL.Add(o276_HL);
                                                    o276_HL.Dispose();
                                                    o276_HL = null;
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);                 //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);                  //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);                   //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);                   //Hierarchical Child Code
                                        }
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                      //2100B RECEIVER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);                   //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);            //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                       //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                      //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                     //Name Middle                                              
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);    //Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);             //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    #endregion
                                }
                                if (sHlQlfr == "19")                                                                    //2000C INFORMATION PROVIDER LEVEL
                                {
                                    #region "INFORMATION PROVIDER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")                                                         //Service Provider Level
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_ST.ST_HL == null)
                                                {
                                                    o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                }
                                                if (o276_HL != null)
                                                {
                                                    o276_ST.ST_HL.Add(o276_HL);
                                                    o276_HL.Dispose();
                                                    o276_HL = null;
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code                                    
                                        }
                                    }
                                    else if (sLoopSection == "HL;NM1")
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);               //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);        //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                   //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                  //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                 //Name Middle
                                            o276_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);                     //Name Suffix
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);//Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);         //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    #endregion
                                }
                                if (sHlQlfr == "22")                                                                    //2000D INFORMATION SUBSCRIBER LEVEL
                                {
                                    #region "INFORMATION SUBSCRIBER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_ST.ST_HL == null)
                                                {
                                                    o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                }
                                                if (o276_HL != null)
                                                {
                                                    o276_ST.ST_HL.Add(o276_HL);
                                                    o276_HL.Dispose();
                                                    o276_HL = null;
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code
                                        }
                                    }
                                    if (sSegmentID == "DMG")
                                    {
                                        if (o276_DMG == null)
                                        {
                                            o276_DMG = new Cls_276X212_DMG();
                                            SegmentCounter++;
                                        }
                                        o276_DMG.DMG01_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(1, 0); //Date Time Period Format Qualifier
                                        o276_DMG.DMG02_DateTimePeriod = oSegment.get_DataElementValue(2, 0);                //Date Time Period
                                        o276_DMG.DMG03_GenderCode = oSegment.get_DataElementValue(3, 0);                    //Gender Code
                                        if (o276_HL != null)
                                        {
                                            o276_HL.HL_DMG = o276_DMG;
                                        }
                                        o276_DMG.Dispose();
                                        o276_DMG = null;
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                             //2100D SUBSCRIBER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);                   //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);            //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                       //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                      //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                     //Name Middle
                                            o276_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);                         //Name Suffix
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);    //Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);             //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    Prase_ClaimStatus(ref oSegment, ref sSegmentID, ref sLoopSection);
                                    #endregion
                                }
                                if (sHlQlfr == "23")                                                                    //2000E INFORMATION PATIENT LEVEL
                                {
                                    #region "INFORMATION PATIENT LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_HL != null)
                                                {
                                                    if (o276_ST.ST_HL == null)
                                                    {
                                                        o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                    }
                                                    if (o276_HL != null)
                                                    {
                                                        o276_ST.ST_HL.Add(o276_HL);
                                                        o276_HL.Dispose();
                                                        o276_HL = null;
                                                    }
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code
                                        }
                                    }
                                    if (sSegmentID == "DMG")
                                    {
                                        if (o276_DMG == null)
                                        {
                                            o276_DMG = new Cls_276X212_DMG();
                                            SegmentCounter++;
                                        }
                                        o276_DMG.DMG01_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(1, 0); //Date Time Period Format Qualifier
                                        o276_DMG.DMG02_DateTimePeriod = oSegment.get_DataElementValue(2, 0);                //Date Time Period
                                        o276_DMG.DMG03_GenderCode = oSegment.get_DataElementValue(3, 0);                    //Gender Code
                                        if (o276_HL != null)
                                        {
                                            o276_HL.HL_DMG = o276_DMG;
                                        }
                                        o276_DMG.Dispose();
                                        o276_DMG = null;
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                             //2100D SUBSCRIBER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);                   //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);            //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                       //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                      //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                     //Name Middle
                                            o276_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);                         //Name Suffix
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);    //Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);             //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    Prase_ClaimStatus(ref oSegment, ref sSegmentID, ref sLoopSection);
                                    #endregion
                                }
                            }
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                        }
                    }
                }
            }
            catch (ediException ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oEdiDoc != null)
                {
                    oEdiDoc.Close();
                    oEdiDoc.Dispose();
                    oEdiDoc = null;
                }
            }
        }

        public void Prase_ClaimStatus(ref ediDataSegment oSegment, ref string sSegmentID, ref string sLoopSection)
        {
            try
            {
                if (sLoopSection == "HL;TRN")                                                          //2200D SUBSCRIBER TRACKING/2200E PATIENT TRACKING                                                         
                {
                    #region "2200D SUBSCRIBER NAME"
                    if (sSegmentID == "TRN")                                                                //Claim Status Tracking Number
                    {
                        if (o276_TRN == null)
                        {
                            o276_TRN = new Cls_276X212_TRN();
                            SegmentCounter++;
                        }
                        o276_TRN.TRN01_TraceTypeID = oSegment.get_DataElementValue(1, 0);                   //Trace Type Code                  
                        o276_TRN.TRN02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0);       //Reference Identification                    
                    }
                    else if (sSegmentID == "REF")                                                           //REFERENCE
                    {
                        if (o276_REF == null)
                        {
                            o276_REF = new Cls_276X212_REF();
                            SegmentCounter++;
                        }
                        o276_REF.REF01_ReferenceIdentificationQualifier = oSegment.get_DataElementValue(1, 0);  //Reference Identification Qualifier
                        o276_REF.REF02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0);           //Reference Identification   
                        if (o276_TRN != null)
                        {
                            if (o276_TRN.TRN_REF == null)
                            {
                                o276_TRN.TRN_REF = new List<Cls_276X212_REF>();
                            }
                            o276_TRN.TRN_REF.Add(o276_REF);
                            o276_REF.Dispose();
                            o276_REF = null;
                        }
                    }
                    else if (sSegmentID == "AMT")                                                   //Claim Submitted Charges
                    {
                        if (o276_AMT == null)
                        {
                            o276_AMT = new Cls_276X212_AMT();
                            SegmentCounter++;
                        }
                        o276_AMT.AMT01_AmountQualifierCode = oSegment.get_DataElementValue(1, 0);   //Amount Qualifier Code
                        o276_AMT.AMT02_MonetaryAmount = oSegment.get_DataElementValue(2, 0);        //Monetary Amount
                        if (o276_TRN != null)
                        {
                            o276_TRN.TRN_AMT = o276_AMT;
                            o276_AMT.Dispose();
                            o276_AMT = null;
                        }
                    }
                    else if (sSegmentID == "DTP")                                                   //Claim Service Date
                    {
                        if (o276_DTP == null)
                        {
                            o276_DTP = new Cls_276X212_DTP();
                            SegmentCounter++;
                        }
                        o276_DTP.DTP01_DateTimeQualifier = oSegment.get_DataElementValue(1, 0);             //Date/Time Qualifier
                        o276_DTP.DTP02_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(2, 0); //Date Time Period Format Qualifier
                        o276_DTP.DTP03_DateTimePeriod = oSegment.get_DataElementValue(3, 0);                //Date Time Period
                        if (o276_TRN != null)
                        {
                            o276_TRN.TRN_DTP = o276_DTP;
                            o276_DTP.Dispose();
                            o276_DTP = null;
                        }
                    }
                    #endregion
                }

                else if (sLoopSection == "HL;TRN;SVC")                                                      //2210D SUBSCRIBER TRACKING/2210E PATIENT TRACKING
                {
                    #region "2210D SUBSCRIBER NAME"
                    if (sSegmentID == "SVC")
                    {
                        if (o276_SVC == null)
                        {
                            o276_SVC = new Cls_276X212_SVC();
                            SegmentCounter++;
                        }
                        o276_SVC.SVC01_01_ProductORServiceIDQualifier = oSegment.get_DataElementValue(1, 0);    //Product/Service ID Qualifier
                        o276_SVC.SVC01_02_ProductORServiceID = oSegment.get_DataElementValue(2, 0);             //Product/Service ID
                        o276_SVC.SVC01_03_ProcedureModifier = oSegment.get_DataElementValue(3, 0);              //Procedure Modifier
                        o276_SVC.SVC01_04_ProcedureModifier = oSegment.get_DataElementValue(4, 0);              //Procedure Modifier
                        o276_SVC.SVC01_05_ProcedureModifier = oSegment.get_DataElementValue(5, 0);              //Procedure Modifier
                        o276_SVC.SVC01_06_ProcedureModifier = oSegment.get_DataElementValue(6, 0);              //Procedure Modifier
                        o276_SVC.SVC02_MonetaryAmount = oSegment.get_DataElementValue(2, 0);                    //Monetary Amount
                        o276_SVC.SVC04_ProductORServiceID = oSegment.get_DataElementValue(4, 0);                //Product/Service ID
                        o276_SVC.SVC07_Quantity = oSegment.get_DataElementValue(7, 0);                          //Quantity
                    }
                    else if (sSegmentID == "REF")
                    {
                        if (o276_REF == null)
                        {
                            o276_REF = new Cls_276X212_REF();
                            SegmentCounter++;
                        }
                        o276_REF.REF01_ReferenceIdentificationQualifier = oSegment.get_DataElementValue(1, 0);  //Reference Identification Qualifier
                        o276_REF.REF02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0);           //Reference Identification          
                        if (o276_SVC != null)
                        {
                            o276_SVC.SVC_REF = o276_REF;
                            o276_REF.Dispose();
                            o276_REF = null;
                        }
                    }
                    else if (sSegmentID == "DTP")
                    {
                        if (o276_DTP == null)
                        {
                            o276_DTP = new Cls_276X212_DTP();
                            SegmentCounter++;
                        }
                        o276_DTP.DTP01_DateTimeQualifier = oSegment.get_DataElementValue(1, 0);             //Date/Time Qualifier
                        o276_DTP.DTP02_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(2, 0); //Date Time Period Format Qualifier
                        o276_DTP.DTP03_DateTimePeriod = oSegment.get_DataElementValue(3, 0);                //Date Time Period
                        if (o276_SVC != null)
                        {
                            o276_SVC.SVC_DTP = o276_DTP;
                            o276_DTP.Dispose();
                            o276_DTP = null;
                        }
                        if (o276_SVC != null)
                        {
                            if (o276_TRN != null)
                            {
                                if (o276_TRN.TRN_SVC == null)
                                {
                                    o276_TRN.TRN_SVC = new List<Cls_276X212_SVC>();
                                }
                                o276_TRN.TRN_SVC.Add(o276_SVC);
                                o276_SVC.Dispose();
                                o276_SVC = null;


                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }


        }//Proc_ClaimStatus

        private DataTable getUniQueIDS()
        {
            DataTable dt = null;
            try
            {
                if (SegmentCounter > 0)
                {
                    if (OpenConnection(true))
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@IDCount", SegmentCounter, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@SingleRow", 1, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.Retrive("gsp_GetUniqueIDs", oDBPara, out dt);
                        CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateSegmentIDs, ex, ActivityOutCome.Failure);
                return null;
            }
            return dt;
        }

        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }

        private void SetIDforSegments()
        {
            try
            {
                if (dtUniqueIDs != null)
                {
                    if (dtUniqueIDs.Rows.Count > 0)
                    {
                        ISAID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                        o276_ISA.ERAFileID = ERAFileID;
                        o276_ISA.ISAID = ISAID;
                        UniqueIdCounter++;
                        CreateISADataTable();

                        o276_ISA.ISA_GS.ERAFileID = ERAFileID;
                        o276_ISA.ISA_GS.GS_ISAID = ISAID;
                        o276_ISA.ISA_GS.GSID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                        CreateGSDatatable(o276_ISA.ISA_GS);
                        UniqueIdCounter++;
                        for (int st = 0; st < o276_ISA.ISA_ST.Count; st++)
                        {
                            o276_ISA.ISA_ST[st].ERAFileID = ERAFileID;
                            o276_ISA.ISA_ST[st].ST_ISAID = ISAID;
                            CurrentSTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                            UniqueIdCounter++;
                            o276_ISA.ISA_ST[st].STID = CurrentSTID;
                            CreateSTDatatable(o276_ISA.ISA_ST[st]);

                            o276_ISA.ISA_ST[st].ST_BHT.ERAFileID = ERAFileID;
                            o276_ISA.ISA_ST[st].ST_BHT.BHT_ISAID = ISAID;
                            o276_ISA.ISA_ST[st].ST_BHT.BHT_STID = CurrentSTID;
                            o276_ISA.ISA_ST[st].ST_BHT.BHTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                            UniqueIdCounter++;
                            CreateBHTDatatable(o276_ISA.ISA_ST[st].ST_BHT);

                            for (int hl = 0; hl < o276_ISA.ISA_ST[st].ST_HL.Count; hl++)
                            {
                                o276_ISA.ISA_ST[st].ST_HL[hl].ERAFileID = ERAFileID;
                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_ISAID = ISAID;
                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_STID = CurrentSTID;
                                CurrentHLId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                o276_ISA.ISA_ST[st].ST_HL[hl].HLID = CurrentHLId;
                                UniqueIdCounter++;

                                switch (Convert.ToString(o276_ISA.ISA_ST[st].ST_HL[hl].HL03_LevelCode))
                                {
                                    case "20": SourceHLId = CurrentHLId;                                    //Source
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = 0;
                                        break;
                                    case "21": ReceiverHLId = CurrentHLId;                                  //Reveiver
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = SourceHLId;
                                        break;
                                    case "19": ProviderHLId = CurrentHLId;                                  //Provider
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = ReceiverHLId;
                                        break;
                                    case "22": SubscriberHLId = CurrentHLId;                                //Subcriber
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = ProviderHLId;
                                        break;
                                    case "23": DependentHLId = CurrentHLId;                                 //Dependent
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = SubscriberHLId;
                                        break;
                                }
                                CreateHLDatatable(o276_ISA.ISA_ST[st].ST_HL[hl]);

                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM != null)
                                {
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.ERAFileID = ERAFileID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_ISAID = ISAID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_STID = CurrentSTID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_HLID = CurrentHLId;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NMID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                    UniqueIdCounter++;
                                    CreateNMDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM);
                                }
                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG != null)
                                {
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.ERAFileID = ERAFileID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMG_ISAID = ISAID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMG_STID = CurrentSTID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMG_HLID = CurrentHLId;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMGID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                    UniqueIdCounter++;
                                    CreateDMGDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG);
                                }

                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN != null)
                                {
                                    for (int trn = 0; trn < o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN.Count; trn++)
                                    {
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].ERAFileID = ERAFileID;
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_ISAID = ISAID;
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STID = CurrentSTID;
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_HLID = CurrentHLId;
                                        CurrentTRNId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRNID = CurrentTRNId;
                                        UniqueIdCounter++;
                                        CreateTRNDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn]);

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF != null)
                                        {
                                            for (int reff = 0; reff < o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF.Count; reff++)
                                            {
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].ERAFileID = ERAFileID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_ISAID = ISAID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_STID = CurrentSTID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_HLID = CurrentHLId;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_TRNID = CurrentTRNId;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REFID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                UniqueIdCounter++;
                                                CreateREFDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff]);
                                            }
                                        }

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT != null)
                                        {
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.ERAFileID = ERAFileID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_ISAID = ISAID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_STID = CurrentSTID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_HLID = CurrentHLId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_TRNID = CurrentTRNId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                            UniqueIdCounter++;
                                            CreateAMTDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT);
                                        }

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP != null)
                                        {
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.ERAFileID = ERAFileID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_ISAID = ISAID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_STID = CurrentSTID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_HLID = CurrentHLId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_TRNID = CurrentTRNId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTPID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                            UniqueIdCounter++;
                                            CreateDTPDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP);
                                        }

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC != null)
                                        {
                                            for (int trnsvc = 0; trnsvc < o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC.Count; trnsvc++)
                                            {
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].ERAFileID = ERAFileID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_ISAID = ISAID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STID = CurrentSTID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_HLID = CurrentHLId;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_TRNID = CurrentTRNId;
                                                CurrentSVCId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVCID = CurrentSVCId;
                                                UniqueIdCounter++;
                                                CreateSVCDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc]);

                                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF != null)
                                                {
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.ERAFileID = ERAFileID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_ISAID = ISAID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_STID = CurrentSTID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_HLID = CurrentHLId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_TRNID = CurrentTRNId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_SVCID = CurrentSVCId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REFID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                    UniqueIdCounter++;
                                                    CreateREFDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF);
                                                }

                                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP != null)
                                                {
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.ERAFileID = ERAFileID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_ISAID = ISAID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_STID = CurrentSTID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_HLID = CurrentHLId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_TRNID = CurrentTRNId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_SVCID = CurrentSVCId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTPID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                    UniqueIdCounter++;
                                                    CreateDTPDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ediException ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateSegmentIDs, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateISADataTable()
        {
            Cls_276X212_ISAs oISAs = new Cls_276X212_ISAs();
            try
            {
                if (o276_ISA != null)
                {
                    oISAs.Add(o276_ISA);
                }
                Cls_276X212_ISA[] oISA = new Cls_276X212_ISA[oISAs.Count];
                oISAs.CopyTo(oISA, 0);
                if (oISA != null)
                {
                    var _test = (from r in oISA select r).ToList();
                    if (dtISA == null)
                    {
                        dtISA = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.ISA);
                    }
                    oISA = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateGSDatatable(Cls_276X212_GS _oGS)
        {
            Cls_276X212_GSs oGSs = new Cls_276X212_GSs();
            try
            {
                if (_oGS != null)
                {
                    oGSs.Add(_oGS);
                }

                Cls_276X212_GS[] oGS = new Cls_276X212_GS[oGSs.Count];
                oGSs.CopyTo(oGS, 0);
                if (oGS != null)
                {
                    var _test = (from r in oGS select r).ToList();

                    if (dtGS == null)
                    {
                        dtGS = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.GS);
                    }
                    oGS = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateSTDatatable(Cls_276X212_ST _oST)
        {
            Cls_276X212_STs oGSs = new Cls_276X212_STs();
            try
            {
                if (_oST != null)
                {
                    oGSs.Add(_oST);
                }

                Cls_276X212_ST[] oST = new Cls_276X212_ST[oGSs.Count];
                oGSs.CopyTo(oST, 0);
                if (oST != null)
                {
                    var _test = (from r in oST select r).ToList();
                    if (dtST == null)
                    {
                        dtST = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.ST);
                    }
                    oST = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }

        }

        private void CreateBHTDatatable(Cls_276X212_BHT _oBHT)
        {
            Cls_276X212_BHTs oBHTs = new Cls_276X212_BHTs();
            try
            {
                if (_oBHT != null)
                {
                    oBHTs.Add(_oBHT);
                }

                Cls_276X212_BHT[] oBHT = new Cls_276X212_BHT[oBHTs.Count];
                oBHTs.CopyTo(oBHT, 0);
                if (oBHT != null)
                {
                    var _test = (from r in oBHT select r).ToList();
                    if (dtBHT == null)
                    {
                        dtBHT = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.BHT);
                    }
                    oBHT = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateHLDatatable(Cls_276X212_HL _oHL)
        {
            Cls_276X212_HLs oHLs = new Cls_276X212_HLs();
            try
            {
                if (_oHL != null)
                {
                    oHLs.Add(_oHL);
                }

                Cls_276X212_HL[] oHL = new Cls_276X212_HL[oHLs.Count];
                oHLs.CopyTo(oHL, 0);
                if (oHL != null)
                {
                    var _test = (from r in oHL select r).ToList();
                    if (dtHL == null)
                    {
                        dtHL = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.HL);
                    }
                    oHL = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateNMDatatable(Cls_276X212_NM _oNM)
        {
            Cls_276X212_NMs oNMs = new Cls_276X212_NMs();
            try
            {
                if (_oNM != null)
                {
                    oNMs.Add(_oNM);
                }

                Cls_276X212_NM[] oNM = new Cls_276X212_NM[oNMs.Count];
                oNMs.CopyTo(oNM, 0);
                if (oNM != null)
                {
                    var _test = (from r in oNM select r).ToList();
                    if (dtNM == null)
                    {
                        dtNM = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.NM);
                    }
                    oNM = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateDMGDatatable(Cls_276X212_DMG _oDMG)
        {
            Cls_276X212_DMGs oDMGs = new Cls_276X212_DMGs();
            try
            {
                if (_oDMG != null)
                {
                    oDMGs.Add(_oDMG);
                }

                Cls_276X212_DMG[] oDMG = new Cls_276X212_DMG[oDMGs.Count];
                oDMGs.CopyTo(oDMG, 0);
                if (oDMG != null)
                {
                    var _test = (from r in oDMG select r).ToList();
                    if (dtDMG == null)
                    {
                        dtDMG = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.DMG);
                    }
                    oDMG = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateTRNDatatable(Cls_276X212_TRN _oTRN)
        {
            Cls_276X212_TRNs oTRNs = new Cls_276X212_TRNs();
            try
            {
                if (_oTRN != null)
                {
                    oTRNs.Add(_oTRN);
                }

                Cls_276X212_TRN[] oTRN = new Cls_276X212_TRN[oTRNs.Count];
                oTRNs.CopyTo(oTRN, 0);
                if (oTRN != null)
                {
                    var _test = (from r in oTRN select r).ToList();
                    if (dtTRN == null)
                    {
                        dtTRN = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.TRN);
                    }
                    oTRN = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateREFDatatable(Cls_276X212_REF _oREF)
        {
            Cls_276X212_REFs oREFs = new Cls_276X212_REFs();
            try
            {
                if (_oREF != null)
                {
                    oREFs.Add(_oREF);
                }

                Cls_276X212_REF[] oREF = new Cls_276X212_REF[oREFs.Count];
                oREFs.CopyTo(oREF, 0);
                if (oREF != null)
                {
                    var _test = (from r in oREF select r).ToList();
                    if (dtREF == null)
                    {
                        dtREF = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.REF);
                    }

                    oREF = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateAMTDatatable(Cls_276X212_AMT _oAMT)
        {
            Cls_276X212_AMTs oAMTs = new Cls_276X212_AMTs();
            try
            {
                if (_oAMT != null)
                {
                    oAMTs.Add(_oAMT);
                }

                Cls_276X212_AMT[] oAMT = new Cls_276X212_AMT[oAMTs.Count];
                oAMTs.CopyTo(oAMT, 0);
                if (oAMT != null)
                {
                    var _test = (from r in oAMT select r).ToList();
                    if (dtAMT == null)
                    {
                        dtAMT = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.AMT);
                    }
                    oAMT = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateDTPDatatable(Cls_276X212_DTP _oDTP)
        {
            Cls_276X212_DTPs oDTPs = new Cls_276X212_DTPs();
            try
            {
                if (_oDTP != null)
                {
                    oDTPs.Add(_oDTP);
                }

                Cls_276X212_DTP[] oDTP = new Cls_276X212_DTP[oDTPs.Count];
                oDTPs.CopyTo(oDTP, 0);
                if (oDTP != null)
                {
                    var _test = (from r in oDTP select r).ToList();
                    if (dtDTP == null)
                    {
                        dtDTP = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.DTP);
                    }
                    oDTP = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateSVCDatatable(Cls_276X212_SVC _oSVC)
        {
            Cls_276X212_SVCs oSVCs = new Cls_276X212_SVCs();
            try
            {
                if (_oSVC != null)
                {
                    oSVCs.Add(_oSVC);
                }

                Cls_276X212_SVC[] oSVC = new Cls_276X212_SVC[oSVCs.Count];
                oSVCs.CopyTo(oSVC, 0);
                if (oSVC != null)
                {
                    var _test = (from r in oSVC select r).ToList();
                    if (dtSVC == null)
                    {
                        dtSVC = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.SVC);
                    }
                    oSVC = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
        }

        private DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var dt = new DataTable();
            try
            {
                var props = typeof(TSource).GetProperties();
                dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
                source.ToList().ForEach(i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return dt;
        }

        private void AddToDataTable<TSource>(IEnumerable<TSource> source, Segment eSegment)
        {
            try
            {


                var props = typeof(TSource).GetProperties();

                switch (eSegment)
                {
                    //dtISA, dtGS, dtST, dtBHT, dtHL, dtNM, dtPER, dtTRN, dtSVC, dtAMT, dtREF, dtDTP
                    case Segment.ISA:
                        source.ToList().ForEach(i => dtISA.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.GS:
                        source.ToList().ForEach(i => dtGS.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.ST:
                        source.ToList().ForEach(i => dtST.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.BHT:
                        source.ToList().ForEach(i => dtBHT.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.HL:
                        source.ToList().ForEach(i => dtHL.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.NM:
                        source.ToList().ForEach(i => dtNM.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.DMG:
                        source.ToList().ForEach(i => dtDMG.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.DTP:
                        source.ToList().ForEach(i => dtDTP.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.TRN:
                        source.ToList().ForEach(i => dtTRN.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.SVC:
                        source.ToList().ForEach(i => dtSVC.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.REF:
                        source.ToList().ForEach(i => dtREF.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.AMT:
                        source.ToList().ForEach(i => dtAMT.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
        }

        private void RemoveUnwantedColumnFromERATables()
        {
            try
            {
                if (dtISA != null)
                {
                    dtISA.Columns.Remove("ISA_GS");
                    dtISA.Columns.Remove("ISA_ST");
                }

                if (dtST != null)
                {
                    dtST.Columns.Remove("ST_BHT");
                    dtST.Columns.Remove("ST_HL");
                }

                if (dtHL != null)
                {
                    dtHL.Columns.Remove("HL_NM");
                    dtHL.Columns.Remove("HL_DMG");
                    dtHL.Columns.Remove("HL_TRN");
                }

                if (dtTRN != null)
                {
                    dtTRN.Columns.Remove("TRN_REF");
                    dtTRN.Columns.Remove("TRN_AMT");
                    dtTRN.Columns.Remove("TRN_DTP");
                    dtTRN.Columns.Remove("TRN_SVC");
                }

                if (dtSVC != null)
                {
                    dtSVC.Columns.Remove("SVC_REF");
                    dtSVC.Columns.Remove("SVC_DTP");
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
        }

        private bool Save276X212InDB()
        {

            SqlConnection conn = new System.Data.SqlClient.SqlConnection(_DataBaseConnectionString);
            conn.Open();
            //SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            SqlBulkCopy bulkCopy = null;
            try
            {
                bulkCopy = new System.Data.SqlClient.SqlBulkCopy(_DataBaseConnectionString);
                bulkCopy.BulkCopyTimeout = 0;

                if (dtISA != null && dtISA.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_ISA";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("ISA01_AuthorInfoQual", "ISA01_AuthorInfoQual");
                    bulkCopy.ColumnMappings.Add("ISA02_AuthorInfo", "ISA02_AuthorInfo");
                    bulkCopy.ColumnMappings.Add("ISA03_SecurityInfoQual", "ISA03_SecurityInfoQual");
                    bulkCopy.ColumnMappings.Add("ISA04_SecurityInfo", "ISA04_SecurityInfo");
                    bulkCopy.ColumnMappings.Add("ISA05_IntrChngIDQual", "ISA05_IntrChngIDQual");
                    bulkCopy.ColumnMappings.Add("ISA06_IntrChngSenderID", "ISA06_IntrChngSenderID");
                    bulkCopy.ColumnMappings.Add("ISA07_IntrChngIDQual", "ISA07_IntrChngIDQual");
                    bulkCopy.ColumnMappings.Add("ISA08_IntrChngReceiverID", "ISA08_IntrChngReceiverID");
                    bulkCopy.ColumnMappings.Add("ISA09_IntrChngDate", "ISA09_IntrChngDate");
                    bulkCopy.ColumnMappings.Add("ISA10_IntrChngTime", "ISA10_IntrChngTime");
                    bulkCopy.ColumnMappings.Add("ISA11_IntrChngRepetitionSeperator", "ISA11_IntrChngRepetitionSeperator");
                    bulkCopy.ColumnMappings.Add("ISA12_IntrChngControlVersionNo", "ISA12_IntrChngControlVersionNo");
                    bulkCopy.ColumnMappings.Add("ISA13_IntrChngControlNo", "ISA13_IntrChngControlNo");
                    bulkCopy.ColumnMappings.Add("ISA14_AckwRequested", "ISA14_AckwRequested");
                    bulkCopy.ColumnMappings.Add("ISA15_UsageIndicator", "ISA15_UsageIndicator");
                    bulkCopy.ColumnMappings.Add("ISA16_ComponentElementSeparator", "ISA16_ComponentElementSeparator");
                    bulkCopy.WriteToServer(dtISA);
                }

                if (dtGS != null && dtGS.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_GS";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("GS_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("GSID", "GSID");
                    bulkCopy.ColumnMappings.Add("GS01_StatusNotification", "GS01_StatusNotification");
                    bulkCopy.ColumnMappings.Add("GS02_SenderID", "GS02_SenderID");
                    bulkCopy.ColumnMappings.Add("GS03_ReceiverID", "GS03_ReceiverID");
                    bulkCopy.ColumnMappings.Add("GS04_FunctionalGroupDate", "GS04_FunctionalGroupDate");
                    bulkCopy.ColumnMappings.Add("GS05_FunctionalGroupTime", "GS05_FunctionalGroupTime");
                    bulkCopy.ColumnMappings.Add("GS06_GroupControlNumber", "GS06_GroupControlNumber");
                    bulkCopy.ColumnMappings.Add("GS07_ResponsibleAgencyCode", "GS07_ResponsibleAgencyCode");
                    bulkCopy.ColumnMappings.Add("GS08_VersionORIndustryIdentifier", "GS08_VersionORIndustryIdentifier");
                    bulkCopy.WriteToServer(dtGS);
                }

                if (dtST != null && dtST.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_ST";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("ST_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("STID", "STID");
                    bulkCopy.ColumnMappings.Add("ST01_IdentifierCode", "ST01_IdentifierCode");
                    bulkCopy.ColumnMappings.Add("ST02_TransactioNSetControlNumber", "ST02_TransactioNSetControlNumber");
                    bulkCopy.ColumnMappings.Add("ST03_ConventionReference", "ST03_ConventionReference");
                    bulkCopy.WriteToServer(dtST);
                }

                if (dtBHT != null && dtBHT.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_BHT";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("BHT_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("BHT_STID", "STID");
                    bulkCopy.ColumnMappings.Add("BHTID", "BHTID");
                    bulkCopy.ColumnMappings.Add("BHT01_StructureCode", "BHT01_StructureCode");
                    bulkCopy.ColumnMappings.Add("BHT02_PurposeCode", "BHT02_PurposeCode");
                    bulkCopy.ColumnMappings.Add("BHT03_ReferenceIdentification", "BHT03_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("BHT04_TransactionDate", "BHT04_TransactionDate");
                    bulkCopy.ColumnMappings.Add("BHT05_TransactionTime", "BHT05_TransactionTime");
                    bulkCopy.ColumnMappings.Add("BHT06_TransactionTypeCode", "BHT06_TransactionTypeCode");
                    bulkCopy.WriteToServer(dtBHT);
                }

                if (dtHL != null && dtHL.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_HL";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("HL_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("HL_STID", "STID");
                    bulkCopy.ColumnMappings.Add("ParentHLID", "ParentHLID");
                    bulkCopy.ColumnMappings.Add("HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("HL01_HLSegmentId", "HL01_HLSegmentId");
                    bulkCopy.ColumnMappings.Add("HL02_HLParentId", "HL02_HLParentId");
                    bulkCopy.ColumnMappings.Add("HL03_LevelCode", "HL03_LevelCode");
                    bulkCopy.ColumnMappings.Add("HL04_ChildCode", "HL04_ChildCode");
                    bulkCopy.WriteToServer(dtHL);
                }

                if (dtNM != null && dtNM.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_NM";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("NM_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("NM_STID", "STID");
                    bulkCopy.ColumnMappings.Add("NM_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("NMID", "NMID");
                    bulkCopy.ColumnMappings.Add("NM101_EntityIdCode", "NM101_EntityIdCode");
                    bulkCopy.ColumnMappings.Add("NM102_EntityTypeQualifier", "NM102_EntityTypeQualifier");
                    bulkCopy.ColumnMappings.Add("NM103_LastName", "NM103_LastName");
                    bulkCopy.ColumnMappings.Add("NM104_FirstName", "NM104_FirstName");
                    bulkCopy.ColumnMappings.Add("NM105_MiddleName", "NM105_MiddleName");
                    bulkCopy.ColumnMappings.Add("NM106_Prefix", "NM106_Prefix");
                    bulkCopy.ColumnMappings.Add("NM107_Suffix", "NM107_Suffix");
                    bulkCopy.ColumnMappings.Add("NM108_IdentificationCodeQualifier", "NM108_IdentificationCodeQualifier");
                    bulkCopy.ColumnMappings.Add("NM109_IdentificationCode", "NM109_IdentificationCode");
                    bulkCopy.ColumnMappings.Add("NM110_EntityRelationShipCode", "NM110_EntityRelationShipCode");
                    bulkCopy.ColumnMappings.Add("NM111_EntityIdentifierCode", "NM111_EntityIdentifierCode");
                    bulkCopy.ColumnMappings.Add("NM112_NameLastOROrganizatioName", "NM112_NameLastOROrganizatioName");
                    bulkCopy.WriteToServer(dtNM);
                }

                if (dtDMG != null && dtDMG.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_DMG";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("DMG_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("DMG_STID", "STID");
                    bulkCopy.ColumnMappings.Add("DMG_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("DMGID", "DMGID");
                    bulkCopy.ColumnMappings.Add("DMG01_DateTimePeriodFormatQualifier", "DMG01_DateTimePeriodFormatQualifier");
                    bulkCopy.ColumnMappings.Add("DMG02_DateTimePeriod", "DMG02_DateTimePeriod");
                    bulkCopy.ColumnMappings.Add("DMG03_GenderCode", "DMG03_GenderCode");
                    bulkCopy.ColumnMappings.Add("DMG04_MaritalStatusCode", "DMG04_MaritalStatusCode");
                    bulkCopy.ColumnMappings.Add("DMG05_01_RaceorEthnicityCode", "DMG05_01_RaceorEthnicityCode");
                    bulkCopy.ColumnMappings.Add("DMG05_02_QualifierCode", "DMG05_02_QualifierCode");
                    bulkCopy.ColumnMappings.Add("DMG05_03_IndustryCode", "DMG05_03_IndustryCode");
                    bulkCopy.ColumnMappings.Add("DMG06_CitizenShipStatusCode", "DMG06_CitizenShipStatusCode");
                    bulkCopy.ColumnMappings.Add("DMG07_CountryCode", "DMG07_CountryCode");
                    bulkCopy.ColumnMappings.Add("DMG08_VerificationCode", "DMG08_VerificationCode");
                    bulkCopy.ColumnMappings.Add("DMG09_Quantity", "DMG09_Quantity");
                    bulkCopy.ColumnMappings.Add("DMG010_CodeListQualifierCode", "DMG010_CodeListQualifierCode");
                    bulkCopy.ColumnMappings.Add("DMG011_IndustryCode", "DMG011_IndustryCode");
                    bulkCopy.WriteToServer(dtDMG);
                }

                if (dtTRN != null && dtTRN.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_TRN";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("TRN_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("TRN_STID", "STID");
                    bulkCopy.ColumnMappings.Add("TRN_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("TRN01_TraceTypeID", "TRN01_TraceTypeID");
                    bulkCopy.ColumnMappings.Add("TRN02_ReferenceIdentification", "TRN02_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("TRN03_OriginatingCompanyIdentifier", "TRN03_OriginatingCompanyIdentifier");
                    bulkCopy.ColumnMappings.Add("TRN04_ReferenceIdentification", "TRN04_ReferenceIdentification");
                    bulkCopy.WriteToServer(dtTRN);
                }

                if (dtSVC != null && dtSVC.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_SVC";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("SVC_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("SVC_STID", "STID");
                    bulkCopy.ColumnMappings.Add("SVC_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("SVC_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("SVC01_01_ProductORServiceIDQualifier", "SVC01_01_ProductORServiceIDQualifier");
                    bulkCopy.ColumnMappings.Add("SVC01_02_ProductORServiceID", "SVC01_02_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC01_03_ProcedureModifier", "SVC01_03_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_04_ProcedureModifier", "SVC01_04_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_05_ProcedureModifier", "SVC01_05_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_06_ProcedureModifier", "SVC01_06_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_07_Description", "SVC01_07_Description");
                    bulkCopy.ColumnMappings.Add("SVC01_08_ProductORServiceID", "SVC01_08_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC02_MonetaryAmount", "SVC02_MonetaryAmount");
                    bulkCopy.ColumnMappings.Add("SVC03_MonetaryAmount", "SVC03_MonetaryAmount");
                    bulkCopy.ColumnMappings.Add("SVC04_ProductORServiceID", "SVC04_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC05_Quantity", "SVC05_Quantity");
                    bulkCopy.ColumnMappings.Add("SVC06_01_ProductORServiceIDQualifier", "SVC06_01_ProductORServiceIDQualifier");
                    bulkCopy.ColumnMappings.Add("SVC06_02_ProductORServiceID", "SVC06_02_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC06_03_ProcedureModifier", "SVC06_03_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_04_ProcedureModifier", "SVC06_04_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_05_ProcedureModifier", "SVC06_05_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_06_ProcedureModifier", "SVC06_06_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_07_Description", "SVC06_07_Description");
                    bulkCopy.ColumnMappings.Add("SVC06_08_ProductORServiceID", "SVC06_08_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC07_Quantity", "SVC07_Quantity");
                    bulkCopy.WriteToServer(dtSVC);
                }

                if (dtREF != null && dtREF.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_REF";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("REF_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("REF_STID", "STID");
                    bulkCopy.ColumnMappings.Add("REF_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("REF_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("REF_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("REFID", "REFID");
                    bulkCopy.ColumnMappings.Add("REF01_ReferenceIdentificationQualifier", "REF01_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF02_ReferenceIdentification", "REF02_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("REF03_Description", "REF03_Description");
                    bulkCopy.ColumnMappings.Add("REF04_01_ReferenceIdentificationQualifier", "REF04_01_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_02_ReferenceIdentification", "REF04_02_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("REF04_03_ReferenceIdentificationQualifier", "REF04_03_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_04_ReferenceIdentification", "REF04_04_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("REF04_05_ReferenceIdentificationQualifier", "REF04_05_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_06_ReferenceIdentification", "REF04_06_ReferenceIdentification");
                    bulkCopy.WriteToServer(dtREF);
                }

                if (dtAMT != null && dtAMT.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "EDIRequest_276Segment_AMT";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("AMT_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("AMT_STID", "STID");
                    bulkCopy.ColumnMappings.Add("AMT_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("AMT_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("AMTID", "AMTID");
                    bulkCopy.ColumnMappings.Add("AMT01_AmountQualifierCode", "AMT01_AmountQualifierCode");
                    bulkCopy.ColumnMappings.Add("AMT02_MonetaryAmount", "AMT02_MonetaryAmount");
                    bulkCopy.ColumnMappings.Add("AMT03_CreditDebitFlagCode", "AMT03_CreditDebitFlagCode");
                    bulkCopy.WriteToServer(dtAMT);
                }

                if (dtDTP != null && dtDTP.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_DTP";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("DTP_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("DTP_STID", "STID");
                    bulkCopy.ColumnMappings.Add("DTP_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("DTP_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("DTP_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("DTPID", "DTPID");
                    bulkCopy.ColumnMappings.Add("DTP01_DateTimeQualifier", "DTP01_DateTimeQualifier");
                    bulkCopy.ColumnMappings.Add("DTP02_DateTimePeriodFormatQualifier", "DTP02_DateTimePeriodFormatQualifier");
                    bulkCopy.ColumnMappings.Add("DTP03_DateTimePeriod", "DTP03_DateTimePeriod");
                    bulkCopy.WriteToServer(dtDTP);
                }
                bulkCopy.Close();
                bulkCopy = null;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "276 Request Parse", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (conn != null) { conn.Dispose(); conn = null; }
                bulkCopy = null;

            }
            return true;
        }

        #endregion

        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    _MessageBoxCaption = string.Empty;
                    _DataBaseConnectionString = string.Empty;
                    _EDIDataBaseConnectionString = string.Empty;
                    ClaimNumber = string.Empty;

                    if (oDB != null)
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDBPara != null)
                    {
                        oDBPara.Dispose();
                        oDBPara = null;
                    }

                    oEdiDoc = null;
                    oSchemas = null;
                    oInterchange = null;
                    oGroup = null;
                    oTransactionSet = null;
                    oSegment = null;

                    seffilepath_276_005010X214_SemRef = string.Empty;
                    //if (oTriarqClaim != null)
                    //{
                    //    oTriarqClaim.Dispose();
                    //    oTriarqClaim = null;
                    //}

                    sPath = string.Empty;
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

    public class cls_EDI276_RequestProcessing_V2
    {
        private string _MessageBoxCaption;
        private string _DataBaseConnectionString;
        private string _EDIDataBaseConnectionString;
       
        private gloDatabaseLayer.DBLayer oDB;
        private gloDatabaseLayer.DBParameters oDBPara;
        
        ediDocument oEdiDoc;
        ediSchemas oSchemas;
        ediInterchange oInterchange = null;
        ediGroup oGroup = null;
        ediTransactionSet oTransactionSet = null;
        ediDataSegment oSegment = null;
        
        string ClaimNumber = string.Empty;
        string AUSID = string.Empty;
       
        string seffilepath_276_005010X214_SemRef = string.Empty;
        string sPath = AppDomain.CurrentDomain.BaseDirectory;
        string x276RequestFilePath=string.Empty;
        string TempFolderpath = gloSettings.FolderSettings.AppTempFolderPath + "EDIFiles";

        Cls_276X212_ISA o276_ISA;
        Cls_276X212_GS o276_GS;
        Cls_276X212_ST o276_ST;
        Cls_276X212_BHT o276_BHT;
        Cls_276X212_HL o276_HL;
        Cls_276X212_NM o276_NM;
        Cls_276X212_DMG o276_DMG;
        Cls_276X212_TRN o276_TRN;
        Cls_276X212_REF o276_REF;
        Cls_276X212_AMT o276_AMT;
        Cls_276X212_DTP o276_DTP;
        Cls_276X212_SVC o276_SVC;

        long ERAFileID = 0;
        long ISAID = 0;

        long CurrentSTID = 0;
        long CurrentHLId = 0;
        long CurrentTRNId = 0;
        long CurrentSVCId = 0;

        long SourceHLId = 0;
        long ReceiverHLId = 0;
        long ProviderHLId = 0;
        long SubscriberHLId = 0;
        long DependentHLId = 0;
                   
        int UniqueIdCounter = 0;
        int SegmentCounter = 0;

        DataTable dtUniqueIDs = null;
        DataTable dtISA, dtGS, dtST, dtBHT, dtHL, dtNM, dtDMG, dtAMT, dtTRN, dtSVC, dtSTC, dtREF, dtDTP = null;

        private enum Segment
        {
            ISA = 1,
            GS = 2,
            ST = 3,
            BHT = 4,
            HL = 5,
            NM = 6,
            DMG = 7,
            AMT = 8,
            TRN = 9,
            SVC = 10,            
            REF = 11,
            DTP = 12
        }

        DataTable dtClearingHouse = null;
        DataTable dtSubmitter = null;
        DataTable dtEDISetting = null;
        DataTable dtAlphaSetting = null;
        DataTable dtClaimData = null;
        DataTable dtClaimLines = null;
        DataTable dtClaimPatient = null;
        DataTable dtClaimClinic = null;
        DataTable dtPatientInsurance = null;
        DataTable dtFacility = null;
        DataTable dtBillingProvider = null;
        DataTable dtRefferingProvider = null;
        DataTable dtRenderingProvider = null;
        DataTable dtMidLevelID = null;
        DataTable dtBillingProviderTaxonomy = null;
        DataTable dtMasterSetting = null;


        public cls_EDI276_RequestProcessing_V2(string ConnString = "")
        {
            _DataBaseConnectionString = ConnString;
            if (_DataBaseConnectionString == "")
            {
                _DataBaseConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[1].ToString();
            }
        }

        public List<TRIARQClaim> FillTriarqClaimData(List<string> ClaimNumbers)
        {
            DataSet dsClaimData = null;
            DataSet dsMasterEDIData = null;
            DataSet dsClearingHouse = null;
            TRIARQClaim oTriarqClaim;
            List<TRIARQClaim> oTriarqClaims = null;
            string InValidClaimNumbers = "";
            string ValidClaimMumbers = "";
            try
            {
                if (ClaimNumbers.Count > 0)
                {
                    oTriarqClaims = new List<TRIARQClaim>();

                    foreach (string ClaimNumber in ClaimNumbers)
                    {
                        dsClaimData = new DataSet();
                        dsMasterEDIData = new DataSet();
                        dsClearingHouse = new DataSet();
                        oTriarqClaim = new TRIARQClaim();

                        dtClearingHouse = null;
                        dtSubmitter = null;
                        dtEDISetting = null;
                        dtAlphaSetting = null;
                        dtClaimData = null;
                        dtClaimLines = null;
                        dtClaimPatient = null;
                        dtClaimClinic = null;
                        dtPatientInsurance = null;
                        dtFacility = null;
                        dtBillingProvider = null;
                        dtRefferingProvider = null;
                        dtRenderingProvider = null;
                        dtMidLevelID = null;
                        dtBillingProviderTaxonomy = null;
                        dtMasterSetting = null;
                        
                        dsClearingHouse = GetClearingHouseData(ClaimNumber);
                        dtClearingHouse = dsClearingHouse.Tables["ClearingHouse"];
                        dtSubmitter = dsClearingHouse.Tables["Submitter"];
                        dtEDISetting = dsClearingHouse.Tables["EDISetting"];
                        dtAlphaSetting = dsClearingHouse.Tables["AlphaSetting"];

                        #region Request Header Data

                        if (dsClearingHouse.Tables["ClearingHouse"] != null)
                        {
                            if (dsClearingHouse.Tables["ClearingHouse"].Rows.Count > 0)
                            {
                                oTriarqClaim.RequestHeader = new TRIARQ276RequestHeader();
                                oTriarqClaim.RequestHeader.ReceiverID = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sReceiverID"]);
                                oTriarqClaim.RequestHeader.SubmitterID = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sSubmitterID"]);
                                oTriarqClaim.RequestHeader.SenderCode = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sSenderCode"]);
                                oTriarqClaim.RequestHeader.VenderIDCode = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sVenderIDCode"]);
                                oTriarqClaim.RequestHeader.ClearingHouseCode = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sClearingHouseCode"]);

                                if (Convert.ToInt32(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["nTypeOfData"]) == 0 || Convert.ToInt32(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["nTypeOfData"]) == 1)
                                {
                                    oTriarqClaim.RequestHeader.TypeOfData = "T";
                                }
                                else if (Convert.ToInt32(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["nTypeOfData"]) == 2)
                                {
                                    oTriarqClaim.RequestHeader.TypeOfData = "P";
                                }

                                oTriarqClaim.RequestHeader.SenderQualifier = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sSenderIDQualifier"]);
                                oTriarqClaim.RequestHeader.ReceiverQualifier = Convert.ToString(dsClearingHouse.Tables["ClearingHouse"].Rows[0]["sReceiverIDQualifier"]);
                            }
                        }

                        #endregion

                        dsClaimData = GetClaimData(ClaimNumber);
                        dtClaimData = dsClaimData.Tables["ClaimData"];
                        dtClaimLines = dsClaimData.Tables["ClaimLines"];
                        dtClaimPatient = dsClaimData.Tables["ClaimPatient"];
                        dtClaimClinic = dsClaimData.Tables["ClaimClinic"];


                        #region "Claim Master Data "
                        oTriarqClaim.MasterTransactionID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionMasterID"]);
                        oTriarqClaim.TransactionID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionID"]);
                        oTriarqClaim.PatientID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nPatientID"]);
                        oTriarqClaim.ClaimNo = Convert.ToString(dsClaimData.Tables["ClaimData"].Rows[0]["nClaimNo"]);
                        oTriarqClaim.SubClaimNo = Convert.ToString(dsClaimData.Tables["ClaimData"].Rows[0]["nSubClaimNo"]);
                        oTriarqClaim.TransactionDate = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionDate"]));
                        oTriarqClaim.ProviderID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionProviderID"]);
                        oTriarqClaim.IsSameAsBillingProvider = Convert.ToBoolean(dsClaimData.Tables["ClaimData"].Rows[0]["bSameAsBillingProvider"]);
                        oTriarqClaim.ReferalProviderID_New = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nReferralProviderID"]);
                        oTriarqClaim.ResponsibilityNo = Convert.ToInt16(dsClaimData.Tables["ClaimData"].Rows[0]["nResponsibilityNo"]);
                        oTriarqClaim.ClinicID = Convert.ToInt16(dsClaimData.Tables["ClaimData"].Rows[0]["ClinicID"]);
                        oTriarqClaim.ContactID = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nContactID"]);
                        oTriarqClaim.FacilityCode = Convert.ToString(dsClaimData.Tables["ClaimData"].Rows[0]["sFacilityCode"]);
                        oTriarqClaim.FromDOS = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionDate"]);
                        oTriarqClaim.ToDOS = Convert.ToInt64(dsClaimData.Tables["ClaimData"].Rows[0]["nTransactionDate"]);
                        oTriarqClaim.TotalClaimAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimData"].Rows[0]["ClaimTotalCharges"]);
                        #endregion

                        #region "Claim Line Data"

                        oTriarqClaim.ClaimServiceLines = new List<ServiceLine>();

                        if (dsClaimData.Tables["ClaimLines"] != null)
                        {
                            if (dsClaimData.Tables["ClaimLines"].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsClaimData.Tables["ClaimLines"].Rows.Count; i++)
                                {
                                    ServiceLine oServiceline = new ServiceLine();
                                    oServiceline.LineID = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nTransactionDetailID"]);
                                    oServiceline.LineNo = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nTransactionLineNo"]);

                                    oServiceline.FromDOS = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nFromDate"]));
                                    if (dsClaimData.Tables["ClaimLines"].Rows[i]["nToDate"] != null && Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nToDate"]) > 0)
                                    {
                                        oServiceline.ToDOS = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nToDate"]));
                                    }
                                    else
                                    {
                                        oServiceline.DateServiceTillIsNull = true;
                                        oServiceline.ToDOS = clsGeneral.DateAsDate(Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nFromDate"]));
                                    }
                                    oServiceline.POSCode = dsClaimData.Tables["ClaimLines"].Rows[i]["sPOSCode"].ToString();
                                    oServiceline.POSDesc = "";
                                    oServiceline.TOSCode = "";
                                    oServiceline.TOSDesc = "";
                                    oServiceline.CPT = dsClaimData.Tables["ClaimLines"].Rows[i]["sCPTCode"].ToString();
                                    oServiceline.CPTDescription = dsClaimData.Tables["ClaimLines"].Rows[i]["sCPTDescription"].ToString();
                                    oServiceline.CrosswalkCPTCode = dsClaimData.Tables["ClaimLines"].Rows[i]["sCrossWalkCPTCode"].ToString();
                                    oServiceline.Modifier1 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod1Code"].ToString();
                                    oServiceline.Modifier2 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod2Code"].ToString();
                                    oServiceline.Modifier3 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod3Code"].ToString();
                                    oServiceline.Modifier4 = dsClaimData.Tables["ClaimLines"].Rows[i]["sMod4Code"].ToString();
                                    oServiceline.ChargeAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dCharges"]);
                                    oServiceline.BilledAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dBilliedAmount"]);
                                    oServiceline.Quantity = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dUnit"]);
                                    oServiceline.TotalChargeAmount = Convert.ToDecimal(dsClaimData.Tables["ClaimLines"].Rows[i]["dTotal"]);
                                    oServiceline.RenderingProviderId = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nProvider"]);
                                    oServiceline.RenderingProvider = new Provider();
                                    oServiceline.RenderingProvider.FirstName = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sFirstName"]);
                                    oServiceline.RenderingProvider.MiddleName = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sMiddleName"]);
                                    oServiceline.RenderingProvider.LastName = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sLastname"]);
                                    oServiceline.RenderingProvider.ID = Convert.ToInt64(dsClaimData.Tables["ClaimLines"].Rows[i]["nProvider"]);
                                    oServiceline.RenderingProvider.NPI = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sNPI"]);
                                    oServiceline.RenderingProvider.Suffix = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sSuffix"]);
                                    oServiceline.RenderingProvider.TIN = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sTaxID"]);
                                    oServiceline.RenderingProvider.Taxonomy = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sTaxonomy"]);
                                    oServiceline.RenderingProvider.Address = new Address();
                                    oServiceline.RenderingProvider.Address.AddressLine1 = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessAddressLine1"]);
                                    oServiceline.RenderingProvider.Address.AddressLine2 = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessAddressLine2"]);
                                    oServiceline.RenderingProvider.Address.City = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessCity"]);
                                    oServiceline.RenderingProvider.Address.State = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessState"]);
                                    oServiceline.RenderingProvider.Address.Zip = Convert.ToString(dsClaimData.Tables["ClaimLines"].Rows[i]["sBusinessZip"]);

                                    oTriarqClaim.ClaimServiceLines.Add(oServiceline);
                                    oServiceline.Dispose();
                                    oServiceline = null;
                                }
                            }
                        }
                        #endregion

                        #region "Claim Patient"
                        if (dsClaimData.Tables["ClaimPatient"] != null)
                        {
                            if (dsClaimData.Tables["ClaimPatient"].Rows.Count > 0)
                            {
                                oTriarqClaim.Patient = new Patient();
                                oTriarqClaim.Patient.PatientID = oTriarqClaim.PatientID;
                                oTriarqClaim.Patient.Code = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientCode"]);
                                oTriarqClaim.Patient.FirstName = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientFirstName"]);
                                oTriarqClaim.Patient.MiddleName = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientMiddleName"]);
                                oTriarqClaim.Patient.LastName = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientLastName"]);
                                oTriarqClaim.Patient.Suffix = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientSuffix"]);
                                oTriarqClaim.Patient.Gender = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientGender"]);
                                oTriarqClaim.Patient.DOB = Convert.ToDateTime(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientDOB"]);
                                oTriarqClaim.Patient.PharmacyNumber = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["Pharmacy"]);
                                oTriarqClaim.Patient.Address = new Address();
                                oTriarqClaim.Patient.Address.AddressLine1 = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientAddress1"]);
                                oTriarqClaim.Patient.Address.AddressLine2 = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientAddress2"]);
                                oTriarqClaim.Patient.Address.City = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientCity"]); ;
                                oTriarqClaim.Patient.Address.State = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientState"]); ;
                                oTriarqClaim.Patient.Address.Country = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientCountry"]); ;
                                oTriarqClaim.Patient.Address.Zip = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["PatientZip"]); ;
                                oTriarqClaim.Patient.Address.AreaCode = Convert.ToString(dsClaimData.Tables["ClaimPatient"].Rows[0]["AreaCode"]); ;
                            }
                        }
                        #endregion

                        #region Claim Clinic

                        if (dsClaimData.Tables["ClaimClinic"] != null)
                        {
                            if (dsClaimData.Tables["ClaimClinic"].Rows.Count > 0)
                            {
                                oTriarqClaim.ClaimClinic = new Clinic();
                                oTriarqClaim.ClaimClinic.ID = Convert.ToInt64(dsClaimData.Tables["ClaimClinic"].Rows[0]["nClinicID"]);
                                oTriarqClaim.ClaimClinic.AUSID = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["AUSID"]);
                                oTriarqClaim.ClaimClinic.Name = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sClinicName"]);
                                oTriarqClaim.ClaimClinic.NPI = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sClinicNPI"]); ;
                                oTriarqClaim.ClaimClinic.TIN = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sTaxId"]);
                                oTriarqClaim.ClaimClinic.Taxonomy = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sTaxonomyCode"]);
                                oTriarqClaim.ClaimClinic.Address = new Address();
                                oTriarqClaim.ClaimClinic.Address.AddressLine1 = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sAddress1"]);
                                oTriarqClaim.ClaimClinic.Address.AddressLine2 = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sStreet"]);
                                oTriarqClaim.ClaimClinic.Address.City = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sCity"]);
                                oTriarqClaim.ClaimClinic.Address.State = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sState"]);
                                oTriarqClaim.ClaimClinic.Address.Zip = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sZip"]);
                                oTriarqClaim.ClaimClinic.Address.Country = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sCountry"]);
                                oTriarqClaim.ClaimClinic.Address.AreaCode = Convert.ToString(dsClaimData.Tables["ClaimClinic"].Rows[0]["sAreaCode"]);
                            }
                        }
                        #endregion


                        if (oTriarqClaim != null)
                        {
                            if (oTriarqClaim.ClaimServiceLines.Count > 0)
                            {
                                dsMasterEDIData = GetMasterEDIData(oTriarqClaim);

                                dtMidLevelID = dsMasterEDIData.Tables["MidLevelID"];  //Rendering ProviderId,Billing Provider Id
                                dtPatientInsurance = dsMasterEDIData.Tables["PatientInsurance"];
                                dtFacility = dsMasterEDIData.Tables["Facility"];
                                dtBillingProvider = dsMasterEDIData.Tables["BillingProvider"];
                                dtRefferingProvider = dsMasterEDIData.Tables["RefferingProvider"];
                                dtRenderingProvider = dsMasterEDIData.Tables["RenderingProvider"];
                                dtBillingProviderTaxonomy = dsMasterEDIData.Tables["BillingProviderTaxonomy"];
                                dtMasterSetting = dsMasterEDIData.Tables["MasterSetting"];

                                #region "Responsible Party"
                                if (dsMasterEDIData.Tables["PatientInsurance"] != null)
                                {
                                    if (dsMasterEDIData.Tables["PatientInsurance"].Rows.Count > 0)
                                    {
                                        oTriarqClaim.ResponsibleParty = new Insurance();
                                        oTriarqClaim.ResponsibleParty.ContactId = Convert.ToInt64(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["nContactID"]);
                                        oTriarqClaim.ResponsibleParty.PayerID = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["PayerID"]);
                                        oTriarqClaim.ResponsibleParty.InsuranceID = Convert.ToInt64(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceID"]);
                                        oTriarqClaim.ResponsibleParty.InsuranceName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceName"]);
                                        oTriarqClaim.ResponsibleParty.PolicyNumber = "";
                                        oTriarqClaim.ResponsibleParty.GroupNumber = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["sGroup"]); ;
                                        oTriarqClaim.ResponsibleParty.InsuranceFlag = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceFlag"]); ;
                                        oTriarqClaim.ResponsibleParty.InsuranceTypeCode = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceTypeCode"]);
                                        oTriarqClaim.ResponsibleParty.InsuranceTypeDesc = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeDescriptionDefault"]); ;
                                        oTriarqClaim.ResponsibleParty.TypeOfBilling = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["BillingType"]);
                                        oTriarqClaim.ResponsibleParty.BillingTypeId = Convert.ToInt64(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["BillingTypeId"]);
                                        oTriarqClaim.ResponsibleParty.InsTypeCodeDefault = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeCodeDefault"]); ;
                                        oTriarqClaim.ResponsibleParty.InsTypeDescriptionDefault = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsuranceTypeDesc"]); ;
                                        oTriarqClaim.ResponsibleParty.InsTypeCodeMedicare = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeCodeMedicare"]);
                                        oTriarqClaim.ResponsibleParty.InsTypeDescriptionMedicare = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["InsTypeDescriptionMedicare"]);

                                        oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode = Convert.ToInt32(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["RelationshipCode"]);
                                        oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipDesc = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["Relationship"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberDOB = Convert.ToDateTime(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["dtDOB"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberFName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubFName"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberMName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubMName"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberLName = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubLName"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberSuffix = "";
                                        oTriarqClaim.ResponsibleParty.SubscriberGender = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberGender"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress = new Address();
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.AddressLine1 = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberAddr1"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.AddressLine2 = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberAddr2"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.City = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberCity"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.State = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberState"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.Country = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberCountryCode"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.Zip = Convert.ToString(dsMasterEDIData.Tables["PatientInsurance"].Rows[0]["SubscriberZip"]);
                                        oTriarqClaim.ResponsibleParty.SubscriberAddress.AreaCode = "";

                                    }
                                }
                                #endregion

                                #region "Billing Provider"
                                if (dsMasterEDIData.Tables["BillingProvider"] != null)
                                {
                                    if (dsMasterEDIData.Tables["BillingProvider"].Rows.Count > 0)
                                    {
                                        oTriarqClaim.BillingProvider = new Provider();
                                        oTriarqClaim.BillingProvider.FirstName = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["FirstName"]);
                                        oTriarqClaim.BillingProvider.MiddleName = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["MiddleName"]);
                                        oTriarqClaim.BillingProvider.LastName = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["LastName"]);
                                        oTriarqClaim.BillingProvider.Suffix = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["sSuffix"]);
                                        oTriarqClaim.BillingProvider.TIN = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["SecondaryQualifierValue"]);
                                        oTriarqClaim.BillingProvider.NPI = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PrimaryQualifierValue"]);
                                        oTriarqClaim.BillingProvider.Taxonomy = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["Taxonomy"]);
                                        oTriarqClaim.BillingProvider.EntityType = Convert.ToInt16(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["EntityType"]);
                                        oTriarqClaim.BillingProvider.Address = new Address();
                                        oTriarqClaim.BillingProvider.Address.AddressLine1 = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyAddline1"]);
                                        oTriarqClaim.BillingProvider.Address.AddressLine2 = "";
                                        oTriarqClaim.BillingProvider.Address.City = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyCity"]);
                                        oTriarqClaim.BillingProvider.Address.State = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyState"]);
                                        oTriarqClaim.BillingProvider.Address.Zip = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyZIP"]);
                                        oTriarqClaim.BillingProvider.Address.Country = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["CountryCode"]);
                                        oTriarqClaim.BillingProvider.Address.AreaCode = Convert.ToString(dsMasterEDIData.Tables["BillingProvider"].Rows[0]["PhyAreaCode"]);
                                    }
                                }
                                #endregion

                                #region "Facility"
                                if (dsMasterEDIData.Tables["Facility"] != null)
                                {
                                    if (dsMasterEDIData.Tables["Facility"].Rows.Count > 0)
                                    {
                                        oTriarqClaim.ClaimFacility = new Facility();
                                        oTriarqClaim.ClaimFacility.Type = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["SecondaryQualifierId"]);
                                        oTriarqClaim.ClaimFacility.Name = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["LastName"]);
                                        oTriarqClaim.ClaimFacility.NPI = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["PrimaryQualifierValue"]);
                                        oTriarqClaim.ClaimFacility.TIN = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["SecondaryQualifierValue"]);
                                        oTriarqClaim.ClaimFacility.Taxonomy = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Taxonomy"]);
                                        oTriarqClaim.ClaimFacility.EntityType = Convert.ToInt16(dsMasterEDIData.Tables["Facility"].Rows[0]["EntityType"]);
                                        oTriarqClaim.ClaimFacility.POSCode = "";
                                        oTriarqClaim.ClaimFacility.POSDesc = "";
                                        oTriarqClaim.ClaimFacility.Address = new Address();
                                        oTriarqClaim.ClaimFacility.Address.AddressLine1 = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Address1"]);
                                        oTriarqClaim.ClaimFacility.Address.AddressLine2 = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Address2"]);
                                        oTriarqClaim.ClaimFacility.Address.City = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["City"]);
                                        oTriarqClaim.ClaimFacility.Address.State = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["State"]);
                                        oTriarqClaim.ClaimFacility.Address.Country = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["CountryCode"]);
                                        oTriarqClaim.ClaimFacility.Address.Zip = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["Zip"]);
                                        oTriarqClaim.ClaimFacility.Address.AreaCode = Convert.ToString(dsMasterEDIData.Tables["Facility"].Rows[0]["AreaCode"]);
                                    }
                                }
                                #endregion
                            }
                        }

                        if (ValidateTriarqClaimData(oTriarqClaim))
                        {
                            oTriarqClaims.Add(oTriarqClaim);
                            if (ValidClaimMumbers == "")
                            {
                                ValidClaimMumbers = ClaimNumber;
                            }
                            else
                            {
                                ValidClaimMumbers = ValidClaimMumbers + "," + ClaimNumber;
                            }
                        }
                        else
                        {
                            if (InValidClaimNumbers == "")
                            {
                                InValidClaimNumbers = ClaimNumber;
                            }
                            else
                            {
                                InValidClaimNumbers = InValidClaimNumbers + "," + ClaimNumber;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FillClaimModel, ex, ActivityOutCome.Failure);
            }

            return oTriarqClaims;

        }

        private DataSet GetClearingHouseData(string ClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsClearingHouse = null;
            try
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Started fetching Claim clearing house.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                oDB.Connect(false);
                oDBParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_SELECT_ClearingHouse_EDI_276", oDBParameters, out dsClearingHouse);
                oDB.Disconnect();
                dsClearingHouse.Tables[0].TableName = "ClearingHouse";
                dsClearingHouse.Tables[1].TableName = "Submitter";
                dsClearingHouse.Tables[2].TableName = "EDISetting";
                dsClearingHouse.Tables[3].TableName = "AlphaSetting";

                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Completed fetching Claim clearing house.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                dsClearingHouse = null;
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                dsClearingHouse = null;
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Exception while Fetching Claim clearing house.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {

                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dsClearingHouse;
        }

        private DataSet GetClaimData(string ClaimNumber)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsTransaction = null;
            try
            {
                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Started fetching Claim Data.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                oDB.Connect(false);
                oDBParameters.Add("@ClaimNumber", ClaimNumber, ParameterDirection.Input, SqlDbType.VarChar);
                oDB.Retrive("BL_SELECT_Claim_EDI_276", oDBParameters, out dsTransaction);
                oDB.Disconnect();
                dsTransaction.Tables[0].TableName = "ClaimData";
                dsTransaction.Tables[1].TableName = "ClaimLines";
                dsTransaction.Tables[3].TableName = "ClaimPatient";
                dsTransaction.Tables[5].TableName = "ClaimClinic";
                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Completed fetching Claim Data.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                dsTransaction = null;
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                dsTransaction = null;
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Exception while Fetching Claim Data.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {

                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dsTransaction;
        }

        private DataSet GetMasterEDIData(TRIARQClaim oTriarqClaim)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet dsMasterEDI = null;
            try
            {
                //  clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Started fetching Claim Master Data.", ActivityOutCome.Started, "", ActivityReference.ClaimNumber, ClaimNumber);
                oDBParameters.Add("@nRenderingProviderID", oTriarqClaim.ClaimServiceLines[0].RenderingProviderId, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nBillingProviderID", oTriarqClaim.ProviderID, System.Data.ParameterDirection.Input, System.Data.SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", oTriarqClaim.ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nResponsibilityNo", oTriarqClaim.ResponsibilityNo, ParameterDirection.Input, SqlDbType.Int);
                oDBParameters.Add("@nTransMasterID", oTriarqClaim.MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nFacilityID", oTriarqClaim.FacilityCode, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", oTriarqClaim.ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@bIsPhysician ", oTriarqClaim.IsSameAsBillingProvider, ParameterDirection.Input, SqlDbType.Bit);
                oDBParameters.Add("@TransactionID", oTriarqClaim.TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@RefferingProvider", oTriarqClaim.ReferalProviderID_New, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@IsSecondary", false, ParameterDirection.Input, SqlDbType.Bit);
                oDB.Connect(false);
                oDB.Retrive("BL_SELECT_Master_EDI_276_5010", oDBParameters, out dsMasterEDI);
                oDB.Disconnect();
                dsMasterEDI.Tables[0].TableName = "MidLevelID";
                dsMasterEDI.Tables[1].TableName = "PatientInsurance";
                dsMasterEDI.Tables[2].TableName = "Facility";
                dsMasterEDI.Tables[3].TableName = "BillingProvider";
                dsMasterEDI.Tables[4].TableName = "RefferingProvider";
                dsMasterEDI.Tables[5].TableName = "RenderingProvider";
                dsMasterEDI.Tables[6].TableName = "BillingProviderTaxonomy";
                dsMasterEDI.Tables[7].TableName = "MasterSetting";
                // clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Exception while Completed fetching Claim Data.", ActivityOutCome.Complete, "", ActivityReference.ClaimNumber, ClaimNumber);
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                dsMasterEDI = null;
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                dsMasterEDI = null;
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.ClaimModel, ActivityType.FetchDatabase, "Fetching Claim Data.", ActivityOutCome.Failure, "", ActivityReference.ClaimNumber, ClaimNumber);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {

                oDBParameters.Dispose();
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
            return dsMasterEDI;
        }

        public bool ValidateTriarqClaimData(TRIARQClaim oTriarqClaim)
        {
            bool ValidClaim = true;

            string _Message = string.Empty;
            string strMissingText = string.Empty;
            string _MessageHeader = string.Empty;
            string strMessage = string.Empty;
            string _ClaimMessageHeader = string.Empty;

            bool _IsClaimNumberAdded = false;
            string strBillingSetting = string.Empty;
            string PrimaryBillingProviderID = string.Empty;

            string _FilePath = Application.StartupPath;

            ClsEDIValidation Edisetting = new ClsEDIValidation();
            ClsAlphaValidation AlphaSetting = new ClsAlphaValidation();

            Edisetting.GetEDIValidation(dtEDISetting);
            AlphaSetting.GetAlphaValidation(dtAlphaSetting);


            _MessageHeader += "";

            #region "Alpha Settings"
            if (AlphaSetting.ClaimValidationSetting == "None")
            {
                if (AlphaSetting.ShowMessageForValidation == true)
                {
                    MessageBox.Show("You have not selected any validation setting, claims may go with invalid data.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else if (AlphaSetting.ClaimValidationSetting == "Alpha2")
            {
                if (!ValidateConnectionString(AlphaSetting.AlphaAuthentication, AlphaSetting.AlphaServerName, AlphaSetting.AlphaDatabaseName, AlphaSetting.AlphaUserName, AlphaSetting.AlphaPassword))
                {
                    MessageBox.Show("Connection for Alpha II cannot be establish, please do the setting from gloPM Admin.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }
            #endregion


            if (dtClearingHouse == null || dtClearingHouse.Rows.Count < 1)
            {
                MessageBox.Show("Clearing House information is not present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (oTriarqClaim != null)
            {
                if (dtSubmitter == null || dtSubmitter.Rows.Count < 1)
                {
                    MessageBox.Show("Submitter/Provider information is not present.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }



            #region " Clearing House "
            //ISA and GS Settings

            if (Convert.ToString(dtClearingHouse.Rows[0]["sSubmitterID"]).Trim() == "")
            {
                if (Edisetting.SenderID == true)
                    strMissingText += "Sender ID" + Environment.NewLine + "" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtClearingHouse.Rows[0]["sReceiverID"]).Trim() == "")
            {
                if (Edisetting.ReceiverID == true)
                    strMissingText += "Receiver ID" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtClearingHouse.Rows[0]["sSenderCode"]).Trim() == "")
            {
                if (Edisetting.SenderCode == true)
                    strMissingText += "Sender Code" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtClearingHouse.Rows[0]["sVenderIDCode"]).Trim() == "")
            {
                if (Edisetting.ReceiverCode == true)
                    strMissingText += "Receiver Code" + Environment.NewLine + "";
            }
            #endregion " Clearing House "

            #region " Submitter "
            //Submitter


            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterName"]).Trim() == "")
            {
                if (Edisetting.SubmitterName == true)
                    strMissingText += "Submitter Name" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterContactName"]).Trim() == "")
            {
                if (Edisetting.SubmitterContactName == true)
                    strMissingText += "Submitter Contact Person Name" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterPhone"]).Trim() == "")
            {
                if (Edisetting.SubmitterPhone == true)
                    strMissingText += "Submitter Contact Person Number" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterCity"]).Trim() == "")
            {
                if (Edisetting.SubscriberCity == true)
                    strMissingText += "Submitter City" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterState"]).Trim() == "")
            {
                if (Edisetting.SubmitterState == true)
                    strMissingText += "Submitter State" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterZIP"]).Trim() == "")
            {
                if (Edisetting.SubmitterZIP == true)
                    strMissingText += "Submitter Zip" + Environment.NewLine + "";
            }
            if (Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress1"]).Trim() + " " + Convert.ToString(dtSubmitter.Rows[0]["SubmitterAddress2"]).Trim() == "")
            {
                if (Edisetting.SubmitterAddress1 == true)
                    strMissingText += "Submitter Address" + Environment.NewLine + "";
            }
            #endregion " Submitter "


            if (strMissingText.Trim() != "")
            {
                _MessageHeader = _MessageHeader + strMissingText;
            }
            else
            {
                _MessageHeader = "";
            }

            #region " Patient Information "

            //Patient Information
            if (oTriarqClaim.Patient != null)
            {
                //if (Convert.ToString(oTransaction.ClaimNumber).Trim() == "")
                //{
                //    strMessage += "Patient Account No" + Environment.NewLine + "";
                //}
                if (oTriarqClaim.Patient.LastName.Trim() == "")
                {
                    //if (GetValidationFieldsSettings("Patient Last Name"))
                    strMessage += "Patient Last Name" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.FirstName.Trim() == "")
                {
                    if (Edisetting.PatientFirstName == true)
                        strMessage += "Patient First Name" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.MiddleName.Trim() == "")
                {
                    if (Edisetting.PatientMiddleName == true)
                        strMessage += "Patient Middle Name" + Environment.NewLine + "";
                }
                //if (oTransaction.PatientSSN.Trim() == "")
                //{
                //    if (Edisetting.PatientSSN == true)
                //        strMessage += "Patient SSN" + Environment.NewLine + "";
                //}
                if (oTriarqClaim.Patient.Gender.Trim() == "")
                {
                    // if (GetValidationFieldsSettings("Patient Gender"))
                    strMessage += "Patient Gender" + Environment.NewLine + "";
                }
                if (Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.Patient.DOB.ToShortDateString())).Trim() == "")
                {
                    //if (GetValidationFieldsSettings("Patient Date of Birth"))
                    strMessage += "Patient Date of Birth" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.Address.AddressLine1.Trim() == "")
                {
                    if (Edisetting.PatientAddress == true)
                        strMessage += "Patient Address line 1" + Environment.NewLine + "";
                }

                //if (oTriarqClaim.Patient.Address.AddressLine2.Trim() == "")
                //{
                //    if (Edisetting.PatientAddress == true)
                //        strMessage += "Patient Address line 2" + Environment.NewLine + "";
                //}


                if (oTriarqClaim.Patient.Address.City.Trim() == "")
                {
                    //  if (GetValidationFieldsSettings("Patient City"))
                    strMessage += "Patient City" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.Address.State.Trim() == "")
                {
                    // if (GetValidationFieldsSettings("Patient State"))
                    strMessage += "Patient State" + Environment.NewLine + "";
                }
                if (oTriarqClaim.Patient.Address.Zip.Trim() == "")
                {
                    //   if (GetValidationFieldsSettings("Patient Zip"))
                    strMessage += "Patient Zip" + Environment.NewLine + "";
                }
            }
            else
            {
                MessageBox.Show("Patient information is not present for claim number " + oTriarqClaim.ClaimNo.ToString().Trim() + ".  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            #endregion " Patient Information "

            if (oTriarqClaim != null)
            {
                if (oTriarqClaim.ClaimServiceLines.Count > 0)
                {
                    if (dtFacility == null)
                    {
                        MessageBox.Show("Facility information is not present for claim number " + oTriarqClaim.ClaimNo.ToString().Trim() + ".  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return false;
                    }

                    if (Convert.ToInt64(oTriarqClaim.ProviderID) != 0 && oTriarqClaim.ProviderID.ToString() != "")
                    {
                        if (dtBillingProvider == null || dtBillingProvider.Rows.Count == 0)
                        {
                            MessageBox.Show("Provider information is not present.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifier"]).Trim() == "")// && IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " is using Billing Type " + dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + " which has no ID Qualifier Code. \n Batch will not send.  Please review Billing ID Qualifier Setup.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (Convert.ToString(dtBillingProvider.Rows[0]["PrimaryQualifierValue"]).ToString().Trim() == "")//&& IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " is using missing " + dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + ".\n Batch will not send.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (dtBillingProvider.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "" && dtBillingProvider.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "-1" && dtBillingProvider.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "0" && Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifiervalue"]) == "" && Convert.ToString(dtBillingProvider.Rows[0]["SecondaryQualifier"]).Trim() == "")//&& IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " is using " + dtBillingProvider.Rows[0]["Setting"].ToString().Trim() + " which has mismatch in source and other ID type.\nBatch will not send.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }

                        if (dtPatientInsurance == null || dtPatientInsurance.Rows.Count < 1)
                        {
                            if (_IsClaimNumberAdded == false)
                            {

                                if (Edisetting.SubscriberLastName == true)
                                    strMessage += "Subscriber Last Name" + Environment.NewLine + "";

                                //if (Edisetting.SubscriberRelationship  == true )
                                strMessage += "Subscriber Relationship" + Environment.NewLine + "";


                                if (Edisetting.PlanType == true)
                                    strMessage += "Plan Type" + Environment.NewLine + "";


                                if (Edisetting.SubscriberFirstName == true)
                                    strMessage += "Subscriber First Name" + Environment.NewLine + "";


                                //     if (GetValidationFieldsSettings("Subscriber Insurance ID"))
                                strMessage += "Insurance ID" + Environment.NewLine + "";


                                if (Edisetting.SubscriberAddress == true)
                                    strMessage += "Subscriber Address" + Environment.NewLine + "";

                                if (Edisetting.SubscriberGroupID == true)
                                    strMessage += "Subscriber Group ID" + Environment.NewLine + "";

                                if (Edisetting.SubscriberCity == true)
                                    strMessage += "Subscriber City" + Environment.NewLine + "";

                                if (Edisetting.SubmitterState == true)
                                    strMessage += "Subscriber State" + Environment.NewLine + "";

                                if (Edisetting.SubmitterZIP == true)
                                    strMessage += "Subscriber Zip" + Environment.NewLine + "";

                                //    if (GetValidationFieldsSettings("Subscriber Date of Birth"))
                                strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";

                                //   if (GetValidationFieldsSettings("Subscriber Gender"))
                                strMessage += "Subscriber Gender" + Environment.NewLine + "";

                                if (Edisetting.PayerName == true)
                                    strMessage += "Payer/Insurance Name" + Environment.NewLine + "";


                                if (Edisetting.PayerId == true)
                                    strMessage += "Payer ID" + Environment.NewLine + "";

                                if (Edisetting.PayerAddress == true)
                                    strMessage += "Payer Address" + Environment.NewLine + "";

                                if (Edisetting.PayerCity == true)
                                    strMessage += "Payer City" + Environment.NewLine + "";

                                if (Edisetting.PayerState == true)
                                    strMessage += "Payer State" + Environment.NewLine + "";

                                if (Edisetting.PayerZip == true)
                                    strMessage += "Payer Zip" + Environment.NewLine + "";

                                _IsClaimNumberAdded = true;
                            }
                        }
                    }

                    _ClaimMessageHeader = " " + Environment.NewLine + "For Patient: " + oTriarqClaim.Patient.FirstName.Trim() + " " + oTriarqClaim.Patient.LastName.Trim() + "  and Claim Number: " + oTriarqClaim.ClaimNo.Trim() + " " + Environment.NewLine + "" + Environment.NewLine + "";


                    string _strMessage1 = "";
                    if (AlphaSetting.IsCheckInvalidICD9 == true)
                    {
                        _strMessage1 = Convert.ToString(dtMasterSetting.Rows[0]["InvalidICD9"]);
                    }

                    if (_strMessage1.Trim() != "")
                    {
                        _strMessage1 = _strMessage1.Substring(0, _strMessage1.Length - 1);
                        strMessage += "Invalid ICD9's " + _strMessage1 + Environment.NewLine + "";
                    }

                }
                else
                {
                    MessageBox.Show("Transaction Lines are not there in selected transaction(s). ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            #region " Billing Provider "
            //Billing Provider
            if (dtBillingProvider != null || dtBillingProvider.Rows.Count > 0)
            {
                string _BillingAddress = "";
                string _BillingCity = "";
                string _BillingState = "";
                string _BillingZIP = "";
                string _BillingNPI = "";

                _BillingAddress = dtBillingProvider.Rows[0]["Address1"].ToString().Trim();
                _BillingCity = dtBillingProvider.Rows[0]["City"].ToString().Trim();
                _BillingState = dtBillingProvider.Rows[0]["State"].ToString().Trim();
                _BillingZIP = dtBillingProvider.Rows[0]["Zip"].ToString().Trim();
                _BillingNPI = dtBillingProvider.Rows[0]["PrimaryQualifierValue"].ToString().Trim();



                if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Facility"))
                {
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.FacilityName == true)
                            strMessage += "Facility Name" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.FacilityAddress1 == true)
                            strMessage += "Facility Address" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        //   if (GetValidationFieldsSettings("Facility City"))
                        strMessage += "Facility City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        // if (GetValidationFieldsSettings("Facility State"))
                        strMessage += "Facility State" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        //  if (GetValidationFieldsSettings("Facility Zip"))
                        strMessage += "Facility Zip" + Environment.NewLine + "";
                    }
                    if (_BillingNPI.Trim() == "")
                    {
                        if (Edisetting.FacilityNPI == true)
                            strMessage += "Facility NPI" + Environment.NewLine + "";
                    }
                }
                else if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Clinic"))
                {
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingLastName == true)
                            strMessage += "Clinic Name" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.BillingAddress == true)
                            strMessage += "Clinic Address" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        if (Edisetting.BillingCity == true)
                            strMessage += "Clinic City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        if (Edisetting.BillingState == true)
                            strMessage += "Clinic State" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        if (Edisetting.BillingZIP == true)
                            strMessage += "Clinic Zip" + Environment.NewLine + "";
                    }
                    if (_BillingNPI.Trim() == "")
                    {
                        if (Edisetting.BillingNPI == true)
                            strMessage += "Clinic NPI" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() == "")
                        {
                            if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "34" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "24" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "SY" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "EI")
                            {
                                strMessage += "Billing Provider " + dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }
                }
                else if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Company"))
                {
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingLastName == true)
                            strMessage += "Company Name" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.BillingAddress == true)
                            strMessage += "Company Address" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        if (Edisetting.BillingCity == true)
                            strMessage += "Company City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        if (Edisetting.BillingState == true)
                            strMessage += "Company State" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        if (Edisetting.BillingZIP == true)
                            strMessage += "Company Zip" + Environment.NewLine + "";
                    }
                    if (_BillingNPI.Trim() == "")
                    {
                        if (Edisetting.BillingNPI == true)
                            strMessage += "Company NPI" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() == "")
                        {
                            if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "34" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "24" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "SY" && dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "EI")
                            {
                                strMessage += "Billing Provider " + dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }
                }
                else
                {

                    if (dtBillingProvider.Rows[0]["FirstName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingFirstName == true)
                            strMessage += "Billing Provider First Name" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["LastName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingLastName == true)
                            strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["MiddleName"].ToString().Trim() == "")
                    {
                        if (Edisetting.BillingMiddleName == true)
                            strMessage += "Billing Provider Middle Name" + Environment.NewLine + "";
                    }
                    if (_BillingCity.Trim() == "")
                    {
                        if (Edisetting.BillingCity == true)
                            strMessage += "Billing Provider City" + Environment.NewLine + "";
                    }
                    if (_BillingState.Trim() == "")
                    {
                        if (Edisetting.BillingState == true)
                            strMessage += "Billing Provider State" + Environment.NewLine + "";
                    }
                    if (_BillingAddress.Trim() == "")
                    {
                        if (Edisetting.BillingAddress == true)
                            strMessage += "Billing Provider Address" + Environment.NewLine + "";
                    }
                    if (_BillingZIP.Trim() == "")
                    {
                        if (Edisetting.BillingZIP == true)
                            strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                    }
                    if (dtBillingProvider.Rows[0]["PrimaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["PrimaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (_BillingNPI.Trim() == "")
                        {
                            strMessage += strBillingSetting + "Billing Provider " + dtBillingProvider.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                        }
                    }

                    if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() != "" || dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() != "")
                    {
                        if (dtBillingProvider.Rows[0]["SecondaryQualifierValue"].ToString().Trim() == "")
                        {
                            if (dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "34" &&
                                dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "24" &&
                                dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "SY" &&
                                dtBillingProvider.Rows[0]["SecondaryQualifier"].ToString().Trim() != "EI")
                            {
                                strMessage += "Billing Provider " + dtBillingProvider.Rows[0]["SecondaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }

                    if (dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Facility") != true || dtBillingProvider.Rows[0]["sSource"].ToString().Trim().Contains("Clinic") != true)
                    {
                        if (dtBillingProvider.Rows[0]["Taxonomy"].ToString().Trim() == "")
                        {
                            if (Edisetting.BillingTaxonomy == true)
                                strMessage += "Billing Provider Taxonomy" + Environment.NewLine + "";
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Billing provider information is not present for claim number " + oTriarqClaim.ClaimNo.Trim() + ".\nClaim Status will not send. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            #endregion " Billing Provider "

            #region "Facility "
            //Billing Provider
            if (dtFacility != null && dtFacility.Rows.Count > 0)
            {
                bool IsincludeFacility = false;
                IsincludeFacility = Convert.ToBoolean(dtFacility.Rows[0]["bIncludeFacility"]);
                if (Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") != PrimaryBillingProviderID || (Convert.ToString(dtFacility.Rows[0]["PrimaryQualifierValue"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") == PrimaryBillingProviderID && IsincludeFacility == true))
                {

                    if (Convert.ToBoolean(dtMasterSetting.Rows[0]["bISOtherID"]) == true)
                    {
                        if (dtFacility.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "" && dtFacility.Rows[0]["SecondaryQualifierID"].ToString().Trim() != "0" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifiervalue"]) == "" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]).Trim() == "")//&& IsEDIgeneration == true
                        {
                            MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.Trim() + " for Patient " + oTriarqClaim.Patient.Code.Trim() + " is using " + dtFacility.Rows[0]["Setting"].ToString().Trim() + " which has mismatch in source and other ID type.\nBatch will not send.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return false;
                        }
                        if (Convert.ToString(dtFacility.Rows[0]["SecondaryQualifierID"].ToString().Trim()) == "0" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifiervalue"]) == "" && Convert.ToString(dtFacility.Rows[0]["SecondaryQualifier"]).Trim() == "")
                        {
                            strMessage += "Facility Tax ID" + Environment.NewLine + "";
                        }
                    }

                    string _FacilityAddress = "";
                    string _FacilityCity = "";
                    string _FacilityState = "";
                    string _FacilityZIP = "";
                    string _FacilityNPI = "";



                    _FacilityAddress = dtFacility.Rows[0]["Address1"].ToString().Trim();
                    _FacilityCity = dtFacility.Rows[0]["City"].ToString().Trim();
                    _FacilityState = dtFacility.Rows[0]["State"].ToString().Trim();
                    _FacilityZIP = dtFacility.Rows[0]["Zip"].ToString().Trim();
                    _FacilityNPI = dtFacility.Rows[0]["PrimaryQualifierValue"].ToString().Trim();



                    if (dtFacility.Rows[0]["sSource"].ToString().Trim().Contains("Facility"))
                    {
                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Facility Name" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Facility Address" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            //   if (GetValidationFieldsSettings("Facility City"))
                            strMessage += "Facility City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Facility State"))
                            strMessage += "Facility State" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            //  if (GetValidationFieldsSettings("Facility Zip"))
                            strMessage += "Facility Zip" + Environment.NewLine + "";
                        }
                        if (_FacilityNPI.Trim() == "")
                        {
                            if (Edisetting.FacilityNPI == true)
                                strMessage += "Facility NPI" + Environment.NewLine + "";
                        }
                    }
                    else if (dtFacility.Rows[0]["sSource"].ToString().Trim().Contains("Clinic"))
                    {
                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Clinic Name" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Clinic Address" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            if (Edisetting.FacilityCity == true)
                                strMessage += "Clinic City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            if (Edisetting.FacilityState == true)
                                strMessage += "Clinic State" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            if (Edisetting.FacilityZip == true)
                                strMessage += "Clinic Zip" + Environment.NewLine + "";
                        }
                        if (_FacilityNPI.Trim() == "")
                        {
                            if (Edisetting.FacilityNPI == true)
                                strMessage += "Clinic NPI" + Environment.NewLine + "";
                        }
                    }
                    else if (dtFacility.Rows[0]["sSource"].ToString().Trim().Contains("Company"))
                    {
                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Company Name" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Company Address" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            if (Edisetting.FacilityCity == true)
                                strMessage += "Company City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            if (Edisetting.FacilityState == true)
                                strMessage += "Company State" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            if (Edisetting.FacilityZip == true)
                                strMessage += "Company Zip" + Environment.NewLine + "";
                        }
                        if (_FacilityNPI.Trim() == "")
                        {
                            if (Edisetting.FacilityNPI == true)
                                strMessage += "Company NPI" + Environment.NewLine + "";
                        }
                    }
                    else
                    {

                        if (dtFacility.Rows[0]["LastName"].ToString().Trim() == "")
                        {
                            if (Edisetting.FacilityName == true)
                                strMessage += "Billing Provider Last Name" + Environment.NewLine + "";
                        }
                        if (_FacilityCity.Trim() == "")
                        {
                            if (Edisetting.BillingCity == true)
                                strMessage += "Billing Provider City" + Environment.NewLine + "";
                        }
                        if (_FacilityState.Trim() == "")
                        {
                            if (Edisetting.FacilityState == true)
                                strMessage += "Billing Provider State" + Environment.NewLine + "";
                        }
                        if (_FacilityAddress.Trim() == "")
                        {
                            if (Edisetting.FacilityAddress1 == true)
                                strMessage += "Billing Provider Address" + Environment.NewLine + "";
                        }
                        if (_FacilityZIP.Trim() == "")
                        {
                            if (Edisetting.FacilityZip == true)
                                strMessage += "Billing Provider Zip" + Environment.NewLine + "";
                        }
                        if (dtFacility.Rows[0]["PrimaryQualifier"].ToString().Trim() != "" || dtFacility.Rows[0]["PrimaryQualifierValue"].ToString().Trim() != "" || dtFacility.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() != "")
                        {
                            if (_FacilityNPI.Trim() == "")
                            {
                                //if (GetValidationFieldsSettings("Billing Provider NPI"))

                                strMessage += strBillingSetting + "Billing Provider " + dtFacility.Rows[0]["PrimaryQualifierDescription"].ToString().Trim() + Environment.NewLine + "";
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Facility information is not present for claim number " + oTriarqClaim.ClaimNo.Trim() + ".\nClaim Status will not send. ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            #endregion " Facility "

            #region " Subscriber "
            //Subscriber
            if (dtPatientInsurance != null && dtPatientInsurance.Rows.Count > 0)
            {
                for (int _InsRow = 0; _InsRow < dtPatientInsurance.Rows.Count; _InsRow++)
                {
                    #region " Primary Insurance "
                    if (_InsRow == 0)
                    {
                        string _strRelation = "";
                        _strRelation = dtPatientInsurance.Rows[0]["RelationshipCode"].ToString().Trim();

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubLName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SubscriberLastName == true)
                                strMessage += "Subscriber Last Name" + Environment.NewLine + "";
                        }

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                        {
                            //if (GetValidationFieldsSettings("Subscriber Relationship"))
                            strMessage += "Subscriber Relationship" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                        {

                            if (Edisetting.PlanType == true)
                                strMessage += "Plan Type" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubFName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SubscriberFirstName == true)
                                strMessage += "Subscriber First Name" + Environment.NewLine + "";
                        }

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Subscriber Insurance ID"))
                            strMessage += "Insurance ID" + Environment.NewLine + "";
                        }


                        if (Convert.ToBoolean(dtPatientInsurance.Rows[0]["bIncludeSubscriberAddress"]) == true || _strRelation == "18")
                        {
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberAddress == true)
                                    strMessage += "Subscriber Address" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sGroup"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberGroupID == true)
                                    strMessage += "Subscriber Group ID" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberCity == true)
                                    strMessage += "Subscriber City" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberState == true)
                                    strMessage += "Subscriber State" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                            {
                                if (Edisetting.SubscriberZip == true)
                                    strMessage += "Subscriber Zip" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["dtDOB"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                            {
                                //if (GetValidationFieldsSettings("Subscriber Date of Birth"))
                                strMessage += "Subscriber Date of Birth" + Environment.NewLine + "";
                            }
                            if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberGender"]).Trim() == "" &&
                                Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                            {
                                //if (GetValidationFieldsSettings("Subscriber Gender"))
                                strMessage += "Subscriber Gender" + Environment.NewLine + "";
                            }
                        }

                        if (dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"].ToString() == "0" || dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"].ToString().Trim() == "")
                        {
                            if (Edisetting.InsuranceTypeCode == true)
                                strMessage += "Insurance Type Code" + Environment.NewLine + "";
                        }

                        //Payer
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceName"]).Trim() == "")
                        {
                            if (Edisetting.PayerName == true)
                                strMessage += "Payer/Insurance Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerID"]).Trim() == "")
                        {
                            if (Edisetting.PayerId == true)
                                strMessage += "Payer ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerAddress1"]).Trim() == "")
                        {
                            if (Edisetting.PayerAddress == true)
                                strMessage += "Payer Address" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerCity"]).Trim() == "")
                        {
                            if (Edisetting.PayerCity == true)
                                strMessage += "Payer City" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerState"]).Trim() == "")
                        {
                            if (Edisetting.PayerState == true)
                                strMessage += "Payer State" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerZip"]).Trim() == "")
                        {
                            if (Edisetting.PayerZip == true)
                                strMessage += "Payer Zip" + Environment.NewLine + "";
                        }

                    }

                    #endregion " Primary Insurance "

                    #region " Secondary Insurance "
                    if (_InsRow == 1)
                    {
                        //Other Insurance
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubLName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SecondarySubLName == true)
                                strMessage += "Secondary Insurance Subscriber Last Name" + Environment.NewLine + "";
                        }

                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryPlanType == true)
                                strMessage += "Secondary Insurance Plan Type" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Relationship"))
                            strMessage += "Secondary Insurance Subscriber Relationship" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance ID"))
                            strMessage += "Secondary Insurance ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sGroup"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryGroupId == true)
                                strMessage += "Secondary Insurance Group ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryInsAddress == true)
                                strMessage += "Secondary Insurance Address" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubFName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.SecondarySubFName == true)
                                strMessage += "Secondary Insurance Subscriber First Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceName"]) == "")
                        {
                            if (Edisetting.SecondaryInsName == true)
                                strMessage += "Secondary Insurance Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerID"]).Trim() == "")
                        {
                            if (Edisetting.SecondaryInsPayerID == true)
                                strMessage += "Secondary Insurance Payer ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubCity == true)
                                strMessage += "Secondary Insurance City" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubState == true)
                                strMessage += "Secondary Insurance State" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubZip == true)
                                strMessage += "Secondary Insurance Zip" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["dtDOB"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Date of Birth"))
                            strMessage += "Secondary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberGender"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            // if (GetValidationFieldsSettings("Secondary Insurance Subscriber Gender"))
                            strMessage += "Secondary Insurance Subscriber Gender" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "0" ||
                            Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == string.Empty ||
                            Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "")
                        {
                            if (Edisetting.SecondarySubInsType == true)
                                strMessage += "Secondary Insurance Type Code" + Environment.NewLine + "";
                        }
                    }

                    #endregion " Secondary Insurance "

                    #region " Tertiary Insurance "
                    if (_InsRow == 2)
                    {
                        //Other Insurance
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubLName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.TertiarySubLName == true)
                                strMessage += "Tertiary Insurance Subscriber Last Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceTypeCode"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryPlanType == true)
                                strMessage += "Tertiary Plan Type" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["RelationshipCode"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Relationship"))
                            strMessage += "Tertiary Insurance Subscriber Relationship" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sSubscriberID"]).Trim() == "")
                        {
                            // if (GetValidationFieldsSettings("Tertiary Insurance ID"))
                            strMessage += "Tertiary Insurance ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["sGroup"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryGroupId == true)
                                strMessage += "Tertiary Insurance Group ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberAddr1"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryInsAddress == true)
                                strMessage += "Tertiary Insurance Address" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubFName"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            if (Edisetting.TertiarySubFName == true)
                                strMessage += "Tertiary Insurance Subscriber First Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsuranceName"]) == "")
                        {
                            if (Edisetting.TertiaryInsName == true)
                                strMessage += "Tertiary Insurance Name" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["PayerID"]).Trim() == "")
                        {
                            if (Edisetting.TertiaryInsPayerID == true)
                                strMessage += "Tertiary Insurance Payer ID" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberCity"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubCity == true)
                                strMessage += "Tertiary Insurance City" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberState"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubState == true)
                                strMessage += "Tertiary Insurance State" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberZip"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubZip == true)
                                strMessage += "Tertiary Insurance Zip" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["dtDOB"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            // if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Date of Birth"))
                            strMessage += "Tertiary Insurance Subscriber Date of Birth" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["SubscriberGender"]).Trim() == "" && Convert.ToBoolean(dtPatientInsurance.Rows[_InsRow]["bIsCompnay"]) == false)
                        {
                            //   if (GetValidationFieldsSettings("Tertiary Insurance Subscriber Gender"))
                            strMessage += "Tertiary Insurance Subscriber Gender" + Environment.NewLine + "";
                        }
                        if (Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "0" || Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == string.Empty || Convert.ToString(dtPatientInsurance.Rows[_InsRow]["InsTypeCodeDefault"]).Trim() == "")
                        {
                            if (Edisetting.TertiarySubInsType == true)
                                strMessage += "Tertiary Insurance Type Code" + Environment.NewLine + "";
                        }
                    }

                    #endregion " Tertiary Insurance "
                }
            }
            #endregion " Subscriber "

            #region " Rendering Provider "

            if (dtRenderingProvider != null && Convert.ToString(dtRenderingProvider.Rows[0]["QualifierMstID"]).Trim() == "0" && Convert.ToString(dtRenderingProvider.Rows[0]["QualifierID"]).Trim() != "0")//&& IsEDIgeneration == true
            {
                MessageBox.Show("Claim # " + oTriarqClaim.ClaimNo.ToString().Trim() + " for Patient " + oTriarqClaim.Patient.Code.ToString().Trim() + " having mismatch in Electronic Rendering ProviderID Type.\nBatch will not send.  Please review Billing ID Qualifier Setup.", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }


            bool IsincludeRenderingProvider = false;
            IsincludeRenderingProvider = Convert.ToBoolean(dtRenderingProvider.Rows[0]["bIncludeRenderingProvider"]);
            if (Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") != PrimaryBillingProviderID || (Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]).Trim().Replace("*", "").Replace("~", "").Replace(":", "").Replace("^", "") == PrimaryBillingProviderID && IsincludeRenderingProvider == true))
            {
                if (dtRenderingProvider != null && dtRenderingProvider.Rows.Count > 0)
                {
                    if (Convert.ToString(dtRenderingProvider.Rows[0]["sLastName"]).Trim() == "")
                    {
                        if (Edisetting.RenderingProLastName == true)
                            strMessage += "Rendering Provider Last Name" + Environment.NewLine + "";
                    }
                    if (Convert.ToString(dtRenderingProvider.Rows[0]["sFirstName"]).Trim() == "")
                    {
                        if (Edisetting.RenderingProFirstName == true)
                            strMessage += "Rendering Provider First Name" + Environment.NewLine + "";
                    }

                    if (Convert.ToString(dtRenderingProvider.Rows[0]["ProviderNPI"]).Trim() == "")
                    {
                        if (Edisetting.RenderingProNPI == true)
                            strMessage += "Rendering Provider NPI" + Environment.NewLine + "";
                    }
                }
            }


            #endregion " Rendering Provider "

            #region " Referring Provider "

            if (oTriarqClaim.ReferalProviderID_New > 0 || oTriarqClaim.IsSameAsBillingProvider)
            {
                if (dtRefferingProvider != null && dtRefferingProvider.Rows.Count > 0)
                {
                    //2310B Referring PROVIDER
                    //NM1 Referring PROVIDER NAME

                    if (dtRefferingProvider.Rows[0]["sLastName"].ToString().Trim().Replace("*", "") == "")
                    {
                        if (Edisetting.ReferringProLastName == true)
                            strMessage += "Referring Provider Last Name" + Environment.NewLine + "";
                    }
                    if (dtRefferingProvider.Rows[0]["sFirstName"].ToString().Trim().Replace("*", "") == "")
                    {
                        if (Edisetting.ReferringProFirstName == true)
                            strMessage += "Referring Provider First Name" + Environment.NewLine + "";
                    }
                    if (dtRefferingProvider.Rows[0]["sNPI"].ToString().Trim().Replace("*", "") == "")
                    {
                        if (Edisetting.ReferringProNPI == true)
                            strMessage += "Referring Provider NPI" + Environment.NewLine + "";
                    }
                }

            }

            #endregion " Referring Provider "

            if (_MessageHeader != "")
            {
                _Message = "";
                _Message = _MessageHeader;
            }


            if (_Message.Trim() != "")
            {
                string _Header = "Following fields are missing in database:" + Environment.NewLine + "" + Environment.NewLine + "";
                _Header += _Message;
                _FilePath = _FilePath + "EDI_276_Validation.txt";
                System.IO.StreamWriter oStreamWriter = new System.IO.StreamWriter(_FilePath, false);
                oStreamWriter.WriteLine(_Header);
                oStreamWriter.Close();
                oStreamWriter.Dispose();
                System.Diagnostics.Process.Start(_FilePath);
                return false;
            }
            else
            {
                //if (ValidClaim == true)
                //{
                //    MessageBox.Show("All data is present and valid.  ", _MessageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                return ValidClaim;
            }

        }

        private bool ValidateConnectionString(string _AlphaIIAuthentication, string _AlphaIIServerName, string _AlphaIIDatabase, string _AlphaIIUserName, string _AlphaIIPassword)
        {
            Boolean _Result = false;
            SqlConnection _connection = new SqlConnection();
            try
            {
                string _connstring = "";
                if (_AlphaIIAuthentication.ToUpper() == "WINDOWS")
                {
                    _connstring = "Integrated Security=SSPI; Persist Security Info=False; Data Source=" + _AlphaIIServerName + "; Initial Catalog=" + _AlphaIIDatabase + "; Connection Timeout = 0";
                }
                else
                {
                    _connstring = "Persist Security Info=False;Data Source=" + _AlphaIIServerName + ";Initial Catalog=" + _AlphaIIDatabase + ";User ID=" + _AlphaIIUserName + ";Pwd=" + _AlphaIIPassword + ";";
                }
                _connection.ConnectionString = _connstring;
                _connection.Open();
                _connection.Close();
                _Result = true;
            }
            catch //(Exception ex)
            {
                _Result = false;
            }
            return _Result;
        }
       
        public void GetSetting(string SettingName, string ClinicId, out object Value)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
            try
            {
                oDB.Connect(false);
                Value = oDB.ExecuteScalar_Query("SELECT ISNULL(sSettingsValue,'') AS sSettingsValue FROM Settings WITH (NOLOCK) WHERE UPPER(sSettingsName) = '" + SettingName.Trim().ToUpper() + "' AND nClinicID = " + ClinicId + "");
            }
            catch (gloDatabaseLayer.DBException DBErr)
            {
                Value = null;
                DBErr.ERROR_Log(DBErr.Message);
            }
            catch (Exception ex)
            {
                Value = null;
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.FetchDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
        }

       
        public Tuple<string, string, long> Generate276BatchCSIString(List<TRIARQClaim> oTriarqClaims, long CSIRequestID)
        {
            Tuple<string, string, long> ClaimStatusRequest = null;
           
            string InterchangeHeader = "";
            bool isAllDataAvilable = true;
            string sEdiFileName = "";
            string sEdiDocString = "";
            //string AusId = "";

            bool isISACreated = false;
            bool isGSCreated = false;


     

            int nHlCounter = 0;
            int nHlInfoReceiverParent = 0;
            int nHlServiceProviderParent = 0;
            int nHlSubscriberParent = 0;
            int nHlDependentParent = 0;

            if (oTriarqClaims != null)
            {
                if (oTriarqClaims.Count > 0)
                {
                    if (!Directory.Exists(TempFolderpath))
                    {
                        Directory.CreateDirectory(TempFolderpath);
                    }

                    sEdiFileName = TempFolderpath + "\\TRIARQ_276_Batch_" + DateTime.Now.ToString("MMddyyyyhhmmsstt") + ".txt.bci";
                    seffilepath_276_005010X214_SemRef = sPath + "\\SEF\\276_005010X212.SemRef.SEF";
                    LoadEdiSchema();

                    foreach (TRIARQClaim oTriarqClaim in oTriarqClaims)
                    {
                        try
                        {
                            if (oTriarqClaim.ResponsibleParty != null &&
                                oTriarqClaim.RequestHeader != null &&
                                oTriarqClaim.ClaimServiceLines != null &&
                                oTriarqClaim.ClaimFacility != null &&
                                oTriarqClaim.ClaimClinic != null &&
                                oTriarqClaim.BillingProvider != null &&
                                oTriarqClaim.Patient != null)
                            {
                                isAllDataAvilable = true;
                            }
                            else
                            {
                                isAllDataAvilable = false;
                            }

                            if (isAllDataAvilable == true)
                            {
                                nHlCounter = 0;
                                nHlInfoReceiverParent = 0;
                                nHlServiceProviderParent = 0;
                                nHlSubscriberParent = 0;
                                nHlDependentParent = 0;

                                if (!isISACreated)
                                {
                                    ediInterchange.Set(ref oInterchange, oEdiDoc.CreateInterchange("X", "005010"));
                                    ediDataSegment.Set(ref oSegment, oInterchange.GetDataSegmentHeader());
                                    #region "ISA"

                                    string ISA_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                                    string ISA_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                                    InterchangeHeader = clsGeneral.ControlNumberGeneration();
                                    oSegment.set_DataElementValue(1, 0, "00"); 		// Authorization Information Qualifier (I01) 
                                    oSegment.set_DataElementValue(2, 0, "          "); 		// Authorization Information (I02) 
                                    oSegment.set_DataElementValue(3, 0, "00"); 		// Security Information Qualifier (I03) 
                                    oSegment.set_DataElementValue(4, 0, "          "); 		// Security Information (I04) 
                                    oSegment.set_DataElementValue(5, 0, oTriarqClaim.RequestHeader.SenderQualifier); 		// Interchange ID Qualifier (I05) 
                                    oSegment.set_DataElementValue(6, 0, Convert.ToString(oTriarqClaim.RequestHeader.SubmitterID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Sender ID (I06) 
                                    oSegment.set_DataElementValue(7, 0, oTriarqClaim.RequestHeader.ReceiverQualifier); 		// Interchange ID Qualifier (I05) 
                                    oSegment.set_DataElementValue(8, 0, Convert.ToString(oTriarqClaim.RequestHeader.ReceiverID).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Receiver ID (I07) 
                                    oSegment.set_DataElementValue(9, 0, ISA_Date.Substring(2)); 		// Interchange Date (I08) 
                                    oSegment.set_DataElementValue(10, 0, clsGeneral.FormattedTime(ISA_Time).Trim().Replace("*", "").Replace("~", "").Replace(":", "")); 		// Interchange Time (I09) 
                                    oSegment.set_DataElementValue(11, 0, ":"); 		// Repetition Separator (I65) 
                                    oSegment.set_DataElementValue(12, 0, "00501"); 		// Interchange Control Version Number (I11) 
                                    oSegment.set_DataElementValue(13, 0, InterchangeHeader); 		// Interchange Control Number (I12) 
                                    oSegment.set_DataElementValue(14, 0, "0"); 		// Acknowledgment Requested (I13) 
                                    oSegment.set_DataElementValue(15, 0, oTriarqClaim.RequestHeader.TypeOfData); 		// Usage Indicator (I14) 
                                    oSegment.set_DataElementValue(16, 0, "!"); 		// Component Element Separator (I15) 

                                    #endregion
                                    isISACreated = true;
                                }

                                if (!isGSCreated)
                                {
                                    ediGroup.Set(ref oGroup, oInterchange.CreateGroup("005010X212"));
                                    ediDataSegment.Set(ref oSegment, oGroup.GetDataSegmentHeader());
                                    #region "GS"
                                    string GS_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                                    string GS_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));

                                    oSegment.set_DataElementValue(1, 0, "HR"); 		// Functional Identifier Code (479) 
                                    oSegment.set_DataElementValue(2, 0, oTriarqClaim.RequestHeader.SenderCode); 		// Application Sender's Code (142) 
                                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.RequestHeader.VenderIDCode); 		// Application Receiver's Code (124) 
                                    oSegment.set_DataElementValue(4, 0, GS_Date); 		// Date (373) 
                                    oSegment.set_DataElementValue(5, 0, GS_Time); 		// Time (337) 
                                    oSegment.set_DataElementValue(6, 0, "1"); 		// Group Control Number (28) 
                                    oSegment.set_DataElementValue(7, 0, "X"); 		// Responsible Agency Code (455) 
                                    oSegment.set_DataElementValue(8, 0, "005010X212"); 		// Version / Release / Industry Identifier Code (480) 
                                    #endregion

                                    isGSCreated = true;
                                }

                                // Creates the Transaction Set header segment (ST)
                                ediTransactionSet.Set(ref oTransactionSet, oGroup.CreateTransactionSet("276"));
                                ediDataSegment.Set(ref oSegment, oTransactionSet.GetDataSegmentHeader());
                                #region "ST"
                                oSegment.set_DataElementValue(1, 0, "276"); 		// Transaction Set Identifier Code (143) 
                                oSegment.set_DataElementValue(2, 0, "0001"); 		// Transaction Set Control Number (329) 
                                oSegment.set_DataElementValue(3, 0, "005010X212"); 		// Implementation Convention Reference (1705) 
                                #endregion

                                //BHT - Beginning of Hierarchical Transaction 
                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment("BHT"));
                                #region "BHT"
                                string BHT_Date = Convert.ToString(clsGeneral.DateAsNumber(DateTime.Now.ToShortDateString()));
                                string BHT_Time = Convert.ToString(clsGeneral.TimeAsNumber(DateTime.Now.ToLocalTime().ToShortTimeString()));
                                InterchangeHeader = clsGeneral.ControlNumberGeneration();
                                oSegment.set_DataElementValue(1, 0, "0010"); 		// Hierarchical Structure Code (1005) 
                                oSegment.set_DataElementValue(2, 0, "13"); 		// Transaction Set Purpose Code (353) 
                                oSegment.set_DataElementValue(3, 0, InterchangeHeader); 		// Reference Identification (127) 
                                oSegment.set_DataElementValue(4, 0, BHT_Date); 		// Date (373) 
                                oSegment.set_DataElementValue(5, 0, BHT_Time); 		// Time (337) 
                                #endregion
                                int nInfoSources = 1;
                                int nInfoSourceCtr = 1;
                                while (nInfoSourceCtr <= nInfoSources)    //2000A
                                {
                                    nHlCounter = nHlCounter + 1;       //increment HL loop
                                    nHlInfoReceiverParent = nHlCounter;   //The value of this HL counter is the HL parent for the HL subscriber loop

                                    //HL - Information Source Level
                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                    #region "HL 2000A SOURCE"
                                    oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                    oSegment.set_DataElementValue(3, 0, "20"); 		// Hierarchical Level Code (735) 
                                    oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                    #endregion

                                    //2100A PAYER NAME
                                    //NM1 - Payer Name
                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                    #region "NM 2100A SOURCE"
                                    oSegment.set_DataElementValue(1, 0, "PR"); 		// Entity Identifier Code (98) 
                                    oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
                                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.InsuranceName); 		// Name Last or Organization Name (1035) 
                                    oSegment.set_DataElementValue(8, 0, "PI"); 		// Identification Code Qualifier (66) 
                                    oSegment.set_DataElementValue(9, 0, oTriarqClaim.ResponsibleParty.PayerID); 		// Identification Code (67) 
                                    #endregion
                                    int nInfoReceivers = 1;
                                    int nInfoReceiverCtr = 1;
                                    // 2000B INFORMATION RECEIVER LEVEL
                                    while (nInfoReceiverCtr <= nInfoReceivers)    //2000B
                                    {
                                        nHlCounter = nHlCounter + 1;
                                        nHlServiceProviderParent = nHlCounter;

                                        //HL - Information Receiver Level
                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                        #region "HL 2000B RECEIVER"
                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                        oSegment.set_DataElementValue(2, 0, nHlInfoReceiverParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                        oSegment.set_DataElementValue(3, 0, "21"); 		// Hierarchical Level Code (735) 
                                        oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                        #endregion
                                        //2100B INFORMATION RECEIVER NAME
                                        //NM1 - Information Receiver Name
                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                        #region "NM 2100B RECEIVER"
                                        oSegment.set_DataElementValue(1, 0, "41"); 		// Entity Identifier Code (98) 
                                        oSegment.set_DataElementValue(2, 0, "2"); 		// Entity Type Qualifier (1065) 
                                        oSegment.set_DataElementValue(3, 0, oTriarqClaim.ClaimClinic.Name); 		// Name Last or Organization Name (1035) 
                                        oSegment.set_DataElementValue(4, 0, ""); 		// Name First (1036) 
                                        oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
                                        oSegment.set_DataElementValue(8, 0, "46"); 		// Identification Code Qualifier (66) 
                                        oSegment.set_DataElementValue(9, 0, oTriarqClaim.ClaimClinic.TIN); 		// Identification Code (67) 
                                        #endregion

                                        int nServiceProviders = 1;
                                        int nServiceProviderCtr = 1;
                                        //2000C SERVICE PROVIDER LEVEL
                                        while (nServiceProviderCtr <= nServiceProviders)    //2000C
                                        {
                                            nHlCounter = nHlCounter + 1;
                                            nHlSubscriberParent = nHlCounter;

                                            //HL - Service Provider Level
                                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                            #region "HL 2000C BILLING PROVIDER"
                                            oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                            oSegment.set_DataElementValue(2, 0, nHlServiceProviderParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                            oSegment.set_DataElementValue(3, 0, "19"); 		// Hierarchical Level Code (735) 
                                            oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                            #endregion

                                            //2100C PROVIDER NAME
                                            //NM1 - Provider Name
                                            ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                            #region "NM 2100C BILLING PROVIDER"
                                            oSegment.set_DataElementValue(1, 0, "1P"); 		// Entity Identifier Code (98) 
                                            oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.BillingProvider.EntityType)); 		// Entity Type Qualifier (1065) 
                                            oSegment.set_DataElementValue(3, 0, oTriarqClaim.BillingProvider.LastName); 		// Name Last or Organization Name (1035) 
                                            oSegment.set_DataElementValue(4, 0, oTriarqClaim.BillingProvider.FirstName); 		// Name First (1036) 
                                            oSegment.set_DataElementValue(5, 0, ""); 		// Name Middle (1037) 
                                            oSegment.set_DataElementValue(7, 0, ""); 		// Name Suffix (1039) 
                                            oSegment.set_DataElementValue(8, 0, "XX"); 		// Identification Code Qualifier (66) 
                                            oSegment.set_DataElementValue(9, 0, oTriarqClaim.BillingProvider.NPI); 		// Identification Code (67) 
                                            #endregion

                                            int nSubscribers = 1;
                                            int nSubscriberCtr = 1;

                                            //2000D SUBSCRIBER LEVEL
                                            while (nSubscriberCtr <= nSubscribers)    //2000D
                                            {
                                                nHlCounter = nHlCounter + 1;
                                                nHlDependentParent = nHlCounter;

                                                //HL - Subscriber Level
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                                #region "HL 2000D SUBSCRIBER
                                                oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                                oSegment.set_DataElementValue(2, 0, nHlSubscriberParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                                oSegment.set_DataElementValue(3, 0, "22"); 		// Hierarchical Level Code (735) 
                                                if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) == "18")
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "0"); 		// Hierarchical Child Code (736) 
                                                }
                                                else
                                                {
                                                    oSegment.set_DataElementValue(4, 0, "1"); 		// Hierarchical Child Code (736) 
                                                }
                                                #endregion

                                                //DMG - Subscriber Demographic Information 
                                                ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
                                                #region "DMG 2000D SUBSCRIBER"
                                                string DMG_DOB = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.ResponsibleParty.SubscriberDOB.ToString()));
                                                oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                                                oSegment.set_DataElementValue(2, 0, DMG_DOB); 		// Date Time Period (1251) 
                                                switch (oTriarqClaim.ResponsibleParty.SubscriberGender)// Gender Code (1068) 
                                                {
                                                    case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
                                                    case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
                                                    case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
                                                    case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
                                                }
                                                #endregion

                                                //2100D SUBSCRIBER NAME
                                                if (true)
                                                {
                                                    // Subscriber Name
                                                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                                    #region "NM 2100D SUBSCRIBER"
                                                    oSegment.set_DataElementValue(1, 0, "IL"); 		// Entity Identifier Code (98) 
                                                    oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
                                                    oSegment.set_DataElementValue(3, 0, oTriarqClaim.ResponsibleParty.SubscriberLName); 		// Name Last or Organization Name (1035) 
                                                    oSegment.set_DataElementValue(4, 0, oTriarqClaim.ResponsibleParty.SubscriberFName); 		// Name First (1036) 
                                                    oSegment.set_DataElementValue(5, 0, oTriarqClaim.ResponsibleParty.SubscriberMName); 		// Name Middle (1037) 
                                                    oSegment.set_DataElementValue(7, 0, oTriarqClaim.ResponsibleParty.SubscriberSuffix); 		// Name Suffix (1039) 
                                                    oSegment.set_DataElementValue(8, 0, "MI"); 		// Identification Code Qualifier (66) 
                                                    oSegment.set_DataElementValue(9, 0, Convert.ToString(oTriarqClaim.ResponsibleParty.InsuranceID)); 		// Identification Code (67) 
                                                    #endregion

                                                }//2100D 

                                                Gen_ClaimStatus(oTransactionSet, oTriarqClaim);
                                                if (Convert.ToString(oTriarqClaim.ResponsibleParty.PatientSubscrbierRelationShipCode) != "18")
                                                {
                                                    int nDependents = 1;
                                                    int nDependentCtr = 1;
                                                    //2000E DEPENDENT LEVEL
                                                    while (nDependentCtr <= nDependents)    //2000E
                                                    {
                                                        nHlCounter = nHlCounter + 1;

                                                        //HL - Dependent Level
                                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\HL"));
                                                        #region HL 2000E DEPENDENT
                                                        oSegment.set_DataElementValue(1, 0, nHlCounter.ToString()); 		// Hierarchical ID Number (628) 
                                                        oSegment.set_DataElementValue(2, 0, nHlDependentParent.ToString()); 		// Hierarchical Parent ID Number (734) 
                                                        oSegment.set_DataElementValue(3, 0, "23"); 		// Hierarchical Level Code (735) 
                                                        #endregion

                                                        //DMG - Dependent Demographic Information
                                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\DMG"));
                                                        #region DMG 2000E DEPENDENT
                                                        string DMG_DOB_E = Convert.ToString(clsGeneral.DateAsNumber(oTriarqClaim.Patient.DOB.ToString()));
                                                        oSegment.set_DataElementValue(1, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                                                        oSegment.set_DataElementValue(2, 0, DMG_DOB_E); 		// Date Time Period (1251) 
                                                        switch (oTriarqClaim.Patient.Gender)// Gender Code (1068) 
                                                        {
                                                            case "Male": oSegment.set_DataElementValue(3, 0, "M"); break;
                                                            case "Female": oSegment.set_DataElementValue(3, 0, "F"); break;
                                                            case "Unknown": oSegment.set_DataElementValue(3, 0, "UNK"); break;
                                                            case "Other": oSegment.set_DataElementValue(3, 0, "UN"); break;
                                                        }
                                                        #endregion

                                                        //2100E DEPENDENT NAME
                                                        //NM1 - Dependent Name
                                                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\NM1\NM1"));
                                                        #region NM 2100E DEPENDENT
                                                        oSegment.set_DataElementValue(1, 0, "QC"); 		// Entity Identifier Code (98) 
                                                        oSegment.set_DataElementValue(2, 0, "1"); 		// Entity Type Qualifier (1065) 
                                                        oSegment.set_DataElementValue(3, 0, oTriarqClaim.Patient.LastName); 		// Name Last or Organization Name (1035) 
                                                        oSegment.set_DataElementValue(4, 0, oTriarqClaim.Patient.FirstName); 		// Name First (1036) 
                                                        oSegment.set_DataElementValue(5, 0, oTriarqClaim.Patient.MiddleName); 		// Name Middle (1037) 
                                                        oSegment.set_DataElementValue(7, 0, oTriarqClaim.Patient.Suffix); 		// Name Suffix (1039) 
                                                        #endregion

                                                        //2200E CLAIM STATUS TRACKING NUMBER
                                                        Gen_ClaimStatus(oTransactionSet, oTriarqClaim);

                                                        nDependentCtr++;
                                                    }//2000E
                                                }
                                                nSubscriberCtr++;

                                            }//2000D

                                            nServiceProviderCtr++;

                                        }//2000C

                                        nInfoReceiverCtr++;
                                    }//2000B

                                    nInfoSourceCtr++;
                                }//2000A
                            }
                        }
                        catch (Exception ex)
                        {
                            sEdiDocString = "";
                            sEdiFileName = "";
                            clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
                        }
                    }

                    sEdiDocString = oEdiDoc.GetEdiString();
                    oEdiDoc.Save(sEdiFileName, 0);
                    
                    ERAFileID = SaveCSIRequestFile(CSIRequestID, sEdiDocString);
                    Parse276(sEdiFileName);
                }
            }

            ClaimStatusRequest = new Tuple<string, string, long>(sEdiFileName, sEdiDocString, ERAFileID);
            return ClaimStatusRequest;
        }

        private void LoadEdiSchema()
        {
            try
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, "EDI 276 File Initialization.", ActivityOutCome.Started);
                oEdiDoc = new ediDocument();
                oSchemas = oEdiDoc.GetSchemas();
                oSchemas.EnableStandardReference = false;
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardWrite;
                oEdiDoc.SegmentTerminator = "~{13:10}";
                oEdiDoc.ElementTerminator = "*";
                oEdiDoc.CompositeTerminator = "!";
                oEdiDoc.LoadSchema(seffilepath_276_005010X214_SemRef, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, "EDI 276 File Initialization.", ActivityOutCome.Complete);
            }
            catch (Exception ex)
            {
                //clsQEDILogs.CreateQEDIActivityLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, "Exception while EDI 276 File Initialization.", ActivityOutCome.Failure);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
            }
        }

        private void Gen_ClaimStatus(ediTransactionSet oTransactionSet, TRIARQClaim oTriarqClaim)
        {
            ediDataSegment oSegment = null;
            string InterchangeHeader = "";
            try
            {
                object objSettingValue = null;
                GetSetting("sClaimPrefix", Convert.ToString(oTriarqClaim.ClinicID), out objSettingValue);

                //2200D / 2200E CLAIM STATUS TRACKING NUMBER
                for (int nTrnCounter = 1; nTrnCounter <= 1; nTrnCounter++)
                {
                    //TRN - Claim Status Tracking Number
                    InterchangeHeader = clsGeneral.ControlNumberGeneration();
                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\TRN"));
                    oSegment.set_DataElementValue(1, 0, "1"); 		// Trace Type Code (481) 
                    oSegment.set_DataElementValue(2, 0, InterchangeHeader); 		// Reference Identification (127)

                    //REF - Payer Claim Control Number
                    if (oTriarqClaim.FormattedClaimNumber != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "1K"); 		// Reference Identification Qualifier (128) 
                        //oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127)  //Claim Number
                        if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                        {
                            oSegment.set_DataElementValue(2, 0, String.Concat(Convert.ToString(objSettingValue), oTriarqClaim.FormattedClaimNumber)); 		// Reference Identification (127) //Payers Claim Number
                        }
                        else
                        {
                            oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127) //Payers Claim Number
                        }
                    }
                    ////REF - Institutional Bill Type Identification
                    //if (oTriarqClaim.Type != "")
                    //{
                    //    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                    //    oSegment.set_DataElementValue(1, 0, "BLT"); 		// Reference Identification Qualifier (128) 
                    //    oSegment.set_DataElementValue(2, 0, oTriarqClaim.Type); 		// Reference Identification (127) 
                    //}

                    //REF - Application or Location System Identifier
                    if (oTriarqClaim.ClaimFacility.POSCode != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "LU"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.ClaimFacility.POSCode); 		// Reference Identification (127) 
                    }

                    //REF - Group Number
                    if (oTriarqClaim.ResponsibleParty.GroupNumber != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "6P"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.ResponsibleParty.GroupNumber); 		// Reference Identification (127) 
                    }

                    //REF - Patient Control Number
                    if (oTriarqClaim.Patient.Code != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "EJ"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, oTriarqClaim.Patient.Code); 		// Reference Identification (127) 
                    }
                    ////REF - Pharmacy Prescription Number
                    //if (oTriarqClaim.Patient.PharmacyNumber != "")
                    //{
                    //    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                    //    oSegment.set_DataElementValue(1, 0, "XZ"); 		// Reference Identification Qualifier (128) 
                    //    oSegment.set_DataElementValue(2, 0, oTriarqClaim.Patient.PharmacyNumber); 		// Reference Identification (127) 
                    //}
                    //REF - Claim Identification Number For Clearinghouses and Other Transmission Intermediaries
                    if (oTriarqClaim.FormattedClaimNumber != "")
                    {
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\REF"));
                        oSegment.set_DataElementValue(1, 0, "D9"); 		// Reference Identification Qualifier (128) 
                        //oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127)  //Claim Number
                        if (!String.IsNullOrEmpty(Convert.ToString(objSettingValue)))
                        {
                            oSegment.set_DataElementValue(2, 0, String.Concat(Convert.ToString(objSettingValue), oTriarqClaim.FormattedClaimNumber)); 		// Reference Identification (127) //Payers Claim Number
                        }
                        else
                        {
                            oSegment.set_DataElementValue(2, 0, oTriarqClaim.FormattedClaimNumber); 		// Reference Identification (127) //Payers Claim Number
                        }
                    }
                    //AMT - Claim Submitted Charges
                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\AMT"));
                    oSegment.set_DataElementValue(1, 0, "T3"); 		// Amount Qualifier Code (522) 
                    oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.TotalClaimAmount)); 		// Monetary Amount (782) 

                    //DTP - Claim Service Date
                    string TRN_DTP = Convert.ToString(clsGeneral.DateAsNumber(Convert.ToString(oTriarqClaim.TransactionDate)));
                    ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\DTP"));
                    oSegment.set_DataElementValue(1, 0, "472"); 		// Date/Time Qualifier (374) 
                    oSegment.set_DataElementValue(2, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                    oSegment.set_DataElementValue(3, 0, TRN_DTP); 		// Date Time Period (1251) 

                    //2200D / 2210E SERVICE LINE INFORMATION
                    for (int nSvcCounter = 1; nSvcCounter <= oTriarqClaim.ClaimServiceLines.Count; nSvcCounter++)
                    {
                        //SVC - Service Line Information
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\SVC\SVC"));
                        oSegment.set_DataElementValue(1, 1, "HC"); 		// Product/Service ID Qualifier (235) 
                        oSegment.set_DataElementValue(1, 2, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].CPT); 		// Product/Service ID (234) 
                        oSegment.set_DataElementValue(1, 3, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier1); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(1, 4, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier2); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(1, 5, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier3); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(1, 6, oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Modifier4); 		// Procedure Modifier (1339) 
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].TotalChargeAmount)); 		// Monetary Amount (782) 
                        oSegment.set_DataElementValue(4, 0, ""); 		// Product/Service ID (234) 
                        oSegment.set_DataElementValue(7, 0, Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].Quantity)); 		// Quantity (380) 

                        //REF - Service Line Item Identification
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\SVC\REF"));
                        oSegment.set_DataElementValue(1, 0, "FJ"); 		// Reference Identification Qualifier (128) 
                        oSegment.set_DataElementValue(2, 0, Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].LineID)); 		// Reference Identification (127) 

                        // Service Line Date
                        string SVC_DTP = Convert.ToString(clsGeneral.DateAsNumber(Convert.ToString(oTriarqClaim.ClaimServiceLines[nSvcCounter - 1].FromDOS)));
                        ediDataSegment.Set(ref oSegment, oTransactionSet.CreateDataSegment(@"HL\TRN\SVC\DTP"));
                        oSegment.set_DataElementValue(1, 0, "472"); 		// Date/Time Qualifier (374) 
                        oSegment.set_DataElementValue(2, 0, "D8"); 		// Date Time Period Format Qualifier (1250) 
                        oSegment.set_DataElementValue(3, 0, SVC_DTP); 		// Date Time Period (1251)                                 
                    }//2200D / 2210E

                }//2200D / 2200E
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateEDI, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oSegment != null)
                    oSegment.Dispose();
                InterchangeHeader = string.Empty;
            }

        }//Proc_ClaimStatus

        private long SaveCSIRequestFile(long CSIRequestID, string CSIRequestString)
        {
            //INUP_CSI_RequestFile
            long CSIRequestFileId = 0;
            object _result = null;
            try
            {
                if (OpenConnection(true))
                {
                    oDBPara.Clear();
                    oDBPara.Add("@CSIRequestFileId", 0, ParameterDirection.Output, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestId", CSIRequestID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestFile", CSIRequestString, ParameterDirection.Input, SqlDbType.VarChar);
                    oDBPara.Add("@LoginUserId", gloGlobal.gloPMGlobal.UserID, ParameterDirection.Input, SqlDbType.BigInt);
                    oDBPara.Add("@CSIRequestFileCreatedOn", DateTime.Now, ParameterDirection.Input, SqlDbType.DateTime);
                    oDB.Execute("INUP_CSI_RequestFile", oDBPara, out _result);
                    if ((_result != null) && (Convert.ToString(_result) != ""))
                    {
                        CSIRequestFileId = Convert.ToInt64(_result);
                    }
                    CloseConnection();
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ex, false);
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            return CSIRequestFileId;
        }




        #region "Parse EDI Request 276 File"

        public void Parse276(string FileName276)
        {
            seffilepath_276_005010X214_SemRef = sPath + "\\SEF\\276_005010X212.SemRef.SEF";
            x276RequestFilePath = FileName276;
            try
            {
                ediDocument.Set(ref oEdiDoc, new ediDocument());
                ediSchemas.Set(ref  oSchemas, new ediSchemas());
                oSchemas.EnableStandardReference = false;
                oEdiDoc.CursorType = DocumentCursorTypeConstants.Cursor_ForwardOnly;
                oEdiDoc.ImportSchema(seffilepath_276_005010X214_SemRef, SchemaTypeIDConstants.Schema_Standard_Exchange_Format);
                Parse276X212RequestFile();
                dtUniqueIDs = getUniQueIDS();
                SetIDforSegments();
                RemoveUnwantedColumnFromERATables();
                Save276X212InDB();
            }
            catch (ediException ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }
            catch (System.Runtime.CompilerServices.RuntimeWrappedException Rex)
            {
                string _strEx = string.Empty;
                ediException oException = null;
                oException = (ediException)Rex.WrappedException;
                _strEx = oException.get_Description();
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.InitializeEDi, null, ActivityOutCome.Failure);
            }
        }

        private void Parse276X212RequestFile()
        {
            ediDataSegment oSegment = null;
            string requestFilePath = string.Empty;
            string sSegmentID;
            string sLoopSection;
            int nArea;
            string sValue;
            string sHlQlfr = "";
            string sQlfr = "";


            try
            {
                //requestFilePath = @"I:\Developer Working Folder\gloSuite9010\gloSuite\gloPM\gloPM\bin\x86\Debug\TRIARQ_276_ClaimNumber_00098_02152018051229PM.txt.bci";// x276RequestFilePath;
                requestFilePath = x276RequestFilePath;
                if (System.IO.File.Exists(requestFilePath) == true)
                {
                    if (oEdiDoc != null)
                    {
                        oEdiDoc.LoadEdi(requestFilePath);
                        ediDataSegment.Set(ref oSegment, (ediDataSegment)oEdiDoc.FirstDataSegment);
                        while (oSegment != null)
                        {
                            sSegmentID = oSegment.ID;
                            sLoopSection = oSegment.LoopSection;
                            nArea = oSegment.Area;
                            if (nArea == 0)
                            {
                                if (sLoopSection == "")
                                {
                                    if (sSegmentID == "ISA")
                                    {
                                        #region "ISA"
                                        if (o276_ISA == null)
                                        {
                                            o276_ISA = new Cls_276X212_ISA();
                                            SegmentCounter++;
                                        }
                                        o276_ISA.ISA01_AuthorInfoQual = oSegment.get_DataElementValue(1, 0);                //Authorization Information Qualifier
                                        o276_ISA.ISA02_AuthorInfo = oSegment.get_DataElementValue(2, 0);                    //Authorization Information
                                        o276_ISA.ISA03_SecurityInfoQual = oSegment.get_DataElementValue(3, 0);              //Security Information Qualifier
                                        o276_ISA.ISA04_SecurityInfo = oSegment.get_DataElementValue(4, 0);                  //Security Information
                                        o276_ISA.ISA05_IntrChngIDQual = oSegment.get_DataElementValue(5, 0);                //Interchange ID Qualifier  
                                        o276_ISA.ISA06_IntrChngSenderID = oSegment.get_DataElementValue(6, 0);              //Interchange Sender ID
                                        o276_ISA.ISA07_IntrChngIDQual = oSegment.get_DataElementValue(7, 0);                //Interchange ID Qualifier
                                        o276_ISA.ISA08_IntrChngReceiverID = oSegment.get_DataElementValue(8, 0);            //Interchange Receiver ID
                                        o276_ISA.ISA09_IntrChngDate = oSegment.get_DataElementValue(9, 0);                  //Interchange Date
                                        o276_ISA.ISA10_IntrChngTime = oSegment.get_DataElementValue(10, 0);                 //Interchange Time
                                        o276_ISA.ISA11_IntrChngRepetitionSeperator = oSegment.get_DataElementValue(11, 0);  //Repetition Separator
                                        o276_ISA.ISA12_IntrChngControlVersionNo = oSegment.get_DataElementValue(12, 0);     //Interchange Control Version Number
                                        o276_ISA.ISA13_IntrChngControlNo = oSegment.get_DataElementValue(13, 0);            //Interchange Control Number
                                        o276_ISA.ISA14_AckwRequested = oSegment.get_DataElementValue(14, 0);                //Acknowledgment Requested
                                        o276_ISA.ISA15_UsageIndicator = oSegment.get_DataElementValue(15, 0);               //Interchange Usage Indicator
                                        o276_ISA.ISA16_ComponentElementSeparator = oSegment.get_DataElementValue(16, 0);    //Component Element Separator                                       
                                        #endregion
                                    }
                                    else if (sSegmentID == "GS")
                                    {
                                        #region "GS"
                                        if (o276_GS == null)
                                        {
                                            o276_GS = new Cls_276X212_GS();
                                            SegmentCounter++;
                                        }
                                        o276_GS.GS01_StatusNotification = oSegment.get_DataElementValue(1, 0);              //Functional Identifier Code
                                        o276_GS.GS02_SenderID = oSegment.get_DataElementValue(2, 0);                        //Application Sender's Code
                                        o276_GS.GS03_ReceiverID = oSegment.get_DataElementValue(3, 0);                      //Application Receiver's Code
                                        o276_GS.GS04_FunctionalGroupDate = oSegment.get_DataElementValue(4, 0);             //Date
                                        o276_GS.GS05_FunctionalGroupTime = oSegment.get_DataElementValue(5, 0);             //Time
                                        o276_GS.GS06_GroupControlNumber = oSegment.get_DataElementValue(6, 0);              //Group Control Number
                                        o276_GS.GS07_ResponsibleAgencyCode = oSegment.get_DataElementValue(7, 0);           //Responsible Agency Code
                                        o276_GS.GS08_VersionORIndustryIdentifier = oSegment.get_DataElementValue(8, 0);     //Version / Release / Industry IdentifierCode
                                        if (o276_ISA != null)
                                        {
                                            o276_ISA.ISA_GS = o276_GS;
                                        }
                                        #endregion
                                    }
                                }
                            }
                            else if (nArea == 1)
                            {
                                if (sLoopSection == "")
                                {
                                    if (sSegmentID == "ST")
                                    {
                                        #region "ST"
                                        if (o276_ST == null)
                                        {
                                            o276_ST = new Cls_276X212_ST();
                                            SegmentCounter++;
                                        }
                                        o276_ST.ST01_IdentifierCode = oSegment.get_DataElementValue(1, 0);              //Transaction Set Identifier Code
                                        o276_ST.ST02_TransactioNSetControlNumber = oSegment.get_DataElementValue(2, 0); //Transaction Set Control Number
                                        o276_ST.ST03_ConventionReference = oSegment.get_DataElementValue(3, 0);         //Implementation Convention Reference
                                        #endregion
                                    }
                                    if (sSegmentID == "BHT")
                                    {
                                        #region "BHT"
                                        if (o276_BHT == null)
                                        {
                                            o276_BHT = new Cls_276X212_BHT();
                                            SegmentCounter++;
                                        }
                                        o276_BHT.BHT01_StructureCode = oSegment.get_DataElementValue(1, 0);             //Hierarchical Structure Code
                                        o276_BHT.BHT02_PurposeCode = oSegment.get_DataElementValue(2, 0);               //Transaction Set Purpose Code
                                        o276_BHT.BHT03_ReferenceIdentification = oSegment.get_DataElementValue(3, 0);   //Reference Identification
                                        o276_BHT.BHT04_TransactionDate = oSegment.get_DataElementValue(4, 0);           //Date
                                        o276_BHT.BHT05_TransactionTime = oSegment.get_DataElementValue(5, 0);           //Time
                                        o276_BHT.BHT06_TransactionTypeCode = oSegment.get_DataElementValue(6, 0);       //Transaction Type Code
                                        if (o276_ST != null)
                                        {
                                            o276_ST.ST_BHT = o276_BHT;
                                        }
                                        o276_BHT.Dispose();
                                        o276_BHT = null;
                                        #endregion
                                    }
                                }
                            }
                            else if (nArea == 2)
                            {
                                if (sLoopSection == "HL" && sSegmentID == "HL")
                                {
                                    sHlQlfr = oSegment.get_DataElementValue(3, 0);
                                }
                                else if (sLoopSection == "" && sSegmentID == "SE")
                                {
                                    if (o276_ST != null)
                                    {
                                        if (o276_HL != null)
                                        {
                                            if (o276_TRN != null)
                                            {
                                                if (o276_HL.HL_TRN == null)
                                                {
                                                    o276_HL.HL_TRN = new List<Cls_276X212_TRN>();
                                                }
                                                o276_HL.HL_TRN.Add(o276_TRN);
                                                o276_TRN.Dispose();
                                                o276_TRN = null;
                                            }
                                            if (o276_ST.ST_HL == null)
                                            {
                                                o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                            }
                                            o276_ST.ST_HL.Add(o276_HL);
                                            o276_HL.Dispose();
                                            o276_HL = null;

                                            if (o276_ISA != null)
                                            {
                                                if (o276_ISA.ISA_ST == null)
                                                {
                                                    o276_ISA.ISA_ST = new List<Cls_276X212_ST>();
                                                }
                                                o276_ISA.ISA_ST.Add(o276_ST);
                                                o276_ST.Dispose();
                                                o276_ST = null;
                                            }
                                        }
                                    }
                                }

                                if (sHlQlfr == "20")                                                                    //2000A INFORMATION SOURCE LEVEL
                                {
                                    #region "2000A INFORMATION SOURCEL LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")                                                         //Information Source Level
                                        {
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code
                                        }
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                  //2100A PAYER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);               //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);        //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                   //Name Last or Organization Name                                                    
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);//Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);         //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    #endregion
                                }
                                if (sHlQlfr == "21")                                                                    //2000B INFORMATION RECEIVER LEVEL
                                {
                                    #region "2000B INFORMATION RECEIVER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")                                                         //Information Receiver Level
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_ST.ST_HL == null)
                                                {
                                                    o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                }
                                                if (o276_HL != null)
                                                {
                                                    o276_ST.ST_HL.Add(o276_HL);
                                                    o276_HL.Dispose();
                                                    o276_HL = null;
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);                 //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);                  //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);                   //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);                   //Hierarchical Child Code
                                        }
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                      //2100B RECEIVER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);                   //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);            //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                       //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                      //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                     //Name Middle                                              
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);    //Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);             //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    #endregion
                                }
                                if (sHlQlfr == "19")                                                                    //2000C INFORMATION PROVIDER LEVEL
                                {
                                    #region "INFORMATION PROVIDER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")                                                         //Service Provider Level
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_ST.ST_HL == null)
                                                {
                                                    o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                }
                                                if (o276_HL != null)
                                                {
                                                    o276_ST.ST_HL.Add(o276_HL);
                                                    o276_HL.Dispose();
                                                    o276_HL = null;
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code                                    
                                        }
                                    }
                                    else if (sLoopSection == "HL;NM1")
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);               //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);        //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                   //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                  //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                 //Name Middle
                                            o276_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);                     //Name Suffix
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);//Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);         //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    #endregion
                                }
                                if (sHlQlfr == "22")                                                                    //2000D INFORMATION SUBSCRIBER LEVEL
                                {
                                    #region "INFORMATION SUBSCRIBER LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_ST.ST_HL == null)
                                                {
                                                    o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                }
                                                if (o276_HL != null)
                                                {
                                                    o276_ST.ST_HL.Add(o276_HL);
                                                    o276_HL.Dispose();
                                                    o276_HL = null;
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code
                                        }
                                    }
                                    if (sSegmentID == "DMG")
                                    {
                                        if (o276_DMG == null)
                                        {
                                            o276_DMG = new Cls_276X212_DMG();
                                            SegmentCounter++;
                                        }
                                        o276_DMG.DMG01_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(1, 0); //Date Time Period Format Qualifier
                                        o276_DMG.DMG02_DateTimePeriod = oSegment.get_DataElementValue(2, 0);                //Date Time Period
                                        o276_DMG.DMG03_GenderCode = oSegment.get_DataElementValue(3, 0);                    //Gender Code
                                        if (o276_HL != null)
                                        {
                                            o276_HL.HL_DMG = o276_DMG;
                                        }
                                        o276_DMG.Dispose();
                                        o276_DMG = null;
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                             //2100D SUBSCRIBER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);                   //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);            //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                       //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                      //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                     //Name Middle
                                            o276_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);                         //Name Suffix
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);    //Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);             //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    Prase_ClaimStatus(ref oSegment, ref sSegmentID, ref sLoopSection);
                                    #endregion
                                }
                                if (sHlQlfr == "23")                                                                    //2000E INFORMATION PATIENT LEVEL
                                {
                                    #region "INFORMATION PATIENT LEVEL"
                                    if (sLoopSection == "HL")
                                    {
                                        if (sSegmentID == "HL")
                                        {
                                            if (o276_ST != null)
                                            {
                                                if (o276_HL != null)
                                                {
                                                    if (o276_ST.ST_HL == null)
                                                    {
                                                        o276_ST.ST_HL = new List<Cls_276X212_HL>();
                                                    }
                                                    if (o276_HL != null)
                                                    {
                                                        o276_ST.ST_HL.Add(o276_HL);
                                                        o276_HL.Dispose();
                                                        o276_HL = null;
                                                    }
                                                }
                                            }
                                            if (o276_HL == null)
                                            {
                                                o276_HL = new Cls_276X212_HL();
                                                SegmentCounter++;
                                            }
                                            o276_HL.HL01_HLSegmentId = oSegment.get_DataElementValue(1, 0);             //Hierarchical ID Number
                                            o276_HL.HL02_HLParentId = oSegment.get_DataElementValue(2, 0);              //Hierarchical Parent ID
                                            o276_HL.HL03_LevelCode = oSegment.get_DataElementValue(3, 0);               //Hierarchical Level Code
                                            o276_HL.HL04_ChildCode = oSegment.get_DataElementValue(4, 0);               //Hierarchical Child Code
                                        }
                                    }
                                    if (sSegmentID == "DMG")
                                    {
                                        if (o276_DMG == null)
                                        {
                                            o276_DMG = new Cls_276X212_DMG();
                                            SegmentCounter++;
                                        }
                                        o276_DMG.DMG01_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(1, 0); //Date Time Period Format Qualifier
                                        o276_DMG.DMG02_DateTimePeriod = oSegment.get_DataElementValue(2, 0);                //Date Time Period
                                        o276_DMG.DMG03_GenderCode = oSegment.get_DataElementValue(3, 0);                    //Gender Code
                                        if (o276_HL != null)
                                        {
                                            o276_HL.HL_DMG = o276_DMG;
                                        }
                                        o276_DMG.Dispose();
                                        o276_DMG = null;
                                    }
                                    else if (sLoopSection == "HL;NM1")                                                             //2100D SUBSCRIBER NAME
                                    {
                                        if (sSegmentID == "NM1")
                                        {
                                            if (o276_NM == null)
                                            {
                                                o276_NM = new Cls_276X212_NM();
                                                SegmentCounter++;
                                            }
                                            o276_NM.NM101_EntityIdCode = oSegment.get_DataElementValue(1, 0);                   //Entity Identifier Code
                                            o276_NM.NM102_EntityTypeQualifier = oSegment.get_DataElementValue(2, 0);            //Entity Type Qualifier
                                            o276_NM.NM103_LastName = oSegment.get_DataElementValue(3, 0);                       //Name Last or Organization Name
                                            o276_NM.NM104_FirstName = oSegment.get_DataElementValue(4, 0);                      //Name First
                                            o276_NM.NM105_MiddleName = oSegment.get_DataElementValue(5, 0);                     //Name Middle
                                            o276_NM.NM107_Suffix = oSegment.get_DataElementValue(7, 0);                         //Name Suffix
                                            o276_NM.NM108_IdentificationCodeQualifier = oSegment.get_DataElementValue(8, 0);    //Identification Code Qualifier
                                            o276_NM.NM109_IdentificationCode = oSegment.get_DataElementValue(9, 0);             //Identification Code
                                            if (o276_HL != null)
                                            {
                                                o276_HL.HL_NM = o276_NM;
                                            }
                                            o276_NM.Dispose();
                                            o276_NM = null;
                                        }
                                    }
                                    Prase_ClaimStatus(ref oSegment, ref sSegmentID, ref sLoopSection);
                                    #endregion
                                }
                            }
                            ediDataSegment.Set(ref oSegment, (ediDataSegment)oSegment.Next());
                        }
                    }
                }
            }
            catch (ediException ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oEdiDoc != null)
                {
                    oEdiDoc.Close();
                    oEdiDoc.Dispose();
                    oEdiDoc = null;
                }

                oSegment = null;
                requestFilePath = string.Empty;
                sSegmentID = string.Empty;
                sLoopSection = string.Empty;
                sValue = string.Empty;
                sHlQlfr = string.Empty;
                sQlfr = string.Empty;
            }
        }

        public void Prase_ClaimStatus(ref ediDataSegment oSegment, ref string sSegmentID, ref string sLoopSection)
        {
            try
            {
                if (sLoopSection == "HL;TRN")                                                          //2200D SUBSCRIBER TRACKING/2200E PATIENT TRACKING                                                         
                {
                    #region "2200D SUBSCRIBER NAME"
                    if (sSegmentID == "TRN")                                                                //Claim Status Tracking Number
                    {
                        if (o276_TRN == null)
                        {
                            o276_TRN = new Cls_276X212_TRN();
                            SegmentCounter++;
                        }
                        o276_TRN.TRN01_TraceTypeID = oSegment.get_DataElementValue(1, 0);                   //Trace Type Code                  
                        o276_TRN.TRN02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0);       //Reference Identification                    
                    }
                    else if (sSegmentID == "REF")                                                           //REFERENCE
                    {
                        if (o276_REF == null)
                        {
                            o276_REF = new Cls_276X212_REF();
                            SegmentCounter++;
                        }
                        o276_REF.REF01_ReferenceIdentificationQualifier = oSegment.get_DataElementValue(1, 0);  //Reference Identification Qualifier
                        o276_REF.REF02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0);           //Reference Identification   
                        if (o276_TRN != null)
                        {
                            if (o276_TRN.TRN_REF == null)
                            {
                                o276_TRN.TRN_REF = new List<Cls_276X212_REF>();
                            }
                            o276_TRN.TRN_REF.Add(o276_REF);
                            o276_REF.Dispose();
                            o276_REF = null;
                        }
                    }
                    else if (sSegmentID == "AMT")                                                   //Claim Submitted Charges
                    {
                        if (o276_AMT == null)
                        {
                            o276_AMT = new Cls_276X212_AMT();
                            SegmentCounter++;
                        }
                        o276_AMT.AMT01_AmountQualifierCode = oSegment.get_DataElementValue(1, 0);   //Amount Qualifier Code
                        o276_AMT.AMT02_MonetaryAmount = oSegment.get_DataElementValue(2, 0);        //Monetary Amount
                        if (o276_TRN != null)
                        {
                            o276_TRN.TRN_AMT = o276_AMT;
                            o276_AMT.Dispose();
                            o276_AMT = null;
                        }
                    }
                    else if (sSegmentID == "DTP")                                                   //Claim Service Date
                    {
                        if (o276_DTP == null)
                        {
                            o276_DTP = new Cls_276X212_DTP();
                            SegmentCounter++;
                        }
                        o276_DTP.DTP01_DateTimeQualifier = oSegment.get_DataElementValue(1, 0);             //Date/Time Qualifier
                        o276_DTP.DTP02_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(2, 0); //Date Time Period Format Qualifier
                        o276_DTP.DTP03_DateTimePeriod = oSegment.get_DataElementValue(3, 0);                //Date Time Period
                        if (o276_TRN != null)
                        {
                            o276_TRN.TRN_DTP = o276_DTP;
                            o276_DTP.Dispose();
                            o276_DTP = null;
                        }
                    }
                    #endregion
                }

                else if (sLoopSection == "HL;TRN;SVC")                                                      //2210D SUBSCRIBER TRACKING/2210E PATIENT TRACKING
                {
                    #region "2210D SUBSCRIBER NAME"
                    if (sSegmentID == "SVC")
                    {
                        if (o276_SVC == null)
                        {
                            o276_SVC = new Cls_276X212_SVC();
                            SegmentCounter++;
                        }
                        o276_SVC.SVC01_01_ProductORServiceIDQualifier = oSegment.get_DataElementValue(1, 0);    //Product/Service ID Qualifier
                        o276_SVC.SVC01_02_ProductORServiceID = oSegment.get_DataElementValue(2, 0);             //Product/Service ID
                        o276_SVC.SVC01_03_ProcedureModifier = oSegment.get_DataElementValue(3, 0);              //Procedure Modifier
                        o276_SVC.SVC01_04_ProcedureModifier = oSegment.get_DataElementValue(4, 0);              //Procedure Modifier
                        o276_SVC.SVC01_05_ProcedureModifier = oSegment.get_DataElementValue(5, 0);              //Procedure Modifier
                        o276_SVC.SVC01_06_ProcedureModifier = oSegment.get_DataElementValue(6, 0);              //Procedure Modifier
                        o276_SVC.SVC02_MonetaryAmount = oSegment.get_DataElementValue(2, 0);                    //Monetary Amount
                        o276_SVC.SVC04_ProductORServiceID = oSegment.get_DataElementValue(4, 0);                //Product/Service ID
                        o276_SVC.SVC07_Quantity = oSegment.get_DataElementValue(7, 0);                          //Quantity
                    }
                    else if (sSegmentID == "REF")
                    {
                        if (o276_REF == null)
                        {
                            o276_REF = new Cls_276X212_REF();
                            SegmentCounter++;
                        }
                        o276_REF.REF01_ReferenceIdentificationQualifier = oSegment.get_DataElementValue(1, 0);  //Reference Identification Qualifier
                        o276_REF.REF02_ReferenceIdentification = oSegment.get_DataElementValue(2, 0);           //Reference Identification          
                        if (o276_SVC != null)
                        {
                            o276_SVC.SVC_REF = o276_REF;
                            o276_REF.Dispose();
                            o276_REF = null;
                        }
                    }
                    else if (sSegmentID == "DTP")
                    {
                        if (o276_DTP == null)
                        {
                            o276_DTP = new Cls_276X212_DTP();
                            SegmentCounter++;
                        }
                        o276_DTP.DTP01_DateTimeQualifier = oSegment.get_DataElementValue(1, 0);             //Date/Time Qualifier
                        o276_DTP.DTP02_DateTimePeriodFormatQualifier = oSegment.get_DataElementValue(2, 0); //Date Time Period Format Qualifier
                        o276_DTP.DTP03_DateTimePeriod = oSegment.get_DataElementValue(3, 0);                //Date Time Period
                        if (o276_SVC != null)
                        {
                            o276_SVC.SVC_DTP = o276_DTP;
                            o276_DTP.Dispose();
                            o276_DTP = null;
                        }
                        if (o276_SVC != null)
                        {
                            if (o276_TRN != null)
                            {
                                if (o276_TRN.TRN_SVC == null)
                                {
                                    o276_TRN.TRN_SVC = new List<Cls_276X212_SVC>();
                                }
                                o276_TRN.TRN_SVC.Add(o276_SVC);
                                o276_SVC.Dispose();
                                o276_SVC = null;


                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.ParseEDI, ex, ActivityOutCome.Failure);
            }


        }//Proc_ClaimStatus

        private DataTable getUniQueIDS()
        {
            DataTable dt = null;
            try
            {
                if (SegmentCounter > 0)
                {
                    if (OpenConnection(true))
                    {
                        oDBPara.Clear();
                        oDBPara.Add("@IDCount", SegmentCounter, ParameterDirection.Input, SqlDbType.BigInt);
                        oDBPara.Add("@SingleRow", 1, ParameterDirection.Input, SqlDbType.Bit);
                        oDB.Retrive("gsp_GetUniqueIDs", oDBPara, out dt);
                        CloseConnection();
                    }
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateSegmentIDs, ex, ActivityOutCome.Failure);
                return null;
            }
            return dt;
        }

        private bool OpenConnection(bool withParameters)
        {
            bool _Result = false;
            try
            {
                if (_DataBaseConnectionString != "")
                {
                    oDB = new gloDatabaseLayer.DBLayer(_DataBaseConnectionString);
                    oDB.Connect(false);
                    if (withParameters)
                        oDBPara = new gloDatabaseLayer.DBParameters();
                    _Result = true;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return _Result;
        }

        private void CloseConnection()
        {
            if (oDB != null)
            {
                oDB.Disconnect();
                oDB.Dispose();
                oDB = null;
            }
            if (oDBPara != null)
            {
                oDBPara.Dispose();
                oDBPara = null;
            }
        }

        private void SetIDforSegments()
        {
            try
            {
                if (dtUniqueIDs != null)
                {
                    if (dtUniqueIDs.Rows.Count > 0)
                    {
                        ISAID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                        o276_ISA.ERAFileID = ERAFileID;
                        o276_ISA.ISAID = ISAID;
                        UniqueIdCounter++;
                        CreateISADataTable();

                        o276_ISA.ISA_GS.ERAFileID = ERAFileID;
                        o276_ISA.ISA_GS.GS_ISAID = ISAID;
                        o276_ISA.ISA_GS.GSID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                        CreateGSDatatable(o276_ISA.ISA_GS);
                        UniqueIdCounter++;
                        for (int st = 0; st < o276_ISA.ISA_ST.Count; st++)
                        {
                            o276_ISA.ISA_ST[st].ERAFileID = ERAFileID;
                            o276_ISA.ISA_ST[st].ST_ISAID = ISAID;
                            CurrentSTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                            UniqueIdCounter++;
                            o276_ISA.ISA_ST[st].STID = CurrentSTID;
                            CreateSTDatatable(o276_ISA.ISA_ST[st]);

                            o276_ISA.ISA_ST[st].ST_BHT.ERAFileID = ERAFileID;
                            o276_ISA.ISA_ST[st].ST_BHT.BHT_ISAID = ISAID;
                            o276_ISA.ISA_ST[st].ST_BHT.BHT_STID = CurrentSTID;
                            o276_ISA.ISA_ST[st].ST_BHT.BHTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                            UniqueIdCounter++;
                            CreateBHTDatatable(o276_ISA.ISA_ST[st].ST_BHT);

                            for (int hl = 0; hl < o276_ISA.ISA_ST[st].ST_HL.Count; hl++)
                            {
                                o276_ISA.ISA_ST[st].ST_HL[hl].ERAFileID = ERAFileID;
                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_ISAID = ISAID;
                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_STID = CurrentSTID;
                                CurrentHLId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                o276_ISA.ISA_ST[st].ST_HL[hl].HLID = CurrentHLId;
                                UniqueIdCounter++;

                                switch (Convert.ToString(o276_ISA.ISA_ST[st].ST_HL[hl].HL03_LevelCode))
                                {
                                    case "20": SourceHLId = CurrentHLId;                                    //Source
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = 0;
                                        break;
                                    case "21": ReceiverHLId = CurrentHLId;                                  //Reveiver
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = SourceHLId;
                                        break;
                                    case "19": ProviderHLId = CurrentHLId;                                  //Provider
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = ReceiverHLId;
                                        break;
                                    case "22": SubscriberHLId = CurrentHLId;                                //Subcriber
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = ProviderHLId;
                                        break;
                                    case "23": DependentHLId = CurrentHLId;                                 //Dependent
                                        o276_ISA.ISA_ST[st].ST_HL[hl].ParentHLID = SubscriberHLId;
                                        break;
                                }
                                CreateHLDatatable(o276_ISA.ISA_ST[st].ST_HL[hl]);

                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM != null)
                                {
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.ERAFileID = ERAFileID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_ISAID = ISAID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_STID = CurrentSTID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NM_HLID = CurrentHLId;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM.NMID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                    UniqueIdCounter++;
                                    CreateNMDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_NM);
                                }
                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG != null)
                                {
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.ERAFileID = ERAFileID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMG_ISAID = ISAID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMG_STID = CurrentSTID;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMG_HLID = CurrentHLId;
                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG.DMGID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                    UniqueIdCounter++;
                                    CreateDMGDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_DMG);
                                }

                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN != null)
                                {
                                    for (int trn = 0; trn < o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN.Count; trn++)
                                    {
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].ERAFileID = ERAFileID;
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_ISAID = ISAID;
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_STID = CurrentSTID;
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_HLID = CurrentHLId;
                                        CurrentTRNId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                        o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRNID = CurrentTRNId;
                                        UniqueIdCounter++;
                                        CreateTRNDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn]);

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF != null)
                                        {
                                            for (int reff = 0; reff < o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF.Count; reff++)
                                            {
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].ERAFileID = ERAFileID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_ISAID = ISAID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_STID = CurrentSTID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_HLID = CurrentHLId;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REF_TRNID = CurrentTRNId;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff].REFID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                UniqueIdCounter++;
                                                CreateREFDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_REF[reff]);
                                            }
                                        }

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT != null)
                                        {
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.ERAFileID = ERAFileID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_ISAID = ISAID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_STID = CurrentSTID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_HLID = CurrentHLId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMT_TRNID = CurrentTRNId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT.AMTID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                            UniqueIdCounter++;
                                            CreateAMTDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_AMT);
                                        }

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP != null)
                                        {
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.ERAFileID = ERAFileID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_ISAID = ISAID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_STID = CurrentSTID;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_HLID = CurrentHLId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTP_TRNID = CurrentTRNId;
                                            o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP.DTPID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                            UniqueIdCounter++;
                                            CreateDTPDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_DTP);
                                        }

                                        if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC != null)
                                        {
                                            for (int trnsvc = 0; trnsvc < o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC.Count; trnsvc++)
                                            {
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].ERAFileID = ERAFileID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_ISAID = ISAID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_STID = CurrentSTID;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_HLID = CurrentHLId;
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_TRNID = CurrentTRNId;
                                                CurrentSVCId = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVCID = CurrentSVCId;
                                                UniqueIdCounter++;
                                                CreateSVCDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc]);

                                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF != null)
                                                {
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.ERAFileID = ERAFileID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_ISAID = ISAID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_STID = CurrentSTID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_HLID = CurrentHLId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_TRNID = CurrentTRNId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REF_SVCID = CurrentSVCId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF.REFID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                    UniqueIdCounter++;
                                                    CreateREFDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_REF);
                                                }

                                                if (o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP != null)
                                                {
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.ERAFileID = ERAFileID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_ISAID = ISAID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_STID = CurrentSTID;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_HLID = CurrentHLId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_TRNID = CurrentTRNId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTP_SVCID = CurrentSVCId;
                                                    o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP.DTPID = Convert.ToInt64(dtUniqueIDs.Rows[UniqueIdCounter][0]);
                                                    UniqueIdCounter++;
                                                    CreateDTPDatatable(o276_ISA.ISA_ST[st].ST_HL[hl].HL_TRN[trn].TRN_SVC[trnsvc].SVC_DTP);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (ediException ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GenerateSegmentIDs, ex, ActivityOutCome.Failure);
            }
        }

        private void CreateISADataTable()
        {
            Cls_276X212_ISAs oISAs = new Cls_276X212_ISAs();
            try
            {
                if (o276_ISA != null)
                {
                    oISAs.Add(o276_ISA);
                }
                Cls_276X212_ISA[] oISA = new Cls_276X212_ISA[oISAs.Count];
                oISAs.CopyTo(oISA, 0);
                if (oISA != null)
                {
                    var _test = (from r in oISA select r).ToList();
                    if (dtISA == null)
                    {
                        dtISA = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.ISA);
                    }
                    oISA = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oISAs != null)
                {
                    oISAs.Dispose();
                    oISAs = null;
                }
            }
        }

        private void CreateGSDatatable(Cls_276X212_GS _oGS)
        {
            Cls_276X212_GSs oGSs = new Cls_276X212_GSs();
            try
            {
                if (_oGS != null)
                {
                    oGSs.Add(_oGS);
                }

                Cls_276X212_GS[] oGS = new Cls_276X212_GS[oGSs.Count];
                oGSs.CopyTo(oGS, 0);
                if (oGS != null)
                {
                    var _test = (from r in oGS select r).ToList();

                    if (dtGS == null)
                    {
                        dtGS = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.GS);
                    }
                    oGS = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oGSs != null)
                {
                    oGSs.Dispose();
                    oGSs = null;
                }
            }
        }

        private void CreateSTDatatable(Cls_276X212_ST _oST)
        {
            Cls_276X212_STs STs = new Cls_276X212_STs();
            try
            {
                if (_oST != null)
                {
                    STs.Add(_oST);
                }

                Cls_276X212_ST[] oST = new Cls_276X212_ST[STs.Count];
                STs.CopyTo(oST, 0);
                if (oST != null)
                {
                    var _test = (from r in oST select r).ToList();
                    if (dtST == null)
                    {
                        dtST = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.ST);
                    }
                    oST = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (STs != null)
                {
                    STs.Dispose();
                    STs = null;
                }
            }

        }

        private void CreateBHTDatatable(Cls_276X212_BHT _oBHT)
        {
            Cls_276X212_BHTs oBHTs = new Cls_276X212_BHTs();
            try
            {
                if (_oBHT != null)
                {
                    oBHTs.Add(_oBHT);
                }

                Cls_276X212_BHT[] oBHT = new Cls_276X212_BHT[oBHTs.Count];
                oBHTs.CopyTo(oBHT, 0);
                if (oBHT != null)
                {
                    var _test = (from r in oBHT select r).ToList();
                    if (dtBHT == null)
                    {
                        dtBHT = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.BHT);
                    }
                    oBHT = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oBHTs != null)
                {
                    oBHTs.Dispose();
                    oBHTs = null;
                }
            }
        }

        private void CreateHLDatatable(Cls_276X212_HL _oHL)
        {
            Cls_276X212_HLs oHLs = new Cls_276X212_HLs();
            try
            {
                if (_oHL != null)
                {
                    oHLs.Add(_oHL);
                }

                Cls_276X212_HL[] oHL = new Cls_276X212_HL[oHLs.Count];
                oHLs.CopyTo(oHL, 0);
                if (oHL != null)
                {
                    var _test = (from r in oHL select r).ToList();
                    if (dtHL == null)
                    {
                        dtHL = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.HL);
                    }
                    oHL = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oHLs != null)
                {
                    oHLs.Dispose();
                    oHLs = null;
                }
            }
        }

        private void CreateNMDatatable(Cls_276X212_NM _oNM)
        {
            Cls_276X212_NMs oNMs = new Cls_276X212_NMs();
            try
            {
                if (_oNM != null)
                {
                    oNMs.Add(_oNM);
                }

                Cls_276X212_NM[] oNM = new Cls_276X212_NM[oNMs.Count];
                oNMs.CopyTo(oNM, 0);
                if (oNM != null)
                {
                    var _test = (from r in oNM select r).ToList();
                    if (dtNM == null)
                    {
                        dtNM = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.NM);
                    }
                    oNM = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oNMs != null)
                {
                    oNMs.Dispose();
                    oNMs = null;
                }
            }
        }

        private void CreateDMGDatatable(Cls_276X212_DMG _oDMG)
        {
            Cls_276X212_DMGs oDMGs = new Cls_276X212_DMGs();
            try
            {
                if (_oDMG != null)
                {
                    oDMGs.Add(_oDMG);
                }

                Cls_276X212_DMG[] oDMG = new Cls_276X212_DMG[oDMGs.Count];
                oDMGs.CopyTo(oDMG, 0);
                if (oDMG != null)
                {
                    var _test = (from r in oDMG select r).ToList();
                    if (dtDMG == null)
                    {
                        dtDMG = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.DMG);
                    }
                    oDMG = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oDMGs != null)
                {
                    oDMGs.Dispose();
                    oDMGs = null;
                }
            }
        }

        private void CreateTRNDatatable(Cls_276X212_TRN _oTRN)
        {
            Cls_276X212_TRNs oTRNs = new Cls_276X212_TRNs();
            try
            {
                if (_oTRN != null)
                {
                    oTRNs.Add(_oTRN);
                }

                Cls_276X212_TRN[] oTRN = new Cls_276X212_TRN[oTRNs.Count];
                oTRNs.CopyTo(oTRN, 0);
                if (oTRN != null)
                {
                    var _test = (from r in oTRN select r).ToList();
                    if (dtTRN == null)
                    {
                        dtTRN = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.TRN);
                    }
                    oTRN = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oTRNs != null)
                {
                    oTRNs.Dispose();
                    oTRNs = null;
                }
            }
        }

        private void CreateREFDatatable(Cls_276X212_REF _oREF)
        {
            Cls_276X212_REFs oREFs = new Cls_276X212_REFs();
            try
            {
                if (_oREF != null)
                {
                    oREFs.Add(_oREF);
                }

                Cls_276X212_REF[] oREF = new Cls_276X212_REF[oREFs.Count];
                oREFs.CopyTo(oREF, 0);
                if (oREF != null)
                {
                    var _test = (from r in oREF select r).ToList();
                    if (dtREF == null)
                    {
                        dtREF = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.REF);
                    }

                    oREF = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oREFs != null)
                {
                    oREFs.Dispose();
                    oREFs = null;
                }
            }
        }

        private void CreateAMTDatatable(Cls_276X212_AMT _oAMT)
        {
            Cls_276X212_AMTs oAMTs = new Cls_276X212_AMTs();
            try
            {
                if (_oAMT != null)
                {
                    oAMTs.Add(_oAMT);
                }

                Cls_276X212_AMT[] oAMT = new Cls_276X212_AMT[oAMTs.Count];
                oAMTs.CopyTo(oAMT, 0);
                if (oAMT != null)
                {
                    var _test = (from r in oAMT select r).ToList();
                    if (dtAMT == null)
                    {
                        dtAMT = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.AMT);
                    }
                    oAMT = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oAMTs != null)
                {
                    oAMTs.Dispose();
                    oAMTs = null;
                }
            }
        }

        private void CreateDTPDatatable(Cls_276X212_DTP _oDTP)
        {
            Cls_276X212_DTPs oDTPs = new Cls_276X212_DTPs();
            try
            {
                if (_oDTP != null)
                {
                    oDTPs.Add(_oDTP);
                }

                Cls_276X212_DTP[] oDTP = new Cls_276X212_DTP[oDTPs.Count];
                oDTPs.CopyTo(oDTP, 0);
                if (oDTP != null)
                {
                    var _test = (from r in oDTP select r).ToList();
                    if (dtDTP == null)
                    {
                        dtDTP = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.DTP);
                    }
                    oDTP = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oDTPs != null)
                {
                    oDTPs.Dispose();
                    oDTPs = null;
                }
            }
        }

        private void CreateSVCDatatable(Cls_276X212_SVC _oSVC)
        {
            Cls_276X212_SVCs oSVCs = new Cls_276X212_SVCs();
            try
            {
                if (_oSVC != null)
                {
                    oSVCs.Add(_oSVC);
                }

                Cls_276X212_SVC[] oSVC = new Cls_276X212_SVC[oSVCs.Count];
                oSVCs.CopyTo(oSVC, 0);
                if (oSVC != null)
                {
                    var _test = (from r in oSVC select r).ToList();
                    if (dtSVC == null)
                    {
                        dtSVC = ConvertToDataTable(_test);
                    }
                    else
                    {
                        AddToDataTable(_test, Segment.SVC);
                    }
                    oSVC = null;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (oSVCs != null)
                {
                    oSVCs.Dispose();
                    oSVCs = null;
                }
            }
        }

        private DataTable ConvertToDataTable<TSource>(IEnumerable<TSource> source)
        {
            var dt = new DataTable();
            try
            {
                var props = typeof(TSource).GetProperties();
                dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
                source.ToList().ForEach(i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.GeneralOperation, ex, ActivityOutCome.Failure);
            }
            return dt;
        }

        private void AddToDataTable<TSource>(IEnumerable<TSource> source, Segment eSegment)
        {
            try
            {


                var props = typeof(TSource).GetProperties();

                switch (eSegment)
                {
                    //dtISA, dtGS, dtST, dtBHT, dtHL, dtNM, dtPER, dtTRN, dtSVC, dtAMT, dtREF, dtDTP
                    case Segment.ISA:
                        source.ToList().ForEach(i => dtISA.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.GS:
                        source.ToList().ForEach(i => dtGS.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.ST:
                        source.ToList().ForEach(i => dtST.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.BHT:
                        source.ToList().ForEach(i => dtBHT.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.HL:
                        source.ToList().ForEach(i => dtHL.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.NM:
                        source.ToList().ForEach(i => dtNM.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.DMG:
                        source.ToList().ForEach(i => dtDMG.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.DTP:
                        source.ToList().ForEach(i => dtDTP.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.TRN:
                        source.ToList().ForEach(i => dtTRN.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.SVC:
                        source.ToList().ForEach(i => dtSVC.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.REF:
                        source.ToList().ForEach(i => dtREF.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                    case Segment.AMT:
                        source.ToList().ForEach(i => dtAMT.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray()));
                        break;
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
        }

        private void RemoveUnwantedColumnFromERATables()
        {
            try
            {
                if (dtISA != null)
                {
                    dtISA.Columns.Remove("ISA_GS");
                    dtISA.Columns.Remove("ISA_ST");
                }

                if (dtST != null)
                {
                    dtST.Columns.Remove("ST_BHT");
                    dtST.Columns.Remove("ST_HL");
                }

                if (dtHL != null)
                {
                    dtHL.Columns.Remove("HL_NM");
                    dtHL.Columns.Remove("HL_DMG");
                    dtHL.Columns.Remove("HL_TRN");
                }

                if (dtTRN != null)
                {
                    dtTRN.Columns.Remove("TRN_REF");
                    dtTRN.Columns.Remove("TRN_AMT");
                    dtTRN.Columns.Remove("TRN_DTP");
                    dtTRN.Columns.Remove("TRN_SVC");
                }

                if (dtSVC != null)
                {
                    dtSVC.Columns.Remove("SVC_REF");
                    dtSVC.Columns.Remove("SVC_DTP");
                }
            }
            catch (Exception ex)
            {
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
        }

        private bool Save276X212InDB()
        {

            SqlConnection conn = new System.Data.SqlClient.SqlConnection(_DataBaseConnectionString);
            conn.Open();
            //SqlTransaction tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            SqlBulkCopy bulkCopy = null;
            try
            {
                bulkCopy = new System.Data.SqlClient.SqlBulkCopy(_DataBaseConnectionString);
                bulkCopy.BulkCopyTimeout = 0;

                if (dtISA != null && dtISA.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_ISA";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("ISA01_AuthorInfoQual", "ISA01_AuthorInfoQual");
                    bulkCopy.ColumnMappings.Add("ISA02_AuthorInfo", "ISA02_AuthorInfo");
                    bulkCopy.ColumnMappings.Add("ISA03_SecurityInfoQual", "ISA03_SecurityInfoQual");
                    bulkCopy.ColumnMappings.Add("ISA04_SecurityInfo", "ISA04_SecurityInfo");
                    bulkCopy.ColumnMappings.Add("ISA05_IntrChngIDQual", "ISA05_IntrChngIDQual");
                    bulkCopy.ColumnMappings.Add("ISA06_IntrChngSenderID", "ISA06_IntrChngSenderID");
                    bulkCopy.ColumnMappings.Add("ISA07_IntrChngIDQual", "ISA07_IntrChngIDQual");
                    bulkCopy.ColumnMappings.Add("ISA08_IntrChngReceiverID", "ISA08_IntrChngReceiverID");
                    bulkCopy.ColumnMappings.Add("ISA09_IntrChngDate", "ISA09_IntrChngDate");
                    bulkCopy.ColumnMappings.Add("ISA10_IntrChngTime", "ISA10_IntrChngTime");
                    bulkCopy.ColumnMappings.Add("ISA11_IntrChngRepetitionSeperator", "ISA11_IntrChngRepetitionSeperator");
                    bulkCopy.ColumnMappings.Add("ISA12_IntrChngControlVersionNo", "ISA12_IntrChngControlVersionNo");
                    bulkCopy.ColumnMappings.Add("ISA13_IntrChngControlNo", "ISA13_IntrChngControlNo");
                    bulkCopy.ColumnMappings.Add("ISA14_AckwRequested", "ISA14_AckwRequested");
                    bulkCopy.ColumnMappings.Add("ISA15_UsageIndicator", "ISA15_UsageIndicator");
                    bulkCopy.ColumnMappings.Add("ISA16_ComponentElementSeparator", "ISA16_ComponentElementSeparator");
                    bulkCopy.WriteToServer(dtISA);
                }

                if (dtGS != null && dtGS.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_GS";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("GS_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("GSID", "GSID");
                    bulkCopy.ColumnMappings.Add("GS01_StatusNotification", "GS01_StatusNotification");
                    bulkCopy.ColumnMappings.Add("GS02_SenderID", "GS02_SenderID");
                    bulkCopy.ColumnMappings.Add("GS03_ReceiverID", "GS03_ReceiverID");
                    bulkCopy.ColumnMappings.Add("GS04_FunctionalGroupDate", "GS04_FunctionalGroupDate");
                    bulkCopy.ColumnMappings.Add("GS05_FunctionalGroupTime", "GS05_FunctionalGroupTime");
                    bulkCopy.ColumnMappings.Add("GS06_GroupControlNumber", "GS06_GroupControlNumber");
                    bulkCopy.ColumnMappings.Add("GS07_ResponsibleAgencyCode", "GS07_ResponsibleAgencyCode");
                    bulkCopy.ColumnMappings.Add("GS08_VersionORIndustryIdentifier", "GS08_VersionORIndustryIdentifier");
                    bulkCopy.WriteToServer(dtGS);
                }

                if (dtST != null && dtST.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_ST";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("ST_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("STID", "STID");
                    bulkCopy.ColumnMappings.Add("ST01_IdentifierCode", "ST01_IdentifierCode");
                    bulkCopy.ColumnMappings.Add("ST02_TransactioNSetControlNumber", "ST02_TransactioNSetControlNumber");
                    bulkCopy.ColumnMappings.Add("ST03_ConventionReference", "ST03_ConventionReference");
                    bulkCopy.WriteToServer(dtST);
                }

                if (dtBHT != null && dtBHT.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_BHT";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("BHT_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("BHT_STID", "STID");
                    bulkCopy.ColumnMappings.Add("BHTID", "BHTID");
                    bulkCopy.ColumnMappings.Add("BHT01_StructureCode", "BHT01_StructureCode");
                    bulkCopy.ColumnMappings.Add("BHT02_PurposeCode", "BHT02_PurposeCode");
                    bulkCopy.ColumnMappings.Add("BHT03_ReferenceIdentification", "BHT03_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("BHT04_TransactionDate", "BHT04_TransactionDate");
                    bulkCopy.ColumnMappings.Add("BHT05_TransactionTime", "BHT05_TransactionTime");
                    bulkCopy.ColumnMappings.Add("BHT06_TransactionTypeCode", "BHT06_TransactionTypeCode");
                    bulkCopy.WriteToServer(dtBHT);
                }

                if (dtHL != null && dtHL.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_HL";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("HL_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("HL_STID", "STID");
                    bulkCopy.ColumnMappings.Add("ParentHLID", "ParentHLID");
                    bulkCopy.ColumnMappings.Add("HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("HL01_HLSegmentId", "HL01_HLSegmentId");
                    bulkCopy.ColumnMappings.Add("HL02_HLParentId", "HL02_HLParentId");
                    bulkCopy.ColumnMappings.Add("HL03_LevelCode", "HL03_LevelCode");
                    bulkCopy.ColumnMappings.Add("HL04_ChildCode", "HL04_ChildCode");
                    bulkCopy.WriteToServer(dtHL);
                }

                if (dtNM != null && dtNM.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_NM";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("NM_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("NM_STID", "STID");
                    bulkCopy.ColumnMappings.Add("NM_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("NMID", "NMID");
                    bulkCopy.ColumnMappings.Add("NM101_EntityIdCode", "NM101_EntityIdCode");
                    bulkCopy.ColumnMappings.Add("NM102_EntityTypeQualifier", "NM102_EntityTypeQualifier");
                    bulkCopy.ColumnMappings.Add("NM103_LastName", "NM103_LastName");
                    bulkCopy.ColumnMappings.Add("NM104_FirstName", "NM104_FirstName");
                    bulkCopy.ColumnMappings.Add("NM105_MiddleName", "NM105_MiddleName");
                    bulkCopy.ColumnMappings.Add("NM106_Prefix", "NM106_Prefix");
                    bulkCopy.ColumnMappings.Add("NM107_Suffix", "NM107_Suffix");
                    bulkCopy.ColumnMappings.Add("NM108_IdentificationCodeQualifier", "NM108_IdentificationCodeQualifier");
                    bulkCopy.ColumnMappings.Add("NM109_IdentificationCode", "NM109_IdentificationCode");
                    bulkCopy.ColumnMappings.Add("NM110_EntityRelationShipCode", "NM110_EntityRelationShipCode");
                    bulkCopy.ColumnMappings.Add("NM111_EntityIdentifierCode", "NM111_EntityIdentifierCode");
                    bulkCopy.ColumnMappings.Add("NM112_NameLastOROrganizatioName", "NM112_NameLastOROrganizatioName");
                    bulkCopy.WriteToServer(dtNM);
                }

                if (dtDMG != null && dtDMG.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_DMG";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("DMG_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("DMG_STID", "STID");
                    bulkCopy.ColumnMappings.Add("DMG_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("DMGID", "DMGID");
                    bulkCopy.ColumnMappings.Add("DMG01_DateTimePeriodFormatQualifier", "DMG01_DateTimePeriodFormatQualifier");
                    bulkCopy.ColumnMappings.Add("DMG02_DateTimePeriod", "DMG02_DateTimePeriod");
                    bulkCopy.ColumnMappings.Add("DMG03_GenderCode", "DMG03_GenderCode");
                    bulkCopy.ColumnMappings.Add("DMG04_MaritalStatusCode", "DMG04_MaritalStatusCode");
                    bulkCopy.ColumnMappings.Add("DMG05_01_RaceorEthnicityCode", "DMG05_01_RaceorEthnicityCode");
                    bulkCopy.ColumnMappings.Add("DMG05_02_QualifierCode", "DMG05_02_QualifierCode");
                    bulkCopy.ColumnMappings.Add("DMG05_03_IndustryCode", "DMG05_03_IndustryCode");
                    bulkCopy.ColumnMappings.Add("DMG06_CitizenShipStatusCode", "DMG06_CitizenShipStatusCode");
                    bulkCopy.ColumnMappings.Add("DMG07_CountryCode", "DMG07_CountryCode");
                    bulkCopy.ColumnMappings.Add("DMG08_VerificationCode", "DMG08_VerificationCode");
                    bulkCopy.ColumnMappings.Add("DMG09_Quantity", "DMG09_Quantity");
                    bulkCopy.ColumnMappings.Add("DMG010_CodeListQualifierCode", "DMG010_CodeListQualifierCode");
                    bulkCopy.ColumnMappings.Add("DMG011_IndustryCode", "DMG011_IndustryCode");
                    bulkCopy.WriteToServer(dtDMG);
                }

                if (dtTRN != null && dtTRN.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_TRN";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("TRN_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("TRN_STID", "STID");
                    bulkCopy.ColumnMappings.Add("TRN_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("TRN01_TraceTypeID", "TRN01_TraceTypeID");
                    bulkCopy.ColumnMappings.Add("TRN02_ReferenceIdentification", "TRN02_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("TRN03_OriginatingCompanyIdentifier", "TRN03_OriginatingCompanyIdentifier");
                    bulkCopy.ColumnMappings.Add("TRN04_ReferenceIdentification", "TRN04_ReferenceIdentification");
                    bulkCopy.WriteToServer(dtTRN);
                }

                if (dtSVC != null && dtSVC.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_SVC";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("SVC_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("SVC_STID", "STID");
                    bulkCopy.ColumnMappings.Add("SVC_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("SVC_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("SVC01_01_ProductORServiceIDQualifier", "SVC01_01_ProductORServiceIDQualifier");
                    bulkCopy.ColumnMappings.Add("SVC01_02_ProductORServiceID", "SVC01_02_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC01_03_ProcedureModifier", "SVC01_03_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_04_ProcedureModifier", "SVC01_04_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_05_ProcedureModifier", "SVC01_05_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_06_ProcedureModifier", "SVC01_06_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC01_07_Description", "SVC01_07_Description");
                    bulkCopy.ColumnMappings.Add("SVC01_08_ProductORServiceID", "SVC01_08_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC02_MonetaryAmount", "SVC02_MonetaryAmount");
                    bulkCopy.ColumnMappings.Add("SVC03_MonetaryAmount", "SVC03_MonetaryAmount");
                    bulkCopy.ColumnMappings.Add("SVC04_ProductORServiceID", "SVC04_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC05_Quantity", "SVC05_Quantity");
                    bulkCopy.ColumnMappings.Add("SVC06_01_ProductORServiceIDQualifier", "SVC06_01_ProductORServiceIDQualifier");
                    bulkCopy.ColumnMappings.Add("SVC06_02_ProductORServiceID", "SVC06_02_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC06_03_ProcedureModifier", "SVC06_03_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_04_ProcedureModifier", "SVC06_04_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_05_ProcedureModifier", "SVC06_05_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_06_ProcedureModifier", "SVC06_06_ProcedureModifier");
                    bulkCopy.ColumnMappings.Add("SVC06_07_Description", "SVC06_07_Description");
                    bulkCopy.ColumnMappings.Add("SVC06_08_ProductORServiceID", "SVC06_08_ProductORServiceID");
                    bulkCopy.ColumnMappings.Add("SVC07_Quantity", "SVC07_Quantity");
                    bulkCopy.WriteToServer(dtSVC);
                }

                if (dtREF != null && dtREF.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_REF";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("REF_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("REF_STID", "STID");
                    bulkCopy.ColumnMappings.Add("REF_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("REF_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("REF_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("REFID", "REFID");
                    bulkCopy.ColumnMappings.Add("REF01_ReferenceIdentificationQualifier", "REF01_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF02_ReferenceIdentification", "REF02_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("REF03_Description", "REF03_Description");
                    bulkCopy.ColumnMappings.Add("REF04_01_ReferenceIdentificationQualifier", "REF04_01_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_02_ReferenceIdentification", "REF04_02_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("REF04_03_ReferenceIdentificationQualifier", "REF04_03_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_04_ReferenceIdentification", "REF04_04_ReferenceIdentification");
                    bulkCopy.ColumnMappings.Add("REF04_05_ReferenceIdentificationQualifier", "REF04_05_ReferenceIdentificationQualifier");
                    bulkCopy.ColumnMappings.Add("REF04_06_ReferenceIdentification", "REF04_06_ReferenceIdentification");
                    bulkCopy.WriteToServer(dtREF);
                }

                if (dtAMT != null && dtAMT.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "EDIRequest_276Segment_AMT";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("AMT_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("AMT_STID", "STID");
                    bulkCopy.ColumnMappings.Add("AMT_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("AMT_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("AMTID", "AMTID");
                    bulkCopy.ColumnMappings.Add("AMT01_AmountQualifierCode", "AMT01_AmountQualifierCode");
                    bulkCopy.ColumnMappings.Add("AMT02_MonetaryAmount", "AMT02_MonetaryAmount");
                    bulkCopy.ColumnMappings.Add("AMT03_CreditDebitFlagCode", "AMT03_CreditDebitFlagCode");
                    bulkCopy.WriteToServer(dtAMT);
                }

                if (dtDTP != null && dtDTP.Rows.Count > 0)
                {
                    bulkCopy.DestinationTableName = "dbo.EDIRequest_276Segment_DTP";
                    bulkCopy.ColumnMappings.Clear();
                    bulkCopy.ColumnMappings.Add("ERAFileID", "ERAFileID");
                    bulkCopy.ColumnMappings.Add("DTP_ISAID", "ISAID");
                    bulkCopy.ColumnMappings.Add("DTP_STID", "STID");
                    bulkCopy.ColumnMappings.Add("DTP_HLID", "HLID");
                    bulkCopy.ColumnMappings.Add("DTP_TRNID", "TRNID");
                    bulkCopy.ColumnMappings.Add("DTP_SVCID", "SVCID");
                    bulkCopy.ColumnMappings.Add("DTPID", "DTPID");
                    bulkCopy.ColumnMappings.Add("DTP01_DateTimeQualifier", "DTP01_DateTimeQualifier");
                    bulkCopy.ColumnMappings.Add("DTP02_DateTimePeriodFormatQualifier", "DTP02_DateTimePeriodFormatQualifier");
                    bulkCopy.ColumnMappings.Add("DTP03_DateTimePeriod", "DTP03_DateTimePeriod");
                    bulkCopy.WriteToServer(dtDTP);
                }
                bulkCopy.Close();
                bulkCopy = null;
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "276 Request Parse", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsQEDILogs.ExceptionLog(ActivityModule.EDI276Operation, ActivityType.SaveToDatabase, ex, ActivityOutCome.Failure);
            }
            finally
            {
                if (conn != null) { conn.Dispose(); conn = null; }
                bulkCopy = null;

            }
            return true;
        }

        #endregion
        #region "IDisposable Support"

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {


                    if (oDB != null)
                    {
                        oDB.Dispose();
                        oDB = null;
                    }
                    if (oDBPara != null)
                    {
                        oDBPara.Dispose();
                        oDBPara = null;
                    }

                    oEdiDoc = null;
                    oSchemas = null;
                    oInterchange = null;
                    oGroup = null;
                    oTransactionSet = null;
                    oSegment = null;

                    _MessageBoxCaption = string.Empty;
                    _DataBaseConnectionString = string.Empty;
                    _EDIDataBaseConnectionString = string.Empty;
                    ClaimNumber = string.Empty;
                    seffilepath_276_005010X214_SemRef = string.Empty;
                    sPath = string.Empty;
                    _MessageBoxCaption = string.Empty;
                    _DataBaseConnectionString = string.Empty;
                    _EDIDataBaseConnectionString = string.Empty;
                    ClaimNumber = string.Empty;
                    AUSID = string.Empty;
                    x276RequestFilePath = string.Empty;
                    TempFolderpath = string.Empty;

                    if (o276_ISA != null) { o276_ISA.Dispose(); o276_ISA = null; }
                    if (o276_GS != null) { o276_GS.Dispose(); o276_ISA = null; }
                    if (o276_ST != null) { o276_ST.Dispose(); o276_ST = null; }
                    if (o276_BHT != null) { o276_BHT.Dispose(); o276_BHT = null; }
                    if (o276_HL != null) { o276_HL.Dispose(); o276_HL = null; }
                    if (o276_NM != null) { o276_NM.Dispose(); o276_NM = null; }
                    if (o276_DMG != null) { o276_DMG.Dispose(); o276_DMG = null; }
                    if (o276_TRN != null) { o276_TRN.Dispose(); o276_TRN = null; }
                    if (o276_REF != null) { o276_REF.Dispose(); o276_REF = null; }
                    if (o276_AMT != null) { o276_AMT.Dispose(); o276_AMT = null; }
                    if (o276_DTP != null) { o276_DTP.Dispose(); o276_DTP = null; }
                    if (o276_SVC != null) { o276_SVC.Dispose(); o276_ISA = null; }

                    if (dtUniqueIDs != null) { dtUniqueIDs.Dispose(); dtUniqueIDs = null; }
                    if (dtISA != null) { dtISA.Dispose(); dtISA = null; }
                    if (dtGS != null) { dtGS.Dispose(); dtGS = null; }
                    if (dtST != null) { dtST.Dispose(); dtST = null; }
                    if (dtBHT != null) { dtBHT.Dispose(); dtBHT = null; }
                    if (dtHL != null) { dtHL.Dispose(); dtHL = null; }
                    if (dtNM != null) { dtNM.Dispose(); dtNM = null; }
                    if (dtDMG != null) { dtDMG.Dispose(); dtDMG = null; }
                    if (dtAMT != null) { dtAMT.Dispose(); dtAMT = null; }
                    if (dtTRN != null) { dtTRN.Dispose(); dtTRN = null; }
                    if (dtSVC != null) { dtSVC.Dispose(); dtSVC = null; }
                    if (dtSTC != null) { dtSTC.Dispose(); dtSTC = null; }
                    if (dtREF != null) { dtREF.Dispose(); dtREF = null; }
                    if (dtDTP != null) { dtDTP.Dispose(); dtDTP = null; }

                    if (dtClearingHouse != null) { dtClearingHouse.Dispose(); ; dtClearingHouse = null; }
                    if (dtSubmitter != null) { dtSubmitter.Dispose(); dtSubmitter = null; }
                    if (dtEDISetting != null) { dtEDISetting.Dispose(); dtEDISetting = null; }
                    if (dtAlphaSetting != null) { dtAlphaSetting.Dispose(); dtAlphaSetting = null; }
                    if (dtClaimData != null) { dtClaimData.Dispose(); dtClaimData = null; }
                    if (dtClaimLines != null) { dtClaimLines.Dispose(); dtClaimLines = null; }
                    if (dtClaimPatient != null) { dtClaimPatient.Dispose(); dtClaimPatient = null; }
                    if (dtClaimClinic != null) { dtClaimClinic.Dispose(); ; dtClaimClinic = null; }
                    if (dtPatientInsurance != null) { dtPatientInsurance.Dispose(); dtPatientInsurance = null; }
                    if (dtFacility != null) { dtFacility.Dispose(); dtFacility = null; }
                    if (dtBillingProvider != null) { dtBillingProvider.Dispose(); dtBillingProvider = null; }
                    if (dtRefferingProvider != null) { dtRefferingProvider.Dispose(); dtRefferingProvider = null; }
                    if (dtRenderingProvider != null) { dtRenderingProvider.Dispose(); dtRenderingProvider = null; }
                    if (dtMidLevelID != null) { dtMidLevelID.Dispose(); dtMidLevelID = null; }
                    if (dtBillingProviderTaxonomy != null) { dtBillingProviderTaxonomy.Dispose(); dtBillingProviderTaxonomy = null; }
                    if (dtMasterSetting != null) { dtMasterSetting.Dispose(); dtMasterSetting = null; }
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
