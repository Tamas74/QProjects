using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gloEDI.gloBilling.Common;
using System.Collections.Specialized;
using gloAppointmentBook.Books;

namespace gloEDI
{
    public enum ProviderType
    {
        BillingProvider = 1,
        PayToProvider = 2,
        RefferingProvider = 3,
        RenderingProvider = 4
    } 

    class gloEDIClaimSubmitter
    {

        #region " Variables "

        private ArrayList _arrHCFAData = null;
        private string _databaseconnectionstring = "";
        private string _messageboxcaption = String.Empty;
        private ArrayList _arrSelectedTransactions = null;
        //private Transaction oTransaction = null;
        private Int64 _TransactionId = 0;
        bool IsSecondaryInsurance = false;
        Int64 _ClinicID = 0;
       
        System.Collections.Specialized.NameValueCollection appSettings = System.Configuration.ConfigurationManager.AppSettings;
        private Int64 _PatientID = 0;
        public Int64 PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }
        public ArrayList SelectedTransactions
        {
            get { return _arrSelectedTransactions; }
            set { _arrSelectedTransactions = value; }
        }

        public Int64 TransactionID
        {
            get { return _TransactionId; }
            set { _TransactionId = value; }
        }

        public ArrayList HCFAData
        {
            get { return _arrHCFAData; }
            set { _arrHCFAData = value; }
        }

        //Referral Provider
        private string _ReferralId = "";
        private string _ReferralFName = "";
        private string _ReferralLName = "";
        private string _ReferralMName = "";
        private string _ReferralAddress = "";
        private string _ReferralCity = "";
        private string _ReferralState = "";
        private string _ReferralZIP = "";
        private string _ReferralNPI = "";
        private string _ReferralSSN = "";
        private string _ReferralEmployerID = "";
        private string _ReferralStateMedicalNo = "";
        private string _ReferralTaxonomy = "";

        //Rendering Provider
        private string _RenderingFName = "";
        private string _RenderingLName = "";
        private string _RenderingMName = "";
        private string _RenderingAddress = "";
        private string _RenderingCity = "";
        private string _RenderingState = "";
        private string _RenderingZIP = "";
        private string _RenderingNPI = "";
        private string _RenderingSSN = "";
        private string _RenderingEmployerID = "";
        private string _RenderingStateMedicalNo = "";
        private string _RenderingTaxonomy = "";

        //Billing Provider
        private string _BillingFName = "";
        private string _BillingLName = "";
        private string _BillingMName = "";
        private string _BillingCity = "";
        private string _BillingState = "";
        private string _BillingAddress = "";
        private string _BillingZIP = "";
        private string _BillingNPI = "";
        private string _BillingSSN = "";
        private string _BillingEmployerID = "";
        private string _BillingStateMedicalNo = "";
        private string _BillingTaxonomy = "";
        private string _BillingPhone = "";

        //Submitter
        private string _SubmitterName = "";
        private string _SubmitterContactPersonName = "";
        private string _SubmitterContactPersonNo = "";
        private string _SubmitterCity = "";
        private string _SubmitterState = "";
        private string _SubmitterZIP = "";
        //private string _SubmitterETIN = "";
        private string _SubmitterAddress = "";

        //Receiver
       // private string _ReceiverName = "";
       // private string _ReceiverETIN = "";

        //Subscriber
        private string _SubscriberLName = "";
       // private string _SubscriberInsurancePST = "";
        private string _SubscriberRelationshipCode = "";
        private string _SubscriberInsuranceBelongs = "";
        private string _SubscriberFName = "";
        private string _SubscriberMName = "";
        private string _SubscriberInsuranceID = "";
        private string _SubscriberAddress = "";
        private string _SubscriberGroupID = "";
        private string _SubscriberCity = "";
        private string _SubscriberState = "";
        private string _SubscriberZIP = "";
        private string _SubscriberDOB = "";
        private string _SubscriberGender = "";
        private string _SubscriberPhone = "";
        //Payer
        private string _PayerName = "";
        private string _PayerID = "";
        private string _PayerAddress = "";
        private string _PayerCity = "";
        private string _PayerState = "";
        private string _PayerZip = "";

        private string _PatientAccountNo = "";


        //Facility
        //private string _FacilityCode = "";
        //private string _FacilityName = "";
        //private string _FacilityAddress = "";
        //private string _FacilityCity = "";
        //private string _FacilityState = "";
        //private string _FacilityZip = "";
        //private string _FacilityNPI = "";


        //Other Insurance
        private string _OtherInsuranceSubscriberLName = "";
      //  private string _OtherInsurancePST = "";
        private string _OtherInsuranceType = "";
        private string _OtherInsuranceRelationshipCode = "";
        private string _OtherInsuranceID = "";
        private string _OtherInsuranceGroupID = "";
        private string _OtherInsuranceAddress = "";
        private string _OtherInsuranceSubscriberFName = "";
        private string _OtherInsuranceSubscriberMName = "";
        private string _OtherInsuranceName = "";
        private string _OtherInsurancePayerID = "";
        private string _OtherInsuranceCity = "";
        private string _OtherInsuranceState = "";
        private string _OtherInsuranceZIP = "";
        private string _OtherInsuranceSubscriberDOB = "";
        private string _OtherInsuranceSubscriberGender = "";
       // private string _OtherInsuranceSubscriberPhone = "";

        //ISA and GS Settings
        //private string _SenderID = "";
        //private string _ReceiverID = "";
        //private string _SenderName = "";
        //private string _ReceiverCode = "";


        //Patient Information
        private string _PatientLastName = "";
        private string _PatientFirstName = "";
        private string _PatientMiddleName = "";
        private string _PatientSSN = "";
        private string _PatientGender = "";
        private string _PatientDOB = "";
        private string _PatientAddress = "";
        private string _PatientCity = "";
        private string _PatientState = "";
        private string _PatientZip = "";
        private string _PatientCode = "";
        private string _PatientPhone = "";

        //Prior Authorization Number
        private string _PriorAuthorizationNo = "";


        #endregion " Variables And Procedures "

