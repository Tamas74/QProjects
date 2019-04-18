using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
namespace gloRxHub
{

    public class clsEligibilityCheckDBLayer : IDisposable
    {
       

        private ClsPatient oPatient;
        //ClsPharmacy oPharmacy = new ClsPharmacy();
        //ClsRxH_271Master oRxh271Master = new ClsRxH_271Master();
        //ClsDrug oclsDrug = new ClsDrug();

        public ClsPatient Patient
        {
            get 
            {
                return oPatient; 
            }
            set 
            {
                oPatient = value;
            }
        }
	
        private bool disposedValue = false;
        // To detect redundant calls 

        // IDisposable 
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                }
                // TODO: free managed resources when explicitly called 
            }

            // TODO: free shared unmanaged resources 
            this.disposedValue = true;
        }

        #region " IDisposable Support "
        // This code added by Visual Basic to correctly implement the disposable pattern. 
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(ByVal disposing As Boolean) above. 
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion 

  



        public bool IsEligibilitygGenerated_validation(Int64 PatientID)
        {
            //string _s270ReqResponse_details = "";
            string strQuery = "";
            System.Data.DataTable dt_Reqtable = null;
            //System.Data.DataTable dt_Resptable = new System.Data.DataTable();
            System.Data.DataTable dt_271Respdettable = null;
            clsgloRxHubDBLayer ogloRxHubDBLayer = new clsgloRxHubDBLayer();
            bool Result = false;
            //bool _ContainPNF = true;
            //// If Result is TRUE then send resquest
            //// IF Result is false then dont send Request
            try
            {
                ogloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString);
                strQuery = "select top 1 *, ISNULL(dt270RequestDateTimeStamp,NULL) AS LastDate ,ISNULL(DATEADD(day,1,dt270RequestDateTimeStamp),NULL) AS AddedDate from RxH_270Request_Details where sPatientCode= '" + PatientID + "' order by dt270RequestDateTimeStamp desc ";
                dt_Reqtable = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                if (dt_Reqtable != null)
                {
                                        
                    if (dt_Reqtable.Rows.Count > 0)
                    {
                        string _271MsgTransactionID = dt_Reqtable.Rows[0]["sTransactionID"].ToString();
                        //compare this  transaction ID in 271_reponse_details table and check the message type column.
                        //if the message type contains message type like "271|PNF" this means that the "patient was not found"
                        //therefor user is eligible to send the 270 request again.
                       // strQuery = "select * from  RxH_271Response_Details where sreference270messageid = '" + _271MsgTransactionID + "'"; //Removed Select *

                        strQuery = "select sMessageType,npatientid  from  RxH_271Response_Details where sreference270messageid = '" + _271MsgTransactionID + "'";
                        dt_271Respdettable = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                        if (dt_271Respdettable != null)
                        {
                            if (dt_271Respdettable.Rows.Count > 0)
                            {
                                for (int i = 0; i <= dt_271Respdettable.Rows.Count - 1; i++)
                                {
                                    string _MessageType = dt_271Respdettable.Rows[i]["sMessageType"].ToString();
                                    if (_MessageType != "")
                                    {
                                        if (_MessageType.Contains("PNF"))
                                        {
                                            //Delete the RxH_270Request_Details/RxH_271Details/RxH_271Master/RxH_271Response_Details table 
                                            //for the entries made against this patient
                                            ogloRxHubDBLayer.DeleteRxH_Table("gsp_DeleteRxHTables", Convert.ToInt64(dt_271Respdettable.Rows[i]["npatientid"]));
                                           // _ContainPNF = true;
                                            Result = true;
                                            return Result;
                                        }
                                        else
                                        {
                                          //  _ContainPNF = false;
                                            Result = ogloRxHubDBLayer.GetReuestDetails(Convert.ToDateTime(dt_Reqtable.Rows[0]["LastDate"]), Convert.ToDateTime(dt_Reqtable.Rows[0]["AddedDate"]));
                                        }
                                    }

                                }
                                dt_271Respdettable.Dispose();
                                dt_271Respdettable = null;
                            }
                            else//this means there is no response returned agains that file or it was a NAK file
                            {
                                Result = true;
                            }
                        }
                        //if (_ContainPNF = true)
                        //{
                        //    //no need to check the 24 hour logic
                        //    Result = true;
                        //}
                        //else//_ContainPNF flag is false meanse that the messagetype variable does not contain any "PNF" string so check the 24 hour logic
                        //{
                        //    Result = ogloRxHubDBLayer.GetReuestDetails(Convert.ToDateTime(dt_Reqtable.Rows[0]["LastDate"]), Convert.ToDateTime(dt_Reqtable.Rows[0]["AddedDate"]));
                        //}

                        
                    }
                    else
                    {
                        Result =  true;
                    }
                    dt_Reqtable.Dispose();
                    dt_Reqtable = null;
                }
                else
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                if (ogloRxHubDBLayer != null)
                {
                    ogloRxHubDBLayer.Disconnect();
                    ogloRxHubDBLayer.Dispose();
                    ogloRxHubDBLayer = null;
                }
            }

            return Result;

        }

        public bool IsEligibilitygGenerated(Int64 PatientID)
        {
            //string _s270ReqResponse_details = "";
            string strQuery = "";
            System.Data.DataTable dt_Reqtable = null;
         //   System.Data.DataTable dt_Resptable = new System.Data.DataTable();
            clsgloRxHubDBLayer ogloRxHubDBLayer = new clsgloRxHubDBLayer();
            bool Result = false;

            //// If Result is TRUE then send resquest
            //// IF Result is false then dont send Request
            try
            {
                ogloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString);
                strQuery = "select top 1 *, ISNULL(dt270RequestDateTimeStamp,NULL) AS LastDate ,ISNULL(DATEADD(day,1,dt270RequestDateTimeStamp),NULL) AS AddedDate from RxH_270Request_Details where sPatientCode= '" + PatientID + "' order by dt270RequestDateTimeStamp desc ";
                dt_Reqtable = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                if (dt_Reqtable != null)
                {
                    if (dt_Reqtable.Rows.Count > 0)
                    {

                        Result = ogloRxHubDBLayer.GetReuestDetails(Convert.ToDateTime(dt_Reqtable.Rows[0]["LastDate"]), Convert.ToDateTime(dt_Reqtable.Rows[0]["AddedDate"]));
                    }
                    else
                    {
                        Result = true;
                    }
                    dt_Reqtable.Dispose();
                    dt_Reqtable = null;
                }
                else
                {
                    Result = true;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return false;
            }
            finally
            {
                if (ogloRxHubDBLayer != null)
                {
                    ogloRxHubDBLayer.Disconnect();
                    ogloRxHubDBLayer.Dispose();
                    ogloRxHubDBLayer = null;
                }
            }

            return Result;

        }
        public DataTable GetEligiblityInformation(Int64 PatientID)
        {

            string strQuery = "";
            DataTable dt_EligiblityInfo = null;
            DataTable dt_datediff = null;
            clsgloRxHubDBLayer ogloRxHubDBLayer = new clsgloRxHubDBLayer();
            try
            {
                //strQuery = " SELECT Top 1 ISNULL(RxH_271Master.sSTLoopControlID,'') AS sSTLoopControlID,ISNULL(RxH_271Master.sPBM_PayerName,'') AS sPBM_PayerName,ISNULL(RxH_271Master.sPBM_PayerParticipantID,'') AS sPBM_PayerParticipantID, ISNULL(RxH_271Master.sPBM_PayerMemberID,'') AS sPBM_PayerMemberID, ISNULL( RxH_271Master.sPhysicianName,'') AS sPhysicianName, ISNULL( RxH_271Master.sPhysicianSuffix,'') as sPhysicianSuffix, ISNULL( RxH_271Master.sHealthPlanNumber,'') as sHealthPlanNumber, "
                //+ "ISNULL(RxH_271Master.sHealthPlanName,'') as sHealthPlanName, ISNULL( RxH_271Master.sRelationshipCode,'') as sRelationshipCode , ISNULL( RxH_271Master.sRelationshipDescription,'' ) as sRelationshipDescription, ISNULL( RxH_271Master.sPersonCode,'') as sPersonCode, ISNULL(RxH_271Master.sCardHolderID,'') as sCardHolderID, ISNULL( RxH_271Master.sGroupID,'') as sGroupID, ISNULL( RxH_271Master.sCardHolderName,'') as sCardHolderName, ISNULL( RxH_271Master.sGroupName,'') as sGroupName, "
                //+ "ISNULL(RxH_271Master.sFormularyListID,'') as sCardHolderName, ISNULL( RxH_271Master.sAlternativeListID,'') as sAlternativeListID, ISNULL( RxH_271Master.sCoverageID,'') as sCoverageID, ISNULL( RxH_271Master.sBINNumber,'') as sBINNumber, ISNULL(RxH_271Master.sCoPayID,'') as sCoPayID, ISNULL( RxH_271Master.sPharmacyEligible,'') as sPharmacyEligible, ISNULL( RxH_271Master.sPharmacyCoverageName,'') as sPharmacyCoverageName, ISNULL(RxH_271Master.sPhEligiblityorBenefitInfo,'') as sPhEligiblityorBenefitInfo, ISNULL( RxH_271Master.sMailOrderRxDrugEligible,'') as sMailOrderRxDrugEligible, ISNULL( RxH_271Master.sMailOrderRxDrugCoverageName,'') as sMailOrderRxDrugCoverageName, "
                //+ "ISNULL(RxH_271Master.sMailOrdEligiblityorBenefitInfo,'') as sMailOrdEligiblityorBenefitInfo, (ISNULL(RxH_271Master.sDFirstName,'') + ' '+ ISNULL(RxH_271Master.sDMiddleName,'') + ' ' + ISNULL( RxH_271Master.sDLastName,'')) AS DependentName, ISNULL(RxH_271Master.sDGender,'') as sDGender, ISNULL( RxH_271Master.sDDOB,'') as sDDOB, ISNULL( RxH_271Master.sDSSN,'') as sDSSN, ISNULL( RxH_271Master.sDAddress1,'') as sDAddress1, ISNULL( RxH_271Master.sDAddress2,'') as sDAddress2, ISNULL(RxH_271Master.sDCity,'') as sDCity, ISNULL( RxH_271Master.sDState,'') as sDState, ISNULL( RxH_271Master.sDZip,'') as sDZip , ISNULL( RxH_271Master.nPatientID,0) as nPatientID, ISNULL( RxH_271Master.dtResquestDateTimeStamp,'') as dtResquestDateTimeStamp, "
                //+ "ISNULL(RxH_271Master.sMessageID,'') as sMessageID,ISNULL(RxH_271Master.IsDependentdemoChange,'false') AS IsDependentdemoChange,ISNULL(RxH_271Master.sDependentdemochgFirstName,'') AS sDependentdemochgFirstName,ISNULL(RxH_271Master.sDependentdemoChgMiddleName,'') AS sDependentdemoChgMiddleName,ISNULL(RxH_271Master.sDependentdemoChgLastName,'') AS sDependentdemoChgLastName,ISNULL(RxH_271Master.sDependentdemoChgGender,'') AS sDependentdemoChgGender,ISNULL(RxH_271Master.sDependentdemoChgDOB,'') AS sDependentdemoChgDOB,ISNULL(RxH_271Master.sDependentdemoChgSSN,'') AS sDependentdemoChgSSN, "
                //+ "ISNULL(RxH_271Master.sDependentdemoChgAddress1,'') AS sDependentdemoChgAddress1,ISNULL(RxH_271Master.sDependentdemoChgAddress2,'') AS sDependentdemoChgAddress2,ISNULL(RxH_271Master.sDependentdemoChgCity,'') AS sDependentdemoChgCity,ISNULL(RxH_271Master.sDependentdemoChgState,'') AS sDependentdemoChgState,ISNULL(RxH_271Master.sDependentdemoChgZip,'') AS sDependentdemoChgZip,(ISNULL(RxH_271Details.sSubscriberFirstName,'') + ' ' + ISNULL( RxH_271Details.sSubscriberMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberLastName,'')) AS SubscriberName, ISNULL( RxH_271Details.sSubscriberGender,'') as sSubscriberGender, ISNULL( RxH_271Details.sSubscriberDOB,'') as sSubscriberDOB, ISNULL( RxH_271Details.sSubscriberSSN,'' ) as sSubscriberSSN,  ISNULL(RxH_271Details.sSubscriberAddress1,'') as sSubscriberAddress1, ISNULL( RxH_271Details.sSubscriberAddress2,'') as sSubscriberAddress2, ISNULL( RxH_271Details.sSubscriberCity,'') as sSubscriberCity, ISNULL( RxH_271Details.sSubscriberState,'') as sSubscriberState, ISNULL(RxH_271Details.sSubscriberZip,'') as sSubscriberZip, "
                //+ "ISNULL(RxH_271Details.IsSubscriberdemoChange,'false') AS IsSubscriberdemoChange,ISNULL(RxH_271Details.sSubscriberDemochgFirstName,'') AS sSubscriberDemochgFirstName,ISNULL(RxH_271Details.sSubscriberDemoChgMiddleName,'') AS sSubscriberDemoChgMiddleName,ISNULL(RxH_271Details.sSubscriberDemoChgLastName,'') AS sSubscriberDemoChgLastName,ISNULL(RxH_271Details.sSubscriberDemoChgGender,'') AS sSubscriberDemoChgGender,ISNULL(RxH_271Details.sSubscriberDemoChgDOB,'') AS sSubscriberDemoChgDOB,ISNULL(RxH_271Details.sSubscriberDemoChgSSN,'') AS sSubscriberDemoChgSSN,ISNULL(RxH_271Details.sSubscriberDemoChgAddress1,'') AS sSubscriberDemoChgAddress1,ISNULL(RxH_271Details.sSubscriberDemoChgAddress2,'') AS sSubscriberDemoChgAddress2, "
                //+ "ISNULL(RxH_271Details.sSubscriberDemoChgCity,'') AS sSubscriberDemoChgCity,ISNULL(RxH_271Details.sSubscriberDemoChgState,'') AS sSubscriberDemoChgState,ISNULL(RxH_271Details.sSubscriberDemoChgZip,'') AS sSubscriberDemoChgZip "
                //+ "FROM RxH_271Master INNER JOIN "
                //+ "RxH_271Details ON RxH_271Master.sMessageID = RxH_271Details.sMessageID WHERE RxH_271Master.nPatientID = " + PatientID + " AND convert(datetime,convert(varchar,dtResquestDateTimeStamp,101)) = '" + DateTime.Now.Date.ToShortDateString() + "'";
                ogloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString);
               strQuery =  "select TOP 1 ISNULL(dbo.gloGetDate(),NULL) AS CurrentDate ,ISNULL(DATEADD(day,-1,dbo.gloGetDate()),NULL) AS SubDate from RxH_270Request_Details";
               dt_datediff = ogloRxHubDBLayer.ReadPatRecord(strQuery);
               if (dt_datediff != null)
               {

                   if (dt_datediff.Rows.Count > 0)
                   {
                       


                      // strQuery = " SELECT  DISTINCT ISNULL(RxH_271Master.sSTLoopControlID,'') AS sSTLoopControlID,ISNULL(RxH_271Master.sPBM_PayerName,'') AS sPBM_PayerName,ISNULL(RxH_271Master.sPBM_PayerParticipantID,'') AS sPBM_PayerParticipantID, ISNULL(RxH_271Master.sPBM_PayerMemberID,'') AS sPBM_PayerMemberID, ISNULL( RxH_271Master.sPhysicianName,'') AS sPhysicianName, ISNULL( RxH_271Master.sPhysicianSuffix,'') as sPhysicianSuffix, ISNULL( RxH_271Master.sHealthPlanNumber,'') as sHealthPlanNumber, "
                      //+ "ISNULL(RxH_271Master.sHealthPlanName,'') as sHealthPlanName, ISNULL( RxH_271Master.sRelationshipCode,'') as sRelationshipCode , ISNULL( RxH_271Master.sRelationshipDescription,'' ) as sRelationshipDescription, ISNULL( RxH_271Master.sPersonCode,'') as sPersonCode, ISNULL(RxH_271Master.sCardHolderID,'') as sCardHolderID, ISNULL( RxH_271Master.sGroupID,'') as sGroupID, ISNULL( RxH_271Master.sCardHolderName,'') as sCardHolderName, ISNULL( RxH_271Master.sGroupName,'') as sGroupName, "
                      //+ "ISNULL(RxH_271Master.sFormularyListID,'') as sFormularyListID, ISNULL( RxH_271Master.sAlternativeListID,'') as sAlternativeListID, ISNULL( RxH_271Master.sCoverageID,'') as sCoverageID, ISNULL( RxH_271Master.sEmployeeID,'') as sEmployeeID, ISNULL( RxH_271Master.sBINNumber,'') as sBINNumber, ISNULL(RxH_271Master.sCoPayID,'') as sCoPayID, ISNULL( RxH_271Master.sPharmacyEligible,'') as sPharmacyEligible, ISNULL( RxH_271Master.sPharmacyCoverageName,'') as sPharmacyCoverageName, ISNULL(RxH_271Master.sPhEligiblityorBenefitInfo,'') as sPhEligiblityorBenefitInfo, ISNULL( RxH_271Master.sMailOrderRxDrugEligible,'') as sMailOrderRxDrugEligible, ISNULL( RxH_271Master.sMailOrderRxDrugCoverageName,'') as sMailOrderRxDrugCoverageName, "
                      //+ "ISNULL(RxH_271Master.sMailOrdEligiblityorBenefitInfo,'') as sMailOrdEligiblityorBenefitInfo, ISNULL(RxH_271Master.sMailOrderInsTypeCode,'') as sMailOrderInsTypeCode,ISNULL(RxH_271Master.sRetailInsTypeCode,'') as sRetailInsTypeCode, ISNULL( RxH_271Master.sEligiblityDate,'') as sEligiblityDate,ISNULL( RxH_271Master.sServiceDate,'') as sServiceDate, ISNULL(RxH_271Master.sMailOrderMonetaryAmount,'') as sMailOrderMonetaryAmount, ISNULL(RxH_271Master.sRetailMonetaryAmount,'') as sRetailMonetaryAmount," 
                      //+ "(ISNULL(RxH_271Master.sDFirstName,'') + ' '+ ISNULL(RxH_271Master.sDMiddleName,'') + ' ' + ISNULL( RxH_271Master.sDLastName,'')) AS DependentName, ISNULL(RxH_271Master.sDGender,'') as sDGender, ISNULL( RxH_271Master.sDDOB,'') as sDDOB, ISNULL( RxH_271Master.sDSSN,'') as sDSSN, ISNULL( RxH_271Master.sDAddress1,'') as sDAddress1, ISNULL( RxH_271Master.sDAddress2,'') as sDAddress2, ISNULL(RxH_271Master.sDCity,'') as sDCity, ISNULL( RxH_271Master.sDState,'') as sDState, ISNULL( RxH_271Master.sDZip,'') as sDZip , ISNULL( RxH_271Master.nPatientID,0) as nPatientID, ISNULL( RxH_271Master.dtResquestDateTimeStamp,'') as dtResquestDateTimeStamp, "
                      //+ "ISNULL(RxH_271Master.sMessageID,'') as sMessageID,ISNULL(RxH_271Master.IsDependentdemoChange,'false') AS IsDependentdemoChange,(ISNULL(RxH_271Master.sDependentdemochgFirstName,'') + ' ' + ISNULL(RxH_271Master.sDependentdemoChgMiddleName,'') + ' ' + ISNULL(RxH_271Master.sDependentdemoChgLastName,'')) AS DependentdemoChgName,ISNULL(RxH_271Master.sDependentdemoChgGender,'') AS sDependentdemoChgGender,ISNULL(RxH_271Master.sDependentdemoChgDOB,'') AS sDependentdemoChgDOB,ISNULL(RxH_271Master.sDependentdemoChgSSN,'') AS sDependentdemoChgSSN, "
                      //+ "ISNULL(RxH_271Master.sDependentdemoChgAddress1,'') AS sDependentdemoChgAddress1,ISNULL(RxH_271Master.sDependentdemoChgAddress2,'') AS sDependentdemoChgAddress2,ISNULL(RxH_271Master.sDependentdemoChgCity,'') AS sDependentdemoChgCity,ISNULL(RxH_271Master.sDependentdemoChgState,'') AS sDependentdemoChgState,ISNULL(RxH_271Master.sDependentdemoChgZip,'') AS sDependentdemoChgZip,(ISNULL(RxH_271Details.sSubscriberFirstName,'') + ' ' + ISNULL( RxH_271Details.sSubscriberMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberLastName,'')) AS SubscriberName, ISNULL(RxH_271Details.sSubscriberSuffix,'') as SubscriberSuffix,ISNULL( RxH_271Details.sSubscriberGender,'') as sSubscriberGender, ISNULL( RxH_271Details.sSubscriberDOB,'') as sSubscriberDOB, ISNULL( RxH_271Details.sSubscriberSSN,'' ) as sSubscriberSSN,  ISNULL(RxH_271Details.sSubscriberAddress1,'') as sSubscriberAddress1, ISNULL( RxH_271Details.sSubscriberAddress2,'') as sSubscriberAddress2, ISNULL( RxH_271Details.sSubscriberCity,'') as sSubscriberCity, ISNULL( RxH_271Details.sSubscriberState,'') as sSubscriberState, ISNULL(RxH_271Details.sSubscriberZip,'') as sSubscriberZip, "
                      //+ "ISNULL(RxH_271Details.IsSubscriberdemoChange,'false') AS IsSubscriberdemoChange,(ISNULL(RxH_271Details.sSubscriberDemochgFirstName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberDemoChgMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberDemoChgLastName,'')) AS SubscriberDemoChgName,ISNULL(RxH_271Details.sSubscriberDemoChgGender,'') AS sSubscriberDemoChgGender,ISNULL(RxH_271Details.sSubscriberDemoChgDOB,'') AS sSubscriberDemoChgDOB,ISNULL(RxH_271Details.sSubscriberDemoChgSSN,'') AS sSubscriberDemoChgSSN,ISNULL(RxH_271Details.sSubscriberDemoChgAddress1,'') AS sSubscriberDemoChgAddress1,ISNULL(RxH_271Details.sSubscriberDemoChgAddress2,'') AS sSubscriberDemoChgAddress2, "
                      //+ "ISNULL(RxH_271Details.sSubscriberDemoChgCity,'') AS sSubscriberDemoChgCity,ISNULL(RxH_271Details.sSubscriberDemoChgState,'') AS sSubscriberDemoChgState,ISNULL(RxH_271Details.sSubscriberDemoChgZip,'') AS sSubscriberDemoChgZip, "
                      //+ "ISNULL(RxH_271Master.sIsContractedProvider,'') AS sIsContractedProvider,ISNULL(RxH_271Master.sContractedProviderName,'') AS sContractedProviderName,ISNULL(RxH_271Master.sContractedProviderNumber,'') AS sContractedProviderNumber,ISNULL(RxH_271Master.sContProvMailOrderEligible,'') AS	sContProvMailOrderEligible,ISNULL(RxH_271Master.sContProvMailOrderCoverageInfo,'') AS	sContProvMailOrderCoverageInfo,ISNULL(RxH_271Master.sContProvMailOrderInsTypeCode,'') AS sContProvMailOrderInsTypeCode,ISNULL(RxH_271Master.sContProvMailOrderMonetaryAmt,'') AS sContProvMailOrderMonetaryAmt,ISNULL(RxH_271Master.sContProvRetailsEligible,'') AS sContProvRetailsEligible,ISNULL(RxH_271Master.sContProvRetailCoverageInfo,'') AS sContProvRetailCoverageInfo,	ISNULL(RxH_271Master.sContProvRetailInsTypeCode,'') AS	sContProvRetailInsTypeCode,ISNULL(RxH_271Master.sContProvRetailMonetaryAmt,'') AS sContProvRetailMonetaryAmt,ISNULL(RxH_271Master.sIsPrimaryPayer,'') AS sIsPrimaryPayer,ISNULL(RxH_271Master.sPrimaryPayerName,'') AS sPrimaryPayerName,	ISNULL(RxH_271Master.sPrimaryPayerNumber,'') AS sPrimaryPayerNumber, "
                      //+ "ISNULL(RxH_271Master.sPrimaryPayerMailOrderEligible,'') AS	sPrimaryPayerMailOrderEligible,ISNULL(RxH_271Master.sPrimaryPayerMailOrderCoverageInfo,'') AS	sPrimaryPayerMailOrderCoverageInfo,ISNULL(RxH_271Master.sPrimaryPayerMailOrderInsTypeCode	,'') AS	sPrimaryPayerMailOrderInsTypeCode,ISNULL(RxH_271Master.sPrimaryPayerMailOrderMonetaryAmt,'') AS sPrimaryPayerMailOrderMonetaryAmt,	ISNULL(RxH_271Master.sPrimaryPayerRetailsEligible,'') AS sPrimaryPayerRetailsEligible,ISNULL(RxH_271Master.sPrimaryPayerRetailCoverageInfo,'') AS sPrimaryPayerRetailCoverageInfo,		ISNULL(RxH_271Master.sPrimaryPayerRetailInsTypeCode,'') AS	sPrimaryPayerRetailInsTypeCode,ISNULL(RxH_271Master.sPrimaryPayerRetailMonetaryAmt,'') AS	sPrimaryPayerRetailMonetaryAmt "	
                      //+ "FROM RxH_271Master INNER JOIN "
                      //+ "RxH_271Details ON RxH_271Master.sMessageID = RxH_271Details.sMessageID AND RxH_271Details.sSTLoopControlID = RxH_271Master.sSTLoopControlID WHERE RxH_271Master.nPatientID = " + PatientID + " "
                      //+ " AND dtResquestDateTimeStamp between  '" + dt_datediff.Rows[0]["SubDate"] + "' and '" + dt_datediff.Rows[0]["CurrentDate"] + "' Order by dtResquestDateTimeStamp desc";
                      ////+ "AND convert(datetime,convert(varchar,dtResquestDateTimeStamp,101)) = '" + DateTime.Now.ToShortDateString() + "' Order by dtResquestDateTimeStamp desc";

                       strQuery = " SELECT  DISTINCT ISNULL(RxH_271Master.sSTLoopControlID,'') AS sSTLoopControlID,ISNULL(RxH_271Master.sPBM_PayerName,'') AS sPBM_PayerName,ISNULL(RxH_271Master.sPBM_PayerParticipantID,'') AS sPBM_PayerParticipantID, ISNULL(RxH_271Master.sPBM_PayerMemberID,'') AS sPBM_PayerMemberID, ISNULL( RxH_271Master.sPhysicianName,'') AS sPhysicianName, ISNULL( RxH_271Master.sPhysicianSuffix,'') as sPhysicianSuffix, ISNULL( RxH_271Master.sHealthPlanNumber,'') as sHealthPlanNumber, "
                       + " ISNULL(RxH_271Master.sHealthPlanName,'') as sHealthPlanName, ISNULL( RxH_271Master.sRelationshipCode,'') as sRelationshipCode , ISNULL( RxH_271Master.sRelationshipDescription,'' ) as sRelationshipDescription, ISNULL( RxH_271Master.sPersonCode,'') as sPersonCode, ISNULL(RxH_271Master.sCardHolderID,'') as sCardHolderID, ISNULL( RxH_271Master.sGroupID,'') as sGroupID, ISNULL( RxH_271Master.sCardHolderName,'') as sCardHolderName, ISNULL( RxH_271Master.sGroupName,'') as sGroupName, "
                       + " ISNULL(RxH_271Master.sFormularyListID,'') as sFormularyListID, ISNULL( RxH_271Master.sAlternativeListID,'') as sAlternativeListID, ISNULL( RxH_271Master.sCoverageID,'') as sCoverageID, ISNULL( RxH_271Master.sEmployeeID,'') as sEmployeeID, ISNULL( RxH_271Master.sBINNumber,'') as sBINNumber, ISNULL(RxH_271Master.sCoPayID,'') as sCoPayID, ISNULL( RxH_271Master.sPharmacyEligible,'') as sPharmacyEligible, ISNULL( RxH_271Master.sPharmacyCoverageName,'') as sPharmacyCoverageName, ISNULL(RxH_271Master.sPhEligiblityorBenefitInfo,'') as sPhEligiblityorBenefitInfo, ISNULL( RxH_271Master.sMailOrderRxDrugEligible,'') as sMailOrderRxDrugEligible, ISNULL( RxH_271Master.sMailOrderRxDrugCoverageName,'') as sMailOrderRxDrugCoverageName, "
                       + " ISNULL(RxH_271Master.sMailOrdEligiblityorBenefitInfo,'') as sMailOrdEligiblityorBenefitInfo, ISNULL(RxH_271Master.sMailOrderInsTypeCode,'') as sMailOrderInsTypeCode,ISNULL(RxH_271Master.sRetailInsTypeCode,'') as sRetailInsTypeCode, ISNULL( RxH_271Master.sEligiblityDate,'') as sEligiblityDate,ISNULL( RxH_271Master.sServiceDate,'') as sServiceDate, ISNULL(RxH_271Master.sMailOrderMonetaryAmount,'') as sMailOrderMonetaryAmount, ISNULL(RxH_271Master.sRetailMonetaryAmount,'') as sRetailMonetaryAmount,"
                       + " (ISNULL(RxH_271Master.sDFirstName,'') + ' '+ ISNULL(RxH_271Master.sDMiddleName,'') + ' ' + ISNULL( RxH_271Master.sDLastName,'')) AS DependentName, ISNULL(RxH_271Master.sDGender,'') as sDGender, ISNULL( RxH_271Master.sDDOB,'') as sDDOB, ISNULL( RxH_271Master.sDSSN,'') as sDSSN, ISNULL( RxH_271Master.sDAddress1,'') as sDAddress1, ISNULL( RxH_271Master.sDAddress2,'') as sDAddress2, ISNULL(RxH_271Master.sDCity,'') as sDCity, ISNULL( RxH_271Master.sDState,'') as sDState, ISNULL( RxH_271Master.sDZip,'') as sDZip , ISNULL( RxH_271Master.nPatientID,0) as nPatientID, ISNULL( RxH_271Master.dtResquestDateTimeStamp,'') as dtResquestDateTimeStamp, "
                       + " ISNULL(RxH_271Master.sMessageID,'') as sMessageID,ISNULL(RxH_271Master.IsDependentdemoChange,'false') AS IsDependentdemoChange,(ISNULL(RxH_271Master.sDependentdemochgFirstName,'') + ' ' + ISNULL(RxH_271Master.sDependentdemoChgMiddleName,'') + ' ' + ISNULL(RxH_271Master.sDependentdemoChgLastName,'')) AS DependentdemoChgName,ISNULL(RxH_271Master.sDependentdemoChgGender,'') AS sDependentdemoChgGender,ISNULL(RxH_271Master.sDependentdemoChgDOB,'') AS sDependentdemoChgDOB,ISNULL(RxH_271Master.sDependentdemoChgSSN,'') AS sDependentdemoChgSSN, "
                       + " ISNULL(RxH_271Master.sDependentdemoChgAddress1,'') AS sDependentdemoChgAddress1,ISNULL(RxH_271Master.sDependentdemoChgAddress2,'') AS sDependentdemoChgAddress2,ISNULL(RxH_271Master.sDependentdemoChgCity,'') AS sDependentdemoChgCity,ISNULL(RxH_271Master.sDependentdemoChgState,'') AS sDependentdemoChgState,ISNULL(RxH_271Master.sDependentdemoChgZip,'') AS sDependentdemoChgZip,(ISNULL(RxH_271Details.sSubscriberFirstName,'') + ' ' + ISNULL( RxH_271Details.sSubscriberMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberLastName,'')) AS SubscriberName, ISNULL(RxH_271Details.sSubscriberSuffix,'') as SubscriberSuffix,ISNULL( RxH_271Details.sSubscriberGender,'') as sSubscriberGender, ISNULL( RxH_271Details.sSubscriberDOB,'') as sSubscriberDOB, ISNULL( RxH_271Details.sSubscriberSSN,'' ) as sSubscriberSSN,  ISNULL(RxH_271Details.sSubscriberAddress1,'') as sSubscriberAddress1, ISNULL( RxH_271Details.sSubscriberAddress2,'') as sSubscriberAddress2, ISNULL( RxH_271Details.sSubscriberCity,'') as sSubscriberCity, ISNULL( RxH_271Details.sSubscriberState,'') as sSubscriberState, ISNULL(RxH_271Details.sSubscriberZip,'') as sSubscriberZip, "
                       + " ISNULL(RxH_271Details.IsSubscriberdemoChange,'false') AS IsSubscriberdemoChange,(ISNULL(RxH_271Details.sSubscriberDemochgFirstName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberDemoChgMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberDemoChgLastName,'')) AS SubscriberDemoChgName,ISNULL(RxH_271Details.sSubscriberDemoChgGender,'') AS sSubscriberDemoChgGender,ISNULL(RxH_271Details.sSubscriberDemoChgDOB,'') AS sSubscriberDemoChgDOB,ISNULL(RxH_271Details.sSubscriberDemoChgSSN,'') AS sSubscriberDemoChgSSN,ISNULL(RxH_271Details.sSubscriberDemoChgAddress1,'') AS sSubscriberDemoChgAddress1,ISNULL(RxH_271Details.sSubscriberDemoChgAddress2,'') AS sSubscriberDemoChgAddress2, "
                       + " ISNULL(RxH_271Details.sSubscriberDemoChgCity,'') AS sSubscriberDemoChgCity,ISNULL(RxH_271Details.sSubscriberDemoChgState,'') AS sSubscriberDemoChgState,ISNULL(RxH_271Details.sSubscriberDemoChgZip,'') AS sSubscriberDemoChgZip, "
                       + " ISNULL(RxH_271Master.sIsContractedProvider,'') AS sIsContractedProvider,ISNULL(RxH_271Master.sContractedProviderName,'') AS sContractedProviderName,ISNULL(RxH_271Master.sContractedProviderNumber,'') AS sContractedProviderNumber,ISNULL(RxH_271Master.sContProvMailOrderEligible,'') AS	sContProvMailOrderEligible,ISNULL(RxH_271Master.sContProvMailOrderCoverageInfo,'') AS	sContProvMailOrderCoverageInfo,ISNULL(RxH_271Master.sContProvMailOrderInsTypeCode,'') AS sContProvMailOrderInsTypeCode,ISNULL(RxH_271Master.sContProvMailOrderMonetaryAmt,'') AS sContProvMailOrderMonetaryAmt,ISNULL(RxH_271Master.sContProvRetailsEligible,'') AS sContProvRetailsEligible,ISNULL(RxH_271Master.sContProvRetailCoverageInfo,'') AS sContProvRetailCoverageInfo,	ISNULL(RxH_271Master.sContProvRetailInsTypeCode,'') AS	sContProvRetailInsTypeCode,ISNULL(RxH_271Master.sContProvRetailMonetaryAmt,'') AS sContProvRetailMonetaryAmt,ISNULL(RxH_271Master.sIsPrimaryPayer,'') AS sIsPrimaryPayer,ISNULL(RxH_271Master.sPrimaryPayerName,'') AS sPrimaryPayerName,	ISNULL(RxH_271Master.sPrimaryPayerNumber,'') AS sPrimaryPayerNumber, "
                       + " ISNULL(RxH_271Master.sPrimaryPayerMailOrderEligible,'') AS	sPrimaryPayerMailOrderEligible,ISNULL(RxH_271Master.sPrimaryPayerMailOrderCoverageInfo,'') AS	sPrimaryPayerMailOrderCoverageInfo,ISNULL(RxH_271Master.sPrimaryPayerMailOrderInsTypeCode	,'') AS	sPrimaryPayerMailOrderInsTypeCode,ISNULL(RxH_271Master.sPrimaryPayerMailOrderMonetaryAmt,'') AS sPrimaryPayerMailOrderMonetaryAmt,	ISNULL(RxH_271Master.sPrimaryPayerRetailsEligible,'') AS sPrimaryPayerRetailsEligible,ISNULL(RxH_271Master.sPrimaryPayerRetailCoverageInfo,'') AS sPrimaryPayerRetailCoverageInfo,		ISNULL(RxH_271Master.sPrimaryPayerRetailInsTypeCode,'') AS	sPrimaryPayerRetailInsTypeCode,ISNULL(RxH_271Master.sPrimaryPayerRetailMonetaryAmt,'') AS	sPrimaryPayerRetailMonetaryAmt , isnull(RxH_271Response_Details.sMessageType,'') as sMessageType, isnull(RxH_271Response_Details.sPBM_PayerParticipantID,'') AS Resp271_PBMPArticipantID "
                       + " FROM RxH_271Master INNER JOIN "
                       + " RxH_271Details ON RxH_271Master.sMessageID = RxH_271Details.sMessageID AND RxH_271Details.sSTLoopControlID = RxH_271Master.sSTLoopControlID INNER JOIN "
                       + " RxH_271Response_Details ON RxH_271Master.sPBM_PayerParticipantID = RxH_271Response_Details.sPBM_PayerParticipantID and RxH_271Master.sMessageID = RxH_271Response_Details.sReference270MessageID"
                       + " WHERE RxH_271Master.nPatientID = " + PatientID + " "
                       + " AND dtResquestDateTimeStamp between  '" + dt_datediff.Rows[0]["SubDate"] + "' and '" + dt_datediff.Rows[0]["CurrentDate"] + "' Order by dtResquestDateTimeStamp desc";
                       //+ "AND convert(datetime,convert(varchar,dtResquestDateTimeStamp,101)) = '" + DateTime.Now.ToShortDateString() + "' Order by dtResquestDateTimeStamp desc";
                       
                       //dt_EligiblityInfo = ogloRxHubDBLayer.ReadPatRecord(strQuery);

                       dt_EligiblityInfo = ogloRxHubDBLayer.Get271ResponseDetails(PatientID, Convert.ToDateTime(dt_datediff.Rows[0]["CurrentDate"]), Convert.ToDateTime(dt_datediff.Rows[0]["SubDate"]));
                   }
                   dt_datediff.Dispose();
                   dt_datediff = null;
               }
                
                
                return dt_EligiblityInfo;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return dt_EligiblityInfo;
            }
            finally
            {
                if (ogloRxHubDBLayer != null)
                {
                    ogloRxHubDBLayer.Disconnect();
                    ogloRxHubDBLayer.Dispose();
                    ogloRxHubDBLayer = null;
                }
            }

        }

        public DataTable Get271Information(Int64 PatientID)
        {

            string strQuery = "";
            DataTable dt_271Info = null;

            clsgloRxHubDBLayer ogloRxHubDBLayer = new clsgloRxHubDBLayer();
            try
            {


                //strQuery = "select * from RxH_271Master where nPatientID =  " + PatientID + " AND sMessageID = (select MAX(sMessageID) from RxH_271Master where nPatientID = " + PatientID + ")  ";

                // strQuery = "select * from RxH_271Master where nPatientID =  " + PatientID + " AND sMessageID = (select MAX(sMessageID) from RxH_271Master where nPatientID = " + PatientID + ")  ";
                strQuery = " select ISNULL(sCoverageID,'') as sCoverageID ,ISNULL(sHealthPlanBenefitCoverageName,'')as sHealthPlanBenefitCoverageName,ISNULL(sPBM_PayerName,'') as sPBM_PayerName,ISNULL(sRetailPhEligiblityorBenefitInfo,'') as sRetailPhEligiblityorBenefitInfo,ISNULL(sMailOrdEligiblityorBenefitInfo,'')as sMailOrdEligiblityorBenefitInfo, " +
                         " ISNULL(sPBM_PayerMemberID,'') as sPBM_PayerMemberID,ISNULL(sPBM_PayerParticipantID,'')as sPBM_PayerParticipantID,ISNULL(sFormularyListID,'')as sFormularyListID,ISNUll(sCoverageID,'')as sCoverageID,ISNULL(sCoPayID,'') as sCoPayID,ISNULL(sAlternativeListID,'') as sAlternativeListID " +
                         ",Isnull(sBinNumber, '') AS BinNumber ,Isnull(sPBM_PayerName, '') AS PayerName ,Isnull(sCardHolderID, '') AS CardHolderID ,Isnull(sGroupID, '') AS GroupID  "+
                         " from RxH_271Master where nPatientID =  " + PatientID + " AND sMessageID = " +
                         "(select MAX(sMessageID) from RxH_271Master where nPatientID = " + PatientID + "  AND dtResquestDateTimeStamp=" +
                         "(select MAX(dtResquestDateTimeStamp) from RxH_271Master where nPatientID = " + PatientID + ")) ";
                //strQuery = "select * from RxH_271Master where nPatientID =  " + PatientID + " AND sMessageID = (select MAX(sMessageID) from RxH_271Master where nPatientID = " + PatientID + ") and  convert(datetime, convert(varchar,dtResquestDateTimeStamp,101)) = '"+ DateTime.Now.Date +"'  ";
                ogloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString);
                dt_271Info = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                return dt_271Info;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                if (ogloRxHubDBLayer != null)
                {
                    ogloRxHubDBLayer.Disconnect();
                    ogloRxHubDBLayer.Dispose();
                    ogloRxHubDBLayer = null;
                }
                return dt_271Info;
            }
            finally
            {
               
                if (ogloRxHubDBLayer != null)
                {
                    ogloRxHubDBLayer.Disconnect();
                    ogloRxHubDBLayer.Dispose();
                    ogloRxHubDBLayer = null;
                }
            }

        }

        public DataTable GetGeneralEligiblityInformation(Int64 PatientID)
        {

           // string strQuery = "";
            DataTable dt_EligiblityInfo = null; // new DataTable();

            //clsgloRxHubDBLayer ogloRxHubDBLayer = new clsgloRxHubDBLayer();
            try
            {
            //    //strQuery = " SELECT Top 1 ISNULL(RxH_271Master.sSTLoopControlID,'') AS sSTLoopControlID,ISNULL(RxH_271Master.sPBM_PayerName,'') AS sPBM_PayerName,ISNULL(RxH_271Master.sPBM_PayerParticipantID,'') AS sPBM_PayerParticipantID, ISNULL(RxH_271Master.sPBM_PayerMemberID,'') AS sPBM_PayerMemberID, ISNULL( RxH_271Master.sPhysicianName,'') AS sPhysicianName, ISNULL( RxH_271Master.sPhysicianSuffix,'') as sPhysicianSuffix, ISNULL( RxH_271Master.sHealthPlanNumber,'') as sHealthPlanNumber, "
            //    //+ "ISNULL(RxH_271Master.sHealthPlanName,'') as sHealthPlanName, ISNULL( RxH_271Master.sRelationshipCode,'') as sRelationshipCode , ISNULL( RxH_271Master.sRelationshipDescription,'' ) as sRelationshipDescription, ISNULL( RxH_271Master.sPersonCode,'') as sPersonCode, ISNULL(RxH_271Master.sCardHolderID,'') as sCardHolderID, ISNULL( RxH_271Master.sGroupID,'') as sGroupID, ISNULL( RxH_271Master.sCardHolderName,'') as sCardHolderName, ISNULL( RxH_271Master.sGroupName,'') as sGroupName, "
            //    //+ "ISNULL(RxH_271Master.sFormularyListID,'') as sCardHolderName, ISNULL( RxH_271Master.sAlternativeListID,'') as sAlternativeListID, ISNULL( RxH_271Master.sCoverageID,'') as sCoverageID, ISNULL( RxH_271Master.sBINNumber,'') as sBINNumber, ISNULL(RxH_271Master.sCoPayID,'') as sCoPayID, ISNULL( RxH_271Master.sPharmacyEligible,'') as sPharmacyEligible, ISNULL( RxH_271Master.sPharmacyCoverageName,'') as sPharmacyCoverageName, ISNULL(RxH_271Master.sPhEligiblityorBenefitInfo,'') as sPhEligiblityorBenefitInfo, ISNULL( RxH_271Master.sMailOrderRxDrugEligible,'') as sMailOrderRxDrugEligible, ISNULL( RxH_271Master.sMailOrderRxDrugCoverageName,'') as sMailOrderRxDrugCoverageName, "
            //    //+ "ISNULL(RxH_271Master.sMailOrdEligiblityorBenefitInfo,'') as sMailOrdEligiblityorBenefitInfo, (ISNULL(RxH_271Master.sDFirstName,'') + ' '+ ISNULL(RxH_271Master.sDMiddleName,'') + ' ' + ISNULL( RxH_271Master.sDLastName,'')) AS DependentName, ISNULL(RxH_271Master.sDGender,'') as sDGender, ISNULL( RxH_271Master.sDDOB,'') as sDDOB, ISNULL( RxH_271Master.sDSSN,'') as sDSSN, ISNULL( RxH_271Master.sDAddress1,'') as sDAddress1, ISNULL( RxH_271Master.sDAddress2,'') as sDAddress2, ISNULL(RxH_271Master.sDCity,'') as sDCity, ISNULL( RxH_271Master.sDState,'') as sDState, ISNULL( RxH_271Master.sDZip,'') as sDZip , ISNULL( RxH_271Master.nPatientID,0) as nPatientID, ISNULL( RxH_271Master.dtResquestDateTimeStamp,'') as dtResquestDateTimeStamp, "
            //    //+ "ISNULL(RxH_271Master.sMessageID,'') as sMessageID,ISNULL(RxH_271Master.IsDependentdemoChange,'false') AS IsDependentdemoChange,ISNULL(RxH_271Master.sDependentdemochgFirstName,'') AS sDependentdemochgFirstName,ISNULL(RxH_271Master.sDependentdemoChgMiddleName,'') AS sDependentdemoChgMiddleName,ISNULL(RxH_271Master.sDependentdemoChgLastName,'') AS sDependentdemoChgLastName,ISNULL(RxH_271Master.sDependentdemoChgGender,'') AS sDependentdemoChgGender,ISNULL(RxH_271Master.sDependentdemoChgDOB,'') AS sDependentdemoChgDOB,ISNULL(RxH_271Master.sDependentdemoChgSSN,'') AS sDependentdemoChgSSN, "
            //    //+ "ISNULL(RxH_271Master.sDependentdemoChgAddress1,'') AS sDependentdemoChgAddress1,ISNULL(RxH_271Master.sDependentdemoChgAddress2,'') AS sDependentdemoChgAddress2,ISNULL(RxH_271Master.sDependentdemoChgCity,'') AS sDependentdemoChgCity,ISNULL(RxH_271Master.sDependentdemoChgState,'') AS sDependentdemoChgState,ISNULL(RxH_271Master.sDependentdemoChgZip,'') AS sDependentdemoChgZip,(ISNULL(RxH_271Details.sSubscriberFirstName,'') + ' ' + ISNULL( RxH_271Details.sSubscriberMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberLastName,'')) AS SubscriberName, ISNULL( RxH_271Details.sSubscriberGender,'') as sSubscriberGender, ISNULL( RxH_271Details.sSubscriberDOB,'') as sSubscriberDOB, ISNULL( RxH_271Details.sSubscriberSSN,'' ) as sSubscriberSSN,  ISNULL(RxH_271Details.sSubscriberAddress1,'') as sSubscriberAddress1, ISNULL( RxH_271Details.sSubscriberAddress2,'') as sSubscriberAddress2, ISNULL( RxH_271Details.sSubscriberCity,'') as sSubscriberCity, ISNULL( RxH_271Details.sSubscriberState,'') as sSubscriberState, ISNULL(RxH_271Details.sSubscriberZip,'') as sSubscriberZip, "
            //    //+ "ISNULL(RxH_271Details.IsSubscriberdemoChange,'false') AS IsSubscriberdemoChange,ISNULL(RxH_271Details.sSubscriberDemochgFirstName,'') AS sSubscriberDemochgFirstName,ISNULL(RxH_271Details.sSubscriberDemoChgMiddleName,'') AS sSubscriberDemoChgMiddleName,ISNULL(RxH_271Details.sSubscriberDemoChgLastName,'') AS sSubscriberDemoChgLastName,ISNULL(RxH_271Details.sSubscriberDemoChgGender,'') AS sSubscriberDemoChgGender,ISNULL(RxH_271Details.sSubscriberDemoChgDOB,'') AS sSubscriberDemoChgDOB,ISNULL(RxH_271Details.sSubscriberDemoChgSSN,'') AS sSubscriberDemoChgSSN,ISNULL(RxH_271Details.sSubscriberDemoChgAddress1,'') AS sSubscriberDemoChgAddress1,ISNULL(RxH_271Details.sSubscriberDemoChgAddress2,'') AS sSubscriberDemoChgAddress2, "
            //    //+ "ISNULL(RxH_271Details.sSubscriberDemoChgCity,'') AS sSubscriberDemoChgCity,ISNULL(RxH_271Details.sSubscriberDemoChgState,'') AS sSubscriberDemoChgState,ISNULL(RxH_271Details.sSubscriberDemoChgZip,'') AS sSubscriberDemoChgZip "
            //    //+ "FROM RxH_271Master INNER JOIN "
            //    //+ "RxH_271Details ON RxH_271Master.sMessageID = RxH_271Details.sMessageID WHERE RxH_271Master.nPatientID = " + PatientID + " AND convert(datetime,convert(varchar,dtResquestDateTimeStamp,101)) = '" + DateTime.Now.Date.ToShortDateString() + "'";


               // strQuery = " SELECT  DISTINCT ISNULL(RxH_271Master.sSTLoopControlID,'') AS sSTLoopControlID,ISNULL( RxH_271Master.dtResquestDateTimeStamp,'') as dtResquestDateTimeStamp,ISNULL(RxH_271Master.sPBM_PayerName,'') AS sPBM_PayerName,ISNULL(RxH_271Master.sPBM_PayerParticipantID,'') AS sPBM_PayerParticipantID, ISNULL(RxH_271Master.sPBM_PayerMemberID,'') AS sPBM_PayerMemberID, ISNULL( RxH_271Master.sPhysicianName,'') AS sPhysicianName, ISNULL( RxH_271Master.sPhysicianSuffix,'') as sPhysicianSuffix, ISNULL( RxH_271Master.sHealthPlanNumber,'') as sHealthPlanNumber, "
               //+ "ISNULL(RxH_271Master.sHealthPlanName,'') as sHealthPlanName, ISNULL( RxH_271Master.sRelationshipCode,'') as sRelationshipCode , ISNULL( RxH_271Master.sRelationshipDescription,'' ) as sRelationshipDescription, ISNULL( RxH_271Master.sPersonCode,'') as sPersonCode, ISNULL(RxH_271Master.sCardHolderID,'') as sCardHolderID, ISNULL( RxH_271Master.sGroupID,'') as sGroupID, ISNULL( RxH_271Master.sCardHolderName,'') as sCardHolderName, ISNULL( RxH_271Master.sGroupName,'') as sGroupName, "
               //+ "ISNULL(RxH_271Master.sFormularyListID,'') as sFormularyListID, ISNULL( RxH_271Master.sAlternativeListID,'') as sAlternativeListID, ISNULL( RxH_271Master.sCoverageID,'') as sCoverageID, ISNULL( RxH_271Master.sBINNumber,'') as sBINNumber, ISNULL(RxH_271Master.sCoPayID,'') as sCoPayID, ISNULL( RxH_271Master.sPharmacyEligible,'') as sPharmacyEligible, ISNULL( RxH_271Master.sPharmacyCoverageName,'') as sPharmacyCoverageName, ISNULL(RxH_271Master.sPhEligiblityorBenefitInfo,'') as sPhEligiblityorBenefitInfo, ISNULL( RxH_271Master.sMailOrderRxDrugEligible,'') as sMailOrderRxDrugEligible, ISNULL( RxH_271Master.sMailOrderRxDrugCoverageName,'') as sMailOrderRxDrugCoverageName, "
               //+ "ISNULL(RxH_271Master.sMailOrdEligiblityorBenefitInfo,'') as sMailOrdEligiblityorBenefitInfo, (ISNULL(RxH_271Master.sDFirstName,'') + ' '+ ISNULL(RxH_271Master.sDMiddleName,'') + ' ' + ISNULL( RxH_271Master.sDLastName,'')) AS DependentName, ISNULL(RxH_271Master.sDGender,'') as sDGender, ISNULL( RxH_271Master.sDDOB,'') as sDDOB, ISNULL( RxH_271Master.sDSSN,'') as sDSSN, ISNULL( RxH_271Master.sDAddress1,'') as sDAddress1, ISNULL( RxH_271Master.sDAddress2,'') as sDAddress2, ISNULL(RxH_271Master.sDCity,'') as sDCity, ISNULL( RxH_271Master.sDState,'') as sDState, ISNULL( RxH_271Master.sDZip,'') as sDZip , ISNULL( RxH_271Master.nPatientID,0) as nPatientID,"
               //+ "ISNULL(RxH_271Master.sMessageID,'') as sMessageID,ISNULL(RxH_271Master.IsDependentdemoChange,'false') AS IsDependentdemoChange,(ISNULL(RxH_271Master.sDependentdemochgFirstName,'') + ' ' + ISNULL(RxH_271Master.sDependentdemoChgMiddleName,'') + ' ' + ISNULL(RxH_271Master.sDependentdemoChgLastName,'')) AS DependentdemoChgName,ISNULL(RxH_271Master.sDependentdemoChgGender,'') AS sDependentdemoChgGender,ISNULL(RxH_271Master.sDependentdemoChgDOB,'') AS sDependentdemoChgDOB,ISNULL(RxH_271Master.sDependentdemoChgSSN,'') AS sDependentdemoChgSSN, "
               //+ "ISNULL(RxH_271Master.sDependentdemoChgAddress1,'') AS sDependentdemoChgAddress1,ISNULL(RxH_271Master.sDependentdemoChgAddress2,'') AS sDependentdemoChgAddress2,ISNULL(RxH_271Master.sDependentdemoChgCity,'') AS sDependentdemoChgCity,ISNULL(RxH_271Master.sDependentdemoChgState,'') AS sDependentdemoChgState,ISNULL(RxH_271Master.sDependentdemoChgZip,'') AS sDependentdemoChgZip,(ISNULL(RxH_271Details.sSubscriberFirstName,'') + ' ' + ISNULL( RxH_271Details.sSubscriberMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberLastName,'')) AS SubscriberName, ISNULL( RxH_271Details.sSubscriberGender,'') as sSubscriberGender, ISNULL( RxH_271Details.sSubscriberDOB,'') as sSubscriberDOB, ISNULL( RxH_271Details.sSubscriberSSN,'' ) as sSubscriberSSN,  ISNULL(RxH_271Details.sSubscriberAddress1,'') as sSubscriberAddress1, ISNULL( RxH_271Details.sSubscriberAddress2,'') as sSubscriberAddress2, ISNULL( RxH_271Details.sSubscriberCity,'') as sSubscriberCity, ISNULL( RxH_271Details.sSubscriberState,'') as sSubscriberState, ISNULL(RxH_271Details.sSubscriberZip,'') as sSubscriberZip, "
               //+ "ISNULL(RxH_271Details.IsSubscriberdemoChange,'false') AS IsSubscriberdemoChange,(ISNULL(RxH_271Details.sSubscriberDemochgFirstName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberDemoChgMiddleName,'') + ' ' + ISNULL(RxH_271Details.sSubscriberDemoChgLastName,'')) AS SubscriberDemoChgName,ISNULL(RxH_271Details.sSubscriberDemoChgGender,'') AS sSubscriberDemoChgGender,ISNULL(RxH_271Details.sSubscriberDemoChgDOB,'') AS sSubscriberDemoChgDOB,ISNULL(RxH_271Details.sSubscriberDemoChgSSN,'') AS sSubscriberDemoChgSSN,ISNULL(RxH_271Details.sSubscriberDemoChgAddress1,'') AS sSubscriberDemoChgAddress1,ISNULL(RxH_271Details.sSubscriberDemoChgAddress2,'') AS sSubscriberDemoChgAddress2, "
               //+ "ISNULL(RxH_271Details.sSubscriberDemoChgCity,'') AS sSubscriberDemoChgCity,ISNULL(RxH_271Details.sSubscriberDemoChgState,'') AS sSubscriberDemoChgState,ISNULL(RxH_271Details.sSubscriberDemoChgZip,'') AS sSubscriberDemoChgZip "
               //+ "FROM RxH_271Master INNER JOIN "
               //+ "RxH_271Details ON RxH_271Master.sMessageID = RxH_271Details.sMessageID AND RxH_271Details.sSTLoopControlID = RxH_271Master.sSTLoopControlID WHERE RxH_271Master.nPatientID = " + PatientID + " Order by dtResquestDateTimeStamp desc";
               // ogloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString);

                using (gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString))
                {
                    oDB.Connect(false);
                    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
        
                    oParameters.Add("@nPatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                  
                    oDB.Retrive("gsp_getPatientEligibilityInfo",oParameters, out dt_EligiblityInfo);
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null; 
                    oDB.Disconnect();
                }

             
                return dt_EligiblityInfo;

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return dt_EligiblityInfo;
            }
            finally
            {

            }

        }


        public void  GetEligibilityCheck(Int64 PatientID)
        {
            clsgloRxHubDBLayer ogloRxHubDBLayer = new clsgloRxHubDBLayer();
            try
            {
                ogloRxHubDBLayer.Connect(ClsgloRxHubGeneral.ConnectionString)  ;
                string strQuery = "Select isnull(nPatientID,0) as PatientID,isnull(sfirstname,'') as FName,isnull(sMiddleName,'') as MName,isnull(slastname,'') as LName,dtDOB as DOB,isnull(sGender,'') as Gender,isnull(nProviderID,0) as ProviderID,isnull(nSSN,0) as SSN, isnull(sAddressLine1,'') as Address1, isnull(sAddressLine2,'') as Address2, isnull(sCity,'') as City, isnull(sState,'') as State,isnull(sZIP,'')as Zip,isnull(sPhone,'') as Phone, isnull(sEmail,'') as Email, isnull(sFAX,'') as Fax, isnull(sMobile,'') as Mobile From Patient where npatientid= " + PatientID + "";
                System.Data.DataTable dt = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                oPatient = new ClsPatient();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //Fill Patient object
                        
                        oPatient.PatientID = Convert.ToInt64(dt.Rows[0]["PatientID"]);
                        oPatient.FirstName = dt.Rows[0]["FName"].ToString();
                        oPatient.LastName = dt.Rows[0]["LName"].ToString();
                        oPatient.MiddleName = dt.Rows[0]["MName"].ToString();
                        oPatient.DOB = Convert.ToDateTime(dt.Rows[0]["DOB"]);
                        oPatient.Gender = dt.Rows[0]["Gender"].ToString();
                        oPatient.Provider.ProviderID = Convert.ToInt64(dt.Rows[0]["ProviderID"]);
                        oPatient.SSN = "000000000"; //dt.Rows[0]["SSN"].ToString(); as per yaw sir discussion SSN no should be sent as "000000000" 
                        oPatient.PatientAddress.AddressLine1 = dt.Rows[0]["Address1"].ToString();
                        oPatient.PatientAddress.AddressLine2 = dt.Rows[0]["Address2"].ToString();
                        oPatient.PatientAddress.City = dt.Rows[0]["City"].ToString();
                        oPatient.PatientAddress.State = dt.Rows[0]["State"].ToString();
                        oPatient.PatientAddress.Zip = dt.Rows[0]["Zip"].ToString();
                        oPatient .PatientContact.Phone =dt.Rows [0]["Phone"].ToString ();
                        oPatient.PatientContact.Email = dt.Rows[0]["Email"].ToString();
                        oPatient.PatientContact.Fax = dt.Rows[0]["Fax"].ToString();
                        oPatient.PatientContact.Mobile = dt.Rows[0]["Mobile"].ToString();
                                          
                      }
                }

                strQuery = "";
                strQuery = "Select isnull(sName ,'')as StoreName,isNull(sAddressLine1 ,'')as AddressLine1,isNull(sAddressLine2 ,'')as AddressLine2,isNull(sCity ,'')as City,isNull(sState ,'')as State,isNull(sZip ,'')as Zip,isnull(sPhone,'')as Phone,isnull(sEmail,'')as Email,isnull(nContactFlag,0)as ContactFlag from Patient_DTL where nPatientID=" + PatientID + "";
                 dt = ogloRxHubDBLayer.ReadPatRecord(strQuery);
             //   oPharmacy = new ClsPharmacy();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        oPatient.Pharmacy.StoreName = dt.Rows[0]["StoreName"].ToString();
                        oPatient.Pharmacy.PhramacyAddress.AddressLine1 = dt.Rows[0]["AddressLine1"].ToString();
                        oPatient.Pharmacy.PhramacyAddress.AddressLine2 = dt.Rows[0]["AddressLine2"].ToString();
                        oPatient.Pharmacy.PhramacyAddress.City = dt.Rows[0]["City"].ToString();
                        oPatient.Pharmacy.PhramacyAddress.State = dt.Rows[0]["State"].ToString();
                        oPatient.Pharmacy.PhramacyAddress.Zip = dt.Rows[0]["Zip"].ToString();
                        oPatient.Pharmacy.PharmacyContactDetails.Phone = dt.Rows[0]["Phone"].ToString();
                        oPatient.Pharmacy.PharmacyContactDetails.Email = dt.Rows[0]["Email"].ToString();
                    }
                }

                strQuery = "select isnull(sDEA,'')as DEANumber,sfirstname as FName,isnull(sMiddlename,'') as MName,slastname as LName,isnull(sGender,'') as Gender,isnull(sNPI,'') as NPI,isnull(sAddress,'') as Address,isnull(sStreet,'') as Street,isnull(sCity,'') as City,isnull(sState,'') as State,isnull(sZIP,'') as Zip,isnull(sPhoneNo,'') as PhoneNo,isnull(sFAX,'') as Fax,isnull(sMobileNo,'') as MobileNo,isnull(sEmail,'') as Email,isnull(sCompanyName,'') as ClinicName,isnull(nProviderType,0)as SpeacailtyType,isnull(sPrefix,'')as Prefix From Provider_Mst where nproviderID= " + oPatient.Provider.ProviderID + "";
                dt = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        //Fill Provider object
                        oPatient.Provider.ProviderDEA = dt.Rows[0]["DEANumber"].ToString();
                        oPatient.Provider.ProviderFirstName = dt.Rows[0]["FName"].ToString();
                        oPatient.Provider.ProviderMiddleName = dt.Rows[0]["MName"].ToString();
                        oPatient.Provider.ProviderLastName = dt.Rows[0]["LName"].ToString();
                        oPatient.Provider.Gender = dt.Rows[0]["Gender"].ToString();
                        oPatient.Provider.ProviderNPI = dt.Rows[0]["NPI"].ToString(); 
                        oPatient.Provider.ProviderAddress.AddressLine1 = dt.Rows[0]["Street"].ToString();
                        oPatient.Provider.ProviderAddress.AddressLine2 = dt.Rows[0]["Address"].ToString();
                        oPatient.Provider.ProviderAddress.City = dt.Rows[0]["City"].ToString();
                        oPatient.Provider.ProviderAddress.State = dt.Rows[0]["State"].ToString();
                        oPatient.Provider.ProviderAddress.Zip = dt.Rows[0]["Zip"].ToString();
                        oPatient.Provider.ProviderContactDtl.Phone = dt.Rows[0]["PhoneNo"].ToString();
                        oPatient.Provider.ProviderContactDtl.Fax  = dt.Rows[0]["Fax"].ToString().Replace ("-","");
                        oPatient.Provider.ProviderContactDtl.Mobile = dt.Rows[0]["MobileNo"].ToString();
                        oPatient.Provider.ProviderContactDtl.Email = dt.Rows[0]["Email"].ToString();
                        oPatient.Provider.ClinicName = dt.Rows[0]["ClinicName"].ToString();
                        oPatient.Provider.AMASpecialtyCode = dt.Rows[0]["SpeacailtyType"].ToString();
                        oPatient.Provider.ProviderPrefix = dt.Rows[0]["Prefix"].ToString();

                    }
                }
                strQuery = "";
                //strQuery = "Select isnull(sPayerName,'')as PayerName,isnull(sMemberID,'')as PBMMemberID,isnull(sCardHolderID,'')as CardHolderID,isnull(sCardHolderName,'')as CardHolderName,isnull(sGroupID,'')as GroupID from RxH_271Master where nPatientID=" + PatientID + "";
                strQuery = "Select isnull(sPBM_PayerName,'')as PayerName,isnull(sPBM_PayerParticipantID,'')as PayerID,isnull(sPBM_PayerMemberID,'')as PBMMemberID,isnull(sRelationshipCode,'') as RelationshipCode,isnull(sCardHolderID,'')as CardHolderID,isnull(sCardHolderName,'')as CardHolderName,isnull(sGroupID,'')as GroupID from RxH_271Master where nPatientID=" + PatientID + "";
                dt = ogloRxHubDBLayer.ReadPatRecord(strQuery);
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        oPatient.RxH271Master.PayerName = dt.Rows[0]["PayerName"].ToString();
                        oPatient.RxH271Master.PayerParticipantId = dt.Rows[0]["PayerID"].ToString();
                        oPatient.RxH271Master.MemberID = dt.Rows[0]["PBMMemberID"].ToString();
                        oPatient.RxH271Master.CardHolderId = dt.Rows[0]["CardHolderID"].ToString();
                        oPatient.RxH271Master.CardHolderName = dt.Rows[0]["CardHolderName"].ToString();
                        oPatient.RxH271Master.GroupId = dt.Rows[0]["GroupID"].ToString();
                        oPatient.RxH271Master.RelationshipCode = dt.Rows[0]["RelationshipCode"].ToString();
                    }
                }

                //get Subscriber information against patientid
                strQuery = "";
                //int i = 0;

                //-------Bug #69460: 00000709: RxElig issue. Subscriber info not required for Rx-eligibility, so it is commented
                ////GLO2011-0011225 --added 2 new fields in query -- dtDOB as SubDOB, sSubscriberGender as SubGender
                //strQuery = "select isnull(sSubscriberID,'') as SubscriberID, isnull(sSubFName,'') as SubFName,isnull(sSubMName,'') as SubMName,isnull(sSubLName,'') as SubLName, dtDOB as SubDOB, sSubscriberGender as SubGender,isnull(sSubscriberAddr1,'') as SubscriberAddr1,isnull(sSubscriberAddr2,'') as SubscriberAddr2,isnull(sSubscriberState,'') as SubscriberState,isnull(sSubscriberCity,'') as SubscriberCity,isnull(sSubscriberZip,'') as SubscriberZip,isnull(sPhone,'') as Phone,isnull( sEmail,'')as Email,isnull(sRelationShip,'')as RelationShip, nInsuranceID as InsuranceID ,isnull(sInsuranceName,'')as InsuranceName,isnull(sPayerID,0) as PayerID From Patientinsurance_DTL where nPatientID =" + PatientID + "";
                //dt = ogloRxHubDBLayer.ReadPatRecord(strQuery );
                //if (dt != null)
                //{
                //   if (dt.Rows.Count > 0)
                //    {
                //        for (i = 0; i < dt.Rows.Count; i++)
                //        {
                //            ClsSubscriber oclsSubscriber = new ClsSubscriber();
                //            oclsSubscriber.SubscriberID = dt.Rows[i]["SubscriberID"].ToString();
                //            oclsSubscriber.SubscriberFirstName = dt.Rows[i]["SubFName"].ToString();
                //            oclsSubscriber.SubscriberLastName = dt.Rows[i]["SubLName"].ToString();
                //            oclsSubscriber.SubscriberMiddleName = dt.Rows[i]["SubMName"].ToString();

                //            //GLO2011-0011225
                //            oclsSubscriber.SubscriberDOB = Convert.ToDateTime(dt.Rows[i]["SubDOB"]);
                //            oclsSubscriber.SubscriberGender = dt.Rows[i]["SubGender"].ToString();

                //            oclsSubscriber.SubscriberAddress.AddressLine1 = dt.Rows[i]["SubscriberAddr1"].ToString();
                //            oclsSubscriber.SubscriberAddress.AddressLine2 = dt.Rows[i]["SubscriberAddr2"].ToString();
                //            oclsSubscriber.SubscriberAddress.State = dt.Rows[i]["SubscriberState"].ToString();
                //            oclsSubscriber.SubscriberAddress.City = dt.Rows[i]["SubscriberCity"].ToString();
                //            oclsSubscriber.SubscriberAddress.Zip = dt.Rows[i]["SubscriberZip"].ToString();
                //            oclsSubscriber.InsuranceID = Convert.ToInt64(dt.Rows[i]["InsuranceID"]);
                //            oclsSubscriber.SubscriberContactDtl.Phone = dt.Rows[i]["Phone"].ToString();
                //            oclsSubscriber.SubscriberContactDtl.Email = dt.Rows[i]["Email"].ToString(); ;
                //            //oclsSubscriber.SubscriberContactDtl .Phone ;
                //            //oclsSubscriber .SubscriberContactDtl .Mobile ;
                //            //oclsSubscriber .SubscriberContactDtl .Fax ;
                //            oclsSubscriber.InsuranceName = dt.Rows[0]["InsuranceName"].ToString();
                            
                //            oclsSubscriber.RelationShip = dt.Rows[0]["RelationShip"].ToString();
                //            oPatient.Subscriber.Add(oclsSubscriber);
                //            oclsSubscriber = null;
                //        }
                                                                                                          
                //    }

                //}              
                ogloRxHubDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                throw ex;
            }
            finally
            {
                if (ogloRxHubDBLayer != null)
                {
                    ogloRxHubDBLayer.Dispose();
                    ogloRxHubDBLayer = null;
                }
            }
        }

        public List<ClsPatient> GetPreviousMedHxRequest(Int64 PatientID)
        {                                    
            DataTable dt = new DataTable();
            List<ClsPatient> returned = null;
            try
            {             
                using (gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(ClsgloRxHubGeneral.ConnectionString))
                {
                    oDB.Connect(false);
                    gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();

                    oParameters.Add("@PatientID", PatientID, ParameterDirection.Input, SqlDbType.BigInt);

                    oDB.Retrive("MedHx_GetPreviousRequest", oParameters, out dt);
                    oParameters.Clear();
                    oParameters.Dispose();
                    oParameters = null;
                    oDB.Disconnect();
                }

                if (dt != null && dt.Rows.Count > 0)
                {
                    returned = new List<ClsPatient>();

                    foreach (DataRow drow in dt.Rows)
                    {
                        ClsPatient p = new ClsPatient();

                        p.PatientID = Convert.ToInt64(dt.Rows[0]["nPatientID"]);
                        p.FirstName = dt.Rows[0]["sPatientFirstName"].ToString();
                        p.MiddleName = dt.Rows[0]["sPatientMiddleName"].ToString();
                        p.LastName = dt.Rows[0]["sPatientLastName"].ToString();
                        p.DOB = Convert.ToDateTime(dt.Rows[0]["sPatientDOB"]);
                        p.Gender = dt.Rows[0]["sPatientGender"].ToString();
                        p.SSN = "000000000";
                        p.PatientAddress.AddressLine1 = dt.Rows[0]["sPatientAddressLine1"].ToString();
                        p.PatientAddress.AddressLine2 = dt.Rows[0]["sPatientAddressLine2"].ToString();
                        p.PatientAddress.City = dt.Rows[0]["sPatientCity"].ToString();
                        p.PatientAddress.State = dt.Rows[0]["sPatientState"].ToString();
                        p.PatientAddress.Zip = dt.Rows[0]["sPatientZip"].ToString();
                        p.PatientContact.Phone = dt.Rows[0]["sPatientPhNo"].ToString();
                        p.PatientContact.Email = dt.Rows[0]["sPatientEmail"].ToString();

                        p.Provider.ProviderDEA = dt.Rows[0]["sPrescriberDEANo"].ToString();
                        p.Provider.ProviderNPI = dt.Rows[0]["sPrescriberNPI"].ToString();
                        p.Provider.ProviderFirstName = dt.Rows[0]["sPrescriberFirstName"].ToString();
                        p.Provider.ProviderMiddleName = dt.Rows[0]["sPrescriberMiddleName"].ToString();
                        p.Provider.ProviderLastName = dt.Rows[0]["sPrescriberLastName"].ToString();
                        
                        p.Provider.ProviderAddress.AddressLine1 = dt.Rows[0]["sPrescriberAddressLine1"].ToString();
                        p.Provider.ProviderAddress.AddressLine2 = dt.Rows[0]["sPrescriberAddressLine2"].ToString();
                        p.Provider.ProviderAddress.State = dt.Rows[0]["sPrescriberState"].ToString();
                        p.Provider.ProviderAddress.City = dt.Rows[0]["sPrescriberCity"].ToString();
                        p.Provider.ProviderAddress.Zip = dt.Rows[0]["sPrescriberZip"].ToString();
                        
                        p.Provider.ProviderContactDtl.Phone = dt.Rows[0]["sPrescriberPhNo"].ToString();
                        p.Provider.ProviderContactDtl.Email = dt.Rows[0]["sPrescriberEmail"].ToString();
                        
                        p.RxH271Master.PayerName = dt.Rows[0]["sPayerName"].ToString();
                        p.RxH271Master.PayerParticipantId = dt.Rows[0]["sPayerID"].ToString();
                        p.RxH271Master.MemberID = dt.Rows[0]["sPBMMember"].ToString();
                        p.RxH271Master.CardHolderId = dt.Rows[0]["sCardholderID"].ToString();
                        p.RxH271Master.CardHolderName = dt.Rows[0]["sCardholderName"].ToString();
                        p.RxH271Master.GroupId = dt.Rows[0]["sGroupID"].ToString();
                        
                        p.TransactionDate = Convert.ToDateTime(Convert.ToString(dt.Rows[0]["dtTransactionDateTime"]));
                        returned.Add(p);
                    }                    
                }

                return returned;
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.Prescription, gloAuditTrail.ActivityCategory.CreatePrescription, gloAuditTrail.ActivityType.General, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                return returned;
            }           
        }
       
        
    }
   
}
