using System;
using System.Collections.Generic;
using System.Text;
using gloPatient;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
namespace gloEmdeonInterface.Classes
{
    public class clsgloLabPatientLayer
    {
        //Added by madan of registration for services
        private bool IsServiceRegistration = false;
        //End madan.

        private clsGeneral objClsGeneral;
        private string _PatientConfirm = string.Empty;
        private String gstrMessageBoxCaption = string.Empty;
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        private bool bIsgloServices = false;

        #region "Constructor & Destructor"
        public clsgloLabPatientLayer()
        {
            objClsGeneral = new clsGeneral(); //Update Log...

            //Added by madan
            #region " Retrieve MessageBoxCaption from AppSettings "

            if (appSettings["MessageBOXCaption"] != null)
            {
                if (appSettings["MessageBOXCaption"] != "")
                {
                    gstrMessageBoxCaption = Convert.ToString(appSettings["MessageBOXCaption"]);
                }
                else
                {
                    gstrMessageBoxCaption = "gloEMR";
                }
            }
            else
            { gstrMessageBoxCaption = "gloEMR"; }

            #endregion
        }

        public clsgloLabPatientLayer(bool _gloServices, string ConnectionString)
        {
            bIsgloServices = _gloServices;
            clsEmdeonGeneral.sConnectionString = ConnectionString;

            objClsGeneral = new clsGeneral(); 
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

        ~clsgloLabPatientLayer()
        {
            Dispose(false);

            if (this.objClsGeneral != null)
            {
                this.objClsGeneral.Dispose();
            }

        }
        #endregion "Constructor & Destructor"


        # region DatabaseLayer
        //Database Connection String..
        //private System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;

        
        // Added by madan on 20100628---
        // Made this table generic To store any external referances towards gloProducts.
        private void gloLabRegisterDB(Int64 PatientID, string ExternalReferenceType, string ReferenceValue)
        {   
            gloDatabaseLayer.DBLayer oDBLayer = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString.Trim());
            gloDatabaseLayer.DBParameters oDBParameters = new gloDatabaseLayer.DBParameters();
            try
            {
                oDBLayer.Connect(false);
                oDBParameters.Clear();
                oDBParameters.Add("@nPatientId", PatientID, ParameterDirection.Input, SqlDbType.BigInt);
                oDBParameters.Add("@sExternalType", "EMDEON", ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sExternalSubType", ExternalReferenceType.Trim(), ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sExternalValue", ReferenceValue, ParameterDirection.Input, SqlDbType.VarChar);
                oDBParameters.Add("@sModuleName", "LABS", ParameterDirection.Input, SqlDbType.VarChar);

                oDBLayer.ExecuteScalar("gsp_INUP_LAB_PatientExternalCodes", oDBParameters);
                oDBParameters.Clear();
                oDBLayer.Disconnect();
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (oDBLayer != null)
                {oDBLayer.Dispose(); }

                if (oDBParameters != null)
                {   oDBParameters.Clear(); oDBParameters.Dispose(); }
            }
        }
        //Getting emdeon refernce value from database-table-patinet_gloLab

        private string DBReferenceValue(long nPatientID, string gloLabtype)
        {

            string _refvalue = string.Empty;
            string _strQuery = string.Empty;
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString.ToString());
            try
            {
                // _strQuery = "Select sgloLabValue from patient_gloLab where nPatientId='" + nPatientID + "'AND sgloLabType='" + gloLabtype + "'";
                _strQuery = "SELECT sExternalValue from PatientExternalCodes where nPatientId='" + nPatientID + "' AND sExternalSubType='" + gloLabtype.Trim() + "' AND sExternalType ='EMDEON'";

                objDbLayer.Connect(false);
                _refvalue = Convert.ToString(objDbLayer.ExecuteScalar_Query(_strQuery));
                objDbLayer.Disconnect();

            }
            catch (SqlException ex)
            {
                _refvalue = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);

            }
            catch (Exception ex)
            {
                _refvalue = string.Empty;
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
            return _refvalue;
        }
        #endregion DatabaseLayer

