using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace gloCMSEDI
{
    public class clsgloBilling
    {
        private string _messageBoxCaption = "gloPM";

          #region "Constructor & Distructor"

        private string _databaseconnectionstring = "";
       // private string _emrdatabaseconnectionstring = "";
        

        //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _ClinicID = 0;
        public Int64 ClinicID
        {
            get { return _ClinicID; }
            set { _ClinicID = value; }
        }


        public clsgloBilling(string DatabaseConnectionString)
        {
            _databaseconnectionstring = DatabaseConnectionString;
            //_emrdatabaseconnectionstring = EMRDatabaseConnectionString;
            //Code added on 9/04/2008 -by Sagar Ghodke for implementing ClinicID;
            if (appSettings["ClinicID"] != null)
            {
                if (appSettings["ClinicID"] != "")
                { _ClinicID = Convert.ToInt64(appSettings["ClinicID"]); }
                else { _ClinicID = 0; }
            }
            else
            { _ClinicID = 0; }
            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    _messageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    _messageBoxCaption = "gloPM";
                }
            }
            else
            { _messageBoxCaption = "gloPM"; }


        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {

                }
            }
            disposed = true;
        }

        ~clsgloBilling()
        {
            Dispose(false);
        }

        #endregion

        public Transaction GetHCFATransactionDetails_Old(Int64 TransactionID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtTrans = new DataTable();
            Transaction oTransaction = new Transaction();
            TransactionLine oLine = null;
            try
            {
                oDB.Connect(false);
                // For BL_Transaction_MST Table.
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_HCFA_Transaction", oDBParameters, out dtTrans);

                if (dtTrans != null)
                {
                    if (dtTrans.Rows.Count > 0)
                    {
                        //nTransactionID, nMasterAppointmentID, nAppointmentID, nVisitID, nOnsiteDate, nInjuryDate, 
                        //nUnableToWorkFromDate, nUnableToWorkTillDate, nTransactionDate, sCaseNoPrefix, nClaimNo, 
                        //nPatientID, nTransactionProviderID, sMaritalStatus, sFacilityCode, sFacilityDescription, 
                        //nTransactionType, nClinicID, nTransactionStatusID, sState, nHopitalizationDateFrom, nHopitalizationDateTo,
                        //bOutSideLab, dOutSideLabCharges, bAutoClaim, nAccidentDate, bWorkersComp
                        oTransaction.TransactionID = TransactionID;
                        oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                        oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                        oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                        oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                        oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                        oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                        oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                        oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                        oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                        oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);
                        oTransaction.ReferralProviderID = Convert.ToInt64(dtTrans.Rows[0]["nReferralID"]);
                        oTransaction.SendCounter = Convert.ToInt32(dtTrans.Rows[0]["nSendCounter"]);
                        oTransaction.SendToRejection = Convert.ToInt32(dtTrans.Rows[0]["nSendToRejection"]);
                        oTransaction.LastStatusId = Convert.ToInt64(dtTrans.Rows[0]["nLastStatusId"]);
                        oTransaction.OtherAccident = Convert.ToBoolean(dtTrans.Rows[0]["bOtherAccident"]);
                        oTransaction.OtherAccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nOtherAccidentDate"]);


                        //Code added on 20090505 By - Sagar Ghodke

                        oTransaction.TransactionUserID = Convert.ToInt64(dtTrans.Rows[0]["nUserID"]);
                        oTransaction.TransactionUserName = Convert.ToString(dtTrans.Rows[0]["sUserName"]);

                        //End code add 20090505,Sagar Ghodke



                        //
                        oTransaction.Transaction_Details.HCFA_PatientFName = Convert.ToString(dtTrans.Rows[0]["PatientFName"]);
                        oTransaction.Transaction_Details.HCFA_PatientMName = Convert.ToString(dtTrans.Rows[0]["PatientMName"]);
                        oTransaction.Transaction_Details.HCFA_PatientLName = Convert.ToString(dtTrans.Rows[0]["PatientLName"]);
                        oTransaction.Transaction_Details.HCFA_PatientCode = Convert.ToString(dtTrans.Rows[0]["PatientCode"]);
                        oTransaction.Transaction_Details.HCFA_PatientSSN = Convert.ToString(dtTrans.Rows[0]["PatientSSN"]);
                        if (dtTrans.Rows[0]["PatientDOB"] != DBNull.Value)
                        {
                            oTransaction.Transaction_Details.HCFA_PatientDOB = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTrans.Rows[0]["PatientDOB"]));
                        }
                        oTransaction.Transaction_Details.HCFA_PatientGender = Convert.ToString(dtTrans.Rows[0]["PatientGender"]);

                        oTransaction.Transaction_Details.HCFA_PatientAddress1 = Convert.ToString(dtTrans.Rows[0]["PatientAddr1"]);
                        oTransaction.Transaction_Details.HCFA_PatientAddress2 = Convert.ToString(dtTrans.Rows[0]["PatientAddr2"]);
                        oTransaction.Transaction_Details.HCFA_PatientCity = Convert.ToString(dtTrans.Rows[0]["PatientCity"]);
                        oTransaction.Transaction_Details.HCFA_PatientState = Convert.ToString(dtTrans.Rows[0]["PatientState"]);
                        oTransaction.Transaction_Details.HCFA_PatientZip = Convert.ToString(dtTrans.Rows[0]["PatientZip"]);
                        oTransaction.Transaction_Details.HCFA_PatientPhone = Convert.ToString(dtTrans.Rows[0]["PatientPhone"]);
                        oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus = Convert.ToString(dtTrans.Rows[0]["PatientEmploymentStatus"]);
                        oTransaction.Transaction_Details.HCFA_PatientEmploymentType = Convert.ToString(dtTrans.Rows[0]["sEmploymentType"]);

                        //Employment Status with refrence to occupation control in gloPatient
                        //1.Employed,//2.UnEmployed,//3.Retired,//4.Self-Employed,//5.Student

                        //Employement type with refrence to occupation control in gloPatient
                        //1.Full Time//2.Part Time

                        switch (oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus.Trim().ToUpper())
                        {
                            case "EMPLOYED":
                                { oTransaction.Transaction_Details.HCFA_IsEmployed = true; }
                                break;
                            case "UNEMPLOYED":
                                break;
                            case "SELF-EMPLOYED":
                                break;
                            case "STUDENT":
                                {
                                    switch (oTransaction.Transaction_Details.HCFA_PatientEmploymentType.Trim().ToUpper())
                                    {
                                        case "FULL TIME":
                                            oTransaction.Transaction_Details.HCFA_IsFullTimeStudent = true;
                                            break;
                                        case "PART TIME":
                                            oTransaction.Transaction_Details.HCFA_IsPartTimeStudent = true;
                                            break;
                                    }
                                }
                                break;
                        }

                        //Patient.sEmployerName,Patient.sOccupation,Patient.sPlaceofEmployment,
                        //Patient.sWorkAddressLine1,Patient.sWorkAddressLine2,
                        //Patient.sWorkCity,Patient.sWorkState,Patient.sWorkState,
                        //Patient.sWorkZIP,Patient.sWorkPhone,Patient.sWorkFAX,Patient.sWorkCounty,Patient.sWorkMobile,
                        //Patient.sWorkEmail,Patient.dtRetirementDate,

                        oTransaction.Transaction_Details.HCFA_PatientEmployer_School_Name = Convert.ToString(dtTrans.Rows[0]["sEmployerName"]);
                        oTransaction.Transaction_Details.HCFA_PriorAuthorizationNo = Convert.ToString(dtTrans.Rows[0]["PriorAuthorizationNo"]);

                        oTransaction.Transaction_Details.HCFA_FacilityCode = Convert.ToString(dtTrans.Rows[0]["FacilityCode"]);
                        oTransaction.Transaction_Details.HCFA_FacilityName = Convert.ToString(dtTrans.Rows[0]["FacilityDescription"]);
                        oTransaction.Transaction_Details.HCFA_FacilityNPI = Convert.ToString(dtTrans.Rows[0]["FacilityNPI"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAddress1 = Convert.ToString(dtTrans.Rows[0]["FacilityAddr1"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAddress2 = Convert.ToString(dtTrans.Rows[0]["FacilityAddr2"]);
                        oTransaction.Transaction_Details.HCFA_FacilityZip = Convert.ToString(dtTrans.Rows[0]["FacilityZip"]);
                        oTransaction.Transaction_Details.HCFA_FacilityCity = Convert.ToString(dtTrans.Rows[0]["FacilityCity"]);
                        oTransaction.Transaction_Details.HCFA_FacilityState = Convert.ToString(dtTrans.Rows[0]["FacilityState"]);
                        oTransaction.Transaction_Details.HCFA_FacilityID = Convert.ToString(dtTrans.Rows[0]["FacilityID"]);
                        oTransaction.Transaction_Details.HCFA_FacilityPhone = Convert.ToString(dtTrans.Rows[0]["FacilityPhone"]);

                        oTransaction.Transaction_Details.HCFA_ProviderFName = Convert.ToString(dtTrans.Rows[0]["ProviderFName"]);
                        oTransaction.Transaction_Details.HCFA_ProviderMName = Convert.ToString(dtTrans.Rows[0]["ProviderMName"]);
                        oTransaction.Transaction_Details.HCFA_ProviderLName = Convert.ToString(dtTrans.Rows[0]["ProviderLName"]);

                        oTransaction.Transaction_Details.HCFA_ProviderAddress1 = Convert.ToString(dtTrans.Rows[0]["ProviderBusAddr1"]);
                        oTransaction.Transaction_Details.HCFA_ProviderAddress2 = Convert.ToString(dtTrans.Rows[0]["ProviderBusAddr2"]);
                        oTransaction.Transaction_Details.HCFA_ProviderCity = Convert.ToString(dtTrans.Rows[0]["ProviderBusCity"]);
                        oTransaction.Transaction_Details.HCFA_ProviderState = Convert.ToString(dtTrans.Rows[0]["ProviderBusState"]);
                        oTransaction.Transaction_Details.HCFA_ProviderZip = Convert.ToString(dtTrans.Rows[0]["ProviderBusZip"]);
                        oTransaction.Transaction_Details.HCFA_ProviderPhone = Convert.ToString(dtTrans.Rows[0]["ProviderBusPhone"]);
                        oTransaction.Transaction_Details.HCFA_ProviderNPI = Convert.ToString(dtTrans.Rows[0]["ProviderNPI"]);
                        oTransaction.Transaction_Details.HCFA_ProviderUPIN = Convert.ToString(dtTrans.Rows[0]["ProviderUPIN"]);
                        oTransaction.Transaction_Details.HCFA_ProviderStateMedicalNo = Convert.ToString(dtTrans.Rows[0]["ProviderStateMedicalNo"]);

                        oTransaction.Transaction_Details.HCFA_ProviderTaxonomy = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyCode"]);
                        //oTransaction.Transaction_Details.HCFA_Provider = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyDesc"]);
                        oTransaction.Transaction_Details.HCFA_ProviderSSN = Convert.ToString(dtTrans.Rows[0]["ProviderSSN"]);
                        oTransaction.Transaction_Details.HCFA_ProviderEIN = Convert.ToString(dtTrans.Rows[0]["ProviderEmployerID"]);

                        oTransaction.PatientID = Convert.ToInt64(dtTrans.Rows[0]["nPatientID"]);
                        oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                        oTransaction.ProviderName = GetProvider(Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]));
                        oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                        oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                        oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                        oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                        oTransaction.ClinicID = ClinicID;
                        oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);

                        oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(dtTrans.Rows[0]["nTransactionStatusID"]);
                        oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                        oTransaction.HospitalizationDateFrom = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateFrom"]);
                        oTransaction.HospitalizationDateTo = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateTo"]);
                        oTransaction.OutSideLab = Convert.ToBoolean(dtTrans.Rows[0]["bOutSideLab"]);
                        oTransaction.OutSideLabCharges = Convert.ToDecimal(dtTrans.Rows[0]["dOutSideLabCharges"]);

                        oTransaction.WorkersComp = Convert.ToBoolean(dtTrans.Rows[0]["bWorkersComp"]);
                        oTransaction.WorkersCompNo = Convert.ToString(dtTrans.Rows[0]["sWorkersCompNo"]);
                        oTransaction.WorkersCompPrintonCMS1500 = Convert.ToBoolean(dtTrans.Rows[0]["bIsWorkersCompOnCMS1500"]);

                        oTransaction.AutoClaim = Convert.ToBoolean(dtTrans.Rows[0]["bAutoClaim"]);
                        oTransaction.AccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nAccidentDate"]);

                    }
                    dtTrans.Dispose();
                    dtTrans = null;
                }
                

                //BL_Transaction_MST_Ins
                DataTable dtInsurance = new DataTable();
                oTransaction.Insurances = new TransactionInsurances();

                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_MST_Ins", oDBParameters, out dtInsurance);


                //BL_Transaction_Lines
                DataTable dtLines = new DataTable(); 
                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
             
                oDB.Retrive("BL_SELECT_HCFA_TransactionLine", oDBParameters, out dtLines);

                if (dtLines != null)
                {
                    if (dtLines.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLines.Rows.Count; i++)
                        {
                            //nTransactionID
                            //nTransactionLineNo nFromDate nToDate sPOSCode sPOSDescription sTOSCode sTOSDescription sCPTCode sCPTDescription sDx1Code 
                            //sDx1Description sDx2Code sDx2Description sDx3Code sDx3Description sDx4Code sDx4Description sDx5Code sDx5Description sDx6Code 
                            //sDx6Description sDx7Code sDx7Description sDx8Code sDx8Description nDx1Pointer nDx2Pointer nDx3Pointer nDx4Pointer 
                            //nDx5Pointer nDx6Pointer nDx7Pointer nDx8Pointer sMod1Code sMod1Description sMod2Code sMod2Description sMod3Code
                            //sMod3Description sMod4Code sMod4Description dCharges dUnit dTotal dAllowed nProvider nClinicID

                            oLine = new TransactionLine();
                            oLine.TransactionId = TransactionID;
                            oLine.TransactionLineId = Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]);
                            oLine.TransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]);
                            oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                            oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nToDate"]));
                            oLine.POSCode = dtLines.Rows[i]["sPOSCode"].ToString();
                            oLine.POSDescription = dtLines.Rows[i]["sPOSDescription"].ToString();
                            oLine.TOSCode = dtLines.Rows[i]["sTOSCode"].ToString();
                            oLine.TOSDescription = dtLines.Rows[i]["sTOSDescription"].ToString();
                            oLine.CPTCode = dtLines.Rows[i]["sCPTCode"].ToString();
                            oLine.CPTDescription = dtLines.Rows[i]["sCPTDescription"].ToString();
                            oLine.Dx1Code = dtLines.Rows[i]["sDx1Code"].ToString();
                            oLine.Dx1Description = dtLines.Rows[i]["sDx1Description"].ToString();
                            oLine.Dx2Code = dtLines.Rows[i]["sDx2Code"].ToString();
                            oLine.Dx2Description = dtLines.Rows[i]["sDx2Description"].ToString();
                            oLine.Dx3Code = dtLines.Rows[i]["sDx3Code"].ToString();
                            oLine.Dx3Description = dtLines.Rows[i]["sDx3Description"].ToString();
                            oLine.Dx4Code = dtLines.Rows[i]["sDx4Code"].ToString();
                            oLine.Dx4Description = dtLines.Rows[i]["sDx4Description"].ToString();
                            oLine.Dx5Code = dtLines.Rows[i]["sDx5Code"].ToString();
                            oLine.Dx5Description = dtLines.Rows[i]["sDx5Description"].ToString();
                            oLine.Dx6Code = dtLines.Rows[i]["sDx6Code"].ToString();
                            oLine.Dx6Description = dtLines.Rows[i]["sDx6Description"].ToString();
                            oLine.Dx7Code = dtLines.Rows[i]["sDx7Code"].ToString();
                            oLine.Dx7Description = dtLines.Rows[i]["sDx7Description"].ToString();
                            oLine.Dx8Code = dtLines.Rows[i]["sDx8Code"].ToString();
                            oLine.Dx8Description = dtLines.Rows[i]["sDx8Description"].ToString();
                            oLine.Dx1Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx1Pointer"]);
                            oLine.Dx2Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx2Pointer"]);
                            oLine.Dx3Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx3Pointer"]);
                            oLine.Dx4Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx4Pointer"]);
                            oLine.Dx5Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx5Pointer"]);
                            oLine.Dx6Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx6Pointer"]);
                            oLine.Dx7Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx7Pointer"]);
                            oLine.Dx8Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx8Pointer"]);
                            oLine.Mod1Code = dtLines.Rows[i]["sMod1Code"].ToString();
                            oLine.Mod1Description = dtLines.Rows[i]["sMod1Description"].ToString();
                            oLine.Mod2Code = dtLines.Rows[i]["sMod2Code"].ToString();
                            oLine.Mod2Description = dtLines.Rows[i]["sMod2Description"].ToString();
                            oLine.Mod3Code = dtLines.Rows[i]["sMod3Code"].ToString();
                            oLine.Mod3Description = dtLines.Rows[i]["sMod3Description"].ToString();
                            oLine.Mod4Code = dtLines.Rows[i]["sMod4Code"].ToString();
                            oLine.Mod4Description = dtLines.Rows[i]["sMod4Description"].ToString();
                            oLine.Charges = Convert.ToDecimal(dtLines.Rows[i]["dCharges"]);
                            oLine.Unit = Convert.ToInt16(dtLines.Rows[i]["dUnit"]);
                            oLine.Total = Convert.ToDecimal(dtLines.Rows[i]["dTotal"]);
                            oLine.AllowedCharges = Convert.ToDecimal(dtLines.Rows[i]["dAllowed"]);
                            oLine.RefferingProviderId = Convert.ToInt64(dtLines.Rows[i]["nProvider"]);
                            oLine.ClinicID = ClinicID;
                            oLine.ClaimNumber = Convert.ToInt64(dtLines.Rows[i]["nClaimNumber"]);
                            oLine.LineStatus = (TransactionStatus)Convert.ToInt32(dtLines.Rows[i]["nTransactionLineStatus"]);

                            //.. Code added on 20090511 by - Sagar Ghodke

                            oLine.IsLabCPT = Convert.ToBoolean(dtLines.Rows[i]["bIsLabCPT"]);
                            oLine.AuthorizationNo = Convert.ToString(dtLines.Rows[i]["sAuthorizationNo"]);
                            oLine.SendToClaim = Convert.ToBoolean(dtLines.Rows[i]["bSentToClaim"]);

                            //..End Code add 20090511,Sagar Ghodke

                            oLine.LinePrimaryDxCode = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxCode"]);
                            oLine.LinePrimaryDxDesc = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxDesc"]);

                            oLine.HCFA_RenderingFName = Convert.ToString(dtLines.Rows[i]["RenderingProviderFName"]);
                            oLine.HCFA_RenderingMName = Convert.ToString(dtLines.Rows[i]["RenderingProviderMName"]);
                            oLine.HCFA_RenderingLName = Convert.ToString(dtLines.Rows[i]["RenderingProviderLName"]);
                            oLine.HCFA_RenderingProviderAddress1 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr1"]);
                            oLine.HCFA_RenderingProviderAddress2 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr2"]);
                            oLine.HCFA_RenderingProviderCity = Convert.ToString(dtLines.Rows[i]["RenderringProviderCity"]);
                            oLine.HCFA_RenderingProviderState = Convert.ToString(dtLines.Rows[i]["RenderringProviderState"]);
                            oLine.HCFA_RenderingProviderZip = Convert.ToString(dtLines.Rows[i]["RenderringProviderZip"]);
                            oLine.HCFA_RenderingProviderPhone = Convert.ToString(dtLines.Rows[i]["RenderringProviderPhone"]);
                            oLine.HCFA_RenderingProviderMedicalLicenceNo = Convert.ToString(dtLines.Rows[i]["RenderringProviderMedicalLicenceNo"]);
                            oLine.HCFA_RenderingProviderUPIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderUPIN"]);
                            oLine.HCFA_RenderingProviderNPI = Convert.ToString(dtLines.Rows[i]["RenderringProviderNPI"]);
                            oLine.HCFA_RenderingProviderTaxonomy = Convert.ToString(dtLines.Rows[i]["RenderringProviderTaxonomy"]);
                            oLine.HCFA_RenderingProviderSSN = Convert.ToString(dtLines.Rows[i]["RenderringProviderSSN"]);
                            oLine.HCFA_RenderingProviderEIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderEmployerID"]);

                            oLine.NDCID = Convert.ToInt64(dtLines.Rows[i]["nNDCID"]);
                            oLine.NDCCodeQualifier = Convert.ToString(dtLines.Rows[i]["sNDCCodeQualifier"]);
                            oLine.NDCCode = Convert.ToString(dtLines.Rows[i]["sNDCCode"]);
                            oLine.NDCDescription = Convert.ToString(dtLines.Rows[i]["sNDCDescription"]);
                            oLine.NDCUnitCode = Convert.ToString(dtLines.Rows[i]["sNDCUnitCode"]);
                            oLine.NDCUnitDescription = Convert.ToString(dtLines.Rows[i]["sNDCUnitDescription"]);
                            oLine.NDCUnit = Convert.ToString(dtLines.Rows[i]["sNDCUnit"]);
                            oLine.NDCUnitPricing = Convert.ToString(dtLines.Rows[i]["sNDCUnitPricing"]);
                            if (dtLines.Rows[i]["HCFA_NDCCode"] != null && dtLines.Rows[i]["HCFA_NDCCode"] != DBNull.Value)
                            { oLine.DisplayNDCCode_HCFA = Convert.ToString(dtLines.Rows[i]["HCFA_NDCCode"]); }

                            //BL_Transaction_Lines_Notes
                            DataTable dtNotes = new DataTable();
                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nLineNo", Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nTransactionDetailID", Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nNoteId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDB.Retrive("BL_SELECT_Transaction_Lines_Notes", oDBParameters, out dtNotes);

                            if (dtNotes != null)
                            {
                                GeneralNote oLineNote = null;
                                for (int j = 0; j < dtNotes.Rows.Count; j++)
                                {
                                    //nTransactionID nLineNo nNoteType nNoteId nNoteDateTime nUserID sNoteDescription nClinicID
                                    //oLine.LineNotes[j].TransactionID = TransactionID;
                                    //oLine.LineNotes[j].TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                    //oLine.LineNotes[j].NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                    //oLine.LineNotes[j].NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                    //oLine.LineNotes[j].UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                    //oLine.LineNotes[j].NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                    //oLine.LineNotes[j].ClinicID = ClinicID;
                                    oLineNote = new GeneralNote();
                                    oLineNote.TransactionID = TransactionID;
                                    oLineNote.TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                    oLineNote.TransactionDetailID = Convert.ToInt64(dtNotes.Rows[j]["nTransactionDetailID"]);
                                    oLineNote.NoteID = Convert.ToInt64(dtNotes.Rows[j]["nNoteId"]);
                                    oLineNote.NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                    oLineNote.NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                    oLineNote.UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                    oLineNote.NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                    oLineNote.ClinicID = ClinicID;
                                    oLine.LineNotes.Add(oLineNote);
                                    if (oLineNote != null)
                                    { oLineNote.Dispose(); }
                                }
                            }

                            if (dtInsurance != null)
                            {
                                if (dtInsurance.Rows.Count > 0)
                                {
                                    //Addded by Anil 20080912 

                                    //nTransactionID,nInsuranceID,nClinicID nTransactionDetailID =1,nTransactionLineNo
                                    for (int j = 0; j < dtInsurance.Rows.Count; j++)
                                    {
                                        if (Convert.ToString(dtInsurance.Rows[j]["nTransactionLineNo"]) != "")
                                        {
                                            if (Convert.ToInt64(dtInsurance.Rows[j]["nTransactionLineNo"]) == oLine.TransactionLineId)
                                            {
                                                oLine.InsuranceID = Convert.ToInt64(dtInsurance.Rows[j]["nInsuranceID"]);
                                                oLine.InsuranceSelfMode = (PayerMode)Convert.ToInt32(dtInsurance.Rows[j]["nPaymentMode"]);

                                                gloInsurance ogloInsurance = new gloInsurance(_databaseconnectionstring);
                                                DataTable dtTempInsurance = new DataTable();
                                                dtTempInsurance = ogloInsurance.GetInsurance(oLine.InsuranceID);
                                                if (dtTempInsurance != null && dtTempInsurance.Rows.Count > 0)
                                                {
                                                    //Contact
                                                    oLine.InsuranceName = Convert.ToString(dtTempInsurance.Rows[0]["Name"]);
                                                    //Vinayak - Is Primary/secondary/tertiary
                                                    if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary.ToString();
                                                    }
                                                    else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary.ToString();
                                                    }
                                                    else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary.ToString();
                                                    }
                                                    else
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = "";
                                                    }

                                                }
                                                if (dtTempInsurance != null) { dtTempInsurance.Dispose(); }
                                                if (ogloInsurance != null) { ogloInsurance.Dispose(); }

                                            }
                                        }
                                    }
                                }
                            }

                            //Transaction line is added in the Transaction
                            oTransaction.Lines.Add(oLine);
                        }
                    }
                    dtLines.Dispose();
                    dtLines = null;
                }

                DataTable dtInsurancePlan = new DataTable();
                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_InsPlan", oDBParameters, out dtInsurancePlan);

                if (dtInsurancePlan != null)
                {
                    for (int i = 0; i < dtInsurancePlan.Rows.Count; i++)
                    {
                        TransactionInsurancePlan _TransactionInsurancePlan = new TransactionInsurancePlan();
                        _TransactionInsurancePlan.TransactionId = Convert.ToInt64(dtInsurancePlan.Rows[i]["nTransactionID"]);
                        _TransactionInsurancePlan.PatientId = Convert.ToInt64(dtInsurancePlan.Rows[i]["nPatientID"]);
                        _TransactionInsurancePlan.ClaimNo = Convert.ToInt64(dtInsurancePlan.Rows[i]["nClaimNo"]);
                        _TransactionInsurancePlan.InsuranceID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nInsuranceID"]);
                        _TransactionInsurancePlan.ContactID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nContactID"]);
                        _TransactionInsurancePlan.ResponsibilityNo = Convert.ToInt64(dtInsurancePlan.Rows[i]["nResponsibilityNo"]);
                        _TransactionInsurancePlan.ResponsibilityType = Convert.ToInt32(dtInsurancePlan.Rows[i]["nResponsibilityType"]);
                        _TransactionInsurancePlan.ClinicID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nClinicId"]);
                        oTransaction.InsurancePlans.Add(_TransactionInsurancePlan);
                    }
                    dtInsurancePlan.Dispose();
                    dtInsurancePlan = null;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtTrans != null)
                {
                    dtTrans.Dispose();
                    dtTrans = null;
                }
                oDBParameters.Dispose();

                oDB.Dispose();

            }
            return oTransaction;
        }

        //02152010 MaheshB
        public Transaction GetHCFATransactionDetails(Int64 TransactionID,Int64 MstTransactionId, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            Int64 nInsurancePlanId = 0;
            DataTable dtTrans = new DataTable();
            Transaction oTransaction = new Transaction();
            TransactionLine oLine = null;
            try
            {
                oDB.Connect(false);
                // For BL_Transaction_MST Table.
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionMasterID", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_HCFA_CLAIMS", oDBParameters, out dtTrans);

                if (dtTrans != null)
                {
                    if (dtTrans.Rows.Count > 0)
                    {
                        //nTransactionID, nMasterAppointmentID, nAppointmentID, nVisitID, nOnsiteDate, nInjuryDate, 
                        //nUnableToWorkFromDate, nUnableToWorkTillDate, nTransactionDate, sCaseNoPrefix, nClaimNo, 
                        //nPatientID, nTransactionProviderID, sMaritalStatus, sFacilityCode, sFacilityDescription, 
                        //nTransactionType, nClinicID, nTransactionStatusID, sState, nHopitalizationDateFrom, nHopitalizationDateTo,
                        //bOutSideLab, dOutSideLabCharges, bAutoClaim, nAccidentDate, bWorkersComp
                        oTransaction.TransactionID = TransactionID;
                        oTransaction.TransactionMasterID = MstTransactionId;
                        oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                        oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                        oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                        oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                        oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                        oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                        oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                        oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                        oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                        oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);
                        oTransaction.SubClaimNo = Convert.ToString(dtTrans.Rows[0]["nSubClaimNo"]);
                        oTransaction.MainClaimNo = Convert.ToString(dtTrans.Rows[0]["sMainClaimNo"]);
                        oTransaction.ReferralProviderID = Convert.ToInt64(dtTrans.Rows[0]["nReferralID"]);
                        oTransaction.SendCounter = Convert.ToInt32(dtTrans.Rows[0]["nSendCounter"]);
                        oTransaction.SendToRejection = Convert.ToInt32(dtTrans.Rows[0]["nSendToRejection"]);
                        oTransaction.LastStatusId = Convert.ToInt64(dtTrans.Rows[0]["nLastStatusId"]);
                        oTransaction.OtherAccident = Convert.ToBoolean(dtTrans.Rows[0]["bOtherAccident"]);
                        oTransaction.OtherAccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nOtherAccidentDate"]);
                        
                        oTransaction.ContactID =Convert.ToInt64(dtTrans.Rows[0]["nContactID"]);
                        //Code added on 20090505 By - Sagar Ghodke
                        oTransaction.TransactionUserID = Convert.ToInt64(dtTrans.Rows[0]["nUserID"]);
                        oTransaction.TransactionUserName = Convert.ToString(dtTrans.Rows[0]["sUserName"]);

                        //MaheshB 20101103
                        oTransaction.ResponsibilityNo = Convert.ToInt32(dtTrans.Rows[0]["nResponsibilityNo"]);

                        //MaheshB
                        oTransaction.IsRebill = Convert.ToBoolean(dtTrans.Rows[0]["bIsRebilled"]);
                        //End code add 20090505,Sagar Ghodke

                        //Added By mukesh on 12 Nov 2010
                        oTransaction.ndtBox15Date = Convert.ToInt64(dtTrans.Rows[0]["ndtBox15Date"]);
                        //**
                        oTransaction.sBox15DateQualifier = Convert.ToString(dtTrans.Rows[0]["sBox15DateQualifier"]);
                        // CMS1500 08/05 Box 19 changes 01032014 Samneer
                        oTransaction.sProviderQualifier = Convert.ToString(dtTrans.Rows[0]["sProviderQualifier"]);

                        oTransaction.Transaction_Details.HCFA_ReferringProviderNPI = Convert.ToString(dtTrans.Rows[0]["RefProviderNPI"]);

                        oTransaction.Transaction_Details.HCFA_LastSeenDate = Convert.ToString(dtTrans.Rows[0]["LastSeenDate"]);
                        oTransaction.Transaction_Details.HCFA_bIsRefProvAsSupervisor = Convert.ToBoolean(dtTrans.Rows[0]["bIsRefProvAsSupervisor"]);


                        //
                        oTransaction.Transaction_Details.HCFA_PatientFName = Convert.ToString(dtTrans.Rows[0]["PatientFName"]);
                        oTransaction.Transaction_Details.HCFA_PatientMName = Convert.ToString(dtTrans.Rows[0]["PatientMName"]);
                        oTransaction.Transaction_Details.HCFA_PatientLName = Convert.ToString(dtTrans.Rows[0]["PatientLName"]);
                        oTransaction.Transaction_Details.HCFA_PatientCode = Convert.ToString(dtTrans.Rows[0]["PatientCode"]);
                        oTransaction.Transaction_Details.HCFA_PatientSSN = Convert.ToString(dtTrans.Rows[0]["PatientSSN"]);
                        if (dtTrans.Rows[0]["PatientDOB"] != DBNull.Value)
                        {
                            oTransaction.Transaction_Details.HCFA_PatientDOB = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(dtTrans.Rows[0]["PatientDOB"]));
                        }
                        oTransaction.Transaction_Details.HCFA_PatientGender = Convert.ToString(dtTrans.Rows[0]["PatientGender"]);

                        oTransaction.Transaction_Details.HCFA_PatientAddress1 = Convert.ToString(dtTrans.Rows[0]["PatientAddr1"]);
                        oTransaction.Transaction_Details.HCFA_PatientAddress2 = Convert.ToString(dtTrans.Rows[0]["PatientAddr2"]);
                        oTransaction.Transaction_Details.HCFA_PatientCity = Convert.ToString(dtTrans.Rows[0]["PatientCity"]);
                        oTransaction.Transaction_Details.HCFA_PatientState = Convert.ToString(dtTrans.Rows[0]["PatientState"]);
                        oTransaction.Transaction_Details.HCFA_PatientZip = Convert.ToString(dtTrans.Rows[0]["PatientZip"]);
                        oTransaction.Transaction_Details.HCFA_PatientPhone = Convert.ToString(dtTrans.Rows[0]["PatientPhone"]);
                        oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus = Convert.ToString(dtTrans.Rows[0]["PatientEmploymentStatus"]);
                        oTransaction.Transaction_Details.HCFA_PatientEmploymentType = Convert.ToString(dtTrans.Rows[0]["sEmploymentType"]);

                        //Employment Status with refrence to occupation control in gloPatient
                        //1.Employed,//2.UnEmployed,//3.Retired,//4.Self-Employed,//5.Student

                        //Employement type with refrence to occupation control in gloPatient
                        //1.Full Time//2.Part Time

                        switch (oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus.Trim().ToUpper())
                        {
                            case "EMPLOYED":
                                { oTransaction.Transaction_Details.HCFA_IsEmployed = true; }
                                break;
                            case "UNEMPLOYED":
                                break;
                            case "SELF-EMPLOYED":
                                break;
                            case "STUDENT":
                                {
                                    switch (oTransaction.Transaction_Details.HCFA_PatientEmploymentType.Trim().ToUpper())
                                    {
                                        case "FULL TIME":
                                            oTransaction.Transaction_Details.HCFA_IsFullTimeStudent = true;
                                            break;
                                        case "PART TIME":
                                            oTransaction.Transaction_Details.HCFA_IsPartTimeStudent = true;
                                            break;
                                    }
                                }
                                break;
                        }

                        //Patient.sEmployerName,Patient.sOccupation,Patient.sPlaceofEmployment,
                        //Patient.sWorkAddressLine1,Patient.sWorkAddressLine2,
                        //Patient.sWorkCity,Patient.sWorkState,Patient.sWorkState,
                        //Patient.sWorkZIP,Patient.sWorkPhone,Patient.sWorkFAX,Patient.sWorkCounty,Patient.sWorkMobile,
                        //Patient.sWorkEmail,Patient.dtRetirementDate,

                        oTransaction.Transaction_Details.HCFA_PatientEmployer_School_Name = Convert.ToString(dtTrans.Rows[0]["sEmployerName"]);
                        oTransaction.Transaction_Details.HCFA_PriorAuthorizationNo = Convert.ToString(dtTrans.Rows[0]["PriorAuthorizationNo"]);

                        oTransaction.Transaction_Details.HCFA_FacilityCode = Convert.ToString(dtTrans.Rows[0]["FacilityCode"]);
                        oTransaction.Transaction_Details.HCFA_FacilityName = Convert.ToString(dtTrans.Rows[0]["FacilityDescription"]);
                        oTransaction.Transaction_Details.HCFA_FacilityNPI = Convert.ToString(dtTrans.Rows[0]["FacilityNPI"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAddress1 = Convert.ToString(dtTrans.Rows[0]["FacilityAddr1"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAddress2 = Convert.ToString(dtTrans.Rows[0]["FacilityAddr2"]);
                        oTransaction.Transaction_Details.HCFA_FacilityZip = Convert.ToString(dtTrans.Rows[0]["FacilityZip"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAreaCode = Convert.ToString(dtTrans.Rows[0]["FacilityAreaCode"]);
                        oTransaction.Transaction_Details.HCFA_FacilityCity = Convert.ToString(dtTrans.Rows[0]["FacilityCity"]);
                        oTransaction.Transaction_Details.HCFA_FacilityState = Convert.ToString(dtTrans.Rows[0]["FacilityState"]);
                        oTransaction.Transaction_Details.HCFA_FacilityID = Convert.ToString(dtTrans.Rows[0]["FacilityID"]);
                        oTransaction.Transaction_Details.HCFA_FacilityPhone = Convert.ToString(dtTrans.Rows[0]["FacilityPhone"]);

                        oTransaction.Transaction_Details.HCFA_ProviderFName = Convert.ToString(dtTrans.Rows[0]["ProviderFName"]);
                        oTransaction.Transaction_Details.HCFA_ProviderMName = Convert.ToString(dtTrans.Rows[0]["ProviderMName"]);
                        oTransaction.Transaction_Details.HCFA_ProviderLName = Convert.ToString(dtTrans.Rows[0]["ProviderLName"]);

                        oTransaction.Transaction_Details.HCFA_ProviderAddress1 = Convert.ToString(dtTrans.Rows[0]["ProviderBusAddr1"]);
                        oTransaction.Transaction_Details.HCFA_ProviderAddress2 = Convert.ToString(dtTrans.Rows[0]["ProviderBusAddr2"]);
                        oTransaction.Transaction_Details.HCFA_ProviderCity = Convert.ToString(dtTrans.Rows[0]["ProviderBusCity"]);
                        oTransaction.Transaction_Details.HCFA_ProviderState = Convert.ToString(dtTrans.Rows[0]["ProviderBusState"]);
                        oTransaction.Transaction_Details.HCFA_ProviderZip = Convert.ToString(dtTrans.Rows[0]["ProviderBusZip"]);
                        oTransaction.Transaction_Details.HCFA_ProviderAreaCode = Convert.ToString(dtTrans.Rows[0]["ProviderBusAreaCode"]);
                        oTransaction.Transaction_Details.HCFA_ProviderPhone = Convert.ToString(dtTrans.Rows[0]["ProviderBusPhone"]);
                        oTransaction.Transaction_Details.HCFA_ProviderNPI = Convert.ToString(dtTrans.Rows[0]["ProviderNPI"]);
                        oTransaction.Transaction_Details.HCFA_ProviderUPIN = Convert.ToString(dtTrans.Rows[0]["ProviderUPIN"]);
                        oTransaction.Transaction_Details.HCFA_ProviderStateMedicalNo = Convert.ToString(dtTrans.Rows[0]["ProviderStateMedicalNo"]);

                        oTransaction.Transaction_Details.HCFA_ProviderTaxonomy = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyCode"]);
                        //oTransaction.Transaction_Details.HCFA_Provider = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyDesc"]);
                        oTransaction.Transaction_Details.HCFA_ProviderSSN = Convert.ToString(dtTrans.Rows[0]["ProviderSSN"]);
                        oTransaction.Transaction_Details.HCFA_ProviderEIN = Convert.ToString(dtTrans.Rows[0]["ProviderEmployerID"]);
                        oTransaction.Transaction_Details.HCFA_ProviderSuffix = Convert.ToString(dtTrans.Rows[0]["sSuffix"]);

                        oTransaction.PatientID = Convert.ToInt64(dtTrans.Rows[0]["nPatientID"]);
                        oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                        oTransaction.ProviderName = GetProvider(Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]));
                        oTransaction.ProviderSuffix = GetProviderSuffix(Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]));
                        oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                        oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                        oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                        oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                        oTransaction.ClinicID = ClinicID;
                        oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);

                        oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(dtTrans.Rows[0]["nTransactionStatusID"]);
                        oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                        oTransaction.HospitalizationDateFrom = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateFrom"]);
                        oTransaction.HospitalizationDateTo = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateTo"]);
                        oTransaction.OutSideLab = Convert.ToBoolean(dtTrans.Rows[0]["bOutSideLab"]);
                        oTransaction.OutSideLabCharges = Convert.ToDecimal(dtTrans.Rows[0]["dOutSideLabCharges"]);

                        oTransaction.WorkersComp = Convert.ToBoolean(dtTrans.Rows[0]["bWorkersComp"]);
                        oTransaction.WorkersCompNo = Convert.ToString(dtTrans.Rows[0]["sWorkersCompNo"]);
                        oTransaction.WorkersCompPrintonCMS1500 = Convert.ToBoolean(dtTrans.Rows[0]["bIsWorkersCompOnCMS1500"]);

                        oTransaction.AutoClaim = Convert.ToBoolean(dtTrans.Rows[0]["bAutoClaim"]);
                        oTransaction.AccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nAccidentDate"]);

                        oTransaction.ReferalProviderID_New = Convert.ToInt64(dtTrans.Rows[0]["nReferralProviderID"]);
                        oTransaction.IsSameAsBillingProvider = Convert.ToBoolean(dtTrans.Rows[0]["bSameAsBillingProvider"]);
                        oTransaction.IsReplacementClaim = Convert.ToBoolean(dtTrans.Rows[0]["bIsReplacementClaim"]);
                        oTransaction.MedicaidResubmissionCode = Convert.ToString(dtTrans.Rows[0]["sMedicaidResubmissionCode"]);
                        oTransaction.bIsEPSDTScreening = Convert.ToBoolean(dtTrans.Rows[0]["bIsEPSDTScreening"]);
                        oTransaction.IsEPSDTReferral = Convert.ToBoolean( dtTrans.Rows[0]["IsEPSDTReferral"]);
                        oTransaction.ReferralCode = Convert.ToString(dtTrans.Rows[0]["ReferralCode"]);
                        oTransaction.ReferralType = Convert.ToString(dtTrans.Rows[0]["ReferralType"]);
                        oTransaction.Box10d = Convert.ToString(dtTrans.Rows[0]["Box10d"]);

                    }
                    dtTrans.Dispose();
                    dtTrans = null;
                }
                
                //**********Added by Abhisekh on 19 Aug 2010
                // Get Corrected Replacement checked or not in Plan setup for current responsible Insurance plan for selected transaction ID.
                #region Get Insurance Plan Current Responsibility.

                    DataTable dtInsCurrResp = new DataTable();
                    string _strCurrRespQuery = "";
                    //DataTable _dt = new DataTable();
                    _strCurrRespQuery = "Select nNextActionContactID" +
                              " FROM bl_eob_nextAction with(nolock) " +
                              " WHERE   ntrackMstTrnID = " + TransactionID + " AND " +
                              " nNextPartyType=2 and nClinicID = " + ClinicID; //17 Charge Billing Note.
                    oDB.Connect(false);
                    oDB.Retrive_Query(_strCurrRespQuery, out dtInsCurrResp);
                    if (dtInsCurrResp != null)
                    {
                        if (dtInsCurrResp.Rows.Count > 0)
                        {
                            nInsurancePlanId = Convert.ToInt64(dtInsCurrResp.Rows[0]["nNextActionContactID"]);
                            DataTable dtCorrRep = new DataTable();
                            _strCurrRespQuery = "Select isnull(bIsCorrectRplmnt,0) as bIsCorrectRplmnt" +
                                " FROM bl_CorrectedReplacement_Plan with(nolock) " +
                                " WHERE   nContactID = " + dtInsCurrResp.Rows[0]["nNextActionContactID"];

                            oDB.Retrive_Query(_strCurrRespQuery, out dtCorrRep);
                            if (dtCorrRep != null)
                            {
                                if (dtCorrRep.Rows.Count > 0)
                                {
                                    oTransaction.IsBox19CorrectRplmnt = Convert.ToBoolean(dtCorrRep.Rows[0]["bIsCorrectRplmnt"]);
                                }
                                else
                                {
                                    oTransaction.IsBox19CorrectRplmnt = false;
                                }
                                dtCorrRep.Dispose();
                                dtCorrRep = null;
                            }
                            else
                            {
                                oTransaction.IsBox19CorrectRplmnt = false;
                            }
                        }
                        else
                        {
                            oTransaction.IsBox19CorrectRplmnt = false;
                        }
                        dtInsCurrResp.Dispose();
                        dtInsCurrResp = null;
                    }
                    else
                    {
                        oTransaction.IsBox19CorrectRplmnt = false;
                    }
                    oDB.Disconnect();
                #endregion


                #region Get Box19 Notes
                    //if (oTransaction.IsReplacementClaim)
                    //{

                        DataTable dtBox19Notes = new DataTable();
                        string _strNotequery = "";
                        //DataTable _dt = new DataTable();
                        _strNotequery = "SELECT ISNULL(sNoteDescription,'') AS  sNoteDescription " +
                                  " FROM BL_Transaction_Lines_Notes with(nolock) " +
                                  //" WHERE   nTransactionID = " + TransactionID + " AND nTransactionDetailID = 0 AND " +
                                  " WHERE   nTransactionID = " + MstTransactionId + " AND nTransactionDetailID = 0 AND " +
                                  " nBillingNoteType=20 AND nNotetype=11 and nClinicID = " + ClinicID + " Order by nNoteId desc";//17 Charge Billing Note.
                        oDB.Connect(false);
                        oDB.Retrive_Query(_strNotequery, out dtBox19Notes);
                        oDB.Disconnect();
                        if (dtBox19Notes != null)
                        {
                            if (dtBox19Notes.Rows.Count > 0)
                            {
                                oTransaction.sBox19Notes = dtBox19Notes.Rows[0]["sNoteDescription"].ToString();
                            }
                            else
                            {
                                oTransaction.sBox19Notes = "";
                            }
                            dtBox19Notes.Dispose();
                            dtBox19Notes = null;
                        }
                        else
                        {
                            oTransaction.sBox19Notes = "";
                        }
                    //}
                    //else
                    //{
                    //    oTransaction.sBox19Notes = "";
                    //}
                #endregion
                //BL_Transaction_MST_Ins
                DataTable dtInsurance = new DataTable();
                oTransaction.Insurances = new TransactionInsurances();

                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_MST_Ins", oDBParameters, out dtInsurance);
                

                //BL_Transaction_Lines
                DataTable dtLines = new DataTable();
                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", oTransaction.ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDB.Retrive("BL_SELECT_HCFA_TransactionLine", oDBParameters, out dtLines);
                oDB.Retrive("BL_SELECT_HCFA_ClaimLine", oDBParameters, out dtLines);
                

                if (dtLines != null)
                {
                    if (dtLines.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLines.Rows.Count; i++)
                        {
                            //nTransactionID
                            //nTransactionLineNo nFromDate nToDate sPOSCode sPOSDescription sTOSCode sTOSDescription sCPTCode sCPTDescription sDx1Code 
                            //sDx1Description sDx2Code sDx2Description sDx3Code sDx3Description sDx4Code sDx4Description sDx5Code sDx5Description sDx6Code 
                            //sDx6Description sDx7Code sDx7Description sDx8Code sDx8Description nDx1Pointer nDx2Pointer nDx3Pointer nDx4Pointer 
                            //nDx5Pointer nDx6Pointer nDx7Pointer nDx8Pointer sMod1Code sMod1Description sMod2Code sMod2Description sMod3Code
                            //sMod3Description sMod4Code sMod4Description dCharges dUnit dTotal dAllowed nProvider nClinicID

                            oLine = new TransactionLine();
                            oLine.TransactionId = TransactionID;
                            oLine.TransactionLineId = Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]);
                            oLine.TransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]);
                            oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                            if (dtLines.Rows[i]["nToDate"] != null && Convert.ToInt64(dtLines.Rows[i]["nToDate"]) > 0)
                            {
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nToDate"]));
                            }
                            else
                            {
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                            }
                            oLine.POSCode = dtLines.Rows[i]["sPOSCode"].ToString();
                            oLine.POSDescription = dtLines.Rows[i]["sPOSDescription"].ToString();
                            oLine.TOSCode = dtLines.Rows[i]["sTOSCode"].ToString();
                            oLine.TOSDescription = dtLines.Rows[i]["sTOSDescription"].ToString();
                            oLine.CPTCode = dtLines.Rows[i]["sCPTCode"].ToString();
                            oLine.CPTDescription = dtLines.Rows[i]["sCPTDescription"].ToString();
                            oLine.CrosswalkCPTCode = dtLines.Rows[i]["sCrossWalkCPTCode"].ToString();
                            oLine.Dx1Code = dtLines.Rows[i]["sDx1Code"].ToString();
                            oLine.Dx1Description = dtLines.Rows[i]["sDx1Description"].ToString();
                            oLine.Dx2Code = dtLines.Rows[i]["sDx2Code"].ToString();
                            oLine.Dx2Description = dtLines.Rows[i]["sDx2Description"].ToString();
                            oLine.Dx3Code = dtLines.Rows[i]["sDx3Code"].ToString();
                            oLine.Dx3Description = dtLines.Rows[i]["sDx3Description"].ToString();
                            oLine.Dx4Code = dtLines.Rows[i]["sDx4Code"].ToString();
                            oLine.Dx4Description = dtLines.Rows[i]["sDx4Description"].ToString();
                            oLine.Dx5Code = dtLines.Rows[i]["sDx5Code"].ToString();
                            oLine.Dx5Description = dtLines.Rows[i]["sDx5Description"].ToString();
                            oLine.Dx6Code = dtLines.Rows[i]["sDx6Code"].ToString();
                            oLine.Dx6Description = dtLines.Rows[i]["sDx6Description"].ToString();
                            oLine.Dx7Code = dtLines.Rows[i]["sDx7Code"].ToString();
                            oLine.Dx7Description = dtLines.Rows[i]["sDx7Description"].ToString();
                            oLine.Dx8Code = dtLines.Rows[i]["sDx8Code"].ToString();
                            oLine.Dx8Description = dtLines.Rows[i]["sDx8Description"].ToString();
                            oLine.Dx1Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx1Pointer"]);
                            oLine.Dx2Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx2Pointer"]);
                            oLine.Dx3Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx3Pointer"]);
                            oLine.Dx4Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx4Pointer"]);
                            oLine.Dx5Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx5Pointer"]);
                            oLine.Dx6Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx6Pointer"]);
                            oLine.Dx7Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx7Pointer"]);
                            oLine.Dx8Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx8Pointer"]);
                            oLine.Mod1Code = dtLines.Rows[i]["sMod1Code"].ToString();
                            oLine.Mod1Description = dtLines.Rows[i]["sMod1Description"].ToString();
                            oLine.Mod2Code = dtLines.Rows[i]["sMod2Code"].ToString();
                            oLine.Mod2Description = dtLines.Rows[i]["sMod2Description"].ToString();
                            oLine.Mod3Code = dtLines.Rows[i]["sMod3Code"].ToString();
                            oLine.Mod3Description = dtLines.Rows[i]["sMod3Description"].ToString();
                            oLine.Mod4Code = dtLines.Rows[i]["sMod4Code"].ToString();
                            oLine.Mod4Description = dtLines.Rows[i]["sMod4Description"].ToString();
                            oLine.Charges = Convert.ToDecimal(dtLines.Rows[i]["dCharges"]);
                            oLine.Unit = Convert.ToDecimal(dtLines.Rows[i]["dUnit"]);
                            oLine.Total = Convert.ToDecimal(dtLines.Rows[i]["dTotal"]);
                            oLine.AllowedCharges = Convert.ToDecimal(dtLines.Rows[i]["dAllowed"]);
                            oLine.RefferingProviderId = Convert.ToInt64(dtLines.Rows[i]["nProvider"]);
                            oLine.ClinicID = ClinicID;
                            oLine.ClaimNumber = Convert.ToInt64(dtLines.Rows[i]["nClaimNumber"]);
                            oLine.LineStatus = (TransactionStatus)Convert.ToInt32(dtLines.Rows[i]["nTransactionLineStatus"]);
                            oLine.NDCPrescriptionDesc = Convert.ToString(dtLines.Rows[i]["PrescriptionDesc"]);
                            //.. Code added on 20090511 by - Sagar Ghodke

                            oLine.IsLabCPT = Convert.ToBoolean(dtLines.Rows[i]["bIsLabCPT"]);
                            oLine.AuthorizationNo = Convert.ToString(dtLines.Rows[i]["sAuthorizationNo"]);
                            oLine.SendToClaim = Convert.ToBoolean(dtLines.Rows[i]["bSentToClaim"]);

                            //..End Code add 20090511,Sagar Ghodke

                            oLine.LinePrimaryDxCode = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxCode"]);
                            oLine.LinePrimaryDxDesc = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxDesc"]);

                            oLine.HCFA_RenderingFName = Convert.ToString(dtLines.Rows[i]["RenderingProviderFName"]);
                            oLine.HCFA_RenderingMName = Convert.ToString(dtLines.Rows[i]["RenderingProviderMName"]);
                            oLine.HCFA_RenderingLName = Convert.ToString(dtLines.Rows[i]["RenderingProviderLName"]);
                            oLine.HCFA_RenderingProviderAddress1 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr1"]);
                            oLine.HCFA_RenderingProviderAddress2 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr2"]);
                            oLine.HCFA_RenderingProviderCity = Convert.ToString(dtLines.Rows[i]["RenderringProviderCity"]);
                            oLine.HCFA_RenderingProviderState = Convert.ToString(dtLines.Rows[i]["RenderringProviderState"]);
                            oLine.HCFA_RenderingProviderZip = Convert.ToString(dtLines.Rows[i]["RenderringProviderZip"]);
                            oLine.HCFA_RenderingProviderAreaCode = Convert.ToString(dtLines.Rows[i]["RenderringProviderAreaCode"]);
                            oLine.HCFA_RenderingProviderPhone = Convert.ToString(dtLines.Rows[i]["RenderringProviderPhone"]);
                            oLine.HCFA_RenderingProviderMedicalLicenceNo = Convert.ToString(dtLines.Rows[i]["RenderringProviderMedicalLicenceNo"]);
                            oLine.HCFA_RenderingProviderUPIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderUPIN"]);
                            oLine.HCFA_RenderingProviderNPI = Convert.ToString(dtLines.Rows[i]["RenderringProviderNPI"]);
                            oLine.HCFA_RenderingProviderTaxonomy = Convert.ToString(dtLines.Rows[i]["RenderringProviderTaxonomy"]);
                            oLine.HCFA_RenderingProviderSSN = Convert.ToString(dtLines.Rows[i]["RenderringProviderSSN"]);
                            oLine.HCFA_RenderingProviderEIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderEmployerID"]);

                            oLine.NDCID = Convert.ToInt64(dtLines.Rows[i]["nNDCID"]);
                            oLine.NDCCodeQualifier = Convert.ToString(dtLines.Rows[i]["sNDCCodeQualifier"]);
                            oLine.NDCCode = Convert.ToString(dtLines.Rows[i]["sNDCCode"]);
                            oLine.NDCDescription = Convert.ToString(dtLines.Rows[i]["sNDCDescription"]);
                            oLine.NDCUnitCode = Convert.ToString(dtLines.Rows[i]["sNDCUnitCode"]);
                            oLine.NDCUnitDescription = Convert.ToString(dtLines.Rows[i]["sNDCUnitDescription"]);
                            oLine.NDCUnit = Convert.ToString(dtLines.Rows[i]["sNDCUnit"]);
                            oLine.NDCUnitPricing = Convert.ToString(dtLines.Rows[i]["sNDCUnitPricing"]);
                            if (dtLines.Rows[i]["HCFA_NDCCode"] != null && dtLines.Rows[i]["HCFA_NDCCode"] != DBNull.Value)
                            { oLine.DisplayNDCCode_HCFA = Convert.ToString(dtLines.Rows[i]["HCFA_NDCCode"]); }

                            //BL_Transaction_Lines_Notes
                            DataTable dtNotes = new DataTable();
                            oDBParameters.Clear();
                            oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nLineNo", Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nTransactionDetailID", Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]), ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nNoteId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);


                            //Added By mukesh on 12 Nov 2010
                            oLine.EMG = Convert.ToBoolean(dtLines.Rows[i]["bEMG"]);
                            //**
                            oLine.IsServiceScreening = Convert.ToBoolean(dtLines.Rows[i]["bIsServiceScreening"]);
                            oLine.IsFamilyPlanningIndicator = Convert.ToBoolean(dtLines.Rows[i]["bIsFamilyPlanningIndicator"]);
                            //oDB.Retrive("BL_SELECT_Transaction_Lines_Notes", oDBParameters, out dtNotes);

                            #region Get Notes

                            string _strquery="";
                            //DataTable _dt = new DataTable();
                            _strquery="SELECT top 1 ISNULL(nNoteType,0) AS  nNoteType,ISNULL(nTransactionID,0) AS  nTransactionID,ISNULL(nTransactionDetailID,0) AS nTransactionDetailID, "+   
                                      " ISNULL(nLineNo,0) AS  nLineNo,ISNULL(nClinicID,0) AS  nClinicID,ISNULL(nNoteId,0) AS  nNoteId, "+
                                      " ISNULL(nNoteDateTime,0) AS  nNoteDateTime,ISNULL(nUserID,0) AS  nUserID, "+
                                      " ISNULL(sNoteDescription,'') AS  sNoteDescription,ISNULL( nBillingNoteType,0) as nBillingNoteType "+
                                      " FROM BL_Transaction_Lines_Notes with(nolock) "+
                                      " WHERE   nTransactionID = " + MstTransactionId + " AND nTransactionDetailID = " + Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterDetailID"]) + " AND " +
                                      " nBillingNoteType=17 AND nClinicID = " + ClinicID + " Order by nNoteId desc";//17 Charge Billing Note.
                            oDB.Connect(false);
                            oDB.Retrive_Query(_strquery, out dtNotes);
                            oDB.Disconnect();
                            #endregion



                            #region Anesthesia
                            DataTable dtAnesthesia = null;
                            _strquery = "";
                            //DataTable _dt = new DataTable();
                            _strquery = "SELECT '7Begin ' + CONVERT(VARCHAR(5),dtstartdatetime,108) + ' End ' + CONVERT(VARCHAR(5),dtenddatetime,108) + " +
                                        " case Temp.sBillUnitsAs when 'Units' then ' Time '+ CONVERT(VARCHAR,nTotalMinutes)+' minutes' else '' end As Ansthesia  , " +
                                        " case Temp.sBillUnitsAs when 'Units' then   dTimeUnits   else nTotalMinutes end as BoxG " +
                                        " FROM BL_Transaction_Anesthesia with(nolock) OUTER APPLY ( SELECT ISNULL(sBillUnitsAs,'') AS sBillUnitsAs  FROM  Contacts_MST left outer  join Contacts_Insurance_DTL  ON  Contacts_MST.nContactID = Contacts_Insurance_DTL.nContactID  WHERE  Contacts_MST.nContactID =" + oTransaction.ContactID.ToString() + " ) As Temp " +
                                        "WHERE nTransactionMasterID = " + MstTransactionId + " AND nTransactionMasterDetailID = " + Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterDetailID"]) + " And ntransactiondetailid=" + dtLines.Rows[i]["nTransactionDetailID"].ToString() +
                                        "and nTotalMinutes > 0 ";
                            oDB.Connect(false);
                            oDB.Retrive_Query(_strquery, out dtAnesthesia);
                            oDB.Disconnect();


                            if (dtAnesthesia != null)
                            {
                                if (dtAnesthesia.Rows.Count > 0)
                                {
                                    oLine.Anesthesia = dtAnesthesia.Rows[0][0].ToString();
                                    oLine.Unit =Convert.ToDecimal(dtAnesthesia.Rows[0][1]);  
                                }
                                dtAnesthesia.Dispose();
                                dtAnesthesia = null;
                            }


                            #endregion





                            if (dtNotes != null)
                            {
                                
                                GeneralNote oLineNote = null;
                                
                                for (int j = 0; j < dtNotes.Rows.Count; j++)
                                {
                                    //nTransactionID nLineNo nNoteType nNoteId nNoteDateTime nUserID sNoteDescription nClinicID
                                    //oLine.LineNotes[j].TransactionID = TransactionID;
                                    //oLine.LineNotes[j].TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                    //oLine.LineNotes[j].NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                    //oLine.LineNotes[j].NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                    //oLine.LineNotes[j].UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                    //oLine.LineNotes[j].NoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                    //oLine.LineNotes[j].ClinicID = ClinicID;
                                    oLineNote = new GeneralNote();
                                    oLineNote.TransactionID = TransactionID;
                                    oLineNote.TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                    oLineNote.TransactionDetailID = Convert.ToInt64(dtNotes.Rows[j]["nTransactionDetailID"]);
                                    oLineNote.NoteID = Convert.ToInt64(dtNotes.Rows[j]["nNoteId"]);
                                    oLineNote.NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                    oLineNote.NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);
                                    oLineNote.UserID = Convert.ToInt64(dtNotes.Rows[j]["nUserID"]);
                                    string _strNoteDescription = _strNoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                    _strNoteDescription = _strNoteDescription.Replace("\r", " ");
                                    _strNoteDescription = _strNoteDescription.Replace("\n", " ");
                                    oLineNote.NoteDescription = _strNoteDescription.Trim();
                                    oLineNote.ClinicID = ClinicID;
                                    oLine.LineNotes.Add(oLineNote);
                                    if (oLineNote != null)
                                    { oLineNote.Dispose(); }
                                }
                                dtNotes.Dispose();
                                dtNotes = null;
                            }

                            if (dtInsurance != null)
                            {
                                if (dtInsurance.Rows.Count > 0)
                                {
                                    //Addded by Anil 20080912 

                                    //nTransactionID,nInsuranceID,nClinicID nTransactionDetailID =1,nTransactionLineNo
                                    for (int j = 0; j < dtInsurance.Rows.Count; j++)
                                    {
                                        if (Convert.ToString(dtInsurance.Rows[j]["nTransactionLineNo"]) != "")
                                        {
                                            if (Convert.ToInt64(dtInsurance.Rows[j]["nTransactionLineNo"]) == oLine.TransactionLineId)
                                            {
                                                oLine.InsuranceID = Convert.ToInt64(dtInsurance.Rows[j]["nInsuranceID"]);
                                                oLine.InsuranceSelfMode = (PayerMode)Convert.ToInt32(dtInsurance.Rows[j]["nPaymentMode"]);

                                                gloInsurance ogloInsurance = new gloInsurance(_databaseconnectionstring);
                                                DataTable dtTempInsurance = new DataTable();
                                                dtTempInsurance = ogloInsurance.GetInsurance(oLine.InsuranceID);
                                                if (dtTempInsurance != null && dtTempInsurance.Rows.Count > 0)
                                                {
                                                    //Contact
                                                    oLine.InsuranceName = Convert.ToString(dtTempInsurance.Rows[0]["Name"]);
                                                    //Vinayak - Is Primary/secondary/tertiary
                                                    if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary.ToString();
                                                    }
                                                    else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary.ToString();
                                                    }
                                                    else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary.ToString();
                                                    }
                                                    else
                                                    {
                                                        oLine.InsurancePrimarySecondaryTertiary = "";
                                                    }

                                                }
                                                if (dtTempInsurance != null) { dtTempInsurance.Dispose(); }
                                                if (ogloInsurance != null) { ogloInsurance.Dispose(); }

                                            }
                                        }
                                    }
                                }
                            }

                            //Transaction line is added in the Transaction
                            oTransaction.Lines.Add(oLine);
                        }
                    }
                    dtLines.Dispose();
                    dtLines = null;
                }
                if (dtInsurance != null)
                {
                    dtInsurance.Dispose();
                    dtInsurance = null;
                }
                DataTable dtInsurancePlan = new DataTable();
                oDBParameters.Clear();
                //MaheshB 02152010
                //oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_InsPlan", oDBParameters, out dtInsurancePlan);
                oDB.Disconnect();
                if (dtInsurancePlan != null)
                {
                    for (int i = 0; i < dtInsurancePlan.Rows.Count; i++)
                    {
                        TransactionInsurancePlan _TransactionInsurancePlan = new TransactionInsurancePlan();
                        _TransactionInsurancePlan.TransactionId = Convert.ToInt64(dtInsurancePlan.Rows[i]["nTransactionID"]);
                        _TransactionInsurancePlan.PatientId = Convert.ToInt64(dtInsurancePlan.Rows[i]["nPatientID"]);
                        _TransactionInsurancePlan.ClaimNo = Convert.ToInt64(dtInsurancePlan.Rows[i]["nClaimNo"]);
                        _TransactionInsurancePlan.InsuranceID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nInsuranceID"]);
                        _TransactionInsurancePlan.ContactID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nContactID"]);
                        _TransactionInsurancePlan.ResponsibilityNo = Convert.ToInt64(dtInsurancePlan.Rows[i]["nResponsibilityNo"]);
                        _TransactionInsurancePlan.ResponsibilityType = Convert.ToInt32(dtInsurancePlan.Rows[i]["nResponsibilityType"]);
                        _TransactionInsurancePlan.ClinicID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nClinicId"]);
                        oTransaction.InsurancePlans.Add(_TransactionInsurancePlan);
                    }
                    dtInsurancePlan.Dispose();
                    dtInsurancePlan = null;
                }

            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtTrans != null)
                {
                    dtTrans.Dispose();
                    dtTrans = null;
                }
                oDBParameters.Dispose();

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return oTransaction;
        }

        public Transaction GetHCFATransactionDetailsNew(Int64 TransactionID, Int64 MstTransactionId, Int64 _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataSet _ds=null;
           // DataTable dtTrans = new DataTable();
            Transaction oTransaction = new Transaction();
            TransactionLine oLine = null;
            try
            {
                oDB.Connect(false);
                // For BL_Transaction_MST Table.
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionMasterID", MstTransactionId, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClaimPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("gsp_GET_Claim_HCFA1500New", oDBParameters, out _ds);

                if (_ds.Tables.Count == 12)
                {
                    _ds.Tables[0].TableName = "ClaimInfo";
                    _ds.Tables[1].TableName = "ClaimPatient";
                    _ds.Tables[2].TableName = "PatientSettings";
                    _ds.Tables[3].TableName = "ClaimInsurance";
                    _ds.Tables[4].TableName = "InsuranceBoxesSettings";
                    _ds.Tables[5].TableName = "InsuranceCompanyName";
                    _ds.Tables[6].TableName = "ClaimNotes";
                    _ds.Tables[7].TableName = "ClaimLines";
                    _ds.Tables[8].TableName = "Box19AndIncludeRefral";
                    _ds.Tables[9].TableName = "Referral/PhysicainInfo";
                    _ds.Tables[10].TableName = "Referral/PhysicainOtherInfo";
                    _ds.Tables[11].TableName = "IsCorrectedAndReplacement";

                }
                

                #region ClaimInsuranceTyeforBox1
             
                if (_ds.Tables["ClaimInsurance"].Rows.Count > 0)
                {
                    oTransaction.ClaimInsurance = _ds.Tables["ClaimInsurance"].Copy();
                }
                
                #endregion

                #region PatientSettings
                if (_ds.Tables["PatientSettings"]!=null && _ds.Tables["PatientSettings"].Rows.Count > 0 )
                {
                    oTransaction.PatientSettings = _ds.Tables["PatientSettings"].Copy();
                }
                #endregion

                #region InsurancesBoxesSettngs
                if (_ds.Tables["InsuranceBoxesSettings"] != null && _ds.Tables["InsuranceBoxesSettings"].Rows.Count > 0)
                {
                    oTransaction.InsurancesBoxSettings = _ds.Tables["InsuranceBoxesSettings"].Copy();
                }
                #endregion

                #region InsuranceCompanyname
                if (_ds.Tables["InsuranceCompanyName"] != null && _ds.Tables["InsuranceCompanyName"].Rows.Count > 0)
                {
                    oTransaction.InsuranceCompanyName= _ds.Tables["InsuranceCompanyName"].Copy();
                }
                #endregion

                #region Box19AndIncludeReferalProviderSettings

                if (_ds.Tables["Box19AndIncludeRefral"] != null && _ds.Tables["Box19AndIncludeRefral"].Rows.Count > 0)
                {
                    oTransaction.Box19AndIncludeReferal = _ds.Tables["Box19AndIncludeRefral"].Copy();
                }

                #endregion

                #region ClaimInformation

                if (_ds.Tables["ClaimInfo"] != null)
                {
                    if (_ds.Tables["ClaimInfo"].Rows.Count > 0)
                    {
                        oTransaction.TransactionID = TransactionID;
                        oTransaction.TransactionMasterID = MstTransactionId;
                        oTransaction.OnsiteDate = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nOnsiteDate"]);
                        oTransaction.InjuryDate = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nInjuryDate"]);
                        oTransaction.UnableToWorkFromDate = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nUnableToWorkFromDate"]);
                        oTransaction.UnableToWorkTillDate = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nUnableToWorkTillDate"]);
                        oTransaction.TransactionDate = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nTransactionDate"]);
                        oTransaction.CaseNoPrefix = _ds.Tables["ClaimInfo"] .Rows[0]["sCaseNoPrefix"].ToString();
                        oTransaction.ClaimNo = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nClaimNo"]);
                        oTransaction.SubClaimNo = Convert.ToString(_ds.Tables["ClaimInfo"] .Rows[0]["nSubClaimNo"]);
                        oTransaction.MainClaimNo = Convert.ToString(_ds.Tables["ClaimInfo"] .Rows[0]["sMainClaimNo"]);
                        oTransaction.ReferralProviderID = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nReferralID"]);
                        oTransaction.OtherAccident = Convert.ToBoolean(_ds.Tables["ClaimInfo"] .Rows[0]["bOtherAccident"]);
                        oTransaction.OtherAccidentDate = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nOtherAccidentDate"]);
                        oTransaction.ContactID = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nContactID"]);
                        oTransaction.TransactionUserID = Convert.ToInt64(_ds.Tables["ClaimInfo"] .Rows[0]["nUserID"]);
                        oTransaction.TransactionUserName = Convert.ToString(_ds.Tables["ClaimInfo"] .Rows[0]["sUserName"]);
                        oTransaction.ResponsibilityNo = Convert.ToInt32(_ds.Tables["ClaimInfo"] .Rows[0]["nResponsibilityNo"]);                                   
                        oTransaction.IsRebill = Convert.ToBoolean(_ds.Tables["ClaimInfo"] .Rows[0]["bIsRebilled"]);
                        oTransaction.ndtBox15Date = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["ndtBox15Date"]);
                        // Line added on 12162013 Sameer
                        oTransaction.sBox15DateQualifier = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sBox15DateQualifier"]);
                        oTransaction.sBox14DateQualifier = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sBox14DateQualifier"]);
                        // Line added on 02072014 for icd 10 changes Sameer
                        oTransaction.nICDRevision = Convert.ToInt16(_ds.Tables["ClaimInfo"].Rows[0]["nICDRevision"]);
                        oTransaction.Transaction_Details.HCFA_LastSeenDate = Convert.ToString(_ds.Tables["ClaimInfo"] .Rows[0]["LastSeenDate"]);
                        oTransaction.Transaction_Details.HCFA_bIsRefProvAsSupervisor = Convert.ToBoolean(_ds.Tables["ClaimInfo"] .Rows[0]["bIsRefProvAsSupervisor"]);
                        oTransaction.Transaction_Details.HCFA_ReferringProviderNPI = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["RefProviderNPI"]);

                        oTransaction.MaritalStatus = _ds.Tables["ClaimInfo"].Rows[0]["sMaritalStatus"].ToString();
                        oTransaction.FacilityCode =_ds.Tables["ClaimInfo"].Rows[0]["sFacilityCode"].ToString();
                        oTransaction.FacilityDescription =_ds.Tables["ClaimInfo"].Rows[0]["sFacilityDescription"].ToString();
                        oTransaction.PrefixID = 0; ////This ID is use to generate a unique TransactionID in Stored Procedure.
                        oTransaction.ClinicID = ClinicID;
                        oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nTransactionType"]);

                        oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(_ds.Tables["ClaimInfo"].Rows[0]["nTransactionStatusID"]);
                        oTransaction.State = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sState"]);
                        oTransaction.HospitalizationDateFrom = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nHopitalizationDateFrom"]);
                        oTransaction.HospitalizationDateTo = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nHopitalizationDateTo"]);
                        oTransaction.OutSideLab = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bOutSideLab"]);
                        oTransaction.OutSideLabCharges = Convert.ToDecimal(_ds.Tables["ClaimInfo"].Rows[0]["dOutSideLabCharges"]);

                        oTransaction.WorkersComp = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bWorkersComp"]);
                        oTransaction.WorkersCompNo = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sWorkersCompNo"]);
                        oTransaction.WorkersCompPrintonCMS1500 = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bIsWorkersCompOnCMS1500"]);

                        oTransaction.AutoClaim = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bAutoClaim"]);
                        oTransaction.AccidentDate = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nAccidentDate"]);

                        oTransaction.ReferalProviderID_New = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nReferralProviderID"]);
                        oTransaction.ProviderIDQualifier = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sProviderQualifier"]);
                        oTransaction.IsSameAsBillingProvider = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bSameAsBillingProvider"]);
                        oTransaction.IsReplacementClaim = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bIsReplacementClaim"]);
                        oTransaction.MedicaidResubmissionCode = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sMedicaidResubmissionCode"]);
                        oTransaction.bIsEPSDTScreening = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["bIsEPSDTScreening"]);
                        oTransaction.IsEPSDTReferral = Convert.ToBoolean(_ds.Tables["ClaimInfo"].Rows[0]["IsEPSDTReferral"]);
                        oTransaction.ReferralCode = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ReferralCode"]);
                        oTransaction.ReferralType = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ReferralType"]);
                         oTransaction.Box10d = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["Box10d"]);
                         
                        #region Provider And Facility
                        oTransaction.Transaction_Details.HCFA_FacilityCode = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityCode"]);
                        oTransaction.Transaction_Details.HCFA_FacilityName = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityDescription"]);
                        oTransaction.Transaction_Details.HCFA_FacilityNPI = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityNPI"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAddress1 = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityAddr1"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAddress2 = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityAddr2"]);
                        oTransaction.Transaction_Details.HCFA_FacilityZip = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityZip"]);
                        oTransaction.Transaction_Details.HCFA_FacilityAreaCode = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityAreaCode"]);
                        oTransaction.Transaction_Details.HCFA_FacilityCity = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityCity"]);
                        oTransaction.Transaction_Details.HCFA_FacilityState = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityState"]);
                        oTransaction.Transaction_Details.HCFA_FacilityID = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityID"]);
                        oTransaction.Transaction_Details.HCFA_FacilityPhone = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["FacilityPhone"]);

                        oTransaction.Transaction_Details.HCFA_ProviderFName = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderFName"]);
                        oTransaction.Transaction_Details.HCFA_ProviderMName = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderMName"]);
                        oTransaction.Transaction_Details.HCFA_ProviderLName = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderLName"]);

                        oTransaction.Transaction_Details.HCFA_ProviderAddress1 = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusAddr1"]);
                        oTransaction.Transaction_Details.HCFA_ProviderAddress2 = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusAddr2"]);
                        oTransaction.Transaction_Details.HCFA_ProviderCity = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusCity"]);
                        oTransaction.Transaction_Details.HCFA_ProviderState = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusState"]);
                        oTransaction.Transaction_Details.HCFA_ProviderZip = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusZip"]);
                        oTransaction.Transaction_Details.HCFA_ProviderAreaCode = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusAreaCode"]);
                        oTransaction.Transaction_Details.HCFA_ProviderPhone = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderBusPhone"]);
                        oTransaction.Transaction_Details.HCFA_ProviderNPI = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderNPI"]);
                        oTransaction.Transaction_Details.HCFA_ProviderUPIN = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderUPIN"]);
                        oTransaction.Transaction_Details.HCFA_ProviderStateMedicalNo = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderStateMedicalNo"]);
                        oTransaction.HCFA_FacilityMammogramCertNumber = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sMammogramCertNumber"]);


                        oTransaction.Transaction_Details.HCFA_ProviderTaxonomy = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderTaxonomyCode"]);
                        //oTransaction.Transaction_Details.HCFA_Provider = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderTaxonomyDesc"]);
                        oTransaction.Transaction_Details.HCFA_ProviderSSN = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderSSN"]);
                        oTransaction.Transaction_Details.HCFA_ProviderEIN = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["ProviderEmployerID"]);
                        oTransaction.Transaction_Details.HCFA_ProviderSuffix = Convert.ToString(_ds.Tables["ClaimInfo"].Rows[0]["sSuffix"]);

                        oTransaction.PatientID = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nPatientID"]);
                        oTransaction.ProviderID = Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nTransactionProviderID"]);
                        oTransaction.ProviderName = GetProvider(Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nTransactionProviderID"]));
                        oTransaction.ProviderSuffix = GetProviderSuffix(Convert.ToInt64(_ds.Tables["ClaimInfo"].Rows[0]["nTransactionProviderID"]));
                        
                        #endregion
                        
                    }
                }
                #endregion

                #region ClaimPatientInformation
                if (_ds.Tables["ClaimPatient"] != null)
                {
                    if (_ds.Tables["ClaimPatient"].Rows.Count > 0)
                    {
                        oTransaction.Transaction_Details.HCFA_PatientFName = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientFName"]);
                        oTransaction.Transaction_Details.HCFA_PatientMName = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientMName"]);
                        oTransaction.Transaction_Details.HCFA_PatientLName = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientLName"]);
                        oTransaction.Transaction_Details.HCFA_PatientSuffix = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientSuffix"]);
                        oTransaction.Transaction_Details.HCFA_PatientCode = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientCode"]);
                        oTransaction.Transaction_Details.HCFA_PatientSSN = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientSSN"]);
                        if (_ds.Tables["ClaimPatient"].Rows[0]["PatientDOB"] != DBNull.Value)
                        {
                            oTransaction.Transaction_Details.HCFA_PatientDOB = gloDateMaster.gloDate.DateAsNumber(Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientDOB"]));
                        }
                        oTransaction.Transaction_Details.HCFA_PatientGender = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientGender"]);
                        oTransaction.Transaction_Details.HCFA_PatientAddress1 = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientAddr1"]);
                        oTransaction.Transaction_Details.HCFA_PatientAddress2 = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientAddr2"]);
                        oTransaction.Transaction_Details.HCFA_PatientCity = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientCity"]);
                        oTransaction.Transaction_Details.HCFA_PatientState = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientState"]);
                        oTransaction.Transaction_Details.HCFA_PatientZip = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientZip"]);
                        oTransaction.Transaction_Details.HCFA_PatientAreaCode = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["AreaCode"]);
                        oTransaction.Transaction_Details.HCFA_PatientPhone = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientPhone"]);
                        oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PatientEmploymentStatus"]);
                        oTransaction.Transaction_Details.HCFA_PatientEmploymentType = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["sEmploymentType"]);
                        oTransaction.Transaction_Details.HCFA_bIsSignatureOnFile = Convert.ToBoolean(_ds.Tables["ClaimPatient"].Rows[0]["bSignatureOnFile"]);

                        switch (oTransaction.Transaction_Details.HCFA_PatientEmploymentStatus.Trim().ToUpper())
                        {
                            case "EMPLOYED":
                                { oTransaction.Transaction_Details.HCFA_IsEmployed = true; }
                                break;
                            case "UNEMPLOYED":
                                break;
                            case "SELF-EMPLOYED":
                                break;
                            case "STUDENT":
                                {
                                    switch (oTransaction.Transaction_Details.HCFA_PatientEmploymentType.Trim().ToUpper())
                                    {
                                        case "FULL TIME":
                                            oTransaction.Transaction_Details.HCFA_IsFullTimeStudent = true;
                                            break;
                                        case "PART TIME":
                                            oTransaction.Transaction_Details.HCFA_IsPartTimeStudent = true;
                                            break;
                                    }
                                }
                                break;
                        }

                        oTransaction.Transaction_Details.HCFA_PatientEmployer_School_Name = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["sEmployerName"]);
                        /// oTransaction.Transaction_Details.HCFA_PriorAuthorizationNo = Convert.ToString(_ds.Tables["ClaimPatient"].Rows[0]["PriorAuthorizationNo"]); Prior Authoriztion ????///
                

                    }
                }
                #endregion

                #region Provider and Facility
                 //if (_ds.Tables["Referral/PhysicainInfo"] != null)
                //{
                //    if (_ds.Tables["Referral/PhysicainInfo"].Rows.Count > 0)
                //    {
                      
                //        oTransaction.Transaction_Details.HCFA_ProviderFName = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sFirstName"]);
                //        oTransaction.Transaction_Details.HCFA_ProviderMName = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sMiddleName"]);
                //        oTransaction.Transaction_Details.HCFA_ProviderLName = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sLastName"]);
                //        if (oTransaction.IsSameAsBillingProvider)
                //        {

                //            oTransaction.Transaction_Details.HCFA_ProviderAddress1 = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["Addressline1"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderAddress2 = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sAddressLine2"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderCity = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sCity"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderState = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sState"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderZip = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sZip"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderAreaCode = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sAreaCode"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderPhone = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sPhone"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderNPI = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sNPI"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderUPIN = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sUPIN"]);
                //            // oTransaction.Transaction_Details.HCFA_ProviderStateMedicalNo = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["ProviderStateMedicalNo"]);

                //            oTransaction.Transaction_Details.HCFA_ProviderTaxonomy = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sTaxonomy"]);
                //            //oTransaction.Transaction_Details.HCFA_Provider = Convert.ToString(dtTrans.Rows[0]["ProviderTaxonomyDesc"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderSSN = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["ProviderSSN"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderEIN = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["ProviderEmployerID"]);
                //            oTransaction.Transaction_Details.HCFA_ProviderSuffix = Convert.ToString(_ds.Tables["Referral/PhysicainInfo"].Rows[0]["sSuffix"]);
                //        }
                       
                //    }
                //}
               
                      
                #endregion


                #region IsCorrectedAndReplacement

                if (_ds.Tables["IsCorrectedAndReplacement"] != null)
                {
                    if (_ds.Tables["IsCorrectedAndReplacement"].Rows.Count > 0)
                    {
                        oTransaction.IsBox19CorrectRplmnt = Convert.ToBoolean(_ds.Tables["IsCorrectedAndReplacement"].Rows[0]["bIsCorrectRplmnt"]);
                    }
                    else
                    {
                        oTransaction.IsBox19CorrectRplmnt = false;
                    }
                }
                else
                {
                    oTransaction.IsBox19CorrectRplmnt = false;
                }
                
               
                #endregion


                #region Get Box19 Notes
               
                if (_ds.Tables["ClaimNotes"]  !=null)
                {
                    if (_ds.Tables["ClaimNotes"].Rows.Count > 0)
                    {
                        DataRow[] _dr;
                        _dr = _ds.Tables["ClaimNotes"].Select(" nBillingNoteType=20 And nNotetype=11");
                        if (_dr != null & _dr.Length>0 )
                        {
                            oTransaction.sBox19Notes = _dr[0][6].ToString();  // dtBox19Notes.Rows[0]["sNoteDescription"].ToString();
                        }
                        else
                        {
                            oTransaction.sBox19Notes = "";
                        }
                    }
                    else
                    {
                         oTransaction.sBox19Notes = "";
                    }
                }
                  else
                    {
                        oTransaction.sBox19Notes = "";
                    }

             
                               
              
                #endregion
              
                
                DataTable dtLines = null;
                #region "ClaimLines"
                if (_ds.Tables["ClaimLines"] != null)
                {
                    if (_ds.Tables["ClaimLines"].Rows.Count > 0)
                    {
                        dtLines = _ds.Tables["ClaimLines"].Copy();  
                    }
                }
                #endregion
                
                if (dtLines != null)
                {
                    if (dtLines.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLines.Rows.Count; i++)
                        {
                            oLine = new TransactionLine();
                            oLine.TransactionId = TransactionID;
                            oLine.TransactionLineId = Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]);
                            oLine.TransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]);
                            oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                            if (dtLines.Rows[i]["nToDate"] != null && Convert.ToInt64(dtLines.Rows[i]["nToDate"]) > 0)
                            {
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nToDate"]));
                            }
                            else
                            {
                                oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                            }
                            oLine.POSCode = dtLines.Rows[i]["sPOSCode"].ToString();
                            oLine.POSDescription = dtLines.Rows[i]["sPOSDescription"].ToString();
                            oLine.TOSCode = dtLines.Rows[i]["sTOSCode"].ToString();
                            oLine.TOSDescription = dtLines.Rows[i]["sTOSDescription"].ToString();
                            oLine.CPTCode = dtLines.Rows[i]["sCPTCode"].ToString();
                            oLine.CPTDescription = dtLines.Rows[i]["sCPTDescription"].ToString();
                            oLine.CrosswalkCPTCode = dtLines.Rows[i]["sCrossWalkCPTCode"].ToString();
                            oLine.Dx1Code = dtLines.Rows[i]["sDx1Code"].ToString();
                            oLine.Dx1Description = dtLines.Rows[i]["sDx1Description"].ToString();
                            oLine.Dx2Code = dtLines.Rows[i]["sDx2Code"].ToString();
                            oLine.Dx2Description = dtLines.Rows[i]["sDx2Description"].ToString();
                            oLine.Dx3Code = dtLines.Rows[i]["sDx3Code"].ToString();
                            oLine.Dx3Description = dtLines.Rows[i]["sDx3Description"].ToString();
                            oLine.Dx4Code = dtLines.Rows[i]["sDx4Code"].ToString();
                            oLine.Dx4Description = dtLines.Rows[i]["sDx4Description"].ToString();
                            oLine.Dx5Code = dtLines.Rows[i]["sDx5Code"].ToString();
                            oLine.Dx5Description = dtLines.Rows[i]["sDx5Description"].ToString();
                            oLine.Dx6Code = dtLines.Rows[i]["sDx6Code"].ToString();
                            oLine.Dx6Description = dtLines.Rows[i]["sDx6Description"].ToString();
                            oLine.Dx7Code = dtLines.Rows[i]["sDx7Code"].ToString();
                            oLine.Dx7Description = dtLines.Rows[i]["sDx7Description"].ToString();
                            oLine.Dx8Code = dtLines.Rows[i]["sDx8Code"].ToString();
                            oLine.Dx8Description = dtLines.Rows[i]["sDx8Description"].ToString();
                            oLine.Dx1Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx1Pointer"]);
                            oLine.Dx2Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx2Pointer"]);
                            oLine.Dx3Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx3Pointer"]);
                            oLine.Dx4Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx4Pointer"]);
                            oLine.Dx5Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx5Pointer"]);
                            oLine.Dx6Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx6Pointer"]);
                            oLine.Dx7Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx7Pointer"]);
                            oLine.Dx8Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx8Pointer"]);
                            oLine.Mod1Code = dtLines.Rows[i]["sMod1Code"].ToString();
                            oLine.Mod1Description = dtLines.Rows[i]["sMod1Description"].ToString();
                            oLine.Mod2Code = dtLines.Rows[i]["sMod2Code"].ToString();
                            oLine.Mod2Description = dtLines.Rows[i]["sMod2Description"].ToString();
                            oLine.Mod3Code = dtLines.Rows[i]["sMod3Code"].ToString();
                            oLine.Mod3Description = dtLines.Rows[i]["sMod3Description"].ToString();
                            oLine.Mod4Code = dtLines.Rows[i]["sMod4Code"].ToString();
                            oLine.Mod4Description = dtLines.Rows[i]["sMod4Description"].ToString();
                            oLine.Charges = Convert.ToDecimal(dtLines.Rows[i]["dCharges"]);
                            oLine.Unit = Convert.ToDecimal(dtLines.Rows[i]["dUnit"]);
                            oLine.Total = Convert.ToDecimal(dtLines.Rows[i]["dTotal"]);
                            oLine.AllowedCharges = Convert.ToDecimal(dtLines.Rows[i]["dAllowed"]);
                            oLine.RefferingProviderId = Convert.ToInt64(dtLines.Rows[i]["nProvider"]);
                            oLine.ClinicID = ClinicID;
                            oLine.ClaimNumber = Convert.ToInt64(dtLines.Rows[i]["nClaimNumber"]);
                            oLine.LineStatus = (TransactionStatus)Convert.ToInt32(dtLines.Rows[i]["nTransactionLineStatus"]);
                            oLine.NDCPrescriptionDesc = Convert.ToString(dtLines.Rows[i]["PrescriptionDesc"]);
                          
                            oLine.IsLabCPT = Convert.ToBoolean(dtLines.Rows[i]["bIsLabCPT"]);
                            oLine.AuthorizationNo = Convert.ToString(dtLines.Rows[i]["sAuthorizationNo"]);
                            oLine.SendToClaim = Convert.ToBoolean(dtLines.Rows[i]["bSentToClaim"]);

                            oLine.LinePrimaryDxCode = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxCode"]);
                            oLine.LinePrimaryDxDesc = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxDesc"]);

                            oLine.HCFA_RenderingFName = Convert.ToString(dtLines.Rows[i]["RenderingProviderFName"]);
                            oLine.HCFA_RenderingMName = Convert.ToString(dtLines.Rows[i]["RenderingProviderMName"]);
                            oLine.HCFA_RenderingLName = Convert.ToString(dtLines.Rows[i]["RenderingProviderLName"]);
                            oLine.HCFA_RenderingProviderAddress1 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr1"]);
                            oLine.HCFA_RenderingProviderAddress2 = Convert.ToString(dtLines.Rows[i]["RenderringProviderBusAddr2"]);
                            oLine.HCFA_RenderingProviderCity = Convert.ToString(dtLines.Rows[i]["RenderringProviderCity"]);
                            oLine.HCFA_RenderingProviderState = Convert.ToString(dtLines.Rows[i]["RenderringProviderState"]);
                            oLine.HCFA_RenderingProviderZip = Convert.ToString(dtLines.Rows[i]["RenderringProviderZip"]);
                            oLine.HCFA_RenderingProviderAreaCode = Convert.ToString(dtLines.Rows[i]["RenderringProviderAreaCode"]);
                            oLine.HCFA_RenderingProviderPhone = Convert.ToString(dtLines.Rows[i]["RenderringProviderPhone"]);
                            oLine.HCFA_RenderingProviderMedicalLicenceNo = Convert.ToString(dtLines.Rows[i]["RenderringProviderMedicalLicenceNo"]);
                            oLine.HCFA_RenderingProviderUPIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderUPIN"]);
                            oLine.HCFA_RenderingProviderNPI = Convert.ToString(dtLines.Rows[i]["RenderringProviderNPI"]);
                            oLine.HCFA_RenderingProviderTaxonomy = Convert.ToString(dtLines.Rows[i]["RenderringProviderTaxonomy"]);
                            oLine.HCFA_RenderingProviderSSN = Convert.ToString(dtLines.Rows[i]["RenderringProviderSSN"]);
                            oLine.HCFA_RenderingProviderEIN = Convert.ToString(dtLines.Rows[i]["RenderringProviderEmployerID"]);

                            oLine.NDCID = Convert.ToInt64(dtLines.Rows[i]["nNDCID"]);
                            oLine.NDCCodeQualifier = Convert.ToString(dtLines.Rows[i]["sNDCCodeQualifier"]);
                            oLine.NDCCode = Convert.ToString(dtLines.Rows[i]["sNDCCode"]);
                            oLine.NDCDescription = Convert.ToString(dtLines.Rows[i]["sNDCDescription"]);
                            oLine.NDCUnitCode = Convert.ToString(dtLines.Rows[i]["sNDCUnitCode"]);
                            oLine.NDCUnitDescription = Convert.ToString(dtLines.Rows[i]["sNDCUnitDescription"]);
                            oLine.NDCUnit = Convert.ToString(dtLines.Rows[i]["sNDCUnit"]);
                            oLine.NDCUnitPricing = Convert.ToString(dtLines.Rows[i]["sNDCUnitPricing"]);
                            if (dtLines.Rows[i]["HCFA_NDCCode"] != null && dtLines.Rows[i]["HCFA_NDCCode"] != DBNull.Value)
                            { oLine.DisplayNDCCode_HCFA = Convert.ToString(dtLines.Rows[i]["HCFA_NDCCode"]); }

                            oLine.EMG = Convert.ToBoolean(dtLines.Rows[i]["bEMG"]);
                            //**
                            oLine.IsServiceScreening = Convert.ToBoolean(dtLines.Rows[i]["bIsServiceScreening"]);
                            oLine.IsFamilyPlanningIndicator = Convert.ToBoolean(dtLines.Rows[i]["bIsFamilyPlanningIndicator"]);
                            oLine.IsMammogramCertNumber = Convert.ToBoolean(dtLines.Rows[i]["bisMammogram"]);
                          
                            #region Get Notes
                             DataTable dtNotes = null;

                            if (_ds.Tables["ClaimNotes"] != null)
                            {
                                if (_ds.Tables["ClaimNotes"].Rows.Count > 0)
                                {
                                    DataView  _dv = null;
                                    _dv = _ds.Tables["ClaimNotes"].DefaultView;
                                    _dv.RowFilter = (_dv.Table.Columns["nBillingNoteType"].ColumnName + " = 17 AND " + _dv.Table.Columns["nTransactionDetailID"].ColumnName + "=" + Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterDetailID"]));
                                    dtNotes = _dv.ToTable();  
                                    
                                }
                                else
                                {
                                    dtNotes = null;
                                }
                            }
                            else
                            {
                               dtNotes = null;
                            }


                          
                            #endregion

                            #region Anesthesia




                            if ((dtLines.Rows[i]["Ansthesia"] != DBNull.Value))
                            {
                                
                               if (Convert.ToString(dtLines.Rows[i]["Ansthesia"]) != "")
                                {

                                    oLine.Anesthesia = Convert.ToString(dtLines.Rows[i]["Ansthesia"]);
                                }
                            }

                            if((dtLines.Rows[i]["BoxG"]!=DBNull.Value   ))
                            {
                                if ( Convert.ToString(dtLines.Rows[i]["BoxG"]) != "")
                                {
                                    oLine.Unit = Convert.ToDecimal(dtLines.Rows[i]["BoxG"]);
                                }
                            }
                           

                            #endregion

                            #region Assign notes
                            if (dtNotes != null)
                            {

                                GeneralNote oLineNote = null;

                                for (int j = 0; j < dtNotes.Rows.Count; j++)
                                {
                                    
                                    oLineNote = new GeneralNote();
                                    oLineNote.TransactionID = TransactionID;
                                    oLineNote.TransactionLineId = Convert.ToInt64(dtNotes.Rows[j]["nLineNo"]);
                                    oLineNote.TransactionDetailID = Convert.ToInt64(dtNotes.Rows[j]["nTransactionDetailID"]);
                                    oLineNote.NoteID = Convert.ToInt64(dtNotes.Rows[j]["nNoteId"]);
                                    oLineNote.NoteType = (NoteType)(dtNotes.Rows[j]["nNoteType"]);
                                    oLineNote.NoteDate = Convert.ToInt64(dtNotes.Rows[j]["nNoteDateTime"]);                                  
                                    string _strNoteDescription = _strNoteDescription = Convert.ToString(dtNotes.Rows[j]["sNoteDescription"]);
                                    _strNoteDescription = _strNoteDescription.Replace("\r", " ");
                                    _strNoteDescription = _strNoteDescription.Replace("\n", " ");
                                    oLineNote.NoteDescription = _strNoteDescription.Trim();
                                    oLineNote.ClinicID = ClinicID;
                                    oLine.LineNotes.Add(oLineNote);
                                    if (oLineNote != null)
                                    { oLineNote.Dispose(); }
                                }
                                dtNotes.Dispose();
                                dtNotes = null;
                            }
                            #endregion
                                                       
                            
                            oTransaction.Lines.Add(oLine);
                        }
                    }
                    dtLines.Dispose();
                    dtLines = null;
                }

                
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
              
                oDBParameters.Dispose();

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (_ds != null)
                {
                    _ds.Tables.Clear();
                    _ds.Dispose();
                    _ds = null;
                }
            }
            return oTransaction;
        }
        public string GetProvider(Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string strProviderName = "";
            try
            {
                oDB.Connect(false);
                string sqlQuery = "SELECT (ISNULL(Provider_MST.sFirstName, '') + SPACE(1) +ISNULL(Provider_MST.sMiddleName, '') + SPACE(1) + ISNULL(Provider_MST.sLastName, '')) AS ProviderName FROM Provider_MST WITH(NOLOCK)  " +
                                  "WHERE nProviderID = " + ProviderID;

                oDB.Retrive_Query(sqlQuery, out dt);
                oDB.Disconnect();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        strProviderName = Convert.ToString(dt.Rows[0][0]);
                    }
                    else
                    {
                        strProviderName = "";
                    }
                }
                else
                {
                    strProviderName = "";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return strProviderName;
        }

        public string GetProviderSuffix(Int64 ProviderID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            DataTable dt = new DataTable();
            string strProviderName = "";
            try
            {
                oDB.Connect(false);
                string sqlQuery = "SELECT (ISNULL(Provider_MST.sSuffix, '')) AS sSuffix FROM Provider_MST WITH(NOLOCK)  " +
                                  "WHERE nProviderID = " + ProviderID;

                oDB.Retrive_Query(sqlQuery, out dt);
                oDB.Disconnect();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        strProviderName = Convert.ToString(dt.Rows[0][0]);
                    }
                    else
                    {
                        strProviderName = "";
                    }
                }
                else
                {
                    strProviderName = "";
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            return strProviderName;
        }

        public DataTable GetSubmitterInfo(Int64 ClinicID, Int64 _providerID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            GeneralSettings oSettings = new GeneralSettings(_databaseconnectionstring);
            string _strSQL = "";
            Object _objResult = null;
            DataTable dt = null;
            string strSetting = "";
            bool _IsColumnPresent = false;
            try
            {
                oDB.Connect(false);
                oSettings.GetSetting("SubmitterSetting", _providerID, ClinicID, out _objResult);
                if (_objResult != null)
                {
                    // |Company|Practice|Business|Clinic"
                    strSetting = Convert.ToString(_objResult);
                }

                switch (strSetting)
                {
                    case "Company":
                        {
                            _strSQL = " SELECT  sCompanyName AS SubmitterName, sCompanyAddressline1 AS SubmitterAddress1, sCompanyAddressline2 AS SubmitterAddress2, sCompanyCity AS SubmitterCity, sCompanyState AS SubmitterState, " +
                                      " sCompanyZIP AS SubmitterZip,ISNULL(sCompanyAreaCode,'') AS SubmitterAreaCode, sCompanyPhone AS SubmitterPhone, sCompanyContactName AS SubmitterContactName, sCompanyFax AS SubmitterFax " +
                                      " FROM  Provider_MST WITH(NOLOCK)  " +
                                      " WHERE (nProviderID = " + _providerID + ") AND (nClinicID =" + ClinicID + ") ";
                        }
                        break;
                    case "Practice":
                        {
                            _strSQL = " SELECT  ISNULL(sLastName ,'')+' '+ ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') AS SubmitterName, sPracticeAddressline1 AS SubmitterAddress1, sPracticeAddressline2 AS SubmitterAddress2, " +
                                       "  sPracticeCity AS SubmitterCity, sPracticeState AS SubmitterState, sPracticeZIP AS SubmitterZip,ISNULL(sPracticeAreaCode,'') AS SubmitterAreaCode, sPracPhoneNo AS SubmitterPhone, sPracFAX AS SubmitterFAX, " +
                                       " sCompanyContactName AS SubmitterContactName" +
                                       " FROM  Provider_MST WITH(NOLOCK)  " +
                                       " WHERE (nProviderID = " + _providerID + ") AND (nClinicID =" + ClinicID + ") ";
                        }
                        break;
                    case "Business":
                        {
                            _strSQL = " SELECT  (ISNULL(sLastName ,'')+' '+ ISNULL(sFirstName,'')+ ' '+ ISNULL(sMiddleName,'') )AS SubmitterName,  sBusinessAddressline1 AS SubmitterAddress1, sBusinessAddressline2 AS SubmitterAddress2, sBusinessCity AS SubmitterCity, sBusinessState AS SubmitterState, sBusinessZIP AS SubmitterZip,ISNULL(sBusinessAreaCode,'') AS SubmitterAreaCode, " +
                                        " sBusPhoneNo AS SubmitterPhone, sBusFAX AS SubmitterFAX, " +
                                        " sCompanyContactName AS SubmitterContactName " +
                                        " FROM  Provider_MST WITH(NOLOCK) " +
                                        " WHERE (nProviderID = " + _providerID + ") AND (nClinicID =" + ClinicID + ") ";
                        }
                        break;
                    case "Clinic":
                        {
                            dt = new DataTable();
                            _strSQL = " select column_name, data_type, character_maximum_length from information_schema.columns where table_name = 'Clinic_MST'";

                            oDB.Retrive_Query(_strSQL, out dt);

                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int _rowIndex = 0; _rowIndex < dt.Rows.Count; _rowIndex++)
                                {
                                    if (dt.Rows[_rowIndex][0].ToString() == "sAddress1")
                                    {
                                        _IsColumnPresent = true;
                                        break;
                                    }
                                }
                                _strSQL = "";
                                if (_IsColumnPresent)
                                {
                                    _strSQL = " SELECT sClinicName AS SubmitterName,sAddress1 AS SubmitterAddress1,sAddress2 AS SubmitterAddress2,sStreet AS SubmitterStreet,sCity AS SubmitterCity,sState AS SubmitterState,sZIP AS SubmitterZIP,ISNULL(sAreaCode,'') AS SubmitterAreaCode,sPhoneNo ,sMobileNo AS SubmitterMobile, " +
                                              " sFAX ,sTAXID AS SubmitterTAXID,sContactPersonName AS SubmitterContactName, sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone AS SubmitterPhone, " +
                                              " sContactPersonFAX AS SubmitterFAX,sContactPersonMobile FROM Clinic_MST WITH(NOLOCK) WHERE nClinicID =" + ClinicID + " ";
                                }
                                else
                                {
                                    _strSQL = " SELECT  sClinicName AS SubmitterName, sAddress AS SubmitterAddress1, sStreet AS SubmitterAddress2, sCity AS SubmitterCity, sState AS SubmitterState, sZIP AS SubmitterZIP,ISNULL(sAreaCode,'') AS SubmitterAreaCode, sPhoneNo, sMobileNo, sFAX, sEmail, sURL, sTAXID, imgClinicLogo, "
                                            + " sContactPersonName AS SubmitterContactName, sContactPersonAddress, sContactPersonPhone AS SubmitterPhone, sContactPersonFAX AS SubmitterFAX, sContactPersonEmail, sContactPersonMobile AS SubmitterMobile, "
                                            + " sExternalcode FROM Clinic_MST WITH(NOLOCK) WHERE nClinicID =" + ClinicID + " ";
                                }
                            }

                        }
                        break;
                    default:
                        dt = new DataTable();
                        _strSQL = " SELECT column_name, data_type, character_maximum_length FROM information_schema.columns WHERE table_name = 'Clinic_MST'";

                        oDB.Retrive_Query(_strSQL, out dt);
                        if (dt.Rows.Count > 0)
                        {
                            for (int _rowIndex = 0; _rowIndex < dt.Rows.Count; _rowIndex++)
                            {
                                if (dt.Rows[_rowIndex][0].ToString() == "sAddress1")
                                {
                                    _IsColumnPresent = true;
                                }
                            }
                            _strSQL = "";
                            if (_IsColumnPresent)
                            {
                                _strSQL = " SELECT sClinicName AS SubmitterName,sAddress1 AS SubmitterAddress1,sAddress2 AS SubmitterAddress2,sStreet AS SubmitterStreet,sCity AS SubmitterCity,sState AS SubmitterState,sZIP AS SubmitterZIP,ISNULL(sAreaCode,'') AS SubmitterAreaCode,sPhoneNo ,sMobileNo AS SubmitterMobile, " +
                                          " sFAX ,sTAXID AS SubmitterTAXID,sContactPersonName AS SubmitterContactName, sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone AS SubmitterPhone, " +
                                          " sContactPersonFAX AS SubmitterFAX,sContactPersonMobile FROM Clinic_MST WITH(NOLOCK) WHERE nClinicID =" + ClinicID + " ";
                            }
                            else
                            {
                                _strSQL = " SELECT sClinicName AS SubmitterName, sAddress AS SubmitterAddress1, sStreet AS SubmitterAddress2, sCity AS SubmitterCity, sState AS SubmitterState, sZIP AS SubmitterZIP,ISNULL(sAreaCode,'') AS SubmitterAreaCode, sPhoneNo, sMobileNo, sFAX, sEmail, sURL, sTAXID, imgClinicLogo, "
                                        + " sContactPersonName AS SubmitterContactName, sContactPersonAddress, sContactPersonPhone AS SubmitterPhone, sContactPersonFAX AS SubmitterFAX, sContactPersonEmail, sContactPersonMobile AS SubmitterMobile, "
                                        + " sExternalcode FROM Clinic_MST WITH(NOLOCK) WHERE nClinicID =" + ClinicID + " ";
                            }
                        }
                        break;
                }
                if (_strSQL != "")
                {
                    dt = new DataTable();
                    oDB.Retrive_Query(_strSQL, out dt);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }
            return dt;
        }

        public Transaction GetChargesClaimDetails(Int64 TransactionID, Int64 ClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtTrans = new DataTable();
            Transaction oTransaction = new Transaction();
            TransactionLine oLine = null;
            Int64 PatientID = 0;
            try
            {

                oDB.Connect(false);
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_Claim_MST", oDBParameters, out dtTrans);

                if (dtTrans != null)
                {
                    if (dtTrans.Rows.Count > 0)
                    {
                        oTransaction.TransactionID = TransactionID;
                        oTransaction.TransactionMasterID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionMasterID"]);
                        oTransaction.MasterAppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nMasterAppointmentID"]);
                        oTransaction.AppointmentID = Convert.ToInt64(dtTrans.Rows[0]["nAppointmentID"]);
                        oTransaction.VisitID = Convert.ToInt64(dtTrans.Rows[0]["nVisitID"]);
                        oTransaction.OnsiteDate = Convert.ToInt64(dtTrans.Rows[0]["nOnsiteDate"]);
                        oTransaction.InjuryDate = Convert.ToInt64(dtTrans.Rows[0]["nInjuryDate"]);
                        oTransaction.UnableToWorkFromDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkFromDate"]);
                        oTransaction.UnableToWorkTillDate = Convert.ToInt64(dtTrans.Rows[0]["nUnableToWorkTillDate"]);
                        oTransaction.TransactionDate = Convert.ToInt64(dtTrans.Rows[0]["nTransactionDate"]);
                        oTransaction.CaseNoPrefix = dtTrans.Rows[0]["sCaseNoPrefix"].ToString();
                        oTransaction.ClaimNo = Convert.ToInt64(dtTrans.Rows[0]["nClaimNo"]);

                        oTransaction.BatchNoPrefix = "Batch";
                        oTransaction.BatchNo = 0;

                        #region "Retrive Batch No"

                        DataTable dtBatchNo = new DataTable();
                        string _strSQLBatchNo = "";
                        _strSQLBatchNo = "SELECT  TOP 1   BL_Batch_MST.sBatchNoPrefix, BL_Batch_MST.nBatchNo " +
                        " FROM BL_Batch_DTL WITH(NOLOCK) INNER JOIN BL_Batch_MST WITH(NOLOCK) ON BL_Batch_DTL.nBatchID = BL_Batch_MST.nBatchID " +
                        " WHERE (BL_Batch_DTL.nTransactionID = " + TransactionID + ")";
                        oDB.Retrive_Query(_strSQLBatchNo, out dtBatchNo);
                        if (dtBatchNo != null)
                        {
                            if (dtBatchNo.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dtBatchNo.Rows.Count - 1; i++)
                                {
                                    if (dtBatchNo.Rows[0]["sBatchNoPrefix"].GetType() != typeof(System.DBNull))
                                    {
                                        oTransaction.BatchNoPrefix = dtBatchNo.Rows[0]["sBatchNoPrefix"].ToString();
                                    }
                                    if (dtBatchNo.Rows[0]["nBatchNo"].GetType() != typeof(System.DBNull))
                                    {
                                        oTransaction.BatchNo = Convert.ToInt64(dtBatchNo.Rows[0]["nBatchNo"].ToString());
                                    }
                                }
                            }
                            dtBatchNo.Dispose();
                            dtBatchNo = null;
                        }
                        #endregion

                        PatientID = Convert.ToInt64(dtTrans.Rows[0]["nPatientID"]);
                        oTransaction.PatientID = PatientID;
                        oTransaction.PatientCode = Convert.ToString(dtTrans.Rows[0]["sPatientCode"]);
                        oTransaction.PatientName = Convert.ToString(dtTrans.Rows[0]["sPatientName"]); 
                        oTransaction.ProviderID = Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]);
                        oTransaction.ProviderName = GetProvider(Convert.ToInt64(dtTrans.Rows[0]["nTransactionProviderID"]));
                        oTransaction.MaritalStatus = dtTrans.Rows[0]["sMaritalStatus"].ToString();
                        oTransaction.FacilityCode = dtTrans.Rows[0]["sFacilityCode"].ToString();
                        oTransaction.FacilityDescription = dtTrans.Rows[0]["sFacilityDescription"].ToString();
                        oTransaction.PrefixID = 0;
                        oTransaction.ClinicID = ClinicID;
                        oTransaction.TransactionMode = (TransactionType)Convert.ToInt64(dtTrans.Rows[0]["nTransactionType"]);
                        oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt32(dtTrans.Rows[0]["nTransactionStatusID"]);
                        oTransaction.State = Convert.ToString(dtTrans.Rows[0]["sState"]);
                        oTransaction.HospitalizationDateFrom = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateFrom"]);
                        oTransaction.HospitalizationDateTo = Convert.ToInt64(dtTrans.Rows[0]["nHopitalizationDateTo"]);
                        oTransaction.OutSideLab = Convert.ToBoolean(dtTrans.Rows[0]["bOutSideLab"]);
                        oTransaction.OutSideLabCharges = Convert.ToDecimal(dtTrans.Rows[0]["dOutSideLabCharges"]);
                        oTransaction.WorkersComp = Convert.ToBoolean(dtTrans.Rows[0]["bWorkersComp"]);
                        oTransaction.WorkersCompNo = Convert.ToString(dtTrans.Rows[0]["sWorkersCompNo"]);
                        oTransaction.WorkersCompPrintonCMS1500 = Convert.ToBoolean(dtTrans.Rows[0]["bIsWorkersCompOnCMS1500"]);
                        oTransaction.AutoClaim = Convert.ToBoolean(dtTrans.Rows[0]["bAutoClaim"]);
                        oTransaction.AccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nAccidentDate"]);
                        oTransaction.PriorAuthorizationID = Convert.ToInt64(dtTrans.Rows[0]["nAuthorizationID"]);
                        oTransaction.PriorAuthorizationNo = Convert.ToString(dtTrans.Rows[0]["sAuthorizationNumber"]);
                        oTransaction.ReferralProviderID = Convert.ToInt64(dtTrans.Rows[0]["nReferralID"]);
                        oTransaction.ReferralProviderName = Convert.ToString(dtTrans.Rows[0]["ReferralName"]);
                        oTransaction.SendCounter = Convert.ToInt32(dtTrans.Rows[0]["nSendCounter"]);
                        oTransaction.SendToRejection = Convert.ToInt32(dtTrans.Rows[0]["nSendToRejection"]);
                        oTransaction.LastStatusId = Convert.ToInt64(dtTrans.Rows[0]["nLastStatusId"]);
                        oTransaction.TransactionUserID = Convert.ToInt64(dtTrans.Rows[0]["nUserID"]);
                        oTransaction.TransactionUserName = Convert.ToString(dtTrans.Rows[0]["sUserName"]);
                        oTransaction.OtherAccident = Convert.ToBoolean(dtTrans.Rows[0]["bOtherAccident"]);
                        oTransaction.OtherAccidentDate = Convert.ToInt64(dtTrans.Rows[0]["nOtherAccidentDate"]);
                        oTransaction.SendToInsuranceFlag = ((InsuranceTypeFlag)Convert.ToInt16(dtTrans.Rows[0]["nSendToInsFlag"]));

                        oTransaction.InsuranceID = Convert.ToInt64(dtTrans.Rows[0]["nInsuranceID"]);
                        oTransaction.InsuranceName = Convert.ToString(dtTrans.Rows[0]["sInsuranceName"]);
                        oTransaction.InsuranceFlag = Convert.ToInt64(dtTrans.Rows[0]["nInsuranceFlag"]);
                        oTransaction.ContactID = Convert.ToInt64(dtTrans.Rows[0]["nContactID"]);
                        oTransaction.ResponsibilityNo = Convert.ToInt16(dtTrans.Rows[0]["nResponsibilityNo"]);
                        oTransaction.ResponsibilityType = ((PayerMode)Convert.ToInt16(dtTrans.Rows[0]["nResponsibilityType"]));
                        oTransaction.SubClaimNo = Convert.ToString(dtTrans.Rows[0]["nSubClaimNo"]);
                        oTransaction.ClaimStatus = (ClaimStatus)Convert.ToInt16(dtTrans.Rows[0]["nClaimStatus"]);
                        oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt16(dtTrans.Rows[0]["nStatus"]);

                        oTransaction.ClaimStatus = (ClaimStatus)Convert.ToInt16(dtTrans.Rows[0]["nClaimStatus"]);
                        oTransaction.Transaction_Status = (TransactionStatus)Convert.ToInt16(dtTrans.Rows[0]["nStatus"]);
                        oTransaction.CloseDayTrayID = Convert.ToInt64(dtTrans.Rows[0]["nChargesDayTrayID"]);
                        oTransaction.CloseDayTrayCode = Convert.ToString(dtTrans.Rows[0]["sChargesTrayCode"]);
                        oTransaction.CloseDayTrayName = Convert.ToString(dtTrans.Rows[0]["sChargesTrayDescription"]);

                        //Parent Claim No and Parent Transaction Id if Claim is splitted
                        oTransaction.ParentTransactionID = Convert.ToInt64(dtTrans.Rows[0]["nParentTransactionID"]);
                        oTransaction.ParentClaimNo = Convert.ToString(dtTrans.Rows[0]["nParentClaimNo"]);


                        //Hold Fee-Schedule
                        oTransaction.FeeScheduleType = (FeeScheduleType)Convert.ToInt16(dtTrans.Rows[0]["nFeeScheduleType"]);
                        oTransaction.FeeScheduleID = Convert.ToInt64(dtTrans.Rows[0]["nFeeScheduleID"]);
                        oTransaction.FacilityType = (FacilityType)Convert.ToInt16(dtTrans.Rows[0]["nFacilityType"]);

                        oTransaction.MainClaimNo = Convert.ToString(dtTrans.Rows[0]["sMainClaimNo"]);

                        //MaheshB 20100426 gloPM5040
                        oTransaction.IsRebill = Convert.ToBoolean(dtTrans.Rows[0]["bIsRebilled"]);

              

                        //Debasish Das 20100508 gloPM5040
                        if (Convert.ToInt16(dtTrans.Rows[0]["ParentClaimStatus"]) == (Int16)TransactionStatus.Resent)
                        {
                            oTransaction.IsResend = true;
                        }
                        else
                        {
                            oTransaction.IsResend = false;
                        }
                        //**
                        //Debasish Das 20100514 gloPM5040
                        if (Convert.ToString(dtTrans.Rows[0]["bIsVoid"]).ToLower() == Boolean.TrueString.ToLower())
                        {
                            oTransaction.IsVoid = true;
                        }
                        else
                        {
                            oTransaction.IsVoid = false;
                        }

                        //**
                        //Debasish Das 20100514 gloPM5040
                        oTransaction.IsSameAsBillingProvider = Convert.ToBoolean(dtTrans.Rows[0]["bSameAsBillingProvider"]);
                        //**

                        //Debasish Das 20100616 gloPM5040
                        oTransaction.ReferalProviderID_New = Convert.ToInt64(dtTrans.Rows[0]["nReferralProviderID"]);
                        //**

                    }
                    dtTrans.Dispose();
                    dtTrans = null;
                }
                

                #region " Commented Insurance"

                ////BL_Transaction_MST_Ins
                //DataTable dtInsurance = new DataTable();
                //oTransaction.Insurances = new TransactionInsurances();

                //oDBParameters.Clear();
                //oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDB.Retrive("BL_SELECT_Transaction_MST_Ins", oDBParameters, out dtInsurance);

                #endregion

                //BL_Transaction_Lines
                DataTable dtLines = new DataTable();
                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nTransactionLineNo", 0, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", oTransaction.ContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Charges_Claim_Lines", oDBParameters, out dtLines);

                if (dtLines != null)
                {
                    if (dtLines.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLines.Rows.Count; i++)
                        {

                            oLine = new TransactionLine();
                            oLine.TransactionId = TransactionID;
                            oLine.TransactionLineId = Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]);
                            oLine.TransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]);

                            oLine.TransactionMasterID = Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterID"]);
                            oLine.TransactionMasterDetailID = Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterDetailID"]);

                            oLine.DateServiceFrom = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nFromDate"]));
                            oLine.DateServiceTill = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtLines.Rows[i]["nToDate"]));
                            oLine.POSCode = dtLines.Rows[i]["sPOSCode"].ToString();
                            oLine.POSDescription = dtLines.Rows[i]["sPOSDescription"].ToString();
                            oLine.TOSCode = dtLines.Rows[i]["sTOSCode"].ToString();
                            oLine.TOSDescription = dtLines.Rows[i]["sTOSDescription"].ToString();
                            oLine.CPTCode = dtLines.Rows[i]["sCPTCode"].ToString();
                            oLine.CPTDescription = dtLines.Rows[i]["sCPTDescription"].ToString();
                            oLine.CrosswalkCPTCode = dtLines.Rows[i]["sCrossWalkCPTCode"].ToString();
                            oLine.Dx1Code = dtLines.Rows[i]["sDx1Code"].ToString();
                            oLine.Dx1Description = dtLines.Rows[i]["sDx1Description"].ToString();
                            oLine.Dx2Code = dtLines.Rows[i]["sDx2Code"].ToString();
                            oLine.Dx2Description = dtLines.Rows[i]["sDx2Description"].ToString();
                            oLine.Dx3Code = dtLines.Rows[i]["sDx3Code"].ToString();
                            oLine.Dx3Description = dtLines.Rows[i]["sDx3Description"].ToString();
                            oLine.Dx4Code = dtLines.Rows[i]["sDx4Code"].ToString();
                            oLine.Dx4Description = dtLines.Rows[i]["sDx4Description"].ToString();
                            oLine.Dx5Code = dtLines.Rows[i]["sDx5Code"].ToString();
                            oLine.Dx5Description = dtLines.Rows[i]["sDx5Description"].ToString();
                            oLine.Dx6Code = dtLines.Rows[i]["sDx6Code"].ToString();
                            oLine.Dx6Description = dtLines.Rows[i]["sDx6Description"].ToString();
                            oLine.Dx7Code = dtLines.Rows[i]["sDx7Code"].ToString();
                            oLine.Dx7Description = dtLines.Rows[i]["sDx7Description"].ToString();
                            oLine.Dx8Code = dtLines.Rows[i]["sDx8Code"].ToString();
                            oLine.Dx8Description = dtLines.Rows[i]["sDx8Description"].ToString();
                            oLine.Dx1Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx1Pointer"]);
                            oLine.Dx2Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx2Pointer"]);
                            oLine.Dx3Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx3Pointer"]);
                            oLine.Dx4Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx4Pointer"]);
                            oLine.Dx5Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx5Pointer"]);
                            oLine.Dx6Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx6Pointer"]);
                            oLine.Dx7Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx7Pointer"]);
                            oLine.Dx8Ptr = Convert.ToBoolean(dtLines.Rows[i]["nDx8Pointer"]);
                            oLine.Mod1Code = dtLines.Rows[i]["sMod1Code"].ToString();
                            oLine.Mod1Description = dtLines.Rows[i]["sMod1Description"].ToString();
                            oLine.Mod2Code = dtLines.Rows[i]["sMod2Code"].ToString();
                            oLine.Mod2Description = dtLines.Rows[i]["sMod2Description"].ToString();
                            oLine.Mod3Code = dtLines.Rows[i]["sMod3Code"].ToString();
                            oLine.Mod3Description = dtLines.Rows[i]["sMod3Description"].ToString();
                            oLine.Mod4Code = dtLines.Rows[i]["sMod4Code"].ToString();
                            oLine.Mod4Description = dtLines.Rows[i]["sMod4Description"].ToString();
                            oLine.Charges = Convert.ToDecimal(dtLines.Rows[i]["dCharges"]);
                            oLine.BilledAmount = Convert.ToDecimal(dtLines.Rows[i]["dBilliedAmount"]);
                            oLine.Unit = Convert.ToDecimal(dtLines.Rows[i]["dUnit"]);
                            oLine.Total = Convert.ToDecimal(dtLines.Rows[i]["dTotal"]);
                            oLine.AllowedCharges = Convert.ToDecimal(dtLines.Rows[i]["dAllowed"]);
                            oLine.PatientResponsibility = Convert.ToDecimal(dtLines.Rows[i]["dPatientResponsibility"]);
                            oLine.RefferingProviderId = Convert.ToInt64(dtLines.Rows[i]["nProvider"]);
                            oLine.ClinicID = ClinicID;
                            oLine.ClaimNumber = Convert.ToInt64(dtLines.Rows[i]["nClaimNumber"]);
                            oLine.LineStatus = (TransactionStatus)Convert.ToInt32(dtLines.Rows[i]["nTransactionLineStatus"]);


                            //Code added on 20090511 by - Sagar Ghodke
                            oLine.IsLabCPT = Convert.ToBoolean(dtLines.Rows[i]["bIsLabCPT"]);
                            oLine.AuthorizationNo = Convert.ToString(dtLines.Rows[i]["sAuthorizationNo"]);
                            oLine.SendToClaim = Convert.ToBoolean(dtLines.Rows[i]["bSentToClaim"]);
                            //End code add 20090511,Sagar Ghodke

                            oLine.IsHold = Convert.ToBoolean(dtLines.Rows[i]["bIsHold"]);
                            oLine.HoldReason = Convert.ToString(dtLines.Rows[i]["sHoldReason"]);

                            oLine.LinePrimaryDxCode = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxCode"]);
                            oLine.LinePrimaryDxDesc = Convert.ToString(dtLines.Rows[i]["sLinePrimaryDxDesc"]);


                            //Parent Parent Transaction Id and Parent Transaction Detail ID if Claim is splitted
                            oLine.ParentTransactionID = Convert.ToInt64(dtLines.Rows[i]["nParentTransactionID"]);
                            oLine.ParentTransactionDetailID = Convert.ToInt64(dtLines.Rows[i]["nParentTransactionDetailID"]);
                            oLine.IsLineSplitted = Convert.ToBoolean(dtLines.Rows[i]["bIsSplitted"]);

                            //Hold Fee-Schedule
                            oLine.FeeScheduleType = (FeeScheduleType)Convert.ToInt16(dtLines.Rows[0]["nFeeScheduleType"]);
                            oLine.FeeScheduleID = Convert.ToInt64(dtLines.Rows[0]["nFeeScheduleID"]);
                            oLine.FacilityType = (FacilityType)Convert.ToInt16(dtLines.Rows[0]["nFacilityType"]);

                            oLine.RevenueCode = Convert.ToString(dtLines.Rows[i]["sRevenueCode"]);

                            //SLR: Commented Since it is not used
                            ////BL_Transaction_Lines_Notes
                            //DataTable dtNotes = new DataTable();
                            //oDBParameters.Clear();
                            ////oDBParameters.Add("@nTransactionID", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                            ////oDBParameters.Add("@nLineNo", Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]), ParameterDirection.Input, SqlDbType.BigInt);
                            ////oDBParameters.Add("@nTransactionDetailID", Convert.ToInt64(dtLines.Rows[i]["nTransactionDetailID"]), ParameterDirection.Input, SqlDbType.BigInt);
                            //oDBParameters.Add("@nLineNo", Convert.ToInt64(dtLines.Rows[i]["nTransactionLineNo"]), ParameterDirection.Input, SqlDbType.BigInt);
                            //oDBParameters.Add("@nTransactionID", Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterID"]), ParameterDirection.Input, SqlDbType.BigInt);
                            //oDBParameters.Add("@nTransactionDetailID", Convert.ToInt64(dtLines.Rows[i]["nTransactionMasterDetailID"]), ParameterDirection.Input, SqlDbType.BigInt);
                            //oDBParameters.Add("@nNoteId", 0, ParameterDirection.Input, SqlDbType.BigInt);
                            //oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                            //oDB.Retrive("BL_SELECT_Transaction_Lines_Notes", oDBParameters, out dtNotes);
                            ////oDB.Retrive("BL_SELECT_Notes", oDBParameters, out dtNotes);

                          

                            // Added By Pramod Nair - To Set the Insurance From Master Object For Each Transaction Line 
                            oLine.InsuranceName = Convert.ToString(oTransaction.InsuranceName);
                            if (Convert.ToInt64(oTransaction.InsuranceFlag) == InsuranceTypeFlag.Primary.GetHashCode())
                            {
                                oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary.ToString();
                            }
                            else if (Convert.ToInt64(oTransaction.InsuranceFlag) == InsuranceTypeFlag.Secondary.GetHashCode())
                            {
                                oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary.ToString();
                            }
                            else if (Convert.ToInt64(oTransaction.InsuranceFlag) == InsuranceTypeFlag.Tertiary.GetHashCode())
                            {
                                oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary.ToString();
                            }
                            else
                            {
                                oLine.InsurancePrimarySecondaryTertiary = "";
                            }

                            #region " Commented Insurance"

                            //if (dtInsurance != null)
                            //{
                            //    if (dtInsurance.Rows.Count > 0)
                            //    {
                            //        //Addded by Anil 20080912 

                            //        //nTransactionID,nInsuranceID,nClinicID nTransactionDetailID =1,nTransactionLineNo
                            //        for (int j = 0; j < dtInsurance.Rows.Count; j++)
                            //        {
                            //            if (Convert.ToString(dtInsurance.Rows[j]["nTransactionLineNo"]) != "")
                            //            {
                            //                if (Convert.ToInt64(dtInsurance.Rows[j]["nTransactionLineNo"]) == oLine.TransactionLineId)
                            //                {
                            //                    oLine.InsuranceID = Convert.ToInt64(dtInsurance.Rows[j]["nInsuranceID"]);
                            //                    oLine.InsuranceSelfMode = (PayerMode)Convert.ToInt32(dtInsurance.Rows[j]["nPaymentMode"]);

                            //                    gloPatient.gloInsurance ogloInsurance = new gloPatient.gloInsurance(_databaseconnectionstring);
                            //                    DataTable dtTempInsurance = new DataTable();
                            //                    //dtTempInsurance = ogloInsurance.GetInsurance(oLine.InsuranceID);
                            //                    dtTempInsurance = ogloInsurance.GetInsurance(oLine.InsuranceID, PatientID);
                            //                    if (dtTempInsurance != null && dtTempInsurance.Rows.Count > 0)
                            //                    {
                            //                        //Contact
                            //                        oLine.InsuranceName = Convert.ToString(dtTempInsurance.Rows[0]["Name"]);
                            //                        //Vinayak - Is Primary/secondary/tertiary
                            //                        if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Primary.GetHashCode())
                            //                        {
                            //                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Primary.ToString();
                            //                        }
                            //                        else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Secondary.GetHashCode())
                            //                        {
                            //                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Secondary.ToString();
                            //                        }
                            //                        else if (Convert.ToInt32(dtInsurance.Rows[j]["nInsuranceFlag"]) == InsuranceTypeFlag.Tertiary.GetHashCode())
                            //                        {
                            //                            oLine.InsurancePrimarySecondaryTertiary = InsuranceTypeFlag.Tertiary.ToString();
                            //                        }
                            //                        else
                            //                        {
                            //                            oLine.InsurancePrimarySecondaryTertiary = "";
                            //                        }

                            //                    }
                            //                    if (dtTempInsurance != null) { dtTempInsurance.Dispose(); }
                            //                    if (ogloInsurance != null) { ogloInsurance.Dispose(); }

                            //                }
                            //            }
                            //        }
                            //    }
                            //}

                            #endregion

                            //Transaction line is added in the Transaction
                            oTransaction.Lines.Add(oLine);
                        }
                    }
                    dtLines.Dispose();
                    dtLines = null;
                }


                DataTable dtInsurancePlan = new DataTable();
                oDBParameters.Clear();
                oDBParameters.Add("@nTransactionID", oTransaction.TransactionMasterID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", ClinicID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_SELECT_Transaction_InsPlan", oDBParameters, out dtInsurancePlan);

                if (dtInsurancePlan != null)
                {
                    for (int i = 0; i < dtInsurancePlan.Rows.Count; i++)
                    {
                        TransactionInsurancePlan _TransactionInsurancePlan = new TransactionInsurancePlan();
                        _TransactionInsurancePlan.TransactionId = Convert.ToInt64(dtInsurancePlan.Rows[i]["nTransactionID"]);
                        _TransactionInsurancePlan.PatientId = Convert.ToInt64(dtInsurancePlan.Rows[i]["nPatientID"]);
                        _TransactionInsurancePlan.ClaimNo = Convert.ToInt64(dtInsurancePlan.Rows[i]["nClaimNo"]);
                        _TransactionInsurancePlan.InsuranceID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nInsuranceID"]);
                        _TransactionInsurancePlan.ContactID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nContactID"]);
                        _TransactionInsurancePlan.ResponsibilityNo = Convert.ToInt16(dtInsurancePlan.Rows[i]["nResponsibilityNo"]);
                        _TransactionInsurancePlan.ResponsibilityType = Convert.ToInt32(dtInsurancePlan.Rows[i]["nResponsibilityType"]);
                        _TransactionInsurancePlan.IsWorkerComp = Convert.ToBoolean(dtInsurancePlan.Rows[i]["bworkerscomp"]);
                        _TransactionInsurancePlan.InsuranceName = Convert.ToString(dtInsurancePlan.Rows[i]["sInsuranceName"]);
                        _TransactionInsurancePlan.CopayAmount = 0;
                        _TransactionInsurancePlan.ClinicID = Convert.ToInt64(dtInsurancePlan.Rows[i]["nClinicId"]);
                        oTransaction.InsurancePlans.Add(_TransactionInsurancePlan);
                    }
                    dtInsurancePlan.Dispose();
                    dtInsurancePlan = null;
                }


            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (dtTrans != null)
                {
                    dtTrans.Dispose();
                    dtTrans = null;
                }
                oDBParameters.Dispose();

                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }

            }
            return oTransaction;
        }

        public DataTable GetMidLevelProviders(Int64 nRenderingProviderID, Int64 nBillingProviderID, Int64 nContactID, Int64 nClinicID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            DataTable dtProviders = null;
            try
            {
                oDB.Connect(false);
                oDBParameters.Add("@nRenderingProviderID", nRenderingProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nBillingProviderID", nBillingProviderID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", nClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("GET_PROVIDER_MIDLEVEL_SETTINGS", oDBParameters, out dtProviders);

                oDB.Disconnect();

            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }
            return dtProviders;
        }

        //This method is not used.
        private void GetExpandedClaimLimits(Int64 nContactID, int nSettingType, Int64 nClinicID, out bool bAllowExpandedClaims, out int nClaimLines, out int nDiagnosis)
        {
            //bool _result = false;
            //bool _IsExpandedClaim = false;
            //Object _objResult = null;
            //gloSettings.GeneralSettings oSettings = null;

            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();

            DataTable dtLimits = null;
            nClaimLines = 6;
            nDiagnosis = 4;
            bAllowExpandedClaims = false;

            try
            {
                //oSettings=new gloSettings.GeneralSettings(gloSettings.AppSettings.ConnectionStringPM);
                //oSettings.GetSetting("ALLOWEXPANDEDELECTRONICCLAIMS", out _objResult);
                //_IsExpandedClaim = Convert.ToBoolean(_objResult);
                //bAllowExpandedClaims = _IsExpandedClaim;

                oDB.Connect(false);
                oDBParameters.Add("@nContactID", nContactID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nSettingType", nSettingType, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@nClinicID", nClinicID, ParameterDirection.Input, SqlDbType.BigInt);

                oDB.Retrive("BL_Get_Expanded_Claim_Setting", oDBParameters, out dtLimits);
                oDB.Disconnect();

                bAllowExpandedClaims = Convert.ToBoolean(dtLimits.Rows[0]["bISALLOWEXPANDEDCLAIMS"]);
                if (dtLimits != null && dtLimits.Rows.Count > 0)//&& Convert.ToBoolean(dtLimits.Rows[0]["bISALLOWEXPANDEDCLAIMS"]) == true 
                {
                    nClaimLines = Convert.ToInt32(dtLimits.Rows[0]["ServiceLineNo"]);
                    nDiagnosis = Convert.ToInt32(dtLimits.Rows[0]["DiagnosisNo"]);

                }
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (oDB != null) { oDB.Dispose(); }
                if (oDBParameters != null) { oDBParameters.Dispose(); }
            }
        }
  

        //This method is not used.
        public bool ValidateExpandedClaimLimits(ArrayList _MastTrans, ArrayList _SelectedTrans, Int64 nClinicID, bool _IsDefault,Boolean IsUB04)
        {
            // _IsDefault flag is used to determine the method is used for sending the claim or Viewing the claim. 
            bool bAllowExpandedClaims = false;
            int nClaimLines=6;
            int nDiagnosis=4;          
           // Transaction oTransaction = new Transaction();           
            DataTable dtDxCount = null;
            bool _result = true;
            string _Message = String.Empty;
            int nSettingType =2;
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = null;
            DataTable dtTran = null;
            try
            {
                //if (_SelectedTrans != null && _SelectedTrans.Count > 0)
                //{                                     

                for (int _Count = 0; _Count < _SelectedTrans.Count; _Count++)
                {
                    nSettingType = 2;
                    //oTransaction = null;
                    //oTransaction = GetChargesClaimDetails(Convert.ToInt64(_SelectedTrans[_Count]), nClinicID);
                    oParameters = new gloDatabaseLayer.DBParameters(); ;
                    oDB.Connect(false);
                    oParameters.Add("@nTransactionID", Convert.ToInt64(_SelectedTrans[_Count]), ParameterDirection.Input, SqlDbType.BigInt);                    
                    oDB.Retrive("BL_CMS_Contact", oParameters, out dtTran);
                    oDB.Disconnect();
                    
                   
                   

                    if (IsUB04 == true)
                    {
                        //nSettingType = GetBillingType(Convert.ToInt64(_MastTrans[_Count]), Convert.ToInt64(_SelectedTrans[_Count]));
                        if (dtTran != null && dtTran.Rows.Count > 0)
                        {
                            nSettingType = Convert.ToInt32(dtTran.Rows[0]["SettingType"]);
                        }
                        if (nSettingType == 1 || nSettingType == 0)
                        {
                            nSettingType = 2;
                        }
                        else
                        {
                            nSettingType = 4;
                        }
                    }
                    if (dtTran != null && dtTran.Rows.Count > 0)
                    {
                        GetExpandedClaimLimits(Convert.ToInt64(dtTran.Rows[0]["ContactId"]), nSettingType, nClinicID, out bAllowExpandedClaims, out nClaimLines, out nDiagnosis);
                    }
                    if (nSettingType == 2)//Convert.ToInt16(gloSettings.TypeOfBilling.Paper.GetHashCode()))
                    {
                        dtDxCount = GetTransaction_DX_Professional(Convert.ToInt64(_MastTrans[_Count]), Convert.ToInt64(_SelectedTrans[_Count]));
                        Int16 _nBillingMethodID = 0;
                        string _sqlquery = "Select nMasterTransactionID,nClaimno,nBillingMethodID from BL_CMSEDI_Claim_Send" +
                                                     " INNER JOIN dbo.BL_Transaction_Batch ON  BL_CMSEDI_Claim_Send.nBatchID=dbo.BL_Transaction_Batch.nBatchID  where nTransactionId=" + Convert.ToInt64(_SelectedTrans[_Count]) + "";
                        oDB.Connect(false);
                        DataTable dt = new DataTable();
                        oDB.Retrive_Query(_sqlquery, out dt);
                        oDB.Disconnect();


                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (Convert.ToString(dt.Rows[0]["nBillingMethodID"]) != "")
                            {
                                _nBillingMethodID = Convert.ToInt16(dt.Rows[0]["nBillingMethodID"]);
                            }
                        }
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }

                        if (_IsDefault == true && _nBillingMethodID == (int)gloSettings.BatchBillingMethod.CMS1500.GetHashCode())
                        {
                            nDiagnosis = 4;
                            nClaimLines = 6;
                        }
                        else if (_nBillingMethodID == (int)gloSettings.BatchBillingMethod.CMS1500New.GetHashCode())
                        {
                            nDiagnosis = 12;
                            nClaimLines = 6;
                        }
                    }
                    else if (nSettingType == 4)//Convert.ToInt16(gloSettings.TypeOfBilling.UB04Paper.GetHashCode()))
                    {
                        dtDxCount = GetTransaction_DX(Convert.ToInt64(_MastTrans[_Count]), Convert.ToInt64(_SelectedTrans[_Count]));
                        if (_IsDefault == true)
                        {
                            nDiagnosis = 18;
                            nClaimLines = 22;
                        }
                    }
                    if (dtDxCount != null && dtDxCount.Rows.Count > nDiagnosis)
                    {
                        _result = false;
                        _Message = "Claim exceeds the limit of # of diagnosis. ";
                        //break;
                    }
                    if (dtTran != null && dtTran.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dtTran.Rows[0]["LineCount"]) > 0 && Convert.ToInt32(dtTran.Rows[0]["LineCount"]) > nClaimLines)
                        {
                            _result = false;
                            if (_Message != "")
                            {
                                _Message.Replace("diagnosis.", "diagnosis and charges.");
                            }
                            else
                            {
                                _Message = "Claim exceeds the limit of # of charges. ";
                            }
                            //break;
                        }
                    }

                    if (_result == false)
                    {
                        DataTable _dtResult = getPatientDetails(Convert.ToInt64(_SelectedTrans[_Count]));
                        string _sPatientName = String.Empty;
                        string _sPatientCode = String.Empty;
                        string _sClaimNumber = String.Empty;
                        if (_dtResult != null & _dtResult.Rows.Count > 0)
                        {
                            _sPatientName = Convert.ToString(_dtResult.Rows[0]["sPatientName"]);
                            _sPatientCode = Convert.ToString(_dtResult.Rows[0]["sPatientCode"]);
                            _sClaimNumber = Convert.ToString(_dtResult.Rows[0]["sClaimNumber"]);
                            
                        }
                        if (_dtResult != null)
                        {
                            _dtResult.Dispose();
                            _dtResult = null;
                        }
                        MessageBox.Show("Batch could not be printed.\nClaim " + _sClaimNumber + " for patient " + _sPatientCode + " – " + _sPatientName + " is too large to print to paper.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    if (dtDxCount != null)
                    {
                        dtDxCount.Dispose();
                        dtDxCount = null;
                    }
                    if (dtTran != null) 
                    { 
                        dtTran.Dispose(); 
                        dtTran = null; 
                    }
                    if (oParameters != null)
                    {
                        oParameters.Dispose();
                        oParameters = null;
                    }
                }
                //}
            }
            catch (gloDatabaseLayer.DBException dbEx)
            {
                dbEx.ERROR_Log(dbEx.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
            }

            finally
            {
                if (dtTran != null) { dtTran.Dispose(); }
                if (oDB != null) { oDB.Dispose(); }
                //if (oTransaction != null) { oTransaction.Dispose(); }
            }
            return _result;
        }


        public  DataTable getPatientDetails(Int64 _TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            String _strSQL = "";
            DataTable dtPatients = new DataTable();
           // bool _result = false;

            try
            {
                oDB.Connect(false);
                _strSQL = " SELECT     BL_Transaction_Claim_MST.nTransactionID, ISNULL(Patient.sPatientCode,'') as sPatientCode,  " +
                          " ISNULL(Patient.sFirstName,'') +' '+ ISNULL(Patient.sMiddleName,'') + ' '+ ISNUll(Patient.sLastName,'') as sPatientName, " +
                          " dbo.getSubClaimNumber(BL_Transaction_Claim_MST.nClaimNo,BL_Transaction_Claim_MST.nSubClaimNo, BL_Transaction_Claim_MST.sMainClaimNo,5) as sClaimNumber " +
                          " FROM       BL_Transaction_Claim_MST WITH(NOLOCK)  Left Outer JOIN " +
                          " Patient WITH(NOLOCK)  ON Patient.nPatientID = BL_Transaction_Claim_MST.nPatientID " +
                          " WHERE     (BL_Transaction_Claim_MST.nTransactionID = " + _TransactionID + ") ";
                oDB.Retrive_Query(_strSQL, out dtPatients);
                oDB.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Dispose();
                }
            }
            return dtPatients;
        }

        public DataTable GetTransaction_DX(Int64 MasterTransactionID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();         
            DataTable dtDX = new DataTable();
            try
            {
                oDB.Connect(false);
                oParameters.Add("@nMasterTransactionid", MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nTransactionid", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                oDB.Retrive("BL_Select_Transaction_DX", oParameters, out dtDX);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return dtDX;
        }

        public DataTable GetTransaction_DX_Professional(Int64 MasterTransactionID, Int64 TransactionID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            //string strSQL = "";
            DataTable dtDX = new DataTable();
            try
            {
                //oDB.Connect(false);
                //oParameters.Add("@nMasterTransactionid", MasterTransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                //oParameters.Add("@nTransactionid", TransactionID, ParameterDirection.Input, SqlDbType.BigInt);
                //oDB.Retrive("EDI837_GetDistinctDiagnosis", oParameters, out dtDX);
                dtDX = GetDistinctDiagnosis(TransactionID, _ClinicID, 0);
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }

            return dtDX;
        }

        private DataTable GetDistinctDiagnosis(Int64 TransactionID, Int64 ClinicID, Int64 ClaimNo)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string strSQL = "";
            DataTable dtAllDx = new DataTable();
            DataTable dtClaimDx = new DataTable();
            dtClaimDx.Columns.Add("DX");

            try
            {
                oDB.Connect(false);

                strSQL = "Select ISNULL(sDx1Code,'') AS sDx1Code,ISNULL(sDx2Code,'') AS sDx2Code, " +
                " ISNULL(sDx3Code,'') AS sDx3Code,ISNULL(sDx4Code,'') AS sDx4Code,ISNULL(sLinePrimaryDxCode,'') AS sLinePrimaryDxCode, " +
                " ISNULL(ntransactionlineno,0) AS ntransactionlineno " +
                " from BL_Transaction_Claim_Lines WITH(NOLOCK)  WHERE (nTransactionID = " + TransactionID + ") AND (nClinicID = " + ClinicID + ") " +
                " order by ntransactionlineno";

                oDB.Retrive_Query(strSQL, out dtAllDx);
                DataRow dr;
                ArrayList _claimDx = new ArrayList();
                string _tempDxCode = "";

                if (dtAllDx != null && dtAllDx.Rows.Count > 0)
                {
                    for (int i = 0; i < dtAllDx.Rows.Count; i++)
                    {

                        //...Line 1 Primary Diagnosis
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sLinePrimaryDxCode"]).Trim().ToUpper();
                        if (Convert.ToInt32(dtAllDx.Rows[i]["ntransactionlineno"]) == 1 && _tempDxCode != "")
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx1
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx1Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx2
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx2Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx3
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx3Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                        //..... Line Dx4
                        _tempDxCode = "";
                        _tempDxCode = Convert.ToString(dtAllDx.Rows[i]["sDx4Code"]).Trim().ToUpper();
                        if (_tempDxCode != "" && _claimDx.Contains(_tempDxCode) == false)
                        { _claimDx.Add(_tempDxCode); }

                    }

                    if (_claimDx != null && _claimDx.Count > 0)
                    {
                        for (int DxIndex = 0; DxIndex < _claimDx.Count; DxIndex++)
                        {
                            dr = dtClaimDx.NewRow();
                            dr["DX"] = _claimDx[DxIndex].ToString();
                            dtClaimDx.Rows.Add(dr);
                            dr = null;
                        }

                        if (dtClaimDx != null)
                        { return dtClaimDx; }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return null;
            }
            finally
            {
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                }
            }
        }
        public string GetFontSetupSettingformCmsAndUB(string sSettingName)
        {
            GeneralSettings ogloSettings = new GeneralSettings(_databaseconnectionstring);
            object value = new object();
            try
            {
                ogloSettings.GetSetting(sSettingName, out value);
                if (value != null && Convert.ToString(value).Trim() != "")
                {
                    return Convert.ToString(value);
                   // value = null;
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.Message, true);
                return "";
            }
            finally
            {
                ogloSettings.Dispose();
                ogloSettings = null;
                value = null;
            }
        }
      

    }
}
