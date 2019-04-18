using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using gloPatientSaving;
using System.Data.Linq;
using System.IO;

namespace gloRxPatientSaving
{
    public class clsDataInsertionLayer
    {

        public class DispositionData
        {
            public string MessageID { get; set; }
            public string FileName { get; set; }
                    
        }

       public clsDataInsertionLayer()
        {
            PatSavGeneral.objDataContext = new PatSavDataClassDataContext(PatSavGeneral.ConnectionString);
        }

       public Boolean InsertPatientSavingMessage(PatientSavingsNotificationType objPSMessage, Int64 PatientID, Int64 _QueueID)
        {
            Boolean _result = false;
            PatSav_Mst objPatSav_Mst = null;
            PatSavOpportunity_Mst objOpportunity_Mst=null;
            PatSavOpportunityAlternativeMed_Dtl objAlternativeMed_Dtl=null;
            PatientSavingsOpportunityType[] objOpportunity=null;
            OpportunityAlternativeMedication[] objaltMed=null;
            PatSavGeneral.objDataContext = new PatSavDataClassDataContext(PatSavGeneral.ConnectionString);
            
            decimal PatSavID = 0;
            try
            {
                if (objPSMessage != null)
                {
                    #region "Insert Data in PatSav_Mst"
                    objPatSav_Mst = new PatSav_Mst();
                    objPatSav_Mst.PSID = GetUniqueueId();
                    objPatSav_Mst.nPatientID = PatientID;
                    objPatSav_Mst.nPatSavQID = _QueueID;
                    if (objPSMessage.Prescriber != null)
                    {
                        objPatSav_Mst.PrescNPI = objPSMessage.Prescriber.Identification!= null?objPSMessage.Prescriber.Identification.NPI:null;
                        if (objPSMessage.Prescriber.Name != null)
                        {
                            objPatSav_Mst.PrescFirstName = objPSMessage.Prescriber.Name.FirstName;
                            objPatSav_Mst.PrescLastName = objPSMessage.Prescriber.Name.LastName;
                            objPatSav_Mst.PrescMiddleName = objPSMessage.Prescriber.Name.MiddleName;
                            objPatSav_Mst.PrescSuffix = objPSMessage.Prescriber.Name.Suffix;
                            objPatSav_Mst.PrescPrefix = objPSMessage.Prescriber.Name.Prefix;
                        }
                    }
                    if (objPSMessage.Patient != null)
                    {
                        objPatSav_Mst.PatMedicalRecIdNumberEHR = objPSMessage.Patient.Identification!= null?objPSMessage.Patient.Identification.MedicalRecordIdentificationNumberEHR:null;
                        if (objPSMessage.Patient.Name != null)
                        {
                            objPatSav_Mst.PatLastName = objPSMessage.Patient.Name.LastName;
                            objPatSav_Mst.PatFirstName = objPSMessage.Patient.Name.FirstName;
                            objPatSav_Mst.PatMiddleName = objPSMessage.Patient.Name.MiddleName;
                        }
                       // objPatSav_Mst.PatGender = objPSMessage.Patient.Gender !=null ? objPSMessage.Patient.Gender.ToString():null;
                        objPatSav_Mst.PatGender =   objPSMessage.Patient.Gender.ToString()  ;
                        objPatSav_Mst.PatDOB = objPSMessage.Patient.DateOfBirth!=null?  objPSMessage.Patient.DateOfBirth.Item.Date : new DateTime() ;
                        if (objPSMessage.Patient.Address  != null)
                        {
                            objPatSav_Mst.PatAddressLine1 = objPSMessage.Patient.Address.AddressLine1;
                            objPatSav_Mst.PatAddressLine2 = objPSMessage.Patient.Address.AddressLine2;
                            objPatSav_Mst.PatCity = objPSMessage.Patient.Address.City;
                         //   objPatSav_Mst.PatState = objPSMessage.Patient.Address.State !=null?objPSMessage.Patient.Address.State.ToString():null;
                            objPatSav_Mst.PatState =   objPSMessage.Patient.Address.State.ToString()  ;
                            objPatSav_Mst.PatZipCode = objPSMessage.Patient.Address.ZipCode;
                        }
                        objPatSav_Mst.PatTelephone = objPSMessage.Patient.CommunicationNumbers != null ? objPSMessage.Patient.CommunicationNumbers.PrimaryTelephone.Number : null;
                    }
                    objPatSav_Mst.BenefitCoordinationPBMMemberID = objPSMessage.BenefitsCoordination!=null ? objPSMessage.BenefitsCoordination.PBMMemberID:null;

                    PatSavGeneral.objDataContext.PatSav_Msts.InsertOnSubmit(objPatSav_Mst);
                    PatSavGeneral.objDataContext.SubmitChanges();
                    PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                    PatSavID=objPatSav_Mst.PSID;
                    
                    #endregion
                                        
                    #region "Insert Data in PatSavOpportunity_Mst"
                    if (PatSavID > 0)
                    {
                        objOpportunity = objPSMessage.Opportunity;
                        if (objOpportunity != null)
                        {
                            foreach (PatientSavingsOpportunityType objOpp in objOpportunity)
                            {
                                if (objOpp != null)
                                {

                                    objOpportunity_Mst = new PatSavOpportunity_Mst();
                                    objOpportunity_Mst.PSOppID = GetUniqueueId();
                                    objOpportunity_Mst.PSID = PatSavID;
                                    objOpportunity_Mst.OpportunityID = Convert.ToString(objOpp.OpportunityID);
                                    objOpportunity_Mst.OpportunityType = objOpp.OpportunityType1;
                                    objOpportunity_Mst.OpportunityHeadline = objOpp.OpportunityHeadline;
                                    objOpportunity_Mst.OpportunityBody = objOpp.OpportunityBody;
                                    objOpportunity_Mst.AnnualCostSavings = Convert.ToDecimal(objOpp.AnnualCostSavings);
                                    if (objOpp.OriginalPrescription != null)
                                    {
                                        objOpportunity_Mst.OrgRxDrugDesc = objOpp.OriginalPrescription.DrugDescription;
                                        if (objOpp.OriginalPrescription.DrugCoded != null && objOpp.OriginalPrescription.DrugCoded.Items != null && objOpp.OriginalPrescription.DrugCoded.Items.Length > 0)
                                        {
                                            objOpportunity_Mst.OrgRxDrugCode = objOpp.OriginalPrescription.DrugCoded.Items[0].Code;
                                            objOpportunity_Mst.OrgRxDrugQlfr = objOpp.OriginalPrescription.DrugCoded.Items[0].Qualifier;
                                        }

                                        objOpportunity_Mst.OrgRxDrugQty = objOpp.OriginalPrescription.Quantity != null ? Convert.ToDecimal(objOpp.OriginalPrescription.Quantity.Value) : 0 ;
                                        objOpportunity_Mst.OrgRxDaysSupply =Convert.ToDecimal(objOpp.OriginalPrescription.DaysSupply);
                                        if (objOpp.OriginalPrescription.WrittenDate != null && objOpp.OriginalPrescription.WrittenDate.Item != null && objOpp.OriginalPrescription.WrittenDate.Item.Date != null)
                                        {
                                            objOpportunity_Mst.OrgRxWrittenDate = objOpp.OriginalPrescription.WrittenDate.Item.Date;
                                        }
                                        if (objOpp.OriginalPrescription.LastFillDate != null && objOpp.OriginalPrescription.LastFillDate.Item != null && objOpp.OriginalPrescription.LastFillDate.Item.Date != null)
                                        {
                                            objOpportunity_Mst.OrgRxLastFillDate = objOpp.OriginalPrescription.LastFillDate.Item.Date;
                                        }
                                        if (objOpp.OriginalPrescription.Pharmacy != null)
                                        {
                                            objOpportunity_Mst.OrgRxPhNCPDPID = objOpp.OriginalPrescription.Pharmacy.Identification != null ? objOpp.OriginalPrescription.Pharmacy.Identification.NCPDPID : null;
                                            objOpportunity_Mst.OrgRxPhBusinessName = objOpp.OriginalPrescription.Pharmacy.BusinessName;
                                            if (objOpp.OriginalPrescription.Pharmacy.Address != null)
                                            {
                                                objOpportunity_Mst.OrgRxPhAddressLine1 = objOpp.OriginalPrescription.Pharmacy.Address.AddressLine1;
                                                objOpportunity_Mst.OrgRxPhAddressLine2 = objOpp.OriginalPrescription.Pharmacy.Address.AddressLine2;
                                                objOpportunity_Mst.OrgRxPhCity = objOpp.OriginalPrescription.Pharmacy.Address.City;
                                               // objOpportunity_Mst.OrgRxPhState = objOpp.OriginalPrescription.Pharmacy.Address.State != null ? objOpp.OriginalPrescription.Pharmacy.Address.State.ToString() : null;
                                                objOpportunity_Mst.OrgRxPhState =   objOpp.OriginalPrescription.Pharmacy.Address.State.ToString() ;
                                                objOpportunity_Mst.OrgRxPhZipCode = objOpp.OriginalPrescription.Pharmacy.Address.ZipCode;
                                            }
                                            if (objOpp.OriginalPrescription.Pharmacy.CommunicationNumbers != null && objOpp.OriginalPrescription.Pharmacy.CommunicationNumbers.PrimaryTelephone != null)
                                            {
                                                objOpportunity_Mst.OrgRxPhTelephone = objOpp.OriginalPrescription.Pharmacy.CommunicationNumbers.PrimaryTelephone.Number;
                                                objOpportunity_Mst.OrgRxPhTelephExt = objOpp.OriginalPrescription.Pharmacy.CommunicationNumbers.PrimaryTelephone.Extension;
                                            }
                                            objOpportunity_Mst.OrgRxPhFax = null;
                                        }
                                        if (objOpp.OriginalPrescription.Prescriber != null)
                                        {
                                            objOpportunity_Mst.OrgRxPrescNPI = objOpp.OriginalPrescription.Prescriber.Identification != null ? objOpp.OriginalPrescription.Prescriber.Identification.NPI : null;
                                            if (objOpp.OriginalPrescription.Prescriber.Name != null)
                                            {
                                                objOpportunity_Mst.OrgRxPrescLastName = objOpp.OriginalPrescription.Prescriber.Name.LastName;
                                                objOpportunity_Mst.OrgRxPrescFirstName = objOpp.OriginalPrescription.Prescriber.Name.FirstName;
                                                objOpportunity_Mst.OrgRxPrescMiddleName = objOpp.OriginalPrescription.Prescriber.Name.MiddleName;
                                                objOpportunity_Mst.OrgRxPrescSuffix = objOpp.OriginalPrescription.Prescriber.Name.Suffix;
                                                objOpportunity_Mst.OrgRxPrescPrefix = objOpp.OriginalPrescription.Prescriber.Name.Prefix;
                                            }
                                        }
                                    }
                                    PatSavGeneral.objDataContext.PatSavOpportunity_Msts.InsertOnSubmit(objOpportunity_Mst);
                                    PatSavGeneral.objDataContext.SubmitChanges();
                                    PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                                    if (objOpportunity_Mst.PSOppID > 0)
                                    {
                                        #region "Insert Data in PatSavOpportunityAlternativeMed_Dtl"
                                        objaltMed = objOpp.AlternativeMedication;
                                        if (objaltMed != null)
                                        {
                                            foreach (OpportunityAlternativeMedication oaltMed in objaltMed)
                                            {
                                                objAlternativeMed_Dtl = new PatSavOpportunityAlternativeMed_Dtl();
                                                objAlternativeMed_Dtl.PSOppAltMedID = GetUniqueueId();
                                                objAlternativeMed_Dtl.PSOppID = objOpportunity_Mst.PSOppID;
                                                objAlternativeMed_Dtl.AltMedDrugDesc = oaltMed.DrugDescription;
                                                if (oaltMed.DrugCoded != null && oaltMed.DrugCoded.Items != null && oaltMed.DrugCoded.Items.Length > 0)
                                                {
                                                    objAlternativeMed_Dtl.AltMedDrugCode = oaltMed.DrugCoded.Items[0].Code;
                                                    objAlternativeMed_Dtl.AltMedDrugQlfr = oaltMed.DrugCoded.Items[0].Qualifier;
                                                }

                                                PatSavGeneral.objDataContext.PatSavOpportunityAlternativeMed_Dtls.InsertOnSubmit(objAlternativeMed_Dtl);
                                                PatSavGeneral.objDataContext.SubmitChanges();
                                                PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                                                objAlternativeMed_Dtl = null;
                                            }
                                           
                                        }
                                        #endregion
                                    }
                                    objOpportunity_Mst = null;
                                }
                                
                            }
                        }
                    _result = true;
                   }         
                    #endregion

                }
            }
            catch //(Exception ex)
            {
                _result = false;
            }
            finally
            {
                 objPatSav_Mst = null;
                 objOpportunity_Mst = null;
                 objAlternativeMed_Dtl = null;
                 objOpportunity = null;
                 objaltMed = null;
                 PatSavID = 0;
            }
            return _result;
        }