        private void FillTransactionOnForm(Int64 TransactionId)
        {

        //    gloEDI.gloBilling.gloBilling ogloBilling = new gloEDI.gloBilling.gloBilling(_databaseconnectionstring, "");
        //    TransactionDetails oTranDetails = null;
        //    TransactionLine oTransLine = null;
        //    try
        //    {
        //        if (TransactionId > 0)
        //        {
        //            oTransaction = ogloBilling.GetHCFATransactionDetails(TransactionId, _ClinicID);
        //            if (oTransaction != null)
        //            {
        //                if (oTransaction.Lines.Count > 0)
        //                {
        //                    decimal TotCharges = 0;
        //                    decimal Sum = 0;

        //                    //txtClaimNo.Text = FormattedClaimNumberGeneration(oTransaction.ClaimNo.ToString());
        //                    //txtClaimNo.Tag = Convert.ToString(oTransaction.ClaimNo);
        //                    //txtMaterAppointmentID.Text = Convert.ToString(oTransaction.MasterAppointmentID);
        //                    //txtAppointmentID.Text = Convert.ToString(oTransaction.AppointmentID);
        //                    //txtVisitId.Text = Convert.ToString(oTransaction.VisitID);



        //                    //txtTransactionID.Text = Convert.ToString(oTransaction.TransactionID);

        //                    //FillAllDetails(oTransaction);
        //                    FillInsurances(oTransaction.PatientID);
        //                    FillSubmitterInfo(oTransaction.ClinicID, oTransaction.ProviderID);

        //                    oTranDetails = oTransaction.Transaction_Details;
        //                    #region Billing Provider Info

        //                    txtBillingProviderInfo.Text = oTranDetails.HCFA_ProviderFName.Trim() + " "
        //                      + oTranDetails.HCFA_ProviderMName.Trim() + " " + oTranDetails.HCFA_ProviderLName.Trim()
        //                      + Environment.NewLine + oTranDetails.HCFA_ProviderAddress1.Trim() + Environment.NewLine +
        //                      oTranDetails.HCFA_ProviderCity.Trim() + " " + oTranDetails.HCFA_ProviderState.Trim() + "   " +
        //                      oTranDetails.HCFA_ProviderZip.Trim();
        //                    txtBillingProviderInfo.Tag = Convert.ToString(oTransaction.ProviderID);

        //                    txtBillingProv_a_NPI.Text = oTranDetails.HCFA_ProviderNPI.Trim();

        //                    if (oTranDetails.HCFA_ProviderPhone.Trim() != "")
        //                    {
        //                        txtBillingProviderPhone1.Text = oTranDetails.HCFA_ProviderPhone.Trim().Substring(0, 3);
        //                        txtBillingProviderPhone2.Text = oTranDetails.HCFA_ProviderPhone.Trim().Substring(3, oTranDetails.HCFA_ProviderPhone.Trim().Length - 3);
        //                    }

        //                    if (oTranDetails.HCFA_ProviderEIN.Trim() != "")
        //                    {
        //                        txtFederalTaxID.Text = oTranDetails.HCFA_ProviderEIN.Trim();
        //                        chkFederalTaxID_EIN.Checked = true;
        //                    }
        //                    else if (oTranDetails.HCFA_ProviderSSN.Trim() != "")
        //                    {
        //                        txtFederalTaxID.Text = oTranDetails.HCFA_ProviderSSN.Trim();
        //                        chkFederalTaxID_SSN.Checked = true;
        //                    }

        //                    #endregion Billing Provider Info

        //                    #region Other Details

        //                    //Logic for this to be implemented
        //                    chkAcceptAssignment_Yes.Checked = true;
        //                    txtPayerNameAndAddress.Text = _PayerName.Trim() + " " + _PayerAddress.Trim() + " " + _PayerCity.Trim() + " " + _PayerState.Trim() + " " + _PayerZip.Trim();

        //                    if (oTransaction.OutSideLab == true)
        //                    {
        //                        chkOutsideLab_Yes.Checked = true;
        //                    }
        //                    else if (oTransaction.OutSideLab == false)
        //                    {
        //                        chkOutsideLab_No.Checked = true;
        //                    }

        //                    #endregion Other Details

        //                    #region Facility Information

        //                    txtFacilityInfo.Text =
        //                    oTranDetails.HCFA_FacilityName.Trim() + Environment.NewLine + oTranDetails.HCFA_FacilityAddress1.Trim() + Environment.NewLine +
        //                    oTranDetails.HCFA_FacilityCity.Trim() + " " + oTranDetails.HCFA_FacilityState.Trim() + " " +
        //                    oTranDetails.HCFA_FacilityZip.Trim();
        //                    txtFacility_a_NPI.Text = _FacilityNPI.Trim();

        //                    txtFacilityCode.Text = oTransaction.FacilityCode;
        //                    txtFacilityDescription.Text = oTransaction.FacilityDescription;

        //                    #endregion Facility Information

        //                    #region Patient Information

        //                    txtPatientName.Text = oTranDetails.HCFA_PatientLName.Trim() + " " + oTranDetails.HCFA_PatientFName.Trim()
        //                        + " " + oTranDetails.HCFA_PatientMName.Trim();
        //                    txtPatientCity.Text = oTranDetails.HCFA_PatientCity.Trim();
        //                    txtPatientAddress.Text = oTranDetails.HCFA_PatientAddress1.Trim();
        //                    txtPatientState.Text = oTranDetails.HCFA_PatientState.Trim();
        //                    txtPatientZip.Text = oTranDetails.HCFA_PatientZip.Trim();

        //                    txtPatientName.Tag = oTransaction.PatientID;

        //                    if (oTranDetails.HCFA_PatientPhone.Trim() != "")
        //                    {
        //                        txtPatientTelephone1.Text = oTranDetails.HCFA_PatientPhone.Trim().Substring(0, 3);
        //                        txtPatientTelephone2.Text = oTranDetails.HCFA_PatientPhone.Trim().Substring(3, oTranDetails.HCFA_PatientPhone.Trim().Length - 3);
        //                    }

        //                    if (oTranDetails.HCFA_PatientDOB > 0)
        //                    {
        //                        dtpPatientDOB.Value = gloDateMaster.gloDate.DateAsDate(Convert.ToInt64(oTranDetails.HCFA_PatientDOB));
        //                    }

        //                    if (oTranDetails.HCFA_PatientGender.ToUpper().Trim().Substring(0, 1) == "M")
        //                    {
        //                        chkPatient_Male.Checked = true;
        //                    }
        //                    else if (oTranDetails.HCFA_PatientGender.ToUpper().Trim().Substring(0, 1) == "F")
        //                    {
        //                        chkPatient_Female.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        chkPatient_Male.Checked = false;
        //                        chkPatient_Female.Checked = false;
        //                    }

        //                    dtpPatientSignDate.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.TransactionDate);
        //                    txtPatientSignature.Text = "SIGNATURE ON FILE";

        //                    #endregion Patient Information

        //                    #region Insurance And Insured Person Details

        //                    txtInsuredIdNumber.Text = _SubscriberInsuranceID.Trim();
        //                    txtInsuredInsurancePlanName.Text = _PayerName.Trim();
        //                    txtInsuredInsurancePlanName.Tag = Convert.ToString(_PayerName);

        //                    txtInsuredName.Text = _SubscriberLName.Trim() + " " + _SubscriberFName.Trim() + " " + _SubscriberMName.Trim();
        //                    txtInsuredsAddress.Text = _SubmitterAddress.Trim();
        //                    txtInsuredsCity.Text = _SubscriberCity.Trim();
        //                    txtInsuredsState.Text = _SubscriberState.Trim();
        //                    txtInsuredsZip.Text = _SubscriberZIP.Trim();
        //                    txtInsuredPolicyorFECANo.Text = _SubscriberGroupID.Trim();
        //                    if (_SubscriberPhone.Trim() != "")
        //                    {
        //                        txtInsuredTelephone1.Text = _SubscriberPhone.Trim().Substring(0, 3);
        //                        txtInsuredTelephone2.Text = _SubscriberPhone.Trim().Substring(3, _SubscriberPhone.Trim().Length - 3);
        //                    }

        //                    //Gender for Insurance Subscriber
        //                    if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
        //                    {
        //                        chkInsuredSex_Male.Checked = true;
        //                    }
        //                    else if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
        //                    {
        //                        chkInsuredSex_Female.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        chkInsuredSex_Male.Checked = false;
        //                        chkInsuredSex_Female.Checked = false;
        //                    }
        //                    txtInsuredPersonSign.Text = "SIGNATURE ON FILE";

        //                    if (_SubscriberDOB.Trim() != "")
        //                    {
        //                        dtpInsuredsDOB.Value = Convert.ToDateTime(_SubscriberDOB.Trim());
        //                    }

        //                    #endregion Insurance And Insured Person Details

        //                    #region Referral/Physicain Info

        //                    dtpPhysicianSignDate.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.TransactionDate);
        //                    txtReferringProvider_NPI.Text = _ReferralNPI.Trim();
        //                    txtReferringProviderName.Text = _ReferralFName.Trim() + " " + _ReferralMName.Trim() + " " + _ReferralLName.Trim();
        //                    txtReferringProviderName.Tag = Convert.ToString(_ReferralId);

        //                    #endregion Referral/Physicain Info

        //                    #region Patient Condition Related To Region

        //                    if (oTransaction.WorkersComp == true)
        //                    {
        //                        if (oTransaction.InjuryDate > 0)
        //                        {
        //                            dtpDateOfCurrentIllness.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.InjuryDate);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (oTransaction.OnsiteDate > 0)
        //                        {
        //                            dtpDateOfCurrentIllness.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.OnsiteDate);
        //                        }
        //                    }

        //                    if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkTillDate > 0)
        //                    {
        //                        dtpUnableToWorkFrom.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkFromDate);
        //                        dtpUnableToWorkTill.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkTillDate);
        //                    }

        //                    if (oTransaction.AutoClaim == true)
        //                    {
        //                        chkPatientCoditionRelatedTo_AutoAccident_Yes.Checked = true;
        //                        txtPatientCoditionRelatedTo_State.Text = oTransaction.State.Trim();
        //                        if (oTransaction.AccidentDate > 0)
        //                        {

        //                        }
        //                    }
        //                    else
        //                    {
        //                        chkPatientCoditionRelatedTo_AutoAccident_No.Checked = true;
        //                        txtPatientCoditionRelatedTo_State.Text = "";
        //                    }

        //                    if (oTransaction.WorkersComp == true)
        //                    {
        //                        chkPatientCoditionRelatedTo_Employment_Yes.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        chkPatientCoditionRelatedTo_Employment_No.Checked = true;
        //                    }
        //                    //This to be provided on Billing Transaction Form
        //                    chkPatientCoditionRelatedTo_OtherAccident_No.Checked = true;

        //                    if (oTransaction.HospitalizationDateFrom > 0)
        //                    { dtpHospitalisationFrom.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateFrom); }
        //                    if (oTransaction.HospitalizationDateTo > 0)
        //                    { dtpHospitalisationTo.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateTo); }


        //                    #endregion Patient Condition Related To Region

        //                    #region Patient Status

        //                    if (oTransaction.MaritalStatus == "Single")
        //                    {
        //                        chkPatientStatus_Single.Checked = true;
        //                    }
        //                    else if (oTransaction.MaritalStatus == "Married")
        //                    {
        //                        chkPatientStatus_Married.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        chkPatientStatus_Other.Checked = true;
        //                    }

        //                    #endregion

        //                    #region Patient Relationship To Insured

        //                    if (_SubscriberRelationshipCode == "01")//01=Spouse
        //                    {
        //                        chkRelationship_Spouse.Checked = true;
        //                    }
        //                    else if (_SubscriberRelationshipCode == "18")//18=Self
        //                    {
        //                        chkRelationship_Self.Checked = true;
        //                    }
        //                    else if (_SubscriberRelationshipCode == "19")//19=Child
        //                    {
        //                        chkRelationship_Child.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        chkRelationship_Other.Checked = true;
        //                    }

        //                    #endregion Patient Relationship To Insured

        //                    #region Patient Insurance Type

        //                    //Insurance Type
        //                    if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MB")
        //                    {
        //                        chkMedicare.Checked = true;
        //                    }
        //                    else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MC")
        //                    {
        //                        chkMedicaid.Checked = true;
        //                    }
        //                    else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CH")
        //                    {
        //                        chkTricareChampus.Checked = true;
        //                    }
        //                    else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CI" || _SubscriberInsuranceBelongs.Trim().ToUpper() == "HM")
        //                    {
        //                        chkGroupHealthPlan.Checked = true;
        //                    }
        //                    else
        //                    {
        //                        chkOtherInsuranceType.Checked = true;
        //                    }

        //                    #endregion Patient Insurance Type

        //                    #region Other Insurance Details

        //                    if (IsSecondaryInsurance == true)
        //                    {
        //                        //Gender for Other Insurance Subscriber
        //                        if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
        //                        {
        //                            chkOtherInsuredSex_Male.Checked = true;
        //                        }
        //                        else if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
        //                        {
        //                            chkOtherInsuredSex_Female.Checked = true;
        //                        }
        //                        else
        //                        {
        //                            chkOtherInsuredSex_Male.Checked = false;
        //                            chkOtherInsuredSex_Female.Checked = false;
        //                        }

        //                        chkIsOtherHealthPlan_Yes.Checked = true;
        //                        txtOtherInsuredInsuranceName.Text = _OtherInsuranceName.Trim();
        //                        txtOtherInsuredName.Text = _OtherInsuranceSubscriberLName.Trim() + " " + _OtherInsuranceSubscriberFName.Trim() + " " + _OtherInsuranceSubscriberMName.Trim();

        //                        if (_OtherInsuranceID.Trim() != "")
        //                        {
        //                            txtOtherInsuredPolicyNo.Text = _OtherInsuranceID.Trim();
        //                            if (_OtherInsuranceGroupID.Trim() != "")
        //                            {
        //                                txtOtherInsuredPolicyNo.Text += " - " + _OtherInsuranceGroupID.Trim();
        //                            }
        //                        }
        //                        else if (_OtherInsuranceGroupID.Trim() != "")
        //                        {
        //                            txtOtherInsuredPolicyNo.Text += _OtherInsuranceGroupID.Trim();
        //                        }

        //                        if (_OtherInsuranceSubscriberDOB.Trim() != "")
        //                        {
        //                            dtpOtherInsuredDOB.Value = Convert.ToDateTime(_OtherInsuranceSubscriberDOB);
        //                        }

        //                    }
        //                    else
        //                    {
        //                        chkIsOtherHealthPlan_No.Checked = true;
        //                    }

        //                    #endregion Other Insurance Details

        //                    #region Prior Authorization And Patient Account No

        //                    ////Prior Authorization Number
        //                    //if (GetPriorAuthorizationNumber(oTransaction.PatientID, oTransaction.Lines[0].InsuranceID).Trim() != "")
        //                    //{
        //                    //    txtPriorAuthorizationNumber.Text = GetPriorAuthorizationNumber(oTransaction.PatientID, oTransaction.Lines[0].InsuranceID);
        //                    //}

        //                    txtPriorAuthorizationNumber.Text = oTranDetails.HCFA_PriorAuthorizationNo.Trim();

        //                    //Patient Account Number
        //                    txtPatientAccountNo.Text = oTranDetails.HCFA_PatientCode.Trim();

        //                    #endregion Prior Authorization And Patient Account No


        //                    //Set Transaction Lines
        //                    for (int j = 0; j < oTransaction.Lines.Count; j++)
        //                    {
        //                        if (oTransaction.Lines[0].POSCode != "11")
        //                        {
        //                            if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateTo > 0)
        //                            {
        //                                dtpHospitalisationFrom.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateFrom);
        //                                dtpHospitalisationTo.Value = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateTo);
        //                            }
        //                        }

        //                        //Set DOS, CPT, Dx's, Charges etc.
        //                        #region Transaction Line No 1

        //                        if (j == 0)
        //                        {
        //                            //cmbBillingProvider.SelectedText = oTransaction.Lines[j].RefferingProviderId; 

        //                            if (oTransaction.Lines[j].LineNotes.Count > 0)
        //                            {
        //                                txtNotes1.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
        //                            }
        //                            dtpDOS1From.Value = oTransaction.Lines[j].DateServiceFrom;
        //                            dtpDOS1To.Value = oTransaction.Lines[j].DateServiceTill;

        //                            txtPOS1.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
        //                            txtPOS1.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

        //                            txtEMG1.Text = "";

        //                            txtCPT1.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
        //                            txtCPT1.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

        //                            string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
        //                            if (Dx1Code.Length > 0)
        //                            {
        //                                if (Dx1Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode11.Text = Dx1Code[0];
        //                                    txtDiagnosisCode12.Text = Dx1Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);

        //                            string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
        //                            if (Dx2Code.Length > 0)
        //                            {
        //                                if (Dx2Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode21.Text = Dx2Code[0];
        //                                    txtDiagnosisCode22.Text = Dx2Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);

        //                            string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
        //                            if (Dx3Code.Length > 0)
        //                            {
        //                                if (Dx3Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode31.Text = Dx3Code[0];
        //                                    txtDiagnosisCode32.Text = Dx3Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);


        //                            string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
        //                            if (Dx4Code.Length > 0)
        //                            {
        //                                if (Dx4Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode41.Text = Dx4Code[0];
        //                                    txtDiagnosisCode42.Text = Dx4Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);


        //                            txtMOD11.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
        //                            txtMOD11.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
        //                            txtMOD12.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
        //                            txtMOD12.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
        //                            txtMOD13.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
        //                            txtMOD13.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
        //                            txtMOD14.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
        //                            txtMOD14.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

        //                            if (oTransaction.Lines[j].Dx1Ptr)
        //                            {
        //                                txtDxPtr1.Text = "1";
        //                            }
        //                            if (oTransaction.Lines[j].Dx2Ptr)
        //                            {
        //                                txtDxPtr1.Text = txtDxPtr1.Text + "," + "2";
        //                            }
        //                            if (oTransaction.Lines[j].Dx3Ptr)
        //                            {
        //                                txtDxPtr1.Text = txtDxPtr1.Text + "," + "3";
        //                            }
        //                            if (oTransaction.Lines[j].Dx4Ptr)
        //                            {
        //                                txtDxPtr1.Text = txtDxPtr1.Text + "," + "4";
        //                            }
        //                            txtUnits1.Text = oTransaction.Lines[j].Unit.ToString();
        //                            string[] Charges = Convert.ToString(oTransaction.Lines[j].Charges).Split('.');
        //                            if (Charges.Length > 0)
        //                            {
        //                                if (Charges.Length > 1)
        //                                {
        //                                    txtCharges1.Text = Charges[0];
        //                                    txtCharges11.Text = Charges[1];
        //                                }
        //                                else
        //                                {
        //                                    txtCharges1.Text = Charges[0];
        //                                }
        //                            }
        //                            //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
        //                            if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
        //                            {
        //                                txtRenderingProvider1_NPI.Text = oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim();
        //                                txtRenderingProvider1_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
        //                            }
        //                        }//end of if(j==0)

        //                        #endregion

        //                        #region Transaction Line No 2

        //                        else if (j == 1)
        //                        {
        //                            if (oTransaction.Lines[j].LineNotes.Count > 0)
        //                            {
        //                                txtNotes2.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
        //                            }
        //                            dtpDOS2From.Value = oTransaction.Lines[j].DateServiceFrom;
        //                            dtpDOS2To.Value = oTransaction.Lines[j].DateServiceTill;

        //                            txtPOS2.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
        //                            txtPOS2.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

        //                            txtEMG2.Text = "";

        //                            txtCPT2.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
        //                            txtCPT2.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

        //                            string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
        //                            if (Dx1Code.Length > 0)
        //                            {
        //                                if (Dx1Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode11.Text = Dx1Code[0];
        //                                    txtDiagnosisCode12.Text = Dx1Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);

        //                            string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
        //                            if (Dx2Code.Length > 0)
        //                            {
        //                                if (Dx2Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode21.Text = Dx2Code[0];
        //                                    txtDiagnosisCode22.Text = Dx2Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);

        //                            string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
        //                            if (Dx3Code.Length > 0)
        //                            {
        //                                if (Dx3Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode31.Text = Dx3Code[0];
        //                                    txtDiagnosisCode32.Text = Dx3Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);


        //                            string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
        //                            if (Dx4Code.Length > 0)
        //                            {
        //                                if (Dx4Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode41.Text = Dx4Code[0];
        //                                    txtDiagnosisCode42.Text = Dx4Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);


        //                            txtMOD21.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
        //                            txtMOD21.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
        //                            txtMOD22.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
        //                            txtMOD22.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
        //                            txtMOD23.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
        //                            txtMOD23.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
        //                            txtMOD24.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
        //                            txtMOD24.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

        //                            if (oTransaction.Lines[j].Dx1Ptr)
        //                            {
        //                                txtDxPtr2.Text = "1";
        //                            }
        //                            if (oTransaction.Lines[j].Dx2Ptr)
        //                            {
        //                                txtDxPtr2.Text = txtDxPtr2.Text + "," + "2";
        //                            }
        //                            if (oTransaction.Lines[j].Dx3Ptr)
        //                            {
        //                                txtDxPtr2.Text = txtDxPtr2.Text + "," + "3";
        //                            }
        //                            if (oTransaction.Lines[j].Dx4Ptr)
        //                            {
        //                                txtDxPtr2.Text = txtDxPtr2.Text + "," + "4";
        //                            }
        //                            txtUnits2.Text = oTransaction.Lines[j].Unit.ToString();
        //                            string[] Charges = Convert.ToString(oTransaction.Lines[j].Charges).Split('.');
        //                            if (Charges.Length > 0)
        //                            {
        //                                if (Charges.Length > 1)
        //                                {
        //                                    txtCharges2.Text = Charges[0];
        //                                    txtCharges21.Text = Charges[1];
        //                                }
        //                                else
        //                                {
        //                                    txtCharges2.Text = Charges[0];
        //                                }
        //                            }
        //                            //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
        //                            if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
        //                            {
        //                                txtRenderingProvider2_NPI.Text = oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim();
        //                                txtRenderingProvider2_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
        //                            }
        //                        }//end of if(j==1)

        //                        #endregion

        //                        #region Transaction Line No 3

        //                        else if (j == 2)
        //                        {
        //                            if (oTransaction.Lines[j].LineNotes.Count > 0)
        //                            {
        //                                txtNotes3.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
        //                            }
        //                            dtpDOS3From.Value = oTransaction.Lines[j].DateServiceFrom;
        //                            dtpDOS3To.Value = oTransaction.Lines[j].DateServiceTill;

        //                            txtPOS3.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
        //                            txtPOS3.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

        //                            txtEMG3.Text = "";

        //                            txtCPT3.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
        //                            txtCPT3.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

        //                            string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
        //                            if (Dx1Code.Length > 0)
        //                            {
        //                                if (Dx1Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode11.Text = Dx1Code[0];
        //                                    txtDiagnosisCode12.Text = Dx1Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);

        //                            string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
        //                            if (Dx2Code.Length > 0)
        //                            {
        //                                if (Dx2Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode21.Text = Dx2Code[0];
        //                                    txtDiagnosisCode22.Text = Dx2Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);

        //                            string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
        //                            if (Dx3Code.Length > 0)
        //                            {
        //                                if (Dx3Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode31.Text = Dx3Code[0];
        //                                    txtDiagnosisCode32.Text = Dx3Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);


        //                            string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
        //                            if (Dx4Code.Length > 0)
        //                            {
        //                                if (Dx4Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode41.Text = Dx4Code[0];
        //                                    txtDiagnosisCode42.Text = Dx4Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);


        //                            txtMOD31.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
        //                            txtMOD31.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
        //                            txtMOD32.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
        //                            txtMOD32.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
        //                            txtMOD33.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
        //                            txtMOD33.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
        //                            txtMOD34.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
        //                            txtMOD34.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

        //                            if (oTransaction.Lines[j].Dx1Ptr)
        //                            {
        //                                txtDxPtr3.Text = "1";
        //                            }
        //                            if (oTransaction.Lines[j].Dx2Ptr)
        //                            {
        //                                txtDxPtr3.Text = txtDxPtr3.Text + "," + "2";
        //                            }
        //                            if (oTransaction.Lines[j].Dx3Ptr)
        //                            {
        //                                txtDxPtr3.Text = txtDxPtr3.Text + "," + "3";
        //                            }
        //                            if (oTransaction.Lines[j].Dx4Ptr)
        //                            {
        //                                txtDxPtr3.Text = txtDxPtr3.Text + "," + "4";
        //                            }
        //                            txtUnits3.Text = oTransaction.Lines[j].Unit.ToString();
        //                            string[] Charges = Convert.ToString(oTransaction.Lines[j].Charges).Split('.');
        //                            if (Charges.Length > 0)
        //                            {
        //                                if (Charges.Length > 1)
        //                                {
        //                                    txtCharges3.Text = Charges[0];
        //                                    txtCharges31.Text = Charges[1];
        //                                }
        //                                else
        //                                {
        //                                    txtCharges3.Text = Charges[0];
        //                                }
        //                            }
        //                            //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
        //                            if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
        //                            {
        //                                txtRenderingProvider3_NPI.Text = oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim();
        //                                txtRenderingProvider3_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
        //                            }
        //                        }//end of if(j==2)

        //                        #endregion

        //                        #region Transaction Line No 4

        //                        else if (j == 3)
        //                        {
        //                            if (oTransaction.Lines[j].LineNotes.Count > 0)
        //                            {
        //                                txtNotes4.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
        //                            }
        //                            dtpDOS4From.Value = oTransaction.Lines[j].DateServiceFrom;
        //                            dtpDOS4To.Value = oTransaction.Lines[j].DateServiceTill;

        //                            txtPOS4.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
        //                            txtPOS4.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

        //                            txtEMG4.Text = "";

        //                            txtCPT4.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
        //                            txtCPT4.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

        //                            string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
        //                            if (Dx1Code.Length > 0)
        //                            {
        //                                if (Dx1Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode11.Text = Dx1Code[0];
        //                                    txtDiagnosisCode12.Text = Dx1Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);

        //                            string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
        //                            if (Dx2Code.Length > 0)
        //                            {
        //                                if (Dx2Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode21.Text = Dx2Code[0];
        //                                    txtDiagnosisCode22.Text = Dx2Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);

        //                            string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
        //                            if (Dx3Code.Length > 0)
        //                            {
        //                                if (Dx3Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode31.Text = Dx3Code[0];
        //                                    txtDiagnosisCode32.Text = Dx3Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);


        //                            string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
        //                            if (Dx4Code.Length > 0)
        //                            {
        //                                if (Dx4Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode41.Text = Dx4Code[0];
        //                                    txtDiagnosisCode42.Text = Dx4Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);


        //                            txtMOD41.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
        //                            txtMOD41.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
        //                            txtMOD42.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
        //                            txtMOD42.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
        //                            txtMOD43.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
        //                            txtMOD43.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
        //                            txtMOD44.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
        //                            txtMOD44.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

        //                            if (oTransaction.Lines[j].Dx1Ptr)
        //                            {
        //                                txtDxPtr4.Text = "1";
        //                            }
        //                            if (oTransaction.Lines[j].Dx2Ptr)
        //                            {
        //                                txtDxPtr4.Text = txtDxPtr4.Text + "," + "2";
        //                            }
        //                            if (oTransaction.Lines[j].Dx3Ptr)
        //                            {
        //                                txtDxPtr4.Text = txtDxPtr4.Text + "," + "3";
        //                            }
        //                            if (oTransaction.Lines[j].Dx4Ptr)
        //                            {
        //                                txtDxPtr4.Text = txtDxPtr4.Text + "," + "4";
        //                            }
        //                            txtUnits4.Text = oTransaction.Lines[j].Unit.ToString();
        //                            string[] Charges = Convert.ToString(oTransaction.Lines[j].Charges).Split('.');
        //                            if (Charges.Length > 0)
        //                            {
        //                                if (Charges.Length > 1)
        //                                {
        //                                    txtCharges4.Text = Charges[0];
        //                                    txtCharges41.Text = Charges[1];
        //                                }
        //                                else
        //                                {
        //                                    txtCharges4.Text = Charges[0];
        //                                }
        //                            }
        //                            //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
        //                            if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
        //                            {
        //                                txtRenderingProvider4_NPI.Text = oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim();
        //                                txtRenderingProvider4_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
        //                            }
        //                        }//End of if(j==3)

        //                        #endregion

        //                        #region Transaction Line No 5

        //                        else if (j == 4)
        //                        {
        //                            if (oTransaction.Lines[j].LineNotes.Count > 0)
        //                            {
        //                                txtNotes5.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
        //                            }
        //                            dtpDOS5From.Value = oTransaction.Lines[j].DateServiceFrom;
        //                            dtpDOS5To.Value = oTransaction.Lines[j].DateServiceTill;

        //                            txtPOS5.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
        //                            txtPOS5.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

        //                            txtEMG5.Text = "";

        //                            txtCPT5.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
        //                            txtCPT5.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

        //                            string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
        //                            if (Dx1Code.Length > 0)
        //                            {
        //                                if (Dx1Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode11.Text = Dx1Code[0];
        //                                    txtDiagnosisCode12.Text = Dx1Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);

        //                            string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
        //                            if (Dx2Code.Length > 0)
        //                            {
        //                                if (Dx2Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode21.Text = Dx2Code[0];
        //                                    txtDiagnosisCode22.Text = Dx2Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);

        //                            string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
        //                            if (Dx3Code.Length > 0)
        //                            {
        //                                if (Dx3Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode31.Text = Dx3Code[0];
        //                                    txtDiagnosisCode32.Text = Dx3Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);


        //                            string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
        //                            if (Dx4Code.Length > 0)
        //                            {
        //                                if (Dx4Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode41.Text = Dx4Code[0];
        //                                    txtDiagnosisCode42.Text = Dx4Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);


        //                            txtMOD51.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
        //                            txtMOD51.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
        //                            txtMOD52.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
        //                            txtMOD52.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
        //                            txtMOD53.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
        //                            txtMOD53.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
        //                            txtMOD54.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
        //                            txtMOD54.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

        //                            if (oTransaction.Lines[j].Dx1Ptr)
        //                            {
        //                                txtDxPtr5.Text = "1";
        //                            }
        //                            if (oTransaction.Lines[j].Dx2Ptr)
        //                            {
        //                                txtDxPtr5.Text = txtDxPtr5.Text + "," + "2";
        //                            }
        //                            if (oTransaction.Lines[j].Dx3Ptr)
        //                            {
        //                                txtDxPtr5.Text = txtDxPtr5.Text + "," + "3";
        //                            }
        //                            if (oTransaction.Lines[j].Dx4Ptr)
        //                            {
        //                                txtDxPtr5.Text = txtDxPtr5.Text + "," + "4";
        //                            }
        //                            txtUnits5.Text = oTransaction.Lines[j].Unit.ToString();
        //                            string[] Charges = Convert.ToString(oTransaction.Lines[j].Charges).Split('.');
        //                            if (Charges.Length > 0)
        //                            {
        //                                if (Charges.Length > 1)
        //                                {
        //                                    txtCharges5.Text = Charges[0];
        //                                    txtCharges51.Text = Charges[1];
        //                                }
        //                                else
        //                                {
        //                                    txtCharges5.Text = Charges[0];
        //                                }
        //                            }
        //                            //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
        //                            if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
        //                            {
        //                                txtRenderingProvider5_NPI.Text = oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim();
        //                                txtRenderingProvider5_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
        //                            }
        //                        }//End of if(j==4)

        //                        #endregion

        //                        #region Transaction Line No 6

        //                        else if (j == 5)
        //                        {
        //                            if (oTransaction.Lines[j].LineNotes.Count > 0)
        //                            {
        //                                txtNotes6.Text = oTransaction.Lines[j].LineNotes[0].NoteDescription;
        //                            }
        //                            dtpDOS6From.Value = oTransaction.Lines[j].DateServiceFrom;
        //                            dtpDOS6To.Value = oTransaction.Lines[j].DateServiceTill;

        //                            txtPOS6.Text = Convert.ToString(oTransaction.Lines[j].POSCode);
        //                            txtPOS6.Tag = Convert.ToString(oTransaction.Lines[j].POSDescription);

        //                            txtEMG6.Text = "";

        //                            txtCPT6.Text = Convert.ToString(oTransaction.Lines[j].CPTCode);
        //                            txtCPT6.Tag = Convert.ToString(oTransaction.Lines[j].CPTDescription);

        //                            string[] Dx1Code = Convert.ToString(oTransaction.Lines[j].Dx1Code).Split('.');
        //                            if (Dx1Code.Length > 0)
        //                            {
        //                                if (Dx1Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode11.Text = Dx1Code[0];
        //                                    txtDiagnosisCode12.Text = Dx1Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode11.Text = Convert.ToString(oTransaction.Lines[j].Dx1Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode11.Tag = Convert.ToString(oTransaction.Lines[j].Dx1Description);

        //                            string[] Dx2Code = Convert.ToString(oTransaction.Lines[j].Dx2Code).Split('.');
        //                            if (Dx2Code.Length > 0)
        //                            {
        //                                if (Dx2Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode21.Text = Dx2Code[0];
        //                                    txtDiagnosisCode22.Text = Dx2Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode21.Text = Convert.ToString(oTransaction.Lines[j].Dx2Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode21.Tag = Convert.ToString(oTransaction.Lines[j].Dx2Description);

        //                            string[] Dx3Code = Convert.ToString(oTransaction.Lines[j].Dx3Code).Split('.');
        //                            if (Dx3Code.Length > 0)
        //                            {
        //                                if (Dx3Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode31.Text = Dx3Code[0];
        //                                    txtDiagnosisCode32.Text = Dx3Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode31.Text = Convert.ToString(oTransaction.Lines[j].Dx3Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode31.Tag = Convert.ToString(oTransaction.Lines[j].Dx3Description);


        //                            string[] Dx4Code = Convert.ToString(oTransaction.Lines[j].Dx4Code).Split('.');
        //                            if (Dx4Code.Length > 0)
        //                            {
        //                                if (Dx4Code.Length > 1)
        //                                {
        //                                    txtDiagnosisCode41.Text = Dx4Code[0];
        //                                    txtDiagnosisCode42.Text = Dx4Code[1];
        //                                }
        //                                else
        //                                {
        //                                    txtDiagnosisCode41.Text = Convert.ToString(oTransaction.Lines[j].Dx4Code);
        //                                }
        //                            }
        //                            txtDiagnosisCode41.Tag = Convert.ToString(oTransaction.Lines[j].Dx4Description);


        //                            txtMOD61.Text = Convert.ToString(oTransaction.Lines[j].Mod1Code);
        //                            txtMOD61.Tag = Convert.ToString(oTransaction.Lines[j].Mod1Description);
        //                            txtMOD62.Text = Convert.ToString(oTransaction.Lines[j].Mod2Code);
        //                            txtMOD62.Tag = Convert.ToString(oTransaction.Lines[j].Mod2Description);
        //                            txtMOD63.Text = Convert.ToString(oTransaction.Lines[j].Mod3Code);
        //                            txtMOD63.Tag = Convert.ToString(oTransaction.Lines[j].Mod3Description);
        //                            txtMOD64.Text = Convert.ToString(oTransaction.Lines[j].Mod4Code);
        //                            txtMOD64.Tag = Convert.ToString(oTransaction.Lines[j].Mod4Description);

        //                            if (oTransaction.Lines[j].Dx1Ptr)
        //                            {
        //                                txtDxPtr6.Text = "1";
        //                            }
        //                            if (oTransaction.Lines[j].Dx2Ptr)
        //                            {
        //                                txtDxPtr6.Text = txtDxPtr6.Text + "," + "2";
        //                            }
        //                            if (oTransaction.Lines[j].Dx3Ptr)
        //                            {
        //                                txtDxPtr6.Text = txtDxPtr6.Text + "," + "3";
        //                            }
        //                            if (oTransaction.Lines[j].Dx4Ptr)
        //                            {
        //                                txtDxPtr6.Text = txtDxPtr6.Text + "," + "4";
        //                            }
        //                            txtUnits6.Text = oTransaction.Lines[j].Unit.ToString();
        //                            string[] Charges = Convert.ToString(oTransaction.Lines[j].Charges).Split('.');
        //                            if (Charges.Length > 0)
        //                            {
        //                                if (Charges.Length > 1)
        //                                {
        //                                    txtCharges6.Text = Charges[0];
        //                                    txtCharges61.Text = Charges[1];
        //                                }
        //                                else
        //                                {
        //                                    txtCharges6.Text = Charges[0];
        //                                }
        //                            }
        //                            //FillProviderDetails(oTransaction.Lines[j].RefferingProviderId, ProviderType.RenderingProvider);
        //                            if (oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim() != "")
        //                            {
        //                                txtRenderingProvider6_NPI.Text = oTransaction.Lines[j].HCFA_RenderingProviderNPI.Trim();
        //                                txtRenderingProvider6_NPI.Tag = Convert.ToString(oTransaction.Lines[j].RefferingProviderId);
        //                            }
        //                        }//End of if(j==5)

        //                        #endregion

        //                        #region Claim Total

        //                        TotCharges += oTransaction.Lines[j].Total;

        //                        //Sum = Sum + TotCharges;
        //                        string[] TotalCharges = Convert.ToString(TotCharges).Split('.');
        //                        if (TotalCharges.Length > 0)
        //                        {
        //                            if (TotalCharges.Length > 1)
        //                            {
        //                                txtTotalCharges.Text = TotalCharges[0];
        //                                txtTotalCharges2.Text = TotalCharges[1];
        //                                txtBalanceDue.Text = TotalCharges[0];
        //                                txtBalanceDue2.Text = TotalCharges[1];
        //                            }
        //                            else
        //                            {
        //                                txtTotalCharges.Text = TotalCharges[0];
        //                                txtBalanceDue.Text = TotalCharges[0];
        //                            }
        //                        }
        //                        #endregion Claim Total

        //                    }

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "gloPM", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        }