        # region RegisterPatientToEmdeon
        //Register Patient in Emdeon
        public Boolean RegisterGloPatient(Patient opatient, string ConnectionString)
        {

            Boolean _blnSuccess = false;
            Boolean _blnValidatePatinet = false;

            //Assigning Database Connection string... 
            //if (appSettings != null)
            //{
            //    clsEmdeonGeneral.sConnectionString = Convert.ToString(appSettings["DataBaseConnectionString"]);
            //}
            clsEmdeonGeneral.sConnectionString = ConnectionString.ToString();
            //gloPatient.gloPatient objgloPatient = new gloPatient.gloPatient(clsEmdeonGeneral.sConnectionString.ToString());
            //opatient = objgloPatient.GetPatient(clsEmdeonGeneral.gloLab_patID);

            string strFileName = string.Empty;
            clsEmdeonLabLayer oLabLayer = new clsEmdeonLabLayer();
            try
            {
                

                if ((objClsGeneral.IsInternetConnectionAvailable()==clsGeneral.InternetConnectivity.Success) && (ConfirmNull(clsEmdeonGeneral.sConnectionString.ToString())))
                { //
                    if (gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonUserName.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonUserPassword.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonURL.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.emdeonFacilityCode.ToString().Trim() != "" && gloEmdeonInterface.Classes.clsEmdeonGeneral.gloLab_hsilabel.ToString().Trim() != "")
                    {
                        //********************New Registration****************
                        //If patient code is available with emdeon. then that particular patient is not allowed to register with emdeon until
                        //patient code get changes.....................
                        //***************************************************************************************
                        //********************Patient Modification**************************************************************
                        //If patient code is available with emdeon.then that partiular pateint referance is compared with local database
                        //if that reference is matching with anyother patient then that patient information cannot be modified.
                        //*************************************************************************************************

                        Boolean blnRefVal = GetPatientCodeConfirmation(opatient.DemographicsDetail.PatientCode.ToString(), opatient);
                        if (blnRefVal == true && (ConfirmNull(_PatientConfirm.ToString()) == false) || _PatientConfirm == "avialable")
                        {
                            //Getting all the reference value from database.
                            //**************************************************************************************************
                            clsEmdeonGeneral.gloLab_personRefval = DBReferenceValue(clsEmdeonGeneral.gloLab_patID, "person");
                            //clsEmdeonGeneral.gloLab_providerRefval = DBReferenceValue(clsEmdeonGeneral.gloLab_patID, "ProviderInfo");
                            //clsEmdeonGeneral.gloLab_ispRefval = DBReferenceValue(clsEmdeonGeneral.gloLab_patID, "isp");
                            //clsEmdeonGeneral.gloLab_guarantorRefval = DBReferenceValue(clsEmdeonGeneral.gloLab_patID, "Guarantor");
                            //clsEmdeonGeneral.gloLab_insuranceRefval = DBReferenceValue(clsEmdeonGeneral.gloLab_patID, "Insurance");
                            clsEmdeonGeneral.gloLab_personhsiRefval = DBReferenceValue(clsEmdeonGeneral.gloLab_patID, "personhsi");
                            //**************************************************************************************************

                            //Validating Patient required information for registering in emdeon.
                            _blnValidatePatinet = ValidatePatient("person_put", opatient);
                            if (_blnValidatePatinet == true)
                            {
                                if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()))
                                {
                                    //registering updated Patient
                                    oLabLayer.ProcessRequest("person_update", opatient);
                                    gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                    _blnSuccess = true;
                                }
                                else
                                {
                                    //Sending request for registering patinet in emdeon
                                    clsEmdeonGeneral.sgloLab_Value = "";
                                    strFileName = oLabLayer.ProcessRequest("Person_Put", opatient);
                                    //Extracting Reference Value from XML file....
                                    clsEmdeonGeneral.sgloLab_Value = ExtractReferanceVal(strFileName, "person");
                                    if (ConfirmNull(clsEmdeonGeneral.sgloLab_Value.ToString()))
                                    {
                                        gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID,  clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                        clsEmdeonGeneral.gloLab_personRefval = clsEmdeonGeneral.sgloLab_Value;
                                        _blnSuccess = true;
                                    }
                                    else
                                        _blnSuccess = false;
                                } //Registering PersonHSI Value...
                                if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()))
                                {
                                    if (ConfirmNull(clsEmdeonGeneral.gloLab_personhsiRefval.ToString()))
                                    {
                                        //UpdateingPersonHSI...
                                        oLabLayer.ProcessRequest("personhsi_update", opatient);
                                        gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                        _blnSuccess = true;

                                        //by Abhijeet on 20100512    
                                        if (IsServiceRegistration==false)
                                        {
                                            gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "Patient Information is successfully updated with external lab service ", opatient.PatientID, 0, opatient.DemographicsDetail.PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                        }                                        
                                    }
                                    else
                                    {
                                        clsEmdeonGeneral.sgloLab_Value = "";
                                        strFileName = oLabLayer.ProcessRequest("personhsi_add", opatient);
                                        clsEmdeonGeneral.sgloLab_Value = ExtractReferanceVal(strFileName, "personhsi");
                                        if (ConfirmNull(clsEmdeonGeneral.sgloLab_Value.ToString()))
                                        {
                                            gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                            clsEmdeonGeneral.gloLab_personhsiRefval = clsEmdeonGeneral.sgloLab_Value;
                                            _blnSuccess = true;

                                            //by Abhijeet on 20100512     
                                            if (IsServiceRegistration == false)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "Patient is successfully registered with external lab service ", opatient.PatientID, 0, opatient.DemographicsDetail.PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                            }
                                        }
                                        else
                                        {
                                            _blnSuccess = false;
                                            //by Abhijeet on 20100512  
                                            if (IsServiceRegistration == false)
                                            {
                                                gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "Patient registration failed with external lab service ", opatient.PatientID, 0, opatient.DemographicsDetail.PatientProviderID, gloAuditTrail.ActivityOutCome.Failure, gloAuditTrail.SoftwareComponent.gloEMR);
                                            }
                                        }
                                    }
                                    //------------------------------------------------------------------
                                    //Removed on 20100424 by madan--- as per prasadsir.. Comment... ----
                                    //-------------------------------------------------------------------
                                    //Gurantaor registration... 
                                    //if (opatient.PatientGuarantors != null && opatient.PatientGuarantors.Count > 0)
                                    //{
                                    //    _blnValidatePatinet = ValidatePatient("guarantor_put", opatient);
                                    //    if (ConfirmNull(clsEmdeonGeneral.gloLab_guarantorRefval.ToString()))
                                    //    {
                                    //        //UpdateingPersonHSI...
                                    //        oLabLayer.ProcessRequest("guarantor_update", opatient);
                                    //        gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                    //        _blnSuccess = true;
                                    //    }
                                    //    else
                                    //    {
                                    //        clsEmdeonGeneral.sgloLab_Value = "";
                                    //        strFileName = oLabLayer.ProcessRequest("guarantor_put", opatient);
                                    //        clsEmdeonGeneral.sgloLab_Value = ExtractReferanceVal(strFileName, "guarantor");
                                    //        if (ConfirmNull(clsEmdeonGeneral.sgloLab_Value.ToString()))
                                    //        {
                                    //            gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                    //            clsEmdeonGeneral.gloLab_guarantorRefval = clsEmdeonGeneral.sgloLab_Value;
                                    //            _blnSuccess = true;
                                    //        }
                                    //    }
                                    //}

                                }
                                else
                                {
                                    _blnSuccess = false;
                                }
                                //Removed on 20100401-- By Madan
                                #region InsuranceInfo
                                //if (ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()) && ConfirmNull(clsEmdeonGeneral.gloLab_personhsiRefval.ToString()))
                                //{
                                //    //registering Patient payment type....
                                //    //Assigning payment type..
                                //    //Added for insurance check 20100223--Added by madan
                                //    clsEmdeonGeneral.gloLab_BillingType = GetPatientBillingType(opatient);
                                //    if (ConfirmNull(clsEmdeonGeneral.gloLab_BillingType.ToString()))
                                //    {
                                //        if (ConfirmNull(clsEmdeonGeneral.gloLab_providerRefval.ToString()))
                                //        {
                                //            oLabLayer.ProcessRequest("payment_update", opatient);
                                //            gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                //            _blnSuccess = true;
                                //        }
                                //        else
                                //        {
                                //            clsEmdeonGeneral.sgloLab_Value = "";
                                //            strFileName = oLabLayer.ProcessRequest("payment_put", opatient);
                                //            clsEmdeonGeneral.sgloLab_Value = ExtractReferanceVal(strFileName, "personprovinfo");
                                //            if (ConfirmNull(clsEmdeonGeneral.sgloLab_Value.ToString()))
                                //            {
                                //                gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                //                clsEmdeonGeneral.gloLab_providerRefval = clsEmdeonGeneral.sgloLab_Value;
                                //                _blnSuccess = true;
                                //            }
                                //        }
                                //    }
                                //    if (_blnSuccess == true)
                                //    {
                                //        //Validating Required information for registering ISP.
                                //        _blnValidatePatinet = ValidatePatient("igsp_put", opatient);
                                //        if ((clsEmdeonGeneral.gloLab_BillingType != "P") && (clsEmdeonGeneral.gloLab_BillingType != "C"))
                                //        {
                                //            if (ConfirmNull(clsEmdeonGeneral.gloLab_ispRefval.ToString()))
                                //            {
                                //                oLabLayer.ProcessRequest("igsp_update", opatient);
                                //                gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                //                _blnSuccess = true;
                                //            }
                                //            else
                                //            {
                                //                clsEmdeonGeneral.sgloLab_Value = "";
                                //                strFileName = oLabLayer.ProcessRequest("igsp_put", opatient);
                                //                clsEmdeonGeneral.sgloLab_Value = ExtractReferanceVal(strFileName, "isp");
                                //                if (ConfirmNull(clsEmdeonGeneral.sgloLab_Value.ToString()))
                                //                {
                                //                    gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                //                    clsEmdeonGeneral.gloLab_ispRefval = clsEmdeonGeneral.sgloLab_Value;
                                //                    _blnSuccess = true;
                                //                }

