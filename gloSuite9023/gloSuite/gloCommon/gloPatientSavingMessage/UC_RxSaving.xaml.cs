using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using gloPatientSaving;
using gloRxPatientSaving;
using System.Data.Linq;
using System.IO;
using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace gloPatientSavingMessage
{
    /// <summary>
    /// Interaction logic for UC_RxSaving.xaml
    /// </summary>
    public partial class UC_RxSaving : System.Windows.Controls.UserControl 
    {
        public event tStrpCloseClickEventHandler tlbCloseClick;
        public delegate void tStrpCloseClickEventHandler(System.Object sender, System.EventArgs e);
        public string PatientCode = string.Empty;
        public UC_RxSaving(IQueryable<PatSav_Mst> oPatientSaving,string _PatientCode)
        {
            objPatientSaving = oPatientSaving.ToList();
            PatientCode = _PatientCode;
            InitializeComponent();

            if (objPatientSaving != null)
            {
                _OppLength = objPatientSaving.Count();
                BindData();
            }
        }

         public delegate void OpensavingPrescription(ArrayList arrdrug, Int64 ProviderId, Int64 m_Visitid = 0, Int64 m_Patientid = 0, Boolean ShowCarryForwardMessage = true);
            public OpensavingPrescription EntOpensavePrescription;

            Int32 _OppCounter = 0;
            List<PatSav_Mst> objPatientSaving = null;
            Int32 _OppLength = 0;
            gloEMRGeneralLibrary.gloEMRActors.Drug oDrug;
            bool _IsPendingMessage = true;
            
            private void BindData()
            {
                try
                {
                    if (PatSavGeneral.objDataContext != null)
                        PatSavGeneral.objDataContext.Dispose();
                    PatSavGeneral.objDataContext = new PatSavDataClassDataContext(PatSavGeneral.ConnectionString);

                    if (objPatientSaving != null && objPatientSaving.Count() > 0 && _OppCounter >= 0 && _OppCounter < objPatientSaving.Count())
                    {
                        tlb3090.Margin = new Thickness(9,0,-8.875,-2);
                        tlb3090.Visibility = System.Windows.Visibility.Visible;
                        tlbGeneric.Visibility = System.Windows.Visibility.Visible;
                        PatSav_Mst oPatSav_Mst = objPatientSaving.ToList()[_OppCounter];
                        tbPatientCode.Text = PatientCode;
                        PatientStrip.DataContext = oPatSav_Mst;
                        if (oPatSav_Mst.PatGender != null)
                        {
                            switch(oPatSav_Mst.PatGender)
                            {
                            case "M":tbPatientGender.Text ="Male";break;
                            case "F" :tbPatientGender.Text ="Female";break;
                            default: tbPatientGender.Text = "Other"; break;
                            }
                        }
                        
                        tbPatientBorn.Text = oPatSav_Mst.PatDOB.Value.ToShortDateString();
                        stkGenericOpportunity.Tag = oPatSav_Mst.PSID;

                        stkGenericOpportunity.Children.Clear();
                        stk3090Opportunity.Children.Clear();
                        
                        List<PatSavOpportunity_Mst> _objOpp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts where result.PSID == oPatSav_Mst.PSID select result).ToList();
                        if (_IsPendingMessage == true)
                        {
                            List<PatSavOpportunity_Mst> res = (from r in _objOpp where r.DispositionFile == null select r).ToList();
                            _objOpp = res;
                        }

                        foreach (PatSavOpportunity_Mst obj in _objOpp)
                        {
                            CreateDynamicExpander(obj);
                        }
                        if (stk3090Opportunity.Children.Count <= 0)
                        {
                            tlb3090.Visibility = System.Windows.Visibility.Hidden;
                            tlbOpp.SelectedIndex = 0;
                            tlbGeneric.Focus();
                        }
                        if (stkGenericOpportunity.Children.Count <= 0)
                        {
                            tlb3090.Margin = new Thickness(-117, 0, 98.1, -2);
                            tlbGeneric.Visibility = System.Windows.Visibility.Hidden;
                            tlbOpp.SelectedIndex = 1;
                            tlb3090.Focus();
                        }

                        if (_OppCounter + 1 == _OppLength)
                            btnNext.IsEnabled = false;
                        else
                            btnNext.IsEnabled = true;

                        if (_OppCounter == 0)
                            btnPrevious.IsEnabled = false;
                        else
                            btnPrevious.IsEnabled = true;

                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.ViewRxSaving, gloAuditTrail.ActivityType.View, "RxSaving Viewed", (long)oPatSav_Mst.nPatientID.Value, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.ViewRxSaving, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    return;
                }

            }

            private void CreateDynamicExpander(PatSavOpportunity_Mst oPatSavOppMST)
            {
                try
                {
                    UC_GenericAlternative _OppControl = new UC_GenericAlternative(oPatSavOppMST);
                    _OppControl.DataContext = oPatSavOppMST;
                    if (oPatSavOppMST.OpportunityType.ToLower() == "generic_alternative")
                    {
                        stkGenericOpportunity.Children.Add(_OppControl);
                    }
                    else
                    {
                        stk3090Opportunity.Children.Add(_OppControl);
                    }

                }
                catch //(Exception ex)
                { }

            }

            private void btnClose_Click(object sender, RoutedEventArgs e)
            {
                tlbCloseClick(sender ,e);
            }

            private void btnPrevious_Click(object sender, RoutedEventArgs e)
            {
                _OppCounter = _OppCounter - 1;
                BindData();
            }

            private void btnNext_Click(object sender, RoutedEventArgs e)
            {
                _OppCounter = _OppCounter + 1;
                BindData();
            }

            class Drugs
            {
                //public string NDCCode;
                //public string DrugName;
                public Int64 PSOppID;
                public Int64 AltID;
                public string OpportunityID;
                public string OpportunityType;
                public string OrgRxDrugQty;
                public string OrgRxDaysSupply;
                //public string OrgRxDrugQlfr;
                //public string OrgRxDrugCode;
                public string DrugCode;
                public string DrugQlfr;
                public string DrugDesc;

                public Drugs(Int64 _PSOppID, string _OpportunityID, string _OpportunityType, string _OrgRxDrugQty, string _OrgRxDaysSupply, string _DrugCode, string _DrugQlfr, string _DrugDesc,Int64 _AltID)
                {

                    //string _NDCCode, string _DrugName,string _OrgRxDrugQlfr, string _OrgRxDrugCode,
                    //this.NDCCode = _NDCCode;
                    //this.DrugName = _DrugName;
                    this.OpportunityID = _OpportunityID;
                    this.OpportunityType = _OpportunityType;
                    this.OrgRxDrugQty = _OrgRxDrugQty;
                    this.OrgRxDaysSupply = _OrgRxDaysSupply;
                    //this.OrgRxDrugQlfr = _OrgRxDrugQlfr;
                    //this.OrgRxDrugCode = _OrgRxDrugCode;
                    this.DrugCode = _DrugCode;
                    this.DrugQlfr = _DrugQlfr;
                    this.DrugDesc = _DrugDesc;
                    this.PSOppID = _PSOppID;
                    this.AltID = _AltID;
                }
            }

            private void btnSave_Cls_Click(object sender, RoutedEventArgs e)
            {
                generateOpportunityResponse();
                tlbCloseClick(sender, e);
            }

            private void generateOpportunityResponse()
            {
                OpportunityResponseType objOppResponse = null;
                try
                {

                            PatSav_Mst oPatSav_Mst = objPatientSaving[_OppCounter];
                            var _objOpp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts  where result.PSID == oPatSav_Mst.PSID &&  result.DispositionCode != null && result.DispositionFile == null select result);
                            gloSurescriptSecureMessage.clsSecureMessageDB oclsSecureMessageDB = new gloSurescriptSecureMessage.clsSecureMessageDB();
                            DataTable dt;
                            List<gloSurescriptSecureMessage.Attachment> oLsAttach = null;

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
                                {
                                    objOppResponse.Patient.CommunicationNumbers = null;
                                }

                                objOppResponse.BenefitsCoordination = new OpportunityBenefitsCoordination();
                                objOppResponse.BenefitsCoordination.PBMMemberID = oPatSav_Mst.BenefitCoordinationPBMMemberID;

                                objOppResponse.OpportunityDisposition = new OpportunityDisposition();
                                objOppResponse.OpportunityDisposition.OpportunityID = oOppMSt.OpportunityID.ToString();
                                objOppResponse.OpportunityDisposition.OpportunityType = oOppMSt.OpportunityType;
                                objOppResponse.OpportunityDisposition.DispositionCode = oOppMSt.DispositionCode;
                                objOppResponse.OpportunityDisposition.DispositionDate = new gloPatientSaving.DateType();
                                objOppResponse.OpportunityDisposition.DispositionDate.Item = new DateTime();
                                objOppResponse.OpportunityDisposition.DispositionDate.Item = (DateTime)oOppMSt.DispositionDate;

                                string _FilePath = PatSavGeneral.getFileName();
                                if (gloSerialization.SetClinicalDocument(_FilePath, objOppResponse))
                                {
                                    PatSavOpportunity_Mst _opp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts where result.PSOppID == oOppMSt.PSOppID select result).FirstOrDefault();
                                    if (_opp != null)
                                    {
                                        _opp.DispositionFile = new Binary(File.ReadAllBytes(_FilePath));
                                    
                                        PatSavGeneral.objDataContext.SubmitChanges();
                                        var _Messageid = (from res in PatSavGeneral.objDataContext.PatSav_Queues where res.nPatSavQID == oPatSav_Mst.nPatSavQID select res.sMessageID).FirstOrDefault();
                                        if (_Messageid != null)
                                        {
                                            dt = oclsSecureMessageDB.GetSecureMessageDatailsUsingMessageID(_Messageid);
                                            oLsAttach = oclsSecureMessageDB.GetFileInformationForAttachment(_FilePath);
                                            if (dt != null)
                                            {
                                                if (dt.Rows.Count > 0)
                                                {
                                                    oclsSecureMessageDB.InsertOppSecureMessage(dt, oLsAttach, Convert.ToInt64(oPatSav_Mst.nPatientID));
                                                    gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.DispositionRxSaving, gloAuditTrail.ActivityType.SendDisposition, "RxSaving Dispostion send", (long)oPatSav_Mst.nPatientID.Value, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                                                }
                                            }
                                        }
                                        if (File.Exists(_FilePath))
                                            File.Delete(_FilePath);
                                    }
                                }
                            
                        PatSavGeneral.objDataContext.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues);
                    }
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.DispositionRxSaving, gloAuditTrail.ActivityType.SendDisposition, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                    return;
                }
                finally
                {
                    objOppResponse = null;
                    
                }
            }

            private void btnRx_Click(object sender, RoutedEventArgs e)
            {
                
                ArrayList lstDrugs = new ArrayList();
                ArrayList lstDrugsList = new ArrayList();
                gloGlobal.DIB.DrugDetails dtDrugInfo = null;
                PatSav_Mst oPatSav_Mst =null;
                clsDataInsertionLayer oDataInsertion = null;

                gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest oDrugInformation = new gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest();
                try
                {
                            oPatSav_Mst = objPatientSaving[_OppCounter];
                            var _objOpp = (from result in PatSavGeneral.objDataContext.PatSavOpportunity_Msts where (result.DispositionCode == "01" || result.DispositionCode == null) && result.PSID == oPatSav_Mst.PSID select result);
                            
                            foreach (PatSavOpportunity_Mst oOppMSt in _objOpp)
                            {

                                if (oOppMSt.OpportunityType.ToLower() == "generic_alternative")
                                {


                                    lstDrugs = AcceptAlternative(lstDrugs, oOppMSt.PSOppID.ToString());
                                        
                                    
                                }
                                else
                                {
                                    if (oOppMSt.DispositionCode == null)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        lstDrugs.Add(new Drugs(Convert.ToInt64(oOppMSt.PSOppID), oOppMSt.OpportunityID, oOppMSt.OpportunityType, oOppMSt.OrgRxDrugQty.ToString(), oOppMSt.OrgRxDaysSupply.ToString(), oOppMSt.OrgRxDrugCode, oOppMSt.OrgRxDrugQlfr, oOppMSt.OrgRxDrugDesc,0));
                                    }
                                }
                            }
                        
                            for (int i = 0; i <= lstDrugs.Count - 1; i++)
                            {
                                ////if (GetDrugType(((Drugs)lstDrugs[i]).NDCCode.ToString()) == -1)
                                ////{
                                ////    if (System.Windows.Forms.MessageBox.Show(((Drugs)lstDrugs[i]).DrugName.ToString() + " not found in the drug database.Do you want to select the Drug", "gloEMR", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Yes)
                                ////    {

                                ////        string strdrugName = "";
                                ////        strdrugName = ((Drugs)lstDrugs[i]).DrugName.ToString();
                                ////        // showSearchUserControl(strdrugName);
                                ////        frmSearchDrugInfo oSearchDrug;
                                ////        oSearchDrug = new frmSearchDrugInfo(strdrugName);
                                ////        oSearchDrug.ShowDialog();
                                ////        if (oSearchDrug.NDCCode != null)
                                ////        { 
                                ////            dtDrugInfo = oDrugInformation.GetDrugInfoFromNDCCode(oSearchDrug.NDCCode);
                                ////        }

                                ////    }
                                ////}
                                ////else
                                ////{
                                ////    dtDrugInfo = oDrugInformation.GetDrugInfoFromNDCCode(((Drugs)lstDrugs[i]).NDCCode.ToString());
                                ////}


                                if (((Drugs)lstDrugs[i]).DrugQlfr.ToString() == "ND")
                                {
                                    dtDrugInfo = oDrugInformation.GetDrugInfoFromNDCCode(((Drugs)lstDrugs[i]).DrugCode.ToString());
                                }
                                else if (((Drugs)lstDrugs[i]).DrugQlfr.ToString() == "MG")
                                {
                                    dtDrugInfo = oDrugInformation.GetDrugInfoFromGPI(((Drugs)lstDrugs[i]).DrugCode.ToString(), ((Drugs)lstDrugs[i]).DrugDesc.ToString());
                                }
                                else if (((Drugs)lstDrugs[i]).DrugQlfr.ToString() == "SBD" || ((Drugs)lstDrugs[i]).DrugQlfr.ToString() == "SCD" || ((Drugs)lstDrugs[i]).DrugQlfr.ToString() == "GPK" || ((Drugs)lstDrugs[i]).DrugQlfr.ToString() == "BPK")
                                {
                                    dtDrugInfo = oDrugInformation.GetDrugInfoFromRxNorm(((Drugs)lstDrugs[i]).DrugCode.ToString());
                                }
                                else
                                {
                                    System.Windows.Forms.MessageBox.Show(" Drug match not found. Please Send Disposition message.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }



                                if (dtDrugInfo != null)
                                {
                                    oDrug = new gloEMRGeneralLibrary.gloEMRActors.Drug();

                                    oDrug.DrugsID = 0; //Convert.ToInt64(dtDrugInfo.Rows[0]["ndrugsid"]);
                                    oDrug.DrugsName = dtDrugInfo.DrugName;// Convert.ToString(dtDrugInfo.Rows[0]["sDrugname"]);
                                    oDrug.Dosage = dtDrugInfo.Dosage;// Convert.ToString(dtDrugInfo.Rows[0]["sDosage"]);
                                    oDrug.DrugForm = dtDrugInfo.DrugForm;// Convert.ToString(dtDrugInfo.Rows[0]["sDrugForm"]);
                                    oDrug.Route = dtDrugInfo.Route;// Convert.ToString(dtDrugInfo.Rows[0]["sRoute"]);
                                    oDrug.Frequency = "";//Convert.ToString(dtDrugInfo.Rows[0]["sFrequency"]);
                                    oDrug.NDCCode = dtDrugInfo.NDC; //Convert.ToString(dtDrugInfo.Rows[0]["sNDCCode"]);
                                    oDrug.IsNarcotics = dtDrugInfo.IsNarcotics; //Convert.ToInt16(dtDrugInfo.Rows[0]["nIsNarcotics"]);
                                    oDrug.Duration = ((Drugs)lstDrugs[i]).OrgRxDaysSupply.ToString();
                                    oDrug.Amount = ((Drugs)lstDrugs[i]).OrgRxDrugQty.ToString();
                                    //oDrug.nddid = Convert.ToInt64(dtDrugInfo.Rows[0]["nddid"]);
                                    //oDrug.DrugQtyQualifier = CType(AssociateNode.Nodes.Item(j), myTreeNode).DrugQtyQualifier

                                    oDrug.OpportunityID = Convert.ToInt64(((Drugs)lstDrugs[i]).PSOppID);
                                    oDrug.AlternateID = Convert.ToInt64(((Drugs)lstDrugs[i]).AltID);
                                    lstDrugsList.Add(oDrug);

                                    if (oDrug != null)
                                    {
                                        oDrug.Dispose();
                                        oDrug = null;
                                    }
                                }
                                //else
                                //{
                                //    System.Windows.Forms.MessageBox.Show(" No Drug information found.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //    return;
                                //}
                            
                    }
                    if (lstDrugsList.Count >= 0)
                    {
                         oDataInsertion = new clsDataInsertionLayer();
                        //oPrescription.EntOpenPrescription = getPrescription;
                        //oPrescription.ShowPrescription(lstDrugsList, 0);

                        // oPrescription.EntOpenPrescription += oPrescription.EntOpenPrescription(lstDrugsList, 0);
                        long ProviderID = oDataInsertion.GetProviderID(Convert.ToInt64(oPatSav_Mst.nPatientID));
                        if ((oDataInsertion != null))
                        {
                            oDataInsertion = null;
                        }
                        getPrescription(lstDrugsList, ProviderID, 0, Convert.ToInt64(oPatSav_Mst.nPatientID));
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.View, "RxSaving Prescription Viewed", (long)oPatSav_Mst.nPatientID.Value, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                        BindData();
                        //40666401244066601

                    }
                    //else
                    //{
                    //    System.Windows.Forms.MessageBox.Show(" Drug match not found. Please Send Disposition message.", "gloEMR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                }
                catch (Exception ex)
                {
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.Prescription, gloAuditTrail.ActivityType.View, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                finally
                {
                    lstDrugs = null;
                    lstDrugsList = null;
                    oPatSav_Mst =null;

                    if ((oDataInsertion != null))
                    {
                        oDataInsertion = null;
                    }

                    if ((dtDrugInfo != null))
                    {
                        dtDrugInfo.Dispose();
                        dtDrugInfo = null;
                    }
                    if ((oDrugInformation != null))
                    {
                        oDrugInformation.Dispose();
                        oDrugInformation = null;
                    }

                }

            }

            private ArrayList AcceptAlternative(ArrayList lstDrugs, string _OPPID)
            {
                UC_GenericAlternative _genControl = null;
                UIElement OBJUIElement = null;
                UC_alternative _OppControl = null;
                try
                {


                    for (int i = 0; i < stkGenericOpportunity.Children.Count; i++)
                    {
                        try { OBJUIElement = stkGenericOpportunity.Children[i]; }
                        catch (Exception) { }
                        if (OBJUIElement != null && (OBJUIElement.GetType() == typeof(UC_GenericAlternative)))
                        {
                            _genControl = (UC_GenericAlternative)OBJUIElement;
                            if (_genControl.lblOppHeader.Tag.ToString() == _OPPID)
                            {
                                for (int j = 0; j < _genControl.stkGenOpportunity.Children.Count; j++)
                                {
                                    try { OBJUIElement = _genControl.stkGenOpportunity.Children[j]; }
                                    catch (Exception) { }

                                    if (OBJUIElement != null && (OBJUIElement.GetType() == typeof(UC_alternative)))
                                    {
                                        _OppControl = (UC_alternative)OBJUIElement;

                                        if (_OppControl.rbAlternate.IsChecked.Value)
                                        {
                                            lstDrugs.Add(new Drugs(Convert.ToInt64(_genControl.lblOppHeader.Tag), _genControl.lblOpportunityID.Text, _genControl.lblOpportunityType.Text, _genControl.lblQuantity1.Text, _genControl.lblDaysSupply1.Text, _OppControl.lblDrugDBCode1.Text, _OppControl.lblQualifier2.Text, _OppControl.lblDrugDescription2.Text, Convert.ToInt64(_OppControl.lblAltrHeader.Tag)));
                                            j = _genControl.stkGenOpportunity.Children.Count;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    return null;
                }
                return lstDrugs;
            }

            public void getPrescription(ArrayList arrdrug, Int64 ProviderId, Int64 m_Visitid = 0, Int64 m_Patientid = 0, Boolean ShowCarryForwardMessage = true)
            {
                EntOpensavePrescription(arrdrug, ProviderId, m_Visitid, m_Patientid, ShowCarryForwardMessage);
            }

            private Int16 GetDrugType(string DrugNDCCode)
            {
                gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest oDrugInformation = new gloEMRGeneralLibrary.gloEMRPrescription.RefillRequest();
                Int16 DrugType = -1;
                try
                {
                    DrugType = Convert.ToInt16(oDrugInformation.GetDrugInfo(DrugNDCCode));
                }
                catch //(Exception ex)
                {
                }
                return DrugType;
            }

            private void radioButton_Checked(object sender, RoutedEventArgs e)
            {
                if (rbAll.IsChecked == true)
                    _IsPendingMessage = false;
                if (rbPending.IsChecked == true)
                    _IsPendingMessage = true;
                BindData();
            }

            private void btnPrint_Click(object sender, RoutedEventArgs e)
            {
                PatSav_Mst oPatSav_Mst = objPatientSaving.ToList()[_OppCounter];
                try
                {
                    if (oPatSav_Mst != null)
                    {
                        this.Cursor = System.Windows.Input.Cursors.Wait;
                        //Cursor.Current = Cursors.WaitCursor;
                        SSRSApplication.frmSSRSViewer frmSSRS = new SSRSApplication.frmSSRSViewer();
                        frmSSRS.Conn = PatSavGeneral.ConnectionString;
                        frmSSRS.reportName = "rptPatientSaving";
                        frmSSRS.reportTitle = "Rx Savings Report";
                        frmSSRS.parameterName = "PSID";
                        frmSSRS.ParameterValue = Convert.ToString(oPatSav_Mst.PSID);

                        frmSSRS.IsgloStreamReport = true;
                        //frmSSRS.MdiParent = (System.Windows.Forms.Form)this.Window;
                        this.Cursor = System.Windows.Input.Cursors.Arrow;
                        frmSSRS.ShowDialog(frmSSRS.Parent);
                        if (frmSSRS != null)
                        {
                            frmSSRS.Dispose();
                            frmSSRS = null;
                        }
                        gloAuditTrail.gloAuditTrail.CreateAuditLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.PrintRxSaving, gloAuditTrail.ActivityType.Print, "RxSaving Printed", (long)oPatSav_Mst.nPatientID.Value, 0, 0, gloAuditTrail.ActivityOutCome.Success, gloAuditTrail.SoftwareComponent.gloEMR);
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = System.Windows.Input.Cursors.Arrow;
                    gloAuditTrail.gloAuditTrail.ExceptionLog(gloAuditTrail.ActivityModule.RxSavings, gloAuditTrail.ActivityCategory.PrintRxSaving, gloAuditTrail.ActivityType.Print, ex.ToString(), gloAuditTrail.ActivityOutCome.Failure);
                }
                finally
                {
                    if (oPatSav_Mst != null)
                    {
                        oPatSav_Mst = null;
                    }
                }
            }


      
    }
}