        public void FillInsurances(Int64 PatientID)
        {
            DataTable dtPatientInsurances = null;
           // gloEDI.gloBilling.gloBilling oglobilling = new gloEDI.gloBilling.gloBilling(_databaseconnectionstring, "");
            try
            {
                IsSecondaryInsurance = false;
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                dtPatientInsurances = ogloPatient.getPatientInsurances(PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;

                if (dtPatientInsurances != null && dtPatientInsurances.Rows.Count > 0)
                {
                    for (int i = 0; i < dtPatientInsurances.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            //Primary Insurance
                            _PayerName = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceName"]);
                            _SubscriberAddress = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberAddr1"]);
                            _SubscriberCity = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberCity"]);
                            _SubscriberState = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberState"]);
                            _SubscriberZIP = Convert.ToString(dtPatientInsurances.Rows[0]["SubscriberZip"]);
                            _SubscriberRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[0]["RelationshipCode"]);
                            _SubscriberMName = Convert.ToString(dtPatientInsurances.Rows[0]["SubMName"]);
                            _SubscriberLName = Convert.ToString(dtPatientInsurances.Rows[0]["SubLName"]);
                            _SubscriberFName = Convert.ToString(dtPatientInsurances.Rows[0]["SubFName"]);
                            _SubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[0]["dtDOB"]);
                            _SubscriberGender = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberGender"]);
                            _SubscriberInsuranceBelongs = Convert.ToString(dtPatientInsurances.Rows[0]["InsuranceTypeCode"]);//"CI"; 
                            _SubscriberInsuranceID = Convert.ToString(dtPatientInsurances.Rows[0]["sSubscriberID"]);
                           // _SubscriberInsurancePST = "P";//Convert.ToString(dtPatientInsurances.Rows[0][""]);
                            _SubscriberGroupID = Convert.ToString(dtPatientInsurances.Rows[0]["sGroup"]);
                            _PayerID = Convert.ToString(dtPatientInsurances.Rows[0]["PayerID"]);
                            _PayerAddress = Convert.ToString(dtPatientInsurances.Rows[0]["PayerAddress1"]);
                            _PayerCity = Convert.ToString(dtPatientInsurances.Rows[0]["PayerCity"]);
                            _PayerState = Convert.ToString(dtPatientInsurances.Rows[0]["PayerState"]);
                            _PayerZip = Convert.ToString(dtPatientInsurances.Rows[0]["PayerZip"]);
                            _SubscriberPhone = Convert.ToString(dtPatientInsurances.Rows[0]["sPhone"]);
                            //Anil Added on 20081030
                            _PriorAuthorizationNo = GetPriorAuthorizationNumber(_PatientID, Convert.ToInt64(dtPatientInsurances.Rows[0]["nInsuranceID"]));

                        }
                        else if (i == 1)
                        {
                            //Secondary Insurance
                            IsSecondaryInsurance = true;
                            _OtherInsuranceAddress = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberAddr1"]);
                            _OtherInsuranceName = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceName"]);
                            _OtherInsuranceSubscriberFName = Convert.ToString(dtPatientInsurances.Rows[i]["SubFName"]);
                            _OtherInsuranceSubscriberLName = Convert.ToString(dtPatientInsurances.Rows[i]["SubLName"]);
                            _OtherInsuranceSubscriberMName = Convert.ToString(dtPatientInsurances.Rows[i]["SubMName"]);
                            _OtherInsuranceSubscriberGender = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberGender"]);
                            _OtherInsuranceSubscriberDOB = Convert.ToString(dtPatientInsurances.Rows[i]["dtDOB"]);
                            _OtherInsuranceZIP = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberZip"]);
                            _OtherInsuranceType = Convert.ToString(dtPatientInsurances.Rows[i]["InsuranceTypeCode"]);//"CI"
                            _OtherInsuranceState = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberState"]);
                            _OtherInsuranceRelationshipCode = Convert.ToString(dtPatientInsurances.Rows[i]["RelationshipCode"]);
                           // _OtherInsurancePST = "S"; //Convert.ToString(dtPatientInsurances.Rows[i][""]);
                            _OtherInsurancePayerID = Convert.ToString(dtPatientInsurances.Rows[i]["PayerID"]);
                            _OtherInsuranceID = Convert.ToString(dtPatientInsurances.Rows[i]["sSubscriberID"]);
                            _OtherInsuranceGroupID = Convert.ToString(dtPatientInsurances.Rows[i]["sGroup"]);
                            _OtherInsuranceCity = Convert.ToString(dtPatientInsurances.Rows[i]["SubscriberCity"]);

                        }
                    }
                }
                if (dtPatientInsurances != null)
                {
                    dtPatientInsurances.Dispose();
                    dtPatientInsurances = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }
        }

        private string GetPriorAuthorizationNumber(Int64 PatientID, Int64 InsuranceID)
        {
            gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
            string _strSQL = "";
            Object _result = null;
            string _PriorAuthorizationNo = "";
            try
            {
                _strSQL = "SELECT sAuthorizationNumber FROM PatientPriorAuthorization WHERE nPatientID=" + PatientID + "  AND nInsuranceID=" + InsuranceID + " ";
                oDB.Connect(false);
                _result = oDB.ExecuteScalar_Query(_strSQL);
                if (_result != null)
                {
                    _PriorAuthorizationNo = Convert.ToString(_result);
                }
            }
            catch (gloDatabaseLayer.DBException ex)
            {
                ex.ERROR_Log(ex.ToString());
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                oDB.Disconnect();
                oDB.Dispose();
            }
            return _PriorAuthorizationNo;
        }

        private void FillPatientInformation(Int64 PatientID)
        {
            //gloEDI.gloBilling.gloBilling oBill = new gloEDI.gloBilling.gloBilling(_databaseconnectionstring, "");
            
            //DataTable dt = new DataTable();
            //DataTable dtClinic = new DataTable();
            gloPatient.Patient oPatient = null;
           // gloPatient.Referrals oReferral = new gloPatient.Referrals();
            try
            {
                //oPatient = ogloPatient.GetPatientDemo(PatientID);
                gloPatient.gloPatient ogloPatient = new gloPatient.gloPatient(_databaseconnectionstring);
                oPatient = ogloPatient.GetPatient(PatientID);
                ogloPatient.Dispose();
                ogloPatient = null;
                if (oPatient != null)
                {
                    _PatientAccountNo = oPatient.DemographicsDetail.PatientCode;
                    //Added on 20081030 by Anil
                    _PatientAddress = oPatient.DemographicsDetail.PatientAddress1;
                    _PatientCity = oPatient.DemographicsDetail.PatientCity;
                    _PatientCode = oPatient.DemographicsDetail.PatientCode;
                    _PatientDOB = Convert.ToString(gloDateMaster.gloDate.DateAsNumber(oPatient.DemographicsDetail.PatientDOB.ToShortDateString()));
                    _PatientFirstName = oPatient.DemographicsDetail.PatientFirstName;
                    _PatientGender = oPatient.DemographicsDetail.PatientGender;
                    _PatientLastName = oPatient.DemographicsDetail.PatientLastName;
                    _PatientMiddleName = oPatient.DemographicsDetail.PatientMiddleName;
                    _PatientSSN = oPatient.DemographicsDetail.PatientSSN;
                    _PatientState = oPatient.DemographicsDetail.PatientState;
                    _PatientZip = oPatient.DemographicsDetail.PatientZip;
                    _PatientPhone = oPatient.DemographicsDetail.PatientPhone;
                    gloPatient.Referrals oReferral;
                    oReferral = oPatient.Referrals;
                    if (oReferral.Count > 0)
                    {
                        gloDatabaseLayer.DBLayer oDB = new gloDatabaseLayer.DBLayer(_databaseconnectionstring);
                        DataTable dtReferral = new DataTable();
                        string _sqlQuery = "";

                        oDB.Connect(false);
                        _sqlQuery = " SELECT sStreet, sCity, sState, sZIP, sFirstName, sMiddleName, sLastName, sGender, nSpecialtyID, sTaxID, sUPIN, sNPI, sContactType, sTaxonomy, sTaxonomyDesc, nContactID " +
                                    " FROM Contacts_MST  " +
                                    " WHERE (nContactID = " + oReferral[0].ReferralID + ") AND (sContactType = 'Referral')";
                        oDB.Retrive_Query(_sqlQuery, out dtReferral);
                        if (dtReferral != null && dtReferral.Rows.Count > 0)
                        {
                            _ReferralFName = dtReferral.Rows[0]["sFirstName"].ToString();
                            _ReferralLName = dtReferral.Rows[0]["sLastName"].ToString();
                            _ReferralMName = dtReferral.Rows[0]["sMiddleName"].ToString();
                            _ReferralCity = dtReferral.Rows[0]["sCity"].ToString();
                            _ReferralState = dtReferral.Rows[0]["sState"].ToString();
                            _ReferralZIP = dtReferral.Rows[0]["sZIP"].ToString();
                            _ReferralNPI = dtReferral.Rows[0]["sNPI"].ToString();
                            _ReferralEmployerID = dtReferral.Rows[0]["sTaxID"].ToString();
                            _ReferralTaxonomy = dtReferral.Rows[0]["sTaxonomy"].ToString();
                        }
                        if (dtReferral != null)
                        {
                            dtReferral.Dispose();
                            dtReferral = null;
                        }
                        oDB.Disconnect();
                        oDB.Dispose();
                        oDB = null;
                    }
                    oPatient.Dispose();
                    oPatient = null;
                }
                
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
        }

        private string FormattedClaimNumberGeneration(string NumberSize)
        {
            int _length = 0;
            _length = NumberSize.Length;
            if (_length == 1)
            {
                NumberSize = "0000" + NumberSize;
            }
            else if (_length == 2)
            {
                NumberSize = "000" + NumberSize;
            }
            else if (_length == 3)
            {
                NumberSize = "00" + NumberSize;
            }
            else if (_length == 4)
            {
                NumberSize = "0" + NumberSize;
            }
            else if (_length == 5)
            {
              //  NumberSize = NumberSize;
            }
            return NumberSize;
        }

        private void FillSubmitterInfo(Int64 _SelectedClinicId, Int64 _nProviderID)
        {
            gloBilling.gloBilling oBill = new gloBilling.gloBilling(_databaseconnectionstring, "");
            DataTable dt = null;
            try
            {
                dt = oBill.GetSubmitterInfo(_SelectedClinicId, _nProviderID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    //nClinicID,sAddress1,sAddress2,sStreet,sCity,sState,sZIP,sPhoneNo,sMobileNo,
                    //sFAX,sTAXID,sContactPersonName,sContactPersonAddress1,sContactPersonAddress2,sContactPersonPhone,
                    //sContactPersonFAX,sContactPersonMobile
                    _SubmitterName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    _SubmitterAddress = Convert.ToString(dt.Rows[0]["SubmitterAddress1"]) + " " + Convert.ToString(dt.Rows[0]["SubmitterAddress2"]);
                    _SubmitterCity = Convert.ToString(dt.Rows[0]["SubmitterCity"]);
                    _SubmitterState = Convert.ToString(dt.Rows[0]["SubmitterState"]);
                    _SubmitterZIP = Convert.ToString(dt.Rows[0]["SubmitterZIP"]);
                    if (Convert.ToString(dt.Rows[0]["SubmitterContactName"]) == "")
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterName"]);
                    }
                    else
                    {
                        _SubmitterContactPersonName = Convert.ToString(dt.Rows[0]["SubmitterContactName"]);
                    }
                    _SubmitterContactPersonNo = Convert.ToString(dt.Rows[0]["SubmitterPhone"]);
                    //_SubmitterETIN = "C0923";

                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (oBill != null)
                {
                    oBill.Dispose();
                    oBill = null;
                }
            }

        }

        private void FillProviderDetails(long _SelectedProviderId, ProviderType _ProviderType)
        {
            Resource oResource = new Resource(_databaseconnectionstring);
            gloSettings.GeneralSettings oSettings = new gloSettings.GeneralSettings(_databaseconnectionstring);
          //  DataTable dtProviderDetails = null;
            Provider _Provider = null;
            Object _objResult = null;
            string strBillingSetting = "";
            string strRenderingSetting = "";
            try
            {

                _Provider = oResource.GetProviderDetail(_SelectedProviderId);

                if (_Provider != null)
                {
                    switch (_ProviderType)
                    {
                        case ProviderType.BillingProvider:
                            {
                                oSettings.GetSetting("BillingSetting", _SelectedProviderId, _ClinicID, out _objResult);
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strBillingSetting = Convert.ToString(_objResult);
                                }

                                _BillingFName = _Provider.FirstName;
                                _BillingLName = _Provider.LastName;
                                _BillingMName = _Provider.MiddleName;
                                _BillingNPI = _Provider.NPI;
                                _BillingStateMedicalNo = _Provider.StateMedicalNo;
                                _BillingSSN = _Provider.SSN;
                                _BillingEmployerID = _Provider.EmployerID;
                                _BillingTaxonomy = _Provider.Taxonomy;

                                switch (strBillingSetting)
                                {
                                    case "Business":
                                        {
                                            _BillingAddress = _Provider.BMAddress1;
                                            _BillingCity = _Provider.BMCity;
                                            _BillingState = _Provider.BMState;
                                            _BillingZIP = _Provider.BMZIP;
                                            _BillingPhone = _Provider.BMPhone;
                                        } break;
                                    case "Practice":
                                        {
                                            _BillingAddress = _Provider.BPracAddress1;
                                            _BillingCity = _Provider.BPracCity;
                                            _BillingState = _Provider.BPracState;
                                            _BillingZIP = _Provider.BPracZIP;
                                            _BillingPhone = _Provider.BPracPhone;
                                        } break;
                                    case "Company":
                                        {
                                            _BillingAddress = _Provider.CompanyAddress1;
                                            _BillingCity = _Provider.CompanyCity;
                                            _BillingState = _Provider.CompanyState;
                                            _BillingZIP = _Provider.CompanyZip;
                                            _BillingPhone = _Provider.CompanyPhone;
                                        } break;
                                    default:
                                        _BillingAddress = _Provider.BMAddress1;
                                        _BillingCity = _Provider.BMCity;
                                        _BillingState = _Provider.BMState;
                                        _BillingZIP = _Provider.BMZIP;
                                        _BillingPhone = _Provider.BMPhone;
                                        break;
                                }
                            }
                            break;
                        case ProviderType.PayToProvider:
                            {
                                //txtPTPAddress.Text = _Provider.BMAddress1;
                                //txtPTPCity.Text = _Provider.BMCity;
                                //txtPTPState.Text = _Provider.BMState;
                                //txtPTPZip.Text = _Provider.BMZIP;
                                //txtPTPNPI_ID.Text = _Provider.NPI;
                                //txtPTPUPIN.Text = _Provider.UPIN;
                            }
                            break;
                        case ProviderType.RefferingProvider:
                            {
                                _ReferralId = Convert.ToString(_Provider.ProviderID);
                                _ReferralFName = _Provider.FirstName;
                                _ReferralAddress = _Provider.BMAddress1;
                                _ReferralLName = _Provider.LastName;
                                _ReferralMName = _Provider.MiddleName;
                                _ReferralCity = _Provider.BMCity;
                                _ReferralState = _Provider.BMState;
                                _ReferralZIP = _Provider.BMZIP;
                                _ReferralNPI = _Provider.NPI;
                                _ReferralStateMedicalNo = _Provider.StateMedicalNo;
                                _ReferralSSN = _Provider.SSN;
                                _ReferralEmployerID = _Provider.EmployerID;
                                _ReferralTaxonomy = _Provider.Taxonomy;

                            }
                            break;
                        case ProviderType.RenderingProvider:
                            {
                                oSettings.GetSetting("RenderingSetting", _SelectedProviderId, _ClinicID, out _objResult);
                                if (_objResult != null)
                                {
                                    // |Company|Practice|Business"
                                    strRenderingSetting = Convert.ToString(_objResult);
                                }

                                _RenderingFName = _Provider.FirstName;
                                _RenderingLName = _Provider.LastName;
                                _RenderingMName = _Provider.MiddleName;
                                _RenderingNPI = _Provider.NPI;
                                _RenderingStateMedicalNo = _Provider.StateMedicalNo;
                                _RenderingSSN = _Provider.SSN;
                                _RenderingEmployerID = _Provider.EmployerID;
                                _RenderingTaxonomy = _Provider.Taxonomy;

                                switch (strRenderingSetting)
                                {
                                    case "Business":
                                        {
                                            _RenderingAddress = _Provider.BMAddress1;
                                            _RenderingCity = _Provider.BMCity;
                                            _RenderingState = _Provider.BMState;
                                            _RenderingZIP = _Provider.BMZIP;

                                        } break;
                                    case "Practice":
                                        {
                                            _RenderingAddress = _Provider.BPracAddress1;
                                            _RenderingCity = _Provider.BPracCity;
                                            _RenderingState = _Provider.BPracState;
                                            _RenderingZIP = _Provider.BPracZIP;
                                        } break;
                                    case "Company":
                                        {
                                            _RenderingAddress = _Provider.CompanyAddress1;
                                            _RenderingCity = _Provider.CompanyCity;
                                            _RenderingState = _Provider.CompanyState;
                                            _RenderingZIP = _Provider.CompanyZip;
                                        } break;
                                    default:
                                        _RenderingAddress = _Provider.BMAddress1;
                                        _RenderingCity = _Provider.BMCity;
                                        _RenderingState = _Provider.BMState;
                                        _RenderingZIP = _Provider.BMZIP;
                                        break;
                                }

                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);
            }
            finally
            {
                if (_Provider != null) { _Provider.Dispose(); }
                if (oResource != null) { oResource.Dispose(); }
                if (oSettings != null)
                {
                    oSettings.Dispose();
                    oSettings = null;
                }
            }
        }
       
        public void GenerateH1500(Transaction oTransaction)
        {
            H1500 form = new H1500();
            gloBilling.gloBilling ogloBilling = new gloBilling.gloBilling(_databaseconnectionstring, "");
            TransactionDetails oTranDetails = null;
           // TransactionLine oTransLine = null;
            Decimal TotalCharges = 0;
            Decimal PaidAmount = 0;
            try
            {
                if (TransactionID > 0)
                {
                    oTransaction = ogloBilling.GetHCFATransactionDetails(TransactionID, _ClinicID);
                    if (oTransaction != null)
                    {
                        if (oTransaction.Lines.Count > 0)
                        {
                            FillInsurances(oTransaction.PatientID);
                            FillSubmitterInfo(oTransaction.ClinicID, oTransaction.ProviderID);

                            oTranDetails = oTransaction.Transaction_Details;

                            form.StartEncounters(); // Start the encounters (Submitter ID)
                            form.StartEncounter(1);	// Start Encounter/Claim 1

                            // Set the XML data elements.
                            #region Type of Insurance

                            if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MB")
                            {
                                form.set_Element(1, "medicare", "1");
                            }
                            else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "MC")
                            {
                                form.set_Element(1, "medicaid", "1");
                            }
                            else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CH")
                            {
                                form.set_Element(1, "champus", "1");
                            }
                            else if (_SubscriberInsuranceBelongs.Trim().ToUpper() == "CI" || _SubscriberInsuranceBelongs.Trim().ToUpper() == "HM")
                            {
                                form.set_Element(1, "group", "1");
                            }
                            else 
                            {
                                form.set_Element(1, "oth", "1");
                            }
                            
                            //form.set_Element(1, "feca", "1");

                            form.set_Element(1, "ins-id", _SubscriberInsuranceID.Trim());
                            form.set_Element(1, "auto", "");
                            form.set_Element(1, "wc", "");
                            form.set_Element(1, "fbcs", "");

                            #endregion Type of Insurance

                            #region Patient Demographics

                            form.set_Element(2, "last", oTranDetails.HCFA_PatientLName.Trim());
                            form.set_Element(2, "first", oTranDetails.HCFA_PatientFName.Trim());
                            form.set_Element(2, "middle", oTranDetails.HCFA_PatientMName.Trim());
                            if (oTranDetails.HCFA_PatientDOB > 0)
                            {
                                DateTime PatientDOB = gloDateMaster.gloDate.DateAsDate(oTranDetails.HCFA_PatientDOB);

                                form.set_Element(3, "m", PatientDOB.Month.ToString());
                                form.set_Element(3, "d", PatientDOB.Date.ToString());
                                form.set_Element(3, "y", PatientDOB.Year.ToString());
                            }
                            else
                            {
                                form.set_Element(3, "m", "");
                                form.set_Element(3, "d", "");
                                form.set_Element(3, "y", "");
                            }
                            if (oTranDetails.HCFA_PatientGender.ToUpper().Substring(0, 1) == "M")
                            {
                                form.set_Element(3, "sex-m", "1");
                                form.set_Element(3, "sex-f", "");
                            }
                            else if (oTranDetails.HCFA_PatientGender.ToUpper().Substring(0, 1) == "M")
                            {
                                form.set_Element(3, "sex-m", "");
                                form.set_Element(3, "sex-f", "1");
                            }

                            #endregion  Patient Demographics

                            #region Insured's Name 

                            form.set_Element(4, "last",_SubscriberLName.Trim());
                            form.set_Element(4, "first",_SubscriberFName.Trim());
                            form.set_Element(4, "middle", _SubscriberMName.Trim());

                            #endregion Insured's Name

                            #region Patient's Address

                            form.set_Element(5, "addr",oTranDetails.HCFA_PatientAddress1.Trim());
                            form.set_Element(5, "city",oTranDetails.HCFA_PatientCity.Trim() );
                            form.set_Element(5, "state", oTranDetails.HCFA_PatientState.Trim());
                            form.set_Element(5, "zip", oTranDetails.HCFA_PatientZip.Trim());
                            form.set_Element(5, "phone", oTranDetails.HCFA_PatientPhone.Trim());

                            #endregion Patient's Address

                            #region Patient Relationship to Insured

                            if (_SubscriberRelationshipCode == "01")//01=Spouse
                            {
                                form.set_Element(6, "self", "");
                                form.set_Element(6, "spouse", "1");
                                form.set_Element(6, "child", "");
                                form.set_Element(6, "other", "");

                            }
                            else if (_SubscriberRelationshipCode == "18")//18=Self
                            {
                                form.set_Element(6, "self", "1");
                                form.set_Element(6, "spouse", "");
                                form.set_Element(6, "child", "");
                                form.set_Element(6, "other", "");

                            }
                            else if (_SubscriberRelationshipCode == "19")//19=Child
                            {
                                form.set_Element(6, "self", "");
                                form.set_Element(6, "spouse", "");
                                form.set_Element(6, "child", "1");
                                form.set_Element(6, "other", "");

                            }
                            else
                            {
                                form.set_Element(6, "self", "");
                                form.set_Element(6, "spouse", "");
                                form.set_Element(6, "child", "");
                                form.set_Element(6, "other", "1");

                            }

                            #endregion Patient Relationship to Insured

                            #region Insured's Address

                            form.set_Element(7, "addr",_SubscriberAddress.Trim());
                            form.set_Element(7, "city", _SubscriberCity.Trim());
                            form.set_Element(7, "state", _SubscriberState.Trim());
                            form.set_Element(7, "zip", _SubscriberZIP.Trim());
                            form.set_Element(7, "phone", _SubscriberPhone.Trim());

                            #endregion Insured's Address

                            #region Patient Information
                            if (oTransaction.MaritalStatus == "Single")
                            {
                                form.set_Element(8, "single", "1");
                                form.set_Element(8, "married", "");
                                form.set_Element(8, "other", "");
                            }
                            else if (oTransaction.MaritalStatus == "Married")
                            {
                                form.set_Element(8, "single", "");
                                form.set_Element(8, "married", "1");
                                form.set_Element(8, "other", "");
                            }
                            else
                            {
                                form.set_Element(8, "single", "");
                                form.set_Element(8, "married", "");
                                form.set_Element(8, "other", "1");
                            }
                            
                            form.set_Element(8, "employed", "");
                            form.set_Element(8, "full-time", "");
                            form.set_Element(8, "part-time", "");

                            #endregion Patient Information

                            #region Other Insurance Details

                            form.set_Element(9, "last", _OtherInsuranceSubscriberLName.Trim());
                            form.set_Element(9, "first", _OtherInsuranceSubscriberFName.Trim());
                            form.set_Element(9, "middle",_OtherInsuranceSubscriberMName.Trim());
                            if (_OtherInsuranceID.Trim() != "")
                            {
                                form.set_Element(9, "policy", _OtherInsuranceGroupID.Trim() + "-" + _OtherInsuranceID.Trim());
                            }
                            else
                            {
                                form.set_Element(9, "policy", _OtherInsuranceGroupID.Trim());
                            }


                            if (_OtherInsuranceSubscriberDOB.Trim() != "")
                            {
                                DateTime dtOtherInsuredDOB = Convert.ToDateTime(_OtherInsuranceSubscriberDOB);
                                form.set_Element(9, "dobm", dtOtherInsuredDOB.Month.ToString());
                                form.set_Element(9, "dobd", dtOtherInsuredDOB.Date.ToString());
                                form.set_Element(9, "doby", dtOtherInsuredDOB.Year.ToString());
                            }
                            else
                            {
                                form.set_Element(9, "dobm","");
                                form.set_Element(9, "dobd", "");
                                form.set_Element(9, "doby", "");
                            }
                                //Gender for Other Insurance Subscriber
                                if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
                                {
                                    form.set_Element(9, "sex-m", "1");
                                    form.set_Element(9, "sex-f", "");
                                }
                                else if (_OtherInsuranceSubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
                                {
                                    form.set_Element(9, "sex-m", "");
                                    form.set_Element(9, "sex-f", "1");
                                }
                                else
                                {
                                    form.set_Element(9, "sex-m", "");
                                    form.set_Element(9, "sex-f", "");
                                }
 
                            #endregion Other Insurance Details

                            #region Patient Condition Related To

                                if (oTransaction.WorkersComp == true)
                                {
                                    form.set_Element(10, "employ-yes", "1");
                                    form.set_Element(10, "employ-no", "");
                                }
                                else
                                {
                                    form.set_Element(10, "employ-yes", "");
                                    form.set_Element(10, "employ-no", "1");
                                }

                                if (oTransaction.AutoClaim == true)
                                {
                                    form.set_Element(10, "auto-yes", "1");
                                    form.set_Element(10, "auto-no", "");
                                    form.set_Element(10, "auto-state", oTransaction.State.Trim());
                                }
                                else
                                {
                                    form.set_Element(10, "auto-yes", "");
                                    form.set_Element(10, "auto-no", "1");
                                    form.set_Element(10, "auto-state", "");
                                }
                                form.set_Element(10, "oth-yes", "");
                                form.set_Element(10, "oth-no", "");

                            #endregion Patient Condition Related To

                            #region Insurance Details

                            form.set_Element(11, "policy", _SubscriberGroupID.Trim());

                            if (_SubscriberDOB.Trim() != "")
                            {
                                DateTime dtInsuredDOB = Convert.ToDateTime(_SubscriberDOB.Trim());
                                form.set_Element(11, "m", dtInsuredDOB.Month.ToString());
                                form.set_Element(11, "d", dtInsuredDOB.Date.ToString());
                                form.set_Element(11, "y", dtInsuredDOB.Year.ToString());
                            }
                            else
                            {
                                form.set_Element(11, "m", "");
                                form.set_Element(11, "d", "");
                                form.set_Element(11, "y", "");
                            }
                            if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "M")
                            {
                                form.set_Element(11, "sex-m", "1");
                                form.set_Element(11, "sex-f", "");
                            }
                            else if (_SubscriberGender.ToUpper().Trim().Substring(0, 1) == "F")
                            {
                                form.set_Element(11, "sex-m", "");
                                form.set_Element(11, "sex-f", "1");
                            }
                            else
                            {
                                form.set_Element(11, "sex-m", "");
                                form.set_Element(11, "sex-f", "");
                            }
 
                            form.set_Element(11, "emp-name", "");
                            form.set_Element(11, "plan-name", _PayerName.Trim());
                            if (IsSecondaryInsurance == true)
                            {
                                form.set_Element(11, "oth-yes", "1");
                                form.set_Element(11, "oth-no", "");
                            }
                            else
                            {
                                form.set_Element(11, "oth-yes", "");
                                form.set_Element(11, "oth-no", "1");
                            }

                            #endregion Insurance Details

                            #region Patient Signature and Date

                            form.set_Element(12, "signed", "Signature on File");
                            form.set_Element(12, "m", DateTime.Now.Month.ToString());
                            form.set_Element(12, "d", DateTime.Now.Date.ToString());
                            form.set_Element(12, "y", DateTime.Now.Year.ToString());

                            #endregion Patient Signature and Date

                            #region Insured's Signature

                            form.set_Element(13, "signed", "Signature on File");

                            #endregion Insured's Signature

                            #region Current illness/Injury Date

                            if (oTransaction.WorkersComp == true)
                            {
                                if (oTransaction.InjuryDate > 0)
                                {
                                    DateTime dtInjuryDate = gloDateMaster.gloDate.DateAsDate(oTransaction.InjuryDate);
                                    form.set_Element(14, "m", dtInjuryDate.Month.ToString());
                                    form.set_Element(14, "d", dtInjuryDate.Date.ToString());
                                    form.set_Element(14, "y", dtInjuryDate.Year.ToString());
                                }
                                else
                                {
                                    form.set_Element(14, "m", "");
                                    form.set_Element(14, "d", "");
                                    form.set_Element(14, "y", "");
                                }
                            }
                            else
                            {
                                if (oTransaction.OnsiteDate > 0)
                                {
                                    DateTime dtOnsiteDate = gloDateMaster.gloDate.DateAsDate(oTransaction.OnsiteDate);
                                    form.set_Element(14, "m", dtOnsiteDate.Month.ToString());
                                    form.set_Element(14, "d", dtOnsiteDate.Date.ToString());
                                    form.set_Element(14, "y", dtOnsiteDate.Year.ToString());
                                }
                                else
                                {
                                    form.set_Element(14, "m", "");
                                    form.set_Element(14, "d", "");
                                    form.set_Element(14, "y", "");
                                }
                            }

                           
                            form.set_Element(15, "m", "");
                            form.set_Element(15, "d", "");
                            form.set_Element(15, "y", "");

                            #endregion Current illness/Injury Date

                            #region Unable to Work Dates

                            if (oTransaction.UnableToWorkFromDate > 0 && oTransaction.UnableToWorkTillDate > 0)
                            {
                                DateTime dtUnableToWorkFrom = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkFromDate);
                                DateTime dtUnableToWorkTo = gloDateMaster.gloDate.DateAsDate(oTransaction.UnableToWorkTillDate);
                                form.set_Element(16, "fm", dtUnableToWorkFrom.Month.ToString());
                                form.set_Element(16, "fd", dtUnableToWorkFrom.Date.ToString());
                                form.set_Element(16, "fy", dtUnableToWorkFrom.Year.ToString());
                                form.set_Element(16, "tm", dtUnableToWorkTo.Month.ToString());
                                form.set_Element(16, "td", dtUnableToWorkTo.Date.ToString());
                                form.set_Element(16, "ty", dtUnableToWorkTo.Year.ToString());
                            }
                            else
                            {
                                form.set_Element(16, "fm", "");
                                form.set_Element(16, "fd", "");
                                form.set_Element(16, "fy", "");
                                form.set_Element(16, "tm", "");
                                form.set_Element(16, "td", "");
                                form.set_Element(16, "ty", "");
                            }

                            #endregion Unable to Work Dates

                            #region Referral Physician Name and NPI

                            form.set_Element(17, "name", "");
                            form.set_Element(17, "id-qual", "");
                            form.set_Element(17, "id-num", "");
                            form.set_Element(17, "npi", "");

                            #endregion Referral Physician Name and NPI

                            #region Hospitalization Dates

                            if (oTransaction.HospitalizationDateFrom > 0 && oTransaction.HospitalizationDateTo > 0)
                            {
                                DateTime dtHospitalisationFrom = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateFrom);
                                DateTime dtHospitalisationTo = gloDateMaster.gloDate.DateAsDate(oTransaction.HospitalizationDateTo);
                                form.set_Element(18, "fm", dtHospitalisationFrom.Month.ToString());
                                form.set_Element(18, "fd", dtHospitalisationFrom.Date.ToString());
                                form.set_Element(18, "fy", dtHospitalisationFrom.Year.ToString());
                                form.set_Element(18, "tm", dtHospitalisationTo.Month.ToString());
                                form.set_Element(18, "td", dtHospitalisationTo.Date.ToString());
                                form.set_Element(18, "ty", dtHospitalisationTo.Year.ToString());
                            }
                            else
                            {
                                form.set_Element(18, "fm", "");
                                form.set_Element(18, "fd", "");
                                form.set_Element(18, "fy", "");
                                form.set_Element(18, "tm", "");
                                form.set_Element(18, "td", "");
                                form.set_Element(18, "ty", "");
                            }

                            #endregion Hospitalization Dates

                            #region Not Used Area
                            form.set_Element(19, "area1", "");
                            form.set_Element(19, "area2", "");
                            #endregion Not Used Area

                            #region Outside Lab Charges

                            if (oTransaction.OutSideLab == true)
                            {
                                form.set_Element(20, "out-yes", "1");
                                form.set_Element(20, "out-no", "");
                                form.set_Element(20, "charges", Convert.ToString(oTransaction.OutSideLabCharges));
                            }
                            else
                            {
                                form.set_Element(20, "out-yes", "");
                                form.set_Element(20, "out-no", "1");
                                form.set_Element(20, "charges", "");
                            }

                            #endregion Outside Lab Charges

                            #region Diagnosis
                            for (int i = 0; i < oTransaction.Lines.Count - 1; i++)
                            {
                                form.set_Element(21, "dx1", Convert.ToString(oTransaction.Lines[i].Dx1Code));
                                form.set_Element(21, "dx2", Convert.ToString(oTransaction.Lines[i].Dx2Code));
                                form.set_Element(21, "dx3", Convert.ToString(oTransaction.Lines[i].Dx2Code));
                                form.set_Element(21, "dx4", Convert.ToString(oTransaction.Lines[i].Dx2Code));
                            }
                            #endregion Diagnosis

                            #region Medicaid Resubmission Code

                            form.set_Element(22, "resub", "");
                            form.set_Element(22, "org", "");

                            #endregion Medicaid Resubmission Code

                            #region Prior Authorisation

                            form.set_Element(23, "prior", GetPriorAuthorizationNumber(oTransaction.PatientID,oTransaction.Lines[0].InsuranceID).Trim());

                            #endregion Prior Authorisation

                            for (int i = 0; i < oTransaction.Lines.Count-1; i++)
                            {
                                #region Transaction Line 1
                                if (i == 0)
                                {
                                    // Line Item # 1
                                    form.set_LineItemElement(1, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                    form.set_LineItemElement(1, "fd", oTransaction.Lines[i].DateServiceFrom.Date.ToString());
                                    form.set_LineItemElement(1, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString());
                                    form.set_LineItemElement(1, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                    form.set_LineItemElement(1, "td", oTransaction.Lines[i].DateServiceTill.Date.ToString());
                                    form.set_LineItemElement(1, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString());
                                    form.set_LineItemElement(1, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                    form.set_LineItemElement(1, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                    form.set_LineItemElement(1, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                    form.set_LineItemElement(1, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                    form.set_LineItemElement(1, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                    form.set_LineItemElement(1, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                    form.set_LineItemElement(1, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                    if (oTransaction.Lines[i].Dx1Ptr == true)
                                    {
                                        form.set_LineItemElement(1, "ptr", "1");
                                    }
                                    else
                                    {
                                        form.set_LineItemElement(1, "ptr", "");
                                    }
                                    form.set_LineItemElement(1, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                    form.set_LineItemElement(1, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                                }
                                #endregion Transaction Line 1

                                #region Transaction Line 2
                                if (i == 1)
                                {
                                    // Line Item # 2
                                    form.set_LineItemElement(2, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                    form.set_LineItemElement(2, "fd", oTransaction.Lines[i].DateServiceFrom.Date.ToString());
                                    form.set_LineItemElement(2, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString());
                                    form.set_LineItemElement(2, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                    form.set_LineItemElement(2, "td", oTransaction.Lines[i].DateServiceTill.Date.ToString());
                                    form.set_LineItemElement(2, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString());
                                    form.set_LineItemElement(2, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                    form.set_LineItemElement(2, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                    form.set_LineItemElement(2, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                    form.set_LineItemElement(2, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                    form.set_LineItemElement(2, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                    form.set_LineItemElement(2, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                    form.set_LineItemElement(2, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                    if (oTransaction.Lines[i].Dx2Ptr == true)
                                    {
                                        form.set_LineItemElement(2, "ptr", "1");
                                    }
                                    else
                                    {
                                        form.set_LineItemElement(2, "ptr", "");
                                    }
                                    form.set_LineItemElement(2, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                    form.set_LineItemElement(2, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                                }
                                #endregion Transaction Line 2

                                #region Transaction Line 3

                                if (i == 2)
                                {
                                    // Line Item # 3
                                    form.set_LineItemElement(3, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                    form.set_LineItemElement(3, "fd", oTransaction.Lines[i].DateServiceFrom.Date.ToString());
                                    form.set_LineItemElement(3, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString());
                                    form.set_LineItemElement(3, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                    form.set_LineItemElement(3, "td", oTransaction.Lines[i].DateServiceTill.Date.ToString());
                                    form.set_LineItemElement(3, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString());
                                    form.set_LineItemElement(3, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                    form.set_LineItemElement(3, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                    form.set_LineItemElement(3, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                    form.set_LineItemElement(3, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                    form.set_LineItemElement(3, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                    form.set_LineItemElement(3, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                    form.set_LineItemElement(3, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                    if (oTransaction.Lines[i].Dx3Ptr == true)
                                    {
                                        form.set_LineItemElement(3, "ptr", "1");
                                    }
                                    else
                                    {
                                        form.set_LineItemElement(3, "ptr", "");
                                    }
                                    form.set_LineItemElement(3, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                    form.set_LineItemElement(3, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                                }

                                #endregion Transaction Line 3

                                #region Transaction Line 4

                                if (i == 3)
                                {
                                    // Line Item # 4
                                    form.set_LineItemElement(4, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                    form.set_LineItemElement(4, "fd", oTransaction.Lines[i].DateServiceFrom.Date.ToString());
                                    form.set_LineItemElement(4, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString());
                                    form.set_LineItemElement(4, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                    form.set_LineItemElement(4, "td", oTransaction.Lines[i].DateServiceTill.Date.ToString());
                                    form.set_LineItemElement(4, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString());
                                    form.set_LineItemElement(4, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                    form.set_LineItemElement(4, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                    form.set_LineItemElement(4, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                    form.set_LineItemElement(4, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                    form.set_LineItemElement(4, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                    form.set_LineItemElement(4, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                    form.set_LineItemElement(4, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                    if (oTransaction.Lines[i].Dx4Ptr == true)
                                    {
                                        form.set_LineItemElement(4, "ptr", "1");
                                    }
                                    else
                                    {
                                        form.set_LineItemElement(4, "ptr", "");
                                    }
                                    form.set_LineItemElement(4, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                    form.set_LineItemElement(4, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                                }
                                #endregion Transaction Line 4

                                #region Transaction Line 5
                                if (i == 4)
                                {
                                    // Line Item # 5
                                    form.set_LineItemElement(5, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                    form.set_LineItemElement(5, "fd", oTransaction.Lines[i].DateServiceFrom.Date.ToString());
                                    form.set_LineItemElement(5, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString());
                                    form.set_LineItemElement(5, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                    form.set_LineItemElement(5, "td", oTransaction.Lines[i].DateServiceTill.Date.ToString());
                                    form.set_LineItemElement(5, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString());
                                    form.set_LineItemElement(5, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                    form.set_LineItemElement(5, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                    form.set_LineItemElement(5, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                    form.set_LineItemElement(5, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                    form.set_LineItemElement(5, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                    form.set_LineItemElement(5, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                    form.set_LineItemElement(5, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                    if (oTransaction.Lines[i].Dx5Ptr == true)
                                    {
                                        form.set_LineItemElement(5, "ptr", "1");
                                    }
                                    else
                                    {
                                        form.set_LineItemElement(5, "ptr", "");
                                    }
                                    form.set_LineItemElement(5, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                    form.set_LineItemElement(5, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                                }
                                #endregion Transaction Line 1

                                #region Transaction Line 6
                                if (i == 5)
                                {
                                    // Line Item # 6
                                    form.set_LineItemElement(6, "fm", oTransaction.Lines[i].DateServiceFrom.Month.ToString());
                                    form.set_LineItemElement(6, "fd", oTransaction.Lines[i].DateServiceFrom.Date.ToString());
                                    form.set_LineItemElement(6, "fy", oTransaction.Lines[i].DateServiceFrom.Year.ToString());
                                    form.set_LineItemElement(6, "tm", oTransaction.Lines[i].DateServiceTill.Month.ToString());
                                    form.set_LineItemElement(6, "td", oTransaction.Lines[i].DateServiceTill.Date.ToString());
                                    form.set_LineItemElement(6, "ty", oTransaction.Lines[i].DateServiceTill.Year.ToString());
                                    form.set_LineItemElement(6, "pos", Convert.ToString(oTransaction.Lines[i].POSCode));
                                    form.set_LineItemElement(6, "tos", Convert.ToString(oTransaction.Lines[i].TOSCode));
                                    form.set_LineItemElement(6, "cpt", Convert.ToString(oTransaction.Lines[i].CPTCode));
                                    form.set_LineItemElement(6, "m1", Convert.ToString(oTransaction.Lines[i].Mod1Code));
                                    form.set_LineItemElement(6, "m2", Convert.ToString(oTransaction.Lines[i].Mod2Code));
                                    form.set_LineItemElement(6, "m3", Convert.ToString(oTransaction.Lines[i].Mod3Code));
                                    form.set_LineItemElement(6, "m4", Convert.ToString(oTransaction.Lines[i].Mod4Code));
                                    if (oTransaction.Lines[i].Dx6Ptr == true)
                                    {
                                        form.set_LineItemElement(6, "ptr", "1");
                                    }
                                    else
                                    {
                                        form.set_LineItemElement(6, "ptr", "");
                                    }
                                    form.set_LineItemElement(6, "charge", Convert.ToString(oTransaction.Lines[i].Charges));
                                    form.set_LineItemElement(6, "unit", Convert.ToString(oTransaction.Lines[i].Unit));
                                }

                                #endregion Transaction Line 6

                                TotalCharges += oTransaction.Lines[i].Total;
                                PaidAmount = 0;
                            }

                            #region Provider Tax ID

                            if (oTranDetails.HCFA_ProviderEIN.Trim() != "")
                            {
                                form.set_Element(25, "taxid", oTranDetails.HCFA_ProviderEIN.Trim());

                                form.set_Element(25, "ssn", "");
                                form.set_Element(25, "ein", "1");
                            }
                            else if (oTranDetails.HCFA_ProviderSSN.Trim() != "")
                            {
                                form.set_Element(25, "taxid", oTranDetails.HCFA_ProviderSSN.Trim());
                                form.set_Element(25, "ssn", "1");
                                form.set_Element(25, "ein", "");
                            }

                            #endregion Provider Tax ID

                            #region Patient Account No

                            form.set_Element(26, "acct-num",oTranDetails.HCFA_PatientCode.Trim());

                            #endregion Patient Account No

                            #region Assignment of Benefits
                            
                            form.set_Element(27, "asg-yes", "1");
                            form.set_Element(27, "asg-no", "");

                            #endregion Assignment of Benefits

                            #region Total
                            form.set_Element(28, "total", Convert.ToString(TotalCharges));
                            #endregion Total

                            #region Paid Amount
                            form.set_Element(29, "paid", Convert.ToString(PaidAmount));
                            #endregion  Paid Amount

                            #region Balance Due
                            form.set_Element(30, "due", Convert.ToString(TotalCharges - PaidAmount));
                            #endregion Balance Due

                            #region Provider Sign, Name and Date

                            form.set_Element(31, "signed", "Signature on File");
                            form.set_Element(31, "m", DateTime.Now.Month.ToString());
                            form.set_Element(31, "d", DateTime.Now.Date.ToString());
                            form.set_Element(31, "y", DateTime.Now.Year.ToString());
                            form.set_Element(31, "name", oTranDetails.HCFA_ProviderFName.Trim()+" "+oTranDetails.HCFA_ProviderLName.Trim());

                            #endregion Provider Sign, Name and Date

                            #region Facility Details

                            form.set_Element(32, "name", oTranDetails.HCFA_FacilityName.Trim());
                            form.set_Element(32, "addr", oTranDetails.HCFA_FacilityAddress1.Trim());
                            form.set_Element(32, "city", oTranDetails.HCFA_FacilityCity.Trim());
                            form.set_Element(32, "state", oTranDetails.HCFA_FacilityState.Trim());
                            form.set_Element(32, "zip", oTranDetails.HCFA_FacilityZip.Trim());
                            form.set_Element(32, "npi", oTranDetails.HCFA_FacilityNPI.Trim());
                            form.set_Element(32, "id-num", "");
                            form.set_Element(32, "clia", "");
                            form.set_Element(32, "mammo", "");

                            #endregion Facility Details

                            #region Billing Provider

                            form.set_Element(33, "name", oTranDetails.HCFA_ProviderFName.Trim() + " " + oTranDetails.HCFA_ProviderLName.Trim());
                            form.set_Element(33, "addr", oTranDetails.HCFA_ProviderAddress1.Trim());
                            form.set_Element(33, "city", oTranDetails.HCFA_ProviderCity.Trim());
                            form.set_Element(33, "state", oTranDetails.HCFA_ProviderState.Trim());
                            form.set_Element(33, "zip", oTranDetails.HCFA_ProviderZip.Trim());
                            form.set_Element(33, "phone", oTranDetails.HCFA_ProviderPhone.Trim());
                            form.set_Element(33, "npi", oTranDetails.HCFA_ProviderNPI.Trim());
                            form.set_Element(33, "id-num", "");

                            #endregion Billing Provider

                            form.EndEncounter();	// End Encounter 
                            form.EndEncounters(); // End Encounters

                            string xml = form.XML;
                            //EDIData.Text = xml;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                gloAuditTrail.gloAuditTrail.ExceptionLog(ex.ToString(), true);

            }

        }

    }

    interface ClaimForm
    {
        string XML
        {
            get;
            set;
        }

        void StartEncounters();

        void StartEncounter(int index);

        void set_Element(int field, string attribute, string value);

        void set_LineItemElement(int line, string attribute, string value);

        void EndEncounter();

        void EndEncounters();
    }

    class H1500 : ClaimForm
    {
        private static string nl = System.Environment.NewLine;

        private StringDictionary openEncounter;

        public H1500()
        {
        }

        //public string Submitter;
        //public string UserID;
        //public string Password;
        //public string Server;
        //public string RPTType;

        public string m_XML;
        public string XML
        {
            get
            {
                return m_XML;
            }
            set
            {
                m_XML = value;
            }
        }

        public void StartEncounters()
        {
            this.XML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + nl;
            this.XML += "<encounters type=\"CMS-1500\">" + nl;
        }

        public void StartEncounter(int index)
        {
            this.XML += "\t<encounter seq=\"" + Convert.ToString(index) + "\">" + nl;
            openEncounter = new StringDictionary();
        }

        public void set_Element(int field, string attribute, string value)
        {
            openEncounter.Add(Convert.ToString(field) + "." + attribute, value);
        }

        public void set_LineItemElement(int line, string attribute, string value)
        {
            openEncounter.Add("line." + Convert.ToString(line) + "." + attribute, value);
        }

        public void EndEncounters()
        {
            this.XML += "</encounters>";
        }

        public void EndEncounter()
        {
            StringBuilder output = new StringBuilder();
            int i = 0;
            int j, k;
            string name;
            while (i < emptyEncounter.Length)
            {
                j = emptyEncounter.IndexOf("{", i);
                if (j == -1)
                {
                    break;
                }
                k = emptyEncounter.IndexOf("}", j);
                if (k == -1)
                {
                    break;
                }
                output.Append(emptyEncounter.Substring(i, j - i));
                name = openEncounter[emptyEncounter.Substring(j + 1, k - j - 1)];
                output.Append("\"" + ((name != null) ? name : "") + "\"");
                i = k + 1;
            }
            output.Append(emptyEncounter.Substring(i));


            this.XML += output.ToString() + nl + "\t</encounter>" + nl;

            openEncounter = null;
        }

        private const string emptyEncounter = @"		<fld-00 payer={0.payer} addr={0.addr} city={0.city} state={0.state} zip={0.zip} payer-id={0.payer-id} />
		<fld-01 medicare={1.medicare} medicaid={1.medicaid} champus={1.champus} champva={1.champva} group={1.group} feca={1.feca} oth={1.oth} ins-id={1.ins-id} auto={1.auto} wc={1.wc} com={1.com} fbcs={1.fbcs} />
		<fld-02 last={2.last} first={2.first} middle={2.middle} />
		<fld-03 m={3.m} d={3.d} y={3.y} sex-m={3.sex-m} sex-f={3.sex-f} />
		<fld-04 last={4.last} first={4.first} middle={4.middle} />
		<fld-05 addr={5.addr} city={5.city} state={5.state} zip={5.zip} phone={5.phone} />
		<fld-06 self={6.self} spouse={6.spouse} child={6.child} other={6.other} />
		<fld-07 addr={7.addr} city={7.city} state={7.state} zip={7.zip} phone={7.phone} />
		<fld-08 single={8.single} married={8.married} other={8.other} full-time={8.full-time} part-time={8.part-time} />
		<fld-09 last={9.last} first={9.first} middle={9.middle} policy={9.policy} dobm={9.dobm} dobd={9.dobd} doby={9.doby} sex-m={9.sex-m} sex-f={9.sex-f} />
		<fld-10 employ-yes={10.employ-yes} employ-no={10.employ-no} auto-yes={10.auto-yes} auto-no={10.auto-no} auto-state={10.auto-state} oth-yes={10.oth-yes} oth-no={10.oth-no} />
		<fld-11 policy={11.policy} m={11.m} d={11.d} y={11.y} sex-m={11.sex-m} sex-f={11.sex-f} emp-name={11.emp-name} plan-name={11.plan-name} oth-yes={11.oth-yes} oth-no={11.oth-no} />
		<fld-12 signed={12.signed} m={12.m} d={12.d} y={12.y} />
		<fld-13 signed={13.signed} />
		<fld-14 m={14.m} d={14.d} y={14.y} />
		<fld-15 m={15.m} d={15.d} y={15.y} />
		<fld-16 fm={16.fm} fd={16.fd} fy={16.fy} tm={16.tm} td={16.td} ty={16.ty} />
		<fld-17 name={17.name} id-qual={17.id-qual} id-num={17.id-num} npi={17.npi} />
		<fld-18 fm={18.fm} fd={18.fd} fy={18.fy} tm={18.tm} td={18.td} ty={18.ty} />
		<fld-19 area1={19.area1} area2={19.area2} />
		<fld-20 out-yes={20.out-yes} out-no={20.out-no} charges={20.charges} />
		<fld-21 dx1={21.dx1} dx2={21.dx2} dx3={21.dx3} dx4={21.dx4} />
		<fld-22 resub={22.resub} org={22.org} />
		<fld-23 prior={23.prior} />
		<fld-24>
			<itm01 fm={line.1.fm} fd={line.1.fd} fy={line.1.fy} tm={line.1.tm} td={line.1.td} ty={line.1.ty} pos={line.1.pos} tos={line.1.tos} cpt={line.1.cpt} m1={line.1.m1} m2={line.1.m2} m3={line.1.m3} ptr={line.1.ptr} charge={line.1.charge} unit={line.1.unit} epsdt={line.1.epsdt} emg={line.1.emg} cob={line.1.cob} res={line.1.res} id-qual={line.1.id-qual} id-num={line.1.id-num} npi={line.1.npi} />
			<itm02 fm={line.2.fm} fd={line.2.fd} fy={line.2.fy} tm={line.2.tm} td={line.2.td} ty={line.2.ty} pos={line.2.pos} tos={line.2.tos} cpt={line.2.cpt} m1={line.2.m1} m2={line.2.m2} m3={line.2.m3} ptr={line.2.ptr} charge={line.2.charge} unit={line.2.unit} epsdt={line.2.epsdt} emg={line.2.emg} cob={line.2.cob} res={line.2.res} id-qual={line.2.id-qual} id-num={line.2.id-num} npi={line.2.npi} />
			<itm03 fm={line.3.fm} fd={line.3.fd} fy={line.3.fy} tm={line.3.tm} td={line.3.td} ty={line.3.ty} pos={line.3.pos} tos={line.3.tos} cpt={line.3.cpt} m1={line.3.m1} m2={line.3.m2} m3={line.3.m3} ptr={line.3.ptr} charge={line.3.charge} unit={line.3.unit} epsdt={line.3.epsdt} emg={line.3.emg} cob={line.3.cob} res={line.3.res} id-qual={line.3.id-qual} id-num={line.3.id-num} npi={line.3.npi} />
			<itm04 fm={line.4.fm} fd={line.4.fd} fy={line.4.fy} tm={line.4.tm} td={line.4.td} ty={line.4.ty} pos={line.4.pos} tos={line.4.tos} cpt={line.4.cpt} m1={line.4.m1} m2={line.4.m2} m3={line.4.m3} ptr={line.4.ptr} charge={line.4.charge} unit={line.4.unit} epsdt={line.4.epsdt} emg={line.4.emg} cob={line.4.cob} res={line.4.res} id-qual={line.4.id-qual} id-num={line.4.id-num} npi={line.4.npi} />
			<itm05 fm={line.5.fm} fd={line.5.fd} fy={line.5.fy} tm={line.5.tm} td={line.5.td} ty={line.5.ty} pos={line.5.pos} tos={line.5.tos} cpt={line.5.cpt} m1={line.5.m1} m2={line.5.m2} m3={line.5.m3} ptr={line.5.ptr} charge={line.5.charge} unit={line.5.unit} epsdt={line.5.epsdt} emg={line.5.emg} cob={line.5.cob} res={line.5.res} id-qual={line.5.id-qual} id-num={line.5.id-num} npi={line.5.npi} />
			<itm06 fm={line.6.fm} fd={line.6.fd} fy={line.6.fy} tm={line.6.tm} td={line.6.td} ty={line.6.ty} pos={line.6.pos} tos={line.6.tos} cpt={line.6.cpt} m1={line.6.m1} m2={line.6.m2} m3={line.6.m3} ptr={line.6.ptr} charge={line.6.charge} unit={line.6.unit} epsdt={line.6.epsdt} emg={line.6.emg} cob={line.6.cob} res={line.6.res} id-qual={line.6.id-qual} id-num={line.6.id-num} npi={line.6.npi} />
		</fld-24>
		<fld-25 taxid={25.taxid} ssn={25.ssn} ein={25.ein} />
		<fld-26 acct-num={26.acct-num} />
		<fld-27 asg-yes={27.asg-yes} asg-no={27.asg-no} />
		<fld-28 total={28.total} />
		<fld-29 paid={29.paid} />
		<fld-30 due={30.due} />
		<fld-31 signed={31.signed} m={31.m} d={31.d} y={31.y} name={31.name} />
		<fld-32 name={32.name} addr={32.addr} city={32.city} state={32.state} zip={32.zip} npi={32.npi} id-num={32.id-num} clia={32.clia} mammo={32.mammo} />
		<fld-33 name={33.name} addr={33.addr} city={33.city} state={33.state} zip={33.zip} phone={33.phone} npi={33.npi} id-num={33.id-num} />";

    }

    class H1450 : ClaimForm
    {
        private static string nl = System.Environment.NewLine;


        private StringDictionary openEncounter;

        public H1450()
        {
        }

        public string m_XML;
        public string XML
        {
            get
            {
                return m_XML;
            }
            set
            {
                m_XML = value;
            }
        }

        public void StartEncounters()
        {
            this.XML = "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + nl;
            this.XML += "<encounters type=\"UB04-1450\">" + nl;
        }

        public void StartEncounter(int index)
        {
            this.XML += "\t<encounter seq=\"" + Convert.ToString(index) + "\">" + nl;
            openEncounter = new StringDictionary();
        }

        public void set_Element(int field, string attribute, string value)
        {
            openEncounter.Add(Convert.ToString(field) + "." + attribute, value);
        }

        public void set_LineItemElement(int line, string attribute, string value)
        {
            openEncounter.Add("line." + Convert.ToString(line) + "." + attribute, value);
        }

        public void EndEncounters()
        {
            this.XML += "</encounters>";
        }

        public void EndEncounter()
        {
            StringBuilder output = new StringBuilder();
            int i = 0;
            int j, k;
            string name;
            while (i < emptyEncounter.Length)
            {
                j = emptyEncounter.IndexOf("{", i);
                if (j == -1)
                {
                    break;
                }
                k = emptyEncounter.IndexOf("}", j);
                if (k == -1)
                {
                    break;
                }
                output.Append(emptyEncounter.Substring(i, j - i));
                name = openEncounter[emptyEncounter.Substring(j + 1, k - j - 1)];
                output.Append("\"" + ((name != null) ? name : "") + "\"");
                i = k + 1;
            }
            output.Append(emptyEncounter.Substring(i));


            this.XML += output.ToString() + nl + "\t</encounter>" + nl;

            openEncounter = null;
        }

        //{1.xxxx}
        private const string emptyEncounter = @"		<fld-01 name={1.name} addr={1.addr} city={1.city} state={1.state} zip={1.zip} phone={1.phone} />
		<fld-02 name={2.name} addr={2.addr} city={2.city} state={2.state} zip={2.zip} phone={2.phone} />
		<fld-03 patctlno={3.patctlno} mrn={3.mrn} />
		<fld-04 typeofbill={4.typeofbill} />
		<fld-05 fedtaxnum={5.fedtaxnum} />
		<fld-06 fdate={6.fdate} tdate={6.tdate} />
		<fld-07 data={7.data} />
		<fld-08 patname-id={8.patname-id} patname={8.patname} />
		<fld-09 addr={9.addr} city={9.city} state={9.state} zip={9.zip} country={9.country} />
		<fld-10 date={10.date} />
		<fld-11 sex={11.sex} />
		<fld-12 date={12.date} />
		<fld-13 hr={13.hr} />
		<fld-14 type={14.type} />
		<fld-15 src={15.src} />
		<fld-16 dhr={16.dhr} />
		<fld-17 stat={17.stat} />
		<fld-18 code={18.code} />
		<fld-19 code={19.code} />
		<fld-20 code={20.code} />
		<fld-21 code={21.code} />
		<fld-22 code={22.code} />
		<fld-23 code={23.code} />
		<fld-24 code={24.code} />
		<fld-25 code={25.code} />
		<fld-26 code={26.code} />
		<fld-27 code={27.code} />
		<fld-28 code={28.code} />
		<fld-29 auto-state={29.auto-state} />
		<fld-30 data={30.data} />
		<fld-31 a-code={31.a-code} a-date={31.a-date} b-code={31.b-code} b-date={31.b-date} />
		<fld-32 a-code={32.a-code} a-date={32.a-date} b-code={32.b-code} b-date={32.b-date} />
		<fld-33 a-code={33.a-code} a-date={33.a-date} b-code={33.b-code} b-date={33.b-date} />
		<fld-34 a-code={34.a-code} a-date={34.a-date} b-code={34.b-code} b-date={34.b-date} />
		<fld-35 a-code={35.a-code} a-fdate={35.a-fdate} a-tdate={35.a-tdate} b-code={35.b-code} b-fdate={35.b-fdate} b-tdate={35.b-tdate} />
		<fld-36 a-code={36.a-code} a-fdate={36.a-fdate} a-tdate={36.a-tdate} b-code={36.b-code} b-fdate={36.b-fdate} b-tdate={36.b-tdate} />
		<fld-37 a={37.s} b={37.b} />
		<fld-38 name={38.name} addr={38.addr} city={38.city} state={38.state} zip={38.zip} country={38.country} phone={38.phone} />
		<fld-39 a-code={39.a-code} a-amount={39.a-amount} b-code={39.b-code} b-amount={39.b-amount} c-code={39.c-code} c-amount={39.c-amount} d-code={39.d-code} d-amount={39.d-amount} />
		<fld-40 a-code={40.a-code} a-amount={40.a-amount} b-code={40.b-code} b-amount={40.b-amount} c-code={40.c-code} c-amount={40.c-amount} d-code={40.d-code} d-amount={40.d-amount} />
		<fld-41 a-code={41.a-code} a-amount={41.a-amount} b-code={41.b-code} b-amount={41.b-amount} c-code={41.c-code} c-amount={41.c-amount} d-code={41.d-code} d-amount={41.d-amount} />
		<items>
			<itm01 fld-42={line.1.fld-42} fld-43={line.1.fld-43} fld-44={line.1.fld-44} fld-45={line.1.fld-45} fld-46={line.1.fld-46} fld-47={line.1.fld-47} fld-48={line.1.fld-48} fld-49={line.1.fld-49} />
			<itm02 fld-42={line.2.fld-42} fld-43={line.2.fld-43} fld-44={line.2.fld-44} fld-45={line.2.fld-45} fld-46={line.2.fld-46} fld-47={line.2.fld-47} fld-48={line.2.fld-48} fld-49={line.2.fld-49} />
			<itm03 fld-42={line.3.fld-42} fld-43={line.3.fld-43} fld-44={line.3.fld-44} fld-45={line.3.fld-45} fld-46={line.3.fld-46} fld-47={line.3.fld-47} fld-48={line.3.fld-48} fld-49={line.3.fld-49} />
			<itm04 fld-42={line.4.fld-42} fld-43={line.4.fld-43} fld-44={line.4.fld-44} fld-45={line.4.fld-45} fld-46={line.4.fld-46} fld-47={line.4.fld-47} fld-48={line.4.fld-48} fld-49={line.4.fld-49} />
			<itm05 fld-42={line.5.fld-42} fld-43={line.5.fld-43} fld-44={line.5.fld-44} fld-45={line.5.fld-45} fld-46={line.5.fld-46} fld-47={line.5.fld-47} fld-48={line.5.fld-48} fld-49={line.5.fld-49} />
			<itm06 fld-42={line.6.fld-42} fld-43={line.6.fld-43} fld-44={line.6.fld-44} fld-45={line.6.fld-45} fld-46={line.6.fld-46} fld-47={line.6.fld-47} fld-48={line.6.fld-48} fld-49={line.6.fld-49} />
			<itm07 fld-42={line.7.fld-42} fld-43={line.7.fld-43} fld-44={line.7.fld-44} fld-45={line.7.fld-45} fld-46={line.7.fld-46} fld-47={line.7.fld-47} fld-48={line.7.fld-48} fld-49={line.7.fld-49} />
			<itm08 fld-42={line.8.fld-42} fld-43={line.8.fld-43} fld-44={line.8.fld-44} fld-45={line.8.fld-45} fld-46={line.8.fld-46} fld-47={line.8.fld-47} fld-48={line.8.fld-48} fld-49={line.8.fld-49} />
			<itm09 fld-42={line.9.fld-42} fld-43={line.9.fld-43} fld-44={line.9.fld-44} fld-45={line.9.fld-45} fld-46={line.9.fld-46} fld-47={line.9.fld-47} fld-48={line.9.fld-48} fld-49={line.9.fld-49} />
			<itm10 fld-42={line.10.fld-42} fld-43={line.10.fld-43} fld-44={line.10.fld-44} fld-45={line.10.fld-45} fld-46={line.10.fld-46} fld-47={line.10.fld-47} fld-48={line.10.fld-48} fld-49={line.10.fld-49} />
			<itm11 fld-42={line.11.fld-42} fld-43={line.11.fld-43} fld-44={line.11.fld-44} fld-45={line.11.fld-45} fld-46={line.11.fld-46} fld-47={line.11.fld-47} fld-48={line.11.fld-48} fld-49={line.11.fld-49} />
			<itm12 fld-42={line.12.fld-42} fld-43={line.12.fld-43} fld-44={line.12.fld-44} fld-45={line.12.fld-45} fld-46={line.12.fld-46} fld-47={line.12.fld-47} fld-48={line.12.fld-48} fld-49={line.12.fld-49} />
			<itm13 fld-42={line.13.fld-42} fld-43={line.13.fld-43} fld-44={line.13.fld-44} fld-45={line.13.fld-45} fld-46={line.13.fld-46} fld-47={line.13.fld-47} fld-48={line.13.fld-48} fld-49={line.13.fld-49} />
			<itm14 fld-42={line.14.fld-42} fld-43={line.14.fld-43} fld-44={line.14.fld-44} fld-45={line.14.fld-45} fld-46={line.14.fld-46} fld-47={line.14.fld-47} fld-48={line.14.fld-48} fld-49={line.14.fld-49} />
			<itm15 fld-42={line.15.fld-42} fld-43={line.15.fld-43} fld-44={line.15.fld-44} fld-45={line.15.fld-45} fld-46={line.15.fld-46} fld-47={line.15.fld-47} fld-48={line.15.fld-48} fld-49={line.15.fld-49} />
			<itm16 fld-42={line.16.fld-42} fld-43={line.16.fld-43} fld-44={line.16.fld-44} fld-45={line.16.fld-45} fld-46={line.16.fld-46} fld-47={line.16.fld-47} fld-48={line.16.fld-48} fld-49={line.16.fld-49} />
			<itm17 fld-42={line.17.fld-42} fld-43={line.17.fld-43} fld-44={line.17.fld-44} fld-45={line.17.fld-45} fld-46={line.17.fld-46} fld-47={line.17.fld-47} fld-48={line.17.fld-48} fld-49={line.17.fld-49} />
			<itm18 fld-42={line.18.fld-42} fld-43={line.18.fld-43} fld-44={line.18.fld-44} fld-45={line.18.fld-45} fld-46={line.18.fld-46} fld-47={line.18.fld-47} fld-48={line.18.fld-48} fld-49={line.18.fld-49} />
			<itm19 fld-42={line.19.fld-42} fld-43={line.19.fld-43} fld-44={line.19.fld-44} fld-45={line.19.fld-45} fld-46={line.19.fld-46} fld-47={line.19.fld-47} fld-48={line.19.fld-48} fld-49={line.19.fld-49} />
			<itm20 fld-42={line.20.fld-42} fld-43={line.20.fld-43} fld-44={line.20.fld-44} fld-45={line.20.fld-45} fld-46={line.20.fld-46} fld-47={line.20.fld-47} fld-48={line.20.fld-48} fld-49={line.20.fld-49} />
			<itm21 fld-42={line.21.fld-42} fld-43={line.21.fld-43} fld-44={line.21.fld-44} fld-45={line.21.fld-45} fld-46={line.21.fld-46} fld-47={line.21.fld-47} fld-48={line.21.fld-48} fld-49={line.21.fld-49} />
			<itm22 fld-42={line.22.fld-42} fld-43={line.22.fld-43} fld-44={line.22.fld-44} fld-45={line.22.fld-45} fld-46={line.22.fld-46} fld-47={line.22.fld-47} fld-48={line.22.fld-48} fld-49={line.22.fld-49} />
			<itm23 page={line.23.page} of={line.23.of} date={line.23.date} total={line.23.total} total-nc={line.23.total-nc} />
		</items>
		<fld-50 payer1={50.payer1} payer2={50.payer2} payer3={50.payer3} fbcs={50.fbcs} />
		<fld-51 payer1-id={51.payer1-id} payer2-id={51.payer2-id} payer3-id={51.payer3-id} />
		<fld-52 roi1={52.roi1} roi2={52.roi2} roi3={52.roi3} />
		<fld-53 aa1={53.aa1} aa2={53.aa2} aa3={53.aa3} />
		<fld-54 paid1={54.paid1} paid2={54.paid2} paid3={54.paid3} />
		<fld-55 due1={55.due1} due2={55.due2} due3={55.due3} />
		<fld-56 npi={56.npi} />
		<fld-57 id1={57.id1} id2={57.id2} id3={57.id3} />
		<fld-58 insured1={58.insured1} insured2={58.insured2} insured3={58.insured3} />
		<fld-59 rel1={59.rel1} rel2={59.rel2} rel3={59.rel3} />
		<fld-60 ins-id1={60.ins-id1} ins-id2={60.ins-id2} ins-id3={60.ins-id3} />
		<fld-61 group1={61.group1} group2={61.group2} group3={61.group3} />
		<fld-62 group-id1={62.group-id1} group-id2={62.group-id2} group-id3={62.group-id3} />
		<fld-63 auth1={63.auth1} auth2={63.auth2} auth3={63.auth3} />
		<fld-64 dcn1={64.dcn1} dcn2={64.dcn2} dcn3={64.dcn3} />
		<fld-65 emp1={65.emp1} emp2={65.emp2} emp3={65.emp3} />
		<fld-66 dx-qual={66.xxxx} />
		<fld-67 pri-diag={67.pri-diag} a={67.a} b={67.b} c={67.c} d={67.d} e={67.e} f={67.f} g={67.g} h={67.h} i={67.i} j={67.j} k={67.k} l={67.l} m={67.m} n={67.n} o={67.o} p={67.p} q={67.q} />
		<fld-68 a={68.a} b={68.b} />
		<fld-69 admit-dx={69.admit-dx} />
		<fld-70 reason-dx1={70.reason-dx1} reason-dx2={70.reason-dx2} reason-dx3={70.reason-dx3} />
		<fld-71 pps={71.pps} />
		<fld-72 ecode1={72.ecode1} ecode2={72.ecode2} ecode3={72.ecode3} />
		<fld-73 data={73.data} />
		<fld-74 pri-code={74.pri-code} date={74.date} />
		<fld-75 a-code={75.a-code} a-date={75.a-date} b-code={75.b-code} b-date={75.b-date} c-code={75.c-code} c-date={75.c-date} d-code={75.d-code} d-date={75.d-date} e-code={75.e-code} e-date={75.e-date} />
		<fld-76 npi={76.npi} id-qual={76.id-qual} id-num={76.id-num} name={76.name} />
		<fld-77 npi={77.npi} id-qual={77.id-qual} id-num={77.id-num} name={77.name} />
		<fld-78 npi={78.npi} id-qual={78.id-qual} id-num={78.id-num} name={78.name} />
		<fld-79 npi={79.npi} id-qual={79.id-qual} id-num={79.id-num} name={79.name} />
		<fld-80 a={80.a} b={80.b} c={80.c} d={80.d} />
		<fld-81 qual1={81.qual1} code1={81.code1} value1={81.value1} qual2={81.qual2} code2={81.code2} value2={81.value2} qual3={81.qual3} code3={81.code3} value3={81.value3} qual4={81.qual4} code4={81.code4} value4={81.value4} />";

    }
}