        private long GetUniqueueId()
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object oResult = new object();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@ID", "0", ParameterDirection.Output, SqlDbType.BigInt);
                oDB.Execute("gsp_GetUniqueID", oParameters, out oResult);
                oDB.Disconnect();
            }
            catch //(Exception ex)
            {
                
            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                }
                if ((oParameters != null))
                {
                    oParameters.Dispose();
                }
            }
            return Convert.ToInt64(oResult);
        }

        //public IQueryable<PatSav_Code> getCode(string _CodeType)
        //{
        //    IQueryable<PatSav_Code> objCodes=null;
            
        //    try
        //    {
        //        objCodes = from result in PatSavGeneral.objDataContext.PatSav_Codes where result.sCodeType == _CodeType select result;
        //       return objCodes;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

       
        public void getandBindPatSavData(decimal _PatientID)
        {
            //IQueryable<PatSav_Mst> objPatSav_Mst = (from result in objDataContext.PatSav_Msts where result.nPatientID == _PatientID select result);
            //MainWindow objWindow = new MainWindow(objPatSav_Mst);
            //objWindow.EntOpensavePrescription = ShowPrescriptionDBLayer;
            //objWindow.ShowDialog();
        }

        public long GetProviderID(long _PatientID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(PatSavGeneral.ConnectionString);
            gloDatabaseLayer.DBParameters oParameters = new gloDatabaseLayer.DBParameters();
            Object oResult = new object();

            try
            {
                oDB.Connect(false);
                oParameters.Clear();
                oParameters.Add("@nPatientID", _PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oParameters.Add("@nProviderID", "0", ParameterDirection.Output, SqlDbType.BigInt);
                oDB.Execute("gsp_GetProviderID", oParameters, out oResult);
                oDB.Disconnect();
            }
            catch //(Exception ex)
            {

            }
            finally
            {
                if ((oDB != null))
                {
                    oDB.Dispose();
                }
                if ((oParameters != null))
                {
                    oParameters.Dispose();
                }
            }
            return Convert.ToInt64(oResult);
        }

        public List <DispositionData> generateOpportunityResponse(Int64 _PatSavMSTID)
        {
            OpportunityResponseType objOppResponse = null;
            List<DispositionData> Dispositionlist = null;
            try
            {
                //IQueryable<PatSav_Mst> oPatientSaving=null;
                PatSav_Mst oPatSav_Mst=null;
                oPatSav_Mst = (from _res in PatSavGeneral.objDataContext.PatSav_Msts where _res.PSID == _PatSavMSTID select _res).FirstOrDefault();
                var _objOpp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts where result.DispositionFile == null && result.PSID == _PatSavMSTID select result);
                //gloSurescriptSecureMessage.clsSecureMessageDB oclsSecureMessageDB = new gloSurescriptSecureMessage.clsSecureMessageDB();
                //DataTable dt=null;
                Dispositionlist = new List<DispositionData>();


                //List<gloSurescriptSecureMessage.Attachment> oLsAttach = null;

                
                foreach (PatSavOpportunity_Mst oOppMSt in _objOpp)
                {
                    objOppResponse = new OpportunityResponseType();
                    objOppResponse.Prescriber = new OpportunityPrescriber();
                    objOppResponse.Prescriber.Identification = new OpportunityProviderID();
                    objOppResponse.Prescriber.Identification.NPI = oPatSav_Mst.PrescNPI;
                    objOppResponse.Prescriber.Name = new gloPatientSaving.NameType();
                    objOppResponse.Prescriber.Name.LastName = oPatSav_Mst.PrescLastName;
                    objOppResponse.Prescriber.Name.FirstName = oPatSav_Mst.PrescFirstName;
                    objOppResponse.Prescriber.Name.MiddleName = oPatSav_Mst.PrescMiddleName;
                    objOppResponse.Prescriber.Name.Suffix = oPatSav_Mst.PrescSuffix;
                    objOppResponse.Prescriber.Name.Prefix = oPatSav_Mst.PrescPrefix;

                    objOppResponse.Patient = new OpportunityPatient();
                    objOppResponse.Patient.Identification = new OpportunityPatientID();
                    objOppResponse.Patient.Identification.MedicalRecordIdentificationNumberEHR = oPatSav_Mst.PatMedicalRecIdNumberEHR;
                    objOppResponse.Patient.Name = new gloPatientSaving.NameType();
                    objOppResponse.Patient.Name.LastName = oPatSav_Mst.PatLastName;
                    objOppResponse.Patient.Name.FirstName = oPatSav_Mst.PatFirstName;
                    objOppResponse.Patient.Name.MiddleName = oPatSav_Mst.PatMiddleName;
                    objOppResponse.Patient.Gender = oPatSav_Mst.PatGender == "F" ? GenderCode.F : (oPatSav_Mst.PatGender == "M" ? GenderCode.M : GenderCode.U);
                    objOppResponse.Patient.DateOfBirth = new gloPatientSaving.DateType();
                    objOppResponse.Patient.DateOfBirth.Item = new DateTime();
                    objOppResponse.Patient.DateOfBirth.ItemElementName = gloPatientSaving.ItemChoiceType.Date;
                    objOppResponse.Patient.DateOfBirth.Item = oPatSav_Mst.PatDOB.Value.Date;
                    objOppResponse.Patient.Address = new gloPatientSaving.MandatoryAddressType();
                    objOppResponse.Patient.Address.AddressLine1 = oPatSav_Mst.PatAddressLine1;
                    objOppResponse.Patient.Address.AddressLine2 = oPatSav_Mst.PatAddressLine2;
                    objOppResponse.Patient.Address.City = oPatSav_Mst.PatCity;
                    objOppResponse.Patient.Address.StateSpecified = true;
                    objOppResponse.Patient.Address.State = (StateCode)Enum.Parse(typeof(StateCode), oPatSav_Mst.PatState);
                    objOppResponse.Patient.Address.ZipCode = oPatSav_Mst.PatZipCode;
                    if (oPatSav_Mst.PatTelephone != null)
                    {
                        objOppResponse.Patient.CommunicationNumbers = new gloPatientSaving.CommunicationNumbersType();
                        objOppResponse.Patient.CommunicationNumbers.PrimaryTelephone = new PhoneType();
                        objOppResponse.Patient.CommunicationNumbers.PrimaryTelephone.Number = oPatSav_Mst.PatTelephone;
                    }
                    else
                        objOppResponse.Patient.CommunicationNumbers = null;
                    {
                    }

                    objOppResponse.BenefitsCoordination = new OpportunityBenefitsCoordination();
                    objOppResponse.BenefitsCoordination.PBMMemberID = oPatSav_Mst.BenefitCoordinationPBMMemberID;

                    objOppResponse.OpportunityDisposition = new OpportunityDisposition();
                    objOppResponse.OpportunityDisposition.OpportunityID = oOppMSt.OpportunityID.ToString();
                    objOppResponse.OpportunityDisposition.OpportunityType = oOppMSt.OpportunityType;
                    objOppResponse.OpportunityDisposition.DispositionCode = "06";
                    objOppResponse.OpportunityDisposition.DispositionDate = new gloPatientSaving.DateType();
                    objOppResponse.OpportunityDisposition.DispositionDate.Item = new DateTime();
                    oOppMSt.DispositionDate = DateTime.Now;
                    objOppResponse.OpportunityDisposition.DispositionDate.Item = (DateTime)oOppMSt.DispositionDate;
                    Int32 i = 0;
                    PatSavGeneral.FilePath = gloRxHub.ClsgloRxHubGeneral.gnstrApplicationFilePath + "Outbox\\";
                    string _FilePath = PatSavGeneral.getFileName();
                    if (gloSerialization.SetClinicalDocument(_FilePath, objOppResponse))
                    {
                        PatSavOpportunity_Mst _opp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts where result.PSOppID == oOppMSt.PSOppID select result).FirstOrDefault();
                        if (_opp != null)
                        {
                            _opp.DispositionDate = oOppMSt.DispositionDate;
                            _opp.DispositionFile = new Binary(File.ReadAllBytes(_FilePath));
                            _opp.DispositionCode = "06";
                            PatSavGeneral.objDataContext.SubmitChanges();
                            PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);

                            var _Messageid = (from res in PatSavGeneral.objDataContext.PatSav_Queues where res.nPatSavQID == oPatSav_Mst.nPatSavQID select res.sMessageID).FirstOrDefault();
                            
                            if (_Messageid != null)
                            {

                                Dispositionlist.Add(new DispositionData() { MessageID = _Messageid, FileName =_FilePath});
                            //    //dt = oclsSecureMessageDB.GetSecureMessageDatailsUsingMessageID(_Messageid);
                                //oLsAttach = oclsSecureMessageDB.GetFileInformationForAttachment(_FilePath);
                                //if (dt != null)
                                //{
                                //    if (dt.Rows.Count > 0)
                                //    {
                                //        oclsSecureMessageDB.InsertOppSecureMessage(dt, oLsAttach, Convert.ToInt64(oPatSav_Mst.nPatientID));
                                //    }
                                //}
                                i = i + 1;
                            }
                            //if (File.Exists(_FilePath))
                            //    File.Delete(_FilePath);
                        }
                    }
                }
                return Dispositionlist;
            }
            catch //(Exception ex)
            {
                return Dispositionlist;
            }
            finally
            {
                objOppResponse = null;
            }
            
        }

       
    }
    
}