                                //            }
                                //            if (ConfirmNull(clsEmdeonGeneral.gloLab_ispRefval.ToString()) && ConfirmNull(clsEmdeonGeneral.gloLab_personRefval.ToString()))
                                //            {
                                //                if (opatient.InsuranceDetails.InsurancesDetails.Count > 0)
                                //                {
                                //                    if (ConfirmNull(clsEmdeonGeneral.gloLab_insuranceRefval.ToString()))
                                //                    {
                                //                        oLabLayer.ProcessRequest("insurance_update", opatient);
                                //                        gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                //                        _blnSuccess = true;
                                //                    }
                                //                    else
                                //                    {
                                //                        clsEmdeonGeneral.sgloLab_Value = "";
                                //                        strFileName = oLabLayer.ProcessRequest("Insurance_add", opatient);
                                //                        clsEmdeonGeneral.sgloLab_Value = ExtractReferanceVal(strFileName, "insurance");
                                //                        if (ConfirmNull(clsEmdeonGeneral.sgloLab_Value.ToString()))
                                //                        {
                                //                            gloLabRegisterDB(clsEmdeonGeneral.gloLab_patID, clsEmdeonGeneral.gloLab_patcode, clsEmdeonGeneral.sgloLabtype, clsEmdeonGeneral.sgloLab_Value);
                                //                            clsEmdeonGeneral.gloLab_insuranceRefval = clsEmdeonGeneral.sgloLab_Value;
                                //                            _blnSuccess = true;
                                //                        }
                                //                    }
                                //                } ///Null insurance values
                                //            }//Null "ISP" && "Person Referance"
                                //        }//Billing Type.. "P" && "c"
                                //    }//Blnsuccess
                                //}//PersonRefernce && HSI 
                                #endregion InsuranceInfo

                            }//Validate Patinet
                        } //PatientCodeValidaion
                        else
                        {
                            _blnSuccess = false;
                        }
                    }
                    else
                    {

                    }
                    //blnsucess
                }
                else //confroming... Internet connectivity.
                {

                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blnSuccess = false;
            }

            return _blnSuccess;
        }


        #endregion RegisterPatientToEmdeon
        //Validate required field to send information for emdeon
        public Boolean ValidatePatient(string validate, Patient lpatient)
        {
            //Patient lpatient=new Patient();

            Boolean _blncheckv = false;
            try
            {
                switch (validate)
                {
                    case "hsilabel_search":
                        // Facility code Mandatory
                        if ((ConfirmNull(clsEmdeonGeneral.emdeonFacilityCode.ToString()) == true))
                        {
                            _blncheckv = true;

                        }
                        else
                        {
                            //objClsGeneral.UpdateLog("Patient cannot registerd due to null facility code");

                        }
                        break;
                    case "person_put":
                        if ((ConfirmNull(lpatient.DemographicsDetail.PatientFirstName.ToString())) == true)
                        {
                            if ((ConfirmNull(lpatient.DemographicsDetail.PatientLastName.ToString())) && (ConfirmNull(lpatient.DemographicsDetail.PatientGender.ToString())))
                            {
                                _blncheckv = true;
                            }
                            else
                            {
                                _blncheckv = false;
                            }
                        }
                        else
                        {
                            _blncheckv = false;
                            //objClsGeneral.UpdateLog("Patient cannot be registerd due to null Patient Patient Demographics");
                        }

                        break;
                    case "guarantor_put":
                        if (bIsgloServices)
                        {
                            _blncheckv = ValidateGuarantor(lpatient);
                        }
                        else
                        {
                            SelectGuarantor(lpatient);

                            if (ConfirmNull(lpatient.DemographicsDetail.PatientCode.ToString()) == true)
                            {

                                if ((lpatient.PatientGuarantors.Count > 0) && ConfirmNull(lpatient.PatientGuarantors[0].LastName.ToString()) && ConfirmNull(lpatient.PatientGuarantors[0].Relation.ToString()) && ConfirmNull(lpatient.PatientGuarantors[0].FirstName.ToString()))
                                    _blncheckv = true;
                                else
                                {
                                    _blncheckv = false;
                                    //objClsGeneral.UpdateLog("Guarantors information cannot be registerd due to null Guarantors Last Name");
                                }
                            }
                            else
                            {
                                //objClsGeneral.UpdateLog("Patient cannot be registerd due to null Persons ID");
                                _blncheckv = false;

                            }
                        }
                       
                        break;

                    case "isp_put":
                        if (lpatient.InsuranceDetails.InsurancesDetails.Count > 0)
                        {
                            if (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].AddrressLine1.ToString()) && ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].City.ToString()) && ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].InsuranceName.ToString()))
                                _blncheckv = true;
                            else
                            {
                                _blncheckv = false;
                            }
                        }
                        else
                            _blncheckv = false;
                        break;
                    case "Insurance_add":

                        _blncheckv = getInsuranceFlag(lpatient);
                        break;
                    //if ((lpatient.InsuranceDetails.InsurancesDetails.Count > 0) && (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].SubscriberLName.ToString())) && (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].SubscriberFName.ToString())) && ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].SubscriberID.ToString()))
                    //{
                    //    if (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].AddrressLine1.ToString()) && (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].ZIP.ToString())) && (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString())))
                    //    {
                    //        if (ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].SubscriberID.ToString()) && ConfirmNull(lpatient.InsuranceDetails.InsurancesDetails[0].PayerID.ToString()) && (lpatient.InsuranceDetails.InsurancesDetails[0].InsuranceFlag != 0))
                    //        {
                    //            _blncheckv = true;
                    //        }
                    //        else
                    //        {
                    //            _blncheckv = false;
                    //            objClsGeneral.UpdateLog("Insurance Information cannot be registerd due to null subscriberID information");
                    //        }
                    //    }
                    //    else
                    //    {
                    //        _blncheckv = false;
                    //        objClsGeneral.UpdateLog("Insurance Information cannot be registerd due to null address information");
                    //    }

                    //}
                    //else
                    //{
                    //    _blncheckv = false;
                    //    objClsGeneral.UpdateLog("Insurance Information cannot be registerd due to null First Name,Last Name");
                    //}
                    //break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _blncheckv = false;
            }

            return _blncheckv;

        }

        //Sanjog
        public int SelectGuarantor(gloPatient.Patient oPat)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString.Trim());
            DataTable dtGuarantors = new DataTable();
            Int64 _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            // Problem No : 00001126 (Incident : 61178 Bad Guarantor issue)
            clsEmdeonGeneral.gloLab_BillGuarantorAcntID = 0;
            Int64 nPAcnt = 0;
            Int64 PatID = 0;
            PatID = oPat.PatientAccounts[0].PatientID;
            try
            {
                if (oPat.PatientAccounts.Count > 0)
                {
                    string strQry = "SELECT DISTINCT * FROM Patient_OtherContacts WHERE nPAccountID IN (SELECT nPAccountID  FROM PA_Accounts_Patients WHERE nPatientID =" + PatID + ")";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strQry, out dtGuarantors);
                    if (dtGuarantors.Rows.Count > 1)
                    {
                        gloEmdeonInterface.Forms.frmSelectGuarantor ofrm = new Forms.frmSelectGuarantor(PatID, oPat, clsEmdeonGeneral.sConnectionString.Trim());
                        //ofrm.StartPosition=
                        ofrm.ShowDialog(ofrm.Parent);
                        nPAcnt = ofrm.nPAcnt;
                        ofrm.Dispose();
                        ofrm = null;
                    }
                }
                if (nPAcnt > 0)
                {
                    clsEmdeonGeneral.gloLab_BillGuarantorAcntID = nPAcnt;
                    string _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                    + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                    + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                    + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                    + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                    + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                    + " FROM Patient_OtherContacts WHERE ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountId =" + nPAcnt + " and bIsAccountGuarantor = 1 ORDER BY nPatientContactTypeFlag";

                    oDB.Retrive_Query(_strSqlQuery, out dtGuarantors);
                    if (dtGuarantors != null && dtGuarantors.Rows.Count > 0)
                    {
                        oPat.PatientGuarantors.Clear();
                        for (int i = 0; i < dtGuarantors.Rows.Count; i++)
                        {
                            gloPatient.PatientOtherContact oGuarantor = new gloPatient.PatientOtherContact();

                            //Account guarantor
                            oGuarantor.PatientID = PatID;
                            oGuarantor.PatientContactID = Convert.ToInt64(dtGuarantors.Rows[i]["nPatientContactID"]);
                            oGuarantor.FirstName = Convert.ToString(dtGuarantors.Rows[i]["sFirstName"]);
                            oGuarantor.MiddleName = Convert.ToString(dtGuarantors.Rows[i]["sMiddleName"]);
                            oGuarantor.LastName = Convert.ToString(dtGuarantors.Rows[i]["sLastName"]);
                            if (dtGuarantors.Rows[i]["nDOB"] != null && dtGuarantors.Rows[i]["nDOB"].ToString() != "")
                            {
                                oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantors.Rows[i]["nDOB"]));
                            }
                            oGuarantor.SSN = Convert.ToString(dtGuarantors.Rows[i]["sSSN"]);
                            if (Convert.ToInt64(dtGuarantors.Rows[i]["nPatientID"]) == PatID)
                            {
                                oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                            }
                            else
                            {
                                oGuarantor.Relation = "Other";
                            }

                            //oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                            oGuarantor.Gender = Convert.ToString(dtGuarantors.Rows[i]["sGender"]);
                            oGuarantor.AddressLine1 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine1"]);
                            oGuarantor.AddressLine2 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine2"]);
                            oGuarantor.City = Convert.ToString(dtGuarantors.Rows[i]["sCity"]);
                            oGuarantor.State = Convert.ToString(dtGuarantors.Rows[i]["sState"]);

                            oGuarantor.County = Convert.ToString(dtGuarantors.Rows[i]["sCounty"]);
                            oGuarantor.Country = Convert.ToString(dtGuarantors.Rows[i]["sCountry"]);

                            oGuarantor.Zip = Convert.ToString(dtGuarantors.Rows[i]["sZIP"]);
                            oGuarantor.Phone = Convert.ToString(dtGuarantors.Rows[i]["sPhone"]);
                            oGuarantor.Mobile = Convert.ToString(dtGuarantors.Rows[i]["sMobile"]);
                            oGuarantor.Email = Convert.ToString(dtGuarantors.Rows[i]["sEmail"]);
                            oGuarantor.Fax = Convert.ToString(dtGuarantors.Rows[i]["sFax"]);
                            oGuarantor.IsActive = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsActive"]);
                            oGuarantor.VisitID = Convert.ToInt64(dtGuarantors.Rows[i]["nVisitID"]);
                            oGuarantor.AppointmentID = Convert.ToInt64(dtGuarantors.Rows[i]["nAppointmentID"]);
                            oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                            oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                            oGuarantor.OtherConatctType = (gloPatient.PatientOtherContactType)Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactType"]);
                            oGuarantor.PAccountID = Convert.ToInt64(oPat.PatientAccounts[0].PAccountID);
                            oGuarantor.GurantorType = (gloPatient.GuarantorType)Convert.ToInt32(dtGuarantors.Rows[i]["nGuarantorType"]);
                            oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsAccountGuarantor"]);
                            oPat.PatientGuarantors.Add(oGuarantor);
                        }
                    }
                }
                else
                {
                    //MessageBox.Show("Patient is not associated with an Account.", _messageBoxCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            catch //(Exception exc)
            {

            }
            finally
            {
                if (dtGuarantors != null)
                {
                    dtGuarantors.Dispose();
                    dtGuarantors = null;
                }
                if (oDB != null)
                {
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return 0;
        }
        //Sanjog

        public bool ValidateGuarantor(gloPatient.Patient oPat)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString.Trim());
            DataTable dtGuarantors = null;
            DataTable dtPAccounts = null;
            //Int64 _ClinicID = Convert.ToInt64(appSettings["ClinicID"]);
            Int64 _ClinicID = 1;
            // Problem No : 00001126 (Incident : 61178 Bad Guarantor issue)
            clsEmdeonGeneral.gloLab_BillGuarantorAcntID = 0;
            Int64 PatID = 0;
            PatID = oPat.PatientAccounts[0].PatientID;
            bool _blncheckv = false;

            try
            {
                if (oPat.PatientAccounts.Count > 0)
                {
                    dtPAccounts = new DataTable();

                    string strQry = "SELECT DISTINCT * FROM Patient_OtherContacts WHERE nPAccountID IN (SELECT nPAccountID  FROM PA_Accounts_Patients WHERE nPatientID =" + PatID + ")";
                    oDB.Connect(false);
                    oDB.Retrive_Query(strQry, out dtPAccounts);
                    
                    if (dtPAccounts != null && dtPAccounts.Rows.Count > 1)
                    {
                        oPat.PatientGuarantors.Clear();

                        for (int iCnt = 0; iCnt < dtPAccounts.Rows.Count; iCnt++)
                        {
                            dtGuarantors = new DataTable();

                            clsEmdeonGeneral.gloLab_BillGuarantorAcntID = Convert.ToInt64(dtPAccounts.Rows[iCnt]["nPAccountID"]);

                            string _strSqlQuery = "SELECT nPatientID, nPatientContactID, nLineNumber,ISNULL(nPatientContactType,0) AS nPatientContactType, "
                            + " ISNULL(sFirstName,'') AS sFirstName,ISNULL(sMiddleName,'') AS sMiddleName,ISNULL(sLastName,'') AS sLastName,"
                            + " nDOB,ISNULL(sSSN,'') AS sSSN,ISNULL(sGender,'') AS sGender,ISNULL(sRelation,'') As sRelation,ISNULL(sAddressLine1,'') As sAddressLine1,"
                            + " ISNULL(sAddressLine2,'') AS sAddressLine2,ISNULL(sCity,'') AS sCity,ISNULL(sState,'') AS sState,ISNULL(sZIP,'') AS sZIP,ISNULL(sCounty,'') AS sCounty,ISNULL(sCountry,'') AS sCountry,"
                            + " ISNULL(sPhone,'') AS sPhone,ISNULL(sMobile,'') AS sMobile,ISNULL(sFax,'') AS sFax,ISNULL(sEmail,'') AS sEmail,"
                            + " ISNULL(nVisitID,0) AS nVisitID,ISNULL(nAppointmentID,0) As nAppointmentID,ISNULL(bIsActive,'false') As  bIsActive,ISNULL(nGuarantorAsPatientID,0) AS nGuarantorAsPatientID,ISNULL(nPatientContactTypeFlag,4) AS nPatientContactTypeFlag,nClinicID,ISNULL(nGuarantorType,1) as nGuarantorType,bIsAccountGuarantor"
                            + " FROM Patient_OtherContacts WHERE ISNULL(nClinicID,1) = " + _ClinicID + " AND nPAccountId =" + Convert.ToInt64(dtPAccounts.Rows[iCnt]["nPAccountID"]) + " and bIsAccountGuarantor = 1 ORDER BY nPatientContactTypeFlag";

                            oDB.Retrive_Query(_strSqlQuery, out dtGuarantors);

                            if (dtGuarantors != null && dtGuarantors.Rows.Count > 0)
                            {
                                for (int i = 0; i < dtGuarantors.Rows.Count; i++)
                                {
                                    gloPatient.PatientOtherContact oGuarantor = new gloPatient.PatientOtherContact();

                                    //Account guarantor
                                    oGuarantor.PatientID = PatID;
                                    oGuarantor.PatientContactID = Convert.ToInt64(dtGuarantors.Rows[i]["nPatientContactID"]);
                                    oGuarantor.FirstName = Convert.ToString(dtGuarantors.Rows[i]["sFirstName"]);
                                    oGuarantor.MiddleName = Convert.ToString(dtGuarantors.Rows[i]["sMiddleName"]);
                                    oGuarantor.LastName = Convert.ToString(dtGuarantors.Rows[i]["sLastName"]);
                                    if (dtGuarantors.Rows[i]["nDOB"] != null && dtGuarantors.Rows[i]["nDOB"].ToString() != "")
                                    {
                                        oGuarantor.DOB = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(dtGuarantors.Rows[i]["nDOB"]));
                                    }
                                    oGuarantor.SSN = Convert.ToString(dtGuarantors.Rows[i]["sSSN"]);
                                    if (Convert.ToInt64(dtGuarantors.Rows[i]["nPatientID"]) == PatID)
                                    {
                                        oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                                    }
                                    else
                                    {
                                        oGuarantor.Relation = "Other";
                                    }

                                    //oGuarantor.Relation = Convert.ToString(dtGuarantors.Rows[i]["sRelation"]);
                                    oGuarantor.Gender = Convert.ToString(dtGuarantors.Rows[i]["sGender"]);
                                    oGuarantor.AddressLine1 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine1"]);
                                    oGuarantor.AddressLine2 = Convert.ToString(dtGuarantors.Rows[i]["sAddressLine2"]);
                                    oGuarantor.City = Convert.ToString(dtGuarantors.Rows[i]["sCity"]);
                                    oGuarantor.State = Convert.ToString(dtGuarantors.Rows[i]["sState"]);

                                    oGuarantor.County = Convert.ToString(dtGuarantors.Rows[i]["sCounty"]);
                                    oGuarantor.Country = Convert.ToString(dtGuarantors.Rows[i]["sCountry"]);

                                    oGuarantor.Zip = Convert.ToString(dtGuarantors.Rows[i]["sZIP"]);
                                    oGuarantor.Phone = Convert.ToString(dtGuarantors.Rows[i]["sPhone"]);
                                    oGuarantor.Mobile = Convert.ToString(dtGuarantors.Rows[i]["sMobile"]);
                                    oGuarantor.Email = Convert.ToString(dtGuarantors.Rows[i]["sEmail"]);
                                    oGuarantor.Fax = Convert.ToString(dtGuarantors.Rows[i]["sFax"]);
                                    oGuarantor.IsActive = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsActive"]);
                                    oGuarantor.VisitID = Convert.ToInt64(dtGuarantors.Rows[i]["nVisitID"]);
                                    oGuarantor.AppointmentID = Convert.ToInt64(dtGuarantors.Rows[i]["nAppointmentID"]);
                                    oGuarantor.GuarantorAsPatientID = Convert.ToInt64(dtGuarantors.Rows[i]["nGuarantorAsPatientID"]);
                                    oGuarantor.nGuarantorTypeFlag = Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactTypeFlag"]);
                                    oGuarantor.OtherConatctType = (gloPatient.PatientOtherContactType)Convert.ToInt32(dtGuarantors.Rows[i]["nPatientContactType"]);
                                    oGuarantor.PAccountID = Convert.ToInt64(oPat.PatientAccounts[0].PAccountID);
                                    oGuarantor.GurantorType = (gloPatient.GuarantorType)Convert.ToInt32(dtGuarantors.Rows[i]["nGuarantorType"]);
                                    oGuarantor.IsAccountGuarantor = Convert.ToBoolean(dtGuarantors.Rows[i]["bIsAccountGuarantor"]);
                                    oPat.PatientGuarantors.Add(oGuarantor);
                                }
                            }
                        }
                    }
                }
                
                if (ConfirmNull(oPat.DemographicsDetail.PatientCode.ToString()) == true)
                {
                    if (oPat.PatientGuarantors.Count > 0)
                    {
                        for (int j = 0; j < oPat.PatientGuarantors.Count; j++)
                        {
                            if (ConfirmNull(oPat.PatientGuarantors[j].LastName.ToString()) && ConfirmNull(oPat.PatientGuarantors[j].Relation.ToString()) && ConfirmNull(oPat.PatientGuarantors[j].FirstName.ToString()) && ConfirmNull(oPat.PatientGuarantors[j].Zip.ToString()) && ConfirmNull(oPat.PatientGuarantors[j].City.ToString()) && ConfirmNull(oPat.PatientGuarantors[j].Phone.ToString()) && ConfirmNull(oPat.PatientGuarantors[j].State.ToString()))
                            {
                                _blncheckv = true;
                                return _blncheckv;
                            }
                        }
                    }
                }
                
            }

            catch //(Exception exc)
            {

            }
            finally
            {
                if (dtPAccounts != null)
                {
                    dtPAccounts.Dispose();
                    dtPAccounts = null;
                }
                if (dtGuarantors != null)
                {
                    dtGuarantors.Dispose();
                    dtGuarantors = null;
                }
                if (oDB != null)
                {
                    oDB.Disconnect();
                    oDB.Dispose();
                    oDB = null;
                }

            }
            return _blncheckv;
        }


        #region Commented old logic
        //public bool getInsuranceFlag(Patient oPatient)
        //{
        //    bool _returnFlag = false;
        //    try
        //    {
        //        if (oPatient.InsuranceDetails.InsurancesDetails.Count > 0)
        //        {
        //            for (int i = 0; i < oPatient.InsuranceDetails.InsurancesDetails.Count; i++)
        //            {
        //                if (checkInsuranceFlag(oPatient, i, 1))
        //                {
        //                    clsEmdeonGeneral.gloLab_insuranceIndex = i;
        //                    _returnFlag = true;
        //                    break;
        //                }
        //                else if (checkInsuranceFlag(oPatient, i, 2))
        //                {
        //                    clsEmdeonGeneral.gloLab_insuranceIndex = i;
        //                    _returnFlag = true;
        //                    break;
        //                }
        //                else if (checkInsuranceFlag(oPatient, i, 3))
        //                {
        //                    clsEmdeonGeneral.gloLab_insuranceIndex = i;
        //                    _returnFlag = true;
        //                    break;
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //        return false;

        //    }
        //    return _returnFlag;
        //} 
        #endregion

        public bool getInsuranceFlag(Patient oPatient)
        {
            bool _returnFlag = false;
            bool _primaryFound = false;
            bool _SecondaryFound = false;
            bool _TertiaryFound = false;
            try
            {
                if (oPatient.InsuranceDetails.InsurancesDetails.Count > 0)
                {
                    for (int i = 0; i < oPatient.InsuranceDetails.InsurancesDetails.Count; i++)
                    {
                        if (_primaryFound == false)
                        {
                            if (checkInsuranceFlag(oPatient, i, 1))
                            {
                                clsEmdeonGeneral.gloLab_insuranceIndex = i;
                                _returnFlag = true;
                                _primaryFound = true;
                            }
                            else
                            {
                                clsEmdeonGeneral.gloLab_insuranceIndex = -1;
                            }
                        }
                        //Secondary
                        if (_SecondaryFound == false)
                        {
                            if (checkInsuranceFlag(oPatient, i, 2))
                            {
                                if (clsEmdeonGeneral.gloLab_insuranceIndex == -1)
                                {
                                    clsEmdeonGeneral.gloLab_insuranceIndex = i;
                                    clsEmdeonGeneral.gloLab_SecondaryinsuranceIndex = -1;
                                    _returnFlag = true;
                                    _primaryFound = true;
                                    _SecondaryFound = true;
                                }
                                else
                                {
                                    clsEmdeonGeneral.gloLab_SecondaryinsuranceIndex = i;
                                    _returnFlag = true;
                                    _SecondaryFound = true;                                    
                                }
                            }
                            else
                            {
                                clsEmdeonGeneral.gloLab_SecondaryinsuranceIndex = -1;
                            }
                        }
                        //Tertiary
                        if (_TertiaryFound == false)
                        {
                            if (checkInsuranceFlag(oPatient, i, 3))
                            {

                                if (clsEmdeonGeneral.gloLab_insuranceIndex == -1)
                                {
                                    clsEmdeonGeneral.gloLab_insuranceIndex = i;
                                    clsEmdeonGeneral.gloLab_TertiaryinsuranceIndex = -1;
                                    _returnFlag = true;
                                    _SecondaryFound = true;
                                    _TertiaryFound = true;
                                }
                                else
                                {
                                    if (clsEmdeonGeneral.gloLab_SecondaryinsuranceIndex == -1)
                                    {
                                        clsEmdeonGeneral.gloLab_SecondaryinsuranceIndex = i;
                                        clsEmdeonGeneral.gloLab_TertiaryinsuranceIndex = -1;
                                        _returnFlag = true;
                                        _TertiaryFound = true;
                                    }
                                    else
                                    {
                                        clsEmdeonGeneral.gloLab_TertiaryinsuranceIndex = i;
                                        _returnFlag = true;
                                        _TertiaryFound = true;
                                    }
                                }
                            }
                            else
                            {
                                clsEmdeonGeneral.gloLab_TertiaryinsuranceIndex = -1;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;

            }
            return _returnFlag;
        }
        private bool checkInsuranceFlag(Patient oPatient, int _index, int InsuranceFlag)
        {
            try
            {
                if (oPatient.InsuranceDetails.InsurancesDetails[_index].InsuranceFlag != 0 && oPatient.InsuranceDetails.InsurancesDetails[_index].InsuranceFlag == InsuranceFlag)
                {
                    if (validateInsurance(oPatient, _index))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }

        }

        private bool validateInsurance(Patient oPatient, int _insuranceIndex)
        {
            try
            {
                if (bIsgloServices)
                {
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].ContactID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberID.ToString()) && oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].InsuranceFlag != 0) //added by manoj on 20120524 to add ncontactid insted of payer id  in third party billing validation version-7005
                    {
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].AddrressLine1.ToString()) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberZip.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].Phone.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberCity.ToString())) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberState.ToString()))
                        {
                            clsEmdeonGeneral.gloLab_insuranceFlag = oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].InsuranceFlag;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].PayerID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].Group.ToString()) && oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].InsuranceFlag != 0) //commnted by manoj on 20120524 to remove payer id validation from third party billing validation version-7005
                    if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].ContactID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].Group.ToString()) && oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].InsuranceFlag != 0) //added by manoj on 20120524 to add ncontactid insted of payer id  in third party billing validation version-7005
                    {
                        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].AddrressLine1.ToString()) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberZip.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].Phone.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberCity.ToString())) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].SubscriberState.ToString()))
                        {
                            clsEmdeonGeneral.gloLab_insuranceFlag = oPatient.InsuranceDetails.InsurancesDetails[_insuranceIndex].InsuranceFlag;
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }

               

                #region Insurance not used
                //switch (_insuranceType)
                //{
                //    case 0:
                //        if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].PayerID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Group.ToString()) && oPatient.InsuranceDetails.InsurancesDetails[0].InsuranceFlag == 1)
                //        {
                //            if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].AddrressLine1.ToString()) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].Phone.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberCity.ToString())) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[0].SubscriberState.ToString()))
                //            {
                //                return true;
                //            }
                //            else
                //            {
                //                return false;
                //            }
                //        }
                //        else
                //            return false;
                //        break;
                //    case 1:
                //        if (oPatient.InsuranceDetails.InsurancesDetails[1] != null)
                //        {
                //            if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].PayerID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].SubscriberID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].Group.ToString()) && oPatient.InsuranceDetails.InsurancesDetails[1].InsuranceFlag==2)
                //            {
                //                if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].AddrressLine1.ToString()) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].SubscriberZip.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].Phone.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].SubscriberCity.ToString())) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[1].SubscriberState.ToString()))
                //                {
                //                    return true;
                //                }
                //                else
                //                    return false;
                //            }
                //            else
                //                return false;
                //        }
                //        else
                //            return false;
                //    case 2:
                //        if (oPatient.InsuranceDetails.InsurancesDetails[2] != null)
                //        {
                //            if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].PayerID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].SubscriberID.ToString()) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].Group.ToString()) && oPatient.InsuranceDetails.InsurancesDetails[2].InsuranceFlag ==3)
                //            {
                //                if (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].AddrressLine1.ToString()) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].SubscriberZip.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].Phone.ToString())) && (ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].SubscriberCity.ToString())) && ConfirmNull(oPatient.InsuranceDetails.InsurancesDetails[2].SubscriberState.ToString()))
                //                {
                //                    return true;
                //                }
                //                else
                //                    return false;
                //            }
                //            else
                //                return false;
                //        }
                //        else
                //            return false;


                //    default:
                //        return false;
                //} 
                #endregion


            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                return false;
            }
            return false;

        }
        //Null Value check
        protected bool ConfirmNull(string checkValue)
        {

            bool _blnvCheck = false;
            try
            {
                if (checkValue != null && checkValue.ToString().Trim().Length != 0 && checkValue.ToString() != "")
                {
                    _blnvCheck = true;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return _blnvCheck;
        }


        //checks the type of Patinet BillingType....

        public string GetPatientBillingType(Patient opatient)
        {

            string payType = string.Empty;

            try
            {
                //Checks whether the insurance information is present or not?

                //If insurance information is present then considers as "third party" 
                //If insurance flag is "0"-It is considerd as inactive... so insurance information cannot be sended to emdeon.
                //if ((opatient.InsuranceDetails.InsurancesDetails.Count > 0) && (opatient.InsuranceDetails.InsurancesDetails[0].InsuranceFlag !=0)&& ConfirmNull(opatient.InsuranceDetails.InsurancesDetails[0].SubscriberAddr1.ToString()) && ConfirmNull(opatient.InsuranceDetails.InsurancesDetails[0].SubscriberZip.ToString()) && ConfirmNull(opatient.InsuranceDetails.InsurancesDetails[0].RelationshipName.ToString()) && ConfirmNull(opatient.InsuranceDetails.InsurancesDetails[0].PayerID.ToString()))
                if ((ValidatePatient("Insurance_add", opatient)) && ValidatePatient("person_put", opatient) == true)
                {

                    //conforming payment type as third party--- "T"--
                    payType = "t";
                    //updateing emdeon log file. 
                    //objclsGeneral.UpdateLog("Patient Code " + opatient.DemographicsDetail.PatientCode.ToString() + " Payment type: Third party");

                }
                else if (ValidatePatient("person_put", opatient) && ValidatePatient("guarantor_put", opatient) && ConfirmNull(opatient.DemographicsDetail.PatientPhone.ToString()) && (bIsgloServices || (ConfirmNull(opatient.PatientGuarantors[0].Zip.ToString()) && ConfirmNull(opatient.PatientGuarantors[0].City.ToString()) && ConfirmNull(opatient.PatientGuarantors[0].Phone.ToString()) && ConfirmNull(opatient.PatientGuarantors[0].State.ToString()))))
                {

                    //Checks wether the patient address information is present....
                    //If patient address information is present then considers as "Patient"
                    payType = "p";
                    //objclsGeneral.UpdateLog("Patient Code " + opatient.DemographicsDetail.PatientCode.ToString() + " Payment type: Patient");

                }
                else
                {
                    //If there is no insurance Address information,patient address information, then we consider as payment type as "clinet"
                    payType = "c";
                    //objclsGeneral.UpdateLog("Patient Code " + opatient.DemographicsDetail.PatientCode.ToString() + " Payment type: Client");
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
            }
            return payType;
        }
        //This code is added for selecting the type of relation ship.. for emdeon...
        public string GetInsuranceRelationshipType(string relType)
        {

            string _relationType = string.Empty;
            try
            {
                switch (relType.ToLower())
                {
                    case "self":
                        _relationType = "18";
                        break;
                    case "spouse":
                        _relationType = "01";
                        break;
                    case "child":
                        _relationType = "19";
                        break;
                    case "father":
                        _relationType = "33";
                        break;
                    case "daughter":
                        _relationType = "62";
                        break;
                    case "mother":
                        _relationType = "32";
                        break;
                    case "employee":
                        _relationType = "20";
                        break;
                    case "life partner":
                        _relationType = "65";
                        break;
                    default:
                        _relationType = "99";
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _relationType = string.Empty;
            }
            return _relationType;
        }
        //this code is added for selecting the status of employee
        public string GetEmployeeStatus(string stsType)
        {
            string _stsType = string.Empty;

            try
            {
                switch (stsType.ToLower())
                {
                    case "employed":
                        _stsType = "A";
                        break;
                    case "retired":
                        _stsType = "R";
                        break;
                    default:
                        _stsType = "zz";
                        break;
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _stsType = string.Empty;
            }
            return _stsType;

        }
        //Extracting reference value from XML file...
        public string ExtractReferanceVal(string strFileName, string strRefVal)
        {
            string _refValue = string.Empty;
            DataSet dsReference = new DataSet();
            try
            {
                if (ConfirmNull(strFileName.ToString()))
                {
                    dsReference.ReadXml(strFileName);
                    if (dsReference.Tables["OBJECT"] != null)
                    {
                        if (dsReference.Tables[1].Rows.Count > 0)
                        {
                            _refValue = dsReference.Tables[1].Rows[0][strRefVal].ToString();
                        }
                        else
                        {
                            _refValue = string.Empty;
                        }
                    }
                    else
                    {
                        if (dsReference.Tables["ERROR"] != null)
                        {
                            gloAuditTrail.gloAuditTrail.ExceptionLog("GL-9.2: Recieved XML response file contains Error.", false);
                        }
                        _refValue = string.Empty;
                    }
                }
                else
                {
                    _refValue = string.Empty;
                }
            }
            catch (SqlException ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _refValue = string.Empty;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _refValue = string.Empty;
            }
            finally
            {
                if (File.Exists(strFileName))
                {
                    File.Delete(strFileName);
                }
            }

            return _refValue;
        }
        //Confirming patient code....
        public Boolean GetPatientCodeConfirmation(string _patientCode, Patient opatient)
        {
            Boolean _result = false;
            string strFileName = string.Empty;
            Boolean _blnStatus = false;
            clsEmdeonLabLayer oLabLayer = new clsEmdeonLabLayer();
            gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString.ToString());
            try
            {
                if (ConfirmNull(_patientCode.ToString()))
                {
                    clsEmdeonGeneral.gloLab_patcode = _patientCode.ToString().Trim();
                    strFileName = oLabLayer.ProcessRequest("personhsi_search", opatient);
                    if (ConfirmNull(strFileName.ToString()))
                    {
                        StreamReader testTxt = new StreamReader(strFileName);
                        string allRead = testTxt.ReadToEnd();
                        testTxt.Close();
                        string regMatch = "person";
                        if (Regex.IsMatch(allRead, regMatch))
                        {
                            _blnStatus = true;
                            _result = false;
                        }
                        else
                        {
                            _blnStatus = false;
                            _result = true;
                        }
                        if (_blnStatus == true)
                        {
                            string _StrVal = ExtractReferanceVal(strFileName, "person");
                            if (ConfirmNull(_StrVal.ToString()))
                            {
                                objDbLayer.Connect(false);
                                // string strQry = "select count(*) from patient_gloLab Where sgloLabValue=" + _StrVal.ToString();
                                string strQry = "select count(*) from PatientExternalCodes  where PatientExternalCodes.sExternalType = 'EMDEON' AND   sExternalValue='" + _StrVal.ToString().Trim() + "'AND sExternalSubType='Person'";
                                Int32 _count = 0;
                                _count = Convert.ToInt32(objDbLayer.ExecuteScalar_Query(strQry));
                                if (_count > 0)
                                {
                                    strQry = "select nPatientId from PatientExternalCodes  where PatientExternalCodes.sExternalType = 'EMDEON' AND   sExternalValue='" + _StrVal.ToString().Trim() + "'";
                                    Int64 _cnt = 0;
                                    _cnt = Convert.ToInt64(objDbLayer.ExecuteScalar_Query(strQry));
                                    if (ConfirmNull(clsEmdeonGeneral.gloLab_patID.ToString()) && ConfirmNull(_cnt.ToString()))
                                    {
                                        int _blnStringResult = string.Compare(Convert.ToString(_cnt.ToString()), Convert.ToString(clsEmdeonGeneral.gloLab_patID.ToString()));
                                        if (_blnStringResult == 0)
                                        {
                                            clsEmdeonGeneral.gloLab_Identifier = string.Empty;//Confirmation for "viewgloLab form"
                                            _PatientConfirm = "avialable";
                                        }
                                        else
                                        {
                                            clsEmdeonGeneral.gloLab_Identifier = "Conflict found for the patient at EMDEON for Patient Code: " + _patientCode;//Confirmation for "viewgloLab form"

                                            _PatientConfirm = "notavialable";
                                        }
                                    }
                                    else
                                    {
                                        _blnStatus = false;
                                        _PatientConfirm = "notavialable";
                                    }
                                }
                                else
                                {
                                    //by Abhijeet on 20100512    
                                    if (IsServiceRegistration == false)
                                    {
                                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.RegisterLabPatient, "Patient not registered with external lab service due to duplicate patient code in lab service  ", opatient.PatientID, 0, opatient.DemographicsDetail.PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                    }
                                    clsEmdeonGeneral.gloLab_Identifier = "The Patient Code assigned to '" + opatient.DemographicsDetail.PatientFirstName.ToString() + "  " + opatient.DemographicsDetail.PatientLastName.ToString() + "' is a duplicate of another patient's code which is already in use for the Lab module.\r\n" +
                                                                           "Please modify the patient and change the patient code before you can proceed with lab ordering.";//Confirmation for "viewgloLab form"
                                    gloAuditTrail.gloAuditTrail.ExceptionLog("Conflict found for the patient at EMDEON for Patient Code: " + _patientCode, false);
                                    _PatientConfirm = "notavialable";
                                }
                            }
                            else
                            {
                                _PatientConfirm = string.Empty;
                            }
                        }
                    }
                    else
                        _result = false;
                }


            }
            catch (IOException ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = false;
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _result = false;
            }
            finally
            {
                if (objDbLayer != null)
                {
                    objDbLayer.Dispose();
                }
            }
            return _result;
        }
        //This method returns sessionid.
        //Added by madan-- on 20100414
        public string GetSessionId(string _strFileName)
        {
            string _strSessionId = "";
            Regex reGex = new Regex("\"([^\"]*)\""); //This is used to get the value inside double quotes from xml file...
            try
            {
                StreamReader testTxt = new StreamReader(_strFileName);
                string allRead = testTxt.ReadToEnd();
                testTxt.Close();
                string regMatch = "sessionid";
                if (Regex.IsMatch(allRead, regMatch))
                {
                    foreach (Match match in reGex.Matches(allRead))
                    {
                        _strSessionId = match.Groups[1].Value;

                        break;
                    }
                }
                else
                {
                    _strSessionId = "";
                }
            }
            catch (Exception ex)
            {

                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _strSessionId = "";
            }
            return _strSessionId;
        }

        //////Added by madan on 20100518
        //public bool UnMatchPatientRegeister(Int64 _nPatientId, string _patientCode, Patient oPatient)
        //{
        //    bool _blnResult = false;
        //    string _strResult = string.Empty;
        //    string strFileName = string.Empty;
        //    clsEmdeonLabLayer oLabLayer = new clsEmdeonLabLayer();
        //    gloDatabaseLayer.DBLayer objDbLayer = new gloDatabaseLayer.DBLayer(clsEmdeonGeneral.sConnectionString.ToString());
        //    try
        //    { 
        //        if (ConfirmNull(_patientCode.ToString()))
        //        {
        //            clsEmdeonGeneral.gloLab_patcode = _patientCode.ToString().Trim();
        //            strFileName = oLabLayer.ProcessRequest("personhsi_search", oPatient);
        //            if (ConfirmNull(strFileName.ToString()))
        //            {
        //                StreamReader testTxt = new StreamReader(strFileName);
        //                string allRead = testTxt.ReadToEnd();
        //                testTxt.Close();
        //                string regMatch = "person";
        //                if (Regex.IsMatch(allRead, regMatch))
        //                {
        //                    _blnResult = true;
        //                }
        //                else
        //                {
        //                    _blnResult = false;
        //                }
        //                if (_blnResult)
        //                {
        //                    _strResult = ExtractReferanceVal(strFileName, "person");
        //                    if (ConfirmNull(_strResult.ToString()))
        //                    {
        //                        gloLabRegisterDB(_nPatientId, _patientCode, "person", _strResult);
        //                        _strResult = string.Empty;                               
        //                    }
        //                    _strResult = ExtractReferanceVal(strFileName, "personhsi");
        //                    if (ConfirmNull(_strResult.ToString()))
        //                    {
        //                        gloLabRegisterDB(_nPatientId, _patientCode, "personhsi", _strResult);
        //                        _strResult = string.Empty;
        //                    }

        //                }

        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
        //    }
        //    return _blnResult;
        //}

        //Madan--Added for billing type:20100302
        protected string SetPatientBillingType(gloPatient.Patient opatient)
        {
            string _billingtype = string.Empty;

            try
            {
                _billingtype = GetPatientBillingType(opatient);
                if (_billingtype == "t")
                {
                    /*clsEmdeonGeneral.gloLab_insuranceIndex for this variable value is assigned in clsgloPatientLayer--validateInsurance() method   */
                    if (opatient.InsuranceDetails.InsurancesDetails.Count > 0)
                    {
                        if (opatient.InsuranceDetails.InsurancesDetails[clsEmdeonGeneral.gloLab_insuranceIndex].InsuranceFlag == 2)
                        {
                            //DialogResult drIns = MessageBox.Show("Primary insurance information is not available for the patient,So Would you like to continue with secondary insurance Or territory insurance.", "Labs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            DialogResult drIns = MessageBox.Show("This patient does not have primary insurance policy set up. This \r\n" +
                                                                "practice requires insurance policy to be billed for all labs \r\n\r\n" +
                                                                "Would you like to continue with secondary or tertiary insurance ? \r\n\r\n",
                                                                gstrMessageBoxCaption, MessageBoxButtons.YesNo);
                            if (drIns == DialogResult.Yes)
                            {
                                //by Abhijeet on 20100512                
                                //gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Secondary insurance has sended to lab as primary insurance for placing lab orders ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                _billingtype = "t";
                            }
                            else
                            {
                                _billingtype = "c";
                            }

                        }
                        else if (opatient.InsuranceDetails.InsurancesDetails[clsEmdeonGeneral.gloLab_insuranceIndex].InsuranceFlag == 3)
                        {
                            //DialogResult drIns = MessageBox.Show("Primary & secondary insurance information is not available for the patient,So Would you like to continue with secondary insurance Or territory insurance.", "Labs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            DialogResult drIns = MessageBox.Show("This patient does not have primary or secondary insurance policy set up. This \r\n" +
                                                            "practice requires insurance policy to be billed for all labs \r\n\r\n" +
                                                            "Would you like to continue with  tertiary insurance ? \r\n\r\n",
                                                            gstrMessageBoxCaption, MessageBoxButtons.YesNo);
                            if (drIns == DialogResult.Yes)
                            {
                                //by Abhijeet on 20100512                
                                // gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.Labs, gloAuditTrail.ActivityCategory.CreateLabs, gloAuditTrail.ActivityType.Open, "Teritory insurance has sended to lab as primary insurance for placing lab orders ", _patientID, 0, _PatientProviderID, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                _billingtype = "t";
                            }
                            else
                            {
                                _billingtype = "c";
                            }

                        }
                    }
                    #region NotUsed
                    //if (clsEmdeonGeneral.gloLab_insuranceFlag == 1)
                    //{
                    ////    DialogResult drIns = MessageBox.Show("Primary insurance information is not avilable for the patient,So Would you like to continue with secondary insurance Or teritory insurance.", "Labs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    ////    if (drIns ==DialogResult.Yes)
                    ////    {
                    ////        _billingtype = "T";
                    ////    }
                    ////    else
                    ////    {
                    ////        _billingtype = "c";
                    ////    }
                    //}
                    //else if (clsEmdeonGeneral.gloLab_insuranceFlag == 2)
                    //{
                    //   DialogResult  drIns = MessageBox.Show("Primary insurance information is not avilable for the patient,So Would you like to continue with secondary insurance Or teritory insurance.", "Labs", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //    if (drIns ==DialogResult.Yes)
                    //    {
                    //        _billingtype = "c";
                    //    }
                    //    else
                    //    {
                    //        _billingtype = "";
                    //    }

                    //} 
                    #endregion
                }

            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), false);
                _billingtype = string.Empty;

            }
            return _billingtype;

        }
        public bool CheckBillingType(gloPatient.Patient objpatient)
        {
            //Isurance,Patient address verification for billing 
            string _billingtype = SetPatientBillingType(objpatient);
            clsEmdeonGeneral.gloLab_BillingType = _billingtype;
            bool _billingStatus = false;
            //******************************************************************//
            //Check's the EMRAdmin Settings for billing status.... "ASK","YES","NO"
            //If billing status is "YES" then patient is directly registered to emdeon.
            //else billing status is "NO" then patient is not registered to emdeon.
            //if the billing status is "ASK" then insurance validation,patient address validation,gurantor address validation is done,according to that billing type will be selected.
            //******************************************************************//
            //message box code added by madan- according to the design given by prasad sir..
            //*********
            System.Text.Encoding Encoder = System.Text.ASCIIEncoding.Default;
            Byte[] buffer = new byte[] { (byte)149 };
            string bullet = Encoding.GetEncoding(1252).GetString(buffer);
            /////***********
            if (ConfirmNull(clsEmdeonGeneral.gloLab_BillingStatus.ToString()))
            {
                switch (clsEmdeonGeneral.gloLab_BillingStatus)
                {
                    case "Yes":
                        _billingStatus = true;
                        break;
                    case "No":
                        if (_billingtype != "")
                        {
                            if ((_billingtype != "t") && (_billingtype != "p") && (_billingtype == "c"))
                            {
                                DialogResult dlgResult = MessageBox.Show("This patient does not have an address or insurance policy set up. This \r\n" +
                                                                        "practice requires a patient or an insurance policy to be billed for all labs. \r\n\r\n" +
                                                                        "In order to use Labs for this patient, you must either: \r\n\r\n" +
                                                                        "     " + bullet + " Set up billing information for the patient, or\r\n" +
                                                                        "     " + bullet + " Change the practice setting in the Admin program.",
                                                                        gstrMessageBoxCaption, MessageBoxButtons.OK);
                                if (dlgResult == DialogResult.Yes)
                                {
                                    _billingStatus = true;
                                }
                                else if (dlgResult == DialogResult.No)
                                {
                                    _billingStatus = false;
                                }
                            }
                            else
                            {
                                _billingStatus = true;
                            }
                        }
                        else
                        {
                            _billingStatus = false;
                        }
                        break;
                    case "Ask":
                        if (_billingtype != "")
                        {
                            if ((_billingtype != "t") && (_billingtype != "p") && (_billingtype == "c"))
                            {
                                DialogResult dlgResult = MessageBox.Show("This patient does not have an address or insurance policy set up.\r\n" +
                                                                         "Would you like to proceed with lab ordering?\r\n\r\n" +
                                                                         "Note: The practice will be billed for any tests ordered.", gstrMessageBoxCaption, MessageBoxButtons.YesNo);
                                if (dlgResult == DialogResult.Yes)
                                {
                                    _billingStatus = true;
                                }
                                else if (dlgResult == DialogResult.No)
                                {
                                    MessageBox.Show("In order to use gloLabs for this patient, you must either: \r\n\r\n" +
                                                    "     " + bullet + " Set up billing information for the patient, or\r\n" +
                                                    "     " + bullet + " Change the practice setting in the Admin program.",
                                                    gstrMessageBoxCaption, MessageBoxButtons.OK);
                                    _billingStatus = false;
                                }
                            }
                            else
                            {
                                _billingStatus = true;
                            }
                        }
                        else
                        {
                            _billingStatus = false;
                        }
                        break;
                    default:
                        _billingStatus = true;
                        break;
                }
            }
            else
            {
                if (_billingtype != "" && clsEmdeonGeneral.IsDemoLab==true)
                {
                    if ((_billingtype != "t") && (_billingtype != "p") && (_billingtype == "c"))
                    {
                        DialogResult dlgResult = MessageBox.Show("This patient does not have an address or insurance policy set up.\r\n" +
                                                                 "Would you like to proceed with lab ordering?\r\n\r\n" +
                                                                 "Note: The practice will be billed for any tests ordered.", gstrMessageBoxCaption, MessageBoxButtons.YesNo);
                        if (dlgResult == DialogResult.Yes)
                        {
                            _billingStatus = true;
                        }
                        else if (dlgResult == DialogResult.No)
                        {
                            MessageBox.Show("In order to use gloLabs for this patient, you must either: \r\n\r\n" +
                                            "     " + bullet + " Set up billing information for the patient, or\r\n" +
                                            "     " + bullet + " Change the practice setting in the Admin program.",
                                            gstrMessageBoxCaption, MessageBoxButtons.OK);
                            _billingStatus = false;
                        }
                    }
                    else
                    {
                        _billingStatus = true;
                    }
                }
                else
                {
                    _billingStatus = false;
                }
            }

            return _billingStatus;
        }

        //Added by madan on 20101115-- for HL7

        /// <summary>
        /// This method is used to register patient to Emdoen through HL7.
        /// </summary>
        /// <param name="opatient"></param>
        /// <param name="ConnectionString"></param>
        /// <returns></returns>
        public Boolean RegisterPatientToEmdeonHL7(Patient opatient, string ConnectionString)
        {
            IsServiceRegistration = true;        

            return RegisterGloPatient(opatient, ConnectionString);             
        }
        //end madan...
    }
}